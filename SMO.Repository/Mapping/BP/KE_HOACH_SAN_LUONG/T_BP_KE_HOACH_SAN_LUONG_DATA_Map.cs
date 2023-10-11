using SMO.Core.Entities;
using SMO.Core.Entities.BP.KE_HOACH_SAN_LUONG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.BP.KE_HOACH_SAN_LUONG
{
    public class T_BP_KE_HOACH_SAN_LUONG_DATA_Map : BaseMapping<T_BP_KE_HOACH_SAN_LUONG_DATA>
    {
        public T_BP_KE_HOACH_SAN_LUONG_DATA_Map()
        {
            Table("T_BP_KE_HOACH_SAN_LUONG_DATA");
            Id(x => x.PKID);
            Map(x => x.ORG_CODE);
            Map(x => x.SAN_LUONG_PROFIT_CENTER_CODE);
            Map(x => x.TEMPLATE_CODE);
            Map(x => x.KHOAN_MUC_SAN_LUONG_CODE);
            Map(x => x.VERSION);
            Map(x => x.TIME_YEAR);
            Map(x => x.VALUE_JAN);
            Map(x => x.VALUE_FEB);
            Map(x => x.VALUE_MAR);
            Map(x => x.VALUE_APR);
            Map(x => x.VALUE_MAY);
            Map(x => x.VALUE_JUN);
            Map(x => x.VALUE_JUL);
            Map(x => x.VALUE_AUG);
            Map(x => x.VALUE_SEP);
            Map(x => x.VALUE_OCT);
            Map(x => x.VALUE_NOV);
            Map(x => x.VALUE_DEC);

            Map(x => x.VALUE_SUM_YEAR_PREVENTIVE);
            Map(x => x.VALUE_SUM_YEAR);

            Map(x => x.DESCRIPTION);
            Map(x => x.STATUS);

            References(x => x.KhoanMucSanLuong).Columns("KHOAN_MUC_SAN_LUONG_CODE", "TIME_YEAR")
                .Not.Insert().Not.Update();
            References(x => x.SanLuongProfitCenter, "SAN_LUONG_PROFIT_CENTER_CODE")
                .Not.Insert().Not.Update();
            References(x => x.Organize, "ORG_CODE")
                .Not.Insert().Not.Update();
            References(x => x.Template, "TEMPLATE_CODE")
                .Not.Insert().Not.Update();
        }
    }
}
