using NHibernate.Type;
using SMO.Core.Entities;
using SMO.Core.Entities.MD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.MD
{
    public class T_MD_VOUCHER_Map : BaseMapping<T_MD_VOUCHER>
    {
        public T_MD_VOUCHER_Map()
        {
            Table("T_MD_VOUCHER");
            Id(x => x.VOUCHER_ID);
            Map(x => x.VOUCHER_TYPE_ID).Nullable();
        }
    }
}
