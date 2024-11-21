using SMO.Core.Entities.MD;
using SMO.Repository.Common;
using SMO.Repository.Interface.MD;
namespace SMO.Repository.Implement.MD
{
    public class InputChiPhiRepo : GenericRepository<T_MD_INPUT_CHI_PHI>, IInputChiPhiRepo
    {
        public InputChiPhiRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {
        }
    }
}
