using SMO.Core.Entities;
using SMO.Core.Entities.BP.KE_HOACH_CHI_PHI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.BP.KE_HOACH_CHI_PHI
{
    public class T_BP_KE_HOACH_CHI_PHI_DATA_Map : BaseMapping<T_BP_KE_HOACH_CHI_PHI_DATA>
    {
        public T_BP_KE_HOACH_CHI_PHI_DATA_Map()
        {
            Table("T_BP_KE_HOACH_CHI_PHI_DATA");
            Id(x => x.PKID);
            Map(x => x.ORG_CODE);
            Map(x => x.CHI_PHI_PROFIT_CENTER_CODE);
            Map(x => x.TEMPLATE_CODE);
            Map(x => x.KHOAN_MUC_HANG_HOA_CODE);
            Map(x => x.VERSION);
            Map(x => x.TIME_YEAR);
            Map(x => x.QUANTITY);
            Map(x => x.PRICE);
            Map(x => x.AMOUNT);
            Map(x => x.QUY_MO);

            Map(x => x.DESCRIPTION);
            Map(x => x.STATUS);

            References(x => x.KhoanMucHangHoa).Columns("KHOAN_MUC_HANG_HOA_CODE", "TIME_YEAR")
                .Not.Insert().Not.Update();
            References(x => x.ChiPhiProfitCenter, "CHI_PHI_PROFIT_CENTER_CODE")
                .Not.Insert().Not.Update();
            References(x => x.Organize, "ORG_CODE")
                .Not.Insert().Not.Update();
            References(x => x.Template, "TEMPLATE_CODE")
                .Not.Insert().Not.Update();
        }
    }
}
