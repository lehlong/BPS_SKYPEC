using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using NHibernate.SqlCommand;
using SMO.Service.BP;
using SMO.Service.BP.KE_HOACH_SAN_LUONG;
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
        private readonly ReportService _service;
        public ReportController()
        {
            _service = new ReportService();
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
        public ActionResult GenDataKichBan(int year, string kichBan, int yearTH)
        {
            var data = _serviceKichBan.GetDataTH(year, kichBan, yearTH);
            ViewBag.KichBan = kichBan;
            ViewBag.Year = year;
            ViewBag.Yth = yearTH;
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

        public ActionResult IndexSanLuong(int? slug)
        {
            ViewBag.Slug = slug;
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
            return File(outFileStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TONG_HOP_KE_HOACH_SAN_LUONG.xlsx");
        }

        public ActionResult ExportExcelDataDoanhThu(int year, string phienBan, string kichBan, string hangHangKhong)
        {
            MemoryStream outFileStream = new MemoryStream();
            var path = Server.MapPath("~/TemplateExcel/TONG_HOP_KE_HOACH_DOANH_THU.xlsx");
            _servicePhienBan.ExportExcelDoanhThu(ref outFileStream, path, year, phienBan, kichBan, hangHangKhong);
            if (!_servicePhienBan.State)
            {
                return Content(_servicePhienBan.ErrorMessage);
            }
            return File(outFileStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TONG_HOP_KE_HOACH_DOANH_THU_THEO_THANG.xlsx");
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
            var data = await _servicePhienBan.GetDataTraNapCungUng(year, phienBan, kichBan, hangHangKhong);
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
            return File(outFileStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TONG_HOP_KE_HOACH_DOANH_THU_TRA_NAP_CUNG_UNG.xlsx");
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
            var data = _servicePhienBan.GetDataReportDauTu(year, phienBan, kichBan, area);
            ViewBag.PhienBan = phienBan;
            ViewBag.Year = year;
            return PartialView(data);
        }

        public ActionResult IndexReportChiPhi()
        {
            return PartialView();
        }

        public ActionResult GenDataChiPhi(int year, string phienBan, string kichBan)
        {
            var data = _servicePhienBan.GetReportDataChiPhi(year, phienBan, kichBan);
            ViewBag.PhienBan = phienBan;
            ViewBag.Year = year;
            var lstHeader = new List<string> { "CQ", "MB", "MT", "MN", "VT" };
            ViewBag.Header = lstHeader;
            return PartialView(data);
        }

        public ActionResult IndexReportCompaseDT()
        {
            return PartialView();
        }

        public ActionResult GetDataReportCompaseDT(int year, string phienBan, string kichBan, string hangHangKhong)
        {
            var data = _servicePhienBan.GetDataReportCompaseDT(year, phienBan, kichBan, hangHangKhong);
            ViewBag.PhienBan = phienBan;
            ViewBag.Year = year;
            return PartialView(data);
        }

        public ActionResult ExportExcelReportCompaseDT(int year, string phienBan, string kichBan, string hangHangKhong)
        {
            var path = Server.MapPath("~/TemplateExcel/TONG_HOP_KE_HOACH_SO_SANH_DT.xlsx");
            MemoryStream outFileStream = new MemoryStream();
            _servicePhienBan.ExportExcelCompaseDT(ref outFileStream, path, year, phienBan, kichBan, hangHangKhong);
            if (!_servicePhienBan.State)
            {
                return Content(_servicePhienBan.ErrorMessage);
            }
            return File(outFileStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TONG_HOP_KE_HOACH_SO_SANH_DOANH_THU.xlsx");
        }

        // Báo cáo tình hình thực hiện đầu tư có xây dựng
        public ActionResult IndexReportImplementDTXD()
        {
            return PartialView();
        }

        public ActionResult GetDataReportImplementDTXD(int year, string phienBan, string kichBan, string area)
        {
            var data = _servicePhienBan.GetDataReportDauTu(year, phienBan, kichBan, area);
            ViewBag.PhienBan = phienBan;
            ViewBag.Year = year;
            return PartialView(data);
        }

        public ActionResult ExportExcelReportImplementDTXD(int year, string phienBan, string kichBan, string area)
        {
            var path = Server.MapPath("~/TemplateExcel/Báo_Cáo_Tình_Hình_Thực_Hiện_Kế_Hoạch_Đầu_Tư.xlsx");
            MemoryStream outFileStream = new MemoryStream();
            _servicePhienBan.ExportExcelCompaseDT(ref outFileStream, path, year, phienBan, kichBan, area);
            if (!_servicePhienBan.State)
            {
                return Content(_servicePhienBan.ErrorMessage);
            }
            return File(outFileStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BAO_CAO_TINH_HINH_THUC_HIEN_DAU_TU.xlsx");
        }

        public ActionResult IndexReportImplementDTTTB()
        {
            return PartialView();
        }

        public ActionResult GetDataReportImplementDTTTB(int year, string phienBan, string kichBan, string area)
        {
            var data = _servicePhienBan.GetDataReportDauTu(year, phienBan, kichBan, area);
            ViewBag.PhienBan = phienBan;
            ViewBag.Year = year;
            return PartialView(data);
        }

        public ActionResult ExportExcelReportImplementDTTTB(int year, string phienBan, string kichBan, string area)
        {
            var path = Server.MapPath("~/TemplateExcel/Báo_Cáo_Tình_Hình_Thực_Hiện_Kế_Hoạch_Đầu_Tư.xlsx");
            MemoryStream outFileStream = new MemoryStream();
            _servicePhienBan.ExportExcelCompaseDT(ref outFileStream, path, year, phienBan, kichBan, area);
            if (!_servicePhienBan.State)
            {
                return Content(_servicePhienBan.ErrorMessage);
            }
            return File(outFileStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BAO_CAO_TINH_HINH_THUC_HIEN_DAU_TU.xlsx");
        }



        #region BM01D - Báo cáo ước tính hình thực hiện đầu tư vào các dự án hình thành TSCĐ năm trước kế hoạch
        public ActionResult IndexBM01D()
        {
            return PartialView();
        }
        public ActionResult GenDataBM01D(int year, string kichBan)
        {
            var data = _service.GenDataBM01D(year, kichBan);
            return PartialView(data);
        }
        #endregion

        #region BM01E - Báo cáo ước tính hình đầu tư vốn ra ngoài doanh nghiệp ra trước kế hoạch
        public ActionResult IndexBM01E()
        {
            return PartialView();
        }
        public ActionResult GenDataBM01E(int year, string phienBan, string kichBan, string hangHangKhong)
        {
            var data = _service.GenDataBM01E(year, kichBan);
            return PartialView(data);
        }
        #endregion

        #region BM02A - Biểu mẫu Kế hoạch đầu tư XDCB và TTB năm kế hoạch 
        public ActionResult IndexBM02A()
        {
            return PartialView();
        }
        public ActionResult GenDataBM02A(int year)
        {
            var data = _service.GenDataBM02A(year);
            return PartialView(data);
        }
        #endregion

        #region BM02B - Biểu mẫu Kế hoạch đầu tư ra ngòai doanh nghiệp năm kế hoạch 
        public ActionResult IndexBM02B()
        {
            return PartialView();
        }
        public ActionResult GenDataBM02B(int year, string phienBan, string kichBan, string hangHangKhong)
        {
            var data = _service.GenDataBM02B(year);
            return PartialView(data);
        }
        #endregion

        #region BM02C - Kế hoạch hiệu quả từng lĩnh vực kinh doanh của doanh nghiệp (KB Cao/Trung bình/Thấp)
        public ActionResult IndexBM02C()
        {
            return PartialView();
        }
        public ActionResult GenDataBM02C(int year, string kichBan)
        {
            var data = _service.GenDataBM02C(year, kichBan);
            return PartialView(data);
        }
        #endregion

        #region BM02C1 - Biểu mẫu hiệu quả từng lĩnh vực kinh doanh của doanh nghiệp các kịch bản
        public ActionResult IndexBM02C1()
        {
            return PartialView();
        }
        public ActionResult GenDataBM02C1(int year, string phienBan, string kichBan, string hangHangKhong)
        {
            var data = _service.GenDataBM02C1(year, phienBan);
            return PartialView(data);
        }
        #endregion

        #region BM02D - Chi tiết các khoản mục sản lượng, doanh thu, chi phí UTH (KB Cao/Trung bình/Thấp)
        public ActionResult IndexBM02D()
        {
            return PartialView();
        }
        public ActionResult GenDataBM02D(int year, string phienBan, string kichBan, string hangHangKhong)
        {
            var data = _service.GenDataBM02D(year, kichBan);
            return PartialView(data);
        }
        #endregion

        #region BM02D1 - Biểu mẫu chi tiết các khoản mục sản lượng, doanh thu, chi phí UTH năm trước theo các kịch bản 
        public ActionResult IndexBM02D1()
        {
            return PartialView();
        }
        public ActionResult GenDataBM02D1(int year, string phienBan, string kichBan, string hangHangKhong)
        {
            var data = _service.GenDataBM02D1(year, phienBan);
            return PartialView(data);
        }
        #endregion

        #region BM02D2 - Tổng hợp những biến động ảnh hưởng đến SXKD của các DN qua các năm 
        public ActionResult IndexBM02D2()
        {
            return PartialView();
        }
        public ActionResult GenDataBM02D2(int year, string phienBan, string kichBan, int month)
        {
            var data = _service.GenDataBM02D2(year, month, phienBan, kichBan);
            return PartialView(data);
        }
        #endregion

        #region BM2107 - BÁO CÁO TÌNH HÌNH THỰC HIỆN KẾ HOẠCH ĐẦU TƯ CÓ XÂY DỰNG
        public ActionResult IndexBM2107()
        {
            return PartialView();
        }
       
        public ActionResult GenDataBM2107(int year, string phienBan, string kichBan, int month, string area)
        {
            var data = _service.GenDataBM2107(year, month, phienBan, kichBan, area);
            return PartialView(data);
        }
        #endregion
        #region BMQT21
        public ActionResult IndexQT21()
        {
            return PartialView();
        }

        public ActionResult GenDataQT21(int year, string phienBan, string kichBan, int month, string area)
        {
            var data = _service.GetReportDataQT21(year, phienBan, kichBan,area, month);
            return PartialView(data);
        }
        #endregion

        #region BM2108 - BÁO CÁO TÌNH HÌNH THỰC HIỆN KẾ HOẠCH ĐẦU TƯ KHÔNG CÓ XÂY DỰNG
        public ActionResult IndexBM2108()
        {
            return PartialView();
        }
        public ActionResult GenDataBM2108(int year, string phienBan, string kichBan, int month, string area)
        {
            var data = _service.GenDataBM2108(year, month, phienBan, kichBan, area);
            return PartialView(data);
        }
        #endregion

        #region BM2109 - BÁO CÁO TÌNH HÌNH THỰC HIỆN KẾ HOẠCH SỬA CHỮA TSCĐ
        public ActionResult IndexBM2109()
        {
            return PartialView();
        }
        public ActionResult GenDataBM2109(int year, string phienBan, string kichBan, int month, string area)
        {
            var data = _service.GenDataBM2109(year, month, phienBan, kichBan, area);
            return PartialView(data);
        }
        #endregion

        #region BM2110 - Tổng hợp kế hoạch chi phí
        public ActionResult IndexBM2110()
        {
            return PartialView();
        }
        public ActionResult GenDataBM2110(int year, string phienBan, string kichBan, int month, string area)
        {
            var data = _service.GenDataBM2110(year, month, phienBan, kichBan, area);
            return PartialView(data);
        }
        #endregion

        #region CKP - BÁO CÁO CẤP KINH PHÍ HÀNG THÁNG CHO ĐƠN VỊ
        public ActionResult IndexCKP()
        {
            return PartialView();
        }
        public ActionResult GenDataCKP(int year, string phienBan, string kichBan, string area, int month)
        {
            var data = _servicePhienBan.GetDataCKP(year, phienBan, kichBan, area, month);
            ViewBag.PhienBan = phienBan;
            ViewBag.Year = year;
            return PartialView(data);
        }
        #endregion

        #region Export Excel Data
        [HttpPost]
        public FileContentResult ExportExcelGridData(string Treedata, string Template)
        {
            var data = JsonConvert.DeserializeObject<List<ReportModel>>(Treedata);
            MemoryStream outFileStream = new MemoryStream();
            var fileName = string.Empty;
            var path = Server.MapPath("~/TemplateExcel/" + Template + ".xlsx");
            int NUMCELL = 0;
            switch (Template)
            {
                case "BM_02A":
                    NUMCELL = 10;
                    _service.ExportExcelGridData(ref outFileStream, data, path, NUMCELL, Template);
                    fileName = "BIỂU MẪU KẾ HOẠCH ĐẦU TƯ XDCB VÀ TTB NĂM KẾ HOẠCH";
                    break;
                case "BM_02B":
                    NUMCELL = 8;
                    _service.ExportExcelGridData(ref outFileStream, data, path, NUMCELL, Template);
                    fileName = "BÁO CÁO TÌNH HÌNH THỰC HIỆN ĐẦU TƯ VÀO CÁC DỰ ÁN HÌNH THÀNH TSCĐ NĂM TRƯỚC KẾ HOẠCH";
                    break;
                case "BM_02C":
                    NUMCELL = 11;
                    _service.ExportExcelGridData(ref outFileStream, data, path, NUMCELL, Template);
                    fileName = "KẾ HOẠCH HIỆU QUẢ TỪNG LĨNH VỰC KINH DOANH CỦA DOANH NGHIỆP";
                    break;
                case "BM_02C1":
                    NUMCELL = 11;
                    _service.ExportExcelGridData(ref outFileStream, data, path, NUMCELL, Template);
                    fileName = "HIỆU QUẢ TỪNG LĨNH VỰC KINH DOANH CỦA DOANH NGHIỆP CÁC KỊCH BẢN";
                    break;
                case "BM_02D":
                    NUMCELL = 8;
                    _service.ExportExcelGridData(ref outFileStream, data, path, NUMCELL, Template);
                    fileName = "CHI TIẾT CÁC KHOẢN MỤC SẢN LƯỢNG, DOANH THU, CHI PHÍ UTH (KỊCH BẢN CAO/TRUNG BÌNH/THẤP)";
                    break;
                case "BM_02D1":
                    NUMCELL = 6;
                    _service.ExportExcelGridData(ref outFileStream, data, path, NUMCELL, Template);
                    fileName = "CHI TIẾT CÁC KHOẢN MỤC SẢN LƯỢNG, DOANH THU, CHI PHÍ UTH NĂM TRƯỚC THEO CÁC KỊCH BẢN";
                    break;
                case "BM_02D2":
                    NUMCELL = 10;
                    _service.ExportExcelGridData(ref outFileStream, data, path, NUMCELL, Template);
                    fileName = "TỔNG HỢP NHỮNG BIẾN ĐỘNG ẢNH HƯỞNG ĐẾN SẢN XUẤT KINH DOANH CỦA CÁC DOANH NGHIỆP QUA CÁC NĂM";
                    break;
                case "BM_01D":
                    NUMCELL = 15;
                    _service.ExportExcelGridData(ref outFileStream, data, path, NUMCELL, Template);
                    fileName = "BÁO CÁO TÌNH HÌNH THỰC HIỆN ĐẦU TƯ VÀO CÁC DỰ ÁN HÌNH THÀNH TSCĐ NĂM TRƯỚC KẾ HOẠCH";
                    break;
                case "BM_01E":
                    NUMCELL = 11;
                    _service.ExportExcelGridData(ref outFileStream, data, path, NUMCELL, Template);
                    fileName = "BÁO CÁO ƯỚC TÍNH TÌNH HÌNH ĐẦU TƯ VỐN RA NGOÀI DOANH NGHIỆP RA TRƯỚC KẾ HOẠCH";
                    break;
                case "BM_2107":
                    NUMCELL = 9;
                    _service.ExportExcelGridData(ref outFileStream, data, path, NUMCELL, Template);
                    fileName = "Báo cáo tình hình thực hiện đầu tư có xây dựng";
                    break;
                case "BM_2108":
                    NUMCELL = 12;
                    _service.ExportExcelGridData(ref outFileStream, data, path, NUMCELL, Template);
                    fileName = "Báo cáo tình hình thực hiện đầu tư không có xây dựng";
                    break;
                case "BM_2109":
                    NUMCELL = 7;
                    _service.ExportExcelGridData(ref outFileStream, data, path, NUMCELL, Template);
                    fileName = "Báo cáo tình hình thực hiện sửa chữa lớn TSCĐ";
                    break;
                case "BM_2110":
                    NUMCELL = 7;
                    _service.ExportExcelGridData(ref outFileStream, data, path, NUMCELL, Template);
                    fileName = "Tổng hợp kế hoạch chi phí";
                    break;
                case "BM_QT21":
                    NUMCELL = 11;
                    _service.ExportExcelGridData(ref outFileStream, data, path, NUMCELL, Template);
                    fileName = "Báo cáo tổng hợp kế hoạch chi phí theo năm";
                    break;
                default:
                    break;
            }
            return File(outFileStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName + ".xlsx");
        }

        [HttpPost]
        public FileContentResult ExportExcelTHKHDT(int year, string phienBan, string kichBan, string area)
        {
            MemoryStream outFileStream = new MemoryStream();
            var data = _servicePhienBan.GetDataReportDauTu(year, phienBan, kichBan, area);
            var path = Server.MapPath("~/TemplateExcel/TONG_HOP_KE_HOACH_DAU_TU.xlsx");
            _service.ExportExcelTHKHDT(ref outFileStream, data, path);
            return File(outFileStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TONG_HOP_KE_HOACH_DAU_TU.xlsx");
        }

        [HttpPost]
        public FileContentResult ExportExcelTHKHSCTX(int year, string phienBan, string kichBan, string area)
        {
            MemoryStream outFileStream = new MemoryStream();
            var data = _servicePhienBan.GetReportDataSuaChuaThuongXuyen(year, phienBan, kichBan, area);
            var path = Server.MapPath("~/TemplateExcel/TONG_HOP_KE_HOACH_SUA_CHUA_THUONG_XUYEN.xlsx");
            _service.ExportExcelTHKHSCTX(ref outFileStream, data, path);
            return File(outFileStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TONG_HOP_KE_HOACH_SUA_CHUA_THUONG_XUYEN.xlsx");
        }

        [HttpPost]
        public FileContentResult ExportExcelTHKHCN(int year, string phienBan, string kichBan, string area)
        {
            MemoryStream outFileStream = new MemoryStream();
            var data = _servicePhienBan.GetDataKeHoachTongHop(year, phienBan, kichBan, area);
            var path = Server.MapPath("~/TemplateExcel/TONG_HOP_KE_HOACH_CHI_NHANH.xlsx");
            _service.ExportExcelTHKHCN(ref outFileStream, data, path);
            return File(outFileStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TONG_HOP_KE_HOACH_CHI_NHANH.xlsx");
        }

        [HttpPost]
        public FileContentResult ExportExcelTHKHCP(int year, string phienBan, string kichBan)
        {
            MemoryStream outFileStream = new MemoryStream();
            var data = _servicePhienBan.GetReportDataChiPhi(year, phienBan, kichBan);
            var path = Server.MapPath("~/TemplateExcel/TONG_HOP_KE_HOACH_CHI_PHI.xlsx");
            _service.ExportExcelTHKHCP(ref outFileStream, data, path);
            return File(outFileStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TONG_HOP_KE_HOACH_CHI_PHI.xlsx");
        }
        #endregion
    }
}