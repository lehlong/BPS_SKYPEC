namespace SMO
{
    public static class ElementType
    {
        public const string SanLuong = "A";
        public const string ChiPhi = "C";
        public const string DoanhThu = "B";
        public const string DauTuXayDung = "D";
        public const string DauTuTrangThietBi = "E";
        public const string DauTuNgoaiDoanhNghiep = "F";
        public const string VanChuyen = "G";
        public const string SuaChuaLon = "H";
        public const string SuaChuaThuongXuyen = "I";

        public static string GetText(string type)
        {
            switch (type)
            {
                case SanLuong:
                    return "Sản lượng";
                case ChiPhi:
                    return "Chi phí";
                case DoanhThu:
                    return "Doanh thu";
                case VanChuyen:
                    return "Vận chuyển";
                case SuaChuaLon:
                    return "Sửa chữa lớn";
                case SuaChuaThuongXuyen:
                    return "Sửa chữa thường xuyên";
                case DauTuXayDung:
                    return "Đầu tư có xây dựng";
                case DauTuTrangThietBi:
                    return "Đầu tư trang thiết bị";
                case DauTuNgoaiDoanhNghiep:
                    return "Đầu tư ngoài doanh nghiệp";
                default:
                    return type;
            }
        }
    }
}