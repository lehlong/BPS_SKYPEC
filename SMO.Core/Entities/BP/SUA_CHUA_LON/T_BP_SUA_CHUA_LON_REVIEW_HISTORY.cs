using SMO.Core.Entities.MD;

using System;

namespace SMO.Core.Entities.BP.SUA_CHUA_LON
{
    public class T_BP_SUA_CHUA_LON_REVIEW_HISTORY : BaseEntity
    {
        public virtual string PKID { get; set; }
        public virtual string ORG_CODE { get; set; }
        public virtual int TIME_YEAR { get; set; }
        public virtual int DATA_VERSION { get; set; }
        public virtual string REVIEW_USER { get; set; }
        public virtual DateTime REVIEW_DATE { get; set; }

        public virtual T_MD_COST_CENTER Organize { get; set; }
        public virtual T_AD_USER UserReview { get; set; }
    }
}
