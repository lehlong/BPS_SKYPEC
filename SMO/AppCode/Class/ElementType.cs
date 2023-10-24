﻿namespace SMO
{
    public static class ElementType
    {
        public const string SanLuong = "A";
        public const string ChiPhi = "C";
        public const string DoanhThu = "B";
        public const string DauTuXayDung = "D";

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
                case DauTuXayDung:
                    return "Đầu tư có xây dựng";
                default:
                    return type;
            }
        }
    }
}