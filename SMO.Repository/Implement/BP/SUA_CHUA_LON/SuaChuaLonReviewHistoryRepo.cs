using SMO.Core.Entities.BP.SUA_CHUA_LON;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP.SUA_CHUA_LON;

namespace SMO.Repository.Implement.BP.SUA_CHUA_LON
{
    public class SuaChuaLonReviewHistoryRepo : GenericRepository<T_BP_SUA_CHUA_LON_REVIEW_HISTORY>, ISuaChuaLonReviewHistoryRepo
    {
        public SuaChuaLonReviewHistoryRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {
        }
    }
}
