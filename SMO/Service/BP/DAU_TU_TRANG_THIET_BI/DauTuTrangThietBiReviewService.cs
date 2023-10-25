using NHibernate.Linq;

using SMO.Core.Entities;

using SMO.Core.Entities.BP.DAU_TU_TRANG_THIET_BI;
using SMO.Core.Entities.MD;
using SMO.Repository.Implement.BP;

using SMO.Repository.Implement.BP.DAU_TU_TRANG_THIET_BI;
using SMO.Repository.Implement.MD;
using SMO.Service.Class;
using SMO.Service.Class.Base;
using SMO.Service.Class.DAU_TU_TRANG_THIET_BI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMO.Service.BP.DAU_TU_TRANG_THIET_BI
{
    public class DauTuTrangThietBiReviewService : BaseBPReviewService<T_BP_DAU_TU_TRANG_THIET_BI_REVIEW, T_BP_DAU_TU_TRANG_THIET_BI_REVIEW_RESULT, DauTuTrangThietBiReviewRepo>
    {
        /// <summary>
        /// List lịch sử thẩm định và tổng kiểm soát
        /// </summary>
        public IList<T_BP_DAU_TU_TRANG_THIET_BI_HISTORY> ListHistory { get; set; }
        /// <summary>
        /// List tổng kiểm soát dữ liệu
        /// </summary>
        public IList<T_BP_DAU_TU_TRANG_THIET_BI_REVIEW> ListControl { get; set; }

        public DauTuTrangThietBiReviewService()
        {
            ListHistory = new List<T_BP_DAU_TU_TRANG_THIET_BI_HISTORY>();
            ListControl = new List<T_BP_DAU_TU_TRANG_THIET_BI_REVIEW>();
        }
        /// <summary>
        /// Lấy dữ liệu lần thẩm định cuối cùng
        /// </summary>
        /// <returns></returns>
        internal IList<T_MD_KHOAN_MUC_DAU_TU> GetLastReview()
        {
            var header = GetNewestByExpression(x =>
            x.ORG_CODE == ObjDetail.ORG_CODE &&
            x.TIME_YEAR == ObjDetail.TIME_YEAR &&
            x.REVIEW_USER == ProfileUtilities.User.USER_NAME &&
            !x.IS_SUMMARY, x => x.DATA_VERSION, true);

            if (header == null)
            {
                return null;
            }
            var dataCost = SummaryCenterVersion(out _, header.DATA_VERSION);
            dataCost = dataCost.OrderBy(x => x.C_ORDER)
                                            .GroupBy(x => x.CODE)
                                            .Select(x => x.First()).ToList();
            return dataCost;
        }

        /// <summary>
        /// Lấy dữ liệu lần tổng kiểm soát cuối cùng
        /// </summary>
        /// <returns></returns>
        internal IList<T_MD_KHOAN_MUC_DAU_TU> GetLastReviewSummary()
        {
            var header = GetNewestByExpression(x =>
            x.ORG_CODE == ObjDetail.ORG_CODE &&
            x.TIME_YEAR == ObjDetail.TIME_YEAR &&
            x.IS_SUMMARY, x => x.DATA_VERSION, true);

            if (header == null)
            {
                return null;
            }
            var dataCost = SummaryCenterVersion(out _, header.DATA_VERSION);
            dataCost = dataCost.OrderBy(x => x.C_ORDER)
                                            .GroupBy(x => x.CODE)
                                            .Select(x => x.First()).ToList();
            return dataCost;
        }
        internal ReviewCenterViewModelBase<DauTuTrangThietBiElementReviewCenter> SummaryReviewDataCenter(ReviewDataCenterModel model)
        {
            var corp = GetCorp()?.CODE;

            var dataCost = SummaryCenterVersion(out IList<T_BP_DAU_TU_TRANG_THIET_BI_DATA> detailCostElements, model, corp);
            dataCost = dataCost.OrderBy(x => x.C_ORDER)
                                            .GroupBy(x => x.CODE)
                                            .Select(x => x.First()).ToList();

            return new ReviewCenterViewModelBase<DauTuTrangThietBiElementReviewCenter>
            {
                Elements = PrepareListReviewSummary(dataCost, model, corp),
                Version = model.VERSION.Value,
                Year = model.YEAR,
                UserControl = GetControlReview(model, corp)?.REVIEW_USER,
                OrgCode = corp,
                UserCouncil = model.ORG_CODE
            };

        }

        private IList<DauTuTrangThietBiElementReviewCenter> PrepareListReviewSummary(IList<T_MD_KHOAN_MUC_DAU_TU> dataCost, ReviewDataCenterModel model, string corp)
        {
            if (!model.IS_COMPLETED && !model.IS_NOT_COMPLETED)
            {
                return new List<DauTuTrangThietBiElementReviewCenter>();
            }
            if (!model.IS_CONTROL && !model.IS_COUNCIL_BUDGET)
            {
                return new List<DauTuTrangThietBiElementReviewCenter>();
            }

            var summaryReview = model.IS_COUNCIL_BUDGET ? model.IS_CONTROL ? (bool?)null : true : false;

            var objReview = GetControlReview(model, corp);
            var lstReviews = GetCouncils(model, corp);
            var comments = UnitOfWork.Repository<DauTuTrangThietBiReviewCommentRepo>()
                    .GetManyByExpression(x =>
                    x.ORG_CODE == corp &&
                    x.TIME_YEAR == model.YEAR);

            if (lstReviews == null || lstReviews.Count == 0)
            {
                if (objReview == null)
                {
                    return new List<DauTuTrangThietBiElementReviewCenter>();
                }
                else
                {
                    return (from d in dataCost
                            let result = objReview?.Results.FirstOrDefault(x => x.KHOAN_MUC_DAU_TU_CODE.Equals(d.CODE))
                            let status = result?.RESULT
                            let elementComments = comments.Where(x => x.KHOAN_MUC_DAU_TU_CODE == d.CODE)
                            let elementCommentsInOrg = elementComments.Where(x => x.ON_ORG_CODE == d.ORG_CODE)
                            select new DauTuTrangThietBiElementReviewCenter(
                                d,
                                null,
                                null,
                                null,
                                status,
                                elementComments.Sum(x => x.NUMBER_COMMENTS),
                                elementCommentsInOrg.Sum(x => x.NUMBER_COMMENTS)
                                )).ToList();
                }
            }

            var lstReviewResult = UnitOfWork.Repository<DauTuTrangThietBiReviewResultRepo>()
                .GetManyByExpression(x => lstReviews.Any(y => y.PKID.Equals(x.HEADER_ID)));

            var lookupReviewByElements = lstReviewResult.ToLookup(x => x.KHOAN_MUC_DAU_TU_CODE);

            var isHasValue = model.IS_COMPLETED ? model.IS_NOT_COMPLETED ? (bool?)null : true : false;

            return (from d in dataCost
                    let result = objReview?.Results.FirstOrDefault(x => x.KHOAN_MUC_DAU_TU_CODE.Equals(d.CODE))
                    let elements = lookupReviewByElements[d.CODE]
                    let success = elements.Count() == 0 ? null : elements.Where(x => x.RESULT.HasValue && x.RESULT.Value).Select(x => x.Header.REVIEW_USER).ToList()
                    let failure = elements.Count() == 0 ? null : elements.Where(x => x.RESULT.HasValue && !x.RESULT.Value).Select(x => x.Header.REVIEW_USER).ToList()
                    let notReviewed = elements.Count() == 0 ? null : elements.Where(x => !x.RESULT.HasValue).Select(x => x.Header.REVIEW_USER).ToList()
                    let status = result?.RESULT
                    let elementComments = comments.Where(x => x.KHOAN_MUC_DAU_TU_CODE == d.CODE)
                    let elementCommentsInOrg = elementComments.Where(x => x.ON_ORG_CODE == d.ORG_CODE)
                    select new DauTuTrangThietBiElementReviewCenter(
                        d,
                        success,
                        failure,
                        notReviewed,
                        status,
                        elementComments.Sum(x => x.NUMBER_COMMENTS),
                        elementCommentsInOrg.Sum(x => x.NUMBER_COMMENTS))).ToList();
        }

        internal T_BP_DAU_TU_TRANG_THIET_BI GetHeader()
        {
            return new DauTuTrangThietBiService().GetBPHeader(string.Empty, ObjDetail.DATA_VERSION, ObjDetail.TIME_YEAR, GetCorp().CODE);
        }

        /// <summary>
        /// Lấy lịch sử thẩm định và tổng kiểm soát dữ liệu
        /// </summary>
        internal void SearchHistory()
        {
            var lstActionsFilter = new string[]
            {
                Approve_Action.TuChoiKiemSoat,
                Approve_Action.KiemSoat,
                Approve_Action.PheDuyetKiemSoat,
                Approve_Action.ThamDinh,
                Approve_Action.TrinhDuyetKiemSoat,
                Approve_Action.KetThucThamDinh
            };
            ListHistory = GetHistory(ObjDetail.TIME_YEAR, lstActionsFilter);
        }

        internal void SearchReview()
        {
            var corp = GetCorp().CODE;

            // Dữ liệu thẩm định
            ObjList = UnitOfWork.Repository<DauTuTrangThietBiReviewRepo>().GetManyWithFetch(x => x.TIME_YEAR == ObjDetail.TIME_YEAR
           && x.DATA_VERSION == ObjDetail.DATA_VERSION
           && x.ORG_CODE == corp && !x.IS_SUMMARY, x => x.Results)
                .GroupBy(x => x.REVIEW_USER)
                .Select(x => x.OrderByDescending(y => y.CREATE_DATE).First())
                .ToList();

            // Dữ liệu tổng kiểm soát
            var tks = GetManyWithFetch(x => x.TIME_YEAR == ObjDetail.TIME_YEAR
           && x.DATA_VERSION == ObjDetail.DATA_VERSION
           && x.ORG_CODE == corp && x.IS_SUMMARY, x => x.Results)
                .OrderByDescending(y => y.UPDATE_DATE).ThenByDescending(x => x.CREATE_DATE)
                .FirstOrDefault();
            if (tks != null)
            {
                ListControl.Add(tks);
            }

        }

        internal IList<T_BP_DAU_TU_TRANG_THIET_BI_HISTORY> GetHistory(int year, IEnumerable<string> lstActionsFilter, int? version = null)
        {
            var corp = GetCorp().CODE;
            if (version == null)
            {
                return UnitOfWork.Repository<DauTuTrangThietBiHistoryRepo>()
                    .GetManyWithFetch(x => x.ORG_CODE == corp && x.TIME_YEAR == year && lstActionsFilter.ToList().Contains(x.ACTION),
                    x => x.ActionUser);
            }
            else
            {
                return UnitOfWork.Repository<DauTuTrangThietBiHistoryRepo>()
                    .GetManyWithFetch(x => x.ORG_CODE == corp && x.VERSION == version && x.TIME_YEAR == year && lstActionsFilter.ToList().Contains(x.ACTION),
                    x => x.ActionUser);
            }
        }

        /// <summary>
        /// Dữ liệu thẩm định của tổng kiểm soát
        /// </summary>
        /// <param name="model"></param>
        /// <param name="org"></param>
        /// <returns></returns>
        private T_BP_DAU_TU_TRANG_THIET_BI_REVIEW GetControlReview(ReviewDataCenterModel model, string org)
        {
            T_BP_DAU_TU_TRANG_THIET_BI_REVIEW objReview = null;
            if (model.IS_CONTROL)
            {
                objReview = GetFirstByExpression(
                    x => x.ORG_CODE.Equals(org) &&
                    x.TIME_YEAR == model.YEAR &&
                    x.DATA_VERSION == model.VERSION &&
                    x.IS_SUMMARY);
            }
            return objReview;
        }

        /// <summary>
        /// Lấy dữ liệu của các người thẩm định (hội đồng thẩm định)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="org"></param>
        /// <returns></returns>
        private IList<T_BP_DAU_TU_TRANG_THIET_BI_REVIEW> GetCouncils(ReviewDataCenterModel model, string org)
        {
            IList<T_BP_DAU_TU_TRANG_THIET_BI_REVIEW> lstReviews = null;
            if (model.IS_COUNCIL_BUDGET)
            {
                if (string.IsNullOrEmpty(model.ORG_CODE))
                {
                    lstReviews = GetManyByExpression(
                        x => x.ORG_CODE.Equals(org) &&
                        x.TIME_YEAR == model.YEAR &&
                        x.DATA_VERSION == model.VERSION &&
                        !x.IS_SUMMARY);
                }
                else
                {
                    lstReviews = GetManyByExpression(
                        x => x.ORG_CODE.Equals(org) &&
                        x.TIME_YEAR == model.YEAR &&
                        x.DATA_VERSION == model.VERSION &&
                        x.REVIEW_USER == model.ORG_CODE &&
                        !x.IS_SUMMARY);
                }
            }
            return lstReviews;
        }

        internal void UpdateReview(DauTuTrangThietBiReviewViewModel model)
        {
            // check budget period
            if (model.IsSummary)
            {
                ValidateBudgetPeriod(model.Year, BudgetPeriod.TKS);
            }
            else
            {
                ValidateBudgetPeriod(model.Year, BudgetPeriod.THAM_DINH);
            }
            if (!State)
            {
                return;
            }
            var resultCheckConstraint = CheckConstraintComments(model, out IDictionary<string, string> elementsNotValid);
            if (resultCheckConstraint)
            {
                try
                {
                    UnitOfWork.BeginTransaction();
                    var currentUser = ProfileUtilities.User.USER_NAME;
                    T_BP_DAU_TU_TRANG_THIET_BI_REVIEW headerFound;

                    if (model.IsSummary)
                    {
                        headerFound = GetFirstByExpression(x => x.ORG_CODE.Equals(model.OrgCode) &&
                            x.TIME_YEAR == model.Year &&
                            x.DATA_VERSION == model.Version &&
                            x.IS_SUMMARY);
                    }
                    else
                    {
                        headerFound = GetFirstByExpression(x => x.ORG_CODE.Equals(model.OrgCode) &&
                            x.TIME_YEAR == model.Year &&
                            x.DATA_VERSION == model.Version &&
                            x.REVIEW_USER == currentUser && !x.IS_SUMMARY);
                    }
                    var headerId = string.Empty;
                    if (headerFound == null)
                    {
                        // create header cost pl review
                        headerId = Guid.NewGuid().ToString();
                        ObjDetail = new T_BP_DAU_TU_TRANG_THIET_BI_REVIEW
                        {
                            PKID = headerId,
                            ORG_CODE = model.OrgCode,
                            TIME_YEAR = model.Year,
                            IS_SUMMARY = model.IsSummary,
                            DATA_VERSION = model.Version,
                            REVIEW_USER = currentUser,
                        };
                        ObjDetail = CurrentRepository.Create(ObjDetail);
                    }
                    else
                    {
                        if (model.IsSummary)
                        {
                            // đã được ttổng kiểm soát trước đây
                            // cập nhật header tổng kiểm soát
                            headerFound.REVIEW_USER = currentUser;
                            headerFound.UPDATE_BY = currentUser;
                            CurrentRepository.Update(headerFound);
                        }

                        headerId = headerFound.PKID;
                        if (model.Status.HasValue)
                        {
                            // nếu có filter thì xóa dữ liệu có filter
                            UnitOfWork.Repository<DauTuTrangThietBiReviewResultRepo>().Delete(x => x.HEADER_ID.Equals(headerId) && x.RESULT == model.Status.Value);
                        }
                        else
                        {
                            UnitOfWork.Repository<DauTuTrangThietBiReviewResultRepo>().Delete(x => x.HEADER_ID.Equals(headerId));
                        }
                    }

                    // create history
                    var history = new T_BP_DAU_TU_TRANG_THIET_BI_REVIEW_HISTORY
                    {
                        PKID = Guid.NewGuid().ToString(),
                        ORG_CODE = model.OrgCode,
                        TIME_YEAR = model.Year,
                        DATA_VERSION = model.Version,
                        REVIEW_USER = currentUser,
                        REVIEW_DATE = DateTime.Now,
                        CREATE_BY = currentUser
                    };
                    UnitOfWork.Repository<DauTuTrangThietBiReviewHistoryRepo>().Create(history);

                    // create history
                    var historyPL = new T_BP_DAU_TU_TRANG_THIET_BI_HISTORY
                    {
                        PKID = Guid.NewGuid().ToString(),
                        ORG_CODE = model.OrgCode,
                        TIME_YEAR = model.Year,
                        TEMPLATE_CODE = string.Empty,
                        VERSION = model.Version,
                        ACTION_USER = currentUser,
                        ACTION_DATE = DateTime.Now,
                        ACTION = !model.IsSummary ? Approve_Action.ThamDinh : Approve_Action.KiemSoat,
                        CREATE_BY = currentUser
                    };
                    UnitOfWork.Repository<DauTuTrangThietBiHistoryRepo>().Create(historyPL);

                    // create results
                    UnitOfWork.Repository<DauTuTrangThietBiReviewResultRepo>()
                        .Create((from r in model.Elements
                                 where !r.IS_GROUP
                                 select new T_BP_DAU_TU_TRANG_THIET_BI_REVIEW_RESULT
                                 {
                                     PKID = Guid.NewGuid().ToString(),
                                     HEADER_ID = headerId,
                                     KHOAN_MUC_DAU_TU_CODE = r.CODE,
                                     RESULT = r.Status,
                                     TIME_YEAR = model.Year
                                 }).ToList());

                    UnitOfWork.Commit();
                }
                catch (Exception e)
                {
                    UnitOfWork.Rollback();
                    State = false;
                    Exception = e;
                    ErrorMessage = e.Message;
                }
            }
            else
            {
                State = false;
                var errorMessage = new StringBuilder("Yêu cầu thêm comments vào những khoản mục không đạt sau: ");
                foreach (var el in elementsNotValid)
                {
                    errorMessage.Append($"<br>{el.Key}: {el.Value}");
                }
                ErrorMessage = errorMessage.ToString();
            }
        }


        /// <summary>
        /// Kiểm tra xem các khoản mục mà không đạt đã được comment bởi current user hay chưa
        /// Nếu chưa kết thúc thì không cần check
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private bool CheckConstraintComments(DauTuTrangThietBiReviewViewModel model, out IDictionary<string, string> elementsNotValid)
        {
            elementsNotValid = new Dictionary<string, string>();
            if (!model.IsEnd)
            {
                return true;
            }

            var elementsNotSuccess = model.Elements.Where(x => x.Status.HasValue && !x.Status.Value && !x.IS_GROUP).Select(x => x.CODE);

            // check có khoản mục không đạt hay không
            if (elementsNotSuccess.Count() == 0)
            {
                return true;
            }

            var currentUser = ProfileUtilities.User.USER_NAME;

            var queryable = UnitOfWork.GetSession().Query<T_BP_DAU_TU_TRANG_THIET_BI_REVIEW_COMMENT>();
            queryable = queryable.Where(x => x.ORG_CODE == model.OrgCode);
            queryable = queryable.Where(x => x.TIME_YEAR == model.Year);
            // queryable = queryable.Where(x => x.DATA_VERSION == model.Version);
            queryable = queryable.Where(x => elementsNotSuccess.Contains(x.KHOAN_MUC_DAU_TU_CODE));
            queryable = queryable.FetchMany(x => x.Comments);

            var lookupComments = queryable.ToList().ToLookup(x => x.KHOAN_MUC_DAU_TU_CODE);
            var allElements = GetAllMasterData<KhoanMucDauTuRepo, T_MD_KHOAN_MUC_DAU_TU>().Where(x => x.TIME_YEAR == model.Year);
            foreach (var element in elementsNotSuccess)
            {
                // check nếu khoản mục chưa được comment bởi current user 
                // hoặc chưa được comment
                if (lookupComments[element].Count() == 0 || !lookupComments[element].Any(x => x.Comments.Any(y => y.CREATE_BY == currentUser)))
                {
                    elementsNotValid.Add(element, allElements.FirstOrDefault(x => x.CODE == element).NAME);
                }
            }
            if (elementsNotValid.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Lấy danh sách các user đã thẩm định
        /// </summary>
        /// <param name="year">Năm thẩm định</param>
        /// <param name="version">Version thẩm định</param>
        /// <returns>Danh sách các user name đã thẩm định trong năm của version</returns>
        internal IList<string> GetReviewUsers(int year, int version)
        {
            var corp = GetCorp()?.CODE;
            return GetManyByExpression(x => x.TIME_YEAR == year && x.ORG_CODE == corp && x.DATA_VERSION == version)
                .Select(x => x.REVIEW_USER)
                .Distinct()
                .OrderBy(x => x)
                .ToList();
        }

        /// <summary>
        /// Lấy danh sách các version đã được thẩm định
        /// </summary>
        /// <param name="year">Năm thẩm định</param>
        /// <returns>Danh sách các version đã được thẩm định trong năm</returns>
        internal IList<int> GetVersions(int year)
        {
            var corp = GetCorp()?.CODE;
            return UnitOfWork.Repository<DauTuTrangThietBiVersionRepo>().GetManyByExpression(x => x.TIME_YEAR == year && x.ORG_CODE == corp)
                .Select(x => x.VERSION)
                .Distinct()
                .OrderByDescending(x => x)
                .ToList();
        }

        /// <summary>
        /// Lấy dữ liệu của đơn vị tập đoàn. Có parent code = empty
        /// </summary>
        /// <returns></returns>
        internal T_MD_COST_CENTER GetCorp()
        {
            return UnitOfWork.Repository<CostCenterRepo>()
                .GetFirstByExpression(x => x.PARENT_CODE == "");
        }

        internal IList<T_MD_KHOAN_MUC_DAU_TU> SummaryCenterVersion(out IList<T_BP_DAU_TU_TRANG_THIET_BI_DATA> detailCostElements, int? version = null)
        {
            return new DauTuTrangThietBiService().SummaryCenterVersion(out detailCostElements, ObjDetail.ORG_CODE, ObjDetail.TIME_YEAR, version);
        }

        internal IList<T_MD_KHOAN_MUC_DAU_TU> SummaryCenterVersion(out IList<T_BP_DAU_TU_TRANG_THIET_BI_DATA> detailCostElements, ReviewDataCenterModel model, string corp)
        {
            return new DauTuTrangThietBiService().SummaryCenterVersion(out detailCostElements, corp, model.YEAR, model.VERSION);
        }

        internal IList<DauTuTrangThietBiElementReview> PrepareListReview(IList<T_MD_KHOAN_MUC_DAU_TU> dataCost)
        {
            var comments = UnitOfWork.Repository<DauTuTrangThietBiReviewCommentRepo>()
                    .GetManyByExpression(x =>
                    x.ORG_CODE == ObjDetail.ORG_CODE &&
                    x.TIME_YEAR == ObjDetail.TIME_YEAR);

            var objReview = new T_BP_DAU_TU_TRANG_THIET_BI_REVIEW();
            if (!IsReview)
            {
                objReview = GetFirstByExpression(
                    x => x.ORG_CODE.Equals(ObjDetail.ORG_CODE) &&
                    x.TIME_YEAR == ObjDetail.TIME_YEAR &&
                    x.DATA_VERSION == ObjDetail.DATA_VERSION &&
                    x.IS_SUMMARY);
            }
            else
            {
                objReview = GetFirstByExpression(
                    x => x.ORG_CODE.Equals(ObjDetail.ORG_CODE) &&
                    x.TIME_YEAR == ObjDetail.TIME_YEAR &&
                    x.DATA_VERSION == ObjDetail.DATA_VERSION &&
                    x.REVIEW_USER == ProfileUtilities.User.USER_NAME &&
                    !x.IS_SUMMARY);
            }

            // lần đầu tiên thẩm định hoặc tks ở version mới
            // lấy lại dữ liệu thẩm định tks ở version liền trước để hiển thị kết quả checkbox
            if (objReview == null)
            {
                if (!IsReview)
                {
                    objReview = GetNewestByExpression(
                        x => x.ORG_CODE.Equals(ObjDetail.ORG_CODE) &&
                        x.TIME_YEAR == ObjDetail.TIME_YEAR &&
                        x.IS_SUMMARY, x => x.DATA_VERSION, true);
                }
                else
                {
                    objReview = GetNewestByExpression(
                        x => x.ORG_CODE.Equals(ObjDetail.ORG_CODE) &&
                        x.TIME_YEAR == ObjDetail.TIME_YEAR &&
                        x.REVIEW_USER == ProfileUtilities.User.USER_NAME &&
                        !x.IS_SUMMARY, x => x.DATA_VERSION, true);
                }
            }

            // show all
            return (from d in dataCost
                    let result = objReview?.Results?.FirstOrDefault(x => x.KHOAN_MUC_DAU_TU_CODE.Equals(d.CODE))
                    let elementComments = comments.Where(x => x.KHOAN_MUC_DAU_TU_CODE == d.CODE)
                    let elementCommentsInOrg = elementComments.Where(x => x.ON_ORG_CODE == d.ORG_CODE)
                    select new DauTuTrangThietBiElementReview(
                        d,
                        result?.RESULT,
                        elementComments.Sum(x => x.NUMBER_COMMENTS),
                        elementCommentsInOrg.Sum(x => x.NUMBER_COMMENTS)
                        )).ToList();

        }
    }
}
