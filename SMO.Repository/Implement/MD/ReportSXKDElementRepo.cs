using SMO.Core.Entities;
using SMO.Repository.Common;
using SMO.Repository.Interface.MD;

using System.Collections.Generic;
using System.Linq;

namespace SMO.Repository.Implement.MD
{
    public class ReportSXKDElementRepo : GenericRepository<T_MD_REPORT_SXKD_ELEMENT>, IReportSXKDElementRepo
    {
        public ReportSXKDElementRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {

        }
    }
}
