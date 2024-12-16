using SMO.Core.Entities;
using SMO.Core.Entities.MD;
using SMO.Repository.Common;
using SMO.Repository.Interface.MD;

using System.Collections.Generic;
using System.Linq;

namespace SMO.Repository.Implement.MD
{
    public class HeaderDmRepo : GenericRepository<T_MD_HEADER_DM>, IHeaderDmRepo
    {
        public HeaderDmRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {

        }
    }
}
