using SMO.Core.Entities;
using SMO.Core.Entities.BP.KE_HOACH_VAN_CHUYEN;
using SMO.Core.Entities.MD;

using System.Collections.Generic;

namespace SMO.ServiceInterface.BP.KeHoachVanChuyen
{
    public interface IKeHoachVanChuyenService
    {
        IList<T_MD_KHOAN_MUC_VAN_CHUYEN> GetDataCostPreview(
            out IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_VAN_CHUYEN> detailCostElements,
            string templateId,
            string centerCode = "",
            int? year = null,
            int? version = null,
            bool? isHasValue = null);
        IList<T_MD_KHOAN_MUC_VAN_CHUYEN> PreparePureList(IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_VAN_CHUYEN> detailCostElements, int year);
        IList<T_MD_KHOAN_MUC_VAN_CHUYEN> PreparePureList(out IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_VAN_CHUYEN> detailCostElements,
        string templateId,
        int year,
        string centerCode = "",
        bool ignoreAuth = false);

        IList<T_MD_KHOAN_MUC_VAN_CHUYEN> PreparePureListForTemplate(out IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_VAN_CHUYEN> detailCostElements,
            string templateId,
            int year);
        IList<T_MD_KHOAN_MUC_VAN_CHUYEN> PreparePureList(out IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_VAN_CHUYEN> detailCostElements,
            IList<string> centerCodes,
            int year);
        IList<T_MD_KHOAN_MUC_VAN_CHUYEN> SummarySumUpCenter(
           out IList<T_BP_KE_HOACH_VAN_CHUYEN_DATA> plDataCostElements,
           int year,
           string centerCode,
           int? version,
           bool? isHasValue = null,
           string templateId = "");
        IList<T_MD_KHOAN_MUC_VAN_CHUYEN> SummaryCenterOut(out IList<T_BP_KE_HOACH_VAN_CHUYEN_DATA> plDataCostElements, string centerCode, int year, int? version, bool? isHasValue = null);
        IList<T_MD_KHOAN_MUC_VAN_CHUYEN> SummaryCenterVersion(out IList<T_BP_KE_HOACH_VAN_CHUYEN_DATA> plDataCostElements, string centerCode, int year, int? version, bool isDrillDown = false);
        //public T_BP_KE_HOACH_VAN_CHUYEN CheckTemplate(string template, int year, string orgCode) { }
    }
}
