using SMO.Core.Entities.CM;
using SMO.Repository.Common;
using SMO.Repository.Interface.CM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Implement.CM
{
    public class CommentDetailsRepo : GenericRepository<T_CM_BP_COMMENT_DETAILs>, ICommentDetailsRepo
    {
        public CommentDetailsRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {

        }
    }
}
