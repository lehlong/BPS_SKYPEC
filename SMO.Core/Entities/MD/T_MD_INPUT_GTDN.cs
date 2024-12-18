using System;

namespace SMO.Core.Entities.MD
{
    public partial class T_MD_INPUT_GTDN : BaseEntity
    {
        public virtual Guid ID { get; set; }
        public virtual decimal ? DN9T { get; set; }
           
        public virtual string PROJECT_CODE { get; set; }
        public virtual decimal? UTH { get; set; }
  
        public virtual decimal? KH{ get; set; }
       
        public virtual int TIME_YEAR { get; set; }
        public virtual T_MD_PROJECT Project { get; set; }

    }
}
