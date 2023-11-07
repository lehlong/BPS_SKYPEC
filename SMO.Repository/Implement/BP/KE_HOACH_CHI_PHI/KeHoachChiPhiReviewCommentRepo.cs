using SMO.Core.Entities.BP.KE_HOACH_CHI_PHI;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP.KE_HOACH_CHI_PHI;

namespace SMO.Repository.Implement.BP.KE_HOACH_CHI_PHI
{
    public class KeHoachChiPhiReviewCommentRepo : GenericRepository<T_BP_KE_HOACH_CHI_PHI_REVIEW_COMMENT>, IKeHoachChiPhiReviewCommentRepo
    {
        public KeHoachChiPhiReviewCommentRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {
        }
    }
}
