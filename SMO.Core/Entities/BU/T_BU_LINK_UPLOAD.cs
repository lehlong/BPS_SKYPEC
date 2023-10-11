using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Core.Entities.BU
{
    public class T_BU_LINK_UPLOAD : BaseEntity
    {
        public virtual string ID { get; set; }
        public virtual string LINK { get; set; }
        public virtual string PARENT { get; set; }
        public virtual bool IS_DELETE { get; set;}
        public virtual int VERSION { get; set;}

    }
}
