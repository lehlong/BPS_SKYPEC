﻿using SMO.Core.Entities.MD;

namespace SMO.Core.Entities.BP.KE_HOACH_SAN_LUONG
{
    public partial class T_BP_KE_HOACH_SAN_LUONG_DATA : BaseEntity
    {
        public virtual string PKID { get; set; }
        public virtual string ORG_CODE { get; set; }
        public virtual string SAN_LUONG_PROFIT_CENTER_CODE { get; set; }
        public virtual string TEMPLATE_CODE { get; set; }
        public virtual string KHOAN_MUC_SAN_LUONG_CODE { get; set; }
        public virtual int VERSION { get; set; }
        public virtual int TIME_YEAR { get; set; }
        public virtual decimal? VALUE_JAN { get; set; }
        public virtual decimal? VALUE_FEB { get; set; }
        public virtual decimal? VALUE_MAR { get; set; }
        public virtual decimal? VALUE_APR { get; set; }
        public virtual decimal? VALUE_MAY { get; set; }
        public virtual decimal? VALUE_JUN { get; set; }
        public virtual decimal? VALUE_JUL { get; set; }
        public virtual decimal? VALUE_AUG { get; set; }
        public virtual decimal? VALUE_SEP { get; set; }
        public virtual decimal? VALUE_OCT { get; set; }
        public virtual decimal? VALUE_NOV { get; set; }
        public virtual decimal? VALUE_DEC { get; set; }
        #region Ngân sách dự phòng
        public virtual decimal? VALUE_SUM_YEAR_PREVENTIVE { get; set; }
        public virtual decimal? VALUE_SUM_YEAR { get; set; }

        #endregion
        public virtual string DESCRIPTION { get; set; }
        public virtual string STATUS { get; set; }

        public virtual T_MD_TEMPLATE Template { get; set; }
        public virtual T_MD_KHOAN_MUC_SAN_LUONG KhoanMucSanLuong { get; set; }
        public virtual T_MD_SAN_LUONG_PROFIT_CENTER SanLuongProfitCenter { get; set; }
        public virtual T_MD_COST_CENTER Organize { get; set; }

    }
}
