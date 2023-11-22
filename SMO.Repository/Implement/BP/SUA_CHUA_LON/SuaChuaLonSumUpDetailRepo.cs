using SMO.Core.Entities.BP.SUA_CHUA_LON;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP.SUA_CHUA_LON;
using System;
using System.Collections.Generic;

namespace SMO.Repository.Implement.BP.SUA_CHUA_LON
{
    public class SuaChuaLonSumUpDetailRepo : GenericRepository<T_BP_SUA_CHUA_LON_SUM_UP_DETAIL>, ISuaChuaLonSumUpDetailRepo
    {
        public SuaChuaLonSumUpDetailRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {
        }

        public override IList<T_BP_SUA_CHUA_LON_SUM_UP_DETAIL> Search(T_BP_SUA_CHUA_LON_SUM_UP_DETAIL objFilter, int pageSize, int pageIndex, out int total)
        {
            throw new NotImplementedException();
        }
    }
}
