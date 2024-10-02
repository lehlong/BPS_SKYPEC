using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Core.Entities.BP.KE_HOACH_VAN_CHUYEN
{
    public class T_BP_KE_HOACH_VAN_CHUYEN_COMMENT : BaseEntity
    {
        public virtual Guid ID { get; set; }
        public virtual string TEMPLATE_CODE { get; set; }
        public virtual int VERSION { get; set; }
        public virtual int YEAR { get; set; }
        public virtual string TYPE { get; set; }
       
        public virtual string ELEMENT_CODE { get; set; }
        public virtual string COMMENT { get; set; }
    }
}
