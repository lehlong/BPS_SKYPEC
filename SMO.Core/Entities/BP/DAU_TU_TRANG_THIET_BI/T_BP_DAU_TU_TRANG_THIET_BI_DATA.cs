using SMO.Core.Entities.MD;

namespace SMO.Core.Entities.BP.DAU_TU_TRANG_THIET_BI
{
    public partial class T_BP_DAU_TU_TRANG_THIET_BI_DATA : BaseEntity
    {
        public virtual string PKID { get; set; }
        public virtual string ORG_CODE { get; set; }
        public virtual string DAU_TU_PROFIT_CENTER_CODE { get; set; }
        public virtual string TEMPLATE_CODE { get; set; }
        public virtual string KHOAN_MUC_DAU_TU_CODE { get; set; }
        public virtual int VERSION { get; set; }
        public virtual int TIME_YEAR { get; set; }
        public virtual decimal? VALUE { get; set; }    
        public virtual string DESCRIPTION { get; set; }
        public virtual string PROCESS { get; set; }
        public virtual string EQUITY_SOURCES { get; set; }
        public virtual string TDTK { get; set; }
        public virtual string QKH { get; set; }
        public virtual string STATUS { get; set; }

        public virtual T_MD_TEMPLATE Template { get; set; }
        public virtual T_MD_KHOAN_MUC_DAU_TU KhoanMucDauTu { get; set; }
        public virtual T_MD_DAU_TU_TRANG_THIET_BI_PROFIT_CENTER DauTuTrangThietBiProfitCenter { get; set; }
        public virtual T_MD_COST_CENTER Organize { get; set; }

    }
}
