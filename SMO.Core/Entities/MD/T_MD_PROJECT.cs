using SMO.Core.Entities.MD;
using System.ComponentModel.DataAnnotations;

namespace SMO.Core.Entities
{
    public partial class T_MD_PROJECT : BaseEntity
    {
        [Required(ErrorMessage = "Trường này bắt buộc nhập", AllowEmptyStrings = false)]
        [MaxLength(length: 50, ErrorMessage = "Chỉ được phép nhập tối đa {1} kí tự")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Không được phép nhập dấu cách")]
        public virtual string CODE { get; set; }
        [Required(ErrorMessage = "Trường này bắt buộc nhập", AllowEmptyStrings = false)]
        public virtual string NAME { get; set; }
        public virtual string LOAI_HINH { get; set; }
        public virtual bool CHUYEN_TIEP { get; set; }
        public virtual bool DAU_TU_MOI { get; set; }
        public virtual bool CHUAN_BI_DAU_TU { get; set; }
        public virtual bool THUC_HIEN_DAU_TU { get; set; }
        public virtual int YEAR { get; set; }
        public virtual string NGANH_NGHE { get; set; }
        public virtual string PHAN_LOAI { get; set; }
        public virtual string AREA_CODE { get; set; }
        public virtual string TYPE { get; set; }
        public virtual decimal CAPITAL { get; set; }

        public virtual bool IsCheck { get; set; } = false;

        private T_MD_AREA _Area;
        public virtual T_MD_AREA Area
        {
            get
            {
                if (_Area == null)
                {
                    _Area = new T_MD_AREA();
                }
                return _Area;
            }
            set
            {
                _Area = value;
            }
        }

        private T_MD_LOAI_HINH_DAU_TU _LoaiHinh;
        public virtual T_MD_LOAI_HINH_DAU_TU LoaiHinh
        {
            get
            {
                if (_LoaiHinh == null)
                {
                    _LoaiHinh = new T_MD_LOAI_HINH_DAU_TU();
                }
                return _LoaiHinh;
            }
            set
            {
                _LoaiHinh = value;
            }
        }

        private T_MD_NGANH_NGHE_DAU_TU _NganhNghe;
        public virtual T_MD_NGANH_NGHE_DAU_TU NganhNghe
        {
            get
            {
                if (_NganhNghe == null)
                {
                    _NganhNghe = new T_MD_NGANH_NGHE_DAU_TU();
                }
                return _NganhNghe;
            }
            set
            {
                _NganhNghe = value;
            }
        }

        private T_MD_PHAN_LOAI_DAU_TU _PhanLoai;
        public virtual T_MD_PHAN_LOAI_DAU_TU PhanLoai
        {
            get
            {
                if (_PhanLoai == null)
                {
                    _PhanLoai = new T_MD_PHAN_LOAI_DAU_TU();
                }
                return _PhanLoai;
            }
            set
            {
                _PhanLoai = value;
            }
        }

        private T_MD_GIAI_DOAN_DAU_TU _GiaiDoan;
        public virtual T_MD_GIAI_DOAN_DAU_TU GiaiDoan
        {
            get
            {
                if (_GiaiDoan == null)
                {
                    _GiaiDoan = new T_MD_GIAI_DOAN_DAU_TU();
                }
                return _GiaiDoan;
            }
            set
            {
                _GiaiDoan = value;
            }
        }
    }
}
