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
    public class TemplateDetailDauTuNgoaiDoanhNghiepRepo : GenericTemplateDetailRepository<T_MD_TEMPLATE_DETAIL_DAU_TU_NGOAI_DOANH_NGHIEP, T_MD_KHOAN_MUC_DAU_TU, T_MD_DAU_TU_NGOAI_DOANH_NGHIEP_PROFIT_CENTER>, ITemplateDetailDauTuNgoaiDoanhNghiepRepo
    {
        public TemplateDetailDauTuNgoaiDoanhNghiepRepo(NHUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
