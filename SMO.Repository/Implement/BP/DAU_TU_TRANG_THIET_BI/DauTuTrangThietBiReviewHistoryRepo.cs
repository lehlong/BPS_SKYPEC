using SMO.Core.Entities.BP.DAU_TU_TRANG_THIET_BI;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP.DAU_TU_TRANG_THIET_BI;

namespace SMO.Repository.Implement.BP.DAU_TU_TRANG_THIET_BI
{
    public class DauTuTrangThietBiReviewHistoryRepo : GenericRepository<T_BP_DAU_TU_TRANG_THIET_BI_REVIEW_HISTORY>, IDauTuTrangThietBiReviewHistoryRepo
    {
        public DauTuTrangThietBiReviewHistoryRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {
        }
    }
}
