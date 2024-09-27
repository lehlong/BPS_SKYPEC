using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Core.Entities.BP.DAU_TU_TRANG_THIET_BI
{

    public partial class T_BP_DAU_TU_TRANG_THIET_BI_DEPARTMENT_EXPERTISE : BaseEntity
    {
        public virtual Guid ID { get; set; }
        public virtual string TEMPLATE_CODE { get; set; }
        public virtual int VERSION { get; set; }
        public virtual int YEAR { get; set; }
        public virtual string ELEMENT_CODE { get; set; }
        public virtual string TYPE { get; set; }


    }
}
