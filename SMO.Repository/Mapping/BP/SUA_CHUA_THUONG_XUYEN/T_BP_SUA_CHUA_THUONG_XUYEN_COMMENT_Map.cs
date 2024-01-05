using NHibernate.Type;
using SMO.Core.Entities.BP.SUA_CHUA_THUONG_XUYEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.BP.SUA_CHUA_THUONG_XUYEN
{
    public class T_BP_SUA_CHUA_THUONG_XUYEN_COMMENT_Map : BaseMapping<T_BP_SUA_CHUA_THUONG_XUYEN_COMMENT>
    {
        public T_BP_SUA_CHUA_THUONG_XUYEN_COMMENT_Map()
        {
            Table("T_BP_SUA_CHUA_THUONG_XUYEN_COMMENT");
            Id(x => x.ID);
            Map(x => x.TEMPLATE_CODE);
            Map(x => x.VERSION);
            Map(x => x.YEAR);
            Map(x => x.ELEMENT_CODE);
            Map(x => x.COMMENT);
            Map(x => x.ACTIVE).Not.Nullable().CustomType<YesNoType>();
        }
    }
}
