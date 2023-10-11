using SMO.Core.Entities.BU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.BU
{
    class T_BU_CONTRACT_HUMAN_Map : BaseMapping<T_BU_CONTRACT_HUMAN>
    {
        public T_BU_CONTRACT_HUMAN_Map()
        {
            Table("T_BU_CONTRACT_HUMAN");
            Id(x => x.ID);
            Map(x => x.USERNAME);
            Map(x => x.ACTION);
            Map(x => x.CONTRACT_ID);
            Map(x => x.NOTE);
            Map(x => x.VERSION);
        }
    }
}
