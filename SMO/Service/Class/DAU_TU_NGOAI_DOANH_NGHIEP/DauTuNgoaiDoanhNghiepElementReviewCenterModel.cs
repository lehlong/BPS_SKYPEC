using SMO.Core.Common;
using SMO.Service.Class.Base;
using SMO.Service.Class.DAU_TU_NGOAI_DOANH_NGHIEP;


using System.Collections.Generic;

namespace SMO.Service.Class.DAU_TU_NGOAI_DOANH_NGHIEP
{
    public class DauTuNgoaiDoanhNghiepReviewCenterViewModel : ReviewCenterViewModelBase<IDauTuNgoaiDoanhNghiepElementReviewCenter>, IReviewCenterViewModelBase<IDauTuNgoaiDoanhNghiepElementReviewCenter>
    {
    }
    public class DauTuNgoaiDoanhNghiepElementReviewCenter : ElementReviewCenterBase, IDauTuNgoaiDoanhNghiepElementReviewCenter
    {
        public DauTuNgoaiDoanhNghiepElementReviewCenter(CoreElement element, IList<string> success, IList<string> failure, IList<string> notReviewed, bool? status, int comments, int commentsInOrg) : base(element, success, failure, notReviewed, status, comments, commentsInOrg)
        {
        }
    }
}
