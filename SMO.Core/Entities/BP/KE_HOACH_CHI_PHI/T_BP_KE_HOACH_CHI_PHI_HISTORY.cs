using SMO.Core.Entities.BP;

namespace SMO.Core.Entities.BP.KE_HOACH_CHI_PHI
{
    public partial class T_BP_KE_HOACH_CHI_PHI_HISTORY : BaseBPHistoryEntity
    {
        public virtual string KICH_BAN { get; set; }
        public virtual string PHIEN_BAN { get; set; }
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
        private T_MD_PHIEN_BAN _PhienBan;
        public virtual T_MD_PHIEN_BAN PhienBan
        {
            get
            {
                if (_PhienBan == null)
                {
                    _PhienBan = new T_MD_PHIEN_BAN();
                }
                return _PhienBan;
            }
            set
            {
                _PhienBan = value;
            }
        }
    }
}
