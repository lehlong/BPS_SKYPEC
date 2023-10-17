using SMO.Core.Entities;
using SMO.Core.Entities.BP.KE_HOACH_DOANH_THU;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP;
using SMO.Repository.Interface.BP.KE_HOACH_DOANH_THU;
using System.Collections.Generic;

namespace SMO.Repository.Implement.BP.KE_HOACH_DOANH_THU
{
    public class KeHoachDoanhThuRepo : GenericBPRepository<T_BP_KE_HOACH_DOANH_THU>, IKeHoachDoanhThuRepo
    {
        public KeHoachDoanhThuRepo(NHUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public override IList<T_BP_KE_HOACH_DOANH_THU> Search(T_BP_KE_HOACH_DOANH_THU objFilter, int pageSize, int pageIndex, out int total)
        {
            var query = NHibernateSession.QueryOver<T_BP_KE_HOACH_DOANH_THU>();

            if (!string.IsNullOrWhiteSpace(objFilter.ORG_CODE))
            {
                query = query.Where(x => x.ORG_CODE == objFilter.ORG_CODE);
            }
            if (string.IsNullOrWhiteSpace(objFilter.PHIEN_BAN))
            {
                objFilter.PHIEN_BAN = "PB1";
            }
            query = query.Where(x => x.PHIEN_BAN == objFilter.PHIEN_BAN);
            if (string.IsNullOrWhiteSpace(objFilter.KICH_BAN))
            {
                objFilter.KICH_BAN = "TB";
            }
            query = query.Where(x => x.KICH_BAN == objFilter.KICH_BAN);
            if (objFilter.TIME_YEAR != 0)
            {
                query = query.Where(x => x.TIME_YEAR == objFilter.TIME_YEAR);
            }
            
            query = query.Fetch(x => x.Template).Eager
                .Fetch(x => x.Organize).Eager;
            query = query.OrderBy(x => x.CREATE_DATE).Desc;
            return base.Paging(query, pageSize, pageIndex, out total);
        }

    }
}
