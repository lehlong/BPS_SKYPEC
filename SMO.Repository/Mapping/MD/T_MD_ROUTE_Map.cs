using NHibernate.Type;

using SMO.Core.Entities.MD;

namespace SMO.Repository.Mapping.MD
{
    public class T_MD_ROUTE_Map : BaseMapping<T_MD_ROUTE>
    {
        public T_MD_ROUTE_Map()
        {
            Table("T_MD_ROUTE");
            Id(x => x.CODE);
            Map(x => x.NAME).Nullable();
            Map(x => x.AREA_CODE).Nullable();
            Map(x => x.ACTIVE).Not.Nullable().CustomType<YesNoType>();
            References(x => x.Area).Column("AREA_CODE").Not.Insert().Not.Update().LazyLoad();
        }
    }
}
