using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Core.Entities.BU
{
    public partial class T_BU_FILE_UPLOAD :BaseEntity
    {
        public virtual string ID { get; set; }
        public virtual string FILE_NAME { get; set; }
        public virtual string FILE_OLD_NAME { get; set; }
        public virtual string FILE_EXT { get; set; }
        public virtual decimal FILE_SIZE { get; set; }
        public virtual string CONNECTION_ID { get; set; }
        public virtual string DIRECTORY_PATH { get; set; }
        public virtual string PARENT { get; set; }
        public virtual string FULL_PATH { get;set; }
        public virtual bool IS_DELETE { get; set; }
        public virtual int VERSION { get; set; }

        

    }
}
