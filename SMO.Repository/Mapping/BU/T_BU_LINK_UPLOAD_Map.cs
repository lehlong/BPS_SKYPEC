using SMO.Core.Entities.BU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.BU
{
    public class T_BU_LINK_UPLOAD_Map : BaseMapping<T_BU_LINK_UPLOAD>
    {
        public T_BU_LINK_UPLOAD_Map()
        {
            Table("T_BU_LINK_UPLOAD");
            Id(x => x.ID);
            Map(x => x.PARENT);
            Map(x => x.LINK);
            Map(x => x.IS_DELETE);
            Map(x => x.VERSION);
        }
    }
}
