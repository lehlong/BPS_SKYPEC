using NHibernate.Type;

using SMO.Core.Entities;

namespace SMO.Repository.Mapping.MD
{
    public class T_MD_DATA_Map : BaseMapping<T_MD_DATA>
    {
        public T_MD_DATA_Map()
        {
            Table("T_MD_DATA");
            Id(x => x.ID);
            Map(x => x.ROUTE_CODE);
            Map(x => x.ROUTE);
            Map(x => x.TIME_YEAR);
            Map(x => x.S0011);
            Map(x => x.S0012);
            Map(x => x.S0013);
            Map(x => x.S0014);
            Map(x => x.S0015);
            Map(x => x.S0016);
            Map(x => x.S0017);
            Map(x => x.S0018);
            Map(x => x.S0019);
            Map(x => x.S0020);
            Map(x => x.S0021);
            Map(x => x.S0022);
            Map(x => x.S0023);
            Map(x => x.S0024);
            Map(x => x.ACTIVE).Not.Nullable().CustomType<YesNoType>();
        }
    }
}
