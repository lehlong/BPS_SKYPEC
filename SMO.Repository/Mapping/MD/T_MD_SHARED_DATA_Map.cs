using NHibernate.Type;

using SMO.Core.Entities;

namespace SMO.Repository.Mapping.MD
{
    public class T_MD_SHARED_DATA_Map : BaseMapping<T_MD_SHARED_DATA>
    {
        public T_MD_SHARED_DATA_Map()
        {
            Table("T_MD_SHARED_DATA");
            Id(x => x.CODE);
            Map(x => x.TEXT).Nullable();
            Map(x => x.VALUE).Nullable();
            Map(x => x.ACTIVE).Not.Nullable().CustomType<YesNoType>();
        }
    }
}
