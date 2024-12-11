using Newtonsoft.Json;
using SMO.Core.Entities;
using SMO.Core.Entities.MD;
using SMO.Service.MD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMO.Areas.MD.Controllers
{
    public class CpTcnlDataController : Controller
    {
        private readonly CpTcnlDataService _service;
        // GET: MD/InputChiPhi
        public CpTcnlDataController()
        {
            _service = new CpTcnlDataService();
        }
        public ActionResult Index()
        {
            return PartialView(_service);
        }
        public ActionResult GenDataCpTcnl(int year)
        {
            var data = _service.GetdataCpnl(year);
            return PartialView(data);
        }
        public ActionResult Update(string data, int year, string area)
        {
            var jsonData = JsonConvert.DeserializeObject<List<CpTcnlImport>>(data);


            var result = new TransferObject
            {
                Type = TransferType.AlertSuccessAndJsCommand
            };

            _service.UpdateData(jsonData, year);
            if (_service.State)
            {
                SMOUtilities.GetMessage("1002", _service, result);
                result.ExtData = "SubmitIndex();";
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
