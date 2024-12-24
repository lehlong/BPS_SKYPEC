using System;

namespace SMO.Core.Entities.MD
{
    public partial class T_MD_HEADER_DM : BaseEntity
    {
        public virtual string ID { get; set; }
        public virtual string NAME { get; set; }
        public virtual string NOTE { get; set; }

        public virtual int STT { get; set; }
        public virtual string DVT { get; set; }
        public virtual decimal? VALUE { get; set; }

        public virtual int YEAR { get; set; }

    }
}
