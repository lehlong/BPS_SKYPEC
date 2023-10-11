using SMO.Core.Common;
using SMO.Service.Class.Base;
using SMO.Service.Class.KE_HOACH_SAN_LUONG;
using SMO.Service.Class.OTHER_COST_CF;

using System.Collections.Generic;

namespace SMO.Service.Class.KE_HOACH_SAN_LUONG
{
    public class KeHoachSanLuongReviewCenterViewModel : ReviewCenterViewModelBase<IKeHoachSanLuongElementReviewCenter>, IReviewCenterViewModelBase<IKeHoachSanLuongElementReviewCenter>
    {
    }
    public class KeHoachSanLuongElementReviewCenter : ElementReviewCenterBase, IKeHoachSanLuongElementReviewCenter
    {
        public KeHoachSanLuongElementReviewCenter(CoreElement element, IList<string> success, IList<string> failure, IList<string> notReviewed, bool? status, int comments, int commentsInOrg) : base(element, success, failure, notReviewed, status, comments, commentsInOrg)
        {
        }
    }
}
