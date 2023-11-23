using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMO.Models
{
    public class DataCenterModel
    {
        public List<KeHoachGiaThanhData> KeHoachGiaThanhData { get; set; } = new List<KeHoachGiaThanhData> { };
        public List<PreTrungBinhKhoData> PreTrungBinhKhoData { get; set; } = new List<PreTrungBinhKhoData> { };
        public List<KeHoachTaiChinhData> KeHoachTaiChinhData { get; set; } = new List<KeHoachTaiChinhData> { };
        public List<PreData> PreData { get; set; } = new List<PreData> { };
        public List<CungUngData> CungUngData { get; set; } = new List<CungUngData> { };
        public List<MuaHangCungUngTraNapData> MuaHangCungUngTraNapData { get; set; } = new List<MuaHangCungUngTraNapData> { };
    }

    public class KeHoachGiaThanhData
    {
        public string WarehouseCode { get; set; }
        public string Warehouse { get; set; }
        public string DeliveryConditions { get; set; }
        public string DeliveryConditionsCode { get; set; }
        public decimal S0001 { get; set;}
        public decimal S0002 { get; set; }
        public decimal S0003 { get; set; }
        public decimal S0004 { get; set; }
        public decimal S0005 { get; set; }
        public decimal S0006 { get; set; }
        public decimal S0007 { get; set; }
        public decimal U0001 { get; set; }
        public decimal U0002 { get; set; }
        public decimal U0003 { get; set; }
        public decimal U0004 { get; set; }
        public decimal U0005 { get; set; }
        public decimal U0006 { get; set; }
        public decimal U0007 { get; set; }
        public decimal U0008 { get; set; }
        public decimal U0009 { get; set; }
        public decimal U0010 { get; set; }

    }

    public class PreTrungBinhKhoData
    {
        public string WarehouseCode { get; set; }
        public string Warehouse { get; set; }
        public decimal PreTrungBinh { get; set; }
        public decimal SanLuong { get; set; }
        public decimal PreDN1 { get; set; }
        public decimal TrungBinh { get; set; }

    }

    public class PreData
    {
        public string FirstPoint1 { get; set; }
        public string FirstPoint2 { get; set; }
        public string FirstPoint3 { get; set; }
        public string RouteCode { get; set; }   
        public string RouteName { get; set; }
        public string FinalPoint { get; set; }
        public decimal Quantity { get; set; }
        public decimal Premium { get; set; }
        public decimal ChenhLech { get; set; }
        public decimal Total { get; set; }

    }

    public class KeHoachTaiChinhData
    {
        public string ElementCode { get; set; }
        public string ElementName { get; set; }
        public string UnitCode { get; set; }
        public decimal? Value { get; set; }
        public decimal? Order { get; set; }
        public string Screen { get; set; }
    }

    public class CungUngData
    {
        public string SanBayCode { get; set; }
        public decimal ChuaPhanBo { get; set; }
        public decimal SanLuongCungUng { get; set; }
        public decimal DanhGiaCPQLPhanBo { get; set; }
        public decimal DanhGiaCPTauNoi { get; set; }
        public decimal DanhGiaCPKhoDN { get; set; }
        public decimal DanhGiaVanTaiDuongBo { get; set; }
        public decimal DanhGiaCUKhongGomHH { get; set; }
        public decimal TyLeHaoHut { get; set; }
        public decimal DanhGiaHaoHutCungUng { get; set; }
        public decimal DanhGiaChiPhiCungUng { get; set; }
    }

    public class MuaHangCungUngTraNapData
    {
        public string SanBayCode { get; set; }
        public int Group { get; set; }
        public decimal SanLuongCungUng { get; set; }
        public decimal SanLuongTraNap { get; set; }
        public decimal Premium { get; set; }
        public decimal ChiPhi { get; set; }
        public decimal ChiPhiCungUng { get; set; }
        public decimal ChiPhiTong { get; set; }
        public decimal TraNap { get; set; }
        public decimal TraNapXe { get; set; }
        public decimal TraNapNgam { get; set; }
        public decimal TraNapTong { get; set; }

        public decimal Vna_Vasco { get; set; }
        public decimal OtherHKVN { get; set; }
        public decimal HKNN { get; set; }

    }
}