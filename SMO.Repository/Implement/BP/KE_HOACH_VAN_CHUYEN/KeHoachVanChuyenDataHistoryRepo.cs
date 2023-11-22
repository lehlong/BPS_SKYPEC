using SMO.Core.Entities;
using SMO.Core.Entities.BP.KE_HOACH_VAN_CHUYEN;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP;
using SMO.Repository.Interface.BP.KE_HOACH_VAN_CHUYEN;

namespace SMO.Repository.Implement.BP.KE_HOACH_VAN_CHUYEN
{
    public class KeHoachVanChuyenDataHistoryRepo : GenericRepository<T_BP_KE_HOACH_VAN_CHUYEN_DATA_HISTORY>, IKeHoachVanChuyenDataHistoryRepo
    {
        public KeHoachVanChuyenDataHistoryRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {

        }
    }
}
