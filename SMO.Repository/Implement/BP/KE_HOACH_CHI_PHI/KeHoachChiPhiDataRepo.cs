using SMO.Core.Entities;
using SMO.Core.Entities.BP.KE_HOACH_CHI_PHI;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP;
using SMO.Repository.Interface.BP.KE_HOACH_CHI_PHI;
using System.Collections.Generic;
using System.Linq;

namespace SMO.Repository.Implement.BP.KE_HOACH_CHI_PHI
{
    public class KeHoachChiPhiDataRepo : GenericRepository<T_BP_KE_HOACH_CHI_PHI_DATA>, IKeHoachChiPhiDataRepo
    {
        public KeHoachChiPhiDataRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
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
        public IList<T_BP_KE_HOACH_CHI_PHI_DATA> GetCFDataByOrgCode(string centerCode, int year, string templateCode, int? version)
        {
            if (templateCode is null)
            {
                if (version == null ||
                    version == NHibernateSession.QueryOver<T_BP_KE_HOACH_CHI_PHI>()
                    .Where(x => x.ORG_CODE == centerCode &&
                    x.TIME_YEAR == year).List().Select(x => x.VERSION).OrderByDescending(x => x).FirstOrDefault())
                {
                    var queryOver = NHibernateSession.QueryOver<T_BP_KE_HOACH_CHI_PHI_DATA>();
                    queryOver = queryOver.Where(x => x.TIME_YEAR == year);
                    queryOver = queryOver.Where(x => x.TEMPLATE_CODE != "");
                    queryOver = queryOver.Where(x => x.ORG_CODE == centerCode);
                    queryOver = queryOver.Fetch(x => x.KhoanMucChiPhi).Eager;
                    queryOver = queryOver.Fetch(x => x.ChiPhiProfitCenter).Eager;
                    return queryOver.List();
                }
                else
                {
                    var queryOver = NHibernateSession.QueryOver<T_BP_KE_HOACH_CHI_PHI_DATA_HISTORY>();
                    queryOver = queryOver.Where(x => x.TIME_YEAR == year);
                    queryOver = queryOver.Where(x => x.ORG_CODE == centerCode);
                    queryOver = queryOver.Where(x => x.VERSION == version);
                    queryOver = queryOver.Fetch(x => x.KhoanMucChiPhi).Eager;
                    queryOver = queryOver.Fetch(x => x.ChiPhiProfitCenter).Eager;
                    return queryOver.List().Select(x => (T_BP_KE_HOACH_CHI_PHI_DATA)x).ToList();
                }
            }
            else
            {
                if (version == null ||
                    version == NHibernateSession.QueryOver<T_BP_KE_HOACH_CHI_PHI>()
                    .Where(x => x.ORG_CODE == centerCode &&
                    x.TIME_YEAR == year &&
                    x.TEMPLATE_CODE == templateCode).List().Select(x => x.VERSION).OrderByDescending(x => x).FirstOrDefault())
                {
                    var queryOver = NHibernateSession.QueryOver<T_BP_KE_HOACH_CHI_PHI_DATA>();
                    queryOver = queryOver.Where(x => x.TIME_YEAR == year);
                    queryOver = queryOver.Where(x => x.ORG_CODE == centerCode);
                    queryOver = queryOver.Where(x => x.TEMPLATE_CODE == templateCode);
                    queryOver = queryOver.Fetch(x => x.KhoanMucChiPhi).Eager;
                    queryOver = queryOver.Fetch(x => x.ChiPhiProfitCenter).Eager;
                    return queryOver.List();
                }
                else
                {
                    var queryOver = NHibernateSession.QueryOver<T_BP_KE_HOACH_CHI_PHI_DATA_HISTORY>();
                    queryOver = queryOver.Where(x => x.TIME_YEAR == year);
                    queryOver = queryOver.Where(x => x.ORG_CODE == centerCode);
                    queryOver = queryOver.Where(x => x.VERSION == version);
                    queryOver = queryOver.Where(x => x.TEMPLATE_CODE == templateCode);
                    queryOver = queryOver.Fetch(x => x.KhoanMucChiPhi).Eager;
                    queryOver = queryOver.Fetch(x => x.ChiPhiProfitCenter).Eager;
                    return queryOver.List().Select(x => (T_BP_KE_HOACH_CHI_PHI_DATA)x).ToList();
                }
            }
        }

        /// <summary>
        /// Lấy dữ liệu theo đơn vị được nộp
        /// </summary>
        /// <param name="orgCode">Mã đơn vị nộp</param>
        /// <param name="centerCode"></param>
        /// <param name="year"></param>
        /// <param name="templateCode"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public IList<T_BP_KE_HOACH_CHI_PHI_DATA> GetCFDataByCenterCode(string orgCode, IList<string> centerCodes, int year, string templateCode, int? version)
        {
            if (templateCode is null)
            {
                if (version == null ||
                    version == NHibernateSession.QueryOver<T_BP_KE_HOACH_CHI_PHI>()
                    .Where(x => x.ORG_CODE == orgCode &&
                    x.TIME_YEAR == year).List().Select(x => x.VERSION).OrderByDescending(x => x).FirstOrDefault())
                {
                    var queryOver = NHibernateSession.QueryOver<T_BP_KE_HOACH_CHI_PHI_DATA>();
                    if (orgCode != null)
                    {
                        queryOver = queryOver.Where(x => x.ORG_CODE == orgCode);
                    }
                    queryOver = queryOver.Where(x => x.TIME_YEAR == year);
                    queryOver = queryOver.AndRestrictionOn(x => x.ORG_CODE).IsIn(centerCodes.ToList());
                    queryOver = queryOver.Fetch(x => x.KhoanMucChiPhi).Eager;
                    queryOver = queryOver.Fetch(x => x.ChiPhiProfitCenter).Eager;
                    return queryOver.List();
                }
                else
                {
                    var queryOver = NHibernateSession.QueryOver<T_BP_KE_HOACH_CHI_PHI_DATA_HISTORY>();
                    queryOver = queryOver.Where(x => x.TIME_YEAR == year);
                    if (orgCode != null)
                    {
                        queryOver = queryOver.Where(x => x.ORG_CODE == orgCode);
                    }
                    queryOver = queryOver.AndRestrictionOn(x => x.ORG_CODE).IsIn(centerCodes.ToList());
                    queryOver = queryOver.Where(x => x.VERSION == version);
                    queryOver = queryOver.Fetch(x => x.KhoanMucChiPhi).Eager;
                    queryOver = queryOver.Fetch(x => x.ChiPhiProfitCenter).Eager;
                    return queryOver.List().Select(x => (T_BP_KE_HOACH_CHI_PHI_DATA)x).ToList();
                }
            }
            else
            {
                if (version == null ||
                    version == NHibernateSession.QueryOver<T_BP_KE_HOACH_CHI_PHI>()
                    .Where(x => x.ORG_CODE == orgCode &&
                    x.TIME_YEAR == year &&
                    x.TEMPLATE_CODE == templateCode).List().Select(x => x.VERSION).OrderByDescending(x => x).FirstOrDefault())
                {
                    var queryOver = NHibernateSession.QueryOver<T_BP_KE_HOACH_CHI_PHI_DATA>();
                    queryOver = queryOver.Where(x => x.TIME_YEAR == year);
                    if (orgCode != null)
                    {
                        queryOver = queryOver.Where(x => x.ORG_CODE == orgCode);
                    }
                    queryOver = queryOver.AndRestrictionOn(x => x.ORG_CODE).IsIn(centerCodes.ToList());
                    queryOver = queryOver.Where(x => x.TEMPLATE_CODE == templateCode);
                    queryOver = queryOver.Fetch(x => x.KhoanMucChiPhi).Eager;
                    queryOver = queryOver.Fetch(x => x.ChiPhiProfitCenter).Eager;
                    return queryOver.List();
                }
                else
                {
                    var queryOver = NHibernateSession.QueryOver<T_BP_KE_HOACH_CHI_PHI_DATA_HISTORY>();
                    queryOver = queryOver.Where(x => x.TIME_YEAR == year);
                    if (orgCode != null)
                    {
                        queryOver = queryOver.Where(x => x.ORG_CODE == orgCode);
                    }
                    queryOver = queryOver.AndRestrictionOn(x => x.ORG_CODE).IsIn(centerCodes.ToList());
                    queryOver = queryOver.Where(x => x.VERSION == version);
                    queryOver = queryOver.Where(x => x.TEMPLATE_CODE == templateCode);
                    queryOver = queryOver.Fetch(x => x.KhoanMucChiPhi).Eager;
                    queryOver = queryOver.Fetch(x => x.ChiPhiProfitCenter).Eager;
                    return queryOver.List().Select(x => (T_BP_KE_HOACH_CHI_PHI_DATA)x).ToList();
                }
            }
        }
    }
}
