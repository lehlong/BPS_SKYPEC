﻿using NHibernate.Type;
using SMO.Core.Entities;
using SMO.Core.Entities.BP.DAU_TU_NGOAI_DOANH_NGHIEP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.BP.DAU_TU_NGOAI_DOANH_NGHIEP
{
    public class T_BP_DAU_TU_NGOAI_DOANH_NGHIEP_Map : BaseBPMapping<T_BP_DAU_TU_NGOAI_DOANH_NGHIEP>
    {
        public T_BP_DAU_TU_NGOAI_DOANH_NGHIEP_Map()
        {
            Map(x => x.TYPE_UPLOAD);
            Map(x => x.KICH_BAN);
            Map(x => x.PHIEN_BAN);
            References(x => x.KichBan).Column("KICH_BAN").Not.Insert().Not.Update().LazyLoad();
            References(x => x.PhienBan).Column("PHIEN_BAN").Not.Insert().Not.Update().LazyLoad();
        }
    }
}
