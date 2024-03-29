﻿using SMO.Service.MD;

using System.Web.Mvc;

namespace SMO.Areas.MD.Controllers
{
    public class DauTuTrangThietBiProfitCenterController : Controller
    {
        private readonly DauTuTrangThietBiProfitCenterService _service;

        public DauTuTrangThietBiProfitCenterController()
        {
            _service = new DauTuTrangThietBiProfitCenterService();
        }

        [MyValidateAntiForgeryToken]
        public JsonResult BuildTreeByTemplate(string templateId, int year)
        {
            var lstCompany = _service.GetNodeCompanyByTemplate(templateId, year);
            var lstProject = _service.GetNodeProjectByTemplate(templateId, year);
            return Json(new { companies = lstCompany, projects = lstProject }, JsonRequestBehavior.AllowGet);
        }
    }
}
