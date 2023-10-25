using SMO.Core.Entities.BP.DAU_TU_TRANG_THIET_BI.DAU_TU_TRANG_THIET_BI_DATA_BASE;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP.DAU_TU_TRANG_THIET_BI.DAU_TU_TRANG_THIET_BI_DATA_BASE;

namespace SMO.Repository.Implement.BP.DAU_TU_TRANG_THIET_BI.DAU_TU_TRANG_THIET_BI_DATA_BASE
{
    public class DauTuTrangThietBiDataBaseHistoryRepo : GenericRepository<T_BP_DAU_TU_TRANG_THIET_BI_DATA_BASE_HISTORY>, IDauTuTrangThietBiDataBaseHistoryRepo
    {
        public DauTuTrangThietBiDataBaseHistoryRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {
        }
    }
}
