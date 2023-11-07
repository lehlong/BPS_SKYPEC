using SMO.Core.Entities.BP.KE_HOACH_CHI_PHI;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP.KE_HOACH_CHI_PHI;
using System;
using System.Collections.Generic;

namespace SMO.Repository.Implement.BP.KE_HOACH_CHI_PHI
{
    public class KeHoachChiPhiSumUpDetailRepo : GenericRepository<T_BP_KE_HOACH_CHI_PHI_SUM_UP_DETAIL>, IKeHoachChiPhiSumUpDetailRepo
    {
        public KeHoachChiPhiSumUpDetailRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {
        }

        public override IList<T_BP_KE_HOACH_CHI_PHI_SUM_UP_DETAIL> Search(T_BP_KE_HOACH_CHI_PHI_SUM_UP_DETAIL objFilter, int pageSize, int pageIndex, out int total)
        {
            throw new NotImplementedException();
        }
    }
}
