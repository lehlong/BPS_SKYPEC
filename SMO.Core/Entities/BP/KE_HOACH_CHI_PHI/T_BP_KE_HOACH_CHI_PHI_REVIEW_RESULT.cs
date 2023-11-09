using SMO.Core.Entities.MD;

namespace SMO.Core.Entities.BP.KE_HOACH_CHI_PHI
{
    public class T_BP_KE_HOACH_CHI_PHI_REVIEW_RESULT : BaseEntity
    {
        public virtual string PKID { get; set; }
        public virtual string HEADER_ID { get; set; }
        public virtual string ELEMENT_CODE { get; set; }
        public virtual bool? RESULT { get; set; }
        public virtual int TIME_YEAR { get; set; }

        public virtual T_MD_KHOAN_MUC_SAN_LUONG Element { get; set; }
        public virtual T_BP_KE_HOACH_CHI_PHI_REVIEW Header { get; set; }
    }
}
