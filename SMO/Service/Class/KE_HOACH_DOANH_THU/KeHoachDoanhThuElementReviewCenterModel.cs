using SMO.Core.Common;
using SMO.Service.Class.Base;
using SMO.Service.Class.KE_HOACH_DOANH_THU;


using System.Collections.Generic;

namespace SMO.Service.Class.KE_HOACH_DOANH_THU
{
    public class KeHoachDoanhThuReviewCenterViewModel : ReviewCenterViewModelBase<IKeHoachDoanhThuElementReviewCenter>, IReviewCenterViewModelBase<IKeHoachDoanhThuElementReviewCenter>
    {
    }
    public class KeHoachDoanhThuElementReviewCenter : ElementReviewCenterBase, IKeHoachDoanhThuElementReviewCenter
    {
        public KeHoachDoanhThuElementReviewCenter(CoreElement element, IList<string> success, IList<string> failure, IList<string> notReviewed, bool? status, int comments, int commentsInOrg) : base(element, success, failure, notReviewed, status, comments, commentsInOrg)
        {
        }
    }
}
