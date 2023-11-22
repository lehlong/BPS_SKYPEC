using SMO.Core.Common;
using SMO.Core.Entities.BP.KE_HOACH_VAN_CHUYEN;

namespace SMO.Core.Entities.MD
{
    public class T_MD_TEMPLATE_DETAIL_KE_HOACH_VAN_CHUYEN : BaseTemplateDetail<T_MD_KHOAN_MUC_VAN_CHUYEN, T_MD_VAN_CHUYEN_PROFIT_CENTER>
    {
        public T_MD_TEMPLATE_DETAIL_KE_HOACH_VAN_CHUYEN()
        {
        }

        public T_MD_TEMPLATE_DETAIL_KE_HOACH_VAN_CHUYEN(string pkid, string templateCode, string elementCode, string centerCode, int year) : base(pkid, templateCode, elementCode, centerCode, year)
        {
        }
        public virtual T_MD_TEMPLATE Template { get; set; }
        public virtual T_BP_KE_HOACH_VAN_CHUYEN_DATA PLData { get; set; }

    }
}
