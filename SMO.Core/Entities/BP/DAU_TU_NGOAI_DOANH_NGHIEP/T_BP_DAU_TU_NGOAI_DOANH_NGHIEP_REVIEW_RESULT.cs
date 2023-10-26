using SMO.Core.Entities.MD;

namespace SMO.Core.Entities.BP.DAU_TU_NGOAI_DOANH_NGHIEP
{
    public class T_BP_DAU_TU_NGOAI_DOANH_NGHIEP_REVIEW_RESULT : BaseEntity
    {
        public virtual string PKID { get; set; }
        public virtual string HEADER_ID { get; set; }
        public virtual string KHOAN_MUC_DAU_TU_CODE { get; set; }
        public virtual bool? RESULT { get; set; }
        public virtual int TIME_YEAR { get; set; }

        public virtual T_MD_KHOAN_MUC_DAU_TU KhoanMucDauTu { get; set; }
        public virtual T_BP_DAU_TU_NGOAI_DOANH_NGHIEP_REVIEW Header { get; set; }
    }
}
