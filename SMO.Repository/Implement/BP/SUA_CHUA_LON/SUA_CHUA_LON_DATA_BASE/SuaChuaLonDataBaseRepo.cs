using SMO.Core.Entities.BP.SUA_CHUA_LON.SUA_CHUA_LON_DATA_BASE;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP.SUA_CHUA_LON.SUA_CHUA_LON_DATA_BASE;

namespace SMO.Repository.Implement.BP.SUA_CHUA_LON.SUA_CHUA_LON_DATA_BASE
{
    public class SuaChuaLonDataBaseRepo : GenericRepository<T_BP_SUA_CHUA_LON_DATA_BASE>, ISuaChuaLonDataBaseRepo
    {
        public SuaChuaLonDataBaseRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {
        }
    }
}
