using Microsoft.CodeAnalysis;
using SMO.Service.MD;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace SMO.Areas.BP.Controllers
{
    public class ReportController : Controller
    {
        private readonly PhienBanService _servicePhienBan;
        private readonly KichBanService _serviceKichBan;
        public ReportController()
        {
            _servicePhienBan = new PhienBanService();
            _serviceKichBan = new KichBanService();
        }
        public ActionResult IndexKichBan()
        {
            return PartialView();
        }
        public ActionResult ExportExcelDataKichBan(int year, string kichBan)
        {
            MemoryStream outFileStream = new MemoryStream();
            var path = Server.MapPath("~/TemplateExcel/TONG_HOP_KE_HOACH_KICH_BAN.xlsx");
            _serviceKichBan.ExportExcel(ref outFileStream, path, year, kichBan);
            if (!_serviceKichBan.State)
            {
                return Content(_serviceKichBan.ErrorMessage);
            }
            return File(outFileStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TONG_HOP_KE_HOACH_KICH_BAN.xlsx");
        }
        public ActionResult GenDataKichBan(int year, string kichBan)
        {
            var data = _serviceKichBan.GetData(year, kichBan);
            ViewBag.KichBan = kichBan;
            ViewBag.Year = year;
            return PartialView(data);
        }
        public ActionResult IndexPhienBan()
        {
            return PartialView();
        }
        public ActionResult GenDataPhienBan(int year, string phienBan)
        {
            var data = _servicePhienBan.GetData(year, phienBan);
            ViewBag.PhienBan = phienBan;
            ViewBag.Year = year;
            return PartialView(data);
        }
        public ActionResult ExportExcelDataPhienBan(int year, string phienBan)
        {
            MemoryStream outFileStream = new MemoryStream();
            var path = Server.MapPath("~/TemplateExcel/TONG_HOP_KE_HOACH_PHIEN_BAN.xlsx");
            _servicePhienBan.ExportExcel(ref outFileStream, path, year, phienBan);
            if (!_servicePhienBan.State)
            {
                return Content(_servicePhienBan.ErrorMessage);
            }
            return File(outFileStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TONG_HOP_KE_HOACH_PHIEN_BAN.xlsx");
        }

        public ActionResult IndexDoanhThu()
        {
            return PartialView();
        }
        public ActionResult GenDataDoanhThu(int year, string phienBan, string kichBan, string hangHangKhong)
        {
            var data = _servicePhienBan.GetDataDoanhThu(year, phienBan, kichBan, hangHangKhong);
            ViewBag.PhienBan = phienBan;
            ViewBag.Year = year;
            return PartialView(data);
        }
        public ActionResult ExportExcelDataDoanhThu(int year, string phienBan)
        {
            MemoryStream outFileStream = new MemoryStream();
            var path = Server.MapPath("~/TemplateExcel/TONG_HOP_KE_HOACH_PHIEN_BAN.xlsx");
            _servicePhienBan.ExportExcel(ref outFileStream, path, year, phienBan);
            if (!_servicePhienBan.State)
            {
                return Content(_servicePhienBan.ErrorMessage);
            }
            return File(outFileStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TONG_HOP_KE_HOACH_PHIEN_BAN.xlsx");
        }
    }
}