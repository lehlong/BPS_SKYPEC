﻿using SMO.Core.Entities.MD;
using SMO.Repository.Common;
using SMO.Repository.Interface.MD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Implement.MD
{
    public class KhoanMucDauTuNDNRepo : GenericElementRepository<T_MD_KHOAN_MUC_DAU_TU_NDN>, IKhoanMucDauTuNDNRepo
    {
        public KhoanMucDauTuNDNRepo(NHUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override IList<T_MD_KHOAN_MUC_DAU_TU_NDN> Search(T_MD_KHOAN_MUC_DAU_TU_NDN objFilter, int pageSize, int pageIndex, out int total)
        {
            var query = Queryable();
            query = query.Where(x => x.TIME_YEAR == objFilter.TIME_YEAR);
            query = query.OrderBy(x => x.C_ORDER);
            total = 0;
            return query.ToList();
        }
    }
}
