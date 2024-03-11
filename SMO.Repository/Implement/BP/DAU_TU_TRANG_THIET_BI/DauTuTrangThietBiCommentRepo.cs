using SMO.Core.Entities.BP.DAU_TU_TRANG_THIET_BI;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP.DAU_TU_TRANG_THIET_BI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Implement.BP.DAU_TU_TRANG_THIET_BI
{
    public class DauTuTrangThietBiCommentRepo : GenericRepository<T_BP_DAU_TU_TRANG_THIET_BI_COMMENT>, IDauTuTrangThietBiCommentRepo
    {
        public DauTuTrangThietBiCommentRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {

        }
    }
}
