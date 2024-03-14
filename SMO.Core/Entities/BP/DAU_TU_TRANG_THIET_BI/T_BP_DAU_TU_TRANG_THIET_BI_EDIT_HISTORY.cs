using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Core.Entities.BP.DAU_TU_TRANG_THIET_BI
{
    public partial class T_BP_DAU_TU_TRANG_THIET_BI_EDIT_HISTORY : BaseEntity
    {
        public virtual Guid ID { get; set; }
        public virtual string TEMPLATE_CODE { get; set; }
        public virtual int VERSION { get; set; }
        public virtual int YEAR { get; set; }
        public virtual string TYPE { get; set; }
        public virtual string COST_CENTER_CODE { get; set; }
        public virtual string PROJECT_CODE { get; set; }
        public virtual string ELEMENT_CODE { get; set; }
        public virtual decimal? OLD_VALUE { get; set; }
        public virtual decimal? NEW_VALUE { get; set; }
        public virtual string OLD_DESCRIPTION { get; set; }
        public virtual string NEW_DESCRIPTION { get; set; }
        public virtual string EQUITY_SOURCE { get; set; }
        public virtual string EQUITY_SOURCE_NEW { get; set; }
        public virtual string PROCESS_OLD { get; set; }
        public virtual string PROCESS_NEW { get; set; }

    }
}
