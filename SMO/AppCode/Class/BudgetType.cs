namespace SMO
{
    public static class BudgetType
    {
        public const string SanLuong = "A";
        public const string DoanhThu = "B";
        public const string DauTuXayDung = "D";
        public const string KinhDoanh = "P";
        public const string DongTien = "C";

        public static string GetText(string type)
        {
            switch (type)
            {
                case SanLuong:
                    return "Sản lượng";
                case KinhDoanh:
                    return "Kinh doanh";
                case DauTuXayDung:
                    return "Đầu tư có xây dựng";
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