using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Core.Entities.BU
{
    public partial class T_BU_CONTRACT_HUMAN :BaseEntity
    {
        public virtual string ID { get; set; }
        public virtual string USERNAME { get; set; }
        public virtual string ACTION { get; set; }
        public virtual string CONTRACT_ID { get; set; }
        public virtual string NOTE { get; set; }
        public virtual int VERSION { get; set; }   
    }
}
