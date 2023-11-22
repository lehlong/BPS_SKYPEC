using SMO.Core.Entities.MD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.MD
{
    class T_MD_CHI_PHI_PROFIT_CENTER_Map : BaseMapping<T_MD_CHI_PHI_PROFIT_CENTER>
    {
        public T_MD_CHI_PHI_PROFIT_CENTER_Map()
        {
            Id(x => x.CODE);
            Map(x => x.COST_CENTER_CODE);
            Map(x => x.SAN_BAY_CODE);

            References(x => x.CostCenter, "COST_CENTER_CODE")
                .Not.Insert()
                .Not.Update();
            References(x => x.SanBay, "SAN_BAY_CODE")
                .Not.Insert()
                .Not.Update();

        }
    }
}
