
using SMO.Core.Entities.BP.DAU_TU_NGOAI_DOANH_NGHIEP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.BP.DAU_TU_NGOAI_DOANH_NGHIEP
{
    class T_BP_DAU_TU_NGOAI_DOANH_NGHIEP_REVIEW_COMMENT_Map : BaseMapping<T_BP_DAU_TU_NGOAI_DOANH_NGHIEP_REVIEW_COMMENT>
    {
        public T_BP_DAU_TU_NGOAI_DOANH_NGHIEP_REVIEW_COMMENT_Map()
        {
            Id(x => x.PKID);
            Map(x => x.KHOAN_MUC_DAU_TU_CODE);
            Map(x => x.DATA_VERSION);
            Map(x => x.ORG_CODE);
            Map(x => x.TIME_YEAR);
            Map(x => x.ON_ORG_CODE);
            Map(x => x.NUMBER_COMMENTS);

            References(x => x.Organize, "ORG_CODE")
                .Not.Insert()
                .Not.Update();
            References(x => x.OnCostCenter, "ON_ORG_CODE")
                .Not.Insert()
                .Not.Update();
            References(x => x.KhoanMucDauTu).Columns("KHOAN_MUC_DAU_TU_CODE", "TIME_YEAR")
                .Not.Insert()
                .Not.Update();
            HasMany(x => x.Comments).KeyColumn("REFRENCE_ID")
                .LazyLoad().Inverse().Cascade.Delete();
        }
    }
}
