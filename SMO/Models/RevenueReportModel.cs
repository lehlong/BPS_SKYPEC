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
        public int? Parent { get; set; }
        public int Level { get; set; }
    }
    public class RevenueByFeeReportModel
    {
        public IList<RevenueReportModel> Tab1 { get; set; } = new List<RevenueReportModel> { };
        public IList<RevenueReportModel> Tab2 { get; set; } = new List<RevenueReportModel> { };
        public IList<RevenueReportModel> Tab3 { get; set; } = new List<RevenueReportModel> { };
        public IList<RevenueReportModel> Tab4 { get; set; } = new List<RevenueReportModel> { };
        public IList<RevenueReportModel> Tab5 { get; set; } = new List<RevenueReportModel> { };

        public IList<RevenueReportModelSL_Tra_Nap> TabSL_TN { get; set; } = new List<RevenueReportModelSL_Tra_Nap>();
    }

    public class RevenueReportModelSL_Tra_Nap
    {
        public string Stt { get; set; }
        public string Parent { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Value1ND { get; set; }
        public decimal Value2ND { get; set; }
        public decimal Value3ND { get; set; }
        public decimal Value4ND { get; set; }
        public decimal Value5ND { get; set; }
        public decimal Value6ND { get; set; }
        public decimal Value7ND { get; set; }
        public decimal Value8ND { get; set; }
        public decimal Value9ND { get; set; }
        public decimal Value10ND { get; set; }
        public decimal Value11ND { get; set; }
        public decimal Value12ND { get; set; }
        public decimal ValueSumYearND { get; set; }
        public decimal ValueSumYearAllND { get; set; }

        public decimal Value1QT { get; set; }
        public decimal Value2QT { get; set; }
        public decimal Value3QT { get; set; }
        public decimal Value4QT { get; set; }
        public decimal Value5QT { get; set; }
        public decimal Value6QT { get; set; }
        public decimal Value7QT { get; set; }
        public decimal Value8QT { get; set; }
        public decimal Value9QT { get; set; }
        public decimal Value10QT { get; set; }
        public decimal Value11QT { get; set; }
        public decimal Value12QT { get; set; }
        public decimal ValueSumYearQT { get; set; }
        public decimal ValueSumYearAllQT { get; set; }
        public decimal ValueSumYearAll_ND_QT { get; set; }

        public bool IsBold { get; set; } = false;
        public int Order { get; set; }
        public int Level { get; set; }
    }
    public class RevenueReportModel
    {
        public string Id { get; set; }
        public string Stt { get; set; }
        public string Parent { get; set; }
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
        public List<SanLuong> SanLuong { get; set; } = new List<SanLuong> { };
        public List<DauTu> DauTu { get; set; } = new List<DauTu> { };
        public List<SuaChuaLon> SuaChuaLon { get; set; } = new List<SuaChuaLon> { };
        public List<ChiPhi> ChiPhi { get; set; } = new List<ChiPhi> { };
        public List<SuaChuaThuongXuyenReportModel> SuaChuaThuongXuyen { get; set; } = new List<SuaChuaThuongXuyenReportModel> { };

    }

    public class ReportCKPModel
    {
        public IList<DauTuCKP> DauTu { get; set; } = new List<DauTuCKP> { };
        public List<SuaChuaCKP> SuaChuaLon { get; set; } = new List<SuaChuaCKP> { };
        public IList<ChiPhiCKP> ChiPhi { get; set; } = new List<ChiPhiCKP> { };
    }


    public class DauTuCKP
    {
        public string Order { get; set; }
        public string Name { get; set; }
        public string Col1 {  get; set; }

        public decimal? Col2 { get; set; }
        public string Col3 { get; set; }
        public decimal Col4 { get; set; }
        public decimal Col5 { get; set; }
        public decimal Col6 { get; set; }
        public decimal Col7 { get; set; }
        public decimal Col8 { get; set; }
        public decimal Col9 { get; set; }
        public decimal Col10 { get; set; }
        public decimal Col11 { get; set; }
        public decimal Col12 { get; set; }
        public decimal Col13 { get; set; }
        public decimal Col14 { get; set; }
        public decimal Col15 { get; set; }
        public decimal Col16 { get; set; }
        public string Col17 { get; set; }
        public bool IsBold { get; set; } = false;
        public int Level { get; set; }

    }

    public class SuaChuaCKP
    {
        public string code { get; set; }
        public string parentCode { get; set; }

        public string Order { get; set; }
        public string Name { get; set; }
        public decimal Col1 { get; set; }

        public decimal Col2 { get; set; }
        public decimal Col3 { get; set; }
        public decimal Col4 { get; set; }
        public decimal Col5 { get; set; }
        public decimal Col6 { get; set; }

        public decimal Col7 { get; set; }
        public decimal Col8 { get; set; }
        public decimal Col9 { get; set; }
        public decimal Col9_2 { get; set; }

        public decimal Col10 { get; set; }
        public decimal Col11 { get; set; }
        public decimal Col12 { get; set; }
        public decimal Col13 { get; set; }
        public decimal Col14 { get; set; }
        public decimal Col15 { get; set; }
        public decimal Col16 { get; set; }
        public string Col17 { get; set; }
        public bool IsBold { get; set; } = false;
        public int Level { get; set; }
    }

    public class ChiPhiCKP
    {
        public string Order { get; set; }
        public string Name { get; set; }
        public string Group_1_ID { get; set; }
        public string Group_2_ID { get; set; }
        public decimal? Col1 { get; set; }

        public decimal? Col2 { get; set; }
        public decimal? Col3 { get; set; }
        public decimal Col4 { get; set; }
        public decimal Col5 { get; set; }
        public decimal Col6 { get; set; }
        public decimal Col6_2 { get; set; }

        public decimal Col7 { get; set; }
        public decimal Col8 { get; set; }
        public decimal Col9 { get; set; }
        public decimal Col10 { get; set; }
        public decimal Col11 { get; set; }
        public decimal Col12 { get; set; }
        public decimal Col13 { get; set; }
        public decimal Col14 { get; set; }
        public decimal Col15 { get; set; }
        public decimal Col16 { get; set; }
        public string Col17 { get; set; }
        public bool IsBold { get; set; } = false;
        public int Level { get; set; }
    }


    public class SuaChuaLonReportModel
    {
        public string Code { get; set; }
        public string Stt { get; set; }
        public string Name { get; set; }
        public string Parent { get; set; }
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
        public string Parent { get; set; }
        public string Name { get; set; }
        public decimal valueGT { get; set; }
        public string valueQM { get; set; }
        public string valueHT { get; set; }
        public bool IsBold { get; set; } = false;
        public int Order { get; set; }
        public string Stt { get; set; }
        public string Des { get; set; }
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

    public class SanLuong
    {
        public string Id { get; set; }  
        public string Stt { get; set; }
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
        public string Stt { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Value1 { get; set; }
        public decimal Value2 { get; set; }
        public decimal Value3 { get; set; }
        public string Value4 { get; set; }
        public decimal Value5 { get; set; }
        public decimal ValueDLTH { get; set; }
        public decimal valueDLTH1 { get; set; }
        public decimal valueDLTH2 { get; set; }
        public decimal valueDLTH3 { get; set; }
        public decimal valueDLTH4 { get; set; }
        public decimal valueDLTH5 { get; set; }
        public decimal valueDLTH6 { get; set; }
        public decimal valueDLTH7 { get; set; }
        public decimal valueDLTH8 { get; set; }
        public decimal valueDLTH9 { get; set; }
        public decimal valueDLTH10 { get; set; }
        public decimal valueDLTH11 { get; set; }
        public decimal valueDLTH12 { get; set; }
        public string Des { get; set; }

        public bool IsBold { get; set; } = false;
        public int Order { get; set; }
        public int Level { get; set; }

    }

    public class SuaChuaLon
    {
        public string stt { get; set; }
        public string code { get; set; }
        public string parentCode { get; set; }
        public string name { get; set; }
        public decimal valueKP { get; set; }
        public string valueQM { get; set; }
        public string des { get; set; }
        public decimal valueDLTH { get; set; }
        public decimal valueDLTH1 { get; set; }
        public decimal valueDLTH2 { get; set; }
        public decimal valueDLTH3 { get; set; }
        public decimal valueDLTH4 { get; set; }
        public decimal valueDLTH5 { get; set; }
        public decimal valueDLTH6 { get; set; }
        public decimal valueDLTH7 { get; set; }
        public decimal valueDLTH8 { get; set; }
        public decimal valueDLTH9 { get; set; }
        public decimal valueDLTH10 { get; set; }
        public decimal valueDLTH11 { get; set; }
        public decimal valueDLTH12 { get; set; }

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
        public string Stt { get; set; }
        public int Level { get; set; }
    }

    public class SyncCostModel
    {
        public string code { get; set; }
        public decimal value { get; set; }
        public decimal accumulation { get; set; }
        public int month { get; set; }
    }


    // Báo cáo TH chi phí
    public class ReportChiPhiModel
    {
        public List<ChiPhiInReport> chiPhiInReports { get; set; } = new List<ChiPhiInReport>();
    }

    public class ChiPhiInReport
    {
        public string Stt { get; set; }
        public string code { get; set; }
        public string name { get; set; }

        public decimal valueCQCT { get; set; }
        public decimal valueCNMB { get; set; }
        public decimal valueCNMT { get; set; }
        public decimal valueCNMN { get; set; }
        public decimal valueCNVT { get; set; }
        public decimal valueKH { get; set; }
        public decimal valueTcty { get; set; }
        public decimal valueWTH { get; set; }

        public bool IsBold { get; set; } = false;
        public int Order { get; set; }
        public int Level { get; set; }
    }

    // Bác cái TH Đầu tư
    public class ReportDauTuModel
    {
        public string Stt { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string equity_sources { get; set; }
        public decimal valueVDT { get; set; }
        public decimal valueKHKP { get; set; }
        public string tdtk { get; set; }
        public int lever { get; set; } = 0;
        public string description { get; set; }
        public bool IsBold { get; set; } = false;
        public int Order { get; set; }
        public string Col1 { get; set; }
        public decimal Col2 { get; set; }
        public decimal Col3 { get; set; }
        public string Col4 { get; set; }
        public string Des { get; set; }
    }

    public class ReportCompaseDTModel
    {
        public List<ReportCompaseDT> Tab1 { get; set; } = new List<ReportCompaseDT>();
        public List<RevenueReportModel> Tab2 { get; set; } = new List<RevenueReportModel>();
        public List<ReportCompaseDT> Tab3 { get; set; } = new List<ReportCompaseDT>();
    }

    public class ReportCompaseDT
    {
        public string Stt { get; set; }
        public string Parent { get; set; }
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
        public bool Isbold { get; set; }
        public int Order { get; set; }
    }
}