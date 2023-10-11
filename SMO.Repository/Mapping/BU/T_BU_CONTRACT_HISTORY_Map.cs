using SMO.Core.Entities.BU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.BU
{
     class T_BU_CONTRACT_HISTORY_Map : BaseMapping<T_BU_CONTRACT_HISTORY>
    {
        public T_BU_CONTRACT_HISTORY_Map()
        {
            Table("T_BU_CONTRACT_HISTORY");
            Id(x => x.ID);
            Map(x => x.ACTION);
            Map(x => x.OLD_VALUE);
            Map(x => x.NEW_VALUE);
            Map(x => x.VERSION);
            Map(x => x.NAME_CONTRACT); 
        }
    }
}
