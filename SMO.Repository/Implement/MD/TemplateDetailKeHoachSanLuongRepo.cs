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
    public class TemplateDetailKeHoachSanLuongRepo : GenericTemplateDetailRepository<T_MD_TEMPLATE_DETAIL_KE_HOACH_SAN_LUONG, T_MD_KHOAN_MUC_SAN_LUONG, T_MD_SAN_LUONG_PROFIT_CENTER>, ITemplateDetailKeHoachSanLuongRepo
    {
        public TemplateDetailKeHoachSanLuongRepo(NHUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
