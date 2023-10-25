using SMO.Core.Entities.MD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.MD
{
    class T_MD_DAU_TU_TRANG_THIET_BI_PROFIT_CENTER_Map : BaseMapping<T_MD_DAU_TU_TRANG_THIET_BI_PROFIT_CENTER>
    {
        public T_MD_DAU_TU_TRANG_THIET_BI_PROFIT_CENTER_Map()
        {
            Id(x => x.CODE);
            Map(x => x.ORG_CODE);
            Map(x => x.PROJECT_CODE);

            References(x => x.Organize, "ORG_CODE")
                .Not.Insert()
                .Not.Update();
            References(x => x.Project, "PROJECT_CODE")
                .Not.Insert()
                .Not.Update();

        }
    }
}
