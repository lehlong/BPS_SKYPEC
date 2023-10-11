﻿using SMO.Core.Entities;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP;

using System.Collections.Generic;
using System.Linq;

namespace SMO.Repository.Implement.BP
{
    public class CostCFDataRepo : GenericRepository<T_BP_COST_CF_DATA>, ICostCFDataRepo
    {
        public CostCFDataRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {

        }

        /// <summary>
        /// Lấy dữ liệu theo đơn vị nộp
        /// </summary>
        /// <param name="centerCode"></param>
        /// <param name="year"></param>
        /// <param name="templateCode"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public IList<T_BP_COST_CF_DATA> GetCFDataByOrgCode(string centerCode, int year, string templateCode, int? version)
        {
            if (templateCode is null)
            {
                if (version == null ||
                    version == NHibernateSession.QueryOver<T_BP_COST_CF>()
                    .Where(x => x.ORG_CODE == centerCode &&
                    x.TIME_YEAR == year).List().Select(x => x.VERSION).OrderByDescending(x => x).FirstOrDefault())
                {
                    var queryOver = NHibernateSession.QueryOver<T_BP_COST_CF_DATA>();
                    queryOver = queryOver.Where(x => x.TIME_YEAR == year);
                    queryOver = queryOver.Where(x => x.TEMPLATE_CODE != "");
                    queryOver = queryOver.Where(x => x.ORG_CODE == centerCode);
                    queryOver = queryOver.Fetch(x => x.CostElement).Eager;
                    queryOver = queryOver.Fetch(x => x.CostCenter).Eager;
                    return queryOver.List();
                }
                else
                {
                    var queryOver = NHibernateSession.QueryOver<T_BP_COST_CF_DATA_HISTORY>();
                    queryOver = queryOver.Where(x => x.TIME_YEAR == year);
                    queryOver = queryOver.Where(x => x.ORG_CODE == centerCode);
                    queryOver = queryOver.Where(x => x.VERSION == version);
                    queryOver = queryOver.Fetch(x => x.CostElement).Eager;
                    queryOver = queryOver.Fetch(x => x.CostCenter).Eager;
                    return queryOver.List().Select(x => (T_BP_COST_CF_DATA)x).ToList();
                }
            }
            else
            {
                if (version == null ||
                    version == NHibernateSession.QueryOver<T_BP_COST_CF>()
                    .Where(x => x.ORG_CODE == centerCode &&
                    x.TIME_YEAR == year &&
                    x.TEMPLATE_CODE == templateCode).List().Select(x => x.VERSION).OrderByDescending(x => x).FirstOrDefault())
                {
                    var queryOver = NHibernateSession.QueryOver<T_BP_COST_CF_DATA>();
                    queryOver = queryOver.Where(x => x.TIME_YEAR == year);
                    queryOver = queryOver.Where(x => x.ORG_CODE == centerCode);
                    queryOver = queryOver.Where(x => x.TEMPLATE_CODE == templateCode);
                    queryOver = queryOver.Fetch(x => x.CostElement).Eager;
                    queryOver = queryOver.Fetch(x => x.CostCenter).Eager;
                    return queryOver.List();
                }
                else
                {
                    var queryOver = NHibernateSession.QueryOver<T_BP_COST_CF_DATA_HISTORY>();
                    queryOver = queryOver.Where(x => x.TIME_YEAR == year);
                    queryOver = queryOver.Where(x => x.ORG_CODE == centerCode);
                    queryOver = queryOver.Where(x => x.VERSION == version);
                    queryOver = queryOver.Where(x => x.TEMPLATE_CODE == templateCode);
                    queryOver = queryOver.Fetch(x => x.CostElement).Eager;
                    queryOver = queryOver.Fetch(x => x.CostCenter).Eager;
                    return queryOver.List().Select(x => (T_BP_COST_CF_DATA)x).ToList();
                }
            }
        }

        /// <summary>
        /// Lấy dữ liệu theo đơn vị được nộp
        /// </summary>
        /// <param name="orgCode">Mã đơn vị nộp</param>
        /// <param name="centerCodes">Danh sách những đơn vị chịu chi phí</param>
        /// <param name="year">Năm ngân sách</param>
        /// <param name="templateCode">Mẫu</param>
        /// <param name="version">Version dữ liệu</param>
        /// <returns></returns>
        public IList<T_BP_COST_CF_DATA> GetCFDataByCenterCode(string orgCode, IList<string> centerCodes, int year, string templateCode, int? version)
        {
            if (templateCode is null)
            {
                if (version == null ||
                    version == NHibernateSession.QueryOver<T_BP_COST_CF>()
                    .Where(x => x.ORG_CODE == orgCode &&
                    x.TIME_YEAR == year).List().Select(x => x.VERSION).OrderByDescending(x => x).FirstOrDefault())
                {
                    var queryOver = NHibernateSession.QueryOver<T_BP_COST_CF_DATA>();
                    if (orgCode != null)
                    {
                        queryOver = queryOver.Where(x => x.ORG_CODE == orgCode);
                    }
                    queryOver = queryOver.Where(x => x.TIME_YEAR == year);
                    queryOver = queryOver.AndRestrictionOn(x => x.COST_CENTER_CODE).IsIn(centerCodes.ToList());
                    queryOver = queryOver.Fetch(x => x.CostElement).Eager;
                    queryOver = queryOver.Fetch(x => x.CostCenter).Eager;
                    return queryOver.List();
                }
                else
                {
                    var queryOver = NHibernateSession.QueryOver<T_BP_COST_CF_DATA_HISTORY>();
                    queryOver = queryOver.Where(x => x.TIME_YEAR == year);
                    if (orgCode != null)
                    {
                        queryOver = queryOver.Where(x => x.ORG_CODE == orgCode);
                    }
                    queryOver = queryOver.AndRestrictionOn(x => x.COST_CENTER_CODE).IsIn(centerCodes.ToList());
                    queryOver = queryOver.Where(x => x.VERSION == version);
                    queryOver = queryOver.Fetch(x => x.CostElement).Eager;
                    queryOver = queryOver.Fetch(x => x.CostCenter).Eager;
                    return queryOver.List().Select(x => (T_BP_COST_CF_DATA)x).ToList();
                }
            }
            else
            {
                if (version == null ||
                    version == NHibernateSession.QueryOver<T_BP_COST_CF>()
                    .Where(x => x.ORG_CODE == orgCode &&
                    x.TIME_YEAR == year &&
                    x.TEMPLATE_CODE == templateCode).List().Select(x => x.VERSION).OrderByDescending(x => x).FirstOrDefault())
                {
                    var queryOver = NHibernateSession.QueryOver<T_BP_COST_CF_DATA>();
                    queryOver = queryOver.Where(x => x.TIME_YEAR == year);
                    if (orgCode != null)
                    {
                        queryOver = queryOver.Where(x => x.ORG_CODE == orgCode);
                    }
                    queryOver = queryOver.AndRestrictionOn(x => x.COST_CENTER_CODE).IsIn(centerCodes.ToList());
                    queryOver = queryOver.Where(x => x.TEMPLATE_CODE == templateCode);
                    queryOver = queryOver.Fetch(x => x.CostElement).Eager;
                    queryOver = queryOver.Fetch(x => x.CostCenter).Eager;
                    return queryOver.List();
                }
                else
                {
                    var queryOver = NHibernateSession.QueryOver<T_BP_COST_CF_DATA_HISTORY>();
                    queryOver = queryOver.Where(x => x.TIME_YEAR == year);
                    if (orgCode != null)
                    {
                        queryOver = queryOver.Where(x => x.ORG_CODE == orgCode);
                    }
                    queryOver = queryOver.AndRestrictionOn(x => x.COST_CENTER_CODE).IsIn(centerCodes.ToList());
                    queryOver = queryOver.Where(x => x.VERSION == version);
                    queryOver = queryOver.Where(x => x.TEMPLATE_CODE == templateCode);
                    queryOver = queryOver.Fetch(x => x.CostElement).Eager;
                    queryOver = queryOver.Fetch(x => x.CostCenter).Eager;
                    return queryOver.List().Select(x => (T_BP_COST_CF_DATA)x).ToList();
                }
            }
        }
    }
}
