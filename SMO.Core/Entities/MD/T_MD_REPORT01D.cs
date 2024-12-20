using System;

namespace SMO.Core.Entities.MD
{
    public partial class T_MD_REPORT01D : BaseEntity
    {
        public virtual Guid ID { get; set; }
        public virtual decimal ? DN9T { get; set; }
        public virtual int C_ORDER { get; set; }        
        public virtual string NAME1D { get; set; }
        public virtual string TT { get; set; }
        public virtual decimal ? GTDN { get; set; }
        public virtual decimal? TH { get; set; }
        public virtual decimal? KH { get; set; }
        public virtual decimal? GTGGDT { get; set; }
        public virtual decimal ? TLGV { get; set; }
        public virtual decimal ? CT {  get; set; }
        public virtual decimal ? TlLN { get; set; }
        public virtual decimal? PKH { get; set; }
        public virtual decimal? GTCN { get; set; }
        public virtual int YEAR { get; set; }
        public virtual bool ISBOLD { get; set; }
    }
}
