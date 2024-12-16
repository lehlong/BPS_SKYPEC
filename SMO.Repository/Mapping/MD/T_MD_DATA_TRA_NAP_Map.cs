using NHibernate.Type;

using SMO.Core.Entities;
using SMO.Core.Entities.MD;

namespace SMO.Repository.Mapping.MD
{
    public class T_MD_DATA_TRA_NAP_Map : BaseMapping<T_MD_DATA_TRA_NAP>
    {
        public T_MD_DATA_TRA_NAP_Map()
        {
            Table("T_MD_INPUT_KH");
            Id(x => x.ID);
            Map(x => x.VALUE);
            Map(x => x.YEAR);

            Map(x => x.ACTIVE).Not.Nullable().CustomType<YesNoType>();
        }
    }
}
