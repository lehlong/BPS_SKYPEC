using SMO.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Core.Entities.MD
{
    public class T_MD_VAN_CHUYEN_PROFIT_CENTER : CoreCenter
    {
        public virtual string ORG_CODE { get; set; }
        public virtual string ROUTE_CODE { get; set; }
        public virtual T_MD_COST_CENTER Organize { get; set; }
        public virtual T_MD_ROUTE Route { get; set; }
    }
}
