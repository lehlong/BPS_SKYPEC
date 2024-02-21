using SMO.Core.Entities.MD;
using System;
using System.ComponentModel.DataAnnotations;

namespace SMO.Core.Entities
{
    public partial class T_MD_UNIT_PRICE_PLAN : BaseEntity
    {
        public virtual Guid ID { get; set; }
        public virtual string SAN_BAY_CODE { get; set; }  
        public virtual decimal VN { get; set; }
        public virtual decimal OV { get; set; }
        public virtual decimal BL { get; set; }
        public virtual decimal VJ { get; set; }
        public virtual decimal QH { get; set; }
        public virtual decimal VU { get; set; }
        public virtual decimal HKTN_OTHER { get; set; }
        public virtual decimal HKNN_ND { get; set; }
        public virtual decimal HKNN_QT { get; set; }
        public virtual int YEAR { get; set; }
    }
}
