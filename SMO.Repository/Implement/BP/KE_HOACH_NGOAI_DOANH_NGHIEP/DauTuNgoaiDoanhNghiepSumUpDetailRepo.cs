using SMO.Core.Entities.BP.DAU_TU_NGOAI_DOANH_NGHIEP;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP.DAU_TU_NGOAI_DOANH_NGHIEP;
using System;
using System.Collections.Generic;

namespace SMO.Repository.Implement.BP.DAU_TU_NGOAI_DOANH_NGHIEP
{
    public class DauTuNgoaiDoanhNghiepSumUpDetailRepo : GenericRepository<T_BP_DAU_TU_NGOAI_DOANH_NGHIEP_SUM_UP_DETAIL>, IDauTuNgoaiDoanhNghiepSumUpDetailRepo
    {
        public DauTuNgoaiDoanhNghiepSumUpDetailRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {
        }

        public override IList<T_BP_DAU_TU_NGOAI_DOANH_NGHIEP_SUM_UP_DETAIL> Search(T_BP_DAU_TU_NGOAI_DOANH_NGHIEP_SUM_UP_DETAIL objFilter, int pageSize, int pageIndex, out int total)
        {
            throw new NotImplementedException();
        }
    }
}
