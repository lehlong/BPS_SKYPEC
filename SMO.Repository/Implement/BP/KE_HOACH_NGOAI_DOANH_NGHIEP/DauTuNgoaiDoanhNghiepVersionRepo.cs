using SMO.Core.Entities;
using SMO.Core.Entities.BP.DAU_TU_NGOAI_DOANH_NGHIEP;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP;
using SMO.Repository.Interface.BP.DAU_TU_NGOAI_DOANH_NGHIEP;

namespace SMO.Repository.Implement.BP.DAU_TU_NGOAI_DOANH_NGHIEP
{
    public class DauTuNgoaiDoanhNghiepVersionRepo : GenericRepository<T_BP_DAU_TU_NGOAI_DOANH_NGHIEP_VERSION>, IDauTuNgoaiDoanhNghiepVersionRepo
    {
        public DauTuNgoaiDoanhNghiepVersionRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {

        }
    }
}
