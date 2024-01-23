using SMO.Core.Entities;
using SMO.Repository.Common;
using SMO.Repository.Interface.MD;

using System.Collections.Generic;
using System.Linq;

namespace SMO.Repository.Implement.MD
{
    public class PurchaseDataRepo : GenericRepository<T_MD_PURCHASE_DATA>, IPurchaseDataRepo
    {
        public PurchaseDataRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {

        }
        public override IList<T_MD_PURCHASE_DATA> Search(T_MD_PURCHASE_DATA objFilter, int pageSize, int pageIndex, out int total)
        {
            var query = Queryable();
            query = query.Where(x => x.TIME_YEAR == objFilter.TIME_YEAR);
            query = query.OrderByDescending(x => x.ACTIVE);
            return base.Paging(query, pageSize, pageIndex, out total).ToList();
        }
    }
}
