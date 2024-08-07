﻿using NHibernate.Type;
using SMO.Core.Entities.BP.DAU_TU_XAY_DUNG;
using SMO.Core.Entities.BP.KE_HOACH_CHI_PHI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.BP.DAU_TU_XAY_DUNG
{
    public class T_BP_DAU_TU_XAY_DUNG_EDIT_HISTORY_Map : BaseMapping<T_BP_DAU_TU_XAY_DUNG_EDIT_HISTORY>
    {
        public T_BP_DAU_TU_XAY_DUNG_EDIT_HISTORY_Map()
        {
            Table("T_BP_DAU_TU_XAY_DUNG_EDIT_HISTORY");
            Id(x => x.ID);
            Map(x => x.TEMPLATE_CODE);
            Map(x => x.VERSION);
            Map(x => x.YEAR);
            Map(x => x.TYPE);
            Map(x => x.COST_CENTER_CODE);
            Map(x => x.PROJECT_CODE);
            Map(x => x.ELEMENT_CODE);
            Map(x => x.OLD_VALUE);
            Map(x => x.NEW_VALUE);
            Map(x => x.OLD_DESCRIPTION);
            Map(x => x.NEW_DESCRIPTION);
            Map(x => x.EQUITY_SOURCE);
            Map(x => x.EQUITY_SOURCE_NEW);
            Map(x => x.PROCESS_OLD);
            Map(x => x.PROCESS_NEW);
            Map(x => x.ACTIVE).Not.Nullable().CustomType<YesNoType>();
        }
    }
}
