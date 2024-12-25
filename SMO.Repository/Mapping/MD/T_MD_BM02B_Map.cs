using NHibernate.Type;

using SMO.Core.Entities;

namespace SMO.Repository.Mapping.MD
{
    public class T_MD_BM02B_Map : BaseMapping<T_MD_BM02B>
    {
        public T_MD_BM02B_Map()
        {
            Table("T_MD_BM02B");
            Id(x => x.ID);
            Map(x => x.C_ORDER);
            Map(x => x.YEAR);
            Map(x => x.COL1);
            Map(x => x.COL2);
            Map(x => x.COL3);
            Map(x => x.COL4);
            Map(x => x.COL5);
            Map(x => x.COL6);
            Map(x => x.COL7);
            Map(x => x.COL8);
            Map(x => x.COL9);
            Map(x => x.ACTIVE).Not.Nullable().CustomType<YesNoType>();
        }
    }
}
