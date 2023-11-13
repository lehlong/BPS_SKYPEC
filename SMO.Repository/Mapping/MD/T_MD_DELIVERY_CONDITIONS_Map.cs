using NHibernate.Type;

using SMO.Core.Entities;

namespace SMO.Repository.Mapping.MD
{
    public class T_MD_DELIVERY_CONDITIONS_Map : BaseMapping<T_MD_DELIVERY_CONDITIONS>
    {
        public T_MD_DELIVERY_CONDITIONS_Map()
        {
            Table("T_MD_DELIVERY_CONDITIONS");
            Id(x => x.CODE);
            Map(x => x.TEXT).Nullable();
            Map(x => x.ACTIVE).Not.Nullable().CustomType<YesNoType>();
        }
    }
}
