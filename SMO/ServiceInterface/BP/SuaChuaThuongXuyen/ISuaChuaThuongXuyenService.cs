using SMO.Core.Entities;
using SMO.Core.Entities.BP.SUA_CHUA_THUONG_XUYEN;
using SMO.Core.Entities.MD;

using System.Collections.Generic;

namespace SMO.ServiceInterface.BP.SuaChuaThuongXuyen
{
    public interface ISuaChuaThuongXuyenService
    {
        IList<T_MD_KHOAN_MUC_SUA_CHUA> GetDataCostPreview(
            out IList<T_MD_TEMPLATE_DETAIL_SUA_CHUA_THUONG_XUYEN> detailCostElements,
            string templateId,
            string centerCode = "",
            int? year = null,
            int? version = null,
            bool? isHasValue = null);
        IList<T_MD_KHOAN_MUC_SUA_CHUA> PreparePureList(IList<T_MD_TEMPLATE_DETAIL_SUA_CHUA_THUONG_XUYEN> detailCostElements, int year);
        IList<T_MD_KHOAN_MUC_SUA_CHUA> PreparePureList(out IList<T_MD_TEMPLATE_DETAIL_SUA_CHUA_THUONG_XUYEN> detailCostElements,
        string templateId,
        int year,
        string centerCode = "",
        bool ignoreAuth = false);

        IList<T_MD_KHOAN_MUC_SUA_CHUA> PreparePureListForTemplate(out IList<T_MD_TEMPLATE_DETAIL_SUA_CHUA_THUONG_XUYEN> detailCostElements,
            string templateId,
            int year);
        IList<T_MD_KHOAN_MUC_SUA_CHUA> PreparePureList(out IList<T_MD_TEMPLATE_DETAIL_SUA_CHUA_THUONG_XUYEN> detailCostElements,
            IList<string> centerCodes,
            int year);
        IList<T_MD_KHOAN_MUC_SUA_CHUA> SummarySumUpCenter(
           out IList<T_BP_SUA_CHUA_THUONG_XUYEN_DATA> plDataCostElements,
           int year,
           string centerCode,
           int? version,
           bool? isHasValue = null,
           string templateId = "");
        IList<T_MD_KHOAN_MUC_SUA_CHUA> SummaryCenterOut(out IList<T_BP_SUA_CHUA_THUONG_XUYEN_DATA> plDataCostElements, string centerCode, int year, int? version, bool? isHasValue = null);
        IList<T_MD_KHOAN_MUC_SUA_CHUA> SummaryCenterVersion(out IList<T_BP_SUA_CHUA_THUONG_XUYEN_DATA> plDataCostElements, string centerCode, int year, int? version, bool isDrillDown = false);
        //public T_BP_SUA_CHUA_THUONG_XUYEN CheckTemplate(string template, int year, string orgCode) { }
    }
}
