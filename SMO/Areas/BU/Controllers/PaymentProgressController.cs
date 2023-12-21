using SMO.Repository.Implement.BU;
using SMO.Service.BU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

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
        [MyValidateAntiForgeryToken]
        public ActionResult Create(string nameContract, int version)
        {
            _service.ObjDetail.VERSION = version;
            _service.ObjDetail.NAME_CONTRACT = nameContract;
            _service.contract = _service.UnitOfWork.Repository<ContractRepo>().Queryable().FirstOrDefault(x => x.NAME_CONTRACT == nameContract);
            return PartialView(_service);
        }
        [ValidateAntiForgeryToken]
        public ActionResult CreatePaymentProgress(PaymentProgressService service)
        {
            var result = new TransferObject();
            result.Type = TransferType.AlertSuccessAndJsCommand;
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
        [MyValidateAntiForgeryToken]
        public ActionResult Update(string id)
        {
            _service.Get(id);
            _service.contract = _service.UnitOfWork.Repository<ContractRepo>().Queryable().FirstOrDefault(x => x.NAME_CONTRACT == _service.ObjDetail.NAME_CONTRACT);
            return PartialView(_service);
        }
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePaymentProgress(PaymentProgressService service)
        {
            var result = new TransferObject();
            result.Type = TransferType.AlertSuccessAndJsCommand;
            service.Update(Request);
            if (service.State)
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
        public ActionResult DeletePaymentProgress(PaymentProgressService service)
        {
            var result = new TransferObject();
            result.Type = TransferType.AlertSuccessAndJsCommand;
            service.DeletePaymentProgress();
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