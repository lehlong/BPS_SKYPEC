using SMO.Core.Entities;
using SMO.Repository.Common;
using SMO.Repository.Interface.MD;

using System.Collections.Generic;
using System.Linq;

namespace SMO.Repository.Implement.MD
{
    public class Bm02bRepo : GenericRepository<T_MD_BM02B>, IBm02bRepo
    {
        public Bm02bRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {

        }
       
    }
}
