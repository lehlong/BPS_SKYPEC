using SMO.Core.Entities.BP.DAU_TU_TRANG_THIET_BI;
using SMO.Core.Entities.BP.DAU_TU_XAY_DUNG;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP.DAU_TU_TRANG_THIET_BI;
using SMO.Repository.Interface.BP.DAU_TU_XAY_DUNG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Implement.BP.DAU_TU_TRANG_THIET_BI
{
    public class DauTuTrangThietBiEditHistoryRepo : GenericRepository<T_BP_DAU_TU_TRANG_THIET_BI_EDIT_HISTORY>, IDauTuTrangThietBiEditHistoryRepo
    {
        public DauTuTrangThietBiEditHistoryRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {

        }
    }
}
