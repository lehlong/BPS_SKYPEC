using SMO.Core.Entities.BP.KE_HOACH_DOANH_THU.KE_HOACH_DOANH_THU_DATA_BASE;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP.KE_HOACH_DOANH_THU.KE_HOACH_DOANH_THU_DATA_BASE;

namespace SMO.Repository.Implement.BP.KE_HOACH_DOANH_THU.KE_HOACH_DOANH_THU_DATA_BASE
{
    public class KeHoachSanLuongDataBaseRepo : GenericRepository<T_BP_KE_HOACH_DOANH_THU_DATA_BASE>, IKeHoachDoanhThuDataBaseRepo
    {
        public KeHoachSanLuongDataBaseRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {
        }
    }
}
