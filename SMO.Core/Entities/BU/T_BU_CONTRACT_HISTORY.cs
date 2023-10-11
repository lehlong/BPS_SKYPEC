using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Core.Entities.BU
{
    public partial class T_BU_CONTRACT_HISTORY :BaseEntity
    {
        public virtual string ID { get; set; }
        public virtual string ACTION { get; set; }
        public virtual string OLD_VALUE { get; set; }
        public virtual string NEW_VALUE { get;set; }
        public virtual int VERSION { get; set; }
        public virtual string NAME_CONTRACT { get; set; }

        

    }
}
