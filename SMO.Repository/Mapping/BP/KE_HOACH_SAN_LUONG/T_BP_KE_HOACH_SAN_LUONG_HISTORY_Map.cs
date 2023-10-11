﻿using SMO.Core.Entities;
using SMO.Core.Entities.BP.KE_HOACH_SAN_LUONG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.BP.KE_HOACH_SAN_LUONG
{
    public class T_BP_KE_HOACH_SAN_LUONG_HISTORY_Map : BaseBPHistoryMapping<T_BP_KE_HOACH_SAN_LUONG_HISTORY>
    {
        public T_BP_KE_HOACH_SAN_LUONG_HISTORY_Map()
        {
            Map(x => x.KICH_BAN);
            References(x => x.KichBan).Column("KICH_BAN").Not.Insert().Not.Update().LazyLoad();
        }
    }
}
