using NHibernate.Type;
using SMO.Core.Entities.BP.SUA_CHUA_LON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.BP.SUA_CHUA_LON
{
    public class T_BP_SUA_CHUA_LON_DEPARTMENT_ASSIGN_Map : BaseMapping<T_BP_SUA_CHUA_LON_DEPARTMENT_ASSIGN>
    {
        public T_BP_SUA_CHUA_LON_DEPARTMENT_ASSIGN_Map()
        {
            Table("T_BP_SUA_CHUA_LON_DEPARTMENT_ASSIGN");
            Id(x => x.ID);
            Map(x => x.TEMPLATE_CODE);
            Map(x => x.VERSION);
            Map(x => x.YEAR);
            Map(x => x.ELEMENT_CODE);
            Map(x => x.DEPARTMENT_CODE);
            Map(x => x.ACTIVE).Not.Nullable().CustomType<YesNoType>();
        }
    }
}
