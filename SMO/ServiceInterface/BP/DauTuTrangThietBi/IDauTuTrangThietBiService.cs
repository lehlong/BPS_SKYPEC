using SMO.Core.Entities;
using SMO.Core.Entities.BP.DAU_TU_TRANG_THIET_BI;
using SMO.Core.Entities.MD;

using System.Collections.Generic;

namespace SMO.ServiceInterface.BP.DauTuTrangThietBi
{
    public interface IDauTuTrangThietBiService
    {
        IList<T_MD_KHOAN_MUC_DAU_TU> GetDataCostPreview(
            out IList<T_MD_TEMPLATE_DETAIL_DAU_TU_TRANG_THIET_BI> detailCostElements,
            string templateId,
            string centerCode = "",
            int? year = null,
            int? version = null,
            bool? isHasValue = null);
        IList<T_MD_KHOAN_MUC_DAU_TU> PreparePureList(IList<T_MD_TEMPLATE_DETAIL_DAU_TU_TRANG_THIET_BI> detailCostElements, int year);
        IList<T_MD_KHOAN_MUC_DAU_TU> PreparePureList(out IList<T_MD_TEMPLATE_DETAIL_DAU_TU_TRANG_THIET_BI> detailCostElements,
        string templateId,
        int year,
        string centerCode = "",
        bool ignoreAuth = false);

        IList<T_MD_KHOAN_MUC_DAU_TU> PreparePureListForTemplate(out IList<T_MD_TEMPLATE_DETAIL_DAU_TU_TRANG_THIET_BI> detailCostElements,
            string templateId,
            int year);
        IList<T_MD_KHOAN_MUC_DAU_TU> PreparePureList(out IList<T_MD_TEMPLATE_DETAIL_DAU_TU_TRANG_THIET_BI> detailCostElements,
            IList<string> centerCodes,
            int year);
        IList<T_MD_KHOAN_MUC_DAU_TU> SummarySumUpCenter(
           out IList<T_BP_DAU_TU_TRANG_THIET_BI_DATA> plDataCostElements,
           int year,
           string centerCode,
           int? version,
           bool? isHasValue = null,
           string templateId = "");
        IList<T_MD_KHOAN_MUC_DAU_TU> SummaryCenterOut(out IList<T_BP_DAU_TU_TRANG_THIET_BI_DATA> plDataCostElements, string centerCode, int year, int? version, bool? isHasValue = null);
        IList<T_MD_KHOAN_MUC_DAU_TU> SummaryCenterVersion(out IList<T_BP_DAU_TU_TRANG_THIET_BI_DATA> plDataCostElements, string centerCode, int year, int? version, bool isDrillDown = false);
        //public T_BP_DAU_TU_TRANG_THIET_BI CheckTemplate(string template, int year, string orgCode) { }
    }
}
