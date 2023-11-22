﻿using SMO.Core.Entities.BP.KE_HOACH_CHI_PHI.KE_HOACH_CHI_PHI_DATA_BASE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.BP.KE_HOACH_CHI_PHI.KE_HOACH_CHI_PHI_DATA_BASE
{
    public class T_BP_KE_HOACH_CHI_PHI_DATA_BASE_HISTORY_Mapping : BaseMapping<T_BP_KE_HOACH_CHI_PHI_DATA_BASE_HISTORY>
    {
        public T_BP_KE_HOACH_CHI_PHI_DATA_BASE_HISTORY_Mapping()
        {
            Id(x => x.PKID);

            Map(x => x.ORG_CODE);
            Map(x => x.CHI_PHI_PROFIT_CENTER_CODE);
            Map(x => x.SAN_BAY_CODE);
            Map(x => x.CHI_PHI_CODE);
            Map(x => x.TEMPLATE_CODE);
            Map(x => x.KHOAN_MUC_HANG_HOA_CODE);
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

            References(x => x.KhoanMucHangHoa).Columns("KHOAN_MUC_HANG_HOA_CODE", "TIME_YEAR")
                .Not.Insert().Not.Update();
            References(x => x.ChiPhiProfitCenter, "CHI_PHI_PROFIT_CENTER_CODE")
                .Not.Insert().Not.Update();
            References(x => x.SanBay, "SAN_BAY_CODE")
                .Not.Insert().Not.Update();
            References(x => x.ChiPhi, "CHI_PHI_CODE")
                .Not.Insert().Not.Update();
            References(x => x.Organize, "ORG_CODE")
                .Not.Insert().Not.Update();
            References(x => x.Template, "TEMPLATE_CODE")
                .Not.Insert().Not.Update();
        }
    }
}