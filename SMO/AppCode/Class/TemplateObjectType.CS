namespace SMO
{
    public static class TemplateObjectType
    {
        
        public const string SanLuong = "A";
        
        public const string DoanhThu = "B";
        public const string DauTuXayDung = "D";
        public const string DauTuTrangThietBi = "E";

        public const string Department = "01";
        
        public const string Project = "02";
        
        public const string DevelopProject = "03";

        public static string GetText(string type)
        {
            switch (type)
            {
                case SanLuong:
                    return "Sản lượng";
                case DoanhThu:
                    return "Doanh thu";
                case DauTuXayDung:
                    return "Đầu tư có xây dựng";
                case DauTuTrangThietBi:
                    return "Đầu tư trang thiết bị";
                case Department:
                    return "Phòng ban";
                case Project:
                    return "Dự án";
                case DevelopProject:
                    return "Xây dựng dự án";
                default:
                    return type;
            }
        }
    }
}
