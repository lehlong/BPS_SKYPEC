using SMO.Core.Entities.MD;
using SMO.Repository.Common;
using SMO.Repository.Interface.MD;

using System.Collections.Generic;
using System.Linq;

namespace SMO.Repository.Implement.MD
{
    public class SuaChuaProfitCenterRepo : GenericCenterRepository<T_MD_SUA_CHUA_PROFIT_CENTER>, ISuaChuaProfitCenterRepo
    {
        public SuaChuaProfitCenterRepo(NHUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override IList<T_MD_SUA_CHUA_PROFIT_CENTER> Search(T_MD_SUA_CHUA_PROFIT_CENTER objFilter, int pageSize, int pageIndex, out int total)
        {
            var query = Queryable();

            if (!string.IsNullOrWhiteSpace(objFilter.CODE))
            {
                query = query.Where(x => x.CODE.Contains(objFilter.CODE) || x.NAME.Contains(objFilter.NAME));
            }

            if (!string.IsNullOrWhiteSpace(objFilter.COST_CENTER_CODE))
            {
                query = query.Where(x => x.COST_CENTER_CODE == objFilter.COST_CENTER_CODE);
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
