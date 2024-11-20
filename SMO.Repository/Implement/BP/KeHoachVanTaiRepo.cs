using SMO.Core.Entities;
using SMO.Core.Entities.BP;
using SMO.Core.Entities.MD;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP;
using SMO.Repository.Interface.MD;

using System.Collections.Generic;
using System.Linq;

namespace SMO.Repository.Implement.BP
{
    public class KeHoachVanTaiRepo : GenericRepository<T_BP_KE_HOACH_VAN_TAI>, IKeHoachVanTaiRepo
    {
        public KeHoachVanTaiRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {

        }
    }
}
