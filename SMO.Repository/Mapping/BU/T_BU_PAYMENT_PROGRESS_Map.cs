using SMO.Core.Entities.BU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.BU
{
    public class T_BU_PAYMENT_PROGRESS_Map : BaseMapping<T_BU_PAYMENT_PROGRESS>
    {
        public T_BU_PAYMENT_PROGRESS_Map()
        {
            Table("T_BU_PAYMENT_PROGRESS");
            Id(x => x.ID);
            Map(x => x.BATCH);
            Map(x => x.PROGRESS);
            Map(x => x.PAYMENT_VALUE);
            Map(x => x.PAYMENT_DESCRIPTION);
            Map(x => x.PROFILE);
            Map(x => x.DATE);
            Map(x => x.STATUS);
            Map(x => x.NAME_CONTRACT);
            Map(x => x.VERSION);
            Map(x => x.UPDATE_HUMAN);
            Map(x => x.UPDATE_TIME);
            Map(x => x.IS_DELETE);
        }
    }
}
