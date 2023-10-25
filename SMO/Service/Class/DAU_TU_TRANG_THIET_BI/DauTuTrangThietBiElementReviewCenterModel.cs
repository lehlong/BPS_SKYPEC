using SMO.Core.Common;
using SMO.Service.Class.Base;
using SMO.Service.Class.DAU_TU_TRANG_THIET_BI;


using System.Collections.Generic;

namespace SMO.Service.Class.DAU_TU_TRANG_THIET_BI
{
    public class DauTuTrangThietBiReviewCenterViewModel : ReviewCenterViewModelBase<IDauTuTrangThietBiElementReviewCenter>, IReviewCenterViewModelBase<IDauTuTrangThietBiElementReviewCenter>
    {
    }
    public class DauTuTrangThietBiElementReviewCenter : ElementReviewCenterBase, IDauTuTrangThietBiElementReviewCenter
    {
        public DauTuTrangThietBiElementReviewCenter(CoreElement element, IList<string> success, IList<string> failure, IList<string> notReviewed, bool? status, int comments, int commentsInOrg) : base(element, success, failure, notReviewed, status, comments, commentsInOrg)
        {
        }
    }
}
