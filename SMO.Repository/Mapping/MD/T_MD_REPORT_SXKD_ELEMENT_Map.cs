using NHibernate.Type;

using SMO.Core.Entities;

namespace SMO.Repository.Mapping.MD
{
    public class T_MD_REPORT_SXKD_ELEMENT_Map : BaseMapping<T_MD_REPORT_SXKD_ELEMENT>
    {
        public T_MD_REPORT_SXKD_ELEMENT_Map()
        {
            Table("T_MD_REPORT_SXKD_ELEMENT");
            Id(x => x.ID);
            Map(x => x.PARENT);
            Map(x => x.C_ORDER);
            Map(x => x.IS_BOLD).Not.Nullable().CustomType<YesNoType>();
            Map(x => x.STT);
            Map(x => x.NAME);
            Map(x => x.DVT);
            Map(x => x.TH_2);
            Map(x => x.KH_1);
            Map(x => x.TDN_1);
            Map(x => x.UTH_1);
            Map(x => x.KH_V1);
            Map(x => x.KH_V2);
            Map(x => x.ACTIVE).Not.Nullable().CustomType<YesNoType>();
        }
    }
}
