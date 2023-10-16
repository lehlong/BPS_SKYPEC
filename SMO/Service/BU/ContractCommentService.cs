using SMO.Core.Entities.BU;
using SMO.Repository.Implement.BU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMO.Service.BU
{
    public class ContractCommentService : GenericService<T_BU_CONTRACT_COMMENT, ContractConmentRepo>
    {
        public ContractCommentService() : base()
        {

        }
        public void CreateComment(string id, string comment, int version)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    this.State = false;
                    return;
                }
                UnitOfWork.BeginTransaction();
                var commentAdd = new T_BU_CONTRACT_COMMENT()
                {
                    ID = Guid.NewGuid().ToString(),
                    COMMENT = comment,
                    VERSION = version,
                    CONTRACT_NAME = id,
                    CREATE_BY = ProfileUtilities.User.USER_NAME,
                };
                this.CurrentRepository.Create(commentAdd);
                UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                this.State = false;
                this.Exception = ex;
            }

        }

        public void GetListComment(string contracName, int version)
        {
            this.ObjList = this.CurrentRepository.Queryable().Where(x => x.CONTRACT_NAME == contracName && x.VERSION == version).ToList();
        }
    }
}