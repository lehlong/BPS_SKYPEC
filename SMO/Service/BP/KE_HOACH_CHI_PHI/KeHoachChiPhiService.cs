using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

using SMO.AppCode.Utilities;
using SMO.Core.Entities;
using SMO.Core.Entities.BP;
using SMO.Core.Entities.BP.KE_HOACH_CHI_PHI;
using SMO.Core.Entities.BP.KE_HOACH_CHI_PHI.KE_HOACH_CHI_PHI_DATA_BASE;
using SMO.Core.Entities.MD;
using SMO.Helper;
using SMO.Models;
using SMO.Repository.Implement.BP;
using SMO.Repository.Implement.BP.KE_HOACH_CHI_PHI;
using SMO.Repository.Implement.BP.KE_HOACH_CHI_PHI.KE_HOACH_CHI_PHI_DATA_BASE;
using SMO.Repository.Implement.MD;
using SMO.Service.Class;
using SMO.Service.Common;
using SMO.ServiceInterface.BP.KeHoachChiPhi;

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

using static SMO.SelectListUtilities;

namespace SMO.Service.BP.KE_HOACH_CHI_PHI
{
    public class KeHoachChiPhiService : BaseBPService<T_BP_KE_HOACH_CHI_PHI, KeHoachChiPhiRepo, T_MD_KHOAN_MUC_HANG_HOA, T_BP_KE_HOACH_CHI_PHI_VERSION, T_BP_KE_HOACH_CHI_PHI_HISTORY, KeHoachChiPhiHistoryRepo>, IKeHoachChiPhiService
    {
        private readonly List<Point> InvalidCellsList;
        private readonly List<string> ListColumnName;
        private readonly List<string> ListColumnNameDataBase;
        private int StartRowData;
        public List<T_BP_KE_HOACH_CHI_PHI_HISTORY> ObjListHistory { get; set; }
        public List<T_BP_KE_HOACH_CHI_PHI_VERSION> ObjListVersion { get; set; }
        public List<T_BP_KE_HOACH_CHI_PHI_SUM_UP_DETAIL> ObjListSumUpHistory { get; set; }
        public KeHoachChiPhiService()
        {
            this.StartRowData = 6;
            this.ListColumnName = new List<string> {
                "MÃ SÂN BAY",
                "TÊN SÂN BAY",
                "MÃ HÃNG HÀNG KHÔNG",
                "TÊN HÃNG HÀNG KHÔNG",
                "MÃ KHOẢN MỤC",
                "TÊN KHOẢN MỤC",
                "THÁNG 1",
                "THÁNG 2",
                "THÁNG 3",
                "THÁNG 4",
                "THÁNG 5",
                "THÁNG 6",
                "THÁNG 7",
                "THÁNG 8",
                "THÁNG 9",
                "THÁNG 10",
                "THÁNG 11",
                "THÁNG 12",
                "TỔNG NĂM",
                "TRUNG BÌNH NĂM",
                "GHI CHÚ"
            };
            this.ListColumnNameDataBase = new List<string>
            {
                "MÃ SÂN BAY",
                "TÊN SÂN BAY",
                "MÃ HÃNG HÀNG KHÔNG",
                "TÊN HÃNG HÀNG KHÔNG",
                "MÃ KHOẢN MỤC",
                "TÊN KHOẢN MỤC",
                "HÀNG HÓA, DỊCH VỤ",
                "ĐƠN VỊ TÍNH",
                "SỐ LƯỢNG", "THỜI GIAN", "ĐƠN GIÁ", "THÀNH TIỀN",
                "SỐ LƯỢNG", "THỜI GIAN", "ĐƠN GIÁ", "THÀNH TIỀN",
                "SỐ LƯỢNG", "THỜI GIAN", "ĐƠN GIÁ", "THÀNH TIỀN",
                "SỐ LƯỢNG", "THỜI GIAN", "ĐƠN GIÁ", "THÀNH TIỀN",
                "SỐ LƯỢNG", "THỜI GIAN", "ĐƠN GIÁ", "THÀNH TIỀN",
                "SỐ LƯỢNG", "THỜI GIAN", "ĐƠN GIÁ", "THÀNH TIỀN",
                "SỐ LƯỢNG", "THỜI GIAN", "ĐƠN GIÁ", "THÀNH TIỀN",
                "SỐ LƯỢNG", "THỜI GIAN", "ĐƠN GIÁ", "THÀNH TIỀN",
                "SỐ LƯỢNG", "THỜI GIAN", "ĐƠN GIÁ", "THÀNH TIỀN",
                "SỐ LƯỢNG", "THỜI GIAN", "ĐƠN GIÁ", "THÀNH TIỀN",
                "SỐ LƯỢNG", "THỜI GIAN", "ĐƠN GIÁ", "THÀNH TIỀN",
                "SỐ LƯỢNG", "THỜI GIAN", "ĐƠN GIÁ", "THÀNH TIỀN",
                "SỐ LƯỢNG", "THÀNH TIỀN",
                "SỐ LƯỢNG", "THÀNH TIỀN",
                "GHI CHÚ"
            };

            this.InvalidCellsList = new List<Point>();
            this.ObjListHistory = new List<T_BP_KE_HOACH_CHI_PHI_HISTORY>();
            this.ObjListVersion = new List<T_BP_KE_HOACH_CHI_PHI_VERSION>();
            this.ObjListSumUpHistory = new List<T_BP_KE_HOACH_CHI_PHI_SUM_UP_DETAIL>();
        }

        /// <summary>
        /// check xem người dùng hiện tại có hiển thị nút thẩm định hay không
        /// </summary>
        /// <returns></returns>
        public override bool ShowReviewBtn()
        {
            return ShowReviewBtn(ObjDetail.TIME_YEAR);
        }

        public override bool ShowReviewBtn(int year)
        {
            var lstReviewUsers = UnitOfWork.Repository<UserReviewRepo>()
                .GetManyWithFetch(x => x.TIME_YEAR == year)
                .Select(x => x.USER_NAME);
            var currentUser = ProfileUtilities.User?.USER_NAME;

            // check người dùng có nằm trong hội đồng thẩm định 
            if (!lstReviewUsers.Contains(currentUser))
            {
                return false;
            }
            else
            {
                var corp = UnitOfWork.Repository<CostCenterRepo>()
                    .GetFirstWithFetch(x => x.PARENT_CODE == "").CODE;
                var version = GetFirstWithFetch(x => x.ORG_CODE == corp &&
                    x.TIME_YEAR == year)?.VERSION;
                if (version == null)
                {
                    return false;
                }
                // check người dùng đã kết thúc thẩm định chưa
                var historyReview = UnitOfWork.Repository<KeHoachChiPhiReviewRepo>()
                    .GetFirstWithFetch(x => x.ORG_CODE == corp &&
                    x.DATA_VERSION == version &&
                    x.TIME_YEAR == year &&
                    x.REVIEW_USER == currentUser &&
                    x.IS_END &&
                    !x.IS_SUMMARY);

                return historyReview == null;
            }
        }

        #region Workflow
        private void SendNotify(Activity activity)
        {
            NotifyUtilities.CreateNotify(
                    new NotifyPara()
                    {
                        Activity = activity,
                        OrgCode = ProfileUtilities.User?.ORGANIZE_CODE,
                        TimeYear = this.ObjDetail.TIME_YEAR,
                        ModulType = ModulType.KeHoachChiPhi,
                        TemplateCode = this.ObjDetail.TEMPLATE_CODE,
                        UserSent = ProfileUtilities.User?.USER_NAME
                    });
        }
        public override void TrinhDuyet(string code)
        {
            try
            {
                var currentUser = ProfileUtilities.User?.USER_NAME;
                if (!ValidateTrinhDuyet(code))
                {
                    return;
                }

                ObjDetail.STATUS = Approve_Status.ChoPheDuyet;
                ObjDetail.UPDATE_BY = currentUser;

                UnitOfWork.BeginTransaction();

                CurrentRepository.Update(ObjDetail);

                UnitOfWork.Repository<KeHoachChiPhiHistoryRepo>()
                    .Create(new T_BP_KE_HOACH_CHI_PHI_HISTORY()
                    {
                        PKID = Guid.NewGuid().ToString(),
                        ORG_CODE = ObjDetail.ORG_CODE,
                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                        VERSION = ObjDetail.VERSION,
                        TIME_YEAR = ObjDetail.TIME_YEAR,
                        ACTION = Approve_Action.TrinhDuyet,
                        ACTION_USER = currentUser,
                        ACTION_DATE = DateTime.Now,
                        CREATE_BY = currentUser
                    });

                //TODO: Cập nhật lại code thực thi
                UnitOfWork.GetSession().CreateSQLQuery($"UPDATE T_BP_KE_HOACH_CHI_PHI_DATA SET STATUS = '{Approve_Status.ChoPheDuyet}' WHERE ORG_CODE = '{ObjDetail.ORG_CODE}' AND TEMPLATE_CODE = '{ObjDetail.TEMPLATE_CODE}' AND TIME_YEAR = {ObjDetail.TIME_YEAR}").ExecuteUpdate();

                UnitOfWork.Commit();

                SendNotify(Activity.AC_TRINH_DUYET);

            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                Exception = ex;
                State = false;
            }
        }

        public override void HuyTrinhDuyet(string code)
        {
            try
            {
                var currentUser = ProfileUtilities.User?.USER_NAME;
                if (!ValidateHuyTrinhDuyet(code))
                {
                    return;
                }

                if (this.ObjDetail.STATUS == Approve_Status.ChoPheDuyet)
                {
                    ObjDetail.STATUS = Approve_Status.ChuaTrinhDuyet;
                }
                else
                {
                    ObjDetail.STATUS = Approve_Status.TGD_HuyTrinh;
                }
                ObjDetail.UPDATE_BY = currentUser;

                UnitOfWork.BeginTransaction();

                CurrentRepository.Update(ObjDetail);

                UnitOfWork.Repository<KeHoachChiPhiHistoryRepo>().Create(new T_BP_KE_HOACH_CHI_PHI_HISTORY()
                {
                    PKID = Guid.NewGuid().ToString(),
                    ORG_CODE = ObjDetail.ORG_CODE,
                    TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                    VERSION = ObjDetail.VERSION,
                    TIME_YEAR = ObjDetail.TIME_YEAR,
                    ACTION = Approve_Action.HuyTrinhDuyet,
                    ACTION_USER = currentUser,
                    ACTION_DATE = DateTime.Now,
                    CREATE_BY = currentUser
                });

                //TODO: Cập nhật lại code thực thi
                UnitOfWork.GetSession().CreateSQLQuery($"UPDATE T_BP_KE_HOACH_CHI_PHI_DATA SET STATUS = '{Approve_Status.ChuaTrinhDuyet}' WHERE ORG_CODE = '{ObjDetail.ORG_CODE}' AND TEMPLATE_CODE = '{ObjDetail.TEMPLATE_CODE}' AND TIME_YEAR = {ObjDetail.TIME_YEAR}").ExecuteUpdate();

                UnitOfWork.Commit();

                if (ObjDetail.STATUS == Approve_Status.TGD_HuyTrinh)
                {
                    SendNotify(Activity.AC_HUY_TRINH_TGD);
                }
                else
                {
                    SendNotify(Activity.AC_HUY_TRINH_DUYET);
                }

            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                Exception = ex;
                State = false;
            }
        }

        public override void TGDTuChoi(string code)
        {
            try
            {
                var currentUser = ProfileUtilities.User?.USER_NAME;
                if (!ValidateTGDTuChoi(code))
                {
                    return;
                }

                ObjDetail.STATUS = Approve_Status.TGD_TuChoi;
                ObjDetail.UPDATE_BY = currentUser;

                UnitOfWork.BeginTransaction();

                CurrentRepository.Update(ObjDetail);

                UnitOfWork.Repository<KeHoachChiPhiHistoryRepo>()
                    .Create(new T_BP_KE_HOACH_CHI_PHI_HISTORY()
                    {
                        PKID = Guid.NewGuid().ToString(),
                        ORG_CODE = ObjDetail.ORG_CODE,
                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                        VERSION = ObjDetail.VERSION,
                        TIME_YEAR = ObjDetail.TIME_YEAR,
                        ACTION = Approve_Action.TGD_TuChoi,
                        ACTION_USER = currentUser,
                        ACTION_DATE = DateTime.Now,
                        CREATE_BY = currentUser
                    });

                UnitOfWork.Commit();
                SendNotify(Activity.AC_TGD_TU_CHOI);

            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                Exception = ex;
                State = false;
            }
        }

        public override void TGDHuyPheDuyet(string code)
        {
            try
            {
                var currentUser = ProfileUtilities.User?.USER_NAME;
                if (!ValidateTGDHuyPheDuyet(code))
                {
                    return;
                }

                ObjDetail.STATUS = Approve_Status.TGD_ChoPheDuyet;
                ObjDetail.UPDATE_BY = currentUser;

                UnitOfWork.BeginTransaction();

                CurrentRepository.Update(ObjDetail);

                UnitOfWork.Repository<KeHoachChiPhiHistoryRepo>().Create(new T_BP_KE_HOACH_CHI_PHI_HISTORY()
                {
                    PKID = Guid.NewGuid().ToString(),
                    ORG_CODE = ObjDetail.ORG_CODE,
                    TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                    VERSION = ObjDetail.VERSION,
                    TIME_YEAR = ObjDetail.TIME_YEAR,
                    ACTION = Approve_Action.TGD_HuyPheDuyet,
                    ACTION_USER = currentUser,
                    ACTION_DATE = DateTime.Now,
                    CREATE_BY = currentUser
                });

                UnitOfWork.Commit();

                SendNotify(Activity.AC_TGD_HUY_PHE_DUYET);

            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                Exception = ex;
                State = false;
            }
        }

        public override void TGDPheDuyet(string code)
        {
            try
            {
                var currentUser = ProfileUtilities.User?.USER_NAME;
                if (!ValidateTGDPheDuyet(code))
                {
                    return;
                }

                ObjDetail.STATUS = Approve_Status.TGD_PheDuyet;
                ObjDetail.UPDATE_BY = currentUser;

                UnitOfWork.BeginTransaction();

                CurrentRepository.Update(ObjDetail);

                UnitOfWork.Repository<KeHoachChiPhiHistoryRepo>().Create(new T_BP_KE_HOACH_CHI_PHI_HISTORY()
                {
                    PKID = Guid.NewGuid().ToString(),
                    ORG_CODE = ObjDetail.ORG_CODE,
                    TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                    VERSION = ObjDetail.VERSION,
                    TIME_YEAR = ObjDetail.TIME_YEAR,
                    ACTION = Approve_Action.TGD_PheDuyet,
                    ACTION_USER = currentUser,
                    ACTION_DATE = DateTime.Now,
                    CREATE_BY = currentUser
                });

                UnitOfWork.Commit();
                SendNotify(Activity.AC_TGD_PHE_DUYET);

            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                Exception = ex;
                State = false;
            }
        }

        public override void PheDuyet(string code)
        {
            try
            {
                var currentUser = ProfileUtilities.User?.USER_NAME;
                if (!ValidatePheDuyet(code))
                {
                    return;
                }
                ObjDetail.STATUS = Approve_Status.DaPheDuyet;
                ObjDetail.UPDATE_BY = currentUser;

                UnitOfWork.BeginTransaction();

                CurrentRepository.Update(ObjDetail);

                UnitOfWork.Repository<KeHoachChiPhiHistoryRepo>().Create(new T_BP_KE_HOACH_CHI_PHI_HISTORY()
                {
                    PKID = Guid.NewGuid().ToString(),
                    ORG_CODE = ObjDetail.ORG_CODE,
                    TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                    VERSION = ObjDetail.VERSION,
                    TIME_YEAR = ObjDetail.TIME_YEAR,
                    ACTION = Approve_Action.PheDuyetDuLieu,
                    ACTION_USER = currentUser,
                    ACTION_DATE = DateTime.Now,
                    CREATE_BY = currentUser
                });

                //TODO: Cập nhật lại code thực thi
                UnitOfWork.GetSession().CreateSQLQuery($"UPDATE T_BP_KE_HOACH_CHI_PHI_DATA SET STATUS = '{Approve_Status.DaPheDuyet}' WHERE ORG_CODE = '{ObjDetail.ORG_CODE}' AND TEMPLATE_CODE = '{ObjDetail.TEMPLATE_CODE}' AND TIME_YEAR = {ObjDetail.TIME_YEAR}").ExecuteUpdate();

                UnitOfWork.Commit();
                SendNotify(Activity.AC_PHE_DUYET);

            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                Exception = ex;
                State = false;
            }
        }

        public override void HuyNop(string code)
        {
            // get header
            var currentUser = ProfileUtilities.User?.USER_NAME;
            if (!ValidateHuyNop(code))
            {
                return;
            }

            // update version property
            try
            {
                UnitOfWork.BeginTransaction();
                // update version table
                UnitOfWork.Repository<KeHoachChiPhiVersionRepo>().Update(x => x.TEMPLATE_CODE == ObjDetail.TEMPLATE_CODE && x.TIME_YEAR == ObjDetail.TIME_YEAR && x.VERSION == ObjDetail.VERSION, x => x.IS_DELETED = 1, x => x.UPDATE_BY = currentUser);

                // update main table
                ObjDetail.IS_DELETED = true; CurrentRepository.Update(ObjDetail);

                // create history log
                UnitOfWork.Repository<KeHoachChiPhiHistoryRepo>().Create(new T_BP_KE_HOACH_CHI_PHI_HISTORY
                {
                    PKID = Guid.NewGuid().ToString(),
                    ORG_CODE = ObjDetail.ORG_CODE,
                    TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                    VERSION = ObjDetail.VERSION,
                    TIME_YEAR = ObjDetail.TIME_YEAR,
                    ACTION = Approve_Action.HuyNop,
                    ACTION_USER = currentUser,
                    CREATE_BY = currentUser,
                    ACTION_DATE = DateTime.Now
                });
                UnitOfWork.Commit();
                SendNotify(Activity.AC_HUY_NOP);
            }
            catch (Exception e)
            {
                UnitOfWork.Rollback();
                State = false;
                Exception = e;
            }
        }

        public override void HuyPheDuyet(string code)
        {
            try
            {
                var currentUser = ProfileUtilities.User?.USER_NAME;
                if (!ValidateHuyPheDuyet(code))
                {
                    return;
                }

                ObjDetail.STATUS = Approve_Status.ChoPheDuyet;
                ObjDetail.UPDATE_BY = currentUser;

                UnitOfWork.BeginTransaction();

                CurrentRepository.Update(ObjDetail);

                UnitOfWork.Repository<KeHoachChiPhiHistoryRepo>().Create(new T_BP_KE_HOACH_CHI_PHI_HISTORY()
                {
                    PKID = Guid.NewGuid().ToString(),
                    ORG_CODE = ObjDetail.ORG_CODE,
                    TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                    VERSION = ObjDetail.VERSION,
                    TIME_YEAR = ObjDetail.TIME_YEAR,
                    ACTION = Approve_Action.HuyPheDuyet,
                    ACTION_USER = currentUser,
                    ACTION_DATE = DateTime.Now,
                    CREATE_BY = currentUser
                });

                //TODO: Cập nhật lại code thực thi
                UnitOfWork.GetSession().CreateSQLQuery($"UPDATE T_BP_KE_HOACH_CHI_PHI_DATA SET STATUS = '{Approve_Status.ChoPheDuyet}' WHERE ORG_CODE = '{ObjDetail.ORG_CODE}' AND TEMPLATE_CODE = '{ObjDetail.TEMPLATE_CODE}' AND TIME_YEAR = {ObjDetail.TIME_YEAR}").ExecuteUpdate();

                UnitOfWork.Commit();

                SendNotify(Activity.AC_HUY_PHE_DUYET);

            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                Exception = ex;
                State = false;
            }
        }

        public override void TuChoi(string code)
        {
            try
            {
                var currentUser = ProfileUtilities.User?.USER_NAME;
                if (!ValidateTuChoi(code))
                {
                    return;
                }

                ObjDetail.STATUS = Approve_Status.TuChoi;
                ObjDetail.UPDATE_BY = currentUser;

                UnitOfWork.BeginTransaction();

                CurrentRepository.Update(ObjDetail);

                UnitOfWork.Repository<KeHoachChiPhiHistoryRepo>().Create(new T_BP_KE_HOACH_CHI_PHI_HISTORY()
                {
                    PKID = Guid.NewGuid().ToString(),
                    ORG_CODE = ObjDetail.ORG_CODE,
                    TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                    VERSION = ObjDetail.VERSION,
                    TIME_YEAR = ObjDetail.TIME_YEAR,
                    ACTION = Approve_Action.TuChoi,
                    ACTION_USER = currentUser,
                    ACTION_DATE = DateTime.Now,
                    CREATE_BY = currentUser
                });

                //TODO: Cập nhật lại code thực thi
                UnitOfWork.GetSession().CreateSQLQuery($"UPDATE T_BP_KE_HOACH_CHI_PHI_DATA SET STATUS = '{Approve_Status.TuChoi}' WHERE ORG_CODE = '{ObjDetail.ORG_CODE}' AND TEMPLATE_CODE = '{ObjDetail.TEMPLATE_CODE}' AND TIME_YEAR = {ObjDetail.TIME_YEAR}").ExecuteUpdate();

                UnitOfWork.Commit();
                SendNotify(Activity.AC_TU_CHOI);

            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                Exception = ex;
                State = false;
            }
        }

        public override void ChuyenTKS(string code)
        {
            try
            {
                var currentUser = ProfileUtilities.User?.USER_NAME;
                if (!ValidateChuyenTKS(code))
                {
                    return;
                }

                ObjDetail.STATUS = Approve_Status.TKS_DuLieu;
                ObjDetail.UPDATE_BY = currentUser;
                ObjDetail.IS_REVIEWED = true;

                UnitOfWork.BeginTransaction();

                CurrentRepository.Update(ObjDetail);

                UnitOfWork.Repository<KeHoachChiPhiHistoryRepo>()
                    .Create(new T_BP_KE_HOACH_CHI_PHI_HISTORY()
                    {
                        PKID = Guid.NewGuid().ToString(),
                        ORG_CODE = ObjDetail.ORG_CODE,
                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                        VERSION = ObjDetail.VERSION,
                        TIME_YEAR = ObjDetail.TIME_YEAR,
                        ACTION = Approve_Action.ChuyenTKS,
                        ACTION_USER = currentUser,
                        ACTION_DATE = DateTime.Now,
                        CREATE_BY = currentUser
                    });

                // TODO: cập nhật trạng thái cho những người thẩm định và tổng kiểm soát về trạng thái chưa kết thúc
                UnitOfWork.Repository<KeHoachChiPhiReviewRepo>()
                    .Update(x => x.ORG_CODE == ObjDetail.ORG_CODE
                    && x.TIME_YEAR == ObjDetail.TIME_YEAR
                    && x.DATA_VERSION == ObjDetail.VERSION,
                    x => x.IS_END = false);

                UnitOfWork.Commit();
                SendNotify(Activity.AC_CHUYEN_TKS);

            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                Exception = ex;
                State = false;
            }
        }

        public override void KetThucThamDinh(string orgCode, int year, int version)
        {
            try
            {
                if (!ValidateKetThucThamDinh(orgCode, year, version))
                {
                    return;
                }
                UnitOfWork.BeginTransaction();
                var currentUser = ProfileUtilities.User?.USER_NAME;
                var reviewHeader = UnitOfWork.Repository<KeHoachChiPhiReviewRepo>()
                    .GetFirstWithFetch(x => x.ORG_CODE == orgCode && x.TIME_YEAR == year && x.DATA_VERSION == version && !x.IS_SUMMARY && x.REVIEW_USER == currentUser);
                if (reviewHeader != null)
                {
                    reviewHeader.IS_END = true;
                    UnitOfWork.Repository<KeHoachChiPhiReviewRepo>().Update(reviewHeader);
                }

                var totalReviewUsers = UnitOfWork.Repository<UserReviewRepo>()
                    .GetManyWithFetch(x => x.TIME_YEAR == ObjDetail.TIME_YEAR).Count;

                var totalUsersEndReview = UnitOfWork.Repository<KeHoachChiPhiReviewRepo>()
                    .GetManyWithFetch(x => x.TIME_YEAR == ObjDetail.TIME_YEAR &&
                    x.ORG_CODE == ObjDetail.ORG_CODE &&
                    x.DATA_VERSION == ObjDetail.VERSION &&
                    x.IS_END &&
                    !x.IS_SUMMARY).Count;

                if (totalReviewUsers <= totalUsersEndReview)
                {
                    ObjDetail.STATUS = Approve_Status.ThamDinh_KetThuc;
                    ObjDetail.UPDATE_BY = currentUser;

                    CurrentRepository.Update(ObjDetail);
                }

                UnitOfWork.Repository<KeHoachChiPhiHistoryRepo>()
                    .Create(new T_BP_KE_HOACH_CHI_PHI_HISTORY()
                    {
                        PKID = Guid.NewGuid().ToString(),
                        ORG_CODE = ObjDetail.ORG_CODE,
                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                        VERSION = ObjDetail.VERSION,
                        TIME_YEAR = ObjDetail.TIME_YEAR,
                        ACTION = Approve_Action.KetThucThamDinh,
                        ACTION_USER = currentUser,
                        ACTION_DATE = DateTime.Now,
                        CREATE_BY = currentUser
                    });

                var review = UnitOfWork.Repository<KeHoachChiPhiReviewRepo>()
                    .GetFirstWithFetch(x => x.ORG_CODE == orgCode
                                                && x.TIME_YEAR == year
                                                && x.DATA_VERSION == version
                                                && !x.IS_SUMMARY
                                                && x.REVIEW_USER == currentUser);
                if (review == null)
                {
                    var reviewId = Guid.NewGuid().ToString();
                    UnitOfWork.Repository<KeHoachChiPhiReviewRepo>().Create(
                        new T_BP_KE_HOACH_CHI_PHI_REVIEW
                        {
                            PKID = reviewId,
                            DATA_VERSION = version,
                            IS_END = true,
                            IS_SUMMARY = false,
                            ORG_CODE = orgCode,
                            REVIEW_USER = currentUser,
                            TIME_YEAR = year,
                            CREATE_BY = currentUser,
                        });

                    // create all result not complete
                    var plReviewService = new KeHoachChiPhiReviewService();
                    plReviewService.ObjDetail.TIME_YEAR = year;
                    plReviewService.ObjDetail.ORG_CODE = orgCode;
                    var dataCost = plReviewService.SummaryCenterVersion(out IList<T_BP_KE_HOACH_CHI_PHI_DATA> detailKhoanMucHangHoas);
                    dataCost = dataCost.OrderBy(x => x.C_ORDER)
                                                    .GroupBy(x => x.CODE)
                                                    .Select(x => x.First()).ToList();
                    var elements = plReviewService.PrepareListReview(dataCost);
                    UnitOfWork.Repository<KeHoachChiPhiReviewResultRepo>()
                        .Create((from e in elements.Where(x => !x.IS_GROUP)
                                 select new T_BP_KE_HOACH_CHI_PHI_REVIEW_RESULT
                                 {
                                     PKID = Guid.NewGuid().ToString(),
                                     KHOAN_MUC_HANG_HOA_CODE = e.CODE,
                                     HEADER_ID = reviewId,
                                     RESULT = false,
                                     TIME_YEAR = year,
                                     CREATE_BY = currentUser
                                 }).ToList());
                }
                else
                {
                    review.IS_END = true;
                    UnitOfWork.Repository<KeHoachChiPhiReviewRepo>().Update(review);
                }

                UnitOfWork.Commit();
                SendNotify(Activity.AC_KET_THUC_THAM_DINH);
                if (totalReviewUsers <= totalUsersEndReview)
                {
                    SendNotify(Activity.AC_KET_THUC_TOAN_BO_THAM_DINH);
                }

            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                Exception = ex;
                State = false;
            }
        }

        public override void PheDuyetTongKiemSoat(string orgCode, int year, int version)
        {
            try
            {
                if (!ValidatePheDuyetTKS(orgCode, year, version))
                {
                    return;
                }

                var currentUser = ProfileUtilities.User?.USER_NAME;

                ObjDetail.STATUS = Approve_Status.TKS_PheDuyet;
                ObjDetail.UPDATE_BY = currentUser;

                UnitOfWork.BeginTransaction();

                CurrentRepository.Update(ObjDetail);

                UnitOfWork.Repository<KeHoachChiPhiHistoryRepo>()
                    .Create(new T_BP_KE_HOACH_CHI_PHI_HISTORY()
                    {
                        PKID = Guid.NewGuid().ToString(),
                        ORG_CODE = ObjDetail.ORG_CODE,
                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                        VERSION = ObjDetail.VERSION,
                        TIME_YEAR = ObjDetail.TIME_YEAR,
                        ACTION = Approve_Action.PheDuyetKiemSoat,
                        ACTION_USER = currentUser,
                        ACTION_DATE = DateTime.Now,
                        CREATE_BY = currentUser
                    });

                //TODO: Cập nhật lại code thực thi
                UnitOfWork.GetSession().CreateSQLQuery($"UPDATE T_BP_KE_HOACH_CHI_PHI_DATA SET STATUS = '{Approve_Status.TKS_PheDuyet}' WHERE ORG_CODE = '{ObjDetail.ORG_CODE}' AND TEMPLATE_CODE = '{ObjDetail.TEMPLATE_CODE}' AND TIME_YEAR = {ObjDetail.TIME_YEAR}").ExecuteUpdate();

                UnitOfWork.Commit();

                SendNotify(Activity.AC_PHE_DUYET_TKS);

            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                Exception = ex;
                State = false;
            }
        }
        internal override void TuChoiTongKiemSoat(string orgCode, int year, int version, string comment)
        {
            try
            {
                if (!ValidateTuChoiTKS(orgCode, year, version))
                {
                    return;
                }

                var currentUser = ProfileUtilities.User?.USER_NAME;

                ObjDetail.STATUS = Approve_Status.TKS_TuChoi;
                ObjDetail.UPDATE_BY = currentUser;

                UnitOfWork.BeginTransaction();

                CurrentRepository.Update(ObjDetail);

                var note = $"Yêu cầu điều chỉnh lại dữ liệu TKS. Lý do: [{comment}]";
                UnitOfWork.Repository<KeHoachChiPhiHistoryRepo>()
                    .Create(new T_BP_KE_HOACH_CHI_PHI_HISTORY()
                    {
                        PKID = Guid.NewGuid().ToString(),
                        ORG_CODE = ObjDetail.ORG_CODE,
                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                        NOTES = note,
                        VERSION = ObjDetail.VERSION,
                        TIME_YEAR = ObjDetail.TIME_YEAR,
                        ACTION = Approve_Action.TuChoiKiemSoat,
                        ACTION_USER = currentUser,
                        ACTION_DATE = DateTime.Now,
                        CREATE_BY = currentUser
                    });

                //TODO: Cập nhật lại code thực thi
                UnitOfWork.GetSession().CreateSQLQuery($"UPDATE T_BP_KE_HOACH_CHI_PHI_DATA SET STATUS = '{Approve_Status.TKS_TuChoi}' WHERE ORG_CODE = '{ObjDetail.ORG_CODE}' AND TEMPLATE_CODE = '{ObjDetail.TEMPLATE_CODE}' AND TIME_YEAR = {ObjDetail.TIME_YEAR}").ExecuteUpdate();
                UnitOfWork.Repository<KeHoachChiPhiReviewRepo>()
                    .Update(x => x.ORG_CODE == orgCode
                                && x.TIME_YEAR == year
                                && x.DATA_VERSION == version
                                && x.IS_SUMMARY, x => x.IS_END = false);
                UnitOfWork.Commit();

                SendNotify(Activity.AC_TU_CHOI_TKS);

            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                Exception = ex;
                State = false;
            }
        }

        public override void TrinhDuyetTongKiemSoat(string orgCode, int year, int version)
        {
            try
            {
                if (!ValidateTrinhDuyetTKS(orgCode, year, version))
                {
                    return;
                }
                var currentUser = ProfileUtilities.User?.USER_NAME;
                UnitOfWork.BeginTransaction();

                ObjDetail.STATUS = Approve_Status.TKS_TrinhDuyet;
                ObjDetail.UPDATE_BY = currentUser;

                CurrentRepository.Update(ObjDetail);

                UnitOfWork.Repository<KeHoachChiPhiHistoryRepo>()
                    .Create(new T_BP_KE_HOACH_CHI_PHI_HISTORY()
                    {
                        PKID = Guid.NewGuid().ToString(),
                        ORG_CODE = ObjDetail.ORG_CODE,
                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                        VERSION = ObjDetail.VERSION,
                        TIME_YEAR = ObjDetail.TIME_YEAR,
                        ACTION = Approve_Action.TrinhDuyetKiemSoat,
                        ACTION_USER = currentUser,
                        ACTION_DATE = DateTime.Now,
                        CREATE_BY = currentUser
                    });
                //TODO: Cập nhật lại code thực thi
                UnitOfWork.GetSession().CreateSQLQuery($"UPDATE T_BP_KE_HOACH_CHI_PHI_DATA SET STATUS = '{Approve_Status.TKS_TrinhDuyet}' WHERE ORG_CODE = '{ObjDetail.ORG_CODE}' AND TEMPLATE_CODE = '{ObjDetail.TEMPLATE_CODE}' AND TIME_YEAR = {ObjDetail.TIME_YEAR}").ExecuteUpdate();
                UnitOfWork.Repository<KeHoachChiPhiReviewRepo>()
                    .Update(x => x.ORG_CODE == orgCode
                                && x.TIME_YEAR == year
                                && x.DATA_VERSION == version
                                && x.IS_SUMMARY, x => x.IS_END = true);

                UnitOfWork.Commit();

                SendNotify(Activity.AC_TRINH_DUYET_TKS);

            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                Exception = ex;
                State = false;
            }
        }

        public override void YeuCauCapDuoiDieuChinh(string childOrgCode, string templateCode, int timeYear, string comment, int? templateVersion, int? parentVersion, bool isSummaryReview)
        {
            try
            {
                var currentUser = ProfileUtilities.User?.USER_NAME;
                // Tìm version hiện tại của đơn vị cha
                var parentOrgCode = ProfileUtilities.User.ORGANIZE_CODE;
                var findParent = GetFirstByExpression(x => x.ORG_CODE == parentOrgCode && x.TIME_YEAR == timeYear);
                var findChild = GetFirstByExpression(x => x.ORG_CODE == childOrgCode && x.TIME_YEAR == timeYear);
                if (!ValidateYeuCauDieuChinh(timeYear, parentOrgCode, isSummaryReview))
                {
                    return;
                }
                if (!string.IsNullOrWhiteSpace(templateCode))
                {
                    findChild = GetFirstByExpression(x => x.ORG_CODE == childOrgCode && x.TEMPLATE_CODE == templateCode && x.TIME_YEAR == timeYear);
                }
                var childOrg = UnitOfWork.Repository<CostCenterRepo>().Get(childOrgCode);
                var parentOrg = UnitOfWork.Repository<CostCenterRepo>().Get(parentOrgCode);
                var template = UnitOfWork.Repository<TemplateRepo>().Get(templateCode);
                var noteParent = $"Yêu cầu đơn vị [{childOrg.NAME}] cập nhật lại kế hoạch của mẫu [{(string.IsNullOrWhiteSpace(templateCode) ? "Dữ liệu tổng hợp" : $"{template.CODE}-{template.NAME}")}]. Lý do: [{comment}]";

                UnitOfWork.BeginTransaction();
                // Tạo history của đơn vị cha
                UnitOfWork.Repository<KeHoachChiPhiHistoryRepo>()
                    .Create(new T_BP_KE_HOACH_CHI_PHI_HISTORY()
                    {
                        PKID = Guid.NewGuid().ToString(),
                        ORG_CODE = parentOrgCode,
                        TEMPLATE_CODE = templateCode,
                        VERSION = parentVersion ?? (findParent == null ? 0 : findParent.VERSION),
                        TIME_YEAR = timeYear,
                        ACTION = Approve_Action.YeuCauCapDuoiDieuChinh,
                        ACTION_USER = currentUser,
                        ACTION_DATE = DateTime.Now,
                        CREATE_BY = currentUser,
                        NOTES = noteParent
                    });

                // Tạo history của đơn vị con
                var noteChild = $"Đơn vị [{parentOrg.NAME}] yêu cầu cập nhật lại kế hoạch mẫu [{(string.IsNullOrWhiteSpace(templateCode) ? "Dữ liệu tổng hợp" : $"{template.CODE}-{template.NAME}")}]. Lý do: [{comment}]";
                UnitOfWork.Repository<KeHoachChiPhiHistoryRepo>()
                    .Create(new T_BP_KE_HOACH_CHI_PHI_HISTORY()
                    {
                        PKID = Guid.NewGuid().ToString(),
                        ORG_CODE = childOrgCode,
                        TEMPLATE_CODE = templateCode,
                        VERSION = templateVersion ?? (findChild == null ? 0 : findChild.VERSION),
                        TIME_YEAR = timeYear,
                        ACTION = Approve_Action.CapTrenYeuCauDieuChinh,
                        ACTION_USER = currentUser,
                        ACTION_DATE = DateTime.Now,
                        CREATE_BY = currentUser,
                        NOTES = noteChild
                    });
                UnitOfWork.Commit();

                NotifyUtilities.CreateNotifyYeuCauCapDuoiDieuChinh(
                   new NotifyPara()
                   {
                       Activity = Activity.AC_YEU_CAU_CAP_DUOI_DIEU_CHINH,
                       OrgCode = ProfileUtilities.User.ORGANIZE_CODE,
                       ChildOrgCode = childOrgCode,
                       TemplateCode = templateCode,
                       TimeYear = timeYear,
                       ModulType = ModulType.KeHoachChiPhi,
                       Note = noteChild,
                       UserSent = currentUser
                   });
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                Exception = ex;
                State = false;
            }
        }

        public override void ChuyenHDNS(string code)
        {
            try
            {
                var currentUser = ProfileUtilities.User?.USER_NAME;
                if (!ValidateChuyenHDNS(code))
                {
                    return;
                }
                ObjDetail.STATUS = Approve_Status.ThamDinh_DuLieu;
                ObjDetail.UPDATE_BY = currentUser;

                UnitOfWork.BeginTransaction();

                CurrentRepository.Update(ObjDetail);

                UnitOfWork.Repository<KeHoachChiPhiHistoryRepo>()
                    .Create(new T_BP_KE_HOACH_CHI_PHI_HISTORY()
                    {
                        PKID = Guid.NewGuid().ToString(),
                        ORG_CODE = ObjDetail.ORG_CODE,
                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                        VERSION = ObjDetail.VERSION,
                        TIME_YEAR = ObjDetail.TIME_YEAR,
                        ACTION = Approve_Action.ChuyenHDNS,
                        ACTION_USER = currentUser,
                        ACTION_DATE = DateTime.Now,
                        CREATE_BY = currentUser
                    });
                // cập nhật dữ liệu kết thúc thẩm định sang chưa kết thúc nếu là chuyển thẳng lên HDNS
                if (ObjDetail.IS_REVIEWED)
                {
                    UnitOfWork.Repository<KeHoachChiPhiReviewRepo>().Update(x => x.DATA_VERSION == ObjDetail.VERSION
                    && x.TIME_YEAR == ObjDetail.TIME_YEAR && !x.IS_SUMMARY && x.ORG_CODE == ObjDetail.ORG_CODE, x => x.IS_END = false);
                }
                UnitOfWork.Commit();
                SendNotify(Activity.AC_CHUYEN_HDNS);

            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                Exception = ex;
                State = false;
            }
        }

        public override void TrinhTGD(string code)
        {
            try
            {
                var currentUser = ProfileUtilities.User?.USER_NAME;
                if (!ValidateTrinhTGD(code))
                {
                    return;
                }

                ObjDetail.STATUS = Approve_Status.TGD_ChoPheDuyet;
                ObjDetail.UPDATE_BY = currentUser;

                UnitOfWork.BeginTransaction();

                CurrentRepository.Update(ObjDetail);

                UnitOfWork.Repository<KeHoachChiPhiHistoryRepo>()
                    .Create(new T_BP_KE_HOACH_CHI_PHI_HISTORY()
                    {
                        PKID = Guid.NewGuid().ToString(),
                        ORG_CODE = ObjDetail.ORG_CODE,
                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                        VERSION = ObjDetail.VERSION,
                        TIME_YEAR = ObjDetail.TIME_YEAR,
                        ACTION = Approve_Action.TrinhTGD,
                        ACTION_USER = currentUser,
                        ACTION_DATE = DateTime.Now,
                        CREATE_BY = currentUser
                    });

                UnitOfWork.Commit();

                SendNotify(Activity.AC_TRINH_TGD);

            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                Exception = ex;
                State = false;
            }
        }

        #endregion


        #region Xử lý dữ liệu tại mỗi đơn vị

        /// <summary>
        /// Danh sách dữ liệu của cấp dưới
        /// </summary>
        public override void GetListOfChild()
        {
            var orgCode = ProfileUtilities.User.ORGANIZE_CODE;
            // Tìm tất cả đơn vị con
            var lstChildOrg = this.UnitOfWork.Repository<CostCenterRepo>().Queryable().Where(
                    x => x.PARENT_CODE == orgCode
                ).Select(x => x.CODE).ToList();
            // Tìm ra tất cả các mẫu đã nộp của đơn vị con

            this.ObjList = this.CurrentRepository.Queryable().Where(
                    x => lstChildOrg.Contains(x.ORG_CODE) && x.TIME_YEAR == this.ObjDetail.TIME_YEAR
                ).ToList();

            // Tìm mẫu nộp hộ
            var listTemplateCode = this.UnitOfWork.Repository<TemplateDetailKeHoachChiPhiRepo>().Queryable().Where(
                    x => lstChildOrg.Contains(x.CENTER_CODE) && x.TIME_YEAR == this.ObjDetail.TIME_YEAR
                    && x.TIME_YEAR == (ObjDetail.TIME_YEAR == 0 ? DateTime.Now.Year : ObjDetail.TIME_YEAR)
                ).Select(x => x.TEMPLATE_CODE).Distinct().ToList();
            var findKeHoachChiPhi = this.CurrentRepository.Queryable().Where(
                    x => listTemplateCode.Contains(x.TEMPLATE_CODE) && !lstChildOrg.Contains(x.ORG_CODE) && x.TIME_YEAR == this.ObjDetail.TIME_YEAR
                );

            this.ObjList.AddRange(findKeHoachChiPhi);
            if (string.IsNullOrEmpty(ObjDetail.KICH_BAN))
            {
                ObjDetail.KICH_BAN = "TB";
            }
            if (string.IsNullOrEmpty(ObjDetail.PHIEN_BAN))
            {
                ObjDetail.PHIEN_BAN = "PB1";
            }
            this.ObjList = this.ObjList.Where(x => x.KICH_BAN == this.ObjDetail.KICH_BAN && x.PHIEN_BAN == this.ObjDetail.PHIEN_BAN).OrderBy(x => x.ORG_CODE).ThenBy(x => x.TEMPLATE_CODE).ToList();
        }

        /// <summary>
        /// Lấy lịch sử tổng hợp của một đơn vị
        /// </summary>
        /// <param name="orgCode"></param>
        public override void GetSumUpHistory(string orgCode, int year = 0, int version = 0)
        {
            var query = UnitOfWork.GetSession().QueryOver<T_BP_KE_HOACH_CHI_PHI_SUM_UP_DETAIL>();
            query = query.Where(x => x.ORG_CODE == orgCode);

            if (year != 0)
            {
                query = query.Where(x => x.TIME_YEAR == year);
            }

            if (version != 0)
            {
                query = query.Where(x => x.SUM_UP_VERSION == version);
            }

            query = query.Fetch(x => x.CostCenter).Eager
                .Fetch(x => x.FromCostCenter).Eager
                .Fetch(x => x.Template).Eager;
            this.ObjListSumUpHistory = query.List().ToList();
        }

        /// <summary>
        /// Lấy dữ liệu được sum up lên đơn vị theo version và khoản mục
        /// </summary>
        /// <param name="year"></param>
        /// <param name="centerCode"></param>
        /// <param name="elementCode"></param>
        /// <param name="sumUpVersion"></param>
        /// <returns></returns>
        private (IList<T_BP_KE_HOACH_CHI_PHI_DATA>, bool) GetDataSumUp(int year, string centerCode, string elementCode, int sumUpVersion)
        {
            var plDataRepo = UnitOfWork.Repository<KeHoachChiPhiDataRepo>();
            var costPlReviewCommentRepo = UnitOfWork.Repository<KeHoachChiPhiReviewCommentRepo>();
            var plVersionRepo = UnitOfWork.Repository<KeHoachChiPhiRepo>();
            var plDataHistoryRepo = UnitOfWork.Repository<KeHoachChiPhiDataHistoryRepo>();

            var lstDetails = UnitOfWork.Repository<KeHoachChiPhiSumUpDetailRepo>()
                .GetManyByExpression(x => x.ORG_CODE == centerCode &&
                    x.TIME_YEAR == year &&
                    x.SUM_UP_VERSION == sumUpVersion);
            var lookup = lstDetails.ToLookup(x => x.FROM_ORG_CODE);
            var lstChildren = GetListOfChildrenCenter(centerCode).Select(x => x.CODE);

            var isCorp = string.IsNullOrEmpty(GetCenter(centerCode).PARENT_CODE);
            var isLeafCenter = IsLeaf(centerCode);

            var data = new List<T_BP_KE_HOACH_CHI_PHI_DATA>();
            foreach (var key in lookup.Select(x => x.Key))
            {
                var costPL = new List<T_BP_KE_HOACH_CHI_PHI_DATA>();
                var isLeaf = IsLeaf(key);
                if (isLeaf)
                {
                    foreach (var item in lookup[key])
                    {
                        if (lstChildren.Contains(GetTemplate(item.TEMPLATE_CODE).ORG_CODE))
                        {
                            // đơn vị con tự nộp
                            costPL.AddRange(plDataRepo.GetCFDataByCenterCode(item.FROM_ORG_CODE, lstChildren.ToList(), year, item.TEMPLATE_CODE, item.DATA_VERSION).ToList());
                        }
                        else
                        {
                            // được nộp hộ
                            var dataFromOrg = plDataRepo.GetCFDataByOrgCode(item.FROM_ORG_CODE, year, item.TEMPLATE_CODE, item.DATA_VERSION).ToList();
                            costPL.AddRange(dataFromOrg.Where(x => lstChildren.Contains(x.ORG_CODE)));
                        }
                    }
                }
                else
                {
                    if (isCorp)
                    {
                        foreach (var item in lookup[key])
                        {
                            costPL.AddRange(plDataRepo.GetCFDataByOrgCode(key, year, string.Empty, item.DATA_VERSION).ToList());
                        }
                    }
                    else
                    {
                        costPL = plDataRepo.GetCFDataByCenterCode(key, lstChildren.ToList(), year, null, sumUpVersion).ToList();
                    }
                }
                data.AddRange(costPL.Where(x => x.KHOAN_MUC_HANG_HOA_CODE == elementCode &&
                lookup[key].Select(y => y.TEMPLATE_CODE).Contains(x.TEMPLATE_CODE)));
                if (data == null || data.Count() == 0)
                {
                    continue;
                }
            }
            return (data, isCorp);
        }

        /// <summary>
        /// Get list of cost elements have summed up
        /// </summary>
        /// <param name="centerCode">Cost center code has summed up</param>
        /// <param name="elementCode">Element code want to get detail</param>
        /// <param name="year">Year summed up</param>
        /// <param name="version">Version summed up</param>
        /// <returns>Returns list of cost elements have summed up</returns>
        public override IEnumerable<T_MD_KHOAN_MUC_HANG_HOA> GetDetailSumUp(string centerCode, string elementCode, int year, int version, int? sumUpVersion, bool isCountComments, bool? isShowFile = null)
        {
            var plDataRepo = UnitOfWork.Repository<KeHoachChiPhiDataRepo>();
            var costPlReviewCommentRepo = UnitOfWork.Repository<KeHoachChiPhiReviewCommentRepo>();
            var plVersionRepo = UnitOfWork.Repository<KeHoachChiPhiRepo>();
            var plDataHistoryRepo = UnitOfWork.Repository<KeHoachChiPhiDataHistoryRepo>();

            if (IsLeaf(centerCode))
            {
                var parentCenterCode = GetCenter(centerCode).PARENT_CODE;
                var lstChildren = GetListOfChildrenCenter(parentCenterCode).Select(x => x.CODE);

                var (data, isCorp) = GetDataSumUp(year, parentCenterCode, elementCode, sumUpVersion.Value);
                var lookupData = data.Where(x => x.ORG_CODE == centerCode).ToLookup(x => x.TEMPLATE_CODE);
                foreach (var key in lookupData.Select(x => x.Key))
                {
                    foreach (var item in lookupData[key])
                    {
                        var element = (T_MD_KHOAN_MUC_HANG_HOA)item;
                        element.IS_GROUP = isShowFile.HasValue; // count comment is in the review, drill down will show the file base so it still will be group
                        yield return element;
                    }
                    //if (lookupData[key].First().Template.IS_BASE)
                    //{
                    //    var isNewestVersion = UnitOfWork.Repository<KeHoachChiPhiVersionRepo>()
                    //        .GetNewestByExpression(x => x.TEMPLATE_CODE == key && x.TIME_YEAR == year,
                    //        order: x => x.VERSION, isDescending: true)
                    //        .VERSION == lookupData[key].First().VERSION;
                    //    foreach (var item in lookupData[key])
                    //    {
                    //        var lstElements = lookupData[key].Where(x => x.CHI_PHI_PROFIT_CENTER_CODE == item.CHI_PHI_PROFIT_CENTER_CODE);
                    //        var element = (T_MD_KHOAN_MUC_HANG_HOA)item;
                    //        element.IsBase = true;
                    //        element.Values = new decimal[14]
                    //        {
                    //            lstElements.Sum(x => x.VALUE_JAN) ?? 0,
                    //            lstElements.Sum(x => x.VALUE_FEB) ?? 0,
                    //            lstElements.Sum(x => x.VALUE_MAR) ?? 0,
                    //            lstElements.Sum(x => x.VALUE_APR) ?? 0,
                    //            lstElements.Sum(x => x.VALUE_MAY) ?? 0,
                    //            lstElements.Sum(x => x.VALUE_JUN) ?? 0,
                    //            lstElements.Sum(x => x.VALUE_JUL) ?? 0,
                    //            lstElements.Sum(x => x.VALUE_AUG) ?? 0,
                    //            lstElements.Sum(x => x.VALUE_SEP) ?? 0,
                    //            lstElements.Sum(x => x.VALUE_OCT) ?? 0,
                    //            lstElements.Sum(x => x.VALUE_NOV) ?? 0,
                    //            lstElements.Sum(x => x.VALUE_DEC) ?? 0,
                    //            lstElements.Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                    //            lstElements.Sum(x => x.VALUE_SUM_YEAR_PREVENTIVE) ?? 0,
                    //        };
                    //        element.NAME = item.Template.NAME;
                    //        yield return element;
                    //    }
                    //    if (isNewestVersion)
                    //    {
                    //        // get data from base
                    //        var baseData = UnitOfWork.Repository<KeHoachChiPhiDataBaseRepo>()
                    //            .GetManyWithFetch(x => x.KHOAN_MUC_HANG_HOA_CODE == elementCode && x.ORG_CODE == centerCode && x.TEMPLATE_CODE == key && x.VERSION == lookupData[key].First().VERSION && x.TIME_YEAR == year);
                    //        foreach (var item in baseData)
                    //        {
                    //            yield return (T_MD_KHOAN_MUC_HANG_HOA)item;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        // get data from base history
                    //        var baseDataHistory = UnitOfWork.Repository<KeHoachChiPhiDataBaseHistoryRepo>()
                    //            .GetManyWithFetch(x => x.KHOAN_MUC_HANG_HOA_CODE == elementCode && x.ORG_CODE == centerCode && x.TEMPLATE_CODE == key && x.VERSION == lookupData[key].First().VERSION && x.TIME_YEAR == year);

                    //        foreach (var item in baseDataHistory)
                    //        {
                    //            yield return (T_MD_KHOAN_MUC_HANG_HOA)item;
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    foreach (var item in lookupData[key])
                    //    {
                    //        var element = (T_MD_KHOAN_MUC_HANG_HOA)item;
                    //        element.IS_GROUP = isShowFile.HasValue; // count comment is in the review, drill down will show the file base so it still will be group
                    //        yield return element;
                    //    }
                    //}
                }
            }
            else
            {
                var comments = new List<T_BP_KE_HOACH_CHI_PHI_REVIEW_COMMENT>();
                if (isCountComments)
                {
                    comments = costPlReviewCommentRepo.GetManyWithFetch(
                            x => x.TIME_YEAR == year &&
                            x.ORG_CODE == GetCorp().CODE &&
                            x.KHOAN_MUC_HANG_HOA_CODE == elementCode).ToList();
                }
                var (data, isCorp) = GetDataSumUp(year, centerCode, elementCode, version);
                var lookupData = data.ToLookup(x => x.ORG_CODE);
                foreach (var key in lookupData.Select(x => x.Key))
                {
                    IEnumerable<string> lstChildren = null;
                    var childComments = 0;
                    var parentComments = 0;
                    if (isCountComments)
                    {
                        lstChildren = GetListOfChildrenCenter(key).Select(x => x.CODE);
                        childComments = comments.Where(x => lstChildren.Contains(x.ON_ORG_CODE)).Sum(x => x.NUMBER_COMMENTS);
                        parentComments = comments.Where(x => x.ON_ORG_CODE == key).Sum(x => x.NUMBER_COMMENTS);
                    }
                    var isChild = IsLeaf(key);
                    yield return new T_MD_KHOAN_MUC_HANG_HOA
                    {
                        ORG_CODE = key,
                        CENTER_CODE = key,
                        CODE = elementCode,
                        IsChildren = isChild,
                        //IS_GROUP = IsLeaf(key) ? lookupData[key].First().Template.IS_BASE ? true : false : true,
                        IS_GROUP = true,
                        TEMPLATE_CODE = lookupData[key].First().TEMPLATE_CODE,
                        TIME_YEAR = year,
                        VERSION = isCorp ? lookupData[key].First().VERSION : version, // cấp tập đoàn thì sẽ là data_version còn nếu cấp dưới thì sẽ là sumup version
                        ORG_NAME = lookupData[key].First().Organize.NAME,
                        Values = new decimal[3]
                            {
                                lookupData[key].Sum(x => x.QUANTITY) ?? 0,
                                lookupData[key].Sum(x => x.PRICE) ?? 0,
                                lookupData[key].Sum(x => x.AMOUNT) ?? 0,
                            },
                        NUMBER_COMMENTS = isCountComments ? isChild ?
                        $"{parentComments}" :
                        $"{parentComments + childComments}|{parentComments}" : $"{childComments}|0"
                    };
                }
            }
        }


        public override IEnumerable<T_MD_KHOAN_MUC_HANG_HOA> GetDetailSumUpTemplate(string elementCode, int year, int version, string templateCode, string centerCode)
        {
            var newestVersionInDb = GetFirstWithFetch(x => x.TEMPLATE_CODE == templateCode && x.TIME_YEAR == year)?.VERSION;
            if (!newestVersionInDb.HasValue)
            {
                return null;
            }
            else
            {
                if (newestVersionInDb.Value == version)
                {
                    // newest data
                    // get data in table base data
                    var baseData = UnitOfWork.Repository<KeHoachChiPhiDataBaseRepo>()
                        .GetManyWithFetch(x => x.TEMPLATE_CODE == templateCode
                        && x.TIME_YEAR == year
                        && x.KHOAN_MUC_HANG_HOA_CODE == elementCode
                        && x.VERSION == version
                        && x.CHI_PHI_PROFIT_CENTER_CODE == centerCode);
                    return from item in baseData
                           select (T_MD_KHOAN_MUC_HANG_HOA)item;
                }
                else
                {
                    // old data
                    // get data in table base data history
                    var baseData = UnitOfWork.Repository<KeHoachChiPhiDataBaseHistoryRepo>()
                        .GetManyWithFetch(x => x.TEMPLATE_CODE == templateCode
                        && x.TIME_YEAR == year
                        && x.KHOAN_MUC_HANG_HOA_CODE == elementCode
                        && x.VERSION == version
                        && x.CHI_PHI_PROFIT_CENTER_CODE == centerCode);
                    return from item in baseData
                           select (T_MD_KHOAN_MUC_HANG_HOA)item;
                }
            }
        }

        public IList<T_MD_KHOAN_MUC_HANG_HOA> GetDataCost(out IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_CHI_PHI> detailOtherKhoanMucHangHoas,
            out IList<T_BP_KE_HOACH_CHI_PHI_DATA> detailOtherCostData,
            out bool isDrillDownApply,
            ViewDataCenterModel model)
        {
            isDrillDownApply = model.IS_DRILL_DOWN;
            if (!model.IS_HAS_NOT_VALUE && !model.IS_HAS_VALUE &&
                (!string.IsNullOrEmpty(model.TEMPLATE_CODE) || model.VERSION == null || model.VERSION.Value == -1))
            {
                detailOtherCostData = null;
                detailOtherKhoanMucHangHoas = null;
                return null;
            }
            var isHasValue = model.IS_HAS_VALUE ? model.IS_HAS_NOT_VALUE ? (bool?)null : true : false;
            var isParent = !IsLeaf(model.ORG_CODE);
            if (!string.IsNullOrEmpty(model.TEMPLATE_CODE))
            {
                // view template Data
                detailOtherCostData = null;
                var lstOrgs = new List<string>();
                if (isParent)
                {
                    lstOrgs.AddRange(GetListOfChildrenCenter(model.ORG_CODE).Select(x => x.CODE));
                }
                else
                {
                    lstOrgs.Add(model.ORG_CODE);
                }
                var elements = GetDataCostPreview(out detailOtherKhoanMucHangHoas, model.TEMPLATE_CODE, lstOrgs, model.YEAR, model.VERSION, isHasValue);
                var sumElements = new T_MD_KHOAN_MUC_HANG_HOA
                {
                    // set tổng năm
                    // tạo element tổng
                    NAME = "TỔNG CỘNG",
                    LEVEL = 0,
                    PARENT_CODE = null,
                    IS_GROUP = true,
                    IsChildren = false,
                    C_ORDER = 0,
                    CODE = string.Empty,
                    Values = new decimal[3]
                };
                //var isTemplateBase = GetTemplate(model.TEMPLATE_CODE)?.IS_BASE;
                //isTemplateBase = isTemplateBase.HasValue && isTemplateBase.Value;
                foreach (var item in elements.Distinct().Where(x => !x.IS_GROUP))
                {
                    //if (isTemplateBase.Value && item.Values.Sum() > 0)
                    //{
                    //    item.IsChildren = true;
                    //}
                    for (int i = 0; i < sumElements.Values.Length; i++)
                    {
                        sumElements.Values[i] += item.Values[i];
                    }
                }
                elements.Add(sumElements);
                return elements;
            }
            else if (model.VERSION == null || model.VERSION.Value == -1)
            {
                // xem dữ liệu trước khi tổng hợp
                detailOtherKhoanMucHangHoas = null;
                // disabled drill down
                isDrillDownApply = false;
                return SummarySumUpCenter(out detailOtherCostData, model.YEAR, model.ORG_CODE, null, isHasValue, templateId: null);
            }
            else
            {
                // xem dữ liệu được tổng hợp cho đơn vị
                detailOtherKhoanMucHangHoas = null;
                return SummaryCenterVersion(out detailOtherCostData, model.ORG_CODE, model.YEAR, model.VERSION, model.IS_DRILL_DOWN);
            }
        }

        public override IList<T_MD_KHOAN_MUC_HANG_HOA> GetDetailPreviewSumUp(string centerCode, string elementCode, int year)
        {
            var plVersionRepo = UnitOfWork.Repository<KeHoachChiPhiRepo>();
            var plDataRepo = UnitOfWork.Repository<KeHoachChiPhiDataRepo>();
            var plDataHistoryRepo = UnitOfWork.Repository<KeHoachChiPhiDataHistoryRepo>();

            var plDataOtherKhoanMucHangHoas = UnitOfWork.Repository<KeHoachChiPhiDataRepo>().GetCFDataByCenterCode(null, new List<string> { centerCode }, year, null, null);

            return plDataOtherKhoanMucHangHoas.Where(x => x.KHOAN_MUC_HANG_HOA_CODE == elementCode)
                .Select(x => (T_MD_KHOAN_MUC_HANG_HOA)x)
                .OrderBy(x => x.C_ORDER).ToList();
        }

        private IList<T_MD_KHOAN_MUC_HANG_HOA> GetDataCostPreview(out IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_CHI_PHI> detailOtherKhoanMucHangHoas, string templateId, List<string> lstOrgs, int year, int? version, bool? isHasValue)
        {
            detailOtherKhoanMucHangHoas = new List<T_MD_TEMPLATE_DETAIL_KE_HOACH_CHI_PHI>();
            var lstElements = new List<T_MD_KHOAN_MUC_HANG_HOA>();
            foreach (var orgCode in lstOrgs)
            {
                var elements = GetDataCostPreview(out IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_CHI_PHI> detail, templateId, orgCode, year, version, isHasValue);
                detailOtherKhoanMucHangHoas = SummaryElement(detailOtherKhoanMucHangHoas, detail);
                lstElements = SummaryUpElement(lstElements, elements).ToList();
            }
            return lstElements;
        }

        /// <summary>
        /// Tổng hợp 2 list elements. Nếu có rồi thì cộng tổng values
        /// </summary>
        /// <param name="lst1"></param>
        /// <param name="lst2"></param>
        /// <returns></returns>
        private IList<T_MD_KHOAN_MUC_HANG_HOA> SummaryUpElement(IList<T_MD_KHOAN_MUC_HANG_HOA> lst1, IList<T_MD_KHOAN_MUC_HANG_HOA> lst2)
        {
            if (lst1.Count == 0)
            {
                return lst2;
            }
            if (lst2.Count == 0)
            {
                return lst1;
            }

            var result = lst2.ToList();
            foreach (var item in lst1)
            {
                var index = result.FindIndex(x => x.CODE == item.CODE && x.CENTER_CODE == item.CENTER_CODE && x.TIME_YEAR == item.TIME_YEAR);
                if (index > 0)
                {
                    for (int i = 0; i < 14; i++)
                    {
                        result[index].Values[i] += item.Values[i];
                    }
                }
                else
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public override T_BP_KE_HOACH_CHI_PHI_VERSION GetHeader(string templateCode, string centerCode, int year, int? version)
        {
            templateCode = templateCode ?? string.Empty;
            if (version == null)
            {
                return UnitOfWork.Repository<KeHoachChiPhiVersionRepo>()
                .GetManyByExpression(x =>
                x.TIME_YEAR == year &&
                x.ORG_CODE == centerCode &&
                x.TEMPLATE_CODE == templateCode).OrderByDescending(x => x.VERSION).FirstOrDefault();
            }
            else
            {
                if (!string.IsNullOrEmpty(templateCode))
                {
                    return UnitOfWork.Repository<KeHoachChiPhiVersionRepo>()
                    .GetFirstByExpression(x =>
                    x.TIME_YEAR == year &&
                    x.VERSION == version &&
                    x.TEMPLATE_CODE == templateCode);
                }
                else
                {
                    return UnitOfWork.Repository<KeHoachChiPhiVersionRepo>()
                    .GetFirstByExpression(x =>
                    x.TIME_YEAR == year &&
                    x.VERSION == version &&
                    x.ORG_CODE == centerCode &&
                    x.TEMPLATE_CODE == templateCode);
                }
            }
        }

        public override void GetHistoryVersion(string orgCode, string templateId, int year)
        {
            ObjListVersion = UnitOfWork.Repository<KeHoachChiPhiVersionRepo>()
                .GetManyWithFetch(x => x.ORG_CODE == orgCode
                && x.TEMPLATE_CODE == templateId
                && x.TIME_YEAR == year, x => x.FileUpload).ToList();
        }

        #endregion

        /// <summary>
        /// Lấy tất cả lịch sử dữ liệu trong năm của một đơn vị
        /// </summary>
        /// <param name="orgCode"></param>
        /// <param name="year"></param>
        public override void GetHistory(string orgCode, int year)
        {
            if (ObjDetail.KICH_BAN == null)
            {
                ObjDetail.KICH_BAN = "TB";
            }
            if (ObjDetail.PHIEN_BAN == null)
            {
                ObjDetail.PHIEN_BAN = "PB1";
            }
            var query = this.UnitOfWork.GetSession().QueryOver<T_BP_KE_HOACH_CHI_PHI_HISTORY>();
            query = query.Where(x => x.ORG_CODE == orgCode && x.TIME_YEAR == year && x.KICH_BAN == ObjDetail.KICH_BAN && x.PHIEN_BAN == ObjDetail.PHIEN_BAN)
                .Fetch(x => x.Template).Eager
                .Fetch(x => x.USER_CREATE).Eager;
            this.ObjListHistory = query.List().ToList();
        }

        public override void GetHistory()
        {
            this.Get(this.ObjDetail.PKID);
            GetHistory(this.ObjDetail.ORG_CODE, this.ObjDetail.TEMPLATE_CODE, this.ObjDetail.TIME_YEAR);
        }

        public override void GetHistory(string orgCode, string templateId, int? year)
        {
            if (year.HasValue)
            {
                this.ObjListHistory = UnitOfWork.Repository<KeHoachChiPhiHistoryRepo>().GetManyByExpression(
                    x => x.ORG_CODE == orgCode &&
                    x.TEMPLATE_CODE == templateId &&
                    x.TIME_YEAR == year.Value
                ).ToList();
            }
            else
            {
                this.ObjListHistory = UnitOfWork.Repository<KeHoachChiPhiHistoryRepo>().GetManyByExpression(
                   x => x.ORG_CODE == orgCode &&
                   x.TEMPLATE_CODE == templateId
               ).ToList();
            }
        }

        #region Import data
        /// <summary>
        /// Kiểm tra có đúng mẫu hay không
        /// Dữ liệu nộp có đúng là số hay không
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="isDataBase">Có phải dữ liệu cơ sở hay không</param>
        public override void ValidateData(DataTable dataTable, bool isDataBase)
        {
            var periodTime = this.UnitOfWork.Repository<PeriodTimeRepo>().Get(this.ObjDetail.TIME_YEAR);
            if (periodTime == null || periodTime.IS_CLOSE)
            {
                this.State = false;
                this.ErrorMessage = $"Năm kế hoạch {this.ObjDetail.TIME_YEAR} đã bị đóng!";
                return;
            }

            if (string.IsNullOrWhiteSpace(this.ObjDetail.TEMPLATE_CODE))
            {
                this.State = false;
                this.ErrorMessage = $"Bạn chưa chọn mẫu khai báo!";
                return;
            }

            var template = this.UnitOfWork.Repository<TemplateRepo>().Get(this.ObjDetail.TEMPLATE_CODE);

            // Kiểm tra template có phải của đơn vị không
            if (template == null || template.ORG_CODE != ProfileUtilities.User.ORGANIZE_CODE)
            {
                this.State = false;
                this.ErrorMessage = "Mẫu khai báo này không phải của đơn vị bạn!";
                return;
            }


            var lstDetailTemplate = this.UnitOfWork.Repository<TemplateDetailKeHoachChiPhiRepo>()
                .GetManyWithFetch(x => x.TIME_YEAR == this.ObjDetail.TIME_YEAR && x.TEMPLATE_CODE == this.ObjDetail.TEMPLATE_CODE,
                x => x.Center);

            var lstElement = this.UnitOfWork.Repository<KhoanMucHangHoaRepo>().GetManyByExpression(x => x.TIME_YEAR == this.ObjDetail.TIME_YEAR);

            // Kiểm tra mẫu này đã được thiết kế cho năm ngân sách đang chọn chưa
            if (lstDetailTemplate.Count == 0)
            {
                this.State = false;
                this.ErrorMessage = $"Mẫu khai báo [{this.ObjDetail.TEMPLATE_CODE}] năm [{this.ObjDetail.TIME_YEAR}] chưa định nghĩa các khoản mục!";
                return;
            }

            // Kiểm tra revenue_center có thuộc mẫu thiết kế không
            // Có để trống dữ liệu tại 2 cột Mã đơn vị, và mã khoản mục không
            var lstOrgInTemplate = lstDetailTemplate.Select(x => x.CENTER_CODE).Distinct().ToList();
            StartRowData = ObjDetail.TYPE_UPLOAD == "01" ? StartRowData : 1;
            for (int i = this.StartRowData; i < dataTable.Rows.Count; i++)
            {
                var projectCode = ObjDetail.TYPE_UPLOAD == "01" ? dataTable.Rows[i][0].ToString().Trim() : dataTable.Rows[i][5].ToString().Trim();
                var companyCode = ObjDetail.TYPE_UPLOAD == "01" ? dataTable.Rows[i][2].ToString().Trim() : dataTable.Rows[i][7].ToString().Trim();
                var elementCodeInExcel = dataTable.Rows[i][4].ToString().Trim();
                if (string.IsNullOrWhiteSpace(projectCode))
                {
                    this.State = false;
                    this.ErrorMessage = $"Mã sân bay [{projectCode}] tại dòng [{i + 1}] trống hoặc không tồn tại trong biểu mẫu {ObjDetail.TEMPLATE_CODE} !";
                    return;
                }
                if (string.IsNullOrWhiteSpace(companyCode))
                {
                    this.State = false;
                    this.ErrorMessage = $"Mã hãng hàng không [{companyCode}] tại dòng [{i + 1}] trống hoặc không tồn tại trong biểu mẫu {ObjDetail.TEMPLATE_CODE} !";
                    return;
                }

                if (string.IsNullOrWhiteSpace(elementCodeInExcel))
                {
                    this.State = false;
                    this.ErrorMessage = $"Không được phép để trống dữ liệu tại cột [Mã khoản mục] tại dòng [{i + 1}] !";
                    return;
                }

                if (ObjDetail.TYPE_UPLOAD == "01")
                {
                    if (lstDetailTemplate.FirstOrDefault(x => x.Center.COST_CENTER_CODE == projectCode && x.Center.SAN_BAY_CODE == companyCode) == null)
                    {
                        this.State = false;
                        this.ErrorMessage = $"Mẫu khai báo [{template.CODE}] không chứa mã [{projectCode}] tại dòng [{i + 1}] !";
                        return;
                    }
                }

                if (ObjDetail.TYPE_UPLOAD == "01")
                {
                    // Kiểm tra các khoản mục lá không nằm trong mẫu thiết kế
                    var findElement = lstElement.FirstOrDefault(x => x.CODE == elementCodeInExcel);
                    if (findElement == null)
                    {
                        this.State = false;
                        this.ErrorMessage = $"Mã khoản mục [{elementCodeInExcel}] không tồn tại !";
                        return;
                    }

                    if (!findElement.IS_GROUP && lstDetailTemplate.Count(x => x.ELEMENT_CODE == elementCodeInExcel
                        && x.Center.COST_CENTER_CODE == projectCode
                        && x.Center.SAN_BAY_CODE == companyCode) == 0)
                    {
                        this.State = false;
                        this.ErrorMessage = $"Mã khoản mục [{elementCodeInExcel}] tại dòng {i + 1} không nằm trong mẫu thiết kế !";
                        return;
                    }
                }
            }

            // Kiểm tra file excel có dữ liệu từ dòng thứ StartRowData hay không
            if (dataTable == null || dataTable.Rows.Count < this.StartRowData)
            {
                this.State = false;
                this.ErrorMessage = "File excel này không có dữ liệu!";
                return;
            }

            //Kiếm tra có đúng mẫu hay không
            //Kiếm tra có đúng mẫu hay không
            if (isDataBase)
            {
                for (int i = 0; i < ListColumnNameDataBase.Count; i++)
                {
                    if (dataTable.Rows[this.StartRowData - 1][i].ToString().ToUpper() != this.ListColumnNameDataBase[i])
                    {
                        this.State = false;
                        this.ErrorMessage = "File excel không đúng theo mẫu thiết kế!";
                        return;
                    }
                }
                this.ConvertData(dataTable, lstElement.ToList(), startColumn: 8, endColumn: ListColumnNameDataBase.Count - 5, isDataBase);

            }
            else
            {

                if (ObjDetail.TYPE_UPLOAD == "01")
                {
                    //for (int i = 0; i < ListColumnName.Count; i++)
                    //{
                    //    if (dataTable.Rows[this.StartRowData - 1][i].ToString().ToUpper() != this.ListColumnName[i])
                    //    {
                    //        this.State = false;
                    //        this.ErrorMessage = "File excel không đúng theo mẫu thiết kế!";
                    //        return;
                    //    }
                    //}
                    // kiểm tra xem có 2 hàng dữ liệu của cùng 1 khoản mục hay không
                    var dictionary = lstDetailTemplate.ToDictionary(x => x.PKID, x => true);
                    for (int i = StartRowData; i < dataTable.Rows.Count; i++)
                    {
                        var projectCode = dataTable.Rows[i][0].ToString().Trim();
                        var companyCode = dataTable.Rows[i][2].ToString().Trim();
                        var elementCodeInExcel = dataTable.Rows[i][4].ToString().Trim();
                        var foundItem = lstDetailTemplate
                            .FirstOrDefault(x => x.ELEMENT_CODE == elementCodeInExcel
                            && x.Center.COST_CENTER_CODE == projectCode
                            && x.Center.SAN_BAY_CODE == companyCode);
                        if (foundItem != null && dictionary[foundItem.PKID])
                        {
                            dictionary[foundItem.PKID] = false;
                        }
                        else if (foundItem != null)
                        {
                            this.State = false;
                            this.ErrorMessage = $"Mã khoản mục [{foundItem.ELEMENT_CODE}] tại dòng {i + 1} bị lặp lại!";
                            return;
                        }
                    }
                }



                // Kiểm tra dữ liệu có phải là số hay không
                if (ObjDetail.TYPE_UPLOAD == "01")
                {
                    this.ConvertData(dataTable, lstElement.ToList(), startColumn: 6, endColumn: 9, isDataBase);
                }
                else
                {
                    this.ConvertData(dataTable, lstElement.ToList(), startColumn: 11, endColumn: 23, isDataBase);
                }
            }
            if (this.InvalidCellsList.Count > 0)
            {
                this.State = false;
                this.ErrorMessage = "Dữ liệu tại các cell sau không phải là số!<br>";
                foreach (var item in this.InvalidCellsList)
                {
                    this.ErrorMessage += $"Dòng [{item.X + 1}] - Cột [{this.ListColumnName[item.Y]}] <br>";
                }
                return;
            }
            if (ObjDetail.TYPE_UPLOAD == "01")
            {
                if (dataTable.Rows.Count == this.StartRowData)
                {
                    this.State = false;
                    this.ErrorMessage = "File excel này không có dữ liệu!";
                    return;
                }
            }

        }

        /// <summary>
        /// Convert data về số
        /// </summary>
        /// <param name="dataTable"></param>
        public override void ConvertData(DataTable dataTable, List<T_MD_KHOAN_MUC_HANG_HOA> lstElement, int startColumn, int endColumn, bool isDataBase)
        {
            var iActualRow = dataTable.Rows.Count;
            for (int i = this.StartRowData; i < iActualRow; i++)
            {
                for (int j = startColumn; j < endColumn; j++)
                {
                    if (isDataBase && (j - startColumn) % 4 == 1)
                    {
                        // ignore column time 
                        continue;
                    }
                    var strValue = (dataTable.Rows[i][j] ?? "0").ToString() ?? "0";
                    strValue = string.IsNullOrWhiteSpace(strValue) ? "0" : strValue.Trim();
                    if (decimal.TryParse(strValue, out decimal data))
                    {
                        dataTable.Rows[i][j] = data;
                    }
                    else
                    {
                        this.InvalidCellsList.Add(new Point(i, j));
                    }

                }
            }

            if (this.InvalidCellsList.Count > 0)
            {
                return;
            }
            if (ObjDetail.TYPE_UPLOAD == "01")
            {
                // Xóa những dữ liệu trắng, và dữ liệu mã cha
                var lstDataRowInValid = new List<DataRow>();
                for (int i = this.StartRowData; i < iActualRow; i++)
                {
                    var yearAmount = 0m;
                    bool isValid = true;
                    bool isEmptyValues = true;
                    var elementCodeInExcel = dataTable.Rows[i][4].ToString().Trim();
                    var findElement = lstElement.FirstOrDefault(x => x.CODE == elementCodeInExcel);
                    if (findElement == null || findElement.IS_GROUP)
                    {
                        isValid = false;
                    }
                    else
                    {
                        for (int j = startColumn; j < endColumn; j++)
                        {

                            var cellValue = dataTable.Rows[i][j].ToString().Trim();
                            if (!string.IsNullOrEmpty(cellValue) && cellValue != "0")
                            {
                                isEmptyValues = false;
                            }
                            if (isDataBase && (j - startColumn) % 4 == 1)
                            {
                                if (!string.IsNullOrEmpty(cellValue))
                                {
                                    isValid = true;
                                }
                                // ignore column time 
                                continue;
                            }
                            if (!string.IsNullOrEmpty(cellValue) &&
                                decimal.TryParse(cellValue, out decimal val))
                            {
                                if ((isDataBase && (j - startColumn) % 4 == 3 && j <= endColumn)
                                    || !isDataBase)
                                {
                                    yearAmount += val;
                                }
                                if (val != 0 && !isValid)
                                {
                                    isValid = true;
                                }
                            }
                        }
                    }
                    if (!isValid || isEmptyValues || yearAmount == 0)
                    {
                        lstDataRowInValid.Add(dataTable.Rows[i]);
                    }
                }

                foreach (var item in lstDataRowInValid)
                {
                    dataTable.Rows.Remove(item);
                }
            }

        }

        /// <summary>
        /// Nhập dữ liệu từ excel vào database
        /// </summary>
        /// <param name="request"></param>
        public override void ImportExcel(HttpRequestBase request)
        {
            base.ImportExcel(request);
            if (!State)
            {
                return;
            }
            var orgCode = ProfileUtilities.User.ORGANIZE_CODE;
            // Lưu file vào database
            var fileStream = new FILE_STREAM()
            {
                PKID = Guid.NewGuid().ToString(),
                FILESTREAM = request.Files[0]
            };
            FileStreamService.InsertFile(fileStream);

            // Xác định version dữ liệu
            var KeHoachChiPhiCurrent = CurrentRepository.Queryable().FirstOrDefault(x => x.ORG_CODE == orgCode
                && x.TIME_YEAR == ObjDetail.TIME_YEAR && x.TEMPLATE_CODE == ObjDetail.TEMPLATE_CODE);

            if (KeHoachChiPhiCurrent != null && !(KeHoachChiPhiCurrent.STATUS == Approve_Status.TuChoi || KeHoachChiPhiCurrent.STATUS == Approve_Status.ChuaTrinhDuyet))
            {
                this.State = false;
                this.ErrorMessage = "Mẫu khai báo này không ở trạng thái Từ chối hoặc Chưa trình duyệt!";
                return;
            }

            var versionNext = 1;
            if (KeHoachChiPhiCurrent != null)
            {
                versionNext = KeHoachChiPhiCurrent.VERSION + 1;
            }

            // Lấy dữ liệu của version hiện tại
            var dataCurrent = new List<T_BP_KE_HOACH_CHI_PHI_DATA>();
            if (KeHoachChiPhiCurrent != null)
            {
                dataCurrent = UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.ORG_CODE == orgCode
                    && x.TIME_YEAR == ObjDetail.TIME_YEAR && x.TEMPLATE_CODE == ObjDetail.TEMPLATE_CODE).ToList();
            }

            //Insert dữ liệu
            try
            {
                var currentUser = ProfileUtilities.User?.USER_NAME;
                DataTable tableData = new DataTable();
                tableData = ExcelDataExchange.ReadData(fileStream.FULL_PATH);

                this.ValidateData(tableData, isDataBase: false);
                if (!this.State)
                {
                    return;
                }

                int actualRows = tableData.Rows.Count;
                UnitOfWork.BeginTransaction();

                // Cập nhật version
                if (KeHoachChiPhiCurrent != null)
                {
                    // Cập nhật next version vào bảng chính
                    KeHoachChiPhiCurrent.VERSION = versionNext;
                    KeHoachChiPhiCurrent.IS_DELETED = false;
                    CurrentRepository.Update(KeHoachChiPhiCurrent);
                }
                else
                {
                    // Tạo mới bản ghi revenue pl
                    CurrentRepository.Create(new T_BP_KE_HOACH_CHI_PHI()
                    {
                        PKID = Guid.NewGuid().ToString(),
                        ORG_CODE = orgCode,
                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                        KICH_BAN = ObjDetail.KICH_BAN,
                        PHIEN_BAN = ObjDetail.PHIEN_BAN,
                        TIME_YEAR = ObjDetail.TIME_YEAR,
                        VERSION = versionNext,
                        STATUS = Approve_Status.ChuaTrinhDuyet,
                        FILE_ID = fileStream.PKID,
                        IS_DELETED = false,
                        IS_SUMUP = false,
                        CREATE_BY = currentUser
                    });
                }

                // Đưa next version vào bảng log
                UnitOfWork.Repository<KeHoachChiPhiVersionRepo>().Create(new T_BP_KE_HOACH_CHI_PHI_VERSION()
                {
                    PKID = Guid.NewGuid().ToString(),
                    ORG_CODE = orgCode,
                    TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                    VERSION = versionNext,
                    KICH_BAN = KeHoachChiPhiCurrent == null ? ObjDetail.KICH_BAN : KeHoachChiPhiCurrent.KICH_BAN,
                    PHIEN_BAN = KeHoachChiPhiCurrent == null ? ObjDetail.PHIEN_BAN : KeHoachChiPhiCurrent.PHIEN_BAN,
                    TIME_YEAR = ObjDetail.TIME_YEAR,
                    FILE_ID = fileStream.PKID,
                    CREATE_BY = currentUser
                });

                // Tạo mới bản ghi log trạng thái
                UnitOfWork.Repository<KeHoachChiPhiHistoryRepo>().Create(new T_BP_KE_HOACH_CHI_PHI_HISTORY()
                {
                    PKID = Guid.NewGuid().ToString(),
                    ORG_CODE = orgCode,
                    TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                    KICH_BAN = KeHoachChiPhiCurrent == null ? ObjDetail.KICH_BAN : KeHoachChiPhiCurrent.KICH_BAN,
                    PHIEN_BAN = KeHoachChiPhiCurrent == null ? ObjDetail.PHIEN_BAN : KeHoachChiPhiCurrent.PHIEN_BAN,
                    VERSION = versionNext,
                    TIME_YEAR = ObjDetail.TIME_YEAR,
                    ACTION = Approve_Action.NhapDuLieu,
                    ACTION_DATE = DateTime.Now,
                    ACTION_USER = currentUser,
                    CREATE_BY = currentUser
                });


                // Insert data vào history
                foreach (var item in dataCurrent)
                {
                    var revenueDataHis = (T_BP_KE_HOACH_CHI_PHI_DATA_HISTORY)item;
                    UnitOfWork.Repository<KeHoachChiPhiDataHistoryRepo>().Create(revenueDataHis);
                    UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Delete(item);
                }

                var allChiPhiProfitCenters = UnitOfWork.Repository<ChiPhiProfitCenterRepo>().GetAll();
                // Insert dữ liệu vào bảng data
                for (int i = this.StartRowData; i < actualRows; i++)
                {
                    var percentagePreventive = GetPreventive(orgCode, year: ObjDetail.TIME_YEAR)?.PERCENTAGE;
                    if (percentagePreventive == null)
                    {
                        percentagePreventive = 1;
                    }
                    else
                    {
                        percentagePreventive = 1 + percentagePreventive / 100;
                    }

                    var centerCode = "";
                    if (ObjDetail.TYPE_UPLOAD == "01")
                    {
                        centerCode = allChiPhiProfitCenters.FirstOrDefault(
                                                x => x.SAN_BAY_CODE == tableData.Rows[i][2].ToString().Trim() &&
                                                x.COST_CENTER_CODE == tableData.Rows[i][0].ToString().Trim())?.CODE;
                    }


                    if (centerCode == null)
                    {
                        throw new Exception($"Định dạng file không đúng ở dòng thứ {i + 1}");
                    }

                    var costData = new T_BP_KE_HOACH_CHI_PHI_DATA();

                    if (ObjDetail.TYPE_UPLOAD == "01")
                    {
                        costData = new T_BP_KE_HOACH_CHI_PHI_DATA()
                        {
                            PKID = Guid.NewGuid().ToString(),
                            ORG_CODE = orgCode,
                            CHI_PHI_PROFIT_CENTER_CODE = centerCode,
                            TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                            TIME_YEAR = ObjDetail.TIME_YEAR,
                            STATUS = Approve_Status.ChuaTrinhDuyet,
                            VERSION = versionNext,
                            KHOAN_MUC_HANG_HOA_CODE = tableData.Rows[i][4].ToString().Trim(),
                           
                            QUANTITY = tableData.Rows[i][6] as decimal? == null ? 0 : tableData.Rows[i][6] as decimal?,
                            PRICE = tableData.Rows[i][7] as decimal? == null ? 0 : tableData.Rows[i][7] as decimal?,

                            DESCRIPTION = tableData.Rows[i][9].ToString(),
                            CREATE_BY = currentUser
                        };
                        costData.AMOUNT = costData.QUANTITY * costData.PRICE;
                    }

                    UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costData);
                }
                UnitOfWork.Commit();
                NotifyUtilities.CreateNotify(
                    new NotifyPara()
                    {
                        Activity = Activity.AC_NHAP_DU_LIEU,
                        OrgCode = orgCode,
                        TemplateCode = ObjDetail.TEMPLATE_CODE,
                        TimeYear = ObjDetail.TIME_YEAR,
                        ModulType = ModulType.KeHoachChiPhi,
                        UserSent = currentUser
                    });
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                State = false;
                Exception = ex;
            }
        }
        /// <summary>
        /// Lấy entity khai báo ngân sách dự phòng
        /// </summary>
        /// <param name="centerCode">Mã phòng ban</param>
        /// <param name="year">Năm ngân sách</param>
        /// <returns></returns>
        private T_MD_PREVENTIVE GetPreventive(string centerCode, int year)
        {
            return UnitOfWork.Repository<PreventiveRepo>()
                .GetFirstWithFetch(x => x.ORG_CODE == centerCode && x.TIME_YEAR == year);
        }

        /// <summary>
        /// Nhập dữ liệu từ CƠ SỞ excel vào database
        /// </summary>
        /// <param name="request"></param>
        public override void ImportExcelBase(HttpRequestBase request)
        {
            base.ImportExcelBase(request);
            if (!State)
            {
                return;
            }
            this.StartRowData = 6;
            var orgCode = ProfileUtilities.User?.ORGANIZE_CODE;
            var KeHoachChiPhiDataRepo = UnitOfWork.Repository<KeHoachChiPhiDataRepo>();
            var KeHoachChiPhiDataBaseRepo = UnitOfWork.Repository<KeHoachChiPhiDataBaseRepo>();
            var currentUser = ProfileUtilities.User?.USER_NAME;

            // Lưu file vào database
            var fileStream = new FILE_STREAM()
            {
                PKID = Guid.NewGuid().ToString(),
                FILESTREAM = request.Files[0]
            };
            FileStreamService.InsertFile(fileStream);

            // Xác định version dữ liệu
            var KeHoachChiPhiCurrent = GetFirstWithFetch(x => x.ORG_CODE == orgCode
                && x.TIME_YEAR == ObjDetail.TIME_YEAR && x.TEMPLATE_CODE == ObjDetail.TEMPLATE_CODE);

            if (KeHoachChiPhiCurrent != null && !(KeHoachChiPhiCurrent.STATUS == Approve_Status.TuChoi || KeHoachChiPhiCurrent.STATUS == Approve_Status.ChuaTrinhDuyet))
            {
                this.State = false;
                this.ErrorMessage = "Mẫu khai báo này không ở trạng thái Từ chối hoặc Chưa trình duyệt!";
                return;
            }

            var versionNext = 1;
            if (KeHoachChiPhiCurrent != null)
            {
                versionNext = KeHoachChiPhiCurrent.VERSION + 1;
            }

            // Lấy dữ liệu của version hiện tại
            var dataCurrent = new List<T_BP_KE_HOACH_CHI_PHI_DATA>();
            var dataCurrentBase = new List<T_BP_KE_HOACH_CHI_PHI_DATA_BASE>();
            if (KeHoachChiPhiCurrent != null)
            {
                dataCurrent = KeHoachChiPhiDataRepo.GetManyWithFetch(x => x.ORG_CODE == orgCode
                    && x.TIME_YEAR == ObjDetail.TIME_YEAR && x.TEMPLATE_CODE == ObjDetail.TEMPLATE_CODE).ToList();

                dataCurrentBase = KeHoachChiPhiDataBaseRepo.GetManyWithFetch(x => x.ORG_CODE == orgCode
                    && x.TIME_YEAR == ObjDetail.TIME_YEAR && x.TEMPLATE_CODE == ObjDetail.TEMPLATE_CODE).ToList();
            }

            //Insert dữ liệu
            try
            {
                DataTable tableData = new DataTable();
                tableData = ExcelDataExchange.ReadData(fileStream.FULL_PATH);

                this.ValidateData(tableData, isDataBase: true);
                if (!this.State)
                {
                    return;
                }

                int actualRows = tableData.Rows.Count;
                UnitOfWork.BeginTransaction();
                // Cập nhật version
                if (KeHoachChiPhiCurrent != null)
                {
                    // Cập nhật next version vào bảng chính
                    KeHoachChiPhiCurrent.VERSION = versionNext;
                    KeHoachChiPhiCurrent.IS_DELETED = false;
                    CurrentRepository.Update(KeHoachChiPhiCurrent);
                }
                else
                {
                    // Tạo mới bản ghi cost pl
                    CurrentRepository.Create(new T_BP_KE_HOACH_CHI_PHI()
                    {
                        PKID = Guid.NewGuid().ToString(),
                        ORG_CODE = orgCode,
                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                        KICH_BAN = ObjDetail.KICH_BAN,
                        PHIEN_BAN = ObjDetail.PHIEN_BAN,
                        TIME_YEAR = ObjDetail.TIME_YEAR,
                        IS_DELETED = false,
                        VERSION = versionNext,
                        STATUS = Approve_Status.ChuaTrinhDuyet,
                        FILE_ID = fileStream.PKID,
                        IS_SUMUP = false,
                        CREATE_BY = currentUser
                    });
                }

                // Đưa next version vào bảng log
                UnitOfWork.Repository<KeHoachChiPhiVersionRepo>().Create(new T_BP_KE_HOACH_CHI_PHI_VERSION()
                {
                    PKID = Guid.NewGuid().ToString(),
                    ORG_CODE = orgCode,
                    KICH_BAN = ObjDetail.KICH_BAN,
                    PHIEN_BAN = ObjDetail.PHIEN_BAN,
                    TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                    VERSION = versionNext,
                    TIME_YEAR = ObjDetail.TIME_YEAR,
                    FILE_ID = fileStream.PKID,
                    CREATE_BY = currentUser
                });

                // Tạo mới bản ghi log trạng thái
                UnitOfWork.Repository<KeHoachChiPhiHistoryRepo>().Create(new T_BP_KE_HOACH_CHI_PHI_HISTORY()
                {
                    PKID = Guid.NewGuid().ToString(),
                    ORG_CODE = orgCode,
                    TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                    VERSION = versionNext,
                    TIME_YEAR = ObjDetail.TIME_YEAR,
                    ACTION = Approve_Action.NhapDuLieu,
                    ACTION_DATE = DateTime.Now,
                    ACTION_USER = currentUser,
                    CREATE_BY = currentUser
                });

                foreach (var item in dataCurrent)
                {
                    var costDataHis = (T_BP_KE_HOACH_CHI_PHI_DATA_HISTORY)item;
                    UnitOfWork.Repository<KeHoachChiPhiDataHistoryRepo>().Create(costDataHis);
                    KeHoachChiPhiDataRepo.Delete(item);
                }

                // Insert dữ liệu vào bảng data
                var lstRowValues = new List<DataRow>();
                for (int i = StartRowData; i < actualRows; i++)
                {
                    lstRowValues.Add(tableData.Rows[i]);
                }

                var allChiPhiProfitCenters = UnitOfWork.Repository<ChiPhiProfitCenterRepo>().GetAll();
                var lookUp = lstRowValues.OfType<DataRow>().ToLookup(x => new { ProjectCode = x[0].ToString().Trim(), CompanyCode = x[2].ToString().Trim(), ElementCode = x[4].ToString().Trim() });

                var percentagePreventive = GetPreventive(orgCode, ObjDetail.TIME_YEAR)?.PERCENTAGE;
                if (percentagePreventive == null)
                {
                    percentagePreventive = 1;
                }
                else
                {
                    percentagePreventive = 1 + percentagePreventive / 100;
                }
                foreach (var key in lookUp.Select(x => x.Key))
                {
                    var centerCode = allChiPhiProfitCenters.FirstOrDefault(
                            x => x.SAN_BAY_CODE == key.ProjectCode &&
                            x.COST_CENTER_CODE == key.CompanyCode)?.CODE;
                    if (centerCode == null)
                    {
                        throw new Exception($"Định dạng file không đúng");
                    }
                    var costData = new T_BP_KE_HOACH_CHI_PHI_DATA()
                    {
                        PKID = Guid.NewGuid().ToString(),
                        ORG_CODE = orgCode,
                        CHI_PHI_PROFIT_CENTER_CODE = centerCode,
                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                        TIME_YEAR = ObjDetail.TIME_YEAR,
                        STATUS = Approve_Status.ChuaTrinhDuyet,
                        VERSION = versionNext,
                        KHOAN_MUC_HANG_HOA_CODE = key.ElementCode,
                        //VALUE = lookUp[key].Sum(x => UtilsCore.StringToDecimal(x[11].ToString())),
                        CREATE_BY = currentUser
                    };

                    KeHoachChiPhiDataRepo.Create(costData);
                }

                // Insert data vào base data history
                var lstBaseDataHistory = (from x in dataCurrentBase
                                          select (T_BP_KE_HOACH_CHI_PHI_DATA_BASE_HISTORY)x).ToList();
                UnitOfWork.Repository<KeHoachChiPhiDataBaseHistoryRepo>().Create(lstObj: lstBaseDataHistory);
                KeHoachChiPhiDataBaseRepo.Delete(dataCurrentBase);

                // Insert dữ liệu vào bảng data
                for (int i = this.StartRowData; i < actualRows; i++)
                {
                    int j = 8;
                    var centerCode = allChiPhiProfitCenters.FirstOrDefault(
                            x => x.SAN_BAY_CODE == tableData.Rows[i][0].ToString().Trim() &&
                            x.COST_CENTER_CODE == tableData.Rows[i][2].ToString().Trim())?.CODE;
                    if (centerCode == null)
                    {
                        throw new Exception($"Định dạng file không đúng");
                    }
                    var costData = new T_BP_KE_HOACH_CHI_PHI_DATA_BASE()
                    {
                        PKID = Guid.NewGuid().ToString(),
                        ORG_CODE = orgCode,
                        SAN_BAY_CODE = tableData.Rows[i][0].ToString().Trim(),
                        //COST_CENTER_CODE = tableData.Rows[i][2].ToString().Trim(),
                        CHI_PHI_PROFIT_CENTER_CODE = centerCode,
                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                        TIME_YEAR = ObjDetail.TIME_YEAR,
                        VERSION = versionNext,
                        KHOAN_MUC_HANG_HOA_CODE = tableData.Rows[i][4].ToString().Trim(),
                        MATERIAL = tableData.Rows[i][6].ToString().Trim(),
                        UNIT = tableData.Rows[i][7].ToString().Trim(),

                        QUANTITY = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),
                        TIME = tableData.Rows[i][j++].ToString().Trim(),
                        PRICE = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),
                        AMOUNT = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),

                        DESCRIPTION = tableData.Rows[i][ListColumnNameDataBase.Count - 1].ToString().Trim(),

                        CREATE_BY = currentUser
                    };
                    KeHoachChiPhiDataBaseRepo.Create(costData);
                }
                UnitOfWork.Commit();

                NotifyUtilities.CreateNotify(
                    new NotifyPara()
                    {
                        Activity = Activity.AC_NHAP_DU_LIEU,
                        OrgCode = orgCode,
                        TemplateCode = ObjDetail.TEMPLATE_CODE,
                        TimeYear = ObjDetail.TIME_YEAR,
                        ModulType = ModulType.KeHoachChiPhi,
                        UserSent = currentUser
                    });
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                State = false;
                Exception = ex;
            }
        }
        #endregion

        public override IList<T_BP_KE_HOACH_CHI_PHI_VERSION> GetVersions(string orgCode, string templateId, int year)
        {
            templateId = templateId ?? string.Empty;
            var lstVersions = GetVersionsNumber(orgCode, templateId, year, "", "");
            return UnitOfWork.Repository<KeHoachChiPhiVersionRepo>()
                .GetManyByExpression(x => x.TEMPLATE_CODE == templateId
                && x.TIME_YEAR == year
                && lstVersions.Contains(x.VERSION));
        }

        #region Get template code
        public override IList<Data> GetTemplates(string orgCode, int? year = null)
        {
            ObjDetail.ORG_CODE = orgCode;
            var lstOrgCodes = new List<string>() { orgCode };
            var lstTemplateCurrentUserSelfUpload = new List<string>();

            var isLeaf = IsLeaf();
            if (isLeaf)
            {
                // is leaf
                lstOrgCodes.Add(orgCode);
                lstTemplateCurrentUserSelfUpload.AddRange(GetTemplateCurrentUserSelfUpload(orgCode));
            }
            else
            {
                // is group
                // get all child
                lstOrgCodes.AddRange(GetListOfChildrenCenter(orgCode).Select(x => x.CODE));
            }

            var templates = lstTemplateCurrentUserSelfUpload;
            if (AuthorizeUtilities.CheckUserRight("R323") || !isLeaf)
            {
                templates = GetTemplateData(lstOrgCodes, year)
                    .Union(GetTemplateDataHistory(lstOrgCodes, year))
                    .Union(lstTemplateCurrentUserSelfUpload)
                    .ToList();
            }
            if (templates == null || templates.Count == 0)
            {
                return null;
            }
            return UnitOfWork.Repository<TemplateRepo>()
                .GetManyWithFetch(x => templates.Contains(x.CODE) && x.CODE != "")
                .OrderByDescending(x => x.CODE)
                .Select(x => new Data
                {
                    Value = x.CODE,
                    Text = $"{x.CODE} - {x.NAME}" + (x.ACTIVE ? "" : $" - {Global.DeactiveTemplate}")
                })
                .ToList();
        }

        private IList<string> GetTemplateCurrentUserSelfUpload(string orgCode)
        {
            return UnitOfWork.Repository<KeHoachChiPhiVersionRepo>()
                .GetManyByExpression(x => x.ORG_CODE == orgCode && x.TEMPLATE_CODE != string.Empty)
                .Select(x => x.TEMPLATE_CODE)
                .Distinct()
                .ToList();
        }

        private IList<string> GetTemplateData(List<string> lstOrgCodes, int? year = null)
        {
            return UnitOfWork.Repository<KeHoachChiPhiDataRepo>()
                .GetManyByExpression(x => lstOrgCodes.Contains(x.ORG_CODE) && x.TEMPLATE_CODE != string.Empty && (!year.HasValue || x.TIME_YEAR == year.Value))
                .Select(x => x.TEMPLATE_CODE)
                .Distinct()
                .ToList();
        }

        private IList<string> GetTemplateDataHistory(List<string> lstOrgCodes, int? year = null)
        {
            return UnitOfWork.Repository<KeHoachChiPhiDataHistoryRepo>()
                .GetManyByExpression(x => lstOrgCodes.Contains(x.ORG_CODE) && x.TEMPLATE_CODE != string.Empty && (!year.HasValue || x.TIME_YEAR == year.Value))
                .Select(x => x.TEMPLATE_CODE)
                .Distinct()
                .ToList();
        }
        #endregion

        #region Get template year
        /// <summary>
        /// lấy tất cả các năm trong các mẫu đã từng được nộp (nộp hộ) của đơn vị hoặc con
        /// </summary>
        /// <param name="orgCode"></param>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public override IList<int> GetYears(string orgCode, string templateId)
        {
            ObjDetail.ORG_CODE = orgCode;
            var lstOrgCodes = new List<string>() { orgCode };
            var templateCurrentUserUpload = new List<int>();
            templateId = templateId ?? string.Empty;
            if (IsLeaf())
            {
                // is leaf
                lstOrgCodes.Add(orgCode);
                // lấy năm của những mẫu tự nộp của đơn vị
            }
            else
            {
                // is group
                // get all child
                lstOrgCodes.AddRange(GetListOfChildrenCenter(orgCode).Select(x => x.CODE));
            }
            templateCurrentUserUpload.AddRange(GetYearCurrentUserUpload(lstOrgCodes, templateId));

            return GetDataYears(lstOrgCodes, templateId)
                .Union(GetDataHistoryYear(lstOrgCodes, templateId))
                .Union(templateCurrentUserUpload)
                .OrderByDescending(x => x)
                .ToList();
        }

        private IList<int> GetYearCurrentUserUpload(IList<string> lstOrgCodes, string templateId)
        {
            if (string.IsNullOrEmpty(templateId))
            {
                return UnitOfWork.Repository<KeHoachChiPhiVersionRepo>()
                    .GetManyByExpression(x => lstOrgCodes.Contains(x.ORG_CODE))
                    .Select(x => x.TIME_YEAR)
                    .Distinct()
                    .ToList();
            }
            else
            {
                return UnitOfWork.Repository<KeHoachChiPhiVersionRepo>()
                    .GetManyByExpression(x => lstOrgCodes.Contains(x.ORG_CODE) && x.TEMPLATE_CODE == templateId)
                    .Select(x => x.TIME_YEAR)
                    .Distinct()
                    .ToList();
            }
        }

        private IList<int> GetDataHistoryYear(List<string> lstOrgCodes, string templateId)
        {
            if (string.IsNullOrEmpty(templateId))
            {
                return UnitOfWork.Repository<KeHoachChiPhiDataHistoryRepo>()
                    .GetManyByExpression(x => lstOrgCodes.Contains(x.ORG_CODE))
                    .Select(x => x.TIME_YEAR)
                    .Distinct()
                    .ToList();
            }
            else
            {
                return UnitOfWork.Repository<KeHoachChiPhiDataHistoryRepo>()
                    .GetManyByExpression(x => lstOrgCodes.Contains(x.ORG_CODE) && x.TEMPLATE_CODE == templateId)
                    .Select(x => x.TIME_YEAR)
                    .Distinct()
                    .ToList();
            }
        }

        private IList<int> GetDataYears(List<string> lstOrgCodes, string templateId)
        {
            if (string.IsNullOrEmpty(templateId))
            {
                return UnitOfWork.Repository<KeHoachChiPhiDataRepo>()
                    .GetManyByExpression(x => lstOrgCodes.Contains(x.ORG_CODE))
                    .Select(x => x.TIME_YEAR)
                    .Distinct()
                    .ToList();
            }
            else
            {
                return UnitOfWork.Repository<KeHoachChiPhiDataRepo>()
                    .GetManyByExpression(x => lstOrgCodes.Contains(x.ORG_CODE) && x.TEMPLATE_CODE == templateId)
                    .Select(x => x.TIME_YEAR)
                    .Distinct()
                    .ToList();
            }
        }
        #endregion

        #region Get template versions
        public override IList<int> GetVersionsNumber(string orgCode, string templateId, int year, string kichBan, string phienBan)
        {
            if (!string.IsNullOrEmpty(templateId))
            {
                return UnitOfWork.Repository<KeHoachChiPhiVersionRepo>()
                    .GetManyWithFetch(x => x.TEMPLATE_CODE == templateId && x.TIME_YEAR == year && x.KICH_BAN == kichBan && x.PHIEN_BAN == phienBan)
                    .Select(x => x.VERSION)
                    .OrderByDescending(x => x)
                    .ToList();
            }
            else
            {
                return UnitOfWork.Repository<KeHoachChiPhiVersionRepo>()
                    .GetManyWithFetch(x => x.TEMPLATE_CODE == templateId && x.TIME_YEAR == year && x.ORG_CODE == orgCode && x.KICH_BAN == kichBan && x.PHIEN_BAN == phienBan)
                    .Select(x => x.VERSION)
                    .OrderByDescending(x => x)
                    .ToList();
            }

            //ObjDetail.ORG_CODE = orgCode;
            //var lstOrgCodes = new List<string>() { orgCode };
            //var lstTemplateCurrentUserSelfUpload = new List<int>();
            //templateId = templateId ?? string.Empty;
            //if (IsLeaf())
            //{
            //    // is leaf
            //    lstOrgCodes.Add(orgCode);
            //}
            //else
            //{
            //    // is group
            //    // get all child
            //    if (!string.IsNullOrEmpty(templateId))
            //    {
            //        lstOrgCodes.AddRange(GetListOfChildrenCenter(orgCode).Select(x => x.CODE));
            //    }
            //}

            //lstTemplateCurrentUserSelfUpload.AddRange(GetVersionCurrentUserSelfUpload(orgCode, templateId, year));
            //return GetVersionTemplateData(lstOrgCodes, templateId, year)
            //    .Union(GetVersionTemplateDataHistory(lstOrgCodes, templateId, year))
            //    .Union(lstTemplateCurrentUserSelfUpload)
            //    .OrderByDescending(x => x)
            //    .ToList();
        }

        #endregion

        public override IList<int> GetTemplateVersion(string templateId, string centerCode, int year)
        {
            if (string.IsNullOrEmpty(templateId))
            {
                return UnitOfWork.Repository<KeHoachChiPhiSumUpDetailRepo>()
                    .GetManyByExpression(x => x.ORG_CODE == centerCode && x.TIME_YEAR == year)
                    .Select(x => x.DATA_VERSION).Distinct().OrderByDescending(x => x).ToList();
            }
            else
            {
                return UnitOfWork.Repository<KeHoachChiPhiVersionRepo>()
                    .GetManyByExpression(x => x.TEMPLATE_CODE == templateId && x.ORG_CODE == centerCode && x.TIME_YEAR == year)
                    .Select(x => x.VERSION).Distinct().OrderByDescending(x => x).ToList();
            }
        }

        /// <summary>
        /// generate excel file and store in path
        /// </summary>
        /// <param name="outFileStream"></param>
        /// <param name="path"></param>
        /// <param name="templateId"></param>
        /// <param name="year"></param>
        public override void GenerateTemplate(ref MemoryStream outFileStream, string path, string templateId, int year)
        {
            var dataOtherCost = PreparePureList(out IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_CHI_PHI> detailOtherKhoanMucHangHoas, templateId, year, ignoreAuth: true);

            if (dataOtherCost.Count == 0 || detailOtherKhoanMucHangHoas.Count == 0)
            {
                State = false;
                ErrorMessage = "Không tìm thấy dữ liệu";
                return;
            }

            try
            {
                //Mở file Template
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                IWorkbook templateWorkbook;
                templateWorkbook = new XSSFWorkbook(fs);
                templateWorkbook.SetSheetName(0, ModulType.GetTextSheetName(ModulType.KeHoachChiPhi));
                fs.Close();
                ISheet sheet = templateWorkbook.GetSheetAt(0);

                //Số hàng và số cột hiện tại
                int numRowCur = 0;
                int NUM_CELL = 21;

                //Style cần dùng
                ICellStyle styleCellHeader = templateWorkbook.CreateCellStyle();
                styleCellHeader.CloneStyleFrom(sheet.GetRow(5).Cells[0].CellStyle);
                styleCellHeader.WrapText = true;

                ICellStyle styleCellDetail = templateWorkbook.CreateCellStyle();
                styleCellDetail.CloneStyleFrom(sheet.GetRow(6).Cells[0].CellStyle);
                styleCellDetail.WrapText = true;

                ICellStyle styleCellNumber = templateWorkbook.CreateCellStyle();
                styleCellNumber.CloneStyleFrom(styleCellDetail);
                styleCellNumber.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,##0");

                ICellStyle styleCellNumberColor = templateWorkbook.CreateCellStyle();
                styleCellNumberColor.CloneStyleFrom(styleCellNumber);
                styleCellNumberColor.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Yellow.Index;
                styleCellNumberColor.FillPattern = FillPattern.SolidForeground;

                ICellStyle styleCellBold = templateWorkbook.CreateCellStyle();
                styleCellBold.CloneStyleFrom(sheet.GetRow(6).Cells[0].CellStyle);
                styleCellBold.WrapText = true;
                var fontBold = templateWorkbook.CreateFont();
                fontBold.Boldweight = (short)FontBoldWeight.Bold;
                fontBold.FontHeightInPoints = 11;
                fontBold.FontName = "Times New Roman";

                #region Header
                var template = UnitOfWork.Repository<TemplateRepo>().Get(templateId);
                var rowHeader1 = ReportUtilities.CreateRow(ref sheet, 0, NUM_CELL);
                ReportUtilities.CreateCell(ref rowHeader1, NUM_CELL);
                rowHeader1.Cells[0].SetCellValue(rowHeader1.Cells[0].StringCellValue + $" {template.Organize?.Parent?.NAME.ToUpper()}");

                var rowHeader2 = ReportUtilities.CreateRow(ref sheet, 1, NUM_CELL);

                ReportUtilities.CreateCell(ref rowHeader2, NUM_CELL);
                rowHeader2.Cells[0].SetCellValue($"{template.Organize.NAME}");
                rowHeader2.Cells[2].SetCellValue(template.TITLE.ToUpper());
                rowHeader2.Cells[18].SetCellValue(rowHeader2.Cells[18].StringCellValue + $" {year}");

                var rowHeader3 = ReportUtilities.CreateRow(ref sheet, 2, NUM_CELL);

                ReportUtilities.CreateCell(ref rowHeader3, NUM_CELL);
                rowHeader3.Cells[1].SetCellValue(template.CREATE_BY);
                //rowHeader2.Cells[14].SetCellValue(string.Concat(rowHeader2.Cells[14].StringCellValue, " ", template.));
                #endregion

                #region Details

                numRowCur = 6;
                var number = 1;
                var rowHeightDetail = sheet.GetRow(6).Height;
                foreach (var detail in detailOtherKhoanMucHangHoas.GroupBy(x => x.CENTER_CODE)
                    .Select(x => x.First())
                    .OrderBy(x => x.Center.SanBay.CODE).ThenBy(x => x.Center.SanBay.CODE))
                {
                    foreach (var item in dataOtherCost
                            .Where(x => x.CENTER_CODE == detail.CENTER_CODE)
                            .OrderBy(x => x.C_ORDER)
                            .GroupBy(x => x.CODE)
                            .Select(x => x.First()))
                    {
                        var space = new StringBuilder();
                        for (int i = 0; i < item.LEVEL; i++)
                        {
                            space.Append("\t");
                        }

                        //ReportUtilities.CreateRow(ref sheet, numRowCur, 50);
                        ReportUtilities.CopyRow(ref sheet, 7, numRowCur);
                        IRow rowCur = ReportUtilities.CreateRow(ref sheet, numRowCur, NUM_CELL);
                        rowCur.Height = -1;

                        rowCur.Cells[2].SetCellValue(detail.Center.SAN_BAY_CODE);
                        rowCur.Cells[2].CellStyle = styleCellDetail;

                        rowCur.Cells[3].SetCellValue(detail.Center.SanBay.NAME);
                        rowCur.Cells[3].CellStyle = styleCellDetail;

                        rowCur.Cells[0].SetCellValue(detail.Center.COST_CENTER_CODE);
                        rowCur.Cells[0].CellStyle = styleCellDetail;

                        rowCur.Cells[1].SetCellValue(detail.Center.CostCenter.NAME);
                        rowCur.Cells[1].CellStyle = styleCellDetail;

                        rowCur.Cells[4].SetCellValue(item.CODE);
                        rowCur.Cells[4].CellStyle = styleCellDetail;

                        rowCur.Cells[5].SetCellValue($"{space}{item.NAME}");
                        if (item.IS_GROUP)
                        {
                            rowCur.Cells[5].CellStyle = styleCellBold;
                            rowCur.Cells[5].CellStyle.SetFont(fontBold);
                        }
                        else
                        {
                            rowCur.Cells[5].CellStyle = styleCellDetail;
                        }

                        for (int i = 6; i < 18; i++)
                        {
                            rowCur.Cells[i].SetCellValue(string.Empty);
                            rowCur.Cells[i].SetCellType(CellType.Numeric);
                            rowCur.Cells[i].CellStyle = styleCellNumber;
                        }

                        rowCur.Cells[18].SetCellFormula($"SUM(G{numRowCur + 1}:R{numRowCur + 1})");
                        rowCur.Cells[18].CellStyle = styleCellNumberColor;
                        rowCur.Cells[18].SetCellType(CellType.Formula);

                        rowCur.Cells[19].SetCellFormula($"S{numRowCur + 1}/12");
                        rowCur.Cells[19].CellStyle = styleCellNumberColor;
                        rowCur.Cells[19].SetCellType(CellType.Formula);

                        rowCur.Cells[20].SetCellValue(string.Empty);
                        rowCur.Cells[20].CellStyle = styleCellDetail;

                        numRowCur++;
                        number++;
                    }
                }

                //Xóa dòng thừa cuối cùng khi tạo các dòng cho detail
                IRow rowLastDetail = ReportUtilities.CreateRow(ref sheet, numRowCur, NUM_CELL);
                ReportUtilities.DeleteRow(ref sheet, rowLastDetail);

                rowLastDetail = ReportUtilities.CreateRow(ref sheet, numRowCur, NUM_CELL);
                ReportUtilities.DeleteRow(ref sheet, rowLastDetail);
                #endregion

                templateWorkbook.Write(outFileStream);
            }
            catch (Exception ex)
            {
                this.State = false;
                this.ErrorMessage = "Có lỗi xảy ra trong quá trình tạo file excel!";
                this.Exception = ex;
            }
        }

        /// <summary>
        /// generate excel file template base and store in path
        /// </summary>
        /// <param name="outFileStream"></param>
        /// <param name="path"></param>
        /// <param name="templateId"></param>
        /// <param name="year"></param>
        public override void GenerateTemplateBase(ref MemoryStream outFileStream, string path, string templateId, int year)
        {
            var dataOtherCost = PreparePureList(out IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_CHI_PHI> detailKhoanMucHangHoas, templateId, year, ignoreAuth: true);

            if (dataOtherCost.Count == 0 || detailKhoanMucHangHoas.Count == 0)
            {
                State = false;
                ErrorMessage = "Không tìm thấy dữ liệu";
                return;
            }

            try
            {
                //Mở file Template
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                IWorkbook templateWorkbook;
                templateWorkbook = new XSSFWorkbook(fs);
                templateWorkbook.SetSheetName(0, ModulType.GetTextSheetName(ModulType.KeHoachChiPhi));
                fs.Close();
                ISheet sheet = templateWorkbook.GetSheetAt(0);

                //Số hàng và số cột hiện tại
                int numRowCur = 0;
                int NUM_CELL = ListColumnNameDataBase.Count;

                //Style cần dùng
                ICellStyle styleCellHeader = templateWorkbook.CreateCellStyle();
                styleCellHeader.CloneStyleFrom(sheet.GetRow(5).Cells[0].CellStyle);
                styleCellHeader.WrapText = true;

                ICellStyle styleCellDetail = templateWorkbook.CreateCellStyle();
                styleCellDetail.CloneStyleFrom(sheet.GetRow(6).Cells[0].CellStyle);
                styleCellDetail.WrapText = true;

                ICellStyle styleCellNumber = templateWorkbook.CreateCellStyle();
                styleCellNumber.CloneStyleFrom(styleCellDetail);
                styleCellNumber.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,##0.00");

                ICellStyle styleCellNumberColor = templateWorkbook.CreateCellStyle();
                styleCellNumberColor.CloneStyleFrom(styleCellNumber);
                styleCellNumberColor.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Yellow.Index;
                styleCellNumberColor.FillPattern = FillPattern.SolidForeground;

                ICellStyle styleCellBold = templateWorkbook.CreateCellStyle();
                styleCellBold.CloneStyleFrom(sheet.GetRow(6).Cells[0].CellStyle);
                styleCellBold.WrapText = true;
                var fontBold = templateWorkbook.CreateFont();
                fontBold.Boldweight = (short)FontBoldWeight.Bold;
                fontBold.FontHeightInPoints = 11;
                fontBold.FontName = "Times New Roman";

                #region Header
                var template = UnitOfWork.Repository<TemplateRepo>().Get(templateId);
                var rowHeader1 = ReportUtilities.CreateRow(ref sheet, 0, NUM_CELL);
                ReportUtilities.CreateCell(ref rowHeader1, NUM_CELL);
                rowHeader1.Cells[0].SetCellValue(template.Organize?.Parent?.NAME.ToUpper());

                var rowHeader2 = ReportUtilities.CreateRow(ref sheet, 1, NUM_CELL);

                ReportUtilities.CreateCell(ref rowHeader2, NUM_CELL);
                rowHeader2.Cells[0].SetCellValue(template.Organize.NAME.ToUpper());
                rowHeader2.Cells[2].SetCellValue(template.TITLE.ToUpper());
                rowHeader2.Cells[8].SetCellValue(rowHeader2.Cells[8].StringCellValue + $" {year}");

                var rowHeader3 = ReportUtilities.CreateRow(ref sheet, 2, NUM_CELL);

                ReportUtilities.CreateCell(ref rowHeader3, NUM_CELL);
                rowHeader3.Cells[1].SetCellValue(template.CREATE_BY);
                #endregion

                #region Details

                numRowCur = 6;
                var number = 1;
                var rowHeightDetail = sheet.GetRow(6).Height;
                foreach (var detail in detailKhoanMucHangHoas.GroupBy(x => x.CENTER_CODE)
                    .Select(x => x.First())
                    .OrderByDescending(x => x.CENTER_CODE))
                {
                    foreach (var item in dataOtherCost
                            .Where(x => x.CENTER_CODE == detail.CENTER_CODE)
                            .OrderBy(x => x.C_ORDER)
                            .GroupBy(x => x.CODE)
                            .Select(x => x.First()))
                    {
                        var space = new StringBuilder();
                        for (int i = 0; i < item.LEVEL; i++)
                        {
                            space.Append("\t");
                        }

                        //ReportUtilities.CreateRow(ref sheet, numRowCur, 50);
                        ReportUtilities.CopyRow(ref sheet, 7, numRowCur);
                        IRow rowCur = ReportUtilities.CreateRow(ref sheet, numRowCur, NUM_CELL);
                        rowCur.Height = -1;
                        int j = 0;
                        rowCur.Cells[j].SetCellValue(detail.Center.SAN_BAY_CODE);
                        rowCur.Cells[j].CellStyle = styleCellDetail;
                        j++;

                        rowCur.Cells[j].SetCellValue(detail.Center.SanBay.NAME);
                        rowCur.Cells[j].CellStyle = styleCellDetail;
                        j++;

                        rowCur.Cells[j].SetCellValue(detail.Center.COST_CENTER_CODE);
                        rowCur.Cells[j].CellStyle = styleCellDetail;
                        j++;

                        rowCur.Cells[j].SetCellValue(detail.Center.CostCenter.NAME);
                        rowCur.Cells[j].CellStyle = styleCellDetail;
                        j++;

                        rowCur.Cells[j].SetCellValue(item.CODE);
                        rowCur.Cells[j].CellStyle = styleCellDetail;
                        j++;

                        rowCur.Cells[j].SetCellValue($"{space}{item.NAME}");
                        if (item.IS_GROUP)
                        {
                            rowCur.Cells[j].CellStyle = styleCellBold;
                            rowCur.Cells[j].CellStyle.SetFont(fontBold);
                        }
                        else
                        {
                            rowCur.Cells[j].CellStyle = styleCellDetail;
                        }
                        j++;
                        rowCur.Cells[j].SetCellValue(string.Empty);
                        rowCur.Cells[j].CellStyle = styleCellDetail;
                        j++;

                        rowCur.Cells[j].SetCellValue(string.Empty);
                        rowCur.Cells[j].CellStyle = styleCellDetail;
                        j++;

                        for (; j < NUM_CELL - 1; j++)
                        {
                            rowCur.Cells[j].SetCellValue(string.Empty);
                            if (j < NUM_CELL - 5 && (j - 7) % 4 == 2)
                            {
                                rowCur.Cells[j].CellStyle = styleCellDetail;
                            }
                            else
                            {
                                if (j >= NUM_CELL - 5)
                                {
                                    rowCur.Cells[j].CellStyle = styleCellNumberColor;
                                }
                                else
                                {
                                    rowCur.Cells[j].CellStyle = styleCellNumber;
                                }
                                rowCur.Cells[j].SetCellType(CellType.Numeric);
                            }
                        }

                        rowCur.Cells[j].SetCellValue(string.Empty);
                        rowCur.Cells[j].CellStyle = styleCellDetail;

                        numRowCur++;
                        number++;
                    }
                }

                //Xóa dòng thừa cuối cùng khi tạo các dòng cho detail
                IRow rowLastDetail = ReportUtilities.CreateRow(ref sheet, numRowCur, NUM_CELL);
                ReportUtilities.DeleteRow(ref sheet, rowLastDetail);

                rowLastDetail = ReportUtilities.CreateRow(ref sheet, numRowCur, NUM_CELL);
                ReportUtilities.DeleteRow(ref sheet, rowLastDetail);
                #endregion

                templateWorkbook.Write(outFileStream);
            }
            catch (Exception ex)
            {
                this.State = false;
                this.ErrorMessage = "Có lỗi xảy ra trong quá trình tạo file excel!";
                this.Exception = ex;
            }
        }

        public override IList<NodeDataFlow> BuildDataFlowTree(string orgCode, int year, int? version, int? sumUpVersion)
        {
            var costCenterRepo = UnitOfWork.Repository<CostCenterRepo>();

            if (costCenterRepo.GetManyByExpression(x => x.PARENT_CODE == orgCode).Count > 0 && version == null)
            {
                // nếu muốn xem tất cả các bản tổng hợp thì để version và sumUpVersion = null ở lần gọi đầu tiên
                if (!sumUpVersion.HasValue)
                {
                    var revenuePLData = UnitOfWork.Repository<KeHoachChiPhiSumUpDetailRepo>()
                        .GetManyByExpression(x => x.ORG_CODE == orgCode && x.TIME_YEAR == year);
                    return (from data in revenuePLData.GroupBy(x => x.SUM_UP_VERSION).Select(x => x.First())
                            orderby data.SUM_UP_VERSION descending
                            select new NodeDataFlow
                            {
                                sumUpVersion = data.SUM_UP_VERSION,
                                version = data.SUM_UP_VERSION,
                                year = year,
                                realId = data.ORG_CODE,
                                id = $"{data.ORG_CODE}_{data.SUM_UP_VERSION}",
                                pId = null,
                                isParent = true.ToString(),
                                name = string.Concat(data.CostCenter.NAME, " - ", $"Lần tổng hợp thứ {data.SUM_UP_VERSION}")
                            }).ToList();
                }
                else
                {
                    // level 1: Center
                    // get centers
                    var revenuePLData = UnitOfWork.Repository<KeHoachChiPhiSumUpDetailRepo>()
                        .GetManyByExpression(x => x.ORG_CODE == orgCode && x.TIME_YEAR == year
                         && x.SUM_UP_VERSION == sumUpVersion.Value);
                    return (from data in revenuePLData.GroupBy(x => x.FROM_ORG_CODE).Select(x => x.First())
                            orderby data.FROM_ORG_CODE descending
                            select new NodeDataFlow
                            {
                                sumUpVersion = data.SUM_UP_VERSION,
                                version = data.SUM_UP_VERSION,
                                year = year,
                                realId = data.FROM_ORG_CODE,
                                id = $"{data.FROM_ORG_CODE}_{data.SUM_UP_VERSION}",
                                pId = $"{orgCode}_{version.Value}",
                                isParent = true.ToString(),
                                name = string.Concat(data.FromCostCenter.NAME, " - ", $"Lần tổng hợp thứ {data.SUM_UP_VERSION}")
                            }).ToList();
                }
            }
            else
            {
                // level 2: department
                // get templates
                var sumupDetails = UnitOfWork.Repository<KeHoachChiPhiSumUpDetailRepo>()
                    .GetManyByExpression(x => x.ORG_CODE.Equals(orgCode) && x.SUM_UP_VERSION == version && x.TIME_YEAR == year)
                    .OrderByDescending(x => x.TEMPLATE_CODE);
                return (from data in sumupDetails
                        select new NodeDataFlow
                        {
                            id = data.TEMPLATE_CODE,
                            pId = orgCode,
                            realId = data.FROM_ORG_CODE,
                            year = year,
                            isParent = (!IsLeaf(data.FROM_ORG_CODE)).ToString(),
                            name = string.Concat(data.TEMPLATE_CODE, " - ", data.FromCostCenter.NAME, " - Version: ", data.DATA_VERSION),
                            version = data.DATA_VERSION
                        }).ToList();
            }
        }

        #region Data Preview

        /// <summary>
        /// Get template to ObjDetail first before call this method
        /// Get data revenue elements include detail revenue elements.
        /// </summary>
        /// <param name="detailOtherKhoanMucHangHoas">out detail revenue elemts</param>
        /// <param name="year">which year of template</param>
        /// <param name="version">which version of template</param>
        /// <returns>Returns list revenue elemts with their parents and their value</returns>
        public IList<T_MD_KHOAN_MUC_HANG_HOA> GetDataCostPreview(
            out IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_CHI_PHI> detailOtherKhoanMucHangHoas,
            string templateId,
            string centerCode = "",
            int? year = null,
            int? version = null,
            bool? isHasValue = null)
        {
            var pureLstItems = PreparePureList(out detailOtherKhoanMucHangHoas, templateId, year.Value, centerCode);
            var sum = GetSumDescendants(detailOtherKhoanMucHangHoas, pureLstItems, parent_id: string.Empty, templateId, year, version).Distinct().ToList();
            if (isHasValue.HasValue)
            {
                if (isHasValue.Value)
                {
                    return sum.Where(x => x.Values.Sum() > 0).ToList();
                }
                else
                {
                    return sum.Where(x => x.Values.Sum() == 0 && !x.IS_GROUP).ToList();
                }
            }
            else
            {
                return sum;
            }
        }

        public IList<T_MD_KHOAN_MUC_HANG_HOA> PreparePureList(IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_CHI_PHI> detailOtherKhoanMucHangHoas, int year)
        {
            // get all revenue elements
            var allOtherKhoanMucHangHoas = UnitOfWork.Repository<KhoanMucHangHoaRepo>().GetManyByExpression(x => x.TIME_YEAR == year);
            // get revenue elements in details revenue elements
            var revenueElements = from d in detailOtherKhoanMucHangHoas
                                  select d.Element;
            // lookup revenue elements by center code
            var lookupElementsCenter = detailOtherKhoanMucHangHoas.ToLookup(x => x.CENTER_CODE);

            var pureLstItems = new List<T_MD_KHOAN_MUC_HANG_HOA>();
            // loop through all center
            foreach (var ctCode in lookupElementsCenter.Select(l => l.Key))
            {
                // lookup revenue elements
                var lookupElements = lookupElementsCenter[ctCode].ToLookup(x => x.Element.PARENT_CODE);
                foreach (var code in lookupElements.Select(l => l.Key))
                {
                    var level = 0;
                    // temp list
                    var lst = new List<T_MD_KHOAN_MUC_HANG_HOA>();
                    // add all leaf to temp list item
                    lst.AddRange(from item in lookupElements[code]
                                 select new T_MD_KHOAN_MUC_HANG_HOA
                                 {
                                     CENTER_CODE = ctCode,
                                     C_ORDER = item.Element.C_ORDER,
                                     NAME = item.Element.NAME,
                                     PARENT_CODE = item.Element.PARENT_CODE,
                                     CODE = item.Element.CODE,
                                     LEVEL = 0,
                                     TIME_YEAR = item.TIME_YEAR
                                 });
                    var parentCode = code;
                    while (!string.IsNullOrEmpty(parentCode))
                    {
                        level++;
                        // find parents to add into list
                        var element = allOtherKhoanMucHangHoas.FirstOrDefault(x => x.CODE == parentCode);
                        if (element != null)
                        {
                            parentCode = element.PARENT_CODE;
                            element.CENTER_CODE = ctCode;
                            element.LEVEL = level;
                            element.IS_GROUP = true;
                            lst.Add((T_MD_KHOAN_MUC_HANG_HOA)element.CloneObject());     // must to clone to other object because it reference to other center
                        }
                        else
                        {
                            break;
                        }
                    }

                    lst.ForEach(x => x.LEVEL = level - x.LEVEL);
                    pureLstItems.AddRange(lst);
                }

            }

            return pureLstItems.OrderBy(x => x.C_ORDER).ToList();
        }

        /// <summary>
        /// Xem theo template
        /// </summary>
        /// <param name="detailOtherKhoanMucHangHoas"></param>
        /// <param name="templateId"></param>
        /// <param name="centerCode"></param>
        /// <returns></returns>
        public IList<T_MD_KHOAN_MUC_HANG_HOA> PreparePureList(out IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_CHI_PHI> detailOtherKhoanMucHangHoas,
            string templateId,
            int year,
            string centerCode = "",
            bool ignoreAuth = false)
        {
            templateId = templateId ?? string.Empty;
            var template = GetTemplate(templateId);

            var currentUserCenterCode = ProfileUtilities.User.ORGANIZE_CODE;
            var childOrgOtherCosts = GetListOfChildrenCenter(currentUserCenterCode).Select(x => x.CODE);

            if (ignoreAuth || /*childOrgOtherCosts.Contains(template.ORG_CODE) ||*/ currentUserCenterCode.Equals(template.ORG_CODE) || template.ORG_CODE == centerCode)
            {
                detailOtherKhoanMucHangHoas = UnitOfWork.Repository<TemplateDetailKeHoachChiPhiRepo>()
                    .GetManyWithFetch(x => x.TEMPLATE_CODE.Equals(templateId) && x.TIME_YEAR == year, x => x.Center);
            }
            else
            {
                // get details revenue elements
                if (string.IsNullOrEmpty(centerCode))
                {
                    var lstChildCenterCodes = UnitOfWork.Repository<CostCenterRepo>()
                        .GetManyByExpression(x => x.PARENT_CODE == currentUserCenterCode || x.CODE.Equals(currentUserCenterCode))
                        .Select(x => x.CODE);
                    detailOtherKhoanMucHangHoas = UnitOfWork.Repository<TemplateDetailKeHoachChiPhiRepo>()
                        .GetManyByExpression(x => x.TEMPLATE_CODE.Equals(templateId) && lstChildCenterCodes.Contains(x.CENTER_CODE) && x.TIME_YEAR == year);
                }
                else
                {
                    detailOtherKhoanMucHangHoas = UnitOfWork.Repository<TemplateDetailKeHoachChiPhiRepo>()
                        .GetManyByExpression(x => x.TEMPLATE_CODE.Equals(templateId) && x.CENTER_CODE == centerCode && x.TIME_YEAR == year);
                }
            }
            return PreparePureList(detailOtherKhoanMucHangHoas, year);
        }

        /// <summary>
        /// Xem theo template
        /// </summary>
        /// <param name="detailOtherKhoanMucHangHoas"></param>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public IList<T_MD_KHOAN_MUC_HANG_HOA> PreparePureListForTemplate(out IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_CHI_PHI> detailOtherKhoanMucHangHoas,
            string templateId,
            int year)
        {

            detailOtherKhoanMucHangHoas = UnitOfWork.Repository<TemplateDetailKeHoachChiPhiRepo>()
                .GetManyByExpression(x => x.TEMPLATE_CODE.Equals(templateId) && x.TIME_YEAR == year);

            return PreparePureList(detailOtherKhoanMucHangHoas, year);
        }



        /// <summary>
        /// Xem theo center code
        /// </summary>
        /// <param name="detailOtherKhoanMucHangHoas"></param>
        /// <param name="centerCodes"></param>
        /// <returns></returns>
        public IList<T_MD_KHOAN_MUC_HANG_HOA> PreparePureList(out IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_CHI_PHI> detailOtherKhoanMucHangHoas,
            IList<string> centerCodes,
            int year)
        {
            // Tìm mẫu nộp hộ
            var listTemplateCodes = this.UnitOfWork.Repository<TemplateDetailKeHoachChiPhiRepo>()
                .GetManyByExpression(x => centerCodes.Contains(x.CENTER_CODE) && x.TIME_YEAR == year)
                .Select(x => x.TEMPLATE_CODE).Distinct().ToList();
            var findKeHoachChiPhi = this.CurrentRepository.GetManyByExpression(
                    x => listTemplateCodes.Contains(x.TEMPLATE_CODE) && !centerCodes.Contains(x.ORG_CODE));
            var lst = new List<T_MD_TEMPLATE_DETAIL_KE_HOACH_CHI_PHI>();
            detailOtherKhoanMucHangHoas = new List<T_MD_TEMPLATE_DETAIL_KE_HOACH_CHI_PHI>();
            foreach (var template in listTemplateCodes)
            {
                lst.AddRange(UnitOfWork.Repository<TemplateDetailKeHoachChiPhiRepo>()
                    .GetManyByExpression(x => x.TEMPLATE_CODE.Equals(template) && centerCodes.Contains(x.CENTER_CODE) && x.TIME_YEAR == year));
            }

            detailOtherKhoanMucHangHoas = lst;

            return PreparePureList(detailOtherKhoanMucHangHoas, year);
        }

        /// <summary>
        /// Sum up data revenue center by center code and year (Tổng hợp dữ liệu tại phòng ban)
        /// </summary>
        /// <param name="revenuePL">Output header revenue pl</param>
        /// <param name="centerCode">OtherCost center code want to sum up</param>
        /// <param name="year">Year want to sum up</param>
        public override void SumUpDataCenter(out T_BP_KE_HOACH_CHI_PHI_VERSION revenuePL, string centerCode, int year, string kichBan, string phienBan, string CostCenter)
        {
            if (string.IsNullOrEmpty(GetCenter(centerCode).PARENT_CODE))
            {
                // BTC tổng hợp ngân sách
                ValidateBudgetPeriod(year, BudgetPeriod.BTC_TONG_HOP_NS);
            }
            else
            {
                // TT tổng hợp ngân sách
                ValidateBudgetPeriod(year, BudgetPeriod.BAN_TT_TONG_HOP_NS);
            }
            this.CheckPeriodTimeValid(year);
            if (!this.State)
            {
                revenuePL = null;
                return;
            }

            var lstData = new List<T_BP_KE_HOACH_CHI_PHI_DATA>();
            revenuePL = new T_BP_KE_HOACH_CHI_PHI_VERSION();

            try
            {
                UnitOfWork.BeginTransaction();
                var sumUpDetailRepo = UnitOfWork.Repository<KeHoachChiPhiSumUpDetailRepo>();
                var revenuePLDataRepo = UnitOfWork.Repository<KeHoachChiPhiDataRepo>();

                // get list all children centers in revenue center tree
                var lstCostCenters = GetListOfChildrenCenter(centerCode);

                // get all data have approved
                var revenuePlDataApproved = revenuePLDataRepo.GetManyByExpression(x => x.ORG_CODE == centerCode && !string.IsNullOrEmpty(x.TEMPLATE_CODE) ||
                x.STATUS == Approve_Status.DaPheDuyet &&
                x.TIME_YEAR == year &&
                lstCostCenters.Any(c => c.CODE.Equals(x.ORG_CODE)));

                if (revenuePlDataApproved.Count == 0)
                {
                    // do not have data to sum up
                    State = false;
                    ErrorMessage = $"Do not have data to sum up in {year}";
                    return;
                }
                // add new record to pl sum up detail
                // get current version in sumup detail
                var newestDetail = sumUpDetailRepo
                    .GetNewestByExpression(x => x.ORG_CODE.Equals(centerCode), x => x.CREATE_DATE, true);
                var version = 1;
                if (newestDetail != null)
                {
                    version = newestDetail.SUM_UP_VERSION + 1;
                }

                // sumup element code with the same element code
                var lookup = revenuePlDataApproved.ToLookup(x => x.KHOAN_MUC_HANG_HOA_CODE);
                foreach (var code in lookup.Select(x => x.Key))
                {
                    // TODO: check if all value of months are equal 0
                    if (lookup[code].Count() == 1)
                    {
                        lstData.Add((T_BP_KE_HOACH_CHI_PHI_DATA)lookup[code].First().CloneObject());
                    }
                    else
                    {
                        lstData.Add(new T_BP_KE_HOACH_CHI_PHI_DATA
                        {
                            QUANTITY = lookup[code].Sum(x => x.QUANTITY),
                            PRICE = lookup[code].Sum(x => x.PRICE),
                            AMOUNT = lookup[code].Sum(x => x.AMOUNT),
                            KHOAN_MUC_HANG_HOA_CODE = lookup[code].First().KHOAN_MUC_HANG_HOA_CODE
                        });
                    }
                }

                // get current version in revenue pl data
                var newestCF = UnitOfWork.Repository<KeHoachChiPhiRepo>()
                    .GetNewestByExpression(x => x.ORG_CODE.Equals(centerCode) && x.TIME_YEAR == year, x => x.CREATE_DATE, true);
                var currentUser = ProfileUtilities.User?.USER_NAME;

                var versionPl = 1;
                if (newestCF != null)
                {
                    newestCF.VERSION++;
                    versionPl = newestCF.VERSION;
                    newestCF.UPDATE_BY = currentUser;
                    newestCF.STATUS = Approve_Status.ChuaTrinhDuyet;
                    newestCF.KICH_BAN = kichBan;
                    newestCF.PHIEN_BAN = phienBan;
                    CurrentRepository.Update(newestCF);
                }
                else
                {
                    // create header revenue pl
                    CurrentRepository.Create(new T_BP_KE_HOACH_CHI_PHI
                    {
                        PKID = Guid.NewGuid().ToString(),
                        IS_SUMUP = true,
                        ORG_CODE = centerCode,
                        TIME_YEAR = year,
                        VERSION = versionPl,
                        PHIEN_BAN = phienBan,
                        KICH_BAN = kichBan,
                        FILE_ID = string.Empty,
                        TEMPLATE_CODE = string.Empty,
                        STATUS = Approve_Status.ChuaTrinhDuyet,
                        CREATE_BY = currentUser
                    });
                }

                // insert to revenue pl version
                revenuePL = new T_BP_KE_HOACH_CHI_PHI_VERSION
                {
                    PKID = Guid.NewGuid().ToString(),
                    ORG_CODE = centerCode,
                    TEMPLATE_CODE = string.Empty,
                    KICH_BAN = kichBan,
                    PHIEN_BAN = phienBan,
                    VERSION = versionPl,
                    TIME_YEAR = year,
                    CREATE_BY = currentUser
                };
                UnitOfWork.Repository<KeHoachChiPhiVersionRepo>().Create(revenuePL);

                foreach (var item in lstData)
                {
                    item.ORG_CODE = centerCode;
                    item.CHI_PHI_PROFIT_CENTER_CODE = string.Empty;
                    item.TEMPLATE_CODE = string.Empty;
                    item.PKID = Guid.NewGuid().ToString();
                    item.VERSION = versionPl;
                    item.TIME_YEAR = year;
                    item.STATUS = Approve_Status.ChuaTrinhDuyet;
                    item.DESCRIPTION = string.Empty;
                    item.CREATE_BY = currentUser;
                }

                // get all revenue pl data current with centercode and year
                var lstOtherCostPlDataOldVersion = revenuePLDataRepo
                    .GetManyByExpression(x => x.ORG_CODE.Equals(centerCode) && x.TIME_YEAR == year);

                // delete them from table pl data
                // TODO: chỉnh sửa câu lệnh sql
                _ = UnitOfWork.GetSession().CreateSQLQuery($"DELETE FROM T_BP_KE_HOACH_CHI_PHI_DATA WHERE ORG_CODE = '{centerCode}' AND TIME_YEAR = {year}")
                    .ExecuteUpdate();

                // insert to pl data history
                UnitOfWork.Repository<KeHoachChiPhiDataHistoryRepo>().Create((from pl in lstOtherCostPlDataOldVersion
                                                                           select (T_BP_KE_HOACH_CHI_PHI_DATA_HISTORY)pl).ToList());

                // insert to pl history
                UnitOfWork.Repository<KeHoachChiPhiHistoryRepo>().Create(new T_BP_KE_HOACH_CHI_PHI_HISTORY
                {
                    ACTION = Approve_Action.TongHop,
                    ACTION_USER = currentUser,
                    CREATE_BY = currentUser,
                    ORG_CODE = ProfileUtilities.User.ORGANIZE_CODE,
                    ACTION_DATE = DateTime.Now,
                    PKID = Guid.NewGuid().ToString(),
                    TIME_YEAR = year,
                    TEMPLATE_CODE = string.Empty,
                    VERSION = revenuePL.VERSION,
                });

                // create new lstData
                revenuePLDataRepo.Create(lstData.ToList());

                // get list revenue pl which have org revenue in revenue center summary
                var lstKeHoachChiPhi = revenuePlDataApproved.ToLookup(x => new { OrgCode = x.ORG_CODE, TemplateCode = x.TEMPLATE_CODE, TemplateVersion = x.VERSION });

                // create list sum up detail
                sumUpDetailRepo.Create((from c in lstKeHoachChiPhi.Select(x => x.Key)
                                        select new T_BP_KE_HOACH_CHI_PHI_SUM_UP_DETAIL
                                        {
                                            PKID = Guid.NewGuid().ToString(),
                                            FROM_ORG_CODE = c.OrgCode,
                                            ORG_CODE = centerCode,
                                            TEMPLATE_CODE = c.TemplateCode,
                                            TIME_YEAR = year,
                                            DATA_VERSION = c.TemplateVersion,
                                            SUM_UP_VERSION = versionPl,
                                            CREATE_BY = currentUser
                                        }).ToList());
                UnitOfWork.Commit();

                // remove session
                foreach (var item in lstData)
                {
                    UnitOfWork.GetSession().Evict(item);
                }

                NotifyUtilities.CreateNotify(
                    new NotifyPara()
                    {
                        Activity = Activity.AC_TONG_HOP,
                        OrgCode = ProfileUtilities.User.ORGANIZE_CODE,
                        TimeYear = year,
                        ModulType = ModulType.KeHoachChiPhi,
                        UserSent = currentUser
                    });
            }
            catch (Exception e)
            {
                UnitOfWork.Rollback();
                State = false;
                ErrorMessage = e.Message;
                Exception = e;
                revenuePL = null;
            }
        }

        /// <summary>
        /// Summary sum up revenue center
        /// </summary>
        /// <param name="plDataOtherKhoanMucHangHoas">List of data revenue element output</param>
        /// <param name="year">Year want to summary</param>
        /// <param name="centerCode">OtherCost center code want to summary</param>
        /// <param name="version">Version want to summary</param>
        /// <returns>Returns list of revenue element with their value</returns>
        public IList<T_MD_KHOAN_MUC_HANG_HOA> SummarySumUpCenter(
            out IList<T_BP_KE_HOACH_CHI_PHI_DATA> plDataOtherKhoanMucHangHoas,
            int year,
            string centerCode,
            int? version,
            bool? isHasValue = null,
            string templateId = "")
        {
            // get newest revenue pl data by center code
            plDataOtherKhoanMucHangHoas = UnitOfWork.Repository<KeHoachChiPhiDataRepo>()
                .GetCFDataByCenterCode(null, new List<string> { centerCode }, year, templateId, version);
            plDataOtherKhoanMucHangHoas = plDataOtherKhoanMucHangHoas.Where(x => x.STATUS == Approve_Status.DaPheDuyet).ToList();
            return SummaryCenter(plDataOtherKhoanMucHangHoas, centerCode, year, isHasValue);
        }

        /// <summary>
        /// Calculate data of each revenue element with their parents.
        /// Data of parents calculated by sum of all children
        /// </summary>
        /// <param name="details">List detail revenue</param>
        /// <param name="pureItems">List elements want to calculate data</param>
        /// <param name="parent_id">Start calculate from which parent. string Empty to the root</param>
        /// <param name="year">Which year of data</param>
        /// <param name="version">Which version of data</param>
        /// <returns>Returns list of revenue element with their data</returns>
        private IEnumerable<T_MD_KHOAN_MUC_HANG_HOA> GetSumDescendants(
            IEnumerable<T_MD_TEMPLATE_DETAIL_KE_HOACH_CHI_PHI> details,
            IEnumerable<T_MD_KHOAN_MUC_HANG_HOA> pureItems,
            string parent_id,
            string templateId,
            int? year = null,
            int? version = null)
        {
            var lookupCenter = pureItems.ToLookup(x => x.CENTER_CODE);
            foreach (var centerCode in lookupCenter.Select(x => x.Key))
            {
                var revenuePlData = GetVersionData(templateId, centerCode, year, version);
                var items = lookupCenter[centerCode];

                var lookup = items.ToLookup(i => i.PARENT_CODE);
                Queue<T_MD_KHOAN_MUC_HANG_HOA> st = new Queue<T_MD_KHOAN_MUC_HANG_HOA>(lookup[parent_id]);

                while (st.Count > 0)
                {
                    // get first item in queue
                    var item = st.Dequeue();
                    // variable to check should return item or not
                    bool shouldReturn = true;
                    // lst to store children of item which have children
                    var lstHasChild = new List<T_MD_KHOAN_MUC_HANG_HOA>();
                    // loop through items which have parent id = item id
                    foreach (var i in lookup[item.CODE])
                    {
                        if (lookup[i.CODE].Count() > 0)
                        {
                            shouldReturn = false;
                            lstHasChild.Add(i);
                            st.Enqueue(i);
                        }
                        else
                        {
                            if (i.HasAssignValue)
                            {
                                // if i is total of its child
                                // add total to parent
                                for (int j = 0; j < item.Values.Length; j++)
                                {
                                    item.Values[j] += i.Values[j];
                                }
                            }
                            else
                            {
                                // if i does not count total yet
                                // get it datatree then add to total
                                var detail = details.FirstOrDefault(x => x.ELEMENT_CODE == i.CODE && x.CENTER_CODE == i.CENTER_CODE);
                                if (detail != null)
                                {
                                    detail.CFData = revenuePlData.FirstOrDefault(x => x.KHOAN_MUC_HANG_HOA_CODE == detail.ELEMENT_CODE && x.CHI_PHI_PROFIT_CENTER_CODE == detail.CENTER_CODE);
                                }
                                var treeData = detail?.CFData;
                                if (treeData != null)
                                {
                                    item.Values[0] += treeData.QUANTITY ?? 0;
                                    item.Values[1] += treeData.PRICE ?? 0;
                                    item.Values[2] += treeData.AMOUNT ?? 0;
                                    item.HasAssignValue = true;

                                    i.Values[0] = treeData.QUANTITY ?? 0;
                                    i.Values[1] = treeData.PRICE ?? 0;
                                    i.Values[2] = treeData.AMOUNT ?? 0;
                                    i.DESCRIPTION = treeData.DESCRIPTION;
                                }
                            }
                            yield return i;
                        }
                    }

                    // remove all child of item
                    // include children have child
                    lookup = lookup
                        .Where(x => x.Key != item.CODE)
                        .SelectMany(x => x)
                        .ToLookup(l => l.PARENT_CODE);
                    if (shouldReturn)
                    {
                        if (item.PARENT_CODE == parent_id && !item.IS_GROUP)
                        {
                            var data = revenuePlData.Where(x => x.KHOAN_MUC_HANG_HOA_CODE == item.CODE && x.CHI_PHI_PROFIT_CENTER_CODE == item.CENTER_CODE);
                            if (data != null)
                            {
                                item.Values = new decimal[3]
                                {
                                data.Sum(x => x.QUANTITY) ?? 0,
                                data.Sum(x => x.PRICE) ?? 0,
                                data.Sum(x => x.AMOUNT) ?? 0
                                };
                            }
                            yield return item;
                        }
                        else
                        {
                            // if item does not have child
                            // return it
                            yield return item;
                        }
                    }
                    else
                    {
                        // add children of item which have chilren to lookup 
                        if (lstHasChild.Count > 0)
                        {
                            lookup = lookup
                                .SelectMany(l => l)
                                .Concat(lstHasChild)
                                .ToLookup(x => x.PARENT_CODE);
                        }
                        // re-enqueue item to queue
                        st.Enqueue(item);
                    }
                }
            }
        }

        /// <summary>
        /// Get data by version
        /// </summary>
        /// <param name="templateCode">Template code of PL data</param>
        /// <param name="orgCode">Center code of PL data</param>
        /// <param name="year">Which year of data</param>
        /// <param name="version">Which version of data</param>
        /// <returns>Returns OtherCost PL Data</returns>
        private IList<T_BP_KE_HOACH_CHI_PHI_DATA> GetVersionData(
            string templateCode,
            string centerCode,
            int? year = null,
            int? version = null)
        {
            string orgCode = ProfileUtilities.User.ORGANIZE_CODE;
            var template = GetTemplate(templateCode);
            var lstChildren = GetListOfChildrenCenter(orgCode).Select(x => x.CODE);
            // check if orgCode is org code of template or not
            if (template.ORG_CODE.Equals(orgCode))
            {
                return UnitOfWork.Repository<KeHoachChiPhiDataRepo>()
                    .GetCFDataByOrgCode(orgCode, year.Value, templateCode, version);
            }
            else if (lstChildren.Contains(template.ORG_CODE))
            {
                return UnitOfWork.Repository<KeHoachChiPhiDataRepo>()
                    .GetCFDataByOrgCode(template.ORG_CODE, year.Value, templateCode, version);
            }
            else
            {
                return UnitOfWork.Repository<KeHoachChiPhiDataRepo>()
                    .GetCFDataByCenterCode(template.ORG_CODE, new List<string> { centerCode }, year.Value, templateCode, version);
            }
        }

        /// <summary>
        /// Summary data of a center with newest data
        /// </summary>
        /// <param name="plDataOtherKhoanMucHangHoas">List revenue pl data want to out</param>
        /// <param name="centerCode">Center code want to summary data</param>
        /// <returns>Returns list revenue elements with their data</returns>
        public IList<T_MD_KHOAN_MUC_HANG_HOA> SummaryCenterOut(out IList<T_BP_KE_HOACH_CHI_PHI_DATA> plDataOtherKhoanMucHangHoas,
                                                              string centerCode,
                                                              int year,
                                                              int? version,
                                                              bool? isHasValue = null)
        {
            if (!version.HasValue)
            {
                // get newest revenue pl data by center code
                plDataOtherKhoanMucHangHoas = UnitOfWork.Repository<KeHoachChiPhiDataRepo>()
                    .GetManyByExpression(x => x.ORG_CODE.Equals(centerCode) && x.TIME_YEAR == year);
            }
            else
            {
                ObjDetail.ORG_CODE = centerCode;
                if (IsLeaf())
                {
                    // get all data have approved
                    plDataOtherKhoanMucHangHoas = UnitOfWork.Repository<KeHoachChiPhiDataRepo>()
                        .GetManyByExpression(x => x.ORG_CODE == centerCode && !string.IsNullOrEmpty(x.TEMPLATE_CODE));
                }
                else
                {
                    // get list all children centers in revenue center tree
                    var lstCostCenters = UnitOfWork.Repository<CostCenterRepo>().GetManyByExpression(x => x.PARENT_CODE == centerCode);

                    // get all data have approved
                    plDataOtherKhoanMucHangHoas = UnitOfWork.Repository<KeHoachChiPhiDataRepo>()
                        .GetManyByExpression(x => x.STATUS == Approve_Status.DaPheDuyet && (x.ORG_CODE == centerCode && !string.IsNullOrEmpty(x.TEMPLATE_CODE) ||
                    x.TIME_YEAR == year &&
                    lstCostCenters.Any(c => c.CODE.Equals(x.ORG_CODE))));
                }
            }

            return SummaryCenter(plDataOtherKhoanMucHangHoas, centerCode, year, isHasValue);
        }

        /// <summary>
        /// Get data has summed up (history)
        /// Lấy dữ liệu đã được tổng hợp lên đơn vị cha theo version
        /// </summary>
        /// <param name="plDataOtherKhoanMucHangHoas">List out data</param>
        /// <param name="centerCode">Org code của đơn vị được tổng hợp</param>
        /// <param name="year">Năm dữ liệu muốn xem</param>
        /// <param name="version">Version của dữ liệu muốn xem. Null thì sẽ lấy mới nhất</param>
        /// <returns></returns>
        public IList<T_MD_KHOAN_MUC_HANG_HOA> SummaryCenterVersion(out IList<T_BP_KE_HOACH_CHI_PHI_DATA> plDataOtherKhoanMucHangHoas,
                                                              string centerCode,
                                                              int year,
                                                              int? version,
                                                              bool isDrillDown = false)
        {
            if (isDrillDown)
            {
                plDataOtherKhoanMucHangHoas = GetAllSumUpDetails(centerCode, year, version.Value);
            }
            else
            {
                plDataOtherKhoanMucHangHoas = UnitOfWork.Repository<KeHoachChiPhiDataRepo>()
                    .GetCFDataByOrgCode(centerCode, year, string.Empty, version);
            }
            return SummaryCenter(plDataOtherKhoanMucHangHoas, centerCode, year);
        }

        /// <summary>
        /// Lấy danh sách tất cả các data đã được tổng hợp lên cho centerCode theo version và năm
        /// </summary>
        /// <param name="centerCode"></param>
        /// <param name="year"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        private IList<T_BP_KE_HOACH_CHI_PHI_DATA> GetAllSumUpDetails(string centerCode, int year, int version)
        {
            var detailsSumUp = UnitOfWork.Repository<KeHoachChiPhiSumUpDetailRepo>()
               .GetManyWithFetch(x => x.ORG_CODE == centerCode && x.TIME_YEAR == year && x.SUM_UP_VERSION == version);

            var lstChildren = GetListOfChildrenCenter(centerCode).Select(x => x.CODE);
            var lstDetails = new List<T_BP_KE_HOACH_CHI_PHI_DATA>();
            var lstResult = new List<T_BP_KE_HOACH_CHI_PHI_DATA>();
            var plDataRepo = UnitOfWork.Repository<KeHoachChiPhiDataRepo>();
            foreach (var detail in detailsSumUp)
            {
                var details = plDataRepo
                    .GetCFDataByCenterCode(
                    detail.FROM_ORG_CODE,
                    lstChildren.ToList(),
                    year, detail.TEMPLATE_CODE,
                    detail.DATA_VERSION);
                if (IsLeaf(detail.FROM_ORG_CODE))
                {
                    details.ForEach(x => x.VERSION = detail.SUM_UP_VERSION);
                }
                else
                {
                    details.ForEach(x => x.VERSION = detail.DATA_VERSION);
                }
                lstDetails.AddRange(details);
            }

            var lookupElement = lstDetails.ToLookup(x => x.KHOAN_MUC_HANG_HOA_CODE);
            foreach (var key in lookupElement.Select(x => x.Key))
            {
                if (lookupElement[key].Count() == 1)
                {
                    lstResult.AddRange(lookupElement[key]);
                }
                else
                {
                    var data = lookupElement[key];
                    var lookupCenter = data.ToLookup(x => x.ORG_CODE);
                    foreach (var orgCode in lookupCenter.Select(x => x.Key))
                    {
                        if (lookupCenter[orgCode].Count() == 1)
                        {
                            lstResult.AddRange(lookupCenter[orgCode]);
                        }
                        else
                        {
                            var dataOrgCode = lookupCenter[orgCode];
                            lstResult.Add(new T_BP_KE_HOACH_CHI_PHI_DATA
                            {
                                ORG_CODE = orgCode,
                                KHOAN_MUC_HANG_HOA_CODE = key,
                                KhoanMucHangHoa = lookupCenter[orgCode].First().KhoanMucHangHoa,
                                ChiPhiProfitCenter = lookupCenter[orgCode].First().ChiPhiProfitCenter,
                                Organize = lookupCenter[orgCode].First().Organize,
                                QUANTITY = dataOrgCode.Sum(x => x.QUANTITY) ?? 0,
                                PRICE = dataOrgCode.Sum(x => x.PRICE) ?? 0,
                                AMOUNT = dataOrgCode.Sum(x => x.AMOUNT) ?? 0,
                            });
                        }
                    }

                }
            }
            return lstResult;
        }

        private IList<T_MD_KHOAN_MUC_HANG_HOA> SummaryCenter(IList<T_BP_KE_HOACH_CHI_PHI_DATA> plDataOtherKhoanMucHangHoas, string centerCode, int year,
                                                          bool? isHasValue = null)
        {

            // get all revenue elements
            var allOtherKhoanMucHangHoa = UnitOfWork.Repository<KhoanMucHangHoaRepo>().GetManyByExpression(x => x.TIME_YEAR == year);
            // get all child
            var childrenCodes = GetListOfChildrenCenter(centerCode).Select(x => x.CODE);
            // list store pure items to send to view
            var pureLstItems = new List<T_MD_KHOAN_MUC_HANG_HOA>();

            // lookup revenue elements by parent code
            var lookupElements = plDataOtherKhoanMucHangHoas.GroupBy(x => x.KHOAN_MUC_HANG_HOA_CODE)
                .Select(x => x.First())
                .ToLookup(x => x.KhoanMucHangHoa.PARENT_CODE);

            var childrenCode = GetListOfChildrenCenter(centerCode)
                .Select(x => x.CODE)
                .ToList();
            childrenCode.Add(centerCode);

            foreach (var code in lookupElements.Select(l => l.Key))
            {
                if (code == string.Empty)
                {
                    continue;
                }
                // set level start by 0
                var level = 0;
                // temp list
                var lst = new List<T_MD_KHOAN_MUC_HANG_HOA>();
                // add all leaf to temp list item with child = true
                lst.AddRange(from item in lookupElements[code]
                             select new T_MD_KHOAN_MUC_HANG_HOA
                             {
                                 CENTER_CODE = item.ORG_CODE,
                                 C_ORDER = item.KhoanMucHangHoa.C_ORDER,
                                 NAME = item.KhoanMucHangHoa.NAME,
                                 PARENT_CODE = item.KhoanMucHangHoa.PARENT_CODE,
                                 CODE = item.KhoanMucHangHoa.CODE,
                                 LEVEL = 0,
                                 IS_GROUP = false,
                                 ORG_NAME = childrenCode.Contains(item.ORG_CODE) ? item.Organize.NAME : item.ChiPhiProfitCenter.NAME,
                                 TEMPLATE_CODE = item.TEMPLATE_CODE,
                                 ORG_CODE = item.ORG_CODE,
                                 IsChildren = true
                             });
                // init parent code = code
                var parentCode = code;

                // find all hierachical parents 
                while (!string.IsNullOrEmpty(parentCode))
                {
                    // increasing level by 1
                    level++;
                    // find parents to add into list
                    var element = allOtherKhoanMucHangHoa.FirstOrDefault(x => x.CODE == parentCode);
                    if (element != null)
                    {
                        // if find parent in all revenue element
                        parentCode = element.PARENT_CODE;
                        element.CENTER_CODE = centerCode;
                        element.LEVEL = level;
                        element.IS_GROUP = true;
                        element.TEMPLATE_CODE = lookupElements[code].FirstOrDefault().TEMPLATE_CODE;
                        element.ORG_CODE = lookupElements[code].FirstOrDefault().ORG_CODE;
                        lst.Add((T_MD_KHOAN_MUC_HANG_HOA)element.CloneObject());     // must to clone to other object because it reference to other center
                    }
                    else
                    {
                        // it mean this element is the root so break of loop
                        break;
                    }
                }


                // subtract level so 0 level is root and higher level is child
                lst.ForEach(x => x.LEVEL = level - x.LEVEL);

                pureLstItems.AddRange(lst);
            }
            // add all leaf where parent is corp to temp list item with child = true
            pureLstItems.AddRange(from item in lookupElements[string.Empty]
                                  select new T_MD_KHOAN_MUC_HANG_HOA
                                  {
                                      CENTER_CODE = item.ORG_CODE,
                                      C_ORDER = item.KhoanMucHangHoa.C_ORDER,
                                      NAME = item.KhoanMucHangHoa.NAME,
                                      PARENT_CODE = string.Empty,
                                      CODE = item.KhoanMucHangHoa.CODE,
                                      LEVEL = 0,
                                      IS_GROUP = false,
                                      ORG_NAME = childrenCode.Contains(item.ORG_CODE) ? item.Organize.NAME : item.ChiPhiProfitCenter.NAME,
                                      TEMPLATE_CODE = item.TEMPLATE_CODE,
                                      ORG_CODE = item.ORG_CODE,
                                      IsChildren = true
                                  });

            // calculate data for all pure list item
            var sum = GetSumDescendants(plDataOtherKhoanMucHangHoas, pureLstItems, parentId: string.Empty).Distinct().ToList();
            if (isHasValue.HasValue)
            {
                if (isHasValue.Value)
                {
                    return sum.Where(x => x.Values.Sum() > 0).ToList();
                }
                else
                {
                    return sum.Where(x => x.Values.Sum() == 0 && !x.IS_GROUP).ToList();
                }
            }
            else
            {
                return sum;
            }

        }

        /// <summary>
        /// Calculate data for list revenue element
        /// </summary>
        /// <param name="costCFData">List revenue pl data containt value</param>
        /// <param name="pureItems">List of revenue element want to calculate</param>
        /// <param name="parentId">Parent id want to start. Empty for root</param>
        /// <returns>Returns list of revenue element has calculated data</returns>
        private IEnumerable<T_MD_KHOAN_MUC_HANG_HOA> GetSumDescendants(
            IList<T_BP_KE_HOACH_CHI_PHI_DATA> costCFData,
            IList<T_MD_KHOAN_MUC_HANG_HOA> pureItems,
            string parentId)
        {
            var lstResult = new List<T_MD_KHOAN_MUC_HANG_HOA>
            {
                // set tổng năm
                // tạo element tổng
                new T_MD_KHOAN_MUC_HANG_HOA
                {
                    NAME = "TỔNG CỘNG",
                    LEVEL = 0,
                    PARENT_CODE = null,
                    IS_GROUP = true,
                    IsChildren = false,
                    C_ORDER = 0,
                    CODE = string.Empty,
                    Values = new decimal[3]
                    {
                        costCFData.Sum(x => x.QUANTITY) ?? 0,
                        costCFData.Sum(x => x.PRICE) ?? 0,
                        costCFData.Sum(x => x.AMOUNT) ?? 0,
                    }
                }
            };
            var lookup = pureItems.ToLookup(i => i.PARENT_CODE);
            Queue<T_MD_KHOAN_MUC_HANG_HOA> st = new Queue<T_MD_KHOAN_MUC_HANG_HOA>(lookup[parentId]);
            while (st.Count > 0)
            {
                // get first item in queue
                var item = st.Dequeue();
                // variable to check should return item or not
                bool shouldReturn = true;
                // lst to store children of item which have children
                var lstOtherKhoanMucHangHoas = new List<T_MD_KHOAN_MUC_HANG_HOA>();
                // loop through items which have parent id = item id
                foreach (var i in lookup[item.CODE])
                {
                    if (lookup[i.CODE].Count() > 0)
                    {
                        shouldReturn = false;
                        lstOtherKhoanMucHangHoas.Add(i);
                        st.Enqueue(i);
                    }
                    else
                    {
                        if (i.HasAssignValue)
                        {
                            // if i is total of its child
                            // add total to parent
                            for (int j = 0; j < item.Values.Length; j++)
                            {
                                item.Values[j] += i.Values[j];
                            }
                        }
                        else
                        {
                            // if i does not count total yet
                            // get its datatree then add to total

                            var treeData = costCFData.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Equals(i.CODE));
                            if (treeData != null && treeData.Count() > 0)
                            {
                                item.Values[0] += treeData.Sum(x => x.QUANTITY) ?? 0;
                                item.Values[1] += treeData.Sum(x => x.PRICE) ?? 0;
                                item.Values[2] += treeData.Sum(x => x.AMOUNT) ?? 0;

                                item.HasAssignValue = true;

                                foreach (var d in treeData)
                                {
                                    var values = new decimal[14];
                                    values[0] = treeData.Sum(x => x.QUANTITY) ?? 0;
                                    values[1] = treeData.Sum(x => x.PRICE) ?? 0;
                                    values[2] = treeData.Sum(x => x.AMOUNT) ?? 0;

                                    i.Values = values;
                                    var clone = (T_MD_KHOAN_MUC_HANG_HOA)i.Clone();
                                    //yield return clone;
                                    lstResult.Add(clone);
                                    break;
                                }
                            }
                        }
                    }
                }

                if (!item.IS_GROUP && item.PARENT_CODE == parentId)
                {
                    var treeData = costCFData.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Equals(item.CODE));
                    if (treeData != null && treeData.Count() > 0)
                    {
                        item.Values[0] += treeData.Sum(x => x.QUANTITY) ?? 0;
                        item.Values[1] += treeData.Sum(x => x.PRICE) ?? 0;
                        item.Values[2] += treeData.Sum(x => x.AMOUNT) ?? 0;

                    }
                    var clone = (T_MD_KHOAN_MUC_HANG_HOA)item.Clone();
                    lstResult.Add(clone);
                }

                // remove all child of item
                // include children have child
                lookup = lookup
                .Where(x => x.Key != item.CODE)
                .SelectMany(x => x)
                .ToLookup(l => l.PARENT_CODE);
                if (shouldReturn)
                {
                    if (item.PARENT_CODE == parentId && !item.IS_GROUP)
                    {
                        var data = costCFData.Where(x => x.KHOAN_MUC_HANG_HOA_CODE == item.CODE && x.ORG_CODE == item.CENTER_CODE);
                        if (data != null)
                        {
                            item.Values = new decimal[3]
                            {
                                data.Sum(x => x.QUANTITY) ?? 0,
                                data.Sum(x => x.PRICE) ?? 0,
                                data.Sum(x => x.AMOUNT) ?? 0,
                            };
                        }
                    }
                    lstResult.Add(item);
                }
                else
                {
                    // add children of item which have chilren to lookup 
                    if (lstOtherKhoanMucHangHoas.Count > 0)
                    {
                        lookup = lookup
                            .SelectMany(l => l)
                            .Concat(lstOtherKhoanMucHangHoas)
                            .ToLookup(x => x.PARENT_CODE);
                    }
                    // re-enqueue item to queue
                    st.Enqueue(item);
                }
            }
            return lstResult;
        }

        #endregion

        #region Export excel from data center view
        public override void GenerateExportExcel(ref MemoryStream outFileStream, dynamic table, string path, int year, string centerCode, int? version, string templateId, string unit, decimal exchangeRate)
        {
            // Create a new workbook and a sheet named "User Accounts"
            //Mở file Template
            var htmlMonth = table.htmlMonth;
            var htmlYear = table.htmlYear;
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            IWorkbook workbook;
            workbook = new XSSFWorkbook(fs);
            workbook.SetSheetName(0, ModulType.GetTextSheetName(ModulType.KeHoachChiPhi));
            fs.Close();


            ISheet sheetMonth = workbook.GetSheetAt(0);
            var metaDataMonth = ExcelHelper.GetExcelMeta(htmlMonth);
            var NUM_CELL_MONTH = string.IsNullOrEmpty(templateId) ? 18 : 22;

            InitHeaderFile(ref sheetMonth, year, centerCode, version, NUM_CELL_MONTH, templateId, "Tấn", exchangeRate);
            ExcelHelperBP.InsertHeaderTable(ref workbook, ref sheetMonth, metaDataMonth.MetaTHead, NUM_CELL_MONTH, ignoreFirstColumn: string.IsNullOrEmpty(templateId) || (!string.IsNullOrEmpty(templateId) && GetTemplate(templateId).IS_BASE));
            ExcelHelperBP.InsertBodyTable(ref workbook,
                ref sheetMonth,
                metaDataMonth.MetaTBody,
                NUM_CELL_MONTH,
                ignoreFirstColumn: string.IsNullOrEmpty(templateId) || (!string.IsNullOrEmpty(templateId) && GetTemplate(templateId).IS_BASE));

            ISheet sheetYear = workbook.GetSheetAt(1);
            var metaDataYear = ExcelHelper.GetExcelMeta(htmlYear);
            var NUM_CELL_YEAR = 7;

            InitHeaderFile(ref sheetYear, year, centerCode, version, NUM_CELL_YEAR, templateId, "Tấn", exchangeRate);
            //ExcelHelperBP.InsertHeaderTable(ref workbook, ref sheetYear, metaDataYear.MetaTHead, NUM_CELL_YEAR, ignoreFirstColumn: string.IsNullOrEmpty(templateId) || (!string.IsNullOrEmpty(templateId) && GetTemplate(templateId).IS_BASE));
            ExcelHelperBP.InsertBodyTableByYear(ref workbook,
                ref sheetYear,
                metaDataYear.MetaTBody,
                NUM_CELL_YEAR,
                ignoreFirstColumn: string.IsNullOrEmpty(templateId) || (!string.IsNullOrEmpty(templateId) && GetTemplate(templateId).IS_BASE));




            // Save the Excel spreadsheet to a MemoryStream and return it to the client
            workbook.Write(outFileStream);
        }

        private void InitHeaderFile(ref ISheet sheet, int year, string centerCode, int? version, int NUM_CELL, string templateId, string unit, decimal exchangeRate)
        {
            var name = "DỮ LIỆU KẾ HOẠCH SẢN LƯỢNG";
            var centerName = GetCenter(centerCode).NAME.ToUpper();
            var template = GetTemplate(templateId);
            var templateName = template != null ? $"Mẫu khai báo: {template.CODE} - {template.NAME}" : "Tổng hợp dữ liệu";

            ExcelHelperBP.InitHeaderFile(ref sheet, year, centerName, version, NUM_CELL, templateName, "Tấn", name, exchangeRate);
        }

        #endregion
        /// <summary>
        /// Lấy danh sách file cơ sở
        /// </summary>
        /// <param name="year">Năm ngân sách</param>
        /// <param name="templateCode">Mã template</param>
        /// <param name="version">Version tổng hợp dữ liệu của đơn vị cha (trung tâm)</param>
        /// <param name="centerCode">Mã đơn vị con (ban)</param>
        /// <returns></returns>
        public override IList<T_CM_FILE_UPLOAD> GetFilesBase(int year, string templateCode, int version, string centerCode)
        {
            var parentCenterCode = GetCenter(centerCode).PARENT_CODE;
            var lstChildren = GetListOfChildrenCenter(parentCenterCode).Select(x => x.CODE);

            var lstDetails = UnitOfWork.Repository<KeHoachChiPhiSumUpDetailRepo>()
            .GetManyByExpression(x => x.ORG_CODE == parentCenterCode &&
                x.TIME_YEAR == year &&
                x.SUM_UP_VERSION == version);
            var templateVersion = lstDetails.FirstOrDefault(y => y.TEMPLATE_CODE == templateCode)?.DATA_VERSION;
            if (templateVersion.HasValue)
            {
                // get file upload base
                return UnitOfWork.Repository<KeHoachChiPhiVersionRepo>()
                    .GetManyWithFetch(x => x.TIME_YEAR == year && x.TEMPLATE_CODE == templateCode && x.VERSION == templateVersion.Value, x => x.FileUpload)
                    .Select(x => x.FileUpload)
                    .ToList();
            }
            else
            {
                return new List<T_CM_FILE_UPLOAD>();
            }

        }

        public override T_BP_TYPE GetBudgetType()
        {
            return UnitOfWork.Repository<TypeRepo>().GetFirstWithFetch(x => x.OBJECT_TYPE == TemplateObjectType.Project && x.ELEMENT_TYPE == ElementType.ChiPhi && x.BUDGET_TYPE == BudgetType.DongTien);
        }

        public T_BP_KE_HOACH_CHI_PHI CheckTemplate(string template, int year, string orgCode)
        {
            try
            {
                var checkTemplate = UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().FirstOrDefault(x => x.TEMPLATE_CODE == template && x.TIME_YEAR == year && x.ORG_CODE == orgCode);
                if (checkTemplate != null)
                {
                    return checkTemplate;
                }
                else
                {
                    return new T_BP_KE_HOACH_CHI_PHI();
                }
            }
            catch (Exception ex)
            {
                this.State = false;
                this.Exception = ex;
                return null;
            }

        }

    }
}
