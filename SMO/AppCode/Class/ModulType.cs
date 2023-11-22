namespace SMO
{
    public static class ModulType
    {
        public const string ContructCostCF = "ContructCostCF";
        public const string ContructCostPL = "ContructCostPL";
        public const string CostCF = "CostCF";
        public const string CostPL = "CostPL";
        public const string OtherCostCF = "OtherCostCF";
        public const string OtherCostPL = "OtherCostPL";
        public const string RevenueCF = "RevenueCF";
        public const string RevenuePL = "RevenuePL";
        public const string KeHoachSanLuong = "SanLuong";
        public const string KeHoachDoanhThu = "DoanhThu";
        public const string KeHoachVanChuyen = "VanChuyen";
        public const string KeHoachChiPhi = "ChiPhi";
        public const string SuaChuaLon = "SuaChuaLon";
        public const string SuaChuaThuongXuyen = "SuaChuaThuongXuyen";
        public const string DauTuXayDung = "DauTuCoXayDung";
        public const string DauTuTrangThietBi = "DauTuTrangThietBi";
        public const string DauTuNgoaiDoanhNghiep = "DauTuNgoaiDoanhNghiep";

        public static string GetTextSheetName(string type)
        {
            // sheet name trong excel bị giới hạn độ dài
            return GetText(type).Replace("Kế hoạch", "KH");
        }

        public static string GetText(string type)
        {
            switch (type)
            {
                case ContructCostCF:
                    return "Kế hoạch chi dự án";
                case ContructCostPL:
                    return "Kế hoạch chi phí dự án";
                case CostCF:
                    return "Kế hoạch chi thường xuyên";
                case CostPL:
                    return "Kế hoạch chi phí";
                case OtherCostCF:
                    return "Kế hoạch chi khác của dự án";
                case SuaChuaLon:
                    return "Kế hoạch sửa chữa lớn";
                case SuaChuaThuongXuyen:
                    return "Kế hoạch sửa chữa thường xuyên";
                case KeHoachSanLuong:
                    return "Kế hoạch sản lượng";
                case KeHoachDoanhThu:
                    return "Kế hoạch doanh thu";
                case KeHoachChiPhi:
                    return "Kế hoạch chi phí";
                case KeHoachVanChuyen:
                    return "Kế hoạch vận chuyển";
                case DauTuXayDung:
                    return "Kế hoạch đầu tư có xây dựng";
                case DauTuTrangThietBi:
                    return "Kế hoạch đầu tư trang thiết bị";
                case DauTuNgoaiDoanhNghiep:
                    return "Kế hoạch đầu tư ngoài doanh nghiệp";
                case OtherCostPL:
                    return "Kế hoạch chi phí khác của dự án";
                case RevenueCF:
                    return "Kế hoạch thu thường xuyên";
                case RevenuePL:
                    return "Kế hoạch doanh thu";
                default:
                    return type;
            }
        }
    }
}