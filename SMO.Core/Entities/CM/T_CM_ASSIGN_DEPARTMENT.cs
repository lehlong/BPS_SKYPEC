using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Core.Entities.CM
{
    public class T_CM_ASSIGN_DEPARTMENT : BaseEntity
    {
        public virtual string PKID { get; set; }
        /// <summary>
        /// Code của center
        /// </summary>
        public virtual string ORG_CODE { get; set; }
        /// <summary>
        /// Tùy thuộc vào comment mà reference code chứa code của element code hoặc code của template code
        /// </summary>
        public virtual string REFERENCE_CODE { get; set; }
        public virtual int YEAR { get; set; }
        public virtual int VERSION { get; set; }
        public virtual string ELEMENT_CODE { get; set; }
        /// <summary>
        /// Lấy dữ liệu từ SMO.AppCode.Class.TemplateObjectType
        /// </summary>
        public virtual string OBJECT_TYPE { get; set; }
        /// <summary>
        /// Lấy dữ liệu từ SMO.BudgetType class
        /// </summary>
        public virtual string BUDGET_TYPE { get; set; }
        /// <summary>
        /// Lấy dữ liệu từ SMO.ElementType
        /// </summary>
        public virtual string ELEMENT_TYPE { get; set; }

        public virtual string DEPARTMENT { get; set; }
    }
}
