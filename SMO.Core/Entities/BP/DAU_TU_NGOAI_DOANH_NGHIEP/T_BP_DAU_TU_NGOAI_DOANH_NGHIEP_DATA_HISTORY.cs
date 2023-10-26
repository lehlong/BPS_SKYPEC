using SMO.Core.Entities.MD;

namespace SMO.Core.Entities.BP.DAU_TU_NGOAI_DOANH_NGHIEP  
{
    public partial class T_BP_DAU_TU_NGOAI_DOANH_NGHIEP_DATA_HISTORY : BaseEntity
    {
        public virtual string PKID { get; set; }
        public virtual string ORG_CODE { get; set; }
        public virtual string DAU_TU_PROFIT_CENTER_CODE { get; set; }
        public virtual string TEMPLATE_CODE { get; set; }
        public virtual string KHOAN_MUC_DAU_TU_CODE { get; set; }
        public virtual int VERSION { get; set; }
        public virtual int TIME_YEAR { get; set; }
        public virtual decimal? VALUE_1 { get; set; }
        public virtual decimal? VALUE_2 { get; set; }
        public virtual decimal? VALUE_3 { get; set; }
        public virtual decimal? VALUE_4 { get; set; }
        public virtual decimal? VALUE_5 { get; set; }
        public virtual decimal? VALUE_6 { get; set; }
        public virtual string DESCRIPTION { get; set; }
        public virtual string PROCESS { get; set; }
        public virtual string STATUS { get; set; }

        public virtual T_MD_KHOAN_MUC_DAU_TU KhoanMucDauTu { get; set; }
        public virtual T_MD_DAU_TU_NGOAI_DOANH_NGHIEP_PROFIT_CENTER DauTuNgoaiDoanhNghiepProfitCenter { get; set; }
        public virtual T_MD_COST_CENTER Organize { get; set; }
        public virtual T_MD_TEMPLATE Template { get; set; }


        public static explicit operator T_BP_DAU_TU_NGOAI_DOANH_NGHIEP_DATA(T_BP_DAU_TU_NGOAI_DOANH_NGHIEP_DATA_HISTORY history)
        {
            return new T_BP_DAU_TU_NGOAI_DOANH_NGHIEP_DATA
            {
                PKID = history.PKID,
                ORG_CODE = history.ORG_CODE,
                DAU_TU_PROFIT_CENTER_CODE = history.DAU_TU_PROFIT_CENTER_CODE,
                TEMPLATE_CODE = history.TEMPLATE_CODE,
                KHOAN_MUC_DAU_TU_CODE = history.KHOAN_MUC_DAU_TU_CODE,
                VERSION = history.VERSION,
                TIME_YEAR = history.TIME_YEAR,
                VALUE_1 = history.VALUE_1,
                VALUE_2 = history.VALUE_2,
                VALUE_3 = history.VALUE_3,
                VALUE_4 = history.VALUE_4,
                VALUE_5 = history.VALUE_5,
                VALUE_6 = history.VALUE_6,

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
                DauTuNgoaiDoanhNghiepProfitCenter = history.DauTuNgoaiDoanhNghiepProfitCenter,
                STATUS = history.STATUS,
                Template = history.Template,
                Organize = history.Organize
            };
        }

        public static implicit operator T_BP_DAU_TU_NGOAI_DOANH_NGHIEP_DATA_HISTORY(T_BP_DAU_TU_NGOAI_DOANH_NGHIEP_DATA history)
        {
            return new T_BP_DAU_TU_NGOAI_DOANH_NGHIEP_DATA_HISTORY
            {
                PKID = history.PKID,
                ORG_CODE = history.ORG_CODE,
                DAU_TU_PROFIT_CENTER_CODE = history.DAU_TU_PROFIT_CENTER_CODE,
                TEMPLATE_CODE = history.TEMPLATE_CODE,
                KHOAN_MUC_DAU_TU_CODE = history.KHOAN_MUC_DAU_TU_CODE,
                VERSION = history.VERSION,
                TIME_YEAR = history.TIME_YEAR,
                VALUE_1 = history.VALUE_1,
                VALUE_2 = history.VALUE_2,
                VALUE_3 = history.VALUE_3,
                VALUE_4 = history.VALUE_4,
                VALUE_5 = history.VALUE_5,
                VALUE_6 = history.VALUE_6,

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
                DauTuNgoaiDoanhNghiepProfitCenter = history.DauTuNgoaiDoanhNghiepProfitCenter,
                STATUS = history.STATUS,
                Template = history.Template,
                Organize = history.Organize
            };
        }
    }
}
