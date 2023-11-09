﻿using SMO.Core.Entities;

namespace SMO.Repository.Mapping.BP
{
    public class T_BP_KE_HOACH_CHI_PHI_DATA_HISTORY_Map : BaseMapping<T_BP_KE_HOACH_CHI_PHI_DATA_HISTORY>
    {
        public T_BP_KE_HOACH_CHI_PHI_DATA_HISTORY_Map()
        {
            Table("T_BP_KE_HOACH_CHI_PHI_DATA_HISTORY");
            Id(x => x.PKID);
            Map(x => x.ORG_CODE);
            Map(x => x.CENTER_CODE);
            Map(x => x.TEMPLATE_CODE);
            Map(x => x.ELEMENT_CODE);
            Map(x => x.VERSION);
            Map(x => x.TIME_YEAR);
            Map(x => x.VALUE_JAN);
            Map(x => x.VALUE_FEB);
            Map(x => x.VALUE_MAR);
            Map(x => x.VALUE_APR);
            Map(x => x.VALUE_MAY);
            Map(x => x.VALUE_JUN);
            Map(x => x.VALUE_JUL);
            Map(x => x.VALUE_AUG);
            Map(x => x.VALUE_SEP);
            Map(x => x.VALUE_OCT);
            Map(x => x.VALUE_NOV);
            Map(x => x.VALUE_DEC);

            Map(x => x.VALUE_SUM_YEAR_PREVENTIVE);
            Map(x => x.VALUE_SUM_YEAR);

            Map(x => x.DESCRIPTION);
            Map(x => x.STATUS);

            References(x => x.CostElement).Columns("ELEMENT_CODE", "TIME_YEAR")
                .Not.Insert().Not.Update().NotFound.Ignore();
            References(x => x.CostCenter, "CENTER_CODE")
                .Not.Insert().Not.Update();
            References(x => x.Organize, "ORG_CODE")
                .Not.Insert().Not.Update();
            References(x => x.Template, "TEMPLATE_CODE")
                .Not.Insert().Not.Update();

        }
    }
}
