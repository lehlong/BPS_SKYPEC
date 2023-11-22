using SMO.Core.Entities.BP.SUA_CHUA_THUONG_XUYEN.SUA_CHUA_THUONG_XUYEN_DATA_BASE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.BP.SUA_CHUA_THUONG_XUYEN.SUA_CHUA_THUONG_XUYEN_DATA_BASE
{
    public class T_BP_SUA_CHUA_THUONG_XUYEN_DATA_BASE_HISTORY_Mapping : BaseMapping<T_BP_SUA_CHUA_THUONG_XUYEN_DATA_BASE_HISTORY>
    {
        public T_BP_SUA_CHUA_THUONG_XUYEN_DATA_BASE_HISTORY_Mapping()
        {
            Id(x => x.PKID);

            Map(x => x.ORG_CODE);
            Map(x => x.SUA_CHUA_PROFIT_CENTER_CODE);
            Map(x => x.SAN_BAY_CODE);
            Map(x => x.COST_CENTER_CODE);
            Map(x => x.TEMPLATE_CODE);
            Map(x => x.KHOAN_MUC_SUA_CHUA_CODE);
            Map(x => x.VERSION);
            Map(x => x.TIME_YEAR);
            Map(x => x.MATERIAL);
            Map(x => x.UNIT);

            Map(x => x.QUANTITY);
            Map(x => x.PRICE);
            Map(x => x.AMOUNT);
            Map(x => x.TIME);
            Map(x => x.QUY_MO);
            Map(x => x.DESCRIPTION);

            References(x => x.KhoanMucSuaChua).Columns("KHOAN_MUC_SUA_CHUA_CODE", "TIME_YEAR")
                .Not.Insert().Not.Update();
            References(x => x.SuaChuaProfitCenter, "SUA_CHUA_PROFIT_CENTER_CODE")
                .Not.Insert().Not.Update();
            References(x => x.SanBay, "SAN_BAY_CODE")
                .Not.Insert().Not.Update();
            References(x => x.CostCenter, "COST_CENTER_CODE")
                .Not.Insert().Not.Update();
            References(x => x.Organize, "ORG_CODE")
                .Not.Insert().Not.Update();
            References(x => x.Template, "TEMPLATE_CODE")
                .Not.Insert().Not.Update();
        }
    }
}
