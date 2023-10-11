using SMO.Core.Entities.BU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.BU
{
     class T_BU_CONTRACT_COMMNET_Map : BaseMapping<T_BU_CONTRACT_COMMENT>
    {
        T_BU_CONTRACT_COMMNET_Map()
        {
            Table("T_BU_CONTRACT_COMMNET");
            Id(x => x.ID);
            Map(x => x.COMMENT);
            Map(x => x.VERSION);
            Map(x => x.CONTRACT_NAME);
        }
    }
}
