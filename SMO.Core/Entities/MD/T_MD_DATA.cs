using SMO.Core.Entities.MD;
using System;
using System.ComponentModel.DataAnnotations;

namespace SMO.Core.Entities
{
    public partial class T_MD_DATA : BaseEntity
    {
        public virtual Guid ID { get; set; }
        public virtual string ROUTE_CODE { get; set; }
        public virtual string ROUTE { get; set; }
        public virtual int TIME_YEAR { get; set; }
        public virtual decimal? S0011 { get; set; }
        public virtual decimal? S0012 { get; set; }
        public virtual decimal? S0013 { get; set; }
        public virtual decimal? S0014 { get; set; }
        public virtual decimal? S0015 { get; set; }
        public virtual decimal? S0016 { get; set; }
        public virtual decimal? S0017 { get; set; }
        public virtual decimal? S0018 { get; set; }
        public virtual decimal? S0019 { get; set; }
        public virtual decimal? S0020 { get; set; }
        public virtual decimal? S0021 { get; set; }
        public virtual decimal? S0022 { get; set; }
        public virtual decimal? S0023 { get; set; }
        public virtual decimal? S0024 { get; set; }

    }
}
