using SMO.Core.Entities.BP.DAU_TU_TRANG_THIET_BI;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP.DAU_TU_TRANG_THIET_BI;
using System;
using System.Collections.Generic;

namespace SMO.Repository.Implement.BP.DAU_TU_TRANG_THIET_BI
{
    public class DauTuTrangThietBiSumUpDetailRepo : GenericRepository<T_BP_DAU_TU_TRANG_THIET_BI_SUM_UP_DETAIL>, IDauTuTrangThietBiSumUpDetailRepo
    {
        public DauTuTrangThietBiSumUpDetailRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {
        }

        public override IList<T_BP_DAU_TU_TRANG_THIET_BI_SUM_UP_DETAIL> Search(T_BP_DAU_TU_TRANG_THIET_BI_SUM_UP_DETAIL objFilter, int pageSize, int pageIndex, out int total)
        {
            throw new NotImplementedException();
        }
    }
}
