using NHibernate.Type;
using SMO.Core.Entities;
using SMO.Core.Entities.MD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.MD
{
    public class T_MD_NHOM_SAN_BAY_Map : BaseMapping<T_MD_NHOM_SAN_BAY>
    {
        public T_MD_NHOM_SAN_BAY_Map()
        {
            Table("T_MD_NHOM_SAN_BAY");
            Id(x => x.CODE);
            Map(x => x.TEXT).Nullable();
            Map(x => x.ACTIVE).Not.Nullable().CustomType<YesNoType>();
        }
    }
}
