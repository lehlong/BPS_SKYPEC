using iTextSharp.text;
using Microsoft.AspNet.SignalR;

using NHibernate.Criterion;
using NHibernate.Linq;
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
using SMO.Core.Entities.BP.KE_HOACH_SAN_LUONG;
using SMO.Repository.Implement.BP.KE_HOACH_SAN_LUONG;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using static NHibernate.Engine.Query.CallableParser;
using static SMO.SelectListUtilities;
using static iTextSharp.text.pdf.AcroFields;

namespace SMO.Service.BP.KE_HOACH_CHI_PHI
{
    public class KeHoachChiPhiService : BaseBPService<T_BP_KE_HOACH_CHI_PHI, KeHoachChiPhiRepo, T_MD_KHOAN_MUC_HANG_HOA, T_BP_KE_HOACH_CHI_PHI_VERSION, T_BP_KE_HOACH_CHI_PHI_HISTORY, KeHoachChiPhiHistoryRepo>, IKeHoachChiPhiService
    {
        private readonly List<Point> InvalidCellsList;
        private readonly List<string> ListColumnName;
        private readonly List<string> ListColumnNameDataBase;
        private int StartRowData;
        private readonly List<string> ListElement = new List<string> { "6273", "B6277G002B3", "B6277G003AB3", "B6277G004AB3", "B6277EA2", "B6277G006AB3", "B6277G007AB3" };
       
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
                        KICH_BAN = ObjDetail.KICH_BAN,
                        PHIEN_BAN = ObjDetail.PHIEN_BAN,
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
                    KICH_BAN = ObjDetail.KICH_BAN,
                    PHIEN_BAN = ObjDetail.PHIEN_BAN,
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
                        KICH_BAN = ObjDetail.KICH_BAN,
                        PHIEN_BAN = ObjDetail.PHIEN_BAN,
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

                UnitOfWork.Repository<KeHoachChiPhiHistoryRepo>().Create(new T_BP_KE_HOACH_CHI_PHI_HISTORY()
                {
                    PKID = Guid.NewGuid().ToString(),
                    ORG_CODE = ObjDetail.ORG_CODE,
                    TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                    VERSION = ObjDetail.VERSION,
                    KICH_BAN = ObjDetail.KICH_BAN,
                    PHIEN_BAN = ObjDetail.PHIEN_BAN,
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
                    KICH_BAN = ObjDetail.KICH_BAN,
                    PHIEN_BAN = ObjDetail.PHIEN_BAN,
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

                UnitOfWork.Repository<KeHoachChiPhiHistoryRepo>().Create(new T_BP_KE_HOACH_CHI_PHI_HISTORY()
                {
                    PKID = Guid.NewGuid().ToString(),
                    ORG_CODE = ObjDetail.ORG_CODE,
                    TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                    VERSION = ObjDetail.VERSION,
                    KICH_BAN = ObjDetail.KICH_BAN,
                    PHIEN_BAN = ObjDetail.PHIEN_BAN,
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
                    KICH_BAN = ObjDetail.KICH_BAN,
                    PHIEN_BAN = ObjDetail.PHIEN_BAN,
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
                        KICH_BAN = ObjDetail.KICH_BAN,
                        PHIEN_BAN = ObjDetail.PHIEN_BAN,
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
                        KICH_BAN = ObjDetail.KICH_BAN,
                        PHIEN_BAN = ObjDetail.PHIEN_BAN,
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
                        KICH_BAN = ObjDetail.KICH_BAN,
                        PHIEN_BAN = ObjDetail.PHIEN_BAN,
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
            if (orgCode == "1000")
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
                if (string.IsNullOrEmpty(ObjDetail.STATUS)){
                    this.ObjList = this.ObjList.Where(x => x.KICH_BAN == this.ObjDetail.KICH_BAN && x.PHIEN_BAN == this.ObjDetail.PHIEN_BAN).OrderBy(x => x.ORG_CODE).ThenBy(x => x.TEMPLATE_CODE).ToList();
                }
                else if (this.ObjDetail.STATUS == "05")
                {
                    this.ObjList = this.ObjList.Where(x => x.KICH_BAN == this.ObjDetail.KICH_BAN && x.PHIEN_BAN == this.ObjDetail.PHIEN_BAN&& this.ObjDetail.IS_DELETED==true).OrderBy(x => x.ORG_CODE).ThenBy(x => x.TEMPLATE_CODE).ToList();
                }
                else
                {
                    this.ObjList = this.ObjList.Where(x => x.KICH_BAN == this.ObjDetail.KICH_BAN && x.PHIEN_BAN == this.ObjDetail.PHIEN_BAN && this.ObjDetail.STATUS == x.STATUS).OrderBy(x => x.ORG_CODE).ThenBy(x => x.TEMPLATE_CODE).ToList();
                }
               
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
                if (string.IsNullOrEmpty(ObjDetail.STATUS))
                {
                    this.ObjList = this.ObjList.Where(x => x.KICH_BAN == this.ObjDetail.KICH_BAN && x.PHIEN_BAN == this.ObjDetail.PHIEN_BAN).OrderBy(x => x.ORG_CODE).ThenBy(x => x.TEMPLATE_CODE).ToList();
                }
                else if (this.ObjDetail.STATUS == "05")
                {
                    this.ObjList = this.ObjList.Where(x => x.KICH_BAN == this.ObjDetail.KICH_BAN && x.PHIEN_BAN == this.ObjDetail.PHIEN_BAN && this.ObjDetail.IS_DELETED == true).OrderBy(x => x.ORG_CODE).ThenBy(x => x.TEMPLATE_CODE).ToList();
                }
                else
                {
                    this.ObjList = this.ObjList.Where(x => x.KICH_BAN == this.ObjDetail.KICH_BAN && x.PHIEN_BAN == this.ObjDetail.PHIEN_BAN && this.ObjDetail.STATUS == x.STATUS).OrderBy(x => x.ORG_CODE).ThenBy(x => x.TEMPLATE_CODE).ToList();
                }
            }

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
                        Values = new decimal[6]
                            {
                                lookupData[key].Sum(x => x.QUANTITY) ?? 0,
                                lookupData[key].Sum(x => x.PRICE) ?? 0,
                                lookupData[key].Sum(x => x.AMOUNT) ?? 0,
                                lookupData[key].Sum(x => x.QUANTITY_TD) ?? 0,
                                lookupData[key].Sum(x => x.PRICE_TD) ?? 0,
                                lookupData[key].Sum(x => x.AMOUNT_TD) ?? 0,
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
        #region Test tối ưu
        private readonly object lockObject = new object();
        public async Task<IList<T_MD_KHOAN_MUC_HANG_HOA>> GetDataChiPhi(ViewDataCenterModel model, List<string> lstSanBay, int? month)
        {
            try
            {
                var template = UnitOfWork.Repository<TemplateRepo>().Get(model.TEMPLATE_CODE);
                List<T_MD_KHOAN_MUC_HANG_HOA> data = new List<T_MD_KHOAN_MUC_HANG_HOA>();
                var elements = UnitOfWork.Repository<KhoanMucHangHoaRepo>().GetAll();
                var ListParentcode = new List<T_MD_KHOAN_MUC_HANG_HOA>();
                var ListPhanbo = new List<T_MD_KHOAN_MUC_HANG_HOA>();

                if (template.DetailKeHoachChiPhi.Any(x => x.Center?.COST_CENTER_CODE == "100001"))
                {
                    elements = elements.Where(x => x.TIME_YEAR == model.YEAR && x.CODE.StartsWith("CQ62")).OrderBy(x => x.C_ORDER).ToList();
                    
                }
                else if (template.DetailKeHoachChiPhi.Any(x => x.Center?.COST_CENTER_CODE == "100002"))
                {
                    elements = elements.Where(x => x.TIME_YEAR == model.YEAR && x.CODE.StartsWith("B62")).OrderBy(x => x.C_ORDER).ToList();
                  
                }
                else if (template.DetailKeHoachChiPhi.Any(x => x.Center?.COST_CENTER_CODE == "100003"))
                {
                    elements = elements.Where(x => x.TIME_YEAR == model.YEAR && x.CODE.StartsWith("T62")).OrderBy(x => x.C_ORDER).ToList();
                   
                }
                else if (template.DetailKeHoachChiPhi.Any(x => x.Center?.COST_CENTER_CODE == "100004"))
                {
                    elements = elements.Where(x => x.TIME_YEAR == model.YEAR && x.CODE.StartsWith("N62")).OrderBy(x => x.C_ORDER).ToList();
                   
                }
                else if (template.DetailKeHoachChiPhi.Any(x => x.Center?.COST_CENTER_CODE == "100005"))
                {
                    elements = elements.Where(x => x.TIME_YEAR == model.YEAR && x.CODE.StartsWith("VT62")).OrderBy(x => x.C_ORDER).ToList();
                 
                }
                foreach (var el in elements)
                {
                    el.Children = elements.Where(x => x.PARENT_CODE == el.CODE).ToList();
                }
                var detail = new List<T_BP_KE_HOACH_CHI_PHI_DATA>();
                if(month != null && month != 0) {
                    detail = UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.ORG_CODE == model.ORG_CODE && x.VERSION == model.VERSION && x.TEMPLATE_CODE == model.TEMPLATE_CODE && x.TIME_YEAR == model.YEAR && x.MONTH == month).ToList();
                }
                else
                {
                    detail = UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.ORG_CODE == model.ORG_CODE && x.VERSION == model.VERSION && x.TEMPLATE_CODE == model.TEMPLATE_CODE && x.TIME_YEAR == model.YEAR).ToList();
                }
                var order = 0;

                var departmentAssign = UnitOfWork.Repository<KeHoachChiPhiDepartmentAssignRepo>().Queryable().Any(x => x.DEPARTMENT_CODE == ProfileUtilities.User.ORGANIZE_CODE && x.TEMPLATE_CODE == model.TEMPLATE_CODE && x.YEAR == model.YEAR);

                if (departmentAssign)
                {
                    var lstElementAssign = UnitOfWork.Repository<KeHoachChiPhiDepartmentAssignRepo>().Queryable().Where(x => x.DEPARTMENT_CODE == ProfileUtilities.User.ORGANIZE_CODE && x.TEMPLATE_CODE == model.TEMPLATE_CODE && x.YEAR == model.YEAR).ToList();
                    var lstElement = new List<T_MD_KHOAN_MUC_HANG_HOA>();
                    foreach (var elementAssign in lstElementAssign)
                    {
                        foreach (var element in elements.OrderByDescending(x => x.C_ORDER))
                        {
                            if (element.CODE == elementAssign.ELEMENT_CODE)
                            {
                                lstElement.Add(element);
                            }
                        }
                    }
                    elements = lstElement;
                }

                var allExpertise = UnitOfWork.Repository<KeHoachChiPhiDepartmentExpertiseRepo>().Queryable().Where(x => x.TEMPLATE_CODE == model.TEMPLATE_CODE && x.VERSION == model.VERSION && x.YEAR == model.YEAR).ToList();

                var lstItem = elements.OrderByDescending(x => x.C_ORDER).ToList();
                var ParentCode = elements.Where(x => x.PARENT_CODE == "VT6273" || x.PARENT_CODE == "B6273" || x.PARENT_CODE == "CQ6273" || x.PARENT_CODE == "N6273" || x.PARENT_CODE == "T6273").Select(x=>x.CODE);
                var CodePhanbo = elements.Where(x => ParentCode.Contains(x.PARENT_CODE)).Where(x=>x.CODE.EndsWith("B"));

                var lstEdited = UnitOfWork.Repository<KeHoachChiPhiEditHistoryRepo>().Queryable().Where(x => x.TEMPLATE_CODE == model.TEMPLATE_CODE && x.VERSION == model.VERSION && x.YEAR == model.YEAR).ToList();
                var lstCommented = UnitOfWork.Repository<KeHoachChiPhiCommentRepo>().Queryable().Where(x => x.TEMPLATE_CODE == model.TEMPLATE_CODE && x.VERSION == model.VERSION && x.YEAR == model.YEAR).ToList();
                var lstParentCode = UnitOfWork.Repository<KhoanMucHangHoaRepo>().Queryable().Select(x => x.PARENT_CODE).ToList();
                var lstCenter = UnitOfWork.Repository<ChiPhiProfitCenterRepo>().GetAll().ToList();
                var flag = lstItem.Count() / 4;
                var lstTask1 = lstItem.Skip(0).Take(flag).ToList();
                var lstTask2 = lstItem.Skip(flag).Take(flag).ToList();
                var lstTask3 = lstItem.Skip(flag*2).Take(flag).ToList();
                var lstTask4 = lstItem.Skip(flag*3).ToList();
                List<T_MD_KHOAN_MUC_HANG_HOA> data1 = new List<T_MD_KHOAN_MUC_HANG_HOA>();
                List<T_MD_KHOAN_MUC_HANG_HOA> data2 = new List<T_MD_KHOAN_MUC_HANG_HOA>();
                List<T_MD_KHOAN_MUC_HANG_HOA> data3 = new List<T_MD_KHOAN_MUC_HANG_HOA>();
                List<T_MD_KHOAN_MUC_HANG_HOA> data4 = new List<T_MD_KHOAN_MUC_HANG_HOA>();

             
                Task task1 = Task.Run(() =>
                {
                    
                   foreach (var element in lstTask1)
                   {

                       var expertise = allExpertise.Any(x => x.ELEMENT_CODE == element.CODE);
                       var isEdited = lstEdited.Any(x => x.ELEMENT_CODE == element.CODE);
                       var isCommented = lstCommented.Any(x => x.ELEMENT_CODE == element.CODE);
                       var item = new T_MD_KHOAN_MUC_HANG_HOA
                       {
                           PARENT_CODE = element.PARENT_CODE,
                           CODE = element.CODE,
                           NAME = element.NAME,
                           C_ORDER = order,
                           IS_GROUP = lstParentCode.Contains(element.CODE),
                           IsChecked = expertise,
                           IsHighLight = isEdited || isCommented ? true : false,
                       };
                   
                      
                       foreach (var sb in lstSanBay)
                       {
                           var query = lstParentCode.Contains(element.CODE) ? detail.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(element.CODE) && x.ChiPhiProfitCenter.SAN_BAY_CODE == sb).ToList() :
                                    detail.Where(x => x.KHOAN_MUC_HANG_HOA_CODE == element.CODE && x.ChiPhiProfitCenter.SAN_BAY_CODE == sb).ToList();
                           var value = lstParentCode.Contains(element.CODE) ? new decimal[3]
                           {
                       query.Sum(x => x.QUANTITY) ?? 0,
                       query.Sum(x => x.PRICE) ?? 0,
                       query.Sum(x => x.AMOUNT) ?? 0,
                           } : new decimal[3]{
                       query.Sum(x => x.QUANTITY) ?? 0,
                       query.Sum(x => x.PRICE) ?? 0,
                       query.Sum(x => x.AMOUNT) ?? 0,
                           };
                           var center = lstCenter.FirstOrDefault(x=>x.SAN_BAY_CODE == sb);
                           item.valueSb.Add(value);
                           item.lstCenter.Add(center);
                       }
                       data1.Add(item);
                       order++;
                   }
                    
                });
                Task task2 = Task.Run(() =>
                {

                    foreach (var element in lstTask2)
                    {

                        var expertise = allExpertise.Any(x => x.ELEMENT_CODE == element.CODE);
                        var isEdited = lstEdited.Any(x => x.ELEMENT_CODE == element.CODE);
                        var isCommented = lstCommented.Any(x => x.ELEMENT_CODE == element.CODE);
                        var item = new T_MD_KHOAN_MUC_HANG_HOA
                        {
                            PARENT_CODE = element.PARENT_CODE,
                            CODE = element.CODE,
                            NAME = element.NAME,
                            C_ORDER = order,
                            IS_GROUP = lstParentCode.Contains(element.CODE),
                            IsChecked = expertise,
                            IsHighLight = isEdited || isCommented ? true : false,
                        };
                        foreach (var sb in lstSanBay)
                        {
                            var query = lstParentCode.Contains(element.CODE) ? detail.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(element.CODE) && x.ChiPhiProfitCenter.SAN_BAY_CODE == sb).ToList() :
                                     detail.Where(x => x.KHOAN_MUC_HANG_HOA_CODE == element.CODE && x.ChiPhiProfitCenter.SAN_BAY_CODE == sb).ToList();
                            var value = lstParentCode.Contains(element.CODE) ? new decimal[3]
                            {
                            query.Sum(x => x.QUANTITY) ?? 0,
                            query.Sum(x => x.PRICE) ?? 0,
                            query.Sum(x => x.AMOUNT) ?? 0,
                            } : new decimal[3]{
                            query.Sum(x => x.QUANTITY) ?? 0,
                            query.Sum(x => x.PRICE) ?? 0,
                            query.Sum(x => x.AMOUNT) ?? 0,
                            };
                            var center = lstCenter.FirstOrDefault(x => x.SAN_BAY_CODE == sb);
                            item.valueSb.Add(value);
                            item.lstCenter.Add(center);
                        }
                        data2.Add(item);
                        order++;
                    }

                });

                Task task3 = Task.Run(() =>
                {

                    foreach (var element in lstTask3)
                    {

                        var expertise = allExpertise.Any(x => x.ELEMENT_CODE == element.CODE);
                        var isEdited = lstEdited.Any(x => x.ELEMENT_CODE == element.CODE);
                        var isCommented = lstCommented.Any(x => x.ELEMENT_CODE == element.CODE);
                        var item = new T_MD_KHOAN_MUC_HANG_HOA
                        {
                            PARENT_CODE = element.PARENT_CODE,
                            CODE = element.CODE,
                            NAME = element.NAME,
                            C_ORDER = order,
                            IS_GROUP = lstParentCode.Contains(element.CODE),
                            IsChecked = expertise,
                            IsHighLight = isEdited || isCommented ? true : false,
                        };
                        foreach (var sb in lstSanBay)
                        {
                            var query = lstParentCode.Contains(element.CODE) ? detail.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(element.CODE) && x.ChiPhiProfitCenter.SAN_BAY_CODE == sb).ToList() :
                                     detail.Where(x => x.KHOAN_MUC_HANG_HOA_CODE == element.CODE && x.ChiPhiProfitCenter.SAN_BAY_CODE == sb).ToList();
                            var value = lstParentCode.Contains(element.CODE) ? new decimal[3]
                            {
                            query.Sum(x => x.QUANTITY) ?? 0,
                            query.Sum(x => x.PRICE) ?? 0,
                            query.Sum(x => x.AMOUNT) ?? 0,
                            } : new decimal[3]{
                            query.Sum(x => x.QUANTITY) ?? 0,
                            query.Sum(x => x.PRICE) ?? 0,
                            query.Sum(x => x.AMOUNT) ?? 0,
                            };
                            var center = lstCenter.FirstOrDefault(x => x.SAN_BAY_CODE == sb);
                            item.valueSb.Add(value);
                            item.lstCenter.Add(center);
                        }
                        data3.Add(item);
                        order++;
                    }

                });

                Task task4 = Task.Run(() =>
                {

                    foreach (var element in lstTask4)
                    {

                        var expertise = allExpertise.Any(x => x.ELEMENT_CODE == element.CODE);
                        var isEdited = lstEdited.Any(x => x.ELEMENT_CODE == element.CODE);
                        var isCommented = lstCommented.Any(x => x.ELEMENT_CODE == element.CODE);
                        var item = new T_MD_KHOAN_MUC_HANG_HOA
                        {
                            PARENT_CODE = element.PARENT_CODE,
                            CODE = element.CODE,
                            NAME = element.NAME,
                            C_ORDER = order,
                            IS_GROUP = lstParentCode.Contains(element.CODE),
                            IsChecked = expertise,
                            IsHighLight = isEdited || isCommented ? true : false,
                        };
                        foreach (var sb in lstSanBay)
                        {
                            
                            var query = lstParentCode.Contains(element.CODE) ? detail.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(element.CODE) && x.ChiPhiProfitCenter.SAN_BAY_CODE == sb).ToList() :
                            detail.Where(x => x.KHOAN_MUC_HANG_HOA_CODE == element.CODE && x.ChiPhiProfitCenter.SAN_BAY_CODE == sb).ToList();
                             var value = lstParentCode.Contains(element.CODE) ? new decimal[3]
                                {
                            query.Sum(x => x.QUANTITY) ?? 0,
                            query.Sum(x => x.PRICE) ?? 0,
                            query.Sum(x => x.AMOUNT) ?? 0,
                                } : new decimal[3]{
                            query.Sum(x => x.QUANTITY) ?? 0,
                            query.Sum(x => x.PRICE) ?? 0,
                            query.Sum(x => x.AMOUNT) ?? 0,
                                };
                                var center = lstCenter.FirstOrDefault(x => x.SAN_BAY_CODE == sb);
                                item.valueSb.Add(value);
                                item.lstCenter.Add(center);
                            
                           
                        }
                        data4.Add(item);
                        order++;
                    }

                });


                await Task.WhenAll(task1, task2,task3,task4);
                data.AddRange(data1);
                data.AddRange(data2);
                data.AddRange(data3);
                data.AddRange(data4);

                return data;
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                this.Exception = ex;
                return new List<T_MD_KHOAN_MUC_HANG_HOA>();
            }
        }
       
        #endregion
        public IList<T_MD_KHOAN_MUC_HANG_HOA> GetData(ViewDataCenterModel model, List<string> lstSanBay)
        {
            try
            {
                var template = UnitOfWork.Repository<TemplateRepo>().Get(model.TEMPLATE_CODE);
                IList<T_MD_KHOAN_MUC_HANG_HOA> data = new List<T_MD_KHOAN_MUC_HANG_HOA>();

                var elements = UnitOfWork.Repository<KhoanMucHangHoaRepo>().GetAll();

                if (template.DetailKeHoachChiPhi.Any(x => x.Center.COST_CENTER_CODE == "100001"))
                {
                    elements = elements.Where(x => x.TIME_YEAR == model.YEAR && x.CODE.StartsWith("CQ62")).OrderBy(x => x.C_ORDER).ToList();
                }
                else if (template.DetailKeHoachChiPhi.Any(x => x.Center.COST_CENTER_CODE == "100002"))
                {
                    elements = elements.Where(x => x.TIME_YEAR == model.YEAR && x.CODE.StartsWith("B62")).OrderBy(x => x.C_ORDER).ToList();
                }
                else if (template.DetailKeHoachChiPhi.Any(x => x.Center.COST_CENTER_CODE == "100003"))
                {
                    elements = elements.Where(x => x.TIME_YEAR == model.YEAR && x.CODE.StartsWith("T62")).OrderBy(x => x.C_ORDER).ToList();
                }
                else if (template.DetailKeHoachChiPhi.Any(x => x.Center.COST_CENTER_CODE == "100004"))
                {
                    elements = elements.Where(x => x.TIME_YEAR == model.YEAR && x.CODE.StartsWith("N62")).OrderBy(x => x.C_ORDER).ToList();
                }
                else if (template.DetailKeHoachChiPhi.Any(x => x.Center.COST_CENTER_CODE == "100005"))
                {
                    elements = elements.Where(x => x.TIME_YEAR == model.YEAR && x.CODE.StartsWith("VT62")).OrderBy(x => x.C_ORDER).ToList();
                }
                foreach (var el in elements)
                {
                    el.Children = elements.Where(x => x.PARENT_CODE == el.CODE).ToList();
                }

                var detail = UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.ORG_CODE == model.ORG_CODE && x.VERSION == model.VERSION && x.TEMPLATE_CODE == model.TEMPLATE_CODE && x.TIME_YEAR == model.YEAR).ToList();
                var order = 0;

                var departmentAssign = UnitOfWork.Repository<KeHoachChiPhiDepartmentAssignRepo>().Queryable().Any(x => x.DEPARTMENT_CODE == ProfileUtilities.User.ORGANIZE_CODE && x.TEMPLATE_CODE == model.TEMPLATE_CODE && x.YEAR == model.YEAR);

                if (departmentAssign)
                {
                    var lstElementAssign = UnitOfWork.Repository<KeHoachChiPhiDepartmentAssignRepo>().Queryable().Where(x => x.DEPARTMENT_CODE == ProfileUtilities.User.ORGANIZE_CODE && x.TEMPLATE_CODE == model.TEMPLATE_CODE && x.YEAR == model.YEAR).ToList();
                    var lstElement = new List<T_MD_KHOAN_MUC_HANG_HOA>();
                    foreach (var elementAssign in lstElementAssign)
                    {
                        foreach (var element in elements.OrderByDescending(x => x.C_ORDER))
                        {
                            if (element.CODE == elementAssign.ELEMENT_CODE)
                            {
                                lstElement.Add(element);
                            }
                        }
                    }
                    elements = lstElement;
                }

                var allExpertise = UnitOfWork.Repository<KeHoachChiPhiDepartmentExpertiseRepo>().Queryable().Where(x => x.TEMPLATE_CODE == model.TEMPLATE_CODE && x.VERSION == model.VERSION && x.YEAR == model.YEAR).ToList();

                var lstItem = elements.OrderByDescending(x => x.C_ORDER).ToList();

                var lstEdited = UnitOfWork.Repository<KeHoachChiPhiEditHistoryRepo>().Queryable().Where(x => x.TEMPLATE_CODE == model.TEMPLATE_CODE && x.VERSION == model.VERSION && x.YEAR == model.YEAR).ToList();
                var lstCommented = UnitOfWork.Repository<KeHoachChiPhiCommentRepo>().Queryable().Where(x => x.TEMPLATE_CODE == model.TEMPLATE_CODE && x.VERSION == model.VERSION && x.YEAR == model.YEAR).ToList();
                var lstParentCode = UnitOfWork.Repository<KhoanMucHangHoaRepo>().Queryable().Select(x => x.PARENT_CODE).ToList();
                /*foreach (var element in lstItem)
                {

                    var expertise = allExpertise.Any(x => x.ELEMENT_CODE == element.CODE);
                    var isEdited = lstEdited.Any(x => x.ELEMENT_CODE == element.CODE);
                    var isCommented = lstCommented.Any(x => x.ELEMENT_CODE == element.CODE);
                    var item = new T_MD_KHOAN_MUC_HANG_HOA
                    {
                        CODE = element.CODE,
                        NAME = element.NAME,
                        C_ORDER = order,
                        IS_GROUP = element.IS_GROUP,
                        IsChecked = expertise,
                        IsHighLight = isEdited || isCommented ? true : false,
                    };
                    foreach (var sb in lstSanBay)
                    {
                        var query = lstParentCode.Contains(element.CODE) ? detail.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(element.CODE) && x.ChiPhiProfitCenter.SAN_BAY_CODE == sb).ToList() :
                                 detail.Where(x => x.KHOAN_MUC_HANG_HOA_CODE == element.CODE && x.ChiPhiProfitCenter.SAN_BAY_CODE == sb).ToList();
                        var value = lstParentCode.Contains(element.CODE) ? new decimal[3]
                        {
                            0,
                            0,
                            query.Sum(x => x.AMOUNT) ?? 0,
                        } : new decimal[3]{
                            query.Sum(x => x.QUANTITY) ?? 0,
                            query.Sum(x => x.PRICE) ?? 0,
                            query.Sum(x => x.AMOUNT) ?? 0,
                        };
                        var center = UnitOfWork.Repository<ChiPhiProfitCenterRepo>().Queryable().FirstOrDefault(x => x.SAN_BAY_CODE == sb);
                        item.valueSb.Add(value);
                        item.lstCenter.Add(center);
                    }
                    data.Add(item);
                    order++;
                }*/
                foreach (var sb in lstSanBay)
                {
                    foreach (var element in lstItem)
                    {
                        if (lstParentCode.Contains(element.CODE))
                        {
                            var query = detail.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(element.CODE) && x.ChiPhiProfitCenter.SAN_BAY_CODE == sb).ToList();
                            var expertise = allExpertise.Any(x => x.ELEMENT_CODE == element.CODE);
                            var isEdited = lstEdited.Any(x => x.ELEMENT_CODE == element.CODE);
                            var isCommented = lstCommented.Any(x => x.ELEMENT_CODE == element.CODE);

                            var item = new T_MD_KHOAN_MUC_HANG_HOA
                            {
                                CODE = element.CODE,
                                NAME = element.NAME,
                                Values = new decimal[3]
                                {
                                     0,
                                     0,
                                     query.Sum(x => x.AMOUNT) ?? 0,
                                },
                                C_ORDER = order,
                                IS_GROUP = element.IS_GROUP,
                                Center = UnitOfWork.Repository<ChiPhiProfitCenterRepo>().Queryable().FirstOrDefault(x => x.SAN_BAY_CODE == sb),
                                IsChecked = expertise,
                                IsHighLight = isEdited || isCommented ? true : false,
                            };
                            data.Add(item);
                            order++;
                        }
                        else
                        {
                            var query = detail.Where(x => x.KHOAN_MUC_HANG_HOA_CODE == element.CODE && x.ChiPhiProfitCenter.SAN_BAY_CODE == sb);
                            var expertise = allExpertise.Any(x => x.ELEMENT_CODE == element.CODE);
                            var isEdited = lstEdited.Any(x => x.ELEMENT_CODE == element.CODE);
                            var isCommented = lstCommented.Any(x => x.ELEMENT_CODE == element.CODE);
                            var item = new T_MD_KHOAN_MUC_HANG_HOA
                            {
                                CODE = element.CODE,
                                NAME = element.NAME,
                                Values = new decimal[3]
                                {
                                      query.Sum(x => x.QUANTITY) ?? 0,
                                      query.Sum(x => x.PRICE) ?? 0,
                                      query.Sum(x => x.AMOUNT) ?? 0,
                                },
                                C_ORDER = order,
                                IS_GROUP = element.IS_GROUP,
                                Center = UnitOfWork.Repository<ChiPhiProfitCenterRepo>().Queryable().FirstOrDefault(x => x.SAN_BAY_CODE == sb),
                                IsChecked = expertise,
                                IsHighLight = isEdited || isCommented ? true : false,
                            };
                            data.Add(item);
                            order++;
                        }
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                this.Exception = ex;
                return new List<T_MD_KHOAN_MUC_HANG_HOA>();
            }
        }

        public IList<string> GetSanBayInTemplate(ViewDataCenterModel model)
        {
            try
            {
                var detail = UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.ORG_CODE == model.ORG_CODE && x.VERSION == model.VERSION && x.TEMPLATE_CODE == model.TEMPLATE_CODE && x.TIME_YEAR == model.YEAR).ToList();
                var lstSanBay = detail.GroupBy(x => x.ChiPhiProfitCenter.SAN_BAY_CODE).Select(x => x.First()).Select(x => x.ChiPhiProfitCenter.SAN_BAY_CODE).ToList();
                return lstSanBay;
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                this.Exception = ex;
                return new List<string>();
            }
        }


        public IList<T_MD_KHOAN_MUC_HANG_HOA> GetDataCost(out IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_CHI_PHI> detailOtherKhoanMucHangHoas,
            out IList<T_BP_KE_HOACH_CHI_PHI_DATA> detailOtherCostData,
            out bool isDrillDownApply,
            ViewDataCenterModel model)
        {
            var detail = UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.ORG_CODE == model.ORG_CODE && x.VERSION == model.VERSION && x.TEMPLATE_CODE == model.TEMPLATE_CODE && x.TIME_YEAR == model.YEAR).ToList();
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
                //var elements = GetDataCostPreview(out detailOtherKhoanMucHangHoas, model.TEMPLATE_CODE, lstOrgs, model.YEAR, model.VERSION, isHasValue);

                var templateDetails = new List<T_MD_TEMPLATE_DETAIL_KE_HOACH_CHI_PHI>();

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
                    Values = new decimal[6]
                };
                detailOtherKhoanMucHangHoas = null;
                //var isTemplateBase = GetTemplate(model.TEMPLATE_CODE)?.IS_BASE;
                //isTemplateBase = isTemplateBase.HasValue && isTemplateBase.Value;
                //foreach (var item in elements.Distinct().Where(x => !x.IS_GROUP))
                //{
                //    //if (isTemplateBase.Value && item.Values.Sum() > 0)
                //    //{
                //    //    item.IsChildren = true;
                //    //}
                //    for (int i = 0; i < sumElements.Values.Length; i++)
                //    {
                //        sumElements.Values[i] += item.Values[i];
                //    }
                //}
                //elements.Add(sumElements);
                return (IList<T_MD_KHOAN_MUC_HANG_HOA>)templateDetails;
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
                    var elementCodeInExcel = dataTable.Rows[i][2].ToString().Trim();
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
        #region import cũ
        /// <summary>
        /// Nhập dữ liệu từ excel vào database
        /// </summary>
        /// <param name="request"></param>
        //public override void ImportExcel(HttpRequestBase request)
        //{
        //    base.ImportExcel(request);
        //    if (!State)
        //    {
        //        return;
        //    }
        //    var orgCode = ProfileUtilities.User.ORGANIZE_CODE;
        //    // Lưu file vào database
        //    var fileStream = new FILE_STREAM()
        //    {
        //        PKID = Guid.NewGuid().ToString(),
        //        FILESTREAM = request.Files[0]
        //    };
        //    FileStreamService.InsertFile(fileStream);

        //    var template = UnitOfWork.Repository<TemplateRepo>().Get(ObjDetail.TEMPLATE_CODE);

        //    // Xác định version dữ liệu
        //    var KeHoachChiPhiCurrent = CurrentRepository.Queryable().FirstOrDefault(x => x.ORG_CODE == orgCode
        //        && x.TIME_YEAR == ObjDetail.TIME_YEAR && x.TEMPLATE_CODE == ObjDetail.TEMPLATE_CODE);

        //    if (KeHoachChiPhiCurrent != null && !(KeHoachChiPhiCurrent.STATUS == Approve_Status.TuChoi || KeHoachChiPhiCurrent.STATUS == Approve_Status.ChuaTrinhDuyet))
        //    {
        //        this.State = false;
        //        this.ErrorMessage = "Mẫu khai báo này không ở trạng thái Từ chối hoặc Chưa trình duyệt!";
        //        return;
        //    }

        //    var versionNext = 1;
        //    if (KeHoachChiPhiCurrent != null)
        //    {
        //        versionNext = KeHoachChiPhiCurrent.VERSION + 1;
        //    }

        //    // Lấy dữ liệu của version hiện tại
        //    var dataCurrent = new List<T_BP_KE_HOACH_CHI_PHI_DATA>();
        //    if (KeHoachChiPhiCurrent != null)
        //    {
        //        dataCurrent = UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.ORG_CODE == orgCode
        //            && x.TIME_YEAR == ObjDetail.TIME_YEAR && x.TEMPLATE_CODE == ObjDetail.TEMPLATE_CODE).ToList();
        //    }

        //    //Insert dữ liệu
        //    try
        //    {
        //        var currentUser = ProfileUtilities.User?.USER_NAME;
        //        DataTable tableData = new DataTable();
        //        tableData = ExcelDataExchange.ReadData(fileStream.FULL_PATH);

        //        this.ValidateData(tableData, isDataBase: false);
        //        if (!this.State)
        //        {
        //            return;
        //        }

        //        int actualRows = tableData.Rows.Count;
        //        UnitOfWork.BeginTransaction();

        //        // Cập nhật version
        //        if (KeHoachChiPhiCurrent != null)
        //        {
        //            // Cập nhật next version vào bảng chính
        //            KeHoachChiPhiCurrent.VERSION = versionNext;
        //            KeHoachChiPhiCurrent.IS_DELETED = false;
        //            CurrentRepository.Update(KeHoachChiPhiCurrent);
        //        }
        //        else
        //        {
        //            // Tạo mới bản ghi revenue pl
        //            CurrentRepository.Create(new T_BP_KE_HOACH_CHI_PHI()
        //            {
        //                PKID = Guid.NewGuid().ToString(),
        //                ORG_CODE = orgCode,
        //                TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                KICH_BAN = ObjDetail.KICH_BAN,
        //                PHIEN_BAN = ObjDetail.PHIEN_BAN,
        //                TIME_YEAR = ObjDetail.TIME_YEAR,
        //                VERSION = versionNext,
        //                STATUS = Approve_Status.ChuaTrinhDuyet,
        //                FILE_ID = fileStream.PKID,
        //                IS_DELETED = false,
        //                IS_SUMUP = false,
        //                CREATE_BY = currentUser
        //            });



        //        }

        //        DataTable tableKHCP = new DataTable();
        //        tableKHCP.TableName = "T_BP_KE_HOACH_CHI_PHI_DATA";
        //        tableKHCP.Columns.Add("PKID", typeof(Guid));
        //        tableKHCP.Columns.Add("ORG_CODE", typeof(string));
        //        tableKHCP.Columns.Add("CHI_PHI_PROFIT_CENTER_CODE", typeof(string));
        //        tableKHCP.Columns.Add("TEMPLATE_CODE", typeof(string));
        //        tableKHCP.Columns.Add("TIME_YEAR", typeof(string));
        //        tableKHCP.Columns.Add("MONTH", typeof(string));
        //        tableKHCP.Columns.Add("STATUS", typeof(string));
        //        tableKHCP.Columns.Add("VERSION", typeof(string));
        //        tableKHCP.Columns.Add("KHOAN_MUC_HANG_HOA_CODE", typeof(string));
        //        tableKHCP.Columns.Add("QUANTITY", typeof(string));
        //        tableKHCP.Columns.Add("PRICE", typeof(string));
        //        tableKHCP.Columns.Add("DESCRIPTION", typeof(string));
        //        tableKHCP.Columns.Add("CREATE_BY", typeof(string));
        //        // Đưa next version vào bảng log
        //        UnitOfWork.Repository<KeHoachChiPhiVersionRepo>().Create(new T_BP_KE_HOACH_CHI_PHI_VERSION()
        //        {
        //            PKID = Guid.NewGuid().ToString(),
        //            ORG_CODE = orgCode,
        //            TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //            VERSION = versionNext,
        //            KICH_BAN = KeHoachChiPhiCurrent == null ? ObjDetail.KICH_BAN : KeHoachChiPhiCurrent.KICH_BAN,
        //            PHIEN_BAN = KeHoachChiPhiCurrent == null ? ObjDetail.PHIEN_BAN : KeHoachChiPhiCurrent.PHIEN_BAN,
        //            TIME_YEAR = ObjDetail.TIME_YEAR,
        //            FILE_ID = fileStream.PKID,
        //            CREATE_BY = currentUser
        //        });

        //        // Tạo mới bản ghi log trạng thái
        //        UnitOfWork.Repository<KeHoachChiPhiHistoryRepo>().Create(new T_BP_KE_HOACH_CHI_PHI_HISTORY()
        //        {
        //            PKID = Guid.NewGuid().ToString(),
        //            ORG_CODE = orgCode,
        //            TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //            KICH_BAN = KeHoachChiPhiCurrent == null ? ObjDetail.KICH_BAN : KeHoachChiPhiCurrent.KICH_BAN,
        //            PHIEN_BAN = KeHoachChiPhiCurrent == null ? ObjDetail.PHIEN_BAN : KeHoachChiPhiCurrent.PHIEN_BAN,
        //            VERSION = versionNext,
        //            TIME_YEAR = ObjDetail.TIME_YEAR,
        //            ACTION = Approve_Action.NhapDuLieu,
        //            ACTION_DATE = DateTime.Now,
        //            ACTION_USER = currentUser,
        //            CREATE_BY = currentUser
        //        });


        //        // Insert data vào history
        //        foreach (var item in dataCurrent)
        //        {
        //            var revenueDataHis = (T_BP_KE_HOACH_CHI_PHI_DATA_HISTORY)item;
        //            UnitOfWork.Repository<KeHoachChiPhiDataHistoryRepo>().Create(revenueDataHis);
        //            UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Delete(item);
        //        }

        //        var allChiPhiProfitCenters = UnitOfWork.Repository<ChiPhiProfitCenterRepo>().GetAll();
        //        // Insert dữ liệu vào bảng data
        //        for (int i = 8; i < actualRows; i++)
        //        {
        //            var percentagePreventive = GetPreventive(orgCode, year: ObjDetail.TIME_YEAR)?.PERCENTAGE;
        //            if (percentagePreventive == null)
        //            {
        //                percentagePreventive = 1;
        //            }
        //            else
        //            {
        //                percentagePreventive = 1 + percentagePreventive / 100;
        //            }

        //            var elementCode = tableData.Rows[i][0].ToString().Trim();

        //            //Check mã khoản mục có phải mã cha không? Nếu có thì bỏ qua.
        //            if (template.DetailKeHoachChiPhi.FirstOrDefault(x => x.ELEMENT_CODE == elementCode) == null)
        //            {
        //                continue;
        //            }



        //            // Rẽ nhánh vào phiên bản bổ sung
        //            if(ObjDetail.PHIEN_BAN == "PB4")
        //            {
        //                for(int month = 1; month <= 12; month++)
        //                {
        //                    if (template.DetailKeHoachChiPhi.Any(x => x.Center.COST_CENTER_CODE == "100001"))
        //                    {

        //                        var str1 = tableData.Rows[i][2].ToString();
        //                        var str3 = tableData.Rows[i][4].ToString();
        //                        var str4 = tableData.Rows[i][5].ToString();
        //                        var str5 = tableData.Rows[i][6].ToString();
        //                        var str6 = tableData.Rows[i][3].ToString();

        //                        var str2 = tableData.Rows[i][8].ToString();
        //                        if (!decimal.TryParse(str1, out decimal value) && !string.IsNullOrEmpty(str1))
        //                        {
        //                            this.State = false;
        //                            this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                            return;
        //                        }
        //                        if (!decimal.TryParse(str3, out decimal value1) && !string.IsNullOrEmpty(str3))
        //                        {
        //                            this.State = false;
        //                            this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                            return;
        //                        }
        //                        if (!decimal.TryParse(str4, out decimal value2) && !string.IsNullOrEmpty(str4))
        //                        {
        //                            this.State = false;
        //                            this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                            return;
        //                        }
        //                        if (!decimal.TryParse(str5, out decimal value3) && !string.IsNullOrEmpty(str5))
        //                        {
        //                            this.State = false;
        //                            this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                            return;
        //                        }
        //                        if (!decimal.TryParse(str6, out decimal value4) && !string.IsNullOrEmpty(str6))
        //                        {
        //                            this.State = false;
        //                            this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                            return;
        //                        }
        //                        if (!decimal.TryParse(str2, out decimal value5) && !string.IsNullOrEmpty(str2))
        //                        {
        //                            this.State = false;
        //                            this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                            return;
        //                        }
        //                        var centerCodeVPCT = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VPCT" && x.COST_CENTER_CODE == "100001");
        //                        var centerCodeMB = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "MB" && x.COST_CENTER_CODE == "100001");
        //                        var centerCodeMT = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "MT" && x.COST_CENTER_CODE == "100001");
        //                        var centerCodeCR = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "CR" && x.COST_CENTER_CODE == "100001");
        //                        var centerCodeMN = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "MN" && x.COST_CENTER_CODE == "100001");

        //                        if (centerCodeVPCT == null || centerCodeMB == null || centerCodeMT == null || centerCodeCR == null || centerCodeMN == null)
        //                        {
        //                            throw new Exception($"Định dạng file không đúng hoặc có lỗi hệ thống xảy ra! Vui lòng liên hệ với quản trị viên!");
        //                        }
        //                        var row = tableKHCP.NewRow();

        //                        var costDataVPCT = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                        costDataVPCT = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                        {
        //                            PKID = Guid.NewGuid().ToString(),
        //                            ORG_CODE = orgCode,
        //                            CHI_PHI_PROFIT_CENTER_CODE = centerCodeVPCT.CODE,
        //                            TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                            TIME_YEAR = ObjDetail.TIME_YEAR,
        //                            MONTH = month,
        //                            STATUS = Approve_Status.ChuaTrinhDuyet,
        //                            VERSION = versionNext,
        //                            KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                            QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][2].ToString()) ? "0" : tableData.Rows[i][2].ToString()),
        //                            PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString()),
        //                            DESCRIPTION = tableData.Rows[i][15].ToString(),
        //                            CREATE_BY = currentUser
        //                        };
        //                        costDataVPCT.AMOUNT = costDataVPCT.QUANTITY * costDataVPCT.PRICE;
        //                        UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataVPCT);
        //                        //row["PKID"] = Guid.NewGuid().ToString();
        //                        //row["ORG_CODE"] = orgCode,
        //                        //row["CHI_PHI_PROFIT_CENTER_CODE"]= centerCodeVPCT.CODE;
        //                        //row["TEMPLATE_CODE"]= ObjDetail.TEMPLATE_CODE;
        //                        //row["TIME_YEAR"] = ObjDetail.TIME_YEAR;
        //                        //row["MONTH"] = month;
        //                        //row["STATUS"] = Approve_Status.ChuaTrinhDuyet;
        //                        //row["VERSION"] = versionNext;
        //                        //row["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
        //                        //row["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][2].ToString()) ? "0" : tableData.Rows[i][2].ToString());
        //                        //row["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString());
        //                        //row["DESCRIPTION"] = tableData.Rows[i][15].ToString();
        //                        //row["CREATE_BY"] = currentUser;

        //                        //MB
        //                        var costDataMB = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                        costDataMB = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                        {
        //                            PKID = Guid.NewGuid().ToString(),
        //                            ORG_CODE = orgCode,
        //                            CHI_PHI_PROFIT_CENTER_CODE = centerCodeMB.CODE,
        //                            TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                            TIME_YEAR = ObjDetail.TIME_YEAR,
        //                            MONTH = month,

        //                            STATUS = Approve_Status.ChuaTrinhDuyet,
        //                            VERSION = versionNext,
        //                            KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                            QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][3].ToString()) ? "0" : tableData.Rows[i][3].ToString()),
        //                            PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString()),
        //                            DESCRIPTION = tableData.Rows[i][15].ToString(),
        //                            CREATE_BY = currentUser
        //                        };
        //                        costDataMB.AMOUNT = costDataMB.QUANTITY * costDataMB.PRICE;
        //                        UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataMB);

        //                        //MT
        //                        var costDataMT = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                        costDataMT = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                        {
        //                            PKID = Guid.NewGuid().ToString(),
        //                            ORG_CODE = orgCode,
        //                            CHI_PHI_PROFIT_CENTER_CODE = centerCodeMT.CODE,
        //                            TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                            TIME_YEAR = ObjDetail.TIME_YEAR,
        //                            MONTH = month,

        //                            STATUS = Approve_Status.ChuaTrinhDuyet,
        //                            VERSION = versionNext,
        //                            KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                            QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][4].ToString()) ? "0" : tableData.Rows[i][4].ToString()),
        //                            PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString()),
        //                            DESCRIPTION = tableData.Rows[i][15].ToString(),
        //                            CREATE_BY = currentUser
        //                        };
        //                        costDataMT.AMOUNT = costDataMT.QUANTITY *  costDataMT.PRICE;
        //                        UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataMT);

        //                        //CR
        //                        var costDataCR = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                        costDataCR = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                        {
        //                            PKID = Guid.NewGuid().ToString(),
        //                            ORG_CODE = orgCode,
        //                            CHI_PHI_PROFIT_CENTER_CODE = centerCodeCR.CODE,
        //                            TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                            TIME_YEAR = ObjDetail.TIME_YEAR,
        //                            MONTH = month,

        //                            STATUS = Approve_Status.ChuaTrinhDuyet,
        //                            VERSION = versionNext,
        //                            KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                            QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][5].ToString()) ? "0" : tableData.Rows[i][5].ToString()),
        //                            PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString()),
        //                            DESCRIPTION = tableData.Rows[i][15].ToString(),
        //                            CREATE_BY = currentUser
        //                        };
        //                        costDataCR.AMOUNT = costDataCR.QUANTITY *  costDataCR.PRICE;
        //                        UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataCR);

        //                        //MN
        //                        var costDataMN = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                        costDataMN = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                        {
        //                            PKID = Guid.NewGuid().ToString(),
        //                            ORG_CODE = orgCode,
        //                            CHI_PHI_PROFIT_CENTER_CODE = centerCodeMN.CODE,
        //                            TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                            TIME_YEAR = ObjDetail.TIME_YEAR,
        //                            MONTH = month,

        //                            STATUS = Approve_Status.ChuaTrinhDuyet,
        //                            VERSION = versionNext,
        //                            KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                            QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][6].ToString()) ? "0" : tableData.Rows[i][6].ToString()),
        //                            PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString()),
        //                            DESCRIPTION = tableData.Rows[i][15].ToString(),
        //                            CREATE_BY = currentUser
        //                        };
        //                        costDataMN.AMOUNT = costDataMN.QUANTITY * costDataMN.PRICE;
        //                        UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataMN);
        //                    }
        //                    else if (template.DetailKeHoachChiPhi.Any(x => x.Center.COST_CENTER_CODE == "100002"))
        //                    {
        //                        var str1 = tableData.Rows[i][2].ToString();
        //                        var str2 = tableData.Rows[i][3].ToString();
        //                        var str3 = tableData.Rows[i][4].ToString();
        //                        var str4 = tableData.Rows[i][5].ToString();
        //                        var str5 = tableData.Rows[i][6].ToString();
        //                        var str6 = tableData.Rows[i][7].ToString();
        //                        var str7 = tableData.Rows[i][8].ToString();

        //                        var str8 = tableData.Rows[i][10].ToString();
        //                        if (!decimal.TryParse(str1, out decimal value) && !string.IsNullOrEmpty(str1))
        //                        {
        //                            this.State = false;
        //                            this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                            return;
        //                        }
        //                        if (!decimal.TryParse(str2, out decimal value1) && !string.IsNullOrEmpty(str2))
        //                        {
        //                            this.State = false;
        //                            this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                            return;
        //                        }
        //                        if (!decimal.TryParse(str3, out decimal value2) && !string.IsNullOrEmpty(str3))
        //                        {
        //                            this.State = false;
        //                            this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                            return;
        //                        }
        //                        if (!decimal.TryParse(str4, out decimal value3) && !string.IsNullOrEmpty(str4))
        //                        {
        //                            this.State = false;
        //                            this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                            return;
        //                        }
        //                        if (!decimal.TryParse(str5, out decimal value4) && !string.IsNullOrEmpty(str5))
        //                        {
        //                            this.State = false;
        //                            this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                            return;
        //                        }
        //                        if (!decimal.TryParse(str6, out decimal value5) && !string.IsNullOrEmpty(str6))
        //                        {
        //                            this.State = false;
        //                            this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                            return;
        //                        }
        //                        if (!decimal.TryParse(str7, out decimal value6) && !string.IsNullOrEmpty(str7))
        //                        {
        //                            this.State = false;
        //                            this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                            return;
        //                        }
        //                        if (!decimal.TryParse(str8, out decimal value7) && !string.IsNullOrEmpty(str8))
        //                        {
        //                            this.State = false;
        //                            this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                            return;
        //                        }


        //                        var centerCodeVPCN = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VPCN" && x.COST_CENTER_CODE == "100002");
        //                        var centerCodeHAN = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "HAN" && x.COST_CENTER_CODE == "100002");
        //                        var centerCodeHPH = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "HPH" && x.COST_CENTER_CODE == "100002");
        //                        var centerCodeTHD = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "THD" && x.COST_CENTER_CODE == "100002");
        //                        var centerCodeVII = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VII" && x.COST_CENTER_CODE == "100002");
        //                        var centerCodeVDH = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VDH" && x.COST_CENTER_CODE == "100002");
        //                        var centerCodeVDO = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VDO" && x.COST_CENTER_CODE == "100002");

        //                        if (centerCodeVPCN == null || centerCodeHAN == null || centerCodeHPH == null || centerCodeTHD == null || centerCodeVII == null || centerCodeVDH == null || centerCodeVDO == null)
        //                        {
        //                            throw new Exception($"Định dạng file không đúng hoặc có lỗi hệ thống xảy ra! Vui lòng liên hệ với quản trị viên!");
        //                        }

        //                        //VPCN
        //                        var costDataVPCN = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                        costDataVPCN = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                        {
        //                            PKID = Guid.NewGuid().ToString(),
        //                            ORG_CODE = orgCode,
        //                            CHI_PHI_PROFIT_CENTER_CODE = centerCodeVPCN.CODE,
        //                            TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                            TIME_YEAR = ObjDetail.TIME_YEAR,
        //                            MONTH = month,

        //                            STATUS = Approve_Status.ChuaTrinhDuyet,
        //                            VERSION = versionNext,
        //                            KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                            QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][2].ToString()) ? "0" : tableData.Rows[i][2].ToString()),
        //                            PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()),
        //                            DESCRIPTION = tableData.Rows[i][19].ToString(),
        //                            CREATE_BY = currentUser
        //                        };
        //                        costDataVPCN.AMOUNT = costDataVPCN.QUANTITY * costDataVPCN.PRICE;
        //                        UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataVPCN);

        //                        //HAN
        //                        var costDataHAN = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                        costDataHAN = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                        {
        //                            PKID = Guid.NewGuid().ToString(),
        //                            ORG_CODE = orgCode,
        //                            CHI_PHI_PROFIT_CENTER_CODE = centerCodeHAN.CODE,
        //                            TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                            TIME_YEAR = ObjDetail.TIME_YEAR,
        //                            MONTH = month,

        //                            STATUS = Approve_Status.ChuaTrinhDuyet,
        //                            VERSION = versionNext,
        //                            KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                            QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][3].ToString()) ? "0" : tableData.Rows[i][3].ToString()),
        //                            PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()),
        //                            DESCRIPTION = tableData.Rows[i][19].ToString(),
        //                            CREATE_BY = currentUser
        //                        };
        //                        costDataHAN.AMOUNT = costDataHAN.QUANTITY *  costDataHAN.PRICE;
        //                        UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataHAN);

        //                        //VDO
        //                        var costDataHPH = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                        costDataHPH = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                        {
        //                            PKID = Guid.NewGuid().ToString(),
        //                            ORG_CODE = orgCode,
        //                            CHI_PHI_PROFIT_CENTER_CODE = centerCodeHPH.CODE,
        //                            TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                            TIME_YEAR = ObjDetail.TIME_YEAR,
        //                            MONTH = month,

        //                            STATUS = Approve_Status.ChuaTrinhDuyet,
        //                            VERSION = versionNext,
        //                            KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                            QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][4].ToString()) ? "0" : tableData.Rows[i][4].ToString()),
        //                            PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()),
        //                            DESCRIPTION = tableData.Rows[i][19].ToString(),
        //                            CREATE_BY = currentUser
        //                        };
        //                        costDataHPH.AMOUNT = costDataHPH.QUANTITY *  costDataHPH.PRICE;
        //                        UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataHPH);

        //                        //HPH
        //                        var costDataTHD = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                        costDataTHD = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                        {
        //                            PKID = Guid.NewGuid().ToString(),
        //                            ORG_CODE = orgCode,
        //                            CHI_PHI_PROFIT_CENTER_CODE = centerCodeTHD.CODE,
        //                            TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                            TIME_YEAR = ObjDetail.TIME_YEAR,
        //                            MONTH = month,

        //                            STATUS = Approve_Status.ChuaTrinhDuyet,
        //                            VERSION = versionNext,
        //                            KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                            QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][5].ToString()) ? "0" : tableData.Rows[i][5].ToString()),
        //                            PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()),
        //                            DESCRIPTION = tableData.Rows[i][19].ToString(),
        //                            CREATE_BY = currentUser
        //                        };
        //                        costDataTHD.AMOUNT = costDataTHD.QUANTITY * costDataTHD.PRICE;
        //                        UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataTHD);

        //                        //DIN
        //                        var costDataVII = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                        costDataVII = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                        {
        //                            PKID = Guid.NewGuid().ToString(),
        //                            ORG_CODE = orgCode,
        //                            CHI_PHI_PROFIT_CENTER_CODE = centerCodeVII.CODE,
        //                            TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                            TIME_YEAR = ObjDetail.TIME_YEAR,
        //                            MONTH = month,

        //                            STATUS = Approve_Status.ChuaTrinhDuyet,
        //                            VERSION = versionNext,
        //                            KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                            QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][6].ToString()) ? "0" : tableData.Rows[i][6].ToString()),
        //                            PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()),
        //                            DESCRIPTION = tableData.Rows[i][19].ToString(),
        //                            CREATE_BY = currentUser
        //                        };
        //                        costDataVII.AMOUNT = costDataVII.QUANTITY * costDataVII.PRICE;
        //                        UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataVII);

        //                        //THD
        //                        var costDataVDH = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                        costDataVDH = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                        {
        //                            PKID = Guid.NewGuid().ToString(),
        //                            ORG_CODE = orgCode,
        //                            CHI_PHI_PROFIT_CENTER_CODE = centerCodeVDH.CODE,
        //                            TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                            TIME_YEAR = ObjDetail.TIME_YEAR,
        //                            MONTH = month,

        //                            STATUS = Approve_Status.ChuaTrinhDuyet,
        //                            VERSION = versionNext,
        //                            KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                            QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString()),
        //                            PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()),
        //                            DESCRIPTION = tableData.Rows[i][19].ToString(),
        //                            CREATE_BY = currentUser
        //                        };
        //                        costDataVDH.AMOUNT = costDataVDH.QUANTITY * costDataVDH.PRICE;
        //                        UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataVDH);

        //                        //NAF
        //                        var costDataVDO = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                        costDataVDO = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                        {
        //                            PKID = Guid.NewGuid().ToString(),
        //                            ORG_CODE = orgCode,
        //                            CHI_PHI_PROFIT_CENTER_CODE = centerCodeVDO.CODE,
        //                            TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                            TIME_YEAR = ObjDetail.TIME_YEAR,
        //                            MONTH = month,

        //                            STATUS = Approve_Status.ChuaTrinhDuyet,
        //                            VERSION = versionNext,
        //                            KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                            QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString()),
        //                            PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()),
        //                            DESCRIPTION = tableData.Rows[i][19].ToString(),
        //                            CREATE_BY = currentUser
        //                        };
        //                        costDataVDO.AMOUNT = costDataVDO.QUANTITY * costDataVDO.PRICE;
        //                        UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataVDO);

        //                    }
        //                    else if (template.DetailKeHoachChiPhi.Any(x => x.Center.COST_CENTER_CODE == "100003"))
        //                    {
        //                        var str1 = tableData.Rows[i][2].ToString();
        //                        var str2 = tableData.Rows[i][3].ToString();
        //                        var str3 = tableData.Rows[i][4].ToString();
        //                        var str4 = tableData.Rows[i][5].ToString();
        //                        var str5 = tableData.Rows[i][6].ToString();
        //                        var str6 = tableData.Rows[i][7].ToString();
        //                        var str7 = tableData.Rows[i][8].ToString();
        //                        var str8 = tableData.Rows[i][9].ToString();

        //                        var str9 = tableData.Rows[i][11].ToString();
        //                        if (!decimal.TryParse(str1, out decimal value) && !string.IsNullOrEmpty(str1))
        //                        {
        //                            this.State = false;
        //                            this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                            return;
        //                        }
        //                        if (!decimal.TryParse(str2, out decimal value1) && !string.IsNullOrEmpty(str2))
        //                        {
        //                            this.State = false;
        //                            this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                            return;
        //                        }
        //                        if (!decimal.TryParse(str3, out decimal value2) && !string.IsNullOrEmpty(str3))
        //                        {
        //                            this.State = false;
        //                            this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                            return;
        //                        }
        //                        if (!decimal.TryParse(str4, out decimal value3) && !string.IsNullOrEmpty(str4))
        //                        {
        //                            this.State = false;
        //                            this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                            return;
        //                        }
        //                        if (!decimal.TryParse(str5, out decimal value4) && !string.IsNullOrEmpty(str5))
        //                        {
        //                            this.State = false;
        //                            this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                            return;
        //                        }
        //                        if (!decimal.TryParse(str6, out decimal value5) && !string.IsNullOrEmpty(str6))
        //                        {
        //                            this.State = false;
        //                            this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                            return;
        //                        }
        //                        if (!decimal.TryParse(str7, out decimal value6) && !string.IsNullOrEmpty(str7))
        //                        {
        //                            this.State = false;
        //                            this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                            return;
        //                        }
        //                        if (!decimal.TryParse(str8, out decimal value7) && !string.IsNullOrEmpty(str8))
        //                        {
        //                            this.State = false;
        //                            this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                            return;
        //                        }
        //                        if (!decimal.TryParse(str9, out decimal value8) && !string.IsNullOrEmpty(str9))
        //                        {
        //                            this.State = false;
        //                            this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                            return;
        //                        }

        //                        var centerCodeVPCN = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VPCN" && x.COST_CENTER_CODE == "100003");
        //                        var centerCodeDAD = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "DAD" && x.COST_CENTER_CODE == "100003");
        //                        var centerCodeCXR = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "CXR" && x.COST_CENTER_CODE == "100003");
        //                        var centerCodeHUI = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "HUI" && x.COST_CENTER_CODE == "100003");
        //                        var centerCodeUIH = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "UIH" && x.COST_CENTER_CODE == "100003");
        //                        var centerCodeVCL = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VCL" && x.COST_CENTER_CODE == "100003");
        //                        var centerCodePXU = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "PXU" && x.COST_CENTER_CODE == "100003");
        //                        var centerCodeTBB = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "TBB" && x.COST_CENTER_CODE == "100003");
        //                        if (centerCodeVPCN == null || centerCodeDAD == null || centerCodeCXR == null || centerCodeHUI == null || centerCodeUIH == null || centerCodeVCL == null || centerCodePXU == null || centerCodeTBB == null)
        //                        {
        //                            throw new Exception($"Định dạng file không đúng hoặc có lỗi hệ thống xảy ra! Vui lòng liên hệ với quản trị viên!");
        //                        }

        //                        //VPCN
        //                        var costDataVPCN = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                        costDataVPCN = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                        {
        //                            PKID = Guid.NewGuid().ToString(),
        //                            ORG_CODE = orgCode,
        //                            CHI_PHI_PROFIT_CENTER_CODE = centerCodeVPCN.CODE,
        //                            TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                            TIME_YEAR = ObjDetail.TIME_YEAR,
        //                            MONTH = month,

        //                            STATUS = Approve_Status.ChuaTrinhDuyet,
        //                            VERSION = versionNext,
        //                            KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                            QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][2].ToString()) ? "0" : tableData.Rows[i][2].ToString()),
        //                            PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString()),
        //                            DESCRIPTION = tableData.Rows[i][21].ToString(),
        //                            CREATE_BY = currentUser
        //                        };
        //                        costDataVPCN.AMOUNT = costDataVPCN.QUANTITY * costDataVPCN.PRICE;
        //                        UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataVPCN);

        //                        //DAD
        //                        var costDataDAD = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                        costDataDAD = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                        {
        //                            PKID = Guid.NewGuid().ToString(),
        //                            ORG_CODE = orgCode,
        //                            CHI_PHI_PROFIT_CENTER_CODE = centerCodeDAD.CODE,
        //                            TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                            TIME_YEAR = ObjDetail.TIME_YEAR,
        //                            MONTH = month,

        //                            STATUS = Approve_Status.ChuaTrinhDuyet,
        //                            VERSION = versionNext,
        //                            KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                            QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][3].ToString()) ? "0" : tableData.Rows[i][3].ToString()),
        //                            PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString()),
        //                            DESCRIPTION = tableData.Rows[i][21].ToString(),
        //                            CREATE_BY = currentUser
        //                        };
        //                        costDataDAD.AMOUNT = costDataDAD.QUANTITY *  costDataDAD.PRICE;
        //                        UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataDAD);

        //                        //CXR
        //                        var costDataCXR = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                        costDataCXR = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                        {
        //                            PKID = Guid.NewGuid().ToString(),
        //                            ORG_CODE = orgCode,
        //                            CHI_PHI_PROFIT_CENTER_CODE = centerCodeCXR.CODE,
        //                            TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                            TIME_YEAR = ObjDetail.TIME_YEAR,
        //                            MONTH = month,

        //                            STATUS = Approve_Status.ChuaTrinhDuyet,
        //                            VERSION = versionNext,
        //                            KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                            QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][4].ToString()) ? "0" : tableData.Rows[i][4].ToString()),
        //                            PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString()),
        //                            DESCRIPTION = tableData.Rows[i][21].ToString(),
        //                            CREATE_BY = currentUser
        //                        };
        //                        costDataCXR.AMOUNT = costDataCXR.QUANTITY * costDataCXR.PRICE;
        //                        UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataCXR);

        //                        //HUI
        //                        var costDataHUI = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                        costDataHUI = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                        {
        //                            PKID = Guid.NewGuid().ToString(),
        //                            ORG_CODE = orgCode,
        //                            CHI_PHI_PROFIT_CENTER_CODE = centerCodeHUI.CODE,
        //                            TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                            TIME_YEAR = ObjDetail.TIME_YEAR,
        //                            MONTH = month,

        //                            STATUS = Approve_Status.ChuaTrinhDuyet,
        //                            VERSION = versionNext,
        //                            KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                            QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][5].ToString()) ? "0" : tableData.Rows[i][5].ToString()),
        //                            PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString()),
        //                            DESCRIPTION = tableData.Rows[i][21].ToString(),
        //                            CREATE_BY = currentUser
        //                        };
        //                        costDataHUI.AMOUNT = costDataHUI.QUANTITY * costDataHUI.PRICE;
        //                        UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataHUI);

        //                        //UIH
        //                        var costDataUIH = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                        costDataUIH = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                        {
        //                            PKID = Guid.NewGuid().ToString(),
        //                            ORG_CODE = orgCode,
        //                            CHI_PHI_PROFIT_CENTER_CODE = centerCodeUIH.CODE,
        //                            TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                            TIME_YEAR = ObjDetail.TIME_YEAR,
        //                            MONTH = month,

        //                            STATUS = Approve_Status.ChuaTrinhDuyet,
        //                            VERSION = versionNext,
        //                            KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                            QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][6].ToString()) ? "0" : tableData.Rows[i][6].ToString()),
        //                            PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString()),
        //                            DESCRIPTION = tableData.Rows[i][21].ToString(),
        //                            CREATE_BY = currentUser
        //                        };
        //                        costDataUIH.AMOUNT = costDataUIH.QUANTITY *  costDataUIH.PRICE;
        //                        UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataUIH);

        //                        //VCL
        //                        var costDataVCL = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                        costDataVCL = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                        {
        //                            PKID = Guid.NewGuid().ToString(),
        //                            ORG_CODE = orgCode,
        //                            CHI_PHI_PROFIT_CENTER_CODE = centerCodeVCL.CODE,
        //                            TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                            TIME_YEAR = ObjDetail.TIME_YEAR,
        //                            MONTH = month,

        //                            STATUS = Approve_Status.ChuaTrinhDuyet,
        //                            VERSION = versionNext,
        //                            KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                            QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString()),
        //                            PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString()),
        //                            DESCRIPTION = tableData.Rows[i][21].ToString(),
        //                            CREATE_BY = currentUser
        //                        };
        //                        costDataVCL.AMOUNT = costDataVCL.QUANTITY *  costDataVCL.PRICE;
        //                        UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataVCL);



        //                        //PXU
        //                        var costDataPXU = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                        costDataPXU = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                        {
        //                            PKID = Guid.NewGuid().ToString(),
        //                            ORG_CODE = orgCode,
        //                            CHI_PHI_PROFIT_CENTER_CODE = centerCodePXU.CODE,
        //                            TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                            TIME_YEAR = ObjDetail.TIME_YEAR,
        //                            MONTH = month,

        //                            STATUS = Approve_Status.ChuaTrinhDuyet,
        //                            VERSION = versionNext,
        //                            KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                            QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString()),
        //                            PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString()),
        //                            DESCRIPTION = tableData.Rows[i][21].ToString(),
        //                            CREATE_BY = currentUser
        //                        };
        //                        costDataPXU.AMOUNT = costDataPXU.QUANTITY * costDataPXU.PRICE;
        //                        UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataPXU);





        //                        //TBB
        //                        var costDataTBB = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                        costDataTBB = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                        {
        //                            PKID = Guid.NewGuid().ToString(),
        //                            ORG_CODE = orgCode,
        //                            CHI_PHI_PROFIT_CENTER_CODE = centerCodeTBB.CODE,
        //                            TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                            TIME_YEAR = ObjDetail.TIME_YEAR,
        //                            MONTH = month,

        //                            STATUS = Approve_Status.ChuaTrinhDuyet,
        //                            VERSION = versionNext,
        //                            KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                            QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][9].ToString()) ? "0" : tableData.Rows[i][9].ToString()),
        //                            PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString()),
        //                            DESCRIPTION = tableData.Rows[i][21].ToString(),
        //                            CREATE_BY = currentUser
        //                        };
        //                        costDataTBB.AMOUNT = costDataTBB.QUANTITY *  costDataTBB.PRICE;
        //                        UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataTBB);

        //                    }
        //                    else if (template.DetailKeHoachChiPhi.Any(x => x.Center.COST_CENTER_CODE == "100004"))
        //                    {
        //                        var str1 = tableData.Rows[i][2].ToString();
        //                        var str2 = tableData.Rows[i][3].ToString();
        //                        var str3 = tableData.Rows[i][4].ToString();
        //                        var str4 = tableData.Rows[i][5].ToString();
        //                        var str5 = tableData.Rows[i][6].ToString();
        //                        var str6 = tableData.Rows[i][7].ToString();
        //                        var str7 = tableData.Rows[i][8].ToString();

        //                        var str9 = tableData.Rows[i][10].ToString();
        //                        if (!decimal.TryParse(str1, out decimal value) && !string.IsNullOrEmpty(str1))
        //                        {
        //                            this.State = false;
        //                            this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                            return;
        //                        }
        //                        if (!decimal.TryParse(str2, out decimal value1) && !string.IsNullOrEmpty(str2))
        //                        {
        //                            this.State = false;
        //                            this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                            return;
        //                        }
        //                        if (!decimal.TryParse(str3, out decimal value2) && !string.IsNullOrEmpty(str3))
        //                        {
        //                            this.State = false;
        //                            this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                            return;
        //                        }
        //                        if (!decimal.TryParse(str4, out decimal value3) && !string.IsNullOrEmpty(str4))
        //                        {
        //                            this.State = false;
        //                            this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                            return;
        //                        }
        //                        if (!decimal.TryParse(str5, out decimal value4) && !string.IsNullOrEmpty(str5))
        //                        {
        //                            this.State = false;
        //                            this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                            return;
        //                        }
        //                        if (!decimal.TryParse(str6, out decimal value5) && !string.IsNullOrEmpty(str6))
        //                        {
        //                            this.State = false;
        //                            this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                            return;
        //                        }
        //                        if (!decimal.TryParse(str7, out decimal value6) && !string.IsNullOrEmpty(str7))
        //                        {
        //                            this.State = false;
        //                            this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                            return;
        //                        }
        //                        if (!decimal.TryParse(str9, out decimal value8) && !string.IsNullOrEmpty(str9))
        //                        {
        //                            this.State = false;
        //                            this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                            return;
        //                        }

        //                        var centerCodeVPCN = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VPCN" && x.COST_CENTER_CODE == "100004");
        //                        var centerCodeSGN = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "SGN" && x.COST_CENTER_CODE == "100004");
        //                        var centerCodePQC = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "PQC" && x.COST_CENTER_CODE == "100004");
        //                        var centerCodeDLI = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "DLI" && x.COST_CENTER_CODE == "100004");
        //                        var centerCodeVCA = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VCA" && x.COST_CENTER_CODE == "100004");
        //                        var centerCodeBMV = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "BMV" && x.COST_CENTER_CODE == "100004");
        //                        var centerCodeVCS = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VCS" && x.COST_CENTER_CODE == "100004");
        //                        if (centerCodeVPCN == null || centerCodeSGN == null || centerCodePQC == null || centerCodeDLI == null || centerCodeVCA == null || centerCodeBMV == null || centerCodeVCS == null)
        //                        {
        //                            throw new Exception($"Định dạng file không đúng hoặc có lỗi hệ thống xảy ra! Vui lòng liên hệ với quản trị viên!");
        //                        }

        //                        //VPCN
        //                        var costDataVPCN = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                        costDataVPCN = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                        {
        //                            PKID = Guid.NewGuid().ToString(),
        //                            ORG_CODE = orgCode,
        //                            CHI_PHI_PROFIT_CENTER_CODE = centerCodeVPCN.CODE,
        //                            TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                            TIME_YEAR = ObjDetail.TIME_YEAR,
        //                            MONTH = month,

        //                            STATUS = Approve_Status.ChuaTrinhDuyet,
        //                            VERSION = versionNext,
        //                            KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                            QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][2].ToString()) ? "0" : tableData.Rows[i][2].ToString()),
        //                            PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()),
        //                            DESCRIPTION = tableData.Rows[i][19].ToString(),
        //                            CREATE_BY = currentUser
        //                        };
        //                        costDataVPCN.AMOUNT = costDataVPCN.QUANTITY * costDataVPCN.PRICE;
        //                        UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataVPCN);

        //                        //SGN
        //                        var costDataSGN = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                        costDataSGN = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                        {
        //                            PKID = Guid.NewGuid().ToString(),
        //                            ORG_CODE = orgCode,
        //                            CHI_PHI_PROFIT_CENTER_CODE = centerCodeSGN.CODE,
        //                            TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                            TIME_YEAR = ObjDetail.TIME_YEAR,
        //                            MONTH = month,

        //                            STATUS = Approve_Status.ChuaTrinhDuyet,
        //                            VERSION = versionNext,
        //                            KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                            QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][3].ToString()) ? "0" : tableData.Rows[i][3].ToString()),
        //                            PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()),
        //                            DESCRIPTION = tableData.Rows[i][19].ToString(),
        //                            CREATE_BY = currentUser
        //                        };
        //                        costDataSGN.AMOUNT = costDataSGN.QUANTITY * costDataSGN.PRICE;
        //                        UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataSGN);

        //                        //PQC
        //                        var costDataPQC = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                        costDataPQC = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                        {
        //                            PKID = Guid.NewGuid().ToString(),
        //                            ORG_CODE = orgCode,
        //                            CHI_PHI_PROFIT_CENTER_CODE = centerCodePQC.CODE,
        //                            TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                            TIME_YEAR = ObjDetail.TIME_YEAR,
        //                            MONTH = month,

        //                            STATUS = Approve_Status.ChuaTrinhDuyet,
        //                            VERSION = versionNext,
        //                            KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                            QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][4].ToString()) ? "0" : tableData.Rows[i][4].ToString()),
        //                            PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()),
        //                            DESCRIPTION = tableData.Rows[i][19].ToString(),
        //                            CREATE_BY = currentUser
        //                        };
        //                        costDataPQC.AMOUNT = costDataPQC.QUANTITY * costDataPQC.PRICE;
        //                        UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataPQC);

        //                        //DLI
        //                        var costDataDLI = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                        costDataDLI = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                        {
        //                            PKID = Guid.NewGuid().ToString(),
        //                            ORG_CODE = orgCode,
        //                            CHI_PHI_PROFIT_CENTER_CODE = centerCodeDLI.CODE,
        //                            TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                            TIME_YEAR = ObjDetail.TIME_YEAR,
        //                            MONTH = month,

        //                            STATUS = Approve_Status.ChuaTrinhDuyet,
        //                            VERSION = versionNext,
        //                            KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                            QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][5].ToString()) ? "0" : tableData.Rows[i][5].ToString()),
        //                            PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()),
        //                            DESCRIPTION = tableData.Rows[i][19].ToString(),
        //                            CREATE_BY = currentUser
        //                        };
        //                        costDataDLI.AMOUNT = costDataDLI.QUANTITY *  costDataDLI.PRICE;
        //                        UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataDLI);

        //                        //VCA
        //                        var costDataVCA = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                        costDataVCA = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                        {
        //                            PKID = Guid.NewGuid().ToString(),
        //                            ORG_CODE = orgCode,
        //                            CHI_PHI_PROFIT_CENTER_CODE = centerCodeVCA.CODE,
        //                            TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                            TIME_YEAR = ObjDetail.TIME_YEAR,
        //                            MONTH = month,

        //                            STATUS = Approve_Status.ChuaTrinhDuyet,
        //                            VERSION = versionNext,
        //                            KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                            QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][6].ToString()) ? "0" : tableData.Rows[i][6].ToString()),
        //                            PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()),
        //                            DESCRIPTION = tableData.Rows[i][19].ToString(),
        //                            CREATE_BY = currentUser
        //                        };
        //                        costDataVCA.AMOUNT = costDataVCA.QUANTITY *  costDataVCA.PRICE;
        //                        UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataVCA);

        //                        //BMV
        //                        var costDataBMV = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                        costDataBMV = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                        {
        //                            PKID = Guid.NewGuid().ToString(),
        //                            ORG_CODE = orgCode,
        //                            CHI_PHI_PROFIT_CENTER_CODE = centerCodeBMV.CODE,
        //                            TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                            TIME_YEAR = ObjDetail.TIME_YEAR,
        //                            MONTH = month,

        //                            STATUS = Approve_Status.ChuaTrinhDuyet,
        //                            VERSION = versionNext,
        //                            KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                            QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString()),
        //                            PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()),
        //                            DESCRIPTION = tableData.Rows[i][19].ToString(),
        //                            CREATE_BY = currentUser
        //                        };
        //                        costDataBMV.AMOUNT = costDataBMV.QUANTITY * costDataBMV.PRICE;
        //                        UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataBMV);

        //                        //VCS
        //                        var costDataVCS = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                        costDataVCS = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                        {
        //                            PKID = Guid.NewGuid().ToString(),
        //                            ORG_CODE = orgCode,
        //                            CHI_PHI_PROFIT_CENTER_CODE = centerCodeVCS.CODE,
        //                            TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                            TIME_YEAR = ObjDetail.TIME_YEAR,
        //                            MONTH = month,

        //                            STATUS = Approve_Status.ChuaTrinhDuyet,
        //                            VERSION = versionNext,
        //                            KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                            QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString()),
        //                            PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()),
        //                            DESCRIPTION = tableData.Rows[i][19].ToString(),
        //                            CREATE_BY = currentUser
        //                        };
        //                        costDataVCS.AMOUNT = costDataVCS.QUANTITY * costDataVCS.PRICE;
        //                        UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataVCS);

        //                    }
        //                    else if (template.DetailKeHoachChiPhi.Any(x => x.Center.COST_CENTER_CODE == "100005"))
        //                    {
        //                        var str1 = tableData.Rows[i][2].ToString();
        //                        var str2 = tableData.Rows[i][3].ToString();
        //                        var str3 = tableData.Rows[i][4].ToString();
        //                        var str4 = tableData.Rows[i][5].ToString();

        //                        var str9 = tableData.Rows[i][7].ToString();
        //                        if (!decimal.TryParse(str1, out decimal value) && !string.IsNullOrEmpty(str1))
        //                        {
        //                            this.State = false;
        //                            this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                            return;
        //                        }
        //                        if (!decimal.TryParse(str2, out decimal value1) && !string.IsNullOrEmpty(str2))
        //                        {
        //                            this.State = false;
        //                            this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                            return;
        //                        }
        //                        if (!decimal.TryParse(str3, out decimal value2) && !string.IsNullOrEmpty(str3))
        //                        {
        //                            this.State = false;
        //                            this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                            return;
        //                        }
        //                        if (!decimal.TryParse(str4, out decimal value3) && !string.IsNullOrEmpty(str4))
        //                        {
        //                            this.State = false;
        //                            this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                            return;
        //                        }
        //                        if (!decimal.TryParse(str9, out decimal value8) && !string.IsNullOrEmpty(str9))
        //                        {
        //                            this.State = false;
        //                            this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                            return;
        //                        }
        //                        var centerCodeVPCN = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VPCN" && x.COST_CENTER_CODE == "100005");
        //                        var centerCodeVTMB = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VTMB" && x.COST_CENTER_CODE == "100005");
        //                        var centerCodeVTMT = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VTMT" && x.COST_CENTER_CODE == "100005");
        //                        var centerCodeVTMN = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VTMN" && x.COST_CENTER_CODE == "100005");
        //                        if (centerCodeVPCN == null || centerCodeVTMB == null || centerCodeVTMT == null || centerCodeVTMN == null)
        //                        {
        //                            throw new Exception($"Định dạng file không đúng hoặc có lỗi hệ thống xảy ra! Vui lòng liên hệ với quản trị viên!");
        //                        }

        //                        //VPCN
        //                        var costDataVPCN = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                        costDataVPCN = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                        {
        //                            PKID = Guid.NewGuid().ToString(),
        //                            ORG_CODE = orgCode,
        //                            CHI_PHI_PROFIT_CENTER_CODE = centerCodeVPCN.CODE,
        //                            TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                            TIME_YEAR = ObjDetail.TIME_YEAR,
        //                            MONTH = month,

        //                            STATUS = Approve_Status.ChuaTrinhDuyet,
        //                            VERSION = versionNext,
        //                            KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                            QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][2].ToString()) ? "0" : tableData.Rows[i][2].ToString()),
        //                            PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString()),
        //                            DESCRIPTION = tableData.Rows[i][13].ToString(),
        //                            CREATE_BY = currentUser
        //                        };
        //                        costDataVPCN.AMOUNT = costDataVPCN.QUANTITY *costDataVPCN.PRICE;
        //                        UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataVPCN);

        //                        //VTMB
        //                        var costDataVTMB = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                        costDataVTMB = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                        {
        //                            PKID = Guid.NewGuid().ToString(),
        //                            ORG_CODE = orgCode,
        //                            CHI_PHI_PROFIT_CENTER_CODE = centerCodeVTMB.CODE,
        //                            TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                            TIME_YEAR = ObjDetail.TIME_YEAR,
        //                            MONTH = month,

        //                            STATUS = Approve_Status.ChuaTrinhDuyet,
        //                            VERSION = versionNext,
        //                            KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                            QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][3].ToString()) ? "0" : tableData.Rows[i][3].ToString()),
        //                            PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString()),
        //                            DESCRIPTION = tableData.Rows[i][13].ToString(),
        //                            CREATE_BY = currentUser
        //                        };
        //                        costDataVTMB.AMOUNT = costDataVTMB.QUANTITY * costDataVTMB.PRICE;
        //                        UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataVTMB);

        //                        //VTMT
        //                        var costDataVTMT = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                        costDataVTMT = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                        {
        //                            PKID = Guid.NewGuid().ToString(),
        //                            ORG_CODE = orgCode,
        //                            CHI_PHI_PROFIT_CENTER_CODE = centerCodeVTMT.CODE,
        //                            TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                            TIME_YEAR = ObjDetail.TIME_YEAR,
        //                            MONTH = month,

        //                            STATUS = Approve_Status.ChuaTrinhDuyet,
        //                            VERSION = versionNext,
        //                            KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                            QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][4].ToString()) ? "0" : tableData.Rows[i][4].ToString()),
        //                            PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString()),
        //                            DESCRIPTION = tableData.Rows[i][13].ToString(),
        //                            CREATE_BY = currentUser
        //                        };
        //                        costDataVTMT.AMOUNT = costDataVTMT.QUANTITY * costDataVTMT.PRICE;
        //                        UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataVTMT);

        //                        //VTMN
        //                        var costDataVTMN = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                        costDataVTMN = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                        {
        //                            PKID = Guid.NewGuid().ToString(),
        //                            ORG_CODE = orgCode,
        //                            CHI_PHI_PROFIT_CENTER_CODE = centerCodeVTMN.CODE,
        //                            TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                            TIME_YEAR = ObjDetail.TIME_YEAR,
        //                            MONTH = month,

        //                            STATUS = Approve_Status.ChuaTrinhDuyet,
        //                            VERSION = versionNext,
        //                            KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                            QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][5].ToString()) ? "0" : tableData.Rows[i][5].ToString()),
        //                            PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString()),
        //                            DESCRIPTION = tableData.Rows[i][13].ToString(),
        //                            CREATE_BY = currentUser
        //                        };
        //                        costDataVTMN.AMOUNT = costDataVTMN.QUANTITY * costDataVTMN.PRICE;
        //                        UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataVTMN);
        //                    }
        //                    else
        //                    {
        //                        throw new Exception($"Định dạng file không đúng hoặc có lỗi hệ thống xảy ra! Vui lòng liên hệ với quản trị viên!");
        //                    }
        //                }
        //            }

        //            else
        //            {
        //                //Rẽ nhánh cho các template khác nhau của 4 chi nhánh

        //                if (template.DetailKeHoachChiPhi.Any(x => x.Center?.COST_CENTER_CODE == "100001"))
        //                {
        //                    var str1 = tableData.Rows[i][2].ToString();
        //                    var str3 = tableData.Rows[i][4].ToString();
        //                    var str4 = tableData.Rows[i][5].ToString();
        //                    var str5 = tableData.Rows[i][6].ToString();
        //                    var str6 = tableData.Rows[i][3].ToString();

        //                    var str2 = tableData.Rows[i][8].ToString();
        //                    if (!decimal.TryParse(str1, out decimal value) && !string.IsNullOrEmpty(str1))
        //                    {
        //                        this.State = false;
        //                        this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                        return;
        //                    }
        //                    if (!decimal.TryParse(str3, out decimal value1) && !string.IsNullOrEmpty(str3))
        //                    {
        //                        this.State = false;
        //                        this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                        return;
        //                    }
        //                    if (!decimal.TryParse(str4, out decimal value2) && !string.IsNullOrEmpty(str4))
        //                    {
        //                        this.State = false;
        //                        this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                        return;
        //                    }
        //                    if (!decimal.TryParse(str5, out decimal value3) && !string.IsNullOrEmpty(str5))
        //                    {
        //                        this.State = false;
        //                        this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                        return;
        //                    }
        //                    if (!decimal.TryParse(str6, out decimal value4) && !string.IsNullOrEmpty(str6))
        //                    {
        //                        this.State = false;
        //                        this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                        return;
        //                    }
        //                    if (!decimal.TryParse(str2, out decimal value5) && !string.IsNullOrEmpty(str2))
        //                    {
        //                        this.State = false;
        //                        this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                        return;
        //                    }

        //                    var centerCodeVPCT = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VPCT" && x.COST_CENTER_CODE == "100001");
        //                    var centerCodeMB = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "MB" && x.COST_CENTER_CODE == "100001");
        //                    var centerCodeMT = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "MT" && x.COST_CENTER_CODE == "100001");
        //                    var centerCodeCR = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "CR" && x.COST_CENTER_CODE == "100001");
        //                    var centerCodeMN = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "MN" && x.COST_CENTER_CODE == "100001");

        //                    if (centerCodeVPCT == null || centerCodeMB == null || centerCodeMT == null || centerCodeCR == null || centerCodeMN == null)
        //                    {
        //                        throw new Exception($"Định dạng file không đúng hoặc có lỗi hệ thống xảy ra! Vui lòng liên hệ với quản trị viên!");
        //                    }

        //                    //VPCT
        //                    var costDataVPCT = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                    costDataVPCT = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                    {
        //                        PKID = Guid.NewGuid().ToString(),
        //                        ORG_CODE = orgCode,
        //                        CHI_PHI_PROFIT_CENTER_CODE = centerCodeVPCT.CODE,
        //                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                        TIME_YEAR = ObjDetail.TIME_YEAR,
        //                        STATUS = Approve_Status.ChuaTrinhDuyet,
        //                        VERSION = versionNext,
        //                        KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                        QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][2].ToString()) ? "0" : tableData.Rows[i][2].ToString()),
        //                        PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString()),
        //                        DESCRIPTION = tableData.Rows[i][15].ToString(),
        //                        CREATE_BY = currentUser
        //                    };
        //                    costDataVPCT.AMOUNT = costDataVPCT.QUANTITY *  costDataVPCT.PRICE;
        //                    UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataVPCT);

        //                    //MB
        //                    var costDataMB = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                    costDataMB = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                    {
        //                        PKID = Guid.NewGuid().ToString(),
        //                        ORG_CODE = orgCode,
        //                        CHI_PHI_PROFIT_CENTER_CODE = centerCodeMB.CODE,
        //                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                        TIME_YEAR = ObjDetail.TIME_YEAR,
        //                        STATUS = Approve_Status.ChuaTrinhDuyet,
        //                        VERSION = versionNext,
        //                        KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                        QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][3].ToString()) ? "0" : tableData.Rows[i][3].ToString()),
        //                        PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString()),
        //                        DESCRIPTION = tableData.Rows[i][15].ToString(),
        //                        CREATE_BY = currentUser
        //                    };
        //                    costDataMB.AMOUNT = costDataMB.QUANTITY * costDataMB.PRICE;
        //                    UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataMB);

        //                    //MT
        //                    var costDataMT = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                    costDataMT = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                    {
        //                        PKID = Guid.NewGuid().ToString(),
        //                        ORG_CODE = orgCode,
        //                        CHI_PHI_PROFIT_CENTER_CODE = centerCodeMT.CODE,
        //                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                        TIME_YEAR = ObjDetail.TIME_YEAR,
        //                        STATUS = Approve_Status.ChuaTrinhDuyet,
        //                        VERSION = versionNext,
        //                        KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                        QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][4].ToString()) ? "0" : tableData.Rows[i][4].ToString()),
        //                        PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString()),
        //                        DESCRIPTION = tableData.Rows[i][15].ToString(),
        //                        CREATE_BY = currentUser
        //                    };
        //                    costDataMT.AMOUNT = costDataMT.QUANTITY * costDataMT.PRICE;
        //                    UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataMT);

        //                    //CR
        //                    var costDataCR = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                    costDataCR = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                    {
        //                        PKID = Guid.NewGuid().ToString(),
        //                        ORG_CODE = orgCode,
        //                        CHI_PHI_PROFIT_CENTER_CODE = centerCodeCR.CODE,
        //                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                        TIME_YEAR = ObjDetail.TIME_YEAR,
        //                        STATUS = Approve_Status.ChuaTrinhDuyet,
        //                        VERSION = versionNext,
        //                        KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                        QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][5].ToString()) ? "0" : tableData.Rows[i][5].ToString()),
        //                        PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString()),
        //                        DESCRIPTION = tableData.Rows[i][15].ToString(),
        //                        CREATE_BY = currentUser
        //                    };
        //                    costDataCR.AMOUNT = costDataCR.QUANTITY *  costDataCR.PRICE;
        //                    UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataCR);

        //                    //MN
        //                    var costDataMN = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                    costDataMN = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                    {
        //                        PKID = Guid.NewGuid().ToString(),
        //                        ORG_CODE = orgCode,
        //                        CHI_PHI_PROFIT_CENTER_CODE = centerCodeMN.CODE,
        //                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                        TIME_YEAR = ObjDetail.TIME_YEAR,
        //                        STATUS = Approve_Status.ChuaTrinhDuyet,
        //                        VERSION = versionNext,
        //                        KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                        QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][6].ToString()) ? "0" : tableData.Rows[i][6].ToString()),
        //                        PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString()),
        //                        DESCRIPTION = tableData.Rows[i][15].ToString(),
        //                        CREATE_BY = currentUser
        //                    };
        //                    costDataMN.AMOUNT = costDataMN.QUANTITY * costDataMN.PRICE;
        //                    UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataMN);
        //                }
        //                else if (template.DetailKeHoachChiPhi.Any(x => x.Center?.COST_CENTER_CODE == "100002"))
        //                {
        //                    var str1 = tableData.Rows[i][2].ToString();
        //                    var str2 = tableData.Rows[i][3].ToString();
        //                    var str3 = tableData.Rows[i][4].ToString();
        //                    var str4 = tableData.Rows[i][5].ToString();
        //                    var str5 = tableData.Rows[i][6].ToString();
        //                    var str6 = tableData.Rows[i][7].ToString();
        //                    var str7 = tableData.Rows[i][8].ToString();

        //                    var str8 = tableData.Rows[i][10].ToString();
        //                    if (!decimal.TryParse(str1, out decimal value) && !string.IsNullOrEmpty(str1))
        //                    {
        //                        this.State = false;
        //                        this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                        return;
        //                    }
        //                    if (!decimal.TryParse(str2, out decimal value1) && !string.IsNullOrEmpty(str2))
        //                    {
        //                        this.State = false;
        //                        this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                        return;
        //                    }
        //                    if (!decimal.TryParse(str3, out decimal value2) && !string.IsNullOrEmpty(str3))
        //                    {
        //                        this.State = false;
        //                        this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                        return;
        //                    }
        //                    if (!decimal.TryParse(str4, out decimal value3) && !string.IsNullOrEmpty(str4))
        //                    {
        //                        this.State = false;
        //                        this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                        return;
        //                    }
        //                    if (!decimal.TryParse(str5, out decimal value4) && !string.IsNullOrEmpty(str5))
        //                    {
        //                        this.State = false;
        //                        this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                        return;
        //                    }
        //                    if (!decimal.TryParse(str6, out decimal value5) && !string.IsNullOrEmpty(str6))
        //                    {
        //                        this.State = false;
        //                        this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                        return;
        //                    }
        //                    if (!decimal.TryParse(str7, out decimal value6) && !string.IsNullOrEmpty(str7))
        //                    {
        //                        this.State = false;
        //                        this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                        return;
        //                    }
        //                    if (!decimal.TryParse(str8, out decimal value7) && !string.IsNullOrEmpty(str8))
        //                    {
        //                        this.State = false;
        //                        this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                        return;
        //                    }

        //                    var centerCodeVPCN = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VPCN" && x.COST_CENTER_CODE == "100002");
        //                    var centerCodeHAN = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "HAN" && x.COST_CENTER_CODE == "100002");
        //                    var centerCodeHPH = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "HPH" && x.COST_CENTER_CODE == "100002");
        //                    var centerCodeTHD = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "THD" && x.COST_CENTER_CODE == "100002");
        //                    var centerCodeVII = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VII" && x.COST_CENTER_CODE == "100002");
        //                    var centerCodeVDH = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VDH" && x.COST_CENTER_CODE == "100002");
        //                    var centerCodeVDO = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VDO" && x.COST_CENTER_CODE == "100002");

        //                    if (centerCodeVPCN == null || centerCodeHAN == null || centerCodeHPH == null || centerCodeTHD == null || centerCodeVII == null || centerCodeVDH == null || centerCodeVDO == null)
        //                    {
        //                        throw new Exception($"Định dạng file không đúng hoặc có lỗi hệ thống xảy ra! Vui lòng liên hệ với quản trị viên!");
        //                    }

        //                    //VPCN
        //                    var costDataVPCN = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                    costDataVPCN = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                    {
        //                        PKID = Guid.NewGuid().ToString(),
        //                        ORG_CODE = orgCode,
        //                        CHI_PHI_PROFIT_CENTER_CODE = centerCodeVPCN.CODE,
        //                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                        TIME_YEAR = ObjDetail.TIME_YEAR,
        //                        STATUS = Approve_Status.ChuaTrinhDuyet,
        //                        VERSION = versionNext,
        //                        KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                        QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][2].ToString()) ? "0" : tableData.Rows[i][2].ToString()),
        //                        PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()),
        //                        DESCRIPTION = tableData.Rows[i][19].ToString(),
        //                        CREATE_BY = currentUser
        //                    };
        //                    costDataVPCN.AMOUNT = costDataVPCN.QUANTITY * costDataVPCN.PRICE;
        //                    UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataVPCN);

        //                    //HAN
        //                    var costDataHAN = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                    costDataHAN = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                    {
        //                        PKID = Guid.NewGuid().ToString(),
        //                        ORG_CODE = orgCode,
        //                        CHI_PHI_PROFIT_CENTER_CODE = centerCodeHAN.CODE,
        //                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                        TIME_YEAR = ObjDetail.TIME_YEAR,
        //                        STATUS = Approve_Status.ChuaTrinhDuyet,
        //                        VERSION = versionNext,
        //                        KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                        QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][3].ToString()) ? "0" : tableData.Rows[i][3].ToString()),
        //                        PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()),
        //                        DESCRIPTION = tableData.Rows[i][19].ToString(),
        //                        CREATE_BY = currentUser
        //                    };
        //                    costDataHAN.AMOUNT = costDataHAN.QUANTITY * costDataHAN.PRICE;
        //                    UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataHAN);

        //                    //VDO
        //                    var costDataHPH = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                    costDataHPH = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                    {
        //                        PKID = Guid.NewGuid().ToString(),
        //                        ORG_CODE = orgCode,
        //                        CHI_PHI_PROFIT_CENTER_CODE = centerCodeHPH.CODE,
        //                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                        TIME_YEAR = ObjDetail.TIME_YEAR,
        //                        STATUS = Approve_Status.ChuaTrinhDuyet,
        //                        VERSION = versionNext,
        //                        KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                        QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][4].ToString()) ? "0" : tableData.Rows[i][4].ToString()),
        //                        PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()),
        //                        DESCRIPTION = tableData.Rows[i][19].ToString(),
        //                        CREATE_BY = currentUser
        //                    };
        //                    costDataHPH.AMOUNT = costDataHPH.QUANTITY * costDataHPH.PRICE;
        //                    UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataHPH);

        //                    //HPH
        //                    var costDataTHD = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                    costDataTHD = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                    {
        //                        PKID = Guid.NewGuid().ToString(),
        //                        ORG_CODE = orgCode,
        //                        CHI_PHI_PROFIT_CENTER_CODE = centerCodeTHD.CODE,
        //                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                        TIME_YEAR = ObjDetail.TIME_YEAR,
        //                        STATUS = Approve_Status.ChuaTrinhDuyet,
        //                        VERSION = versionNext,
        //                        KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                        QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][5].ToString()) ? "0" : tableData.Rows[i][5].ToString()),
        //                        PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()),
        //                        DESCRIPTION = tableData.Rows[i][19].ToString(),
        //                        CREATE_BY = currentUser
        //                    };
        //                    costDataTHD.AMOUNT = costDataTHD.QUANTITY * costDataTHD.PRICE;
        //                    UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataTHD);

        //                    //DIN
        //                    var costDataVII = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                    costDataVII = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                    {
        //                        PKID = Guid.NewGuid().ToString(),
        //                        ORG_CODE = orgCode,
        //                        CHI_PHI_PROFIT_CENTER_CODE = centerCodeVII.CODE,
        //                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                        TIME_YEAR = ObjDetail.TIME_YEAR,
        //                        STATUS = Approve_Status.ChuaTrinhDuyet,
        //                        VERSION = versionNext,
        //                        KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                        QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][6].ToString()) ? "0" : tableData.Rows[i][6].ToString()),
        //                        PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()),
        //                        DESCRIPTION = tableData.Rows[i][19].ToString(),
        //                        CREATE_BY = currentUser
        //                    };
        //                    costDataVII.AMOUNT = costDataVII.QUANTITY * costDataVII.PRICE;
        //                    UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataVII);

        //                    //THD
        //                    var costDataVDH = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                    costDataVDH = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                    {
        //                        PKID = Guid.NewGuid().ToString(),
        //                        ORG_CODE = orgCode,
        //                        CHI_PHI_PROFIT_CENTER_CODE = centerCodeVDH.CODE,
        //                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                        TIME_YEAR = ObjDetail.TIME_YEAR,
        //                        STATUS = Approve_Status.ChuaTrinhDuyet,
        //                        VERSION = versionNext,
        //                        KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                        QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString()),
        //                        PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()),
        //                        DESCRIPTION = tableData.Rows[i][19].ToString(),
        //                        CREATE_BY = currentUser
        //                    };
        //                    costDataVDH.AMOUNT = costDataVDH.QUANTITY *costDataVDH.PRICE;
        //                    UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataVDH);

        //                    //NAF
        //                    var costDataVDO = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                    costDataVDO = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                    {
        //                        PKID = Guid.NewGuid().ToString(),
        //                        ORG_CODE = orgCode,
        //                        CHI_PHI_PROFIT_CENTER_CODE = centerCodeVDO.CODE,
        //                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                        TIME_YEAR = ObjDetail.TIME_YEAR,
        //                        STATUS = Approve_Status.ChuaTrinhDuyet,
        //                        VERSION = versionNext,
        //                        KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                        QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString()),
        //                        PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()),
        //                        DESCRIPTION = tableData.Rows[i][19].ToString(),
        //                        CREATE_BY = currentUser
        //                    };
        //                    costDataVDO.AMOUNT = costDataVDO.QUANTITY *  costDataVDO.PRICE;
        //                    UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataVDO);

        //                }
        //                else if (template.DetailKeHoachChiPhi.Any(x => x.Center?.COST_CENTER_CODE == "100003"))
        //                {
        //                    var str1 = tableData.Rows[i][2].ToString();
        //                    var str2 = tableData.Rows[i][3].ToString();
        //                    var str3 = tableData.Rows[i][4].ToString();
        //                    var str4 = tableData.Rows[i][5].ToString();
        //                    var str5 = tableData.Rows[i][6].ToString();
        //                    var str6 = tableData.Rows[i][7].ToString();
        //                    var str7 = tableData.Rows[i][8].ToString();
        //                    var str8 = tableData.Rows[i][9].ToString();

        //                    var str9 = tableData.Rows[i][11].ToString();
        //                    if (!decimal.TryParse(str1, out decimal value) && !string.IsNullOrEmpty(str1))
        //                    {
        //                        this.State = false;
        //                        this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                        return;
        //                    }
        //                    if (!decimal.TryParse(str2, out decimal value1) && !string.IsNullOrEmpty(str2))
        //                    {
        //                        this.State = false;
        //                        this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                        return;
        //                    }
        //                    if (!decimal.TryParse(str3, out decimal value2) && !string.IsNullOrEmpty(str3))
        //                    {
        //                        this.State = false;
        //                        this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                        return;
        //                    }
        //                    if (!decimal.TryParse(str4, out decimal value3) && !string.IsNullOrEmpty(str4))
        //                    {
        //                        this.State = false;
        //                        this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                        return;
        //                    }
        //                    if (!decimal.TryParse(str5, out decimal value4) && !string.IsNullOrEmpty(str5))
        //                    {
        //                        this.State = false;
        //                        this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                        return;
        //                    }
        //                    if (!decimal.TryParse(str6, out decimal value5) && !string.IsNullOrEmpty(str6))
        //                    {
        //                        this.State = false;
        //                        this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                        return;
        //                    }
        //                    if (!decimal.TryParse(str7, out decimal value6) && !string.IsNullOrEmpty(str7))
        //                    {
        //                        this.State = false;
        //                        this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                        return;
        //                    }
        //                    if (!decimal.TryParse(str8, out decimal value7) && !string.IsNullOrEmpty(str8))
        //                    {
        //                        this.State = false;
        //                        this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                        return;
        //                    }
        //                    if (!decimal.TryParse(str9, out decimal value8) && !string.IsNullOrEmpty(str9))
        //                    {
        //                        this.State = false;
        //                        this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                        return;
        //                    }

        //                    var centerCodeVPCN = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VPCN" && x.COST_CENTER_CODE == "100003");
        //                    var centerCodeDAD = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "DAD" && x.COST_CENTER_CODE == "100003");
        //                    var centerCodeCXR = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "CXR" && x.COST_CENTER_CODE == "100003");
        //                    var centerCodeHUI = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "HUI" && x.COST_CENTER_CODE == "100003");
        //                    var centerCodeUIH = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "UIH" && x.COST_CENTER_CODE == "100003");
        //                    var centerCodeVCL = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VCL" && x.COST_CENTER_CODE == "100003");
        //                    var centerCodePXU = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "PXU" && x.COST_CENTER_CODE == "100003");
        //                    var centerCodeTBB = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "TBB" && x.COST_CENTER_CODE == "100003");
        //                    if (centerCodeVPCN == null || centerCodeDAD == null || centerCodeCXR == null || centerCodeHUI == null || centerCodeUIH == null || centerCodeVCL == null || centerCodePXU == null || centerCodeTBB == null)
        //                    {
        //                        throw new Exception($"Định dạng file không đúng hoặc có lỗi hệ thống xảy ra! Vui lòng liên hệ với quản trị viên!");
        //                    }

        //                    //VPCN
        //                    var costDataVPCN = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                    costDataVPCN = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                    {
        //                        PKID = Guid.NewGuid().ToString(),
        //                        ORG_CODE = orgCode,
        //                        CHI_PHI_PROFIT_CENTER_CODE = centerCodeVPCN.CODE,
        //                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                        TIME_YEAR = ObjDetail.TIME_YEAR,
        //                        STATUS = Approve_Status.ChuaTrinhDuyet,
        //                        VERSION = versionNext,
        //                        KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                        QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][2].ToString()) ? "0" : tableData.Rows[i][2].ToString()),
        //                        PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString()),
        //                        DESCRIPTION = tableData.Rows[i][21].ToString(),
        //                        CREATE_BY = currentUser
        //                    };
        //                    costDataVPCN.AMOUNT = costDataVPCN.QUANTITY *  costDataVPCN.PRICE;
        //                    UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataVPCN);

        //                    //DAD
        //                    var costDataDAD = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                    costDataDAD = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                    {
        //                        PKID = Guid.NewGuid().ToString(),
        //                        ORG_CODE = orgCode,
        //                        CHI_PHI_PROFIT_CENTER_CODE = centerCodeDAD.CODE,
        //                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                        TIME_YEAR = ObjDetail.TIME_YEAR,
        //                        STATUS = Approve_Status.ChuaTrinhDuyet,
        //                        VERSION = versionNext,
        //                        KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                        QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][3].ToString()) ? "0" : tableData.Rows[i][3].ToString()),
        //                        PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString()),
        //                        DESCRIPTION = tableData.Rows[i][21].ToString(),
        //                        CREATE_BY = currentUser
        //                    };
        //                    costDataDAD.AMOUNT = costDataDAD.QUANTITY *  costDataDAD.PRICE;
        //                    UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataDAD);

        //                    //CXR
        //                    var costDataCXR = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                    costDataCXR = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                    {
        //                        PKID = Guid.NewGuid().ToString(),
        //                        ORG_CODE = orgCode,
        //                        CHI_PHI_PROFIT_CENTER_CODE = centerCodeCXR.CODE,
        //                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                        TIME_YEAR = ObjDetail.TIME_YEAR,
        //                        STATUS = Approve_Status.ChuaTrinhDuyet,
        //                        VERSION = versionNext,
        //                        KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                        QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][4].ToString()) ? "0" : tableData.Rows[i][4].ToString()),
        //                        PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString()),
        //                        DESCRIPTION = tableData.Rows[i][21].ToString(),
        //                        CREATE_BY = currentUser
        //                    };
        //                    costDataCXR.AMOUNT = costDataCXR.QUANTITY * costDataCXR.PRICE;
        //                    UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataCXR);

        //                    //HUI
        //                    var costDataHUI = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                    costDataHUI = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                    {
        //                        PKID = Guid.NewGuid().ToString(),
        //                        ORG_CODE = orgCode,
        //                        CHI_PHI_PROFIT_CENTER_CODE = centerCodeHUI.CODE,
        //                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                        TIME_YEAR = ObjDetail.TIME_YEAR,
        //                        STATUS = Approve_Status.ChuaTrinhDuyet,
        //                        VERSION = versionNext,
        //                        KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                        QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][5].ToString()) ? "0" : tableData.Rows[i][5].ToString()),
        //                        PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString()),
        //                        DESCRIPTION = tableData.Rows[i][21].ToString(),
        //                        CREATE_BY = currentUser
        //                    };
        //                    costDataHUI.AMOUNT = costDataHUI.QUANTITY *  costDataHUI.PRICE;
        //                    UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataHUI);

        //                    //UIH
        //                    var costDataUIH = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                    costDataUIH = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                    {
        //                        PKID = Guid.NewGuid().ToString(),
        //                        ORG_CODE = orgCode,
        //                        CHI_PHI_PROFIT_CENTER_CODE = centerCodeUIH.CODE,
        //                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                        TIME_YEAR = ObjDetail.TIME_YEAR,
        //                        STATUS = Approve_Status.ChuaTrinhDuyet,
        //                        VERSION = versionNext,
        //                        KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                        QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][6].ToString()) ? "0" : tableData.Rows[i][6].ToString()),
        //                        PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString()),
        //                        DESCRIPTION = tableData.Rows[i][21].ToString(),
        //                        CREATE_BY = currentUser
        //                    };
        //                    costDataUIH.AMOUNT = costDataUIH.QUANTITY *  costDataUIH.PRICE;
        //                    UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataUIH);

        //                    //VCL
        //                    var costDataVCL = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                    costDataVCL = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                    {
        //                        PKID = Guid.NewGuid().ToString(),
        //                        ORG_CODE = orgCode,
        //                        CHI_PHI_PROFIT_CENTER_CODE = centerCodeVCL.CODE,
        //                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                        TIME_YEAR = ObjDetail.TIME_YEAR,
        //                        STATUS = Approve_Status.ChuaTrinhDuyet,
        //                        VERSION = versionNext,
        //                        KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                        QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString()),
        //                        PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString()),
        //                        DESCRIPTION = tableData.Rows[i][21].ToString(),
        //                        CREATE_BY = currentUser
        //                    };
        //                    costDataVCL.AMOUNT = costDataVCL.QUANTITY * costDataVCL.PRICE;
        //                    UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataVCL);



        //                    //PXU
        //                    var costDataPXU = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                    costDataPXU = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                    {
        //                        PKID = Guid.NewGuid().ToString(),
        //                        ORG_CODE = orgCode,
        //                        CHI_PHI_PROFIT_CENTER_CODE = centerCodePXU.CODE,
        //                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                        TIME_YEAR = ObjDetail.TIME_YEAR,
        //                        STATUS = Approve_Status.ChuaTrinhDuyet,
        //                        VERSION = versionNext,
        //                        KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                        QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString()),
        //                        PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString()),
        //                        DESCRIPTION = tableData.Rows[i][21].ToString(),
        //                        CREATE_BY = currentUser
        //                    };
        //                    costDataPXU.AMOUNT = costDataPXU.QUANTITY * costDataPXU.PRICE;
        //                    UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataPXU);





        //                    //TBB
        //                    var costDataTBB = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                    costDataTBB = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                    {
        //                        PKID = Guid.NewGuid().ToString(),
        //                        ORG_CODE = orgCode,
        //                        CHI_PHI_PROFIT_CENTER_CODE = centerCodeTBB.CODE,
        //                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                        TIME_YEAR = ObjDetail.TIME_YEAR,
        //                        STATUS = Approve_Status.ChuaTrinhDuyet,
        //                        VERSION = versionNext,
        //                        KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                        QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][9].ToString()) ? "0" : tableData.Rows[i][9].ToString()),
        //                        PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString()),
        //                        DESCRIPTION = tableData.Rows[i][21].ToString(),
        //                        CREATE_BY = currentUser
        //                    };
        //                    costDataTBB.AMOUNT = costDataTBB.QUANTITY * costDataTBB.PRICE;
        //                    UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataTBB);

        //                }
        //                else if (template.DetailKeHoachChiPhi.Any(x => x.Center?.COST_CENTER_CODE == "100004"))
        //                {
        //                    var str1 = tableData.Rows[i][2].ToString();
        //                    var str2 = tableData.Rows[i][3].ToString();
        //                    var str3 = tableData.Rows[i][4].ToString();
        //                    var str4 = tableData.Rows[i][5].ToString();
        //                    var str5 = tableData.Rows[i][6].ToString();
        //                    var str6 = tableData.Rows[i][7].ToString();
        //                    var str7 = tableData.Rows[i][8].ToString();

        //                    var str9 = tableData.Rows[i][10].ToString();
        //                    if (!decimal.TryParse(str1, out decimal value) && !string.IsNullOrEmpty(str1))
        //                    {
        //                        this.State = false;
        //                        this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                        return;
        //                    }
        //                    if (!decimal.TryParse(str2, out decimal value1) && !string.IsNullOrEmpty(str2))
        //                    {
        //                        this.State = false;
        //                        this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                        return;
        //                    }
        //                    if (!decimal.TryParse(str3, out decimal value2) && !string.IsNullOrEmpty(str3))
        //                    {
        //                        this.State = false;
        //                        this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                        return;
        //                    }
        //                    if (!decimal.TryParse(str4, out decimal value3) && !string.IsNullOrEmpty(str4))
        //                    {
        //                        this.State = false;
        //                        this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                        return;
        //                    }
        //                    if (!decimal.TryParse(str5, out decimal value4) && !string.IsNullOrEmpty(str5))
        //                    {
        //                        this.State = false;
        //                        this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                        return;
        //                    }
        //                    if (!decimal.TryParse(str6, out decimal value5) && !string.IsNullOrEmpty(str6))
        //                    {
        //                        this.State = false;
        //                        this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                        return;
        //                    }
        //                    if (!decimal.TryParse(str7, out decimal value6) && !string.IsNullOrEmpty(str7))
        //                    {
        //                        this.State = false;
        //                        this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                        return;
        //                    }
        //                    if (!decimal.TryParse(str9, out decimal value8) && !string.IsNullOrEmpty(str9))
        //                    {
        //                        this.State = false;
        //                        this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                        return;
        //                    }


        //                    var centerCodeVPCN = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VPCN" && x.COST_CENTER_CODE == "100004");
        //                    var centerCodeSGN = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "SGN" && x.COST_CENTER_CODE == "100004");
        //                    var centerCodePQC = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "PQC" && x.COST_CENTER_CODE == "100004");
        //                    var centerCodeDLI = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "DLI" && x.COST_CENTER_CODE == "100004");
        //                    var centerCodeVCA = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VCA" && x.COST_CENTER_CODE == "100004");
        //                    var centerCodeBMV = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "BMV" && x.COST_CENTER_CODE == "100004");
        //                    var centerCodeVCS = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VCS" && x.COST_CENTER_CODE == "100004");
        //                    if (centerCodeVPCN == null || centerCodeSGN == null || centerCodePQC == null || centerCodeDLI == null || centerCodeVCA == null || centerCodeBMV == null /*|| centerCodeVCS == null*/)
        //                    {
        //                        throw new Exception($"Định dạng file không đúng hoặc có lỗi hệ thống xảy ra! Vui lòng liên hệ với quản trị viên!");
        //                    }

        //                    //VPCN
        //                    var costDataVPCN = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                    costDataVPCN = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                    {
        //                        PKID = Guid.NewGuid().ToString(),
        //                        ORG_CODE = orgCode,
        //                        CHI_PHI_PROFIT_CENTER_CODE = centerCodeVPCN.CODE,
        //                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                        TIME_YEAR = ObjDetail.TIME_YEAR,
        //                        STATUS = Approve_Status.ChuaTrinhDuyet,
        //                        VERSION = versionNext,
        //                        KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                        QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][2].ToString()) ? "0" : tableData.Rows[i][2].ToString()),
        //                        PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()),
        //                        DESCRIPTION = tableData.Rows[i][19].ToString(),
        //                        CREATE_BY = currentUser
        //                    };
        //                    costDataVPCN.AMOUNT = costDataVPCN.QUANTITY *costDataVPCN.PRICE;
        //                    UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataVPCN);

        //                    //SGN
        //                    var costDataSGN = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                    costDataSGN = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                    {
        //                        PKID = Guid.NewGuid().ToString(),
        //                        ORG_CODE = orgCode,
        //                        CHI_PHI_PROFIT_CENTER_CODE = centerCodeSGN.CODE,
        //                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                        TIME_YEAR = ObjDetail.TIME_YEAR,
        //                        STATUS = Approve_Status.ChuaTrinhDuyet,
        //                        VERSION = versionNext,
        //                        KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                        QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][3].ToString()) ? "0" : tableData.Rows[i][3].ToString()),
        //                        PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()),
        //                        DESCRIPTION = tableData.Rows[i][19].ToString(),
        //                        CREATE_BY = currentUser
        //                    };
        //                    costDataSGN.AMOUNT = costDataSGN.QUANTITY * costDataSGN.PRICE;
        //                    UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataSGN);

        //                    //PQC
        //                    var costDataPQC = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                    costDataPQC = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                    {
        //                        PKID = Guid.NewGuid().ToString(),
        //                        ORG_CODE = orgCode,
        //                        CHI_PHI_PROFIT_CENTER_CODE = centerCodePQC.CODE,
        //                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                        TIME_YEAR = ObjDetail.TIME_YEAR,
        //                        STATUS = Approve_Status.ChuaTrinhDuyet,
        //                        VERSION = versionNext,
        //                        KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                        QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][4].ToString()) ? "0" : tableData.Rows[i][4].ToString()),
        //                        PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()),
        //                        DESCRIPTION = tableData.Rows[i][19].ToString(),
        //                        CREATE_BY = currentUser
        //                    };
        //                    costDataPQC.AMOUNT = costDataPQC.QUANTITY * costDataPQC.PRICE;
        //                    UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataPQC);

        //                    //DLI
        //                    var costDataDLI = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                    costDataDLI = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                    {
        //                        PKID = Guid.NewGuid().ToString(),
        //                        ORG_CODE = orgCode,
        //                        CHI_PHI_PROFIT_CENTER_CODE = centerCodeDLI.CODE,
        //                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                        TIME_YEAR = ObjDetail.TIME_YEAR,
        //                        STATUS = Approve_Status.ChuaTrinhDuyet,
        //                        VERSION = versionNext,
        //                        KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                        QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][5].ToString()) ? "0" : tableData.Rows[i][5].ToString()),
        //                        PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()),
        //                        DESCRIPTION = tableData.Rows[i][19].ToString(),
        //                        CREATE_BY = currentUser
        //                    };
        //                    costDataDLI.AMOUNT = costDataDLI.QUANTITY *  costDataDLI.PRICE;
        //                    UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataDLI);

        //                    //VCA
        //                    var costDataVCA = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                    costDataVCA = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                    {
        //                        PKID = Guid.NewGuid().ToString(),
        //                        ORG_CODE = orgCode,
        //                        CHI_PHI_PROFIT_CENTER_CODE = centerCodeVCA.CODE,
        //                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                        TIME_YEAR = ObjDetail.TIME_YEAR,
        //                        STATUS = Approve_Status.ChuaTrinhDuyet,
        //                        VERSION = versionNext,
        //                        KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                        QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][6].ToString()) ? "0" : tableData.Rows[i][6].ToString()),
        //                        PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()),
        //                        DESCRIPTION = tableData.Rows[i][19].ToString(),
        //                        CREATE_BY = currentUser
        //                    };
        //                    costDataVCA.AMOUNT = costDataVCA.QUANTITY * costDataVCA.PRICE;
        //                    UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataVCA);

        //                    //BMV
        //                    var costDataBMV = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                    costDataBMV = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                    {
        //                        PKID = Guid.NewGuid().ToString(),
        //                        ORG_CODE = orgCode,
        //                        CHI_PHI_PROFIT_CENTER_CODE = centerCodeBMV.CODE,
        //                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                        TIME_YEAR = ObjDetail.TIME_YEAR,
        //                        STATUS = Approve_Status.ChuaTrinhDuyet,
        //                        VERSION = versionNext,
        //                        KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                        QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString()),
        //                        PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()),
        //                        DESCRIPTION = tableData.Rows[i][19].ToString(),
        //                        CREATE_BY = currentUser
        //                    };
        //                    costDataBMV.AMOUNT = costDataBMV.QUANTITY * costDataBMV.PRICE;
        //                    UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataBMV);

        //                    //VCS
        //                    var costDataVCS = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                    if (centerCodeVCS != null) {
        //                        costDataVCS = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                        {
        //                            PKID = Guid.NewGuid().ToString(),
        //                            ORG_CODE = orgCode,
        //                            CHI_PHI_PROFIT_CENTER_CODE = centerCodeVCS.CODE,
        //                            TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                            TIME_YEAR = ObjDetail.TIME_YEAR,
        //                            STATUS = Approve_Status.ChuaTrinhDuyet,
        //                            VERSION = versionNext,
        //                            KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                            QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString()),
        //                            PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()),
        //                            DESCRIPTION = tableData.Rows[i][19].ToString(),
        //                            CREATE_BY = currentUser
        //                        };
        //                        costDataVCS.AMOUNT = costDataVCS.QUANTITY *  costDataVCS.PRICE;
        //                        UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataVCS);
        //                    }
        //                }
        //                else if (template.DetailKeHoachChiPhi.Any(x => x.Center?.COST_CENTER_CODE == "100005"))
        //                {

        //                    var str1 = tableData.Rows[i][2].ToString();
        //                    var str2 = tableData.Rows[i][3].ToString();
        //                    var str3 = tableData.Rows[i][4].ToString();
        //                    var str4 = tableData.Rows[i][5].ToString();

        //                    var str9 = tableData.Rows[i][7].ToString();
        //                    if (!decimal.TryParse(str1, out decimal value) && !string.IsNullOrEmpty(str1))
        //                    {
        //                        this.State = false;
        //                        this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                        return;
        //                    }
        //                    if (!decimal.TryParse(str2, out decimal value1) && !string.IsNullOrEmpty(str2))
        //                    {
        //                        this.State = false;
        //                        this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                        return;
        //                    }
        //                    if (!decimal.TryParse(str3, out decimal value2) && !string.IsNullOrEmpty(str3))
        //                    {
        //                        this.State = false;
        //                        this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                        return;
        //                    }
        //                    if (!decimal.TryParse(str4, out decimal value3) && !string.IsNullOrEmpty(str4))
        //                    {
        //                        this.State = false;
        //                        this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                        return;
        //                    }
        //                    if (!decimal.TryParse(str9, out decimal value8) && !string.IsNullOrEmpty(str9))
        //                    {
        //                        this.State = false;
        //                        this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
        //                        return;
        //                    }

        //                    var centerCodeVPCN = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VPCN" && x.COST_CENTER_CODE == "100005");
        //                    var centerCodeVTMB = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VTMB" && x.COST_CENTER_CODE == "100005");
        //                    var centerCodeVTMT = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VTMT" && x.COST_CENTER_CODE == "100005");
        //                    var centerCodeVTMN = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VTMN" && x.COST_CENTER_CODE == "100005");
        //                    if (centerCodeVPCN == null || centerCodeVTMB == null || centerCodeVTMT == null || centerCodeVTMN == null)
        //                    {
        //                        throw new Exception($"Định dạng file không đúng hoặc có lỗi hệ thống xảy ra! Vui lòng liên hệ với quản trị viên!");
        //                    }

        //                    //VPCN
        //                    var costDataVPCN = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                    costDataVPCN = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                    {
        //                        PKID = Guid.NewGuid().ToString(),
        //                        ORG_CODE = orgCode,
        //                        CHI_PHI_PROFIT_CENTER_CODE = centerCodeVPCN.CODE,
        //                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                        TIME_YEAR = ObjDetail.TIME_YEAR,
        //                        STATUS = Approve_Status.ChuaTrinhDuyet,
        //                        VERSION = versionNext,
        //                        KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                        QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][2].ToString()) ? "0" : tableData.Rows[i][2].ToString()),
        //                        PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString()),
        //                        DESCRIPTION = tableData.Rows[i][13].ToString(),
        //                        CREATE_BY = currentUser
        //                    };
        //                    costDataVPCN.AMOUNT = costDataVPCN.QUANTITY * costDataVPCN.PRICE;
        //                    UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataVPCN);

        //                    //VTMB
        //                    var costDataVTMB = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                    costDataVTMB = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                    {
        //                        PKID = Guid.NewGuid().ToString(),
        //                        ORG_CODE = orgCode,
        //                        CHI_PHI_PROFIT_CENTER_CODE = centerCodeVTMB.CODE,
        //                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                        TIME_YEAR = ObjDetail.TIME_YEAR,
        //                        STATUS = Approve_Status.ChuaTrinhDuyet,
        //                        VERSION = versionNext,
        //                        KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                        QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][3].ToString()) ? "0" : tableData.Rows[i][3].ToString()),
        //                        PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString()),
        //                        DESCRIPTION = tableData.Rows[i][13].ToString(),
        //                        CREATE_BY = currentUser
        //                    };
        //                    costDataVTMB.AMOUNT = costDataVTMB.QUANTITY *  costDataVTMB.PRICE;
        //                    UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataVTMB);

        //                    //VTMT
        //                    var costDataVTMT = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                    costDataVTMT = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                    {
        //                        PKID = Guid.NewGuid().ToString(),
        //                        ORG_CODE = orgCode,
        //                        CHI_PHI_PROFIT_CENTER_CODE = centerCodeVTMT.CODE,
        //                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                        TIME_YEAR = ObjDetail.TIME_YEAR,
        //                        STATUS = Approve_Status.ChuaTrinhDuyet,
        //                        VERSION = versionNext,
        //                        KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                        QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][4].ToString()) ? "0" : tableData.Rows[i][4].ToString()),
        //                        PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString()),
        //                        DESCRIPTION = tableData.Rows[i][13].ToString(),
        //                        CREATE_BY = currentUser
        //                    };
        //                    costDataVTMT.AMOUNT = costDataVTMT.QUANTITY *  costDataVTMT.PRICE;
        //                    UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataVTMT);

        //                    //VTMN
        //                    var costDataVTMN = new T_BP_KE_HOACH_CHI_PHI_DATA();
        //                    costDataVTMN = new T_BP_KE_HOACH_CHI_PHI_DATA()
        //                    {
        //                        PKID = Guid.NewGuid().ToString(),
        //                        ORG_CODE = orgCode,
        //                        CHI_PHI_PROFIT_CENTER_CODE = centerCodeVTMN.CODE,
        //                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
        //                        TIME_YEAR = ObjDetail.TIME_YEAR,
        //                        STATUS = Approve_Status.ChuaTrinhDuyet,
        //                        VERSION = versionNext,
        //                        KHOAN_MUC_HANG_HOA_CODE = elementCode,
        //                        QUANTITY = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][5].ToString()) ? "0" : tableData.Rows[i][5].ToString()),
        //                        PRICE = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString()),
        //                        DESCRIPTION = tableData.Rows[i][13].ToString(),
        //                        CREATE_BY = currentUser
        //                    };
        //                    costDataVTMN.AMOUNT = costDataVTMN.QUANTITY *  costDataVTMN.PRICE;
        //                    UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(costDataVTMN);
        //                }
        //                else
        //                {
        //                    throw new Exception($"Định dạng file không đúng hoặc có lỗi hệ thống xảy ra! Vui lòng liên hệ với quản trị viên!");
        //                }
        //            }

        //        }
        //        UnitOfWork.Commit();
        //        NotifyUtilities.CreateNotify(
        //            new NotifyPara()
        //            {
        //                Activity = Activity.AC_NHAP_DU_LIEU,
        //                OrgCode = orgCode,
        //                TemplateCode = ObjDetail.TEMPLATE_CODE,
        //                TimeYear = ObjDetail.TIME_YEAR,
        //                ModulType = ModulType.KeHoachChiPhi,
        //                UserSent = currentUser
        //            });
        //    }
        //    catch (Exception ex)
        //    {
        //        UnitOfWork.Rollback();
        //        State = false;
        //        Exception = ex;
        //    }
        //}
        #endregion
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

            var template = UnitOfWork.Repository<TemplateRepo>().Get(ObjDetail.TEMPLATE_CODE);

            // gọi dl chung
            var dataHeaderDoanhThu = UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == ObjDetail.TIME_YEAR && x.PHIEN_BAN == "PB1" && x.KICH_BAN == "TB" && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
            var dataInHeader = UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.TIME_YEAR == ObjDetail.TIME_YEAR && dataHeaderDoanhThu.Contains(x.TEMPLATE_CODE)).ToList();
            var dataDetails = dataInHeader.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010" || x.KHOAN_MUC_SAN_LUONG_CODE == "10020").ToList();
            var DLCHUN = UnitOfWork.Repository<SharedDataRepo>().GetAll().ToList();
            var DMDNMB = DLCHUN.FirstOrDefault(x => x.CODE == "ĐMDN_CNMB").VALUE;
            var DMNAPXA_CNMB_HAN = DLCHUN.FirstOrDefault(x => x.CODE == "DMNAPXA_CNMB_HAN").VALUE;
            var DMNAPXA_CNMB_TDH = DLCHUN.FirstOrDefault(x => x.CODE == "DMNAPXA_CNMB_TDH").VALUE;
            var DMNAPXA_CNMB_VII = DLCHUN.FirstOrDefault(x => x.CODE == "DMNAPXA_CNMB_VII").VALUE;
            var DMNAPXA_CNMB_VDH = DLCHUN.FirstOrDefault(x => x.CODE == "DMNAPXA_CNMB_VDH").VALUE;
            var DMNAPXA_CNMB_VDO = DLCHUN.FirstOrDefault(x => x.CODE == "DMNAPXA_CNMB_VDO").VALUE;
            var DMNAPXA_CNMB_HPH = DLCHUN.FirstOrDefault(x => x.CODE == "DMNAPXA_CNMB_HPH").VALUE;
            var datatcnl = UnitOfWork.Repository<CptcnlDataRepo>().Queryable().Where(x => x.YEAR == ObjDetail.TIME_YEAR).ToList();
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
                //dataCurrent = UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.ORG_CODE == orgCode
                //    && x.TIME_YEAR == ObjDetail.TIME_YEAR && x.TEMPLATE_CODE == ObjDetail.TEMPLATE_CODE).ToList();

                var strSQL = "DELETE T_BP_KE_HOACH_CHI_PHI_DATA WHERE ORG_CODE = :ORG_CODE and TIME_YEAR=:TIME_YEAR and TEMPLATE_CODE=:TEMPLATE_CODE ";
                UnitOfWork.GetSession().CreateSQLQuery(strSQL).SetParameter("ORG_CODE", orgCode)
                                                               .SetParameter("TIME_YEAR", ObjDetail.TIME_YEAR)
                                                               .SetParameter("TEMPLATE_CODE", ObjDetail.TEMPLATE_CODE)
                                                               .ExecuteUpdate();
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

              
                //foreach (var item in dataCurrent)
                //{
                //    UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Delete(item);
                //}
                UnitOfWork.Commit();

                //Insert data vào history
                SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["SMO_MSSQL_Connection"].ConnectionString);
                objConn.Open();

                string tableNamehistory = "T_BP_KE_HOACH_CHI_PHI_DATA_HISTORY";
                SqlDataAdapter daAdapterhistory = new SqlDataAdapter("SELECT * FROM " + tableNamehistory, objConn);
                daAdapterhistory.UpdateBatchSize = 500;
                DataSet dataSethistory = new DataSet(tableNamehistory);
                daAdapterhistory.FillSchema(dataSethistory, SchemaType.Source, tableNamehistory);
                daAdapterhistory.Fill(dataSethistory, tableNamehistory);
                DataTable dataTablehistory;
                dataTablehistory = dataSethistory.Tables[tableNamehistory];
                var allChiPhiProfitCenters = UnitOfWork.Repository<ChiPhiProfitCenterRepo>().GetAll();

                foreach (var item in dataCurrent)
                {

                    var revenueDataHis = (T_BP_KE_HOACH_CHI_PHI_DATA_HISTORY)item;
                    DataRow newRow = dataTablehistory.NewRow();
                    newRow["PKID"] = revenueDataHis.PKID;
                    newRow["ORG_CODE"] = revenueDataHis.ORG_CODE;
                    newRow["CHI_PHI_PROFIT_CENTER_CODE"] = revenueDataHis.CHI_PHI_PROFIT_CENTER_CODE;
                    newRow["TEMPLATE_CODE"] = revenueDataHis.TEMPLATE_CODE;
                    newRow["KHOAN_MUC_HANG_HOA_CODE"] = revenueDataHis.KHOAN_MUC_HANG_HOA_CODE; 
                    newRow["VERSION"] = revenueDataHis.VERSION;
                    newRow["TIME_YEAR"] = revenueDataHis.TIME_YEAR;
                    newRow["QUANTITY"] = revenueDataHis.QUANTITY ??  0;
                    newRow["PRICE"] = revenueDataHis.PRICE?? 0;
                    newRow["AMOUNT"] = revenueDataHis.AMOUNT?? 0;
                    newRow["QUY_MO"] = revenueDataHis.QUY_MO;
                    newRow["STATUS"] = revenueDataHis.STATUS;

                    newRow["DESCRIPTION"] = revenueDataHis.DESCRIPTION;
                    newRow["QUANTITY_TD"] = revenueDataHis.QUANTITY_TD ?? 0;
                    newRow["PRICE_TD"] = revenueDataHis.PRICE_TD ?? 0;
                    newRow["AMOUNT_TD"] = revenueDataHis.AMOUNT_TD ?? 0;
                    newRow["DESCRIPTION_TD"] = revenueDataHis.DESCRIPTION_TD;
                    dataTablehistory.Rows.Add(newRow);

               

                }
                SqlCommandBuilder objCommandBuilder = new SqlCommandBuilder(daAdapterhistory);
                daAdapterhistory.Update(dataSethistory, tableNamehistory);
           

                

               
                DataTable tableKHCP = new DataTable();
                tableKHCP.TableName = "DataInsertChiPhi";
                tableKHCP.Columns.Add("PKID", typeof(string));
                tableKHCP.Columns.Add("ORG_CODE", typeof(string));
                tableKHCP.Columns.Add("CHI_PHI_PROFIT_CENTER_CODE", typeof(string));
                tableKHCP.Columns.Add("TEMPLATE_CODE", typeof(string));
                tableKHCP.Columns.Add("TIME_YEAR", typeof(int));
                tableKHCP.Columns.Add("MONTH", typeof(int));
                tableKHCP.Columns.Add("STATUS", typeof(string));
                tableKHCP.Columns.Add("VERSION", typeof(int));
                tableKHCP.Columns.Add("KHOAN_MUC_HANG_HOA_CODE", typeof(string));
                tableKHCP.Columns.Add("QUANTITY", typeof(decimal));
                tableKHCP.Columns.Add("PRICE", typeof(decimal));
                tableKHCP.Columns.Add("DESCRIPTION", typeof(string));
                tableKHCP.Columns.Add("CREATE_BY", typeof(string));
                tableKHCP.Columns.Add("AMOUNT", typeof(decimal));

                #region insert data

                for (int i = 8; i < actualRows; i++)
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

                    var elementCode = tableData.Rows[i][0].ToString().Trim();

                    //Check mã khoản mục có phải mã cha không? Nếu có thì bỏ qua.
                    if (template.DetailKeHoachChiPhi.FirstOrDefault(x => x.ELEMENT_CODE == elementCode) == null)
                    {
                        continue;
                    }



                    // Rẽ nhánh vào phiên bản bổ sung
                    if (ObjDetail.PHIEN_BAN == "PB4")
                    {
                        for (int month = 1; month <= 12; month++)
                        {
                            if (template.DetailKeHoachChiPhi.Any(x => x.Center.COST_CENTER_CODE == "100001"))
                            {

                                var str1 = tableData.Rows[i][2].ToString();
                                var str3 = tableData.Rows[i][4].ToString();
                                var str4 = tableData.Rows[i][5].ToString();
                                var str5 = tableData.Rows[i][6].ToString();
                                var str6 = tableData.Rows[i][3].ToString();

                                var str2 = tableData.Rows[i][8].ToString();
                                if (!decimal.TryParse(str1, out decimal value) && !string.IsNullOrEmpty(str1))
                                {
                                    this.State = false;
                                    this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                    return;
                                }
                                if (!decimal.TryParse(str3, out decimal value1) && !string.IsNullOrEmpty(str3))
                                {
                                    this.State = false;
                                    this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                    return;
                                }
                                if (!decimal.TryParse(str4, out decimal value2) && !string.IsNullOrEmpty(str4))
                                {
                                    this.State = false;
                                    this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                    return;
                                }
                                if (!decimal.TryParse(str5, out decimal value3) && !string.IsNullOrEmpty(str5))
                                {
                                    this.State = false;
                                    this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                    return;
                                }
                                if (!decimal.TryParse(str6, out decimal value4) && !string.IsNullOrEmpty(str6))
                                {
                                    this.State = false;
                                    this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                    return;
                                }
                                if (!decimal.TryParse(str2, out decimal value5) && !string.IsNullOrEmpty(str2))
                                {
                                    this.State = false;
                                    this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                    return;
                                }
                                var centerCodeVPCT = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VPCT" && x.COST_CENTER_CODE == "100001");
                                var centerCodeMB = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "MB" && x.COST_CENTER_CODE == "100001");
                                var centerCodeMT = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "MT" && x.COST_CENTER_CODE == "100001");
                                var centerCodeCR = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "CR" && x.COST_CENTER_CODE == "100001");
                                var centerCodeMN = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "MN" && x.COST_CENTER_CODE == "100001");

                                if (centerCodeVPCT == null || centerCodeMB == null || centerCodeMT == null || centerCodeCR == null || centerCodeMN == null)
                                {
                                    throw new Exception($"Định dạng file không đúng hoặc có lỗi hệ thống xảy ra! Vui lòng liên hệ với quản trị viên!");
                                }
                                var row = tableKHCP.NewRow();


                                row["PKID"] = Guid.NewGuid().ToString();
                                row["ORG_CODE"] = orgCode;
                                row["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeVPCT.CODE;
                                row["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                                row["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                                row["MONTH"] = month;
                                row["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                                row["VERSION"] = versionNext;
                                row["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                                row["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][2].ToString()) ? "0" : tableData.Rows[i][2].ToString());
                                row["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString());
                                row["DESCRIPTION"] = tableData.Rows[i][15].ToString();
                                row["CREATE_BY"] = currentUser;
                                row["AMOUNT"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][2].ToString()) ? "0" : tableData.Rows[i][2].ToString()) * Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString());

                                tableKHCP.Rows.Add(row);
                                //MB
                                var row2 = tableKHCP.NewRow();
                                row2["PKID"] = Guid.NewGuid().ToString();
                                row2["ORG_CODE"] = orgCode;
                                row["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeMB.CODE;
                                row2["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                                row2["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                                row2["MONTH"] = month;
                                row2["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                                row2["VERSION"] = versionNext;
                                row2["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                                row2["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][3].ToString()) ? "0" : tableData.Rows[i][3].ToString());
                                row2["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString());
                                row2["DESCRIPTION"] = tableData.Rows[i][15].ToString();
                                row2["CREATE_BY"] = currentUser;
                                row2["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][3].ToString()) ? "0" : tableData.Rows[i][3].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString()));
                                tableKHCP.Rows.Add(row2);

                                //MT
                                var row3 = tableKHCP.NewRow();
                                row3["PKID"] = Guid.NewGuid().ToString();
                                row3["ORG_CODE"] = orgCode;
                                row3["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeMT.CODE;
                                row3["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                                row3["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                                row3["MONTH"] = month;
                                row3["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                                row3["VERSION"] = versionNext;
                                row3["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                                row3["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][4].ToString()) ? "0" : tableData.Rows[i][4].ToString());
                                row3["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString());
                                row3["DESCRIPTION"] = tableData.Rows[i][15].ToString();
                                row3["CREATE_BY"] = currentUser;
                                row3["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][4].ToString()) ? "0" : tableData.Rows[i][4].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString()));
                                tableKHCP.Rows.Add(row3);


                                //CR

                                var row4 = tableKHCP.NewRow();
                                row4["PKID"] = Guid.NewGuid().ToString();
                                row4["ORG_CODE"] = orgCode;
                                row4["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeCR.CODE;
                                row4["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                                row4["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                                row4["MONTH"] = month;
                                row4["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                                row4["VERSION"] = versionNext;
                                row4["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                                row4["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][5].ToString()) ? "0" : tableData.Rows[i][5].ToString());
                                row4["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString());
                                row4["DESCRIPTION"] = tableData.Rows[i][15].ToString();
                                row4["CREATE_BY"] = currentUser;
                                row4["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][5].ToString()) ? "0" : tableData.Rows[i][5].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString()));
                                tableKHCP.Rows.Add(row4);


                                //MN
                                var row5 = tableKHCP.NewRow();
                                row5["PKID"] = Guid.NewGuid().ToString();
                                row5["ORG_CODE"] = orgCode;
                                row5["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeMN.CODE;
                                row5["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                                row5["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                                row5["MONTH"] = month;
                                row5["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                                row5["VERSION"] = versionNext;
                                row5["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                                row5["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][6].ToString()) ? "0" : tableData.Rows[i][6].ToString());
                                row5["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString());
                                row5["DESCRIPTION"] = tableData.Rows[i][15].ToString();
                                row5["CREATE_BY"] = currentUser;
                                row5["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][6].ToString()) ? "0" : tableData.Rows[i][6].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString()));
                                tableKHCP.Rows.Add(row5);

                            }
                            else if (template.DetailKeHoachChiPhi.Any(x => x.Center.COST_CENTER_CODE == "100002"))
                            {
                                var str1 = tableData.Rows[i][2].ToString();
                                var str2 = tableData.Rows[i][3].ToString();
                                var str3 = tableData.Rows[i][4].ToString();
                                var str4 = tableData.Rows[i][5].ToString();
                                var str5 = tableData.Rows[i][6].ToString();
                                var str6 = tableData.Rows[i][7].ToString();
                                var str7 = tableData.Rows[i][8].ToString();

                                var str8 = tableData.Rows[i][10].ToString();
                                if (!decimal.TryParse(str1, out decimal value) && !string.IsNullOrEmpty(str1))
                                {
                                    this.State = false;
                                    this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                    return;
                                }
                                if (!decimal.TryParse(str2, out decimal value1) && !string.IsNullOrEmpty(str2))
                                {
                                    this.State = false;
                                    this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                    return;
                                }
                                if (!decimal.TryParse(str3, out decimal value2) && !string.IsNullOrEmpty(str3))
                                {
                                    this.State = false;
                                    this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                    return;
                                }
                                if (!decimal.TryParse(str4, out decimal value3) && !string.IsNullOrEmpty(str4))
                                {
                                    this.State = false;
                                    this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                    return;
                                }
                                if (!decimal.TryParse(str5, out decimal value4) && !string.IsNullOrEmpty(str5))
                                {
                                    this.State = false;
                                    this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                    return;
                                }
                                if (!decimal.TryParse(str6, out decimal value5) && !string.IsNullOrEmpty(str6))
                                {
                                    this.State = false;
                                    this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                    return;
                                }
                                if (!decimal.TryParse(str7, out decimal value6) && !string.IsNullOrEmpty(str7))
                                {
                                    this.State = false;
                                    this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                    return;
                                }
                                if (!decimal.TryParse(str8, out decimal value7) && !string.IsNullOrEmpty(str8))
                                {
                                    this.State = false;
                                    this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                    return;
                                }


                                var centerCodeVPCN = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VPCN" && x.COST_CENTER_CODE == "100002");
                                var centerCodeHAN = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "HAN" && x.COST_CENTER_CODE == "100002");
                                var centerCodeHPH = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "HPH" && x.COST_CENTER_CODE == "100002");
                                var centerCodeTHD = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "THD" && x.COST_CENTER_CODE == "100002");
                                var centerCodeVII = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VII" && x.COST_CENTER_CODE == "100002");
                                var centerCodeVDH = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VDH" && x.COST_CENTER_CODE == "100002");
                                var centerCodeVDO = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VDO" && x.COST_CENTER_CODE == "100002");

                                if (centerCodeVPCN == null || centerCodeHAN == null || centerCodeHPH == null || centerCodeTHD == null || centerCodeVII == null || centerCodeVDH == null || centerCodeVDO == null)
                                {
                                    throw new Exception($"Định dạng file không đúng hoặc có lỗi hệ thống xảy ra! Vui lòng liên hệ với quản trị viên!");
                                }

                                //VPCN
                                var row1 = tableKHCP.NewRow();
                                row1["PKID"] = Guid.NewGuid().ToString();
                                row1["ORG_CODE"] = orgCode;
                                row1["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeVPCN.CODE;
                                row1["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                                row1["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                                row1["MONTH"] = month;
                                row1["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                                row1["VERSION"] = versionNext;
                                row1["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                                row1["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][2].ToString()) ? "0" : tableData.Rows[i][2].ToString());
                                row1["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                                row1["DESCRIPTION"] = tableData.Rows[i][15].ToString();
                                row1["CREATE_BY"] = currentUser;
                                row1["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][2].ToString()) ? "0" : tableData.Rows[i][2].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()));
                                tableKHCP.Rows.Add(row1);



                                //HAN
                                var row2 = tableKHCP.NewRow();
                                row2["PKID"] = Guid.NewGuid().ToString();
                                row2["ORG_CODE"] = orgCode;
                                row2["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeHAN.CODE;
                                row2["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                                row2["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                                row2["MONTH"] = month;
                                row2["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                                row2["VERSION"] = versionNext;
                                row2["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                                row2["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][3].ToString()) ? "0" : tableData.Rows[i][3].ToString());
                                row2["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                                row2["DESCRIPTION"] = tableData.Rows[i][19].ToString();
                                row2["CREATE_BY"] = currentUser;
                                row2["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][3].ToString()) ? "0" : tableData.Rows[i][3].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()));
                                tableKHCP.Rows.Add(row2);


                                //VDO
                                var row3 = tableKHCP.NewRow();
                                row3["PKID"] = Guid.NewGuid().ToString();
                                row3["ORG_CODE"] = orgCode;
                                row3["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeHAN.CODE;
                                row3["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                                row3["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                                row3["MONTH"] = month;
                                row3["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                                row3["VERSION"] = versionNext;
                                row3["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                                row3["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][4].ToString()) ? "0" : tableData.Rows[i][4].ToString());
                                row3["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                                row3["DESCRIPTION"] = tableData.Rows[i][19].ToString();
                                row3["CREATE_BY"] = currentUser;
                                row3["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][4].ToString()) ? "0" : tableData.Rows[i][4].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()));
                                tableKHCP.Rows.Add(row3);


                                //HPH
                                var row4 = tableKHCP.NewRow();
                                row4["PKID"] = Guid.NewGuid().ToString();
                                row4["ORG_CODE"] = orgCode;
                                row4["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeTHD.CODE;
                                row4["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                                row4["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                                row4["MONTH"] = month;
                                row4["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                                row4["VERSION"] = versionNext;
                                row4["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                                row4["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][5].ToString()) ? "0" : tableData.Rows[i][5].ToString());
                                row4["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                                row4["DESCRIPTION"] = tableData.Rows[i][19].ToString();
                                row4["CREATE_BY"] = currentUser;
                                row4["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][5].ToString()) ? "0" : tableData.Rows[i][5].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()));
                                tableKHCP.Rows.Add(row4);


                                //DIN
                                var row5 = tableKHCP.NewRow();
                                row5["PKID"] = Guid.NewGuid().ToString();
                                row5["ORG_CODE"] = orgCode;
                                row5["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeVII.CODE;
                                row5["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                                row5["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                                row5["MONTH"] = month;
                                row5["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                                row5["VERSION"] = versionNext;
                                row5["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                                row5["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][6].ToString()) ? "0" : tableData.Rows[i][6].ToString());
                                row5["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                                row5["DESCRIPTION"] = tableData.Rows[i][19].ToString();
                                row5["CREATE_BY"] = currentUser;
                                row5["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][6].ToString()) ? "0" : tableData.Rows[i][6].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()));
                                tableKHCP.Rows.Add(row5);


                                //THD

                                var row6 = tableKHCP.NewRow();
                                row6["PKID"] = Guid.NewGuid().ToString();
                                row6["ORG_CODE"] = orgCode;
                                row6["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeVDH.CODE;
                                row6["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                                row6["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                                row6["MONTH"] = month;
                                row6["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                                row6["VERSION"] = versionNext;
                                row6["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                                row6["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString());
                                row6["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                                row6["DESCRIPTION"] = tableData.Rows[i][19].ToString();
                                row6["CREATE_BY"] = currentUser;
                                row6["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()));
                                tableKHCP.Rows.Add(row6);


                                //NAF
                                var row7 = tableKHCP.NewRow();
                                row7["PKID"] = Guid.NewGuid().ToString();
                                row7["ORG_CODE"] = orgCode;
                                row7["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeVDO.CODE;
                                row7["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                                row7["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                                row7["MONTH"] = month;
                                row7["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                                row7["VERSION"] = versionNext;
                                row7["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                                row7["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString());
                                row7["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                                row7["DESCRIPTION"] = tableData.Rows[i][19].ToString();
                                row7["CREATE_BY"] = currentUser;
                                row7["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()));
                                tableKHCP.Rows.Add(row7);


                            }
                            else if (template.DetailKeHoachChiPhi.Any(x => x.Center.COST_CENTER_CODE == "100003"))
                            {
                                var str1 = tableData.Rows[i][2].ToString();
                                var str2 = tableData.Rows[i][3].ToString();
                                var str3 = tableData.Rows[i][4].ToString();
                                var str4 = tableData.Rows[i][5].ToString();
                                var str5 = tableData.Rows[i][6].ToString();
                                var str6 = tableData.Rows[i][7].ToString();
                                var str7 = tableData.Rows[i][8].ToString();
                                var str8 = tableData.Rows[i][9].ToString();

                                var str9 = tableData.Rows[i][11].ToString();
                                if (!decimal.TryParse(str1, out decimal value) && !string.IsNullOrEmpty(str1))
                                {
                                    this.State = false;
                                    this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                    return;
                                }
                                if (!decimal.TryParse(str2, out decimal value1) && !string.IsNullOrEmpty(str2))
                                {
                                    this.State = false;
                                    this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                    return;
                                }
                                if (!decimal.TryParse(str3, out decimal value2) && !string.IsNullOrEmpty(str3))
                                {
                                    this.State = false;
                                    this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                    return;
                                }
                                if (!decimal.TryParse(str4, out decimal value3) && !string.IsNullOrEmpty(str4))
                                {
                                    this.State = false;
                                    this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                    return;
                                }
                                if (!decimal.TryParse(str5, out decimal value4) && !string.IsNullOrEmpty(str5))
                                {
                                    this.State = false;
                                    this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                    return;
                                }
                                if (!decimal.TryParse(str6, out decimal value5) && !string.IsNullOrEmpty(str6))
                                {
                                    this.State = false;
                                    this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                    return;
                                }
                                if (!decimal.TryParse(str7, out decimal value6) && !string.IsNullOrEmpty(str7))
                                {
                                    this.State = false;
                                    this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                    return;
                                }
                                if (!decimal.TryParse(str8, out decimal value7) && !string.IsNullOrEmpty(str8))
                                {
                                    this.State = false;
                                    this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                    return;
                                }
                                if (!decimal.TryParse(str9, out decimal value8) && !string.IsNullOrEmpty(str9))
                                {
                                    this.State = false;
                                    this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                    return;
                                }

                                var centerCodeVPCN = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VPCN" && x.COST_CENTER_CODE == "100003");
                                var centerCodeDAD = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "DAD" && x.COST_CENTER_CODE == "100003");
                                var centerCodeCXR = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "CXR" && x.COST_CENTER_CODE == "100003");
                                var centerCodeHUI = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "HUI" && x.COST_CENTER_CODE == "100003");
                                var centerCodeUIH = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "UIH" && x.COST_CENTER_CODE == "100003");
                                var centerCodeVCL = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VCL" && x.COST_CENTER_CODE == "100003");
                                var centerCodePXU = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "PXU" && x.COST_CENTER_CODE == "100003");
                                var centerCodeTBB = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "TBB" && x.COST_CENTER_CODE == "100003");
                                if (centerCodeVPCN == null || centerCodeDAD == null || centerCodeCXR == null || centerCodeHUI == null || centerCodeUIH == null || centerCodeVCL == null || centerCodePXU == null || centerCodeTBB == null)
                                {
                                    throw new Exception($"Định dạng file không đúng hoặc có lỗi hệ thống xảy ra! Vui lòng liên hệ với quản trị viên!");
                                }

                                //VPCN
                                var row1 = tableKHCP.NewRow();
                                row1["PKID"] = Guid.NewGuid().ToString();
                                row1["ORG_CODE"] = orgCode;
                                row1["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeVPCN.CODE;
                                row1["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                                row1["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                                row1["MONTH"] = month;
                                row1["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                                row1["VERSION"] = versionNext;
                                row1["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                                row1["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][2].ToString()) ? "0" : tableData.Rows[i][2].ToString());
                                row1["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString());
                                row1["DESCRIPTION"] = tableData.Rows[i][21].ToString();
                                row1["CREATE_BY"] = currentUser;
                                row1["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][2].ToString()) ? "0" : tableData.Rows[i][2].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString()));
                                tableKHCP.Rows.Add(row1);


                                //DAD
                                var row2 = tableKHCP.NewRow();
                                row2["PKID"] = Guid.NewGuid().ToString();
                                row2["ORG_CODE"] = orgCode;
                                row2["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeDAD.CODE;
                                row2["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                                row2["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                                row2["MONTH"] = month;
                                row2["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                                row2["VERSION"] = versionNext;
                                row2["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                                row2["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][3].ToString()) ? "0" : tableData.Rows[i][3].ToString());
                                row2["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString());
                                row2["DESCRIPTION"] = tableData.Rows[i][21].ToString();
                                row2["CREATE_BY"] = currentUser;
                                row2["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][3].ToString()) ? "0" : tableData.Rows[i][3].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString()));
                                tableKHCP.Rows.Add(row2);


                                //CXR
                                var row3 = tableKHCP.NewRow();
                                row3["PKID"] = Guid.NewGuid().ToString();
                                row3["ORG_CODE"] = orgCode;
                                row3["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeCXR.CODE;
                                row3["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                                row3["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                                row3["MONTH"] = month;
                                row3["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                                row3["VERSION"] = versionNext;
                                row3["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                                row3["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][4].ToString()) ? "0" : tableData.Rows[i][4].ToString());
                                row3["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString());
                                row3["DESCRIPTION"] = tableData.Rows[i][21].ToString();
                                row3["CREATE_BY"] = currentUser;
                                row3["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][4].ToString()) ? "0" : tableData.Rows[i][4].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString()));
                                tableKHCP.Rows.Add(row3);

                                //HUI
                                var row4 = tableKHCP.NewRow();
                                row4["PKID"] = Guid.NewGuid().ToString();
                                row4["ORG_CODE"] = orgCode;
                                row4["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeHUI.CODE;
                                row4["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                                row4["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                                row4["MONTH"] = month;
                                row4["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                                row4["VERSION"] = versionNext;
                                row4["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                                row4["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][5].ToString()) ? "0" : tableData.Rows[i][5].ToString());
                                row4["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString());
                                row4["DESCRIPTION"] = tableData.Rows[i][21].ToString();
                                row4["CREATE_BY"] = currentUser;
                                row4["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][5].ToString()) ? "0" : tableData.Rows[i][5].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString()));
                                tableKHCP.Rows.Add(row4);


                                //UIH
                                var row5 = tableKHCP.NewRow();
                                row5["PKID"] = Guid.NewGuid().ToString();
                                row5["ORG_CODE"] = orgCode;
                                row5["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeUIH.CODE;
                                row5["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                                row5["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                                row5["MONTH"] = month;
                                row5["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                                row5["VERSION"] = versionNext;
                                row5["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                                row5["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][6].ToString()) ? "0" : tableData.Rows[i][6].ToString());
                                row5["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString());
                                row5["DESCRIPTION"] = tableData.Rows[i][21].ToString();
                                row5["CREATE_BY"] = currentUser;
                                row5["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][6].ToString()) ? "0" : tableData.Rows[i][6].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString()));
                                tableKHCP.Rows.Add(row5);


                                //VCL
                                var row6 = tableKHCP.NewRow();
                                row6["PKID"] = Guid.NewGuid().ToString();
                                row6["ORG_CODE"] = orgCode;
                                row6["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeVCL.CODE;
                                row6["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                                row6["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                                row6["MONTH"] = month;
                                row6["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                                row6["VERSION"] = versionNext;
                                row6["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                                row6["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString());
                                row6["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString());
                                row6["DESCRIPTION"] = tableData.Rows[i][21].ToString();
                                row6["CREATE_BY"] = currentUser;
                                row6["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString()));
                                tableKHCP.Rows.Add(row6);




                                //PXU
                                var row7 = tableKHCP.NewRow();
                                row7["PKID"] = Guid.NewGuid().ToString();
                                row7["ORG_CODE"] = orgCode;
                                row7["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodePXU.CODE;
                                row7["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                                row7["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                                row7["MONTH"] = month;
                                row7["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                                row7["VERSION"] = versionNext;
                                row7["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                                row7["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString());
                                row7["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString());
                                row7["DESCRIPTION"] = tableData.Rows[i][21].ToString();
                                row7["CREATE_BY"] = currentUser;
                                row7["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString()));
                                tableKHCP.Rows.Add(row7);






                                //TBB
                                var row8 = tableKHCP.NewRow();
                                row8["PKID"] = Guid.NewGuid().ToString();
                                row8["ORG_CODE"] = orgCode;
                                row8["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeTBB.CODE;
                                row8["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                                row8["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                                row8["MONTH"] = month;
                                row8["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                                row8["VERSION"] = versionNext;
                                row8["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                                row8["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][9].ToString()) ? "0" : tableData.Rows[i][9].ToString());
                                row8["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString());
                                row8["DESCRIPTION"] = tableData.Rows[i][21].ToString();
                                row8["CREATE_BY"] = currentUser;
                                row8["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][9].ToString()) ? "0" : tableData.Rows[i][9].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()));
                                tableKHCP.Rows.Add(row8);


                            }
                            else if (template.DetailKeHoachChiPhi.Any(x => x.Center.COST_CENTER_CODE == "100004"))
                            {
                                var str1 = tableData.Rows[i][2].ToString();
                                var str2 = tableData.Rows[i][3].ToString();
                                var str3 = tableData.Rows[i][4].ToString();
                                var str4 = tableData.Rows[i][5].ToString();
                                var str5 = tableData.Rows[i][6].ToString();
                                var str6 = tableData.Rows[i][7].ToString();
                                var str7 = tableData.Rows[i][8].ToString();

                                var str9 = tableData.Rows[i][10].ToString();
                                if (!decimal.TryParse(str1, out decimal value) && !string.IsNullOrEmpty(str1))
                                {
                                    this.State = false;
                                    this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                    return;
                                }
                                if (!decimal.TryParse(str2, out decimal value1) && !string.IsNullOrEmpty(str2))
                                {
                                    this.State = false;
                                    this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                    return;
                                }
                                if (!decimal.TryParse(str3, out decimal value2) && !string.IsNullOrEmpty(str3))
                                {
                                    this.State = false;
                                    this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                    return;
                                }
                                if (!decimal.TryParse(str4, out decimal value3) && !string.IsNullOrEmpty(str4))
                                {
                                    this.State = false;
                                    this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                    return;
                                }
                                if (!decimal.TryParse(str5, out decimal value4) && !string.IsNullOrEmpty(str5))
                                {
                                    this.State = false;
                                    this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                    return;
                                }
                                if (!decimal.TryParse(str6, out decimal value5) && !string.IsNullOrEmpty(str6))
                                {
                                    this.State = false;
                                    this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                    return;
                                }
                                if (!decimal.TryParse(str7, out decimal value6) && !string.IsNullOrEmpty(str7))
                                {
                                    this.State = false;
                                    this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                    return;
                                }
                                if (!decimal.TryParse(str9, out decimal value8) && !string.IsNullOrEmpty(str9))
                                {
                                    this.State = false;
                                    this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                    return;
                                }

                                var centerCodeVPCN = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VPCN" && x.COST_CENTER_CODE == "100004");
                                var centerCodeSGN = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "SGN" && x.COST_CENTER_CODE == "100004");
                                var centerCodePQC = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "PQC" && x.COST_CENTER_CODE == "100004");
                                var centerCodeDLI = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "DLI" && x.COST_CENTER_CODE == "100004");
                                var centerCodeVCA = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VCA" && x.COST_CENTER_CODE == "100004");
                                var centerCodeBMV = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "BMV" && x.COST_CENTER_CODE == "100004");
                                var centerCodeVCS = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VCS" && x.COST_CENTER_CODE == "100004");
                                if (centerCodeVPCN == null || centerCodeSGN == null || centerCodePQC == null || centerCodeDLI == null || centerCodeVCA == null || centerCodeBMV == null || centerCodeVCS == null)
                                {
                                    throw new Exception($"Định dạng file không đúng hoặc có lỗi hệ thống xảy ra! Vui lòng liên hệ với quản trị viên!");
                                }

                                //VPCN
                                var row1 = tableKHCP.NewRow();
                                row1["PKID"] = Guid.NewGuid().ToString();
                                row1["ORG_CODE"] = orgCode;
                                row1["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeVPCN.CODE;
                                row1["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                                row1["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                                row1["MONTH"] = month;
                                row1["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                                row1["VERSION"] = versionNext;
                                row1["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                                row1["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][2].ToString()) ? "0" : tableData.Rows[i][2].ToString());
                                row1["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                                row1["DESCRIPTION"] = tableData.Rows[i][19].ToString();
                                row1["CREATE_BY"] = currentUser;
                                row1["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][2].ToString()) ? "0" : tableData.Rows[i][2].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()));
                                tableKHCP.Rows.Add(row1);


                                //SGN
                                var row2 = tableKHCP.NewRow();
                                row2["PKID"] = Guid.NewGuid().ToString();
                                row2["ORG_CODE"] = orgCode;
                                row2["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeSGN.CODE;
                                row2["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                                row2["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                                row2["MONTH"] = month;
                                row2["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                                row2["VERSION"] = versionNext;
                                row2["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                                row2["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][3].ToString()) ? "0" : tableData.Rows[i][3].ToString());
                                row2["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                                row2["DESCRIPTION"] = tableData.Rows[i][19].ToString();
                                row2["CREATE_BY"] = currentUser;
                                row2["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][3].ToString()) ? "0" : tableData.Rows[i][3].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()));
                                tableKHCP.Rows.Add(row2);



                                //PQC
                                var row3 = tableKHCP.NewRow();
                                row3["PKID"] = Guid.NewGuid().ToString();
                                row3["ORG_CODE"] = orgCode;
                                row3["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodePQC.CODE;
                                row3["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                                row3["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                                row3["MONTH"] = month;
                                row3["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                                row3["VERSION"] = versionNext;
                                row3["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                                row3["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][4].ToString()) ? "0" : tableData.Rows[i][4].ToString());
                                row3["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                                row3["DESCRIPTION"] = tableData.Rows[i][19].ToString();
                                row3["CREATE_BY"] = currentUser;
                                row3["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][4].ToString()) ? "0" : tableData.Rows[i][4].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()));
                                tableKHCP.Rows.Add(row3);


                                //DLI
                                var row4 = tableKHCP.NewRow();
                                row4["PKID"] = Guid.NewGuid().ToString();
                                row4["ORG_CODE"] = orgCode;
                                row4["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeDLI.CODE;
                                row4["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                                row4["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                                row4["MONTH"] = month;
                                row4["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                                row4["VERSION"] = versionNext;
                                row4["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                                row4["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][5].ToString()) ? "0" : tableData.Rows[i][5].ToString());
                                row4["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                                row4["DESCRIPTION"] = tableData.Rows[i][19].ToString();
                                row4["CREATE_BY"] = currentUser;
                                row4["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][5].ToString()) ? "0" : tableData.Rows[i][5].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()));
                                tableKHCP.Rows.Add(row4);


                                //VCA
                                var row5 = tableKHCP.NewRow();
                                row5["PKID"] = Guid.NewGuid().ToString();
                                row5["ORG_CODE"] = orgCode;
                                row5["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeDLI.CODE;
                                row5["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                                row5["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                                row5["MONTH"] = month;
                                row5["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                                row5["VERSION"] = versionNext;
                                row5["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                                row5["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][6].ToString()) ? "0" : tableData.Rows[i][6].ToString());
                                row5["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                                row5["DESCRIPTION"] = tableData.Rows[i][19].ToString();
                                row5["CREATE_BY"] = currentUser;
                                row5["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][6].ToString()) ? "0" : tableData.Rows[i][6].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()));
                                tableKHCP.Rows.Add(row5);


                                //BMV
                                var row6 = tableKHCP.NewRow();
                                row6["PKID"] = Guid.NewGuid().ToString();
                                row6["ORG_CODE"] = orgCode;
                                row6["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeBMV.CODE;
                                row6["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                                row6["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                                row6["MONTH"] = month;
                                row6["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                                row6["VERSION"] = versionNext;
                                row6["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                                row6["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString());
                                row6["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                                row6["DESCRIPTION"] = tableData.Rows[i][19].ToString();
                                row6["CREATE_BY"] = currentUser;
                                row6["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()));
                                tableKHCP.Rows.Add(row6);


                                //VCS
                                var row7 = tableKHCP.NewRow();
                                row7["PKID"] = Guid.NewGuid().ToString();
                                row7["ORG_CODE"] = orgCode;
                                row7["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeVCS.CODE;
                                row7["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                                row7["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                                row7["MONTH"] = month;
                                row7["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                                row7["VERSION"] = versionNext;
                                row7["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                                row7["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString());
                                row7["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                                row7["DESCRIPTION"] = tableData.Rows[i][19].ToString();
                                row7["CREATE_BY"] = currentUser;
                                row7["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()));
                                tableKHCP.Rows.Add(row7);


                            }
                            else if (template.DetailKeHoachChiPhi.Any(x => x.Center.COST_CENTER_CODE == "100005"))
                            {
                                var str1 = tableData.Rows[i][2].ToString();
                                var str2 = tableData.Rows[i][3].ToString();
                                var str3 = tableData.Rows[i][4].ToString();
                                var str4 = tableData.Rows[i][5].ToString();

                                var str9 = tableData.Rows[i][7].ToString();
                                if (!decimal.TryParse(str1, out decimal value) && !string.IsNullOrEmpty(str1))
                                {
                                    this.State = false;
                                    this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                    return;
                                }
                                if (!decimal.TryParse(str2, out decimal value1) && !string.IsNullOrEmpty(str2))
                                {
                                    this.State = false;
                                    this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                    return;
                                }
                                if (!decimal.TryParse(str3, out decimal value2) && !string.IsNullOrEmpty(str3))
                                {
                                    this.State = false;
                                    this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                    return;
                                }
                                if (!decimal.TryParse(str4, out decimal value3) && !string.IsNullOrEmpty(str4))
                                {
                                    this.State = false;
                                    this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                    return;
                                }
                                if (!decimal.TryParse(str9, out decimal value8) && !string.IsNullOrEmpty(str9))
                                {
                                    this.State = false;
                                    this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                    return;
                                }
                                var centerCodeVPCN = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VPCN" && x.COST_CENTER_CODE == "100005");
                                var centerCodeVTMB = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VTMB" && x.COST_CENTER_CODE == "100005");
                                var centerCodeVTMT = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VTMT" && x.COST_CENTER_CODE == "100005");
                                var centerCodeVTMN = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VTMN" && x.COST_CENTER_CODE == "100005");
                                if (centerCodeVPCN == null || centerCodeVTMB == null || centerCodeVTMT == null || centerCodeVTMN == null)
                                {
                                    throw new Exception($"Định dạng file không đúng hoặc có lỗi hệ thống xảy ra! Vui lòng liên hệ với quản trị viên!");
                                }

                                //VPCN
                                var row1 = tableKHCP.NewRow();
                                row1["PKID"] = Guid.NewGuid().ToString();
                                row1["ORG_CODE"] = orgCode;
                                row1["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeVPCN.CODE;
                                row1["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                                row1["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                                row1["MONTH"] = month;
                                row1["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                                row1["VERSION"] = versionNext;
                                row1["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                                row1["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][2].ToString()) ? "0" : tableData.Rows[i][2].ToString());
                                row1["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString());
                                row1["DESCRIPTION"] = tableData.Rows[i][13].ToString();
                                row1["CREATE_BY"] = currentUser;
                                row1["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][2].ToString()) ? "0" : tableData.Rows[i][2].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString()));
                                tableKHCP.Rows.Add(row1);


                                //VTMB
                                var row2 = tableKHCP.NewRow();
                                row2["PKID"] = Guid.NewGuid().ToString();
                                row2["ORG_CODE"] = orgCode;
                                row2["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeVTMB.CODE;
                                row2["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                                row2["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                                row2["MONTH"] = month;
                                row2["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                                row2["VERSION"] = versionNext;
                                row2["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                                row2["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][3].ToString()) ? "0" : tableData.Rows[i][2].ToString());
                                row2["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString());
                                row2["DESCRIPTION"] = tableData.Rows[i][13].ToString();
                                row2["CREATE_BY"] = currentUser;
                                row2["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][3].ToString()) ? "0" : tableData.Rows[i][2].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString()));
                                tableKHCP.Rows.Add(row2);


                                //VTMT
                                var row3 = tableKHCP.NewRow();
                                row3["PKID"] = Guid.NewGuid().ToString();
                                row3["ORG_CODE"] = orgCode;
                                row3["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeVTMT.CODE;
                                row3["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                                row3["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                                row3["MONTH"] = month;
                                row3["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                                row3["VERSION"] = versionNext;
                                row3["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                                row3["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][4].ToString()) ? "0" : tableData.Rows[i][4].ToString());
                                row3["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString());
                                row3["DESCRIPTION"] = tableData.Rows[i][13].ToString();
                                row3["CREATE_BY"] = currentUser;
                                row3["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][3].ToString()) ? "0" : tableData.Rows[i][2].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString()));
                                tableKHCP.Rows.Add(row3);


                                //VTMN
                                var row4 = tableKHCP.NewRow();
                                row4["PKID"] = Guid.NewGuid().ToString();
                                row4["ORG_CODE"] = orgCode;
                                row4["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeVTMN.CODE;
                                row4["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                                row4["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                                row4["MONTH"] = month;
                                row4["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                                row4["VERSION"] = versionNext;
                                row4["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                                row4["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][5].ToString()) ? "0" : tableData.Rows[i][5].ToString());
                                row4["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString());
                                row4["DESCRIPTION"] = tableData.Rows[i][13].ToString();
                                row4["CREATE_BY"] = currentUser;
                                row4["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][5].ToString()) ? "0" : tableData.Rows[i][5].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString()));
                                tableKHCP.Rows.Add(row4);

                            }
                            else
                            {
                                throw new Exception($"Định dạng file không đúng hoặc có lỗi hệ thống xảy ra! Vui lòng liên hệ với quản trị viên!");
                            }
                        }
                    }

                    else
                    {
                        //Rẽ nhánh cho các template khác nhau của 4 chi nhánh

                        if (template.DetailKeHoachChiPhi.Any(x => x.Center?.COST_CENTER_CODE == "100001"))
                        {
                            var str1 = tableData.Rows[i][2].ToString();
                            var str3 = tableData.Rows[i][4].ToString();
                            var str4 = tableData.Rows[i][5].ToString();
                            var str5 = tableData.Rows[i][6].ToString();
                            var str6 = tableData.Rows[i][3].ToString();

                            var str2 = tableData.Rows[i][8].ToString();
                            if (!decimal.TryParse(str1, out decimal value) && !string.IsNullOrEmpty(str1))
                            {
                                this.State = false;
                                this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                return;
                            }
                            if (!decimal.TryParse(str3, out decimal value1) && !string.IsNullOrEmpty(str3))
                            {
                                this.State = false;
                                this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                return;
                            }
                            if (!decimal.TryParse(str4, out decimal value2) && !string.IsNullOrEmpty(str4))
                            {
                                this.State = false;
                                this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                return;
                            }
                            if (!decimal.TryParse(str5, out decimal value3) && !string.IsNullOrEmpty(str5))
                            {
                                this.State = false;
                                this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                return;
                            }
                            if (!decimal.TryParse(str6, out decimal value4) && !string.IsNullOrEmpty(str6))
                            {
                                this.State = false;
                                this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                return;
                            }
                            if (!decimal.TryParse(str2, out decimal value5) && !string.IsNullOrEmpty(str2))
                            {
                                this.State = false;
                                this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                return;
                            }

                            var centerCodeVPCT = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VPCT" && x.COST_CENTER_CODE == "100001");
                            var centerCodeMB = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "MB" && x.COST_CENTER_CODE == "100001");
                            var centerCodeMT = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "MT" && x.COST_CENTER_CODE == "100001");
                            var centerCodeCR = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "CR" && x.COST_CENTER_CODE == "100001");
                            var centerCodeMN = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "MN" && x.COST_CENTER_CODE == "100001");

                            if (centerCodeVPCT == null || centerCodeMB == null || centerCodeMT == null || centerCodeCR == null || centerCodeMN == null)
                            {
                                throw new Exception($"Định dạng file không đúng hoặc có lỗi hệ thống xảy ra! Vui lòng liên hệ với quản trị viên!");
                            }

                            //VPNC
                            var row1 = tableKHCP.NewRow();
                            row1["PKID"] = Guid.NewGuid().ToString();
                            row1["ORG_CODE"] = orgCode;
                            row1["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeVPCT.CODE;
                            row1["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                            row1["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                            row1["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                            row1["VERSION"] = versionNext;
                            row1["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                            row1["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][3].ToString()) ? "0" : tableData.Rows[i][3].ToString());
                           
                            switch (elementCode)
                            {
                                case "CQ62711A":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 4).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ62711B":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 6).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ62711C":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 7).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ62711D":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 8).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ62711E":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 9).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ62711G":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 10).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ62711F":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 11).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ6272B007A01":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 13).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ6278H002A01":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 15).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ6278H008A01":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 18).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ6278H006A01":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 16).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ6278H007AA09":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 17).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ6278H018AA01":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 19).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ6278H019AD02":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 20).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ6278H019AD03":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 21).Sum(x => x.U_CQCT);
                                    break;
                                default:
                                    row1["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString());
                                    break;
                            }
                            row1["DESCRIPTION"] = tableData.Rows[i][15].ToString();
                            row1["CREATE_BY"] = currentUser;
                            row1["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][3].ToString()) ? "0" : tableData.Rows[i][3].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString()));
                            tableKHCP.Rows.Add(row1);


                            //HAN
                            var row2 = tableKHCP.NewRow();
                            row2["PKID"] = Guid.NewGuid().ToString();
                            row2["ORG_CODE"] = orgCode;
                            row2["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeMB.CODE;
                            row2["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                            row2["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                            row2["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                            row2["VERSION"] = versionNext;
                            row2["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                            row2["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][3].ToString()) ? "0" : tableData.Rows[i][3].ToString());
                            switch (elementCode)
                            {
                                case "CQ62711A":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 4).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ62711B":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 6).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ62711C":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 7).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ62711D":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 8).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ62711E":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 9).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ62711G":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 10).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ62711F":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 11).Sum(x => x.U_CQCT);
                                    break;
                               
                                case "CQ6272B007A01":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 13).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ6278H002A01":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 15).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ6278H008A01":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 18).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ6278H006A01":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 16).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ6278H007AA09":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 17).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ6278H018AA01":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 19).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ6278H019AD02":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 20).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ6278H019AD03":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 21).Sum(x => x.U_CQCT);
                                    break;

                                default:
                                    row2["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString());
                                    break;
                            }
                            row2["DESCRIPTION"] = tableData.Rows[i][15].ToString();
                            row2["CREATE_BY"] = currentUser;
                            row2["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString()));
                            tableKHCP.Rows.Add(row2);


                            //THD
                            var row3 = tableKHCP.NewRow();
                            row3["PKID"] = Guid.NewGuid().ToString();
                            row3["ORG_CODE"] = orgCode;
                            row3["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeMT.CODE;
                            row3["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                            row3["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                            row3["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                            row3["VERSION"] = versionNext;
                            row3["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                            row3["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][4].ToString()) ? "0" : tableData.Rows[i][4].ToString());
                            //row3["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString());
                            switch (elementCode)
                            {
                                case "CQ62711A":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 4).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ62711B":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 6).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ62711C":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 7).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ62711D":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 8).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ62711E":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 9).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ62711G":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 10).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ62711F":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 11).Sum(x => x.U_CQCT);
                                    break;
                               
                                case "CQ6272B007A01":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 13).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ6278H002A01":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 15).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ6278H008A01":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 18).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ6278H006A01":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 16).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ6278H007AA09":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 17).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ6278H018AA01":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 19).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ6278H019AD02":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 20).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ6278H019AD03":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 21).Sum(x => x.U_CQCT);
                                    break;
                                default:
                                    row3["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString());
                                    break;
                            }
                            row3["DESCRIPTION"] = tableData.Rows[i][15].ToString();
                            row3["CREATE_BY"] = currentUser;
                            row3["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][4].ToString()) ? "0" : tableData.Rows[i][4].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString()));
                            tableKHCP.Rows.Add(row3);


                            
                            var row4 = tableKHCP.NewRow();
                            row4["PKID"] = Guid.NewGuid().ToString();
                            row4["ORG_CODE"] = orgCode;
                            row4["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeCR.CODE;
                            row4["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                            row4["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                            row4["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                            row4["VERSION"] = versionNext;
                            row4["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                            row4["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][5].ToString()) ? "0" : tableData.Rows[i][5].ToString());
                            //row4["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString());
                            switch (elementCode)
                            {
                                case "CQ62711A":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 4).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ62711B":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 6).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ62711C":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 7).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ62711D":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 8).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ62711E":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 9).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ62711G":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 10).Sum(x => x.U_CQCT);
                                    break;
                                   case "CQ62711F":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 11).Sum(x => x.U_CQCT);
                                    break;
                                
                                case "CQ6272B007A01":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 13).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ6278H002A01":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 15).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ6278H008A01":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 18).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ6278H006A01":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 16).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ6278H007AA09":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 17).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ6278H018AA01":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 19).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ6278H019AD02":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 20).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ6278H019AD03":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 21).Sum(x => x.U_CQCT);
                                    break;
                                default:
                                    row4["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString());
                                    break;
                            }

                            row4["DESCRIPTION"] = tableData.Rows[i][15].ToString();
                            row4["CREATE_BY"] = currentUser;
                            row4["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][5].ToString()) ? "0" : tableData.Rows[i][5].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString()));
                            tableKHCP.Rows.Add(row4);


                            //MN
                            var row5 = tableKHCP.NewRow();
                            row5["PKID"] = Guid.NewGuid().ToString();
                            row5["ORG_CODE"] = orgCode;
                            row5["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeMN.CODE;
                            row5["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                            row5["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                            row5["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                            row5["VERSION"] = versionNext;
                            row5["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                            row5["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][6].ToString()) ? "0" : tableData.Rows[i][6].ToString());
                            //row5["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString());
                            switch (elementCode)
                            {
                                case "CQ62711A":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 4).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ62711B":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 6).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ62711C":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 7).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ62711D":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 8).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ62711E":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 9).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ62711G":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 10).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ62711F":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 11).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ6272B007A01":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 13).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ6278H002A01":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 15).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ6278H008A01":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 18).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ6278H006A01":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 16).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ6278H007AA09":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 17).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ6278H018AA01":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 19).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ6278H019AD02":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 20).Sum(x => x.U_CQCT);
                                    break;
                                case "CQ6278H019AD03":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 21).Sum(x => x.U_CQCT);
                                    break;
                                default:
                                    row5["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString());
                                    break;
                            }
                           
                            row5["DESCRIPTION"] = tableData.Rows[i][15].ToString();
                            row5["CREATE_BY"] = currentUser;
                            row5["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][6].ToString()) ? "0" : tableData.Rows[i][6].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString()));
                            tableKHCP.Rows.Add(row5);
                            
                        }
                        else if (template.DetailKeHoachChiPhi.Any(x => x.Center?.COST_CENTER_CODE == "100002"))
                        {
                            var str1 = tableData.Rows[i][2].ToString();
                            var str2 = tableData.Rows[i][3].ToString();
                            var str3 = tableData.Rows[i][4].ToString();
                            var str4 = tableData.Rows[i][5].ToString();
                            var str5 = tableData.Rows[i][6].ToString();
                            var str6 = tableData.Rows[i][7].ToString();
                            var str7 = tableData.Rows[i][8].ToString();

                            var str8 = tableData.Rows[i][10].ToString();
                            if (!decimal.TryParse(str1, out decimal value) && !string.IsNullOrEmpty(str1))
                            {
                                this.State = false;
                                this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                return;
                            }
                            if (!decimal.TryParse(str2, out decimal value1) && !string.IsNullOrEmpty(str2))
                            {
                                this.State = false;
                                this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                return;
                            }
                            if (!decimal.TryParse(str3, out decimal value2) && !string.IsNullOrEmpty(str3))
                            {
                                this.State = false;
                                this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                return;
                            }
                            if (!decimal.TryParse(str4, out decimal value3) && !string.IsNullOrEmpty(str4))
                            {
                                this.State = false;
                                this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                return;
                            }
                            if (!decimal.TryParse(str5, out decimal value4) && !string.IsNullOrEmpty(str5))
                            {
                                this.State = false;
                                this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                return;
                            }
                            if (!decimal.TryParse(str6, out decimal value5) && !string.IsNullOrEmpty(str6))
                            {
                                this.State = false;
                                this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                return;
                            }
                            if (!decimal.TryParse(str7, out decimal value6) && !string.IsNullOrEmpty(str7))
                            {
                                this.State = false;
                                this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                return;
                            }
                            if (!decimal.TryParse(str8, out decimal value7) && !string.IsNullOrEmpty(str8))
                            {
                                this.State = false;
                                this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                return;
                            }

                            var centerCodeVPCN = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VPCN" && x.COST_CENTER_CODE == "100002");
                            var centerCodeHAN = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "HAN" && x.COST_CENTER_CODE == "100002");
                            var centerCodeHPH = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "HPH" && x.COST_CENTER_CODE == "100002");
                            var centerCodeTHD = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "THD" && x.COST_CENTER_CODE == "100002");
                            var centerCodeVII = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VII" && x.COST_CENTER_CODE == "100002");
                            var centerCodeVDH = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VDH" && x.COST_CENTER_CODE == "100002");
                            var centerCodeVDO = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VDO" && x.COST_CENTER_CODE == "100002");

                            if (centerCodeVPCN == null || centerCodeHAN == null || centerCodeHPH == null || centerCodeTHD == null || centerCodeVII == null || centerCodeVDH == null || centerCodeVDO == null)
                            {
                                throw new Exception($"Định dạng file không đúng hoặc có lỗi hệ thống xảy ra! Vui lòng liên hệ với quản trị viên!");
                            }

                            //VPCN
                            var row1 = tableKHCP.NewRow();
                            row1["PKID"] = Guid.NewGuid().ToString();
                            row1["ORG_CODE"] = orgCode;
                            row1["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeVPCN.CODE;
                            row1["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                            row1["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                            row1["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                            row1["VERSION"] = versionNext;
                            row1["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                            row1["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][2].ToString()) ? "0" : tableData.Rows[i][2].ToString());
                            //row1["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                            switch (elementCode)
                            {
                                case "B62711A":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 4).Sum(x => x.U_MB);
                                    break;
                                case "B62711B":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 6).Sum(x => x.U_MB);
                                    break;
                                case "B62711C":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 7).Sum(x => x.U_MB);
                                    break;
                                case "B62711D":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 8).Sum(x => x.U_MB);
                                    break;
                                case "B62711E":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 9).Sum(x => x.U_MB);
                                    break;
                                case "B62711G":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 10).Sum(x => x.U_MB);
                                    break;
                                case "B62711F":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 11).Sum(x => x.U_MB);
                                    break;
                                case "B6272B007B1":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 13).Sum(x => x.U_MB);
                                    break;
                                case "B6278H002A1":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 15).Sum(x => x.U_MB);
                                    break;
                               
                                case "B6278H006A1":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 16).Sum(x => x.U_MB);
                                    break;
                                case "B6278H007A11":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 17).Sum(x => x.U_MB);
                                    break;
                                case "B6278H008A1":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 18).Sum(x => x.U_MB);
                                    break;
                                case "B6278H018A1":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 19).Sum(x => x.U_MB);
                                    break;
                                case "B6278H019A5":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 20).Sum(x => x.U_MB);
                                    break;
                                case "B6278H019A6":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 21).Sum(x => x.U_MB);
                                    break;
                                default:
                                    row1["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                                    break;
                            }
                            row1["DESCRIPTION"] = tableData.Rows[i][19].ToString();
                            row1["CREATE_BY"] = currentUser;
                            row1["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][2].ToString()) ? "0" : tableData.Rows[i][2].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()));
                            tableKHCP.Rows.Add(row1);


                            //HAN
                            var row2 = tableKHCP.NewRow();
                            row2["PKID"] = Guid.NewGuid().ToString();
                            row2["ORG_CODE"] = orgCode;
                            row2["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeHAN.CODE;
                            row2["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                            row2["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                            row2["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                            row2["VERSION"] = versionNext;
                            row2["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                            row2["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][3].ToString()) ? "0" : tableData.Rows[i][3].ToString());
                            //row2["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                            row2["DESCRIPTION"] = tableData.Rows[i][19].ToString();
                            row2["CREATE_BY"] = currentUser;
                            //if (elementCode == "B621A004")
                            //{
                            //    row2["QUANTITY"] = Convert.ToDecimal(DMDNMB * 100);
                            //    row2["PRICE"] = 0;
                            //}
                            //if (elementCode == "B621A003")
                            //{
                            //    row2["QUANTITY"] = Convert.ToDecimal(DMNAPXA_CNMB_HAN );
                            //    row2["PRICE"] = 0;
                            //}
                            //if (elementCode == "B621A001")
                            //{
                            //    row2["QUANTITY"] = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE == "HAN").Sum(x => x.VALUE_SUM_YEAR) ?? 0;
                            //    row2["PRICE"] = 0;
                            //}
                            switch (elementCode)
                            {
                                case "B62711A":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 4).Sum(x => x.U_MB);
                                    break;
                                case "B62711B":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 6).Sum(x => x.U_MB);
                                    break;
                                case "B62711C":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 7).Sum(x => x.U_MB);
                                    break;
                                case "B62711D":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 8).Sum(x => x.U_MB);
                                    break;
                                case "B62711E":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 9).Sum(x => x.U_MB);
                                    break;
                                case "B62711G":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 10).Sum(x => x.U_MB);
                                    break;
                                case "B62711F":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 11).Sum(x => x.U_MB);
                                    break;
                                case "B6272B007B1":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 13).Sum(x => x.U_MB);
                                    break;
                                case "B6278H002A1":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 15).Sum(x => x.U_MB);
                                    break;

                                case "B6278H006A1":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 16).Sum(x => x.U_MB);
                                    break;
                                case "B6278H007A11":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 17).Sum(x => x.U_MB);
                                    break;
                                case "B6278H008A1":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 18).Sum(x => x.U_MB);
                                    break;
                                case "B6278H018A1":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 19).Sum(x => x.U_MB);
                                    break;
                                case "B6278H019A5":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 20).Sum(x => x.U_MB);
                                    break;
                                case "B6278H019A6":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 21).Sum(x => x.U_MB);
                                    break;
                                default:
                                    row2["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                                    break;
                            }
                            row2["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][3].ToString()) ? "0" : tableData.Rows[i][3].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()));
                            tableKHCP.Rows.Add(row2);


                            //VDO
                            var row3 = tableKHCP.NewRow();
                            row3["PKID"] = Guid.NewGuid().ToString();
                            row3["ORG_CODE"] = orgCode;
                            row3["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeHPH.CODE;
                            row3["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                            row3["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                            row3["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                            row3["VERSION"] = versionNext;
                            row3["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                            row3["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][4].ToString()) ? "0" : tableData.Rows[i][4].ToString());
                            //row3["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                            switch (elementCode)
                            {
                                case "B62711A":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 4).Sum(x => x.U_MB);
                                    break;
                                case "B62711B":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 6).Sum(x => x.U_MB);
                                    break;
                                case "B62711C":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 7).Sum(x => x.U_MB);
                                    break;
                                case "B62711D":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 8).Sum(x => x.U_MB);
                                    break;
                                case "B62711E":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 9).Sum(x => x.U_MB);
                                    break;
                                case "B62711G":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 10).Sum(x => x.U_MB);
                                    break;
                                case "B62711F":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 11).Sum(x => x.U_MB);
                                    break;
                                case "B6272B007B1":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 13).Sum(x => x.U_MB);
                                    break;
                                case "B6278H002A1":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 15).Sum(x => x.U_MB);
                                    break;

                                case "B6278H006A1":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 16).Sum(x => x.U_MB);
                                    break;
                                case "B6278H007A11":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 17).Sum(x => x.U_MB);
                                    break;
                                case "B6278H008A1":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 18).Sum(x => x.U_MB);
                                    break;
                                case "B6278H018A1":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 19).Sum(x => x.U_MB);
                                    break;
                                case "B6278H019A5":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 20).Sum(x => x.U_MB);
                                    break;
                                case "B6278H019A6":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 21).Sum(x => x.U_MB);
                                    break;
                                default:
                                    row3["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                                    break;
                            }
                            row3["DESCRIPTION"] = tableData.Rows[i][19].ToString();
                            row3["CREATE_BY"] = currentUser;
                            row3["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][4].ToString()) ? "0" : tableData.Rows[i][4].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()));

                            //if (elementCode == "B621A004")
                            //{
                            //    row3["QUANTITY"] = Convert.ToDecimal(DMDNMB * 100);
                            //    row3["PRICE"] = 0;
                            //}
                            //if (elementCode == "B621A003")
                            //{
                            //    row3["QUANTITY"] = Convert.ToDecimal(DMNAPXA_CNMB_HPH);
                            //    row3["PRICE"] = 0;
                            //}
                            //if (elementCode == "B621A001")
                            //{
                            //    row3["QUANTITY"] = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE == "HPH").Sum(x => x.VALUE_SUM_YEAR) ?? 0;
                            //    row3["PRICE"] = 0;
                            //}
                            tableKHCP.Rows.Add(row3);


                            //HPH
                            var row4 = tableKHCP.NewRow();
                            row4["PKID"] = Guid.NewGuid().ToString();
                            row4["ORG_CODE"] = orgCode;
                            row4["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeTHD.CODE;
                            row4["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                            row4["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                            row4["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                            row4["VERSION"] = versionNext;
                            row4["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                            row4["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][5].ToString()) ? "0" : tableData.Rows[i][5].ToString());
                            //row4["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                            switch (elementCode)
                            {
                                case "B62711A":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 4).Sum(x => x.U_MB);
                                    break;
                                case "B62711B":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 6).Sum(x => x.U_MB);
                                    break;
                                case "B62711C":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 7).Sum(x => x.U_MB);
                                    break;
                                case "B62711D":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 8).Sum(x => x.U_MB);
                                    break;
                                case "B62711E":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 9).Sum(x => x.U_MB);
                                    break;
                                case "B62711G":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 10).Sum(x => x.U_MB);
                                    break;
                                case "B62711F":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 11).Sum(x => x.U_MB);
                                    break;
                                case "B6272B007B1":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 13).Sum(x => x.U_MB);
                                    break;
                                case "B6278H002A1":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 15).Sum(x => x.U_MB);
                                    break;

                                case "B6278H006A1":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 16).Sum(x => x.U_MB);
                                    break;
                                case "B6278H007A11":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 17).Sum(x => x.U_MB);
                                    break;
                                case "B6278H008A1":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 18).Sum(x => x.U_MB);
                                    break;
                                case "B6278H018A1":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 19).Sum(x => x.U_MB);
                                    break;
                                case "B6278H019A5":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 20).Sum(x => x.U_MB);
                                    break;
                                case "B6278H019A6":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 21).Sum(x => x.U_MB);
                                    break;
                                default:
                                    row4["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                                    break;
                            };
                            row4["DESCRIPTION"] = tableData.Rows[i][19].ToString();
                            row4["CREATE_BY"] = currentUser;
                            row4["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][5].ToString()) ? "0" : tableData.Rows[i][5].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()));
                            //if (elementCode == "B621A004")
                            //{
                            //    row3["QUANTITY"] = Convert.ToDecimal(DMDNMB * 100);
                            //    row3["PRICE"] = 0;
                            //}
                            //if (elementCode == "B621A003")
                            //{
                            //    row3["QUANTITY"] = Convert.ToDecimal(DMNAPXA_CNMB_TDH);
                            //    row3["PRICE"] = 0;
                            //}
                            //if (elementCode == "B621A001")
                            //{
                            //    row3["QUANTITY"] = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE == "TDH").Sum(x => x.VALUE_SUM_YEAR) ?? 0;
                            //    row3["PRICE"] = 0;
                            //}
                            tableKHCP.Rows.Add(row4);


                            //DIN
                            var row5 = tableKHCP.NewRow();
                            row5["PKID"] = Guid.NewGuid().ToString();
                            row5["ORG_CODE"] = orgCode;
                            row5["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeVII.CODE;
                            row5["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                            row5["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                            row5["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                            row5["VERSION"] = versionNext;
                            row5["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                            row5["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][6].ToString()) ? "0" : tableData.Rows[i][6].ToString());
                            //row5["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                            switch (elementCode)
                            {
                                case "B62711A":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 4).Sum(x => x.U_MB);
                                    break;
                                case "B62711B":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 6).Sum(x => x.U_MB);
                                    break;
                                case "B62711C":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 7).Sum(x => x.U_MB);
                                    break;
                                case "B62711D":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 8).Sum(x => x.U_MB);
                                    break;
                                case "B62711E":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 9).Sum(x => x.U_MB);
                                    break;
                                case "B62711G":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 10).Sum(x => x.U_MB);
                                    break;
                                case "B62711F":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 11).Sum(x => x.U_MB);
                                    break;
                                case "B6272B007B1":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 13).Sum(x => x.U_MB);
                                    break;
                                case "B6278H002A1":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 15).Sum(x => x.U_MB);
                                    break;

                                case "B6278H006A1":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 16).Sum(x => x.U_MB);
                                    break;
                                case "B6278H007A11":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 17).Sum(x => x.U_MB);
                                    break;
                                case "B6278H008A1":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 18).Sum(x => x.U_MB);
                                    break;
                                case "B6278H018A1":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 19).Sum(x => x.U_MB);
                                    break;
                                case "B6278H019A5":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 20).Sum(x => x.U_MB);
                                    break;
                                case "B6278H019A6":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 21).Sum(x => x.U_MB);
                                    break;
                                default:
                                    row5["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                                    break;
                            }
                            row5["DESCRIPTION"] = tableData.Rows[i][19].ToString();
                            row5["CREATE_BY"] = currentUser;
                            row5["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][6].ToString()) ? "0" : tableData.Rows[i][6].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()));
                            //if (elementCode == "B621A004")
                            //{
                            //    row5["QUANTITY"] = Convert.ToDecimal(DMDNMB * 100);
                            //    row5["PRICE"] = 0;
                            //}
                            //if (elementCode == "B621A003")
                            //{
                            //    row5["QUANTITY"] = Convert.ToDecimal(DMNAPXA_CNMB_VII);
                            //    row5["PRICE"] = 0;
                            //}
                            //if (elementCode == "B621A001")
                            //{
                            //    row5["QUANTITY"] = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE == "VII").Sum(x => x.VALUE_SUM_YEAR) ?? 0;
                            //    row5["PRICE"] = 0;
                            //}
                            tableKHCP.Rows.Add(row5);


                            //THD
                            var row6 = tableKHCP.NewRow();
                            row6["PKID"] = Guid.NewGuid().ToString();
                            row6["ORG_CODE"] = orgCode;
                            row6["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeVII.CODE;
                            row6["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                            row6["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                            row6["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                            row6["VERSION"] = versionNext;
                            row6["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                            row6["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString());
                            //row6["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                            switch (elementCode)
                            {
                                case "B62711A":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 4).Sum(x => x.U_MB);
                                    break;
                                case "B62711B":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 6).Sum(x => x.U_MB);
                                    break;
                                case "B62711C":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 7).Sum(x => x.U_MB);
                                    break;
                                case "B62711D":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 8).Sum(x => x.U_MB);
                                    break;
                                case "B62711E":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 9).Sum(x => x.U_MB);
                                    break;
                                case "B62711G":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 10).Sum(x => x.U_MB);
                                    break;
                                case "B62711F":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 11).Sum(x => x.U_MB);
                                    break;
                                case "B6272B007B1":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 13).Sum(x => x.U_MB);
                                    break;
                                case "B6278H002A1":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 15).Sum(x => x.U_MB);
                                    break;

                                case "B6278H006A1":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 16).Sum(x => x.U_MB);
                                    break;
                                case "B6278H007A11":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 17).Sum(x => x.U_MB);
                                    break;
                                case "B6278H008A1":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 18).Sum(x => x.U_MB);
                                    break;
                                case "B6278H018A1":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 19).Sum(x => x.U_MB);
                                    break;
                                case "B6278H019A5":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 20).Sum(x => x.U_MB);
                                    break;
                                case "B6278H019A6":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 21).Sum(x => x.U_MB);
                                    break;
                                default:
                                    row6["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                                    break;
                            }
                            row6["DESCRIPTION"] = tableData.Rows[i][19].ToString();
                            row6["CREATE_BY"] = currentUser;
                            row6["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()));
                            //if (elementCode == "B621A004")
                            //{
                            //    row6["QUANTITY"] = Convert.ToDecimal(DMDNMB * 100);
                            //    row6["PRICE"] = 0;
                            //}
                            //if (elementCode == "B621A003")
                            //{
                            //    row6["QUANTITY"] = Convert.ToDecimal(DMNAPXA_CNMB_VDH);
                            //    row6["PRICE"] = 0;
                            //}
                            //if (elementCode == "B621A001")
                            //{
                            //    row6["QUANTITY"] = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE == "VDH").Sum(x => x.VALUE_SUM_YEAR) ?? 0;
                            //    row6["PRICE"] = 0;
                            //}
                            tableKHCP.Rows.Add(row6);


                            //NAF
                            var row7 = tableKHCP.NewRow();
                            row7["PKID"] = Guid.NewGuid().ToString();
                            row7["ORG_CODE"] = orgCode;
                            row7["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeVDO.CODE;
                            row7["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                            row7["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                            row7["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                            row7["VERSION"] = versionNext;
                            row7["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                            row7["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString());
                            //row7["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                            switch (elementCode)
                            {
                                case "B62711A":
                                    row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 4).Sum(x => x.U_MB);
                                    break;
                                case "B62711B":
                                    row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 6).Sum(x => x.U_MB);
                                    break;
                                case "B62711C":
                                    row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 7).Sum(x => x.U_MB);
                                    break;
                                case "B62711D":
                                    row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 8).Sum(x => x.U_MB);
                                    break;
                                case "B62711E":
                                    row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 9).Sum(x => x.U_MB);
                                    break;
                                case "B62711G":
                                    row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 10).Sum(x => x.U_MB);
                                    break;
                                case "B62711F":
                                    row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 11).Sum(x => x.U_MB);
                                    break;
                                case "B6272B007B1":
                                    row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 13).Sum(x => x.U_MB);
                                    break;
                                case "B6278H002A1":
                                    row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 15).Sum(x => x.U_MB);
                                    break;

                                case "B6278H006A1":
                                    row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 16).Sum(x => x.U_MB);
                                    break;
                                case "B6278H007A11":
                                    row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 17).Sum(x => x.U_MB);
                                    break;
                                case "B6278H008A1":
                                    row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 18).Sum(x => x.U_MB);
                                    break;
                                case "B6278H018A1":
                                    row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 19).Sum(x => x.U_MB);
                                    break;
                                case "B6278H019A5":
                                    row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 20).Sum(x => x.U_MB);
                                    break;
                                case "B6278H019A6":
                                    row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 21).Sum(x => x.U_MB);
                                    break;
                                default:
                                    row7["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                                    break;
                            }
                            row7["DESCRIPTION"] = tableData.Rows[i][19].ToString();
                            row7["CREATE_BY"] = currentUser;
                            row7["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()));
                            //if (elementCode == "B621A004")
                            //{
                            //    row7["QUANTITY"] = Convert.ToDecimal(DMDNMB * 100);
                            //    row7["PRICE"] = 0;
                            //}
                            //if (elementCode == "B621A003")
                            //{
                            //    row7["QUANTITY"] = Convert.ToDecimal(DMNAPXA_CNMB_VDO);
                            //    row7["PRICE"] = 0;
                            //}
                            //if (elementCode == "B621A001")
                            //{
                            //    row7["QUANTITY"] = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE == "VDO").Sum(x => x.VALUE_SUM_YEAR) ?? 0;
                            //    row7["PRICE"] = 0;
                            //}
                            tableKHCP.Rows.Add(row7);


                        }
                        else if (template.DetailKeHoachChiPhi.Any(x => x.Center?.COST_CENTER_CODE == "100003"))
                        {
                            var str1 = tableData.Rows[i][2].ToString();
                            var str2 = tableData.Rows[i][3].ToString();
                            var str3 = tableData.Rows[i][4].ToString();
                            var str4 = tableData.Rows[i][5].ToString();
                            var str5 = tableData.Rows[i][6].ToString();
                            var str6 = tableData.Rows[i][7].ToString();
                            var str7 = tableData.Rows[i][8].ToString();
                            var str8 = tableData.Rows[i][9].ToString();

                            var str9 = tableData.Rows[i][11].ToString();
                            if (!decimal.TryParse(str1, out decimal value) && !string.IsNullOrEmpty(str1))
                            {
                                this.State = false;
                                this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                return;
                            }
                            if (!decimal.TryParse(str2, out decimal value1) && !string.IsNullOrEmpty(str2))
                            {
                                this.State = false;
                                this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                return;
                            }
                            if (!decimal.TryParse(str3, out decimal value2) && !string.IsNullOrEmpty(str3))
                            {
                                this.State = false;
                                this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                return;
                            }
                            if (!decimal.TryParse(str4, out decimal value3) && !string.IsNullOrEmpty(str4))
                            {
                                this.State = false;
                                this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                return;
                            }
                            if (!decimal.TryParse(str5, out decimal value4) && !string.IsNullOrEmpty(str5))
                            {
                                this.State = false;
                                this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                return;
                            }
                            if (!decimal.TryParse(str6, out decimal value5) && !string.IsNullOrEmpty(str6))
                            {
                                this.State = false;
                                this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                return;
                            }
                            if (!decimal.TryParse(str7, out decimal value6) && !string.IsNullOrEmpty(str7))
                            {
                                this.State = false;
                                this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                return;
                            }
                            if (!decimal.TryParse(str8, out decimal value7) && !string.IsNullOrEmpty(str8))
                            {
                                this.State = false;
                                this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                return;
                            }
                            if (!decimal.TryParse(str9, out decimal value8) && !string.IsNullOrEmpty(str9))
                            {
                                this.State = false;
                                this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                return;
                            }

                            var centerCodeVPCN = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VPCN" && x.COST_CENTER_CODE == "100003");
                            var centerCodeDAD = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "DAD" && x.COST_CENTER_CODE == "100003");
                            var centerCodeCXR = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "CXR" && x.COST_CENTER_CODE == "100003");
                            var centerCodeHUI = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "HUI" && x.COST_CENTER_CODE == "100003");
                            var centerCodeUIH = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "UIH" && x.COST_CENTER_CODE == "100003");
                            var centerCodeVCL = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VCL" && x.COST_CENTER_CODE == "100003");
                            var centerCodePXU = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "PXU" && x.COST_CENTER_CODE == "100003");
                            var centerCodeTBB = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "TBB" && x.COST_CENTER_CODE == "100003");
                            if (centerCodeVPCN == null || centerCodeDAD == null || centerCodeCXR == null || centerCodeHUI == null || centerCodeUIH == null || centerCodeVCL == null || centerCodePXU == null || centerCodeTBB == null)
                            {
                                throw new Exception($"Định dạng file không đúng hoặc có lỗi hệ thống xảy ra! Vui lòng liên hệ với quản trị viên!");
                            }

                            //VPCN
                            var row1 = tableKHCP.NewRow();
                            row1["PKID"] = Guid.NewGuid().ToString();
                            row1["ORG_CODE"] = orgCode;
                            row1["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeVPCN.CODE;
                            row1["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                            row1["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                            row1["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                            row1["VERSION"] = versionNext;
                            row1["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                            row1["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][2].ToString()) ? "0" : tableData.Rows[i][2].ToString());
                            //row1["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString());
                            switch (elementCode)
                            {
                                case "T62711A0001":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 4).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0002":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 6).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0003":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 7).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0004":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 8).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0005":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 9).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0007":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 10).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0006":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 11).Sum(x => x.U_MT);
                                    break;
                                case "T6272B007B1":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 13).Sum(x => x.U_MT);
                                    break;
                                case "T6278H002A01":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 15).Sum(x => x.U_MT);
                                    break;

                                case "T6278H006A1":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 16).Sum(x => x.U_MT);
                                    break;
                                case "T6278H007AB":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 17).Sum(x => x.U_MT);
                                    break;
                                case "T6278H008A1":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 18).Sum(x => x.U_MT);
                                    break;
                                case "T6278H018A1":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 19).Sum(x => x.U_MT);
                                    break;
                                case "T6278H019AF":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 20).Sum(x => x.U_MT);
                                    break;
                                case "T6278H019AE":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 21).Sum(x => x.U_MT);
                                    break;
                                default:
                                    row1["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString());
                                    break;
                            };
                            row1["DESCRIPTION"] = tableData.Rows[i][21].ToString();
                            row1["CREATE_BY"] = currentUser;
                            row1["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][2].ToString()) ? "0" : tableData.Rows[i][2].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString()));
                            tableKHCP.Rows.Add(row1);

                            //DAD
                            var row2 = tableKHCP.NewRow();
                            row2["PKID"] = Guid.NewGuid().ToString();
                            row2["ORG_CODE"] = orgCode;
                            row2["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeDAD.CODE;
                            row2["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                            row2["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                            row2["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                            row2["VERSION"] = versionNext;
                            row2["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                            row2["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][3].ToString()) ? "0" : tableData.Rows[i][3].ToString());
                            //row2["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString());
                            switch (elementCode)
                            {
                                case "T62711A0001":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 4).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0002":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 6).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0003":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 7).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0004":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 8).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0005":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 9).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0007":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 10).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0006":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 11).Sum(x => x.U_MT);
                                    break;
                                case "T6272B007B1":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 13).Sum(x => x.U_MT);
                                    break;
                                case "T6278H002A01":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 15).Sum(x => x.U_MT);
                                    break;

                                case "T6278H006A1":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 16).Sum(x => x.U_MT);
                                    break;
                                case "T6278H007AB":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 17).Sum(x => x.U_MT);
                                    break;
                                case "T6278H008A1":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 18).Sum(x => x.U_MT);
                                    break;
                                case "T6278H018A1":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 19).Sum(x => x.U_MT);
                                    break;
                                case "T6278H019AF":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 20).Sum(x => x.U_MT);
                                    break;
                                case "T6278H019AE":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 21).Sum(x => x.U_MT);
                                    break;
                                default:
                                    row2["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString());
                                    break;
                            };
                            row2["DESCRIPTION"] = tableData.Rows[i][21].ToString();
                            row2["CREATE_BY"] = currentUser;
                            row2["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][3].ToString()) ? "0" : tableData.Rows[i][3].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString()));
                            tableKHCP.Rows.Add(row2);

                            //CXR
                            var row3 = tableKHCP.NewRow();
                            row3["PKID"] = Guid.NewGuid().ToString();
                            row3["ORG_CODE"] = orgCode;
                            row3["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeCXR.CODE;
                            row3["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                            row3["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                            row3["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                            row3["VERSION"] = versionNext;
                            row3["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                            row3["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][4].ToString()) ? "0" : tableData.Rows[i][4].ToString());
                            //row3["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString());
                             switch (elementCode)
                            {
                                case "T62711A0001":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 4).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0002":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 6).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0003":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 7).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0004":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 8).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0005":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 9).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0007":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 10).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0006":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 11).Sum(x => x.U_MT);
                                    break;
                                case "T6272B007B1":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 13).Sum(x => x.U_MT);
                                    break;
                                case "T6278H002A01":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 15).Sum(x => x.U_MT);
                                    break;

                                case "T6278H006A1":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 16).Sum(x => x.U_MT);
                                    break;
                                case "T6278H007AB":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 17).Sum(x => x.U_MT);
                                    break;
                                case "T6278H008A1":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 18).Sum(x => x.U_MT);
                                    break;
                                case "T6278H018A1":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 19).Sum(x => x.U_MT);
                                    break;
                                case "T6278H019AF":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 20).Sum(x => x.U_MT);
                                    break;
                                case "T6278H019AE":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 21).Sum(x => x.U_MT);
                                    break;
                                default:
                                    row3["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString());
                                    break;
                            };
                            row3["DESCRIPTION"] = tableData.Rows[i][21].ToString();
                            row3["CREATE_BY"] = currentUser;
                            row3["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][4].ToString()) ? "0" : tableData.Rows[i][4].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString()));
                            tableKHCP.Rows.Add(row3);


                            //HUI
                            var row4 = tableKHCP.NewRow();
                            row4["PKID"] = Guid.NewGuid().ToString();
                            row4["ORG_CODE"] = orgCode;
                            row4["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeHUI.CODE;
                            row4["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                            row4["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                            row4["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                            row4["VERSION"] = versionNext;
                            row4["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                            row4["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][5].ToString()) ? "0" : tableData.Rows[i][5].ToString());
                            //row4["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString());
                            switch (elementCode)
                            {
                                case "T62711A0001":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 4).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0002":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 6).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0003":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 7).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0004":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 8).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0005":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 9).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0007":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 10).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0006":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 11).Sum(x => x.U_MT);
                                    break;
                                case "T6272B007B1":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 13).Sum(x => x.U_MT);
                                    break;
                                case "T6278H002A01":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 15).Sum(x => x.U_MT);
                                    break;

                                case "T6278H006A1":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 16).Sum(x => x.U_MT);
                                    break;
                                case "T6278H007AB":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 17).Sum(x => x.U_MT);
                                    break;
                                case "T6278H008A1":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 18).Sum(x => x.U_MT);
                                    break;
                                case "T6278H018A1":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 19).Sum(x => x.U_MT);
                                    break;
                                case "T6278H019AF":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 20).Sum(x => x.U_MT);
                                    break;
                                case "T6278H019AE":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 21).Sum(x => x.U_MT);
                                    break;
                                default:
                                    row4["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString());
                                    break;
                            };
                            row4["DESCRIPTION"] = tableData.Rows[i][21].ToString();
                            row4["CREATE_BY"] = currentUser;
                            row4["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][5].ToString()) ? "0" : tableData.Rows[i][5].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString()));
                            tableKHCP.Rows.Add(row4);


                            //UIH
                            var row5 = tableKHCP.NewRow();
                            row5["PKID"] = Guid.NewGuid().ToString();
                            row5["ORG_CODE"] = orgCode;
                            row5["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeUIH.CODE;
                            row5["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                            row5["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                            row5["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                            row5["VERSION"] = versionNext;
                            row5["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                            row5["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][6].ToString()) ? "0" : tableData.Rows[i][6].ToString());
                            //row5["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString());
                            switch (elementCode)
                            {
                                case "T62711A0001":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 4).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0002":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 6).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0003":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 7).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0004":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 8).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0005":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 9).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0007":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 10).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0006":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 11).Sum(x => x.U_MT);
                                    break;
                                case "T6272B007B1":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 13).Sum(x => x.U_MT);
                                    break;
                                case "T6278H002A01":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 15).Sum(x => x.U_MT);
                                    break;

                                case "T6278H006A1":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 16).Sum(x => x.U_MT);
                                    break;
                                case "T6278H007AB":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 17).Sum(x => x.U_MT);
                                    break;
                                case "T6278H008A1":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 18).Sum(x => x.U_MT);
                                    break;
                                case "T6278H018A1":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 19).Sum(x => x.U_MT);
                                    break;
                                case "T6278H019AF":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 20).Sum(x => x.U_MT);
                                    break;
                                case "T6278H019AE":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 21).Sum(x => x.U_MT);
                                    break;
                                default:
                                    row5["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString());
                                    break;
                            };
                            row5["DESCRIPTION"] = tableData.Rows[i][21].ToString();
                            row5["CREATE_BY"] = currentUser;
                            row5["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][6].ToString()) ? "0" : tableData.Rows[i][6].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString()));
                            tableKHCP.Rows.Add(row5);


                            //VCL
                            var row6 = tableKHCP.NewRow();
                            row6["PKID"] = Guid.NewGuid().ToString();
                            row6["ORG_CODE"] = orgCode;
                            row6["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeVCL.CODE;
                            row6["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                            row6["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                            row6["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                            row6["VERSION"] = versionNext;
                            row6["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                            row6["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString());
                            //row6["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString());
                            switch (elementCode)
                            {
                                case "T62711A0001":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 4).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0002":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 6).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0003":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 7).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0004":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 8).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0005":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 9).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0007":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 10).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0006":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 11).Sum(x => x.U_MT);
                                    break;
                                case "T6272B007B1":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 13).Sum(x => x.U_MT);
                                    break;
                                case "T6278H002A01":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 15).Sum(x => x.U_MT);
                                    break;

                                case "T6278H006A1":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 16).Sum(x => x.U_MT);
                                    break;
                                case "T6278H007AB":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 17).Sum(x => x.U_MT);
                                    break;
                                case "T6278H008A1":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 18).Sum(x => x.U_MT);
                                    break;
                                case "T6278H018A1":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 19).Sum(x => x.U_MT);
                                    break;
                                case "T6278H019AF":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 20).Sum(x => x.U_MT);
                                    break;
                                case "T6278H019AE":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 21).Sum(x => x.U_MT);
                                    break;
                                default:
                                    row6["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString());
                                    break;
                            };
                            row6["DESCRIPTION"] = tableData.Rows[i][21].ToString();
                            row6["CREATE_BY"] = currentUser;
                            row6["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString()));
                            tableKHCP.Rows.Add(row6);




                            //PXU
                            var row7 = tableKHCP.NewRow();
                            row7["PKID"] = Guid.NewGuid().ToString();
                            row7["ORG_CODE"] = orgCode;
                            row7["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodePXU.CODE;
                            row7["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                            row7["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                            row7["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                            row7["VERSION"] = versionNext;
                            row7["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                            row7["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString());
                            //row7["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString());
                            switch (elementCode)
                            {
                                case "T62711A0001":
                                    row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 4).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0002":
                                    row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 6).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0003":
                                    row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 7).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0004":
                                    row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 8).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0005":
                                    row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 9).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0007":
                                    row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 10).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0006":
                                    row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 11).Sum(x => x.U_MT);
                                    break;
                                case "T6272B007B1":
                                    row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 13).Sum(x => x.U_MT);
                                    break;
                                case "T6278H002A01":
                                    row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 15).Sum(x => x.U_MT);
                                    break;

                                case "T6278H006A1":
                                    row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 16).Sum(x => x.U_MT);
                                    break;
                                case "T6278H007AB":
                                    row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 17).Sum(x => x.U_MT);
                                    break;
                                case "T6278H008A1":
                                    row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 18).Sum(x => x.U_MT);
                                    break;
                                case "T6278H018A1":
                                    row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 19).Sum(x => x.U_MT);
                                    break;
                                case "T6278H019AF":
                                    row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 20).Sum(x => x.U_MT);
                                    break;
                                case "T6278H019AE":
                                    row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 21).Sum(x => x.U_MT);
                                    break;
                                default:
                                    row7["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString());
                                    break;
                            };
                            row7["DESCRIPTION"] = tableData.Rows[i][21].ToString();
                            row7["CREATE_BY"] = currentUser;
                            row7["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString()));
                            tableKHCP.Rows.Add(row7);






                            //TBB
                            var row8 = tableKHCP.NewRow();
                            row8["PKID"] = Guid.NewGuid().ToString();
                            row8["ORG_CODE"] = orgCode;
                            row8["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeTBB.CODE;
                            row8["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                            row8["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                            row8["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                            row8["VERSION"] = versionNext;
                            row8["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                            row8["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][9].ToString()) ? "0" : tableData.Rows[i][9].ToString());
                            //row8["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString());
                            switch (elementCode)
                            {
                                case "T62711A0001":
                                    row8["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 4).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0002":
                                    row8["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 6).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0003":
                                    row8["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 7).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0004":
                                    row8["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 8).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0005":
                                    row8["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 9).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0007":
                                    row8["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 10).Sum(x => x.U_MT);
                                    break;
                                case "T62711A0006":
                                    row8["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 11).Sum(x => x.U_MT);
                                    break;
                                case "T6272B007B1":
                                    row8["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 13).Sum(x => x.U_MT);
                                    break;
                                case "T6278H002A01":
                                    row8["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 15).Sum(x => x.U_MT);
                                    break;

                                case "T6278H006A1":
                                    row8["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 16).Sum(x => x.U_MT);
                                    break;
                                case "T6278H007AB":
                                    row8["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 17).Sum(x => x.U_MT);
                                    break;
                                case "T6278H008A1":
                                    row8["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 18).Sum(x => x.U_MT);
                                    break;
                                case "T6278H018A1":
                                    row8["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 19).Sum(x => x.U_MT);
                                    break;
                                case "T6278H019AF":
                                    row8["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 20).Sum(x => x.U_MT);
                                    break;
                                case "T6278H019AE":
                                    row8["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 21).Sum(x => x.U_MT);
                                    break;
                                default:
                                    row8["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][11].ToString()) ? "0" : tableData.Rows[i][11].ToString());
                                    break;
                            };
                            row8["DESCRIPTION"] = tableData.Rows[i][21].ToString();
                            row8["CREATE_BY"] = currentUser;
                            row8["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][9].ToString()) ? "0" : tableData.Rows[i][9].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()));
                            tableKHCP.Rows.Add(row8);


                        }
                        else if (template.DetailKeHoachChiPhi.Any(x => x.Center?.COST_CENTER_CODE == "100004"))
                        {
                            var str1 = tableData.Rows[i][2].ToString();
                            var str2 = tableData.Rows[i][3].ToString();
                            var str3 = tableData.Rows[i][4].ToString();
                            var str4 = tableData.Rows[i][5].ToString();
                            var str5 = tableData.Rows[i][6].ToString();
                            var str6 = tableData.Rows[i][7].ToString();
                            var str7 = tableData.Rows[i][8].ToString();

                            var str9 = tableData.Rows[i][10].ToString();
                            if (!decimal.TryParse(str1, out decimal value) && !string.IsNullOrEmpty(str1))
                            {
                                this.State = false;
                                this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                return;
                            }
                            if (!decimal.TryParse(str2, out decimal value1) && !string.IsNullOrEmpty(str2))
                            {
                                this.State = false;
                                this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                return;
                            }
                            if (!decimal.TryParse(str3, out decimal value2) && !string.IsNullOrEmpty(str3))
                            {
                                this.State = false;
                                this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                return;
                            }
                            if (!decimal.TryParse(str4, out decimal value3) && !string.IsNullOrEmpty(str4))
                            {
                                this.State = false;
                                this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                return;
                            }
                            if (!decimal.TryParse(str5, out decimal value4) && !string.IsNullOrEmpty(str5))
                            {
                                this.State = false;
                                this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                return;
                            }
                            if (!decimal.TryParse(str6, out decimal value5) && !string.IsNullOrEmpty(str6))
                            {
                                this.State = false;
                                this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                return;
                            }
                            if (!decimal.TryParse(str7, out decimal value6) && !string.IsNullOrEmpty(str7))
                            {
                                this.State = false;
                                this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                return;
                            }
                            if (!decimal.TryParse(str9, out decimal value8) && !string.IsNullOrEmpty(str9))
                            {
                                this.State = false;
                                this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                return;
                            }


                            var centerCodeVPCN = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VPCN" && x.COST_CENTER_CODE == "100004");
                            var centerCodeSGN = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "SGN" && x.COST_CENTER_CODE == "100004");
                            var centerCodePQC = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "PQC" && x.COST_CENTER_CODE == "100004");
                            var centerCodeDLI = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "DLI" && x.COST_CENTER_CODE == "100004");
                            var centerCodeVCA = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VCA" && x.COST_CENTER_CODE == "100004");
                            var centerCodeBMV = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "BMV" && x.COST_CENTER_CODE == "100004");
                            var centerCodeVCS = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VCS" && x.COST_CENTER_CODE == "100004");
                            if (centerCodeVPCN == null || centerCodeSGN == null || centerCodePQC == null || centerCodeDLI == null || centerCodeVCA == null || centerCodeBMV == null /*|| centerCodeVCS == null*/)
                            {
                                throw new Exception($"Định dạng file không đúng hoặc có lỗi hệ thống xảy ra! Vui lòng liên hệ với quản trị viên!");
                            }

                            //VPCN
                            var row1 = tableKHCP.NewRow();
                            row1["PKID"] = Guid.NewGuid().ToString();
                            row1["ORG_CODE"] = orgCode;
                            row1["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeVPCN.CODE;
                            row1["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                            row1["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                            row1["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                            row1["VERSION"] = versionNext;
                            row1["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                            row1["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][2].ToString()) ? "0" : tableData.Rows[i][2].ToString());
                            //row1["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                            switch (elementCode)
                            {
                                case "N62711A1":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 4).Sum(x => x.U_MN);
                                    break;
                                case "N62711A2":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 6).Sum(x => x.U_MN);
                                    break;
                                case "N62711A3":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 7).Sum(x => x.U_MN);
                                    break;
                                case "N62711A4":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 8).Sum(x => x.U_MN);
                                    break;
                                case "N62711A5":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 9).Sum(x => x.U_MN);
                                    break;
                                case "N62711A7":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 10).Sum(x => x.U_MN);
                                    break;
                                case "N62711A6":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 11).Sum(x => x.U_MN);
                                    break;
                                case "N6272B007B1":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 13).Sum(x => x.U_MN);
                                    break;
                                case "N6278H002001":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 15).Sum(x => x.U_MN);
                                    break;

                                case "N6278H06A01":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 16).Sum(x => x.U_MN);
                                    break;
                                case "N6278H07A2":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 17).Sum(x => x.U_MN);
                                    break;
                                case "N6278H008A1":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 18).Sum(x => x.U_MN);
                                    break;
                                case "N6278H018A01":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 19).Sum(x => x.U_MN);
                                    break;
                                case "N6278H019A18":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 20).Sum(x => x.U_MN);
                                    break;
                                case "N6278H019A19":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 21).Sum(x => x.U_MN);
                                    break;
                                default:
                                    row1["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                                    break;
                            };
                            row1["DESCRIPTION"] = tableData.Rows[i][19].ToString();
                            row1["CREATE_BY"] = currentUser;
                            row1["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][2].ToString()) ? "0" : tableData.Rows[i][2].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()));
                            tableKHCP.Rows.Add(row1);


                            //SGN
                            var row2 = tableKHCP.NewRow();
                            row2["PKID"] = Guid.NewGuid().ToString();
                            row2["ORG_CODE"] = orgCode;
                            row2["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeSGN.CODE;
                            row2["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                            row2["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                            row2["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                            row2["VERSION"] = versionNext;
                            row2["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                            row2["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][3].ToString()) ? "0" : tableData.Rows[i][3].ToString());
                            //row2["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                            switch (elementCode)
                            {
                                case "N62711A1":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 4).Sum(x => x.U_MN);
                                    break;
                                case "N62711A2":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 6).Sum(x => x.U_MN);
                                    break;
                                case "N62711A3":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 7).Sum(x => x.U_MN);
                                    break;
                                case "N62711A4":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 8).Sum(x => x.U_MN);
                                    break;
                                case "N62711A5":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 9).Sum(x => x.U_MN);
                                    break;
                                case "N62711A7":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 10).Sum(x => x.U_MN);
                                    break;
                                case "N62711A6":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 11).Sum(x => x.U_MN);
                                    break;
                                case "N6272B007B1":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 13).Sum(x => x.U_MN);
                                    break;
                                case "N6278H002001":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 15).Sum(x => x.U_MN);
                                    break;

                                case "N6278H06A01":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 16).Sum(x => x.U_MN);
                                    break;
                                case "N6278H07A2":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 17).Sum(x => x.U_MN);
                                    break;
                                case "N6278H008A1":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 18).Sum(x => x.U_MN);
                                    break;
                                case "N6278H018A01":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 19).Sum(x => x.U_MN);
                                    break;
                                case "N6278H019A18":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 20).Sum(x => x.U_MN);
                                    break;
                                case "N6278H019A19":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 21).Sum(x => x.U_MN);
                                    break;
                                default:
                                    row2["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                                    break;
                            };
                            row2["DESCRIPTION"] = tableData.Rows[i][19].ToString();
                            row2["CREATE_BY"] = currentUser;
                            row2["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][3].ToString()) ? "0" : tableData.Rows[i][3].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()));
                            tableKHCP.Rows.Add(row2);


                            //PQC
                            var row3 = tableKHCP.NewRow();
                            row3["PKID"] = Guid.NewGuid().ToString();
                            row3["ORG_CODE"] = orgCode;
                            row3["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodePQC.CODE;
                            row3["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                            row3["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                            row3["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                            row3["VERSION"] = versionNext;
                            row3["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                            row3["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][4].ToString()) ? "0" : tableData.Rows[i][4].ToString());
                            //row3["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                            switch (elementCode)
                            {
                                case "N62711A1":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 4).Sum(x => x.U_MN);
                                    break;
                                case "N62711A2":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 6).Sum(x => x.U_MN);
                                    break;
                                case "N62711A3":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 7).Sum(x => x.U_MN);
                                    break;
                                case "N62711A4":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 8).Sum(x => x.U_MN);
                                    break;
                                case "N62711A5":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 9).Sum(x => x.U_MN);
                                    break;
                                case "N62711A7":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 10).Sum(x => x.U_MN);
                                    break;
                                case "N62711A6":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 11).Sum(x => x.U_MN);
                                    break;
                                case "N6272B007B1":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 13).Sum(x => x.U_MN);
                                    break;
                                case "N6278H002001":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 15).Sum(x => x.U_MN);
                                    break;

                                case "N6278H06A01":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 16).Sum(x => x.U_MN);
                                    break;
                                case "N6278H07A2":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 17).Sum(x => x.U_MN);
                                    break;
                                case "N6278H008A1":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 18).Sum(x => x.U_MN);
                                    break;
                                case "N6278H018A01":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 19).Sum(x => x.U_MN);
                                    break;
                                case "N6278H019A18":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 20).Sum(x => x.U_MN);
                                    break;
                                case "N6278H019A19":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 21).Sum(x => x.U_MN);
                                    break;
                                default:
                                    row3["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                                    break;
                            };
                            row3["DESCRIPTION"] = tableData.Rows[i][19].ToString();
                            row3["CREATE_BY"] = currentUser;
                            row3["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][4].ToString()) ? "0" : tableData.Rows[i][4].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()));
                            tableKHCP.Rows.Add(row3);


                            //DLI
                            var row4 = tableKHCP.NewRow();
                            row4["PKID"] = Guid.NewGuid().ToString();
                            row4["ORG_CODE"] = orgCode;
                            row4["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodePQC.CODE;
                            row4["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                            row4["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                            row4["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                            row4["VERSION"] = versionNext;
                            row4["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                            row4["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][5].ToString()) ? "0" : tableData.Rows[i][5].ToString());
                            //row4["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                            switch (elementCode)
                            {
                                case "N62711A1":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 4).Sum(x => x.U_MN);
                                    break;
                                case "N62711A2":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 6).Sum(x => x.U_MN);
                                    break;
                                case "N62711A3":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 7).Sum(x => x.U_MN);
                                    break;
                                case "N62711A4":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 8).Sum(x => x.U_MN);
                                    break;
                                case "N62711A5":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 9).Sum(x => x.U_MN);
                                    break;
                                case "N62711A7":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 10).Sum(x => x.U_MN);
                                    break;
                                case "N62711A6":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 11).Sum(x => x.U_MN);
                                    break;
                                case "N6272B007B1":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 13).Sum(x => x.U_MN);
                                    break;
                                case "N6278H002001":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 15).Sum(x => x.U_MN);
                                    break;

                                case "N6278H06A01":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 16).Sum(x => x.U_MN);
                                    break;
                                case "N6278H07A2":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 17).Sum(x => x.U_MN);
                                    break;
                                case "N6278H008A1":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 18).Sum(x => x.U_MN);
                                    break;
                                case "N6278H018A01":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 19).Sum(x => x.U_MN);
                                    break;
                                case "N6278H019A18":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 20).Sum(x => x.U_MN);
                                    break;
                                case "N6278H019A19":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 21).Sum(x => x.U_MN);
                                    break;
                                default:
                                    row4["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                                    break;
                            };
                            row4["DESCRIPTION"] = tableData.Rows[i][19].ToString();
                            row4["CREATE_BY"] = currentUser;
                            row4["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][5].ToString()) ? "0" : tableData.Rows[i][5].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()));
                            tableKHCP.Rows.Add(row4);


                            //VCA
                            var row5 = tableKHCP.NewRow();
                            row5["PKID"] = Guid.NewGuid().ToString();
                            row5["ORG_CODE"] = orgCode;
                            row5["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeVCA.CODE;
                            row5["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                            row5["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                            row5["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                            row5["VERSION"] = versionNext;
                            row5["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                            row5["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][6].ToString()) ? "0" : tableData.Rows[i][6].ToString());
                            //row5["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][19].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                            switch (elementCode)
                            {
                                case "N62711A1":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 4).Sum(x => x.U_MN);
                                    break;
                                case "N62711A2":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 6).Sum(x => x.U_MN);
                                    break;
                                case "N62711A3":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 7).Sum(x => x.U_MN);
                                    break;
                                case "N62711A4":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 8).Sum(x => x.U_MN);
                                    break;
                                case "N62711A5":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 9).Sum(x => x.U_MN);
                                    break;
                                case "N62711A7":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 10).Sum(x => x.U_MN);
                                    break;
                                case "N62711A6":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 11).Sum(x => x.U_MN);
                                    break;
                                case "N6272B007B1":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 13).Sum(x => x.U_MN);
                                    break;
                                case "N6278H002001":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 15).Sum(x => x.U_MN);
                                    break;

                                case "N6278H06A01":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 16).Sum(x => x.U_MN);
                                    break;
                                case "N6278H07A2":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 17).Sum(x => x.U_MN);
                                    break;
                                case "N6278H008A1":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 18).Sum(x => x.U_MN);
                                    break;
                                case "N6278H018A01":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 19).Sum(x => x.U_MN);
                                    break;
                                case "N6278H019A18":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 20).Sum(x => x.U_MN);
                                    break;
                                case "N6278H019A19":
                                    row5["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 21).Sum(x => x.U_MN);
                                    break;
                                default:
                                    row5["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                                    break;
                            };
                            row5["DESCRIPTION"] = tableData.Rows[i][19].ToString();
                            row5["CREATE_BY"] = currentUser;
                            row5["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][6].ToString()) ? "0" : tableData.Rows[i][6].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()));
                            tableKHCP.Rows.Add(row5);


                            //BMV
                            var row6 = tableKHCP.NewRow();
                            row6["PKID"] = Guid.NewGuid().ToString();
                            row6["ORG_CODE"] = orgCode;
                            row6["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeBMV.CODE;
                            row6["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                            row6["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                            row6["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                            row6["VERSION"] = versionNext;
                            row6["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                            row6["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString());
                            //row6["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                            switch (elementCode)
                            {
                                case "N62711A1":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 4).Sum(x => x.U_MN);
                                    break;
                                case "N62711A2":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 6).Sum(x => x.U_MN);
                                    break;
                                case "N62711A3":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 7).Sum(x => x.U_MN);
                                    break;
                                case "N62711A4":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 8).Sum(x => x.U_MN);
                                    break;
                                case "N62711A5":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 9).Sum(x => x.U_MN);
                                    break;
                                case "N62711A7":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 10).Sum(x => x.U_MN);
                                    break;
                                case "N62711A6":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 11).Sum(x => x.U_MN);
                                    break;
                                case "N6272B007B1":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 13).Sum(x => x.U_MN);
                                    break;
                                case "N6278H002001":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 15).Sum(x => x.U_MN);
                                    break;

                                case "N6278H06A01":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 16).Sum(x => x.U_MN);
                                    break;
                                case "N6278H07A2":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 17).Sum(x => x.U_MN);
                                    break;
                                case "N6278H008A1":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 18).Sum(x => x.U_MN);
                                    break;
                                case "N6278H018A01":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 19).Sum(x => x.U_MN);
                                    break;
                                case "N6278H019A18":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 20).Sum(x => x.U_MN);
                                    break;
                                case "N6278H019A19":
                                    row6["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 21).Sum(x => x.U_MN);
                                    break;
                                default:
                                    row6["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                                    break;
                            };
                            row6["DESCRIPTION"] = tableData.Rows[i][19].ToString();
                            row6["CREATE_BY"] = currentUser;
                            row6["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()));
                            tableKHCP.Rows.Add(row6);


                            //VCS
                            if (centerCodeVCS != null)
                            {
                                var row7 = tableKHCP.NewRow();
                                row7["PKID"] = Guid.NewGuid().ToString();
                                row7["ORG_CODE"] = orgCode;
                                row7["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeVCS.CODE;
                                row7["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                                row7["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                                row7["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                                row7["VERSION"] = versionNext;
                                row7["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                                row7["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString());
                                //row7["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                                switch (elementCode)
                                {
                                    case "N62711A1":
                                        row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 4).Sum(x => x.U_MN);
                                        break;
                                    case "N62711A2":
                                        row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 6).Sum(x => x.U_MN);
                                        break;
                                    case "N62711A3":
                                        row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 7).Sum(x => x.U_MN);
                                        break;
                                    case "N62711A4":
                                        row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 8).Sum(x => x.U_MN);
                                        break;
                                    case "N62711A5":
                                        row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 9).Sum(x => x.U_MN);
                                        break;
                                    case "N62711A7":
                                        row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 10).Sum(x => x.U_MN);
                                        break;
                                    case "N62711A6":
                                        row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 11).Sum(x => x.U_MN);
                                        break;
                                    case "N6272B007B1":
                                        row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 13).Sum(x => x.U_MN);
                                        break;
                                    case "N6278H002001":
                                        row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 15).Sum(x => x.U_MN);
                                        break;

                                    case "N6278H06A01":
                                        row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 16).Sum(x => x.U_MN);
                                        break;
                                    case "N6278H07A2":
                                        row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 17).Sum(x => x.U_MN);
                                        break;
                                    case "N6278H008A1":
                                        row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 18).Sum(x => x.U_MN);
                                        break;
                                    case "N6278H018A01":
                                        row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 19).Sum(x => x.U_MN);
                                        break;
                                    case "N6278H019A18":
                                        row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 20).Sum(x => x.U_MN);
                                        break;
                                    case "N6278H019A19":
                                        row7["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 21).Sum(x => x.U_MN);
                                        break;
                                    default:
                                        row7["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString());
                                        break;
                                };
                                row7["DESCRIPTION"] = tableData.Rows[i][19].ToString();
                                row7["CREATE_BY"] = currentUser;
                                row7["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][8].ToString()) ? "0" : tableData.Rows[i][8].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][10].ToString()) ? "0" : tableData.Rows[i][10].ToString()));
                                tableKHCP.Rows.Add(row7);
                            }


                        }
                        else if (template.DetailKeHoachChiPhi.Any(x => x.Center?.COST_CENTER_CODE == "100005"))
                        {

                            var str1 = tableData.Rows[i][2].ToString();
                            var str2 = tableData.Rows[i][3].ToString();
                            var str3 = tableData.Rows[i][4].ToString();
                            var str4 = tableData.Rows[i][5].ToString();

                            var str9 = tableData.Rows[i][7].ToString();
                            if (!decimal.TryParse(str1, out decimal value) && !string.IsNullOrEmpty(str1))
                            {
                                this.State = false;
                                this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                return;
                            }
                            if (!decimal.TryParse(str2, out decimal value1) && !string.IsNullOrEmpty(str2))
                            {
                                this.State = false;
                                this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                return;
                            }
                            if (!decimal.TryParse(str3, out decimal value2) && !string.IsNullOrEmpty(str3))
                            {
                                this.State = false;
                                this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                return;
                            }
                            if (!decimal.TryParse(str4, out decimal value3) && !string.IsNullOrEmpty(str4))
                            {
                                this.State = false;
                                this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                return;
                            }
                            if (!decimal.TryParse(str9, out decimal value8) && !string.IsNullOrEmpty(str9))
                            {
                                this.State = false;
                                this.ErrorMessage = $"Sai định dạng ở dòng thứ {i + 1}";
                                return;
                            }

                            var centerCodeVPCN = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VPCN" && x.COST_CENTER_CODE == "100005");
                            var centerCodeVTMB = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VTMB" && x.COST_CENTER_CODE == "100005");
                            var centerCodeVTMT = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VTMT" && x.COST_CENTER_CODE == "100005");
                            var centerCodeVTMN = allChiPhiProfitCenters.FirstOrDefault(x => x.SAN_BAY_CODE == "VTMN" && x.COST_CENTER_CODE == "100005");
                            if (centerCodeVPCN == null || centerCodeVTMB == null || centerCodeVTMT == null || centerCodeVTMN == null)
                            {
                                throw new Exception($"Định dạng file không đúng hoặc có lỗi hệ thống xảy ra! Vui lòng liên hệ với quản trị viên!");
                            }

                            //VPCN
                            var row1 = tableKHCP.NewRow();
                            row1["PKID"] = Guid.NewGuid().ToString();
                            row1["ORG_CODE"] = orgCode;
                            row1["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeVPCN.CODE;
                            row1["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                            row1["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                            row1["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                            row1["VERSION"] = versionNext;
                            row1["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                            row1["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][2].ToString()) ? "0" : tableData.Rows[i][2].ToString());
                            //row1["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString());
                            switch (elementCode)
                            {
                                case "VT62711A001":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 4).Sum(x => x.U_VT);
                                    break;
                                case "VT62711A002":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 6).Sum(x => x.U_VT);
                                    break;
                                case "VT62711A003":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 7).Sum(x => x.U_VT);
                                    break;
                                case "VT62711A004":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 8).Sum(x => x.U_VT);
                                    break;
                                case "VT62711A005":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 9).Sum(x => x.U_VT);
                                    break;
                                case "VT62711A007":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 10).Sum(x => x.U_VT);
                                    break;
                                case "VT62711A006":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 11).Sum(x => x.U_VT);
                                    break;
                                case "VT6272B007A":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 13).Sum(x => x.U_VT);
                                    break;
                                case "VT6278H002A1":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 15).Sum(x => x.U_VT);
                                    break;

                                case "VT6278H0062":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 16).Sum(x => x.U_VT);
                                    break;
                                case "VT6278H007A7":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 17).Sum(x => x.U_VT);
                                    break;
                                case "VT6278H008A1":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 18).Sum(x => x.U_VT);
                                    break;
                                case "VT6278H018A1":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 19).Sum(x => x.U_VT);
                                    break;
                                case "VT6278H019A11":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 20).Sum(x => x.U_VT);
                                    break;
                                case "VT6278H019A12":
                                    row1["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 21).Sum(x => x.U_MN);
                                    break;
                                default:
                                    row1["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString());
                                    break;
                            };
                            row1["DESCRIPTION"] = tableData.Rows[i][13].ToString();
                            row1["CREATE_BY"] = currentUser;
                            row1["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][2].ToString()) ? "0" : tableData.Rows[i][2].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString()));
                            tableKHCP.Rows.Add(row1);


                            //VTMB
                            var row2 = tableKHCP.NewRow();
                            row2["PKID"] = Guid.NewGuid().ToString();
                            row2["ORG_CODE"] = orgCode;
                            row2["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeVTMB.CODE;
                            row2["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                            row2["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                            row2["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                            row2["VERSION"] = versionNext;
                            row2["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                            row2["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][3].ToString()) ? "0" : tableData.Rows[i][3].ToString());
                            //row2["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString());
                            switch (elementCode)
                            {
                                case "VT62711A001":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 4).Sum(x => x.U_VT);
                                    break;
                                case "VT62711A002":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 6).Sum(x => x.U_VT);
                                    break;
                                case "VT62711A003":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 7).Sum(x => x.U_VT);
                                    break;
                                case "VT62711A004":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 8).Sum(x => x.U_VT);
                                    break;
                                case "VT62711A005":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 9).Sum(x => x.U_VT);
                                    break;
                                case "VT62711A007":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 10).Sum(x => x.U_VT);
                                    break;
                                case "VT62711A006":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 11).Sum(x => x.U_VT);
                                    break;
                                case "VT6272B007A":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 13).Sum(x => x.U_VT);
                                    break;
                                case "VT6278H002A1":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 15).Sum(x => x.U_VT);
                                    break;

                                case "VT6278H0062":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 16).Sum(x => x.U_VT);
                                    break;
                                case "VT6278H007A7":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 17).Sum(x => x.U_VT);
                                    break;
                                case "VT6278H008A1":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 18).Sum(x => x.U_VT);
                                    break;
                                case "VT6278H018A1":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 19).Sum(x => x.U_VT);
                                    break;
                                case "VT6278H019A11":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 20).Sum(x => x.U_VT);
                                    break;
                                case "VT6278H019A12":
                                    row2["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 21).Sum(x => x.U_MN);
                                    break;
                                default:
                                    row2["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString());
                                    break;
                            };
                            row2["DESCRIPTION"] = tableData.Rows[i][13].ToString();
                            row2["CREATE_BY"] = currentUser;
                            row2["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][3].ToString()) ? "0" : tableData.Rows[i][3].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString()));
                            tableKHCP.Rows.Add(row2);


                            //VTMT
                            var row3 = tableKHCP.NewRow();
                            row3["PKID"] = Guid.NewGuid().ToString();
                            row3["ORG_CODE"] = orgCode;
                            row3["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeVTMT.CODE;
                            row3["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                            row3["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                            row3["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                            row3["VERSION"] = versionNext;
                            row3["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                            row3["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][4].ToString()) ? "0" : tableData.Rows[i][4].ToString());
                            //row3["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString());
                            switch (elementCode)
                            {
                                case "VT62711A001":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 4).Sum(x => x.U_VT);
                                    break;
                                case "VT62711A002":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 6).Sum(x => x.U_VT);
                                    break;
                                case "VT62711A003":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 7).Sum(x => x.U_VT);
                                    break;
                                case "VT62711A004":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 8).Sum(x => x.U_VT);
                                    break;
                                case "VT62711A005":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 9).Sum(x => x.U_VT);
                                    break;
                                case "VT62711A007":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 10).Sum(x => x.U_VT);
                                    break;
                                case "VT62711A006":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 11).Sum(x => x.U_VT);
                                    break;
                                case "VT6272B007A":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 13).Sum(x => x.U_VT);
                                    break;
                                case "VT6278H002A1":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 15).Sum(x => x.U_VT);
                                    break;

                                case "VT6278H0062":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 16).Sum(x => x.U_VT);
                                    break;
                                case "VT6278H007A7":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 17).Sum(x => x.U_VT);
                                    break;
                                case "VT6278H008A1":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 18).Sum(x => x.U_VT);
                                    break;
                                case "VT6278H018A1":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 19).Sum(x => x.U_VT);
                                    break;
                                case "VT6278H019A11":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 20).Sum(x => x.U_VT);
                                    break;
                                case "VT6278H019A12":
                                    row3["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 21).Sum(x => x.U_MN);
                                    break;
                                default:
                                    row3["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString());
                                    break;
                            };
                            row3["DESCRIPTION"] = tableData.Rows[i][13].ToString();
                            row3["CREATE_BY"] = currentUser;
                            row3["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][4].ToString()) ? "0" : tableData.Rows[i][4].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString()));
                            tableKHCP.Rows.Add(row3);


                            //VTMN
                            var row4 = tableKHCP.NewRow();
                            row4["PKID"] = Guid.NewGuid().ToString();
                            row4["ORG_CODE"] = orgCode;
                            row4["CHI_PHI_PROFIT_CENTER_CODE"] = centerCodeVPCN.CODE;
                            row4["TEMPLATE_CODE"] = ObjDetail.TEMPLATE_CODE;
                            row4["TIME_YEAR"] = ObjDetail.TIME_YEAR;
                            row4["STATUS"] = Approve_Status.ChuaTrinhDuyet;
                            row4["VERSION"] = versionNext;
                            row4["KHOAN_MUC_HANG_HOA_CODE"] = elementCode;
                            row4["QUANTITY"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][5].ToString()) ? "0" : tableData.Rows[i][5].ToString());
                            //row4["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString());
                            switch (elementCode)
                            {
                                case "VT62711A001":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 4).Sum(x => x.U_VT);
                                    break;
                                case "VT62711A002":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 6).Sum(x => x.U_VT);
                                    break;
                                case "VT62711A003":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 7).Sum(x => x.U_VT);
                                    break;
                                case "VT62711A004":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 8).Sum(x => x.U_VT);
                                    break;
                                case "VT62711A005":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 9).Sum(x => x.U_VT);
                                    break;
                                case "VT62711A007":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 10).Sum(x => x.U_VT);
                                    break;
                                case "VT62711A006":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 11).Sum(x => x.U_VT);
                                    break;
                                case "VT6272B007A":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 13).Sum(x => x.U_VT);
                                    break;
                                case "VT6278H002A1":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 15).Sum(x => x.U_VT);
                                    break;

                                case "VT6278H0062":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 16).Sum(x => x.U_VT);
                                    break;
                                case "VT6278H007A7":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 17).Sum(x => x.U_VT);
                                    break;
                                case "VT6278H008A1":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 18).Sum(x => x.U_VT);
                                    break;
                                case "VT6278H018A1":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 19).Sum(x => x.U_VT);
                                    break;
                                case "VT6278H019A11":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 20).Sum(x => x.U_VT);
                                    break;
                                case "VT6278H019A12":
                                    row4["PRICE"] = datatcnl.Where(x => x.idcenter.C_ORDER == 21).Sum(x => x.U_MN);
                                    break;
                                default:
                                    row4["PRICE"] = Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString());
                                    break;
                            };
                            row4["DESCRIPTION"] = tableData.Rows[i][13].ToString();
                            row4["CREATE_BY"] = currentUser;
                            row4["AMOUNT"] = (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][5].ToString()) ? "0" : tableData.Rows[i][5].ToString())) * (Convert.ToDecimal(string.IsNullOrEmpty(tableData.Rows[i][7].ToString()) ? "0" : tableData.Rows[i][7].ToString()));
                            tableKHCP.Rows.Add(row4);

                        }
                        else
                        {
                            throw new Exception($"Định dạng file không đúng hoặc có lỗi hệ thống xảy ra! Vui lòng liên hệ với quản trị viên!");
                        }
                    }

                }
                #endregion


    
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SMO_MSSQL_Connection"].ConnectionString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connection))
                    {
                        sqlBulkCopy.DestinationTableName = "dbo.T_BP_KE_HOACH_CHI_PHI_DATA";
                        sqlBulkCopy.ColumnMappings.Add("PKID", "PKID");
                        sqlBulkCopy.ColumnMappings.Add("ORG_CODE", "ORG_CODE");
                        sqlBulkCopy.ColumnMappings.Add("CHI_PHI_PROFIT_CENTER_CODE", "CHI_PHI_PROFIT_CENTER_CODE");
                        sqlBulkCopy.ColumnMappings.Add("TEMPLATE_CODE", "TEMPLATE_CODE");
                        sqlBulkCopy.ColumnMappings.Add("TIME_YEAR", "TIME_YEAR");
                        sqlBulkCopy.ColumnMappings.Add("MONTH", "MONTH");
                        sqlBulkCopy.ColumnMappings.Add("STATUS", "STATUS");
                        sqlBulkCopy.ColumnMappings.Add("VERSION", "VERSION");
                        sqlBulkCopy.ColumnMappings.Add("KHOAN_MUC_HANG_HOA_CODE", "KHOAN_MUC_HANG_HOA_CODE");
                        sqlBulkCopy.ColumnMappings.Add("QUANTITY", "QUANTITY");
                        sqlBulkCopy.ColumnMappings.Add("PRICE", "PRICE");
                        sqlBulkCopy.ColumnMappings.Add("DESCRIPTION", "DESCRIPTION");
                        sqlBulkCopy.ColumnMappings.Add("CREATE_BY", "CREATE_BY");
                        sqlBulkCopy.ColumnMappings.Add("AMOUNT", "AMOUNT");
                        sqlBulkCopy.BatchSize = 2000;
                        connection.Open();
                        sqlBulkCopy.WriteToServer(tableKHCP);
                    }
                };
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
                        COST_CENTER_CODE = tableData.Rows[i][2].ToString().Trim(),
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
            return UnitOfWork.GetSession().Query<T_BP_KE_HOACH_CHI_PHI_DATA>().Where(x => lstOrgCodes.Contains(x.ORG_CODE) && x.TEMPLATE_CODE != string.Empty && (!year.HasValue || x.TIME_YEAR == year.Value)).Select(x => x.TEMPLATE_CODE)
                .Distinct()
                .ToList();
            //return UnitOfWork.Repository<KeHoachChiPhiDataRepo>()
            //    .GetManyByExpression(x => lstOrgCodes.Contains(x.ORG_CODE) && x.TEMPLATE_CODE != string.Empty && (!year.HasValue || x.TIME_YEAR == year.Value))
            //    .Select(x => x.TEMPLATE_CODE)
            //    .Distinct()
            //    .ToList();
        }

        private IList<string> GetTemplateDataHistory(List<string> lstOrgCodes, int? year = null)
        {
            return UnitOfWork.GetSession().Query<T_BP_KE_HOACH_CHI_PHI_DATA_HISTORY>().Where(x => lstOrgCodes.Contains(x.ORG_CODE) && x.TEMPLATE_CODE != string.Empty && (!year.HasValue || x.TIME_YEAR == year.Value)).Select(x => x.TEMPLATE_CODE)
                .Distinct()
                .ToList();

            //return UnitOfWork.Repository<KeHoachChiPhiDataHistoryRepo>()
            //    .GetManyByExpression(x => lstOrgCodes.Contains(x.ORG_CODE) && x.TEMPLATE_CODE != string.Empty && (!year.HasValue || x.TIME_YEAR == year.Value))
            //    .Select(x => x.TEMPLATE_CODE)
            //    .Distinct()
            //    .ToList();
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
                int NUM_CELL = 7;

                //Style cần dùng

                ICellStyle styleCellNumber = templateWorkbook.CreateCellStyle();
                styleCellNumber.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,##0");

                ICellStyle styleCellBold = templateWorkbook.CreateCellStyle();
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

                var rowHeader3 = ReportUtilities.CreateRow(ref sheet, 2, NUM_CELL);

                ReportUtilities.CreateCell(ref rowHeader3, NUM_CELL);
                rowHeader3.Cells[1].SetCellValue(template.CREATE_BY);
                #endregion

                #region Details

                numRowCur = 8;
                var number = 1;
                var data = dataOtherCost.GroupBy(x => x.CODE).Select(x => x.First()).OrderBy(x=>x.C_ORDER).ToList();

                foreach (var item in data)
                {
                    IRow rowCur = ReportUtilities.CreateRow(ref sheet, numRowCur, NUM_CELL);

                    rowCur.Cells[0].SetCellValue(item.CODE);
                    rowCur.Cells[1].SetCellValue(item.NAME);

                    if (item.IS_GROUP)
                    {
                        rowCur.Cells[0].CellStyle = styleCellBold;
                        rowCur.Cells[0].CellStyle.SetFont(fontBold);
                        rowCur.Cells[1].CellStyle = styleCellBold;
                        rowCur.Cells[1].CellStyle.SetFont(fontBold);
                    }
                    numRowCur++;
                    number++;
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

        private ICellStyle GetCellStyleNumber(IWorkbook templateWorkbook)
        {
            ICellStyle styleCellNumber = templateWorkbook.CreateCellStyle();
            styleCellNumber.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");
            return styleCellNumber;
        }
        public void GenerateData(ref MemoryStream outFileStream, string path, ViewDataCenterModel model, string orgCode, List<string>lstSanBay)
        {
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
                int NUM_CELL = 22;

                //Style cần dùng

                var styleCellNumber = GetCellStyleNumber(templateWorkbook);

                ICellStyle styleCellBold = templateWorkbook.CreateCellStyle();
                styleCellBold.WrapText = true;
                var fontBold = templateWorkbook.CreateFont();
                fontBold.Boldweight = (short)FontBoldWeight.Bold;
                fontBold.FontHeightInPoints = 11;
                fontBold.FontName = "Times New Roman";

                #region Header
                var template = UnitOfWork.Repository<TemplateRepo>().Get(model.TEMPLATE_CODE);
                var rowHeader1 = ReportUtilities.CreateRow(ref sheet, 0, NUM_CELL);
                ReportUtilities.CreateCell(ref rowHeader1, NUM_CELL);
                rowHeader1.Cells[0].SetCellValue(rowHeader1.Cells[0].StringCellValue + $" {template.Organize?.Parent?.NAME.ToUpper()}");

                var rowHeader2 = ReportUtilities.CreateRow(ref sheet, 1, NUM_CELL);

                ReportUtilities.CreateCell(ref rowHeader2, NUM_CELL);
                rowHeader2.Cells[0].SetCellValue($"{template.Organize.NAME}");
                rowHeader2.Cells[2].SetCellValue(template.TITLE.ToUpper());

                var rowHeader3 = ReportUtilities.CreateRow(ref sheet, 2, NUM_CELL);

                ReportUtilities.CreateCell(ref rowHeader3, NUM_CELL);
                rowHeader3.Cells[1].SetCellValue(template.CREATE_BY);
                #endregion

                #region Details

                numRowCur = 8;
                var number = 1;

                var data = GetData(model, lstSanBay);

                switch (orgCode)
                {
                    case "100001":
                        foreach (var item in data.OrderBy(x => x.CODE).GroupBy(x => x.CODE).Select(x => x.First()))
                        {
                            IRow rowCur = ReportUtilities.CreateRow(ref sheet, numRowCur, NUM_CELL);

                            rowCur.Cells[0].SetCellValue(item.CODE);
                            rowCur.Cells[1].SetCellValue(item.NAME);

                            var VPCT = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VPCT" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[2].SetCellValue(string.IsNullOrEmpty(VPCT) ? 0 : Convert.ToDouble(VPCT));
                            rowCur.Cells[2].CellStyle = styleCellNumber;

                            var MB = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "MB" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[3].SetCellValue(string.IsNullOrEmpty(MB) ? 0 : Convert.ToDouble(MB));
                            rowCur.Cells[3].CellStyle = styleCellNumber;

                            var MT = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "MT" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[4].SetCellValue(string.IsNullOrEmpty(MT) ? 0 : Convert.ToDouble(MT));
                            rowCur.Cells[4].CellStyle = styleCellNumber;

                            var CR = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "CR" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[5].SetCellValue(string.IsNullOrEmpty(CR) ? 0 : Convert.ToDouble(CR));
                            rowCur.Cells[5].CellStyle = styleCellNumber;

                            var MN = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "MN" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[6].SetCellValue(string.IsNullOrEmpty(MN) ? 0 : Convert.ToDouble(MN));
                            rowCur.Cells[6].CellStyle = styleCellNumber;

                            var sumQuantity = data.Where(x => x.CODE == item.CODE).Sum(x => x.Values[0]);
                            rowCur.Cells[7].SetCellValue(Convert.ToDouble(sumQuantity));
                            rowCur.Cells[7].CellStyle = styleCellNumber;

                            var price = data.FirstOrDefault(x => x.CENTER_CODE == item.CENTER_CODE && x.CODE == item.CODE)?.Values[1];
                            rowCur.Cells[8].SetCellValue(price == 0 || price == null ? 0 : Convert.ToDouble(price));
                            rowCur.Cells[8].CellStyle = styleCellNumber;

                            var totalVPCT = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VPCT" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[9].SetCellValue(string.IsNullOrEmpty(totalVPCT) ? 0 : Convert.ToDouble(totalVPCT));
                            rowCur.Cells[9].CellStyle = styleCellNumber;

                            var totalMB = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "MB" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[10].SetCellValue(string.IsNullOrEmpty(totalMB) ? 0 : Convert.ToDouble(totalMB));
                            rowCur.Cells[10].CellStyle = styleCellNumber;

                            var totalMT = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "MT" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[11].SetCellValue(string.IsNullOrEmpty(totalMT) ? 0 : Convert.ToDouble(totalMT));
                            rowCur.Cells[11].CellStyle = styleCellNumber;

                            var totalCR = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "CR" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[12].SetCellValue(string.IsNullOrEmpty(totalCR) ? 0 : Convert.ToDouble(totalCR));
                            rowCur.Cells[12].CellStyle = styleCellNumber;

                            var totalMN = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "MN" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[13].SetCellValue(string.IsNullOrEmpty(totalMN) ? 0 : Convert.ToDouble(totalMN));
                            rowCur.Cells[13].CellStyle = styleCellNumber;

                            var sumTotal = data.Where(x => x.CODE == item.CODE).Sum(x => x.Values[2]);
                            rowCur.Cells[14].SetCellValue(Convert.ToDouble(sumTotal));
                            rowCur.Cells[14].CellStyle = styleCellNumber;

                            if (item.IS_GROUP)
                            {
                                rowCur.Cells[0].CellStyle = styleCellBold;
                                rowCur.Cells[0].CellStyle.SetFont(fontBold);
                                rowCur.Cells[1].CellStyle = styleCellBold;
                                rowCur.Cells[1].CellStyle.SetFont(fontBold);
                            }
                            numRowCur++;
                            number++;
                        }
                        break;
                    case "100002":
                        foreach (var item in data.OrderBy(x => x.CODE).GroupBy(x => x.CODE).Select(x => x.First()))
                        {
                            IRow rowCur = ReportUtilities.CreateRow(ref sheet, numRowCur, NUM_CELL);

                            rowCur.Cells[0].SetCellValue(item.CODE);
                            rowCur.Cells[1].SetCellValue(item.NAME);

                            var VPCN = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VPCN" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[2].SetCellValue(string.IsNullOrEmpty(VPCN) ? 0 : Convert.ToDouble(VPCN));
                            rowCur.Cells[2].CellStyle = styleCellNumber;

                            var HAN = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "HAN" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[3].SetCellValue(string.IsNullOrEmpty(HAN) ? 0 : Convert.ToDouble(HAN));
                            rowCur.Cells[3].CellStyle = styleCellNumber;

                            var HPH = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "HPH" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[4].SetCellValue(string.IsNullOrEmpty(HPH) ? 0 : Convert.ToDouble(HPH));
                            rowCur.Cells[4].CellStyle = styleCellNumber;

                            var THD = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "THD" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[5].SetCellValue(string.IsNullOrEmpty(THD) ? 0 : Convert.ToDouble(THD));
                            rowCur.Cells[5].CellStyle = styleCellNumber;

                            var VII = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VII" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[6].SetCellValue(string.IsNullOrEmpty(VII) ? 0 : Convert.ToDouble(VII));
                            rowCur.Cells[6].CellStyle = styleCellNumber;

                            var VDH = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VDH" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[7].SetCellValue(string.IsNullOrEmpty(VDH) ? 0 : Convert.ToDouble(VDH));
                            rowCur.Cells[7].CellStyle = styleCellNumber;

                            var VDO = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VDO" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[8].SetCellValue(string.IsNullOrEmpty(VDO) ? 0 : Convert.ToDouble(VDO));
                            rowCur.Cells[8].CellStyle = styleCellNumber;

                            var sumQuantity = data.Where(x => x.CODE == item.CODE).Sum(x => x.Values[0]);
                            rowCur.Cells[9].SetCellValue(Convert.ToDouble(sumQuantity));
                            rowCur.Cells[9].CellStyle = styleCellNumber;

                            var price = data.FirstOrDefault(x => x.CENTER_CODE == item.CENTER_CODE && x.CODE == item.CODE)?.Values[1];
                            rowCur.Cells[10].SetCellValue(price == 0 || price == null ? 0 : Convert.ToDouble(price));
                            rowCur.Cells[10].CellStyle = styleCellNumber;

                            var totalVPCN = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VPCN" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[11].SetCellValue(string.IsNullOrEmpty(totalVPCN) ? 0 : Convert.ToDouble(totalVPCN));
                            rowCur.Cells[11].CellStyle = styleCellNumber;

                            var totalHAN = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "HAN" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[12].SetCellValue(string.IsNullOrEmpty(totalHAN) ? 0 : Convert.ToDouble(totalHAN));
                            rowCur.Cells[12].CellStyle = styleCellNumber;

                            var totalHPH = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "HPH" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[13].SetCellValue(string.IsNullOrEmpty(totalHPH) ? 0 : Convert.ToDouble(totalHPH));
                            rowCur.Cells[13].CellStyle = styleCellNumber;

                            var totalTHD = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "THD" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[14].SetCellValue(string.IsNullOrEmpty(totalTHD) ? 0 : Convert.ToDouble(totalTHD));
                            rowCur.Cells[14].CellStyle = styleCellNumber;

                            var totalVII = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VII" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[15].SetCellValue(string.IsNullOrEmpty(totalVII) ? 0 : Convert.ToDouble(totalVII));
                            rowCur.Cells[15].CellStyle = styleCellNumber;

                            var totalVDH = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VDH" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[16].SetCellValue(string.IsNullOrEmpty(totalVDH) ? 0 : Convert.ToDouble(totalVDH));
                            rowCur.Cells[16].CellStyle = styleCellNumber;

                            var totalVDO = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VDO" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[17].SetCellValue(string.IsNullOrEmpty(totalVDO) ? 0 : Convert.ToDouble(totalVDO));
                            rowCur.Cells[17].CellStyle = styleCellNumber;

                            var sumTotal = data.Where(x => x.CODE == item.CODE).Sum(x => x.Values[2]);
                            rowCur.Cells[18].SetCellValue(Convert.ToDouble(sumTotal));
                            rowCur.Cells[18].CellStyle = styleCellNumber;

                            if (item.IS_GROUP)
                            {
                                rowCur.Cells[0].CellStyle = styleCellBold;
                                rowCur.Cells[0].CellStyle.SetFont(fontBold);
                                rowCur.Cells[1].CellStyle = styleCellBold;
                                rowCur.Cells[1].CellStyle.SetFont(fontBold);
                            }
                            numRowCur++;
                            number++;
                        }
                        break;
                    case "100003":
                        foreach (var item in data.OrderBy(x => x.CODE).GroupBy(x => x.CODE).Select(x => x.First()))
                        {
                            IRow rowCur = ReportUtilities.CreateRow(ref sheet, numRowCur, NUM_CELL);

                            rowCur.Cells[0].SetCellValue(item.CODE);
                            rowCur.Cells[1].SetCellValue(item.NAME);

                            var VPCN = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VPCN" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[2].SetCellValue(string.IsNullOrEmpty(VPCN) ? 0 : Convert.ToDouble(VPCN));
                            rowCur.Cells[2].CellStyle = styleCellNumber;

                            var DAD = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "DAD" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[3].SetCellValue(string.IsNullOrEmpty(DAD) ? 0 : Convert.ToDouble(DAD));
                            rowCur.Cells[3].CellStyle = styleCellNumber;

                            var CXR = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "CXR" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[4].SetCellValue(string.IsNullOrEmpty(CXR) ? 0 : Convert.ToDouble(CXR));
                            rowCur.Cells[4].CellStyle = styleCellNumber;

                            var HUI = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "HUI" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[5].SetCellValue(string.IsNullOrEmpty(HUI) ? 0 : Convert.ToDouble(HUI));
                            rowCur.Cells[5].CellStyle = styleCellNumber;

                            var UIH = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "UIH" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[6].SetCellValue(string.IsNullOrEmpty(UIH) ? 0 : Convert.ToDouble(UIH));
                            rowCur.Cells[6].CellStyle = styleCellNumber;

                            var VCL = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VCL" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[7].SetCellValue(string.IsNullOrEmpty(VCL) ? 0 : Convert.ToDouble(VCL));
                            rowCur.Cells[7].CellStyle = styleCellNumber;

                            var PXU = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "PXU" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[8].SetCellValue(string.IsNullOrEmpty(PXU) ? 0 : Convert.ToDouble(PXU));
                            rowCur.Cells[8].CellStyle = styleCellNumber;

                            var TBB = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "TBB" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[9].SetCellValue(string.IsNullOrEmpty(TBB) ? 0 : Convert.ToDouble(TBB));
                            rowCur.Cells[9].CellStyle = styleCellNumber;

                            var sumQuantity = data.Where(x => x.CODE == item.CODE).Sum(x => x.Values[0]);
                            rowCur.Cells[10].SetCellValue(Convert.ToDouble(sumQuantity));
                            rowCur.Cells[10].CellStyle = styleCellNumber;

                            var price = data.FirstOrDefault(x => x.CENTER_CODE == item.CENTER_CODE && x.CODE == item.CODE)?.Values[1];
                            rowCur.Cells[11].SetCellValue(price == 0 || price == null ? 0 : Convert.ToDouble(price));
                            rowCur.Cells[11].CellStyle = styleCellNumber;

                            var totalVPCN = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VPCN" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[12].SetCellValue(string.IsNullOrEmpty(totalVPCN) ? 0 : Convert.ToDouble(totalVPCN));
                            rowCur.Cells[12].CellStyle = styleCellNumber;

                            var totalDAD = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "DAD" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[13].SetCellValue(string.IsNullOrEmpty(totalDAD) ? 0 : Convert.ToDouble(totalDAD));
                            rowCur.Cells[13].CellStyle = styleCellNumber;

                            var totalCXR = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "CXR" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[14].SetCellValue(string.IsNullOrEmpty(totalCXR) ? 0 : Convert.ToDouble(totalCXR));
                            rowCur.Cells[14].CellStyle = styleCellNumber;

                            var totalHUI = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "HUI" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[15].SetCellValue(string.IsNullOrEmpty(totalHUI) ? 0 : Convert.ToDouble(totalHUI));
                            rowCur.Cells[15].CellStyle = styleCellNumber;

                            var totalUIH = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "UIH" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[16].SetCellValue(string.IsNullOrEmpty(totalUIH) ? 0 : Convert.ToDouble(totalUIH));
                            rowCur.Cells[16].CellStyle = styleCellNumber;

                            var totalVCL = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VCL" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[17].SetCellValue(string.IsNullOrEmpty(totalVCL) ? 0 : Convert.ToDouble(totalVCL));
                            rowCur.Cells[17].CellStyle = styleCellNumber;

                            var totalPXU = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "PXU" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[18].SetCellValue(string.IsNullOrEmpty(totalPXU) ? 0 : Convert.ToDouble(totalPXU));
                            rowCur.Cells[18].CellStyle = styleCellNumber;

                            var totalTBB = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "TBB" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[19].SetCellValue(string.IsNullOrEmpty(totalTBB) ? 0 : Convert.ToDouble(totalTBB));
                            rowCur.Cells[19].CellStyle = styleCellNumber;

                            var sumTotal = data.Where(x => x.CODE == item.CODE).Sum(x => x.Values[2]);
                            rowCur.Cells[20].SetCellValue(Convert.ToDouble(sumTotal));
                            rowCur.Cells[20].CellStyle = styleCellNumber;

                            if (item.IS_GROUP)
                            {
                                rowCur.Cells[0].CellStyle = styleCellBold;
                                rowCur.Cells[0].CellStyle.SetFont(fontBold);
                                rowCur.Cells[1].CellStyle = styleCellBold;
                                rowCur.Cells[1].CellStyle.SetFont(fontBold);
                            }
                            numRowCur++;
                            number++;
                        }
                        break;
                    case "100004":
                        foreach (var item in data.OrderBy(x => x.CODE).GroupBy(x => x.CODE).Select(x => x.First()))
                        {
                            IRow rowCur = ReportUtilities.CreateRow(ref sheet, numRowCur, NUM_CELL);

                            rowCur.Cells[0].SetCellValue(item.CODE);
                            rowCur.Cells[1].SetCellValue(item.NAME);

                            var VPCN = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VPCN" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[2].SetCellValue(string.IsNullOrEmpty(VPCN) ? 0 : Convert.ToDouble(VPCN));
                            rowCur.Cells[2].CellStyle = styleCellNumber;

                            var SGN = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "SGN" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[3].SetCellValue(string.IsNullOrEmpty(SGN) ? 0 : Convert.ToDouble(SGN));
                            rowCur.Cells[3].CellStyle = styleCellNumber;

                            var PQC = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "PQC" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[4].SetCellValue(string.IsNullOrEmpty(PQC) ? 0 : Convert.ToDouble(PQC));
                            rowCur.Cells[4].CellStyle = styleCellNumber;

                            var DLI = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "DLI" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[5].SetCellValue(string.IsNullOrEmpty(DLI) ? 0 : Convert.ToDouble(DLI));
                            rowCur.Cells[5].CellStyle = styleCellNumber;

                            var VCA = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VCA" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[6].SetCellValue(string.IsNullOrEmpty(VCA) ? 0 : Convert.ToDouble(VCA));
                            rowCur.Cells[6].CellStyle = styleCellNumber;

                            var BMV = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "BMV" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[7].SetCellValue(string.IsNullOrEmpty(BMV) ? 0 : Convert.ToDouble(BMV));
                            rowCur.Cells[7].CellStyle = styleCellNumber;

                            var VCS = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VCS" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[8].SetCellValue(string.IsNullOrEmpty(VCS) ? 0 : Convert.ToDouble(VCS));
                            rowCur.Cells[8].CellStyle = styleCellNumber;

                            var sumQuantity = data.Where(x => x.CODE == item.CODE).Sum(x => x.Values[0]);
                            rowCur.Cells[9].SetCellValue(Convert.ToDouble(sumQuantity));
                            rowCur.Cells[9].CellStyle = styleCellNumber;

                            var price = data.FirstOrDefault(x => x.CENTER_CODE == item.CENTER_CODE && x.CODE == item.CODE)?.Values[1];
                            rowCur.Cells[10].SetCellValue(price == 0 || price == null ? 0 : Convert.ToDouble(price));
                            rowCur.Cells[10].CellStyle = styleCellNumber;

                            var totalVPCN = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VPCN" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[11].SetCellValue(string.IsNullOrEmpty(totalVPCN) ? 0 : Convert.ToDouble(totalVPCN));
                            rowCur.Cells[11].CellStyle = styleCellNumber;

                            var totalSGN = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "SGN" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[12].SetCellValue(string.IsNullOrEmpty(totalSGN) ? 0 : Convert.ToDouble(totalSGN));
                            rowCur.Cells[12].CellStyle = styleCellNumber;

                            var totalPQC = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "PQC" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[13].SetCellValue(string.IsNullOrEmpty(totalPQC) ? 0 : Convert.ToDouble(totalPQC));
                            rowCur.Cells[13].CellStyle = styleCellNumber;

                            var totalDLI = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "DLI" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[14].SetCellValue(string.IsNullOrEmpty(totalDLI) ? 0 : Convert.ToDouble(totalDLI));
                            rowCur.Cells[14].CellStyle = styleCellNumber;

                            var totalVCA = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VCA" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[15].SetCellValue(string.IsNullOrEmpty(totalVCA) ? 0 : Convert.ToDouble(totalVCA));
                            rowCur.Cells[15].CellStyle = styleCellNumber;

                            var totalBMV = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "BMV" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[16].SetCellValue(string.IsNullOrEmpty(totalBMV) ? 0 : Convert.ToDouble(totalBMV));
                            rowCur.Cells[16].CellStyle = styleCellNumber;

                            var totalVCS = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VCS" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[17].SetCellValue(string.IsNullOrEmpty(totalVCS) ? 0 : Convert.ToDouble(totalVCS));
                            rowCur.Cells[17].CellStyle = styleCellNumber;

                            var sumTotal = data.Where(x => x.CODE == item.CODE).Sum(x => x.Values[2]);
                            rowCur.Cells[18].SetCellValue(Convert.ToDouble(sumTotal));
                            rowCur.Cells[18].CellStyle = styleCellNumber;

                            if (item.IS_GROUP)
                            {
                                rowCur.Cells[0].CellStyle = styleCellBold;
                                rowCur.Cells[0].CellStyle.SetFont(fontBold);
                                rowCur.Cells[1].CellStyle = styleCellBold;
                                rowCur.Cells[1].CellStyle.SetFont(fontBold);
                            }
                            numRowCur++;
                            number++;
                        }
                        break;
                    case "100005":

                        foreach (var item in data.OrderBy(x => x.CODE).GroupBy(x => x.CODE).Select(x => x.First()))
                        {
                            IRow rowCur = ReportUtilities.CreateRow(ref sheet, numRowCur, NUM_CELL);

                            rowCur.Cells[0].SetCellValue(item.CODE);
                            rowCur.Cells[1].SetCellValue(item.NAME);

                            var VPCN = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VPCN" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[2].SetCellValue(string.IsNullOrEmpty(VPCN) ? 0 : Convert.ToDouble(VPCN));
                            rowCur.Cells[2].CellStyle = styleCellNumber;

                            var VTMB = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VTMB" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[3].SetCellValue(string.IsNullOrEmpty(VTMB) ? 0 : Convert.ToDouble(VTMB));
                            rowCur.Cells[3].CellStyle = styleCellNumber;

                            var VTMT = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VTMT" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[4].SetCellValue(string.IsNullOrEmpty(VTMT) ? 0 : Convert.ToDouble(VTMT));
                            rowCur.Cells[4].CellStyle = styleCellNumber;

                            var VTMN = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VTMN" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[5].SetCellValue(string.IsNullOrEmpty(VTMN) ? 0 : Convert.ToDouble(VTMN));
                            rowCur.Cells[5].CellStyle = styleCellNumber;
                           
                            var sumQuantity = data.Where(x => x.CODE == item.CODE).Sum(x => x.Values[0]);
                            rowCur.Cells[6].SetCellValue(Convert.ToDouble(sumQuantity));
                            rowCur.Cells[6].CellStyle = styleCellNumber;

                            var price = data.FirstOrDefault(x => x.CENTER_CODE == item.CENTER_CODE && x.CODE == item.CODE)?.Values[1];
                            rowCur.Cells[7].SetCellValue(price == 0 || price == null ? 0 : Convert.ToDouble(price));
                            rowCur.Cells[7].CellStyle = styleCellNumber;

                            var totalVPCN = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VPCN" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[8].SetCellValue(string.IsNullOrEmpty(totalVPCN) ? 0 : Convert.ToDouble(totalVPCN));
                            rowCur.Cells[8].CellStyle = styleCellNumber;

                            var totalVTMB = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VTMB" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[9].SetCellValue(string.IsNullOrEmpty(totalVTMB) ? 0 : Convert.ToDouble(totalVTMB));
                            rowCur.Cells[9].CellStyle = styleCellNumber;

                            var totalVTMT = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VTMT" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[10].SetCellValue(string.IsNullOrEmpty(totalVTMT) ? 0 : Convert.ToDouble(totalVTMT));
                            rowCur.Cells[10].CellStyle = styleCellNumber;

                            var totalVTMN = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VTMN" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[11].SetCellValue(string.IsNullOrEmpty(totalVTMN) ? 0 : Convert.ToDouble(totalVTMN));
                            rowCur.Cells[11].CellStyle = styleCellNumber;

                            var sumTotal = data.Where(x => x.CODE == item.CODE).Sum(x => x.Values[2]);
                            rowCur.Cells[12].SetCellValue(Convert.ToDouble(sumTotal));
                            rowCur.Cells[12].CellStyle = styleCellNumber;

                            if (item.IS_GROUP)
                            {
                                rowCur.Cells[0].CellStyle = styleCellBold;
                                rowCur.Cells[0].CellStyle.SetFont(fontBold);
                                rowCur.Cells[1].CellStyle = styleCellBold;
                                rowCur.Cells[1].CellStyle.SetFont(fontBold);
                            }
                            numRowCur++;
                            number++;
                        }
                        break;
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

        public override void GenerateTemplateBase(ref MemoryStream outFileStream, string path, string templateId, int year) { }

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
            var code = UnitOfWork.Repository<CostCenterRepo>().Queryable().Where(x => x.CODE == template.ORG_CODE).Select(x => x.PARENT_CODE).FirstOrDefault();
            var currentUserCenterCode = ProfileUtilities.User.ORGANIZE_CODE;
            if (ProfileUtilities.User.ORGANIZE_CODE == "1000")
            {
                currentUserCenterCode = code;
            }
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
                                    detail.PLData = revenuePlData.FirstOrDefault(x => x.KHOAN_MUC_HANG_HOA_CODE == detail.ELEMENT_CODE && x.CHI_PHI_PROFIT_CENTER_CODE == detail.CENTER_CODE);
                                }
                                var treeData = detail?.PLData;
                                if (treeData != null)
                                {
                                    item.Values[2] += treeData.AMOUNT ?? 0;
                                    item.HasAssignValue = true;

                                    i.Values[0] = treeData.QUANTITY ?? 0;
                                    i.Values[1] = treeData.PRICE ?? 0;
                                    i.Values[2] = treeData.AMOUNT ?? 0;

                                    i.Values[3] = treeData.QUANTITY_TD ?? 0;
                                    i.Values[4] = treeData.PRICE_TD ?? 0;
                                    i.Values[5] = treeData.AMOUNT_TD ?? 0;

                                    i.DESCRIPTION = treeData.DESCRIPTION;
                                    i.DESCRIPTION_TD = treeData.DESCRIPTION_TD;
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
                                item.Values = new decimal[6]
                                {
                                data.Sum(x => x.QUANTITY) ?? 0,
                                data.Sum(x => x.PRICE) ?? 0,
                                data.Sum(x => x.AMOUNT) ?? 0,
                                data.Sum(x => x.QUANTITY_TD) ?? 0,
                                data.Sum(x => x.PRICE_TD) ?? 0,
                                data.Sum(x => x.AMOUNT_TD) ?? 0
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
            var code = UnitOfWork.Repository<CostCenterRepo>().Queryable().Where(x => x.CODE == template.ORG_CODE).Select(x => x.PARENT_CODE).FirstOrDefault();
            if (ProfileUtilities.User.ORGANIZE_CODE == "1000")
            {
                orgCode = code;
            }
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
                    Values = new decimal[6]
                    {
                        costCFData.Sum(x => x.QUANTITY) ?? 0,
                         costCFData.Sum(x => x.PRICE) ?? 0,
                          costCFData.Sum(x => x.AMOUNT) ?? 0,
                          costCFData.Sum(x => x.QUANTITY_TD) ?? 0,
                         costCFData.Sum(x => x.PRICE_TD) ?? 0,
                          costCFData.Sum(x => x.AMOUNT_TD) ?? 0,
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
                                item.Values[3] += treeData.Sum(x => x.QUANTITY_TD) ?? 0;
                                item.Values[4] += treeData.Sum(x => x.PRICE_TD) ?? 0;
                                item.Values[5] += treeData.Sum(x => x.AMOUNT_TD) ?? 0;
                                item.HasAssignValue = true;

                                foreach (var d in treeData)
                                {
                                    var values = new decimal[3];
                                    values[0] = treeData.Sum(x => x.QUANTITY) ?? 0;
                                    values[1] = treeData.Sum(x => x.PRICE) ?? 0;
                                    values[2] = treeData.Sum(x => x.AMOUNT) ?? 0;
                                    values[3] = treeData.Sum(x => x.QUANTITY_TD) ?? 0;
                                    values[4] = treeData.Sum(x => x.PRICE_TD) ?? 0;
                                    values[5] = treeData.Sum(x => x.AMOUNT_TD) ?? 0;

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
                        item.Values[3] += treeData.Sum(x => x.QUANTITY_TD) ?? 0;
                        item.Values[4] += treeData.Sum(x => x.PRICE_TD) ?? 0;
                        item.Values[5] += treeData.Sum(x => x.AMOUNT_TD) ?? 0;
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
                            item.Values = new decimal[6]
                            {
                                data.Sum(x => x.QUANTITY) ?? 0,
                                data.Sum(x => x.PRICE) ?? 0,
                                data.Sum(x => x.AMOUNT) ?? 0,
                                data.Sum(x => x.QUANTITY_TD) ?? 0,
                                data.Sum(x => x.PRICE_TD) ?? 0,
                                data.Sum(x => x.AMOUNT_TD) ?? 0,
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



        public void GenerateData2(ref MemoryStream outFileStream, string path, ViewDataCenterModel model, string orgCode, List<string> lstSanBay, DLkehoachData[] dataUI)
        {
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
                int NUM_CELL = 22;

                //Style cần dùng

                var styleCellNumber = GetCellStyleNumber(templateWorkbook);

                ICellStyle styleCellBold = templateWorkbook.CreateCellStyle();
                styleCellBold.WrapText = true;
                var fontBold = templateWorkbook.CreateFont();
                fontBold.Boldweight = (short)FontBoldWeight.Bold;
                fontBold.FontHeightInPoints = 11;
                fontBold.FontName = "Times New Roman";

                #region Header
                var template = UnitOfWork.Repository<TemplateRepo>().Get(model.TEMPLATE_CODE);
                var rowHeader1 = ReportUtilities.CreateRow(ref sheet, 0, NUM_CELL);
                ReportUtilities.CreateCell(ref rowHeader1, NUM_CELL);
                rowHeader1.Cells[0].SetCellValue(rowHeader1.Cells[0].StringCellValue + $" {template.Organize?.Parent?.NAME.ToUpper()}");

                var rowHeader2 = ReportUtilities.CreateRow(ref sheet, 1, NUM_CELL);

                ReportUtilities.CreateCell(ref rowHeader2, NUM_CELL);
                rowHeader2.Cells[0].SetCellValue($"{template.Organize.NAME}");
                rowHeader2.Cells[2].SetCellValue(template.TITLE.ToUpper());

                var rowHeader3 = ReportUtilities.CreateRow(ref sheet, 2, NUM_CELL);

                ReportUtilities.CreateCell(ref rowHeader3, NUM_CELL);
                rowHeader3.Cells[1].SetCellValue(template.CREATE_BY);
                #endregion

                #region Details

                numRowCur = 8;
                var number = 1;

                //var data = GetData(model, lstSanBay);
                // dữ liệu được lấy từ màn hình
                var DLUI = dataUI;

                switch (orgCode)
                {
                    case "100001":
                        foreach (var item in dataUI)
                        {
                            IRow rowCur = ReportUtilities.CreateRow(ref sheet, numRowCur, NUM_CELL);

                            rowCur.Cells[0].SetCellValue(item.code);
                            rowCur.Cells[1].SetCellValue(item.name);


                            rowCur.Cells[2].SetCellValue(Convert.ToDouble(item.quantity_VPCT));
                            rowCur.Cells[2].CellStyle = styleCellNumber;


                            rowCur.Cells[3].SetCellValue(Convert.ToDouble(item.quantity_MB));
                            rowCur.Cells[3].CellStyle = styleCellNumber;


                            rowCur.Cells[4].SetCellValue(Convert.ToDouble(item.quantity_MT));
                            rowCur.Cells[4].CellStyle = styleCellNumber;

                            //var CR = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "CR" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[5].SetCellValue(Convert.ToDouble(item.quantity_CR));
                            rowCur.Cells[5].CellStyle = styleCellNumber;

                            //var MN = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "MN" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[6].SetCellValue(Convert.ToDouble(item.quantity_MN));
                            rowCur.Cells[6].CellStyle = styleCellNumber;

                            //var sumQuantity = data.Where(x => x.CODE == item.CODE).Sum(x => x.Values[0]);
                            rowCur.Cells[7].SetCellValue(Convert.ToDouble(item.sumQuantity));
                            rowCur.Cells[7].CellStyle = styleCellNumber;


                            rowCur.Cells[8].SetCellValue(Convert.ToDouble(item.price));
                            rowCur.Cells[8].CellStyle = styleCellNumber;

                            //var totalVPCT = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VPCT" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[9].SetCellValue(Convert.ToDouble(item.total_VPCT));
                            rowCur.Cells[9].CellStyle = styleCellNumber;

                            //var totalMB = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "MB" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[10].SetCellValue(Convert.ToDouble(item.total_MB));
                            rowCur.Cells[10].CellStyle = styleCellNumber;

                            //var totalMT = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "MT" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[11].SetCellValue(Convert.ToDouble(item.total_MT));
                            rowCur.Cells[11].CellStyle = styleCellNumber;

                            //var totalCR = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "CR" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[12].SetCellValue(Convert.ToDouble(item.total_CR));
                            rowCur.Cells[12].CellStyle = styleCellNumber;

                            //var totalMN = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "MN" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[13].SetCellValue(Convert.ToDouble(item.total_MN));
                            rowCur.Cells[13].CellStyle = styleCellNumber;

                            //var sumTotal = data.Where(x => x.CODE == item.CODE).Sum(x => x.Values[2]);
                            rowCur.Cells[14].SetCellValue(Convert.ToDouble(item.sumTotal));
                            rowCur.Cells[14].CellStyle = styleCellNumber;

                            if (item.isBold)
                            {
                                rowCur.Cells[0].CellStyle = styleCellBold;
                                rowCur.Cells[0].CellStyle.SetFont(fontBold);
                                rowCur.Cells[1].CellStyle = styleCellBold;
                                rowCur.Cells[1].CellStyle.SetFont(fontBold);
                            }
                            numRowCur++;
                            number++;
                        }
                        break;
                    case "100002":
                        foreach (var item in dataUI)
                        {
                            IRow rowCur = ReportUtilities.CreateRow(ref sheet, numRowCur, NUM_CELL);

                            rowCur.Cells[0].SetCellValue(item.code);
                            rowCur.Cells[1].SetCellValue(item.name);

                            //var VPCN = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VPCN" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[2].SetCellValue(Convert.ToDouble(item.quantity_VPCN));
                            rowCur.Cells[2].CellStyle = styleCellNumber;

                            //var HAN = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "HAN" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[3].SetCellValue(Convert.ToDouble(item.quantity_HAN));
                            rowCur.Cells[3].CellStyle = styleCellNumber;

                            //var HPH = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "HPH" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[4].SetCellValue(Convert.ToDouble(item.quantity_HPH));
                            rowCur.Cells[4].CellStyle = styleCellNumber;

                            //var THD = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "THD" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[5].SetCellValue(Convert.ToDouble(item.quantity_THD));
                            rowCur.Cells[5].CellStyle = styleCellNumber;

                            //var VII = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VII" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[6].SetCellValue(Convert.ToDouble(item.quantity_VII));
                            rowCur.Cells[6].CellStyle = styleCellNumber;

                            //var VDH = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VDH" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[7].SetCellValue(Convert.ToDouble(item.quantity_VDH));
                            rowCur.Cells[7].CellStyle = styleCellNumber;

                            //var VDO = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VDO" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[8].SetCellValue(Convert.ToDouble(item.quantity_VDO));
                            rowCur.Cells[8].CellStyle = styleCellNumber;

                            //var sumQuantity = data.Where(x => x.CODE == item.CODE).Sum(x => x.Values[0]);
                            rowCur.Cells[9].SetCellValue(Convert.ToDouble(item.sumQuantity));
                            rowCur.Cells[9].CellStyle = styleCellNumber;

                            //var price = data.FirstOrDefault(x => x.CENTER_CODE == item.CENTER_CODE && x.CODE == item.CODE)?.Values[1];
                            rowCur.Cells[10].SetCellValue(Convert.ToDouble(item.price));
                            rowCur.Cells[10].CellStyle = styleCellNumber;

                            //var totalVPCN = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VPCN" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[11].SetCellValue(Convert.ToDouble(item.total_VPCN));
                            rowCur.Cells[11].CellStyle = styleCellNumber;

                            //var totalHAN = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "HAN" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[12].SetCellValue(Convert.ToDouble(item.total_HAN));
                            rowCur.Cells[12].CellStyle = styleCellNumber;

                            //var totalHPH = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "HPH" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[13].SetCellValue(Convert.ToDouble(item.total_HPH));
                            rowCur.Cells[13].CellStyle = styleCellNumber;

                            //var totalTHD = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "THD" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[14].SetCellValue(Convert.ToDouble(item.total_THD));
                            rowCur.Cells[14].CellStyle = styleCellNumber;

                            //var totalVII = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VII" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[15].SetCellValue(Convert.ToDouble(item.total_VII));
                            rowCur.Cells[15].CellStyle = styleCellNumber;

                            //var totalVDH = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VDH" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[16].SetCellValue(Convert.ToDouble(item.total_VDH));
                            rowCur.Cells[16].CellStyle = styleCellNumber;

                            //var totalVDO = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VDO" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[17].SetCellValue(Convert.ToDouble(item.total_VDO));
                            rowCur.Cells[17].CellStyle = styleCellNumber;

                            //var sumTotal = data.Where(x => x.CODE == item.CODE).Sum(x => x.Values[2]);
                            rowCur.Cells[18].SetCellValue(Convert.ToDouble(item.sumTotal));
                            rowCur.Cells[18].CellStyle = styleCellNumber;

                            if (item.isBold)
                            {
                                rowCur.Cells[0].CellStyle = styleCellBold;
                                rowCur.Cells[0].CellStyle.SetFont(fontBold);
                                rowCur.Cells[1].CellStyle = styleCellBold;
                                rowCur.Cells[1].CellStyle.SetFont(fontBold);
                            }
                            numRowCur++;
                            number++;
                        }
                        break;
                    case "100003":
                        foreach (var item in dataUI)
                        {
                            IRow rowCur = ReportUtilities.CreateRow(ref sheet, numRowCur, NUM_CELL);

                            rowCur.Cells[0].SetCellValue(item.code);
                            rowCur.Cells[1].SetCellValue(item.name);

                            //var VPCN = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VPCN" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[2].SetCellValue(Convert.ToDouble(item.quantity_VPCN));
                            rowCur.Cells[2].CellStyle = styleCellNumber;

                            //var DAD = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "DAD" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[3].SetCellValue(Convert.ToDouble(item.quantity_DAD));
                            rowCur.Cells[3].CellStyle = styleCellNumber;

                            //var CXR = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "CXR" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[4].SetCellValue(Convert.ToDouble(item.quantity_CXR));
                            rowCur.Cells[4].CellStyle = styleCellNumber;

                            //var HUI = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "HUI" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[5].SetCellValue(Convert.ToDouble(item.quantity_HUI));
                            rowCur.Cells[5].CellStyle = styleCellNumber;

                            //var UIH = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "UIH" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[6].SetCellValue(Convert.ToDouble(item.quantity_UIH));
                            rowCur.Cells[6].CellStyle = styleCellNumber;

                            //var VCL = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VCL" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[7].SetCellValue(Convert.ToDouble(item.quantity_VCL));
                            rowCur.Cells[7].CellStyle = styleCellNumber;

                            //var PXU = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "PXU" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[8].SetCellValue(Convert.ToDouble(item.quantity_PXU));
                            rowCur.Cells[8].CellStyle = styleCellNumber;

                            //var TBB = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "TBB" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[9].SetCellValue(Convert.ToDouble(item.quantity_TBB));
                            rowCur.Cells[9].CellStyle = styleCellNumber;

                            //var sumQuantity = data.Where(x => x.CODE == item.CODE).Sum(x => x.Values[0]);
                            rowCur.Cells[10].SetCellValue(Convert.ToDouble(item.sumQuantity));
                            rowCur.Cells[10].CellStyle = styleCellNumber;

                            //var price = data.FirstOrDefault(x => x.CENTER_CODE == item.CENTER_CODE && x.CODE == item.CODE)?.Values[1];
                            rowCur.Cells[11].SetCellValue(Convert.ToDouble(item.price));
                            rowCur.Cells[11].CellStyle = styleCellNumber;

                            //var totalVPCN = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VPCN" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[12].SetCellValue(Convert.ToDouble(item.total_VPCN));
                            rowCur.Cells[12].CellStyle = styleCellNumber;

                            //var totalDAD = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "DAD" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[13].SetCellValue(Convert.ToDouble(item.total_DAD));
                            rowCur.Cells[13].CellStyle = styleCellNumber;

                            //var totalCXR = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "CXR" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[14].SetCellValue(Convert.ToDouble(item.total_CXR));
                            rowCur.Cells[14].CellStyle = styleCellNumber;

                            //var totalHUI = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "HUI" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[15].SetCellValue(Convert.ToDouble(item.total_HUI));
                            rowCur.Cells[15].CellStyle = styleCellNumber;

                            //var totalUIH = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "UIH" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[16].SetCellValue(Convert.ToDouble(item.total_UIH));
                            rowCur.Cells[16].CellStyle = styleCellNumber;

                            //var totalVCL = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VCL" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[17].SetCellValue(Convert.ToDouble(item.total_VCL));
                            rowCur.Cells[17].CellStyle = styleCellNumber;

                            //var totalPXU = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "PXU" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[18].SetCellValue(Convert.ToDouble(item.total_PXU));
                            rowCur.Cells[18].CellStyle = styleCellNumber;

                            //var totalTBB = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "TBB" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[19].SetCellValue(Convert.ToDouble(item.total_TBB));
                            rowCur.Cells[19].CellStyle = styleCellNumber;

                            //var sumTotal = data.Where(x => x.CODE == item.CODE).Sum(x => x.Values[2]);
                            rowCur.Cells[20].SetCellValue(Convert.ToDouble(item.sumTotal));
                            rowCur.Cells[20].CellStyle = styleCellNumber;

                            if (item.isBold)
                            {
                                rowCur.Cells[0].CellStyle = styleCellBold;
                                rowCur.Cells[0].CellStyle.SetFont(fontBold);
                                rowCur.Cells[1].CellStyle = styleCellBold;
                                rowCur.Cells[1].CellStyle.SetFont(fontBold);
                            }
                            numRowCur++;
                            number++;
                        }
                        break;
                    case "100004":
                        foreach (var item in dataUI)
                        {
                            IRow rowCur = ReportUtilities.CreateRow(ref sheet, numRowCur, NUM_CELL);

                            rowCur.Cells[0].SetCellValue(item.code);
                            rowCur.Cells[1].SetCellValue(item.name);

                            //var VPCN = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VPCN" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[2].SetCellValue(Convert.ToDouble(item.quantity_VPCN));
                            rowCur.Cells[2].CellStyle = styleCellNumber;

                            //var SGN = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "SGN" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[3].SetCellValue(Convert.ToDouble(item.quantity_SGN));
                            rowCur.Cells[3].CellStyle = styleCellNumber;

                            //var PQC = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "PQC" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[4].SetCellValue(Convert.ToDouble(item.quantity_PQC));
                            rowCur.Cells[4].CellStyle = styleCellNumber;

                            //var DLI = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "DLI" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[5].SetCellValue(Convert.ToDouble(item.quantity_DLI));
                            rowCur.Cells[5].CellStyle = styleCellNumber;

                            //var VCA = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VCA" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[6].SetCellValue(Convert.ToDouble(item.quantity_VCA));
                            rowCur.Cells[6].CellStyle = styleCellNumber;

                            //var BMV = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "BMV" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[7].SetCellValue(Convert.ToDouble(item.quantity_BMV));
                            rowCur.Cells[7].CellStyle = styleCellNumber;

                            //var VCS = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VCS" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[8].SetCellValue(Convert.ToDouble(item.quantity_VCS));
                            rowCur.Cells[8].CellStyle = styleCellNumber;

                            //var sumQuantity = data.Where(x => x.CODE == item.CODE).Sum(x => x.Values[0]);
                            rowCur.Cells[9].SetCellValue(Convert.ToDouble(item.sumQuantity));
                            rowCur.Cells[9].CellStyle = styleCellNumber;

                            //var price = data.FirstOrDefault(x => x.CENTER_CODE == item.CENTER_CODE && x.CODE == item.CODE)?.Values[1];
                            rowCur.Cells[10].SetCellValue(Convert.ToDouble(item.price));
                            rowCur.Cells[10].CellStyle = styleCellNumber;

                            //var totalVPCN = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VPCN" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[11].SetCellValue(Convert.ToDouble(item.total_VPCN));
                            rowCur.Cells[11].CellStyle = styleCellNumber;

                            //var totalSGN = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "SGN" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[12].SetCellValue(Convert.ToDouble(item.total_SGN));
                            rowCur.Cells[12].CellStyle = styleCellNumber;

                            //var totalPQC = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "PQC" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[13].SetCellValue(Convert.ToDouble(item.total_PQC));
                            rowCur.Cells[13].CellStyle = styleCellNumber;

                            //var totalDLI = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "DLI" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[14].SetCellValue(Convert.ToDouble(item.total_DLI));
                            rowCur.Cells[14].CellStyle = styleCellNumber;

                            //var totalVCA = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VCA" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[15].SetCellValue(Convert.ToDouble(item.quantity_VCA));
                            rowCur.Cells[15].CellStyle = styleCellNumber;

                            //var totalBMV = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "BMV" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[16].SetCellValue(Convert.ToDouble(item.quantity_BMV));
                            rowCur.Cells[16].CellStyle = styleCellNumber;

                            //var totalVCS = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VCS" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[17].SetCellValue(Convert.ToDouble(item.total_VCS));
                            rowCur.Cells[17].CellStyle = styleCellNumber;

                            //var sumTotal = data.Where(x => x.CODE == item.CODE).Sum(x => x.Values[2]);
                            rowCur.Cells[18].SetCellValue(Convert.ToDouble(item.sumTotal));
                            rowCur.Cells[18].CellStyle = styleCellNumber;

                            if (item.isBold)
                            {
                                rowCur.Cells[0].CellStyle = styleCellBold;
                                rowCur.Cells[0].CellStyle.SetFont(fontBold);
                                rowCur.Cells[1].CellStyle = styleCellBold;
                                rowCur.Cells[1].CellStyle.SetFont(fontBold);
                            }
                            numRowCur++;
                            number++;
                        }
                        break;
                    case "100005":

                        foreach (var item in dataUI)
                        {
                            IRow rowCur = ReportUtilities.CreateRow(ref sheet, numRowCur, NUM_CELL);

                            rowCur.Cells[0].SetCellValue(item.code);
                            rowCur.Cells[1].SetCellValue(item.name);

                            //var VPCN = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VPCN" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[2].SetCellValue(Convert.ToDouble(item.quantity_VPCN));
                            rowCur.Cells[2].CellStyle = styleCellNumber;

                            //var VTMB = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VTMB" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[3].SetCellValue(Convert.ToDouble(item.quantity_VTMB));
                            rowCur.Cells[3].CellStyle = styleCellNumber;

                            //var VTMT = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VTMT" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[4].SetCellValue(Convert.ToDouble(item.quantity_VTMT));
                            rowCur.Cells[4].CellStyle = styleCellNumber;

                            //var VTMN = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VTMN" && x.CODE == item.CODE)?.Values[0].ToString();
                            rowCur.Cells[5].SetCellValue(Convert.ToDouble(item.quantity_VTMN));
                            rowCur.Cells[5].CellStyle = styleCellNumber;

                            //var sumQuantity = data.Where(x => x.CODE == item.CODE).Sum(x => x.Values[0]);
                            rowCur.Cells[6].SetCellValue(Convert.ToDouble(item.sumQuantity));
                            rowCur.Cells[6].CellStyle = styleCellNumber;

                            //var price = data.FirstOrDefault(x => x.CENTER_CODE == item.CENTER_CODE && x.CODE == item.CODE)?.Values[1];
                            rowCur.Cells[7].SetCellValue(Convert.ToDouble(item.price));
                            rowCur.Cells[7].CellStyle = styleCellNumber;

                            //var totalVPCN = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VPCN" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[8].SetCellValue(Convert.ToDouble(item.total_VPCN));
                            rowCur.Cells[8].CellStyle = styleCellNumber;

                            //var totalVTMB = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VTMB" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[9].SetCellValue(Convert.ToDouble(item.total_VTMB));
                            rowCur.Cells[9].CellStyle = styleCellNumber;

                            //var totalVTMT = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VTMT" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[10].SetCellValue(Convert.ToDouble(item.total_VTMT));
                            rowCur.Cells[10].CellStyle = styleCellNumber;

                            //var totalVTMN = data.FirstOrDefault(x => x.Center.SAN_BAY_CODE == "VTMN" && x.CODE == item.CODE)?.Values[2].ToString();
                            rowCur.Cells[11].SetCellValue(Convert.ToDouble(item.total_VTMN));
                            rowCur.Cells[11].CellStyle = styleCellNumber;

                            //var sumTotal = data.Where(x => x.CODE == item.CODE).Sum(x => x.Values[2]);
                            rowCur.Cells[12].SetCellValue(Convert.ToDouble(item.sumTotal));
                            rowCur.Cells[12].CellStyle = styleCellNumber;

                            if (item.isBold)
                            {
                                rowCur.Cells[0].CellStyle = styleCellBold;
                                rowCur.Cells[0].CellStyle.SetFont(fontBold);
                                rowCur.Cells[1].CellStyle = styleCellBold;
                                rowCur.Cells[1].CellStyle.SetFont(fontBold);
                            }
                            numRowCur++;
                            number++;
                        }
                        break;
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
        #region Export excel from data center view
        public override void GenerateExportExcel(ref MemoryStream outFileStream, dynamic table, string path, int year, string centerCode, int? version, string templateId, string unit, decimal exchangeRate)
        {
            // Create a new workbook and a sheet named "User Accounts"
            //Mở file Template
            var htmlMonth = table.htmlMonth;
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            IWorkbook workbook;
            workbook = new XSSFWorkbook(fs);
            workbook.SetSheetName(0, ModulType.GetTextSheetName(ModulType.KeHoachChiPhi));
            fs.Close();
            var module = "KeHoachChiPhi";

            ISheet sheetMonth = workbook.GetSheetAt(0);
            var metaDataMonth = ExcelHelper.GetExcelMeta(htmlMonth);
            var NUM_CELL_MONTH = metaDataMonth.MetaTBody[1].Count;

            InitHeaderFile(ref sheetMonth, year, centerCode, version, NUM_CELL_MONTH, templateId, "Tấn", exchangeRate);
            ExcelHelperBP.InsertHeaderTable(ref workbook, ref sheetMonth, metaDataMonth.MetaTHead, NUM_CELL_MONTH, module, ignoreFirstColumn: string.IsNullOrEmpty(templateId) || (!string.IsNullOrEmpty(templateId) && !GetTemplate(templateId).IS_BASE));
            ExcelHelperBP.InsertBodyTableByYear(ref workbook,
                ref sheetMonth,
                metaDataMonth.MetaTBody,
                NUM_CELL_MONTH,
                module,
                ignoreFirstColumn: string.IsNullOrEmpty(templateId) || (!string.IsNullOrEmpty(templateId) && !GetTemplate(templateId).IS_BASE)
                );





            // Save the Excel spreadsheet to a MemoryStream and return it to the client
            workbook.Write(outFileStream);
        }

        private void InitHeaderFile(ref ISheet sheet, int year, string centerCode, int? version, int NUM_CELL, string templateId, string unit, decimal exchangeRate)
        {
            var name = "DỮ LIỆU KẾ HOẠCH CHI PHÍ";
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

        public T_MD_KHOAN_MUC_HANG_HOA GetKMHangHoa(string code)
        {
            var KMHangHoa = UnitOfWork.Repository<KhoanMucHangHoaRepo>()
                .Queryable()
                .FirstOrDefault(x => x.CODE == code);

            return KMHangHoa;
        }


        public void UpdateCellValue(string templateCode, int version, int year, string type, string sanBay, string costCenter, string elementCode, string valueInput, int month, string org)
        {
            try
            {
                UnitOfWork.BeginTransaction();
                string value = valueInput.Replace(".", "");
                var lstCostCenter = UnitOfWork.Repository<ChiPhiProfitCenterRepo>().Queryable().Where(x => x.SAN_BAY_CODE == sanBay && x.COST_CENTER_CODE == costCenter).Select(x => x.COST_CENTER_CODE).ToList();
                var prc = UnitOfWork.Repository<ChiPhiProfitCenterRepo>().Queryable().FirstOrDefault(x => x.SAN_BAY_CODE == sanBay && x.COST_CENTER_CODE == costCenter)?.CODE;
                #region Phiên bản bổ sung
                if (month > 0)
                {
                    if (type == "SL")
                    {
                        var rowsChange = UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.TEMPLATE_CODE == templateCode && x.VERSION == version && x.TIME_YEAR == year && lstCostCenter.Contains(costCenter) && x.ChiPhiProfitCenter.SAN_BAY_CODE == sanBay && x.KHOAN_MUC_HANG_HOA_CODE == elementCode && x.MONTH == month).ToList();
                        if (rowsChange.Count() == 0)
                        {
                            UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(new T_BP_KE_HOACH_CHI_PHI_DATA
                            {
                                PKID = Guid.NewGuid().ToString(),
                                ORG_CODE = org,
                                TEMPLATE_CODE = templateCode,
                                CHI_PHI_PROFIT_CENTER_CODE = prc,
                                KHOAN_MUC_HANG_HOA_CODE = elementCode,
                                VERSION = version,
                                TIME_YEAR = year,
                                QUANTITY = Convert.ToDecimal(value),
                                MONTH = month,
                            }) ;
                            UnitOfWork.Commit();
                            return;
                        }
                        var oldValue = rowsChange.FirstOrDefault().QUANTITY;
                        foreach (var item in rowsChange)
                        {
                            item.QUANTITY = Convert.ToDecimal(value);
                            item.AMOUNT =  item.PRICE * Convert.ToDecimal(value);
                            UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Update(item);
                        }
                        // Lưu lịch sử
                        UnitOfWork.Repository<KeHoachChiPhiEditHistoryRepo>().Create(new T_BP_KE_HOACH_CHI_PHI_EDIT_HISTORY
                        {
                            ID = Guid.NewGuid(),
                            TEMPLATE_CODE = templateCode,
                            VERSION = version,
                            YEAR = year,
                            TYPE = "Cập nhật sản lượng",
                            COST_CENTER_CODE = costCenter,
                            SAN_BAY_CODE = sanBay,
                            ELEMENT_CODE = elementCode,
                            OLD_VALUE = oldValue,
                            NEW_VALUE = Convert.ToDecimal(value),
                            CREATE_BY = ProfileUtilities.User.USER_NAME,
                            CREATE_DATE = DateTime.Now,
                        });
                    }
                    else if (type == "DG")
                    {
                        var rowsChange = UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.TEMPLATE_CODE == templateCode && x.VERSION == version && x.TIME_YEAR == year && x.KHOAN_MUC_HANG_HOA_CODE == elementCode && x.MONTH == month).ToList();
                        if (rowsChange.Count() == 0)
                        {
                            UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(new T_BP_KE_HOACH_CHI_PHI_DATA
                            {
                                PKID = Guid.NewGuid().ToString(),
                                ORG_CODE = org,
                                TEMPLATE_CODE = templateCode,
                                CHI_PHI_PROFIT_CENTER_CODE = prc,
                                KHOAN_MUC_HANG_HOA_CODE = elementCode,
                                VERSION = version,
                                TIME_YEAR = year,
                                PRICE = Convert.ToDecimal(value),
                                MONTH = month,
                            });
                            UnitOfWork.Commit();
                            return;
                        }
                        var oldValue = rowsChange.FirstOrDefault().PRICE;
                        foreach (var item in rowsChange)
                        {
                            item.PRICE = Convert.ToDecimal(value);
                            item.AMOUNT = item.QUANTITY *  item.PRICE ;
                            UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Update(item);
                        }
                        // Lưu lịch sử
                        UnitOfWork.Repository<KeHoachChiPhiEditHistoryRepo>().Create(new T_BP_KE_HOACH_CHI_PHI_EDIT_HISTORY
                        {
                            ID = Guid.NewGuid(),
                            TEMPLATE_CODE = templateCode,
                            VERSION = version,
                            YEAR = year,
                            TYPE = "Cập nhật đơn giá",
                            COST_CENTER_CODE = costCenter,
                            SAN_BAY_CODE = sanBay,
                            ELEMENT_CODE = elementCode,
                            OLD_VALUE = oldValue,
                            NEW_VALUE = Convert.ToDecimal(value),
                            CREATE_BY = ProfileUtilities.User.USER_NAME,
                            CREATE_DATE = DateTime.Now,
                        });
                    }
                    else if (type == "SL_TD")
                    {
                        var rowsChange = UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.TEMPLATE_CODE == templateCode && x.VERSION == version && x.TIME_YEAR == year && x.KHOAN_MUC_HANG_HOA_CODE == elementCode && x.MONTH == month).ToList();
                        if (rowsChange.Count() == 0)
                        {
                            UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(new T_BP_KE_HOACH_CHI_PHI_DATA
                            {
                                PKID = Guid.NewGuid().ToString(),
                                ORG_CODE = org,
                                TEMPLATE_CODE = templateCode,
                                CHI_PHI_PROFIT_CENTER_CODE = prc,
                                KHOAN_MUC_HANG_HOA_CODE = elementCode,
                                VERSION = version,
                                TIME_YEAR = year,
                                QUANTITY_TD = Convert.ToDecimal(value),
                                MONTH = month,
                            });
                            UnitOfWork.Commit();
                            return;
                        }
                        var oldValue = rowsChange.FirstOrDefault().QUANTITY_TD;
                        foreach (var item in rowsChange)
                        {
                            item.QUANTITY_TD = Convert.ToDecimal(value);
                            item.AMOUNT_TD =  item.PRICE_TD* Convert.ToDecimal(value);
                            UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Update(item);
                        }
                        // Lưu lịch sử
                        UnitOfWork.Repository<KeHoachChiPhiEditHistoryRepo>().Create(new T_BP_KE_HOACH_CHI_PHI_EDIT_HISTORY
                        {
                            ID = Guid.NewGuid(),
                            TEMPLATE_CODE = templateCode,
                            VERSION = version,
                            YEAR = year,
                            TYPE = "Thẩm định sản lượng",
                            COST_CENTER_CODE = costCenter,
                            SAN_BAY_CODE = sanBay,
                            ELEMENT_CODE = elementCode,
                            OLD_VALUE = oldValue,
                            NEW_VALUE = Convert.ToDecimal(value),
                            CREATE_BY = ProfileUtilities.User.USER_NAME,
                            CREATE_DATE = DateTime.Now,
                        });
                    }
                    else if (type == "DG_TD")
                    {
                        var rowsChange = UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.TEMPLATE_CODE == templateCode && x.VERSION == version && x.TIME_YEAR == year && x.KHOAN_MUC_HANG_HOA_CODE == elementCode && x.MONTH == month).ToList();
                        if (rowsChange.Count() == 0)
                        {
                            UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(new T_BP_KE_HOACH_CHI_PHI_DATA
                            {
                                PKID = Guid.NewGuid().ToString(),
                                ORG_CODE = org,
                                TEMPLATE_CODE = templateCode,
                                CHI_PHI_PROFIT_CENTER_CODE = prc,
                                KHOAN_MUC_HANG_HOA_CODE = elementCode,
                                VERSION = version,
                                TIME_YEAR = year,
                                PRICE_TD = Convert.ToDecimal(value),
                                MONTH = month,
                            });
                            UnitOfWork.Commit();
                            return;
                        }
                        var oldValue = rowsChange.FirstOrDefault().PRICE_TD;
                        foreach (var item in rowsChange)
                        {
                            item.PRICE_TD = Convert.ToDecimal(value);
                            item.AMOUNT_TD = item.QUANTITY_TD *  item.PRICE_TD;
                            UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Update(item);
                        }
                        // Lưu lịch sử
                        UnitOfWork.Repository<KeHoachChiPhiEditHistoryRepo>().Create(new T_BP_KE_HOACH_CHI_PHI_EDIT_HISTORY
                        {
                            ID = Guid.NewGuid(),
                            TEMPLATE_CODE = templateCode,
                            VERSION = version,
                            YEAR = year,
                            TYPE = "Thẩm định đơn giá",
                            COST_CENTER_CODE = costCenter,
                            SAN_BAY_CODE = sanBay,
                            ELEMENT_CODE = elementCode,
                            OLD_VALUE = oldValue,
                            NEW_VALUE = Convert.ToDecimal(value),
                            CREATE_BY = ProfileUtilities.User.USER_NAME,
                            CREATE_DATE = DateTime.Now,
                        });
                    }
                    else if (type == "GC_TD")
                    {
                        var rowsChange = UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.TEMPLATE_CODE == templateCode && x.VERSION == version && x.TIME_YEAR == year && x.KHOAN_MUC_HANG_HOA_CODE == elementCode && x.MONTH == month).ToList();
                        if (rowsChange.Count() == 0)
                        {
                            this.State = false;
                            this.ErrorMessage = "Có lỗi hệ thống xảy ra! Vui lòng liên hệ với quản trị viên!";
                            return;
                        }
                        var oldValue = rowsChange.FirstOrDefault().DESCRIPTION_TD;
                        foreach (var item in rowsChange)
                        {
                            item.DESCRIPTION_TD = value;
                            UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Update(item);
                        }
                        // Lưu lịch sử
                        UnitOfWork.Repository<KeHoachChiPhiEditHistoryRepo>().Create(new T_BP_KE_HOACH_CHI_PHI_EDIT_HISTORY
                        {
                            ID = Guid.NewGuid(),
                            TEMPLATE_CODE = templateCode,
                            VERSION = version,
                            YEAR = year,
                            TYPE = "Thẩm định ghi chú",
                            COST_CENTER_CODE = costCenter,
                            SAN_BAY_CODE = sanBay,
                            ELEMENT_CODE = elementCode,
                            OLD_VALUE = 0,
                            NEW_VALUE = 0,
                            OLD_DESCRIPTION = oldValue,
                            NEW_DESCRIPTION = value,
                            CREATE_BY = ProfileUtilities.User.USER_NAME,
                            CREATE_DATE = DateTime.Now,
                        });
                    }
                }
                #endregion

                else
                {
                    if (type == "SL")
                    {
                        var rowsChange = UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.TEMPLATE_CODE == templateCode && x.VERSION == version && x.TIME_YEAR == year && lstCostCenter.Contains(costCenter) && x.ChiPhiProfitCenter.SAN_BAY_CODE == sanBay && x.KHOAN_MUC_HANG_HOA_CODE == elementCode).ToList();
                        if (rowsChange.Count() == 0)
                        {
                            UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(new T_BP_KE_HOACH_CHI_PHI_DATA
                            {
                                PKID = Guid.NewGuid().ToString(),
                                ORG_CODE = org,
                                TEMPLATE_CODE = templateCode,
                                CHI_PHI_PROFIT_CENTER_CODE = prc,
                                KHOAN_MUC_HANG_HOA_CODE = elementCode,
                                VERSION = version,
                                TIME_YEAR = year,
                                QUANTITY = Convert.ToDecimal(value),
                            });
                            UnitOfWork.Commit();
                            return;
                        }
                        var oldValue = rowsChange.FirstOrDefault().QUANTITY;
                        foreach (var item in rowsChange)
                        {
                            item.QUANTITY = Convert.ToDecimal(value);
                            item.AMOUNT = item.PRICE * Convert.ToDecimal(value);
                            UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Update(item);
                        }
                        // Lưu lịch sử
                        UnitOfWork.Repository<KeHoachChiPhiEditHistoryRepo>().Create(new T_BP_KE_HOACH_CHI_PHI_EDIT_HISTORY
                        {
                            ID = Guid.NewGuid(),
                            TEMPLATE_CODE = templateCode,
                            VERSION = version,
                            YEAR = year,
                            TYPE = "Cập nhật sản lượng",
                            COST_CENTER_CODE = costCenter,
                            SAN_BAY_CODE = sanBay,
                            ELEMENT_CODE = elementCode,
                            OLD_VALUE = oldValue,
                            NEW_VALUE = Convert.ToDecimal(value),
                            CREATE_BY = ProfileUtilities.User.USER_NAME,
                            CREATE_DATE = DateTime.Now,
                        });
                    }
                    else if (type == "DG")
                    {
                        var rowsChange = UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.TEMPLATE_CODE == templateCode && x.VERSION == version && x.TIME_YEAR == year && x.KHOAN_MUC_HANG_HOA_CODE == elementCode).ToList();
                        if (rowsChange.Count() == 0)
                        {
                            UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(new T_BP_KE_HOACH_CHI_PHI_DATA
                            {
                                PKID = Guid.NewGuid().ToString(),
                                ORG_CODE = org,
                                TEMPLATE_CODE = templateCode,
                                CHI_PHI_PROFIT_CENTER_CODE = prc,
                                KHOAN_MUC_HANG_HOA_CODE = elementCode,
                                VERSION = version,
                                TIME_YEAR = year,
                                PRICE = Convert.ToDecimal(value),
                            });
                            UnitOfWork.Commit();
                            return;
                        }
                        var oldValue = rowsChange.FirstOrDefault().PRICE;
                        foreach (var item in rowsChange)
                        {
                            item.PRICE = Convert.ToDecimal(value);
                           
                            item.AMOUNT = item.QUANTITY *  item.PRICE ;
                            UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Update(item);
                        }
                        // Lưu lịch sử
                        UnitOfWork.Repository<KeHoachChiPhiEditHistoryRepo>().Create(new T_BP_KE_HOACH_CHI_PHI_EDIT_HISTORY
                        {
                            ID = Guid.NewGuid(),
                            TEMPLATE_CODE = templateCode,
                            VERSION = version,
                            YEAR = year,
                            TYPE = "Cập nhật đơn giá",
                            COST_CENTER_CODE = costCenter,
                            SAN_BAY_CODE = sanBay,
                            ELEMENT_CODE = elementCode,
                            OLD_VALUE = oldValue,
                            NEW_VALUE = Convert.ToDecimal(value),
                            CREATE_BY = ProfileUtilities.User.USER_NAME,
                            CREATE_DATE = DateTime.Now,
                        });
                    }
                    else if (type == "SL_TD")
                    {
                        var rowsChange = UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.TEMPLATE_CODE == templateCode && x.VERSION == version && x.TIME_YEAR == year && x.KHOAN_MUC_HANG_HOA_CODE == elementCode).ToList();
                        if (rowsChange.Count() == 0)
                        {
                            UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(new T_BP_KE_HOACH_CHI_PHI_DATA
                            {
                                PKID = Guid.NewGuid().ToString(),
                                ORG_CODE = org,
                                TEMPLATE_CODE = templateCode,
                                CHI_PHI_PROFIT_CENTER_CODE = prc,
                                KHOAN_MUC_HANG_HOA_CODE = elementCode,
                                VERSION = version,
                                TIME_YEAR = year,
                                QUANTITY_TD = Convert.ToDecimal(value),
                            });
                            UnitOfWork.Commit();
                            return;
                        }
                        var oldValue = rowsChange.FirstOrDefault().QUANTITY_TD;
                        foreach (var item in rowsChange)
                        {
                            item.QUANTITY_TD = Convert.ToDecimal(value);
                            item.AMOUNT_TD = item.PRICE_TD * Convert.ToDecimal(value);
                            UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Update(item);
                        }
                        // Lưu lịch sử
                        UnitOfWork.Repository<KeHoachChiPhiEditHistoryRepo>().Create(new T_BP_KE_HOACH_CHI_PHI_EDIT_HISTORY
                        {
                            ID = Guid.NewGuid(),
                            TEMPLATE_CODE = templateCode,
                            VERSION = version,
                            YEAR = year,
                            TYPE = "Thẩm định sản lượng",
                            COST_CENTER_CODE = costCenter,
                            SAN_BAY_CODE = sanBay,
                            ELEMENT_CODE = elementCode,
                            OLD_VALUE = oldValue,
                            NEW_VALUE = Convert.ToDecimal(value),
                            CREATE_BY = ProfileUtilities.User.USER_NAME,
                            CREATE_DATE = DateTime.Now,
                        });
                    }
                    else if (type == "DG_TD")
                    {
                        var rowsChange = UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.TEMPLATE_CODE == templateCode && x.VERSION == version && x.TIME_YEAR == year && x.KHOAN_MUC_HANG_HOA_CODE == elementCode).ToList();
                        if (rowsChange.Count() == 0)
                        {
                            UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Create(new T_BP_KE_HOACH_CHI_PHI_DATA
                            {
                                PKID = Guid.NewGuid().ToString(),
                                ORG_CODE = org,
                                TEMPLATE_CODE = templateCode,
                                CHI_PHI_PROFIT_CENTER_CODE = prc,
                                KHOAN_MUC_HANG_HOA_CODE = elementCode,
                                VERSION = version,
                                TIME_YEAR = year,
                                PRICE_TD = Convert.ToDecimal(value),
                                MONTH = month,
                            });
                            UnitOfWork.Commit();
                            return;
                        }
                        var oldValue = rowsChange.FirstOrDefault().PRICE_TD;
                        foreach (var item in rowsChange)
                        {
                            item.PRICE_TD = Convert.ToDecimal(value);
                            item.AMOUNT_TD = item.QUANTITY_TD *  item.PRICE_TD;
                            UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Update(item);
                        }
                        // Lưu lịch sử
                        UnitOfWork.Repository<KeHoachChiPhiEditHistoryRepo>().Create(new T_BP_KE_HOACH_CHI_PHI_EDIT_HISTORY
                        {
                            ID = Guid.NewGuid(),
                            TEMPLATE_CODE = templateCode,
                            VERSION = version,
                            YEAR = year,
                            TYPE = "Thẩm định đơn giá",
                            COST_CENTER_CODE = costCenter,
                            SAN_BAY_CODE = sanBay,
                            ELEMENT_CODE = elementCode,
                            OLD_VALUE = oldValue,
                            NEW_VALUE = Convert.ToDecimal(value),
                            CREATE_BY = ProfileUtilities.User.USER_NAME,
                            CREATE_DATE = DateTime.Now,
                        });
                    }
                    else if (type == "GC_TD")
                    {
                        var rowsChange = UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.TEMPLATE_CODE == templateCode && x.VERSION == version && x.TIME_YEAR == year && x.KHOAN_MUC_HANG_HOA_CODE == elementCode).ToList();
                        if (rowsChange.Count() == 0)
                        {
                            this.State = false;
                            this.ErrorMessage = "Có lỗi hệ thống xảy ra! Vui lòng liên hệ với quản trị viên!";
                            return;
                        }
                        var oldValue = rowsChange.FirstOrDefault().DESCRIPTION_TD;
                        foreach (var item in rowsChange)
                        {
                            item.DESCRIPTION_TD = value;
                            UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Update(item);
                        }
                        // Lưu lịch sử
                        UnitOfWork.Repository<KeHoachChiPhiEditHistoryRepo>().Create(new T_BP_KE_HOACH_CHI_PHI_EDIT_HISTORY
                        {
                            ID = Guid.NewGuid(),
                            TEMPLATE_CODE = templateCode,
                            VERSION = version,
                            YEAR = year,
                            TYPE = "Thẩm định ghi chú",
                            COST_CENTER_CODE = costCenter,
                            SAN_BAY_CODE = sanBay,
                            ELEMENT_CODE = elementCode,
                            OLD_VALUE = 0,
                            NEW_VALUE = 0,
                            OLD_DESCRIPTION = oldValue,
                            NEW_DESCRIPTION = value,
                            CREATE_BY = ProfileUtilities.User.USER_NAME,
                            CREATE_DATE = DateTime.Now,
                        });
                    }
                }
                UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                this.State = false;
                this.Exception = ex;
            }
        }

        public IList<T_BP_KE_HOACH_CHI_PHI_EDIT_HISTORY> GetHistoryEditElement(string templateCode, int version, int year, string elementCode)
        {
            try
            {
                return UnitOfWork.Repository<KeHoachChiPhiEditHistoryRepo>().Queryable().Where(x => x.TEMPLATE_CODE == templateCode && x.VERSION == version && x.YEAR == year && x.ELEMENT_CODE == elementCode).ToList();
            }
            catch (Exception ex)
            {
                this.State = false;
                this.Exception = ex;
                return new List<T_BP_KE_HOACH_CHI_PHI_EDIT_HISTORY>();
            }
        }

        public void InsertComment(string templateCode, int version, int year, string type, string sanBay, string costCenter, string elementCode, string value)
        {
            try
            {
                UnitOfWork.BeginTransaction();
                UnitOfWork.Repository<KeHoachChiPhiCommentRepo>().Create(new T_BP_KE_HOACH_CHI_PHI_COMMENT
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

        public IList<T_BP_KE_HOACH_CHI_PHI_COMMENT> GetCommentElement(string templateCode, int version, int year, string elementCode)
        {
            try
            {
                return UnitOfWork.Repository<KeHoachChiPhiCommentRepo>().Queryable().Where(x => x.TEMPLATE_CODE == templateCode && x.VERSION == version && x.YEAR == year && x.ELEMENT_CODE == elementCode).ToList();
            }
            catch (Exception ex)
            {
                this.State = false;
                this.Exception = ex;
                return new List<T_BP_KE_HOACH_CHI_PHI_COMMENT>();
            }
        }

        public IList<T_MD_COST_CENTER> GetCostCenter()
        {
            try
            {
                return UnitOfWork.Repository<CostCenterRepo>().GetAll().OrderBy(x => x.C_ORDER).ToList();
            }
            catch (Exception ex)
            {
                this.State = false;
                this.Exception = ex;
                return new List<T_MD_COST_CENTER>();
            }
        }

        public IList<T_BP_KE_HOACH_CHI_PHI_DEPARTMENT_ASSIGN> GetDepartmentAssignElement(string templateCode, int version, int year, string elementCode)
        {
            try
            {
                return UnitOfWork.Repository<KeHoachChiPhiDepartmentAssignRepo>().Queryable().Where(x => x.TEMPLATE_CODE == templateCode && x.VERSION == version && x.YEAR == year && x.ELEMENT_CODE == elementCode).ToList();
            }
            catch (Exception ex)
            {
                this.State = false;
                this.Exception = ex;
                return new List<T_BP_KE_HOACH_CHI_PHI_DEPARTMENT_ASSIGN>();
            }
        }

        public void AssignDepartment(string templateCode, int version, int year, string elementCode, string value)
        {
            try
            {
                UnitOfWork.BeginTransaction();
                UnitOfWork.Repository<KeHoachChiPhiDepartmentAssignRepo>().Create(new T_BP_KE_HOACH_CHI_PHI_DEPARTMENT_ASSIGN
                {
                    ID = Guid.NewGuid(),
                    TEMPLATE_CODE = templateCode,
                    VERSION = version,
                    YEAR = year,
                    ELEMENT_CODE = elementCode,
                    DEPARTMENT_CODE = value,
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

        public void UnAssignDepartment(string templateCode, int version, int year, string elementCode, string value)
        {
            try
            {
                UnitOfWork.BeginTransaction();
                var department = UnitOfWork.Repository<KeHoachChiPhiDepartmentAssignRepo>().Queryable().Where(x => x.DEPARTMENT_CODE == value && x.TEMPLATE_CODE == templateCode && x.VERSION == version && x.YEAR == year && x.ELEMENT_CODE == elementCode).FirstOrDefault();
                if (department != null)
                {
                    UnitOfWork.Repository<KeHoachChiPhiDepartmentAssignRepo>().Delete(department);
                }
                UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                this.State = false;
                this.Exception = ex;
            }
        }

        public void Expertise(string templateCode, int version, int year, string elementCode)
        {
            try
            {
                UnitOfWork.BeginTransaction();
                UnitOfWork.Repository<KeHoachChiPhiDepartmentExpertiseRepo>().Create(new T_BP_KE_HOACH_CHI_PHI_DEPARTMENT_EXPERTISE
                {
                    ID = Guid.NewGuid(),
                    TEMPLATE_CODE = templateCode,
                    VERSION = version,
                    YEAR = year,
                    ELEMENT_CODE = elementCode,
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

        public void UnExpertise(string templateCode, int version, int year, string elementCode)
        {
            try
            {
                UnitOfWork.BeginTransaction();
                var KhoanMuc = UnitOfWork.Repository<KeHoachChiPhiDepartmentExpertiseRepo>().Queryable().Where(x => x.TEMPLATE_CODE == templateCode && x.VERSION == version && x.YEAR == year && x.ELEMENT_CODE == elementCode).FirstOrDefault();
                if (KhoanMuc != null)
                {
                    UnitOfWork.Repository<KeHoachChiPhiDepartmentExpertiseRepo>().Delete(KhoanMuc);
                }
                UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                this.State = false;
                this.Exception = ex;
            }
        }

        public override void Search()
        {
            base.Search();
            var departmentAssign = UnitOfWork.Repository<KeHoachChiPhiDepartmentAssignRepo>().Queryable().Where(x => x.DEPARTMENT_CODE == ProfileUtilities.User.Organize.CODE).FirstOrDefault();
            if (departmentAssign != null)
            {
                if (string.IsNullOrEmpty(ObjDetail.STATUS))
                {
                    var lstTemplate = UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TEMPLATE_CODE == departmentAssign.TEMPLATE_CODE && x.VERSION == departmentAssign.VERSION && x.TIME_YEAR == departmentAssign.YEAR && x.PHIEN_BAN == ObjDetail.PHIEN_BAN && x.KICH_BAN == ObjDetail.KICH_BAN);
                    ObjList.AddRange(lstTemplate);
                    this.ObjList = this.ObjList.Where(x=>x.PHIEN_BAN==ObjDetail.PHIEN_BAN && x.TIME_YEAR==ObjDetail.TIME_YEAR&& x.KICH_BAN==ObjDetail.KICH_BAN).ToList();
                }
                else if(ObjDetail.STATUS=="05")
                {
                    var lstTemplate = UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TEMPLATE_CODE == departmentAssign.TEMPLATE_CODE && x.VERSION == departmentAssign.VERSION && x.TIME_YEAR == departmentAssign.YEAR && x.PHIEN_BAN == ObjDetail.PHIEN_BAN && x.KICH_BAN == ObjDetail.KICH_BAN);
                    ObjList.AddRange(lstTemplate);
                    this.ObjList = this.ObjList.Where(x => x.PHIEN_BAN == ObjDetail.PHIEN_BAN && x.TIME_YEAR == ObjDetail.TIME_YEAR && x.KICH_BAN == ObjDetail.KICH_BAN && x.IS_DELETED==true).ToList();
                }
                else
                {
                    var lstTemplate = UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TEMPLATE_CODE == departmentAssign.TEMPLATE_CODE && x.VERSION == departmentAssign.VERSION && x.TIME_YEAR == departmentAssign.YEAR && x.PHIEN_BAN == ObjDetail.PHIEN_BAN && x.KICH_BAN == ObjDetail.KICH_BAN);
                    ObjList.AddRange(lstTemplate);
                    this.ObjList = this.ObjList.Where(x => x.PHIEN_BAN == ObjDetail.PHIEN_BAN && x.TIME_YEAR == ObjDetail.TIME_YEAR && x.KICH_BAN == ObjDetail.KICH_BAN && x.STATUS==ObjDetail.STATUS).ToList();
                }
               
            }
        }



    }
}
