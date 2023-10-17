using SMO.Core.Entities;
using SMO.Core.Entities.BP.KE_HOACH_DOANH_THU;
using SMO.Core.Entities.MD;

using System.Collections.Generic;

namespace SMO.ServiceInterface.BP.KeHoachDoanhThu
{
    public interface IKeHoachDoanhThuService
    {
        IList<T_MD_KHOAN_MUC_DOANH_THU> GetDataCostPreview(
            out IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_DOANH_THU> detailCostElements,
            string templateId,
            string centerCode = "",
            int? year = null,
            int? version = null,
            bool? isHasValue = null);
        IList<T_MD_KHOAN_MUC_DOANH_THU> PreparePureList(IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_DOANH_THU> detailCostElements, int year);
        IList<T_MD_KHOAN_MUC_DOANH_THU> PreparePureList(out IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_DOANH_THU> detailCostElements,
        string templateId,
        int year,
        string centerCode = "",
        bool ignoreAuth = false);

        IList<T_MD_KHOAN_MUC_DOANH_THU> PreparePureListForTemplate(out IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_DOANH_THU> detailCostElements,
            string templateId,
            int year);
        IList<T_MD_KHOAN_MUC_DOANH_THU> PreparePureList(out IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_DOANH_THU> detailCostElements,
            IList<string> centerCodes,
            int year);
        IList<T_MD_KHOAN_MUC_DOANH_THU> SummarySumUpCenter(
           out IList<T_BP_KE_HOACH_DOANH_THU_DATA> plDataCostElements,
           int year,
           string centerCode,
           int? version,
           bool? isHasValue = null,
           string templateId = "");
        IList<T_MD_KHOAN_MUC_DOANH_THU> SummaryCenterOut(out IList<T_BP_KE_HOACH_DOANH_THU_DATA> plDataCostElements, string centerCode, int year, int? version, bool? isHasValue = null);
        IList<T_MD_KHOAN_MUC_DOANH_THU> SummaryCenterVersion(out IList<T_BP_KE_HOACH_DOANH_THU_DATA> plDataCostElements, string centerCode, int year, int? version, bool isDrillDown = false);
        //public T_BP_KE_HOACH_DOANH_THU CheckTemplate(string template, int year, string orgCode) { }
    }
}
