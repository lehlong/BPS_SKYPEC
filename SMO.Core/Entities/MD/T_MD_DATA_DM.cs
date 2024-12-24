using System;

namespace SMO.Core.Entities.MD
{
    public partial class T_MD_DATA_DM : BaseEntity
    {
        public virtual Guid ID { get; set; }
        public virtual decimal ? VALUE { get; set; }
        public virtual string NOTE { get; set; }
        public virtual int ID_CENTER { get; set; }
        public virtual int YEAR { get; set; }
    }
}
