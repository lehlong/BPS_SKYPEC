﻿using SMO.Core.Entities.BP.CONTRUCT_COST_CF.CONTRUCT_COST_CF_DATA_BASE;

namespace SMO.Repository.Mapping.BP.CONTRUCT_COST_CF.CONTRUCT_COST_CF_DATA_BASE
{
    public class T_BP_CONTRUCT_COST_CF_DATA_BASE_Mapping : BaseMapping<T_BP_CONTRUCT_COST_CF_DATA_BASE>
    {
        public T_BP_CONTRUCT_COST_CF_DATA_BASE_Mapping()
        {
            Id(x => x.PKID);

            Map(x => x.ORG_CODE);
            Map(x => x.INTERNAL_ORDER_CODE);
            Map(x => x.TEMPLATE_CODE);
            Map(x => x.COST_CF_ELEMENT_CODE);
            Map(x => x.VERSION);
            Map(x => x.TIME_YEAR);
            Map(x => x.MATERIAL);
            Map(x => x.UNIT);

            Map(x => x.QUANTITY_M1);
            Map(x => x.PRICE_M1);
            Map(x => x.AMOUNT_M1);
            Map(x => x.TIME_M1);

            Map(x => x.QUANTITY_M2);
            Map(x => x.PRICE_M2);
            Map(x => x.AMOUNT_M2);
            Map(x => x.TIME_M2);

            Map(x => x.QUANTITY_M3);
            Map(x => x.PRICE_M3);
            Map(x => x.AMOUNT_M3);
            Map(x => x.TIME_M3);

            Map(x => x.QUANTITY_M4);
            Map(x => x.PRICE_M4);
            Map(x => x.AMOUNT_M4);
            Map(x => x.TIME_M4);

            Map(x => x.QUANTITY_M5);
            Map(x => x.PRICE_M5);
            Map(x => x.AMOUNT_M5);
            Map(x => x.TIME_M5);

            Map(x => x.QUANTITY_M6);
            Map(x => x.PRICE_M6);
            Map(x => x.AMOUNT_M6);
            Map(x => x.TIME_M6);

            Map(x => x.QUANTITY_M7);
            Map(x => x.PRICE_M7);
            Map(x => x.AMOUNT_M7);
            Map(x => x.TIME_M7);

            Map(x => x.QUANTITY_M8);
            Map(x => x.PRICE_M8);
            Map(x => x.AMOUNT_M8);
            Map(x => x.TIME_M8);

            Map(x => x.QUANTITY_M9);
            Map(x => x.PRICE_M9);
            Map(x => x.AMOUNT_M9);
            Map(x => x.TIME_M9);

            Map(x => x.QUANTITY_M10);
            Map(x => x.PRICE_M10);
            Map(x => x.AMOUNT_M10);
            Map(x => x.TIME_M10);

            Map(x => x.QUANTITY_M11);
            Map(x => x.PRICE_M11);
            Map(x => x.AMOUNT_M11);
            Map(x => x.TIME_M11);

            Map(x => x.QUANTITY_M12);
            Map(x => x.PRICE_M12);
            Map(x => x.AMOUNT_M12);
            Map(x => x.TIME_M12);

            Map(x => x.AMOUNT_YEAR_PREVENTIVE);
            Map(x => x.AMOUNT_YEAR);

            Map(x => x.DESCRIPTION);

            References(x => x.CostElement).Columns("COST_CF_ELEMENT_CODE", "TIME_YEAR")
                .Not.Insert().Not.Update();
            References(x => x.InternalOrder, "INTERNAL_ORDER_CODE")
                .Not.Insert().Not.Update();
            References(x => x.Organize, "ORG_CODE")
                .Not.Insert().Not.Update();
            References(x => x.Template, "TEMPLATE_CODE")
                .Not.Insert().Not.Update();
        }
    }
}
