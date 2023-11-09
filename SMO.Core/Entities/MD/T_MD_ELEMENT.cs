using System.ComponentModel.DataAnnotations;

namespace SMO.Core.Entities
{
    public partial class T_MD_ELEMENT : BaseEntity
    {
        [Required(ErrorMessage = "Trường này bắt buộc nhập", AllowEmptyStrings = false)]
        [MaxLength(length: 50, ErrorMessage = "Chỉ được phép nhập tối đa {1} kí tự")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Không được phép nhập dấu cách")]
        public virtual string CODE { get; set; }
        [Required(ErrorMessage = "Trường này bắt buộc nhập", AllowEmptyStrings = false)]
        public virtual string NAME { get; set; }
        public virtual string ELEMENT_TYPE { get; set; }
        public virtual int PRIORITY { get; set; }
        public virtual string STATUS { get; set; }
        public virtual string FORMULA { get; set; }
        public virtual string UNIT_CODE { get; set; }
        public virtual string QUERY { get; set; }
        public virtual string SCREEN { get; set; }
        public virtual decimal? VALUE { get; set; }
    }
}
