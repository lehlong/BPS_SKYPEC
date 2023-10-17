using SMO.Core.Entities.MD;
using System;
using System.ComponentModel.DataAnnotations;

namespace SMO.Core.Entities
{
    public partial class T_MD_UNIT_PRICE : BaseEntity
    {
        public virtual Guid ID { get; set; }
        public virtual string SAN_BAY_CODE { get; set; }
        public virtual int YEAR { get; set; }
        public virtual int VN { get; set; }
        public virtual int OV { get; set; }
        public virtual int BL { get; set; }
        public virtual int VJ { get; set; }
        public virtual int QH { get; set; }
        public virtual int VU { get; set; }
        public virtual int OTHER_HKTN { get; set; }
        public virtual int HKNN { get; set; }

        private T_MD_SAN_BAY _SanBay;
        public virtual T_MD_SAN_BAY SanBay
        {
            get
            {
                if (_SanBay == null)
                {
                    _SanBay = new T_MD_SAN_BAY();
                }
                return _SanBay;
            }
            set
            {
                _SanBay = value;
            }
        }

    }
}
