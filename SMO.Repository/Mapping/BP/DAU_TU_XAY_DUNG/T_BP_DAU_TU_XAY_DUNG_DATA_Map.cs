﻿using SMO.Core.Entities;
using SMO.Core.Entities.BP.DAU_TU_XAY_DUNG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.BP.DAU_TU_XAY_DUNG
{
    public class T_BP_DAU_TU_XAY_DUNG_DATA_Map : BaseMapping<T_BP_DAU_TU_XAY_DUNG_DATA>
    {
        public T_BP_DAU_TU_XAY_DUNG_DATA_Map()
        {
            Table("T_BP_DAU_TU_XAY_DUNG_DATA");
            Id(x => x.PKID);
            Map(x => x.ORG_CODE);
            Map(x => x.DAU_TU_PROFIT_CENTER_CODE);
            Map(x => x.TEMPLATE_CODE);
            Map(x => x.KHOAN_MUC_DAU_TU_CODE);
            Map(x => x.VERSION);
            Map(x => x.TIME_YEAR);
            Map(x => x.VALUE_1);
            Map(x => x.VALUE_2);
            Map(x => x.VALUE_3);
            Map(x => x.VALUE_4);
            Map(x => x.VALUE_5);
            Map(x => x.VALUE_6);
            Map(x => x.VALUE_7);
            Map(x => x.VALUE_8);

            Map(x => x.MONTH);
            Map(x => x.DESCRIPTION);
            Map(x => x.STATUS);
            References(x => x.KhoanMucDauTu).Columns("KHOAN_MUC_DAU_TU_CODE", "TIME_YEAR")
                .Not.Insert().Not.Update();
            References(x => x.DauTuXayDungProfitCenter, "DAU_TU_PROFIT_CENTER_CODE")
                .Not.Insert().Not.Update();
            References(x => x.Organize, "ORG_CODE")
                .Not.Insert().Not.Update();
            References(x => x.Template, "TEMPLATE_CODE")
                .Not.Insert().Not.Update();
        }
    }
}
