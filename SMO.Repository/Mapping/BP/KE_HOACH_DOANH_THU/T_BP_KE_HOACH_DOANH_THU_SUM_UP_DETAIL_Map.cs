﻿
using SMO.Core.Entities.BP.KE_HOACH_DOANH_THU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.BP.KE_HOACH_DOANH_THU
{
    class T_BP_KE_HOACH_DOANH_THU_SUM_UP_DETAIL_Map : BaseMapping<T_BP_KE_HOACH_DOANH_THU_SUM_UP_DETAIL>
    {
        public T_BP_KE_HOACH_DOANH_THU_SUM_UP_DETAIL_Map()
        {
            Id(x => x.PKID);
            Map(x => x.ORG_CODE);
            Map(x => x.FROM_ORG_CODE);
            Map(x => x.TEMPLATE_CODE);
            Map(x => x.SUM_UP_VERSION);
            Map(x => x.TIME_YEAR);
            Map(x => x.DATA_VERSION);

            References(x => x.CostCenter, "ORG_CODE")
                .Not.Insert()
                .Not.Update();
            References(x => x.FromCostCenter, "FROM_ORG_CODE")
                .Not.Insert()
                .Not.Update();
            References(x => x.Template, "TEMPLATE_CODE")
                .Not.Insert()
                .Not.Update();
        }
    }
}
