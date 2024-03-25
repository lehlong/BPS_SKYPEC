using SMO.Core.Entities;
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
    public class ReportChiPhiCodeRepo : GenericRepository<T_MD_REPORT_CHI_PHI_CODE>, IReportChiPhiCodeRepo
    {
        public ReportChiPhiCodeRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {

        }
        public override IList<T_MD_REPORT_CHI_PHI_CODE> Search(T_MD_REPORT_CHI_PHI_CODE objFilter, int pageSize, int pageIndex, out int total)
        {
            var query = Queryable();
            query = query.Where(x => x.TIME_YEAR == objFilter.TIME_YEAR);
            query = query.OrderByDescending(x => x.ACTIVE);
            return base.Paging(query, pageSize, pageIndex, out total).ToList();
        }
    }
}
