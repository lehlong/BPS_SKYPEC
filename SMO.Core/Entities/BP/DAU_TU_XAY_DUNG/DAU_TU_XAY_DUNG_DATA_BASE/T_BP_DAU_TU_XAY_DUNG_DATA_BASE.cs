using SMO.Core.Entities.MD;
using System.Collections.Generic;

namespace SMO.Core.Entities.BP.DAU_TU_XAY_DUNG.DAU_TU_XAY_DUNG_DATA_BASE
{
    public class T_BP_DAU_TU_XAY_DUNG_DATA_BASE : BaseEntity
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

        public virtual decimal QUANTITY { get; set; }
        public virtual string TIME { get; set; }
        public virtual decimal PRICE { get; set; }
        public virtual decimal AMOUNT { get; set; }

        public virtual string DESCRIPTION { get; set; }

        public virtual T_MD_TEMPLATE Template { get; set; }
        public virtual T_MD_KHOAN_MUC_DAU_TU KhoanMucDauTu { get; set; }
        public virtual T_MD_DAU_TU_XAY_DUNG_PROFIT_CENTER DauTuXayDungProfitCenter { get; set; }
        public virtual T_MD_COST_CENTER Organize { get; set; }
    }
}
