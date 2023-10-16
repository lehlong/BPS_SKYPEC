using SMO.Core.Entities.BP.KE_HOACH_SAN_LUONG;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP.KE_HOACH_SAN_LUONG;
using System;
using System.Collections.Generic;

namespace SMO.Repository.Implement.BP.KE_HOACH_SAN_LUONG
{
    public class KeHoachSanLuongSumUpDetailRepo : GenericRepository<T_BP_KE_HOACH_SAN_LUONG_SUM_UP_DETAIL>, IKeHoachSanLuongSumUpDetailRepo
    {
        public KeHoachSanLuongSumUpDetailRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {
        }

        public override IList<T_BP_KE_HOACH_SAN_LUONG_SUM_UP_DETAIL> Search(T_BP_KE_HOACH_SAN_LUONG_SUM_UP_DETAIL objFilter, int pageSize, int pageIndex, out int total)
        {
            throw new NotImplementedException();
        }
    }
}
