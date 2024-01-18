using SMO.Core.Entities.CM;
using SMO.Repository.Implement.CM;
using SMO.Repository.Implement.MD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMO.Service.CM
{
    public class CommentDetailsService : GenericService<T_CM_BP_COMMENT_DETAILs, CommentDetailsRepo>
    {
        public CommentDetailsService() : base() { }
        public void Create(string code, string content, string orgCode,
                    string referenceCode,
                    int year,
                    int version)
        {
            ObjDetail.PKID = Guid.NewGuid().ToString();
            ObjDetail.CREATE_BY = ProfileUtilities.User.USER_NAME;
            ObjDetail.ELEMENT_CODE = code;
            ObjDetail.CONTENT = content;
            ObjDetail.ORG_CODE = orgCode;
            ObjDetail.REFERENCE_CODE = referenceCode;
            ObjDetail.YEAR = year;
            ObjDetail.VERSION = version;
            base.Create();
        }

        public IList<T_CM_BP_COMMENT_DETAILs> GetAll(string code)
        {
            var lstComment = UnitOfWork.Repository<CommentDetailsRepo>().Queryable().Where(x => x.ELEMENT_CODE == code).ToList();
            return lstComment;
        }

        public IList<T_CM_BP_COMMENT_DETAILs> GetAllComment()
        {
            var lstComment = UnitOfWork.Repository<CommentDetailsRepo>().Queryable().ToList();
            return lstComment;
        }
    }
}