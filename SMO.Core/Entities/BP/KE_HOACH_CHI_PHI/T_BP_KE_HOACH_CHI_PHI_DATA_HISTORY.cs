using SMO.Core.Entities.MD;

namespace SMO.Core.Entities.BP.KE_HOACH_CHI_PHI
{
    public partial class T_BP_KE_HOACH_CHI_PHI_DATA_HISTORY : BaseEntity
    {
        public virtual string PKID { get; set; }
        public virtual string ORG_CODE { get; set; }
        public virtual string CHI_PHI_PROFIT_CENTER_CODE { get; set; }
        public virtual string TEMPLATE_CODE { get; set; }
        public virtual string KHOAN_MUC_HANG_HOA_CODE { get; set; }
        public virtual int VERSION { get; set; }
        public virtual int TIME_YEAR { get; set; }
        public virtual decimal? QUANTITY { get; set; }
        public virtual decimal? PRICE { get; set; }
        public virtual decimal? AMOUNT { get; set; }
        public virtual string QUY_MO { get; set; }
        public virtual string DESCRIPTION { get; set; }
        public virtual string STATUS { get; set; }

        public virtual decimal? QUANTITY_TD { get; set; }
        public virtual decimal? PRICE_TD { get; set; }
        public virtual decimal? AMOUNT_TD { get; set; }
        public virtual string DESCRIPTION_TD { get; set; }

        public virtual T_MD_KHOAN_MUC_HANG_HOA KhoanMucHangHoa { get; set; }
        public virtual T_MD_CHI_PHI_PROFIT_CENTER ChiPhiProfitCenter { get; set; }
        public virtual T_MD_COST_CENTER Organize { get; set; }
        public virtual T_MD_TEMPLATE Template { get; set; }


        public static explicit operator T_BP_KE_HOACH_CHI_PHI_DATA(T_BP_KE_HOACH_CHI_PHI_DATA_HISTORY history)
        {
            return new T_BP_KE_HOACH_CHI_PHI_DATA
            {
                PKID = history.PKID,
                ORG_CODE = history.ORG_CODE,
                CHI_PHI_PROFIT_CENTER_CODE = history.CHI_PHI_PROFIT_CENTER_CODE,
                TEMPLATE_CODE = history.TEMPLATE_CODE,
                KHOAN_MUC_HANG_HOA_CODE = history.KHOAN_MUC_HANG_HOA_CODE,
                VERSION = history.VERSION,
                TIME_YEAR = history.TIME_YEAR,
                QUANTITY = history.QUANTITY,
                PRICE = history.PRICE,
                AMOUNT = history.AMOUNT,

                DESCRIPTION = history.DESCRIPTION,
                ACTIVE = history.ACTIVE,
                CREATE_BY = history.CREATE_BY,
                CREATE_DATE = history.CREATE_DATE,
                UPDATE_BY = history.UPDATE_BY,
                UPDATE_DATE = history.UPDATE_DATE,
                USER_CREATE = history.USER_CREATE,
                USER_UPDATE = history.USER_UPDATE,
                KhoanMucHangHoa = history.KhoanMucHangHoa,
                ChiPhiProfitCenter = history.ChiPhiProfitCenter,
                STATUS = history.STATUS,
                Template = history.Template,
                Organize = history.Organize
            };
        }

        public static implicit operator T_BP_KE_HOACH_CHI_PHI_DATA_HISTORY(T_BP_KE_HOACH_CHI_PHI_DATA data)
        {
            return new T_BP_KE_HOACH_CHI_PHI_DATA_HISTORY
            {
                PKID = data.PKID,
                ORG_CODE = data.ORG_CODE,
                CHI_PHI_PROFIT_CENTER_CODE = data.CHI_PHI_PROFIT_CENTER_CODE,
                TEMPLATE_CODE = data.TEMPLATE_CODE,
                KHOAN_MUC_HANG_HOA_CODE = data.KHOAN_MUC_HANG_HOA_CODE,
                VERSION = data.VERSION,
                TIME_YEAR = data.TIME_YEAR,
                QUANTITY = data.QUANTITY,
                PRICE = data.PRICE,
                AMOUNT = data.AMOUNT,

                DESCRIPTION = data.DESCRIPTION,
                ACTIVE = data.ACTIVE,
                CREATE_BY = data.CREATE_BY,
                CREATE_DATE = data.CREATE_DATE,
                UPDATE_BY = data.UPDATE_BY,
                UPDATE_DATE = data.UPDATE_DATE,
                USER_CREATE = data.USER_CREATE,
                USER_UPDATE = data.USER_UPDATE,
                KhoanMucHangHoa = data.KhoanMucHangHoa,
                ChiPhiProfitCenter = data.ChiPhiProfitCenter,
                STATUS = data.STATUS,
                Organize = data.Organize,
                Template = data.Template,

                QUANTITY_TD = data.QUANTITY_TD,
                PRICE_TD = data.PRICE_TD,
                AMOUNT_TD = data.AMOUNT_TD,
                DESCRIPTION_TD = data.DESCRIPTION_TD,
            };
        }
    }
}
