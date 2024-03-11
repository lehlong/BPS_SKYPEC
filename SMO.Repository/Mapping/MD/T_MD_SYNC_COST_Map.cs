using NHibernate.Type;

using SMO.Core.Entities;

namespace SMO.Repository.Mapping.MD
{
    public class T_MD_SYNC_COST_Map : BaseMapping<T_MD_SYNC_COST>
    {
        public T_MD_SYNC_COST_Map()
        {
            Table("T_MD_SYNC_COST");
            Id(x => x.ID);
            Map(x => x.CHI_NHANH);
            Map(x => x.GROUP_3_ID);
            Map(x => x.GROUP_NAME_3);
            Map(x => x.GROUP_2_ID);
            Map(x => x.GROUP_NAME_2);
            Map(x => x.GROUP_1_ID);
            Map(x => x.GROUP_NAME_1);
            Map(x => x.GROUP_NAME_E2);
            Map(x => x.VALUE);
            Map(x => x.ACCUMULATION);
            Map(x => x.MONTH);
            Map(x => x.YEAR);
            Map(x => x.ACTIVE).Not.Nullable().CustomType<YesNoType>();
        }
    }
}
