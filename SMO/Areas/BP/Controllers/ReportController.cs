using Microsoft.CodeAnalysis;
using SMO.Service.MD;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

        public ActionResult IndexSanLuong()
        {
            return PartialView();
        }
        public ActionResult GenDataSanLuong(int year, string phienBan, string kichBan, string hangHangKhong)
        {
            var data = _servicePhienBan.GetDataSanLuong(year, phienBan, kichBan, hangHangKhong);
            ViewBag.PhienBan = phienBan;
            ViewBag.Year = year;
            return PartialView(data);
        }

        public ActionResult IndexKeHoachTongHop()
        {
            return PartialView();
        }
        public ActionResult GenDataKeHoachTongHop(int year, string phienBan, string kichBan, string area)
        {
            var data = _servicePhienBan.GetDataKeHoachTongHop(year, phienBan, kichBan, area);
            ViewBag.PhienBan = phienBan;
            ViewBag.Year = year;
            return PartialView(data);
        }

        public async Task<ActionResult> ExportExcelDataTongHop(int year, string phienBan, string kichBan, string area)
        {
            var path = Server.MapPath("~/TemplateExcel/TONG_HOP_KE_HOACH.xlsx");
            MemoryStream outFileStream = await _servicePhienBan.ExportExcelTongHop(path, year, phienBan, kichBan, area);
            if (!_servicePhienBan.State)
            {
                return Content(_servicePhienBan.ErrorMessage);
            }
            return File(outFileStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TONG_HOP_KE_HOACH.xlsx");

        }

        public ActionResult ExportExcelDataSanLuong(int year, string phienBan, string kichBan, string hangHangKhong)
        {
            MemoryStream outFileStream = new MemoryStream();
            var path = Server.MapPath("~/TemplateExcel/TONG_HOP_KE_HOACH_SAN_LUONG.xlsx");
            _servicePhienBan.ExportExcelSanLuong(ref outFileStream, path, year, phienBan, kichBan, hangHangKhong);
            if (!_servicePhienBan.State)
            {
                return Content(_servicePhienBan.ErrorMessage);
            }
            return File(outFileStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TONG_HOP_KE_HOACH_PHIEN_BAN.xlsx");
        }

        public ActionResult ExportExcelDataDoanhThu(int year, string phienBan, string kichBan, string hangHangKhong)
        {
            MemoryStream outFileStream = new MemoryStream();
            var path = Server.MapPath("~/TemplateExcel/TONG_HOP_KE_HOACH_DOANH_THU.xlsx");
            _servicePhienBan.ExportExcelDoanhThu(ref outFileStream, path, year,  phienBan, kichBan, hangHangKhong);
            if (!_servicePhienBan.State)
            {
                return Content(_servicePhienBan.ErrorMessage);
            }
            return File(outFileStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TONG_HOP_KE_HOACH_PHIEN_BAN.xlsx");
        }

        public ActionResult IndexDoanhThuTheoPhi()
        {
            return PartialView();
        }
        public ActionResult GenDataDoanhThuTheoPhi(int year, string phienBan, string kichBan, string hangHangKhong)
        {
            var data = _servicePhienBan.GetDataDoanhThuTheoPhi(year, phienBan, kichBan, hangHangKhong);
            ViewBag.PhienBan = phienBan;
            ViewBag.Year = year;
            return PartialView(data);
        }
        public ActionResult ExportExcelDataDoanhThuTheoPhi(int year, string phienBan, string kichBan, string hangHangKhong)
        {
            MemoryStream outFileStream = new MemoryStream();
            var path = Server.MapPath("~/TemplateExcel/TONG_HOP_KE_HOACH_DOANH_THU_THEO_CHI_PHI.xlsx");
            _servicePhienBan.ExportExcelDoanhThuTheoChiPhi(ref outFileStream, path, year, phienBan, kichBan, hangHangKhong);
            if (!_servicePhienBan.State)
            {
                return Content(_servicePhienBan.ErrorMessage);
            }
            return File(outFileStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TONG_HOP_KE_HOACH_DOANH_THU_THEO_CHI_PHI.xlsx");
        }

        public ActionResult IndexTraNapCungUng()
        {
            return PartialView();
        }

        public async Task<ActionResult> GenDataTraNapCungUng(int year, string phienBan, string kichBan, string hangHangKhong)
        {
            var data =await _servicePhienBan.GetDataTraNapCungUng(year, phienBan, kichBan, hangHangKhong);
            ViewBag.PhienBan = phienBan;
            ViewBag.Year = year;
            return PartialView(data);
        }

        public async Task<ActionResult> ExportExcelDataTraNapCungUng(int year, string phienBan, string kichBan, string hangHangKhong)
        {
            var path = Server.MapPath("~/TemplateExcel/TONG_HOP_KE_HOACH_TRA_NAP_CUNG_UNG.xlsx");
            MemoryStream outFileStream = await _servicePhienBan.ExportExcelTraNapCungUng(path, year, phienBan, kichBan, hangHangKhong);
            if (!_servicePhienBan.State)
            {
                return Content(_servicePhienBan.ErrorMessage);
            }
            return File(outFileStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TONG_HOP_KE_HOACH_DOANH_THU_THEO_CHI_PHI.xlsx");
        }

        public ActionResult IndexSuaChuaLon()
        {
            return PartialView();
        }


        public ActionResult GenDataSuaChuaLon(int year, string phienBan, string kichBan, string area)
        {
            var data = _servicePhienBan.GetReportDataSuaChuaLon(year, phienBan, kichBan, area);
            ViewBag.PhienBan = phienBan;
            ViewBag.Year = year;
            return PartialView(data);
        }

        public ActionResult IndexSuaChuaThuongXuyen()
        {
            return PartialView();
        }

        public ActionResult GenDataSuaChuaThuongXuyen(int year, string phienBan, string kichBan, string area)
        {
            var data = _servicePhienBan.GetReportDataSuaChuaThuongXuyen(year, phienBan, kichBan, area);
            ViewBag.PhienBan = phienBan;
            ViewBag.Year = year;
            return PartialView(data);
        }

        public ActionResult ExportExcelSuaChuaLon(int year, string phienBan, string kichBan, string area)
        {
            var path = Server.MapPath("~/TemplateExcel/TONG_HOP_KE_HOACH_SUA_CHUA_LON.xlsx");
            MemoryStream outFileStream = _servicePhienBan.ExportExcelSuaChuaLon(path, year, phienBan, kichBan, area);
            if (!_servicePhienBan.State)
            {
                return Content(_servicePhienBan.ErrorMessage);
            }
            return File(outFileStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TONG_HOP_KE_HOACH_SUA_CHUA_LON.xlsx");
        }

        public ActionResult IndexDauTu()
        {
            return PartialView();
        }

        public ActionResult GenDataDauTu(int year, string phienBan, string kichBan, string area)
        {
            var data = _servicePhienBan.GetReportDataDauTu(year, phienBan, kichBan, area);
            ViewBag.PhienBan = phienBan;
            ViewBag.Year = year;
            return PartialView(data);
        }

    }
}