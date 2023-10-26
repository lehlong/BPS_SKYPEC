using SMO.Core.Entities.MD;

namespace SMO.Core.Entities.BP.DAU_TU_NGOAI_DOANH_NGHIEP
{
    public partial class T_BP_DAU_TU_NGOAI_DOANH_NGHIEP_DATA : BaseEntity
    {
        public virtual string PKID { get; set; }
        public virtual string ORG_CODE { get; set; }
        public virtual string DAU_TU_PROFIT_CENTER_CODE { get; set; }
        public virtual string TEMPLATE_CODE { get; set; }
        public virtual string KHOAN_MUC_DAU_TU_CODE { get; set; }
        public virtual int VERSION { get; set; }
        public virtual int TIME_YEAR { get; set; }
        public virtual decimal? VALUE_1 { get; set; }
        public virtual decimal? VALUE_2 { get; set; }
        public virtual decimal? VALUE_3 { get; set; }
        public virtual decimal? VALUE_4 { get; set; }
        public virtual decimal? VALUE_5 { get; set; }
        public virtual decimal? VALUE_6 { get; set; }
        
        public virtual string DESCRIPTION { get; set; }
        public virtual string PROCESS { get; set; }
        public virtual string STATUS { get; set; }

        public virtual T_MD_TEMPLATE Template { get; set; }
        public virtual T_MD_KHOAN_MUC_DAU_TU KhoanMucDauTu { get; set; }
        public virtual T_MD_DAU_TU_NGOAI_DOANH_NGHIEP_PROFIT_CENTER DauTuNgoaiDoanhNghiepProfitCenter { get; set; }
        public virtual T_MD_COST_CENTER Organize { get; set; }

    }
}
