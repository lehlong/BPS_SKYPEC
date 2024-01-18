using SMO.Core.Entities.CM;
using SMO.Repository.Implement.CM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMO.Service.CM
{
    public class AssignDepartmentService : GenericService<T_CM_ASSIGN_DEPARTMENT, AssignDepartmentRepo>
    {
        public AssignDepartmentService() : base() { }
        public void Create(string code, string content, string orgCode,
                    string referenceCode,
                    int year,
                    int version)
        {
                var isItem = this.CurrentRepository.Queryable().Any(x => x.DEPARTMENT == content && x.ELEMENT_CODE == code);
                if (isItem)
                {
                ErrorMessage = "Phòng ban đã được gán vào khoản mục này!";
                State = false;
                return;
            }
                ObjDetail.PKID = Guid.NewGuid().ToString();
                ObjDetail.CREATE_BY = ProfileUtilities.User.USER_NAME;
                ObjDetail.ELEMENT_CODE = code;
                ObjDetail.DEPARTMENT = content;
                ObjDetail.ORG_CODE = orgCode;
                ObjDetail.REFERENCE_CODE = referenceCode;
                ObjDetail.YEAR = year;
                ObjDetail.VERSION = version;
                base.Create();
            
        }

        public IList<T_CM_ASSIGN_DEPARTMENT> GetAll(string code)
        {
            var lstDepartment = UnitOfWork.Repository<AssignDepartmentRepo>().Queryable().Where(x => x.ELEMENT_CODE == code).ToList();
            return lstDepartment;
        }

        public IList<T_CM_ASSIGN_DEPARTMENT> GetAllAssignDepartment()
        {
            var lstDepartment = UnitOfWork.Repository<AssignDepartmentRepo>().Queryable().ToList();
            return lstDepartment;
        }
    }
}