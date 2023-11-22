using SMO.Core.Common;
using SMO.Core.Entities.BP.KE_HOACH_CHI_PHI;

namespace SMO.Core.Entities.MD
{
    public class T_MD_TEMPLATE_DETAIL_KE_HOACH_CHI_PHI : BaseTemplateDetail<T_MD_KHOAN_MUC_HANG_HOA, T_MD_CHI_PHI_PROFIT_CENTER>
    {
        public T_MD_TEMPLATE_DETAIL_KE_HOACH_CHI_PHI() : base()
        {

        }
        public T_MD_TEMPLATE_DETAIL_KE_HOACH_CHI_PHI(string pkid, string templateCode, string elementCode, string centerCode, int year) : base(pkid, templateCode, elementCode, centerCode, year)
        {
        }
        public virtual T_MD_TEMPLATE Template { get; set; }
        public virtual T_BP_KE_HOACH_CHI_PHI_DATA CFData { get; set; }
    }
}
