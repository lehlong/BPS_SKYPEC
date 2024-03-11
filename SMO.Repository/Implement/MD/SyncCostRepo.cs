using SMO.Core.Entities;
using SMO.Repository.Common;
using SMO.Repository.Interface.MD;

using System.Collections.Generic;
using System.Linq;

namespace SMO.Repository.Implement.MD
{
    public class SyncCostRepo : GenericRepository<T_MD_SYNC_COST>, ISyncCostRepo
    {
        public SyncCostRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {

        }

        public override IList<T_MD_SYNC_COST> Search(T_MD_SYNC_COST objFilter, int pageSize, int pageIndex, out int total)
        {
            var query = Queryable();
            query = query.Where(x => x.YEAR == objFilter.YEAR);
            query = query.OrderByDescending(x => x.ACTIVE);
            return base.Paging(query, pageSize, pageIndex, out total).ToList();
        }
    }
}
