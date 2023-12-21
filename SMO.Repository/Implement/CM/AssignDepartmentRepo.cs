using SMO.Core.Entities.CM;
using SMO.Repository.Common;
using SMO.Repository.Interface.CM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Implement.CM
{
    public class AssignDepartmentRepo : GenericRepository<T_CM_ASSIGN_DEPARTMENT>, IAssignDepartmentRepo
    {
        public AssignDepartmentRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {

        }
    }
}
