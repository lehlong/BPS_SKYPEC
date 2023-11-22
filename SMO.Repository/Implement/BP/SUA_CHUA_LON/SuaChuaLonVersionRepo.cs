using SMO.Core.Entities;
using SMO.Core.Entities.BP.SUA_CHUA_LON;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP;
using SMO.Repository.Interface.BP.SUA_CHUA_LON;

namespace SMO.Repository.Implement.BP.SUA_CHUA_LON
{
    public class SuaChuaLonVersionRepo : GenericRepository<T_BP_SUA_CHUA_LON_VERSION>, ISuaChuaLonVersionRepo
    {
        public SuaChuaLonVersionRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {

        }
    }
}
