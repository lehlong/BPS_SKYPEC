using SMO.Core.Entities.BP.DAU_TU_XAY_DUNG;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP.DAU_TU_XAY_DUNG;

namespace SMO.Repository.Implement.BP.DAU_TU_XAY_DUNG
{
    public class DauTuXayDungReviewRepo : GenericRepository<T_BP_DAU_TU_XAY_DUNG_REVIEW>, IDauTuXayDungReviewRepo
    {
        public DauTuXayDungReviewRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {
        }
    }
}
