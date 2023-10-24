using SMO.Core.Entities.BP.DAU_TU_XAY_DUNG;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP.DAU_TU_XAY_DUNG;

namespace SMO.Repository.Implement.BP.DAU_TU_XAY_DUNG
{
    public class DauTuXayDungReviewCommentRepo : GenericRepository<T_BP_DAU_TU_XAY_DUNG_REVIEW_COMMENT>, IDauTuXayDungReviewCommentRepo
    {
        public DauTuXayDungReviewCommentRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {
        }
    }
}
