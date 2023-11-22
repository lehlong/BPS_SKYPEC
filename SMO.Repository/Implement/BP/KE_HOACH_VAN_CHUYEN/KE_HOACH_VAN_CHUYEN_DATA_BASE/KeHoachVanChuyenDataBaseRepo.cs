using SMO.Core.Entities.BP.KE_HOACH_VAN_CHUYEN.KE_HOACH_VAN_CHUYEN_DATA_BASE;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP.KE_HOACH_VAN_CHUYEN.KE_HOACH_VAN_CHUYEN_DATA_BASE;

namespace SMO.Repository.Implement.BP.KE_HOACH_VAN_CHUYEN.KE_HOACH_VAN_CHUYEN_DATA_BASE
{
    public class KeHoachVanChuyenDataBaseRepo : GenericRepository<T_BP_KE_HOACH_VAN_CHUYEN_DATA_BASE>, IKeHoachVanChuyenDataBaseRepo
    {
        public KeHoachVanChuyenDataBaseRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {
        }
    }
}
