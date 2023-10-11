using SMO.Service.BU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace SMO.Areas.BU.Controllers
{
    public class FileUploadController : Controller
    {
        // GET: BU/FileUpload
        private FileUploadService _service;
        public FileUploadController()
        {
            _service = new FileUploadService();
        }
        public ActionResult Index(string id)
        {
           _service.ObjDetail.PARENT = id;
          //  _service.GetListFiles();
            return PartialView(_service);
        }
        public ActionResult UpdateViewContract(string id, string isRemoveFile,int version)
        {
            if (!string.IsNullOrEmpty(isRemoveFile))
            {
                ViewBag.IsRemoveFile = isRemoveFile;
            }
            if(version==0)
            {
                _service.version=0;

            }
            else
            {
                _service.version = version;
            }
            _service.ObjDetail.PARENT = id;
            _service.GetListFiles();
            return PartialView(_service);
        }
        public ActionResult GetListFiles(string id, string isRemoveFile,int version)
        {
            ViewBag.IsRemoveFile = isRemoveFile;
            if (version == 0)
            {
                _service.version = 0;

            }
            else
            {
                _service.version = version;
            }
            _service.ObjDetail.PARENT = id;
            return PartialView(_service);
        }
        public ActionResult SearchListFiles(FileUploadService service,string isRemoveFile)
        {
            if (!string.IsNullOrEmpty(isRemoveFile))
            {
                ViewBag.IsRemoveFile = isRemoveFile;
            }
            service.GetListFiles();
            return PartialView(service);
        }
        public ActionResult UpdateFiles(FileUploadService service,HttpPostedFileBase file)
        {
            var result = new TransferObject();
            result.Type = TransferType.AlertSuccessAndJsCommand;
            service.UpdateFile(Request);
            if (service.State)
            {
                SMOUtilities.GetMessage("1001", service, result);
            }
            else
            {
                result.Type = TransferType.AlertDanger;
                SMOUtilities.GetMessage("1004", service, result);
            }
            return result.ToJsonResult();
        }
    }
}