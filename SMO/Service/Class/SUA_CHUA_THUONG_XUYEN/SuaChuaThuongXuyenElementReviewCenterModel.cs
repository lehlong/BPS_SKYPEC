using SMO.Core.Common;
using SMO.Service.Class.Base;
using SMO.Service.Class.SUA_CHUA_THUONG_XUYEN;


using System.Collections.Generic;

namespace SMO.Service.Class.SUA_CHUA_THUONG_XUYEN
{
    public class SuaChuaThuongXuyenReviewCenterViewModel : ReviewCenterViewModelBase<ISuaChuaThuongXuyenElementReviewCenter>, IReviewCenterViewModelBase<ISuaChuaThuongXuyenElementReviewCenter>
    {
    }
    public class SuaChuaThuongXuyenElementReviewCenter : ElementReviewCenterBase, ISuaChuaThuongXuyenElementReviewCenter
    {
        public SuaChuaThuongXuyenElementReviewCenter(CoreElement element, IList<string> success, IList<string> failure, IList<string> notReviewed, bool? status, int comments, int commentsInOrg) : base(element, success, failure, notReviewed, status, comments, commentsInOrg)
        {
        }
    }
}
