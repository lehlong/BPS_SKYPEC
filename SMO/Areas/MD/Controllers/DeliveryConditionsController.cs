using SMO.Service.MD;

using System.Web.Mvc;

namespace SMO.Areas.MD.Controllers
{
    [AuthorizeCustom(Right = "R6.5")]
    public class DeliveryConditionsController : Controller
    {
        private readonly DeliveryConditionsService _service;

        public DeliveryConditionsController()
        {
            _service = new DeliveryConditionsService();
        }

        [MyValidateAntiForgeryToken]
        public ActionResult Index()
        {
            return PartialView(_service);
        }

        [ValidateAntiForgeryToken]
        public ActionResult List(DeliveryConditionsService service)
        {
            service.Search();
            return PartialView(service);
        }

        [MyValidateAntiForgeryToken]
        public ActionResult Create()
        {
            return PartialView(_service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DeliveryConditionsService service)
        {
            var result = new TransferObject
            {
                Type = TransferType.AlertSuccessAndJsCommand
            };
            service.Create();
            if (service.State)
            {
                SMOUtilities.GetMessage("1001", service, result);
                result.ExtData = "SubmitIndex();";
            }
            else
            {
                result.Type = TransferType.AlertDanger;
                SMOUtilities.GetMessage("1004", service, result);
            }
            return result.ToJsonResult();
        }

        [MyValidateAntiForgeryToken]
        public ActionResult Edit(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                _service.Get(id);
            }
            return PartialView(_service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(DeliveryConditionsService service)
        {
            var result = new TransferObject
            {
                Type = TransferType.AlertSuccessAndJsCommand
            };
            service.Update();
            if (service.State)
            {
                SMOUtilities.GetMessage("1002", service, result);
                result.ExtData = "SubmitIndex();";
            }
            else
            {
                result.Type = TransferType.AlertDanger;
                SMOUtilities.GetMessage("1005", service, result);
            }
            return result.ToJsonResult();
        }

    }
}