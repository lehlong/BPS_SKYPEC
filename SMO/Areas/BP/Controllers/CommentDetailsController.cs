using Microsoft.Ajax.Utilities;
using NPOI.SS.Formula.Functions;
using SMO.Service.CM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMO.Areas.BP.Controllers
{
    public class CommentDetailsController : Controller
    {
        private readonly CommentDetailsService _service;
        public CommentDetailsController()
        {
            _service = new CommentDetailsService();
        }
        // GET: BP/CommentDetails
        [HttpPost]
        public ActionResult Create(string code, string content, string orgCode,
                    string referenceCode,
                    int year,
                    int version)
        {
            _service.Create(code, content, orgCode, referenceCode, year, version);
            var resultData = new
            {
                code = code // Đã loại bỏ dấu chấm phẩy và sử dụng {} thay thế
            };
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult List(string code)
        {
            var lstComment = _service.GetAll(code);
            return Json(lstComment, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult List()
        {
            var lstComment = _service.GetAll();
            return Json(lstComment, JsonRequestBehavior.AllowGet);
        }

        
    }
}