using NPOI.XSSF.Streaming.Values;
using SMO.Service.AD;
using SMO.Service.CM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace SMO.Areas.BP.Controllers
{
    public class KeHoachVanTaiController : Controller
    {
        private readonly UserService _service;
        public KeHoachVanTaiController()
        {
            _service = new UserService();
        }
        public ActionResult Index()
        {
            return PartialView(_service);
        }
        public ActionResult DownloadTemplateData()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/TemplateExcel/KeHoachVanTai.xlsx"));
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "KeHoachVanTai.xlsx");

        }
        public ActionResult ImportView(UserService service)
        {
            return PartialView(service);
        }
        [HttpPost]
        public ActionResult ImportDataVT(UserService service)
        {
            var result = new TransferObject
            {
                Type = TransferType.AlertSuccessAndJsCommand
            };
            var year = Request.Form.GetValues("YEAR")[0];
            service.ImportDataVT(Request, Convert.ToInt32(year));

            if (service.State)
            {
                SMOUtilities.GetMessage("1002", service, result);
            }
            else
            {
                result.Type = TransferType.AlertDanger;
                SMOUtilities.GetMessage("1005", service, result);
            }
            return result.ToJsonResult();
        }
        public ActionResult GetDataVTByYear(int year)
        {
            var data = _service.GetDataVT(year);
            return PartialView("GenDataVT", data);
        }
    }
}