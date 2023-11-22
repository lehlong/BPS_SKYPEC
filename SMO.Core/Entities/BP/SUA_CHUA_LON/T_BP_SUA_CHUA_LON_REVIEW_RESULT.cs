using SMO.Core.Entities.MD;

namespace SMO.Core.Entities.BP.SUA_CHUA_LON
{
    public class T_BP_SUA_CHUA_LON_REVIEW_RESULT : BaseEntity
    {
        public virtual string PKID { get; set; }
        public virtual string HEADER_ID { get; set; }
        public virtual string KHOAN_MUC_SUA_CHUA_CODE { get; set; }
        public virtual bool? RESULT { get; set; }
        public virtual int TIME_YEAR { get; set; }

        public virtual T_MD_KHOAN_MUC_SUA_CHUA KhoanMucSuaChua { get; set; }
        public virtual T_BP_SUA_CHUA_LON_REVIEW Header { get; set; }
    }
}
