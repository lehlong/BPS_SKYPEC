using SMO.Core.Entities;
using SMO.Core.Entities.BP.KE_HOACH_DOANH_THU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.BP.KE_HOACH_DOANH_THU
{
    public class T_BP_KE_HOACH_DOANH_THU_VERSION_Map : BaseBPVersionMapping<T_BP_KE_HOACH_DOANH_THU_VERSION>
    {
        public T_BP_KE_HOACH_DOANH_THU_VERSION_Map()
        {
            Map(x => x.TYPE_UPLOAD);
            Map(x => x.KICH_BAN);
            Map(x => x.PHIEN_BAN);
            References(x => x.KichBan).Column("KICH_BAN").Not.Insert().Not.Update().LazyLoad();
            References(x => x.PhienBan).Column("PHIEN_BAN").Not.Insert().Not.Update().LazyLoad();
        }
    }
}
