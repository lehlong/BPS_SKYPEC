using SMO.Service.CM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using ZXing;

namespace SMO.Areas.BP.Controllers
{
    public class AssignDepartmentController : Controller
    {
        // GET: BP/AssignDepartment
        private readonly AssignDepartmentService _service;
        public AssignDepartmentController()
        {
            _service = new AssignDepartmentService();
        }
        // GET: BP/CommentDetails
        [HttpPost]
        public ActionResult Create(string code, string department, string orgCode,
                    string referenceCode,
                    int year,
                    int version)
        {
            var result = new TransferObject();
            result.Type = TransferType.AlertSuccessAndJsCommand;
            List<string> lstDepartment = new List<string>();
            if (department.Contains(","))
            {
                lstDepartment.AddRange(department.Split(','));
            }
            else
            {
                lstDepartment.Add(department);
            }
            foreach (var item in lstDepartment)
            {
                var service = new AssignDepartmentService();
                service.Create(code, item, orgCode, referenceCode, year, version);
                if (service.State)
                {
                    SMOUtilities.GetMessage("1001", service, result);
                }
                else
                {
                    result.Type = TransferType.AlertDanger;
                    SMOUtilities.GetMessage("1004", service, result);
                    return result.ToJsonResult();
                }
            }
            
            result.Data = new
            {
                code = code // Đã loại bỏ dấu chấm phẩy và sử dụng {} thay thế
            };
            return result.ToJsonResult();
        }

        public ActionResult List(string code)
        {
            var lstDepartment = _service.GetAll(code);
            return Json(lstDepartment, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult List()
        {
            var lstComment = _service.GetAllAssignDepartment();
            return Json(lstComment, JsonRequestBehavior.AllowGet);
        }
    }
}