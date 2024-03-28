using SMO.Service.MD;

using System.Web.Mvc;

namespace SMO.Areas.MD.Controllers
{
    public class DauTuNgoaiDoanhNghiepProfitCenterController : Controller
    {
        private readonly DauTuNgoaiDoanhNghiepProfitCenterService _service;

        public DauTuNgoaiDoanhNghiepProfitCenterController()
        {
            _service = new DauTuNgoaiDoanhNghiepProfitCenterService();
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
