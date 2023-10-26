using SMO.Core.Common;
using SMO.Core.Entities.BP.DAU_TU_NGOAI_DOANH_NGHIEP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Core.Entities.MD
{
    public class T_MD_TEMPLATE_DETAIL_DAU_TU_NGOAI_DOANH_NGHIEP : BaseTemplateDetail<T_MD_KHOAN_MUC_DAU_TU, T_MD_DAU_TU_NGOAI_DOANH_NGHIEP_PROFIT_CENTER>
    {
        public T_MD_TEMPLATE_DETAIL_DAU_TU_NGOAI_DOANH_NGHIEP()
        {
        }

        public T_MD_TEMPLATE_DETAIL_DAU_TU_NGOAI_DOANH_NGHIEP(string pkid, string templateCode, string elementCode, string centerCode, int year) : base(pkid, templateCode, elementCode, centerCode, year)
        {
        }
        public virtual T_MD_TEMPLATE Template { get; set; }
        public virtual T_BP_DAU_TU_NGOAI_DOANH_NGHIEP_DATA PLData { get; set; }

    }
}
