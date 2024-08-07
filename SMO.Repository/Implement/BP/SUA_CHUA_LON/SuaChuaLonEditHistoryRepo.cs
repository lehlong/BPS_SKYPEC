﻿using SMO.Core.Entities;
using SMO.Core.Entities.BP.SUA_CHUA_LON;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP.SUA_CHUA_LON;
using SMO.Repository.Interface.MD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Implement.BP.SUA_CHUA_LON
{
    public class SuaChuaLonEditHistoryRepo : GenericRepository<T_BP_SUA_CHUA_LON_EDIT_HISTORY>, ISuaChuaLonEditHistoryRepo
    {
        public SuaChuaLonEditHistoryRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {

        }
    }
}
