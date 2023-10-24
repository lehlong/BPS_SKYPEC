using SMO.Core.Entities.BP.DAU_TU_XAY_DUNG.DAU_TU_XAY_DUNG_DATA_BASE;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP.DAU_TU_XAY_DUNG.DAU_TU_XAY_DUNG_DATA_BASE;

namespace SMO.Repository.Implement.BP.DAU_TU_XAY_DUNG.DAU_TU_XAY_DUNG_DATA_BASE
{
    public class DauTuXayDungDataBaseHistoryRepo : GenericRepository<T_BP_DAU_TU_XAY_DUNG_DATA_BASE_HISTORY>, IDauTuXayDungDataBaseHistoryRepo
    {
        public DauTuXayDungDataBaseHistoryRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {
        }
    }
}
