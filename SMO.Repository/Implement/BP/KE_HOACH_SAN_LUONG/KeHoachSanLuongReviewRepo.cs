using SMO.Core.Entities.BP.KE_HOACH_SAN_LUONG;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP.KE_HOACH_SAN_LUONG;

namespace SMO.Repository.Implement.BP.KE_HOACH_SAN_LUONG
{
    public class KeHoachSanLuongReviewRepo : GenericRepository<T_BP_KE_HOACH_SAN_LUONG_REVIEW>, IKeHoachSanLuongReviewRepo
    {
        public KeHoachSanLuongReviewRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {
        }
    }
}
