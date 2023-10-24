using SMO.Core.Entities.BP.DAU_TU_XAY_DUNG;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP.DAU_TU_XAY_DUNG;

namespace SMO.Repository.Implement.BP.DAU_TU_XAY_DUNG
{
    public class DauTuXayDungReviewResultRepo : GenericRepository<T_BP_DAU_TU_XAY_DUNG_REVIEW_RESULT>, IDauTuXayDungReviewResultRepo
    {
        public DauTuXayDungReviewResultRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {
        }
    }
}
