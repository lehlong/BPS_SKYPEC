using NHibernate.Type;

using SMO.Core.Entities.BP.DAU_TU_TRANG_THIET_BI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.BP.DAU_TU_TRANG_THIET_BI
{
    class T_BP_DAU_TU_TRANG_THIET_BI_REVIEW_RESULT_Map : BaseMapping<T_BP_DAU_TU_TRANG_THIET_BI_REVIEW_RESULT>
    {
        public T_BP_DAU_TU_TRANG_THIET_BI_REVIEW_RESULT_Map()
        {
            Id(x => x.PKID);
            Map(x => x.KHOAN_MUC_DAU_TU_CODE);
            Map(x => x.HEADER_ID);
            Map(x => x.TIME_YEAR);
            Map(x => x.RESULT).CustomType<YesNoType>();

            References(x => x.Header, "HEADER_ID")
                .Not.Insert()
                .Not.Update();
            References(x => x.KhoanMucDauTu).Columns("KHOAN_MUC_DAU_TU_CODE", "TIME_YEAR")
                .Not.Insert()
                .Not.Update();
        }
    }
}
