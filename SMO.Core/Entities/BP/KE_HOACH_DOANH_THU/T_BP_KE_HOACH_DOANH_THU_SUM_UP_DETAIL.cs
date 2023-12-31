﻿using SMO.Core.Entities.MD;

namespace SMO.Core.Entities.BP.KE_HOACH_DOANH_THU
{
    public class T_BP_KE_HOACH_DOANH_THU_SUM_UP_DETAIL : BaseEntity
    {
        public virtual string PKID { get; set; }
        public virtual string ORG_CODE { get; set; }
        public virtual string FROM_ORG_CODE { get; set; }
        public virtual string TEMPLATE_CODE { get; set; }
        public virtual int TIME_YEAR { get; set; }
        public virtual int SUM_UP_VERSION { get; set; }
        public virtual int DATA_VERSION { get; set; }
        public virtual T_MD_COST_CENTER CostCenter { get; set; }
        public virtual T_MD_COST_CENTER FromCostCenter { get; set; }
        public virtual T_MD_TEMPLATE Template { get; set; }

    }
}
