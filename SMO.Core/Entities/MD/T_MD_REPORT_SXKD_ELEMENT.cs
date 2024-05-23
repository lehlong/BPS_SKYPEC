using System;
using System.ComponentModel.DataAnnotations;

namespace SMO.Core.Entities
{
    public partial class T_MD_REPORT_SXKD_ELEMENT : BaseEntity
    {
        public virtual Guid ID { get; set; }
        public virtual Guid PARENT { get; set; }
        public virtual int C_ORDER {  get; set; }
        public virtual bool IS_BOLD { get; set; }
        public virtual string STT { get; set; }
        public virtual string NAME { get; set; }
        public virtual string DVT { get; set;}

        public virtual string TH_2 { get; set; }
        public virtual string KH_1 { get; set; }
        public virtual string TDN_1 { get; set; }
        public virtual string UTH_1 { get; set; }
        public virtual string KH_V1 { get; set; }
        public virtual string KH_V2 { get; set; }

    }
}
