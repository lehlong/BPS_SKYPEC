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
            Map(x => x.SAN_BAY_CODE);
            Map(x => x.YEAR);
            Map(x => x.VN);
            Map(x => x.OV);
            Map(x => x.BL);
            Map(x => x.VJ);
            Map(x => x.QH);
            Map(x => x.VU);
            Map(x => x.OTHER_HKTN);
            Map(x => x.HKNN);
            Map(x => x.ACTIVE).Not.Nullable().CustomType<YesNoType>();
            References(x => x.SanBay).Column("SAN_BAY_CODE").Not.Insert().Not.Update().LazyLoad();
        }
    }
}
