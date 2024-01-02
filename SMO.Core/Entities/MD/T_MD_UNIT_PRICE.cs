using SMO.Core.Entities.MD;
using System;
using System.ComponentModel.DataAnnotations;

namespace SMO.Core.Entities
{
    public partial class T_MD_UNIT_PRICE : BaseEntity
    {
        public virtual Guid ID { get; set; }
        public virtual string SHORT_OBJECT_ID { get; set; }
        public virtual string OBJECT_ID { get; set; }
        public virtual string OBJECT_TYPE_ID { get; set; }
        public virtual string CURRENCY_ID { get; set; }
        public virtual decimal MOPS_PRICE { get; set; }
        public virtual string UNIT_ID { get; set; }
        public virtual string WAREHOUSE_ID { get; set; }
        public virtual decimal PRICE_01 { get; set; }
        public virtual decimal PRICE_02 { get; set; }
        public virtual decimal ENVIRONMENT_AMOUNT { get; set; }
        public virtual decimal SERVICE_PRICE { get; set; }
        public virtual decimal PUMP_FEE { get; set; }
        public virtual int YEAR { get; set; }
    }
}
