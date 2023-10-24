using SMO.Core.Entities;
using SMO.Core.Entities.BP.DAU_TU_XAY_DUNG;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP;
using SMO.Repository.Interface.BP.DAU_TU_XAY_DUNG;

namespace SMO.Repository.Implement.BP.DAU_TU_XAY_DUNG
{
    public class DauTuXayDungDataHistoryRepo : GenericRepository<T_BP_DAU_TU_XAY_DUNG_DATA_HISTORY>, IDauTuXayDungDataHistoryRepo
    {
        public DauTuXayDungDataHistoryRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {

        }
    }
}
