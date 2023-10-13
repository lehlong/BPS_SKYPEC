using Hangfire.Storage;
using SMO.Core.Entities.BU;
using SMO.Service.BU;
using SMO.Service.MD;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace SMO.Areas.BU.Controllers
{
    public class ContractController : Controller
    {
        // GET: BU/Contract
        private ContractService _service;
        public ContractController()
        {
            _service = new ContractService();
        }
        [MyValidateAntiForgeryToken]
        public ActionResult Index()
        {
            ViewBag.DataDashboard = _service.GetDataDashboard();
            return View(_service);
        }
        [ValidateAntiForgeryToken]
        public ActionResult List(ContractService service)
        {
            service.Search();
            return PartialView(service);
        }
        [HttpPost]
        public ActionResult FilterList(ContractService service)
        {
            service.Search();
            foreach (var item in service.ObjList)
            {
                try
                {
                    item.PHANTRAM = Math.Floor((service.getAmount(item.NAME_CONTRACT, item.VERSION) / item.CONTRACT_VALUE_VAT) * 100);
                }
                catch
                {
                    item.PHANTRAM = 0;
                }
            }
            var jsonData = _service.converListContract(service.ObjList);
            return Content(jsonData, "application/json");
        }
        public ActionResult ShowListFile(string id, int version)
        {
            var service = new FileUploadService();
            service.ObjDetail.PARENT = id;
            service.ObjDetail.VERSION = version;
            return View(service);
        }
        public ActionResult showContractChild(string id)
        {
            var jsonData = _service.getContractChild(id);
            return Content(jsonData, "application/json");
        }

        [MyValidateAntiForgeryToken]
        public ActionResult Create()
        {
            return PartialView(_service);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateContract(ContractService service, HttpPostedFileBase file)
        {
            var result = new TransferObject();
            result.Type = TransferType.AlertSuccessAndJsCommand;
            service.Create(Request);
            if (service.State)
            {
                result.Data = service.ObjDetail.NAME_CONTRACT;
                SMOUtilities.GetMessage("1001", service, result);
            }
            else
            {
                result.Type = TransferType.AlertDanger;
                SMOUtilities.GetMessage("1004", service, result);
            }
            return result.ToJsonResult();
        }
        public ActionResult CreateContractChild(string id)
        {
            _service.ObjDetail.PARENT = id;
            _service.GetParent();
            return PartialView(_service);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateChildContract(ContractService service, HttpPostedFileBase file)
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

        public ActionResult Detail(string id, int version)
        {
            // id chính là nameContract
            if (version != 0)
            {
                //_service.getMaxVersion(id);
                _service.GetOldVersion(id, version);
                _service.getListFollowers(id, version);
                return PartialView(_service);
            }
            // _service.getMaxVersion(id);
            _service.GetOldVersion(id, 0);
            _service.getListFollowers(id, _service.ObjDetail.VERSION);
            _service.checkAccessRights();

            if (!_service.State)
            {
                return PartialView("ERROR", _service);
            }
            return PartialView(_service);
        }

        public ActionResult showHistoryProcess(string id, int version)
        {
            var service = new ContractProcessServices();
            service.GetListProcess(id, version);
            return PartialView(service);
        }
        public ActionResult showHistory(string id, int version)
        {
            var service = new ContractHistoryService();
            service.GetHistory(id, version);
            return PartialView(service);
        }

        public ActionResult showComment(string id, int version)
        {
            var service = new ContractCommentService();
            service.GetListComment(id, version);
            return PartialView(service);
        }
        [HttpPost]
        public ActionResult TrinhDuyet(string id)
        {
            var result = new TransferObject();
            result.Type = TransferType.AlertSuccessAndJsCommand;
            _service.Get(id);
            _service.TrinhDuyet();
            if (_service.State)
            {
                SMOUtilities.GetMessage("7005", _service, result);
            }
            else
            {
                result.Type = TransferType.AlertDanger;
                SMOUtilities.GetMessage("7004", _service, result);
            }
            return result.ToJsonResult();
        }
        [HttpPost]
        public ActionResult HuyTrinhDuyet(string id)
        {
            var result = new TransferObject();
            result.Type = TransferType.AlertSuccessAndJsCommand;
            _service.Get(id);
            _service.HuyTrinhDuyet();
            if (_service.State)
            {
                SMOUtilities.GetMessage("7005", _service, result);
            }
            else
            {
                result.Type = TransferType.AlertDanger;
                SMOUtilities.GetMessage("7004", _service, result);
            }
            return result.ToJsonResult();
        }
        [HttpPost]
        public ActionResult PheDuyet(string id)
        {
            var result = new TransferObject();
            result.Type = TransferType.AlertSuccessAndJsCommand;
            _service.Get(id);
            _service.PheDuyet();
            if (_service.State)
            {
                SMOUtilities.GetMessage("7005", _service, result);
            }
            else
            {
                result.Type = TransferType.AlertDanger;
                SMOUtilities.GetMessage("7004", _service, result);
            }
            return result.ToJsonResult();
        }
        [HttpPost]
        public ActionResult TuChoi(string id)
        {
            var result = new TransferObject();
            result.Type = TransferType.AlertSuccessAndJsCommand;
            _service.Get(id);
            _service.TuChoi();
            if (_service.State)
            {
                SMOUtilities.GetMessage("7005", _service, result);
            }
            else
            {
                result.Type = TransferType.AlertDanger;
                SMOUtilities.GetMessage("7004", _service, result);
            }
            return result.ToJsonResult();
        }
        [HttpPost]
        public ActionResult SendComment(string id, string comment, int version)
        {
            var result = new TransferObject();
            result.Type = TransferType.AlertSuccessAndJsCommand;
            var service = new ContractCommentService();
            service.CreateComment(id, comment, version);
            if (_service.State)
            {
                SMOUtilities.GetMessage("7005", service, result);
            }
            else
            {
                result.Type = TransferType.AlertDanger;
                SMOUtilities.GetMessage("7004", service, result);
            }
            return result.ToJsonResult();
        }
        public ActionResult Edit(string id)
        {
            _service.GetOldVersion(id, 0);
            _service.getListFollowers(id, _service.version);
            return PartialView(_service);
        }
        [HttpPost]
        public ActionResult UpdateContract(ContractService service)
        {
            var result = new TransferObject();
            result.Type = TransferType.AlertSuccessAndJsCommand;
            service.UpdateContract(Request);
            if (service.State)
            {
                SMOUtilities.GetMessage("7005", service, result);
                result.ExtData = $"SubmitIndex();Forms.Close('{service.ViewId}');Forms.LoadAjax({{url:'{Url.Action("Edit", new { id = service.ObjDetail.ID })}'}});";
            }
            else
            {
                result.Type = TransferType.AlertDanger;
                SMOUtilities.GetMessage("7004", service, result);
            }
            return result.ToJsonResult();
        }
        [HttpPost]
        public ActionResult CreateVersion(ContractService service)
        {
            var result = new TransferObject();
            result.Type = TransferType.AlertSuccessAndJsCommand;
            service.CreateVersionContract(Request);
            if (service.State)
            {
                SMOUtilities.GetMessage("7005", service, result);
                result.ExtData = $"reload();";
            }
            else
            {
                result.Type = TransferType.AlertDanger;
                SMOUtilities.GetMessage("7004", service, result);
            }
            return result.ToJsonResult();
        }
        public ActionResult ExportExcelTemplate()
        {
            MemoryStream outFileStream = new MemoryStream();
            var path = Server.MapPath("~/TemplateExcel/Template_Base_Contract.xlsx");
            _service.ExportExcelTemplate(ref outFileStream, path);
            if (!_service.State)
            {
                return Content(_service.ErrorMessage);
            }
            return File(outFileStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "HangMucDuAn.xlsx");
        }
        public ActionResult GetCustomer(string code)
        {
            var result = new TransferObject();
            result.Type = TransferType.AlertSuccessAndJsCommand;
            result.Data = _service.GetCustomer(code);      
            return result.ToJsonResult();
        }

    }
}