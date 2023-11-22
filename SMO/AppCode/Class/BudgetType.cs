namespace SMO
{
    public static class BudgetType
    {
        public const string SanLuong = "A";
        public const string DoanhThu = "B";
        public const string ChiPhi = "C";
        public const string DauTuXayDung = "D";
        public const string DauTuTrangThietBi = "E";
        public const string DauTuNgoaiDoanhNghiep = "F";
        public const string VanChuyen = "G";
        public const string SuaChuaLon = "H";
        public const string SuaChuaThuongXuyen = "I";
        public const string KinhDoanh = "P";
        public const string DongTien = "M";

        public static string GetText(string type)
        {
            switch (type)
            {
                case SanLuong:
                    return "Sản lượng";
                case KinhDoanh:
                    return "Kinh doanh";
                case ChiPhi:
                    return "Chi phí";
                case VanChuyen:
                    return "Vận chuyển";
                case SuaChuaLon:
                    return "Sua chữa lớn";
                case SuaChuaThuongXuyen:
                    return "Sua chữa thường xuyên";
                case DauTuXayDung:
                    return "Đầu tư có xây dựng";
                case DauTuTrangThietBi:
                    return "Đầu tư trang thiết bị";
                case DauTuNgoaiDoanhNghiep:
                    return "Đầu tư ngoài doanh nghiệp";
                case DongTien:
                    return "Dòng tiền";
                case DoanhThu:
                    return "Doanh thu";
                default:
                    return type;
            }
        }
    }
}