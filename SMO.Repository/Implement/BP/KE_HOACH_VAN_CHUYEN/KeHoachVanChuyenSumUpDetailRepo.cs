using SMO.Core.Entities.BP.KE_HOACH_VAN_CHUYEN;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP.KE_HOACH_VAN_CHUYEN;
using System;
using System.Collections.Generic;

namespace SMO.Repository.Implement.BP.KE_HOACH_VAN_CHUYEN
{
    public class KeHoachVanChuyenSumUpDetailRepo : GenericRepository<T_BP_KE_HOACH_VAN_CHUYEN_SUM_UP_DETAIL>, IKeHoachVanChuyenSumUpDetailRepo
    {
        public KeHoachVanChuyenSumUpDetailRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {
        }

        public override IList<T_BP_KE_HOACH_VAN_CHUYEN_SUM_UP_DETAIL> Search(T_BP_KE_HOACH_VAN_CHUYEN_SUM_UP_DETAIL objFilter, int pageSize, int pageIndex, out int total)
        {
            throw new NotImplementedException();
        }
    }
}
