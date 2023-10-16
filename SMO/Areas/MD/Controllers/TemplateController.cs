﻿using SMO.Service.MD;

using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace SMO.Areas.MD.Controllers
{
    [AuthorizeCustom(Right = "R400")]
    public class TemplateController : Controller
    {
        private readonly TemplateService _service;
        public TemplateController()
        {
            _service = new TemplateService();
        }
        // GET: MD/Template
        [MyValidateAntiForgeryToken]
        public ActionResult Index(string objectType, string budgetType, string elementType)
        {
            _service.ObjDetail.OBJECT_TYPE = objectType;
            _service.ObjDetail.BUDGET_TYPE = budgetType;
            _service.ObjDetail.ELEMENT_TYPE = elementType;

            //ViewBag.HeaderName = 
            return PartialView(_service);
        }

        [ValidateAntiForgeryToken]
        public ActionResult List(TemplateService service)
        {
            service.Search();
            return PartialView(service);
        }

        [MyValidateAntiForgeryToken]
        public ActionResult Details(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                _service.Get(id);
            }
            if (_service.ObjDetail.OBJECT_TYPE.Trim() == TemplateObjectType.SanLuong && _service.ObjDetail.ELEMENT_TYPE == ElementType.SanLuong)
            {
                return PartialView("TemplateKeHoachSanLuong", _service);
            }
            if (_service.ObjDetail.OBJECT_TYPE == TemplateObjectType.Project && _service.ObjDetail.ELEMENT_TYPE == ElementType.ChiPhi)
            {
                return PartialView("OtherBudgetProject", _service);
            }
            else
            {
                return PartialView(_service);
            }
        }

        /// <summary>
        /// get details information of center code
        /// </summary>
        /// <param name="templateId">template code</param>
        /// <param name="centerCode">cost center code or profit center code</param>
        /// <param name="type">enum Budget. Type profit center or cost center</param>
        /// <returns></returns>
        [MyValidateAntiForgeryToken]
        public JsonResult GetDetailInformation(string templateId, string centerCode, string type, int year)
        {
            var isEnum = Enum.TryParse(type, out Budget budget);
            if (!isEnum)
            {
                return Json(new List<Node>(), JsonRequestBehavior.AllowGet);
            }
            var nodeDetailElements = _service.GetNodeDetailElement(budget, centerCode, year, templateId);
            return Json(nodeDetailElements, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDetailInformationOtherElement(string templateId, string projectCode, string type, int year, string companyCode)
        {
            var isEnum = Enum.TryParse(type, out Budget budget);
            if (!isEnum)
            {
                return Json(new List<Node>(), JsonRequestBehavior.AllowGet);
            }
            var nodeDetailElements = _service.GetNodeDetailOtherElement(budget, projectCode, companyCode, year, templateId);
            return Json(nodeDetailElements, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDetailInformationKhoanMucSanLuong(string templateId, string projectCode, string type, int year, string companyCode)
        {
            var isEnum = Enum.TryParse(type, out Budget budget);
            if (!isEnum)
            {
                return Json(new List<Node>(), JsonRequestBehavior.AllowGet);
            }
            var nodeDetailElements = _service.GetNodeDetailKhoanMucSanLuong(budget, projectCode, companyCode, year, templateId);
            return Json(nodeDetailElements, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDetailInformationOtherCompany(string templateId, string type, int year, string projectCode)
        {
            var isEnum = Enum.TryParse(type, out Budget budget);
            if (!isEnum)
            {
                return Json(new List<Node>(), JsonRequestBehavior.AllowGet);
            }
            var nodeDetailElements = _service.GetNodeDetailOtherCompany(budget, projectCode, year, templateId);
            return Json(nodeDetailElements, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDetailInformationHangHangKhong(string templateId, string type, int year, string projectCode)
        {
            var isEnum = Enum.TryParse(type, out Budget budget);
            if (!isEnum)
            {
                return Json(new List<Node>(), JsonRequestBehavior.AllowGet);
            }
            var nodeDetailElements = _service.GetNodeDetailHangHangKhong(budget, projectCode, year, templateId);
            return Json(nodeDetailElements, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewTemplate(string templateId, int? version, int? year)
        {
            _service.Get(templateId);
            switch (_service.ObjDetail.OBJECT_TYPE.Trim())
            {
                case TemplateObjectType.Department:
                    switch (_service.ObjDetail.ELEMENT_TYPE)
                    {
                        // revenue
                        case ElementType.DoanhThu:
                            if (_service.ObjDetail.BUDGET_TYPE.Equals(BudgetType.KinhDoanh))
                            {
                                return RedirectToAction("ViewTemplate", "../BP/RevenuePL", new { templateId, version, year });
                            }
                            else
                            {
                                // budget c
                                return RedirectToAction("ViewTemplate", "../BP/RevenueCF", new { templateId, version, year });
                            }
                        // cost
                        case ElementType.ChiPhi:
                            if (_service.ObjDetail.BUDGET_TYPE.Equals(BudgetType.KinhDoanh))
                            {
                                return RedirectToAction("ViewTemplate", "../BP/CostPL", new { templateId, version, year });
                            }
                            else
                            {
                                // budget c
                                return RedirectToAction("ViewTemplate", "../BP/CostCF", new { templateId, version, year });
                            }
                        
                        default:
                            return HttpNotFound();
                    }
                case TemplateObjectType.DevelopProject:
                    if (_service.ObjDetail.BUDGET_TYPE.Equals(BudgetType.KinhDoanh))
                    {
                        return RedirectToAction("ViewTemplate", "../BP/ContructCostPL", new { templateId, version, year });
                    }
                    else
                    {
                        // budget c
                        return RedirectToAction("ViewTemplate", "../BP/ContructCostCF", new { templateId, version, year });
                    }
                case TemplateObjectType.Project:
                    if (_service.ObjDetail.BUDGET_TYPE.Equals(BudgetType.KinhDoanh))
                    {
                        if (_service.ObjDetail.ELEMENT_TYPE == ElementType.ChiPhi)
                        {
                            return RedirectToAction("ViewTemplate", "../BP/OtherCostPL", new { templateId, version, year });
                        }
                        else
                        {
                            return RedirectToAction("ViewTemplate", "../BP/RevenuePL", new { templateId, version, year });
                        }
                    }
                    else
                    {
                        if (_service.ObjDetail.ELEMENT_TYPE == ElementType.DoanhThu)
                        {
                            return RedirectToAction("ViewTemplate", "../BP/RevenueCF", new { templateId, version, year });
                        }
                        else
                        {
                            return RedirectToAction("ViewTemplate", "../BP/OtherCostCF", new { templateId, version, year });
                        }
                        // budget c
                    }

                case TemplateObjectType.SanLuong:

                    return RedirectToAction("ViewTemplate", "../BP/KeHoachSanLuong", new { templateId, version, year });

                default:
                    return HttpNotFound();
            }
        }

        // /MD/Template/DifferentVersion?templateId=CP0001&yearSource=2019&versionSource=2&versionCompare=1&yearCompare=2019
        public ActionResult DifferentVersion(string templateId, int? versionSource, int? versionCompare, int? yearSource, int? yearCompare)
        {
            _service.Get(templateId);
            switch (_service.ObjDetail.ELEMENT_TYPE)
            {
                // revenue
                case ElementType.DoanhThu:
                    return RedirectToAction("DifferentVersion", "ProfitCenter", new { templateId, versionSource, versionCompare, yearSource, yearCompare });
                // cost
                case ElementType.ChiPhi:
                    return RedirectToAction("DifferentVersion", "CostCenter", new { templateId, versionSource, versionCompare, yearSource, yearCompare });
                default:
                    return HttpNotFound();
            }
        }

        //[MyValidateAntiForgeryToken]
        //[HttpGet]
        public ActionResult DownloadTemplate(string templateId, int? year)
        {
            _service.Get(templateId);
            year = year ?? DateTime.Now.Year;
            switch (_service.ObjDetail.OBJECT_TYPE.Trim())
            {
                case TemplateObjectType.Department:
                    switch (_service.ObjDetail.ELEMENT_TYPE)
                    {
                        // revenue
                        case ElementType.DoanhThu:
                            if (_service.ObjDetail.BUDGET_TYPE.Equals(BudgetType.KinhDoanh))
                            {
                                return RedirectToAction("DownloadTemplate", "../BP/RevenuePL", new { templateId, year });
                            }
                            else
                            {
                                // budget c
                                return RedirectToAction("DownloadTemplate", "../BP/RevenueCF", new { templateId, year });
                            }
                        case ElementType.ChiPhi:
                            if (_service.ObjDetail.BUDGET_TYPE.Equals(BudgetType.KinhDoanh))
                            {
                                return RedirectToAction("DownloadTemplate", "../BP/CostPL", new { templateId, year });
                            }
                            else
                            {
                                // budget c
                                return RedirectToAction("DownloadTemplate", "../BP/CostCF", new { templateId, year });
                            }
                        default:
                            return HttpNotFound();
                    }
                case TemplateObjectType.DevelopProject:
                    if (_service.ObjDetail.BUDGET_TYPE.Equals(BudgetType.KinhDoanh))
                    {
                        return RedirectToAction("DownloadTemplate", "../BP/ContructCostPL", new { templateId, year });
                    }
                    else
                    {
                        // budget c
                        return RedirectToAction("DownloadTemplate", "../BP/ContructCostCF", new { templateId, year });
                    }
                case TemplateObjectType.Project:
                    if (_service.ObjDetail.BUDGET_TYPE.Equals(BudgetType.KinhDoanh))
                    {
                        if (_service.ObjDetail.ELEMENT_TYPE == ElementType.ChiPhi)
                        {
                            return RedirectToAction("DownloadTemplate", "../BP/OtherCostPL", new { templateId, year });
                        }
                        else
                        {
                            return RedirectToAction("DownloadTemplate", "../BP/RevenuePL", new { templateId, year });
                        }
                    }
                    else
                    {
                        if (_service.ObjDetail.ELEMENT_TYPE == ElementType.DoanhThu)
                        {
                            return RedirectToAction("DownloadTemplate", "../BP/RevenueCF", new { templateId, year });
                        }
                        else
                        {
                            return RedirectToAction("DownloadTemplate", "../BP/OtherCostCF", new { templateId, year });
                        }
                        // budget c
                    }

                case TemplateObjectType.SanLuong:
                    return RedirectToAction("DownloadTemplate", "../BP/KeHoachSanLuong", new { templateId, year });
                default:
                    return HttpNotFound();
            }

        }

        [MyValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult UpdateDetailInformation(string centerCode, string centerType, string template, int year, IList<string> detailCodes)
        {
            var result = new TransferObject
            {
                Type = TransferType.AlertSuccessAndJsCommand
            };
            var isEnum = Enum.TryParse(centerType, out Budget budget);
            if (!isEnum)
            {
                _service.Exception = new FormatException("Type of center not support");
                _service.ErrorMessage = "Type of center not support";
                _service.State = false;
                result.Type = TransferType.AlertDanger;
                SMOUtilities.GetMessage("1005", _service, result);
                return result.ToJsonResult();
            }
            _service.UpdateDetailInformation(centerCode, template, year, detailCodes, budget);
            if (_service.State)
            {
                SMOUtilities.GetMessage("1002", _service, result);
                result.ExtData = "SubmitIndex();";
            }
            else
            {
                result.Type = TransferType.AlertDanger;
                SMOUtilities.GetMessage("1005", _service, result);
            }
            return result.ToJsonResult();
        }

        [MyValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult UpdateDetailInformationOther(string projectCode, string companyCode, string centerType, string template, int year, IList<string> detailCodes)
        {
            var result = new TransferObject
            {
                Type = TransferType.AlertSuccessAndJsCommand
            };
            var isEnum = Enum.TryParse(centerType, out Budget budget);
            if (!isEnum)
            {
                _service.Exception = new FormatException("Type of center not support");
                _service.ErrorMessage = "Type of center not support";
                _service.State = false;
                result.Type = TransferType.AlertDanger;
                SMOUtilities.GetMessage("1005", _service, result);
                return result.ToJsonResult();
            }
            _service.UpdateDetailInformationOther(projectCode, companyCode, template, year, detailCodes, budget);
            if (_service.State)
            {
                SMOUtilities.GetMessage("1002", _service, result);
                result.ExtData = "SubmitIndex();";
            }
            else
            {
                result.Type = TransferType.AlertDanger;
                SMOUtilities.GetMessage("1005", _service, result);
            }
            return result.ToJsonResult();
        }


        [MyValidateAntiForgeryToken]
        public ActionResult BuildTree(string templateId, string type, int year)
        {
            var isEnum = Enum.TryParse(type, out TemplateType templateType);
            if (!isEnum)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _service.Get(templateId);
            switch (templateType)
            {
                case TemplateType.CENTER:
                    switch (_service.ObjDetail.OBJECT_TYPE.Trim())
                    {
                        case TemplateObjectType.Department:
                            if (_service.ObjDetail.ELEMENT_TYPE.Equals(ElementType.DoanhThu))
                            {
                                return RedirectToAction("BuildTreeByTemplate", "ProfitCenter", new { templateId, year });
                            }
                            else
                            {
                                return RedirectToAction("BuildTreeByTemplate", "CostCenter", new { templateId, year });
                            }
                        case TemplateObjectType.DevelopProject:
                            return RedirectToAction("BuildTreeByTemplate", "InternalOrder", new { templateId, year });
                        case TemplateObjectType.SanLuong:
                            return RedirectToAction("BuildTreeByTemplate", "SanLuongProfitCenter", new { templateId, year });
                        case TemplateObjectType.Project:
                            if (_service.ObjDetail.ELEMENT_TYPE == ElementType.ChiPhi)
                            {
                                return RedirectToAction("BuildTreeByTemplate", "OtherProfitCenter", new { templateId, year });
                            }
                            else
                            {
                                return RedirectToAction("BuildTreeByTemplate", "ProfitCenter", new { templateId, year });
                            }
                        default:
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                case TemplateType.ELEMENT:
                    if (_service.ObjDetail.ELEMENT_TYPE.Equals(ElementType.DoanhThu))
                    {
                        if (_service.ObjDetail.BUDGET_TYPE.Equals(BudgetType.KinhDoanh))
                        {
                            return RedirectToAction("BuildTreeByTemplate", "RevenuePLElement", new { year });
                        }
                        else
                        {
                            // budget c
                            return RedirectToAction("BuildTreeByTemplate", "RevenueCFElement", new { year });
                        }
                    }
                    else if (_service.ObjDetail.ELEMENT_TYPE.Equals(ElementType.SanLuong))
                    {
                        return RedirectToAction("BuildTreeByTemplate", "KhoanMucSanLuong", new { year });
                    }
                    else
                    {
                        if (_service.ObjDetail.BUDGET_TYPE.Equals(BudgetType.KinhDoanh))
                        {
                            return RedirectToAction("BuildTreeByTemplate", "CostPLElement", new { year });
                        }
                        else
                        {
                            return RedirectToAction("BuildTreeByTemplate", "CostCFElement", new { year });
                        }
                    }
                default:
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [MyValidateAntiForgeryToken]
        public ActionResult BuildTreeKeHoachSanLuong(string templateId, string type, int year)
        {
            var isEnum = Enum.TryParse(type, out TemplateType templateType);
            if (!isEnum)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _service.Get(templateId);

            if (templateType == TemplateType.CENTER)
            {
                return RedirectToAction("BuildTreeByTemplate", "SanLuongProfitCenter", new { templateId, year });

            }
            else if (templateType == TemplateType.ELEMENT)
            {
                return RedirectToAction("BuildTreeByTemplate", "KhoanMucSanLuong", new { year });
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // GET: MD/Template/Create
        [MyValidateAntiForgeryToken]
        public ActionResult Create(string objectType, string budgetType, string elementType)
        {
            _service.ObjDetail.ORG_CODE = ProfileUtilities.User.ORGANIZE_CODE;
            _service.ObjDetail.OBJECT_TYPE = objectType;
            _service.ObjDetail.BUDGET_TYPE = budgetType;
            _service.ObjDetail.ELEMENT_TYPE = elementType;
            _service.ObjDetail.CODE = "BM" + _service.GetSequence("TEMPLATE");

            return PartialView(_service);
        }

        // POST: MD/Template/Create
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(TemplateService service)
        {
            var result = new TransferObject
            {
                Type = TransferType.AlertSuccessAndJsCommand
            };
            service.Create();
            if (service.State)
            {
                SMOUtilities.GetMessage("1001", service, result);
                result.ExtData = $"SubmitIndex(); " +
                    $"Forms.Close('{service.ViewId}'); " +
                    $"Forms.LoadAjax('{Url.Action(nameof(Details), new { id = service.ObjDetail.CODE })}');";
            }
            else
            {
                result.Type = TransferType.AlertDanger;
                SMOUtilities.GetMessage("1004", service, result);
            }
            return result.ToJsonResult();
        }

        // GET: MD/Template/Edit/5
        [MyValidateAntiForgeryToken]
        public ActionResult Edit(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                _service.Get(id);
            }
            return PartialView(_service);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Update(TemplateService service)
        {
            var result = new TransferObject
            {
                Type = TransferType.AlertSuccessAndJsCommand
            };
            service.Update();
            if (service.State)
            {
                SMOUtilities.GetMessage("1002", service, result);
                result.ExtData = "SubmitIndex();";
            }
            else
            {
                result.Type = TransferType.AlertDanger;
                SMOUtilities.GetMessage("1005", service, result);
            }
            return result.ToJsonResult();
        }

        // POST: MD/Template/Delete/5
        [HttpPost]
        [MyValidateAntiForgeryToken]
        public ActionResult Delete(string pStrListSelected)
        {
            var result = new TransferObject
            {
                Type = TransferType.AlertSuccessAndJsCommand
            };
            _service.Delete(pStrListSelected);
            if (_service.State)
            {
                SMOUtilities.GetMessage("1003", _service, result);
                result.ExtData = "SubmitIndex();";
            }
            else
            {
                result.Type = TransferType.AlertDanger;
                SMOUtilities.GetMessage("1006", _service, result);
            }
            return result.ToJsonResult();
        }

        public ActionResult THMCenter()
        {
            return PartialView();
        }

        [HttpPost]
        [MyValidateAntiForgeryToken]
        public ActionResult ToggleStatusTemplate(string templateId, bool currentStatus)
        {
            var result = new TransferObject
            {
                Type = TransferType.AlertSuccessAndJsCommand
            };
            _service.ToggleStatusTemplate(templateId, currentStatus);
            if (_service.State)
            {
                SMOUtilities.GetMessage("1002", _service, result);
                result.ExtData = "SubmitIndex();";
            }
            else
            {
                result.Type = TransferType.AlertDangerAndJsCommand;
                SMOUtilities.GetMessage("1005", _service, result);
                result.ExtData = "SubmitIndex();";
            }
            return result.ToJsonResult();
        }

        [HttpPost]
        [MyValidateAntiForgeryToken]
        public ActionResult CopyTemplate(int sourceYear, int destinationYear, string templateId)
        {
            var result = new TransferObject
            {
                Type = TransferType.AlertSuccessAndJsCommand
            };
            _service.CopyTemplate(sourceYear, destinationYear, templateId);
            if (_service.State)
            {
                SMOUtilities.GetMessage("1001", _service, result);
                result.ExtData = $"ChangeYear({destinationYear})";
            }
            else
            {
                result.Type = TransferType.AlertDanger;
                SMOUtilities.GetMessage("1004", _service, result);
            }
            return result.ToJsonResult();
        }
    }
}
