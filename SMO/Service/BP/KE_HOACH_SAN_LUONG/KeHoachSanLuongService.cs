using NHibernate.SqlCommand;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;

using SMO.AppCode.Utilities;
using SMO.Core.Entities;
using SMO.Core.Entities.BP;
using SMO.Core.Entities.BP.KE_HOACH_SAN_LUONG;
using SMO.Core.Entities.BP.KE_HOACH_SAN_LUONG.KE_HOACH_SAN_LUONG_DATA_BASE;
using SMO.Core.Entities.MD;
using SMO.Helper;
using SMO.Models;
using SMO.Repository.Implement.BP;
using SMO.Repository.Implement.BP.KE_HOACH_SAN_LUONG;
using SMO.Repository.Implement.BP.KE_HOACH_SAN_LUONG.KE_HOACH_SAN_LUONG_DATA_BASE;
using SMO.Repository.Implement.MD;
using SMO.Service.Class;
using SMO.Service.Common;
using SMO.ServiceInterface.BP.KeHoachSanLuong;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.HtmlControls;
using static SMO.SelectListUtilities;

namespace SMO.Service.BP.KE_HOACH_SAN_LUONG
{
    public class KeHoachSanLuongService : BaseBPService<T_BP_KE_HOACH_SAN_LUONG, KeHoachSanLuongRepo, T_MD_KHOAN_MUC_SAN_LUONG, T_BP_KE_HOACH_SAN_LUONG_VERSION, T_BP_KE_HOACH_SAN_LUONG_HISTORY, KeHoachSanLuongHistoryRepo>, IKeHoachSanLuongService
    {
        private readonly List<Point> InvalidCellsList;
        private readonly List<string> ListColumnName;
        private readonly List<string> ListColumnNameDataBase;
        private int StartRowData;
        public List<T_BP_KE_HOACH_SAN_LUONG_HISTORY> ObjListHistory { get; set; }
        public List<T_BP_KE_HOACH_SAN_LUONG_VERSION> ObjListVersion { get; set; }
        public List<T_BP_KE_HOACH_SAN_LUONG_SUM_UP_DETAIL> ObjListSumUpHistory { get; set; }
        public KeHoachSanLuongService()
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
            this.ObjListHistory = new List<T_BP_KE_HOACH_SAN_LUONG_HISTORY>();
            this.ObjListVersion = new List<T_BP_KE_HOACH_SAN_LUONG_VERSION>();
            this.ObjListSumUpHistory = new List<T_BP_KE_HOACH_SAN_LUONG_SUM_UP_DETAIL>();
        }

        /// <summary>
        /// check xem người dùng hiện tại có hiển thị nút thẩm định hay không
        /// </summary>
        /// <returns></returns>
        public override bool ShowReviewBtn()
        {
            return ShowReviewBtn(ObjDetail.TIME_YEAR);
        }

        public List<ViewDataQuantityPlan> ExportData(string templateCode, int year, int version, string orgCode, string nhomSanBay, string chiNhanh)
        {
            var result = UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.ORG_CODE == orgCode && x.TEMPLATE_CODE == templateCode && x.VERSION == version && x.TIME_YEAR == year).OrderBy(x => x.SanLuongProfitCenter.SAN_BAY_CODE).ThenBy(x => x.SanLuongProfitCenter.HANG_HANG_KHONG_CODE).ToList();
            if (!string.IsNullOrEmpty(nhomSanBay))
            {
                result = result.Where(x => x.SanLuongProfitCenter.SanBay.NHOM_SAN_BAY_CODE == nhomSanBay).ToList();
            }
            if (!string.IsNullOrEmpty(chiNhanh))
            {
                result = result.Where(x => x.SanLuongProfitCenter.SanBay.AREA_CODE == chiNhanh).ToList();
            }

            var data = new List<ViewDataQuantityPlan>();

            foreach (var i in result)
            {
                data.Add(new ViewDataQuantityPlan
                {
                    Airport = i.SanLuongProfitCenter?.SAN_BAY_CODE + " - " + i.SanLuongProfitCenter?.SanBay?.NAME,
                    Airlines = i.SanLuongProfitCenter?.HANG_HANG_KHONG_CODE + " - " + i.SanLuongProfitCenter?.HangHangKhong?.NAME,
                    Element = i.KhoanMucSanLuong?.NAME,
                    Jan = i.VALUE_JAN,
                    Feb = i.VALUE_FEB,
                    Mar = i.VALUE_MAR,
                    Apr = i.VALUE_APR,
                    May = i.VALUE_MAY,
                    Jun = i.VALUE_JUN,
                    Jul = i.VALUE_JUL,
                    Aug = i.VALUE_AUG,
                    Sep = i.VALUE_SEP,
                    Oct = i.VALUE_OCT,
                    Nov = i.VALUE_NOV,
                    Dec = i.VALUE_DEC,
                    Total = i.VALUE_SUM_YEAR,
                    Average = i.VALUE_SUM_YEAR / 12,
                    Des = i.DESCRIPTION
                });
            }
            return data;
        }

        public List<ViewDataQuantityPlanYear> ExportDataYear(string templateCode, int year, int version, string orgCode, string nhomSanBay, string chiNhanh)
        {
            var result = UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.ORG_CODE == orgCode && x.TEMPLATE_CODE == templateCode && x.VERSION == version && x.TIME_YEAR == year).OrderBy(x => x.SanLuongProfitCenter.SAN_BAY_CODE).ThenBy(x => x.SanLuongProfitCenter.HANG_HANG_KHONG_CODE).ToList();
            var data = new List<ViewDataQuantityPlanYear>();
            if (!string.IsNullOrEmpty(nhomSanBay))
            {
                result = result.Where(x => x.SanLuongProfitCenter.SanBay.NHOM_SAN_BAY_CODE == nhomSanBay).ToList();
            }
            if (!string.IsNullOrEmpty(chiNhanh))
            {
                result = result.Where(x => x.SanLuongProfitCenter.SanBay.AREA_CODE == chiNhanh).ToList();
            }
            var lstAirports = result.GroupBy(x => x.SanLuongProfitCenter.SAN_BAY_CODE).Select(x => x.First().SanLuongProfitCenter).ToList();

            foreach (var i in lstAirports)
            {
                data.Add(new ViewDataQuantityPlanYear
                {
                    Airport = i.SAN_BAY_CODE,
                    AirportName = i.SanBay?.NAME,
                    Vna = result.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE == i.SAN_BAY_CODE && x.SanLuongProfitCenter.HangHangKhong.IS_VNA == true).Sum(x =>x.VALUE_SUM_YEAR) ?? 0,
                    NotVna = result.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE == i.SAN_BAY_CODE && x.SanLuongProfitCenter.HangHangKhong.IS_VNA == false && x.SanLuongProfitCenter.HangHangKhong.TYPE == "ND").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                    SumNd = result.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE == i.SAN_BAY_CODE  && x.SanLuongProfitCenter.HangHangKhong.TYPE == "ND").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                    SumQt = result.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE == i.SAN_BAY_CODE && x.SanLuongProfitCenter.HangHangKhong.TYPE == "QT").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                    Total = result.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE == i.SAN_BAY_CODE).Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                });
            }
            return data;
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
                var historyReview = UnitOfWork.Repository<KeHoachSanLuongReviewRepo>()
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
                        ModulType = ModulType.KeHoachSanLuong,
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

                UnitOfWork.Repository<KeHoachSanLuongHistoryRepo>()
                    .Create(new T_BP_KE_HOACH_SAN_LUONG_HISTORY()
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
                UnitOfWork.GetSession().CreateSQLQuery($"UPDATE T_BP_KE_HOACH_SAN_LUONG_DATA SET STATUS = '{Approve_Status.ChoPheDuyet}' WHERE ORG_CODE = '{ObjDetail.ORG_CODE}' AND TEMPLATE_CODE = '{ObjDetail.TEMPLATE_CODE}' AND TIME_YEAR = {ObjDetail.TIME_YEAR}").ExecuteUpdate();

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

                UnitOfWork.Repository<KeHoachSanLuongHistoryRepo>().Create(new T_BP_KE_HOACH_SAN_LUONG_HISTORY()
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
                UnitOfWork.GetSession().CreateSQLQuery($"UPDATE T_BP_KE_HOACH_SAN_LUONG_DATA SET STATUS = '{Approve_Status.ChuaTrinhDuyet}' WHERE ORG_CODE = '{ObjDetail.ORG_CODE}' AND TEMPLATE_CODE = '{ObjDetail.TEMPLATE_CODE}' AND TIME_YEAR = {ObjDetail.TIME_YEAR}").ExecuteUpdate();

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

                UnitOfWork.Repository<KeHoachSanLuongHistoryRepo>()
                    .Create(new T_BP_KE_HOACH_SAN_LUONG_HISTORY()
                    {
                        PKID = Guid.NewGuid().ToString(),
                        ORG_CODE = ObjDetail.ORG_CODE,
                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                        VERSION = ObjDetail.VERSION,
                        KICH_BAN = ObjDetail.KICH_BAN,
                        PHIEN_BAN = ObjDetail.PHIEN_BAN,
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

                UnitOfWork.Repository<KeHoachSanLuongHistoryRepo>().Create(new T_BP_KE_HOACH_SAN_LUONG_HISTORY()
                {
                    PKID = Guid.NewGuid().ToString(),
                    ORG_CODE = ObjDetail.ORG_CODE,
                    TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                    VERSION = ObjDetail.VERSION,
                    TIME_YEAR = ObjDetail.TIME_YEAR,
                    KICH_BAN = ObjDetail.KICH_BAN,
                    PHIEN_BAN = ObjDetail.PHIEN_BAN,
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

                UnitOfWork.Repository<KeHoachSanLuongHistoryRepo>().Create(new T_BP_KE_HOACH_SAN_LUONG_HISTORY()
                {
                    PKID = Guid.NewGuid().ToString(),
                    ORG_CODE = ObjDetail.ORG_CODE,
                    TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                    VERSION = ObjDetail.VERSION,
                    TIME_YEAR = ObjDetail.TIME_YEAR,
                    KICH_BAN = ObjDetail.KICH_BAN,
                    PHIEN_BAN = ObjDetail.PHIEN_BAN,
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

                UnitOfWork.Repository<KeHoachSanLuongHistoryRepo>().Create(new T_BP_KE_HOACH_SAN_LUONG_HISTORY()
                {
                    PKID = Guid.NewGuid().ToString(),
                    ORG_CODE = ObjDetail.ORG_CODE,
                    TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                    VERSION = ObjDetail.VERSION,
                    TIME_YEAR = ObjDetail.TIME_YEAR,
                    KICH_BAN = ObjDetail.KICH_BAN,
                    PHIEN_BAN = ObjDetail.PHIEN_BAN,
                    ACTION = Approve_Action.PheDuyetDuLieu,
                    ACTION_USER = currentUser,
                    ACTION_DATE = DateTime.Now,
                    CREATE_BY = currentUser
                });

                //TODO: Cập nhật lại code thực thi
                UnitOfWork.GetSession().CreateSQLQuery($"UPDATE T_BP_KE_HOACH_SAN_LUONG_DATA SET STATUS = '{Approve_Status.DaPheDuyet}' WHERE ORG_CODE = '{ObjDetail.ORG_CODE}' AND TEMPLATE_CODE = '{ObjDetail.TEMPLATE_CODE}' AND TIME_YEAR = {ObjDetail.TIME_YEAR}").ExecuteUpdate();

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
                UnitOfWork.Repository<KeHoachSanLuongVersionRepo>().Update(x => x.TEMPLATE_CODE == ObjDetail.TEMPLATE_CODE && x.TIME_YEAR == ObjDetail.TIME_YEAR && x.VERSION == ObjDetail.VERSION, x => x.IS_DELETED = 1, x => x.UPDATE_BY = currentUser);

                // update main table
                ObjDetail.IS_DELETED = true; CurrentRepository.Update(ObjDetail);

                // create history log
                UnitOfWork.Repository<KeHoachSanLuongHistoryRepo>().Create(new T_BP_KE_HOACH_SAN_LUONG_HISTORY
                {
                    PKID = Guid.NewGuid().ToString(),
                    ORG_CODE = ObjDetail.ORG_CODE,
                    TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                    VERSION = ObjDetail.VERSION,
                    KICH_BAN = ObjDetail.KICH_BAN,
                    PHIEN_BAN = ObjDetail.PHIEN_BAN,
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

                UnitOfWork.Repository<KeHoachSanLuongHistoryRepo>().Create(new T_BP_KE_HOACH_SAN_LUONG_HISTORY()
                {
                    PKID = Guid.NewGuid().ToString(),
                    ORG_CODE = ObjDetail.ORG_CODE,
                    TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                    VERSION = ObjDetail.VERSION,
                    TIME_YEAR = ObjDetail.TIME_YEAR,
                    KICH_BAN = ObjDetail.KICH_BAN,
                    PHIEN_BAN = ObjDetail.PHIEN_BAN,
                    ACTION = Approve_Action.HuyPheDuyet,
                    ACTION_USER = currentUser,
                    ACTION_DATE = DateTime.Now,
                    CREATE_BY = currentUser
                });

                //TODO: Cập nhật lại code thực thi
                UnitOfWork.GetSession().CreateSQLQuery($"UPDATE T_BP_KE_HOACH_SAN_LUONG_DATA SET STATUS = '{Approve_Status.ChoPheDuyet}' WHERE ORG_CODE = '{ObjDetail.ORG_CODE}' AND TEMPLATE_CODE = '{ObjDetail.TEMPLATE_CODE}' AND TIME_YEAR = {ObjDetail.TIME_YEAR}").ExecuteUpdate();

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

                UnitOfWork.Repository<KeHoachSanLuongHistoryRepo>().Create(new T_BP_KE_HOACH_SAN_LUONG_HISTORY()
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
                UnitOfWork.GetSession().CreateSQLQuery($"UPDATE T_BP_KE_HOACH_SAN_LUONG_DATA SET STATUS = '{Approve_Status.TuChoi}' WHERE ORG_CODE = '{ObjDetail.ORG_CODE}' AND TEMPLATE_CODE = '{ObjDetail.TEMPLATE_CODE}' AND TIME_YEAR = {ObjDetail.TIME_YEAR}").ExecuteUpdate();

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

                UnitOfWork.Repository<KeHoachSanLuongHistoryRepo>()
                    .Create(new T_BP_KE_HOACH_SAN_LUONG_HISTORY()
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
                UnitOfWork.Repository<KeHoachSanLuongReviewRepo>()
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
                var reviewHeader = UnitOfWork.Repository<KeHoachSanLuongReviewRepo>()
                    .GetFirstWithFetch(x => x.ORG_CODE == orgCode && x.TIME_YEAR == year && x.DATA_VERSION == version && !x.IS_SUMMARY && x.REVIEW_USER == currentUser);
                if (reviewHeader != null)
                {
                    reviewHeader.IS_END = true;
                    UnitOfWork.Repository<KeHoachSanLuongReviewRepo>().Update(reviewHeader);
                }

                var totalReviewUsers = UnitOfWork.Repository<UserReviewRepo>()
                    .GetManyWithFetch(x => x.TIME_YEAR == ObjDetail.TIME_YEAR).Count;

                var totalUsersEndReview = UnitOfWork.Repository<KeHoachSanLuongReviewRepo>()
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

                UnitOfWork.Repository<KeHoachSanLuongHistoryRepo>()
                    .Create(new T_BP_KE_HOACH_SAN_LUONG_HISTORY()
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

                var review = UnitOfWork.Repository<KeHoachSanLuongReviewRepo>()
                    .GetFirstWithFetch(x => x.ORG_CODE == orgCode
                                                && x.TIME_YEAR == year
                                                && x.DATA_VERSION == version
                                                && !x.IS_SUMMARY
                                                && x.REVIEW_USER == currentUser);
                if (review == null)
                {
                    var reviewId = Guid.NewGuid().ToString();
                    UnitOfWork.Repository<KeHoachSanLuongReviewRepo>().Create(
                        new T_BP_KE_HOACH_SAN_LUONG_REVIEW
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
                    var plReviewService = new KeHoachSanLuongReviewService();
                    plReviewService.ObjDetail.TIME_YEAR = year;
                    plReviewService.ObjDetail.ORG_CODE = orgCode;
                    var dataCost = plReviewService.SummaryCenterVersion(out IList<T_BP_KE_HOACH_SAN_LUONG_DATA> detailKhoanMucSanLuongs);
                    dataCost = dataCost.OrderBy(x => x.C_ORDER)
                                                    .GroupBy(x => x.CODE)
                                                    .Select(x => x.First()).ToList();
                    var elements = plReviewService.PrepareListReview(dataCost);
                    UnitOfWork.Repository<KeHoachSanLuongReviewResultRepo>()
                        .Create((from e in elements.Where(x => !x.IS_GROUP)
                                 select new T_BP_KE_HOACH_SAN_LUONG_REVIEW_RESULT
                                 {
                                     PKID = Guid.NewGuid().ToString(),
                                     KHOAN_MUC_SAN_LUONG_CODE = e.CODE,
                                     HEADER_ID = reviewId,
                                     RESULT = false,
                                     TIME_YEAR = year,
                                     CREATE_BY = currentUser
                                 }).ToList());
                }
                else
                {
                    review.IS_END = true;
                    UnitOfWork.Repository<KeHoachSanLuongReviewRepo>().Update(review);
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

                UnitOfWork.Repository<KeHoachSanLuongHistoryRepo>()
                    .Create(new T_BP_KE_HOACH_SAN_LUONG_HISTORY()
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
                UnitOfWork.GetSession().CreateSQLQuery($"UPDATE T_BP_KE_HOACH_SAN_LUONG_DATA SET STATUS = '{Approve_Status.TKS_PheDuyet}' WHERE ORG_CODE = '{ObjDetail.ORG_CODE}' AND TEMPLATE_CODE = '{ObjDetail.TEMPLATE_CODE}' AND TIME_YEAR = {ObjDetail.TIME_YEAR}").ExecuteUpdate();

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
                UnitOfWork.Repository<KeHoachSanLuongHistoryRepo>()
                    .Create(new T_BP_KE_HOACH_SAN_LUONG_HISTORY()
                    {
                        PKID = Guid.NewGuid().ToString(),
                        ORG_CODE = ObjDetail.ORG_CODE,
                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                        NOTES = note,
                        KICH_BAN = ObjDetail.KICH_BAN,
                        PHIEN_BAN = ObjDetail.PHIEN_BAN,
                        VERSION = ObjDetail.VERSION,
                        TIME_YEAR = ObjDetail.TIME_YEAR,
                        ACTION = Approve_Action.TuChoiKiemSoat,
                        ACTION_USER = currentUser,
                        ACTION_DATE = DateTime.Now,
                        CREATE_BY = currentUser
                    });

                //TODO: Cập nhật lại code thực thi
                UnitOfWork.GetSession().CreateSQLQuery($"UPDATE T_BP_KE_HOACH_SAN_LUONG_DATA SET STATUS = '{Approve_Status.TKS_TuChoi}' WHERE ORG_CODE = '{ObjDetail.ORG_CODE}' AND TEMPLATE_CODE = '{ObjDetail.TEMPLATE_CODE}' AND TIME_YEAR = {ObjDetail.TIME_YEAR}").ExecuteUpdate();
                UnitOfWork.Repository<KeHoachSanLuongReviewRepo>()
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

                UnitOfWork.Repository<KeHoachSanLuongHistoryRepo>()
                    .Create(new T_BP_KE_HOACH_SAN_LUONG_HISTORY()
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
                UnitOfWork.GetSession().CreateSQLQuery($"UPDATE T_BP_KE_HOACH_SAN_LUONG_DATA SET STATUS = '{Approve_Status.TKS_TrinhDuyet}' WHERE ORG_CODE = '{ObjDetail.ORG_CODE}' AND TEMPLATE_CODE = '{ObjDetail.TEMPLATE_CODE}' AND TIME_YEAR = {ObjDetail.TIME_YEAR}").ExecuteUpdate();
                UnitOfWork.Repository<KeHoachSanLuongReviewRepo>()
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
                UnitOfWork.Repository<KeHoachSanLuongHistoryRepo>()
                    .Create(new T_BP_KE_HOACH_SAN_LUONG_HISTORY()
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
                UnitOfWork.Repository<KeHoachSanLuongHistoryRepo>()
                    .Create(new T_BP_KE_HOACH_SAN_LUONG_HISTORY()
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
                       ModulType = ModulType.KeHoachSanLuong,
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

                UnitOfWork.Repository<KeHoachSanLuongHistoryRepo>()
                    .Create(new T_BP_KE_HOACH_SAN_LUONG_HISTORY()
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
                    UnitOfWork.Repository<KeHoachSanLuongReviewRepo>().Update(x => x.DATA_VERSION == ObjDetail.VERSION
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

                UnitOfWork.Repository<KeHoachSanLuongHistoryRepo>()
                    .Create(new T_BP_KE_HOACH_SAN_LUONG_HISTORY()
                    {
                        PKID = Guid.NewGuid().ToString(),
                        ORG_CODE = ObjDetail.ORG_CODE,
                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                        VERSION = ObjDetail.VERSION,
                        KICH_BAN = ObjDetail.KICH_BAN,
                        PHIEN_BAN = ObjDetail.PHIEN_BAN,
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
                List<string> lstChildOrg =new List<string>();
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
                var listTemplateCode = this.UnitOfWork.Repository<TemplateDetailKeHoachSanLuongRepo>().Queryable().Where(
                        x => lstChildOrg.Contains(x.CENTER_CODE) && x.TIME_YEAR == this.ObjDetail.TIME_YEAR
                        && x.TIME_YEAR == (ObjDetail.TIME_YEAR == 0 ? DateTime.Now.Year : ObjDetail.TIME_YEAR)
                    ).Select(x => x.TEMPLATE_CODE).Distinct().ToList();
                var findKeHoachSanLuong = this.CurrentRepository.Queryable().Where(
                        x => listTemplateCode.Contains(x.TEMPLATE_CODE) && !lstChildOrg.Contains(x.ORG_CODE) && x.TIME_YEAR == this.ObjDetail.TIME_YEAR
                    );

                this.ObjList.AddRange(findKeHoachSanLuong);
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
                var listTemplateCode = this.UnitOfWork.Repository<TemplateDetailKeHoachSanLuongRepo>().Queryable().Where(
                        x => lstChildOrg.Contains(x.CENTER_CODE) && x.TIME_YEAR == this.ObjDetail.TIME_YEAR
                        && x.TIME_YEAR == (ObjDetail.TIME_YEAR == 0 ? DateTime.Now.Year : ObjDetail.TIME_YEAR)
                    ).Select(x => x.TEMPLATE_CODE).Distinct().ToList();
                var findKeHoachSanLuong = this.CurrentRepository.Queryable().Where(
                        x => listTemplateCode.Contains(x.TEMPLATE_CODE) && !lstChildOrg.Contains(x.ORG_CODE) && x.TIME_YEAR == this.ObjDetail.TIME_YEAR
                    );

                this.ObjList.AddRange(findKeHoachSanLuong);
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
            var query = UnitOfWork.GetSession().QueryOver<T_BP_KE_HOACH_SAN_LUONG_SUM_UP_DETAIL>();
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
        private (IList<T_BP_KE_HOACH_SAN_LUONG_DATA>, bool) GetDataSumUp(int year, string centerCode, string elementCode, int sumUpVersion)
        {
            var plDataRepo = UnitOfWork.Repository<KeHoachSanLuongDataRepo>();
            var costPlReviewCommentRepo = UnitOfWork.Repository<KeHoachSanLuongReviewCommentRepo>();
            var plVersionRepo = UnitOfWork.Repository<KeHoachSanLuongRepo>();
            var plDataHistoryRepo = UnitOfWork.Repository<KeHoachSanLuongDataHistoryRepo>();

            var lstDetails = UnitOfWork.Repository<KeHoachSanLuongSumUpDetailRepo>()
                .GetManyByExpression(x => x.ORG_CODE == centerCode &&
                    x.TIME_YEAR == year &&
                    x.SUM_UP_VERSION == sumUpVersion);
            var lookup = lstDetails.ToLookup(x => x.FROM_ORG_CODE);
            var lstChildren = GetListOfChildrenCenter(centerCode).Select(x => x.CODE);

            var isCorp = string.IsNullOrEmpty(GetCenter(centerCode).PARENT_CODE);
            var isLeafCenter = IsLeaf(centerCode);

            var data = new List<T_BP_KE_HOACH_SAN_LUONG_DATA>();
            foreach (var key in lookup.Select(x => x.Key))
            {
                var costPL = new List<T_BP_KE_HOACH_SAN_LUONG_DATA>();
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
                data.AddRange(costPL.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == elementCode &&
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
        public override IEnumerable<T_MD_KHOAN_MUC_SAN_LUONG> GetDetailSumUp(string centerCode, string elementCode, int year, int version, int? sumUpVersion, bool isCountComments, bool? isShowFile = null)
        {
            var plDataRepo = UnitOfWork.Repository<KeHoachSanLuongDataRepo>();
            var costPlReviewCommentRepo = UnitOfWork.Repository<KeHoachSanLuongReviewCommentRepo>();
            var plVersionRepo = UnitOfWork.Repository<KeHoachSanLuongRepo>();
            var plDataHistoryRepo = UnitOfWork.Repository<KeHoachSanLuongDataHistoryRepo>();

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
                        var element = (T_MD_KHOAN_MUC_SAN_LUONG)item;
                        element.IS_GROUP = isShowFile.HasValue; // count comment is in the review, drill down will show the file base so it still will be group
                        yield return element;
                    }
                    //if (lookupData[key].First().Template.IS_BASE)
                    //{
                    //    var isNewestVersion = UnitOfWork.Repository<KeHoachSanLuongVersionRepo>()
                    //        .GetNewestByExpression(x => x.TEMPLATE_CODE == key && x.TIME_YEAR == year,
                    //        order: x => x.VERSION, isDescending: true)
                    //        .VERSION == lookupData[key].First().VERSION;
                    //    foreach (var item in lookupData[key])
                    //    {
                    //        var lstElements = lookupData[key].Where(x => x.SAN_LUONG_PROFIT_CENTER_CODE == item.SAN_LUONG_PROFIT_CENTER_CODE);
                    //        var element = (T_MD_KHOAN_MUC_SAN_LUONG)item;
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
                    //        var baseData = UnitOfWork.Repository<KeHoachSanLuongDataBaseRepo>()
                    //            .GetManyWithFetch(x => x.KHOAN_MUC_SAN_LUONG_CODE == elementCode && x.ORG_CODE == centerCode && x.TEMPLATE_CODE == key && x.VERSION == lookupData[key].First().VERSION && x.TIME_YEAR == year);
                    //        foreach (var item in baseData)
                    //        {
                    //            yield return (T_MD_KHOAN_MUC_SAN_LUONG)item;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        // get data from base history
                    //        var baseDataHistory = UnitOfWork.Repository<KeHoachSanLuongDataBaseHistoryRepo>()
                    //            .GetManyWithFetch(x => x.KHOAN_MUC_SAN_LUONG_CODE == elementCode && x.ORG_CODE == centerCode && x.TEMPLATE_CODE == key && x.VERSION == lookupData[key].First().VERSION && x.TIME_YEAR == year);

                    //        foreach (var item in baseDataHistory)
                    //        {
                    //            yield return (T_MD_KHOAN_MUC_SAN_LUONG)item;
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    foreach (var item in lookupData[key])
                    //    {
                    //        var element = (T_MD_KHOAN_MUC_SAN_LUONG)item;
                    //        element.IS_GROUP = isShowFile.HasValue; // count comment is in the review, drill down will show the file base so it still will be group
                    //        yield return element;
                    //    }
                    //}
                }
            }
            else
            {
                var comments = new List<T_BP_KE_HOACH_SAN_LUONG_REVIEW_COMMENT>();
                if (isCountComments)
                {
                    comments = costPlReviewCommentRepo.GetManyWithFetch(
                            x => x.TIME_YEAR == year &&
                            x.ORG_CODE == GetCorp().CODE &&
                            x.KHOAN_MUC_SAN_LUONG_CODE == elementCode).ToList();
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
                    yield return new T_MD_KHOAN_MUC_SAN_LUONG
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
                        Values = new decimal[14]
                            {
                                lookupData[key].Sum(x => x.VALUE_JAN) ?? 0,
                                lookupData[key].Sum(x => x.VALUE_FEB) ?? 0,
                                lookupData[key].Sum(x => x.VALUE_MAR) ?? 0,
                                lookupData[key].Sum(x => x.VALUE_APR) ?? 0,
                                lookupData[key].Sum(x => x.VALUE_MAY) ?? 0,
                                lookupData[key].Sum(x => x.VALUE_JUN) ?? 0,
                                lookupData[key].Sum(x => x.VALUE_JUL) ?? 0,
                                lookupData[key].Sum(x => x.VALUE_AUG) ?? 0,
                                lookupData[key].Sum(x => x.VALUE_SEP) ?? 0,
                                lookupData[key].Sum(x => x.VALUE_OCT) ?? 0,
                                lookupData[key].Sum(x => x.VALUE_NOV) ?? 0,
                                lookupData[key].Sum(x => x.VALUE_DEC) ?? 0,
                                lookupData[key].Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                                lookupData[key].Sum(x => x.VALUE_SUM_YEAR_PREVENTIVE) ?? 0,
                            },
                        NUMBER_COMMENTS = isCountComments ? isChild ?
                        $"{parentComments}" :
                        $"{parentComments + childComments}|{parentComments}" : $"{childComments}|0"
                    };
                }
            }
        }


        public override IEnumerable<T_MD_KHOAN_MUC_SAN_LUONG> GetDetailSumUpTemplate(string elementCode, int year, int version, string templateCode, string centerCode)
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
                    var baseData = UnitOfWork.Repository<KeHoachSanLuongDataBaseRepo>()
                        .GetManyWithFetch(x => x.TEMPLATE_CODE == templateCode
                        && x.TIME_YEAR == year
                        && x.KHOAN_MUC_SAN_LUONG_CODE == elementCode
                        && x.VERSION == version
                        && x.SAN_LUONG_PROFIT_CENTER_CODE == centerCode);
                    return from item in baseData
                           select (T_MD_KHOAN_MUC_SAN_LUONG)item;
                }
                else
                {
                    // old data
                    // get data in table base data history
                    var baseData = UnitOfWork.Repository<KeHoachSanLuongDataBaseHistoryRepo>()
                        .GetManyWithFetch(x => x.TEMPLATE_CODE == templateCode
                        && x.TIME_YEAR == year
                        && x.KHOAN_MUC_SAN_LUONG_CODE == elementCode
                        && x.VERSION == version
                        && x.SAN_LUONG_PROFIT_CENTER_CODE == centerCode);
                    return from item in baseData
                           select (T_MD_KHOAN_MUC_SAN_LUONG)item;
                }
            }
        }

        public IList<T_MD_KHOAN_MUC_SAN_LUONG> GetDataCost(out IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_SAN_LUONG> detailOtherKhoanMucSanLuongs,
            out IList<T_BP_KE_HOACH_SAN_LUONG_DATA> detailOtherCostData,
            out bool isDrillDownApply,
            ViewDataCenterModel model)
        {
            isDrillDownApply = model.IS_DRILL_DOWN;
            if (!model.IS_HAS_NOT_VALUE && !model.IS_HAS_VALUE &&
                (!string.IsNullOrEmpty(model.TEMPLATE_CODE) || model.VERSION == null || model.VERSION.Value == -1))
            {
                detailOtherCostData = null;
                detailOtherKhoanMucSanLuongs = null;
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
                var elements = GetDataCostPreview(out detailOtherKhoanMucSanLuongs, model.TEMPLATE_CODE, lstOrgs, model.YEAR, model.VERSION, isHasValue);
                if (!string.IsNullOrEmpty(model.HANG_HANG_KHONG_CODE) || !string.IsNullOrEmpty(model.SAN_BAY_CODE) || !string.IsNullOrEmpty(model.AREA_CODE) || !string.IsNullOrEmpty(model.NHOM_SAN_BAY_CODE))
                {
                    var sanbayCode = UnitOfWork.Repository<SanBayRepo>().Queryable().Where(x => x.AREA_CODE == model.AREA_CODE || x.NHOM_SAN_BAY_CODE == model.NHOM_SAN_BAY_CODE).Select(x => x.CODE).ToList();
                    var centerCode = UnitOfWork.Repository<SanLuongProfitCenterRepo>().Queryable().Where(x => x.HANG_HANG_KHONG_CODE == model.HANG_HANG_KHONG_CODE || x.SAN_BAY_CODE == model.SAN_BAY_CODE || sanbayCode.Contains(x.SAN_BAY_CODE)).Select(x => x.CODE).ToList();
                    if (!string.IsNullOrEmpty(model.HANG_HANG_KHONG_CODE) && !string.IsNullOrEmpty(model.SAN_BAY_CODE))
                    {
                        centerCode = UnitOfWork.Repository<SanLuongProfitCenterRepo>().Queryable().Where(x => x.HANG_HANG_KHONG_CODE == model.HANG_HANG_KHONG_CODE && x.SAN_BAY_CODE == model.SAN_BAY_CODE).Select(x => x.CODE).ToList();
                    }
                    elements = elements.Where(e => centerCode.Contains(e.CENTER_CODE)).ToList();
                }
                var sumElements = new T_MD_KHOAN_MUC_SAN_LUONG
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
                    Values = new decimal[14]
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
                detailOtherKhoanMucSanLuongs = null;
                // disabled drill down
                isDrillDownApply = false;
                return SummarySumUpCenter(out detailOtherCostData, model.YEAR, model.ORG_CODE, null, isHasValue, templateId: null);
            }
            else
            {
                // xem dữ liệu được tổng hợp cho đơn vị
                detailOtherKhoanMucSanLuongs = null;
                return SummaryCenterVersion(out detailOtherCostData, model.ORG_CODE, model.YEAR, model.VERSION, model.IS_DRILL_DOWN);
            }
        }

        public override IList<T_MD_KHOAN_MUC_SAN_LUONG> GetDetailPreviewSumUp(string centerCode, string elementCode, int year)
        {
            var plVersionRepo = UnitOfWork.Repository<KeHoachSanLuongRepo>();
            var plDataRepo = UnitOfWork.Repository<KeHoachSanLuongDataRepo>();
            var plDataHistoryRepo = UnitOfWork.Repository<KeHoachSanLuongDataHistoryRepo>();

            var plDataOtherKhoanMucSanLuongs = UnitOfWork.Repository<KeHoachSanLuongDataRepo>().GetCFDataByCenterCode(null, new List<string> { centerCode }, year, null, null);

            return plDataOtherKhoanMucSanLuongs.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == elementCode)
                .Select(x => (T_MD_KHOAN_MUC_SAN_LUONG)x)
                .OrderBy(x => x.C_ORDER).ToList();
        }

        private IList<T_MD_KHOAN_MUC_SAN_LUONG> GetDataCostPreview(out IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_SAN_LUONG> detailOtherKhoanMucSanLuongs, string templateId, List<string> lstOrgs, int year, int? version, bool? isHasValue)
        {
            detailOtherKhoanMucSanLuongs = new List<T_MD_TEMPLATE_DETAIL_KE_HOACH_SAN_LUONG>();
            var lstElements = new List<T_MD_KHOAN_MUC_SAN_LUONG>();
            foreach (var orgCode in lstOrgs)
            {
                var elements = GetDataCostPreview(out IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_SAN_LUONG> detail, templateId, orgCode, year, version, isHasValue);
                detailOtherKhoanMucSanLuongs = SummaryElement(detailOtherKhoanMucSanLuongs, detail);
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
        private IList<T_MD_KHOAN_MUC_SAN_LUONG> SummaryUpElement(IList<T_MD_KHOAN_MUC_SAN_LUONG> lst1, IList<T_MD_KHOAN_MUC_SAN_LUONG> lst2)
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

        public override T_BP_KE_HOACH_SAN_LUONG_VERSION GetHeader(string templateCode, string centerCode, int year, int? version)
        {
            templateCode = templateCode ?? string.Empty;
            if (version == null)
            {
                return UnitOfWork.Repository<KeHoachSanLuongVersionRepo>()
                .GetManyByExpression(x =>
                x.TIME_YEAR == year &&
                x.ORG_CODE == centerCode &&
                x.TEMPLATE_CODE == templateCode).OrderByDescending(x => x.VERSION).FirstOrDefault();
            }
            else
            {
                if (!string.IsNullOrEmpty(templateCode))
                {
                    return UnitOfWork.Repository<KeHoachSanLuongVersionRepo>()
                    .GetFirstByExpression(x =>
                    x.TIME_YEAR == year &&
                    x.VERSION == version &&
                    x.TEMPLATE_CODE == templateCode);
                }
                else
                {
                    return UnitOfWork.Repository<KeHoachSanLuongVersionRepo>()
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
            ObjListVersion = UnitOfWork.Repository<KeHoachSanLuongVersionRepo>()
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
            var query = this.UnitOfWork.GetSession().QueryOver<T_BP_KE_HOACH_SAN_LUONG_HISTORY>();
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
                this.ObjListHistory = UnitOfWork.Repository<KeHoachSanLuongHistoryRepo>().GetManyByExpression(
                    x => x.ORG_CODE == orgCode &&
                    x.TEMPLATE_CODE == templateId &&
                    x.TIME_YEAR == year.Value
                ).ToList();
            }
            else
            {
                this.ObjListHistory = UnitOfWork.Repository<KeHoachSanLuongHistoryRepo>().GetManyByExpression(
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


            var lstDetailTemplate = this.UnitOfWork.Repository<TemplateDetailKeHoachSanLuongRepo>()
                .GetManyWithFetch(x => x.TIME_YEAR == this.ObjDetail.TIME_YEAR && x.TEMPLATE_CODE == this.ObjDetail.TEMPLATE_CODE,
                x => x.Center);

            var lstElement = this.UnitOfWork.Repository<KhoanMucSanLuongRepo>().GetManyByExpression(x => x.TIME_YEAR == this.ObjDetail.TIME_YEAR);

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
                    if (lstDetailTemplate.FirstOrDefault(x => x.Center.SAN_BAY_CODE == projectCode && x.Center.HANG_HANG_KHONG_CODE == companyCode) == null)
                    {
                        this.State = false;
                        this.ErrorMessage = $"Mẫu khai báo [{template.CODE}] không chứa mã [{projectCode}] tại dòng [{i + 1}] !";
                        return;
                    }
                }
                else
                {
                    if (lstDetailTemplate.FirstOrDefault(x => x.Center.SanBay.OTHER_PM_CODE == projectCode && x.Center.HangHangKhong.OTHER_PM_CODE == companyCode) == null)
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
                        && x.Center.SAN_BAY_CODE == projectCode
                        && x.Center.HANG_HANG_KHONG_CODE == companyCode) == 0)
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
                    for (int i = 0; i < ListColumnName.Count; i++)
                    {
                        if (dataTable.Rows[this.StartRowData - 1][i].ToString().ToUpper() != this.ListColumnName[i])
                        {
                            this.State = false;
                            this.ErrorMessage = "File excel không đúng theo mẫu thiết kế!";
                            return;
                        }
                    }
                    // kiểm tra xem có 2 hàng dữ liệu của cùng 1 khoản mục hay không
                    var dictionary = lstDetailTemplate.ToDictionary(x => x.PKID, x => true);
                    for (int i = StartRowData; i < dataTable.Rows.Count; i++)
                    {
                        var projectCode = dataTable.Rows[i][0].ToString().Trim();
                        var companyCode = dataTable.Rows[i][2].ToString().Trim();
                        var elementCodeInExcel = dataTable.Rows[i][4].ToString().Trim();
                        var foundItem = lstDetailTemplate
                            .FirstOrDefault(x => x.ELEMENT_CODE == elementCodeInExcel
                            && x.Center.SAN_BAY_CODE == projectCode
                            && x.Center.HANG_HANG_KHONG_CODE == companyCode);
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
                    this.ConvertData(dataTable, lstElement.ToList(), startColumn: 6, endColumn: ListColumnName.Count - 3, isDataBase);
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
        public override void ConvertData(DataTable dataTable, List<T_MD_KHOAN_MUC_SAN_LUONG> lstElement, int startColumn, int endColumn, bool isDataBase)
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
            var KeHoachSanLuongCurrent = CurrentRepository.Queryable().FirstOrDefault(x => x.ORG_CODE == orgCode
                && x.TIME_YEAR == ObjDetail.TIME_YEAR && x.TEMPLATE_CODE == ObjDetail.TEMPLATE_CODE);

            if (KeHoachSanLuongCurrent != null && !(KeHoachSanLuongCurrent.STATUS == Approve_Status.TuChoi || KeHoachSanLuongCurrent.STATUS == Approve_Status.ChuaTrinhDuyet))
            {
                this.State = false;
                this.ErrorMessage = "Mẫu khai báo này không ở trạng thái Từ chối hoặc Chưa trình duyệt!";
                return;
            }

            var versionNext = 1;
            if (KeHoachSanLuongCurrent != null)
            {
                versionNext = KeHoachSanLuongCurrent.VERSION + 1;
            }

            // Lấy dữ liệu của version hiện tại
            var dataCurrent = new List<T_BP_KE_HOACH_SAN_LUONG_DATA>();
            if (KeHoachSanLuongCurrent != null)
            {
                dataCurrent = UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.ORG_CODE == orgCode
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
                if (KeHoachSanLuongCurrent != null)
                {
                    // Cập nhật next version vào bảng chính
                    KeHoachSanLuongCurrent.VERSION = versionNext;
                    KeHoachSanLuongCurrent.IS_DELETED = false;
                    CurrentRepository.Update(KeHoachSanLuongCurrent);
                }
                else
                {
                    // Tạo mới bản ghi revenue pl
                    CurrentRepository.Create(new T_BP_KE_HOACH_SAN_LUONG()
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
                UnitOfWork.Repository<KeHoachSanLuongVersionRepo>().Create(new T_BP_KE_HOACH_SAN_LUONG_VERSION()
                {
                    PKID = Guid.NewGuid().ToString(),
                    ORG_CODE = orgCode,
                    TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                    VERSION = versionNext,
                    KICH_BAN = KeHoachSanLuongCurrent == null ? ObjDetail.KICH_BAN : KeHoachSanLuongCurrent.KICH_BAN,
                    PHIEN_BAN = KeHoachSanLuongCurrent == null ? ObjDetail.PHIEN_BAN : KeHoachSanLuongCurrent.PHIEN_BAN,
                    TIME_YEAR = ObjDetail.TIME_YEAR,
                    FILE_ID = fileStream.PKID,
                    CREATE_BY = currentUser
                });

                // Tạo mới bản ghi log trạng thái
                UnitOfWork.Repository<KeHoachSanLuongHistoryRepo>().Create(new T_BP_KE_HOACH_SAN_LUONG_HISTORY()
                {
                    PKID = Guid.NewGuid().ToString(),
                    ORG_CODE = orgCode,
                    TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                    KICH_BAN = KeHoachSanLuongCurrent == null ? ObjDetail.KICH_BAN : KeHoachSanLuongCurrent.KICH_BAN,
                    PHIEN_BAN = KeHoachSanLuongCurrent == null ? ObjDetail.PHIEN_BAN : KeHoachSanLuongCurrent.PHIEN_BAN,
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
                    var revenueDataHis = (T_BP_KE_HOACH_SAN_LUONG_DATA_HISTORY)item;
                    UnitOfWork.Repository<KeHoachSanLuongDataHistoryRepo>().Create(revenueDataHis);
                    UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Delete(item);
                }

                var allSanLuongProfitCenters = UnitOfWork.Repository<SanLuongProfitCenterRepo>().GetAll();
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
                        centerCode = allSanLuongProfitCenters.FirstOrDefault(
                                                x => x.SAN_BAY_CODE == tableData.Rows[i][0].ToString().Trim() &&
                                                x.HANG_HANG_KHONG_CODE == tableData.Rows[i][2].ToString().Trim())?.CODE;
                    }
                    else
                    {
                        centerCode = allSanLuongProfitCenters.FirstOrDefault(
                                                x => x.SanBay.OTHER_PM_CODE == tableData.Rows[i][5].ToString().Trim() &&
                                                x.HangHangKhong.OTHER_PM_CODE == tableData.Rows[i][7].ToString().Trim())?.CODE;
                    }

                    if (centerCode == null)
                    {
                        throw new Exception($"Định dạng file không đúng ở dòng thứ {i + 1}");
                    }

                    var costData = new T_BP_KE_HOACH_SAN_LUONG_DATA();

                    if (ObjDetail.TYPE_UPLOAD == "01")
                    {
                        costData = new T_BP_KE_HOACH_SAN_LUONG_DATA()
                        {
                            PKID = Guid.NewGuid().ToString(),
                            ORG_CODE = orgCode,
                            SAN_LUONG_PROFIT_CENTER_CODE = centerCode,
                            TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                            TIME_YEAR = ObjDetail.TIME_YEAR,
                            STATUS = Approve_Status.ChuaTrinhDuyet,
                            VERSION = versionNext,
                            KHOAN_MUC_SAN_LUONG_CODE = tableData.Rows[i][4].ToString().Trim(),
                            VALUE_JAN = tableData.Rows[i][6] as decimal? == null ? 0 : tableData.Rows[i][6] as decimal?,
                            VALUE_FEB = tableData.Rows[i][7] as decimal? == null ? 0 : tableData.Rows[i][7] as decimal?,
                            VALUE_MAR = tableData.Rows[i][8] as decimal? == null ? 0 : tableData.Rows[i][8] as decimal?,
                            VALUE_APR = tableData.Rows[i][9] as decimal? == null ? 0 : tableData.Rows[i][9] as decimal?,
                            VALUE_MAY = tableData.Rows[i][10] as decimal? == null ? 0 : tableData.Rows[i][10] as decimal?,
                            VALUE_JUN = tableData.Rows[i][11] as decimal? == null ? 0 : tableData.Rows[i][11] as decimal?,
                            VALUE_JUL = tableData.Rows[i][12] as decimal? == null ? 0 : tableData.Rows[i][12] as decimal?,
                            VALUE_AUG = tableData.Rows[i][13] as decimal? == null ? 0 : tableData.Rows[i][13] as decimal?,
                            VALUE_SEP = tableData.Rows[i][14] as decimal? == null ? 0 : tableData.Rows[i][14] as decimal?,
                            VALUE_OCT = tableData.Rows[i][15] as decimal? == null ? 0 : tableData.Rows[i][15] as decimal?,
                            VALUE_NOV = tableData.Rows[i][16] as decimal? == null ? 0 : tableData.Rows[i][16] as decimal?,
                            VALUE_DEC = tableData.Rows[i][17] as decimal? == null ? 0 : tableData.Rows[i][17] as decimal?,
                            DESCRIPTION = tableData.Rows[i][20].ToString(),
                            CREATE_BY = currentUser
                        };
                        costData.VALUE_SUM_YEAR = costData.VALUE_JAN + costData.VALUE_FEB + costData.VALUE_MAR + costData.VALUE_APR + costData.VALUE_MAY + costData.VALUE_JUN + costData.VALUE_JUL + costData.VALUE_AUG + costData.VALUE_SEP + costData.VALUE_OCT + costData.VALUE_NOV + costData.VALUE_DEC;

                        costData.VALUE_SUM_YEAR_PREVENTIVE = costData.VALUE_SUM_YEAR * percentagePreventive;
                    }
                    else
                    {
                        costData = new T_BP_KE_HOACH_SAN_LUONG_DATA()
                        {
                            PKID = Guid.NewGuid().ToString(),
                            ORG_CODE = orgCode,
                            SAN_LUONG_PROFIT_CENTER_CODE = centerCode,
                            TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                            TIME_YEAR = ObjDetail.TIME_YEAR,
                            STATUS = Approve_Status.ChuaTrinhDuyet,
                            VERSION = versionNext,
                            KHOAN_MUC_SAN_LUONG_CODE = (tableData.Rows[i][9].ToString().Trim() == "0") ? "10010" : "10020",
                            VALUE_JAN = tableData.Rows[i][11] as decimal? == null ? 0 : tableData.Rows[i][11] as decimal?,
                            VALUE_FEB = tableData.Rows[i][12] as decimal? == null ? 0 : tableData.Rows[i][12] as decimal?,
                            VALUE_MAR = tableData.Rows[i][13] as decimal? == null ? 0 : tableData.Rows[i][13] as decimal?,
                            VALUE_APR = tableData.Rows[i][14] as decimal? == null ? 0 : tableData.Rows[i][14] as decimal?,
                            VALUE_MAY = tableData.Rows[i][15] as decimal? == null ? 0 : tableData.Rows[i][15] as decimal?,
                            VALUE_JUN = tableData.Rows[i][16] as decimal? == null ? 0 : tableData.Rows[i][16] as decimal?,
                            VALUE_JUL = tableData.Rows[i][17] as decimal? == null ? 0 : tableData.Rows[i][17] as decimal?,
                            VALUE_AUG = tableData.Rows[i][18] as decimal? == null ? 0 : tableData.Rows[i][18] as decimal?,
                            VALUE_SEP = tableData.Rows[i][19] as decimal? == null ? 0 : tableData.Rows[i][19] as decimal?,
                            VALUE_OCT = tableData.Rows[i][20] as decimal? == null ? 0 : tableData.Rows[i][20] as decimal?,
                            VALUE_NOV = tableData.Rows[i][21] as decimal? == null ? 0 : tableData.Rows[i][21] as decimal?,
                            VALUE_DEC = tableData.Rows[i][22] as decimal? == null ? 0 : tableData.Rows[i][22] as decimal?,
                            DESCRIPTION = "",
                            CREATE_BY = currentUser
                        };
                        costData.VALUE_SUM_YEAR = costData.VALUE_JAN + costData.VALUE_FEB + costData.VALUE_MAR + costData.VALUE_APR + costData.VALUE_MAY + costData.VALUE_JUN + costData.VALUE_JUL + costData.VALUE_AUG + costData.VALUE_SEP + costData.VALUE_OCT + costData.VALUE_NOV + costData.VALUE_DEC;

                        costData.VALUE_SUM_YEAR_PREVENTIVE = costData.VALUE_SUM_YEAR * percentagePreventive;
                    }
                    
                    UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Create(costData);
                }
                UnitOfWork.Commit();
                NotifyUtilities.CreateNotify(
                    new NotifyPara()
                    {
                        Activity = Activity.AC_NHAP_DU_LIEU,
                        OrgCode = orgCode,
                        TemplateCode = ObjDetail.TEMPLATE_CODE,
                        TimeYear = ObjDetail.TIME_YEAR,
                        ModulType = ModulType.KeHoachSanLuong,
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
            var KeHoachSanLuongDataRepo = UnitOfWork.Repository<KeHoachSanLuongDataRepo>();
            var KeHoachSanLuongDataBaseRepo = UnitOfWork.Repository<KeHoachSanLuongDataBaseRepo>();
            var currentUser = ProfileUtilities.User?.USER_NAME;

            // Lưu file vào database
            var fileStream = new FILE_STREAM()
            {
                PKID = Guid.NewGuid().ToString(),
                FILESTREAM = request.Files[0]
            };
            FileStreamService.InsertFile(fileStream);

            // Xác định version dữ liệu
            var KeHoachSanLuongCurrent = GetFirstWithFetch(x => x.ORG_CODE == orgCode
                && x.TIME_YEAR == ObjDetail.TIME_YEAR && x.TEMPLATE_CODE == ObjDetail.TEMPLATE_CODE);

            if (KeHoachSanLuongCurrent != null && !(KeHoachSanLuongCurrent.STATUS == Approve_Status.TuChoi || KeHoachSanLuongCurrent.STATUS == Approve_Status.ChuaTrinhDuyet))
            {
                this.State = false;
                this.ErrorMessage = "Mẫu khai báo này không ở trạng thái Từ chối hoặc Chưa trình duyệt!";
                return;
            }

            var versionNext = 1;
            if (KeHoachSanLuongCurrent != null)
            {
                versionNext = KeHoachSanLuongCurrent.VERSION + 1;
            }

            // Lấy dữ liệu của version hiện tại
            var dataCurrent = new List<T_BP_KE_HOACH_SAN_LUONG_DATA>();
            var dataCurrentBase = new List<T_BP_KE_HOACH_SAN_LUONG_DATA_BASE>();
            if (KeHoachSanLuongCurrent != null)
            {
                dataCurrent = KeHoachSanLuongDataRepo.GetManyWithFetch(x => x.ORG_CODE == orgCode
                    && x.TIME_YEAR == ObjDetail.TIME_YEAR && x.TEMPLATE_CODE == ObjDetail.TEMPLATE_CODE).ToList();

                dataCurrentBase = KeHoachSanLuongDataBaseRepo.GetManyWithFetch(x => x.ORG_CODE == orgCode
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
                if (KeHoachSanLuongCurrent != null)
                {
                    // Cập nhật next version vào bảng chính
                    KeHoachSanLuongCurrent.VERSION = versionNext;
                    KeHoachSanLuongCurrent.IS_DELETED = false;
                    CurrentRepository.Update(KeHoachSanLuongCurrent);
                }
                else
                {
                    // Tạo mới bản ghi cost pl
                    CurrentRepository.Create(new T_BP_KE_HOACH_SAN_LUONG()
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
                UnitOfWork.Repository<KeHoachSanLuongVersionRepo>().Create(new T_BP_KE_HOACH_SAN_LUONG_VERSION()
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
                UnitOfWork.Repository<KeHoachSanLuongHistoryRepo>().Create(new T_BP_KE_HOACH_SAN_LUONG_HISTORY()
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
                    var costDataHis = (T_BP_KE_HOACH_SAN_LUONG_DATA_HISTORY)item;
                    UnitOfWork.Repository<KeHoachSanLuongDataHistoryRepo>().Create(costDataHis);
                    KeHoachSanLuongDataRepo.Delete(item);
                }

                // Insert dữ liệu vào bảng data
                var lstRowValues = new List<DataRow>();
                for (int i = StartRowData; i < actualRows; i++)
                {
                    lstRowValues.Add(tableData.Rows[i]);
                }

                var allSanLuongProfitCenters = UnitOfWork.Repository<SanLuongProfitCenterRepo>().GetAll();
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
                    var centerCode = allSanLuongProfitCenters.FirstOrDefault(
                            x => x.SAN_BAY_CODE == key.ProjectCode &&
                            x.HANG_HANG_KHONG_CODE == key.CompanyCode)?.CODE;
                    if (centerCode == null)
                    {
                        throw new Exception($"Định dạng file không đúng");
                    }
                    var costData = new T_BP_KE_HOACH_SAN_LUONG_DATA()
                    {
                        PKID = Guid.NewGuid().ToString(),
                        ORG_CODE = orgCode,
                        SAN_LUONG_PROFIT_CENTER_CODE = centerCode,
                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                        TIME_YEAR = ObjDetail.TIME_YEAR,
                        STATUS = Approve_Status.ChuaTrinhDuyet,
                        VERSION = versionNext,
                        KHOAN_MUC_SAN_LUONG_CODE = key.ElementCode,
                        VALUE_JAN = lookUp[key].Sum(x => UtilsCore.StringToDecimal(x[11].ToString())),
                        VALUE_FEB = lookUp[key].Sum(x => UtilsCore.StringToDecimal(x[15].ToString())),
                        VALUE_MAR = lookUp[key].Sum(x => UtilsCore.StringToDecimal(x[19].ToString())),
                        VALUE_APR = lookUp[key].Sum(x => UtilsCore.StringToDecimal(x[23].ToString())),
                        VALUE_MAY = lookUp[key].Sum(x => UtilsCore.StringToDecimal(x[27].ToString())),
                        VALUE_JUN = lookUp[key].Sum(x => UtilsCore.StringToDecimal(x[31].ToString())),
                        VALUE_JUL = lookUp[key].Sum(x => UtilsCore.StringToDecimal(x[35].ToString())),
                        VALUE_AUG = lookUp[key].Sum(x => UtilsCore.StringToDecimal(x[39].ToString())),
                        VALUE_SEP = lookUp[key].Sum(x => UtilsCore.StringToDecimal(x[43].ToString())),
                        VALUE_OCT = lookUp[key].Sum(x => UtilsCore.StringToDecimal(x[47].ToString())),
                        VALUE_NOV = lookUp[key].Sum(x => UtilsCore.StringToDecimal(x[51].ToString())),
                        VALUE_DEC = lookUp[key].Sum(x => UtilsCore.StringToDecimal(x[55].ToString())),
                        CREATE_BY = currentUser
                    };
                    costData.VALUE_SUM_YEAR = costData.VALUE_JAN + costData.VALUE_FEB + costData.VALUE_MAR + costData.VALUE_APR + costData.VALUE_MAY + costData.VALUE_JUN + costData.VALUE_JUL + costData.VALUE_AUG + costData.VALUE_SEP + costData.VALUE_OCT + costData.VALUE_NOV + costData.VALUE_DEC;

                    costData.VALUE_SUM_YEAR_PREVENTIVE = costData.VALUE_SUM_YEAR * percentagePreventive;
                    KeHoachSanLuongDataRepo.Create(costData);
                }

                // Insert data vào base data history
                var lstBaseDataHistory = (from x in dataCurrentBase
                                          select (T_BP_KE_HOACH_SAN_LUONG_DATA_BASE_HISTORY)x).ToList();
                UnitOfWork.Repository<KeHoachSanLuongDataBaseHistoryRepo>().Create(lstObj: lstBaseDataHistory);
                KeHoachSanLuongDataBaseRepo.Delete(dataCurrentBase);

                // Insert dữ liệu vào bảng data
                for (int i = this.StartRowData; i < actualRows; i++)
                {
                    int j = 8;
                    var centerCode = allSanLuongProfitCenters.FirstOrDefault(
                            x => x.SAN_BAY_CODE == tableData.Rows[i][0].ToString().Trim() &&
                            x.HANG_HANG_KHONG_CODE == tableData.Rows[i][2].ToString().Trim())?.CODE;
                    if (centerCode == null)
                    {
                        throw new Exception($"Định dạng file không đúng");
                    }
                    var costData = new T_BP_KE_HOACH_SAN_LUONG_DATA_BASE()
                    {
                        PKID = Guid.NewGuid().ToString(),
                        ORG_CODE = orgCode,
                        SAN_BAY_CODE = tableData.Rows[i][0].ToString().Trim(),
                        HANG_HANG_KHONG_CODE = tableData.Rows[i][2].ToString().Trim(),
                        SAN_LUONG_PROFIT_CENTER_CODE = centerCode,
                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                        TIME_YEAR = ObjDetail.TIME_YEAR,
                        VERSION = versionNext,
                        KHOAN_MUC_SAN_LUONG_CODE = tableData.Rows[i][4].ToString().Trim(),
                        MATERIAL = tableData.Rows[i][6].ToString().Trim(),
                        UNIT = tableData.Rows[i][7].ToString().Trim(),

                        QUANTITY_M1 = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),
                        TIME_M1 = tableData.Rows[i][j++].ToString().Trim(),
                        PRICE_M1 = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),
                        AMOUNT_M1 = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),

                        QUANTITY_M2 = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),
                        TIME_M2 = tableData.Rows[i][j++].ToString().Trim(),
                        PRICE_M2 = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),
                        AMOUNT_M2 = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),

                        QUANTITY_M3 = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),
                        TIME_M3 = tableData.Rows[i][j++].ToString().Trim(),
                        PRICE_M3 = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),
                        AMOUNT_M3 = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),

                        QUANTITY_M4 = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),
                        TIME_M4 = tableData.Rows[i][j++].ToString().Trim(),
                        PRICE_M4 = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),
                        AMOUNT_M4 = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),

                        QUANTITY_M5 = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),
                        TIME_M5 = tableData.Rows[i][j++].ToString().Trim(),
                        PRICE_M5 = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),
                        AMOUNT_M5 = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),

                        QUANTITY_M6 = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),
                        TIME_M6 = tableData.Rows[i][j++].ToString().Trim(),
                        PRICE_M6 = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),
                        AMOUNT_M6 = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),

                        QUANTITY_M7 = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),
                        TIME_M7 = tableData.Rows[i][j++].ToString().Trim(),
                        PRICE_M7 = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),
                        AMOUNT_M7 = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),

                        QUANTITY_M8 = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),
                        TIME_M8 = tableData.Rows[i][j++].ToString().Trim(),
                        PRICE_M8 = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),
                        AMOUNT_M8 = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),

                        QUANTITY_M9 = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),
                        TIME_M9 = tableData.Rows[i][j++].ToString().Trim(),
                        PRICE_M9 = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),
                        AMOUNT_M9 = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),

                        QUANTITY_M10 = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),
                        TIME_M10 = tableData.Rows[i][j++].ToString().Trim(),
                        PRICE_M10 = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),
                        AMOUNT_M10 = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),

                        QUANTITY_M11 = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),
                        TIME_M11 = tableData.Rows[i][j++].ToString().Trim(),
                        PRICE_M11 = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),
                        AMOUNT_M11 = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),

                        QUANTITY_M12 = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),
                        TIME_M12 = tableData.Rows[i][j++].ToString().Trim(),
                        PRICE_M12 = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),
                        AMOUNT_M12 = UtilsCore.StringToDecimal(tableData.Rows[i][j++].ToString().Trim()),

                        DESCRIPTION = tableData.Rows[i][ListColumnNameDataBase.Count - 1].ToString().Trim(),

                        CREATE_BY = currentUser
                    };
                    costData.AMOUNT_YEAR = costData.AMOUNT_M1 + costData.AMOUNT_M2 + costData.AMOUNT_M3 + costData.AMOUNT_M4 + costData.AMOUNT_M5 + costData.AMOUNT_M6 + costData.AMOUNT_M7 + costData.AMOUNT_M8 + costData.AMOUNT_M9 + costData.AMOUNT_M10 + costData.AMOUNT_M11 + costData.AMOUNT_M12;
                    costData.AMOUNT_YEAR_PREVENTIVE = costData.AMOUNT_YEAR * percentagePreventive.Value;

                    KeHoachSanLuongDataBaseRepo.Create(costData);
                }
                UnitOfWork.Commit();

                NotifyUtilities.CreateNotify(
                    new NotifyPara()
                    {
                        Activity = Activity.AC_NHAP_DU_LIEU,
                        OrgCode = orgCode,
                        TemplateCode = ObjDetail.TEMPLATE_CODE,
                        TimeYear = ObjDetail.TIME_YEAR,
                        ModulType = ModulType.KeHoachSanLuong,
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

        public override IList<T_BP_KE_HOACH_SAN_LUONG_VERSION> GetVersions(string orgCode, string templateId, int year)
        {
            templateId = templateId ?? string.Empty;
            var lstVersions = GetVersionsNumber(orgCode, templateId, year,"", "");
            return UnitOfWork.Repository<KeHoachSanLuongVersionRepo>()
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
            return UnitOfWork.Repository<KeHoachSanLuongVersionRepo>()
                .GetManyByExpression(x => x.ORG_CODE == orgCode && x.TEMPLATE_CODE != string.Empty)
                .Select(x => x.TEMPLATE_CODE)
                .Distinct()
                .ToList();
        }

        private IList<string> GetTemplateData(List<string> lstOrgCodes, int? year = null)
        {
            return UnitOfWork.Repository<KeHoachSanLuongDataRepo>()
                .GetManyByExpression(x => lstOrgCodes.Contains(x.ORG_CODE) && x.TEMPLATE_CODE != string.Empty && (!year.HasValue || x.TIME_YEAR == year.Value))
                .Select(x => x.TEMPLATE_CODE)
                .Distinct()
                .ToList();
        }

        private IList<string> GetTemplateDataHistory(List<string> lstOrgCodes, int? year = null)
        {
            return UnitOfWork.Repository<KeHoachSanLuongDataHistoryRepo>()
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
                return UnitOfWork.Repository<KeHoachSanLuongVersionRepo>()
                    .GetManyByExpression(x => lstOrgCodes.Contains(x.ORG_CODE))
                    .Select(x => x.TIME_YEAR)
                    .Distinct()
                    .ToList();
            }
            else
            {
                return UnitOfWork.Repository<KeHoachSanLuongVersionRepo>()
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
                return UnitOfWork.Repository<KeHoachSanLuongDataHistoryRepo>()
                    .GetManyByExpression(x => lstOrgCodes.Contains(x.ORG_CODE))
                    .Select(x => x.TIME_YEAR)
                    .Distinct()
                    .ToList();
            }
            else
            {
                return UnitOfWork.Repository<KeHoachSanLuongDataHistoryRepo>()
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
                return UnitOfWork.Repository<KeHoachSanLuongDataRepo>()
                    .GetManyByExpression(x => lstOrgCodes.Contains(x.ORG_CODE))
                    .Select(x => x.TIME_YEAR)
                    .Distinct()
                    .ToList();
            }
            else
            {
                return UnitOfWork.Repository<KeHoachSanLuongDataRepo>()
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
                
                return UnitOfWork.Repository<KeHoachSanLuongVersionRepo>()
                    .GetManyWithFetch(x => x.TEMPLATE_CODE == templateId && x.TIME_YEAR == year && x.KICH_BAN == kichBan && x.PHIEN_BAN == phienBan)
                    .Select(x => x.VERSION)
                    .OrderByDescending(x => x)
                    .ToList();
            }
            else
            {
                return UnitOfWork.Repository<KeHoachSanLuongVersionRepo>()
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

        public  IList<int> GetVersionsNumberSL(string orgCode, string templateId, int year, string kichBan, string phienBan, string hanghangkhong, string sanbay, string khuvuc, string nhomsanbay)
        {
            if (!string.IsNullOrEmpty(templateId))
            {   
                var sanbayCode = UnitOfWork.Repository<SanBayRepo>().Queryable().Where(x => x.AREA_CODE == khuvuc || x.NHOM_SAN_BAY_CODE == nhomsanbay).Select(x=>x.CODE).ToList();
                var centerCode = UnitOfWork.Repository<SanLuongProfitCenterRepo>().Queryable().Where(x => x.HANG_HANG_KHONG_CODE == hanghangkhong || x.SAN_BAY_CODE == sanbay || sanbayCode.Contains(x.SAN_BAY_CODE)).Select(x => x.CODE).ToList();
                if(string.IsNullOrEmpty(hanghangkhong) && string.IsNullOrEmpty(sanbay) && string.IsNullOrEmpty(khuvuc) && string.IsNullOrEmpty(nhomsanbay))
                {
                    return UnitOfWork.Repository<KeHoachSanLuongVersionRepo>()
                    .GetManyWithFetch(x => x.TEMPLATE_CODE == templateId && x.TIME_YEAR == year && x.KICH_BAN == kichBan && x.PHIEN_BAN == phienBan)
                    .Select(x => x.VERSION)
                    .OrderByDescending(x => x)
                    .ToList();
                }
                else
                {
                    var templateCode = string.Empty;
                    if(!string.IsNullOrEmpty(hanghangkhong) && !string.IsNullOrEmpty(sanbay))
                    {
                        centerCode = UnitOfWork.Repository<SanLuongProfitCenterRepo>().Queryable().Where(x => x.HANG_HANG_KHONG_CODE == hanghangkhong && x.SAN_BAY_CODE == sanbay).Select(x => x.CODE).ToList();
                    }
                    templateCode = UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => centerCode.Contains(x.SAN_LUONG_PROFIT_CENTER_CODE) && x.TEMPLATE_CODE == templateId).Select(x => x.TEMPLATE_CODE).FirstOrDefault();
                    return UnitOfWork.Repository<KeHoachSanLuongVersionRepo>()
                    .GetManyWithFetch(x => x.TEMPLATE_CODE == templateCode && x.TIME_YEAR == year && x.KICH_BAN == kichBan && x.PHIEN_BAN == phienBan)
                    .Select(x => x.VERSION)
                    .OrderByDescending(x => x)
                    .ToList();
                }
            }
            else
            {
                var centerCode = UnitOfWork.Repository<SanLuongProfitCenterRepo>().Queryable().Where(x => x.HANG_HANG_KHONG_CODE == hanghangkhong).Select(x => x.CODE).FirstOrDefault();
                var templateCode = UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.SAN_LUONG_PROFIT_CENTER_CODE == centerCode).Select(x => x.TEMPLATE_CODE).FirstOrDefault();
                if (string.IsNullOrEmpty(hanghangkhong))
                {
                    return UnitOfWork.Repository<KeHoachSanLuongVersionRepo>()
                    .GetManyWithFetch(x => x.TEMPLATE_CODE == templateId && x.TIME_YEAR == year && x.ORG_CODE == orgCode && x.KICH_BAN == kichBan && x.PHIEN_BAN == phienBan)
                    .Select(x => x.VERSION)
                    .OrderByDescending(x => x)
                    .ToList();
                }
                else
                {
                    return UnitOfWork.Repository<KeHoachSanLuongVersionRepo>()
                    .GetManyWithFetch(x => x.TEMPLATE_CODE == templateCode && x.TIME_YEAR == year && x.ORG_CODE == orgCode && x.KICH_BAN == kichBan && x.PHIEN_BAN == phienBan)
                    .Select(x => x.VERSION)
                    .OrderByDescending(x => x)
                    .ToList();
                }
            }

        }

        #endregion

        public override IList<int> GetTemplateVersion(string templateId, string centerCode, int year)
        {
            if (string.IsNullOrEmpty(templateId))
            {
                return UnitOfWork.Repository<KeHoachSanLuongSumUpDetailRepo>()
                    .GetManyByExpression(x => x.ORG_CODE == centerCode && x.TIME_YEAR == year)
                    .Select(x => x.DATA_VERSION).Distinct().OrderByDescending(x => x).ToList();
            }
            else
            {
                return UnitOfWork.Repository<KeHoachSanLuongVersionRepo>()
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
            var dataOtherCost = PreparePureList(out IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_SAN_LUONG> detailOtherKhoanMucSanLuongs, templateId, year, ignoreAuth: true);

            if (dataOtherCost.Count == 0 || detailOtherKhoanMucSanLuongs.Count == 0)
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
                templateWorkbook.SetSheetName(0, ModulType.GetTextSheetName(ModulType.KeHoachSanLuong));
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
                foreach (var detail in detailOtherKhoanMucSanLuongs.GroupBy(x => x.CENTER_CODE)
                    .Select(x => x.First())
                    .OrderBy(x => x.Center.SanBay.CODE).ThenBy(x=> x.Center.SanBay.CODE))
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

                        rowCur.Cells[0].SetCellValue(detail.Center.SAN_BAY_CODE);
                        rowCur.Cells[0].CellStyle = styleCellDetail;

                        rowCur.Cells[1].SetCellValue(detail.Center.SanBay.NAME);
                        rowCur.Cells[1].CellStyle = styleCellDetail;

                        rowCur.Cells[2].SetCellValue(detail.Center.HANG_HANG_KHONG_CODE);
                        rowCur.Cells[2].CellStyle = styleCellDetail;

                        rowCur.Cells[3].SetCellValue(detail.Center.HangHangKhong.NAME);
                        rowCur.Cells[3].CellStyle = styleCellDetail;

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
            var dataOtherCost = PreparePureList(out IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_SAN_LUONG> detailKhoanMucSanLuongs, templateId, year, ignoreAuth: true);

            if (dataOtherCost.Count == 0 || detailKhoanMucSanLuongs.Count == 0)
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
                templateWorkbook.SetSheetName(0, ModulType.GetTextSheetName(ModulType.KeHoachSanLuong));
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
                foreach (var detail in detailKhoanMucSanLuongs.GroupBy(x => x.CENTER_CODE)
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

                        rowCur.Cells[j].SetCellValue(detail.Center.HANG_HANG_KHONG_CODE);
                        rowCur.Cells[j].CellStyle = styleCellDetail;
                        j++;

                        rowCur.Cells[j].SetCellValue(detail.Center.HangHangKhong.NAME);
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
                    var revenuePLData = UnitOfWork.Repository<KeHoachSanLuongSumUpDetailRepo>()
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
                    var revenuePLData = UnitOfWork.Repository<KeHoachSanLuongSumUpDetailRepo>()
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
                var sumupDetails = UnitOfWork.Repository<KeHoachSanLuongSumUpDetailRepo>()
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
        /// <param name="detailOtherKhoanMucSanLuongs">out detail revenue elemts</param>
        /// <param name="year">which year of template</param>
        /// <param name="version">which version of template</param>
        /// <returns>Returns list revenue elemts with their parents and their value</returns>
        public IList<T_MD_KHOAN_MUC_SAN_LUONG> GetDataCostPreview(
            out IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_SAN_LUONG> detailOtherKhoanMucSanLuongs,
            string templateId,
            string centerCode = "",
            int? year = null,
            int? version = null,
            bool? isHasValue = null)
        {
            var pureLstItems = PreparePureList(out detailOtherKhoanMucSanLuongs, templateId, year.Value, centerCode);
            var sum = GetSumDescendants(detailOtherKhoanMucSanLuongs, pureLstItems, parent_id: string.Empty, templateId, year, version).Distinct().ToList();
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

        public IList<T_MD_KHOAN_MUC_SAN_LUONG> PreparePureList(IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_SAN_LUONG> detailOtherKhoanMucSanLuongs, int year)
        {
            // get all revenue elements
            var allOtherKhoanMucSanLuongs = UnitOfWork.Repository<KhoanMucSanLuongRepo>().GetManyByExpression(x => x.TIME_YEAR == year);
            // get revenue elements in details revenue elements
            var revenueElements = from d in detailOtherKhoanMucSanLuongs
                                  select d.Element;
            // lookup revenue elements by center code
            var lookupElementsCenter = detailOtherKhoanMucSanLuongs.ToLookup(x => x.CENTER_CODE);

            var pureLstItems = new List<T_MD_KHOAN_MUC_SAN_LUONG>();
            // loop through all center
            foreach (var ctCode in lookupElementsCenter.Select(l => l.Key))
            {
                // lookup revenue elements
                var lookupElements = lookupElementsCenter[ctCode].ToLookup(x => x.Element.PARENT_CODE);
                foreach (var code in lookupElements.Select(l => l.Key))
                {
                    var level = 0;
                    // temp list
                    var lst = new List<T_MD_KHOAN_MUC_SAN_LUONG>();
                    // add all leaf to temp list item
                    lst.AddRange(from item in lookupElements[code]
                                 select new T_MD_KHOAN_MUC_SAN_LUONG
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
                        var element = allOtherKhoanMucSanLuongs.FirstOrDefault(x => x.CODE == parentCode);
                        if (element != null)
                        {
                            parentCode = element.PARENT_CODE;
                            element.CENTER_CODE = ctCode;
                            element.LEVEL = level;
                            element.IS_GROUP = true;
                            lst.Add((T_MD_KHOAN_MUC_SAN_LUONG)element.CloneObject());     // must to clone to other object because it reference to other center
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
        /// <param name="detailOtherKhoanMucSanLuongs"></param>
        /// <param name="templateId"></param>
        /// <param name="centerCode"></param>
        /// <returns></returns>
        public IList<T_MD_KHOAN_MUC_SAN_LUONG> PreparePureList(out IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_SAN_LUONG> detailOtherKhoanMucSanLuongs,
            string templateId,
            int year,
            string centerCode = "",
            bool ignoreAuth = false)
        {
            templateId = templateId ?? string.Empty;
            var template = GetTemplate(templateId);
            var code = UnitOfWork.Repository<CostCenterRepo>().Queryable().Where(x => x.CODE == template.ORG_CODE).Select(x => x.PARENT_CODE).FirstOrDefault();
            var currentUserCenterCode = ProfileUtilities.User.ORGANIZE_CODE;
            if(ProfileUtilities.User.ORGANIZE_CODE == "1000")
            {
                currentUserCenterCode = code;
            }
            var childOrgOtherCosts = GetListOfChildrenCenter(currentUserCenterCode).Select(x => x.CODE);

            if (ignoreAuth || /*childOrgOtherCosts.Contains(template.ORG_CODE) ||*/ currentUserCenterCode.Equals(template.ORG_CODE) || template.ORG_CODE == centerCode)
            {
                detailOtherKhoanMucSanLuongs = UnitOfWork.Repository<TemplateDetailKeHoachSanLuongRepo>()
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
                    detailOtherKhoanMucSanLuongs = UnitOfWork.Repository<TemplateDetailKeHoachSanLuongRepo>()
                        .GetManyByExpression(x => x.TEMPLATE_CODE.Equals(templateId) && lstChildCenterCodes.Contains(x.CENTER_CODE) && x.TIME_YEAR == year);
                }
                else
                {
                    detailOtherKhoanMucSanLuongs = UnitOfWork.Repository<TemplateDetailKeHoachSanLuongRepo>()
                        .GetManyByExpression(x => x.TEMPLATE_CODE.Equals(templateId) && x.CENTER_CODE == centerCode && x.TIME_YEAR == year);
                }
            }
            return PreparePureList(detailOtherKhoanMucSanLuongs, year);
        }

        /// <summary>
        /// Xem theo template
        /// </summary>
        /// <param name="detailOtherKhoanMucSanLuongs"></param>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public IList<T_MD_KHOAN_MUC_SAN_LUONG> PreparePureListForTemplate(out IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_SAN_LUONG> detailOtherKhoanMucSanLuongs,
            string templateId,
            int year)
        {

            detailOtherKhoanMucSanLuongs = UnitOfWork.Repository<TemplateDetailKeHoachSanLuongRepo>()
                .GetManyByExpression(x => x.TEMPLATE_CODE.Equals(templateId) && x.TIME_YEAR == year);

            return PreparePureList(detailOtherKhoanMucSanLuongs, year);
        }



        /// <summary>
        /// Xem theo center code
        /// </summary>
        /// <param name="detailOtherKhoanMucSanLuongs"></param>
        /// <param name="centerCodes"></param>
        /// <returns></returns>
        public IList<T_MD_KHOAN_MUC_SAN_LUONG> PreparePureList(out IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_SAN_LUONG> detailOtherKhoanMucSanLuongs,
            IList<string> centerCodes,
            int year)
        {
            // Tìm mẫu nộp hộ
            var listTemplateCodes = this.UnitOfWork.Repository<TemplateDetailKeHoachSanLuongRepo>()
                .GetManyByExpression(x => centerCodes.Contains(x.CENTER_CODE) && x.TIME_YEAR == year)
                .Select(x => x.TEMPLATE_CODE).Distinct().ToList();
            var findKeHoachSanLuong = this.CurrentRepository.GetManyByExpression(
                    x => listTemplateCodes.Contains(x.TEMPLATE_CODE) && !centerCodes.Contains(x.ORG_CODE));
            var lst = new List<T_MD_TEMPLATE_DETAIL_KE_HOACH_SAN_LUONG>();
            detailOtherKhoanMucSanLuongs = new List<T_MD_TEMPLATE_DETAIL_KE_HOACH_SAN_LUONG>();
            foreach (var template in listTemplateCodes)
            {
                lst.AddRange(UnitOfWork.Repository<TemplateDetailKeHoachSanLuongRepo>()
                    .GetManyByExpression(x => x.TEMPLATE_CODE.Equals(template) && centerCodes.Contains(x.CENTER_CODE) && x.TIME_YEAR == year));
            }

            detailOtherKhoanMucSanLuongs = lst;

            return PreparePureList(detailOtherKhoanMucSanLuongs, year);
        }

        /// <summary>
        /// Sum up data revenue center by center code and year (Tổng hợp dữ liệu tại phòng ban)
        /// </summary>
        /// <param name="revenuePL">Output header revenue pl</param>
        /// <param name="centerCode">OtherCost center code want to sum up</param>
        /// <param name="year">Year want to sum up</param>
        public override void SumUpDataCenter(out T_BP_KE_HOACH_SAN_LUONG_VERSION revenuePL, string centerCode, int year, string kichBan, string phienBan, string hangHangKhong)
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

            if (string.IsNullOrEmpty(phienBan))
            {
                phienBan = ObjDetail.PHIEN_BAN;
            }

            var lstData = new List<T_BP_KE_HOACH_SAN_LUONG_DATA>();
            revenuePL = new T_BP_KE_HOACH_SAN_LUONG_VERSION();

            try
            {
                UnitOfWork.BeginTransaction();
                var sumUpDetailRepo = UnitOfWork.Repository<KeHoachSanLuongSumUpDetailRepo>();
                var revenuePLDataRepo = UnitOfWork.Repository<KeHoachSanLuongDataRepo>();

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
                var lookup = revenuePlDataApproved.ToLookup(x => x.KHOAN_MUC_SAN_LUONG_CODE);
                foreach (var code in lookup.Select(x => x.Key))
                {
                    // TODO: check if all value of months are equal 0
                    if (lookup[code].Count() == 1)
                    {
                        lstData.Add((T_BP_KE_HOACH_SAN_LUONG_DATA)lookup[code].First().CloneObject());
                    }
                    else
                    {
                        lstData.Add(new T_BP_KE_HOACH_SAN_LUONG_DATA
                        {
                            VALUE_APR = lookup[code].Sum(x => x.VALUE_APR),
                            VALUE_AUG = lookup[code].Sum(x => x.VALUE_AUG),
                            VALUE_DEC = lookup[code].Sum(x => x.VALUE_DEC),
                            VALUE_FEB = lookup[code].Sum(x => x.VALUE_FEB),
                            VALUE_JAN = lookup[code].Sum(x => x.VALUE_JAN),
                            VALUE_JUL = lookup[code].Sum(x => x.VALUE_JUL),
                            VALUE_JUN = lookup[code].Sum(x => x.VALUE_JUN),
                            VALUE_MAR = lookup[code].Sum(x => x.VALUE_MAR),
                            VALUE_MAY = lookup[code].Sum(x => x.VALUE_MAY),
                            VALUE_NOV = lookup[code].Sum(x => x.VALUE_NOV),
                            VALUE_OCT = lookup[code].Sum(x => x.VALUE_OCT),
                            VALUE_SEP = lookup[code].Sum(x => x.VALUE_SEP),
                            VALUE_SUM_YEAR = lookup[code].Sum(x => x.VALUE_SUM_YEAR),
                            VALUE_SUM_YEAR_PREVENTIVE = lookup[code].Sum(x => x.VALUE_SUM_YEAR_PREVENTIVE),
                            KHOAN_MUC_SAN_LUONG_CODE = lookup[code].First().KHOAN_MUC_SAN_LUONG_CODE
                        });
                    }
                }

                // get current version in revenue pl data
                var newestCF = UnitOfWork.Repository<KeHoachSanLuongRepo>()
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
                    CurrentRepository.Create(new T_BP_KE_HOACH_SAN_LUONG
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
                revenuePL = new T_BP_KE_HOACH_SAN_LUONG_VERSION
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
                UnitOfWork.Repository<KeHoachSanLuongVersionRepo>().Create(revenuePL);

                foreach (var item in lstData)
                {
                    item.ORG_CODE = centerCode;
                    item.SAN_LUONG_PROFIT_CENTER_CODE = string.Empty;
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
                _ = UnitOfWork.GetSession().CreateSQLQuery($"DELETE FROM T_BP_KE_HOACH_SAN_LUONG_DATA WHERE ORG_CODE = '{centerCode}' AND TIME_YEAR = {year}")
                    .ExecuteUpdate();

                // insert to pl data history
                UnitOfWork.Repository<KeHoachSanLuongDataHistoryRepo>().Create((from pl in lstOtherCostPlDataOldVersion
                                                                                select (T_BP_KE_HOACH_SAN_LUONG_DATA_HISTORY)pl).ToList());

                // insert to pl history
                UnitOfWork.Repository<KeHoachSanLuongHistoryRepo>().Create(new T_BP_KE_HOACH_SAN_LUONG_HISTORY
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
                var lstKeHoachSanLuong = revenuePlDataApproved.ToLookup(x => new { OrgCode = x.ORG_CODE, TemplateCode = x.TEMPLATE_CODE, TemplateVersion = x.VERSION });

                // create list sum up detail
                sumUpDetailRepo.Create((from c in lstKeHoachSanLuong.Select(x => x.Key)
                                        select new T_BP_KE_HOACH_SAN_LUONG_SUM_UP_DETAIL
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
                        ModulType = ModulType.KeHoachSanLuong,
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
        /// <param name="plDataOtherKhoanMucSanLuongs">List of data revenue element output</param>
        /// <param name="year">Year want to summary</param>
        /// <param name="centerCode">OtherCost center code want to summary</param>
        /// <param name="version">Version want to summary</param>
        /// <returns>Returns list of revenue element with their value</returns>
        public IList<T_MD_KHOAN_MUC_SAN_LUONG> SummarySumUpCenter(
            out IList<T_BP_KE_HOACH_SAN_LUONG_DATA> plDataOtherKhoanMucSanLuongs,
            int year,
            string centerCode,
            int? version,
            bool? isHasValue = null,
            string templateId = "")
        {
            // get newest revenue pl data by center code
            plDataOtherKhoanMucSanLuongs = UnitOfWork.Repository<KeHoachSanLuongDataRepo>()
                .GetCFDataByCenterCode(null, new List<string> { centerCode }, year, templateId, version);
            plDataOtherKhoanMucSanLuongs = plDataOtherKhoanMucSanLuongs.Where(x => x.STATUS == Approve_Status.DaPheDuyet).ToList();
            return SummaryCenter(plDataOtherKhoanMucSanLuongs, centerCode, year, isHasValue);
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
        private IEnumerable<T_MD_KHOAN_MUC_SAN_LUONG> GetSumDescendants(
            IEnumerable<T_MD_TEMPLATE_DETAIL_KE_HOACH_SAN_LUONG> details,
            IEnumerable<T_MD_KHOAN_MUC_SAN_LUONG> pureItems,
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
                Queue<T_MD_KHOAN_MUC_SAN_LUONG> st = new Queue<T_MD_KHOAN_MUC_SAN_LUONG>(lookup[parent_id]);

                while (st.Count > 0)
                {
                    // get first item in queue
                    var item = st.Dequeue();
                    // variable to check should return item or not
                    bool shouldReturn = true;
                    // lst to store children of item which have children
                    var lstHasChild = new List<T_MD_KHOAN_MUC_SAN_LUONG>();
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
                                    detail.PLData = revenuePlData.FirstOrDefault(x => x.KHOAN_MUC_SAN_LUONG_CODE == detail.ELEMENT_CODE && x.SAN_LUONG_PROFIT_CENTER_CODE == detail.CENTER_CODE);
                                }
                                var treeData = detail?.PLData;
                                if (treeData != null)
                                {
                                    item.Values[0] += treeData.VALUE_JAN ?? 0;
                                    item.Values[1] += treeData.VALUE_FEB ?? 0;
                                    item.Values[2] += treeData.VALUE_MAR ?? 0;
                                    item.Values[3] += treeData.VALUE_APR ?? 0;
                                    item.Values[4] += treeData.VALUE_MAY ?? 0;
                                    item.Values[5] += treeData.VALUE_JUN ?? 0;
                                    item.Values[6] += treeData.VALUE_JUL ?? 0;
                                    item.Values[7] += treeData.VALUE_AUG ?? 0;
                                    item.Values[8] += treeData.VALUE_SEP ?? 0;
                                    item.Values[9] += treeData.VALUE_OCT ?? 0;
                                    item.Values[10] += treeData.VALUE_NOV ?? 0;
                                    item.Values[11] += treeData.VALUE_DEC ?? 0;
                                    item.Values[12] += treeData.VALUE_SUM_YEAR ?? 0;
                                    item.Values[13] += treeData.VALUE_SUM_YEAR_PREVENTIVE ?? 0;
                                    item.HasAssignValue = true;

                                    i.Values[0] = treeData.VALUE_JAN ?? 0;
                                    i.Values[1] = treeData.VALUE_FEB ?? 0;
                                    i.Values[2] = treeData.VALUE_MAR ?? 0;
                                    i.Values[3] = treeData.VALUE_APR ?? 0;
                                    i.Values[4] = treeData.VALUE_MAY ?? 0;
                                    i.Values[5] = treeData.VALUE_JUN ?? 0;
                                    i.Values[6] = treeData.VALUE_JUL ?? 0;
                                    i.Values[7] = treeData.VALUE_AUG ?? 0;
                                    i.Values[8] = treeData.VALUE_SEP ?? 0;
                                    i.Values[9] = treeData.VALUE_OCT ?? 0;
                                    i.Values[10] = treeData.VALUE_NOV ?? 0;
                                    i.Values[11] = treeData.VALUE_DEC ?? 0;
                                    i.Values[12] = treeData.VALUE_SUM_YEAR ?? 0;
                                    i.Values[13] = treeData.VALUE_SUM_YEAR_PREVENTIVE ?? 0;
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
                            var data = revenuePlData.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == item.CODE && x.SAN_LUONG_PROFIT_CENTER_CODE == item.CENTER_CODE);
                            if (data != null)
                            {
                                item.Values = new decimal[14]
                                {
                                data.Sum(x => x.VALUE_JAN) ?? 0,
                                data.Sum(x => x.VALUE_FEB) ?? 0,
                                data.Sum(x => x.VALUE_MAR) ?? 0,
                                data.Sum(x => x.VALUE_APR) ?? 0,
                                data.Sum(x => x.VALUE_MAY) ?? 0,
                                data.Sum(x => x.VALUE_JUN) ?? 0,
                                data.Sum(x => x.VALUE_JUL) ?? 0,
                                data.Sum(x => x.VALUE_AUG) ?? 0,
                                data.Sum(x => x.VALUE_SEP) ?? 0,
                                data.Sum(x => x.VALUE_OCT) ?? 0,
                                data.Sum(x => x.VALUE_NOV) ?? 0,
                                data.Sum(x => x.VALUE_DEC) ?? 0,
                                data.Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                                data.Sum(x => x.VALUE_SUM_YEAR_PREVENTIVE) ?? 0,
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
        private IList<T_BP_KE_HOACH_SAN_LUONG_DATA> GetVersionData(
            string templateCode,
            string centerCode,
            int? year = null,
            int? version = null)
        {
            string orgCode = ProfileUtilities.User.ORGANIZE_CODE;
            var template = GetTemplate(templateCode);
            var code = UnitOfWork.Repository<CostCenterRepo>().Queryable().Where(x => x.CODE == template.ORG_CODE).Select(x => x.PARENT_CODE).FirstOrDefault();
            if(ProfileUtilities.User.ORGANIZE_CODE == "1000") {
                orgCode = code;
            }
            var lstChildren = GetListOfChildrenCenter(orgCode).Select(x => x.CODE);
            // check if orgCode is org code of template or not
            if (template.ORG_CODE.Equals(orgCode))
            {
                return UnitOfWork.Repository<KeHoachSanLuongDataRepo>()
                    .GetCFDataByOrgCode(orgCode, year.Value, templateCode, version);
            }
            else if (lstChildren.Contains(template.ORG_CODE))
            {
                return UnitOfWork.Repository<KeHoachSanLuongDataRepo>()
                    .GetCFDataByOrgCode(template.ORG_CODE, year.Value, templateCode, version);
            }
            else
            {
                return UnitOfWork.Repository<KeHoachSanLuongDataRepo>()
                    .GetCFDataByCenterCode(template.ORG_CODE, new List<string> { centerCode }, year.Value, templateCode, version);
            }
        }

        /// <summary>
        /// Summary data of a center with newest data
        /// </summary>
        /// <param name="plDataOtherKhoanMucSanLuongs">List revenue pl data want to out</param>
        /// <param name="centerCode">Center code want to summary data</param>
        /// <returns>Returns list revenue elements with their data</returns>
        public IList<T_MD_KHOAN_MUC_SAN_LUONG> SummaryCenterOut(out IList<T_BP_KE_HOACH_SAN_LUONG_DATA> plDataOtherKhoanMucSanLuongs,
                                                              string centerCode,
                                                              int year,
                                                              int? version,
                                                              bool? isHasValue = null)
        {
            if (!version.HasValue)
            {
                // get newest revenue pl data by center code
                plDataOtherKhoanMucSanLuongs = UnitOfWork.Repository<KeHoachSanLuongDataRepo>()
                    .GetManyByExpression(x => x.ORG_CODE.Equals(centerCode) && x.TIME_YEAR == year);
            }
            else
            {
                ObjDetail.ORG_CODE = centerCode;
                if (IsLeaf())
                {
                    // get all data have approved
                    plDataOtherKhoanMucSanLuongs = UnitOfWork.Repository<KeHoachSanLuongDataRepo>()
                        .GetManyByExpression(x => x.ORG_CODE == centerCode && !string.IsNullOrEmpty(x.TEMPLATE_CODE));
                }
                else
                {
                    // get list all children centers in revenue center tree
                    var lstCostCenters = UnitOfWork.Repository<CostCenterRepo>().GetManyByExpression(x => x.PARENT_CODE == centerCode);

                    // get all data have approved
                    plDataOtherKhoanMucSanLuongs = UnitOfWork.Repository<KeHoachSanLuongDataRepo>()
                        .GetManyByExpression(x => x.STATUS == Approve_Status.DaPheDuyet && (x.ORG_CODE == centerCode && !string.IsNullOrEmpty(x.TEMPLATE_CODE) ||
                    x.TIME_YEAR == year &&
                    lstCostCenters.Any(c => c.CODE.Equals(x.ORG_CODE))));
                }
            }

            return SummaryCenter(plDataOtherKhoanMucSanLuongs, centerCode, year, isHasValue);
        }

        /// <summary>
        /// Get data has summed up (history)
        /// Lấy dữ liệu đã được tổng hợp lên đơn vị cha theo version
        /// </summary>
        /// <param name="plDataOtherKhoanMucSanLuongs">List out data</param>
        /// <param name="centerCode">Org code của đơn vị được tổng hợp</param>
        /// <param name="year">Năm dữ liệu muốn xem</param>
        /// <param name="version">Version của dữ liệu muốn xem. Null thì sẽ lấy mới nhất</param>
        /// <returns></returns>
        public IList<T_MD_KHOAN_MUC_SAN_LUONG> SummaryCenterVersion(out IList<T_BP_KE_HOACH_SAN_LUONG_DATA> plDataOtherKhoanMucSanLuongs,
                                                              string centerCode,
                                                              int year,
                                                              int? version,
                                                              bool isDrillDown = false)
        {
            if (isDrillDown)
            {
                plDataOtherKhoanMucSanLuongs = GetAllSumUpDetails(centerCode, year, version.Value);
            }
            else
            {
                plDataOtherKhoanMucSanLuongs = UnitOfWork.Repository<KeHoachSanLuongDataRepo>()
                    .GetCFDataByOrgCode(centerCode, year, string.Empty, version);
            }
            return SummaryCenter(plDataOtherKhoanMucSanLuongs, centerCode, year);
        }

        /// <summary>
        /// Lấy danh sách tất cả các data đã được tổng hợp lên cho centerCode theo version và năm
        /// </summary>
        /// <param name="centerCode"></param>
        /// <param name="year"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        private IList<T_BP_KE_HOACH_SAN_LUONG_DATA> GetAllSumUpDetails(string centerCode, int year, int version)
        {
            var detailsSumUp = UnitOfWork.Repository<KeHoachSanLuongSumUpDetailRepo>()
               .GetManyWithFetch(x => x.ORG_CODE == centerCode && x.TIME_YEAR == year && x.SUM_UP_VERSION == version);

            var lstChildren = GetListOfChildrenCenter(centerCode).Select(x => x.CODE);
            var lstDetails = new List<T_BP_KE_HOACH_SAN_LUONG_DATA>();
            var lstResult = new List<T_BP_KE_HOACH_SAN_LUONG_DATA>();
            var plDataRepo = UnitOfWork.Repository<KeHoachSanLuongDataRepo>();
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

            var lookupElement = lstDetails.ToLookup(x => x.KHOAN_MUC_SAN_LUONG_CODE);
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
                            lstResult.Add(new T_BP_KE_HOACH_SAN_LUONG_DATA
                            {
                                ORG_CODE = orgCode,
                                KHOAN_MUC_SAN_LUONG_CODE = key,
                                KhoanMucSanLuong = lookupCenter[orgCode].First().KhoanMucSanLuong,
                                SanLuongProfitCenter = lookupCenter[orgCode].First().SanLuongProfitCenter,
                                Organize = lookupCenter[orgCode].First().Organize,
                                VALUE_JAN = dataOrgCode.Sum(x => x.VALUE_JAN) ?? 0,
                                VALUE_FEB = dataOrgCode.Sum(x => x.VALUE_FEB) ?? 0,
                                VALUE_MAR = dataOrgCode.Sum(x => x.VALUE_MAR) ?? 0,
                                VALUE_APR = dataOrgCode.Sum(x => x.VALUE_APR) ?? 0,
                                VALUE_MAY = dataOrgCode.Sum(x => x.VALUE_MAY) ?? 0,
                                VALUE_JUN = dataOrgCode.Sum(x => x.VALUE_JUN) ?? 0,
                                VALUE_JUL = dataOrgCode.Sum(x => x.VALUE_JUL) ?? 0,
                                VALUE_AUG = dataOrgCode.Sum(x => x.VALUE_AUG) ?? 0,
                                VALUE_SEP = dataOrgCode.Sum(x => x.VALUE_SEP) ?? 0,
                                VALUE_OCT = dataOrgCode.Sum(x => x.VALUE_OCT) ?? 0,
                                VALUE_NOV = dataOrgCode.Sum(x => x.VALUE_NOV) ?? 0,
                                VALUE_DEC = dataOrgCode.Sum(x => x.VALUE_DEC) ?? 0,
                                VALUE_SUM_YEAR = dataOrgCode.Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                                VALUE_SUM_YEAR_PREVENTIVE = dataOrgCode.Sum(x => x.VALUE_SUM_YEAR_PREVENTIVE) ?? 0,
                            });
                        }
                    }

                }
            }
            return lstResult;
        }

        private IList<T_MD_KHOAN_MUC_SAN_LUONG> SummaryCenter(IList<T_BP_KE_HOACH_SAN_LUONG_DATA> plDataOtherKhoanMucSanLuongs, string centerCode, int year,
                                                          bool? isHasValue = null)
        {

            // get all revenue elements
            var allOtherKhoanMucSanLuong = UnitOfWork.Repository<KhoanMucSanLuongRepo>().GetManyByExpression(x => x.TIME_YEAR == year);
            // get all child
            var childrenCodes = GetListOfChildrenCenter(centerCode).Select(x => x.CODE);
            // list store pure items to send to view
            var pureLstItems = new List<T_MD_KHOAN_MUC_SAN_LUONG>();

            // lookup revenue elements by parent code
            var lookupElements = plDataOtherKhoanMucSanLuongs.GroupBy(x => x.KHOAN_MUC_SAN_LUONG_CODE)
                .Select(x => x.First())
                .ToLookup(x => x.KhoanMucSanLuong.PARENT_CODE);

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
                var lst = new List<T_MD_KHOAN_MUC_SAN_LUONG>();
                // add all leaf to temp list item with child = true
                lst.AddRange(from item in lookupElements[code]
                             select new T_MD_KHOAN_MUC_SAN_LUONG
                             {
                                 CENTER_CODE = item.ORG_CODE,
                                 C_ORDER = item.KhoanMucSanLuong.C_ORDER,
                                 NAME = item.KhoanMucSanLuong.NAME,
                                 PARENT_CODE = item.KhoanMucSanLuong.PARENT_CODE,
                                 CODE = item.KhoanMucSanLuong.CODE,
                                 LEVEL = 0,
                                 IS_GROUP = false,
                                 ORG_NAME = childrenCode.Contains(item.ORG_CODE) ? item.Organize.NAME : item.SanLuongProfitCenter.NAME,
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
                    var element = allOtherKhoanMucSanLuong.FirstOrDefault(x => x.CODE == parentCode);
                    if (element != null)
                    {
                        // if find parent in all revenue element
                        parentCode = element.PARENT_CODE;
                        element.CENTER_CODE = centerCode;
                        element.LEVEL = level;
                        element.IS_GROUP = true;
                        element.TEMPLATE_CODE = lookupElements[code].FirstOrDefault().TEMPLATE_CODE;
                        element.ORG_CODE = lookupElements[code].FirstOrDefault().ORG_CODE;
                        lst.Add((T_MD_KHOAN_MUC_SAN_LUONG)element.CloneObject());     // must to clone to other object because it reference to other center
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
                                  select new T_MD_KHOAN_MUC_SAN_LUONG
                                  {
                                      CENTER_CODE = item.ORG_CODE,
                                      C_ORDER = item.KhoanMucSanLuong.C_ORDER,
                                      NAME = item.KhoanMucSanLuong.NAME,
                                      PARENT_CODE = string.Empty,
                                      CODE = item.KhoanMucSanLuong.CODE,
                                      LEVEL = 0,
                                      IS_GROUP = false,
                                      ORG_NAME = childrenCode.Contains(item.ORG_CODE) ? item.Organize.NAME : item.SanLuongProfitCenter.NAME,
                                      TEMPLATE_CODE = item.TEMPLATE_CODE,
                                      ORG_CODE = item.ORG_CODE,
                                      IsChildren = true
                                  });

            // calculate data for all pure list item
            var sum = GetSumDescendants(plDataOtherKhoanMucSanLuongs, pureLstItems, parentId: string.Empty).Distinct().ToList();
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
        private IEnumerable<T_MD_KHOAN_MUC_SAN_LUONG> GetSumDescendants(
            IList<T_BP_KE_HOACH_SAN_LUONG_DATA> costCFData,
            IList<T_MD_KHOAN_MUC_SAN_LUONG> pureItems,
            string parentId)
        {
            var lstResult = new List<T_MD_KHOAN_MUC_SAN_LUONG>
            {
                // set tổng năm
                // tạo element tổng
                new T_MD_KHOAN_MUC_SAN_LUONG
                {
                    NAME = "TỔNG CỘNG",
                    LEVEL = 0,
                    PARENT_CODE = null,
                    IS_GROUP = true,
                    IsChildren = false,
                    C_ORDER = 0,
                    CODE = string.Empty,
                    Values = new decimal[14]
                    {
                        costCFData.Sum(x => x.VALUE_JAN) ?? 0,
                        costCFData.Sum(x => x.VALUE_FEB) ?? 0,
                        costCFData.Sum(x => x.VALUE_MAR) ?? 0,
                        costCFData.Sum(x => x.VALUE_APR) ?? 0,
                        costCFData.Sum(x => x.VALUE_MAY) ?? 0,
                        costCFData.Sum(x => x.VALUE_JUN) ?? 0,
                        costCFData.Sum(x => x.VALUE_JUL) ?? 0,
                        costCFData.Sum(x => x.VALUE_AUG) ?? 0,
                        costCFData.Sum(x => x.VALUE_SEP) ?? 0,
                        costCFData.Sum(x => x.VALUE_OCT) ?? 0,
                        costCFData.Sum(x => x.VALUE_NOV) ?? 0,
                        costCFData.Sum(x => x.VALUE_DEC) ?? 0,
                        costCFData.Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        costCFData.Sum(x => x.VALUE_SUM_YEAR_PREVENTIVE) ?? 0,
                    }
                }
            };
            var lookup = pureItems.ToLookup(i => i.PARENT_CODE);
            Queue<T_MD_KHOAN_MUC_SAN_LUONG> st = new Queue<T_MD_KHOAN_MUC_SAN_LUONG>(lookup[parentId]);
            while (st.Count > 0)
            {
                // get first item in queue
                var item = st.Dequeue();
                // variable to check should return item or not
                bool shouldReturn = true;
                // lst to store children of item which have children
                var lstOtherKhoanMucSanLuongs = new List<T_MD_KHOAN_MUC_SAN_LUONG>();
                // loop through items which have parent id = item id
                foreach (var i in lookup[item.CODE])
                {
                    if (lookup[i.CODE].Count() > 0)
                    {
                        shouldReturn = false;
                        lstOtherKhoanMucSanLuongs.Add(i);
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

                            var treeData = costCFData.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE.Equals(i.CODE));
                            if (treeData != null && treeData.Count() > 0)
                            {
                                item.Values[0] += treeData.Sum(x => x.VALUE_JAN) ?? 0;
                                item.Values[1] += treeData.Sum(x => x.VALUE_FEB) ?? 0;
                                item.Values[2] += treeData.Sum(x => x.VALUE_MAR) ?? 0;
                                item.Values[3] += treeData.Sum(x => x.VALUE_APR) ?? 0;
                                item.Values[4] += treeData.Sum(x => x.VALUE_MAY) ?? 0;
                                item.Values[5] += treeData.Sum(x => x.VALUE_JUN) ?? 0;
                                item.Values[6] += treeData.Sum(x => x.VALUE_JUL) ?? 0;
                                item.Values[7] += treeData.Sum(x => x.VALUE_AUG) ?? 0;
                                item.Values[8] += treeData.Sum(x => x.VALUE_SEP) ?? 0;
                                item.Values[9] += treeData.Sum(x => x.VALUE_OCT) ?? 0;
                                item.Values[10] += treeData.Sum(x => x.VALUE_NOV) ?? 0;
                                item.Values[11] += treeData.Sum(x => x.VALUE_DEC) ?? 0;
                                item.Values[12] += treeData.Sum(x => x.VALUE_SUM_YEAR) ?? 0;
                                item.Values[13] += treeData.Sum(x => x.VALUE_SUM_YEAR_PREVENTIVE) ?? 0;
                                item.HasAssignValue = true;

                                foreach (var d in treeData)
                                {
                                    var values = new decimal[14];
                                    values[0] = treeData.Sum(x => x.VALUE_JAN) ?? 0;
                                    values[1] = treeData.Sum(x => x.VALUE_FEB) ?? 0;
                                    values[2] = treeData.Sum(x => x.VALUE_MAR) ?? 0;
                                    values[3] = treeData.Sum(x => x.VALUE_APR) ?? 0;
                                    values[4] = treeData.Sum(x => x.VALUE_MAY) ?? 0;
                                    values[5] = treeData.Sum(x => x.VALUE_JUN) ?? 0;
                                    values[6] = treeData.Sum(x => x.VALUE_JUL) ?? 0;
                                    values[7] = treeData.Sum(x => x.VALUE_AUG) ?? 0;
                                    values[8] = treeData.Sum(x => x.VALUE_SEP) ?? 0;
                                    values[9] = treeData.Sum(x => x.VALUE_OCT) ?? 0;
                                    values[10] = treeData.Sum(x => x.VALUE_NOV) ?? 0;
                                    values[11] = treeData.Sum(x => x.VALUE_DEC) ?? 0;
                                    values[12] = treeData.Sum(x => x.VALUE_SUM_YEAR) ?? 0;
                                    values[13] = treeData.Sum(x => x.VALUE_SUM_YEAR_PREVENTIVE) ?? 0;
                                    i.Values = values;
                                    var clone = (T_MD_KHOAN_MUC_SAN_LUONG)i.Clone();
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
                    var treeData = costCFData.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE.Equals(item.CODE));
                    if (treeData != null && treeData.Count() > 0)
                    {
                        item.Values[0] += treeData.Sum(x => x.VALUE_JAN) ?? 0;
                        item.Values[1] += treeData.Sum(x => x.VALUE_FEB) ?? 0;
                        item.Values[2] += treeData.Sum(x => x.VALUE_MAR) ?? 0;
                        item.Values[3] += treeData.Sum(x => x.VALUE_APR) ?? 0;
                        item.Values[4] += treeData.Sum(x => x.VALUE_MAY) ?? 0;
                        item.Values[5] += treeData.Sum(x => x.VALUE_JUN) ?? 0;
                        item.Values[6] += treeData.Sum(x => x.VALUE_JUL) ?? 0;
                        item.Values[7] += treeData.Sum(x => x.VALUE_AUG) ?? 0;
                        item.Values[8] += treeData.Sum(x => x.VALUE_SEP) ?? 0;
                        item.Values[9] += treeData.Sum(x => x.VALUE_OCT) ?? 0;
                        item.Values[10] += treeData.Sum(x => x.VALUE_NOV) ?? 0;
                        item.Values[11] += treeData.Sum(x => x.VALUE_DEC) ?? 0;
                        item.Values[12] += treeData.Sum(x => x.VALUE_SUM_YEAR) ?? 0;
                        item.Values[13] += treeData.Sum(x => x.VALUE_SUM_YEAR_PREVENTIVE) ?? 0;
                    }
                    var clone = (T_MD_KHOAN_MUC_SAN_LUONG)item.Clone();
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
                        var data = costCFData.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == item.CODE && x.ORG_CODE == item.CENTER_CODE);
                        if (data != null)
                        {
                            item.Values = new decimal[14]
                            {
                                data.Sum(x => x.VALUE_JAN) ?? 0,
                                data.Sum(x => x.VALUE_FEB) ?? 0,
                                data.Sum(x => x.VALUE_MAR) ?? 0,
                                data.Sum(x => x.VALUE_APR) ?? 0,
                                data.Sum(x => x.VALUE_MAY) ?? 0,
                                data.Sum(x => x.VALUE_JUN) ?? 0,
                                data.Sum(x => x.VALUE_JUL) ?? 0,
                                data.Sum(x => x.VALUE_AUG) ?? 0,
                                data.Sum(x => x.VALUE_SEP) ?? 0,
                                data.Sum(x => x.VALUE_OCT) ?? 0,
                                data.Sum(x => x.VALUE_NOV) ?? 0,
                                data.Sum(x => x.VALUE_DEC) ?? 0,
                                data.Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                                data.Sum(x => x.VALUE_SUM_YEAR_PREVENTIVE) ?? 0,
                            };
                        }
                    }
                    lstResult.Add(item);
                }
                else
                {
                    // add children of item which have chilren to lookup 
                    if (lstOtherKhoanMucSanLuongs.Count > 0)
                    {
                        lookup = lookup
                            .SelectMany(l => l)
                            .Concat(lstOtherKhoanMucSanLuongs)
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
        public void GenerateExportExcelKHSL(ref MemoryStream outFileStream, dynamic table, string path, int year, string centerCode, int? version, string templateId, string unit, decimal exchangeRate, IList<T_MD_KHOAN_MUC_SAN_LUONG> dataCost, IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_SAN_LUONG> detailCostElements)
        {
            // Create a new workbook and a sheet named "User Accounts"
            //Mở file Template
            var htmlMonth = table.htmlMonth;
            var htmlYear = table.htmlYear;
            var module = "KeHoachSanLuong";
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            IWorkbook workbook;
            workbook = new XSSFWorkbook(fs);
            workbook.SetSheetName(0, ModulType.GetTextSheetName(ModulType.KeHoachSanLuong));
            fs.Close();
            ISheet sheetMonth = workbook.GetSheetAt(0);
            var NUM_CELL_MONTH = string.IsNullOrEmpty(templateId) ? 18 : 22;
            InitHeaderFile(ref sheetMonth, year, centerCode, version, NUM_CELL_MONTH, templateId, "Tấn", exchangeRate);
            GenerateData(ref workbook,ref sheetMonth, dataCost, detailCostElements);

            ISheet sheetYear = workbook.GetSheetAt(1);
            var metaDataYear = ExcelHelper.GetExcelMeta(htmlYear);
            var NUM_CELL_YEAR = 7;

            InitHeaderFile(ref sheetYear, year, centerCode, version, NUM_CELL_YEAR, templateId, "Tấn", exchangeRate);
            //ExcelHelperBP.InsertHeaderTable(ref workbook, ref sheetYear, metaDataYear.MetaTHead, NUM_CELL_YEAR, ignoreFirstColumn: string.IsNullOrEmpty(templateId) || (!string.IsNullOrEmpty(templateId) && GetTemplate(templateId).IS_BASE));
            ExcelHelperBP.InsertBodyTableByYear(ref workbook,
                ref sheetYear,
                metaDataYear.MetaTBody,
                NUM_CELL_YEAR,
                module,
                ignoreFirstColumn: string.IsNullOrEmpty(templateId) || (!string.IsNullOrEmpty(templateId) && !GetTemplate(templateId).IS_BASE));




            // Save the Excel spreadsheet to a MemoryStream and return it to the client
            workbook.Write(outFileStream);
        }
        public override void GenerateExportExcel(ref MemoryStream outFileStream, dynamic table, string path, int year, string centerCode, int? version, string templateId, string unit, decimal exchangeRate)
        {
            // Create a new workbook and a sheet named "User Accounts"
            //Mở file Template
            var htmlMonth = table.htmlMonth;
            var htmlYear = table.htmlYear;
            var module = "KeHoachSanLuong";
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            IWorkbook workbook;
            workbook = new XSSFWorkbook(fs);
            workbook.SetSheetName(0, ModulType.GetTextSheetName(ModulType.KeHoachSanLuong));
            fs.Close();
            ISheet sheetMonth = workbook.GetSheetAt(0);
            var metaDataMonth = ExcelHelper.GetExcelMeta(htmlMonth);
            var NUM_CELL_MONTH = string.IsNullOrEmpty(templateId) ? 18 : 22;
            var a = GetTemplate(templateId).IS_BASE;
            InitHeaderFile(ref sheetMonth, year, centerCode, version, NUM_CELL_MONTH, templateId, "Tấn", exchangeRate);
            ExcelHelperBP.InsertHeaderTable(ref workbook, ref sheetMonth, metaDataMonth.MetaTHead, NUM_CELL_MONTH, module, ignoreFirstColumn: string.IsNullOrEmpty(templateId) || (!string.IsNullOrEmpty(templateId) && GetTemplate(templateId).IS_BASE));
            ExcelHelperBP.InsertBodyTable(ref workbook,
                ref sheetMonth,
                metaDataMonth.MetaTBody,
                NUM_CELL_MONTH,
                ignoreFirstColumn: string.IsNullOrEmpty(templateId) || (!string.IsNullOrEmpty(templateId) && !GetTemplate(templateId).IS_BASE));

            ISheet sheetYear = workbook.GetSheetAt(1);
            var metaDataYear = ExcelHelper.GetExcelMeta(htmlYear);
            var NUM_CELL_YEAR = 7;

            InitHeaderFile(ref sheetYear, year, centerCode, version, NUM_CELL_YEAR, templateId, "Tấn", exchangeRate);
            //ExcelHelperBP.InsertHeaderTable(ref workbook, ref sheetYear, metaDataYear.MetaTHead, NUM_CELL_YEAR, ignoreFirstColumn: string.IsNullOrEmpty(templateId) || (!string.IsNullOrEmpty(templateId) && GetTemplate(templateId).IS_BASE));
            ExcelHelperBP.InsertBodyTableByYear(ref workbook,
                ref sheetYear,
                metaDataYear.MetaTBody,
                NUM_CELL_YEAR,
                module,
                ignoreFirstColumn: string.IsNullOrEmpty(templateId) || (!string.IsNullOrEmpty(templateId) && !GetTemplate(templateId).IS_BASE));




            // Save the Excel spreadsheet to a MemoryStream and return it to the client
            workbook.Write(outFileStream);
        }

        private void InitHeaderFile(ref ISheet sheet, int year, string centerCode, int? version, int NUM_CELL, string templateId, string unit, decimal exchangeRate)
        {
            var name = "";
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

            var lstDetails = UnitOfWork.Repository<KeHoachSanLuongSumUpDetailRepo>()
            .GetManyByExpression(x => x.ORG_CODE == parentCenterCode &&
                x.TIME_YEAR == year &&
                x.SUM_UP_VERSION == version);
            var templateVersion = lstDetails.FirstOrDefault(y => y.TEMPLATE_CODE == templateCode)?.DATA_VERSION;
            if (templateVersion.HasValue)
            {
                // get file upload base
                return UnitOfWork.Repository<KeHoachSanLuongVersionRepo>()
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

        public void UpdateCellValue(string code,string profitCenter,int version, string colEdit, decimal? value)
        {
            UnitOfWork.Clear();
            UnitOfWork.BeginTransaction();
            var item = UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().FirstOrDefault(x => x.KHOAN_MUC_SAN_LUONG_CODE == code && x.SAN_LUONG_PROFIT_CENTER_CODE == profitCenter);
            if (item == null)
            {
                var itemHistory = UnitOfWork.Repository<KeHoachSanLuongDataHistoryRepo>().Queryable().FirstOrDefault(x => x.KHOAN_MUC_SAN_LUONG_CODE == code && x.SAN_LUONG_PROFIT_CENTER_CODE == profitCenter && x.VERSION == version);
                switch (colEdit)
                {
                    case "1":
                        itemHistory.VALUE_JAN = value;
                        break;
                    case "2":
                        itemHistory.VALUE_FEB = value;
                        break;
                    case "3":
                        itemHistory.VALUE_MAR = value;
                        break;
                    case "4":
                        itemHistory.VALUE_APR = value;
                        break;
                    case "5":
                        itemHistory.VALUE_MAY = value;
                        break;
                    case "6":
                        itemHistory.VALUE_JUN = value;
                        break;
                    case "7":
                        itemHistory.VALUE_JUL = value;
                        break;
                    case "8":
                        itemHistory.VALUE_AUG = value;
                        break;
                    case "9":
                        itemHistory.VALUE_SEP = value;
                        break;
                    case "10":
                        itemHistory.VALUE_OCT = value;
                        break;
                    case "11":
                        itemHistory.VALUE_NOV = value;
                        break;
                    case "12":
                        itemHistory.VALUE_DEC = value;
                        break;
                }
                itemHistory.VALUE_SUM_YEAR = itemHistory.VALUE_JAN + itemHistory.VALUE_FEB + itemHistory.VALUE_MAR + itemHistory.VALUE_APR + itemHistory.VALUE_MAY + itemHistory.VALUE_JUN + itemHistory.VALUE_JUL + itemHistory.VALUE_AUG + itemHistory.VALUE_SEP + itemHistory.VALUE_OCT + itemHistory.VALUE_NOV + itemHistory.VALUE_DEC;
                itemHistory.VALUE_SUM_YEAR_PREVENTIVE = itemHistory.VALUE_SUM_YEAR;
                UnitOfWork.Repository<KeHoachSanLuongDataHistoryRepo>().Update(itemHistory);
            }
            else
            {
                switch (colEdit)
                {
                    case "1":
                        item.VALUE_JAN = value;
                        break;
                    case "2":
                        item.VALUE_FEB = value;
                        break;
                    case "3":
                        item.VALUE_MAR = value;
                        break;
                    case "4":
                        item.VALUE_APR = value;
                        break;
                    case "5":
                        item.VALUE_MAY = value;
                        break;
                    case "6":
                        item.VALUE_JUN = value;
                        break;
                    case "7":
                        item.VALUE_JUL = value;
                        break;
                    case "8":
                        item.VALUE_AUG = value;
                        break;
                    case "9":
                        item.VALUE_SEP = value;
                        break;
                    case "10":
                        item.VALUE_OCT = value;
                        break;
                    case "11":
                        item.VALUE_NOV = value;
                        break;
                    case "12":
                        item.VALUE_DEC = value;
                        break;
                }
                item.VALUE_SUM_YEAR = item.VALUE_JAN + item.VALUE_FEB + item.VALUE_MAR + item.VALUE_APR + item.VALUE_MAY + item.VALUE_JUN + item.VALUE_JUL + item.VALUE_AUG + item.VALUE_SEP + item.VALUE_OCT + item.VALUE_NOV + item.VALUE_DEC;
                item.VALUE_SUM_YEAR_PREVENTIVE = item.VALUE_SUM_YEAR;
                UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Update(item);
            }
            UnitOfWork.Commit();

        }
        public T_BP_KE_HOACH_SAN_LUONG CheckTemplate(string template, int year, string orgCode) {
            try
            {
                var checkTemplate = UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().FirstOrDefault(x => x.TEMPLATE_CODE == template && x.TIME_YEAR == year && x.ORG_CODE == orgCode);
                if (checkTemplate != null)
                {
                    return checkTemplate;
                }
                else
                {
                    return new T_BP_KE_HOACH_SAN_LUONG();
                }
            }
            catch (Exception ex)
            {
                this.State = false;
                this.Exception = ex;
                return null;
            }        
        }

        public void SynchronizeData()
        {
            string connection = ConfigurationManager.ConnectionStrings["SKYPEC"].ConnectionString;
            DataTable tableData = new DataTable();
            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM SKYPECLGS_KHBAY WHERE TranYear = '{ObjDetail.TIME_YEAR}'", con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                try
                {
                    adapter.Fill(tableData);
                }
                catch (Exception ex)
                {
                    this.State = false;
                    this.Exception = ex;
                }
            }

            try
            {
                var orgCode = ProfileUtilities.User.ORGANIZE_CODE;
                var KeHoachSanLuongCurrent = CurrentRepository.Queryable().FirstOrDefault(x => x.ORG_CODE == orgCode && x.TIME_YEAR == ObjDetail.TIME_YEAR && x.TEMPLATE_CODE == ObjDetail.TEMPLATE_CODE);
                if (KeHoachSanLuongCurrent != null && !(KeHoachSanLuongCurrent.STATUS == Approve_Status.TuChoi || KeHoachSanLuongCurrent.STATUS == Approve_Status.ChuaTrinhDuyet))
                {
                    this.State = false;
                    this.ErrorMessage = "Mẫu khai báo này không ở trạng thái Từ chối hoặc Chưa trình duyệt!";
                    return;
                }

                var template = UnitOfWork.Repository<TemplateRepo>().Get(ObjDetail.TEMPLATE_CODE);
                if (template.DetailKeHoachSanLuong.Where(x => x.TIME_YEAR == ObjDetail.TIME_YEAR).Count() == 0)
                {
                    this.State = false;
                    this.ErrorMessage = $"Mẫu khai báo này chưa được định nghĩa tại năm {ObjDetail.TIME_YEAR}!";
                    return;
                }
                var versionNext = 1;
                if (KeHoachSanLuongCurrent != null)
                {
                    versionNext = KeHoachSanLuongCurrent.VERSION + 1;
                }
                var dataCurrent = new List<T_BP_KE_HOACH_SAN_LUONG_DATA>();
                if (KeHoachSanLuongCurrent != null)
                {
                    dataCurrent = UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.ORG_CODE == orgCode
                        && x.TIME_YEAR == ObjDetail.TIME_YEAR && x.TEMPLATE_CODE == ObjDetail.TEMPLATE_CODE).ToList();
                }

                var currentUser = ProfileUtilities.User?.USER_NAME;
                
                int actualRows = tableData.Rows.Count;
                if(actualRows == 0)
                {
                    this.State = false;
                    this.ErrorMessage = $"Dữ liệu năm {ObjDetail.TIME_YEAR} từ hệ thống khác không có dữ liệu! Vui lòng kiểm tra lại!";
                    return;
                }
                UnitOfWork.BeginTransaction();

                // Cập nhật version
                if (KeHoachSanLuongCurrent != null)
                {
                    // Cập nhật next version vào bảng chính
                    KeHoachSanLuongCurrent.VERSION = versionNext;
                    KeHoachSanLuongCurrent.IS_DELETED = false;
                    CurrentRepository.Update(KeHoachSanLuongCurrent);
                }
                else
                {
                    // Tạo mới bản ghi revenue pl
                    CurrentRepository.Create(new T_BP_KE_HOACH_SAN_LUONG()
                    {
                        PKID = Guid.NewGuid().ToString(),
                        ORG_CODE = orgCode,
                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                        KICH_BAN = ObjDetail.KICH_BAN,
                        PHIEN_BAN = ObjDetail.PHIEN_BAN,
                        TIME_YEAR = ObjDetail.TIME_YEAR,
                        VERSION = versionNext,
                        STATUS = Approve_Status.ChuaTrinhDuyet,
                        IS_DELETED = false,
                        IS_SUMUP = false,
                        CREATE_BY = currentUser
                    });
                }

                // Đưa next version vào bảng log
                UnitOfWork.Repository<KeHoachSanLuongVersionRepo>().Create(new T_BP_KE_HOACH_SAN_LUONG_VERSION()
                {
                    PKID = Guid.NewGuid().ToString(),
                    ORG_CODE = orgCode,
                    TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                    VERSION = versionNext,
                    KICH_BAN = KeHoachSanLuongCurrent == null ? ObjDetail.KICH_BAN : KeHoachSanLuongCurrent.KICH_BAN,
                    PHIEN_BAN = KeHoachSanLuongCurrent == null ? ObjDetail.PHIEN_BAN : KeHoachSanLuongCurrent.PHIEN_BAN,
                    TIME_YEAR = ObjDetail.TIME_YEAR,
                    CREATE_BY = currentUser
                });

                // Tạo mới bản ghi log trạng thái
                UnitOfWork.Repository<KeHoachSanLuongHistoryRepo>().Create(new T_BP_KE_HOACH_SAN_LUONG_HISTORY()
                {
                    PKID = Guid.NewGuid().ToString(),
                    ORG_CODE = orgCode,
                    TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                    KICH_BAN = KeHoachSanLuongCurrent == null ? ObjDetail.KICH_BAN : KeHoachSanLuongCurrent.KICH_BAN,
                    PHIEN_BAN = KeHoachSanLuongCurrent == null ? ObjDetail.PHIEN_BAN : KeHoachSanLuongCurrent.PHIEN_BAN,
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
                    var revenueDataHis = (T_BP_KE_HOACH_SAN_LUONG_DATA_HISTORY)item;
                    UnitOfWork.Repository<KeHoachSanLuongDataHistoryRepo>().Create(revenueDataHis);
                    UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Delete(item);
                }

                var allSanLuongProfitCenters = UnitOfWork.Repository<TemplateDetailKeHoachSanLuongRepo>().Queryable().Where(x=>x.TEMPLATE_CODE == ObjDetail.TEMPLATE_CODE).Select(x=> x.Center);
                List<T_BP_KE_HOACH_SAN_LUONG_DATA> lstData = new List<T_BP_KE_HOACH_SAN_LUONG_DATA>();
                // Insert dữ liệu vào bảng data
                for (int i = 0; i < actualRows; i++)
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
                    if(string.IsNullOrEmpty(tableData.Rows[i][15].ToString().Trim()) || string.IsNullOrEmpty(tableData.Rows[i][16].ToString().Trim()))
                    {
                        continue;
                    }

                    var centerCode = allSanLuongProfitCenters.FirstOrDefault(
                                                x => x.SanBay.OTHER_PM_CODE == tableData.Rows[i][15].ToString().Trim() &&
                                                x.HangHangKhong.OTHER_PM_CODE == tableData.Rows[i][16].ToString().Trim())?.CODE;
                    if (centerCode == null)
                    {
                        throw new Exception($"Biểu mẫu chưa khai báo đủ sân bay và hãng hàng không! Mã sân bay: {tableData.Rows[i][15].ToString().Trim()} - Mã hãng hàng không: {tableData.Rows[i][16].ToString().Trim()}");
                    }

                    var costData = new T_BP_KE_HOACH_SAN_LUONG_DATA();


                    costData = new T_BP_KE_HOACH_SAN_LUONG_DATA()
                    {
                        PKID = Guid.NewGuid().ToString(),
                        ORG_CODE = orgCode,
                        SAN_LUONG_PROFIT_CENTER_CODE = centerCode,
                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                        TIME_YEAR = ObjDetail.TIME_YEAR,
                        STATUS = Approve_Status.ChuaTrinhDuyet,
                        VERSION = versionNext,
                        KHOAN_MUC_SAN_LUONG_CODE = (tableData.Rows[i][17].ToString().Trim() == "0") ? "10010" : "10020",
                        VALUE_JAN = tableData.Rows[i][20] as decimal? == null ? 0 : tableData.Rows[i][20] as decimal?,
                        VALUE_FEB = tableData.Rows[i][21] as decimal? == null ? 0 : tableData.Rows[i][21] as decimal?,
                        VALUE_MAR = tableData.Rows[i][22] as decimal? == null ? 0 : tableData.Rows[i][22] as decimal?,
                        VALUE_APR = tableData.Rows[i][23] as decimal? == null ? 0 : tableData.Rows[i][23] as decimal?,
                        VALUE_MAY = tableData.Rows[i][24] as decimal? == null ? 0 : tableData.Rows[i][24] as decimal?,
                        VALUE_JUN = tableData.Rows[i][25] as decimal? == null ? 0 : tableData.Rows[i][25] as decimal?,
                        VALUE_JUL = tableData.Rows[i][26] as decimal? == null ? 0 : tableData.Rows[i][26] as decimal?,
                        VALUE_AUG = tableData.Rows[i][27] as decimal? == null ? 0 : tableData.Rows[i][27] as decimal?,
                        VALUE_SEP = tableData.Rows[i][28] as decimal? == null ? 0 : tableData.Rows[i][28] as decimal?,
                        VALUE_OCT = tableData.Rows[i][29] as decimal? == null ? 0 : tableData.Rows[i][29] as decimal?,
                        VALUE_NOV = tableData.Rows[i][30] as decimal? == null ? 0 : tableData.Rows[i][30] as decimal?,
                        VALUE_DEC = tableData.Rows[i][31] as decimal? == null ? 0 : tableData.Rows[i][31] as decimal?,
                        DESCRIPTION = "",
                        CREATE_BY = currentUser
                    };
                    costData.VALUE_SUM_YEAR = costData.VALUE_JAN + costData.VALUE_FEB + costData.VALUE_MAR + costData.VALUE_APR + costData.VALUE_MAY + costData.VALUE_JUN + costData.VALUE_JUL + costData.VALUE_AUG + costData.VALUE_SEP + costData.VALUE_OCT + costData.VALUE_NOV + costData.VALUE_DEC;

                    costData.VALUE_SUM_YEAR_PREVENTIVE = costData.VALUE_SUM_YEAR * percentagePreventive;

                    if (costData.VALUE_SUM_YEAR != 0 && costData.VALUE_SUM_YEAR != null)
                    {
                        lstData.Add(costData);
                    }
                    /*if (costData.VALUE_SUM_YEAR != 0 && costData.VALUE_SUM_YEAR != null)
                    {
                        UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Create(costData);
                    }   */
                }
                // Sum dữ liệu
                var groupedData = lstData
                .GroupBy(item => Tuple.Create(item.SAN_LUONG_PROFIT_CENTER_CODE, item.KHOAN_MUC_SAN_LUONG_CODE))
                .ToDictionary(
                    group => group.Key,
                    group => new T_BP_KE_HOACH_SAN_LUONG_DATA
                    {
                        PKID = Guid.NewGuid().ToString(),
                        ORG_CODE = orgCode,
                        SAN_LUONG_PROFIT_CENTER_CODE = group.Key.Item1,
                        TEMPLATE_CODE = ObjDetail.TEMPLATE_CODE,
                        TIME_YEAR = ObjDetail.TIME_YEAR,
                        STATUS = Approve_Status.ChuaTrinhDuyet,
                        VERSION = versionNext,
                        KHOAN_MUC_SAN_LUONG_CODE = group.Key.Item2,
                        VALUE_JAN = group.Sum(item => item.VALUE_JAN ),
                        VALUE_FEB = group.Sum(item => item.VALUE_FEB ),
                        VALUE_MAR = group.Sum(item => item.VALUE_MAR ),
                        VALUE_APR = group.Sum(item => item.VALUE_APR ),
                        VALUE_MAY = group.Sum(item => item.VALUE_MAY ),
                        VALUE_JUN = group.Sum(item => item.VALUE_JUN ),
                        VALUE_JUL = group.Sum(item => item.VALUE_JUL ),
                        VALUE_AUG = group.Sum(item => item.VALUE_AUG ),
                        VALUE_SEP = group.Sum(item => item.VALUE_SEP ),
                        VALUE_OCT = group.Sum(item => item.VALUE_OCT ),
                        VALUE_NOV = group.Sum(item => item.VALUE_NOV ),
                        VALUE_DEC = group.Sum(item => item.VALUE_DEC),
                        VALUE_SUM_YEAR = group.Sum(item => item.VALUE_SUM_YEAR),
                        VALUE_SUM_YEAR_PREVENTIVE = group.Sum(item => item.VALUE_SUM_YEAR_PREVENTIVE),
                        DESCRIPTION = "",
                        CREATE_BY = currentUser
                    }
                    );
                foreach(var item in groupedData)
                {
                    UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Create(item.Value);
                }

                UnitOfWork.Commit();
                NotifyUtilities.CreateNotify(
                    new NotifyPara()
                    {
                        Activity = Activity.AC_NHAP_DU_LIEU,
                        OrgCode = orgCode,
                        TemplateCode = ObjDetail.TEMPLATE_CODE,
                        TimeYear = ObjDetail.TIME_YEAR,
                        ModulType = ModulType.KeHoachSanLuong,
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

        public void GenerateData(ref IWorkbook templateWorkbook, ref ISheet sheet, IList<T_MD_KHOAN_MUC_SAN_LUONG> dataCost, IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_SAN_LUONG> detailCostElements)
        {
            try
            {
                ICellStyle styleCellBold = templateWorkbook.CreateCellStyle();
                styleCellBold.CloneStyleFrom(sheet.GetRow(9).Cells[0].CellStyle);
                var fontBold = templateWorkbook.CreateFont();
                fontBold.Boldweight = (short)FontBoldWeight.Bold;
                fontBold.FontHeightInPoints = 11;
                fontBold.FontName = "Times New Roman";

                //StyleCell
                ICellStyle styleCellHeader = templateWorkbook.CreateCellStyle();
                styleCellHeader.CloneStyleFrom(sheet.GetRow(7).Cells[0].CellStyle);
                styleCellHeader.WrapText = true;
                styleCellHeader.Alignment = HorizontalAlignment.Center;

                ICellStyle styleCellBody = templateWorkbook.CreateCellStyle();
                styleCellBody.CloneStyleFrom(sheet.GetRow(8).Cells[0].CellStyle);
                styleCellBody.WrapText = true;

                //Header
                var NUM_CELL = 19;

                #region Header
                var template = UnitOfWork.Repository<TemplateRepo>().Get(detailCostElements.FirstOrDefault().TEMPLATE_CODE);
                var rowHeader1 = ReportUtilities.CreateRow(ref sheet, 0, NUM_CELL);
                ReportUtilities.CreateCell(ref rowHeader1, NUM_CELL);
                rowHeader1.Cells[0].SetCellValue(rowHeader1.Cells[0].StringCellValue + $" {template.Organize?.Parent?.NAME.ToUpper()}");

                var rowHeader2 = ReportUtilities.CreateRow(ref sheet, 1, NUM_CELL);

                ReportUtilities.CreateCell(ref rowHeader2, NUM_CELL);
                rowHeader2.Cells[0].SetCellValue($"{template.Organize.NAME}");
                rowHeader2.Cells[2].SetCellValue(template.TITLE.ToUpper());
                #endregion

                //Body
                var rowStartBody = 8;
                var sumElement = dataCost.FirstOrDefault(x => string.IsNullOrEmpty(x.CODE));
                if (sumElement != null)
                {
                    IRow rowCur = ReportUtilities.CreateRow(ref sheet, rowStartBody, NUM_CELL);
                    for (int i = 0; i < NUM_CELL; i++)
                    {
                        rowCur.Cells[i].CellStyle = styleCellBody;
                    }
                    rowCur.Cells[2].SetCellValue(sumElement.NAME);
                    rowCur.Cells[3].SetCellValue(UtilsCore.DecimalToString(sumElement.Values[0]));
                    rowCur.Cells[4].SetCellValue(UtilsCore.DecimalToString(sumElement.Values[1]));
                    rowCur.Cells[5].SetCellValue(UtilsCore.DecimalToString(sumElement.Values[2]));
                    rowCur.Cells[6].SetCellValue(UtilsCore.DecimalToString(sumElement.Values[3]));
                    rowCur.Cells[7].SetCellValue(UtilsCore.DecimalToString(sumElement.Values[4]));
                    rowCur.Cells[8].SetCellValue(UtilsCore.DecimalToString(sumElement.Values[5]));
                    rowCur.Cells[9].SetCellValue(UtilsCore.DecimalToString(sumElement.Values[6]));
                    rowCur.Cells[10].SetCellValue(UtilsCore.DecimalToString(sumElement.Values[7]));
                    rowCur.Cells[11].SetCellValue(UtilsCore.DecimalToString(sumElement.Values[8]));
                    rowCur.Cells[12].SetCellValue(UtilsCore.DecimalToString(sumElement.Values[9]));
                    rowCur.Cells[13].SetCellValue(UtilsCore.DecimalToString(sumElement.Values[10]));
                    rowCur.Cells[14].SetCellValue(UtilsCore.DecimalToString(sumElement.Values[11]));
                    rowCur.Cells[15].SetCellValue(UtilsCore.DecimalToString(sumElement.Values[12]));
                    rowCur.Cells[16].SetCellValue(UtilsCore.DecimalToString(sumElement.Values[13]));
                    rowCur.Cells[17].SetCellValue(UtilsCore.DecimalToString((sumElement.Values[12]/12)));
                    rowStartBody++;
                }
                foreach (var detail in detailCostElements.GroupBy(x => x.CENTER_CODE).Select(x => x.First()).OrderBy(x => x.Center.SanBay.CODE).ThenBy(x => x.Center.HangHangKhong.CODE))
                {
                    foreach (var item in dataCost
                                    .Where(x => x.CENTER_CODE == detail.CENTER_CODE)
                                    .OrderBy(x => x.C_ORDER)
                                    .GroupBy(x => x.CODE)
                                    .Select(x => x.First()))
                    {
                        IRow rowCur = ReportUtilities.CreateRow(ref sheet, rowStartBody, NUM_CELL);
                        for (int i = 0; i < NUM_CELL; i++)
                        {
                            if (item.IS_GROUP)
                            {
                                rowCur.Cells[i].CellStyle = styleCellBold;
                                rowCur.Cells[i].CellStyle.SetFont(fontBold);
                            }
                            else
                            {
                                rowCur.Cells[i].CellStyle = styleCellBody;
                            }
                        }
                        rowCur.Cells[0].SetCellValue(detail.Center.SAN_BAY_CODE + "-" + detail.Center.SanBay.NAME);
                        rowCur.Cells[1].SetCellValue(detail.Center.HANG_HANG_KHONG_CODE +"-"+ detail.Center.HangHangKhong.NAME);
                        rowCur.Cells[2].SetCellValue(item.NAME);
                        rowCur.Cells[3].SetCellValue(UtilsCore.DecimalToString(item.Values[0]));
                        rowCur.Cells[4].SetCellValue(UtilsCore.DecimalToString(item.Values[1]));
                        rowCur.Cells[5].SetCellValue(UtilsCore.DecimalToString(item.Values[2]));
                        rowCur.Cells[6].SetCellValue(UtilsCore.DecimalToString(item.Values[3]));
                        rowCur.Cells[7].SetCellValue(UtilsCore.DecimalToString(item.Values[4]));
                        rowCur.Cells[8].SetCellValue(UtilsCore.DecimalToString(item.Values[5]));
                        rowCur.Cells[9].SetCellValue(UtilsCore.DecimalToString(item.Values[6]));
                        rowCur.Cells[10].SetCellValue(UtilsCore.DecimalToString(item.Values[7]));
                        rowCur.Cells[11].SetCellValue(UtilsCore.DecimalToString(item.Values[8]));
                        rowCur.Cells[12].SetCellValue(UtilsCore.DecimalToString(item.Values[9]));
                        rowCur.Cells[13].SetCellValue(UtilsCore.DecimalToString(item.Values[10]));
                        rowCur.Cells[14].SetCellValue(UtilsCore.DecimalToString(item.Values[11]));
                        rowCur.Cells[15].SetCellValue(UtilsCore.DecimalToString(item.Values[12]));
                        rowCur.Cells[16].SetCellValue(UtilsCore.DecimalToString(item.Values[13]));
                        rowCur.Cells[17].SetCellValue(UtilsCore.DecimalToString((item.Values[12] / 12)));
                        rowCur.Cells[18].SetCellValue(item.DESCRIPTION);
                        rowStartBody++;
                    }
                }
            }
            catch (Exception ex)
            {
                this.State = false;
                this.ErrorMessage = "Có lỗi xảy ra trong quá trình tạo file excel!";
                this.Exception = ex;
            }
        }

        public override void Search()
        {
            base.Search();
            /*if (ProfileUtilities.User.Organize.IS_GROUP)
            {
                var lstTemSumUp = UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.ORG_CODE == this.ObjDetail.ORG_CODE && x.TIME_YEAR == this.ObjDetail.TIME_YEAR && x.KICH_BAN == this.ObjDetail.KICH_BAN && x.IS_SUMUP);
                this.ObjList.AddRange(lstTemSumUp);
            }*/
        }
    }

    public class ViewDataQuantityPlan
    {
        public string Airport { get; set; }
        public string AirportName { get; set; }
        public string Airlines { get; set; }
        public string AirlinesName { get; set; }
        public string Element { get; set; }
        public decimal? Jan { get; set; }
        public decimal? Feb { get; set; }
        public decimal? Mar { get; set; }
        public decimal? Apr { get; set; }
        public decimal? May { get; set; }
        public decimal? Jun { get; set; }
        public decimal? Jul { get; set; }
        public decimal? Aug { get; set; }
        public decimal? Sep { get; set; }
        public decimal? Oct { get; set; }
        public decimal? Nov { get; set; }
        public decimal? Dec { get; set; }
        public decimal? Total { get; set; }
        public decimal? Average { get; set; }
        public string Des { get; set; }
    }

    public class ViewDataQuantityPlanYear
    {
        public string Airport { get; set; }
        public string AirportName { get; set; }
        public decimal? Vna { get; set; }
        public decimal? NotVna { get; set; }
        public decimal? SumNd { get; set; }
        public decimal? SumQt { get; set; }
        public decimal? Total { get; set; }
    }
}
