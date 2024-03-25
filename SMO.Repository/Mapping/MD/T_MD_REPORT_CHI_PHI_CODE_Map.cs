using NHibernate.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMO.Core.Entities;
using SMO.Core.Entities.MD;
namespace SMO.Repository.Mapping.MD
{
    public class T_MD_REPORT_CHI_PHI_CODE_Map : BaseMapping<T_MD_REPORT_CHI_PHI_CODE>
    {
        public T_MD_REPORT_CHI_PHI_CODE_Map()
        {
            Table("T_MD_REPORT_CHI_PHI_CODE");
            Id(x => x.ID);
            Map(x => x.GROUP_1_ID);
            Map(x => x.GROUP_2_ID);
            Map(x => x.GROUP_NAME);
            Map(x => x.TIME_YEAR);
            Map(x => x.ACTIVE).Not.Nullable().CustomType<YesNoType>();
        }
    }
}
