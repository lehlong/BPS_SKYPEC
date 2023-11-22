using NHibernate.Type;
using SMO.Core.Entities;
using SMO.Core.Entities.BP.SUA_CHUA_THUONG_XUYEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.BP.SUA_CHUA_THUONG_XUYEN
{
    public class T_BP_SUA_CHUA_THUONG_XUYEN_Map : BaseBPMapping<T_BP_SUA_CHUA_THUONG_XUYEN>
    {
        public T_BP_SUA_CHUA_THUONG_XUYEN_Map()
        {
            Map(x => x.TYPE_UPLOAD);
            Map(x => x.KICH_BAN);
            Map(x => x.PHIEN_BAN);
            References(x => x.KichBan).Column("KICH_BAN").Not.Insert().Not.Update().LazyLoad();
            References(x => x.PhienBan).Column("PHIEN_BAN").Not.Insert().Not.Update().LazyLoad();
        }
    }
}
