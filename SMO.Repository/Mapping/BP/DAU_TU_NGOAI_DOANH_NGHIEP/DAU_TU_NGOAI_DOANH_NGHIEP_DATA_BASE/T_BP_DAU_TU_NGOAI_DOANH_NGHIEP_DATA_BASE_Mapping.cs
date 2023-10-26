using SMO.Core.Entities.BP.DAU_TU_NGOAI_DOANH_NGHIEP.DAU_TU_NGOAI_DOANH_NGHIEP_DATA_BASE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.BP.DAU_TU_NGOAI_DOANH_NGHIEP.DAU_TU_NGOAI_DOANH_NGHIEP_DATA_BASE
{
    public class T_BP_DAU_TU_NGOAI_DOANH_NGHIEP_DATA_BASE_Mapping : BaseMapping<T_BP_DAU_TU_NGOAI_DOANH_NGHIEP_DATA_BASE>
    {
        public T_BP_DAU_TU_NGOAI_DOANH_NGHIEP_DATA_BASE_Mapping()
        {
            Id(x => x.PKID);

            Map(x => x.ORG_CODE);
            Map(x => x.DAU_TU_PROFIT_CENTER_CODE);
            Map(x => x.TEMPLATE_CODE);
            Map(x => x.KHOAN_MUC_DAU_TU_CODE);
            Map(x => x.VERSION);
            Map(x => x.TIME_YEAR);
            Map(x => x.MATERIAL);
            Map(x => x.UNIT);

            Map(x => x.QUANTITY_1);
            Map(x => x.PRICE_1);
            Map(x => x.AMOUNT_1);
            Map(x => x.TIME_1);

            Map(x => x.QUANTITY_2);
            Map(x => x.PRICE_2);
            Map(x => x.AMOUNT_2);
            Map(x => x.TIME_2);

            Map(x => x.QUANTITY_3);
            Map(x => x.PRICE_3);
            Map(x => x.AMOUNT_3);
            Map(x => x.TIME_3);

            Map(x => x.QUANTITY_4);
            Map(x => x.PRICE_4);
            Map(x => x.AMOUNT_4);
            Map(x => x.TIME_4);

            Map(x => x.QUANTITY_5);
            Map(x => x.PRICE_5);
            Map(x => x.AMOUNT_5);
            Map(x => x.TIME_5);

            Map(x => x.QUANTITY_6);
            Map(x => x.PRICE_6);
            Map(x => x.AMOUNT_6);
            Map(x => x.TIME_6);

            Map(x => x.DESCRIPTION);

            References(x => x.KhoanMucDauTu).Columns("KHOAN_MUC_DAU_TU_CODE", "TIME_YEAR")
                .Not.Insert().Not.Update();
            References(x => x.DauTuNgoaiDoanhNghiepProfitCenter, "DAU_TU_PROFIT_CENTER_CODE")
                .Not.Insert().Not.Update();
            References(x => x.Organize, "ORG_CODE")
                .Not.Insert().Not.Update();
            References(x => x.Template, "TEMPLATE_CODE")
                .Not.Insert().Not.Update();
        }
    }
}
