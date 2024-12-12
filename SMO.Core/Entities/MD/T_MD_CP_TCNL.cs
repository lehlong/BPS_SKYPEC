using System;

namespace SMO.Core.Entities.MD
{
    public partial class T_MD_CP_TCNL : BaseEntity
    {
        public virtual string ID { get; set; }
        public virtual string PARENT { get; set; }
        public virtual string NAME { get; set; }
        public virtual bool IS_BOLD { get; set; }
        public virtual string STT { get; set; }
        //public virtual T_MD_CP_TCNL_DATA idcenter { get; set; }
        public virtual int C_ORDER { get; set; }

    }
}
