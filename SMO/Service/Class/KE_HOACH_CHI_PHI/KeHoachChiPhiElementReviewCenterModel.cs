using SMO.Core.Common;
using SMO.Service.Class.Base;
using SMO.Service.Class.KE_HOACH_CHI_PHI;


using System.Collections.Generic;

namespace SMO.Service.Class.KE_HOACH_CHI_PHI
{
    public class KeHoachChiPhiReviewCenterViewModel : ReviewCenterViewModelBase<IKeHoachChiPhiElementReviewCenter>, IReviewCenterViewModelBase<IKeHoachChiPhiElementReviewCenter>
    {
    }
    public class KeHoachChiPhiElementReviewCenter : ElementReviewCenterBase, IKeHoachChiPhiElementReviewCenter
    {
        public KeHoachChiPhiElementReviewCenter(CoreElement element, IList<string> success, IList<string> failure, IList<string> notReviewed, bool? status, int comments, int commentsInOrg) : base(element, success, failure, notReviewed, status, comments, commentsInOrg)
        {
        }
    }
}
