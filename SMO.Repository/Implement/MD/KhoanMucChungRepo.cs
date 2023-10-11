using SMO.Core.Entities.MD;
using SMO.Repository.Common;
using SMO.Repository.Interface.MD;

namespace SMO.Repository.Implement.MD
{
    public class KhoanMucChungRepo : GenericCenterRepository<T_MD_KHOAN_MUC_CHUNG>, IKhoanMucChungRepo
    {
        public KhoanMucChungRepo(NHUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
