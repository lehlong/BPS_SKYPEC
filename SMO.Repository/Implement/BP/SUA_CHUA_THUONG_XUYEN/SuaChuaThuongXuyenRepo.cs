﻿using SMO.Core.Entities;
using SMO.Core.Entities.BP.SUA_CHUA_THUONG_XUYEN;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP;
using SMO.Repository.Interface.BP.SUA_CHUA_THUONG_XUYEN;
using System.Collections.Generic;

namespace SMO.Repository.Implement.BP.SUA_CHUA_THUONG_XUYEN
{
    public class SuaChuaThuongXuyenRepo : GenericBPRepository<T_BP_SUA_CHUA_THUONG_XUYEN>, ISuaChuaThuongXuyenRepo
    {
        public SuaChuaThuongXuyenRepo(NHUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public override IList<T_BP_SUA_CHUA_THUONG_XUYEN> Search(T_BP_SUA_CHUA_THUONG_XUYEN objFilter, int pageSize, int pageIndex, out int total)
        {
            var query = NHibernateSession.QueryOver<T_BP_SUA_CHUA_THUONG_XUYEN>();

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
