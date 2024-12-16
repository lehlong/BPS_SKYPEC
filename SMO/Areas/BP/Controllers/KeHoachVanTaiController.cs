using Newtonsoft.Json;
using NPOI.XSSF.Streaming.Values;
using SMO.Core.Entities.BP;
using SMO.Core.Entities.BP.KE_HOACH_VAN_CHUYEN;
using SMO.Repository.Mapping.BP;
using SMO.Service.AD;
using SMO.Service.BP.KE_HOACH_DOANH_THU;
using SMO.Service.BP.KE_HOACH_SAN_LUONG;
using SMO.Service.CM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using static SMO.Service.AD.UserService;

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
        public FileContentResult ExportExcelGridData(string Treedata , string template)
        {
            var dataTab1 = JsonConvert.DeserializeObject<List<T_BP_KE_HOACH_VAN_TAI>>(Treedata);
            MemoryStream outFileStream = new MemoryStream();
            var path = Server.MapPath("~/TemplateExcel/" + "KeHoachVanTai.xlsx");
            _service.ExportExcelGridData(ref outFileStream , path, dataTab1);
            var fileName = "Kế_Hoạch_Vận_Tải";
            return File(outFileStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName + ".xlsx");
        }
        [HttpPost]
        [MyValidateAntiForgeryToken] 
        public ActionResult UpdateCellValue(string id, int year, string column, string value)
        {
            var result = new TransferObject
            {
                Type = TransferType.AlertSuccessAndJsCommand
            };
            _service.UpdateCellValue(id, year, column, value);
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
    }
}