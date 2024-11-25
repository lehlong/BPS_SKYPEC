using NHibernate.Type;

using SMO.Core.Entities;
using SMO.Core.Entities.MD;

namespace SMO.Repository.Mapping.MD
{
    public class T_MD_INPUT_KH_Map : BaseMapping<T_MD_INPUT_KH>
    {
        public T_MD_INPUT_KH_Map()
        {
            Table("T_MD_INPUT_KH");
            Id(x => x.ID);
            Id(x => x.PARENT);
            Map(x => x.ID_CENTER);
            Map(x => x.C_ORDER);
            Map(x => x.IS_BOLD).Not.Nullable().CustomType<YesNoType>();
            Map(x => x.STT);
            Map(x => x.NAME);
            Map(x => x.DVT);
            Map(x => x.KH_V2);
            Map(x => x.YEAR);
            Map(x => x.ACTIVE).Not.Nullable().CustomType<YesNoType>();
        }
    }
}
