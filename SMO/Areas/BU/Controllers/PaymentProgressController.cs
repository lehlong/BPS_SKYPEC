using SMO.Service.BU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMO.Areas.BU.Controllers
{
    public class PaymentProgressController : Controller
    {
        // GET: BU/PaymentProgress
        private PaymentProgressService _service;
        public PaymentProgressController()
        {
            _service = new PaymentProgressService();
        }
        public ActionResult Index(string nameContract, int version)
        {
            _service.ObjDetail.NAME_CONTRACT = nameContract;
            _service.ObjDetail.VERSION = version;
            _service.getContract();
            return View(_service);
        }

        [ValidateAntiForgeryToken]
        public ActionResult List(PaymentProgressService service)
        {
            service.Search();
            return PartialView(service);
        }
        [ValidateAntiForgeryToken]
        public ActionResult Create(PaymentProgressService service)
        {
            service.Search();
            var result = new TransferObject();
            result.Type = TransferType.AlertSuccessAndJsCommand;
            service.getContract();
            service.Create(Request);
            if (service.State)
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
        [HttpPost]
        public ActionResult DeletePharse(PaymentProgressService service)
        {
            var result = new TransferObject();
            result.Type = TransferType.AlertSuccessAndJsCommand;
            service.Delete(Request);
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