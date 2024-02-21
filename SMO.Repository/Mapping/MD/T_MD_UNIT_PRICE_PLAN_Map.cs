using NHibernate.Type;

using SMO.Core.Entities;

namespace SMO.Repository.Mapping.MD
{
    public class T_MD_UNIT_PRICE_PLAN_Map : BaseMapping<T_MD_UNIT_PRICE_PLAN>
    {
        public T_MD_UNIT_PRICE_PLAN_Map()
        {
            Table("T_MD_UNIT_PRICE_PLAN");
            Id(x => x.ID);
            Map(x => x.SAN_BAY_CODE);
            Map(x => x.VN);
            Map(x => x.OV);
            Map(x => x.BL);
            Map(x => x.VJ);
            Map(x => x.QH);
            Map(x => x.VU);
            Map(x => x.HKTN_OTHER);
            Map(x => x.HKNN_ND);
            Map(x => x.HKNN_QT);
            Map(x => x.YEAR);
            Map(x => x.ACTIVE).Not.Nullable().CustomType<YesNoType>();
        }
    }
}
