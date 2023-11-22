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
    public class TemplateDetailSuaChuaThuongXuyenRepo : GenericTemplateDetailRepository<T_MD_TEMPLATE_DETAIL_SUA_CHUA_THUONG_XUYEN, T_MD_KHOAN_MUC_SUA_CHUA, T_MD_SUA_CHUA_THUONG_XUYEN_PROFIT_CENTER>, ITemplateDetailSuaChuaThuongXuyenRepo
    {
        public TemplateDetailSuaChuaThuongXuyenRepo(NHUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
