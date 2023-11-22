using SMO.Core.Common;
using SMO.Service.Class.Base;
using SMO.Service.Class.SUA_CHUA_LON;


using System.Collections.Generic;

namespace SMO.Service.Class.SUA_CHUA_LON
{
    public class SuaChuaLonReviewCenterViewModel : ReviewCenterViewModelBase<ISuaChuaLonElementReviewCenter>, IReviewCenterViewModelBase<ISuaChuaLonElementReviewCenter>
    {
    }
    public class SuaChuaLonElementReviewCenter : ElementReviewCenterBase, ISuaChuaLonElementReviewCenter
    {
        public SuaChuaLonElementReviewCenter(CoreElement element, IList<string> success, IList<string> failure, IList<string> notReviewed, bool? status, int comments, int commentsInOrg) : base(element, success, failure, notReviewed, status, comments, commentsInOrg)
        {
        }
    }
}
