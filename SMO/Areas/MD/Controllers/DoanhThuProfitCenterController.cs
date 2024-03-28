using SMO.Service.MD;

using System.Web.Mvc;

namespace SMO.Areas.MD.Controllers
{
    public class DoanhThuProfitCenterController : Controller
    {
        private readonly DoanhThuProfitCenterService _service;

        public DoanhThuProfitCenterController()
        {
            _service = new DoanhThuProfitCenterService();
        }

        [MyValidateAntiForgeryToken]
        public JsonResult BuildTreeByTemplate(string templateId, int year)
        {
            var lstHangHangKhong = _service.GetNodeHangHangKhongByTemplate(templateId, year);
            var lstSanBay = _service.GetNodeSanBayByTemplate(templateId, year);
            return Json(new { companies = lstHangHangKhong, projects = lstSanBay }, JsonRequestBehavior.AllowGet);
        }
    }
}
