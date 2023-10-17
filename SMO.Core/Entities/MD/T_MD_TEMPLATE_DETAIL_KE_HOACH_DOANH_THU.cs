using SMO.Core.Common;
using SMO.Core.Entities.BP.KE_HOACH_DOANH_THU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Core.Entities.MD
{
    public class T_MD_TEMPLATE_DETAIL_KE_HOACH_DOANH_THU : BaseTemplateDetail<T_MD_KHOAN_MUC_DOANH_THU, T_MD_DOANH_THU_PROFIT_CENTER>
    {
        public T_MD_TEMPLATE_DETAIL_KE_HOACH_DOANH_THU()
        {
        }

        public T_MD_TEMPLATE_DETAIL_KE_HOACH_DOANH_THU(string pkid, string templateCode, string elementCode, string centerCode, int year) : base(pkid, templateCode, elementCode, centerCode, year)
        {
        }
        public virtual T_MD_TEMPLATE Template { get; set; }
        public virtual T_BP_KE_HOACH_DOANH_THU_DATA PLData { get; set; }

    }
}
