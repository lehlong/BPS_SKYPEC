using SMO.Core.Entities.MD;

namespace SMO.Core.Entities.BP.KE_HOACH_CHI_PHI
{
    public partial class T_BP_KE_HOACH_CHI_PHI_DATA : BaseEntity
    {
        public virtual string PKID { get; set; }
        public virtual string ORG_CODE { get; set; }
        public virtual string CHI_PHI_PROFIT_CENTER_CODE { get; set; }
        public virtual string TEMPLATE_CODE { get; set; }
        public virtual string KHOAN_MUC_HANG_HOA_CODE { get; set; }
        public virtual int VERSION { get; set; }
        public virtual int TIME_YEAR { get; set; }
        public virtual int MONTH { get; set; }

        public virtual decimal? QUANTITY { get; set; }
        public virtual decimal? PRICE { get; set; }
        public virtual decimal? AMOUNT { get; set; }
        public virtual string QUY_MO { get; set; }

        public virtual string DESCRIPTION { get; set; }

        public virtual decimal? QUANTITY_TD { get; set; }
        public virtual decimal? PRICE_TD { get; set; }
        public virtual decimal? AMOUNT_TD { get; set; }
        public virtual string DESCRIPTION_TD { get; set; }
        public virtual string STATUS { get; set; }

        public virtual T_MD_TEMPLATE Template { get; set; }
        public virtual T_MD_KHOAN_MUC_HANG_HOA KhoanMucHangHoa { get; set; }
        public virtual T_MD_CHI_PHI_PROFIT_CENTER ChiPhiProfitCenter { get; set; }
        public virtual T_MD_COST_CENTER Organize { get; set; }

    }
}
