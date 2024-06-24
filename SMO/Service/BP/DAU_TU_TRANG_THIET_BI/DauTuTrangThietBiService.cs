using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

using SMO.AppCode.Utilities;
using SMO.Core.Entities;
using SMO.Core.Entities.BP;
using SMO.Core.Entities.BP.DAU_TU_TRANG_THIET_BI;
using SMO.Core.Entities.BP.DAU_TU_TRANG_THIET_BI.DAU_TU_TRANG_THIET_BI_DATA_BASE;
using SMO.Core.Entities.BP.DAU_TU_XAY_DUNG;
using SMO.Core.Entities.MD;
using SMO.Helper;
using SMO.Models;
using SMO.Repository.Implement.BP;
using SMO.Repository.Implement.BP.DAU_TU_TRANG_THIET_BI;
using SMO.Repository.Implement.BP.DAU_TU_TRANG_THIET_BI.DAU_TU_TRANG_THIET_BI_DATA_BASE;
using SMO.Repository.Implement.BP.DAU_TU_XAY_DUNG;
using SMO.Repository.Implement.MD;
using SMO.Service.Class;
using SMO.Service.Common;
using SMO.ServiceInterface.BP.DauTuTrangThietBi;

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

using static SMO.SelectListUtilities;

namespace SMO.Service.BP.DAU_TU_TRANG_THIET_BI
{
    public class DauTuTrangThietBiService : BaseBPService<T_BP_DAU_TU_TRANG_THIET_BI, DauTuTrangThietBiRepo, T_MD_KHOAN_MUC_DAU_TU, T_BP_DAU_TU_TRANG_THIET_BI_VERSION, T_BP_DAU_TU_TRANG_THIET_BI_HISTORY, DauTuTrangThietBiHistoryRepo>, IDauTuTrangThietBiService
    {
        private readonly List<Point> InvalidCellsList;
        private readonly List<string> ListColumnName;
        private readonly List<string> ListElement;
        private readonly List<string> ListColumnNameDataBase;
        private int StartRowData;
        public List<T_BP_DAU_TU_TRANG_THIET_BI_HISTORY> ObjListHistory { get; set; }
        public List<T_BP_DAU_TU_TRANG_THIET_BI_VERSION> ObjListVersion { get; set; }
        public List<T_BP_DAU_TU_TRANG_THIET_BI_SUM_UP_DETAIL> ObjListSumUpHistory { get; set; }
        public DauTuTrangThietBiService()
        {
            this.StartRowData = 8;
            this.ListColumnName = new List<string> {
                "MÃ DỰ ÁN",
                "TÊN DỰ ÁN",
                "TỔNG MỨC ĐẦU TƯ",
                "NGUỒN VSCH",
                "TIẾN ĐỘ THỰC HIỆN",
                "TÊN KHOẢN MỤC",
                "LUỸ KẾ ĐẾN ĐẦU KỲ LẬP KH",
                "KH NĂM",
                "TIẾN ĐỘ TRIỂN KHAI TH NĂM KH",
                "LUỸ KẾ ĐẾN ĐẦU KỲ LẬP KH",
                "QKH NĂM",
                "GHI CHÚ",
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
            this.ObjListHistory = new List<T_BP_DAU_TU_TRANG_THIET_BI_HISTORY>();
            this.ObjListVersion = new List<T_BP_DAU_TU_TRANG_THIET_BI_VERSION>();
            this.ObjListSumUpHistory = new List<T_BP_DAU_TU_TRANG_THIET_BI_SUM_UP_DETAIL>();
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
                var historyReview = UnitOfWork.Repository<DauTuTrangThietBiReviewRepo>()
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
                        ModulType = ModulType.DauTuTrangThietBi,
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

                UnitOfWork.Repository<DauTuTrangThietBiHistoryRepo>()
                    .Create(new T_BP_DAU_TU_TRANG_THIET_BI_HISTORY()
                    {
                        PKID = Guid.NewGuid().ToString(),
                        ORG_CODE = ObjDetail.ORG_CODE,
                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                        KICH_BAN = ObjDetail.KICH_BAN,
                        PHIEN_BAN = ObjDetail.PHIEN_BAN,
                        VERSION = ObjDetail.VERSION,
                        TIME_YEAR = ObjDetail.TIME_YEAR,
                        ACTION = Approve_Action.TrinhDuyet,
                        ACTION_USER = currentUser,
                        ACTION_DATE = DateTime.Now,
                        CREATE_BY = currentUser
                    });

                //TODO: Cập nhật lại code thực thi
                UnitOfWork.GetSession().CreateSQLQuery($"UPDATE T_BP_DAU_TU_TRANG_THIET_BI_DATA SET STATUS = '{Approve_Status.ChoPheDuyet}' WHERE ORG_CODE = '{ObjDetail.ORG_CODE}' AND TEMPLATE_CODE = '{ObjDetail.TEMPLATE_CODE}' AND TIME_YEAR = {ObjDetail.TIME_YEAR}").ExecuteUpdate();

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

                UnitOfWork.Repository<DauTuTrangThietBiHistoryRepo>().Create(new T_BP_DAU_TU_TRANG_THIET_BI_HISTORY()
                {
                    PKID = Guid.NewGuid().ToString(),
                    ORG_CODE = ObjDetail.ORG_CODE,
                    TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                    KICH_BAN = ObjDetail.KICH_BAN,
                    PHIEN_BAN = ObjDetail.PHIEN_BAN,
                    VERSION = ObjDetail.VERSION,
                    TIME_YEAR = ObjDetail.TIME_YEAR,
                    ACTION = Approve_Action.HuyTrinhDuyet,
                    ACTION_USER = currentUser,
                    ACTION_DATE = DateTime.Now,
                    CREATE_BY = currentUser
                });

                //TODO: Cập nhật lại code thực thi
                UnitOfWork.GetSession().CreateSQLQuery($"UPDATE T_BP_DAU_TU_TRANG_THIET_BI_DATA SET STATUS = '{Approve_Status.ChuaTrinhDuyet}' WHERE ORG_CODE = '{ObjDetail.ORG_CODE}' AND TEMPLATE_CODE = '{ObjDetail.TEMPLATE_CODE}' AND TIME_YEAR = {ObjDetail.TIME_YEAR}").ExecuteUpdate();

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

                UnitOfWork.Repository<DauTuTrangThietBiHistoryRepo>()
                    .Create(new T_BP_DAU_TU_TRANG_THIET_BI_HISTORY()
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

                UnitOfWork.Repository<DauTuTrangThietBiHistoryRepo>().Create(new T_BP_DAU_TU_TRANG_THIET_BI_HISTORY()
                {
                    PKID = Guid.NewGuid().ToString(),
                    ORG_CODE = ObjDetail.ORG_CODE,
                    TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                    KICH_BAN = ObjDetail.KICH_BAN,
                    PHIEN_BAN = ObjDetail.PHIEN_BAN,
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

                UnitOfWork.Repository<DauTuTrangThietBiHistoryRepo>().Create(new T_BP_DAU_TU_TRANG_THIET_BI_HISTORY()
                {
                    PKID = Guid.NewGuid().ToString(),
                    ORG_CODE = ObjDetail.ORG_CODE,
                    TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                    KICH_BAN = ObjDetail.KICH_BAN,
                    PHIEN_BAN = ObjDetail.PHIEN_BAN,
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

                UnitOfWork.Repository<DauTuTrangThietBiHistoryRepo>().Create(new T_BP_DAU_TU_TRANG_THIET_BI_HISTORY()
                {
                    PKID = Guid.NewGuid().ToString(),
                    ORG_CODE = ObjDetail.ORG_CODE,
                    TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                    KICH_BAN = ObjDetail.KICH_BAN,
                    PHIEN_BAN = ObjDetail.PHIEN_BAN,
                    VERSION = ObjDetail.VERSION,
                    TIME_YEAR = ObjDetail.TIME_YEAR,
                    ACTION = Approve_Action.PheDuyetDuLieu,
                    ACTION_USER = currentUser,
                    ACTION_DATE = DateTime.Now,
                    CREATE_BY = currentUser
                });

                //TODO: Cập nhật lại code thực thi
                UnitOfWork.GetSession().CreateSQLQuery($"UPDATE T_BP_DAU_TU_TRANG_THIET_BI_DATA SET STATUS = '{Approve_Status.DaPheDuyet}' WHERE ORG_CODE = '{ObjDetail.ORG_CODE}' AND TEMPLATE_CODE = '{ObjDetail.TEMPLATE_CODE}' AND TIME_YEAR = {ObjDetail.TIME_YEAR}").ExecuteUpdate();

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
                UnitOfWork.Repository<DauTuTrangThietBiVersionRepo>().Update(x => x.TEMPLATE_CODE == ObjDetail.TEMPLATE_CODE && x.TIME_YEAR == ObjDetail.TIME_YEAR && x.VERSION == ObjDetail.VERSION, x => x.IS_DELETED = 1, x => x.UPDATE_BY = currentUser);

                // update main table
                ObjDetail.IS_DELETED = true; CurrentRepository.Update(ObjDetail);

                // create history log
                UnitOfWork.Repository<DauTuTrangThietBiHistoryRepo>().Create(new T_BP_DAU_TU_TRANG_THIET_BI_HISTORY
                {
                    PKID = Guid.NewGuid().ToString(),
                    ORG_CODE = ObjDetail.ORG_CODE,
                    TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                    KICH_BAN = ObjDetail.KICH_BAN,
                    PHIEN_BAN = ObjDetail.PHIEN_BAN,
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

                UnitOfWork.Repository<DauTuTrangThietBiHistoryRepo>().Create(new T_BP_DAU_TU_TRANG_THIET_BI_HISTORY()
                {
                    PKID = Guid.NewGuid().ToString(),
                    ORG_CODE = ObjDetail.ORG_CODE,
                    TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                    KICH_BAN = ObjDetail.KICH_BAN,
                    PHIEN_BAN = ObjDetail.PHIEN_BAN,
                    VERSION = ObjDetail.VERSION,
                    TIME_YEAR = ObjDetail.TIME_YEAR,
                    ACTION = Approve_Action.HuyPheDuyet,
                    ACTION_USER = currentUser,
                    ACTION_DATE = DateTime.Now,
                    CREATE_BY = currentUser
                });

                //TODO: Cập nhật lại code thực thi
                UnitOfWork.GetSession().CreateSQLQuery($"UPDATE T_BP_DAU_TU_TRANG_THIET_BI_DATA SET STATUS = '{Approve_Status.ChoPheDuyet}' WHERE ORG_CODE = '{ObjDetail.ORG_CODE}' AND TEMPLATE_CODE = '{ObjDetail.TEMPLATE_CODE}' AND TIME_YEAR = {ObjDetail.TIME_YEAR}").ExecuteUpdate();

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

                UnitOfWork.Repository<DauTuTrangThietBiHistoryRepo>().Create(new T_BP_DAU_TU_TRANG_THIET_BI_HISTORY()
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
                UnitOfWork.GetSession().CreateSQLQuery($"UPDATE T_BP_DAU_TU_TRANG_THIET_BI_DATA SET STATUS = '{Approve_Status.TuChoi}' WHERE ORG_CODE = '{ObjDetail.ORG_CODE}' AND TEMPLATE_CODE = '{ObjDetail.TEMPLATE_CODE}' AND TIME_YEAR = {ObjDetail.TIME_YEAR}").ExecuteUpdate();

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

                UnitOfWork.Repository<DauTuTrangThietBiHistoryRepo>()
                    .Create(new T_BP_DAU_TU_TRANG_THIET_BI_HISTORY()
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
                UnitOfWork.Repository<DauTuTrangThietBiReviewRepo>()
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
                var reviewHeader = UnitOfWork.Repository<DauTuTrangThietBiReviewRepo>()
                    .GetFirstWithFetch(x => x.ORG_CODE == orgCode && x.TIME_YEAR == year && x.DATA_VERSION == version && !x.IS_SUMMARY && x.REVIEW_USER == currentUser);
                if (reviewHeader != null)
                {
                    reviewHeader.IS_END = true;
                    UnitOfWork.Repository<DauTuTrangThietBiReviewRepo>().Update(reviewHeader);
                }

                var totalReviewUsers = UnitOfWork.Repository<UserReviewRepo>()
                    .GetManyWithFetch(x => x.TIME_YEAR == ObjDetail.TIME_YEAR).Count;

                var totalUsersEndReview = UnitOfWork.Repository<DauTuTrangThietBiReviewRepo>()
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

                UnitOfWork.Repository<DauTuTrangThietBiHistoryRepo>()
                    .Create(new T_BP_DAU_TU_TRANG_THIET_BI_HISTORY()
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

                var review = UnitOfWork.Repository<DauTuTrangThietBiReviewRepo>()
                    .GetFirstWithFetch(x => x.ORG_CODE == orgCode
                                                && x.TIME_YEAR == year
                                                && x.DATA_VERSION == version
                                                && !x.IS_SUMMARY
                                                && x.REVIEW_USER == currentUser);
                if (review == null)
                {
                    var reviewId = Guid.NewGuid().ToString();
                    UnitOfWork.Repository<DauTuTrangThietBiReviewRepo>().Create(
                        new T_BP_DAU_TU_TRANG_THIET_BI_REVIEW
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
                    var plReviewService = new DauTuTrangThietBiReviewService();
                    plReviewService.ObjDetail.TIME_YEAR = year;
                    plReviewService.ObjDetail.ORG_CODE = orgCode;
                    var dataCost = plReviewService.SummaryCenterVersion(out IList<T_BP_DAU_TU_TRANG_THIET_BI_DATA> detailKhoanMucDauTus);
                    dataCost = dataCost.OrderBy(x => x.C_ORDER)
                                                    .GroupBy(x => x.CODE)
                                                    .Select(x => x.First()).ToList();
                    var elements = plReviewService.PrepareListReview(dataCost);
                    UnitOfWork.Repository<DauTuTrangThietBiReviewResultRepo>()
                        .Create((from e in elements.Where(x => !x.IS_GROUP)
                                 select new T_BP_DAU_TU_TRANG_THIET_BI_REVIEW_RESULT
                                 {
                                     PKID = Guid.NewGuid().ToString(),
                                     KHOAN_MUC_DAU_TU_CODE = e.CODE,
                                     HEADER_ID = reviewId,
                                     RESULT = false,
                                     TIME_YEAR = year,
                                     CREATE_BY = currentUser
                                 }).ToList());
                }
                else
                {
                    review.IS_END = true;
                    UnitOfWork.Repository<DauTuTrangThietBiReviewRepo>().Update(review);
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

                UnitOfWork.Repository<DauTuTrangThietBiHistoryRepo>()
                    .Create(new T_BP_DAU_TU_TRANG_THIET_BI_HISTORY()
                    {
                        PKID = Guid.NewGuid().ToString(),
                        ORG_CODE = ObjDetail.ORG_CODE,
                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                        KICH_BAN = ObjDetail.KICH_BAN,
                        PHIEN_BAN = ObjDetail.PHIEN_BAN,
                        VERSION = ObjDetail.VERSION,
                        TIME_YEAR = ObjDetail.TIME_YEAR,
                        ACTION = Approve_Action.PheDuyetKiemSoat,
                        ACTION_USER = currentUser,
                        ACTION_DATE = DateTime.Now,
                        CREATE_BY = currentUser
                    });

                //TODO: Cập nhật lại code thực thi
                UnitOfWork.GetSession().CreateSQLQuery($"UPDATE T_BP_DAU_TU_TRANG_THIET_BI_DATA SET STATUS = '{Approve_Status.TKS_PheDuyet}' WHERE ORG_CODE = '{ObjDetail.ORG_CODE}' AND TEMPLATE_CODE = '{ObjDetail.TEMPLATE_CODE}' AND TIME_YEAR = {ObjDetail.TIME_YEAR}").ExecuteUpdate();

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
                UnitOfWork.Repository<DauTuTrangThietBiHistoryRepo>()
                    .Create(new T_BP_DAU_TU_TRANG_THIET_BI_HISTORY()
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
                UnitOfWork.GetSession().CreateSQLQuery($"UPDATE T_BP_DAU_TU_TRANG_THIET_BI_DATA SET STATUS = '{Approve_Status.TKS_TuChoi}' WHERE ORG_CODE = '{ObjDetail.ORG_CODE}' AND TEMPLATE_CODE = '{ObjDetail.TEMPLATE_CODE}' AND TIME_YEAR = {ObjDetail.TIME_YEAR}").ExecuteUpdate();
                UnitOfWork.Repository<DauTuTrangThietBiReviewRepo>()
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

                UnitOfWork.Repository<DauTuTrangThietBiHistoryRepo>()
                    .Create(new T_BP_DAU_TU_TRANG_THIET_BI_HISTORY()
                    {
                        PKID = Guid.NewGuid().ToString(),
                        ORG_CODE = ObjDetail.ORG_CODE,
                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                        KICH_BAN = ObjDetail.KICH_BAN,
                        PHIEN_BAN = ObjDetail.PHIEN_BAN,
                        VERSION = ObjDetail.VERSION,
                        TIME_YEAR = ObjDetail.TIME_YEAR,
                        ACTION = Approve_Action.TrinhDuyetKiemSoat,
                        ACTION_USER = currentUser,
                        ACTION_DATE = DateTime.Now,
                        CREATE_BY = currentUser
                    });
                //TODO: Cập nhật lại code thực thi
                UnitOfWork.GetSession().CreateSQLQuery($"UPDATE T_BP_DAU_TU_TRANG_THIET_BI_DATA SET STATUS = '{Approve_Status.TKS_TrinhDuyet}' WHERE ORG_CODE = '{ObjDetail.ORG_CODE}' AND TEMPLATE_CODE = '{ObjDetail.TEMPLATE_CODE}' AND TIME_YEAR = {ObjDetail.TIME_YEAR}").ExecuteUpdate();
                UnitOfWork.Repository<DauTuTrangThietBiReviewRepo>()
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
                UnitOfWork.Repository<DauTuTrangThietBiHistoryRepo>()
                    .Create(new T_BP_DAU_TU_TRANG_THIET_BI_HISTORY()
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
                UnitOfWork.Repository<DauTuTrangThietBiHistoryRepo>()
                    .Create(new T_BP_DAU_TU_TRANG_THIET_BI_HISTORY()
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
                       ModulType = ModulType.DauTuTrangThietBi,
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

                UnitOfWork.Repository<DauTuTrangThietBiHistoryRepo>()
                    .Create(new T_BP_DAU_TU_TRANG_THIET_BI_HISTORY()
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
                    UnitOfWork.Repository<DauTuTrangThietBiReviewRepo>().Update(x => x.DATA_VERSION == ObjDetail.VERSION
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

                UnitOfWork.Repository<DauTuTrangThietBiHistoryRepo>()
                    .Create(new T_BP_DAU_TU_TRANG_THIET_BI_HISTORY()
                    {
                        PKID = Guid.NewGuid().ToString(),
                        ORG_CODE = ObjDetail.ORG_CODE,
                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                        KICH_BAN = ObjDetail.KICH_BAN,
                        PHIEN_BAN = ObjDetail.PHIEN_BAN,
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
            if(orgCode == "1000")
            {
                var lstOrg = this.UnitOfWork.Repository<CostCenterRepo>().Queryable().Where(
                    x => x.PARENT_CODE == orgCode
                ).Select(x => x.CODE).ToList();
                List<string> lstChildOrg = new List<string>();
                foreach (var org in lstOrg)
                {
                    var lstChild = this.UnitOfWork.Repository<CostCenterRepo>().Queryable().Where(
                    x => x.PARENT_CODE == org
                    ).Select(x => x.CODE).ToList();
                    lstChildOrg.AddRange(lstChild);
                }
                this.ObjList = this.CurrentRepository.Queryable().Where(
                    x => lstChildOrg.Contains(x.ORG_CODE) && x.TIME_YEAR == this.ObjDetail.TIME_YEAR
                ).ToList();

                // Tìm mẫu nộp hộ
                var listTemplateCode = this.UnitOfWork.Repository<TemplateDetailDauTuTrangThietBiRepo>().Queryable().Where(
                        x => lstChildOrg.Contains(x.CENTER_CODE) && x.TIME_YEAR == this.ObjDetail.TIME_YEAR
                        && x.TIME_YEAR == (ObjDetail.TIME_YEAR == 0 ? DateTime.Now.Year : ObjDetail.TIME_YEAR)
                    ).Select(x => x.TEMPLATE_CODE).Distinct().ToList();
                var findDauTuTrangThietBi = this.CurrentRepository.Queryable().Where(
                        x => listTemplateCode.Contains(x.TEMPLATE_CODE) && !lstChildOrg.Contains(x.ORG_CODE) && x.TIME_YEAR == this.ObjDetail.TIME_YEAR
                    );

                this.ObjList.AddRange(findDauTuTrangThietBi);
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
            else
            {
                // Tìm tất cả đơn vị con
                var lstChildOrg = this.UnitOfWork.Repository<CostCenterRepo>().Queryable().Where(
                        x => x.PARENT_CODE == orgCode
                    ).Select(x => x.CODE).ToList();
                // Tìm ra tất cả các mẫu đã nộp của đơn vị con

                this.ObjList = this.CurrentRepository.Queryable().Where(
                        x => lstChildOrg.Contains(x.ORG_CODE) && x.TIME_YEAR == this.ObjDetail.TIME_YEAR
                    ).ToList();

                // Tìm mẫu nộp hộ
                var listTemplateCode = this.UnitOfWork.Repository<TemplateDetailDauTuTrangThietBiRepo>().Queryable().Where(
                        x => lstChildOrg.Contains(x.CENTER_CODE) && x.TIME_YEAR == this.ObjDetail.TIME_YEAR
                        && x.TIME_YEAR == (ObjDetail.TIME_YEAR == 0 ? DateTime.Now.Year : ObjDetail.TIME_YEAR)
                    ).Select(x => x.TEMPLATE_CODE).Distinct().ToList();
                var findDauTuTrangThietBi = this.CurrentRepository.Queryable().Where(
                        x => listTemplateCode.Contains(x.TEMPLATE_CODE) && !lstChildOrg.Contains(x.ORG_CODE) && x.TIME_YEAR == this.ObjDetail.TIME_YEAR
                    );

                this.ObjList.AddRange(findDauTuTrangThietBi);
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
            
        }

        /// <summary>
        /// Lấy lịch sử tổng hợp của một đơn vị
        /// </summary>
        /// <param name="orgCode"></param>
        public override void GetSumUpHistory(string orgCode, int year = 0, int version = 0)
        {
            var query = UnitOfWork.GetSession().QueryOver<T_BP_DAU_TU_TRANG_THIET_BI_SUM_UP_DETAIL>();
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
        private (IList<T_BP_DAU_TU_TRANG_THIET_BI_DATA>, bool) GetDataSumUp(int year, string centerCode, string elementCode, int sumUpVersion)
        {
            var plDataRepo = UnitOfWork.Repository<DauTuTrangThietBiDataRepo>();
            var costPlReviewCommentRepo = UnitOfWork.Repository<DauTuTrangThietBiReviewCommentRepo>();
            var plVersionRepo = UnitOfWork.Repository<DauTuTrangThietBiRepo>();
            var plDataHistoryRepo = UnitOfWork.Repository<DauTuTrangThietBiDataHistoryRepo>();

            var lstDetails = UnitOfWork.Repository<DauTuTrangThietBiSumUpDetailRepo>()
                .GetManyByExpression(x => x.ORG_CODE == centerCode &&
                    x.TIME_YEAR == year &&
                    x.SUM_UP_VERSION == sumUpVersion);
            var lookup = lstDetails.ToLookup(x => x.FROM_ORG_CODE);
            var lstChildren = GetListOfChildrenCenter(centerCode).Select(x => x.CODE);

            var isCorp = string.IsNullOrEmpty(GetCenter(centerCode).PARENT_CODE);
            var isLeafCenter = IsLeaf(centerCode);

            var data = new List<T_BP_DAU_TU_TRANG_THIET_BI_DATA>();
            foreach (var key in lookup.Select(x => x.Key))
            {
                var costPL = new List<T_BP_DAU_TU_TRANG_THIET_BI_DATA>();
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
                data.AddRange(costPL.Where(x => x.KHOAN_MUC_DAU_TU_CODE == elementCode &&
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
        public override IEnumerable<T_MD_KHOAN_MUC_DAU_TU> GetDetailSumUp(string centerCode, string elementCode, int year, int version, int? sumUpVersion, bool isCountComments, bool? isShowFile = null)
        {
            var plDataRepo = UnitOfWork.Repository<DauTuTrangThietBiDataRepo>();
            var costPlReviewCommentRepo = UnitOfWork.Repository<DauTuTrangThietBiReviewCommentRepo>();
            var plVersionRepo = UnitOfWork.Repository<DauTuTrangThietBiRepo>();
            var plDataHistoryRepo = UnitOfWork.Repository<DauTuTrangThietBiDataHistoryRepo>();

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
                        var element = (T_MD_KHOAN_MUC_DAU_TU)item;
                        element.IS_GROUP = isShowFile.HasValue; // count comment is in the review, drill down will show the file base so it still will be group
                        yield return element;
                    }
                    //if (lookupData[key].First().Template.IS_BASE)
                    //{
                    //    var isNewestVersion = UnitOfWork.Repository<DauTuTrangThietBiVersionRepo>()
                    //        .GetNewestByExpression(x => x.TEMPLATE_CODE == key && x.TIME_YEAR == year,
                    //        order: x => x.VERSION, isDescending: true)
                    //        .VERSION == lookupData[key].First().VERSION;
                    //    foreach (var item in lookupData[key])
                    //    {
                    //        var lstElements = lookupData[key].Where(x => x.DAU_TU_PROFIT_CENTER_CODE == item.DAU_TU_PROFIT_CENTER_CODE);
                    //        var element = (T_MD_KHOAN_MUC_DAU_TU)item;
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
                    //        var baseData = UnitOfWork.Repository<DauTuTrangThietBiDataBaseRepo>()
                    //            .GetManyWithFetch(x => x.KHOAN_MUC_DAU_TU_CODE == elementCode && x.ORG_CODE == centerCode && x.TEMPLATE_CODE == key && x.VERSION == lookupData[key].First().VERSION && x.TIME_YEAR == year);
                    //        foreach (var item in baseData)
                    //        {
                    //            yield return (T_MD_KHOAN_MUC_DAU_TU)item;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        // get data from base history
                    //        var baseDataHistory = UnitOfWork.Repository<DauTuTrangThietBiDataBaseHistoryRepo>()
                    //            .GetManyWithFetch(x => x.KHOAN_MUC_DAU_TU_CODE == elementCode && x.ORG_CODE == centerCode && x.TEMPLATE_CODE == key && x.VERSION == lookupData[key].First().VERSION && x.TIME_YEAR == year);

                    //        foreach (var item in baseDataHistory)
                    //        {
                    //            yield return (T_MD_KHOAN_MUC_DAU_TU)item;
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    foreach (var item in lookupData[key])
                    //    {
                    //        var element = (T_MD_KHOAN_MUC_DAU_TU)item;
                    //        element.IS_GROUP = isShowFile.HasValue; // count comment is in the review, drill down will show the file base so it still will be group
                    //        yield return element;
                    //    }
                    //}
                }
            }
            else
            {
                var comments = new List<T_BP_DAU_TU_TRANG_THIET_BI_REVIEW_COMMENT>();
                if (isCountComments)
                {
                    comments = costPlReviewCommentRepo.GetManyWithFetch(
                            x => x.TIME_YEAR == year &&
                            x.ORG_CODE == GetCorp().CODE &&
                            x.KHOAN_MUC_DAU_TU_CODE == elementCode).ToList();
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
                    yield return new T_MD_KHOAN_MUC_DAU_TU
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
                        Values = new decimal[1]
                            {
                                lookupData[key].Sum(x => x.VALUE) ?? 0,
                            },
                        NUMBER_COMMENTS = isCountComments ? isChild ?
                        $"{parentComments}" :
                        $"{parentComments + childComments}|{parentComments}" : $"{childComments}|0"
                    };
                }
            }
        }


        public override IEnumerable<T_MD_KHOAN_MUC_DAU_TU> GetDetailSumUpTemplate(string elementCode, int year, int version, string templateCode, string centerCode)
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
                    var baseData = UnitOfWork.Repository<DauTuTrangThietBiDataBaseRepo>()
                        .GetManyWithFetch(x => x.TEMPLATE_CODE == templateCode
                        && x.TIME_YEAR == year
                        && x.KHOAN_MUC_DAU_TU_CODE == elementCode
                        && x.VERSION == version
                        && x.DAU_TU_PROFIT_CENTER_CODE == centerCode);
                    return from item in baseData
                           select (T_MD_KHOAN_MUC_DAU_TU)item;
                }
                else
                {
                    // old data
                    // get data in table base data history
                    var baseData = UnitOfWork.Repository<DauTuTrangThietBiDataBaseHistoryRepo>()
                        .GetManyWithFetch(x => x.TEMPLATE_CODE == templateCode
                        && x.TIME_YEAR == year
                        && x.KHOAN_MUC_DAU_TU_CODE == elementCode
                        && x.VERSION == version
                        && x.DAU_TU_PROFIT_CENTER_CODE == centerCode);
                    return from item in baseData
                           select (T_MD_KHOAN_MUC_DAU_TU)item;
                }
            }
        }

        public IList<T_MD_KHOAN_MUC_DAU_TU> GetDataCost(out IList<T_MD_TEMPLATE_DETAIL_DAU_TU_TRANG_THIET_BI> detailOtherKhoanMucDauTus,
            out IList<T_BP_DAU_TU_TRANG_THIET_BI_DATA> detailOtherCostData,
            out bool isDrillDownApply,
            ViewDataCenterModel model)
        {
            isDrillDownApply = model.IS_DRILL_DOWN;
            if (!model.IS_HAS_NOT_VALUE && !model.IS_HAS_VALUE &&
                (!string.IsNullOrEmpty(model.TEMPLATE_CODE) || model.VERSION == null || model.VERSION.Value == -1))
            {
                detailOtherCostData = null;
                detailOtherKhoanMucDauTus = null;
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
                var elements = GetDataCostPreview(out detailOtherKhoanMucDauTus, model.TEMPLATE_CODE, lstOrgs, model.YEAR, model.VERSION, isHasValue);
                var sumElements = new T_MD_KHOAN_MUC_DAU_TU
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
                    Values = new decimal[6]
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
                detailOtherKhoanMucDauTus = null;
                // disabled drill down
                isDrillDownApply = false;
                return SummarySumUpCenter(out detailOtherCostData, model.YEAR, model.ORG_CODE, null, isHasValue, templateId: null);
            }
            else
            {
                // xem dữ liệu được tổng hợp cho đơn vị
                detailOtherKhoanMucDauTus = null;
                /* return SummaryCenterVersion(out detailOtherCostData, model.ORG_CODE, model.YEAR, model.VERSION, model.IS_DRILL_DOWN);*/
                return SummaryDauTu(out detailOtherCostData, model.ORG_CODE, model.YEAR, model.VERSION);
            }
        }

        public IList<T_MD_KHOAN_MUC_DAU_TU> SummaryDauTu(out IList<T_BP_DAU_TU_TRANG_THIET_BI_DATA> plDataOtherKhoanMucDauTus, string orgCode, int year, int? version)
        {
            plDataOtherKhoanMucDauTus = UnitOfWork.Repository<DauTuTrangThietBiDataRepo>()
                    .GetCFDataByOrgCode(orgCode, year, string.Empty, version);
            var lstDataSumUp = UnitOfWork.Repository<DauTuTrangThietBiDataRepo>().Queryable().Where(x => x.ORG_CODE == orgCode && x.TIME_YEAR == year && x.VERSION == version).ToList();
            var lstElementDauTu = new List<T_MD_KHOAN_MUC_DAU_TU>();
            foreach (var item in lstDataSumUp)
            {
                var element = new T_MD_KHOAN_MUC_DAU_TU
                {
                    DauTuTrangThietBiProfitCenter = item.DauTuTrangThietBiProfitCenter == null ? new T_MD_DAU_TU_TRANG_THIET_BI_PROFIT_CENTER() : item.DauTuTrangThietBiProfitCenter,
                    CODE = item.KHOAN_MUC_DAU_TU_CODE,
                    CENTER_CODE = item.DAU_TU_PROFIT_CENTER_CODE,
                    PROCESS = item.PROCESS,
                    Values = new decimal[1]
                    {
                        item.VALUE??0
                    }
                };
                lstElementDauTu.Add(element);
            }
            return lstElementDauTu;
        }

        public override IList<T_MD_KHOAN_MUC_DAU_TU> GetDetailPreviewSumUp(string centerCode, string elementCode, int year)
        {
            var plVersionRepo = UnitOfWork.Repository<DauTuTrangThietBiRepo>();
            var plDataRepo = UnitOfWork.Repository<DauTuTrangThietBiDataRepo>();
            var plDataHistoryRepo = UnitOfWork.Repository<DauTuTrangThietBiDataHistoryRepo>();

            var plDataOtherKhoanMucDauTus = UnitOfWork.Repository<DauTuTrangThietBiDataRepo>().GetCFDataByCenterCode(null, new List<string> { centerCode }, year, null, null);

            return plDataOtherKhoanMucDauTus.Where(x => x.KHOAN_MUC_DAU_TU_CODE == elementCode)
                .Select(x => (T_MD_KHOAN_MUC_DAU_TU)x)
                .OrderBy(x => x.C_ORDER).ToList();
        }

        private IList<T_MD_KHOAN_MUC_DAU_TU> GetDataCostPreview(out IList<T_MD_TEMPLATE_DETAIL_DAU_TU_TRANG_THIET_BI> detailOtherKhoanMucDauTus, string templateId, List<string> lstOrgs, int year, int? version, bool? isHasValue)
        {
            detailOtherKhoanMucDauTus = new List<T_MD_TEMPLATE_DETAIL_DAU_TU_TRANG_THIET_BI>();
            var lstElements = new List<T_MD_KHOAN_MUC_DAU_TU>();
            foreach (var orgCode in lstOrgs)
            {
                var elements = GetDataCostPreview(out IList<T_MD_TEMPLATE_DETAIL_DAU_TU_TRANG_THIET_BI> detail, templateId, orgCode, year, version, isHasValue);
                detailOtherKhoanMucDauTus = SummaryElement(detailOtherKhoanMucDauTus, detail);
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
        private IList<T_MD_KHOAN_MUC_DAU_TU> SummaryUpElement(IList<T_MD_KHOAN_MUC_DAU_TU> lst1, IList<T_MD_KHOAN_MUC_DAU_TU> lst2)
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

        public override T_BP_DAU_TU_TRANG_THIET_BI_VERSION GetHeader(string templateCode, string centerCode, int year, int? version)
        {
            templateCode = templateCode ?? string.Empty;
            if (version == null)
            {
                return UnitOfWork.Repository<DauTuTrangThietBiVersionRepo>()
                .GetManyByExpression(x =>
                x.TIME_YEAR == year &&
                x.ORG_CODE == centerCode &&
                x.TEMPLATE_CODE == templateCode).OrderByDescending(x => x.VERSION).FirstOrDefault();
            }
            else
            {
                if (!string.IsNullOrEmpty(templateCode))
                {
                    return UnitOfWork.Repository<DauTuTrangThietBiVersionRepo>()
                    .GetFirstByExpression(x =>
                    x.TIME_YEAR == year &&
                    x.VERSION == version &&
                    x.TEMPLATE_CODE == templateCode);
                }
                else
                {
                    return UnitOfWork.Repository<DauTuTrangThietBiVersionRepo>()
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
            ObjListVersion = UnitOfWork.Repository<DauTuTrangThietBiVersionRepo>()
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
            var query = this.UnitOfWork.GetSession().QueryOver<T_BP_DAU_TU_TRANG_THIET_BI_HISTORY>();
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
                this.ObjListHistory = UnitOfWork.Repository<DauTuTrangThietBiHistoryRepo>().GetManyByExpression(
                    x => x.ORG_CODE == orgCode &&
                    x.TEMPLATE_CODE == templateId &&
                    x.TIME_YEAR == year.Value
                ).ToList();
            }
            else
            {
                this.ObjListHistory = UnitOfWork.Repository<DauTuTrangThietBiHistoryRepo>().GetManyByExpression(
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


            var lstDetailTemplate = this.UnitOfWork.Repository<TemplateDetailDauTuTrangThietBiRepo>()
                .GetManyWithFetch(x => x.TIME_YEAR == this.ObjDetail.TIME_YEAR && x.TEMPLATE_CODE == this.ObjDetail.TEMPLATE_CODE,
                x => x.Center);

            var lstElement = this.UnitOfWork.Repository<KhoanMucDauTuRepo>().GetManyByExpression(x => x.TIME_YEAR == this.ObjDetail.TIME_YEAR);

            // Kiểm tra mẫu này đã được thiết kế cho năm ngân sách đang chọn chưa
            if (lstDetailTemplate.Count == 0)
            {
                this.State = false;
                this.ErrorMessage = $"Mẫu khai báo [{this.ObjDetail.TEMPLATE_CODE}] năm [{this.ObjDetail.TIME_YEAR}] chưa định nghĩa các khoản mục!";
                return;
            }

            // Kiểm tra file excel có dữ liệu từ dòng thứ StartRowData hay không
            if (dataTable == null || dataTable.Rows.Count < this.StartRowData)
            {
                this.State = false;
                this.ErrorMessage = "File excel này không có dữ liệu!";
                return;
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
                /*if (dataTable.Rows.Count == this.StartRowData)
                {
                    this.State = false;
                    this.ErrorMessage = "File excel này không có dữ liệu!";
                    return;
                }*/
            }

        }

        /// <summary>
        /// Convert data về số
        /// </summary>
        /// <param name="dataTable"></param>
        public override void ConvertData(DataTable dataTable, List<T_MD_KHOAN_MUC_DAU_TU> lstElement, int startColumn, int endColumn, bool isDataBase)
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
            var DauTuTrangThietBiCurrent = CurrentRepository.Queryable().FirstOrDefault(x => x.ORG_CODE == orgCode
                && x.TIME_YEAR == ObjDetail.TIME_YEAR && x.TEMPLATE_CODE == ObjDetail.TEMPLATE_CODE);

            if (DauTuTrangThietBiCurrent != null && !(DauTuTrangThietBiCurrent.STATUS == Approve_Status.TuChoi || DauTuTrangThietBiCurrent.STATUS == Approve_Status.ChuaTrinhDuyet))
            {
                this.State = false;
                this.ErrorMessage = "Mẫu khai báo này không ở trạng thái Từ chối hoặc Chưa trình duyệt!";
                return;
            }

            var versionNext = 1;
            if (DauTuTrangThietBiCurrent != null)
            {
                versionNext = DauTuTrangThietBiCurrent.VERSION + 1;
                ObjDetail.PHIEN_BAN = DauTuTrangThietBiCurrent.PHIEN_BAN;

            }

            // Lấy dữ liệu của version hiện tại
            var dataCurrent = new List<T_BP_DAU_TU_TRANG_THIET_BI_DATA>();
            if (DauTuTrangThietBiCurrent != null)
            {
                dataCurrent = UnitOfWork.Repository<DauTuTrangThietBiDataRepo>().Queryable().Where(x => x.ORG_CODE == orgCode
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
                if (DauTuTrangThietBiCurrent != null)
                {
                    // Cập nhật next version vào bảng chính
                    DauTuTrangThietBiCurrent.VERSION = versionNext;
                    DauTuTrangThietBiCurrent.IS_DELETED = false;
                    CurrentRepository.Update(DauTuTrangThietBiCurrent);
                }
                else
                {
                    // Tạo mới bản ghi revenue pl
                    CurrentRepository.Create(new T_BP_DAU_TU_TRANG_THIET_BI()
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
                UnitOfWork.Repository<DauTuTrangThietBiVersionRepo>().Create(new T_BP_DAU_TU_TRANG_THIET_BI_VERSION()
                {
                    PKID = Guid.NewGuid().ToString(),
                    ORG_CODE = orgCode,
                    TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                    VERSION = versionNext,
                    KICH_BAN = DauTuTrangThietBiCurrent == null ? ObjDetail.KICH_BAN : DauTuTrangThietBiCurrent.KICH_BAN,
                    PHIEN_BAN = DauTuTrangThietBiCurrent == null ? ObjDetail.PHIEN_BAN : DauTuTrangThietBiCurrent.PHIEN_BAN,
                    TIME_YEAR = ObjDetail.TIME_YEAR,
                    FILE_ID = fileStream.PKID,
                    CREATE_BY = currentUser
                });

                // Tạo mới bản ghi log trạng thái
                UnitOfWork.Repository<DauTuTrangThietBiHistoryRepo>().Create(new T_BP_DAU_TU_TRANG_THIET_BI_HISTORY()
                {
                    PKID = Guid.NewGuid().ToString(),
                    ORG_CODE = orgCode,
                    TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                    KICH_BAN = DauTuTrangThietBiCurrent == null ? ObjDetail.KICH_BAN : DauTuTrangThietBiCurrent.KICH_BAN,
                    PHIEN_BAN = DauTuTrangThietBiCurrent == null ? ObjDetail.PHIEN_BAN : DauTuTrangThietBiCurrent.PHIEN_BAN,
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
                    var revenueDataHis = (T_BP_DAU_TU_TRANG_THIET_BI_DATA_HISTORY)item;
                    UnitOfWork.Repository<DauTuTrangThietBiDataHistoryRepo>().Create(revenueDataHis);
                    UnitOfWork.Repository<DauTuTrangThietBiDataRepo>().Delete(item);
                }

                var lstElement = UnitOfWork.Repository<TemplateDetailDauTuTrangThietBiRepo>().Queryable().Where(x => x.TEMPLATE_CODE == ObjDetail.TEMPLATE_CODE && x.TIME_YEAR == ObjDetail.TIME_YEAR).ToList();
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
                        centerCode = UnitOfWork.Repository<TemplateDetailDauTuTrangThietBiRepo>().Queryable().FirstOrDefault(
                                                x => x.TEMPLATE_CODE == ObjDetail.TEMPLATE_CODE &&
                                                x.Center.PROJECT_CODE == tableData.Rows[i][0].ToString().Trim())?.CENTER_CODE;
                    }
                    else
                    {
                        centerCode = UnitOfWork.Repository<DauTuTrangThietBiProfitCenterRepo>().Queryable().FirstOrDefault(
                                                x => x.Organize.CODE == tableData.Rows[i][5].ToString().Trim() &&
                                                x.Project.CODE == tableData.Rows[i][7].ToString().Trim())?.CODE;
                    }

                    if (centerCode == null)
                    {
                        throw new Exception($"Định dạng file không đúng ở dòng thứ {i + 1}");
                    }

                    var costData = new T_BP_DAU_TU_TRANG_THIET_BI_DATA();

                    if (ObjDetail.TYPE_UPLOAD == "01")
                    {
                        var value_1 = tableData.Rows[i][3].ToString();
                        var value_4 = tableData.Rows[i][6].ToString();
                        var value_5 = tableData.Rows[i][7].ToString();
                        var value_6 = tableData.Rows[i][8].ToString();
                        var value_7 = tableData.Rows[i][9].ToString();
                        var value_9 = tableData.Rows[i][11].ToString();
                        if (!decimal.TryParse(value_1, out decimal valuestr) && !string.IsNullOrEmpty(value_1))
                        {
                            this.State = false;
                            this.ErrorMessage = $"Sai định dạng ở dòng D{i}";
                            return;
                        }
                        if (!decimal.TryParse(value_4, out decimal valuestr1) && !string.IsNullOrEmpty(value_4))
                        {
                            this.State = false;
                            this.ErrorMessage = $"Sai định dạng ở dòng G{i}";
                            return;
                        }
                        if (!decimal.TryParse(value_5, out decimal valuestr2) && !string.IsNullOrEmpty(value_5))
                        {
                            this.State = false;
                            this.ErrorMessage = $"Sai định dạng ở dòng H{i}";
                            return;
                        }
                        if (!decimal.TryParse(value_6, out decimal valuestr3) && !string.IsNullOrEmpty(value_6))
                        {
                            this.State = false;
                            this.ErrorMessage = $"Sai định dạng ở dòng I{i}";
                            return;
                        }
                        if (!decimal.TryParse(value_7, out decimal valuestr4) && !string.IsNullOrEmpty(value_7))
                        {
                            this.State = false;
                            this.ErrorMessage = $"Sai định dạng ở dòng J{i}";
                            return;
                        }
                        if (!decimal.TryParse(value_9, out decimal valuestr5) && !string.IsNullOrEmpty(value_9))
                        {
                            this.State = false;
                            this.ErrorMessage = $"Sai định dạng ở dòng L{i}";
                            return;
                        }
                        if (ObjDetail.PHIEN_BAN == "PB1")
                        {
                            var value1 = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][3].ToString()) ? 0 : tableData.Rows[i][3]);
                            var value2 = tableData.Rows[i][4].ToString();
                            var value3 = tableData.Rows[i][5].ToString();
                            var value4 = tableData.Rows[i][6].ToString();
                            var value5 = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? 0 : tableData.Rows[i][7]);
                            var value6 = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? 0 : tableData.Rows[i][8]);
                            var value7 = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][9].ToString()) ? 0 : tableData.Rows[i][9]);
                            var value8 = tableData.Rows[i][10].ToString();
                            var value9 = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? 0 : tableData.Rows[i][11]);
                            var value10 = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][12].ToString()) ? 0 : tableData.Rows[i][12]);
                            var description = tableData.Rows[i][13].ToString();
                            var elementCode = tableData.Rows[i][1].ToString();
                            if (!string.IsNullOrEmpty(elementCode))
                            {
                                costData = new T_BP_DAU_TU_TRANG_THIET_BI_DATA()
                                {
                                    PKID = Guid.NewGuid().ToString(),
                                    ORG_CODE = orgCode,
                                    DAU_TU_PROFIT_CENTER_CODE = centerCode,
                                    TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                                    TIME_YEAR = ObjDetail.TIME_YEAR,
                                    STATUS = Approve_Status.ChuaTrinhDuyet,
                                    VERSION = versionNext,
                                    KHOAN_MUC_DAU_TU_CODE = elementCode,
                                    VALUE_1 = value1,
                                    VALUE_2 = value2,
                                    VALUE_3 = value3,
                                    VALUE_4 = value4,
                                    VALUE_5 = value5,
                                    VALUE_6 = value6,
                                    VALUE_7 = value7,
                                    VALUE_8 = value8,
                                    VALUE_9 = value9,
                                    VALUE_10 = value10,
                                    DESCRIPTION = description,
                                    CREATE_BY = currentUser
                                };
                                UnitOfWork.Repository<DauTuTrangThietBiDataRepo>().Create(costData);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            var value1 = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][3].ToString()) ? 0 : tableData.Rows[i][3]);
                            var value2 = tableData.Rows[i][4].ToString();
                            var value3 = tableData.Rows[i][5].ToString();
                            var value4 = tableData.Rows[i][6].ToString();
                            var value5 = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? 0 : tableData.Rows[i][7]);
                            var value6 = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? 0 : tableData.Rows[i][8]);
                            var value7 = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][9].ToString()) ? 0 : tableData.Rows[i][9]);
                            var value8 = tableData.Rows[i][10].ToString();
                            var value9 = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? 0 : tableData.Rows[i][11]);
                            var value10 = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][12].ToString()) ? 0 : tableData.Rows[i][12]);
                            var description = tableData.Rows[i][13].ToString();
                            var elementCode = tableData.Rows[i][1].ToString();
                            if (!string.IsNullOrEmpty(elementCode))
                            {
                                for (int j = 1; j <= 12; j++)
                                {
                                    costData = new T_BP_DAU_TU_TRANG_THIET_BI_DATA()
                                    {
                                        PKID = Guid.NewGuid().ToString(),
                                        ORG_CODE = orgCode,
                                        DAU_TU_PROFIT_CENTER_CODE = centerCode,
                                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                                        TIME_YEAR = ObjDetail.TIME_YEAR,
                                        STATUS = Approve_Status.ChuaTrinhDuyet,
                                        VERSION = versionNext,
                                        KHOAN_MUC_DAU_TU_CODE = elementCode,
                                        VALUE_1 = value1,
                                        VALUE_2 = value2,
                                        VALUE_3 = value3,
                                        VALUE_4 = value4,
                                        VALUE_5 = value5,
                                        VALUE_6 = value6,
                                        VALUE_7 = value5 * value6,
                                        VALUE_8 = value8,
                                        VALUE_9 = value9,
                                        VALUE_10 = value10,
                                        MONTH = j,
                                        DESCRIPTION = description,
                                        CREATE_BY = currentUser
                                    };
                                    UnitOfWork.Repository<DauTuTrangThietBiDataRepo>().Create(costData);
                                }
                            }

                        }
                    }
                    else
                    {
                        costData = new T_BP_DAU_TU_TRANG_THIET_BI_DATA()
                        {
                            PKID = Guid.NewGuid().ToString(),
                            ORG_CODE = orgCode,
                            DAU_TU_PROFIT_CENTER_CODE = centerCode,
                            TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                            TIME_YEAR = ObjDetail.TIME_YEAR,
                            STATUS = Approve_Status.ChuaTrinhDuyet,
                            VERSION = versionNext,
                            KHOAN_MUC_DAU_TU_CODE = (tableData.Rows[i][9].ToString().Trim() == "0") ? "10010" : "10020",
                            VALUE = tableData.Rows[i][11] as decimal? == null ? 0 : tableData.Rows[i][11] as decimal?,

                            DESCRIPTION = "",
                            CREATE_BY = currentUser
                        };
                    }
                    
                    //UnitOfWork.Repository<DauTuTrangThietBiDataRepo>().Create(costData);
                }
                UnitOfWork.Commit();
                NotifyUtilities.CreateNotify(
                    new NotifyPara()
                    {
                        Activity = Activity.AC_NHAP_DU_LIEU,
                        OrgCode = orgCode,
                        TemplateCode = ObjDetail.TEMPLATE_CODE,
                        TimeYear = ObjDetail.TIME_YEAR,
                        ModulType = ModulType.DauTuTrangThietBi,
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

        public IList<T_MD_KHOAN_MUC_DAU_TU> GetDataDauTu(ViewDataCenterModel model)
        {
            var lstKhoanMuc = new List<T_MD_KHOAN_MUC_DAU_TU>();
            // Lấy danh sách dự án
            var lstData = UnitOfWork.Repository<DauTuTrangThietBiDataRepo>().Queryable().Where(x => x.TEMPLATE_CODE == model.TEMPLATE_CODE && x.ORG_CODE == model.ORG_CODE && x.VERSION == model.VERSION && x.TIME_YEAR == model.YEAR).OrderByDescending(x => x.DauTuTrangThietBiProfitCenter.Project.TYPE).ThenBy(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE).ToList();
            lstData = model.PHIEN_BAN != "PB1" ? lstData.Where(x => x.MONTH == model.MONTH).ToList() : lstData;
            var lstProfit = lstData.Select(x => x.DauTuTrangThietBiProfitCenter).Distinct().ToList();

            var Order = 0;
            foreach (var profit in lstProfit)
            {
                var item = new T_MD_KHOAN_MUC_DAU_TU
                {
                    PKID = profit.PROJECT_CODE,
                    PROJECT_CODE = profit.PROJECT_CODE,
                    PROJECT_NAME = profit.Project.NAME,
                    CODE = profit.PROJECT_CODE,
                    PARENT_CODE = string.Empty,
                    TYPE = profit.Project.TYPE == "TTB-LON" ? string.Empty : profit.Project.TYPE,
                    VALUETTB_1 = lstData.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == profit.CODE).Sum(x => x.VALUE_1) == null ? 0 : Convert.ToDecimal(lstData.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == profit.CODE).Sum(x => x.VALUE_1)),
                    VALUETTB_2 = profit.Project.TYPE == "TTB-LON" ? string.Empty : lstData.FirstOrDefault(x => x.DAU_TU_PROFIT_CENTER_CODE == profit.CODE).VALUE_2,
                    VALUETTB_3 = profit.Project.TYPE == "TTB-LON" ? string.Empty : lstData.FirstOrDefault(x => x.DAU_TU_PROFIT_CENTER_CODE == profit.CODE).VALUE_3,
                    VALUETTB_4 = profit.Project.TYPE == "TTB-LON" ? string.Empty : lstData.FirstOrDefault(x => x.DAU_TU_PROFIT_CENTER_CODE == profit.CODE).VALUE_4,
                    VALUETTB_5 = lstData.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == profit.CODE).Sum(x => x.VALUE_5) == null ? 0 : Convert.ToDecimal(lstData.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == profit.CODE).Sum(x => x.VALUE_5)),
                    VALUETTB_6 = lstData.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == profit.CODE).Sum(x => x.VALUE_6) == null ? 0 : Convert.ToDecimal(lstData.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == profit.CODE).Sum(x => x.VALUE_6)),
                    VALUETTB_7 = lstData.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == profit.CODE).Sum(x => x.VALUE_7) == null ? 0 : Convert.ToDecimal(lstData.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == profit.CODE).Sum(x => x.VALUE_7)),
                    VALUETTB_8 = profit.Project.TYPE == "TTB-LON" ? string.Empty : lstData.FirstOrDefault(x => x.DAU_TU_PROFIT_CENTER_CODE == profit.CODE).VALUE_8,
                    VALUETTB_9 = lstData.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == profit.CODE).Sum(x => x.VALUE_9) == null ? 0 : Convert.ToDecimal(lstData.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == profit.CODE).Sum(x => x.VALUE_9)),
                    VALUETTB_10 = lstData.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == profit.CODE).Sum(x => x.VALUE_10) == null ? 0 : Convert.ToDecimal(lstData.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == profit.CODE).Sum(x => x.VALUE_10)),

                    ISEDIT = profit.Project.TYPE == "TTB-LON" ? false : true,
                    DESCRIPTION = profit.Project.TYPE == "TTB-LON" ? string.Empty : lstData.FirstOrDefault(x => x.DAU_TU_PROFIT_CENTER_CODE == profit.CODE).DESCRIPTION,
                    ORDER = Order,
                    LEVEL = 0,
                    IS_GROUP = profit.Project.TYPE == "TTB-LON" ? true : false,
                };
                lstKhoanMuc.Add(item);
                var lstgiaiDoan = UnitOfWork.Repository<GiaiDoanDauTuRepo>().Queryable().Where(x => x.TYPE == profit.Project.TYPE).ToList();
                if (lstgiaiDoan.Count > 1)
                {
                    foreach (var gd in lstgiaiDoan)
                    {
                        var child = lstData.FirstOrDefault(x => x.DAU_TU_PROFIT_CENTER_CODE == profit.CODE && x.KHOAN_MUC_DAU_TU_CODE == gd.CODE);
                        var itemChild = new T_MD_KHOAN_MUC_DAU_TU
                        {
                            PKID = profit.PROJECT_CODE + gd.CODE,
                            PROJECT_CODE = profit.PROJECT_CODE,
                            PROJECT_NAME = gd?.TEXT,
                            PARENT_CODE = profit.PROJECT_CODE,
                            CODE = string.Empty,
                            TYPE = gd.CODE,
                            VALUETTB_1 = child.VALUE_1 == null ? 0 : Convert.ToDecimal(child.VALUE_1),
                            VALUETTB_2 = child.VALUE_2,
                            VALUETTB_3 = child.VALUE_3,
                            VALUETTB_4 = child.VALUE_4,
                            VALUETTB_5 = child.VALUE_5 == null ? 0 : Convert.ToDecimal(child.VALUE_5),
                            VALUETTB_6 = child.VALUE_6 == null ? 0 : Convert.ToDecimal(child.VALUE_6),
                            VALUETTB_7 = child.VALUE_7 == null ? 0 : Convert.ToDecimal(child.VALUE_7),
                            VALUETTB_8 = child.VALUE_8,
                            VALUETTB_9 = child.VALUE_9 == null ? 0 : Convert.ToDecimal(child.VALUE_9),
                            VALUETTB_10 = child.VALUE_10 == null ? 0 : Convert.ToDecimal(child.VALUE_10),
                            ISEDIT = profit.Project.TYPE == "TTB-LON" ? true : false,
                            ORDER = Order++,
                            DESCRIPTION = child.DESCRIPTION,
                            LEVEL = 1
                        };
                        lstKhoanMuc.Add(itemChild);
                        Order++;
                    }
                }
                Order++;
            }

            return lstKhoanMuc;
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
            var DauTuTrangThietBiDataRepo = UnitOfWork.Repository<DauTuTrangThietBiDataRepo>();
            var DauTuTrangThietBiDataBaseRepo = UnitOfWork.Repository<DauTuTrangThietBiDataBaseRepo>();
            var currentUser = ProfileUtilities.User?.USER_NAME;

            // Lưu file vào database
            var fileStream = new FILE_STREAM()
            {
                PKID = Guid.NewGuid().ToString(),
                FILESTREAM = request.Files[0]
            };
            FileStreamService.InsertFile(fileStream);

            // Xác định version dữ liệu
            var DauTuTrangThietBiCurrent = GetFirstWithFetch(x => x.ORG_CODE == orgCode
                && x.TIME_YEAR == ObjDetail.TIME_YEAR && x.TEMPLATE_CODE == ObjDetail.TEMPLATE_CODE);

            if (DauTuTrangThietBiCurrent != null && !(DauTuTrangThietBiCurrent.STATUS == Approve_Status.TuChoi || DauTuTrangThietBiCurrent.STATUS == Approve_Status.ChuaTrinhDuyet))
            {
                this.State = false;
                this.ErrorMessage = "Mẫu khai báo này không ở trạng thái Từ chối hoặc Chưa trình duyệt!";
                return;
            }

            var versionNext = 1;
            if (DauTuTrangThietBiCurrent != null)
            {
                versionNext = DauTuTrangThietBiCurrent.VERSION + 1;
            }

            // Lấy dữ liệu của version hiện tại
            var dataCurrent = new List<T_BP_DAU_TU_TRANG_THIET_BI_DATA>();
            var dataCurrentBase = new List<T_BP_DAU_TU_TRANG_THIET_BI_DATA_BASE>();
            if (DauTuTrangThietBiCurrent != null)
            {
                dataCurrent = DauTuTrangThietBiDataRepo.GetManyWithFetch(x => x.ORG_CODE == orgCode
                    && x.TIME_YEAR == ObjDetail.TIME_YEAR && x.TEMPLATE_CODE == ObjDetail.TEMPLATE_CODE).ToList();

                dataCurrentBase = DauTuTrangThietBiDataBaseRepo.GetManyWithFetch(x => x.ORG_CODE == orgCode
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
                if (DauTuTrangThietBiCurrent != null)
                {
                    // Cập nhật next version vào bảng chính
                    DauTuTrangThietBiCurrent.VERSION = versionNext;
                    DauTuTrangThietBiCurrent.IS_DELETED = false;
                    CurrentRepository.Update(DauTuTrangThietBiCurrent);
                }
                else
                {
                    // Tạo mới bản ghi cost pl
                    CurrentRepository.Create(new T_BP_DAU_TU_TRANG_THIET_BI()
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
                UnitOfWork.Repository<DauTuTrangThietBiVersionRepo>().Create(new T_BP_DAU_TU_TRANG_THIET_BI_VERSION()
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
                UnitOfWork.Repository<DauTuTrangThietBiHistoryRepo>().Create(new T_BP_DAU_TU_TRANG_THIET_BI_HISTORY()
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
                    var costDataHis = (T_BP_DAU_TU_TRANG_THIET_BI_DATA_HISTORY)item;
                    UnitOfWork.Repository<DauTuTrangThietBiDataHistoryRepo>().Create(costDataHis);
                    DauTuTrangThietBiDataRepo.Delete(item);
                }

                // Insert dữ liệu vào bảng data
                var lstRowValues = new List<DataRow>();
                for (int i = StartRowData; i < actualRows; i++)
                {
                    lstRowValues.Add(tableData.Rows[i]);
                }

                var allDauTuProfitCenters = UnitOfWork.Repository<DauTuTrangThietBiProfitCenterRepo>().GetAll();
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
                    var centerCode = allDauTuProfitCenters.FirstOrDefault(
                            x => x.ORG_CODE == key.ProjectCode &&
                            x.PROJECT_CODE == key.CompanyCode)?.CODE;
                    if (centerCode == null)
                    {
                        throw new Exception($"Định dạng file không đúng");
                    }
                    var costData = new T_BP_DAU_TU_TRANG_THIET_BI_DATA()
                    {
                        PKID = Guid.NewGuid().ToString(),
                        ORG_CODE = orgCode,
                        DAU_TU_PROFIT_CENTER_CODE = centerCode,
                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                        TIME_YEAR = ObjDetail.TIME_YEAR,
                        STATUS = Approve_Status.ChuaTrinhDuyet,
                        VERSION = versionNext,
                        KHOAN_MUC_DAU_TU_CODE = key.ElementCode,
                        VALUE = lookUp[key].Sum(x => UtilsCore.StringToDecimal(x[11].ToString())),                
                        CREATE_BY = currentUser
                    };
                    DauTuTrangThietBiDataRepo.Create(costData);
                }

                // Insert data vào base data history
                var lstBaseDataHistory = (from x in dataCurrentBase
                                          select (T_BP_DAU_TU_TRANG_THIET_BI_DATA_BASE_HISTORY)x).ToList();
                UnitOfWork.Repository<DauTuTrangThietBiDataBaseHistoryRepo>().Create(lstObj: lstBaseDataHistory);
                DauTuTrangThietBiDataBaseRepo.Delete(dataCurrentBase);

                // Insert dữ liệu vào bảng data
                for (int i = this.StartRowData; i < actualRows; i++)
                {
                    int j = 8;
                    var centerCode = allDauTuProfitCenters.FirstOrDefault(
                            x => x.ORG_CODE == tableData.Rows[i][0].ToString().Trim() &&
                            x.PROJECT_CODE == tableData.Rows[i][2].ToString().Trim())?.CODE;
                    if (centerCode == null)
                    {
                        throw new Exception($"Định dạng file không đúng");
                    }
                    var costData = new T_BP_DAU_TU_TRANG_THIET_BI_DATA_BASE()
                    {
                        PKID = Guid.NewGuid().ToString(),
                        ORG_CODE = orgCode,
                        DAU_TU_PROFIT_CENTER_CODE = centerCode,
                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                        TIME_YEAR = ObjDetail.TIME_YEAR,
                        VERSION = versionNext,
                        KHOAN_MUC_DAU_TU_CODE = tableData.Rows[i][4].ToString().Trim(),
                        MATERIAL = tableData.Rows[i][6].ToString().Trim(),
                        UNIT = tableData.Rows[i][7].ToString().Trim(),

                        QUANTITY = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),
                        TIME = tableData.Rows[i][j++].ToString().Trim(),
                        PRICE= UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),
                        AMOUNT = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),
                        DESCRIPTION = tableData.Rows[i][ListColumnNameDataBase.Count - 1].ToString().Trim(),

                        CREATE_BY = currentUser
                    };
                    DauTuTrangThietBiDataBaseRepo.Create(costData);
                }
                UnitOfWork.Commit();

                NotifyUtilities.CreateNotify(
                    new NotifyPara()
                    {
                        Activity = Activity.AC_NHAP_DU_LIEU,
                        OrgCode = orgCode,
                        TemplateCode = ObjDetail.TEMPLATE_CODE,
                        TimeYear = ObjDetail.TIME_YEAR,
                        ModulType = ModulType.DauTuTrangThietBi,
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

        public override IList<T_BP_DAU_TU_TRANG_THIET_BI_VERSION> GetVersions(string orgCode, string templateId, int year)
        {
            templateId = templateId ?? string.Empty;
            var lstVersions = GetVersionsNumber(orgCode, templateId, year,"", "");
            return UnitOfWork.Repository<DauTuTrangThietBiVersionRepo>()
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
            return UnitOfWork.Repository<DauTuTrangThietBiVersionRepo>()
                .GetManyByExpression(x => x.ORG_CODE == orgCode && x.TEMPLATE_CODE != string.Empty)
                .Select(x => x.TEMPLATE_CODE)
                .Distinct()
                .ToList();
        }

        private IList<string> GetTemplateData(List<string> lstOrgCodes, int? year = null)
        {
            return UnitOfWork.Repository<DauTuTrangThietBiDataRepo>()
                .GetManyByExpression(x => lstOrgCodes.Contains(x.ORG_CODE) && x.TEMPLATE_CODE != string.Empty && (!year.HasValue || x.TIME_YEAR == year.Value))
                .Select(x => x.TEMPLATE_CODE)
                .Distinct()
                .ToList();
        }

        private IList<string> GetTemplateDataHistory(List<string> lstOrgCodes, int? year = null)
        {
            return UnitOfWork.Repository<DauTuTrangThietBiDataHistoryRepo>()
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
                return UnitOfWork.Repository<DauTuTrangThietBiVersionRepo>()
                    .GetManyByExpression(x => lstOrgCodes.Contains(x.ORG_CODE))
                    .Select(x => x.TIME_YEAR)
                    .Distinct()
                    .ToList();
            }
            else
            {
                return UnitOfWork.Repository<DauTuTrangThietBiVersionRepo>()
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
                return UnitOfWork.Repository<DauTuTrangThietBiDataHistoryRepo>()
                    .GetManyByExpression(x => lstOrgCodes.Contains(x.ORG_CODE))
                    .Select(x => x.TIME_YEAR)
                    .Distinct()
                    .ToList();
            }
            else
            {
                return UnitOfWork.Repository<DauTuTrangThietBiDataHistoryRepo>()
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
                return UnitOfWork.Repository<DauTuTrangThietBiDataRepo>()
                    .GetManyByExpression(x => lstOrgCodes.Contains(x.ORG_CODE))
                    .Select(x => x.TIME_YEAR)
                    .Distinct()
                    .ToList();
            }
            else
            {
                return UnitOfWork.Repository<DauTuTrangThietBiDataRepo>()
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
                return UnitOfWork.Repository<DauTuTrangThietBiVersionRepo>()
                    .GetManyWithFetch(x => x.TEMPLATE_CODE == templateId && x.TIME_YEAR == year && x.KICH_BAN == kichBan && x.PHIEN_BAN == phienBan)
                    .Select(x => x.VERSION)
                    .OrderByDescending(x => x)
                    .ToList();
            }
            else
            {
                return UnitOfWork.Repository<DauTuTrangThietBiVersionRepo>()
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
                return UnitOfWork.Repository<DauTuTrangThietBiSumUpDetailRepo>()
                    .GetManyByExpression(x => x.ORG_CODE == centerCode && x.TIME_YEAR == year)
                    .Select(x => x.DATA_VERSION).Distinct().OrderByDescending(x => x).ToList();
            }
            else
            {
                return UnitOfWork.Repository<DauTuTrangThietBiVersionRepo>()
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
            var dataOtherCost = GetDataTemPlate(templateId, year);

            if (dataOtherCost.Count == 0)
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
                templateWorkbook.SetSheetName(0, ModulType.GetTextSheetName(ModulType.DauTuTrangThietBi));
                fs.Close();
                ISheet sheet = templateWorkbook.GetSheetAt(0);

                //Số hàng và số cột hiện tại
                int numRowCur = 0;
                int NUM_CELL = 14;

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

                ICellStyle styleCellName = templateWorkbook.CreateCellStyle();
                styleCellName.CloneStyleFrom(sheet.GetRow(8).Cells[0].CellStyle);

                ICellStyle styleCellName2 = templateWorkbook.CreateCellStyle();
                styleCellName2.CloneStyleFrom(sheet.GetRow(8).Cells[1].CellStyle);

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
                rowHeader2.Cells[10].SetCellValue(rowHeader2.Cells[10].StringCellValue + $" {year}");

                var rowHeader3 = ReportUtilities.CreateRow(ref sheet, 2, NUM_CELL);

                ReportUtilities.CreateCell(ref rowHeader3, NUM_CELL);
                rowHeader3.Cells[1].SetCellValue(template.CREATE_BY);
                #endregion

                #region Details

                numRowCur = 8;
                foreach (var item in dataOtherCost)
                {
                    var space = new StringBuilder();
                    for (int i = 0; i < item.LEVEL; i++)
                    {
                        space.Append("    ");
                    }
                    var rowCur = ReportUtilities.CreateRow(ref sheet, numRowCur++, NUM_CELL);
                    rowCur.Cells[0].SetCellValue(item.PROJECT_CODE);
                    rowCur.Cells[1].SetCellValue(item.TYPE);
                    rowCur.Cells[2].SetCellValue(space.ToString() + item.PROJECT_NAME);
                    for (int i = 0; i < NUM_CELL; i++)
                    {
                        rowCur.Cells[i].CellStyle = styleCellName2;
                    }
                }
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
            var dataOtherCost = PreparePureList(out IList<T_MD_TEMPLATE_DETAIL_DAU_TU_TRANG_THIET_BI> detailKhoanMucDauTus, templateId, year, ignoreAuth: true);

            if (dataOtherCost.Count == 0 || detailKhoanMucDauTus.Count == 0)
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
                templateWorkbook.SetSheetName(0, ModulType.GetTextSheetName(ModulType.DauTuTrangThietBi));
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
                foreach (var detail in detailKhoanMucDauTus.GroupBy(x => x.CENTER_CODE)
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
                        rowCur.Cells[j].SetCellValue(detail.Center.ORG_CODE);
                        rowCur.Cells[j].CellStyle = styleCellDetail;
                        j++;

                        rowCur.Cells[j].SetCellValue(detail.Center.Organize.NAME);
                        rowCur.Cells[j].CellStyle = styleCellDetail;
                        j++;

                        rowCur.Cells[j].SetCellValue(detail.Center.PROJECT_CODE);
                        rowCur.Cells[j].CellStyle = styleCellDetail;
                        j++;

                        rowCur.Cells[j].SetCellValue(detail.Center.Project.NAME);
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
                    var revenuePLData = UnitOfWork.Repository<DauTuTrangThietBiSumUpDetailRepo>()
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
                    var revenuePLData = UnitOfWork.Repository<DauTuTrangThietBiSumUpDetailRepo>()
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
                var sumupDetails = UnitOfWork.Repository<DauTuTrangThietBiSumUpDetailRepo>()
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
        /// <param name="detailOtherKhoanMucDauTus">out detail revenue elemts</param>
        /// <param name="year">which year of template</param>
        /// <param name="version">which version of template</param>
        /// <returns>Returns list revenue elemts with their parents and their value</returns>
        public IList<T_MD_KHOAN_MUC_DAU_TU> GetDataCostPreview(
            out IList<T_MD_TEMPLATE_DETAIL_DAU_TU_TRANG_THIET_BI> detailOtherKhoanMucDauTus,
            string templateId,
            string centerCode = "",
            int? year = null,
            int? version = null,
            bool? isHasValue = null)
        {
            var pureLstItems = PreparePureList(out detailOtherKhoanMucDauTus, templateId, year.Value, centerCode);
            var sum = GetSumDescendants(detailOtherKhoanMucDauTus, pureLstItems, parent_id: string.Empty, templateId, year, version).Distinct().ToList();
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

        public IList<T_MD_KHOAN_MUC_DAU_TU> GetDataTemPlate(string templateId, int year)
        {
            var templateDetails = UnitOfWork.Repository<TemplateDetailDauTuTrangThietBiRepo>().Queryable().Where(x => x.TEMPLATE_CODE == templateId && x.TIME_YEAR == year).OrderByDescending(x => x.Center.Project.TYPE).ToList();
            var lstProject = templateDetails.Select(x => x.Center.Project).Distinct().ToList();
            var lstKhoanMuc = new List<T_MD_KHOAN_MUC_DAU_TU>();
            foreach (var project in lstProject)
            {
                var item = new T_MD_KHOAN_MUC_DAU_TU
                {
                    PROJECT_CODE = project.CODE,
                    PROJECT_NAME = project.NAME,
                    TYPE = project.TYPE == "TTB-LON"? string.Empty: project.TYPE,
                    LEVEL = 0
                };
                lstKhoanMuc.Add(item);
                if(project.TYPE == "TTB-LON")
                {
                    var lstgiaiDoan = UnitOfWork.Repository<GiaiDoanDauTuRepo>().Queryable().Where(x => x.TYPE == project.TYPE).ToList();
                    foreach (var gd in lstgiaiDoan)
                    {
                        var itemChild = new T_MD_KHOAN_MUC_DAU_TU
                        {
                            PROJECT_CODE = project.CODE,
                            PROJECT_NAME = gd?.TEXT,
                            TYPE = gd.CODE,
                            LEVEL = 1
                        };
                        lstKhoanMuc.Add(itemChild);
                    }
                }
            }
            return lstKhoanMuc;
        }

        public IList<T_MD_KHOAN_MUC_DAU_TU> PreparePureList(IList<T_MD_TEMPLATE_DETAIL_DAU_TU_TRANG_THIET_BI> detailOtherKhoanMucDauTus, int year)
        {
            // get all revenue elements
            var allOtherKhoanMucDauTus = UnitOfWork.Repository<KhoanMucDauTuRepo>().GetManyByExpression(x => x.TIME_YEAR == year);
            // get revenue elements in details revenue elements
            var revenueElements = from d in detailOtherKhoanMucDauTus
                                  select d.Element;
            // lookup revenue elements by center code
            var lookupElementsCenter = detailOtherKhoanMucDauTus.ToLookup(x => x.CENTER_CODE);

            var pureLstItems = new List<T_MD_KHOAN_MUC_DAU_TU>();
            // loop through all center
            foreach (var ctCode in lookupElementsCenter.Select(l => l.Key))
            {
                // lookup revenue elements
                var lookupElements = lookupElementsCenter[ctCode].ToLookup(x => x.Element.PARENT_CODE);
                foreach (var code in lookupElements.Select(l => l.Key))
                {
                    var level = 0;
                    // temp list
                    var lst = new List<T_MD_KHOAN_MUC_DAU_TU>();
                    // add all leaf to temp list item
                    lst.AddRange(from item in lookupElements[code]
                                 let center = UnitOfWork.Repository<DauTuTrangThietBiProfitCenterRepo>().Queryable().FirstOrDefault(x => x.CODE == ctCode)
                                 select new T_MD_KHOAN_MUC_DAU_TU
                                 {
                                     CENTER_CODE = ctCode,
                                     C_ORDER = item.Element.C_ORDER,
                                     NAME = item.Element.NAME,
                                     PARENT_CODE = item.Element.PARENT_CODE,
                                     CODE = item.Element.CODE,
                                     LEVEL = 0,
                                     TIME_YEAR = item.TIME_YEAR,
                                     DauTuTrangThietBiProfitCenter = center == null ? new T_MD_DAU_TU_TRANG_THIET_BI_PROFIT_CENTER() : center,
                                 });
                    var parentCode = code;
                    while (!string.IsNullOrEmpty(parentCode))
                    {
                        level++;
                        // find parents to add into list
                        var element = allOtherKhoanMucDauTus.FirstOrDefault(x => x.CODE == parentCode);
                        if (element != null)
                        {
                            parentCode = element.PARENT_CODE;
                            element.CENTER_CODE = ctCode;
                            element.LEVEL = level;
                            element.IS_GROUP = true;
                            lst.Add((T_MD_KHOAN_MUC_DAU_TU)element.CloneObject());     // must to clone to other object because it reference to other center
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
        /// <param name="detailOtherKhoanMucDauTus"></param>
        /// <param name="templateId"></param>
        /// <param name="centerCode"></param>
        /// <returns></returns>
        public IList<T_MD_KHOAN_MUC_DAU_TU> PreparePureList(out IList<T_MD_TEMPLATE_DETAIL_DAU_TU_TRANG_THIET_BI> detailOtherKhoanMucDauTus,
            string templateId,
            int year,
            string centerCode = "",
            bool ignoreAuth = false)
        {
            templateId = templateId ?? string.Empty;
            var template = GetTemplate(templateId);
            var code = UnitOfWork.Repository<CostCenterRepo>().Queryable().Where(x => x.CODE == template.ORG_CODE).Select(x => x.PARENT_CODE).FirstOrDefault();
            var currentUserCenterCode = ProfileUtilities.User.ORGANIZE_CODE;
            if (ProfileUtilities.User.ORGANIZE_CODE == "1000")
            {
                currentUserCenterCode = code;
            }
            var childOrgOtherCosts = GetListOfChildrenCenter(currentUserCenterCode).Select(x => x.CODE);

            if (ignoreAuth || /*childOrgOtherCosts.Contains(template.ORG_CODE) ||*/ currentUserCenterCode.Equals(template.ORG_CODE) || template.ORG_CODE == centerCode)
            {
                detailOtherKhoanMucDauTus = UnitOfWork.Repository<TemplateDetailDauTuTrangThietBiRepo>()
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
                    detailOtherKhoanMucDauTus = UnitOfWork.Repository<TemplateDetailDauTuTrangThietBiRepo>()
                        .GetManyByExpression(x => x.TEMPLATE_CODE.Equals(templateId) && lstChildCenterCodes.Contains(x.CENTER_CODE) && x.TIME_YEAR == year);
                }
                else
                {
                    detailOtherKhoanMucDauTus = UnitOfWork.Repository<TemplateDetailDauTuTrangThietBiRepo>()
                        .GetManyByExpression(x => x.TEMPLATE_CODE.Equals(templateId) && x.CENTER_CODE == centerCode && x.TIME_YEAR == year);
                }
            }
            return PreparePureList(detailOtherKhoanMucDauTus, year);
        }

        /// <summary>
        /// Xem theo template
        /// </summary>
        /// <param name="detailOtherKhoanMucDauTus"></param>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public IList<T_MD_KHOAN_MUC_DAU_TU> PreparePureListForTemplate(out IList<T_MD_TEMPLATE_DETAIL_DAU_TU_TRANG_THIET_BI> detailOtherKhoanMucDauTus,
            string templateId,
            int year)
        {

            detailOtherKhoanMucDauTus = UnitOfWork.Repository<TemplateDetailDauTuTrangThietBiRepo>()
                .GetManyByExpression(x => x.TEMPLATE_CODE.Equals(templateId) && x.TIME_YEAR == year);

            return PreparePureList(detailOtherKhoanMucDauTus, year);
        }



        /// <summary>
        /// Xem theo center code
        /// </summary>
        /// <param name="detailOtherKhoanMucDauTus"></param>
        /// <param name="centerCodes"></param>
        /// <returns></returns>
        public IList<T_MD_KHOAN_MUC_DAU_TU> PreparePureList(out IList<T_MD_TEMPLATE_DETAIL_DAU_TU_TRANG_THIET_BI> detailOtherKhoanMucDauTus,
            IList<string> centerCodes,
            int year)
        {
            // Tìm mẫu nộp hộ
            var listTemplateCodes = this.UnitOfWork.Repository<TemplateDetailDauTuTrangThietBiRepo>()
                .GetManyByExpression(x => centerCodes.Contains(x.CENTER_CODE) && x.TIME_YEAR == year)
                .Select(x => x.TEMPLATE_CODE).Distinct().ToList();
            var findDauTuTrangThietBi = this.CurrentRepository.GetManyByExpression(
                    x => listTemplateCodes.Contains(x.TEMPLATE_CODE) && !centerCodes.Contains(x.ORG_CODE));
            var lst = new List<T_MD_TEMPLATE_DETAIL_DAU_TU_TRANG_THIET_BI>();
            detailOtherKhoanMucDauTus = new List<T_MD_TEMPLATE_DETAIL_DAU_TU_TRANG_THIET_BI>();
            foreach (var template in listTemplateCodes)
            {
                lst.AddRange(UnitOfWork.Repository<TemplateDetailDauTuTrangThietBiRepo>()
                    .GetManyByExpression(x => x.TEMPLATE_CODE.Equals(template) && centerCodes.Contains(x.CENTER_CODE) && x.TIME_YEAR == year));
            }

            detailOtherKhoanMucDauTus = lst;

            return PreparePureList(detailOtherKhoanMucDauTus, year);
        }

        /// <summary>
        /// Sum up data revenue center by center code and year (Tổng hợp dữ liệu tại phòng ban)
        /// </summary>
        /// <param name="revenuePL">Output header revenue pl</param>
        /// <param name="centerCode">OtherCost center code want to sum up</param>
        /// <param name="year">Year want to sum up</param>
        public override void SumUpDataCenter(out T_BP_DAU_TU_TRANG_THIET_BI_VERSION revenuePL, string centerCode, int year, string kichBan, string phienBan, string hangHangKhong)
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

            var lstData = new List<T_BP_DAU_TU_TRANG_THIET_BI_DATA>();
            revenuePL = new T_BP_DAU_TU_TRANG_THIET_BI_VERSION();

            try
            {
                UnitOfWork.BeginTransaction();
                var sumUpDetailRepo = UnitOfWork.Repository<DauTuTrangThietBiSumUpDetailRepo>();
                var revenuePLDataRepo = UnitOfWork.Repository<DauTuTrangThietBiDataRepo>();

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
                var lookup = revenuePlDataApproved.ToLookup(x => Tuple.Create(x.DAU_TU_PROFIT_CENTER_CODE, x.KHOAN_MUC_DAU_TU_CODE));
                foreach (var code in lookup.Select(x => x.Key))
                {
                    // TODO: check if all value of months are equal 0
                    if (lookup[code].Count() == 1)
                    {
                        lstData.Add((T_BP_DAU_TU_TRANG_THIET_BI_DATA)lookup[code].First().CloneObject());
                    }
                    else
                    {
                        lstData.Add(new T_BP_DAU_TU_TRANG_THIET_BI_DATA
                        {
                            DAU_TU_PROFIT_CENTER_CODE = code.Item1,
                            VALUE = lookup[code].Sum(x => x.VALUE),
                            KHOAN_MUC_DAU_TU_CODE = lookup[code].First().KHOAN_MUC_DAU_TU_CODE,
                        });
                    }
                }

                // get current version in revenue pl data
                var newestCF = UnitOfWork.Repository<DauTuTrangThietBiRepo>()
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
                    CurrentRepository.Create(new T_BP_DAU_TU_TRANG_THIET_BI
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
                revenuePL = new T_BP_DAU_TU_TRANG_THIET_BI_VERSION
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
                UnitOfWork.Repository<DauTuTrangThietBiVersionRepo>().Create(revenuePL);

                foreach (var item in lstData)
                {
                    item.ORG_CODE = centerCode;
                    item.DAU_TU_PROFIT_CENTER_CODE = item.DAU_TU_PROFIT_CENTER_CODE;
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
                _ = UnitOfWork.GetSession().CreateSQLQuery($"DELETE FROM T_BP_DAU_TU_TRANG_THIET_BI_DATA WHERE ORG_CODE = '{centerCode}' AND TIME_YEAR = {year}")
                    .ExecuteUpdate();

                // insert to pl data history
                UnitOfWork.Repository<DauTuTrangThietBiDataHistoryRepo>().Create((from pl in lstOtherCostPlDataOldVersion
                                                                                select (T_BP_DAU_TU_TRANG_THIET_BI_DATA_HISTORY)pl).ToList());

                // insert to pl history
                UnitOfWork.Repository<DauTuTrangThietBiHistoryRepo>().Create(new T_BP_DAU_TU_TRANG_THIET_BI_HISTORY
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
                var lstDauTuTrangThietBi = revenuePlDataApproved.ToLookup(x => new { OrgCode = x.ORG_CODE, TemplateCode = x.TEMPLATE_CODE, TemplateVersion = x.VERSION });

                // create list sum up detail
                sumUpDetailRepo.Create((from c in lstDauTuTrangThietBi.Select(x => x.Key)
                                        select new T_BP_DAU_TU_TRANG_THIET_BI_SUM_UP_DETAIL
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
                        ModulType = ModulType.DauTuTrangThietBi,
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
        /// <param name="plDataOtherKhoanMucDauTus">List of data revenue element output</param>
        /// <param name="year">Year want to summary</param>
        /// <param name="centerCode">OtherCost center code want to summary</param>
        /// <param name="version">Version want to summary</param>
        /// <returns>Returns list of revenue element with their value</returns>
        public IList<T_MD_KHOAN_MUC_DAU_TU> SummarySumUpCenter(
            out IList<T_BP_DAU_TU_TRANG_THIET_BI_DATA> plDataOtherKhoanMucDauTus,
            int year,
            string centerCode,
            int? version,
            bool? isHasValue = null,
            string templateId = "")
        {
            // get newest revenue pl data by center code
            plDataOtherKhoanMucDauTus = UnitOfWork.Repository<DauTuTrangThietBiDataRepo>()
                .GetCFDataByCenterCode(null, new List<string> { centerCode }, year, templateId, version);
            plDataOtherKhoanMucDauTus = plDataOtherKhoanMucDauTus.Where(x => x.STATUS == Approve_Status.DaPheDuyet).ToList();
            return SummaryCenter(plDataOtherKhoanMucDauTus, centerCode, year, isHasValue);
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
        private IEnumerable<T_MD_KHOAN_MUC_DAU_TU> GetSumDescendants(
            IEnumerable<T_MD_TEMPLATE_DETAIL_DAU_TU_TRANG_THIET_BI> details,
            IEnumerable<T_MD_KHOAN_MUC_DAU_TU> pureItems,
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
                Queue<T_MD_KHOAN_MUC_DAU_TU> st = new Queue<T_MD_KHOAN_MUC_DAU_TU>(lookup[parent_id]);

                while (st.Count > 0)
                {
                    // get first item in queue
                    var item = st.Dequeue();
                    // variable to check should return item or not
                    bool shouldReturn = true;
                    // lst to store children of item which have children
                    var lstHasChild = new List<T_MD_KHOAN_MUC_DAU_TU>();
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
                                    detail.PLData = revenuePlData.FirstOrDefault(x => x.KHOAN_MUC_DAU_TU_CODE == detail.ELEMENT_CODE && x.DAU_TU_PROFIT_CENTER_CODE == detail.CENTER_CODE);
                                }
                                var treeData = detail?.PLData;
                                if (treeData != null)
                                {
                                    item.Values[0] += treeData.VALUE ?? 0;
                                    item.HasAssignValue = true;

                                    i.Values[0] = treeData.VALUE ?? 0;
                                    i.DESCRIPTION = treeData.DESCRIPTION;
                                    i.PROCESS = treeData.PROCESS;
                                    i.EQUITY_SOURCES = treeData.EQUITY_SOURCES;
                                    i.TDTK = treeData.TDTK;
                                    i.QKH = treeData.QKH;
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
                            var data = revenuePlData.Where(x => x.KHOAN_MUC_DAU_TU_CODE == item.CODE && x.DAU_TU_PROFIT_CENTER_CODE == item.CENTER_CODE);
                            if (data != null)
                            {
                                item.Values = new decimal[1]
                                {
                                data.Sum(x => x.VALUE) ?? 0,
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
        private IList<T_BP_DAU_TU_TRANG_THIET_BI_DATA> GetVersionData(
            string templateCode,
            string centerCode,
            int? year = null,
            int? version = null)
        {
            string orgCode = ProfileUtilities.User.ORGANIZE_CODE;
            var template = GetTemplate(templateCode);
            var code = UnitOfWork.Repository<CostCenterRepo>().Queryable().Where(x => x.CODE == template.ORG_CODE).Select(x => x.PARENT_CODE).FirstOrDefault();
            if (ProfileUtilities.User.ORGANIZE_CODE == "1000")
            {
                orgCode = code;
            }
            var lstChildren = GetListOfChildrenCenter(orgCode).Select(x => x.CODE);
            // check if orgCode is org code of template or not
            if (template.ORG_CODE.Equals(orgCode))
            {
                return UnitOfWork.Repository<DauTuTrangThietBiDataRepo>()
                    .GetCFDataByOrgCode(orgCode, year.Value, templateCode, version);
            }
            else if (lstChildren.Contains(template.ORG_CODE))
            {
                return UnitOfWork.Repository<DauTuTrangThietBiDataRepo>()
                    .GetCFDataByOrgCode(template.ORG_CODE, year.Value, templateCode, version);
            }
            else
            {
                return UnitOfWork.Repository<DauTuTrangThietBiDataRepo>()
                    .GetCFDataByCenterCode(template.ORG_CODE, new List<string> { centerCode }, year.Value, templateCode, version);
            }
        }

        /// <summary>
        /// Summary data of a center with newest data
        /// </summary>
        /// <param name="plDataOtherKhoanMucDauTus">List revenue pl data want to out</param>
        /// <param name="centerCode">Center code want to summary data</param>
        /// <returns>Returns list revenue elements with their data</returns>
        public IList<T_MD_KHOAN_MUC_DAU_TU> SummaryCenterOut(out IList<T_BP_DAU_TU_TRANG_THIET_BI_DATA> plDataOtherKhoanMucDauTus,
                                                              string centerCode,
                                                              int year,
                                                              int? version,
                                                              bool? isHasValue = null)
        {
            if (!version.HasValue)
            {
                // get newest revenue pl data by center code
                plDataOtherKhoanMucDauTus = UnitOfWork.Repository<DauTuTrangThietBiDataRepo>()
                    .GetManyByExpression(x => x.ORG_CODE.Equals(centerCode) && x.TIME_YEAR == year);
            }
            else
            {
                ObjDetail.ORG_CODE = centerCode;
                if (IsLeaf())
                {
                    // get all data have approved
                    plDataOtherKhoanMucDauTus = UnitOfWork.Repository<DauTuTrangThietBiDataRepo>()
                        .GetManyByExpression(x => x.ORG_CODE == centerCode && !string.IsNullOrEmpty(x.TEMPLATE_CODE));
                }
                else
                {
                    // get list all children centers in revenue center tree
                    var lstCostCenters = UnitOfWork.Repository<CostCenterRepo>().GetManyByExpression(x => x.PARENT_CODE == centerCode);

                    // get all data have approved
                    plDataOtherKhoanMucDauTus = UnitOfWork.Repository<DauTuTrangThietBiDataRepo>()
                        .GetManyByExpression(x => x.STATUS == Approve_Status.DaPheDuyet && (x.ORG_CODE == centerCode && !string.IsNullOrEmpty(x.TEMPLATE_CODE) ||
                    x.TIME_YEAR == year &&
                    lstCostCenters.Any(c => c.CODE.Equals(x.ORG_CODE))));
                }
            }

            return SummaryCenter(plDataOtherKhoanMucDauTus, centerCode, year, isHasValue);
        }

        /// <summary>
        /// Get data has summed up (history)
        /// Lấy dữ liệu đã được tổng hợp lên đơn vị cha theo version
        /// </summary>
        /// <param name="plDataOtherKhoanMucDauTus">List out data</param>
        /// <param name="centerCode">Org code của đơn vị được tổng hợp</param>
        /// <param name="year">Năm dữ liệu muốn xem</param>
        /// <param name="version">Version của dữ liệu muốn xem. Null thì sẽ lấy mới nhất</param>
        /// <returns></returns>
        public IList<T_MD_KHOAN_MUC_DAU_TU> SummaryCenterVersion(out IList<T_BP_DAU_TU_TRANG_THIET_BI_DATA> plDataOtherKhoanMucDauTus,
                                                              string centerCode,
                                                              int year,
                                                              int? version,
                                                              bool isDrillDown = false)
        {
            if (isDrillDown)
            {
                plDataOtherKhoanMucDauTus = GetAllSumUpDetails(centerCode, year, version.Value);
            }
            else
            {
                plDataOtherKhoanMucDauTus = UnitOfWork.Repository<DauTuTrangThietBiDataRepo>()
                    .GetCFDataByOrgCode(centerCode, year, string.Empty, version);
            }
            return SummaryCenter(plDataOtherKhoanMucDauTus, centerCode, year);
        }

        /// <summary>
        /// Lấy danh sách tất cả các data đã được tổng hợp lên cho centerCode theo version và năm
        /// </summary>
        /// <param name="centerCode"></param>
        /// <param name="year"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        private IList<T_BP_DAU_TU_TRANG_THIET_BI_DATA> GetAllSumUpDetails(string centerCode, int year, int version)
        {
            var detailsSumUp = UnitOfWork.Repository<DauTuTrangThietBiSumUpDetailRepo>()
               .GetManyWithFetch(x => x.ORG_CODE == centerCode && x.TIME_YEAR == year && x.SUM_UP_VERSION == version);

            var lstChildren = GetListOfChildrenCenter(centerCode).Select(x => x.CODE);
            var lstDetails = new List<T_BP_DAU_TU_TRANG_THIET_BI_DATA>();
            var lstResult = new List<T_BP_DAU_TU_TRANG_THIET_BI_DATA>();
            var plDataRepo = UnitOfWork.Repository<DauTuTrangThietBiDataRepo>();
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

            var lookupElement = lstDetails.ToLookup(x => x.KHOAN_MUC_DAU_TU_CODE);
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
                            lstResult.Add(new T_BP_DAU_TU_TRANG_THIET_BI_DATA
                            {
                                ORG_CODE = orgCode,
                                KHOAN_MUC_DAU_TU_CODE = key,
                                KhoanMucDauTu = lookupCenter[orgCode].First().KhoanMucDauTu,
                                DauTuTrangThietBiProfitCenter = lookupCenter[orgCode].First().DauTuTrangThietBiProfitCenter,
                                Organize = lookupCenter[orgCode].First().Organize,
                                VALUE = dataOrgCode.Sum(x => x.VALUE) ?? 0,
                            });
                        }
                    }

                }
            }
            return lstResult;
        }

        private IList<T_MD_KHOAN_MUC_DAU_TU> SummaryCenter(IList<T_BP_DAU_TU_TRANG_THIET_BI_DATA> plDataOtherKhoanMucDauTus, string centerCode, int year,
                                                          bool? isHasValue = null)
        {

            // get all revenue elements
            var allOtherKhoanMucDauTu = UnitOfWork.Repository<KhoanMucDauTuRepo>().GetManyByExpression(x => x.TIME_YEAR == year);
            // get all child
            var childrenCodes = GetListOfChildrenCenter(centerCode).Select(x => x.CODE);
            // list store pure items to send to view
            var pureLstItems = new List<T_MD_KHOAN_MUC_DAU_TU>();

            // lookup revenue elements by parent code
            var lookupElements = plDataOtherKhoanMucDauTus.GroupBy(x => x.KHOAN_MUC_DAU_TU_CODE)
                .Select(x => x.First())
                .ToLookup(x => x.KhoanMucDauTu.PARENT_CODE);

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
                var lst = new List<T_MD_KHOAN_MUC_DAU_TU>();
                // add all leaf to temp list item with child = true
                lst.AddRange(from item in lookupElements[code]
                             select new T_MD_KHOAN_MUC_DAU_TU
                             {
                                 CENTER_CODE = item.ORG_CODE,
                                 C_ORDER = item.KhoanMucDauTu.C_ORDER,
                                 NAME = item.KhoanMucDauTu.NAME,
                                 PARENT_CODE = item.KhoanMucDauTu.PARENT_CODE,
                                 CODE = item.KhoanMucDauTu.CODE,
                                 LEVEL = 0,
                                 IS_GROUP = false,
                                 ORG_NAME = childrenCode.Contains(item.ORG_CODE) ? item.Organize.NAME : item.DauTuTrangThietBiProfitCenter.NAME,
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
                    var element = allOtherKhoanMucDauTu.FirstOrDefault(x => x.CODE == parentCode);
                    if (element != null)
                    {
                        // if find parent in all revenue element
                        parentCode = element.PARENT_CODE;
                        element.CENTER_CODE = centerCode;
                        element.LEVEL = level;
                        element.IS_GROUP = true;
                        element.TEMPLATE_CODE = lookupElements[code].FirstOrDefault().TEMPLATE_CODE;
                        element.ORG_CODE = lookupElements[code].FirstOrDefault().ORG_CODE;
                        lst.Add((T_MD_KHOAN_MUC_DAU_TU)element.CloneObject());     // must to clone to other object because it reference to other center
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
                                  select new T_MD_KHOAN_MUC_DAU_TU
                                  {
                                      CENTER_CODE = item.ORG_CODE,
                                      C_ORDER = item.KhoanMucDauTu.C_ORDER,
                                      NAME = item.KhoanMucDauTu.NAME,
                                      PARENT_CODE = string.Empty,
                                      CODE = item.KhoanMucDauTu.CODE,
                                      LEVEL = 0,
                                      IS_GROUP = false,
                                      ORG_NAME = childrenCode.Contains(item.ORG_CODE) ? item.Organize.NAME : item.DauTuTrangThietBiProfitCenter.NAME,
                                      TEMPLATE_CODE = item.TEMPLATE_CODE,
                                      ORG_CODE = item.ORG_CODE,
                                      IsChildren = true
                                  });

            // calculate data for all pure list item
            var sum = GetSumDescendants(plDataOtherKhoanMucDauTus, pureLstItems, parentId: string.Empty).Distinct().ToList();
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
        private IEnumerable<T_MD_KHOAN_MUC_DAU_TU> GetSumDescendants(
            IList<T_BP_DAU_TU_TRANG_THIET_BI_DATA> costCFData,
            IList<T_MD_KHOAN_MUC_DAU_TU> pureItems,
            string parentId)
        {
            var lstResult = new List<T_MD_KHOAN_MUC_DAU_TU>
            {
                // set tổng năm
                // tạo element tổng
                new T_MD_KHOAN_MUC_DAU_TU
                {
                    NAME = "TỔNG CỘNG",
                    LEVEL = 0,
                    PARENT_CODE = null,
                    IS_GROUP = true,
                    IsChildren = false,
                    C_ORDER = 0,
                    CODE = string.Empty,
                    Values = new decimal[1]
                    {
                        costCFData.Sum(x => x.VALUE) ?? 0,
                    }
                }
            };
            var lookup = pureItems.ToLookup(i => i.PARENT_CODE);
            Queue<T_MD_KHOAN_MUC_DAU_TU> st = new Queue<T_MD_KHOAN_MUC_DAU_TU>(lookup[parentId]);
            while (st.Count > 0)
            {
                // get first item in queue
                var item = st.Dequeue();
                // variable to check should return item or not
                bool shouldReturn = true;
                // lst to store children of item which have children
                var lstOtherKhoanMucDauTus = new List<T_MD_KHOAN_MUC_DAU_TU>();
                // loop through items which have parent id = item id
                foreach (var i in lookup[item.CODE])
                {
                    if (lookup[i.CODE].Count() > 0)
                    {
                        shouldReturn = false;
                        lstOtherKhoanMucDauTus.Add(i);
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

                            var treeData = costCFData.Where(x => x.KHOAN_MUC_DAU_TU_CODE.Equals(i.CODE));
                            if (treeData != null && treeData.Count() > 0)
                            {
                                item.Values[0] += treeData.Sum(x => x.VALUE) ?? 0;
                                item.HasAssignValue = true;

                                foreach (var d in treeData)
                                {
                                    var values = new decimal[14];
                                    values[0] = treeData.Sum(x => x.VALUE) ?? 0;
                                    
                                    i.Values = values;
                                    var clone = (T_MD_KHOAN_MUC_DAU_TU)i.Clone();
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
                    var treeData = costCFData.Where(x => x.KHOAN_MUC_DAU_TU_CODE.Equals(item.CODE));
                    if (treeData != null && treeData.Count() > 0)
                    {
                        item.Values[0] += treeData.Sum(x => x.VALUE) ?? 0;
                        
                    }
                    var clone = (T_MD_KHOAN_MUC_DAU_TU)item.Clone();
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
                        var data = costCFData.Where(x => x.KHOAN_MUC_DAU_TU_CODE == item.CODE && x.ORG_CODE == item.CENTER_CODE);
                        if (data != null)
                        {
                            item.Values = new decimal[1]
                            {
                                data.Sum(x => x.VALUE) ?? 0,
                                
                            };
                        }
                    }
                    lstResult.Add(item);
                }
                else
                {
                    // add children of item which have chilren to lookup 
                    if (lstOtherKhoanMucDauTus.Count > 0)
                    {
                        lookup = lookup
                            .SelectMany(l => l)
                            .Concat(lstOtherKhoanMucDauTus)
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
            var module = "DauTuTrangThietBi";
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            IWorkbook workbook;
            workbook = new XSSFWorkbook(fs);
            workbook.SetSheetName(0, ModulType.GetTextSheetName(ModulType.DauTuTrangThietBi));
            fs.Close();


            ISheet sheetMonth = workbook.GetSheetAt(0);
            var metaDataMonth = ExcelHelper.GetExcelMeta(htmlMonth);
            var NUM_CELL_MONTH = metaDataMonth.MetaTBody[0].Count;

            InitHeaderFile(ref sheetMonth, year, centerCode, version, NUM_CELL_MONTH, templateId, "Triệu đồng", exchangeRate);
            ExcelHelperBP.InsertHeaderTable(ref workbook, ref sheetMonth, metaDataMonth.MetaTHead, NUM_CELL_MONTH,module, ignoreFirstColumn: string.IsNullOrEmpty(templateId) || (!string.IsNullOrEmpty(templateId) && GetTemplate(templateId).IS_BASE));
            ExcelHelperBP.InsertBodyTableByYear(ref workbook,
                ref sheetMonth,
                metaDataMonth.MetaTBody,
                NUM_CELL_MONTH,
                module,
                ignoreFirstColumn: string.IsNullOrEmpty(templateId) || (!string.IsNullOrEmpty(templateId) && GetTemplate(templateId).IS_BASE));


            // Save the Excel spreadsheet to a MemoryStream and return it to the client
            workbook.Write(outFileStream);
        }

        private void InitHeaderFile(ref ISheet sheet, int year, string centerCode, int? version, int NUM_CELL, string templateId, string unit, decimal exchangeRate)
        {
            var name = "DỮ LIỆU KẾ HOẠCH ĐẦU TƯ TRANG THIẾT BỊ";
            var centerName = GetCenter(centerCode).NAME.ToUpper();
            var template = GetTemplate(templateId);
            var templateName = template != null ? $"Mẫu khai báo: {template.CODE} - {template.NAME}" : "Tổng hợp dữ liệu";

            ExcelHelperBP.InitHeaderFile(ref sheet, year, centerName, version, NUM_CELL, templateName, "Triệu đồng", name, exchangeRate);
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

            var lstDetails = UnitOfWork.Repository<DauTuTrangThietBiSumUpDetailRepo>()
            .GetManyByExpression(x => x.ORG_CODE == parentCenterCode &&
                x.TIME_YEAR == year &&
                x.SUM_UP_VERSION == version);
            var templateVersion = lstDetails.FirstOrDefault(y => y.TEMPLATE_CODE == templateCode)?.DATA_VERSION;
            if (templateVersion.HasValue)
            {
                // get file upload base
                return UnitOfWork.Repository<DauTuTrangThietBiVersionRepo>()
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

        public T_BP_DAU_TU_TRANG_THIET_BI CheckTemplate(string template, int year, string orgCode) {
            try
            {
                var checkTemplate = UnitOfWork.Repository<DauTuTrangThietBiRepo>().Queryable().FirstOrDefault(x => x.TEMPLATE_CODE == template && x.TIME_YEAR == year && x.ORG_CODE == orgCode);
                if (checkTemplate != null)
                {
                    return checkTemplate;
                }
                else
                {
                    return new T_BP_DAU_TU_TRANG_THIET_BI();
                }
            }
            catch (Exception ex)
            {
                this.State = false;
                this.Exception = ex;
                return null;
            }
            
        }

        public IList<T_BP_DAU_TU_TRANG_THIET_BI_COMMENT> GetCommentElement(string templateCode, int version, int year, string elementCode)
        {
            try
            {
                return UnitOfWork.Repository<DauTuTrangThietBiCommentRepo>().Queryable().Where(x => x.TEMPLATE_CODE == templateCode && x.VERSION == version && x.YEAR == year && x.ELEMENT_CODE == elementCode).ToList();
            }
            catch (Exception ex)
            {
                this.State = false;
                this.Exception = ex;
                return new List<T_BP_DAU_TU_TRANG_THIET_BI_COMMENT>();
            }
        }

        public void InsertComment(string templateCode, int version, int year, string type, string sanBay, string costCenter, string elementCode, string value)
        {
            try
            {
                UnitOfWork.BeginTransaction();
                UnitOfWork.Repository<DauTuTrangThietBiCommentRepo>().Create(new T_BP_DAU_TU_TRANG_THIET_BI_COMMENT
                {
                    ID = Guid.NewGuid(),
                    TEMPLATE_CODE = templateCode,
                    VERSION = version,
                    YEAR = year,
                    ELEMENT_CODE = elementCode,
                    COMMENT = value,
                    CREATE_BY = ProfileUtilities.User.USER_NAME,
                    CREATE_DATE = DateTime.Now,
                });

                UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                this.State = false;
                this.Exception = ex;
            }
        }

        public void UpdateCellValue(string templateCode, int version, int year, string projectCode, string elementCode, string valueInput, string column, int? month)
        {
            try
            {
                UnitOfWork.BeginTransaction();
                var value = valueInput.Replace(".", "");
                var rowsChange = new List<T_BP_DAU_TU_TRANG_THIET_BI_DATA>();
                if (month == null || month == 0)
                {
                    rowsChange = UnitOfWork.Repository<DauTuTrangThietBiDataRepo>().Queryable().Where(x => x.TEMPLATE_CODE == templateCode && x.VERSION == version && x.TIME_YEAR == year && x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == projectCode && x.KHOAN_MUC_DAU_TU_CODE == elementCode).ToList();
                }
                else
                {
                    rowsChange = UnitOfWork.Repository<DauTuTrangThietBiDataRepo>().Queryable().Where(x => x.TEMPLATE_CODE == templateCode && x.VERSION == version && x.TIME_YEAR == year && x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == projectCode && x.KHOAN_MUC_DAU_TU_CODE == elementCode && x.MONTH == month).ToList();
                }
                if (rowsChange.Count() == 0)
                {
                    this.State = false;
                    this.ErrorMessage = "Có lỗi hệ thống xảy ra! Vui lòng liên hệ với quản trị viên!";
                    return;
                }
                var oldValue = rowsChange.FirstOrDefault().VALUE;
                foreach (var row in rowsChange)
                {
                    switch (column)
                    {
                        case "VALUE_1":
                            row.VALUE_1 = string.IsNullOrEmpty(value) ? 0 : Convert.ToDecimal(value);
                            break;
                        case "VALUE_2":
                            row.VALUE_2 = value.ToString();
                            break;
                        case "VALUE_3":
                            row.VALUE_3 = value;
                            break;
                        case "VALUE_4":
                            row.VALUE_4 = value;
                            break;
                        case "VALUE_5":
                            row.VALUE_5 = string.IsNullOrEmpty(value) ? 0 : Convert.ToDecimal(value);
                            row.VALUE_7 = row.VALUE_5 * row.VALUE_6;
                            break;
                        case "VALUE_6":
                            row.VALUE_6 = string.IsNullOrEmpty(value) ? 0 : Convert.ToDecimal(value);
                            row.VALUE_7 = row.VALUE_5 * row.VALUE_6;
                            break;
                        case "VALUE_8":
                            row.VALUE_8 = value;
                            break;
                        case "VALUE_9":
                            row.VALUE_9 = string.IsNullOrEmpty(value) ? 0 : Convert.ToDecimal(value);
                            break;
                        case "VALUE_10":
                            row.VALUE_10 = string.IsNullOrEmpty(value) ? 0 : Convert.ToDecimal(value);
                            break;
                        case "DESCRIPTION":
                            row.DESCRIPTION = value;
                            break;
                    }
                    UnitOfWork.Repository<DauTuTrangThietBiDataRepo>().Update(row);
                }
                var typeName = UnitOfWork.Repository<KhoanMucDauTuRepo>().Queryable().FirstOrDefault(x => x.CODE == elementCode).NAME;
                // Lưu lịch sử
                UnitOfWork.Repository<DauTuTrangThietBiEditHistoryRepo>().Create(new T_BP_DAU_TU_TRANG_THIET_BI_EDIT_HISTORY
                {
                    ID = Guid.NewGuid(),
                    TEMPLATE_CODE = templateCode,
                    VERSION = version,
                    YEAR = year,
                    TYPE = "Cập nhật " + typeName,
                    PROJECT_CODE = projectCode,
                    ELEMENT_CODE = elementCode,
                    OLD_VALUE = oldValue,
                    CREATE_BY = ProfileUtilities.User.USER_NAME,
                    CREATE_DATE = DateTime.Now,
                });
                UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                this.State = false;
                this.Exception = ex;
            }
        }

        public List<T_BP_DAU_TU_TRANG_THIET_BI_DATA> GetProject(string TemplateCode, int? version, int year)
        {
            var lstProject = UnitOfWork.Repository<DauTuTrangThietBiDataRepo>().Queryable().Where(x => x.TEMPLATE_CODE == TemplateCode && x.VERSION == version && x.TIME_YEAR == year).ToList();
            return lstProject;
        }

        public void GetDataProject(ViewDataCenterModel model)
        {
            var lstdata = UnitOfWork.Repository<DauTuTrangThietBiDataRepo>().Queryable().Where(x => x.TEMPLATE_CODE == model.TEMPLATE_CODE && x.VERSION == model.VERSION && x.TIME_YEAR == model.YEAR).ToList();
            var lstKhoanMuc = new List<T_MD_KHOAN_MUC_DAU_TU>();
            foreach (var item in lstdata)
            {
                var value = item.GetType().GetProperty($"MONTH{model.MONTH}").GetValue(item);
                item.VALUE = value != null ? Convert.ToDecimal(value) : 0;
                lstKhoanMuc.Add(item.KhoanMucDauTu);
            }
        }
        public void GenerateData(ref MemoryStream outFileStream, string path, ViewDataCenterModel model, List<T_BP_DAU_TU_TRANG_THIET_BI_DATA> lstData, List<T_MD_DAU_TU_TRANG_THIET_BI_PROFIT_CENTER> lstProject)
        {
            //Mở file Template
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            IWorkbook templateWorkbook;
            templateWorkbook = new XSSFWorkbook(fs);
            templateWorkbook.SetSheetName(0, ModulType.GetTextSheetName(ModulType.KeHoachChiPhi));
            fs.Close();
            ISheet sheet = templateWorkbook.GetSheetAt(0);

            //Số hàng và số cột hiện tại
            int startRow = 8;
            int NUM_CELL = 13;

            //Style cần dùng
            ICellStyle styleCellBold = templateWorkbook.CreateCellStyle();
            styleCellBold.WrapText = true;
            var fontBold = templateWorkbook.CreateFont();
            fontBold.Boldweight = (short)FontBoldWeight.Bold;
            fontBold.FontHeightInPoints = 11;
            fontBold.FontName = "Times New Roman";

            ICellStyle styleCellNumber = templateWorkbook.CreateCellStyle();
            styleCellNumber.CloneStyleFrom(sheet.GetRow(8).Cells[0].CellStyle);
            styleCellNumber.WrapText = true;

            foreach (var detail in lstProject.Where(x => x.CODE != null).Select(x => x.CODE).Distinct())
            {
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, NUM_CELL);
                var item = lstData.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == detail).ToList();
                rowCur.Cells[0].SetCellValue(item.FirstOrDefault()?.DauTuTrangThietBiProfitCenter.Project?.CODE);
                rowCur.Cells[1].SetCellValue(item.FirstOrDefault()?.DauTuTrangThietBiProfitCenter.Project?.NAME);
                rowCur.Cells[2].SetCellValue(item.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.VALUE)?.ToStringVN());
                rowCur.Cells[3].SetCellValue(item.FirstOrDefault()?.EQUITY_SOURCES);
                rowCur.Cells[4].SetCellValue(item.FirstOrDefault()?.PROCESS);
                rowCur.Cells[5].SetCellValue(item.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4010").Sum(x => x.VALUE)?.ToStringVN());
                rowCur.Cells[6].SetCellValue(item.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4030").Sum(x => x.VALUE)?.ToStringVN());
                rowCur.Cells[7].SetCellValue(item.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4031").Sum(x => x.VALUE)?.ToStringVN());
                rowCur.Cells[8].SetCellValue(item.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4032").Sum(x => x.VALUE)?.ToStringVN());
                rowCur.Cells[9].SetCellValue(item.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4012").Sum(x => x.VALUE)?.ToStringVN());
                rowCur.Cells[10].SetCellValue(item.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4020").Sum(x => x.VALUE)?.ToStringVN());
                rowCur.Cells[11].SetCellValue(item.FirstOrDefault()?.QKH);
                rowCur.Cells[12].SetCellValue(item.FirstOrDefault()?.DESCRIPTION);
                for (int i = 0; i < NUM_CELL; i++)
                {
                    rowCur.Cells[i].CellStyle = styleCellNumber;
                }
            }

            templateWorkbook.Write(outFileStream);
        }
    }
}
