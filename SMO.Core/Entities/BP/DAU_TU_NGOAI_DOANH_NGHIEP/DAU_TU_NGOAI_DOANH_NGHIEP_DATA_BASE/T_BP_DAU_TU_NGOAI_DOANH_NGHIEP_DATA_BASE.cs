using SMO.Core.Entities.MD;
using System.Collections.Generic;

namespace SMO.Core.Entities.BP.DAU_TU_NGOAI_DOANH_NGHIEP.DAU_TU_NGOAI_DOANH_NGHIEP_DATA_BASE
{
    public class T_BP_DAU_TU_NGOAI_DOANH_NGHIEP_DATA_BASE : BaseEntity
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
        public virtual string PROCESS { get; set; }

        public virtual T_MD_TEMPLATE Template { get; set; }
        public virtual T_MD_KHOAN_MUC_DAU_TU KhoanMucDauTu { get; set; }
        public virtual T_MD_DAU_TU_NGOAI_DOANH_NGHIEP_PROFIT_CENTER DauTuNgoaiDoanhNghiepProfitCenter { get; set; }
        public virtual T_MD_COST_CENTER Organize { get; set; }
    }
}
