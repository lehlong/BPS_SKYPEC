﻿using NHibernate.Type;
using SMO.Core.Entities.MD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.MD
{
    class T_MD_KHOAN_MUC_HANG_HOA_Map : BaseMapping<T_MD_KHOAN_MUC_HANG_HOA>
    {
        public T_MD_KHOAN_MUC_HANG_HOA_Map()
        {
            CompositeId()
                .KeyProperty(x => x.CODE)
                .KeyProperty(x => x.TIME_YEAR);
            Map(x => x.C_ORDER);
            Map(x => x.PARENT_CODE);
            Map(x => x.NAME);
            Map(x => x.ACTIVE).CustomType<YesNoType>();
            Map(x => x.IS_GROUP).CustomType<YesNoType>();
        }
    }
}
