using SMO.Core.Entities.BP.KE_HOACH_VAN_CHUYEN.KE_HOACH_VAN_CHUYEN_DATA_BASE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.BP.KE_HOACH_VAN_CHUYEN.KE_HOACH_VAN_CHUYEN_DATA_BASE
{
    public class T_BP_KE_HOACH_VAN_CHUYEN_DATA_BASE_HISTORY_Mapping : BaseMapping<T_BP_KE_HOACH_VAN_CHUYEN_DATA_BASE_HISTORY>
    {
        public T_BP_KE_HOACH_VAN_CHUYEN_DATA_BASE_HISTORY_Mapping()
        {
            Id(x => x.PKID);

            Map(x => x.ORG_CODE);
            Map(x => x.VAN_CHUYEN_PROFIT_CENTER_CODE);
            Map(x => x.TEMPLATE_CODE);
            Map(x => x.KHOAN_MUC_VAN_CHUYEN_CODE);
            Map(x => x.VERSION);
            Map(x => x.TIME_YEAR);
            Map(x => x.MATERIAL);
            Map(x => x.UNIT);

            Map(x => x.QUANTITY);
            Map(x => x.PRICE);
            Map(x => x.AMOUNT);
            Map(x => x.TIME);

            Map(x => x.DESCRIPTION);
            Map(x => x.PROCESS);

            References(x => x.KhoanMucDauTu).Columns("KHOAN_MUC_VAN_CHUYEN_CODE", "TIME_YEAR")
                .Not.Insert().Not.Update();
            References(x => x.VanChuyenProfitCenter, "VAN_CHUYEN_PROFIT_CENTER_CODE")
                .Not.Insert().Not.Update();
            References(x => x.Organize, "ORG_CODE")
                .Not.Insert().Not.Update();
            References(x => x.Template, "TEMPLATE_CODE")
                .Not.Insert().Not.Update();
        }
    }
}
