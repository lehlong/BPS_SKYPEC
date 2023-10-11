﻿using NHibernate.Type;

using SMO.Core.Entities;

namespace SMO.Repository.Mapping.MD
{
    public class T_MD_AREA_Map : BaseMapping<T_MD_AREA>
    {
        public T_MD_AREA_Map()
        {
            Table("T_MD_AREA");
            Id(x => x.CODE);
            Map(x => x.TEXT).Nullable();
            Map(x => x.ACTIVE).Not.Nullable().CustomType<YesNoType>();
        }
    }
}
