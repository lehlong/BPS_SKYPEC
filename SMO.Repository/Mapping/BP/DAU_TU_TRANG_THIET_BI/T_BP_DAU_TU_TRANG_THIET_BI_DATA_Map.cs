using SMO.Core.Entities;
using SMO.Core.Entities.BP.DAU_TU_TRANG_THIET_BI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.BP.DAU_TU_TRANG_THIET_BI
{
    public class T_BP_DAU_TU_TRANG_THIET_BI_DATA_Map : BaseMapping<T_BP_DAU_TU_TRANG_THIET_BI_DATA>
    {
        public T_BP_DAU_TU_TRANG_THIET_BI_DATA_Map()
        {
            Table("T_BP_DAU_TU_TRANG_THIET_BI_DATA");
            Id(x => x.PKID);
            Map(x => x.ORG_CODE);
            Map(x => x.DAU_TU_PROFIT_CENTER_CODE);
            Map(x => x.TEMPLATE_CODE);
            Map(x => x.KHOAN_MUC_DAU_TU_CODE);
            Map(x => x.VERSION);
            Map(x => x.TIME_YEAR);
            Map(x => x.MONTH1);
            Map(x => x.MONTH2);
            Map(x => x.MONTH3);
            Map(x => x.MONTH4);
            Map(x => x.MONTH5);
            Map(x => x.MONTH6);
            Map(x => x.MONTH7);
            Map(x => x.MONTH8);
            Map(x => x.MONTH9);
            Map(x => x.MONTH10);
            Map(x => x.MONTH11);
            Map(x => x.MONTH12);
            Map(x => x.SumMonth);
            Map(x => x.VALUE);
            Map(x => x.TDTK);
            Map(x => x.DESCRIPTION);
            Map(x => x.PROCESS);
            Map(x => x.EQUITY_SOURCES);
            Map(x => x.STATUS);
            Map(x => x.QKH);
            References(x => x.KhoanMucDauTu).Columns("KHOAN_MUC_DAU_TU_CODE", "TIME_YEAR")
                .Not.Insert().Not.Update();
            References(x => x.DauTuTrangThietBiProfitCenter, "DAU_TU_PROFIT_CENTER_CODE")
                .Not.Insert().Not.Update();
            References(x => x.Organize, "ORG_CODE")
                .Not.Insert().Not.Update();
            References(x => x.Template, "TEMPLATE_CODE")
                .Not.Insert().Not.Update();
        }
    }
}
