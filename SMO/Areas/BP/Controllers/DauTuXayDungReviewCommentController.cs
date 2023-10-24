﻿using SMO.Service.BP.DAU_TU_XAY_DUNG;
using SMO.Service.BP.KE_HOACH_SAN_LUONG;
using SMO.Service.Class;

using System.Web.Mvc;

namespace SMO.Areas.BP.Controllers
{
    public class DauTuXayDungReviewCommentController : Controller
    {
        private readonly DauTuXayDungReviewCommentService _service;

        public DauTuXayDungReviewCommentController()
        {
            _service = new DauTuXayDungReviewCommentService();
        }

        /// <summary>
        /// modal
        /// </summary>
        /// <param name="orgCode"></param>
        /// <param name="elementCode"></param>
        /// <param name="year"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        [MyValidateAntiForgeryToken]
        public ActionResult ReviewData(string orgCode, string elementCode, int year, int version, string onOrgCode)
        {
            _service.GetHeader(orgCode, elementCode, year, version, onOrgCode);

            return PartialView(_service);
        }

        //[MyValidateAntiForgeryToken]
        public ActionResult Index(string orgCode, string elementCode, int year, int version, string onOrgCode)
        {
            _service.GetHeader(orgCode, elementCode, year, version, onOrgCode);

            return PartialView(_service);
        }

        [HttpGet]
        [MyValidateAntiForgeryToken]
        public JsonResult RefreshComment(int year, string orgCode, string elementCode, string onOrgCode)
        {
            return Json(_service.GetComments(year, orgCode, elementCode, onOrgCode), JsonRequestBehavior.AllowGet);
        }


        [ValidateAntiForgeryToken]
        public ActionResult List(DauTuXayDungReviewCommentService service)
        {
            service.GetComments();
            return PartialView("../Comment/ListDauTuXayDung", service.ObjList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Comment(DauTuXayDungReviewCommentService service)
        {
            var result = new TransferObject
            {
                Type = TransferType.AlertSuccessAndJsCommand
            };
            service.Create();
            if (service.State)
            {
                SMOUtilities.GetMessage("1001", service, result);
                result.ExtData = string.Format("Forms.SubmitForm('{0}'); $('#txtContent').val('')", service.ObjDetail.PKID);
            }
            else
            {
                result.Type = TransferType.AlertDanger;
                SMOUtilities.GetMessage("1004", service, result);
            }
            return result.ToJsonResult();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CommentDataCenter(DauTuXayDungReviewCommentService service)
        {
            var result = new TransferObject
            {
                Type = TransferType.AlertSuccessAndJsCommand
            };
            service.Create();
            if (service.State)
            {
                SMOUtilities.GetMessage("1001", service, result);
                result.ExtData = $"Forms.SubmitForm('{service.ObjDetail.PKID}'); " +
                    $"RefreshComment('{service.ObjDetail.KHOAN_MUC_DAU_TU_CODE}', '{service.ObjDetail.ON_ORG_CODE}'); " +
                    $"$('#txtContent').val('')";
            }
            else
            {
                result.Type = TransferType.AlertDanger;
                SMOUtilities.GetMessage("1004", service, result);
            }
            return result.ToJsonResult();

        }

        [MyValidateAntiForgeryToken]
        public ActionResult FilterCommentCenter(FilterCommentCenterViewModel model)
        {
            return PartialView(model);
        }
    }
}