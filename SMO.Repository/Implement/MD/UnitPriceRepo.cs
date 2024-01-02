using SMO.Core.Entities;
using SMO.Repository.Common;
using SMO.Repository.Interface.MD;

using System.Collections.Generic;
using System.Linq;

namespace SMO.Repository.Implement.MD
{
    public class UnitPriceRepo : GenericRepository<T_MD_UNIT_PRICE>, IUnitPriceRepo
    {
        public UnitPriceRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {

        }

        public override IList<T_MD_UNIT_PRICE> Search(T_MD_UNIT_PRICE objFilter, int pageSize, int pageIndex, out int total)
        {
            var query = Queryable();
            query = query.Where(x => x.YEAR == objFilter.YEAR);

            if (!string.IsNullOrEmpty(objFilter.OBJECT_ID))
            {
                query = query.Where(x => x.OBJECT_ID == objFilter.OBJECT_ID);
            }
            if (!string.IsNullOrEmpty(objFilter.WAREHOUSE_ID))
            {
                query = query.Where(x => x.WAREHOUSE_ID == objFilter.WAREHOUSE_ID);
            }

            query = query.OrderByDescending(x => x.ACTIVE);
            return base.Paging(query, pageSize, pageIndex, out total).ToList();
        }
    }
}
