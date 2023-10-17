using SMO.Core.Entities.BP.KE_HOACH_DOANH_THU;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP.KE_HOACH_DOANH_THU;
using System;
using System.Collections.Generic;

namespace SMO.Repository.Implement.BP.KE_HOACH_DOANH_THU
{
    public class KeHoachDoanhThuSumUpDetailRepo : GenericRepository<T_BP_KE_HOACH_DOANH_THU_SUM_UP_DETAIL>, IKeHoachDoanhThuSumUpDetailRepo
    {
        public KeHoachDoanhThuSumUpDetailRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {
        }

        public override IList<T_BP_KE_HOACH_DOANH_THU_SUM_UP_DETAIL> Search(T_BP_KE_HOACH_DOANH_THU_SUM_UP_DETAIL objFilter, int pageSize, int pageIndex, out int total)
        {
            throw new NotImplementedException();
        }
    }
}
