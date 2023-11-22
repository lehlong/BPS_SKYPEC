using SMO.Core.Common;
using SMO.Service.Class.Base;
using SMO.Service.Class.KE_HOACH_VAN_CHUYEN;


using System.Collections.Generic;

namespace SMO.Service.Class.KE_HOACH_VAN_CHUYEN
{
    public class KeHoachVanChuyenReviewCenterViewModel : ReviewCenterViewModelBase<IKeHoachVanChuyenElementReviewCenter>, IReviewCenterViewModelBase<IKeHoachVanChuyenElementReviewCenter>
    {
    }
    public class KeHoachVanChuyenElementReviewCenter : ElementReviewCenterBase, IKeHoachVanChuyenElementReviewCenter
    {
        public KeHoachVanChuyenElementReviewCenter(CoreElement element, IList<string> success, IList<string> failure, IList<string> notReviewed, bool? status, int comments, int commentsInOrg) : base(element, success, failure, notReviewed, status, comments, commentsInOrg)
        {
        }
    }
}
