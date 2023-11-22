using SMO.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Core.Entities.MD
{
    public class T_MD_SUA_CHUA_THUONG_XUYEN_PROFIT_CENTER : CoreCenter
    {
        public virtual string COST_CENTER_CODE { get; set; }
        public virtual string SAN_BAY_CODE { get; set; }
        public virtual T_MD_COST_CENTER CostCenter { get; set; }
        public virtual T_MD_SAN_BAY SanBay { get; set; }
    }
}
