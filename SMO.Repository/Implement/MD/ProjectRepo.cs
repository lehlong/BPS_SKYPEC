using SMO.Core.Entities;
using SMO.Repository.Common;
using SMO.Repository.Interface.MD;

using System.Collections.Generic;
using System.Linq;

namespace SMO.Repository.Implement.MD
{
    public class ProjectRepo : GenericRepository<T_MD_PROJECT>, IProjectRepo
    {
        public ProjectRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {

        }

        public override IList<T_MD_PROJECT> Search(T_MD_PROJECT objFilter, int pageSize, int pageIndex, out int total)
        {
            var query = Queryable();

            if (!string.IsNullOrWhiteSpace(objFilter.CODE))
            {
                query = query.Where(x => x.CODE.ToLower().Contains(objFilter.CODE.ToLower()) || x.NAME.ToLower().Contains(objFilter.CODE.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(objFilter.YEAR.ToString()))
            {
                query = query.Where(x => x.YEAR == objFilter.YEAR);
            }
            if (!string.IsNullOrWhiteSpace(objFilter.LOAI_HINH))
            {
                query = query.Where(x => x.LOAI_HINH == objFilter.LOAI_HINH);
            }

            if (!string.IsNullOrWhiteSpace(objFilter.AREA_CODE))
            {
                query = query.Where(x => x.AREA_CODE == objFilter.AREA_CODE);
            }

            total = 0;
            query = query.OrderByDescending(x => x.CODE);
            return query.ToList();
        }
    }
}
