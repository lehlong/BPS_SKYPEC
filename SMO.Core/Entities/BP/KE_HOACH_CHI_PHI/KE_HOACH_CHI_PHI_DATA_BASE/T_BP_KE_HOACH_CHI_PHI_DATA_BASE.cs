using SMO.Core.Entities.MD;
using System.Collections.Generic;

namespace SMO.Core.Entities.BP.KE_HOACH_CHI_PHI.KE_HOACH_CHI_PHI_DATA_BASE
{
    public class T_BP_KE_HOACH_CHI_PHI_DATA_BASE : BaseEntity
    {
        public virtual string PKID { get; set; }
        public virtual string ORG_CODE { get; set; }
        public virtual string COST_CENTER_CODE { get; set; }
        public virtual string SAN_BAY_CODE { get; set; }
        public virtual string CHI_PHI_PROFIT_CENTER_CODE { get; set; }
        public virtual string TEMPLATE_CODE { get; set; }
        public virtual string KHOAN_MUC_HANG_HOA_CODE { get; set; }
        public virtual int VERSION { get; set; }
        public virtual int TIME_YEAR { get; set; }
        public virtual string MATERIAL { get; set; }
        public virtual string UNIT { get; set; }

        public virtual decimal QUANTITY { get; set; }
        public virtual string TIME { get; set; }
        public virtual decimal PRICE { get; set; }
        public virtual decimal AMOUNT { get; set; }

        public virtual string QUY_MO { get; set; }

        public virtual string DESCRIPTION { get; set; }

        public virtual T_MD_TEMPLATE Template { get; set; }
        public virtual T_MD_KHOAN_MUC_HANG_HOA KhoanMucHangHoa { get; set; }
        public virtual T_MD_CHI_PHI_PROFIT_CENTER ChiPhiProfitCenter { get; set; }
        public virtual T_MD_SAN_BAY SanBay { get; set; }
        public virtual T_MD_COST_CENTER CostCenter { get; set; }
        public virtual T_MD_COST_CENTER Organize { get; set; }
    }
}
