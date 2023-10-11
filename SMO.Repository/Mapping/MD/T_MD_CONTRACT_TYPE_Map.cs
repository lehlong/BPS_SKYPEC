using NHibernate.Type;

using SMO.Core.Entities;

namespace SMO.Repository.Mapping.MD
{
    public class T_MD_CONTRACT_TYPE_Map : BaseMapping<T_MD_CONTRACT_TYPE>
    {
        public T_MD_CONTRACT_TYPE_Map()
        {
            Table("T_MD_CONTRACT_TYPE");
            Id(x => x.CODE);
            Map(x => x.TEXT).Nullable();
            Map(x => x.ACTIVE).Not.Nullable().CustomType<YesNoType>();
        }
    }
}
