using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Core.Entities.BU
{
    public partial class T_BU_CONTRACT_COMMENT :BaseEntity
    {
        public virtual string ID { get; set; }
        public virtual string COMMENT { get; set; }
        public virtual int VERSION { get; set;}
        public virtual string CONTRACT_NAME { get; set; }

    }
}
