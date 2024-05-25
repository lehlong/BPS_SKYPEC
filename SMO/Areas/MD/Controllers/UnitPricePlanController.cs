using Newtonsoft.Json;
using SMO.Service.BP;
using SMO.Service.MD;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace SMO.Areas.MD.Controllers
{
    [AuthorizeCustom(Right = "R6.16")]
    public class UnitPricePlanController : Controller
    {
        private readonly UnitPricePlanService _service;

        public UnitPricePlanController()
        {
            _service = new UnitPricePlanService();
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
        public ActionResult List(UnitPricePlanService service)
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
        public ActionResult Create(UnitPricePlanService service)
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
        public ActionResult Edit(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                _service.Get(Guid.Parse(id));
            }
            return PartialView(_service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UnitPricePlanService service)
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
        public FileContentResult ExportExcelGridData(int year)
        {
            MemoryStream outFileStream = new MemoryStream();
            var path = Server.MapPath("~/TemplateExcel/" + "DON_GIA_KE_HOACH.xlsx");
            int NUMCELL = 0;
            _service.ExportExcel(ref outFileStream, path, NUMCELL, year);
           
            return File(outFileStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"DON_GIA_KE_HOACH_{year}" + ".xlsx");
        }

    }
}