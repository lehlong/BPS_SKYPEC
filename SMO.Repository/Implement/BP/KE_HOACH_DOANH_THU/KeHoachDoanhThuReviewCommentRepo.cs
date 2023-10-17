using SMO.Core.Entities.BP.KE_HOACH_DOANH_THU;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP.KE_HOACH_DOANH_THU;

namespace SMO.Repository.Implement.BP.KE_HOACH_DOANH_THU
{
    public class KeHoachDoanhThuReviewCommentRepo : GenericRepository<T_BP_KE_HOACH_DOANH_THU_REVIEW_COMMENT>, IKeHoachDoanhThuReviewCommentRepo
    {
        public KeHoachDoanhThuReviewCommentRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {
        }
    }
}
