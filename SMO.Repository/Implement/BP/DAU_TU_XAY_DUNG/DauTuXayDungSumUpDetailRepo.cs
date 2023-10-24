using SMO.Core.Entities.BP.DAU_TU_XAY_DUNG;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP.DAU_TU_XAY_DUNG;
using System;
using System.Collections.Generic;

namespace SMO.Repository.Implement.BP.DAU_TU_XAY_DUNG
{
    public class DauTuXayDungSumUpDetailRepo : GenericRepository<T_BP_DAU_TU_XAY_DUNG_SUM_UP_DETAIL>, IDauTuXayDungSumUpDetailRepo
    {
        public DauTuXayDungSumUpDetailRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {
        }

        public override IList<T_BP_DAU_TU_XAY_DUNG_SUM_UP_DETAIL> Search(T_BP_DAU_TU_XAY_DUNG_SUM_UP_DETAIL objFilter, int pageSize, int pageIndex, out int total)
        {
            throw new NotImplementedException();
        }
    }
}
