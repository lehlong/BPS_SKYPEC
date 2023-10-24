using SMO.Core.Common;
using SMO.Core.Entities.BP.DAU_TU_XAY_DUNG;

namespace SMO.Core.Entities.MD
{
    public class T_MD_TEMPLATE_DETAIL_DAU_TU_XAY_DUNG : BaseTemplateDetail<T_MD_KHOAN_MUC_DAU_TU, T_MD_DAU_TU_XAY_DUNG_PROFIT_CENTER>
    {
        public T_MD_TEMPLATE_DETAIL_DAU_TU_XAY_DUNG()
        {
        }

        public T_MD_TEMPLATE_DETAIL_DAU_TU_XAY_DUNG(string pkid, string templateCode, string elementCode, string centerCode, int year) : base(pkid, templateCode, elementCode, centerCode, year)
        {
        }
        public virtual T_MD_TEMPLATE Template { get; set; }
        public virtual T_BP_DAU_TU_XAY_DUNG_DATA PLData { get; set; }

    }
}
