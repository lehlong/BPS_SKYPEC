using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Core.Entities.MD
{
    public partial class T_MD_REPORT_CHI_PHI_CODE : BaseEntity
    {
        public virtual Guid ID { get; set; }
        public virtual string GROUP_1_ID { get; set; }
        public virtual string GROUP_2_ID { get; set; }
        public virtual string GROUP_NAME { get; set; }
        public virtual int TIME_YEAR { get; set; }


    }
}
