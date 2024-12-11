using System;

namespace SMO.Core.Entities.MD
{
    public partial class T_MD_CP_TCNL_DATA : BaseEntity
    {

        public virtual Guid ID { get; set; }
        public virtual string ID_CENTER { get; set; }
        public virtual decimal? U_CBQL { get; set; }
        public virtual decimal? U_CQCT { get; set; }
        public virtual decimal? U_OIL_SOUCE { get; set; }
        public virtual decimal? U_MB { get; set; }
        public virtual decimal? U_MT { get; set; }
        public virtual decimal? U_VT { get; set; }
        public virtual decimal? U_MN { get; set; }
        public virtual decimal? KH_TOTALKH { get; set; }
        public virtual decimal? KH_CBQL { get; set; }
        public virtual decimal? KH_CQCT { get; set; }
        public virtual decimal? KH_OIL_SOUCE { get; set; }
        public virtual decimal? KH_MB { get; set; }
        public virtual decimal? KH_MT { get; set; }
        public virtual decimal? KH_MN { get; set; }
        public virtual decimal? KH_VT { get; set; }
        public virtual int YEAR { get; set; }


    }
}
