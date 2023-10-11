using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Core.Entities.BU
{
    public partial class T_BU_CONTRACT_VERSION : BaseEntity
    {
        public virtual string ID { get; set; }
        public virtual string PARENT { get; set; }
        public virtual string NAME { get; set; }
        public virtual string CONTRACT_NUMBER { get; set; }
        public virtual string CONTRACT_TYPE { get; set; }
        public virtual DateTime START_DATE { get; set; }
        public virtual DateTime FINISH_DATE { get; set; }
        public virtual decimal CONTRACT_VALUE { get; set; }
        public virtual decimal VAT { get; set; }
        public virtual decimal CONTRACT_VALUE_VAT { get; set; }
        public virtual string NOTES { get; set; }
        public virtual string REPRESENT_A { get; set; }
        public virtual string REPRESENT_B { get; set; }
        public virtual int VERSION { get; set; }
        public virtual string CONTRACT_PHASE { get; set; }
        public virtual string APPROVER { get; set; }
        public virtual string CONTRACT_MANAGER { get; set; }
        public virtual string DEPARTMENT { get; set; }
        public virtual string CUSTOMER { get; set; }
        public virtual string STATUS { get; set; }
        public virtual string FILE_CHILD { get; set; }
        public virtual string NAME_CONTRACT { get; set; }

    }
}
