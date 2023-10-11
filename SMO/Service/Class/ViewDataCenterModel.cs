namespace SMO.Service.Class
{
    public class ViewDataCenterModel
    {
        public string ORG_CODE { get; set; }
        public string TEMPLATE_CODE { get; set; }
        public string KICH_BAN { get; set; }
        public int YEAR { get; set; }
        public decimal? EXCHANGE_RATE { get; set; } = 1;
        public string EXCHANGE_TYPE { get; set; } = "VND";  // fix value is VND
        public int? VERSION { get; set; }
        public bool IS_DRILL_DOWN { get; set; }
        public bool IS_HAS_VALUE { get; set; }
        public bool IS_HAS_NOT_VALUE { get; set; }
        public bool IS_LEAF { get; internal set; }
    }

    public class ViewDataYearModel
    {
        public string SAN_BAY_CODE { get; set; }
        public string SAN_BAY_NAME { get; set; }
        public decimal? IS_VNA { get; set; }
        public decimal? NOT_VNA { get; set; }
        public decimal? SUM_VNA { get; set; }
        public decimal? QUOC_TE { get; set; }
        public decimal? TONG_CONG { get; set; }

    }
}
