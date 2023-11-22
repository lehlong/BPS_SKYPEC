using SMO.Core.Entities.MD;
using SMO.Repository.Implement.MD;
using SMO.Service.Common;

using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SMO.Service.MD
{
    public class SuaChuaThuongXuyenProfitCenterService : GenericCenterService<T_MD_SUA_CHUA_THUONG_XUYEN_PROFIT_CENTER, SuaChuaThuongXuyenProfitCenterRepo>
    {
        internal IList<NodeCostCenter> GetNodeCostCenterByTemplate(string templateId, int year)
        {
            var lstNode = new List<NodeCostCenter>();

            var template = UnitOfWork.Repository<TemplateRepo>().Get(templateId);
            var allHangHangKhong = UnitOfWork.Repository<CostCenterRepo>().GetAll();
            var lstDetails = new List<string>();
            if (template != null)
            {

                lstDetails = template.DetailSuaChuaThuongXuyen
                                        .Where(x => x.TIME_YEAR == year)
                                        .GroupBy(x => x.CENTER_CODE)
                                        .Select(x => x.First().CENTER_CODE).ToList();
            }

            var selectedHangHangKhong = CurrentRepository.GetManyWithFetch(x => lstDetails.Contains(x.CODE))
                .GroupBy(x => x.COST_CENTER_CODE)
                .Select(x => x.First().COST_CENTER_CODE);
            foreach (var item in allHangHangKhong)
            {
                var node = new NodeCostCenter()
                {
                    id = item.CODE,
                    pId = null,
                    name = $"<span>{item.CODE} - {item.NAME}</span>",
                    type = Budget.SUA_CHUA.ToString()
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

                lstDetails = template.DetailSuaChuaThuongXuyen
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
                    type = Budget.SUA_CHUA.ToString()
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
