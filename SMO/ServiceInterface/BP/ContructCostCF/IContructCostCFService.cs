﻿using SMO.Core.Entities;
using SMO.Core.Entities.MD;

using System.Collections.Generic;

namespace SMO.ServiceInterface.BP.ContructCostCF
{
    public interface IContructCostCFService
    {
        IList<T_MD_COST_CF_ELEMENT> GetDataCostPreview(
            out IList<T_MD_TEMPLATE_DETAIL_CONTRUCT_COST_CF> detailCostElements,
            string templateId,
            string centerCode = "",
            int? year = null,
            int? version = null,
            bool? isHasValue = null);
        IList<T_MD_COST_CF_ELEMENT> PreparePureList(IList<T_MD_TEMPLATE_DETAIL_CONTRUCT_COST_CF> detailCostElements, int year);
        IList<T_MD_COST_CF_ELEMENT> PreparePureList(out IList<T_MD_TEMPLATE_DETAIL_CONTRUCT_COST_CF> detailCostElements,
        string templateId,
        int year,
        string centerCode = "",
        bool ignoreAuth = false);

        IList<T_MD_COST_CF_ELEMENT> PreparePureListForTemplate(out IList<T_MD_TEMPLATE_DETAIL_CONTRUCT_COST_CF> detailCostElements,
            string templateId,
            int year);
        IList<T_MD_COST_CF_ELEMENT> PreparePureList(out IList<T_MD_TEMPLATE_DETAIL_CONTRUCT_COST_CF> detailCostElements,
            IList<string> centerCodes,
            int year);
        IList<T_MD_COST_CF_ELEMENT> SummarySumUpCenter(
           out IList<T_BP_CONTRUCT_COST_CF_DATA> plDataCostElements,
           int year,
           string centerCode,
           int? version,
           bool? isHasValue = null,
           string templateId = "");
        IList<T_MD_COST_CF_ELEMENT> SummaryCenterOut(out IList<T_BP_CONTRUCT_COST_CF_DATA> plDataCostElements, string centerCode, int year, int? version, bool? isHasValue = null);
        IList<T_MD_COST_CF_ELEMENT> SummaryCenterVersion(out IList<T_BP_CONTRUCT_COST_CF_DATA> plDataCostElements, string centerCode, int year, int? version, bool isDrillDown = false);
    }
}
