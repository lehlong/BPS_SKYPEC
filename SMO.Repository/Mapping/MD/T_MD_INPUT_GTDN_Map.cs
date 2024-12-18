using NHibernate.Type;

using SMO.Core.Entities;
using SMO.Core.Entities.MD;

namespace SMO.Repository.Mapping.MD
{
    public class T_MD_INPUT_GTDN_Map : BaseMapping<T_MD_INPUT_GTDN>
    {
        public T_MD_INPUT_GTDN_Map()
        {
            Id(x => x.ID);
            Map(x => x.DN9T);
            Map(x => x.PROJECT_CODE);
            Map(x => x.UTH);
            Map(x => x.KH);
            Map(x => x.TIME_YEAR);
            References(x => x.Project).Column("PROJECT_CODE").Not.Insert().Not.Update();
        }
    }
}
