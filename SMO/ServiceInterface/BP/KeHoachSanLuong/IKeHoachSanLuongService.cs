using SMO.Core.Entities;
using SMO.Core.Entities.BP.KE_HOACH_SAN_LUONG;
using SMO.Core.Entities.MD;

using System.Collections.Generic;

namespace SMO.ServiceInterface.BP.KeHoachSanLuong
{
    public interface IKeHoachSanLuongService
    {
        IList<T_MD_KHOAN_MUC_SAN_LUONG> GetDataCostPreview(
            out IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_SAN_LUONG> detailCostElements,
            string templateId,
            string centerCode = "",
            int? year = null,
            int? version = null,
            bool? isHasValue = null);
        IList<T_MD_KHOAN_MUC_SAN_LUONG> PreparePureList(IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_SAN_LUONG> detailCostElements, int year);
        IList<T_MD_KHOAN_MUC_SAN_LUONG> PreparePureList(out IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_SAN_LUONG> detailCostElements,
        string templateId,
        int year,
        string centerCode = "",
        bool ignoreAuth = false);

        IList<T_MD_KHOAN_MUC_SAN_LUONG> PreparePureListForTemplate(out IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_SAN_LUONG> detailCostElements,
            string templateId,
            int year);
        IList<T_MD_KHOAN_MUC_SAN_LUONG> PreparePureList(out IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_SAN_LUONG> detailCostElements,
            IList<string> centerCodes,
            int year);
        IList<T_MD_KHOAN_MUC_SAN_LUONG> SummarySumUpCenter(
           out IList<T_BP_KE_HOACH_SAN_LUONG_DATA> plDataCostElements,
           int year,
           string centerCode,
           int? version,
           bool? isHasValue = null,
           string templateId = "");
        IList<T_MD_KHOAN_MUC_SAN_LUONG> SummaryCenterOut(out IList<T_BP_KE_HOACH_SAN_LUONG_DATA> plDataCostElements, string centerCode, int year, int? version, bool? isHasValue = null);
        IList<T_MD_KHOAN_MUC_SAN_LUONG> SummaryCenterVersion(out IList<T_BP_KE_HOACH_SAN_LUONG_DATA> plDataCostElements, string centerCode, int year, int? version, bool isDrillDown = false);
        //public T_BP_KE_HOACH_SAN_LUONG CheckTemplate(string template, int year, string orgCode) { }
    }
}
