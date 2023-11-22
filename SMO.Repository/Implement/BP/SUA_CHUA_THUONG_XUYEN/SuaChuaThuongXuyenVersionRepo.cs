using SMO.Core.Entities;
using SMO.Core.Entities.BP.SUA_CHUA_THUONG_XUYEN;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP;
using SMO.Repository.Interface.BP.SUA_CHUA_THUONG_XUYEN;

namespace SMO.Repository.Implement.BP.SUA_CHUA_THUONG_XUYEN
{
    public class SuaChuaThuongXuyenVersionRepo : GenericRepository<T_BP_SUA_CHUA_THUONG_XUYEN_VERSION>, ISuaChuaThuongXuyenVersionRepo
    {
        public SuaChuaThuongXuyenVersionRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {

        }
    }
}
