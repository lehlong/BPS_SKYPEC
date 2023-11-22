using SMO.Core.Entities.MD;

namespace SMO.Core.Entities.BP.KE_HOACH_VAN_CHUYEN
{
    public partial class T_BP_KE_HOACH_VAN_CHUYEN_DATA : BaseEntity
    {
        public virtual string PKID { get; set; }
        public virtual string ORG_CODE { get; set; }
        public virtual string VAN_CHUYEN_PROFIT_CENTER_CODE { get; set; }
        public virtual string TEMPLATE_CODE { get; set; }
        public virtual string KHOAN_MUC_VAN_CHUYEN_CODE { get; set; }
        public virtual int VERSION { get; set; }
        public virtual int TIME_YEAR { get; set; }
        public virtual decimal? VALUE { get; set; }    
        public virtual string DESCRIPTION { get; set; }
        public virtual string PROCESS { get; set; }
        public virtual string STATUS { get; set; }

        public virtual T_MD_TEMPLATE Template { get; set; }
        public virtual T_MD_KHOAN_MUC_VAN_CHUYEN KhoanMucVanChuyen { get; set; }
        public virtual T_MD_VAN_CHUYEN_PROFIT_CENTER VanChuyenProfitCenter { get; set; }
        public virtual T_MD_COST_CENTER Organize { get; set; }

    }
}
