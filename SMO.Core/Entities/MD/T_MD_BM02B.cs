using System;
using System.ComponentModel.DataAnnotations;

namespace SMO.Core.Entities
{
    public partial class T_MD_BM02B : BaseEntity
    {
        public virtual Guid ID { get; set; }
        public virtual int C_ORDER { get; set; }
        public virtual int YEAR { get; set; }
        public virtual string COL1 { get; set; }
        public virtual string COL2 { get; set; }
        public virtual decimal COL3 { get; set; }
        public virtual decimal COL4 { get; set; }
        public virtual decimal COL5 { get; set; }
        public virtual decimal COL6 { get; set; }
        public virtual decimal COL7 { get; set; }
        public virtual decimal COL8 { get; set; }
        public virtual string COL9 { get; set; }
    }
}
