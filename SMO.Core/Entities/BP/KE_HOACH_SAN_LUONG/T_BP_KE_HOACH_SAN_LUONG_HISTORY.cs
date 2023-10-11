﻿using SMO.Core.Entities.BP;

namespace SMO.Core.Entities.BP.KE_HOACH_SAN_LUONG
{
    public partial class T_BP_KE_HOACH_SAN_LUONG_HISTORY : BaseBPHistoryEntity
    {
        public virtual string KICH_BAN { get; set; }
        private T_MD_KICH_BAN _KichBan;
        public virtual T_MD_KICH_BAN KichBan
        {
            get
            {
                if (_KichBan == null)
                {
                    _KichBan = new T_MD_KICH_BAN();
                }
                return _KichBan;
            }
            set
            {
                _KichBan = value;
            }
        }
    }
}
