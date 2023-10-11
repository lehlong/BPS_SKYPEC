using SMO.Core.Entities.BP.COST_CF;
using SMO.Core.Entities.BP.KE_HOACH_SAN_LUONG;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP.COST_CF;
using SMO.Repository.Interface.BP.KE_HOACH_SAN_LUONG;

namespace SMO.Repository.Implement.BP.KE_HOACH_SAN_LUONG
{
    public class KeHoachSanLuongReviewHistoryRepo : GenericRepository<T_BP_KE_HOACH_SAN_LUONG_REVIEW_HISTORY>, IKeHoachSanLuongReviewHistoryRepo
    {
        public KeHoachSanLuongReviewHistoryRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {
        }
    }
}
