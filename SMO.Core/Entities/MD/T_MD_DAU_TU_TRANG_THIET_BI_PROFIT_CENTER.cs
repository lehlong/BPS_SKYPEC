using SMO.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Core.Entities.MD
{
    public class T_MD_DAU_TU_TRANG_THIET_BI_PROFIT_CENTER : CoreCenter
    {
        public virtual string ORG_CODE { get; set; }
        public virtual string PROJECT_CODE { get; set; }
        public virtual T_MD_COST_CENTER Organize { get; set; }
        public virtual T_MD_PROJECT Project { get; set; }
    }
}
