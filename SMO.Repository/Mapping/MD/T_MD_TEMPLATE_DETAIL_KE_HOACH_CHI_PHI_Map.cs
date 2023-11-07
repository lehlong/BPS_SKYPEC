using SMO.Core.Entities.MD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.MD
{
    class T_MD_TEMPLATE_DETAIL_KE_HOACH_CHI_PHI_Map : BaseMapping<T_MD_TEMPLATE_DETAIL_KE_HOACH_CHI_PHI>
    {
        public T_MD_TEMPLATE_DETAIL_KE_HOACH_CHI_PHI_Map()
        {
            Id(x => x.PKID);
            Map(x => x.CENTER_CODE, "CENTER_CODE");
            Map(x => x.ELEMENT_CODE, "ELEMENT_CODE");
            Map(x => x.TEMPLATE_CODE);
            Map(x => x.TIME_YEAR);

            References(x => x.Center, "CENTER_CODE")
                .Not.Insert().Not.Update();
            References(x => x.Element).Columns("ELEMENT_CODE", "TIME_YEAR")
                .Not.Insert().Not.Update();
            References(x => x.Template, "TEMPLATE_CODE")
                .Not.Insert().Not.Update();
        }
    }
}
