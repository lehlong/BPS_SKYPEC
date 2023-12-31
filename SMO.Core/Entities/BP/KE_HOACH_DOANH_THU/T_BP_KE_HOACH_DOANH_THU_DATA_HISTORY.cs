﻿using SMO.Core.Entities.MD;

namespace SMO.Core.Entities.BP.KE_HOACH_DOANH_THU   
{
    public partial class T_BP_KE_HOACH_DOANH_THU_DATA_HISTORY : BaseEntity
    {
        public virtual string PKID { get; set; }
        public virtual string ORG_CODE { get; set; }
        public virtual string DOANH_THU_PROFIT_CENTER_CODE { get; set; }
        public virtual string TEMPLATE_CODE { get; set; }
        public virtual string KHOAN_MUC_DOANH_THU_CODE { get; set; }
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

        public virtual T_MD_KHOAN_MUC_DOANH_THU KhoanMucDoanhThu { get; set; }
        public virtual T_MD_DOANH_THU_PROFIT_CENTER DoanhThuProfitCenter { get; set; }
        public virtual T_MD_COST_CENTER Organize { get; set; }
        public virtual T_MD_TEMPLATE Template { get; set; }


        public static explicit operator T_BP_KE_HOACH_DOANH_THU_DATA(T_BP_KE_HOACH_DOANH_THU_DATA_HISTORY history)
        {
            return new T_BP_KE_HOACH_DOANH_THU_DATA
            {
                PKID = history.PKID,
                ORG_CODE = history.ORG_CODE,
                DOANH_THU_PROFIT_CENTER_CODE = history.DOANH_THU_PROFIT_CENTER_CODE,
                TEMPLATE_CODE = history.TEMPLATE_CODE,
                KHOAN_MUC_DOANH_THU_CODE = history.KHOAN_MUC_DOANH_THU_CODE,
                VERSION = history.VERSION,
                TIME_YEAR = history.TIME_YEAR,
                VALUE_APR = history.VALUE_APR,
                VALUE_AUG = history.VALUE_AUG,
                VALUE_DEC = history.VALUE_DEC,
                VALUE_FEB = history.VALUE_FEB,
                VALUE_JAN = history.VALUE_JAN,
                VALUE_JUL = history.VALUE_JUL,
                VALUE_JUN = history.VALUE_JUN,
                VALUE_MAR = history.VALUE_MAR,
                VALUE_MAY = history.VALUE_MAY,
                VALUE_NOV = history.VALUE_NOV,
                VALUE_OCT = history.VALUE_OCT,
                VALUE_SEP = history.VALUE_SEP,

                VALUE_SUM_YEAR = history.VALUE_SUM_YEAR,
                VALUE_SUM_YEAR_PREVENTIVE = history.VALUE_SUM_YEAR_PREVENTIVE,

                DESCRIPTION = history.DESCRIPTION,
                ACTIVE = history.ACTIVE,
                CREATE_BY = history.CREATE_BY,
                CREATE_DATE = history.CREATE_DATE,
                UPDATE_BY = history.UPDATE_BY,
                UPDATE_DATE = history.UPDATE_DATE,
                USER_CREATE = history.USER_CREATE,
                USER_UPDATE = history.USER_UPDATE,
                KhoanMucDoanhThu = history.KhoanMucDoanhThu,
                DoanhThuProfitCenter = history.DoanhThuProfitCenter,
                STATUS = history.STATUS,
                Template = history.Template,
                Organize = history.Organize
            };
        }

        public static implicit operator T_BP_KE_HOACH_DOANH_THU_DATA_HISTORY(T_BP_KE_HOACH_DOANH_THU_DATA data)
        {
            return new T_BP_KE_HOACH_DOANH_THU_DATA_HISTORY
            {
                PKID = data.PKID,
                ORG_CODE = data.ORG_CODE,
                DOANH_THU_PROFIT_CENTER_CODE = data.DOANH_THU_PROFIT_CENTER_CODE,
                TEMPLATE_CODE = data.TEMPLATE_CODE,
                KHOAN_MUC_DOANH_THU_CODE = data.KHOAN_MUC_DOANH_THU_CODE,
                VERSION = data.VERSION,
                TIME_YEAR = data.TIME_YEAR,
                VALUE_APR = data.VALUE_APR,
                VALUE_AUG = data.VALUE_AUG,
                VALUE_DEC = data.VALUE_DEC,
                VALUE_FEB = data.VALUE_FEB,
                VALUE_JAN = data.VALUE_JAN,
                VALUE_JUL = data.VALUE_JUL,
                VALUE_JUN = data.VALUE_JUN,
                VALUE_MAR = data.VALUE_MAR,
                VALUE_MAY = data.VALUE_MAY,
                VALUE_NOV = data.VALUE_NOV,
                VALUE_OCT = data.VALUE_OCT,
                VALUE_SEP = data.VALUE_SEP,

                VALUE_SUM_YEAR = data.VALUE_SUM_YEAR,
                VALUE_SUM_YEAR_PREVENTIVE = data.VALUE_SUM_YEAR_PREVENTIVE,

                DESCRIPTION = data.DESCRIPTION,
                ACTIVE = data.ACTIVE,
                CREATE_BY = data.CREATE_BY,
                CREATE_DATE = data.CREATE_DATE,
                UPDATE_BY = data.UPDATE_BY,
                UPDATE_DATE = data.UPDATE_DATE,
                USER_CREATE = data.USER_CREATE,
                USER_UPDATE = data.USER_UPDATE,
                KhoanMucDoanhThu = data.KhoanMucDoanhThu,
                DoanhThuProfitCenter = data.DoanhThuProfitCenter,
                STATUS = data.STATUS,
                Organize = data.Organize,
                Template = data.Template
            };
        }
    }
}
