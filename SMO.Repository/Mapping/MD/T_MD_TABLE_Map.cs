using NHibernate.Type;

using SMO.Core.Entities;

namespace SMO.Repository.Mapping.MD
{
    public class T_MD_TABLE_Map : BaseMapping<T_MD_TABLE>
    {
        public T_MD_TABLE_Map()
        {
            Table("T_MD_TABLE");
            Id(x => x.CODE);
            Map(x => x.TEXT).Nullable();
            Map(x => x.ACTIVE).Not.Nullable().CustomType<YesNoType>();
        }
    }
}
