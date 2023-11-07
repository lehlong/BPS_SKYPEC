using SMO.Core.Entities.BP.KE_HOACH_CHI_PHI.KE_HOACH_CHI_PHI_DATA_BASE;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP.KE_HOACH_CHI_PHI.KE_HOACH_CHI_PHI_DATA_BASE;

namespace SMO.Repository.Implement.BP.KE_HOACH_CHI_PHI.KE_HOACH_CHI_PHI_DATA_BASE
{
    public class KeHoachChiPhiDataBaseHistoryRepo : GenericRepository<T_BP_KE_HOACH_CHI_PHI_DATA_BASE_HISTORY>, IKeHoachChiPhiDataBaseHistoryRepo
    {
        public KeHoachChiPhiDataBaseHistoryRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {
        }
    }
}
