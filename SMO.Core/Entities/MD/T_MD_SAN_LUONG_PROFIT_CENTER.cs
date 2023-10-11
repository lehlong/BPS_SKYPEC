using SMO.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Core.Entities.MD
{
    public class T_MD_SAN_LUONG_PROFIT_CENTER : CoreCenter
    {
        public virtual string HANG_HANG_KHONG_CODE { get; set; }
        public virtual string SAN_BAY_CODE { get; set; }
        public virtual T_MD_HANG_HANG_KHONG HangHangKhong { get; set; }
        public virtual T_MD_SAN_BAY SanBay { get; set; }
    }
}
