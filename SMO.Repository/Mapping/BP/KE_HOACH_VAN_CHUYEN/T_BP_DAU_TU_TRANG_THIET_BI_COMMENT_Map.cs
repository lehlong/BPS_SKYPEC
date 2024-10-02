using NHibernate.Type;
using SMO.Core.Entities.BP.KE_HOACH_VAN_CHUYEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.BP.KE_HOACH_VAN_CHUYEN
{
    public class T_BP_KE_HOACH_VAN_CHUYEN_COMMENT_Map : BaseMapping<T_BP_KE_HOACH_VAN_CHUYEN_COMMENT>
    {
        public T_BP_KE_HOACH_VAN_CHUYEN_COMMENT_Map()
        {
            Table("T_BP_KE_HOACH_VAN_CHUYEN_COMMENT");
            Id(x => x.ID);
            Map(x => x.TEMPLATE_CODE);
            Map(x => x.VERSION);
            Map(x => x.YEAR);
            Map(x => x.TYPE);
            Map(x => x.ELEMENT_CODE);
            Map(x => x.COMMENT);
            Map(x => x.ACTIVE).Not.Nullable().CustomType<YesNoType>();
        }
    }
}
