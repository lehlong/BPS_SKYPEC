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
    public class TemplateDetailKeHoachDoanhThuRepo : GenericTemplateDetailRepository<T_MD_TEMPLATE_DETAIL_KE_HOACH_DOANH_THU, T_MD_KHOAN_MUC_DOANH_THU, T_MD_DOANH_THU_PROFIT_CENTER>, ITemplateDetailKeHoachDoanhThuRepo
    {
        public TemplateDetailKeHoachDoanhThuRepo(NHUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
