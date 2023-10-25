using SMO.Core.Entities;
using SMO.Core.Entities.BP.DAU_TU_TRANG_THIET_BI;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP;
using SMO.Repository.Interface.BP.DAU_TU_TRANG_THIET_BI;

namespace SMO.Repository.Implement.BP.DAU_TU_TRANG_THIET_BI
{
    public class DauTuTrangThietBiDataHistoryRepo : GenericRepository<T_BP_DAU_TU_TRANG_THIET_BI_DATA_HISTORY>, IDauTuTrangThietBiDataHistoryRepo
    {
        public DauTuTrangThietBiDataHistoryRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {

        }
    }
}
