using SMO.Core.Entities.MD;
using SMO.Repository.Common;
using SMO.Repository.Interface.MD;

using System.Collections.Generic;
using System.Linq;

namespace SMO.Repository.Implement.MD
{
    public class DauTuTrangThietBiProfitCenterRepo : GenericCenterRepository<T_MD_DAU_TU_TRANG_THIET_BI_PROFIT_CENTER>, IDauTuTrangThietBiProfitCenterRepo
    {
        public DauTuTrangThietBiProfitCenterRepo(NHUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override IList<T_MD_DAU_TU_TRANG_THIET_BI_PROFIT_CENTER> Search(T_MD_DAU_TU_TRANG_THIET_BI_PROFIT_CENTER objFilter, int pageSize, int pageIndex, out int total)
        {
            var query = Queryable();

            if (!string.IsNullOrWhiteSpace(objFilter.CODE))
            {
                query = query.Where(x => x.CODE.Contains(objFilter.CODE) || x.NAME.Contains(objFilter.NAME));
            }

            query = query.OrderBy(x => x.CODE);
            return base.Paging(query, pageSize, pageIndex, out total).ToList();
        }
    }
}
