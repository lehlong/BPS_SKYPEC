using SMO.Core.Common;

namespace SMO.Core.Entities.MD
{
    public class T_MD_TEMPLATE_DETAIL_KE_HOACH_CHI_PHI : BaseTemplateDetail<T_MD_KHOAN_MUC_CHI_PHI, T_MD_COST_CENTER>
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
