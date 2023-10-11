using SMO.Core.Entities;
using SMO.Core.Entities.BP.KE_HOACH_SAN_LUONG;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP;
using SMO.Repository.Interface.BP.KE_HOACH_SAN_LUONG;

namespace SMO.Repository.Implement.BP.KE_HOACH_SAN_LUONG
{
    public class KeHoachSanLuongDataHistoryRepo : GenericRepository<T_BP_KE_HOACH_SAN_LUONG_DATA_HISTORY>, IKeHoachSanLuongDataHistoryRepo
    {
        public KeHoachSanLuongDataHistoryRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {

        }
    }
}
