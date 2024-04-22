using iTextSharp.text;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SMO.Core.Entities;
using SMO.Models;
using SMO.Repository.Implement.BP.DAU_TU_NGOAI_DOANH_NGHIEP;
using SMO.Repository.Implement.BP.DAU_TU_TRANG_THIET_BI;
using SMO.Repository.Implement.BP.DAU_TU_XAY_DUNG;
using SMO.Repository.Implement.BP.KE_HOACH_CHI_PHI;
using SMO.Repository.Implement.BP.KE_HOACH_DOANH_THU;
using SMO.Repository.Implement.BP.KE_HOACH_SAN_LUONG;
using SMO.Repository.Implement.MD;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SMO.Service.MD
{
    public class KichBanService : GenericService<T_MD_KICH_BAN, KichBanRepo>
    {
        public KichBanService() : base()
        {

        }

        public override void Create()
        {
            try
            {
                if (!CheckExist(x => x.CODE == ObjDetail.CODE))
                {
                    base.Create();
                }
                else
                {
                    State = false;
                    MesseageCode = "1101";
                }
            }
            catch (Exception ex)
            {
                State = false;
                Exception = ex;
            }
        }

        public IList<SynthesisReportModel> GetData(int year, string kichBan)
        {
            var _elementService = new ElementService();
            var dataKeHoachTaiChinh = _elementService.GetDataKeHoachTaiChinh(year).KeHoachTaiChinhData;
            var data = new List<SynthesisReportModel>();

            #region Nộp ngân sách Nhà nước
            data.Add(new SynthesisReportModel
            {
                Stt = "I",
                Name = "Nộp ngân sách Nhà nước",
                UnitName = "Tr.đ",
                IsBold = true,
                Order = 1,
            });
            #endregion

            #region Trong đó: Các loại thuế
            data.Add(new SynthesisReportModel
            {
                Name = "Trong đó: Các loại thuế",
                UnitName = "Tr.đ",
                Order = 2,
            });
            #endregion

            #region Sản lượng
            var value_3_1 = (from x in UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year - 2 && x.PHIEN_BAN == "PB5" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachSanLuongDataHistoryRepo>().GetAll() on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_3_2 = (from x in UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year - 1 && x.PHIEN_BAN == "PB1" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachSanLuongDataRepo>().GetAll() on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_3_4 = (from x in UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year - 1 && x.PHIEN_BAN == "PB5" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachSanLuongDataRepo>().GetAll() on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_3_5 = (from x in UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB1" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachSanLuongDataRepo>().GetAll() on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_3_6 = (from x in UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB2" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachSanLuongDataRepo>().GetAll() on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            data.Add(new SynthesisReportModel
            {
                Id = "I",
                Stt = "II",
                Name = "Sản lượng",
                Order = 3,
                IsBold = true,
                Value1 = value_3_1 ?? 0,
                Value2 = value_3_2 ?? 0,
                Value3 = 0,
                Value4 = value_3_4 ?? 0,
                Value5 = value_3_5 ?? 0,
                Value6 = value_3_6 ?? 0,
                Value7 = (value_3_5 == 0 || value_3_1 == 0) ? 0 : value_3_5 / value_3_1,
                Value8 = value_3_5 == 0 || value_3_4 == 0 ? 0 : value_3_5 / value_3_4,
            });
            #endregion

            #region Cung ứng cho VNA Group
            var value_4_1 = (from x in UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year - 2 && x.PHIEN_BAN == "PB5" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachSanLuongDataHistoryRepo>().Queryable().Where(x => x.SanLuongProfitCenter.HangHangKhong.IS_VNA == true).ToList() on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_4_2 = (from x in UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year - 1 && x.PHIEN_BAN == "PB1" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.SanLuongProfitCenter.HangHangKhong.IS_VNA == true).ToList() on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_4_4 = (from x in UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year - 1 && x.PHIEN_BAN == "PB5" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.SanLuongProfitCenter.HangHangKhong.IS_VNA == true).ToList() on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_4_5 = (from x in UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB1" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.SanLuongProfitCenter.HangHangKhong.IS_VNA == true).ToList() on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_4_6 = (from x in UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB2" && x.KICH_BAN == kichBan).ToList()
                             join y in UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.SanLuongProfitCenter.HangHangKhong.IS_VNA == true).ToList() on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            data.Add(new SynthesisReportModel
            {
                Id = "I.1",
                Stt = "1",
                Name = "Cung ứng cho VNA Group",
                Order = 4,
                Value1 = value_4_1 ?? 0,
                Value2 = value_4_2 ?? 0,
                Value3 = 0,
                Value4 = value_4_4 ?? 0,
                Value5 = value_4_5 ?? 0,
                Value6 = value_4_6 ?? 0,
                Value7 = (value_4_5 == 0 || value_4_1 == 0) ? 0 : value_4_5 / value_4_1,
                Value8 = value_4_5 == 0 || value_4_4 == 0 ? 0 : value_4_5 / value_4_4,
            });
            #endregion

            #region Cung ứng cho VNA
            var value_5_1 = (from x in UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year - 2 && x.PHIEN_BAN == "PB5" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.SanLuongProfitCenter.HANG_HANG_KHONG_CODE == "VN").ToList() on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_5_2 = (from x in UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year - 1 && x.PHIEN_BAN == "PB1" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.SanLuongProfitCenter.HANG_HANG_KHONG_CODE == "VN").ToList() on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_5_4 = (from x in UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year - 1 && x.PHIEN_BAN == "PB5" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.SanLuongProfitCenter.HANG_HANG_KHONG_CODE == "VN").ToList() on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_5_5 = (from x in UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB1" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.SanLuongProfitCenter.HANG_HANG_KHONG_CODE == "VN").ToList() on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_5_6 = (from x in UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB2" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.SanLuongProfitCenter.HANG_HANG_KHONG_CODE == "VN").ToList() on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            data.Add(new SynthesisReportModel
            {
                Id= "I.1.1",
                Name = "Cung ứng cho VNA",
                Order = 5,
                Value1 = value_5_1 ?? 0,
                Value2 = value_5_2 ?? 0,
                Value3 = 0,
                Value4 = value_5_4 ?? 0,
                Value5 = value_5_5 ?? 0,
                Value6 = value_5_6 ?? 0,
                Value7 = (value_5_5 == 0 || value_5_1 == 0) ? 0 : value_5_5 / value_5_1,
                Value8 = value_5_5 == 0 || value_5_4 == 0 ? 0 : value_5_5 / value_5_4,
            });
            #endregion

            var value_6_1 = (from x in UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year - 2 && x.PHIEN_BAN == "PB5" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.SanLuongProfitCenter.HangHangKhong.IS_VNA == true && x.SanLuongProfitCenter.HANG_HANG_KHONG_CODE != "VN").ToList() on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_6_2 = (from x in UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year - 1 && x.PHIEN_BAN == "PB1" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.SanLuongProfitCenter.HangHangKhong.IS_VNA == true && x.SanLuongProfitCenter.HANG_HANG_KHONG_CODE != "VN").ToList() on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_6_4 = (from x in UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year - 1 && x.PHIEN_BAN == "PB5" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.SanLuongProfitCenter.HangHangKhong.IS_VNA == true && x.SanLuongProfitCenter.HANG_HANG_KHONG_CODE != "VN").ToList() on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_6_5 = (from x in UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB1" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.SanLuongProfitCenter.HangHangKhong.IS_VNA == true && x.SanLuongProfitCenter.HANG_HANG_KHONG_CODE != "VN").ToList() on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_6_6 = (from x in UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB2" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.SanLuongProfitCenter.HangHangKhong.IS_VNA == true && x.SanLuongProfitCenter.HANG_HANG_KHONG_CODE != "VN").ToList() on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            data.Add(new SynthesisReportModel
            {
                Id= "I.1.2",
                Name = "Cung ứng cho các hãng hàng không khác trong VNA Group",
                Order = 6,
                Value1 = value_6_1 ?? 0,
                Value2 = value_6_2 ?? 0,
                Value3 = 0,
                Value4 = value_6_4 ?? 0,
                Value5 = value_6_5 ?? 0,
                Value6 = value_6_6 ?? 0,
                Value7 = (value_6_5 == 0 || value_6_1 == 0) ? 0 : value_6_5 / value_6_1,
                Value8 = value_6_5 == 0 || value_6_4 == 0 ? 0 : value_6_5 / value_6_4,
            });
            var value_7_1 = (from x in UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year - 2 && x.PHIEN_BAN == "PB5" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.SanLuongProfitCenter.HangHangKhong.IS_VNA == false).ToList() on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_7_2 = (from x in UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year - 1 && x.PHIEN_BAN == "PB1" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.SanLuongProfitCenter.HangHangKhong.IS_VNA == false).ToList() on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_7_4 = (from x in UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year - 1 && x.PHIEN_BAN == "PB5" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.SanLuongProfitCenter.HangHangKhong.IS_VNA == false).ToList() on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_7_5 = (from x in UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB1" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.SanLuongProfitCenter.HangHangKhong.IS_VNA == false).ToList() on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_7_6 = (from x in UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB2" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.SanLuongProfitCenter.HangHangKhong.IS_VNA == false).ToList() on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            data.Add(new SynthesisReportModel
            {
                Id = "I.2",
                Stt = "2",
                Name = "Cung ứng cho đối tác khác (*)",
                UnitName = "Tr.đ",
                Order = 7,
                Value1 = value_7_1 ?? 0,
                Value2 = value_7_2 ?? 0,
                Value3 = 0,
                Value4 = value_7_4 ?? 0,
                Value5 = value_7_5 ?? 0,
                Value6 = value_7_6 ?? 0,
                Value7 = (value_7_5 == 0 || value_7_1 == 0) ? 0 : value_7_5 / value_7_1,
                Value8 = value_7_5 == 0 || value_7_4 == 0 ? 0 : value_7_5 / value_7_4,
            });


            var value_9_1 = (from x in UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.TIME_YEAR == year - 2 && x.PHIEN_BAN == "PB5" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachDoanhThuDataHistoryRepo>().Queryable().Where(x => x.KHOAN_MUC_DOANH_THU_CODE == "2001" || x.KHOAN_MUC_DOANH_THU_CODE == "2002") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_9_2 = (from x in UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.TIME_YEAR == year - 1 && x.PHIEN_BAN == "PB1" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => x.KHOAN_MUC_DOANH_THU_CODE == "2001" || x.KHOAN_MUC_DOANH_THU_CODE == "2002") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_9_4 = (from x in UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.TIME_YEAR == year - 1 && x.PHIEN_BAN == "PB5" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => x.KHOAN_MUC_DOANH_THU_CODE == "2001" || x.KHOAN_MUC_DOANH_THU_CODE == "2002") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_9_5 = (from x in UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB1" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => x.KHOAN_MUC_DOANH_THU_CODE == "2001" || x.KHOAN_MUC_DOANH_THU_CODE == "2002") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_9_6 = (from x in UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB2" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => x.KHOAN_MUC_DOANH_THU_CODE == "2001" || x.KHOAN_MUC_DOANH_THU_CODE == "2002") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            data.Add(new SynthesisReportModel
            {
                Id = "II",
                Stt = "1",
                Name = "Doanh thu từ hoạt động SXKD",
                UnitName = "Tr.đ",
                Order = 9,
                Value1 = value_9_1 ?? 0,
                Value2 = value_9_2 ?? 0,
                Value3 = 0,
                Value4 = value_9_4 ?? 0,
                Value5 = value_9_5 ?? 0,
                Value6 = value_9_6 ?? 0,
                Value7 = (value_9_5 == 0 || value_9_1 == 0) ? 0 : value_9_5 / value_9_1,
                Value8 = value_9_5 == 0 || value_9_4 == 0 ? 0 : value_9_5 / value_9_4,
            });
            data.Add(new SynthesisReportModel
            {
                Id = "II.1",
                Name = " - Doanh thu cung ứng cho VNA ",
                UnitName = "Tr.đ",
                Order = 10,
            });

            var value_11_1 = (from x in UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.TIME_YEAR == year - 2 && x.PHIEN_BAN == "PB5" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachDoanhThuDataHistoryRepo>().Queryable().Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == "VN").Where(x => x.KHOAN_MUC_DOANH_THU_CODE == "2001" || x.KHOAN_MUC_DOANH_THU_CODE == "2002") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_11_2 = (from x in UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.TIME_YEAR == year - 1 && x.PHIEN_BAN == "PB1" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == "VN").Where(x => x.KHOAN_MUC_DOANH_THU_CODE == "2001" || x.KHOAN_MUC_DOANH_THU_CODE == "2002") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_11_4 = (from x in UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.TIME_YEAR == year - 1 && x.PHIEN_BAN == "PB5" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == "VN").Where(x => x.KHOAN_MUC_DOANH_THU_CODE == "2001" || x.KHOAN_MUC_DOANH_THU_CODE == "2002") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_11_5 = (from x in UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB1" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == "VN").Where(x => x.KHOAN_MUC_DOANH_THU_CODE == "2001" || x.KHOAN_MUC_DOANH_THU_CODE == "2002") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_11_6 = (from x in UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB2" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == "VN").Where(x => x.KHOAN_MUC_DOANH_THU_CODE == "2001" || x.KHOAN_MUC_DOANH_THU_CODE == "2002") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.VALUE_SUM_YEAR);
            data.Add(new SynthesisReportModel
            {
                Id = "II.1.1",
                Name = "+ Doanh thu VNA",
                UnitName = "Tr.đ",
                Order = 11,
                Value1 = value_11_1 ?? 0,
                Value2 = value_11_2 ?? 0,
                Value3 = 0,
                Value4 = value_11_4 ?? 0,
                Value5 = value_11_5 ?? 0,
                Value6 = value_11_6 ?? 0,
                Value7 = (value_11_5 == 0 || value_11_1 == 0) ? 0 : value_11_5 / value_11_1,
                Value8 = value_11_5 == 0 || value_11_4 == 0 ? 0 : value_11_5 / value_11_4,
            });
            data.Add(new SynthesisReportModel
            {
                Id = "II.1.1.1",
                Name = "Trong đó: CK/Giảm giá",
                Order = 12,
            });

            var value_13_1 = (from x in UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.TIME_YEAR == year - 2 && x.PHIEN_BAN == "PB5" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE != "VN" && x.DoanhThuProfitCenter.HangHangKhong.IS_VNA == true).Where(x => x.KHOAN_MUC_DOANH_THU_CODE == "2001" || x.KHOAN_MUC_DOANH_THU_CODE == "2002") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_13_2 = (from x in UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.TIME_YEAR == year - 1 && x.PHIEN_BAN == "PB1" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE != "VN" && x.DoanhThuProfitCenter.HangHangKhong.IS_VNA == true).Where(x => x.KHOAN_MUC_DOANH_THU_CODE == "2001" || x.KHOAN_MUC_DOANH_THU_CODE == "2002") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_13_4 = (from x in UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.TIME_YEAR == year - 1 && x.PHIEN_BAN == "PB5" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE != "VN" && x.DoanhThuProfitCenter.HangHangKhong.IS_VNA == true).Where(x => x.KHOAN_MUC_DOANH_THU_CODE == "2001" || x.KHOAN_MUC_DOANH_THU_CODE == "2002") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_13_5 = (from x in UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB1" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE != "VN" && x.DoanhThuProfitCenter.HangHangKhong.IS_VNA == true).Where(x => x.KHOAN_MUC_DOANH_THU_CODE == "2001" || x.KHOAN_MUC_DOANH_THU_CODE == "2002") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_13_6 = (from x in UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB2" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE != "VN" && x.DoanhThuProfitCenter.HangHangKhong.IS_VNA == true).Where(x => x.KHOAN_MUC_DOANH_THU_CODE == "2001" || x.KHOAN_MUC_DOANH_THU_CODE == "2002") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.VALUE_SUM_YEAR);
            data.Add(new SynthesisReportModel
            {
                Id = "II.1.2",
                Name = "+ Doanh thu hãng HK trong VNA group",
                UnitName = "Tr.đ",
                Order = 13,
                Value1 = value_13_1 ?? 0,
                Value2 = value_13_2 ?? 0,
                Value3 = 0,
                Value4 = value_13_4 ?? 0,
                Value5 = value_13_5 ?? 0,
                Value6 = value_13_6 ?? 0,
                Value7 = (value_13_5 == 0 || value_13_1 == 0) ? 0 : value_13_5 / value_13_1,
                Value8 = value_13_5 == 0 || value_13_4 == 0 ? 0 : value_13_5 / value_13_4,
            });
            data.Add(new SynthesisReportModel
            {
                Id = "II.1.2.1",
                Name = "Trong đó: CK/Giảm giá",
                Order = 14,
            });

            var value_15_1 = (from x in UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.TIME_YEAR == year - 2 && x.PHIEN_BAN == "PB5" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => x.DoanhThuProfitCenter.HangHangKhong.IS_VNA == false).Where(x => x.KHOAN_MUC_DOANH_THU_CODE == "2001" || x.KHOAN_MUC_DOANH_THU_CODE == "2002") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_15_2 = (from x in UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.TIME_YEAR == year - 1 && x.PHIEN_BAN == "PB1" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => x.DoanhThuProfitCenter.HangHangKhong.IS_VNA == false).Where(x => x.KHOAN_MUC_DOANH_THU_CODE == "2001" || x.KHOAN_MUC_DOANH_THU_CODE == "2002") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_15_4 = (from x in UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.TIME_YEAR == year - 1 && x.PHIEN_BAN == "PB5" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => x.DoanhThuProfitCenter.HangHangKhong.IS_VNA == false).Where(x => x.KHOAN_MUC_DOANH_THU_CODE == "2001" || x.KHOAN_MUC_DOANH_THU_CODE == "2002") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_15_5 = (from x in UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB1" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => x.DoanhThuProfitCenter.HangHangKhong.IS_VNA == false).Where(x => x.KHOAN_MUC_DOANH_THU_CODE == "2001" || x.KHOAN_MUC_DOANH_THU_CODE == "2002") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_15_6 = (from x in UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB2" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => x.DoanhThuProfitCenter.HangHangKhong.IS_VNA == false).Where(x => x.KHOAN_MUC_DOANH_THU_CODE == "2001" || x.KHOAN_MUC_DOANH_THU_CODE == "2002") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.VALUE_SUM_YEAR);
            data.Add(new SynthesisReportModel
            {
                Id = "II.2",
                Name = " - Doanh thu cung ứng cho đối tác khác (*)",
                UnitName = "Tr.đ",
                Order = 15,
                Value1 = value_15_1 ?? 0,
                Value2 = value_15_2 ?? 0,
                Value3 = 0,
                Value4 = value_15_4 ?? 0,
                Value5 = value_15_5 ?? 0,
                Value6 = value_15_6 ?? 0,
                Value7 = (value_15_5 == 0 || value_15_1 == 0) ? 0 : value_15_5 / value_15_1,
                Value8 = value_15_5 == 0 || value_15_4 == 0 ? 0 : value_15_5 / value_15_4,
            });
            data.Add(new SynthesisReportModel
            {
                Id = "II.2.1",
                Name = "Trong đó: CK/Giảm giá",
                Order = 16,
            });

            #region - Chênh lệch tỷ giá

            var value18_5 = dataKeHoachTaiChinh.FirstOrDefault(x => x.ElementCode == "U0130" && x.Screen == "KE_HOACH_TAI_CHINH_2").Value;
            data.Add(new SynthesisReportModel
            {
                Name = " - Chênh lệch tỷ giá",
                UnitName = "Tr.đ",
                Order = 18,
                Value5 = value18_5 ?? 0,
            });
            #endregion

            #region - Doanh thu HĐ tài chính khác
            var value19_5 = dataKeHoachTaiChinh.FirstOrDefault(x => x.ElementCode == "U0132" && x.Screen == "KE_HOACH_TAI_CHINH_2").Value;
            data.Add(new SynthesisReportModel
            {
                Name = " - Doanh thu HĐ tài chính khác",
                UnitName = "Tr.đ",
                Order = 19,
                Value5 = value19_5 ?? 0,
            });
            #endregion

            #region Doanh thu từ hoạt động tài chính
            var value_17_5 = value19_5 + value18_5;
            data.Add(new SynthesisReportModel
            {
                Stt = "2",
                Name = "Doanh thu từ hoạt động tài chính",
                UnitName = "Tr.đ",
                Order = 17,
                Value5 = value_17_5 ?? 0,
            });
            #endregion

            data.Add(new SynthesisReportModel
            {
                Stt = "3",
                Name = "Thu nhập khác",
                UnitName = "Tr.đ",
                Order = 20,
            });

            #region Tổng doanh thu và thu nhập khác
            var value_8_5 = value_9_5 + value_17_5;
            data.Add(new SynthesisReportModel
            {
                Stt = "III",
                Name = "Tổng doanh thu và thu nhập khác",
                UnitName = "Tr.đ",
                Order = 8,
                IsBold = true,
                Value5 = value_8_5 ?? 0,
            });
            #endregion

            #region - Chi phí nhân công
            var value_23_5 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB1" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6271")) on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            data.Add(new SynthesisReportModel
            {
                Name = " - Chi phí nhân công",
                UnitName = "Tr.đ",
                Order = 23,
                Value5 = value_23_5 ?? 0,
            });
            #endregion


            data.Add(new SynthesisReportModel
            {
                Name = "Trong đó:",
                Order = 24,
            });

            #region + Quỹ lương
            var value_25_5 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB1" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6271A001")) on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            data.Add(new SynthesisReportModel
            {
                Name = " + Quỹ lương",
                UnitName = "Tr.đ",
                Order = 25,
                Value5 = value_25_5 ?? 0,
            });
            #endregion

            #region + Các khoản đóng góp (BHXH, BHYT, BHTN, KPCĐ)
            var value_26_5 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB1" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6271A001") && x.KHOAN_MUC_HANG_HOA_CODE != "B6271A001" && x.KHOAN_MUC_HANG_HOA_CODE != "T6271A001" && x.KHOAN_MUC_HANG_HOA_CODE != "N6271A001" && x.KHOAN_MUC_HANG_HOA_CODE != "VT6271A001") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            data.Add(new SynthesisReportModel
            {
                Name = " + Các khoản đóng góp (BHXH, BHYT, BHTN, KPCĐ)",
                UnitName = "Tr.đ",
                Order = 26,
                Value5 = value_26_5 ?? 0,
            });
            #endregion

            #region  - Chi phí nguyên vật liệu, vật tư, vốn hàng
            var value_27_5 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB1" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6272")) on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            data.Add(new SynthesisReportModel
            {
                Name = " - Chi phí nguyên vật liệu, vật tư, vốn hàng",
                UnitName = "Tr.đ",
                Order = 27,
                Value5 = value_27_5 ?? 0,
            });
            #endregion

            #region - Khấu hao tài sản cố định
            var value_28_5 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB1" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6274")) on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            data.Add(new SynthesisReportModel
            {
                Name = "- Khấu hao tài sản cố định",
                UnitName = "Tr.đ",
                Order = 28,
                Value5 = value_28_5 ?? 0,
            });
            #endregion

            #region - Chi phí bảo dưỡng sửa chữa tài sản
            var value_29_5 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB1" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "3000104") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            data.Add(new SynthesisReportModel
            {
                Name = "- Chi phí bảo dưỡng sửa chữa tài sản",
                UnitName = "Tr.đ",
                Order = 29,
                Value5 = value_29_5 ?? 0,
            });
            #endregion

            #region - Chi phí dụng cụ sản xuất
            var value_30_5 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB1" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6273")) on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            data.Add(new SynthesisReportModel
            {
                Name = " - Chi phí dụng cụ sản xuất",
                UnitName = "Tr.đ",
                Order = 30,
                Value5 = value_30_5 ?? 0,
            });
            #endregion

            #region - Chi phí dịch vụ mua ngoài (*)
            var value_31_5 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB1" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277")) on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            data.Add(new SynthesisReportModel
            {
                Name = " - Chi phí dịch vụ mua ngoài (*)",
                UnitName = "Tr.đ",
                Order = 31,
                Value5 = value_31_5 ?? 0,
            });
            #endregion

            #region - Chi phí khác bằng tiền (*)
            var value_32_5 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB1" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278")) on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            data.Add(new SynthesisReportModel
            {
                Name = "- Chi phí khác bằng tiền (*)",
                UnitName = "Tr.đ",
                Order = 32,
                Value5 = value_32_5 ?? 0,
            });
            #endregion

            #region - Dự phòng trợ cấp mất việc
            var value_33_5 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB1" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "3000108") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            data.Add(new SynthesisReportModel
            {
                Name = "- Dự phòng trợ cấp mất việc",
                UnitName = "Tr.đ",
                Order = 33,
                Value5 = value_33_5 ?? 0,
            });
            #endregion

            #region - Thuế, phí lệ phí
            var value_34_5 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB1" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6275")) on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            data.Add(new SynthesisReportModel
            {
                Name = "- Thuế, phí lệ phí",
                UnitName = "Tr.đ",
                Order = 34,
                Value5 = value_34_5 ?? 0,
            });
            #endregion

            #region - Chi phí dự phòng
            var value_35_5 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB1" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6276")) on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            data.Add(new SynthesisReportModel
            {
                Name = " - Chi phí dự phòng",
                UnitName = "Tr.đ",
                Order = 35,
                Value5 = value_35_5 ?? 0,
            });
            #endregion

            #region Chi phí tài chính
            var value_36_5 = dataKeHoachTaiChinh.FirstOrDefault(x => x.ElementCode == "U0138" && x.Screen == "KE_HOACH_TAI_CHINH_2").Value;
            data.Add(new SynthesisReportModel
            {
                Stt="2",
                Name = "Chi phí tài chính",
                UnitName = "Tr.đ",
                Order = 36,
                Value5 = value_36_5 ?? 0,
            });
            #endregion

            #region Chi phí khác
            var value_37_5 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB1" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "30003") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            data.Add(new SynthesisReportModel
            {
                Stt="3",
                Name = "Chi phí khác",
                UnitName = "Tr.đ",
                Order = 37,
                Value5 = value_37_5 ?? 0,
            });
            #endregion

            data.Add(new SynthesisReportModel
            {
                Stt="V",
                Name = "Lợi nhuận",
                Order = 38,
                IsBold=true,
            });

            #region Chi phí sản xuất kinh doanh
            var value_22_5 = value_23_5 + value_27_5 + value_28_5 + value_29_5 + value_30_5 + value_31_5 + value_32_5 + value_33_5 + value_34_5 + value_35_5;
            data.Add(new SynthesisReportModel
            {
                Stt = "1",
                Name = "Chi phí sản xuất kinh doanh",
                UnitName = "Tr.đ",
                Order = 22,
                Value5 = value_22_5 ?? 0,
            });
            #endregion

            #region Tổng chi phí
            var value_21_5 = value_22_5 + value_36_5 + value_37_5;
            data.Add(new SynthesisReportModel
            {
                Stt = "IV",
                Name = "Tổng chi phí",
                UnitName = "Tr.đ",
                Order = 21,
                IsBold = true,
                Value5 = value_21_5 ?? 0,
            });
            #endregion

            #region Tổng LN kế toán trước thuế
            var value_39_5 = value_8_5 - value_21_5;
            data.Add(new SynthesisReportModel
            {
                Stt = "1",
                Name = "Tổng LN kế toán trước thuế",
                UnitName = "Tr.đ",
                Order = 39,
                Value5 = value_39_5 ?? 0,
            });
            #endregion

            #region Trong đó: Lợi nhuận từ HĐ SXKD
            var value_40_5 = value_9_5 - (value_23_5 + value_27_5 + value_28_5 + value_29_5 + value_30_5 + value_31_5 + value_32_5 + value_33_5 + value_34_5 + value_35_5);
            data.Add(new SynthesisReportModel
            {
                Name = "Trong đó: Lợi nhuận từ HĐ SXKD",
                Order = 40,
                Value5 = value_40_5 ?? 0,
            });
            #endregion

            data.Add(new SynthesisReportModel
            {
                Stt = "2",
                Name = "Lợi nhuận sau thuế",
                UnitName = "Tr.đ",
                Order = 41,
            });
            data.Add(new SynthesisReportModel
            {
                Stt="3",
                Name = "Lợi nhuận chia về TCTHK",
                UnitName = "Tr.đ",
                Order = 42,
            });
            data.Add(new SynthesisReportModel
            {
                Stt="VI",
                Name = "Lao động sử dụng",
                UnitName = "Tr.đ",
                Order = 43,
                IsBold = true,
            });
            data.Add(new SynthesisReportModel
            {
                Name = " - Lao động bình quân",
                UnitName = "Người",
                Order = 44,
            });
            data.Add(new SynthesisReportModel
            {
                Name = "- Thu nhập bình quân",
                UnitName = "Tr.đ/Năm",
                Order = 45,
            });
            data.Add(new SynthesisReportModel
            {
                Stt="VII",
                Name = "Vốn đầu tư của chủ sở hữu",
                Order = 46,
                IsBold= true,
            });
            data.Add(new SynthesisReportModel
            {
                Name = " - Vốn góp CSH cuối kỳ báo cáo",
                UnitName = "Tr.đ",
                Order = 47,
            });
            data.Add(new SynthesisReportModel
            {
                Name = "- Tăng giảm vốn góp CSH trong kỳ",
                UnitName = "Tr.đ",
                Order = 48,
            });
            data.Add(new SynthesisReportModel
            {
                Name = "- VCSH bình quân trong kỳ",
                UnitName = "Tr.đ",
                Order = 49,
            });
            data.Add(new SynthesisReportModel
            {
                Stt="VIII",
                Name = "Tỷ suất LN thực hiện/Vốn CSH BQ",
                UnitName = "%",
                Order = 50,
                IsBold = true,
            });

            #region - Giá trị khối lượng công việc hoàn thành
            var value_t1_53_5 = (from x in UnitOfWork.Repository<DauTuXayDungRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB2" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<DauTuXayDungDataRepo>().Queryable().Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4011") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.VALUE);
            var value_t2_53_5 = (from x in UnitOfWork.Repository<DauTuTrangThietBiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB2" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                                 join y in UnitOfWork.Repository<DauTuTrangThietBiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4032") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                                 select y).Sum(x => x.VALUE);
            var value_53_5 = value_t1_53_5 + value_t2_53_5;
            data.Add(new SynthesisReportModel
            {
                Name = " - Giá trị khối lượng công việc hoàn thành ",
                UnitName = "Tr.đ",
                Order = 53,
                Value5 = value_53_5 ?? 0,
            });
            #endregion

            #region - Giá trị giải ngân
            var value_t1_54_5 = (from x in UnitOfWork.Repository<DauTuXayDungRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB2" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                                 join y in UnitOfWork.Repository<DauTuXayDungDataRepo>().Queryable().Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4021") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                                 select y).Sum(x => x.VALUE);
            var value_t2_54_5 = (from x in UnitOfWork.Repository<DauTuTrangThietBiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB2" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                                 join y in UnitOfWork.Repository<DauTuTrangThietBiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4021") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                                 select y).Sum(x => x.VALUE);
            var value_54_5 = value_t1_54_5 + value_t2_54_5;
            data.Add(new SynthesisReportModel
            {
                Name = "- Giá trị giải ngân ",
                UnitName = "Tr.đ",
                Order = 54,
                Value5 = value_54_5 ?? 0,
            });
            #endregion

            #region Đầu tư XDCB và TTB
            var value_52_5 = value_54_5 + value_53_5;
            data.Add(new SynthesisReportModel
            {
                Stt = "1",
                Name = "Đầu tư XDCB và TTB",
                Order = 52,
                Value5 = value_52_5 ?? 0,
            });
            #endregion

            #region Đầu tư vốn vào DN khác
            var value_55_5 = (from x in UnitOfWork.Repository<DauTuNgoaiDoanhNghiepRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB2" && x.KICH_BAN == kichBan && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<DauTuNgoaiDoanhNghiepDataRepo>().Queryable().Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.VALUE_3);
            data.Add(new SynthesisReportModel
            {
                Stt="2",
                Name = "Đầu tư vốn vào DN khác",
                UnitName = "Tr.đ",
                Order = 55,
                Value5 = value_55_5 ?? 0,
            });
            #endregion

            #region Kế hoạch đầu tư
            var value_51_5 = value_52_5 + value_55_5;
            data.Add(new SynthesisReportModel
            {
                Stt = "IX",
                Name = "Kế hoạch đầu tư",
                Order = 51,
                IsBold = true,
                Value5 = value_51_5 ?? 0,
            });
            #endregion

            return data;
        }

        internal void ExportExcel(ref MemoryStream outFileStream,
                                        string path, int year, string kichBan)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                IWorkbook templateWorkbook;
                templateWorkbook = new XSSFWorkbook(fs);
                fs.Close();
                ISheet sheet = templateWorkbook.GetSheetAt(0);
                //var styleCellNumber = GetCellStyleNumber(templateWorkbook);
                //var styleCellNumberDecimal = GetCellStyleNumberDecimal(templateWorkbook);

                var data = GetData(year, kichBan);

                if (data.Count <= 1)
                {
                    this.State = false;
                    this.ErrorMessage = "Không có dữ liệu!";
                    return;
                }

                var startRow = 9;

                for (int i = 0; i < data.Count(); i++)
                {
                    var dataRow = data[i];
                    IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, 11);
                    rowCur.Cells[3].SetCellValue(data[i]?.Value1 == null ? 0 : (double)data[i]?.Value1);
                    rowCur.Cells[4].SetCellValue(data[i]?.Value2 == null ? 0 : (double)data[i]?.Value2);
                    rowCur.Cells[5].SetCellValue(data[i]?.Value3 == null ? 0 : (double)data[i]?.Value3);
                    rowCur.Cells[6].SetCellValue(data[i]?.Value4 == null ? 0 : (double)data[i]?.Value4);
                    rowCur.Cells[7].SetCellValue(data[i]?.Value5 == null ? 0 : (double)data[i]?.Value5);
                    rowCur.Cells[8].SetCellValue(data[i]?.Value6 == null ? 0 : (double)data[i]?.Value6);
                    rowCur.Cells[9].SetCellValue(data[i]?.Value7 == null ? 0 : (double)data[i]?.Value7);
                    rowCur.Cells[10].SetCellValue(data[i]?.Value8 == null ? 0 : (double)data[i]?.Value8);
                }
                templateWorkbook.Write(outFileStream);
            }
            catch (Exception ex)
            {
                this.State = false;
                this.ErrorMessage = "Có lỗi xẩy ra trong quá trình tạo file excel!";
                this.Exception = ex;
            }
        }
    }
}
