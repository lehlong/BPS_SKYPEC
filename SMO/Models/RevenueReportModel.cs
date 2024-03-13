using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMO.Models
{
    public class SupplyReportModel
    {
        public string Name { get; set; }
        public decimal ValueDG { get; set; }
        public decimal ValueMOPS { get; set; }
        public decimal ValueTNK { get; set; }
        public decimal ValueD { get; set; }
        public decimal ValueFH { get; set; }

        public decimal ValueThue { get; set; }
        public decimal ValueDT { get; set; }

        public decimal ValueDTMOPS { get; set; }
        public decimal ValueDTTNK { get; set; }
        public decimal ValueDTD { get; set; }
        public decimal ValueDTFH { get; set; }
        public decimal ValueSL { get; set; }
        public bool IsBold { get; set; } = false;
        public int Order { get; set; }
        public int Level { get; set; }
    }
    public class RevenueByFeeReportModel
    {
        public IList<RevenueReportModel> Tab1 { get; set; } = new List<RevenueReportModel> { };
        public IList<RevenueReportModel> Tab2 { get; set; } = new List<RevenueReportModel> { };
        public IList<RevenueReportModel> Tab3 { get; set; } = new List<RevenueReportModel> { };
        public IList<RevenueReportModel> Tab4 { get; set; } = new List<RevenueReportModel> { };
        public IList<RevenueReportModel> Tab5 { get; set; } = new List<RevenueReportModel> { };

    }
    public class RevenueReportModel
    {
        public string Stt { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Value1 { get; set; }
        public decimal Value2 { get; set; }
        public decimal Value3 { get; set; }
        public decimal Value4 { get; set; }
        public decimal Value5 { get; set; }
        public decimal Value6 { get; set; }
        public decimal Value7 { get; set; }
        public decimal Value8 { get; set; }
        public decimal Value9 { get; set; }
        public decimal Value10 { get; set; }
        public decimal Value11 { get; set; }
        public decimal Value12 { get; set; }
        public decimal ValueSumYear { get; set; }
        public decimal ValueSumYearAll { get; set; }
        public bool IsBold { get; set; } = false;
        public int Order { get; set; }
        public int Level { get; set; }
    }


    public class SynthesizeThePlanReportModel
    {
        public IList<SanLuong> SanLuong { get; set; } = new List<SanLuong> { };
        public IList<DauTu> DauTu { get; set; } = new List<DauTu> { };
        public IList<SuaChuaLon> SuaChuaLon { get; set; } = new List<SuaChuaLon> { };
        public IList<ChiPhi> ChiPhi { get; set; } = new List<ChiPhi> { };

    }

    public class SuaChuaLonReportModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal valueGT { get; set; }
        public string valueQM { get; set; }
        public string valueHT { get; set; }
        public bool IsBold { get; set; } = false;
        public int Order { get; set; }
        public int Level { get; set; }
    }

    public class SuaChuaThuongXuyenReportModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal valueGT { get; set; }
        public string valueQM { get; set; }
        public string valueHT { get; set; }
        public bool IsBold { get; set; } = false;
        public int Order { get; set; }
        public int Level { get; set; }
    }

    public class DauTuReportModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Equity_Source { get; set; }
        public decimal SumEquity { get; set; }
        public decimal ValueKPDT { get; set; }

        public string Process { get; set; }

        public string Decription { get; set; }

        public bool IsBold { get; set; } = false;
        public int Order { get; set; }
        public int Level { get; set; }
    }

    public class ChiPhiReportModel
    {
        public string code { get; set; }
        public string name { get; set; }
        public string area { get; set; }
        public decimal value { get; set; }
        public bool IsBold { get; set; } = false;
        public int Order { get; set; }
        public int Level { get; set; }
    }
    public class SanLuong
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Value1 { get; set; }
        public decimal Value2 { get; set; }
        public decimal Value3 { get; set; }
        public decimal Value4 { get; set; }
        public decimal Value5 { get; set; }
        public bool IsBold { get; set; } = false;
        public int Order { get; set; }
        public int Level { get; set; }

    }

    public class DauTu
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Value1 { get; set; }
        public decimal Value2 { get; set; }
        public decimal Value3 { get; set; }
        public string Value4 { get; set; }
        public string Value5 { get; set; }
        public bool IsBold { get; set; } = false;
        public int Order { get; set; }
        public int Level { get; set; }

    }

    public class SuaChuaLon
    {
        public string code { get; set; }
        public string parentCode { get; set; }
        public string name { get; set; }
        public decimal valueKP { get; set; }
        public string valueQM { get; set; }
        public string description{ get; set; }

        public bool IsBold { get; set; } = false;
        public int Order { get; set; }
        public int Level { get; set; }
    }

    public class ChiPhi
    {
        public string code { get; set; }
        public string name { get; set; }
        public string parenCode { get; set; }
        public string parentCode2 { get; set; }
        public decimal valueCP { get; set; }
        public decimal price { get; set; }
        public decimal Value1 { get; set; }
        public decimal Value2 { get; set; }
        public decimal Value3 { get; set; }
        public decimal Value4 { get; set; }
        public decimal Value5 { get; set; }
        public decimal Value6 { get; set; }
        public decimal Value7 { get; set; }
        public decimal Value8 { get; set; }
        public decimal Value9 { get; set; }
        public decimal Value10 { get; set; }
        public decimal Value11 { get; set; }
        public decimal Value12 { get; set; }
        public string description { get; set; }
        public bool IsBold { get; set; } = false;
        public int Order { get; set; }
        public int Level { get; set; }
    }

    public class SyncCostModel
    {
        public string code { get; set; }
        public decimal value { get; set; }
        public decimal accumulation { get; set; }
        public int month { get; set; }
    }
}