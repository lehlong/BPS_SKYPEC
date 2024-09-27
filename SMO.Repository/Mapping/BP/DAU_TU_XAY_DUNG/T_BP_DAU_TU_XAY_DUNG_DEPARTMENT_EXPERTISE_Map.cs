
using NHibernate.Type;
using SMO.Core.Entities.BP.DAU_TU_XAY_DUNG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.BP.DAU_TU_XAY_DUNG
{
    public class T_BP_DAU_TU_XAY_DUNG_DEPARTMENT_EXPERTISE_Map : BaseMapping<T_BP_DAU_TU_XAY_DUNG_DEPARTMENT_EXPERTISE>
    {
        public T_BP_DAU_TU_XAY_DUNG_DEPARTMENT_EXPERTISE_Map()
        {
            Table("T_BP_DAU_TU_XAY_DUNG_DEPARTMENT_EXPERTISE");
            Id(x => x.ID);
            Map(x => x.TEMPLATE_CODE);
            Map(x => x.VERSION);
            Map(x => x.YEAR);
            Map(x => x.ELEMENT_CODE);
            Map(x => x.TYPE);
            Map(x => x.ACTIVE).Not.Nullable().CustomType<YesNoType>();
        }

    }
}
