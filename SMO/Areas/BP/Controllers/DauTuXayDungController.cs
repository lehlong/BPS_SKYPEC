
using Newtonsoft.Json;
using SMO.Core.Entities;
using SMO.Core.Entities.BP.DAU_TU_XAY_DUNG;
using SMO.Core.Entities.MD;
using SMO.Repository.Implement.BP;
using SMO.Repository.Implement.BP.DAU_TU_XAY_DUNG;
using SMO.Repository.Implement.MD;
using SMO.Service.BP;
using SMO.Service.BP.DAU_TU_XAY_DUNG;
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
    public class DauTuXayDungController :
        BPControllerBase<DauTuXayDungService, T_BP_DAU_TU_XAY_DUNG, DauTuXayDungRepo, T_MD_KHOAN_MUC_DAU_TU, T_BP_DAU_TU_XAY_DUNG_VERSION, T_BP_DAU_TU_XAY_DUNG_HISTORY, DauTuXayDungHistoryRepo>,
        IBPController<DauTuXayDungService, T_BP_DAU_TU_XAY_DUNG, DauTuXayDungRepo, T_MD_KHOAN_MUC_DAU_TU, T_BP_DAU_TU_XAY_DUNG_VERSION, T_BP_DAU_TU_XAY_DUNG_HISTORY, DauTuXayDungHistoryRepo>
    {
        [MyValidateAntiForgeryToken]
        public ActionResult IndexDLTH(int? year)
        {
            _service.ObjDetail.TIME_YEAR = year ?? 0;
            _service.ObjDetail.PHIEN_BAN = "PB5";
            return PartialView("Index", _service);
        }
        public override FileContentResult DownloadTemplate(string templateId, int year)
        {
            var template = _service.GetTemplate(templateId);

            MemoryStream outFileStream = new MemoryStream();

            string path = Server.MapPath("~/TemplateExcel/" + "Template_DauTuXayDung.xlsx");
            _service.GenerateTemplate(ref outFileStream, path, templateId, year);
            var fileName = template.NAME;

            return File(outFileStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName + ".xlsx");
        }

        public override ActionResult ViewTemplate(string templateId, int? version, int year, string centerCode = "")
        {
            if (!string.IsNullOrEmpty(templateId))
            {
                var dataCost = _service.PreparePureListForTemplate(out IList<T_MD_TEMPLATE_DETAIL_DAU_TU_XAY_DUNG> detailOtherCostElements, templateId, year);
                ViewBag.detailOtherCostElements = detailOtherCostElements;
                return PartialView("ViewTemplateDauTuXayDung", dataCost);
                //var isBaseTemplate = _service.GetTemplate(templateId).IS_BASE;
                //if (isBaseTemplate)
                //{
                //    return PartialView("ViewTemplateBaseDauTuXayDung", dataCost);
                //}
                //else
                //{
                //    return PartialView("ViewTemplateDauTuXayDung", dataCost);
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
            var dataCost = _service.GetDataCost(out IList<T_MD_TEMPLATE_DETAIL_DAU_TU_XAY_DUNG> detailCostElements,
                out IList<T_BP_DAU_TU_XAY_DUNG_DATA> detailCostData, out bool isDrillDownApply, model);
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
            ViewBag.costCFHeader = _service.GetHeader(model);
            model.IS_DRILL_DOWN = isDrillDownApply;
            model.EXCHANGE_RATE = 12;
            ViewBag.dataCenterModel = model;
            ViewBag.lstProject = _service.GetProject(model.TEMPLATE_CODE, model.VERSION, model.YEAR);
            if (model.PHIEN_BAN == "PB3" || model.PHIEN_BAN == "PB5" || model.PHIEN_BAN == "PB4")
            {
                _service.GetDataProject(model);
            }
            return PartialView(dataCost);
        }

        public override ActionResult SummaryCenter(string centerCode, int? year, int? version, bool isRenderPartial = false)
        {
            // cost
            var dataCost = _service.SummaryCenterOut(out IList<T_BP_DAU_TU_XAY_DUNG_DATA> detailCostElements, centerCode ?? ProfileUtilities.User.ORGANIZE_CODE, year.Value, version);
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
        public ActionResult UpdateCellValue(string templateCode, int version, int year, string type, string projectCode, string costCenter, string elementCode, string value, int?month)
        {
            var result = new TransferObject
            {
                Type = TransferType.AlertSuccessAndJsCommand
            };
            _service.UpdateCellValue(templateCode, version, year, type, projectCode, costCenter, elementCode, value, month);
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

        public ActionResult ViewDataTemplate(string templateCode, int year, int version, string orgCode, string kichBan, string phienBan)
        {
            orgCode = orgCode ?? ProfileUtilities.User.ORGANIZE_CODE;
            var viewDataCenterModel = new ViewDataCenterModel();
            if (ProfileUtilities.User.ORGANIZE_CODE == "1000")
            {
                viewDataCenterModel = new ViewDataCenterModel
                {
                    ORG_CODE = orgCode,
                    IS_LEAF = _service.IsLeaf(),
                    TEMPLATE_CODE = templateCode ?? string.Empty,
                    KICH_BAN = kichBan,
                    PHIEN_BAN = phienBan,
                    YEAR = year,
                    VERSION = version,
                    IS_HAS_NOT_VALUE = false,
                    IS_HAS_VALUE = true,
                };
            }
            else
            {
                viewDataCenterModel = new ViewDataCenterModel
                {
                    ORG_CODE = _service.CalculateOrgCode(orgCode, templateCode),
                    IS_LEAF = _service.IsLeaf(),
                    TEMPLATE_CODE = templateCode ?? string.Empty,
                    KICH_BAN = kichBan,
                    PHIEN_BAN = phienBan,
                    YEAR = year,
                    VERSION = version,
                    IS_HAS_NOT_VALUE = false,
                    IS_HAS_VALUE = true,
                };
            }

            ViewBag.currencies = _service.GetAllMasterData<CurrencyRepo, T_MD_CURRENCY>();
            return PartialView("ViewData", viewDataCenterModel);
        }

        public FileContentResult DownloadData(string modelJson)
        {
            var model = JsonConvert.DeserializeObject<ViewDataCenterModel>(modelJson);
            var template = _service.GetTemplate(model.TEMPLATE_CODE);
            var lstData = _service.GetProject(model.TEMPLATE_CODE, model.VERSION, model.YEAR);
            var lstProject = lstData.Select(x => x.DauTuXayDungProfitCenter).ToList();
            MemoryStream outFileStream = new MemoryStream();
            var fileName = "Dữ liệu kế hoạch đầu tư xây dựng";
            var templateExcel = "Template_DauTuXayDung.xlsx";
            string path = Server.MapPath("~/TemplateExcel/" + templateExcel);
            _service.GenerateData(ref outFileStream, path, model, lstData, lstProject);
            return File(outFileStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName + ".xlsx");
        }


    }
}