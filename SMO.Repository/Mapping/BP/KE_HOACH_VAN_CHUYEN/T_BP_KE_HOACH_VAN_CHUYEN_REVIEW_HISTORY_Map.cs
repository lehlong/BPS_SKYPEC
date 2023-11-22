
using SMO.Core.Entities.BP.KE_HOACH_VAN_CHUYEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.BP.KE_HOACH_VAN_CHUYEN
{
    class T_BP_KE_HOACH_VAN_CHUYEN_REVIEW_HISTORY_Map : BaseMapping<T_BP_KE_HOACH_VAN_CHUYEN_REVIEW_HISTORY>
    {
        public T_BP_KE_HOACH_VAN_CHUYEN_REVIEW_HISTORY_Map()
        {
            Id(x => x.PKID);
            Map(x => x.ORG_CODE);
            Map(x => x.REVIEW_DATE);
            Map(x => x.REVIEW_USER);
            Map(x => x.TIME_YEAR);
            Map(x => x.DATA_VERSION);

            References(x => x.Organize, "ORG_CODE")
                .Not.Insert()
                .Not.Update();
            References(x => x.UserReview, "REVIEW_USER")
                .Not.Insert()
                .Not.Update();
        }
    }
}
