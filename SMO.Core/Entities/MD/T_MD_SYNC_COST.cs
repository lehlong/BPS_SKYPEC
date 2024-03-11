using SMO.Core.Entities.MD;
using System;
using System.ComponentModel.DataAnnotations;

namespace SMO.Core.Entities
{
    public partial class T_MD_SYNC_COST : BaseEntity
    {
        public virtual Guid ID { get; set; }
        public virtual string CHI_NHANH { get; set; }
        public virtual string GROUP_3_ID { get; set; }
        public virtual string GROUP_NAME_3 { get; set; }
        public virtual string GROUP_2_ID { get; set; }
        public virtual string GROUP_NAME_2 { get; set; }
        public virtual string GROUP_NAME_E2 { get; set; }
        public virtual string GROUP_1_ID { get; set; }
        public virtual string GROUP_NAME_1 { get; set; }
        public virtual decimal VALUE { get; set; }
        public virtual decimal ACCUMULATION { get; set; }
        public virtual int MONTH { get; set; }
        public virtual int YEAR { get; set; }
    }
}
