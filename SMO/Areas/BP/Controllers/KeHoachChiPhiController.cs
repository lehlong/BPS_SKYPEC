﻿
using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using NHibernate.SqlCommand;
using SMO.Core.Entities;
using SMO.Core.Entities.BP.KE_HOACH_CHI_PHI;
using SMO.Core.Entities.MD;
using SMO.Models;
using SMO.Repository.Implement.BP;
using SMO.Repository.Implement.BP.KE_HOACH_CHI_PHI;
using SMO.Repository.Implement.MD;
using SMO.Service.BP;
using SMO.Service.BP.KE_HOACH_CHI_PHI;
using SMO.Service.Class;
using SMO.Service.CM;
using SMO.Service.MD;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using static SMO.SelectListUtilities;

namespace SMO.Areas.BP.Controllers
{
    [SessionState(System.Web.SessionState.SessionStateBehavior.ReadOnly)]
    public class KeHoachChiPhiController :
        BPControllerBase<KeHoachChiPhiService, T_BP_KE_HOACH_CHI_PHI, KeHoachChiPhiRepo, T_MD_KHOAN_MUC_HANG_HOA, T_BP_KE_HOACH_CHI_PHI_VERSION, T_BP_KE_HOACH_CHI_PHI_HISTORY, KeHoachChiPhiHistoryRepo>,
        IBPController<KeHoachChiPhiService, T_BP_KE_HOACH_CHI_PHI, KeHoachChiPhiRepo, T_MD_KHOAN_MUC_HANG_HOA, T_BP_KE_HOACH_CHI_PHI_VERSION, T_BP_KE_HOACH_CHI_PHI_HISTORY, KeHoachChiPhiHistoryRepo>
    {
        public override FileContentResult DownloadTemplate(string templateId, int year)
        {
            var template = _service.GetTemplate(templateId);
            MemoryStream outFileStream = new MemoryStream();
            var templateExcel = "";
            if (template.DetailKeHoachChiPhi.Where(x=>x.Center!=null).Any(x => x.Center.COST_CENTER_CODE == "100001"))
            {
                templateExcel = "Template_KeHoachChiPhiCoQuanCongTy.xlsx";
            }
            else if (template.DetailKeHoachChiPhi.Where(x => x.Center != null).Any(x => x.Center.COST_CENTER_CODE == "100002"))
            {
                templateExcel = "Template_KeHoachChiPhiMienBac.xlsx";
            }
            else if (template.DetailKeHoachChiPhi.Where(x => x.Center != null).Any(x => x.Center.COST_CENTER_CODE == "100003"))
            {
                templateExcel = "Template_KeHoachChiPhiMienTrung.xlsx";
            }
            else if (template.DetailKeHoachChiPhi.Where(x => x.Center != null).Any(x => x.Center.COST_CENTER_CODE == "100004"))
            {
                templateExcel = "Template_KeHoachChiPhiMienNam.xlsx";
            }
            else if (template.DetailKeHoachChiPhi.Where(x => x.Center != null).Any(x => x.Center.COST_CENTER_CODE == "100005"))
            {
                templateExcel = "Template_KeHoachChiPhiDoiVanTai.xlsx";
            }

            string path = Server.MapPath("~/TemplateExcel/" + templateExcel);
            _service.GenerateTemplate(ref outFileStream, path, templateId, year);
            var fileName = template.NAME;

            return File(outFileStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName + ".xlsx");
        }
        [HttpPost]
        [ValidateInput(false)]
        public FileContentResult DownloadData(string modelJson,string dataCP)
        {
            var data = modelJson;
            var DataUI = dataCP;
            var model = JsonConvert.DeserializeObject<ViewDataCenterModel>(modelJson);
            var DLUI= JsonConvert.DeserializeObject<DLkehoachData[]>(dataCP);
            var template = _service.GetTemplate(model.TEMPLATE_CODE);
            var lstSanBay = _service.GetSanBayInTemplate(model).ToList();
            MemoryStream outFileStream = new MemoryStream();

            var orgCodeInTemplate = "";

            var templateExcel = "";
            if (template.DetailKeHoachChiPhi.Any(x => x.Center.COST_CENTER_CODE == "100001"))
            {
                templateExcel = "Template_KeHoachChiPhiCoQuanCongTy.xlsx";
                orgCodeInTemplate = "100001";
            }
            else if (template.DetailKeHoachChiPhi.Any(x => x.Center.COST_CENTER_CODE == "100002"))
            {
                templateExcel = "Template_KeHoachChiPhiMienBac.xlsx";
                orgCodeInTemplate = "100002";
            }
            else if (template.DetailKeHoachChiPhi.Any(x => x.Center.COST_CENTER_CODE == "100003"))
            {
                templateExcel = "Template_KeHoachChiPhiMienTrung.xlsx";
                orgCodeInTemplate = "100003";
            }
            else if (template.DetailKeHoachChiPhi.Any(x => x.Center.COST_CENTER_CODE == "100004"))
            {
                templateExcel = "Template_KeHoachChiPhiMienNam.xlsx";
                orgCodeInTemplate = "100004";
            }
            else if (template.DetailKeHoachChiPhi.Any(x => x.Center.COST_CENTER_CODE == "100005"))
            {
                templateExcel = "Template_KeHoachChiPhiDoiVanTai.xlsx";
                orgCodeInTemplate = "100005";
            }

            string path = Server.MapPath("~/TemplateExcel/" + templateExcel);
            _service.GenerateData2(ref outFileStream, path, model, orgCodeInTemplate, lstSanBay, DLUI);
            var fileName = template.NAME;
            var a = outFileStream.ToArray();
            return File(outFileStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName + ".xlsx");
        }
      
      

        public override ActionResult ViewTemplate(string templateId, int? version, int year, string centerCode = "")
        {
            if (!string.IsNullOrEmpty(templateId))
            {
                var dataCost = _service.PreparePureListForTemplate(out IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_CHI_PHI> detailOtherCostElements, templateId, year);
                ViewBag.detailOtherCostElements = detailOtherCostElements;

                if (detailOtherCostElements.Any(x => x.Center.COST_CENTER_CODE == "100001"))
                {
                    return PartialView("ViewTemplateKeHoachChiPhiCoQuanCongTy", dataCost);
                }
                else if (detailOtherCostElements.Any(x => x.Center.COST_CENTER_CODE == "100002"))
                {
                    return PartialView("ViewTemplateKeHoachChiPhiMienBac", dataCost);
                }
                else if (detailOtherCostElements.Any(x => x.Center.COST_CENTER_CODE == "100003"))
                {
                    return PartialView("ViewTemplateKeHoachChiPhiMienTrung", dataCost);
                }
                else if (detailOtherCostElements.Any(x => x.Center.COST_CENTER_CODE == "100004"))
                {
                    return PartialView("ViewTemplateKeHoachChiPhiMienNam", dataCost);
                }
                else if(detailOtherCostElements.Any(x => x.Center.COST_CENTER_CODE == "100005"))
                {
                    return PartialView("ViewTemplateKeHoachChiPhiDoiVanTai", dataCost);
                }
                else
                {
                    return RedirectToAction(nameof(ExportData), new { version, year, orgCode = centerCode, templateCode = string.Empty });
                }
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
            var modelString = model.ToString();
            var modelJson = JsonConvert.DeserializeObject<ViewDataCenterModel>(modelString);
            var lstSanBay = _service.GetSanBayInTemplate(modelJson).ToList();
            var data = _service.GetData(model, lstSanBay);
            ViewBag.dataCenterModel = model;
            return PartialView(data);
        }

        public ActionResult ViewDataTemplate(string model)
        {
            var modelJson = JsonConvert.DeserializeObject<ViewDataCenterModel>(model);
            var data = _service.GetSanBayInTemplate(modelJson);

            ViewBag.dataCenterModel = modelJson;
            ViewBag.Skip = 0;
            return PartialView(data);
        }

        public async Task<ActionResult> ViewDataTemplatePaging(string model, int skip, int? month)
        {
            var modelJson = JsonConvert.DeserializeObject<ViewDataCenterModel>(model);
            var lstSanBay = _service.GetSanBayInTemplate(modelJson).ToList();
            var data = await _service.GetDataChiPhi(modelJson, lstSanBay, month);
            var template = _service.GetTemplate(modelJson.TEMPLATE_CODE);
            ViewBag.Chinhanh = template.Organize.CODE;
            ViewBag.Sap = template.Organize.SAP_CODE;
            ViewBag.dataCenterModel = modelJson;
            skip = skip < 0 ? 0 : skip;
            ViewBag.Skip = skip;
            return PartialView(data);
        }

        public override ActionResult SummaryCenter(string centerCode, int? year, int? version, bool isRenderPartial = false)
        {
            // cost
            var dataCost = _service.SummaryCenterOut(out IList<T_BP_KE_HOACH_CHI_PHI_DATA> detailCostElements, centerCode ?? ProfileUtilities.User.ORGANIZE_CODE, year.Value, version);
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

        public ActionResult CommentKM(string code)
        {
            var khoanmuc = _service.GetKMHangHoa(code);
            var resultData = new
            {
                khoanmuc = khoanmuc
            };

            return Json(resultData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AssignKM(string code)
        {
            var khoanmuc = _service.GetKMHangHoa(code);
            var resultData = new
            {
                khoanmuc = khoanmuc
            };

            return Json(resultData, JsonRequestBehavior.AllowGet);
        }

        #region Update and History cell value
        [HttpPost]
        [MyValidateAntiForgeryToken]
        public ActionResult UpdateCellValue(string templateCode, int version, int year, string type, string sanBay, string costCenter, string elementCode, string value, int month, string org)
        {
            var result = new TransferObject
            {
                Type = TransferType.AlertSuccessAndJsCommand
            };
            _service.UpdateCellValue(templateCode,version, year, type, sanBay, costCenter, elementCode, value, month, org);
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
        public ActionResult GetHistoryEditElement(string templateCode, int version, int year, string elementCode)
        {
            var data = _service.GetHistoryEditElement(templateCode, version, year, elementCode);
            return PartialView(data);
        }
        #endregion

        #region Comment Elements
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
        #endregion

        #region Get Assign Department
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

        #endregion
    }
}