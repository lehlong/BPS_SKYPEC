using Microsoft.Office.Interop.Excel;

namespace SMO.Service.Class
{
    public class ViewDataCenterModel
    {
        public string ORG_CODE { get; set; }
        public string TEMPLATE_CODE { get; set; }
        public string HANG_HANG_KHONG_CODE { get; set; }
        public string SAN_BAY_CODE { get; set; }
        public string AREA_CODE { get; set; }
        public string NHOM_SAN_BAY_CODE { get; set; }
        public string CHI_NHANH { get; set; }

        public string KICH_BAN { get; set; }
        public string PHIEN_BAN { get; set; }
        public int YEAR { get; set; }
        public int? MONTH { get; set; }
        public decimal? EXCHANGE_RATE { get; set; } = 1;
        public string EXCHANGE_TYPE { get; set; } = "VND";  // fix value is VND
        public int? VERSION { get; set; }
        public bool IS_DRILL_DOWN { get; set; }
        public bool IS_HAS_VALUE { get; set; }
        public bool IS_HAS_NOT_VALUE { get; set; }
        public bool IS_LEAF { get; internal set; }
    }

    public class DLkehoachData
    {
        public string Id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public bool isChecked { get; set; }
        public decimal? price { get; set; }
        public decimal? quantity_VPCN { get; set; }
        public decimal? total_VPCN { get; set; }
        public decimal? quantity_VII { get; set; }
        public decimal? total_VII { get; set; }
        public decimal? quantity_THD { get; set; }
        public decimal? total_THD { get; set; }

        public decimal? quantity_VDO { get; set; }
        public decimal? total_VDO { get; set; }
        public decimal? quantity_HPH { get; set; }
        public decimal? total_HPH { get; set; }
        public decimal? quantity_HAN { get; set; }
        public decimal? total_HAN { get; set; }
        public decimal? quantity_VDH { get; set; }
        public decimal? total_VDH { get; set; }
        public decimal? sumQuantity { get; set; }
        public decimal? sumTotal { get; set; }
        public string  parent { get; set; }
        public decimal? quantity_CXR { get; set; }
        public decimal? quantity_DAD { get; set; }
        public decimal? quantity_HUI { get; set; }
        public decimal? quantity_PXU { get; set; }
        public decimal? quantity_TBB { get; set; }
        public decimal? quantity_UIH { get; set; }
        public decimal? quantity_VCL { get; set; }
        public decimal? total_CXR { get; set; }
        public decimal? total_DAD { get; set; }
        public decimal? total_HUI { get; set; }
        public decimal? total_PXU { get; set; }
        public decimal? total_TBB { get; set; }
        public decimal? total_UIH { get; set; }
        public decimal? total_VCL { get; set; }
        // MN
        public decimal? quantity_BMV { get; set; }
        public decimal? quantity_DLI { get; set; }
        public decimal? quantity_PQC { get; set; }
        public decimal? quantity_SGN { get; set; }
        public decimal? quantity_VCA { get; set; }

        public decimal? total_BMV { get; set; }
        public decimal? total_DLI { get; set; }
        public decimal? total_PQC { get; set; }
        public decimal? total_SGN { get; set; }
        public decimal? total_VCA { get; set; }
        // CQ
        public decimal? quantity_VPCT { get; set; }
        public decimal? quantity_MB { get; set; }
        public decimal? quantity_MT { get; set; }
        public decimal? quantity_MN { get; set; }
        public decimal? quantity_CR { get; set; }
        public decimal? quantity_VCS { get; set; }
        public decimal? total_VCS { get; set; }
        public decimal? total_VPCT { get; set; }
        public decimal? total_MB { get; set; }
        public decimal? total_MT { get; set; }
        public decimal? total_MN { get; set; }
        public decimal? total_CR { get; set; }
        //VT

        public decimal? quantity_VTMB { get; set; }
        public decimal? quantity_VTMN { get; set; }
        public decimal? quantity_VTMT { get; set; }
        public decimal? total_VTMB { get; set; }
        public decimal? total_VTMN { get; set; }
        public decimal? total_VTMT { get; set; }
        public bool isBold { get; set; }

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

    public class ViewDataYearDoanhThuModel
    {
        public string SAN_BAY_CODE { get; set; }
        public string VN { get; set; }
        public string OV { get; set; }
        public string BL { get; set; }
        public string VJ { get; set; }
        public string QH { get; set; }
        public string VU { get; set; }
        public string OTHER_HKTN { get; set; }
        public string HKNN { get; set; }
        public string TOTAL { get; set; }

    }
}
