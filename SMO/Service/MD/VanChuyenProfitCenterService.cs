using SMO.Core.Entities.MD;
using SMO.Repository.Implement.MD;
using SMO.Service.Common;

using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SMO.Service.MD
{
    public class VanChuyenProfitCenterService : GenericCenterService<T_MD_VAN_CHUYEN_PROFIT_CENTER, VanChuyenProfitCenterRepo>
    {
        internal IList<NodeCompany> GetNodeCompanyByTemplate(string templateId, int year)
        {
            var lstNode = new List<NodeCompany>();

            var template = UnitOfWork.Repository<TemplateRepo>().Get(templateId);
            var allCompany = UnitOfWork.Repository<CostCenterRepo>().GetAll();
            var lstDetails = new List<string>();
            if (template != null)
            {
                // get all cost center selected in template
                lstDetails = template.DetailKeHoachVanChuyen
                    .Where(x => x.TIME_YEAR == year)
                    .GroupBy(x => x.CENTER_CODE)
                    .Select(x => x.First().CENTER_CODE).ToList();
            }

            var selectedCompany = CurrentRepository.GetManyWithFetch(x => lstDetails.Contains(x.CODE))
                .GroupBy(x => x.ORG_CODE)
                .Select(x => x.First().ORG_CODE);
            foreach (var item in allCompany)
            {
                var node = new NodeCompany()
                {
                    id = item.CODE,
                    pId = null,
                    name = $"<span>{item.CODE} - {item.NAME}</span>",
                    type = Budget.VAN_CHUYEN.ToString()
                };

                // check selected cost center
                if (selectedCompany.SingleOrDefault(x => x.Equals(item.CODE)) != null)
                    node.@checked = "true";
                if (item.CODE == ProfileUtilities.User.ORGANIZE_CODE)
                    node.@checked = "true";
                lstNode.Add(node);
            }

            return lstNode;
        }

        internal IList<NodeProject> GetNodeProjectByTemplate(string templateId, int year)
        {
            var lstNode = new List<NodeProject>();

            var template = UnitOfWork.Repository<TemplateRepo>().Get(templateId);
            var allProjects = UnitOfWork.Repository<RouteRepo>().GetAll();
            var lstDetails = new List<string>();
            if (template != null)
            {
                lstDetails = template.DetailKeHoachVanChuyen
                        .Where(x => x.TIME_YEAR == year)
                        .GroupBy(x => x.CENTER_CODE)
                        .Select(x => x.First().CENTER_CODE).ToList();
            }

            var selectedProjects = CurrentRepository.GetManyWithFetch(x => lstDetails.Contains(x.CODE))
                .GroupBy(x => x.ROUTE_CODE)
                .Select(x => x.First().ROUTE_CODE);
            foreach (var item in allProjects)
            {
                var node = new NodeProject()
                {
                    id = item.CODE,
                    pId = null,
                    name = $"<span>{item.CODE} - {item.NAME}</span>",
                    type = Budget.VAN_CHUYEN.ToString()
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
