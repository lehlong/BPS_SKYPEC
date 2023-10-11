using SMO.Service.BU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace SMO.Areas.BU.Controllers
{
    public class PaymentController : Controller
    {
        // GET: BU/Payment
        private PaymentService _service;
        public PaymentController()
        {
            _service = new PaymentService();
        }
        public ActionResult Index(string nameContract,int version)
        {
            _service.ObjDetail.NAME_CONTRACT = nameContract;
            _service.ObjDetail.VERSION = version;
            _service.getContract();
            return View(_service);
        }
        [ValidateAntiForgeryToken]
        public ActionResult List(PaymentService service)
        {
            service.Search();
            return PartialView(service);
        }
        [MyValidateAntiForgeryToken]
        public ActionResult Create(string nameContract , int version)
        {
            _service.ObjDetail.VERSION = version;
            _service.ObjDetail.NAME_CONTRACT = nameContract;
            return PartialView(_service);
        }
        [ValidateAntiForgeryToken]
        public ActionResult CreatePayment(PaymentService service)
        {
            var result = new TransferObject();
            result.Type = TransferType.AlertSuccessAndJsCommand;
            service.Create(Request);
            if (_service.State)
            {
                SMOUtilities.GetMessage("1001", service, result);
                result.ExtData = $"SubmitIndex();Forms.Close('{service.ViewId}');Forms.LoadAjax({{url:'{Url.Action("List")}'}});";
            }
            else
            {
                result.Type = TransferType.AlertDanger;
                SMOUtilities.GetMessage("1004", service, result);
            }
            return result.ToJsonResult();
        }
        [MyValidateAntiForgeryToken]
        public ActionResult Update(string id)
        {
            _service.Get(id);
            return PartialView(_service);
        }
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePayment(PaymentService service)
        {
            var result = new TransferObject();
            result.Type = TransferType.AlertSuccessAndJsCommand;
            service.Update(Request);
            if (_service.State)
            {
                SMOUtilities.GetMessage("7005", service, result);
                result.ExtData = $"SubmitIndex();Forms.Close('{service.ViewId}');Forms.LoadAjax({{url:'{Url.Action("List")}'}});";
            }
            else
            {
                result.Type = TransferType.AlertDanger;
                SMOUtilities.GetMessage("7004", service, result);
            }
            return result.ToJsonResult();
        }
        [HttpPost]
        public ActionResult DeletePayment(PaymentService service)
        {
            var result = new TransferObject();
            result.Type = TransferType.AlertSuccessAndJsCommand;
            service.DeletePayment();
            if (_service.State)
            {
                SMOUtilities.GetMessage("7005", service, result);
                result.ExtData = $"SubmitIndex();Forms.Close('{service.ViewId}');Forms.LoadAjax({{url:'{Url.Action("List")}'}});";
            }
            else
            {
                result.Type = TransferType.AlertDanger;
                SMOUtilities.GetMessage("7004", service, result);
            }
            return result.ToJsonResult();
        }
    }
}