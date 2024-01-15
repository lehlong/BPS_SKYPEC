
using SMO.Core.Entities.MD;
using SMO.Repository.Implement.MD;

using SMO.Service.AD;
using SMO.Service.Common;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SMO.Service.MD
{
    public class InternalOrderService : GenericCenterService<T_MD_INTERNAL_ORDER, InternalOrderRepo>
    {
        public void Synchronize()
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                this.State = false;
                this.Exception = ex;
            }
        }

        internal IList<NodeInternalOrder> GetNodeCostCenterByTemplate(string templateId, int year)
        {
            var template = UnitOfWork.Repository<TemplateRepo>().Get(templateId);
            var lstNode = new List<NodeInternalOrder>();

            // get all cost center
            var lstInternalOrder = CurrentRepository.GetAll().OrderBy(x => x.C_ORDER).ToList();

            foreach (var item in lstInternalOrder)
            {
                var stack = new string[]
                {
                    item.PROJECT_CODE,
                    item.BLOCK_CODE,
                    item.IO_LEVEL1_CODE,
                    item.IO_LEVEL2_CODE,
                };
                item.PARENT_CODE = string.Join("_", stack.Where(s => !string.IsNullOrEmpty(s)));
            }
            // set all parent code to leave
            lstInternalOrder.ForEach(x => x.REAL_CENTER_CODE = x.CODE);

            var lstDetails = new List<string>();
            if (template != null)
            {
                switch (template.OBJECT_TYPE)
                {
                    case TemplateObjectType.DevelopProject:
                        //if (template.BUDGET_TYPE == BudgetType.KinhDoanh)
                        //{
                        //    // get all cost center selected in template
                        //    lstDetails = template.DetailContructCostPL
                        //        .Where(x => x.TIME_YEAR == year)
                        //        .GroupBy(x => x.CENTER_CODE)
                        //        .Select(x => x.First().CENTER_CODE).ToList();
                        //}
                        //else
                        //{
                        //    // get all cost center selected in template
                        //    lstDetails = template.DetailContructCostCF
                        //        .Where(x => x.TIME_YEAR == year)
                        //        .GroupBy(x => x.CENTER_CODE)
                        //        .Select(x => x.First().CENTER_CODE).ToList();
                        //}
                        //break;
                    case TemplateObjectType.Department:
                    case TemplateObjectType.Project:
                    default:
                        break;
                }

            }
            var lookupProject = lstInternalOrder.ToLookup(x => x.PROJECT_CODE);
            foreach (var keyProject in lookupProject.Select(x => x.Key))
            {
                var stack = new string[4];
                stack[0] = keyProject;
                lstInternalOrder.Add(new T_MD_INTERNAL_ORDER
                {
                    PARENT_CODE = null,
                    IS_GROUP = true,
                    CODE = keyProject,
                    REAL_CENTER_CODE = keyProject,
                    NAME = lookupProject[keyProject].First().PROJECT_NAME,
                });
                var lookupBlock = lookupProject[keyProject].ToLookup(x => x.BLOCK_CODE);
                foreach (var keyBlock in lookupBlock.Select(x => x.Key))
                {
                    stack[1] = keyBlock;
                    if (!string.IsNullOrEmpty(keyBlock))
                    {
                        lstInternalOrder.Add(new T_MD_INTERNAL_ORDER
                        {
                            PARENT_CODE = keyProject,
                            IS_GROUP = true,
                            REAL_CENTER_CODE = keyBlock,
                            CODE = string.Concat(keyProject, "_", keyBlock),
                            NAME = lookupBlock[keyBlock].First().BLOCK_NAME,
                        });
                    }
                    var lookupIOLv1 = lookupBlock[keyBlock].ToLookup(x => x.IO_LEVEL1_CODE);

                    foreach (var keyIOLv1 in lookupIOLv1.Select(x => x.Key))
                    {
                        if (!string.IsNullOrEmpty(keyIOLv1))
                        {
                            stack[2] = string.Empty;
                            lstInternalOrder.Add(new T_MD_INTERNAL_ORDER
                            {
                                PARENT_CODE = string.Join("_", stack.Where(s => !string.IsNullOrEmpty(s))),
                                REAL_CENTER_CODE = keyIOLv1,
                                IS_GROUP = true,
                                CODE = string.Join("_", stack.Where(s => !string.IsNullOrEmpty(s))) + string.Concat("_", keyIOLv1),
                                NAME = lookupIOLv1[keyIOLv1].First().IO_LEVEL1_NAME,
                            });
                        }
                        stack[2] = keyIOLv1;
                        var lookupIOLv2 = lookupIOLv1[keyIOLv1].ToLookup(x => x.IO_LEVEL2_CODE);

                        foreach (var keyIOLv2 in lookupIOLv2.Select(x => x.Key))
                        {
                            stack[3] = keyIOLv2;
                            if (!string.IsNullOrEmpty(keyIOLv2))
                            {
                                stack[3] = string.Empty;
                                lstInternalOrder.Add(new T_MD_INTERNAL_ORDER
                                {
                                    IS_GROUP = true,
                                    PARENT_CODE = string.Join("_", stack.Where(s => !string.IsNullOrEmpty(s))),
                                    REAL_CENTER_CODE = keyIOLv2,
                                    CODE = string.Join("_", stack.Where(s => !string.IsNullOrEmpty(s))) + string.Concat("_", keyIOLv2),
                                    NAME = lookupIOLv2[keyIOLv2].First().IO_LEVEL2_NAME,
                                });
                            }
                        }
                    }
                }
            }

            foreach (var item in lstInternalOrder)
            {
                var node = new NodeInternalOrder()
                {
                    id = item.CODE,
                    pId = item.PARENT_CODE,
                    isParent = item.IS_GROUP.ToString(),
                    name = $"<span>{item.REAL_CENTER_CODE} - {item.NAME}</span>",
                    type = Budget.INTERNAL_ORDER.ToString()
                };

                // check selected cost center
                if (lstDetails.SingleOrDefault(x => x.Equals(item.CODE)) != null)
                {
                    node.name = $"<span class='col-red'>{item.CODE} - {item.NAME}</span>";
                    node.@checked = "true";
                }

                lstNode.Add(node);
            }

            return lstNode;
        }

    }
}
