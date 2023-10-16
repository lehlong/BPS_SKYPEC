namespace SMO
{
    public static class ElementType
    {
        public const string SanLuong = "A";
        public const string ChiPhi = "C";
        public const string DoanhThu = "B";

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
                default:
                    return type;
            }
        }
    }
}