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
    }
}
