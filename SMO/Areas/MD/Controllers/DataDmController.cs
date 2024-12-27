using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using Newtonsoft.Json;
using SMO.Core.Entities.MD;
using SMO.Service.MD;

namespace SMO.Areas.MD.Controllers
{
    public class DataDmController : Controller
    {
        private readonly InputKhService _service;
        // GET: MD/InputKh
        public DataDmController()
        {
            _service = new InputKhService();

        }

        public ActionResult Index()
        {
            return PartialView(_service);
        }
        public ActionResult GenDataDm(int year,string area)
        {
            var data= _service.GetdataDm(year,area);

            return PartialView(data);
        }
        public ActionResult Update(string data, int year, string area)
        {
            var jsonData = JsonConvert.DeserializeObject<List<T_MD_HEADER_DM>>(data);


            var result = new TransferObject
            {
                Type = TransferType.AlertSuccessAndJsCommand
            };

            _service.UpdateDM(jsonData, year,area);
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