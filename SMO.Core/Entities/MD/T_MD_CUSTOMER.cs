using System.ComponentModel.DataAnnotations;

namespace SMO.Core.Entities
{
    public partial class T_MD_CUSTOMER : BaseEntity
    {
        [Required(ErrorMessage = "Trường này bắt buộc nhập", AllowEmptyStrings = false)]
        [MaxLength(length: 50, ErrorMessage = "Chỉ được phép nhập tối đa {1} kí tự")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Không được phép nhập dấu cách")]
        public virtual string CODE { get; set; }
        [Required(ErrorMessage = "Trường này bắt buộc nhập", AllowEmptyStrings = false)]
        public virtual string TEXT { get; set; }
        public virtual string OLD_CODE { get; set; }
        public virtual string ADDRESS { get; set; }
        public virtual string MST { get; set; }
        public virtual string PHONE { get; set; }
        public virtual string BANK { get; set; }
        public virtual string OTHER_INFO { get; set; }
    }
}
