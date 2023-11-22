using SMO.Core.Entities.MD;

namespace SMO.Core.Entities.BP.SUA_CHUA_LON
{
    public partial class T_BP_SUA_CHUA_LON_DATA : BaseEntity
    {
        public virtual string PKID { get; set; }
        public virtual string ORG_CODE { get; set; }
        public virtual string SUA_CHUA_PROFIT_CENTER_CODE { get; set; }
        public virtual string TEMPLATE_CODE { get; set; }
        public virtual string KHOAN_MUC_SUA_CHUA_CODE { get; set; }
        public virtual int VERSION { get; set; }
        public virtual int TIME_YEAR { get; set; }
        public virtual decimal? VALUE { get; set; }   
        public virtual string QUY_MO { get; set; }

        public virtual string DESCRIPTION { get; set; }
        public virtual string STATUS { get; set; }

        public virtual T_MD_TEMPLATE Template { get; set; }
        public virtual T_MD_KHOAN_MUC_SUA_CHUA KhoanMucSuaChua { get; set; }
        public virtual T_MD_SUA_CHUA_PROFIT_CENTER SuaChuaProfitCenter { get; set; }
        public virtual T_MD_COST_CENTER Organize { get; set; }

    }
}
