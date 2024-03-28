using SMO.Service.MD;

using System.Web.Mvc;

namespace SMO.Areas.MD.Controllers
{
    public class DauTuXayDungProfitCenterController : Controller
    {
        private readonly DauTuXayDungProfitCenterService _service;

        public DauTuXayDungProfitCenterController()
        {
            _service = new DauTuXayDungProfitCenterService();
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
