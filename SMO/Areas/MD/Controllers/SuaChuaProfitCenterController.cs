using SMO.Service.MD;

using System.Web.Mvc;

namespace SMO.Areas.MD.Controllers
{
    public class SuaChuaProfitCenterController : Controller
    {
        private readonly SuaChuaProfitCenterService _service;

        public SuaChuaProfitCenterController()
        {
            _service = new SuaChuaProfitCenterService();
        }

        [MyValidateAntiForgeryToken]
        public JsonResult BuildTreeByTemplate(string templateId, int year)
        {
            var lstHangHangKhong = _service.GetNodeSanBayByTemplate(templateId, year);
            var lstSanBay = _service.GetNodeCostCenterByTemplate(templateId, year);
            return Json(new { companies = lstHangHangKhong, projects = lstSanBay }, JsonRequestBehavior.AllowGet);
        }
    }
}
