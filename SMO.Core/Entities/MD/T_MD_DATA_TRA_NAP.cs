using System;

namespace SMO.Core.Entities.MD
{
    public partial class T_MD_DATA_TRA_NAP : BaseEntity
    {
        public virtual string ID { get; set; }
        public virtual decimal ? VALUE { get; set; }
       
        public virtual int YEAR { get; set; }
        public virtual string ID_CENTER { get; set;}

    }
}
