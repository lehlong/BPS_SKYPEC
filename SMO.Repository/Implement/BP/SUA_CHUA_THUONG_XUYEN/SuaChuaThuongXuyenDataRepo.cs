using SMO.Core.Entities;
using SMO.Core.Entities.BP.SUA_CHUA_THUONG_XUYEN;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP;
using SMO.Repository.Interface.BP.SUA_CHUA_THUONG_XUYEN;
using System.Collections.Generic;
using System.Linq;

namespace SMO.Repository.Implement.BP.SUA_CHUA_THUONG_XUYEN
{
    public class SuaChuaThuongXuyenDataRepo : GenericRepository<T_BP_SUA_CHUA_THUONG_XUYEN_DATA>, ISuaChuaThuongXuyenDataRepo
    {
        public SuaChuaThuongXuyenDataRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
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
        public IList<T_BP_SUA_CHUA_THUONG_XUYEN_DATA> GetCFDataByOrgCode(string centerCode, int year, string templateCode, int? version)
        {
            if (templateCode is null)
            {
                if (version == null ||
                    version == NHibernateSession.QueryOver<T_BP_SUA_CHUA_THUONG_XUYEN>()
                    .Where(x => x.ORG_CODE == centerCode &&
                    x.TIME_YEAR == year).List().Select(x => x.VERSION).OrderByDescending(x => x).FirstOrDefault())
                {
                    var queryOver = NHibernateSession.QueryOver<T_BP_SUA_CHUA_THUONG_XUYEN_DATA>();
                    queryOver = queryOver.Where(x => x.TIME_YEAR == year);
                    queryOver = queryOver.Where(x => x.TEMPLATE_CODE != "");
                    queryOver = queryOver.Where(x => x.ORG_CODE == centerCode);
                    queryOver = queryOver.Fetch(x => x.KhoanMucSuaChua).Eager;
                    queryOver = queryOver.Fetch(x => x.SuaChuaProfitCenter).Eager;
                    return queryOver.List();
                }
                else
                {
                    var queryOver = NHibernateSession.QueryOver<T_BP_SUA_CHUA_THUONG_XUYEN_DATA_HISTORY>();
                    queryOver = queryOver.Where(x => x.TIME_YEAR == year);
                    queryOver = queryOver.Where(x => x.ORG_CODE == centerCode);
                    queryOver = queryOver.Where(x => x.VERSION == version);
                    queryOver = queryOver.Fetch(x => x.KhoanMucSuaChua).Eager;
                    queryOver = queryOver.Fetch(x => x.SuaChuaProfitCenter).Eager;
                    return queryOver.List().Select(x => (T_BP_SUA_CHUA_THUONG_XUYEN_DATA)x).ToList();
                }
            }
            else
            {
                if (version == null ||
                    version == NHibernateSession.QueryOver<T_BP_SUA_CHUA_THUONG_XUYEN>()
                    .Where(x => x.ORG_CODE == centerCode &&
                    x.TIME_YEAR == year &&
                    x.TEMPLATE_CODE == templateCode).List().Select(x => x.VERSION).OrderByDescending(x => x).FirstOrDefault())
                {
                    var queryOver = NHibernateSession.QueryOver<T_BP_SUA_CHUA_THUONG_XUYEN_DATA>();
                    queryOver = queryOver.Where(x => x.TIME_YEAR == year);
                    queryOver = queryOver.Where(x => x.ORG_CODE == centerCode);
                    queryOver = queryOver.Where(x => x.TEMPLATE_CODE == templateCode);
                    queryOver = queryOver.Fetch(x => x.KhoanMucSuaChua).Eager;
                    queryOver = queryOver.Fetch(x => x.SuaChuaProfitCenter).Eager;
                    return queryOver.List();
                }
                else
                {
                    var queryOver = NHibernateSession.QueryOver<T_BP_SUA_CHUA_THUONG_XUYEN_DATA_HISTORY>();
                    queryOver = queryOver.Where(x => x.TIME_YEAR == year);
                    queryOver = queryOver.Where(x => x.ORG_CODE == centerCode);
                    queryOver = queryOver.Where(x => x.VERSION == version);
                    queryOver = queryOver.Where(x => x.TEMPLATE_CODE == templateCode);
                    queryOver = queryOver.Fetch(x => x.KhoanMucSuaChua).Eager;
                    queryOver = queryOver.Fetch(x => x.SuaChuaProfitCenter).Eager;
                    return queryOver.List().Select(x => (T_BP_SUA_CHUA_THUONG_XUYEN_DATA)x).ToList();
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
        public IList<T_BP_SUA_CHUA_THUONG_XUYEN_DATA> GetCFDataByCenterCode(string orgCode, IList<string> centerCodes, int year, string templateCode, int? version)
        {
            if (templateCode is null)
            {
                if (version == null ||
                    version == NHibernateSession.QueryOver<T_BP_SUA_CHUA_THUONG_XUYEN>()
                    .Where(x => x.ORG_CODE == orgCode &&
                    x.TIME_YEAR == year).List().Select(x => x.VERSION).OrderByDescending(x => x).FirstOrDefault())
                {
                    var queryOver = NHibernateSession.QueryOver<T_BP_SUA_CHUA_THUONG_XUYEN_DATA>();
                    if (orgCode != null)
                    {
                        queryOver = queryOver.Where(x => x.ORG_CODE == orgCode);
                    }
                    queryOver = queryOver.Where(x => x.TIME_YEAR == year);
                    queryOver = queryOver.AndRestrictionOn(x => x.ORG_CODE).IsIn(centerCodes.ToList());
                    queryOver = queryOver.Fetch(x => x.KhoanMucSuaChua).Eager;
                    queryOver = queryOver.Fetch(x => x.SuaChuaProfitCenter).Eager;
                    return queryOver.List();
                }
                else
                {
                    var queryOver = NHibernateSession.QueryOver<T_BP_SUA_CHUA_THUONG_XUYEN_DATA_HISTORY>();
                    queryOver = queryOver.Where(x => x.TIME_YEAR == year);
                    if (orgCode != null)
                    {
                        queryOver = queryOver.Where(x => x.ORG_CODE == orgCode);
                    }
                    queryOver = queryOver.AndRestrictionOn(x => x.ORG_CODE).IsIn(centerCodes.ToList());
                    queryOver = queryOver.Where(x => x.VERSION == version);
                    queryOver = queryOver.Fetch(x => x.KhoanMucSuaChua).Eager;
                    queryOver = queryOver.Fetch(x => x.SuaChuaProfitCenter).Eager;
                    return queryOver.List().Select(x => (T_BP_SUA_CHUA_THUONG_XUYEN_DATA)x).ToList();
                }
            }
            else
            {
                if (version == null ||
                    version == NHibernateSession.QueryOver<T_BP_SUA_CHUA_THUONG_XUYEN>()
                    .Where(x => x.ORG_CODE == orgCode &&
                    x.TIME_YEAR == year &&
                    x.TEMPLATE_CODE == templateCode).List().Select(x => x.VERSION).OrderByDescending(x => x).FirstOrDefault())
                {
                    var queryOver = NHibernateSession.QueryOver<T_BP_SUA_CHUA_THUONG_XUYEN_DATA>();
                    queryOver = queryOver.Where(x => x.TIME_YEAR == year);
                    if (orgCode != null)
                    {
                        queryOver = queryOver.Where(x => x.ORG_CODE == orgCode);
                    }
                    queryOver = queryOver.AndRestrictionOn(x => x.ORG_CODE).IsIn(centerCodes.ToList());
                    queryOver = queryOver.Where(x => x.TEMPLATE_CODE == templateCode);
                    queryOver = queryOver.Fetch(x => x.KhoanMucSuaChua).Eager;
                    queryOver = queryOver.Fetch(x => x.SuaChuaProfitCenter).Eager;
                    return queryOver.List();
                }
                else
                {
                    var queryOver = NHibernateSession.QueryOver<T_BP_SUA_CHUA_THUONG_XUYEN_DATA_HISTORY>();
                    queryOver = queryOver.Where(x => x.TIME_YEAR == year);
                    if (orgCode != null)
                    {
                        queryOver = queryOver.Where(x => x.ORG_CODE == orgCode);
                    }
                    queryOver = queryOver.AndRestrictionOn(x => x.ORG_CODE).IsIn(centerCodes.ToList());
                    queryOver = queryOver.Where(x => x.VERSION == version);
                    queryOver = queryOver.Where(x => x.TEMPLATE_CODE == templateCode);
                    queryOver = queryOver.Fetch(x => x.KhoanMucSuaChua).Eager;
                    queryOver = queryOver.Fetch(x => x.SuaChuaProfitCenter).Eager;
                    return queryOver.List().Select(x => (T_BP_SUA_CHUA_THUONG_XUYEN_DATA)x).ToList();
                }
            }
        }
    }
}
