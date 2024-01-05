using SMO.Core.Entities.BP.SUA_CHUA_LON;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP.SUA_CHUA_LON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Implement.BP.SUA_CHUA_LON
{
    public class SuaChuaLonDepartmentAssignRepo : GenericRepository<T_BP_SUA_CHUA_LON_DEPARTMENT_ASSIGN>, ISuaChuaLonDepartmentAssignRepo
    {
        public SuaChuaLonDepartmentAssignRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {

        }
    }
}
