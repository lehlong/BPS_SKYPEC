using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Core.Entities.MD
{
    public partial class T_MD_ROUTE : BaseEntity
    {
        [Required(ErrorMessage = "Trường này bắt buộc nhập", AllowEmptyStrings = false)]
        [MaxLength(length: 50, ErrorMessage = "Chỉ được phép nhập tối đa {1} kí tự")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Không được phép nhập dấu cách")]
        public virtual string CODE { get; set; }
        [Required(ErrorMessage = "Trường này bắt buộc nhập", AllowEmptyStrings = false)]
        public virtual string NAME { get; set; }
        public virtual string AREA_CODE { get; set; }
        public virtual string FIRST_POINT { get; set; }
        public virtual string FINAL_POINT { get; set; }
        public virtual decimal KM_CO_HANG { get; set; }
        public virtual decimal KM_KHONG_HANG { get; set; }
        public virtual decimal HE_SO_HAO_HUT { get; set; }
        public virtual string PARENT_LEVEL_ID { get; set; }
    }
}
