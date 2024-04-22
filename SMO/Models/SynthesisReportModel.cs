using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMO.Models
{
    public class SynthesisReportModel
    {
        public string Id { get; set; }
        public string Stt { get; set; }
        public string Name { get; set; }
        public string UnitName { get; set; }
        public decimal Value1 { get; set; }
        public decimal Value2 { get; set; }
        public decimal Value3 { get; set; }
        public decimal Value4 { get; set; }
        public decimal Value5 { get; set; }
        public decimal Value6 { get; set; }
        public decimal? Value7 { get; set; }
        public decimal? Value8 { get; set; }
        public bool IsBold { get; set; } = false;
        public int Order { get; set; }
    }
}