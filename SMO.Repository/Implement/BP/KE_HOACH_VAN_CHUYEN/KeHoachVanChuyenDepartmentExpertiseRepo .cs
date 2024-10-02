using SMO.Core.Entities.BP.KE_HOACH_VAN_CHUYEN;
using SMO.Repository.Common;

using SMO.Repository.Interface.BP.KE_HOACH_VAN_CHUYEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Implement.BP.KE_HOACH_VAN_CHUYEN
{
    public class KeHoachVanChuyenDepartmentExpertiseRepo : GenericRepository<T_BP_KE_HOACH_VAN_CHUYEN_DEPARTMENT_EXPERTISE>, IKeHoachVanChuyenDepartmentExpertiseRepo
    {
        public KeHoachVanChuyenDepartmentExpertiseRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {

        }
    }
}
