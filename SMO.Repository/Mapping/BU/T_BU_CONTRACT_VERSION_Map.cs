using SMO.Core.Entities.BU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.BU
{
    class T_BU_CONTRACT_VERSION_Map : BaseMapping<T_BU_CONTRACT_VERSION>
    {
        public T_BU_CONTRACT_VERSION_Map()
        {
            Table("T_BU_CONTRACT_VERSION");
            Id(x => x.ID);
            Map(x => x.PARENT);
            Map(x => x.NAME);
            Map(x => x.CONTRACT_NUMBER);
            Map(x => x.CONTRACT_TYPE);
            Map(x => x.START_DATE);
            Map(x => x.FINISH_DATE);
            Map(x => x.CONTRACT_VALUE);
            Map(x => x.VAT);
            Map(x => x.CONTRACT_VALUE_VAT);
            Map(x => x.NOTES);
            Map(x => x.REPRESENT_A);
            Map(x => x.REPRESENT_B);
            Map(x => x.VERSION);
            Map(x => x.CONTRACT_PHASE);
            Map(x => x.APPROVER);
            Map(x => x.CONTRACT_MANAGER);
            Map(x => x.DEPARTMENT);
            Map(x => x.CUSTOMER);
            Map(x => x.STATUS);
            Map(x => x.NAME_CONTRACT);
            Map(x => x.FILE_CHILD);
        }
    }
}
