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
    public class DataTraNapController : Controller
    {
        private readonly InputKhService _service;
        // GET: MD/InputKh
        public DataTraNapController()
        {
            _service = new InputKhService();

        }

        public ActionResult Index()
        {
            return PartialView(_service);
        }
        public ActionResult GenDataTraNap(int year)
        {
            var data= _service.GetdataTraNapIP(year);

            return PartialView(data);
        }
        public ActionResult Update(string data, int year)
        {
            var jsonData = JsonConvert.DeserializeObject<List<T_MD_DATA_TRA_NAP>>(data);


            var result = new TransferObject
            {
                Type = TransferType.AlertSuccessAndJsCommand
            };

            _service.UpdateTraNap(jsonData, year);
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