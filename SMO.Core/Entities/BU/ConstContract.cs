using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Core.Entities.BU
{
    public class ConstContract
    {
        public const string KhoiTao = "01";
        public const string TrinhDuyet = "02";
        public const string NguoiTheoDoi = "01";
        public const string PheDuyet = "03";
        public const string TuChoi = "04";
        public const string HuyTrinhDuyet = "05";
        public const string SuaTen = "06";
        public const string SuaSoHopDong = "07";
        public const string SuaLoaiHopDong = "08";
        public const string SuaKhachHang = "09";
        public const string SuaPhongBanPhuTrach = "10";
        public const string SuaGiaiDoanHopDong = "11";
        public const string SuaNgayBatDau = "12";
        public const string SuaNgayKetThuc = "13";
        public const string SuaGiaTriHopDong = "14";
        public const string SuaVat = "15";
        public const string SuaGiaTriHopDongVat = "31";
        public const string SuaDaiDienA = "16";
        public const string SuaDaiDienB = "17";
        public const string SuaMoTa = "18";
        public const string SuaNguoiPheDuyet = "19";
        public const string SuaNguoiQuanLy = "20";
        public const string SuaNguoiTheoDoi = "21";
        public const string XoaFile = "22";
        public const string XoaLink = "23";
        public const string ThemFile = "24";
        public const string ThemLink = "25";
        public const string ThemNguoiTheoDoi = "26";
        public const string XoaNguoiTheoDoi = "27";
        public const string TaoDotThanhToan = "28";
        public const string SuaDotThanhToan = "29";
        public const string XoaDotThanhToan = "30";
        public const string TaoVersionMoi = "33";
        public const string SuaNgayKyKet = "32";
        public const string ThemTienDoThanhToan = "34";
        public const string XoaTienDoThanhToan = "35";
        public const string ChinhSuaTienDoThanhToan = "36";


        public static string convertPhaseToString(string action)
        {
            switch (action)
            {
                case "01":
                    return "Đang thực hiện";
                case "02":
                    return "Đã hoàn thành";
                case "03":
                    return "Bị hủy";

                default:
                    return "";
            }
        }

        public static string convertStringToPhase(string action)
        {
            switch (action)
            {
                case "Đang thực hiện":
                    return "01";
                case "Đã hoàn thành":
                    return "02";
                case "Bị hủy":
                    return "03";
                default:
                    return "";
            }
        }

        public static string convertStringToProgress(string action)
        {
            switch (action)
            {
                case "Đã thanh toán":
                    return "01";
                case "Đã thanh toán một phần":
                    return "02";
                case "Chưa thanh toán":
                    return "03";
                default:
                    return "";
            }
        }

        public static string convertProgressToString(string action)
        {
            switch (action)
            {
                case "01":
                    return "Đã thanh toán";
                case "02":
                    return "Đã thanh toán một phần";
                case "03":
                    return "Chưa thanh toán";
                default:
                    return "";
            }
        }

        public static string convertStatusToString(string action)
        {
            switch (action)
            {
                case "01":
                    return "Khởi tạo";
                case "02":
                    return "Trình duyệt";
                case "03":
                    return "Phê duyệt";
                case "04":
                    return "Từ chối phê duyệt";
                case "05":
                    return "Huỷ trình duyệt";
                default:
                    return "";
            }
        }
        public static string convertStringToStatus(string description)
        {
            switch (description)
            {
                case "Khởi tạo":
                    return "01";
                case "Trình duyệt":
                    return "02";
                case "Phê duyệt":
                    return "03";
                case "Từ chối phê duyệt":
                    return "04";
                case "Huỷ trình duyệt":
                    return "05";
                default:
                    return "";
            }
        }
        public static string convertActionToString(string action)
        {
            switch (action)
            {
                case KhoiTao:
                    return "Khởi Tạo";
                case TrinhDuyet:
                    return "Trình Duyệt";
                case PheDuyet:
                    return "Phê Duyệt";
                case TuChoi:
                    return "Từ Chối";
                case HuyTrinhDuyet:
                    return "Hủy trình duyệt";
                case SuaTen:
                    return "Sửa Tên";
                case SuaSoHopDong:
                    return "Sửa Số Hợp Đồng";
                case SuaLoaiHopDong:
                    return "Sửa Loại Hợp Đồng";
                case SuaKhachHang:
                    return "Sửa Khách Hàng";
                case SuaPhongBanPhuTrach:
                    return "Sửa Phòng Ban Phụ Trách";
                case SuaGiaiDoanHopDong:
                    return "Sửa Giai Đoạn Hợp Đồng";
                case SuaNgayBatDau:
                    return "Sửa Ngày Bắt Đầu";
                case SuaNgayKetThuc:
                    return "Sửa Ngày Kết Thúc";
                case SuaGiaTriHopDong:
                    return "Sửa Giá Trị Hợp Đồng";
                case SuaVat:
                    return "Sửa Vật";
                case SuaGiaTriHopDongVat:
                    return "Sửa giá trị hợp đồng (sau vat)";
                case SuaDaiDienA:
                    return "Sửa Đại Diện A";
                case SuaDaiDienB:
                    return "Sửa Đại Diện B";
                case SuaMoTa:
                    return "Sửa Mô Tả";
                case SuaNguoiPheDuyet:
                    return "Sửa Người Phê Duyệt";
                case SuaNguoiQuanLy:
                    return "Sửa Người Quản Lý";
                case SuaNguoiTheoDoi:
                    return "Sửa Người Theo Dõi";
                case XoaFile:
                    return "Xóa File";
                case XoaLink:
                    return "Xóa Link";
                case ThemFile:
                    return "Thêm File";
                case ThemLink:
                    return "Thêm Link";
                case ThemNguoiTheoDoi:
                    return "Thêm Người Theo Dõi";
                case XoaNguoiTheoDoi:
                    return "Xóa Người Theo Dõi";
                case TaoDotThanhToan:
                    return "Tạo Đợt Thanh Toán";
                case SuaDotThanhToan:
                    return "Sửa Đợt Thanh Toán";
                case XoaDotThanhToan:
                    return "Xóa Đợt Thanh Toán";
                case SuaNgayKyKet:
                    return "Sửa ngày ký kết";
                case TaoVersionMoi:
                    return "Tạo version mới";
                default:
                    return "";
            }
        }
        public static string getTextHistory(string action, string oldValue, string newValue)
        {
            if (action == ConstContract.KhoiTao || action == ConstContract.TaoVersionMoi || action == ConstContract.ThemFile || action == ConstContract.XoaFile||action == ConstContract.XoaLink||action == ConstContract.ThemLink)
            {
                return convertActionToString(action);
            }
            else
            {
                return convertActionToString(action) + " từ " + oldValue + " sang " + newValue;
            }
        }
    
    }
}
