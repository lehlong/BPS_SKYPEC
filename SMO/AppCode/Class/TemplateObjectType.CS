﻿namespace SMO
{
    public static class TemplateObjectType
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
                case ChiPhi:
                    return "Chi phí";
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
