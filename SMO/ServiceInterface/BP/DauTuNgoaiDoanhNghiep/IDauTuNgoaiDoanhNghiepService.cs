using SMO.Core.Entities;
using SMO.Core.Entities.BP.DAU_TU_NGOAI_DOANH_NGHIEP;
using SMO.Core.Entities.MD;

using System.Collections.Generic;

namespace SMO.ServiceInterface.BP.DauTuNgoaiDoanhNghiep
{
    public interface IDauTuNgoaiDoanhNghiepService
    {
        IList<T_MD_KHOAN_MUC_DAU_TU> GetDataCostPreview(
            out IList<T_MD_TEMPLATE_DETAIL_DAU_TU_NGOAI_DOANH_NGHIEP> detailCostElements,
            string templateId,
            string centerCode = "",
            int? year = null,
            int? version = null,
            bool? isHasValue = null);
        IList<T_MD_KHOAN_MUC_DAU_TU> PreparePureList(IList<T_MD_TEMPLATE_DETAIL_DAU_TU_NGOAI_DOANH_NGHIEP> detailCostElements, int year);
        IList<T_MD_KHOAN_MUC_DAU_TU> PreparePureList(out IList<T_MD_TEMPLATE_DETAIL_DAU_TU_NGOAI_DOANH_NGHIEP> detailCostElements,
        string templateId,
        int year,
        string centerCode = "",
        bool ignoreAuth = false);

        IList<T_MD_KHOAN_MUC_DAU_TU> PreparePureListForTemplate(out IList<T_MD_TEMPLATE_DETAIL_DAU_TU_NGOAI_DOANH_NGHIEP> detailCostElements,
            string templateId,
            int year);
        IList<T_MD_KHOAN_MUC_DAU_TU> PreparePureList(out IList<T_MD_TEMPLATE_DETAIL_DAU_TU_NGOAI_DOANH_NGHIEP> detailCostElements,
            IList<string> centerCodes,
            int year);
        IList<T_MD_KHOAN_MUC_DAU_TU> SummarySumUpCenter(
           out IList<T_BP_DAU_TU_NGOAI_DOANH_NGHIEP_DATA> plDataCostElements,
           int year,
           string centerCode,
           int? version,
           bool? isHasValue = null,
           string templateId = "");
        IList<T_MD_KHOAN_MUC_DAU_TU> SummaryCenterOut(out IList<T_BP_DAU_TU_NGOAI_DOANH_NGHIEP_DATA> plDataCostElements, string centerCode, int year, int? version, bool? isHasValue = null);
        IList<T_MD_KHOAN_MUC_DAU_TU> SummaryCenterVersion(out IList<T_BP_DAU_TU_NGOAI_DOANH_NGHIEP_DATA> plDataCostElements, string centerCode, int year, int? version, bool isDrillDown = false);
        //public T_BP_DAU_TU_NGOAI_DOANH_NGHIEP CheckTemplate(string template, int year, string orgCode) { }
    }
}
