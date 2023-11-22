using SMO.Core.Entities.BP.KE_HOACH_VAN_CHUYEN;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP.DAU_TU_XAY_DUNG;
using SMO.Repository.Interface.BP.KE_HOACH_VAN_CHUYEN;

namespace SMO.Repository.Implement.BP.KE_HOACH_VAN_CHUYEN
{
    public class KeHoachVanChuyenReviewHistoryRepo : GenericRepository<T_BP_KE_HOACH_VAN_CHUYEN_REVIEW_HISTORY>, IKeHoachVanChuyenReviewHistoryRepo
    {
        public KeHoachVanChuyenReviewHistoryRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {
        }
    }
}
