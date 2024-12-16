using System;

namespace SMO.Core.Entities.MD
{
    public partial class T_MD_REPORT01D : BaseEntity
    {
        public virtual string ID { get; set; }
        public virtual decimal ? DN9T { get; set; }
        public virtual string NAME1D { get; set; }
        public virtual string TT { get; set; }
        public virtual decimal ? GTDN { get; set; }
        public virtual decimal? TH { get; set; }
        public virtual decimal? KH { get; set; }
        public virtual decimal? GTGGDT { get; set; }
        public virtual decimal? GTGDT { get; set; }
        public virtual int YEAR { get; set; }
    }
}
