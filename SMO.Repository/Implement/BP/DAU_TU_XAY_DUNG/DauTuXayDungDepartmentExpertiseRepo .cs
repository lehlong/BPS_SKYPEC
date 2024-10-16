﻿using SMO.Core.Entities.BP.DAU_TU_XAY_DUNG;
using SMO.Core.Entities.BP.DAU_TU_XAY_DUNG;
using SMO.Repository.Common;

using SMO.Repository.Interface.BP.DAU_TU_XAY_DUNG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Implement.BP.DAU_TU_XAY_DUNG
{
    public class DauTuXayDungDepartmentExpertiseRepo : GenericRepository<T_BP_DAU_TU_XAY_DUNG_DEPARTMENT_EXPERTISE>, IDauTuXayDungDepartmentExpertiseRepo
    {
        public DauTuXayDungDepartmentExpertiseRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {

        }
    }
}
