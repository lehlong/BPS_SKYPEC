using NHibernate.Type;

using SMO.Core.Entities.MD;

namespace SMO.Repository.Mapping.MD
{
    public class T_MD_SAN_BAY_Map : BaseMapping<T_MD_SAN_BAY>
    {
        public T_MD_SAN_BAY_Map()
        {
            Table("T_MD_SAN_BAY");
            Id(x => x.CODE);
            Map(x => x.NAME).Nullable();
            Map(x => x.AREA_CODE).Nullable();
            Map(x => x.PROVINCE_CODE).Nullable();
            Map(x => x.OTHER_PM_CODE).Nullable();
            Map(x => x.NHOM_SAN_BAY_CODE).Nullable();
            Map(x => x.ACTIVE).Not.Nullable().CustomType<YesNoType>();
            References(x => x.Area).Column("AREA_CODE").Not.Insert().Not.Update().LazyLoad();
            References(x => x.Province).Column("PROVINCE_CODE").Not.Insert().Not.Update().LazyLoad();
            References(x => x.NhomSanBay).Column("NHOM_SAN_BAY_CODE").Not.Insert().Not.Update().LazyLoad();
        }
    }
}
