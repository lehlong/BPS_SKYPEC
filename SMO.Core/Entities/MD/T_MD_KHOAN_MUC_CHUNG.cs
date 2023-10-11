using SMO.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Core.Entities.MD
{
    public class T_MD_KHOAN_MUC_CHUNG : CoreCenter
    {
        private T_MD_KHOAN_MUC_CHUNG parent;

        public virtual bool IS_GROUP { get; set; }
        public virtual T_MD_KHOAN_MUC_CHUNG Parent
        {
            get => string.IsNullOrEmpty(PARENT_CODE) ? null : parent;
            set => parent = value;
        }
    }
}
