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
    public class PhienBanService : GenericService<T_MD_PHIEN_BAN, PhienBanRepo>
    {
        public PhienBanService() : base()
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

        public IList<RevenueReportModel> GetDataDoanhThu(int year, string phienBan, string kichBan, string hangHangKhong)
        {
            try
            {
                var data = new List<RevenueReportModel>();
                var lstHangHangKhong = UnitOfWork.Repository<HangHangKhongRepo>().GetAll().ToList();
                var sanBayGroup = UnitOfWork.Repository<NhomSanBayRepo>().GetAll().ToList();
                lstHangHangKhong = string.IsNullOrEmpty(hangHangKhong) ? lstHangHangKhong : lstHangHangKhong.Where(x => x.CODE == hangHangKhong).ToList();

                var dataHeaderDoanhThu = UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == kichBan && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                if (dataHeaderDoanhThu.Count() == 0)
                {
                    return new List<RevenueReportModel>();
                }

                var dataInHeader = UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => x.TIME_YEAR == year && dataHeaderDoanhThu.Contains(x.TEMPLATE_CODE)).ToList();

                var dataDetails = dataInHeader.Where(x => x.KHOAN_MUC_DOANH_THU_CODE == "2001" || x.KHOAN_MUC_DOANH_THU_CODE == "2002").ToList();

                int countGroup = sanBayGroup.Count();
                int order = 0;

                data.Add(new RevenueReportModel
                {
                    Name = "TỔNG CỘNG",
                    Value1 = dataDetails.Sum(x => x.VALUE_JAN) ?? 0,
                    Value2 = dataDetails.Sum(x => x.VALUE_FEB) ?? 0,
                    Value3 = dataDetails.Sum(x => x.VALUE_MAR) ?? 0,
                    Value4 = dataDetails.Sum(x => x.VALUE_APR) ?? 0,
                    Value5 = dataDetails.Sum(x => x.VALUE_MAY) ?? 0,
                    Value6 = dataDetails.Sum(x => x.VALUE_JUN) ?? 0,
                    Value7 = dataDetails.Sum(x => x.VALUE_JUL) ?? 0,
                    Value8 = dataDetails.Sum(x => x.VALUE_AUG) ?? 0,
                    Value9 = dataDetails.Sum(x => x.VALUE_SEP) ?? 0,
                    Value10 = dataDetails.Sum(x => x.VALUE_OCT) ?? 0,
                    Value11 = dataDetails.Sum(x => x.VALUE_NOV) ?? 0,
                    Value12 = dataDetails.Sum(x => x.VALUE_SEP) ?? 0,
                    ValueSumYear = dataDetails.Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                    IsBold = true,
                    Order = -1,
                    Level = 0
                });

                foreach (var hhk in lstHangHangKhong)
                {
                    data.Add(new RevenueReportModel
                    {
                        Name = hhk.NAME,
                        Value1 = dataDetails.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE).Sum(x => x.VALUE_JAN) ?? 0,
                        Value2 = dataDetails.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE).Sum(x => x.VALUE_FEB) ?? 0,
                        Value3 = dataDetails.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE).Sum(x => x.VALUE_MAR) ?? 0,
                        Value4 = dataDetails.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE).Sum(x => x.VALUE_APR) ?? 0,
                        Value5 = dataDetails.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE).Sum(x => x.VALUE_MAY) ?? 0,
                        Value6 = dataDetails.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE).Sum(x => x.VALUE_JUN) ?? 0,
                        Value7 = dataDetails.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE).Sum(x => x.VALUE_JUL) ?? 0,
                        Value8 = dataDetails.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE).Sum(x => x.VALUE_AUG) ?? 0,
                        Value9 = dataDetails.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE).Sum(x => x.VALUE_SEP) ?? 0,
                        Value10 = dataDetails.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE).Sum(x => x.VALUE_OCT) ?? 0,
                        Value11 = dataDetails.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE).Sum(x => x.VALUE_NOV) ?? 0,
                        Value12 = dataDetails.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE).Sum(x => x.VALUE_SEP) ?? 0,
                        ValueSumYear = dataDetails.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE).Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        IsBold = true,
                        Order = order,
                        Level = 0
                    });
                    data.Add(new RevenueReportModel
                    {
                        Name = "Nội địa",
                        Value1 = dataDetails.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_JAN) ?? 0,
                        Value2 = dataDetails.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_FEB) ?? 0,
                        Value3 = dataDetails.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_MAR) ?? 0,
                        Value4 = dataDetails.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_APR) ?? 0,
                        Value5 = dataDetails.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_MAY) ?? 0,
                        Value6 = dataDetails.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_JUN) ?? 0,
                        Value7 = dataDetails.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_JUL) ?? 0,
                        Value8 = dataDetails.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_AUG) ?? 0,
                        Value9 = dataDetails.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_SEP) ?? 0,
                        Value10 = dataDetails.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_OCT) ?? 0,
                        Value11 = dataDetails.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_NOV) ?? 0,
                        Value12 = dataDetails.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_SEP) ?? 0,
                        ValueSumYear = dataDetails.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        IsBold = true,
                        Order = order + 1,
                        Level = 1
                    });

                    for (var i = 0; i < sanBayGroup.Count(); i++)
                    {
                        data.Add(new RevenueReportModel
                        {
                            Name = sanBayGroup[i].TEXT,
                            Value1 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_JAN) ?? 0,
                            Value2 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_FEB) ?? 0,
                            Value3 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_MAR) ?? 0,
                            Value4 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_APR) ?? 0,
                            Value5 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_MAY) ?? 0,
                            Value6 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_JUN) ?? 0,
                            Value7 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_JUL) ?? 0,
                            Value8 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_AUG) ?? 0,
                            Value9 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_SEP) ?? 0,
                            Value10 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_OCT) ?? 0,
                            Value11 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_NOV) ?? 0,
                            Value12 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_SEP) ?? 0,
                            ValueSumYear = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                            Order = order + 2 + i,
                            Level = 2
                        });
                    }


                    data.Add(new RevenueReportModel
                    {
                        Name = "Quốc tế",
                        Value1 = dataDetails.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_JAN) ?? 0,
                        Value2 = dataDetails.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_FEB) ?? 0,
                        Value3 = dataDetails.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_MAR) ?? 0,
                        Value4 = dataDetails.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_APR) ?? 0,
                        Value5 = dataDetails.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_MAY) ?? 0,
                        Value6 = dataDetails.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_JUN) ?? 0,
                        Value7 = dataDetails.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_JUL) ?? 0,
                        Value8 = dataDetails.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_AUG) ?? 0,
                        Value9 = dataDetails.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_SEP) ?? 0,
                        Value10 = dataDetails.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_OCT) ?? 0,
                        Value11 = dataDetails.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_NOV) ?? 0,
                        Value12 = dataDetails.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_SEP) ?? 0,
                        ValueSumYear = dataDetails.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        IsBold = true,
                        Order = order + 2 + countGroup,
                        Level = 1
                    });

                    for (var i = 0; i < sanBayGroup.Count(); i++)
                    {
                        data.Add(new RevenueReportModel
                        {
                            Name = sanBayGroup[i].TEXT,
                            Value1 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_JAN) ?? 0,
                            Value2 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_FEB) ?? 0,
                            Value3 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_MAR) ?? 0,
                            Value4 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_APR) ?? 0,
                            Value5 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_MAY) ?? 0,
                            Value6 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_JUN) ?? 0,
                            Value7 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_JUL) ?? 0,
                            Value8 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_AUG) ?? 0,
                            Value9 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_SEP) ?? 0,
                            Value10 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_OCT) ?? 0,
                            Value11 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_NOV) ?? 0,
                            Value12 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_SEP) ?? 0,
                            ValueSumYear = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == hhk.CODE && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                            Order = order + 8 + i,
                            Level = 2
                        });
                    }
                    order += 3 + countGroup * 2;
                }
                return data;
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                this.State = false;
                this.Exception = ex;
                return new List<RevenueReportModel>();
            }
        }

        public IList<SynthesisReportModel> GetData(int year, string phienBan)
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
            var value_3_1 = (from x in UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "C" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachSanLuongDataRepo>().GetAll() on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_3_2 = (from x in UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "TB" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachSanLuongDataRepo>().GetAll() on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_3_3 = (from x in UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "T" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachSanLuongDataRepo>().GetAll() on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            data.Add(new SynthesisReportModel
            {
                Stt = "II",
                Name = "Sản lượng",
                Order = 3,
                IsBold = true,
                Value1 = value_3_1 ?? 0,
                Value2 = value_3_2 ?? 0,
                Value3 = value_3_3 ?? 0,
            });
            #endregion

            #region Cung ứng cho VNA Group
            var value_4_1 = (from x in UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "C" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.SanLuongProfitCenter.HangHangKhong.IS_VNA == true).ToList() on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_4_2 = (from x in UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "TB" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.SanLuongProfitCenter.HangHangKhong.IS_VNA == true).ToList() on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_4_3 = (from x in UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "T" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.SanLuongProfitCenter.HangHangKhong.IS_VNA == true).ToList() on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);

            data.Add(new SynthesisReportModel
            {
                Stt = "1",
                Name = "Cung ứng cho VNA Group",
                Order = 4,
                Value1 = value_4_1 ?? 0,
                Value2 = value_4_2 ?? 0,
                Value3 = value_4_3 ?? 0,
            });
            #endregion

            #region Cung ứng cho VNA
            var value_5_1 = (from x in UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "C" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.SanLuongProfitCenter.HANG_HANG_KHONG_CODE == "VN").ToList() on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_5_2 = (from x in UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "TB" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.SanLuongProfitCenter.HANG_HANG_KHONG_CODE == "VN").ToList() on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_5_3 = (from x in UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "T" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.SanLuongProfitCenter.HANG_HANG_KHONG_CODE == "VN").ToList() on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            data.Add(new SynthesisReportModel
            {
                Name = "Cung ứng cho VNA",
                Order = 5,
                Value1 = value_5_1 ?? 0,
                Value2 = value_5_2 ?? 0,
                Value3 = value_5_3 ?? 0,
            });
            #endregion


            var value_6_1 = (from x in UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "C" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.SanLuongProfitCenter.HangHangKhong.IS_VNA == true && x.SanLuongProfitCenter.HANG_HANG_KHONG_CODE != "VN").ToList() on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_6_2 = (from x in UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "TB" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.SanLuongProfitCenter.HangHangKhong.IS_VNA == true && x.SanLuongProfitCenter.HANG_HANG_KHONG_CODE != "VN").ToList() on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_6_3 = (from x in UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "T" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.SanLuongProfitCenter.HangHangKhong.IS_VNA == true && x.SanLuongProfitCenter.HANG_HANG_KHONG_CODE != "VN").ToList() on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            data.Add(new SynthesisReportModel
            {
                Name = "Cung ứng cho các hãng hàng không khác trong VNA Group",
                Order = 6,
                Value1 = value_6_1 ?? 0,
                Value2 = value_6_2 ?? 0,
                Value3 = value_6_3 ?? 0,
            });

            var value_7_1 = (from x in UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "C" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.SanLuongProfitCenter.HangHangKhong.IS_VNA == false).ToList() on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_7_2 = (from x in UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "TB" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.SanLuongProfitCenter.HangHangKhong.IS_VNA == false).ToList() on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_7_3 = (from x in UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "T" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.SanLuongProfitCenter.HangHangKhong.IS_VNA == false).ToList() on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);

            data.Add(new SynthesisReportModel
            {
                Stt = "2",
                Name = "Cung ứng cho đối tác khác (*)",
                UnitName = "Tr.đ",
                Order = 7,
                Value1 = value_7_1 ?? 0,
                Value2 = value_7_2 ?? 0,
                Value3 = value_7_3 ?? 0,
            });


            var value_9_1 = (from x in UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "C" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => x.KHOAN_MUC_DOANH_THU_CODE != "10010" && x.KHOAN_MUC_DOANH_THU_CODE != "10020") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_9_2 = (from x in UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "TB" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => x.KHOAN_MUC_DOANH_THU_CODE != "10010" && x.KHOAN_MUC_DOANH_THU_CODE != "10020") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_9_3 = (from x in UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "T" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                             join y in UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => x.KHOAN_MUC_DOANH_THU_CODE != "10010" && x.KHOAN_MUC_DOANH_THU_CODE != "10020") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                             select y).Sum(x => x.VALUE_SUM_YEAR);
            data.Add(new SynthesisReportModel
            {
                Stt = "1",
                Name = "Doanh thu từ hoạt động SXKD",
                UnitName = "Tr.đ",
                Order = 9,
                Value1 = value_9_1 ?? 0,
                Value2 = value_9_2 ?? 0,
                Value3 = value_9_3 ?? 0,
            });
            data.Add(new SynthesisReportModel
            {
                Name = " - Doanh thu cung ứng cho VNA ",
                UnitName = "Tr.đ",
                Order = 10,
            });

            var value_11_1 = (from x in UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "C" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => x.KHOAN_MUC_DOANH_THU_CODE != "10010" && x.KHOAN_MUC_DOANH_THU_CODE != "10020" && x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == "VN") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_11_2 = (from x in UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "TB" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => x.KHOAN_MUC_DOANH_THU_CODE != "10010" && x.KHOAN_MUC_DOANH_THU_CODE != "10020" && x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == "VN") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_11_3 = (from x in UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "T" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => x.KHOAN_MUC_DOANH_THU_CODE != "10010" && x.KHOAN_MUC_DOANH_THU_CODE != "10020" && x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == "VN") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.VALUE_SUM_YEAR);

            data.Add(new SynthesisReportModel
            {
                Name = "+ Doanh thu VNA",
                UnitName = "Tr.đ",
                Order = 11,
                Value1 = value_11_1 ?? 0,
                Value2 = value_11_2 ?? 0,
                Value3 = value_11_3 ?? 0,
            });
            data.Add(new SynthesisReportModel
            {
                Name = "Trong đó: CK/Giảm giá",
                Order = 12,
            });


            var value_13_1 = (from x in UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "C" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => x.KHOAN_MUC_DOANH_THU_CODE != "10010" && x.KHOAN_MUC_DOANH_THU_CODE != "10020" && x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE != "VN" && x.DoanhThuProfitCenter.HangHangKhong.IS_VNA == true) on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_13_2 = (from x in UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "TB" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => x.KHOAN_MUC_DOANH_THU_CODE != "10010" && x.KHOAN_MUC_DOANH_THU_CODE != "10020" && x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE != "VN" && x.DoanhThuProfitCenter.HangHangKhong.IS_VNA == true) on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_13_3 = (from x in UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "T" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => x.KHOAN_MUC_DOANH_THU_CODE != "10010" && x.KHOAN_MUC_DOANH_THU_CODE != "10020" && x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE != "VN" && x.DoanhThuProfitCenter.HangHangKhong.IS_VNA == true) on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.VALUE_SUM_YEAR);

            data.Add(new SynthesisReportModel
            {
                Name = "+ Doanh thu hãng HK trong VNA group",
                UnitName = "Tr.đ",
                Order = 13,
                Value1 = value_13_1 ?? 0,
                Value2 = value_13_2 ?? 0,
                Value3 = value_13_3 ?? 0,
            });
            data.Add(new SynthesisReportModel
            {
                Name = "Trong đó: CK/Giảm giá",
                Order = 14,
            });

            var value_15_1 = (from x in UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "C" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => x.KHOAN_MUC_DOANH_THU_CODE != "10010" && x.KHOAN_MUC_DOANH_THU_CODE != "10020" && x.DoanhThuProfitCenter.HangHangKhong.IS_VNA == false) on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_15_2 = (from x in UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "TB" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => x.KHOAN_MUC_DOANH_THU_CODE != "10010" && x.KHOAN_MUC_DOANH_THU_CODE != "10020" && x.DoanhThuProfitCenter.HangHangKhong.IS_VNA == false) on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.VALUE_SUM_YEAR);
            var value_15_3 = (from x in UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "T" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => x.KHOAN_MUC_DOANH_THU_CODE != "10010" && x.KHOAN_MUC_DOANH_THU_CODE != "10020" && x.DoanhThuProfitCenter.HangHangKhong.IS_VNA == false) on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.VALUE_SUM_YEAR);

            data.Add(new SynthesisReportModel
            {
                Name = " - Doanh thu cung ứng cho đối tác khác (*)",
                UnitName = "Tr.đ",
                Order = 15,
                Value1 = value_15_1 ?? 0,
                Value2 = value_15_2 ?? 0,
                Value3 = value_15_3 ?? 0,
            });
            data.Add(new SynthesisReportModel
            {
                Name = "Trong đó: CK/Giảm giá",
                Order = 16,
            });

            #region - Chênh lệch tỷ giá

            var value18_2 = dataKeHoachTaiChinh.FirstOrDefault(x => x.ElementCode == "U0130" && x.Screen == "KE_HOACH_TAI_CHINH_2").Value;
            data.Add(new SynthesisReportModel
            {
                Name = " - Chênh lệch tỷ giá",
                UnitName = "Tr.đ",
                Order = 18,
                Value2 = value18_2 ?? 0,
            });
            #endregion

            #region - Doanh thu HĐ tài chính khác
            var value19_2 = dataKeHoachTaiChinh.FirstOrDefault(x => x.ElementCode == "U0132" && x.Screen == "KE_HOACH_TAI_CHINH_2").Value;
            data.Add(new SynthesisReportModel
            {
                Name = " - Doanh thu HĐ tài chính khác",
                UnitName = "Tr.đ",
                Order = 19,
                Value2 = value19_2 ?? 0,
            });
            #endregion

            #region Doanh thu từ hoạt động tài chính
            var value_17_2 = value19_2 + value18_2;
            data.Add(new SynthesisReportModel
            {
                Stt = "2",
                Name = "Doanh thu từ hoạt động tài chính",
                UnitName = "Tr.đ",
                Order = 17,
                Value2 = value_17_2 ?? 0,
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
            var value_8_2 = value_9_2 + value_17_2;
            data.Add(new SynthesisReportModel
            {
                Stt = "III",
                Name = "Tổng doanh thu và thu nhập khác",
                UnitName = "Tr.đ",
                Order = 8,
                IsBold = true,
                Value2 = value_8_2 ?? 0,
            });
            #endregion

            #region - Chi phí nhân công
            var value_23_1 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "C" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "300010101" || x.KHOAN_MUC_HANG_HOA_CODE == "300010102") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            var value_23_2 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "TB" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "300010101" || x.KHOAN_MUC_HANG_HOA_CODE == "300010102") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            var value_23_3 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "T" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "300010101" || x.KHOAN_MUC_HANG_HOA_CODE == "300010102") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            data.Add(new SynthesisReportModel
            {
                Name = " - Chi phí nhân công",
                UnitName = "Tr.đ",
                Order = 23,
                Value1 = value_23_1 ?? 0,
                Value2 = value_23_2 ?? 0,
                Value3 = value_23_3 ?? 0,
            });
            #endregion


            data.Add(new SynthesisReportModel
            {
                Name = "Trong đó:",
                Order = 24,
            });

            #region + Quỹ lương
            var value_25_1 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "C" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "300010101") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            var value_25_2 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "TB" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "300010101") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            var value_25_3 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "T" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "300010101") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            data.Add(new SynthesisReportModel
            {
                Name = " + Quỹ lương",
                UnitName = "Tr.đ",
                Order = 25,
                Value1 = value_25_1 ?? 0,
                Value2 = value_25_2 ?? 0,
                Value3 = value_25_3 ?? 0,
            });
            #endregion

            #region + Các khoản đóng góp (BHXH, BHYT, BHTN, KPCĐ)
            var value_26_1 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "C" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "300010102") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            var value_26_2 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "TB" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "300010102") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            var value_26_3 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "T" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "300010102") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            data.Add(new SynthesisReportModel
            {
                Name = " + Các khoản đóng góp (BHXH, BHYT, BHTN, KPCĐ)",
                UnitName = "Tr.đ",
                Order = 26,
                Value1 = value_26_1 ?? 0,
                Value2 = value_26_2 ?? 0,
                Value3 = value_26_3 ?? 0,
            });
            #endregion

            #region  - Chi phí nguyên vật liệu, vật tư, vốn hàng
            var value_27_1 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "C" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "3000102") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            var value_27_2 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "TB" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "3000102") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            var value_27_3 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "T" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "3000102") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            data.Add(new SynthesisReportModel
            {
                Name = " - Chi phí nguyên vật liệu, vật tư, vốn hàng",
                UnitName = "Tr.đ",
                Order = 27,
                Value1 = value_27_1 ?? 0,
                Value2 = value_27_2 ?? 0,
                Value3 = value_27_3 ?? 0,
            });
            #endregion

            #region - Khấu hao tài sản cố định
            var value_28_1 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "C" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "3000103") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            var value_28_2 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "TB" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "3000103") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            var value_28_3 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "T" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "3000103") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            data.Add(new SynthesisReportModel
            {
                Name = "- Khấu hao tài sản cố định",
                UnitName = "Tr.đ",
                Order = 28,
                Value1 = value_28_1 ?? 0,
                Value2 = value_28_2 ?? 0,
                Value3 = value_28_3 ?? 0,
            });
            #endregion

            #region - Chi phí bảo dưỡng sửa chữa tài sản
            var value_29_1 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "C" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "3000104") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            var value_29_2 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "TB" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "3000104") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            var value_29_3 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "T" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "3000104") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            data.Add(new SynthesisReportModel
            {
                Name = "- Chi phí bảo dưỡng sửa chữa tài sản",
                UnitName = "Tr.đ",
                Order = 29,
                Value1 = value_29_1 ?? 0,
                Value2 = value_29_2 ?? 0,
                Value3 = value_29_3 ?? 0,
            });
            #endregion

            #region - Chi phí dụng cụ sản xuất
            var value_30_1 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "C" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "3000105") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            var value_30_2 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "TB" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "3000105") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            var value_30_3 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "T" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "3000105") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            data.Add(new SynthesisReportModel
            {
                Name = " - Chi phí dụng cụ sản xuất",
                UnitName = "Tr.đ",
                Order = 30,
                Value1 = value_30_1 ?? 0,
                Value2 = value_30_2 ?? 0,
                Value3 = value_30_3 ?? 0,
            });
            #endregion

            #region - Chi phí dịch vụ mua ngoài (*)
            var value_31_1 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "C" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "3000106") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            var value_31_2 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "TB" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "3000106") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            var value_31_3 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "T" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "3000106") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            data.Add(new SynthesisReportModel
            {
                Name = " - Chi phí dịch vụ mua ngoài (*)",
                UnitName = "Tr.đ",
                Order = 31,
                Value1 = value_31_1 ?? 0,
                Value2 = value_31_2 ?? 0,
                Value3 = value_31_3 ?? 0,
            });
            #endregion

            #region - Chi phí khác bằng tiền (*)
            var value_32_1 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "C" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "3000107") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            var value_32_2 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "TB" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "3000107") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            var value_32_3 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "T" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "3000107") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            data.Add(new SynthesisReportModel
            {
                Name = "- Chi phí khác bằng tiền (*)",
                UnitName = "Tr.đ",
                Order = 32,
                Value1 = value_32_1 ?? 0,
                Value2 = value_32_2 ?? 0,
                Value3 = value_32_3 ?? 0,
            });
            #endregion

            #region - Dự phòng trợ cấp mất việc
            var value_33_1 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "C" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "3000108") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            var value_33_2 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "TB" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "3000108") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            var value_33_3 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "T" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "3000108") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            data.Add(new SynthesisReportModel
            {
                Name = "- Dự phòng trợ cấp mất việc",
                UnitName = "Tr.đ",
                Order = 33,
                Value1 = value_33_1 ?? 0,
                Value2 = value_33_2 ?? 0,
                Value3 = value_33_3 ?? 0,
            });
            #endregion

            #region - Thuế, phí lệ phí
            var value_34_1 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "C" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "3000109") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            var value_34_2 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "TB" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "3000109") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            var value_34_3 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "T" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "3000109") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            data.Add(new SynthesisReportModel
            {
                Name = "- Thuế, phí lệ phí",
                UnitName = "Tr.đ",
                Order = 34,
                Value1 = value_34_1 ?? 0,
                Value2 = value_34_2 ?? 0,
                Value3 = value_34_3 ?? 0,
            });
            #endregion

            #region - Chi phí dự phòng
            var value_35_1 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "C" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "3000110") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            var value_35_2 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "TB" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "3000110") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            var value_35_3 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "T" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "3000110") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            data.Add(new SynthesisReportModel
            {
                Name = " - Chi phí dự phòng",
                UnitName = "Tr.đ",
                Order = 35,
                Value1 = value_35_1 ?? 0,
                Value2 = value_35_2 ?? 0,
                Value3 = value_35_3 ?? 0,
            });
            #endregion

            #region Chi phí tài chính
            var value_36_2 = dataKeHoachTaiChinh.FirstOrDefault(x => x.ElementCode == "U0138" && x.Screen == "KE_HOACH_TAI_CHINH_2").Value;
            data.Add(new SynthesisReportModel
            {
                Stt = "2",
                Name = "Chi phí tài chính",
                UnitName = "Tr.đ",
                Order = 36,
                Value2 = value_36_2 ?? 0,
            });
            #endregion

            #region Chi phí khác
            var value_37_1 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "C" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "30003") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            var value_37_2 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "TB" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "30003") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            var value_37_3 = (from x in UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "T" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_HANG_HOA_CODE == "30003") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.AMOUNT);
            data.Add(new SynthesisReportModel
            {
                Stt = "3",
                Name = "Chi phí khác",
                UnitName = "Tr.đ",
                Order = 37,
                Value1 = value_37_1 ?? 0,
                Value2 = value_37_2 ?? 0,
                Value3 = value_37_3 ?? 0,
            });
            #endregion

            data.Add(new SynthesisReportModel
            {
                Stt = "V",
                Name = "Lợi nhuận",
                Order = 38,
                IsBold = true,
            });

            #region Chi phí sản xuất kinh doanh
            var value_22_1 = value_23_1 + value_27_1 + value_28_1 + value_29_1 + value_30_1 + value_31_1 + value_32_1 + value_33_1 + value_34_1 + value_35_1;
            var value_22_2 = value_23_2 + value_27_2 + value_28_2 + value_29_2 + value_30_2 + value_31_2 + value_32_2 + value_33_2 + value_34_2 + value_35_2;
            var value_22_3 = value_23_3 + value_27_3 + value_28_3 + value_29_3 + value_30_3 + value_31_3 + value_32_3 + value_33_3 + value_34_3 + value_35_3;
            data.Add(new SynthesisReportModel
            {
                Stt = "1",
                Name = "Chi phí sản xuất kinh doanh",
                UnitName = "Tr.đ",
                Order = 22,
                Value1 = value_22_1 ?? 0,
                Value2 = value_22_2 ?? 0,
                Value3 = value_22_3 ?? 0,
            });
            #endregion

            #region Tổng chi phí
            var value_21_1 = value_22_1 + value_37_1;
            var value_21_2 = value_22_2 + value_36_2 + value_37_2;
            var value_21_3 = value_22_3 + value_37_3;
            data.Add(new SynthesisReportModel
            {
                Stt = "IV",
                Name = "Tổng chi phí",
                UnitName = "Tr.đ",
                Order = 21,
                IsBold = true,
                Value1 = value_21_1 ?? 0,
                Value2 = value_21_2 ?? 0,
                Value3 = value_21_3 ?? 0,
            });
            #endregion

            #region Tổng LN kế toán trước thuế
            var value_39_1 = value_21_1;
            var value_39_2 = value_8_2 - value_21_2;
            var value_39_3 = value_21_3;
            data.Add(new SynthesisReportModel
            {
                Stt = "1",
                Name = "Tổng LN kế toán trước thuế",
                UnitName = "Tr.đ",
                Order = 39,
                Value1 = value_39_1 ?? 0,
                Value2 = value_39_2 ?? 0,
                Value3 = value_39_3 ?? 0,
            });
            #endregion

            #region Trong đó: Lợi nhuận từ HĐ SXKD
            var value_40_1 = value_9_1 - (value_23_1 + value_27_1 + value_28_1 + value_29_1 + value_30_1 + value_31_1 + value_32_1 + value_33_1 + value_34_1 + value_35_1);
            var value_40_2 = value_9_2 - (value_23_2 + value_27_2 + value_28_2 + value_29_2 + value_30_2 + value_31_2 + value_32_2 + value_33_2 + value_34_2 + value_35_2);
            var value_40_3 = value_9_3 - (value_23_3 + value_27_3 + value_28_3 + value_29_3 + value_30_3 + value_31_3 + value_32_3 + value_33_3 + value_34_3 + value_35_3);
            data.Add(new SynthesisReportModel
            {
                Name = "Trong đó: Lợi nhuận từ HĐ SXKD",
                Order = 40,
                Value1 = value_40_1 ?? 0,
                Value2 = value_40_2 ?? 0,
                Value3 = value_40_3 ?? 0,
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
                Stt = "3",
                Name = "Lợi nhuận chia về TCTHK",
                UnitName = "Tr.đ",
                Order = 42,
            });
            data.Add(new SynthesisReportModel
            {
                Stt = "VI",
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
                Stt = "VII",
                Name = "Vốn đầu tư của chủ sở hữu",
                Order = 46,
                IsBold = true,
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
                Stt = "VIII",
                Name = "Tỷ suất LN thực hiện/Vốn CSH BQ",
                UnitName = "%",
                Order = 50,
                IsBold = true,
            });

            #region - Giá trị khối lượng công việc hoàn thành
            var value_t1_53_1 = (from x in UnitOfWork.Repository<DauTuXayDungRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "C" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                                 join y in UnitOfWork.Repository<DauTuXayDungDataRepo>().Queryable().Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4011") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                                 select y).Sum(x => x.VALUE);
            var value_t2_53_1 = (from x in UnitOfWork.Repository<DauTuTrangThietBiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "C" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                                 join y in UnitOfWork.Repository<DauTuTrangThietBiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4032") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                                 select y).Sum(x => x.VALUE);
            var value_t1_53_2 = (from x in UnitOfWork.Repository<DauTuXayDungRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "TB" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                                 join y in UnitOfWork.Repository<DauTuXayDungDataRepo>().Queryable().Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4011") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                                 select y).Sum(x => x.VALUE);
            var value_t2_53_2 = (from x in UnitOfWork.Repository<DauTuTrangThietBiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "TB" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                                 join y in UnitOfWork.Repository<DauTuTrangThietBiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4032") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                                 select y).Sum(x => x.VALUE);
            var value_t1_53_3 = (from x in UnitOfWork.Repository<DauTuXayDungRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "T" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                                 join y in UnitOfWork.Repository<DauTuXayDungDataRepo>().Queryable().Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4011") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                                 select y).Sum(x => x.VALUE);
            var value_t2_53_3 = (from x in UnitOfWork.Repository<DauTuTrangThietBiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "T" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                                 join y in UnitOfWork.Repository<DauTuTrangThietBiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4032") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                                 select y).Sum(x => x.VALUE);
            var value_53_1 = value_t1_53_1 + value_t2_53_1;
            var value_53_2 = value_t1_53_2 + value_t2_53_2;
            var value_53_3 = value_t1_53_3 + value_t2_53_3;
            data.Add(new SynthesisReportModel
            {
                Name = " - Giá trị khối lượng công việc hoàn thành ",
                UnitName = "Tr.đ",
                Order = 53,
                Value1 = value_53_1 ?? 0,
                Value2 = value_53_2 ?? 0,
                Value3 = value_53_3 ?? 0,
            });
            #endregion

            #region - Giá trị giải ngân
            var value_t1_54_1 = (from x in UnitOfWork.Repository<DauTuXayDungRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "C" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                                 join y in UnitOfWork.Repository<DauTuXayDungDataRepo>().Queryable().Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4021") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                                 select y).Sum(x => x.VALUE);
            var value_t2_54_1 = (from x in UnitOfWork.Repository<DauTuTrangThietBiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "C" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                                 join y in UnitOfWork.Repository<DauTuTrangThietBiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4021") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                                 select y).Sum(x => x.VALUE);
            var value_t1_54_2 = (from x in UnitOfWork.Repository<DauTuXayDungRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "TB" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                                 join y in UnitOfWork.Repository<DauTuXayDungDataRepo>().Queryable().Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4021") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                                 select y).Sum(x => x.VALUE);
            var value_t2_54_2 = (from x in UnitOfWork.Repository<DauTuTrangThietBiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "TB" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                                 join y in UnitOfWork.Repository<DauTuTrangThietBiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4021") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                                 select y).Sum(x => x.VALUE);
            var value_t1_54_3 = (from x in UnitOfWork.Repository<DauTuXayDungRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "C" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                                 join y in UnitOfWork.Repository<DauTuXayDungDataRepo>().Queryable().Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4021") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                                 select y).Sum(x => x.VALUE);
            var value_t2_54_3 = (from x in UnitOfWork.Repository<DauTuTrangThietBiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "C" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                                 join y in UnitOfWork.Repository<DauTuTrangThietBiDataRepo>().Queryable().Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4021") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                                 select y).Sum(x => x.VALUE);
            var value_54_1 = value_t1_54_1 + value_t2_54_1;
            var value_54_2 = value_t1_54_2 + value_t2_54_2;
            var value_54_3 = value_t1_54_3 + value_t2_54_3;
            data.Add(new SynthesisReportModel
            {
                Name = "- Giá trị giải ngân ",
                UnitName = "Tr.đ",
                Order = 54,
                Value1 = value_54_1 ?? 0,
                Value2 = value_54_2 ?? 0,
                Value3 = value_54_3 ?? 0,
            });
            #endregion

            #region Đầu tư XDCB và TTB
            var value_52_1 = value_54_1 + value_53_1;
            var value_52_2 = value_54_2 + value_53_2;
            var value_52_3 = value_54_3 + value_53_3;
            data.Add(new SynthesisReportModel
            {
                Stt = "1",
                Name = "Đầu tư XDCB và TTB",
                Order = 52,
                Value1 = value_52_1 ?? 0,
                Value2 = value_52_2 ?? 0,
                Value3 = value_52_3 ?? 0,
            });
            #endregion

            #region Đầu tư vốn vào DN khác
            var value_55_1 = (from x in UnitOfWork.Repository<DauTuNgoaiDoanhNghiepRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "C" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<DauTuNgoaiDoanhNghiepDataRepo>().Queryable().Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.VALUE_3);
            var value_55_2 = (from x in UnitOfWork.Repository<DauTuNgoaiDoanhNghiepRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "TB" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<DauTuNgoaiDoanhNghiepDataRepo>().Queryable().Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.VALUE_3);
            var value_55_3 = (from x in UnitOfWork.Repository<DauTuNgoaiDoanhNghiepRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == "T" && x.STATUS == Approve_Status.DaPheDuyet).ToList()
                              join y in UnitOfWork.Repository<DauTuNgoaiDoanhNghiepDataRepo>().Queryable().Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101") on x.TEMPLATE_CODE equals y.TEMPLATE_CODE
                              select y).Sum(x => x.VALUE_3);
            data.Add(new SynthesisReportModel
            {
                Stt = "2",
                Name = "Đầu tư vốn vào DN khác",
                UnitName = "Tr.đ",
                Order = 55,
                Value1 = value_55_1 ?? 0,
                Value2 = value_55_2 ?? 0,
                Value3 = value_55_3 ?? 0,
            });
            #endregion

            #region Kế hoạch đầu tư
            var value_51_1 = value_52_1 + value_55_1;
            var value_51_2 = value_52_2 + value_55_2;
            var value_51_3 = value_52_3 + value_55_3;
            data.Add(new SynthesisReportModel
            {
                Stt = "IX",
                Name = "Kế hoạch đầu tư",
                Order = 51,
                IsBold = true,
                Value1 = value_51_1 ?? 0,
                Value2 = value_51_2 ?? 0,
                Value3 = value_51_3 ?? 0,
            });
            #endregion

            return data;
        }

        internal void ExportExcel(ref MemoryStream outFileStream, string path, int year, string kichBan)
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
