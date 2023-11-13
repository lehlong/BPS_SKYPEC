using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMO.Models
{
    public class DataCenterModel
    {
        public List<KeHoachGiaThanhData> KeHoachGiaThanhData { get; set; } = new List<KeHoachGiaThanhData> { };
    }

    public class KeHoachGiaThanhData
    {
        public string Warehouse { get; set; }
        public string DeliveryConditions { get; set; }
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
}