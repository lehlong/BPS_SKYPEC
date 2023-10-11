using SMO.Core.Entities.BU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.BU
{
    class T_BU_CONTACT_PROCESS_Map : BaseMapping<T_BU_CONTRACT_PROCESS>
    {
        public T_BU_CONTACT_PROCESS_Map()
        {
            Table("T_BU_CONTACT_PROCESS");
            Id(x=>x.ID);
            Map(x => x.VERSION);
            Map(x => x.ACTION);
            Map(x => x.CONTRACT_NAME);
        }
    }
}
