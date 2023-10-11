﻿using NHibernate.Type;

using SMO.Core.Entities.MD;

namespace SMO.Repository.Mapping.MD
{
    public class T_SAP_MD_REVENUE_CF_ELEMENT_Map : BaseMapping<T_SAP_MD_REVENUE_CF_ELEMENT>
    {
        public T_SAP_MD_REVENUE_CF_ELEMENT_Map()
        {
            Table("T_SAP_MD_REVENUE_CF_ELEMENT");
            Id(x => x.CODE);
            Map(x => x.NAME);
            Map(x => x.PARENT_CODE);
            Map(x => x.C_ORDER);
            Map(x => x.ACTIVE).Not.Nullable().CustomType<YesNoType>();
        }
    }
}
