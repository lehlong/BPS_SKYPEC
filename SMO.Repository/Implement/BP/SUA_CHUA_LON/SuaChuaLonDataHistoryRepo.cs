using SMO.Core.Entities;
using SMO.Core.Entities.BP.SUA_CHUA_LON;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP;
using SMO.Repository.Interface.BP.SUA_CHUA_LON;

namespace SMO.Repository.Implement.BP.SUA_CHUA_LON
{
    public class SuaChuaLonDataHistoryRepo : GenericRepository<T_BP_SUA_CHUA_LON_DATA_HISTORY>, ISuaChuaLonDataHistoryRepo
    {
        public SuaChuaLonDataHistoryRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {

        }
    }
}
