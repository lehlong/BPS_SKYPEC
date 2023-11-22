using SMO.Core.Entities.MD;

using System.Collections.Generic;

namespace SMO.Service.Class
{
    public class KeHoachChiPhiReviewViewModel : BaseReviewViewModel
    {
        public KeHoachChiPhiReviewViewModel()
        {
            Elements = new List<KeHoachChiPhiElementReview>();
        }
        public IList<KeHoachChiPhiElementReview> Elements { get; set; }
    }

    public class KeHoachChiPhiElementReview : T_MD_KHOAN_MUC_HANG_HOA
    {
        public KeHoachChiPhiElementReview()
        {

        }
        public KeHoachChiPhiElementReview(T_MD_KHOAN_MUC_HANG_HOA element, bool? status, int comments, int commentsInOrg)
        {
            Status = status;
            CENTER_CODE = CENTER_CODE;
            CODE = element.CODE;
            Values = element.Values;
            IS_GROUP = element.IS_GROUP;
            DESCRIPTION = element.DESCRIPTION;
            NAME = element.NAME;
            LEVEL = element.LEVEL;
            IsChildren = element.IsChildren;
            Comments = comments;
            CommentsInOrg = commentsInOrg;
        }
        public int Comments { get; private set; }
        public int CommentsInOrg { get; private set; }
        public bool? Status { get; set; }
    }
}