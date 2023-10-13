using SMO.Core.Entities;
using SMO.Core.Entities.MD;
using SMO.Repository.Common;
using SMO.Repository.Interface.MD;
using System.Collections.Generic;
using System.Linq;

namespace SMO.Repository.Implement.MD
{
    public class ContractAlertRepo : GenericRepository<T_MD_CONTRACT_ALERT>, IContractAlertRepo
    {
        public ContractAlertRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {

        }

        public override IList<T_MD_CONTRACT_ALERT> Search(T_MD_CONTRACT_ALERT objFilter, int pageSize, int pageIndex, out int total)
        {
            var query = Queryable();
            return base.Paging(query, pageSize, pageIndex, out total).ToList();
        }
    }
}
