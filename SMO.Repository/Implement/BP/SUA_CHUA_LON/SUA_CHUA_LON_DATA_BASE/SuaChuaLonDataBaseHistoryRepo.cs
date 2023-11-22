using SMO.Core.Entities.BP.SUA_CHUA_LON.SUA_CHUA_LON_DATA_BASE;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP.SUA_CHUA_LON.SUA_CHUA_LON_DATA_BASE;

namespace SMO.Repository.Implement.BP.SUA_CHUA_LON.SUA_CHUA_LON_DATA_BASE
{
    public class SuaChuaLonDataBaseHistoryRepo : GenericRepository<T_BP_SUA_CHUA_LON_DATA_BASE_HISTORY>, ISuaChuaLonDataBaseHistoryRepo
    {
        public SuaChuaLonDataBaseHistoryRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {
        }
    }
}
