using NHibernate.Type;

using SMO.Core.Entities;
using SMO.Core.Entities.MD;

namespace SMO.Repository.Mapping.MD
{
    public class T_MD_HEADER_DM_Map : BaseMapping<T_MD_HEADER_DM>
    {
        public T_MD_HEADER_DM_Map()
        {
            Id(x => x.ID);
            Map(x => x.NAME);
            Map(x => x.NOTE);
            Map(x => x.STT);
            Map(x => x.DVT);
            Map(x => x.ACTIVE).Not.Nullable().CustomType<YesNoType>();
        }
    }
}
