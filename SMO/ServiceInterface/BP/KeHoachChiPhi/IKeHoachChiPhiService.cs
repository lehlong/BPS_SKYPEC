using SMO.Core.Entities;
using SMO.Core.Entities.BP.KE_HOACH_CHI_PHI;
using SMO.Core.Entities.MD;

using System.Collections.Generic;

namespace SMO.ServiceInterface.BP.KeHoachChiPhi
{
    public interface IKeHoachChiPhiService
    {
        IList<T_MD_KHOAN_MUC_CHI_PHI> GetDataCostPreview(
            out IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_CHI_PHI> detailCostElements,
            string templateId,
            string centerCode = "",
            int? year = null,
            int? version = null,
            bool? isHasValue = null);
        IList<T_MD_KHOAN_MUC_CHI_PHI> PreparePureList(IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_CHI_PHI> detailCostElements, int year);
        IList<T_MD_KHOAN_MUC_CHI_PHI> PreparePureList(out IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_CHI_PHI> detailCostElements,
        string templateId,
        int year,
        string centerCode = "",
        bool ignoreAuth = false);

        IList<T_MD_KHOAN_MUC_CHI_PHI> PreparePureListForTemplate(out IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_CHI_PHI> detailCostElements,
            string templateId,
            int year);
        IList<T_MD_KHOAN_MUC_CHI_PHI> PreparePureList(out IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_CHI_PHI> detailCostElements,
            IList<string> centerCodes,
            int year);
        IList<T_MD_KHOAN_MUC_CHI_PHI> SummarySumUpCenter(
           out IList<T_BP_KE_HOACH_CHI_PHI_DATA> plDataCostElements,
           int year,
           string centerCode,
           int? version,
           bool? isHasValue = null,
           string templateId = "");
        IList<T_MD_KHOAN_MUC_CHI_PHI> SummaryCenterOut(out IList<T_BP_KE_HOACH_CHI_PHI_DATA> plDataCostElements, string centerCode, int year, int? version, bool? isHasValue = null);
        IList<T_MD_KHOAN_MUC_CHI_PHI> SummaryCenterVersion(out IList<T_BP_KE_HOACH_CHI_PHI_DATA> plDataCostElements, string centerCode, int year, int? version, bool isDrillDown = false);
        //public T_BP_KE_HOACH_CHI_PHI CheckTemplate(string template, int year, string orgCode) { }
    }
}
