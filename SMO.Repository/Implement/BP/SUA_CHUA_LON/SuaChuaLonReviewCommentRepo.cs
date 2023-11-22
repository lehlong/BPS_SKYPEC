using SMO.Core.Entities.BP.SUA_CHUA_LON;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP.SUA_CHUA_LON;

namespace SMO.Repository.Implement.BP.SUA_CHUA_LON
{
    public class SuaChuaLonReviewCommentRepo : GenericRepository<T_BP_SUA_CHUA_LON_REVIEW_COMMENT>, ISuaChuaLonReviewCommentRepo
    {
        public SuaChuaLonReviewCommentRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {
        }
    }
}
