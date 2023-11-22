using SMO.Core.Entities;
using SMO.Core.Entities.BP.SUA_CHUA_LON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.BP.SUA_CHUA_LON
{
    public class T_BP_SUA_CHUA_LON_DATA_HISTORY_Map : BaseMapping<T_BP_SUA_CHUA_LON_DATA_HISTORY>
    {
        public T_BP_SUA_CHUA_LON_DATA_HISTORY_Map()
        {
            Table("T_BP_SUA_CHUA_LON_DATA_HISTORY");
            Id(x => x.PKID);
            Map(x => x.ORG_CODE);
            Map(x => x.SUA_CHUA_PROFIT_CENTER_CODE);
            Map(x => x.TEMPLATE_CODE);
            Map(x => x.KHOAN_MUC_SUA_CHUA_CODE);
            Map(x => x.VERSION);
            Map(x => x.TIME_YEAR);
            Map(x => x.VALUE);
            Map(x => x.QUY_MO);

            Map(x => x.STATUS);
            Map(x => x.DESCRIPTION);

            References(x => x.KhoanMucSuaChua).Columns("KHOAN_MUC_SUA_CHUA_CODE", "TIME_YEAR")
                .Not.Insert().Not.Update();
            References(x => x.SuaChuaProfitCenter, "SUA_CHUA_PROFIT_CENTER_CODE")
                .Not.Insert().Not.Update();
            References(x => x.Organize, "ORG_CODE")
                .Not.Insert().Not.Update();
            References(x => x.Template, "TEMPLATE_CODE")
                .Not.Insert().Not.Update();

        }
    }
}
