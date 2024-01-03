﻿using SMO.Core.Entities;
using SMO.Core.Entities.BP.KE_HOACH_CHI_PHI;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP.KE_HOACH_CHI_PHI;
using SMO.Repository.Interface.MD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Implement.BP.KE_HOACH_CHI_PHI
{
    public class KeHoachChiPhiCommentRepo : GenericRepository<T_BP_KE_HOACH_CHI_PHI_COMMENT>, IKeHoachChiPhiCommentRepo
    {
        public KeHoachChiPhiCommentRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {

        }
    }
}
