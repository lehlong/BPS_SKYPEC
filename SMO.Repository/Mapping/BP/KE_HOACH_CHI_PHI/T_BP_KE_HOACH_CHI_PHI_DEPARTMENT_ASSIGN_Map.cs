using NHibernate.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMO.Core.Entities.BP.KE_HOACH_CHI_PHI;

namespace SMO.Repository.Mapping.BP.KE_HOACH_CHI_PHI
{
    public class T_BP_KE_HOACH_CHI_PHI_DEPARTMENT_ASSIGN_Map : BaseMapping<T_BP_KE_HOACH_CHI_PHI_DEPARTMENT_ASSIGN>
    {
        public T_BP_KE_HOACH_CHI_PHI_DEPARTMENT_ASSIGN_Map()
        {
            Table("T_BP_KE_HOACH_CHI_PHI_DEPARTMENT_ASSIGN");
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
