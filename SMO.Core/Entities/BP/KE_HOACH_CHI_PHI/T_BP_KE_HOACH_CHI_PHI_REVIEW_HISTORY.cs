﻿using SMO.Core.Entities.MD;

using System;

namespace SMO.Core.Entities.BP.KE_HOACH_CHI_PHI
{
    public class T_BP_KE_HOACH_CHI_PHI_REVIEW_HISTORY : BaseEntity
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
