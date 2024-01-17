
using Newtonsoft.Json;
using SMO.Core.Entities;
using SMO.Core.Entities.BP.SUA_CHUA_LON;
using SMO.Core.Entities.MD;
using SMO.Repository.Implement.BP;
using SMO.Repository.Implement.BP.SUA_CHUA_LON;
using SMO.Service.BP;
using SMO.Service.BP.SUA_CHUA_LON;
using SMO.Service.Class;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMO.Areas.BP.Controllers
{
    [SessionState(System.Web.SessionState.SessionStateBehavior.ReadOnly)]
    public class SuaChuaLonController :
        BPControllerBase<SuaChuaLonService, T_BP_SUA_CHUA_LON, SuaChuaLonRepo, T_MD_KHOAN_MUC_SUA_CHUA, T_BP_SUA_CHUA_LON_VERSION, T_BP_SUA_CHUA_LON_HISTORY, SuaChuaLonHistoryRepo>,
        IBPController<SuaChuaLonService, T_BP_SUA_CHUA_LON, SuaChuaLonRepo, T_MD_KHOAN_MUC_SUA_CHUA, T_BP_SUA_CHUA_LON_VERSION, T_BP_SUA_CHUA_LON_HISTORY, SuaChuaLonHistoryRepo>
    {
        public override FileContentResult DownloadTemplate(string templateId, int year)
        {
            var template = _service.GetTemplate(templateId);
            //string path;
            MemoryStream outFileStream = new MemoryStream();
            //if (template.IS_BASE)
            //{
            //    path = Server.MapPath("~/TemplateExcel/" + "Template_Base_SuaChuaLon.xlsx");
            //    _service.GenerateTemplateBase(ref outFileStream, path, templateId, year);
            //}
            //else
            //{
            //    path = Server.MapPath("~/TemplateExcel/" + "Template_SuaChuaLon.xlsx");
            //    _service.GenerateTemplate(ref outFileStream, path, templateId, year);
            //}

            string path = Server.MapPath("~/TemplateExcel/" + "Template_SuaChuaLon.xlsx");
            _service.GenerateTemplate(ref outFileStream, path, templateId, year);
            var fileName = template.NAME;

            return File(outFileStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName + ".xlsx");
        }

        public override ActionResult ViewTemplate(string templateId, int? version, int year, string centerCode = "")
        {
            if (!string.IsNullOrEmpty(templateId))
            {
                var dataCost = _service.PreparePureListForTemplate(out IList<T_MD_TEMPLATE_DETAIL_SUA_CHUA_LON> detailOtherCostElements, templateId, year);
                ViewBag.detailOtherCostElements = detailOtherCostElements;
                return PartialView("ViewTemplateSuaChuaLon", dataCost);
                //var isBaseTemplate = _service.GetTemplate(templateId).IS_BASE;
                //if (isBaseTemplate)
                //{
                //    return PartialView("ViewTemplateBaseSuaChuaLon", dataCost);
                //}
                //else
                //{
                //    return PartialView("ViewTemplateSuaChuaLon", dataCost);
                //}
            }
            else
            {
                return RedirectToAction(nameof(ExportData), new { version, year, orgCode = centerCode, templateCode = string.Empty });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult SummaryDataCenter(ViewDataCenterModel model)
        {
            var dataCost = _service.GetDataCost(out IList<T_MD_TEMPLATE_DETAIL_SUA_CHUA_LON> detailCostElements,
                out IList<T_BP_SUA_CHUA_LON_DATA> detailCostData, out bool isDrillDownApply, model);

            
            if (dataCost == null)
            {
                ViewBag.dataCenterModel = model;
                return PartialView(dataCost);
            }
            dataCost = dataCost.Distinct().ToList();
            // chuyển đơn vị tiền tệ 
            if (model.EXCHANGE_RATE.HasValue && model.EXCHANGE_RATE != 1)
            {
                foreach (var data in dataCost)
                {
                    for (int i = 0; i < data.Values.Length; i++)
                    {
                        data.Values[i] = Math.Round(data.Values[i] / model.EXCHANGE_RATE.Value, 2);
                    }
                }
                if (isDrillDownApply && detailCostData != null)
                {
                    foreach (var data in detailCostData)
                    {
                        data.VALUE = Math.Round((data.VALUE ?? 0) / model.EXCHANGE_RATE.Value, 2);
                    }
                }
            }

            if (detailCostData != null)
            {
                ViewBag.detailCostElements = detailCostData;
            }
            if (detailCostElements != null)
            {
                ViewBag.detailCostElements = detailCostElements;
            }
            if(string.IsNullOrEmpty(model.TEMPLATE_CODE))
            {
                ViewBag.lstCenterCode = _service.GetSanBaySuaChua(model);
            }
            ViewBag.costCFHeader = _service.GetHeader(model);
            model.IS_DRILL_DOWN = isDrillDownApply;
            model.EXCHANGE_RATE = 12;
            ViewBag.dataCenterModel = model;
            return PartialView(dataCost);
        }

        [HttpPost]
        public FileContentResult DownloadDataSCL(string model)
        {
            var jsonModel = JsonConvert.DeserializeObject<ViewDataCenterModel>(model);
            var dataCost = _service.GetDataCost(out IList<T_MD_TEMPLATE_DETAIL_SUA_CHUA_LON> detailCostElements,
                out IList<T_BP_SUA_CHUA_LON_DATA> detailCostData, out bool isDrillDownApply, jsonModel);
            var lstSanBay = _service.GetSanBaySuaChuaWithTemplate(jsonModel);
            var template = _service.GetTemplate(jsonModel.TEMPLATE_CODE);
            MemoryStream outFileStream = new MemoryStream();
            var templateExcel = "Template_Export_SuaChua.xlsx";
            string path = Server.MapPath("~/TemplateExcel/" + templateExcel);
            _service.GenerateData(ref outFileStream, path, lstSanBay, dataCost, detailCostElements);
            var fileName = template.NAME;
            return File(outFileStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName + ".xlsx");
        }

        public override ActionResult SummaryCenter(string centerCode, int? year, int? version, bool isRenderPartial = false)
        {
            // cost
            var dataCost = _service.SummaryCenterOut(out IList<T_BP_SUA_CHUA_LON_DATA> detailCostElements, centerCode ?? ProfileUtilities.User.ORGANIZE_CODE, year.Value, version);
            ViewBag.detailCostElements = detailCostElements;
            ViewBag.Header = _service.GetHeader(string.Empty, centerCode ?? ProfileUtilities.User.ORGANIZE_CODE, year.Value, version);
            if (!isRenderPartial)
            {
                return PartialView("ViewSummaryCenterCostCF", dataCost);
            }
            else
            {
                return PartialView("_PartialViewSummaryCenterCostCF", dataCost);
            }
        }
        
        public ActionResult CheckTemplate(string template, int year, string orgCode)
        {
            var result = new TransferObject
            {
                Type = TransferType.AlertSuccessAndJsCommand
            };
            var check = _service.CheckTemplate(template, year, orgCode);
            if (_service.State)
            {
                result.Data = check;
                SMOUtilities.GetMessage("1002", _service, result);
            }
            else
            {
                result.Type = TransferType.AlertDangerAndJsCommand;
                SMOUtilities.GetMessage("1005", _service, result);
            }
            return result.ToJsonResult();
        }

        [HttpPost]
        [MyValidateAntiForgeryToken]
        public ActionResult InsertComment(string templateCode, int version, int year, string type, string sanBay, string costCenter, string elementCode, string value)
        {
            var result = new TransferObject
            {
                Type = TransferType.AlertSuccessAndJsCommand
            };
            _service.InsertComment(templateCode, version, year, type, sanBay, costCenter, elementCode, value);
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

        [HttpPost]
        [MyValidateAntiForgeryToken]
        public ActionResult GetCommentElement(string templateCode, int version, int year, string elementCode)
        {
            var data = _service.GetCommentElement(templateCode, version, year, elementCode);
            ViewBag.TemplateCode = templateCode;
            ViewBag.Version = version;
            ViewBag.Year = year;
            ViewBag.ElementCode = elementCode;
            return PartialView(data);
        }

        [HttpPost]
        [MyValidateAntiForgeryToken]
        public ActionResult AssignDepartment(string templateCode, int version, int year, string elementCode, string departmentCode)
        {
            var result = new TransferObject
            {
                Type = TransferType.AlertSuccessAndJsCommand
            };
            _service.AssignDepartment(templateCode, version, year, elementCode, departmentCode);
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

        [HttpPost]
        [MyValidateAntiForgeryToken]
        public ActionResult UnAssignDepartment(string templateCode, int version, int year, string elementCode, string departmentCode)
        {
            var result = new TransferObject
            {
                Type = TransferType.AlertSuccessAndJsCommand
            };
            _service.UnAssignDepartment(templateCode, version, year, elementCode, departmentCode);
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

        [HttpPost]
        [MyValidateAntiForgeryToken]
        public ActionResult Expertise(string templateCode, int version, int year, string elementCode)
        {
            var result = new TransferObject
            {
                Type = TransferType.AlertSuccessAndJsCommand
            };
            _service.Expertise(templateCode, version, year, elementCode);
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

        [HttpPost]
        [MyValidateAntiForgeryToken]
        public ActionResult UnExpertise(string templateCode, int version, int year, string elementCode)
        {
            var result = new TransferObject
            {
                Type = TransferType.AlertSuccessAndJsCommand
            };
            _service.UnExpertise(templateCode, version, year, elementCode);
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

        [HttpPost]
        [MyValidateAntiForgeryToken]
        public ActionResult GetAssignDepartmentElement(string templateCode, int version, int year, string elementCode)
        {
            var data = _service.GetDepartmentAssignElement(templateCode, version, year, elementCode);
            var lstCostCenter = _service.GetCostCenter();
            ViewBag.LstCostCenter = lstCostCenter;
            ViewBag.TemplateCode = templateCode;
            ViewBag.Version = version;
            ViewBag.Year = year;
            ViewBag.ElementCode = elementCode;
            return PartialView(data);
        }

        [HttpPost]
        [MyValidateAntiForgeryToken]
        public ActionResult EditCellValue(string templateCode, int version, int year, string elementCode, string sanBayCode, decimal value)
        {
            var result = new TransferObject
            {
                Type = TransferType.AlertSuccessAndJsCommand
            };
            _service.EditCellValue(templateCode, version, year, elementCode, sanBayCode, value);
            if (_service.State)
            {
                SMOUtilities.GetMessage("1002", _service, result);
            }
            else
            {
                result.Type = TransferType.AlertDanger;
                SMOUtilities.GetMessage("1005", _service, result);
            }
            return result.ToJsonResult();
        }

        [HttpPost]
        [MyValidateAntiForgeryToken]
        public ActionResult GetHistoryElement(string templateCode, int version, int year, string elementCode)
        {
            var data = _service.GetEditHistory(templateCode, version, year, elementCode);
            return PartialView(data);
        }
    }
}