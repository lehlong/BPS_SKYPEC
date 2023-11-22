using SMO.Core.Entities.BP.KE_HOACH_VAN_CHUYEN;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP.KE_HOACH_VAN_CHUYEN;

namespace SMO.Repository.Implement.BP.KE_HOACH_VAN_CHUYEN
{
    public class KeHoachVanChuyenReviewResultRepo : GenericRepository<T_BP_KE_HOACH_VAN_CHUYEN_REVIEW_RESULT>, IKeHoachVanChuyenReviewResultRepo
    {
        public KeHoachVanChuyenReviewResultRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {
        }
    }
}
