using SMO.Core.Entities;
using SMO.Core.Entities.BP.DAU_TU_TRANG_THIET_BI;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP;
using SMO.Repository.Interface.BP.DAU_TU_TRANG_THIET_BI;

namespace SMO.Repository.Implement.BP.DAU_TU_TRANG_THIET_BI
{
    public class DauTuTrangThietBiHistoryRepo : GenericRepository<T_BP_DAU_TU_TRANG_THIET_BI_HISTORY>, IDauTuTrangThietBiHistoryRepo
    {
        public DauTuTrangThietBiHistoryRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {

        }
    }
}
