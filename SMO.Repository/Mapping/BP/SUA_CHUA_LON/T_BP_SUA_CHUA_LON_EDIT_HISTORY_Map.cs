﻿using NHibernate.Type;
using SMO.Core.Entities.BP.SUA_CHUA_LON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.BP.SUA_CHUA_LON
{
    public class T_BP_SUA_CHUA_LON_EDIT_HISTORY_Map : BaseMapping<T_BP_SUA_CHUA_LON_EDIT_HISTORY>
    {
        public T_BP_SUA_CHUA_LON_EDIT_HISTORY_Map()
        {
            Table("T_BP_SUA_CHUA_LON_EDIT_HISTORY");
            Id(x => x.ID);
            Map(x => x.TEMPLATE_CODE);
            Map(x => x.VERSION);
            Map(x => x.YEAR);
            Map(x => x.TYPE);
            Map(x => x.COST_CENTER_CODE);
            Map(x => x.SAN_BAY_CODE);
            Map(x => x.ELEMENT_CODE);
            Map(x => x.OLD_VALUE);
            Map(x => x.NEW_VALUE);
            Map(x => x.OLD_DESCRIPTION);
            Map(x => x.NEW_DESCRIPTION);
            Map(x => x.ACTIVE).Not.Nullable().CustomType<YesNoType>();
        }
    }
}
