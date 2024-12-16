using SMO.Core.Entities;
using SMO.Core.Entities.MD;
using SMO.Repository.Common;
using SMO.Repository.Interface.MD;

using System.Collections.Generic;
using System.Linq;

namespace SMO.Repository.Implement.MD
{
    public class Report01DRepo : GenericRepository<T_MD_REPORT01D>, IReport01DRepo
    {
        public Report01DRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {

        }

      
    }
}
