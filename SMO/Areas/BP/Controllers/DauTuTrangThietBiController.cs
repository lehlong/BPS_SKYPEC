
using SMO.Core.Entities;
using SMO.Core.Entities.BP.DAU_TU_TRANG_THIET_BI;
using SMO.Core.Entities.MD;
using SMO.Repository.Implement.BP;
using SMO.Repository.Implement.BP.DAU_TU_TRANG_THIET_BI;
using SMO.Service.BP;
using SMO.Service.BP.DAU_TU_TRANG_THIET_BI;
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
    public class DauTuTrangThietBiController :
        BPControllerBase<DauTuTrangThietBiService, T_BP_DAU_TU_TRANG_THIET_BI, DauTuTrangThietBiRepo, T_MD_KHOAN_MUC_DAU_TU, T_BP_DAU_TU_TRANG_THIET_BI_VERSION, T_BP_DAU_TU_TRANG_THIET_BI_HISTORY, DauTuTrangThietBiHistoryRepo>,
        IBPController<DauTuTrangThietBiService, T_BP_DAU_TU_TRANG_THIET_BI, DauTuTrangThietBiRepo, T_MD_KHOAN_MUC_DAU_TU, T_BP_DAU_TU_TRANG_THIET_BI_VERSION, T_BP_DAU_TU_TRANG_THIET_BI_HISTORY, DauTuTrangThietBiHistoryRepo>
    {
        public override FileContentResult DownloadTemplate(string templateId, int year)
        {
            var template = _service.GetTemplate(templateId);

            MemoryStream outFileStream = new MemoryStream();

            string path = Server.MapPath("~/TemplateExcel/" + "Template_DauTuTrangThietBi.xlsx");
            _service.GenerateTemplate(ref outFileStream, path, templateId, year);
            var fileName = template.NAME;

            return File(outFileStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName + ".xlsx");
        }

        public override ActionResult ViewTemplate(string templateId, int? version, int year, string centerCode = "")
        {
            if (!string.IsNullOrEmpty(templateId))
            {
                var dataCost = _service.PreparePureListForTemplate(out IList<T_MD_TEMPLATE_DETAIL_DAU_TU_TRANG_THIET_BI> detailOtherCostElements, templateId, year);
                ViewBag.detailOtherCostElements = detailOtherCostElements;
                return PartialView("ViewTemplateDauTuTrangThietBi", dataCost);
                //var isBaseTemplate = _service.GetTemplate(templateId).IS_BASE;
                //if (isBaseTemplate)
                //{
                //    return PartialView("ViewTemplateBaseDauTuTrangThietBi", dataCost);
                //}
                //else
                //{
                //    return PartialView("ViewTemplateDauTuTrangThietBi", dataCost);
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
            var dataCost = _service.GetDataCost(out IList<T_MD_TEMPLATE_DETAIL_DAU_TU_TRANG_THIET_BI> detailCostElements,
                out IList<T_BP_DAU_TU_TRANG_THIET_BI_DATA> detailCostData, out bool isDrillDownApply, model);
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
            return PartialView(dataCost);
        }

        public override ActionResult SummaryCenter(string centerCode, int? year, int? version, bool isRenderPartial = false)
        {
            // cost
            var dataCost = _service.SummaryCenterOut(out IList<T_BP_DAU_TU_TRANG_THIET_BI_DATA> detailCostElements, centerCode ?? ProfileUtilities.User.ORGANIZE_CODE, year.Value, version);
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
    }
}