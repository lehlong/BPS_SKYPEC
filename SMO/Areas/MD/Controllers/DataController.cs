﻿using iTextSharp.text;
using Newtonsoft.Json;
using SMO.Core.Entities;
using SMO.Service.MD;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SMO.Areas.MD.Controllers
{
    [AuthorizeCustom(Right = "R6.2")]
    public class DataController : Controller
    {
        private readonly DataService _service;

        public DataController()
        {
            _service = new DataService();
        }

        [MyValidateAntiForgeryToken]
        public ActionResult Index()
        {
            return PartialView(_service);
        }

        public ActionResult GetDataByTimeYear(int year)
        {
            var data = _service.GetDataByTimeYear(year);
            return PartialView(data);
        }

        [HttpPost]
        public ActionResult Update(string data)
        {
            var jsonData = JsonConvert.DeserializeObject<List<T_MD_DATA>>(data);
            var result = new TransferObject
            {
                Type = TransferType.AlertSuccessAndJsCommand
            };
            _service.UpdateData(jsonData);
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