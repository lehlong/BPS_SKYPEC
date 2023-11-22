using SMO.Core.Entities.MD;
using SMO.Repository.Common;
using SMO.Repository.Interface.MD;

namespace SMO.Repository.Implement.MD
{
    public class TemplateDetailKeHoachChiPhiRepo : GenericTemplateDetailRepository<T_MD_TEMPLATE_DETAIL_KE_HOACH_CHI_PHI, T_MD_KHOAN_MUC_HANG_HOA, T_MD_CHI_PHI_PROFIT_CENTER>, ITemplateDetailKeHoachChiPhiRepo
    {
        public TemplateDetailKeHoachChiPhiRepo(NHUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
