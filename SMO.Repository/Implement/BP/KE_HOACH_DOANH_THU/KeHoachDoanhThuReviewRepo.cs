using SMO.Core.Entities.BP.KE_HOACH_DOANH_THU;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP.KE_HOACH_DOANH_THU;

namespace SMO.Repository.Implement.BP.KE_HOACH_DOANH_THU
{
    public class KeHoachDoanhThuReviewRepo : GenericRepository<T_BP_KE_HOACH_DOANH_THU_REVIEW>, IKeHoachDoanhThuReviewRepo
    {
        public KeHoachDoanhThuReviewRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {
        }
    }
}
