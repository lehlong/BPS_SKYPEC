using SMO.Core.Common;
using SMO.Core.Entities.BP.KE_HOACH_SAN_LUONG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Core.Entities.MD
{
    public class T_MD_TEMPLATE_DETAIL_KE_HOACH_SAN_LUONG : BaseTemplateDetail<T_MD_KHOAN_MUC_SAN_LUONG, T_MD_SAN_LUONG_PROFIT_CENTER>
    {
        public T_MD_TEMPLATE_DETAIL_KE_HOACH_SAN_LUONG()
        {
        }

        public T_MD_TEMPLATE_DETAIL_KE_HOACH_SAN_LUONG(string pkid, string templateCode, string elementCode, string centerCode, int year) : base(pkid, templateCode, elementCode, centerCode, year)
        {
        }
        public virtual T_MD_TEMPLATE Template { get; set; }
        public virtual T_BP_KE_HOACH_SAN_LUONG_DATA PLData { get; set; }

    }
}
