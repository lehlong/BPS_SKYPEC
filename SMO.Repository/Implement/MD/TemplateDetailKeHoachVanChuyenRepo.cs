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
    public class TemplateDetailKeHoachVanChuyenRepo : GenericTemplateDetailRepository<T_MD_TEMPLATE_DETAIL_KE_HOACH_VAN_CHUYEN, T_MD_KHOAN_MUC_VAN_CHUYEN, T_MD_VAN_CHUYEN_PROFIT_CENTER>, ITemplateDetailKeHoachVanChuyenRepo
    {
        public TemplateDetailKeHoachVanChuyenRepo(NHUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
