﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Core.Entities.BP.KE_HOACH_CHI_PHI
{
    public partial class T_BP_KE_HOACH_CHI_PHI_DEPARTMENT_EXPERTISE : BaseEntity
    {
        public virtual Guid ID { get; set; }
        public virtual string TEMPLATE_CODE { get; set; }
        public virtual int VERSION { get; set; }
        public virtual int YEAR { get; set; }
        public virtual string ELEMENT_CODE { get; set; }
    }
}
