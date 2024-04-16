using NHibernate.Type;

using SMO.Core.Entities;

namespace SMO.Repository.Mapping.MD
{
    public class T_MD_PURCHASE_DATA_Map : BaseMapping<T_MD_PURCHASE_DATA>
    {
        public T_MD_PURCHASE_DATA_Map()
        {
            Table("T_MD_PURCHASE_DATA");
            Id(x => x.ID);
            Map(x => x.WAREHOUSE_CODE);
            Map(x => x.DELIVERY_CONDITIONS_CODE);
            Map(x => x.TIME_YEAR);
            Map(x => x.S0001);
            Map(x => x.S0002);
            Map(x => x.S0003);
            Map(x => x.S0004);
            Map(x => x.S0005);
            Map(x => x.S0006);
            Map(x => x.S0007);
            Map(x => x.S0008);
            Map(x => x.ID_KHNH);
            Map(x => x.NAME);
            Map(x => x.YEAR);
            Map(x => x.SIZE_ID);
            Map(x => x.AREA_ID);
            Map(x => x.ACTIVE).Not.Nullable().CustomType<YesNoType>();
            References(x => x.Warehouse).Column("WAREHOUSE_CODE").Not.Insert().Not.Update().LazyLoad();
            References(x => x.DeliveryConditions).Column("DELIVERY_CONDITIONS_CODE").Not.Insert().Not.Update().LazyLoad();
        }
    }
}
