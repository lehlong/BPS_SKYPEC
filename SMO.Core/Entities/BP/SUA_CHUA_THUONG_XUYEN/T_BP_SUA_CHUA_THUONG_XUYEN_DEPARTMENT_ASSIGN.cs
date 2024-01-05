using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Core.Entities.BP.SUA_CHUA_THUONG_XUYEN
{
    public partial class T_BP_SUA_CHUA_THUONG_XUYEN_DEPARTMENT_ASSIGN : BaseEntity
    {
        public virtual Guid ID { get; set; }
        public virtual string TEMPLATE_CODE { get; set; }
        public virtual int VERSION { get; set; }
        public virtual int YEAR { get; set; }
        public virtual string ELEMENT_CODE { get; set; }
        public virtual string DEPARTMENT_CODE { get; set; }
    }
}
