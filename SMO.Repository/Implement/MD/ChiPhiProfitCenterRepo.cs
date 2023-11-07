using SMO.Core.Entities.MD;
using SMO.Repository.Common;
using SMO.Repository.Interface.MD;

using System.Collections.Generic;
using System.Linq;

namespace SMO.Repository.Implement.MD
{
    public class ChiPhiProfitCenterRepo : GenericCenterRepository<T_MD_CHI_PHI_PROFIT_CENTER>, IChiPhiProfitCenterRepo
    {
        public ChiPhiProfitCenterRepo(NHUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override IList<T_MD_CHI_PHI_PROFIT_CENTER> Search(T_MD_CHI_PHI_PROFIT_CENTER objFilter, int pageSize, int pageIndex, out int total)
        {
            var query = Queryable();

            if (!string.IsNullOrWhiteSpace(objFilter.CODE))
            {
                query = query.Where(x => x.CODE.Contains(objFilter.CODE) || x.NAME.Contains(objFilter.NAME));
            }

            if (!string.IsNullOrWhiteSpace(objFilter.HANG_HANG_KHONG_CODE))
            {
                query = query.Where(x => x.HANG_HANG_KHONG_CODE == objFilter.HANG_HANG_KHONG_CODE);
            }

            if (!string.IsNullOrWhiteSpace(objFilter.SAN_BAY_CODE))
            {
                query = query.Where(x => x.SAN_BAY_CODE == objFilter.SAN_BAY_CODE);
            }

            query = query.OrderBy(x => x.SAN_BAY_CODE).ThenBy(x => x.SAN_BAY_CODE).ThenBy(x => x.CODE);
            return base.Paging(query, pageSize, pageIndex, out total).ToList();
        }
    }
}
