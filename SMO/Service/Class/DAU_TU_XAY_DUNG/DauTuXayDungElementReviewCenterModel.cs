using SMO.Core.Common;
using SMO.Service.Class.Base;
using SMO.Service.Class.DAU_TU_XAY_DUNG;


using System.Collections.Generic;

namespace SMO.Service.Class.DAU_TU_XAY_DUNG
{
    public class DauTuXayDungReviewCenterViewModel : ReviewCenterViewModelBase<IDauTuXayDungElementReviewCenter>, IReviewCenterViewModelBase<IDauTuXayDungElementReviewCenter>
    {
    }
    public class DauTuXayDungElementReviewCenter : ElementReviewCenterBase, IDauTuXayDungElementReviewCenter
    {
        public DauTuXayDungElementReviewCenter(CoreElement element, IList<string> success, IList<string> failure, IList<string> notReviewed, bool? status, int comments, int commentsInOrg) : base(element, success, failure, notReviewed, status, comments, commentsInOrg)
        {
        }
    }
}
