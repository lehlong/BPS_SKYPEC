using NHibernate.Type;

using SMO.Core.Entities.MD;

namespace SMO.Repository.Mapping.MD
{
    public class T_MD_HANG_HANG_KHONG_Map : BaseMapping<T_MD_HANG_HANG_KHONG>
    {
        public T_MD_HANG_HANG_KHONG_Map()
        {
            Table("T_MD_HANG_HANG_KHONG");
            Id(x => x.CODE);
            Map(x => x.NAME);
            Map(x => x.TYPE);
            Map(x => x.OTHER_PM_CODE);
            Map(x => x.GROUP_ITEM);
            Map(x => x.ACTIVE).Not.Nullable().CustomType<YesNoType>();
            Map(x => x.IS_VNA).CustomType<YesNoType>();
        }
    }
}
