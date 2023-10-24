﻿using SMO.Core.Entities.MD;

using System.Collections.Generic;

namespace SMO.Core.Entities.BP.DAU_TU_XAY_DUNG
{
    public class T_BP_DAU_TU_XAY_DUNG_REVIEW_COMMENT : BaseEntity
    {
        public virtual string PKID { get; set; }
        public virtual string ORG_CODE { get; set; }
        public virtual string KHOAN_MUC_DAU_TU_CODE { get; set; }
        public virtual int TIME_YEAR { get; set; }
        public virtual int DATA_VERSION { get; set; }
        /// <summary>
        /// Comment trên org code nào. Phục vụ cho việc comment chi tiết khi drill down
        /// </summary>
        public virtual string ON_ORG_CODE { get; set; }
        public virtual int NUMBER_COMMENTS { get; set; }

        public virtual string CONTENT { get; set; }

        public virtual T_MD_COST_CENTER Organize { get; set; }
        public virtual T_MD_COST_CENTER OnCostCenter { get; set; }
        public virtual T_MD_KHOAN_MUC_DAU_TU KhoanMucDauTu { get; set; }
        public virtual IList<T_CM_COMMENT> Comments { get; set; }

    }
}
