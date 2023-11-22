using SMO.Core.Entities.MD;

namespace SMO.Core.Entities.BP.KE_HOACH_VAN_CHUYEN.KE_HOACH_VAN_CHUYEN_DATA_BASE
{
    public class T_BP_KE_HOACH_VAN_CHUYEN_DATA_BASE_HISTORY : BaseEntity
    {
        public virtual string PKID { get; set; }

        public virtual string ORG_CODE { get; set; }
        public virtual string VAN_CHUYEN_PROFIT_CENTER_CODE { get; set; }
        public virtual string TEMPLATE_CODE { get; set; }
        public virtual string KHOAN_MUC_VAN_CHUYEN_CODE { get; set; }
        public virtual int VERSION { get; set; }
        public virtual int TIME_YEAR { get; set; }
        public virtual string MATERIAL { get; set; }
        public virtual string UNIT { get; set; }

        public virtual decimal QUANTITY { get; set; }
        public virtual string TIME { get; set; }
        public virtual decimal PRICE { get; set; }
        public virtual decimal AMOUNT { get; set; }

        public virtual string DESCRIPTION { get; set; }
        public virtual string PROCESS { get; set; }

        public virtual T_MD_TEMPLATE Template { get; set; }
        public virtual T_MD_KHOAN_MUC_VAN_CHUYEN KhoanMucDauTu { get; set; }
        public virtual T_MD_VAN_CHUYEN_PROFIT_CENTER VanChuyenProfitCenter { get; set; }
        public virtual T_MD_COST_CENTER Organize { get; set; }

        public static explicit operator T_BP_KE_HOACH_VAN_CHUYEN_DATA_BASE_HISTORY(T_BP_KE_HOACH_VAN_CHUYEN_DATA_BASE v)
        {
            return new T_BP_KE_HOACH_VAN_CHUYEN_DATA_BASE_HISTORY
            {
                PKID = v.PKID,
                ORG_CODE = v.ORG_CODE,
                VAN_CHUYEN_PROFIT_CENTER_CODE = v.VAN_CHUYEN_PROFIT_CENTER_CODE,
                TEMPLATE_CODE = v.TEMPLATE_CODE,
                KHOAN_MUC_VAN_CHUYEN_CODE = v.KHOAN_MUC_VAN_CHUYEN_CODE,
                VERSION = v.VERSION,
                TIME_YEAR = v.TIME_YEAR,
                MATERIAL = v.MATERIAL,
                UNIT = v.UNIT,

                QUANTITY = v.QUANTITY,
                PRICE = v.PRICE,
                AMOUNT = v.AMOUNT,
                TIME = v.TIME,
                DESCRIPTION = v.DESCRIPTION,
                PROCESS = v.PROCESS,
            };
        }
    }
}
