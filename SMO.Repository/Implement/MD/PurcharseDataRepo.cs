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
    }
}
