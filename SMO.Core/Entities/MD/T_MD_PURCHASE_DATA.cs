using System;
using System.ComponentModel.DataAnnotations;

namespace SMO.Core.Entities
{
    public partial class T_MD_PURCHASE_DATA: BaseEntity
    {
        public virtual Guid ID { get; set; }
        public virtual string WAREHOUSE_CODE { get; set; }
        public virtual string DELIVERY_CONDITIONS_CODE { get; set; }
        public virtual int TIME_YEAR { get; set; }
        public virtual decimal? S0001 { get; set; }
        public virtual decimal? S0002 { get; set; }
        public virtual decimal? S0003 { get; set; }
        public virtual decimal? S0004 { get; set; }
        public virtual decimal? S0005 { get; set; }
        public virtual decimal? S0006 { get;set; }
        public virtual decimal? S0007 { get; set; }

        private T_MD_WAREHOUSE _Warehouse;
        public virtual T_MD_WAREHOUSE Warehouse
        {
            get
            {
                if (_Warehouse == null)
                {
                    _Warehouse = new T_MD_WAREHOUSE();
                }
                return _Warehouse;
            }
            set
            {
                _Warehouse = value;
            }
        }

        private T_MD_DELIVERY_CONDITIONS _DeliveryConditions;
        public virtual T_MD_DELIVERY_CONDITIONS DeliveryConditions
        {
            get
            {
                if (_DeliveryConditions == null)
                {
                    _DeliveryConditions = new T_MD_DELIVERY_CONDITIONS();
                }
                return _DeliveryConditions;
            }
            set
            {
                _DeliveryConditions = value;
            }
        }
    }
}
