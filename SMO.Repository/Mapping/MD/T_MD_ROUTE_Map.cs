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
            Map(x => x.FINAL_POINT).Nullable();
            Map(x => x.FIRST_POINT).Nullable();
            Map(x => x.KM_CO_HANG).Nullable();
            Map(x => x.KM_KHONG_HANG).Nullable();
            Map(x => x.HE_SO_HAO_HUT).Nullable();
            Map(x => x.PARENT_LEVEL_ID).Nullable();
            Map(x => x.ACTIVE).Not.Nullable().CustomType<YesNoType>();
            Map(x => x.AREA_CODE).Nullable();
        }
    }
}
