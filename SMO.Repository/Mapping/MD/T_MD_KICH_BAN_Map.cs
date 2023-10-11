﻿using NHibernate.Type;

using SMO.Core.Entities;

namespace SMO.Repository.Mapping.MD
{
    public class T_MD_KICH_BAN_Map : BaseMapping<T_MD_KICH_BAN>
    {
        public T_MD_KICH_BAN_Map()
        {
            Table("T_MD_KICH_BAN");
            Id(x => x.CODE);
            Map(x => x.TEXT).Nullable();
            Map(x => x.ACTIVE).Not.Nullable().CustomType<YesNoType>();
        }
    }
}
