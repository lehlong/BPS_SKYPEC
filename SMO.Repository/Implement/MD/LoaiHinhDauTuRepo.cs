using SMO.Core.Entities;
using SMO.Repository.Common;
using SMO.Repository.Interface.MD;

using System.Collections.Generic;
using System.Linq;

namespace SMO.Repository.Implement.MD
{
    public class LoaiHinhDauTuRepo : GenericRepository<T_MD_LOAI_HINH_DAU_TU>, ILoaiHinhDauTuRepo
    {
        public LoaiHinhDauTuRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {

        }

        public override IList<T_MD_LOAI_HINH_DAU_TU> Search(T_MD_LOAI_HINH_DAU_TU objFilter, int pageSize, int pageIndex, out int total)
        {
            var query = Queryable();

            if (!string.IsNullOrWhiteSpace(objFilter.CODE))
            {
                query = query.Where(x => x.CODE.ToLower().Contains(objFilter.CODE.ToLower()) || x.TEXT.ToLower().Contains(objFilter.CODE.ToLower()));
            }

            query = query.OrderByDescending(x => x.ACTIVE);
            return base.Paging(query, pageSize, pageIndex, out total).ToList();
        }
    }
}
