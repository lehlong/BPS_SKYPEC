using NHibernate.Type;

using SMO.Core.Entities.MD;

namespace SMO.Repository.Mapping.MD
{
    public class T_MD_CP_TCNL_DATA_map : BaseMapping<T_MD_CP_TCNL_DATA>
    {
        public T_MD_CP_TCNL_DATA_map()
        {
            Table("T_MD_CP_TCNL_DATA");
            Id(x => x.ID);
            Map(x => x.ID_CENTER);
            Map(x => x.U_CBQL);
            Map(x => x.U_CQCT);
            Map(x => x.U_OIL_SOUCE);
            Map(x => x.U_MB);
            Map(x => x.U_MT);
            Map(x => x.U_VT);
            Map(x => x.U_MN);
            Map(x => x.KH_TOTALKH);
            Map(x => x.KH_CBQL);
            Map(x => x.KH_CQCT);
            Map(x => x.KH_OIL_SOUCE);
            Map(x => x.KH_MB);
            Map(x => x.KH_MT);
            Map(x => x.KH_MN);
            Map(x => x.KH_VT);
            Map(x => x.YEAR);
            Map(x => x.ACTIVE).CustomType<YesNoType>();
            References(x => x.idcenter, "ID_CENTER");


        }
    }
}
