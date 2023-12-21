using SMO.Core.Entities.CM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.CM
{
    public class T_CM_ASSIGN_DEPARTMENT_Map : BaseMapping<T_CM_ASSIGN_DEPARTMENT>
    {
        public T_CM_ASSIGN_DEPARTMENT_Map()
        {
            Id(x => x.PKID);
            Map(x => x.REFERENCE_CODE);
            Map(x => x.ORG_CODE);
            Map(x => x.VERSION);
            Map(x => x.DEPARTMENT);
            Map(x => x.YEAR);
            Map(x => x.ELEMENT_CODE);
            Map(x => x.BUDGET_TYPE);
            Map(x => x.ELEMENT_TYPE);
            Map(x => x.OBJECT_TYPE);
        }
    }
}
