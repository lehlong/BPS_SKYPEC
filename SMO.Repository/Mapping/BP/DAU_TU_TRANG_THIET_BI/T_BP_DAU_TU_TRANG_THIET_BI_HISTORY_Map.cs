using SMO.Core.Entities;
using SMO.Core.Entities.BP.DAU_TU_TRANG_THIET_BI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.BP.DAU_TU_TRANG_THIET_BI
{
    public class T_BP_DAU_TU_TRANG_THIET_BI_HISTORY_Map : BaseBPHistoryMapping<T_BP_DAU_TU_TRANG_THIET_BI_HISTORY>
    {
        public T_BP_DAU_TU_TRANG_THIET_BI_HISTORY_Map()
        {
            Map(x => x.KICH_BAN);
            Map(x => x.PHIEN_BAN);
            References(x => x.KichBan).Column("KICH_BAN").Not.Insert().Not.Update().LazyLoad();
            References(x => x.PhienBan).Column("PHIEN_BAN").Not.Insert().Not.Update().LazyLoad();
        }
    }
}
