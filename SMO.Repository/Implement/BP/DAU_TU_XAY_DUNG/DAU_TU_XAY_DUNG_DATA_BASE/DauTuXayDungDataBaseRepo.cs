using SMO.Core.Entities.BP.DAU_TU_XAY_DUNG.DAU_TU_XAY_DUNG_DATA_BASE;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP.DAU_TU_XAY_DUNG.DAU_TU_XAY_DUNG_DATA_BASE;

namespace SMO.Repository.Implement.BP.DAU_TU_XAY_DUNG.DAU_TU_XAY_DUNG_DATA_BASE
{
    public class DauTuXayDungDataBaseRepo : GenericRepository<T_BP_DAU_TU_XAY_DUNG_DATA_BASE>, IDauTuXayDungDataBaseRepo
    {
        public DauTuXayDungDataBaseRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {
        }
    }
}
