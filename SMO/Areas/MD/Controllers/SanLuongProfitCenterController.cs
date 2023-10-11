using SMO.Service.MD;

using System.Web.Mvc;

namespace SMO.Areas.MD.Controllers
{
    //[AuthorizeCustom(Right = "R208")]
    public class SanLuongProfitCenterController : Controller
    {
        private readonly SanLuongProfitCenterService _service;

        public SanLuongProfitCenterController()
        {
            _service = new SanLuongProfitCenterService();
        }

        [AuthorizeCustom(Right = "R302")]
        [MyValidateAntiForgeryToken]
        public JsonResult BuildTreeByTemplate(string templateId, int year)
        {
            var lstHangHangKhong = _service.GetNodeHangHangKhongByTemplate(templateId, year);
            var lstSanBay = _service.GetNodeSanBayByTemplate(templateId, year);
            return Json(new { companies = lstHangHangKhong, projects = lstSanBay }, JsonRequestBehavior.AllowGet);
        }
    }
}
