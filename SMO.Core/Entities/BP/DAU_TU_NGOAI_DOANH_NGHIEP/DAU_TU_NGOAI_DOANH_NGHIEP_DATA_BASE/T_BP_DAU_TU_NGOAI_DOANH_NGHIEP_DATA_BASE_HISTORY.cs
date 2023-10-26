using SMO.Core.Entities.MD;

namespace SMO.Core.Entities.BP.DAU_TU_NGOAI_DOANH_NGHIEP.DAU_TU_NGOAI_DOANH_NGHIEP_DATA_BASE
{
    public class T_BP_DAU_TU_NGOAI_DOANH_NGHIEP_DATA_BASE_HISTORY : BaseEntity
    {
        public virtual string PKID { get; set; }

        public virtual string ORG_CODE { get; set; }
        public virtual string DAU_TU_PROFIT_CENTER_CODE { get; set; }
        public virtual string TEMPLATE_CODE { get; set; }
        public virtual string KHOAN_MUC_DAU_TU_CODE { get; set; }
        public virtual int VERSION { get; set; }
        public virtual int TIME_YEAR { get; set; }
        public virtual string MATERIAL { get; set; }
        public virtual string UNIT { get; set; }

        public virtual decimal QUANTITY_1 { get; set; }
        public virtual string TIME_1 { get; set; }
        public virtual decimal PRICE_1 { get; set; }
        public virtual decimal AMOUNT_1 { get; set; }

        public virtual decimal QUANTITY_2 { get; set; }
        public virtual string TIME_2 { get; set; }
        public virtual decimal PRICE_2 { get; set; }
        public virtual decimal AMOUNT_2 { get; set; }

        public virtual decimal QUANTITY_3 { get; set; }
        public virtual string TIME_3 { get; set; }
        public virtual decimal PRICE_3 { get; set; }
        public virtual decimal AMOUNT_3 { get; set; }

        public virtual decimal QUANTITY_4 { get; set; }
        public virtual string TIME_4 { get; set; }
        public virtual decimal PRICE_4 { get; set; }
        public virtual decimal AMOUNT_4 { get; set; }

        public virtual decimal QUANTITY_5 { get; set; }
        public virtual string TIME_5 { get; set; }
        public virtual decimal PRICE_5 { get; set; }
        public virtual decimal AMOUNT_5 { get; set; }

        public virtual decimal QUANTITY_6 { get; set; }
        public virtual string TIME_6 { get; set; }
        public virtual decimal PRICE_6 { get; set; }
        public virtual decimal AMOUNT_6 { get; set; }

        public virtual string DESCRIPTION { get; set; }

        public virtual T_MD_TEMPLATE Template { get; set; }
        public virtual T_MD_KHOAN_MUC_DAU_TU KhoanMucDauTu { get; set; }
        public virtual T_MD_DAU_TU_NGOAI_DOANH_NGHIEP_PROFIT_CENTER DauTuNgoaiDoanhNghiepProfitCenter { get; set; }
        public virtual T_MD_COST_CENTER Organize { get; set; }

        public static explicit operator T_BP_DAU_TU_NGOAI_DOANH_NGHIEP_DATA_BASE_HISTORY(T_BP_DAU_TU_NGOAI_DOANH_NGHIEP_DATA_BASE v)
        {
            return new T_BP_DAU_TU_NGOAI_DOANH_NGHIEP_DATA_BASE_HISTORY
            {
                PKID = v.PKID,
                ORG_CODE = v.ORG_CODE,
                DAU_TU_PROFIT_CENTER_CODE = v.DAU_TU_PROFIT_CENTER_CODE,
                TEMPLATE_CODE = v.TEMPLATE_CODE,
                KHOAN_MUC_DAU_TU_CODE = v.KHOAN_MUC_DAU_TU_CODE,
                VERSION = v.VERSION,
                TIME_YEAR = v.TIME_YEAR,
                MATERIAL = v.MATERIAL,
                UNIT = v.UNIT,

                QUANTITY_1 = v.QUANTITY_1,
                PRICE_1 = v.PRICE_1,
                AMOUNT_1 = v.AMOUNT_1,
                TIME_1 = v.TIME_1,

                QUANTITY_2 = v.QUANTITY_2,
                PRICE_2 = v.PRICE_2,
                AMOUNT_2 = v.AMOUNT_2,
                TIME_2 = v.TIME_2,

                QUANTITY_3 = v.QUANTITY_3,
                PRICE_3 = v.PRICE_3,
                AMOUNT_3 = v.AMOUNT_3,
                TIME_3 = v.TIME_3,

                QUANTITY_4 = v.QUANTITY_4,
                PRICE_4 = v.PRICE_4,
                AMOUNT_4 = v.AMOUNT_4,
                TIME_4 = v.TIME_4,

                QUANTITY_5 = v.QUANTITY_5,
                PRICE_5 = v.PRICE_5,
                AMOUNT_5 = v.AMOUNT_5,
                TIME_5 = v.TIME_5,

                QUANTITY_6 = v.QUANTITY_6,
                PRICE_6 = v.PRICE_6,
                AMOUNT_6 = v.AMOUNT_6,
                TIME_6 = v.TIME_6,

                DESCRIPTION = v.DESCRIPTION,
            };
        }
    }
}
