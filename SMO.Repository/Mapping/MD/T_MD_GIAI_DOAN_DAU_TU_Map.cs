using NHibernate.Type;

using SMO.Core.Entities;
using SMO.Core.Entities.MD;

namespace SMO.Repository.Mapping.MD
{
    public class T_MD_GIAI_DOAN_DAU_TU_Map : BaseMapping<T_MD_GIAI_DOAN_DAU_TU>
    {
        public T_MD_GIAI_DOAN_DAU_TU_Map()
        {
            Table("T_MD_GIAI_DOAN_DAU_TU");
            Id(x => x.CODE);
            Map(x => x.TEXT).Nullable();
            Map(x => x.ACTIVE).Not.Nullable().CustomType<YesNoType>();
        }
    }
}
