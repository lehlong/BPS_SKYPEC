using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMO.Models
{
    public class RevenueReportModel
    {
        public string Stt { get; set; }
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
        public bool IsBold { get; set; } = false;
        public int Order { get; set; }
        public int Level { get; set; }
    }
}