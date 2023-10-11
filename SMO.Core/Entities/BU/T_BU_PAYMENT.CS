using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Core.Entities.BU
{
    public partial class T_BU_PAYMENT : BaseEntity
    {
        public virtual string ID { get; set; }
        public virtual string NUMBER_INVOICE { get; set; }      
        public virtual decimal VALUE_INVOICE { get; set; }
        public virtual decimal AMOUNT { get; set; }
        public virtual decimal ADVANCE_PAYMENT { get; set; }
        public virtual decimal TOTAL { get; set;}
        public virtual string ACTION { get;set; }
        public virtual string NOTE { get; set; }
        public virtual DateTime DATE { get; set; }
        public virtual string CONTENT_PAYMENT { get; set;}
        public virtual string FILE_CHILD { get; set;}
        public virtual string NAME_CONTRACT { get; set; }
        public virtual int VERSION { get; set; }
        public virtual string UPDATE_HUMAN { get; set; }
        public virtual DateTime UPDATE_TIME { get; set; }
        public virtual bool IS_DELETE { get; set; }

    }
}
