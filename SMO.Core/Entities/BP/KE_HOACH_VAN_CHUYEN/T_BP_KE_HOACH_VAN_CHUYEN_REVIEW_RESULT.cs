using SMO.Core.Entities.MD;

namespace SMO.Core.Entities.BP.KE_HOACH_VAN_CHUYEN
{
    public class T_BP_KE_HOACH_VAN_CHUYEN_REVIEW_RESULT : BaseEntity
    {
        public virtual string PKID { get; set; }
        public virtual string HEADER_ID { get; set; }
        public virtual string KHOAN_MUC_VAN_CHUYEN_CODE { get; set; }
        public virtual bool? RESULT { get; set; }
        public virtual int TIME_YEAR { get; set; }

        public virtual T_MD_KHOAN_MUC_VAN_CHUYEN KhoanMucVanChuyen { get; set; }
        public virtual T_BP_KE_HOACH_VAN_CHUYEN_REVIEW Header { get; set; }
    }
}
