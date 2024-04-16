using Newtonsoft.Json;
using SMO.Core.Entities;
using SMO.Service.BP.KE_HOACH_SAN_LUONG;
using SMO.Service.MD;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace SMO.Areas.MD.Controllers
{
    public class SyncCostController : Controller
    {
        private readonly SyncCostService _service;

        public SyncCostController()
        {
            _service = new SyncCostService();
        }

        [MyValidateAntiForgeryToken]
        public ActionResult Index(int? year)
        {
            if (!year.HasValue)
            {
                year = DateTime.Now.Year;
            }
            _service.ObjDetail.YEAR = year.Value;
            return PartialView(_service);
        }

        [ValidateAntiForgeryToken]
        public ActionResult List(SyncCostService service)
        {
            service.Search();
            return PartialView(service);
        }

        [MyValidateAntiForgeryToken]
        public ActionResult Create()
        {
            return PartialView(_service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SyncCostService service)
        {
            var result = new TransferObject
            {
                Type = TransferType.AlertSuccessAndJsCommand
            };
            service.ObjDetail.ID = Guid.NewGuid();
            service.Create();
            if (service.State)
            {
                SMOUtilities.GetMessage("1001", service, result);
                result.ExtData = "SubmitIndex();";
            }
            else
            {
                result.Type = TransferType.AlertDanger;
                SMOUtilities.GetMessage("1004", service, result);
            }
            return result.ToJsonResult();
        }

        [MyValidateAntiForgeryToken]
        public ActionResult Edit(Guid id)
        {
            if (id != Guid.Empty)
            {
                _service.Get(id);
            }
            return PartialView(_service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(SyncCostService service)
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

        [HttpPost]
        [MyValidateAntiForgeryToken]
        public ActionResult SynchornizeData(int year)
        {
            var result = new TransferObject
            {
                Type = TransferType.AlertSuccessAndJsCommand
            };
            _service.SynchornizeData(year);
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
        public FileContentResult ExportExcelGridData(string Treedata)
        {
            var data = JsonConvert.DeserializeObject<List<T_MD_SYNC_COST>>(Treedata);
           
            MemoryStream outFileStream = new MemoryStream();
            var path = Server.MapPath("~/TemplateExcel/" + "Template_dữ_liệu_thực_hiện_chi_phí.xlsx");
            _service.ExportExcelGridData(ref outFileStream, data, path);
            var fileName = "Dữ_liệu_thực_hiện_chi_phí";
            return File(outFileStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName + ".xlsx");
        }

    }
}