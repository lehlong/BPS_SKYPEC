using SMO.Core.Entities;
using SMO.Repository.Common;
using SMO.Repository.Interface.MD;

using System.Collections.Generic;
using System.Linq;

namespace SMO.Repository.Implement.MD
{
    public class UnitPricePlanRepo : GenericRepository<T_MD_UNIT_PRICE_PLAN>, IUnitPricePlanRepo
    {
        public UnitPricePlanRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {

        }

        public override IList<T_MD_UNIT_PRICE_PLAN> Search(T_MD_UNIT_PRICE_PLAN objFilter, int pageSize, int pageIndex, out int total)
        {
            var query = Queryable();
            query = query.Where(x => x.YEAR == objFilter.YEAR);

            return base.Paging(query, pageSize, pageIndex, out total).ToList();
        }
    }
}
