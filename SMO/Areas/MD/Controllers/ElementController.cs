using SMO.Repository.Implement.MD;
using SMO.Service.MD;
using System.Linq;
using System.Web.Mvc;

namespace SMO.Areas.MD.Controllers
{
    [AuthorizeCustom(Right = "R270")]
    public class ElementController : Controller
    {
        private readonly ElementService _service;

        public ElementController()
        {
            _service = new ElementService();
        }

        [MyValidateAntiForgeryToken]
        public ActionResult Index()
        {
            return PartialView(_service);
        }

        [MyValidateAntiForgeryToken]
        public ActionResult IndexKeHoachGiaThanh()
        {
            return PartialView(_service);
        }


        [MyValidateAntiForgeryToken]
        public ActionResult IndexKeHoachTaiChinh()
        {
            return PartialView(_service);
        }
        public ActionResult GetDataKeHoachTaiChinh(int year)
        {
            ViewBag.LstSharedData = _service.UnitOfWork.Repository<SharedDataRepo>().GetAll().ToList();
            var data = _service.GetDataKeHoachTaiChinh(year);
            return PartialView(data);
        }

        public ActionResult GetDataKeHoachGiaThanh(int year)
        {
            ViewBag.LstSharedData = _service.UnitOfWork.Repository<SharedDataRepo>().GetAll().ToList();
            var data = _service.GetDataKeHoachGiaThanh(year);
            return PartialView(data);
        }

        [ValidateAntiForgeryToken]
        public ActionResult List(ElementService service)
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
        public ActionResult Create(ElementService service)
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
        public ActionResult Update(ElementService service)
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