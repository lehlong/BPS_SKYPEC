using System;


namespace SMO.Core.Entities.MD
{
    public class T_MD_INPUT_KH:BaseEntity
    {
        public virtual Guid ID { get; set; }
        public virtual Guid PARENT { get; set; }
        public virtual Guid ID_CENTER { get; set; }
        public virtual int C_ORDER { get; set; }
        public virtual bool IS_BOLD { get; set; }
        public virtual string STT { get; set; }
        public virtual string NAME { get; set; }
        public virtual string KH_V2 { get; set; }
        public virtual string DVT { get; set; }
        public virtual int YEAR { get; set; }

    }
}
