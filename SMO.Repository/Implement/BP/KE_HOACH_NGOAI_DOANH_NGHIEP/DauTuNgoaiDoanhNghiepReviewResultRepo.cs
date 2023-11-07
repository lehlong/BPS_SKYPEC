﻿using SMO.Core.Entities.BP.DAU_TU_NGOAI_DOANH_NGHIEP;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP.DAU_TU_NGOAI_DOANH_NGHIEP;

namespace SMO.Repository.Implement.BP.DAU_TU_NGOAI_DOANH_NGHIEP
{
    public class DauTuNgoaiDoanhNghiepReviewResultRepo : GenericRepository<T_BP_DAU_TU_NGOAI_DOANH_NGHIEP_REVIEW_RESULT>, IDauTuNgoaiDoanhNghiepReviewResultRepo
    {
        public DauTuNgoaiDoanhNghiepReviewResultRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {
        }
    }
}
