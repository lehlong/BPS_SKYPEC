using System;

namespace SMO.Core.Entities.MD
{
    public partial class T_MD_INPUT_CHI_PHI : BaseEntity
    {
        public virtual string ID { get; set; }
        public virtual Guid ID_CENTER { get; set; }
        public virtual string GROUP_1_ID { get; set; }
        public virtual string GROUP_2_ID { get; set; }
        public virtual string GROUP_NAME { get; set; }
        public virtual decimal ? UOC_THUC_HIEN { get; set; }
        public virtual decimal? TH9T { get; set; }
        public virtual int C_ORDER { get; set; }
        public virtual string STT { get; set; }
        public virtual int ? TIME_YEAR { get; set; }
        public virtual bool IS_BOLD { get; set; }


    }
}
