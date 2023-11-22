using SMO.Core.Entities.MD;

using System.Collections.Generic;

namespace SMO.Service.Class
{
    public class KeHoachVanChuyenReviewViewModel : BaseReviewViewModel
    {
        public KeHoachVanChuyenReviewViewModel()
        {
            Elements = new List<KeHoachVanChuyenElementReview>();
        }
        public IList<KeHoachVanChuyenElementReview> Elements { get; set; }
    }

    public class KeHoachVanChuyenElementReview : T_MD_KHOAN_MUC_VAN_CHUYEN
    {
        public KeHoachVanChuyenElementReview()
        {

        }
        public KeHoachVanChuyenElementReview(T_MD_KHOAN_MUC_VAN_CHUYEN element, bool? status, int comments, int commentsInOrg)
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