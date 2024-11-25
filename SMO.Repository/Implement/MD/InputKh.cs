using SMO.Core.Entities;
using SMO.Core.Entities.MD;
using SMO.Repository.Common;
using SMO.Repository.Interface.MD;

using System.Collections.Generic;
using System.Linq;

namespace SMO.Repository.Implement.MD
{
    public class InputKhRepo : GenericRepository<T_MD_INPUT_KH>, IInputKhRepo
    {
        public InputKhRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {

        }
    }
}
