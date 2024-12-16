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
        public ActionResult GenDataDm(int year)
        {
            var data= _service.GetdataKh(year);

            return PartialView(data);
        }
        public ActionResult Update(string data, int year)
        {
            var jsonData = JsonConvert.DeserializeObject<List<T_MD_INPUT_KH>>(data);


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