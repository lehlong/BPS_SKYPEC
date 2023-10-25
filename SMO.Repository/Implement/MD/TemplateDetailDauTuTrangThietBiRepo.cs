using SMO.Core.Entities.MD;
using SMO.Repository.Common;
using SMO.Repository.Interface.MD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Implement.MD
{
    public class TemplateDetailDauTuTrangThietBiRepo : GenericTemplateDetailRepository<T_MD_TEMPLATE_DETAIL_DAU_TU_TRANG_THIET_BI, T_MD_KHOAN_MUC_DAU_TU, T_MD_DAU_TU_TRANG_THIET_BI_PROFIT_CENTER>, ITemplateDetailDauTuTrangThietBiRepo
    {
        public TemplateDetailDauTuTrangThietBiRepo(NHUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
