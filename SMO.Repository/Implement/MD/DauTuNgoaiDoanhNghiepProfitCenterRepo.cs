using SMO.Core.Entities.MD;
using SMO.Repository.Common;
using SMO.Repository.Interface.MD;

using System.Collections.Generic;
using System.Linq;

namespace SMO.Repository.Implement.MD
{
    public class DauTuNgoaiDoanhNghiepProfitCenterRepo : GenericCenterRepository<T_MD_DAU_TU_NGOAI_DOANH_NGHIEP_PROFIT_CENTER>, IDauTuNgoaiDoanhNghiepProfitCenterRepo
    {
        public DauTuNgoaiDoanhNghiepProfitCenterRepo(NHUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override IList<T_MD_DAU_TU_NGOAI_DOANH_NGHIEP_PROFIT_CENTER> Search(T_MD_DAU_TU_NGOAI_DOANH_NGHIEP_PROFIT_CENTER objFilter, int pageSize, int pageIndex, out int total)
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
