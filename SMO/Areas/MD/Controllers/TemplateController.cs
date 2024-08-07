﻿using SMO.Service.MD;

using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace SMO.Areas.MD.Controllers
{
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
            else if (_service.ObjDetail.OBJECT_TYPE.Trim() == TemplateObjectType.DoanhThu && _service.ObjDetail.ELEMENT_TYPE == ElementType.DoanhThu)
            {
                return PartialView("TemplateKeHoachDoanhThu", _service);
            }
            else if (_service.ObjDetail.OBJECT_TYPE.Trim() == TemplateObjectType.ChiPhi && _service.ObjDetail.ELEMENT_TYPE == ElementType.ChiPhi)
            {
                return PartialView("TemplateKeHoachChiPhi", _service);
            }
            else if (_service.ObjDetail.OBJECT_TYPE.Trim() == TemplateObjectType.DauTuXayDung && _service.ObjDetail.ELEMENT_TYPE == ElementType.DauTuXayDung)
            {
                return PartialView("TemplateDauTuXayDung", _service);
            }
            else if (_service.ObjDetail.OBJECT_TYPE.Trim() == TemplateObjectType.DauTuTrangThietBi && _service.ObjDetail.ELEMENT_TYPE == ElementType.DauTuTrangThietBi)
            {
                return PartialView("TemplateDauTuTrangThietBi", _service);
            }
            else if (_service.ObjDetail.OBJECT_TYPE.Trim() == TemplateObjectType.DauTuNgoaiDoanhNghiep && _service.ObjDetail.ELEMENT_TYPE == ElementType.DauTuNgoaiDoanhNghiep)
            {
                return PartialView("TemplateDauTuNgoaiDoanhNghiep", _service);
            }
            else if (_service.ObjDetail.OBJECT_TYPE.Trim() == TemplateObjectType.VanChuyen && _service.ObjDetail.ELEMENT_TYPE == ElementType.VanChuyen)
            {
                return PartialView("TemplateKeHoachVanChuyen", _service);
            }
            else if (_service.ObjDetail.OBJECT_TYPE.Trim() == TemplateObjectType.SuaChuaLon && _service.ObjDetail.ELEMENT_TYPE == ElementType.SuaChuaLon)
            {
                return PartialView("TemplateSuaChuaLon", _service);
            }
            else if (_service.ObjDetail.OBJECT_TYPE.Trim() == TemplateObjectType.SuaChuaThuongXuyen && _service.ObjDetail.ELEMENT_TYPE == ElementType.SuaChuaThuongXuyen)
            {
                return PartialView("TemplateSuaChuaThuongXuyen", _service);
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
        public JsonResult GetDetailInformationKhoanMucSuaChua(string templateId, string projectCode, string type, int year, string companyCode)
        {
            var isEnum = Enum.TryParse(type, out Budget budget);
            if (!isEnum)
            {
                return Json(new List<Node>(), JsonRequestBehavior.AllowGet);
            }
            var nodeDetailElements = _service.GetNodeDetailKhoanMucSuaChua(budget, projectCode, companyCode, year, templateId);
            return Json(nodeDetailElements, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDetailInformationKhoanMucHangHoa(string templateId, string projectCode, string type, int year, string companyCode)
        {
            var isEnum = Enum.TryParse(type, out Budget budget);
            if (!isEnum)
            {
                return Json(new List<Node>(), JsonRequestBehavior.AllowGet);
            }
            var nodeDetailElements = _service.GetNodeDetailKhoanMucHangHoa(budget, projectCode, companyCode, year, templateId);
            return Json(nodeDetailElements, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDetailInformationKhoanMucSuaChuaThuongXuyen(string templateId, string projectCode, string type, int year, string companyCode)
        {
            var isEnum = Enum.TryParse(type, out Budget budget);
            if (!isEnum)
            {
                return Json(new List<Node>(), JsonRequestBehavior.AllowGet);
            }
            var nodeDetailElements = _service.GetNodeDetailKhoanMucSuaChuaThuongXuyen(budget, projectCode, companyCode, year, templateId);
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
        public JsonResult GetDetailInformationKhoanMucChiPhi(string templateId, string projectCode, string type, int year, string companyCode)
        {
            var isEnum = Enum.TryParse(type, out Budget budget);
            if (!isEnum)
            {
                return Json(new List<Node>(), JsonRequestBehavior.AllowGet);
            }
            var nodeDetailElements = _service.GetNodeDetailKhoanMucHangHoa(budget, projectCode, companyCode, year, templateId);
            return Json(nodeDetailElements, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDetailInformationKhoanMucDauTuXayDung(string templateId, string projectCode, string type, int year, string companyCode)
        {
            var isEnum = Enum.TryParse(type, out Budget budget);
            if (!isEnum)
            {
                return Json(new List<Node>(), JsonRequestBehavior.AllowGet);
            }
            var nodeDetailElements = _service.GetNodeDetailKhoanMucDauTuXayDung(budget, projectCode, companyCode, year, templateId);
            return Json(nodeDetailElements, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDetailInformationKhoanMucVanChuyen(string templateId, string projectCode, string type, int year, string companyCode)
        {
            var isEnum = Enum.TryParse(type, out Budget budget);
            if (!isEnum)
            {
                return Json(new List<Node>(), JsonRequestBehavior.AllowGet);
            }
            var nodeDetailElements = _service.GetNodeDetailKhoanMucVanChuyen(budget, projectCode, companyCode, year, templateId);
            return Json(nodeDetailElements, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDetailInformationKhoanMucDoanhThu(string templateId, string projectCode, string type, int year, string companyCode)
        {
            var isEnum = Enum.TryParse(type, out Budget budget);
            if (!isEnum)
            {
                return Json(new List<Node>(), JsonRequestBehavior.AllowGet);
            }
            var nodeDetailElements = _service.GetNodeDetailKhoanMucDoanhThu(budget, projectCode, companyCode, year, templateId);
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

        public JsonResult GetDetailInformationSanBay(string templateId, string type, int year, string projectCode)
        {
            var isEnum = Enum.TryParse(type, out Budget budget);
            if (!isEnum)
            {
                return Json(new List<Node>(), JsonRequestBehavior.AllowGet);
            }
            var nodeDetailElements = _service.GetNodeDetailSanBay(budget, projectCode, year, templateId);
            return Json(nodeDetailElements, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDetailInformationCompany(string templateId, string type, int year, string projectCode)
        {
            var isEnum = Enum.TryParse(type, out Budget budget);
            if (!isEnum)
            {
                return Json(new List<Node>(), JsonRequestBehavior.AllowGet);
            }
            var nodeDetailElements = _service.GetNodeDetailCompany(budget, projectCode, year, templateId);
            return Json(nodeDetailElements, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewTemplate(string templateId, int? version, int? year)
        {
            _service.Get(templateId);
            switch (_service.ObjDetail.OBJECT_TYPE.Trim())
            {
                case TemplateObjectType.SanLuong:
                    return RedirectToAction("ViewTemplate", "../BP/KeHoachSanLuong", new { templateId, version, year });
                case TemplateObjectType.DoanhThu:
                    return RedirectToAction("ViewTemplate", "../BP/KeHoachDoanhThu", new { templateId, version, year });
                case TemplateObjectType.ChiPhi:
                    return RedirectToAction("ViewTemplate", "../BP/KeHoachChiPhi", new { templateId, version, year });
                case TemplateObjectType.DauTuXayDung:
                    return RedirectToAction("ViewTemplate", "../BP/DauTuXayDung", new { templateId, version, year });
                case TemplateObjectType.DauTuTrangThietBi:
                    return RedirectToAction("ViewTemplate", "../BP/DauTuTrangThietBi", new { templateId, version, year });
                case TemplateObjectType.DauTuNgoaiDoanhNghiep:
                    return RedirectToAction("ViewTemplate", "../BP/DauTuNgoaiDoanhNghiep", new { templateId, version, year });
                case TemplateObjectType.VanChuyen:
                    return RedirectToAction("ViewTemplate", "../BP/KeHoachVanChuyen", new { templateId, version, year });
                case TemplateObjectType.SuaChuaLon:
                    return RedirectToAction("ViewTemplate", "../BP/SuaChuaLon", new { templateId, version, year });
                case TemplateObjectType.SuaChuaThuongXuyen:
                    return RedirectToAction("ViewTemplate", "../BP/SuaChuaThuongXuyen", new { templateId, version, year });
                default:
                    return HttpNotFound();
            }
        }

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
                case TemplateObjectType.SanLuong:
                    return RedirectToAction("DownloadTemplate", "../BP/KeHoachSanLuong", new { templateId, year });
                case TemplateObjectType.DoanhThu:
                    return RedirectToAction("DownloadTemplate", "../BP/KeHoachDoanhThu", new { templateId, year });
                case TemplateObjectType.ChiPhi:
                    return RedirectToAction("DownloadTemplate", "../BP/KeHoachChiPhi", new { templateId, year });
                case TemplateObjectType.DauTuXayDung:
                    return RedirectToAction("DownloadTemplate", "../BP/DauTuXayDung", new { templateId, year });
                case TemplateObjectType.DauTuTrangThietBi:
                    return RedirectToAction("DownloadTemplate", "../BP/DauTuTrangThietBi", new { templateId, year });
                case TemplateObjectType.DauTuNgoaiDoanhNghiep:
                    return RedirectToAction("DownloadTemplate", "../BP/DauTuNgoaiDoanhNghiep", new { templateId, year });
                case TemplateObjectType.VanChuyen:
                    return RedirectToAction("DownloadTemplate", "../BP/KeHoachVanChuyen", new { templateId, year });
                case TemplateObjectType.SuaChuaLon:
                    return RedirectToAction("DownloadTemplate", "../BP/SuaChuaLon", new { templateId, year });
                case TemplateObjectType.SuaChuaThuongXuyen:
                    return RedirectToAction("DownloadTemplate", "../BP/SuaChuaThuongXuyen", new { templateId, year });

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
        [HttpPost]
        public ActionResult GenFullTemplateSanLuong(string template, int year)
        {
            var result = new TransferObject
            {
                Type = TransferType.AlertSuccessAndJsCommand
            };
            _service.GenFullTemplateSanLuong(template, year);
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
                        case TemplateObjectType.SanLuong:
                            return RedirectToAction("BuildTreeByTemplate", "SanLuongProfitCenter", new { templateId, year });
                        case TemplateObjectType.DoanhThu:
                            return RedirectToAction("BuildTreeByTemplate", "DoanhThuProfitCenter", new { templateId, year });
                        case TemplateObjectType.ChiPhi:
                            return RedirectToAction("BuildTreeByTemplate", "ChiPhiProfitCenter", new { templateId, year });
                        case TemplateObjectType.DauTuXayDung:
                            return RedirectToAction("BuildTreeByTemplate", "DauTuXayDungProfitCenter", new { templateId, year });
                        case TemplateObjectType.DauTuTrangThietBi:
                            return RedirectToAction("BuildTreeByTemplate", "DauTuTrangThietBiProfitCenter", new { templateId, year });
                        case TemplateObjectType.DauTuNgoaiDoanhNghiep:
                            return RedirectToAction("BuildTreeByTemplate", "DauTuNgoaiDoanhNghiepProfitCenter", new { templateId, year });
                        case TemplateObjectType.VanChuyen:
                            return RedirectToAction("BuildTreeByTemplate", "VanChuyenProfitCenter", new { templateId, year });
                        case TemplateObjectType.SuaChuaLon:
                            return RedirectToAction("BuildTreeByTemplate", "SuaChuaProfitCenter", new { templateId, year });
                        case TemplateObjectType.SuaChuaThuongXuyen:
                            return RedirectToAction("BuildTreeByTemplate", "SuaChuaThuongXuyenProfitCenter", new { templateId, year });

                        default:
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                case TemplateType.ELEMENT:
                    if (_service.ObjDetail.ELEMENT_TYPE.Equals(ElementType.DoanhThu))
                    {
                        return RedirectToAction("BuildTreeByTemplate", "KhoanMucDoanhThu", new { year });
                    }
                    else if (_service.ObjDetail.ELEMENT_TYPE.Equals(ElementType.SanLuong))
                    {
                        return RedirectToAction("BuildTreeByTemplate", "KhoanMucSanLuong", new { year });
                    }
                    else if (_service.ObjDetail.ELEMENT_TYPE.Equals(ElementType.ChiPhi))
                    {
                        return RedirectToAction("BuildTreeByTemplate", "KhoanMucHangHoa", new { year });
                    }
                    else if (_service.ObjDetail.ELEMENT_TYPE.Equals(ElementType.DauTuXayDung))
                    {
                        return RedirectToAction("BuildTreeByTemplate", "KhoanMucDauTu", new { year });
                    }
                    else if (_service.ObjDetail.ELEMENT_TYPE.Equals(ElementType.DauTuTrangThietBi))
                    {
                        return RedirectToAction("BuildTreeByTemplate", "KhoanMucDauTu", new { year });
                    }
                    else if (_service.ObjDetail.ELEMENT_TYPE.Equals(ElementType.DauTuNgoaiDoanhNghiep))
                    {
                        return RedirectToAction("BuildTreeByTemplate", "KhoanMucDauTu", new { year });
                    }
                    else if (_service.ObjDetail.ELEMENT_TYPE.Equals(ElementType.VanChuyen))
                    {
                        return RedirectToAction("BuildTreeByTemplate", "KhoanMucVanChuyen", new { year });
                    }
                    else if (_service.ObjDetail.ELEMENT_TYPE.Equals(ElementType.SuaChuaLon))
                    {
                        return RedirectToAction("BuildTreeByTemplate", "KhoanMucSuaChua", new { year });
                    }
                    else if (_service.ObjDetail.ELEMENT_TYPE.Equals(ElementType.SuaChuaThuongXuyen))
                    {
                        return RedirectToAction("BuildTreeByTemplate", "KhoanMucSuaChua", new { year });
                    }
                    else
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
        [MyValidateAntiForgeryToken]
        public ActionResult BuildTreeKeHoachChiPhi(string templateId, string type, int year)
        {
            var isEnum = Enum.TryParse(type, out TemplateType templateType);
            if (!isEnum)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _service.Get(templateId);

            if (templateType == TemplateType.CENTER)
            {
                return RedirectToAction("BuildTreeByTemplate", "ChiPhiProfitCenter", new { templateId, year });

            }
            else if (templateType == TemplateType.ELEMENT)
            {
                return RedirectToAction("BuildTreeByTemplate", "KhoanMucHangHoa", new { year });
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
        [MyValidateAntiForgeryToken]
        public ActionResult BuildTreeSuaChuaLon(string templateId, string type, int year)
        {
            var isEnum = Enum.TryParse(type, out TemplateType templateType);
            if (!isEnum)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _service.Get(templateId);

            if (templateType == TemplateType.CENTER)
            {
                return RedirectToAction("BuildTreeByTemplate", "SuaChuaProfitCenter", new { templateId, year });

            }
            else if (templateType == TemplateType.ELEMENT)
            {
                return RedirectToAction("BuildTreeByTemplate", "KhoanMucSuaChua", new { year });
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [MyValidateAntiForgeryToken]
        public ActionResult BuildTreeSuaChuaThuongXuyen(string templateId, string type, int year)
        {
            var isEnum = Enum.TryParse(type, out TemplateType templateType);
            if (!isEnum)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _service.Get(templateId);

            if (templateType == TemplateType.CENTER)
            {
                return RedirectToAction("BuildTreeByTemplate", "SuaChuaThuongXuyenProfitCenter", new { templateId, year });

            }
            else if (templateType == TemplateType.ELEMENT)
            {
                return RedirectToAction("BuildTreeByTemplate", "KhoanMucSuaChua", new { year });
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [MyValidateAntiForgeryToken]
        public ActionResult BuildTreeKeHoachDoanhThu(string templateId, string type, int year)
        {
            var isEnum = Enum.TryParse(type, out TemplateType templateType);
            if (!isEnum)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _service.Get(templateId);

            if (templateType == TemplateType.CENTER)
            {
                return RedirectToAction("BuildTreeByTemplate", "DoanhThuProfitCenter", new { templateId, year });

            }
            else if (templateType == TemplateType.ELEMENT)
            {
                return RedirectToAction("BuildTreeByTemplate", "KhoanMucDoanhThu", new { year });
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        public ActionResult ViewDataDauTu(string templateId, string type, int year)
        {
            _service.Get(templateId);
            _service.TIME_YEAR = year;
            var data = _service.GetDataProject(templateId,type, year);
            _service.lstProject = data;
            return PartialView(_service);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SaveTemplate(TemplateService service,List<string> selectedCodes)
        {
            var result = new TransferObject
            {
                Type = TransferType.AlertSuccessAndJsCommand
            };
            if(selectedCodes == null || selectedCodes.Count == 0)
            {
                service.State = false;
                service.ErrorMessage = "Bạn chưa chọn dự án nào!";
                result.Type = TransferType.AlertDanger;
                SMOUtilities.GetMessage("1005", service, result);
            }
            _service.ObjDetail = service.ObjDetail;
            _service.TIME_YEAR = service.TIME_YEAR;
            _service.SaveTemplate(selectedCodes);
            if (_service.State)
            {
                SMOUtilities.GetMessage("1002", service, result);
            }
            return result.ToJsonResult();
        }

        [MyValidateAntiForgeryToken]
        public ActionResult BuildTreeDauTuXayDung(string templateId, string type, int year)
        {
            var isEnum = Enum.TryParse(type, out TemplateType templateType);
            if (!isEnum)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _service.Get(templateId);

            if (templateType == TemplateType.CENTER)
            {
                return RedirectToAction("BuildTreeByTemplate", "DauTuXayDungProfitCenter", new { templateId, year });

            }
            else if (templateType == TemplateType.ELEMENT)
            {
                return RedirectToAction("BuildTreeByTemplate", "KhoanMucDauTu", new { year });
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [MyValidateAntiForgeryToken]
        public ActionResult BuildTreeKeHoachVanChuyen(string templateId, string type, int year)
        {
            var isEnum = Enum.TryParse(type, out TemplateType templateType);
            if (!isEnum)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _service.Get(templateId);

            if (templateType == TemplateType.CENTER)
            {
                return RedirectToAction("BuildTreeByTemplate", "VanChuyenProfitCenter", new { templateId, year });

            }
            else if (templateType == TemplateType.ELEMENT)
            {
                return RedirectToAction("BuildTreeByTemplate", "KhoanMucVanChuyen", new { year });
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [MyValidateAntiForgeryToken]
        public ActionResult BuildTreeDauTuTrangThietBi(string templateId, string type, int year)
        {
            var isEnum = Enum.TryParse(type, out TemplateType templateType);
            if (!isEnum)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _service.Get(templateId);

            if (templateType == TemplateType.CENTER)
            {
                return RedirectToAction("BuildTreeByTemplate", "DauTuTrangThietBiProfitCenter", new { templateId, year });

            }
            else if (templateType == TemplateType.ELEMENT)
            {
                return RedirectToAction("BuildTreeByTemplate", "KhoanMucDauTu", new { year });
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [MyValidateAntiForgeryToken]
        public ActionResult BuildTreeDauTuNgoaiDoanhNghiep(string templateId, string type, int year)
        {
            var isEnum = Enum.TryParse(type, out TemplateType templateType);
            if (!isEnum)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _service.Get(templateId);

            if (templateType == TemplateType.CENTER)
            {
                return RedirectToAction("BuildTreeByTemplate", "DauTuNgoaiDoanhNghiepProfitCenter", new { templateId, year });

            }
            else if (templateType == TemplateType.ELEMENT)
            {
                return RedirectToAction("BuildTreeByTemplate", "KhoanMucDauTu", new { year });
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
