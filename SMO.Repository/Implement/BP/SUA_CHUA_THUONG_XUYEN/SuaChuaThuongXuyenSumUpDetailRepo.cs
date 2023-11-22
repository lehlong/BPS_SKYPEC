using SMO.Core.Entities.BP.SUA_CHUA_THUONG_XUYEN;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP.SUA_CHUA_THUONG_XUYEN;
using System;
using System.Collections.Generic;

namespace SMO.Repository.Implement.BP.SUA_CHUA_THUONG_XUYEN
{
    public class SuaChuaThuongXuyenSumUpDetailRepo : GenericRepository<T_BP_SUA_CHUA_THUONG_XUYEN_SUM_UP_DETAIL>, ISuaChuaThuongXuyenSumUpDetailRepo
    {
        public SuaChuaThuongXuyenSumUpDetailRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {
        }

        public override IList<T_BP_SUA_CHUA_THUONG_XUYEN_SUM_UP_DETAIL> Search(T_BP_SUA_CHUA_THUONG_XUYEN_SUM_UP_DETAIL objFilter, int pageSize, int pageIndex, out int total)
        {
            throw new NotImplementedException();
        }
    }
}
