using SMO.Core.Entities.BU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.BU
{
    public class T_BU_PAYMENT_Map : BaseMapping<T_BU_PAYMENT>
    {
        public T_BU_PAYMENT_Map()
        {
            Table("T_BU_PAYMENT");
            Id(x => x.ID);
            Map(x => x.NUMBER_INVOICE);
            Map(x => x.VALUE_INVOICE);
            Map(x => x.AMOUNT);
            Map(x => x.ADVANCE_PAYMENT);
            Map(x => x.TOTAL);
            Map(x => x.ACTION);
            Map(x => x.NOTE);
            Map(x => x.DATE);
            Map(x => x.CONTENT_PAYMENT);
            Map(x => x.FILE_CHILD);
            Map(x => x.NAME_CONTRACT);
            Map(x => x.VERSION);
            Map(x => x.UPDATE_HUMAN);
            Map(x => x.UPDATE_TIME);
            Map(x => x.IS_DELETE);
        }
    }
}
