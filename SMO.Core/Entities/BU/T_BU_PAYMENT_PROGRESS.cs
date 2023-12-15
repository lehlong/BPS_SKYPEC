using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Core.Entities.BU
{
    public class T_BU_PAYMENT_PROGRESS : BaseEntity
    {
        public virtual string ID { get; set; }
        public virtual string BATCH { get; set; }
        public virtual decimal PROGRESS { get; set; }
        public virtual decimal PAYMENT_VALUE { get; set; }
        public virtual string PAYMENT_DESCRIPTION { get; set; }
        public virtual string PROFILE { get; set; }
        public virtual DateTime DATE { get; set; }
        public virtual string STATUS { get; set; }
        public virtual string NAME_CONTRACT { get; set; }
        public virtual int VERSION { get; set; }
        public virtual string UPDATE_HUMAN { get; set; }
        public virtual DateTime UPDATE_TIME { get; set; }
        public virtual bool IS_DELETE { get; set; }
    }
}
