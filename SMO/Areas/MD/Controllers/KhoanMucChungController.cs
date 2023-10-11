using SMO.Service.MD;

using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SMO.Areas.MD.Controllers
{
    public class KhoanMucChungController : Controller
    {
        private readonly KhoanMucChungService _service;

        public KhoanMucChungController()
        {
            _service = new KhoanMucChungService();
        }

        [AuthorizeCustom(Right = "R204")]
        [MyValidateAntiForgeryToken]
        public ActionResult Index()
        {
            return PartialView(_service);
        }

        [AuthorizeCustom(Right = "R302")]
        [MyValidateAntiForgeryToken]
        public ActionResult BuildTree(string KhoanMucChungSelected)
        {
            var lstNode = _service.BuildTree();
            JavaScriptSerializer oSerializer = new JavaScriptSerializer
            {
                MaxJsonLength = int.MaxValue
            };
            ViewBag.zNode = oSerializer.Serialize(lstNode);
            ViewBag.KhoanMucChungSelected = KhoanMucChungSelected;
            return PartialView();
        }

        //[AuthorizeCustom(Right = "R302")]
        //[MyValidateAntiForgeryToken]
        //public JsonResult BuildTreeByTemplate(string templateId, int year)
        //{
        //    var lstKhoanMucChung = _service.GetNodeKhoanMucChungByTemplate(templateId, year);
        //    return Json(lstKhoanMucChung, JsonRequestBehavior.AllowGet);
        //}

        [AuthorizeCustom(Right = "R204")]
        [MyValidateAntiForgeryToken]
        public ActionResult Create(string parent)
        {
            _service.ObjDetail.PARENT_CODE = parent;
            return PartialView(_service);
        }

        [AuthorizeCustom(Right = "R204")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(KhoanMucChungService service)
        {
            var result = new TransferObject
            {
                Type = TransferType.AlertSuccessAndJsCommand
            };
            service.Create();
            if (service.State)
            {
                SMOUtilities.GetMessage("1001", service, result);
                result.ExtData = string.Format("BuildTree('{0}', true);", service.ObjDetail.CODE);
            }
            else
            {
                result.Type = TransferType.AlertDanger;
                SMOUtilities.GetMessage("1004", service, result);
            }
            return result.ToJsonResult();
        }

        [AuthorizeCustom(Right = "R204")]
        [MyValidateAntiForgeryToken]
        public ActionResult Edit(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                _service.Get(id);
            }
            return PartialView(_service);
        }

        [AuthorizeCustom(Right = "R204")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(KhoanMucChungService service)
        {
            var result = new TransferObject
            {
                Type = TransferType.AlertSuccessAndJsCommand
            };
            service.Update();
            if (service.State)
            {
                SMOUtilities.GetMessage("1002", service, result);
                result.ExtData = string.Format("BuildTree('{0}', true);", service.ObjDetail.CODE);
            }
            else
            {
                result.Type = TransferType.AlertDanger;
                SMOUtilities.GetMessage("1005", service, result);
            }
            return result.ToJsonResult();
        }


        [AuthorizeCustom(Right = "R204")]
        [HttpPost]
        [MyValidateAntiForgeryToken]
        public ActionResult Delete(string code)
        {
            var result = new TransferObject
            {
                Type = TransferType.AlertSuccessAndJsCommand,
                State = true
            };
            _service.Delete(code);
            if (_service.State)
            {
                SMOUtilities.GetMessage("1003", _service, result);
                result.ExtData = string.Format("BuildTree('', true);");
            }
            else
            {
                result.Type = TransferType.AlertDanger;
                SMOUtilities.GetMessage("1006", _service, result);
            }
            return result.ToJsonResult();
        }

        [AuthorizeCustom(Right = "R204")]
        [HttpPost]
        [MyValidateAntiForgeryToken]
        public ActionResult UpdateTree(List<NodeKhoanMucChung> lstNode)
        {
            var result = new TransferObject
            {
                State = true,
                Type = TransferType.AlertSuccessAndJsCommand
            };
            _service.UpdateTree(lstNode);
            if (_service.State)
            {
                SMOUtilities.GetMessage("1002", _service, result);
                result.ExtData = "";
            }
            else
            {
                result.Type = TransferType.AlertDanger;
                SMOUtilities.GetMessage("1005", _service, result);
            }
            return result.ToJsonResult();
        }

    }
}
