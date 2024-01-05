using SMO.Core.Entities.BP.SUA_CHUA_THUONG_XUYEN;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP.SUA_CHUA_THUONG_XUYEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Implement.BP.SUA_CHUA_THUONG_XUYEN
{
    public class SuaChuaThuongXuyenDepartmentExpertiseRepo : GenericRepository<T_BP_SUA_CHUA_THUONG_XUYEN_DEPARTMENT_EXPERTISE>, ISuaChuaThuongXuyenDepartmentExpertiseRepo
    {
        public SuaChuaThuongXuyenDepartmentExpertiseRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {

        }
    }
}
