using SMO.Core.Entities.BP.DAU_TU_NGOAI_DOANH_NGHIEP.DAU_TU_NGOAI_DOANH_NGHIEP_DATA_BASE;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP.DAU_TU_NGOAI_DOANH_NGHIEP.DAU_TU_NGOAI_DOANH_NGHIEP_DATA_BASE;

namespace SMO.Repository.Implement.BP.DAU_TU_NGOAI_DOANH_NGHIEP.DAU_TU_NGOAI_DOANH_NGHIEP_DATA_BASE
{
    public class DauTuNgoaiDoanhNghiepDataBaseRepo : GenericRepository<T_BP_DAU_TU_NGOAI_DOANH_NGHIEP_DATA_BASE>, IDauTuNgoaiDoanhNghiepDataBaseRepo
    {
        public DauTuNgoaiDoanhNghiepDataBaseRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {
        }
    }
}
