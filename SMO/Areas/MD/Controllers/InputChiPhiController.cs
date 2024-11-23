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
    public class InputChiPhiController : Controller
    {
        private readonly InputChiPhiService _service;
        // GET: MD/InputChiPhi
        public InputChiPhiController()
        {
            _service = new InputChiPhiService();
        }
        public ActionResult Index()
        {
            return PartialView(_service);
        }
        public ActionResult GenDataChiPhiReport(int year,string area)
        {
            ViewBag.areacode = area;
            var data = _service.GetDataChiPhi(year,area);
            return PartialView(data);
        }
        public ActionResult Update(string data, int year, string area)
        {
            var jsonData = JsonConvert.DeserializeObject<List<T_MD_INPUT_CHI_PHI>>(data);
          
           
            var result = new TransferObject
            {
                Type = TransferType.AlertSuccessAndJsCommand
            };

            _service.UpdateData(jsonData, year, area);
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