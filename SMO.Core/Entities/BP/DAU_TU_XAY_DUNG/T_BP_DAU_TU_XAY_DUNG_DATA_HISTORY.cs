using SMO.Core.Entities.MD;

namespace SMO.Core.Entities.BP.DAU_TU_XAY_DUNG   
{
    public partial class T_BP_DAU_TU_XAY_DUNG_DATA_HISTORY : BaseEntity
    {
        public virtual string PKID { get; set; }
        public virtual string ORG_CODE { get; set; }
        public virtual string DAU_TU_PROFIT_CENTER_CODE { get; set; }
        public virtual string TEMPLATE_CODE { get; set; }
        public virtual string KHOAN_MUC_DAU_TU_CODE { get; set; }
        public virtual int VERSION { get; set; }
        public virtual int TIME_YEAR { get; set; }
        public virtual decimal? VALUE { get; set; }
        public virtual string DESCRIPTION { get; set; }
        public virtual string PROCESS { get; set; }
        public virtual string STATUS { get; set; }
        public virtual string TDTK { get; set; }
        public virtual string EQUITY_SOURCES { get; set; }


        public virtual T_MD_KHOAN_MUC_DAU_TU KhoanMucDauTu { get; set; }
        public virtual T_MD_DAU_TU_XAY_DUNG_PROFIT_CENTER DauTuXayDungProfitCenter { get; set; }
        public virtual T_MD_COST_CENTER Organize { get; set; }
        public virtual T_MD_TEMPLATE Template { get; set; }


        public static explicit operator T_BP_DAU_TU_XAY_DUNG_DATA(T_BP_DAU_TU_XAY_DUNG_DATA_HISTORY history)
        {
            return new T_BP_DAU_TU_XAY_DUNG_DATA
            {
                PKID = history.PKID,
                ORG_CODE = history.ORG_CODE,
                DAU_TU_PROFIT_CENTER_CODE = history.DAU_TU_PROFIT_CENTER_CODE,
                TEMPLATE_CODE = history.TEMPLATE_CODE,
                KHOAN_MUC_DAU_TU_CODE = history.KHOAN_MUC_DAU_TU_CODE,
                VERSION = history.VERSION,
                TIME_YEAR = history.TIME_YEAR,
                VALUE = history.VALUE,
                EQUITY_SOURCES = history.EQUITY_SOURCES,
                TDTK = history.TDTK,
                DESCRIPTION = history.DESCRIPTION,
                PROCESS = history.PROCESS,
                ACTIVE = history.ACTIVE,
                CREATE_BY = history.CREATE_BY,
                CREATE_DATE = history.CREATE_DATE,
                UPDATE_BY = history.UPDATE_BY,
                UPDATE_DATE = history.UPDATE_DATE,
                USER_CREATE = history.USER_CREATE,
                USER_UPDATE = history.USER_UPDATE,
                KhoanMucDauTu = history.KhoanMucDauTu,
                DauTuXayDungProfitCenter = history.DauTuXayDungProfitCenter,
                STATUS = history.STATUS,
                Template = history.Template,
                Organize = history.Organize
            };
        }

        public static implicit operator T_BP_DAU_TU_XAY_DUNG_DATA_HISTORY(T_BP_DAU_TU_XAY_DUNG_DATA data)
        {
            return new T_BP_DAU_TU_XAY_DUNG_DATA_HISTORY
            {
                PKID = data.PKID,
                ORG_CODE = data.ORG_CODE,
                DAU_TU_PROFIT_CENTER_CODE = data.DAU_TU_PROFIT_CENTER_CODE,
                TEMPLATE_CODE = data.TEMPLATE_CODE,
                KHOAN_MUC_DAU_TU_CODE = data.KHOAN_MUC_DAU_TU_CODE,
                VERSION = data.VERSION,
                TIME_YEAR = data.TIME_YEAR,
                VALUE = data.VALUE,
                EQUITY_SOURCES = data.EQUITY_SOURCES,
                TDTK = data.TDTK,
                DESCRIPTION = data.DESCRIPTION,
                PROCESS = data.PROCESS,
                ACTIVE = data.ACTIVE,
                CREATE_BY = data.CREATE_BY,
                CREATE_DATE = data.CREATE_DATE,
                UPDATE_BY = data.UPDATE_BY,
                UPDATE_DATE = data.UPDATE_DATE,
                USER_CREATE = data.USER_CREATE,
                USER_UPDATE = data.USER_UPDATE,
                KhoanMucDauTu = data.KhoanMucDauTu,
                DauTuXayDungProfitCenter = data.DauTuXayDungProfitCenter,
                STATUS = data.STATUS,
                Organize = data.Organize,
                Template = data.Template
            };
        }
    }
}
