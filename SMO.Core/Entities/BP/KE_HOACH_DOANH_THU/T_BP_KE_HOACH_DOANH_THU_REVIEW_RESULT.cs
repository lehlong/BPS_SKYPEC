using SMO.Core.Entities.MD;

namespace SMO.Core.Entities.BP.KE_HOACH_DOANH_THU
{
    public class T_BP_KE_HOACH_DOANH_THU_REVIEW_RESULT : BaseEntity
    {
        public virtual string PKID { get; set; }
        public virtual string HEADER_ID { get; set; }
        public virtual string KHOAN_MUC_DOANH_THU_CODE { get; set; }
        public virtual bool? RESULT { get; set; }
        public virtual int TIME_YEAR { get; set; }

        public virtual T_MD_KHOAN_MUC_DOANH_THU KhoanMucDoanhThu { get; set; }
        public virtual T_BP_KE_HOACH_DOANH_THU_REVIEW Header { get; set; }
    }
}
