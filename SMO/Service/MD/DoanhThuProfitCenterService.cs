using SMO.Core.Entities.MD;
using SMO.Repository.Implement.MD;
using SMO.Service.Common;

using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SMO.Service.MD
{
    public class DoanhThuProfitCenterService : GenericCenterService<T_MD_DOANH_THU_PROFIT_CENTER, DoanhThuProfitCenterRepo>
    {
        internal IList<NodeHangHangKhong> GetNodeHangHangKhongByTemplate(string templateId, int year)
        {
            var lstNode = new List<NodeHangHangKhong>();

            var template = UnitOfWork.Repository<TemplateRepo>().Get(templateId);
            var allHangHangKhong = UnitOfWork.Repository<HangHangKhongRepo>().GetAll();
            var lstDetails = new List<string>();
            if (template != null)
            {
                // get all cost center selected in template
                lstDetails = template.DetailKeHoachDoanhThu
                    .Where(x => x.TIME_YEAR == year)
                    .GroupBy(x => x.CENTER_CODE)
                    .Select(x => x.First().CENTER_CODE).ToList();
            }

            var selectedHangHangKhong = CurrentRepository.GetManyWithFetch(x => lstDetails.Contains(x.CODE))
                .GroupBy(x => x.HANG_HANG_KHONG_CODE)
                .Select(x => x.First().HANG_HANG_KHONG_CODE);
            foreach (var item in allHangHangKhong)
            {
                var node = new NodeHangHangKhong()
                {
                    id = item.CODE,
                    pId = null,
                    name = $"<span>{item.CODE} - {item.NAME}</span>",
                    type = Budget.DOANH_THU.ToString()
                };

                // check selected cost center
                if (selectedHangHangKhong.SingleOrDefault(x => x.Equals(item.CODE)) != null)
                    node.@checked = "true";

                lstNode.Add(node);
            }

            return lstNode;
        }

        internal IList<NodeSanBay> GetNodeSanBayByTemplate(string templateId, int year)
        {
            var lstNode = new List<NodeSanBay>();

            var template = UnitOfWork.Repository<TemplateRepo>().Get(templateId);
            var allProjects = UnitOfWork.Repository<SanBayRepo>().GetAll();
            var lstDetails = new List<string>();
            if (template != null)
            {
                lstDetails = template.DetailKeHoachDoanhThu
                        .Where(x => x.TIME_YEAR == year)
                        .GroupBy(x => x.CENTER_CODE)
                        .Select(x => x.First().CENTER_CODE).ToList();
            }

            var selectedProjects = CurrentRepository.GetManyWithFetch(x => lstDetails.Contains(x.CODE))
                .GroupBy(x => x.SAN_BAY_CODE)
                .Select(x => x.First().SAN_BAY_CODE);
            foreach (var item in allProjects)
            {
                var node = new NodeSanBay()
                {
                    id = item.CODE,
                    pId = null,
                    name = $"<span>{item.CODE} - {item.NAME}</span>",
                    type = Budget.DOANH_THU.ToString()
                };

                // check selected cost center
                if (selectedProjects.SingleOrDefault(x => x.Equals(item.CODE)) != null)
                    node.@checked = "true";

                lstNode.Add(node);
            }

            return lstNode;
        }
    }
}
