using SMO.Core.Entities.BP.KE_HOACH_SAN_LUONG.KE_HOACH_SAN_LUONG_DATA_BASE;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP.KE_HOACH_SAN_LUONG.KE_HOACH_SAN_LUONG_DATA_BASE;

namespace SMO.Repository.Implement.BP.KE_HOACH_SAN_LUONG.KE_HOACH_SAN_LUONG_DATA_BASE
{
    public class KeHoachSanLuongDataBaseRepo : GenericRepository<T_BP_KE_HOACH_SAN_LUONG_DATA_BASE>, IKeHoachSanLuongDataBaseRepo
    {
        public KeHoachSanLuongDataBaseRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {
        }
    }
}
