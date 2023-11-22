using NHibernate.Type;

using SMO.Core.Entities.BP.KE_HOACH_CHI_PHI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.BP.KE_HOACH_CHI_PHI
{
    class T_BP_KE_HOACH_CHI_PHI_REVIEW_RESULT_Map : BaseMapping<T_BP_KE_HOACH_CHI_PHI_REVIEW_RESULT>
    {
        public T_BP_KE_HOACH_CHI_PHI_REVIEW_RESULT_Map()
        {
            Id(x => x.PKID);
            Map(x => x.KHOAN_MUC_HANG_HOA_CODE);
            Map(x => x.HEADER_ID);
            Map(x => x.TIME_YEAR);
            Map(x => x.RESULT).CustomType<YesNoType>();

            References(x => x.Header, "HEADER_ID")
                .Not.Insert()
                .Not.Update();
            References(x => x.KhoanMucHangHoa).Columns("KHOAN_MUC_HANG_HOA_CODE", "TIME_YEAR")
                .Not.Insert()
                .Not.Update();
        }
    }
}
