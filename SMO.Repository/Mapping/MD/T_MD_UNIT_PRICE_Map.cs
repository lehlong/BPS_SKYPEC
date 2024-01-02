using NHibernate.Type;

using SMO.Core.Entities;

namespace SMO.Repository.Mapping.MD
{
    public class T_MD_UNIT_PRICE_Map : BaseMapping<T_MD_UNIT_PRICE>
    {
        public T_MD_UNIT_PRICE_Map()
        {
            Table("T_MD_UNIT_PRICE");
            Id(x => x.ID);
            Map(x => x.SHORT_OBJECT_ID);
            Map(x => x.OBJECT_ID);
            Map(x => x.OBJECT_TYPE_ID);
            Map(x => x.CURRENCY_ID);
            Map(x => x.MOPS_PRICE);
            Map(x => x.UNIT_ID);
            Map(x => x.WAREHOUSE_ID);
            Map(x => x.PRICE_01);
            Map(x => x.PRICE_02);
            Map(x => x.ENVIRONMENT_AMOUNT);
            Map(x => x.SERVICE_PRICE);
            Map(x => x.PUMP_FEE);
            Map(x => x.YEAR);
            Map(x => x.ACTIVE).Not.Nullable().CustomType<YesNoType>();
        }
    }
}
