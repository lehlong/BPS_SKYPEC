using SMO.Core.Entities.BP;

using System.Diagnostics;

namespace SMO.Core.Entities.BP.DAU_TU_XAY_DUNG
{
    [DebuggerDisplay("template = {TEMPLATE_CODE}")]
    public partial class T_BP_DAU_TU_XAY_DUNG : T_BP_BASE
    {
        public virtual string KICH_BAN { get; set; }
        public virtual string PHIEN_BAN { get; set; }
        public virtual string TYPE_UPLOAD { get; set; }
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
