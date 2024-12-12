using NHibernate.Type;

using SMO.Core.Entities.MD;

namespace SMO.Repository.Mapping.MD
{
    public class T_MD_CP_TCNL_map : BaseMapping<T_MD_CP_TCNL>
    {
        public T_MD_CP_TCNL_map()
        {
            Id(x => x.ID);
            Map(x => x.NAME);
            Map(x => x.PARENT);
            Map(x => x.STT);
            Map(x => x.C_ORDER);
            Map(x => x.IS_BOLD).CustomType<YesNoType>();
            Map(x => x.ACTIVE).CustomType<YesNoType>();
           
        }
    }
}
