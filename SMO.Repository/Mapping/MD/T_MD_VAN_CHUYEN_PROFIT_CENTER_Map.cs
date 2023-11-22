using SMO.Core.Entities.MD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.MD
{
    class T_MD_VAN_CHUYEN_PROFIT_CENTER_Map : BaseMapping<T_MD_VAN_CHUYEN_PROFIT_CENTER>
    {
        public T_MD_VAN_CHUYEN_PROFIT_CENTER_Map()
        {
            Id(x => x.CODE);
            Map(x => x.ORG_CODE);
            Map(x => x.ROUTE_CODE);

            References(x => x.Organize, "ORG_CODE")
                .Not.Insert()
                .Not.Update();
            References(x => x.Route, "ROUTE_CODE")
                .Not.Insert()
                .Not.Update();

        }
    }
}
