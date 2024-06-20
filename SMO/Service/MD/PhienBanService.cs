using iTextSharp.text;
using Microsoft.Ajax.Utilities;
using Microsoft.Office.Interop.Excel;
using NHibernate.Criterion;
using NHibernate.Mapping;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using SMO.Core.Entities;
using SMO.Core.Entities.BP.DAU_TU_TRANG_THIET_BI;
using SMO.Core.Entities.BP.DAU_TU_XAY_DUNG;
using SMO.Core.Entities.BP.KE_HOACH_CHI_PHI;
using SMO.Core.Entities.BP.KE_HOACH_DOANH_THU;
using SMO.Core.Entities.BP.KE_HOACH_SAN_LUONG;
using SMO.Core.Entities.BP.SUA_CHUA_LON;
using SMO.Core.Entities.BP.SUA_CHUA_THUONG_XUYEN;
using SMO.Core.Entities.MD;
using SMO.Models;
using SMO.Repository.Implement.BP.DAU_TU_NGOAI_DOANH_NGHIEP;
using SMO.Repository.Implement.BP.DAU_TU_TRANG_THIET_BI;
using SMO.Repository.Implement.BP.DAU_TU_XAY_DUNG;
using SMO.Repository.Implement.BP.KE_HOACH_CHI_PHI;
using SMO.Repository.Implement.BP.KE_HOACH_DOANH_THU;
using SMO.Repository.Implement.BP.KE_HOACH_SAN_LUONG;
using SMO.Repository.Implement.BP.SUA_CHUA_LON;
using SMO.Repository.Implement.BP.SUA_CHUA_THUONG_XUYEN;
using SMO.Repository.Implement.MD;
using SMO.Service.BP.KE_HOACH_DOANH_THU;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using static iTextSharp.text.pdf.AcroFields;

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
                var lstHangHangKhong = UnitOfWork.Repository<HangHangKhongRepo>().GetAll().GroupBy(x => x.GROUP_ITEM).Select(x => x.First()).Where(x => !string.IsNullOrEmpty(x.GROUP_ITEM)).ToList();
                var sanBayGroup = UnitOfWork.Repository<NhomSanBayRepo>().GetAll().ToList();
                lstHangHangKhong = string.IsNullOrEmpty(hangHangKhong) ? lstHangHangKhong : lstHangHangKhong.Where(x => x.CODE == hangHangKhong).ToList();

                var dataHeaderDoanhThu = UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == kichBan && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                if (dataHeaderDoanhThu.Count() == 0)
                {
                    return new List<RevenueReportModel>();
                }

                var dataInHeader = UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => x.TIME_YEAR == year && dataHeaderDoanhThu.Contains(x.TEMPLATE_CODE)).ToList();

                var dataDetails = dataInHeader.Where(x => x.KHOAN_MUC_DOANH_THU_CODE == "2001" || x.KHOAN_MUC_DOANH_THU_CODE == "2002").ToList();
                dataDetails = dataDetails.Where(x => !string.IsNullOrEmpty(x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM)).ToList();

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
                        Code = hhk.GROUP_ITEM,
                        Name = hhk.GROUP_ITEM,
                        Value1 = dataDetails.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_JAN) ?? 0,
                        Value2 = dataDetails.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_FEB) ?? 0,
                        Value3 = dataDetails.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_MAR) ?? 0,
                        Value4 = dataDetails.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_APR) ?? 0,
                        Value5 = dataDetails.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_MAY) ?? 0,
                        Value6 = dataDetails.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_JUN) ?? 0,
                        Value7 = dataDetails.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_JUL) ?? 0,
                        Value8 = dataDetails.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_AUG) ?? 0,
                        Value9 = dataDetails.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_SEP) ?? 0,
                        Value10 = dataDetails.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_OCT) ?? 0,
                        Value11 = dataDetails.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_NOV) ?? 0,
                        Value12 = dataDetails.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_SEP) ?? 0,
                        ValueSumYear = dataDetails.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        IsBold = true,
                        Parent = "-1",
                        Order = order,
                        Level = 0
                    });
                    data.Add(new RevenueReportModel
                    {
                        Name = "Nội địa",
                        Value1 = dataDetails.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_JAN) ?? 0,
                        Value2 = dataDetails.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_FEB) ?? 0,
                        Value3 = dataDetails.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_MAR) ?? 0,
                        Value4 = dataDetails.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_APR) ?? 0,
                        Value5 = dataDetails.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_MAY) ?? 0,
                        Value6 = dataDetails.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_JUN) ?? 0,
                        Value7 = dataDetails.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_JUL) ?? 0,
                        Value8 = dataDetails.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_AUG) ?? 0,
                        Value9 = dataDetails.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_SEP) ?? 0,
                        Value10 = dataDetails.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_OCT) ?? 0,
                        Value11 = dataDetails.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_NOV) ?? 0,
                        Value12 = dataDetails.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_SEP) ?? 0,
                        ValueSumYear = dataDetails.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        IsBold = true,
                        Parent = order.ToString(),
                        Order = order + 1,
                        Level = 1
                    });

                    for (var i = 0; i < sanBayGroup.Count(); i++)
                    {
                        data.Add(new RevenueReportModel
                        {
                            Name = sanBayGroup[i].TEXT,
                            Value1 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_JAN) ?? 0,
                            Value2 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_FEB) ?? 0,
                            Value3 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_MAR) ?? 0,
                            Value4 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_APR) ?? 0,
                            Value5 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_MAY) ?? 0,
                            Value6 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_JUN) ?? 0,
                            Value7 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_JUL) ?? 0,
                            Value8 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_AUG) ?? 0,
                            Value9 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_SEP) ?? 0,
                            Value10 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_OCT) ?? 0,
                            Value11 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_NOV) ?? 0,
                            Value12 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_SEP) ?? 0,
                            ValueSumYear = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2001").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                            Order = order + 2 + i,
                            Parent = (order + 1).ToString(),
                            Level = 2
                        });
                    }


                    data.Add(new RevenueReportModel
                    {
                        Name = "Quốc tế",
                        Value1 = dataDetails.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_JAN) ?? 0,
                        Value2 = dataDetails.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_FEB) ?? 0,
                        Value3 = dataDetails.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_MAR) ?? 0,
                        Value4 = dataDetails.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_APR) ?? 0,
                        Value5 = dataDetails.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_MAY) ?? 0,
                        Value6 = dataDetails.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_JUN) ?? 0,
                        Value7 = dataDetails.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_JUL) ?? 0,
                        Value8 = dataDetails.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_AUG) ?? 0,
                        Value9 = dataDetails.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_SEP) ?? 0,
                        Value10 = dataDetails.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_OCT) ?? 0,
                        Value11 = dataDetails.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_NOV) ?? 0,
                        Value12 = dataDetails.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_SEP) ?? 0,
                        ValueSumYear = dataDetails.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        IsBold = true,
                        Order = order + 2 + countGroup,
                        Parent = order.ToString(),
                        Level = 1
                    });

                    for (var i = 0; i < sanBayGroup.Count(); i++)
                    {
                        data.Add(new RevenueReportModel
                        {
                            Name = sanBayGroup[i].TEXT,
                            Value1 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_JAN) ?? 0,
                            Value2 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_FEB) ?? 0,
                            Value3 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_MAR) ?? 0,
                            Value4 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_APR) ?? 0,
                            Value5 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_MAY) ?? 0,
                            Value6 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_JUN) ?? 0,
                            Value7 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_JUL) ?? 0,
                            Value8 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_AUG) ?? 0,
                            Value9 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_SEP) ?? 0,
                            Value10 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_OCT) ?? 0,
                            Value11 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_NOV) ?? 0,
                            Value12 = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_SEP) ?? 0,
                            ValueSumYear = dataDetails.Where(x => x.DoanhThuProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_DOANH_THU_CODE == "2002").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                            Order = order + 8 + i,
                            Parent = (order + 2 + countGroup).ToString(),
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

        public RevenueByFeeReportModel GetDataSanLuong(int year, string phienBan, string kichBan, string hangHangKhong)
        {
            try
            {
                var data = new RevenueByFeeReportModel();
                var lstHangHangKhong = UnitOfWork.Repository<HangHangKhongRepo>().GetAll().GroupBy(x => x.GROUP_ITEM).Select(x => x.First()).Where(x => !string.IsNullOrEmpty(x.GROUP_ITEM)).ToList();
                var sanBayGroup = UnitOfWork.Repository<NhomSanBayRepo>().GetAll().ToList();
                lstHangHangKhong = string.IsNullOrEmpty(hangHangKhong) ? lstHangHangKhong : lstHangHangKhong.Where(x => x.CODE == hangHangKhong).ToList();

                var dataHeaderDoanhThu = UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == kichBan && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                if (dataHeaderDoanhThu.Count() == 0)
                {
                    return new RevenueByFeeReportModel();
                }

                var dataInHeader = UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.TIME_YEAR == year && dataHeaderDoanhThu.Contains(x.TEMPLATE_CODE)).ToList();

                var dataDetails = dataInHeader.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010" || x.KHOAN_MUC_SAN_LUONG_CODE == "10020").ToList();

                int countGroup = sanBayGroup.Count();
                int order = 0;


                #region Tab1
                data.Tab1.Add(new RevenueReportModel
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
                    Level = 0,
                    Id = "SUM",
                });

                foreach (var hhk in lstHangHangKhong)
                {
                    data.Tab1.Add(new RevenueReportModel
                    {
                        Name = hhk.GROUP_ITEM,
                        Value1 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_JAN) ?? 0,
                        Value2 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_FEB) ?? 0,
                        Value3 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_MAR) ?? 0,
                        Value4 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_APR) ?? 0,
                        Value5 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_MAY) ?? 0,
                        Value6 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_JUN) ?? 0,
                        Value7 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_JUL) ?? 0,
                        Value8 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_AUG) ?? 0,
                        Value9 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_SEP) ?? 0,
                        Value10 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_OCT) ?? 0,
                        Value11 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_NOV) ?? 0,
                        Value12 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_SEP) ?? 0,
                        ValueSumYear = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        IsBold = true,
                        Order = order,
                        Id = hhk.GROUP_ITEM,
                        Parent = "SUM",
                        Level = 1
                    });
                    data.Tab1.Add(new RevenueReportModel
                    {
                        Name = "Nội địa",
                        Value1 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_JAN) ?? 0,
                        Value2 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_FEB) ?? 0,
                        Value3 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_MAR) ?? 0,
                        Value4 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_APR) ?? 0,
                        Value5 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_MAY) ?? 0,
                        Value6 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_JUN) ?? 0,
                        Value7 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_JUL) ?? 0,
                        Value8 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_AUG) ?? 0,
                        Value9 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_SEP) ?? 0,
                        Value10 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_OCT) ?? 0,
                        Value11 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_NOV) ?? 0,
                        Value12 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_SEP) ?? 0,
                        ValueSumYear = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        IsBold = true,
                        Order = order + 1,
                        Parent = hhk.GROUP_ITEM,
                        Level = 2,
                        Id = hhk.GROUP_ITEM + "ND",
                    });

                    data.Tab1.Add(new RevenueReportModel
                    {
                        Name = "Qua xe",
                        IsBold = true,
                        Order = order + 2,
                        Parent = hhk.GROUP_ITEM + "ND",
                        Level = 2,
                        Id = hhk.GROUP_ITEM + "ND" + "X",
                        Value1 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_JAN) ?? 0,
                        Value2 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_FEB) ?? 0,
                        Value3 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_MAR) ?? 0,
                        Value4 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_APR) ?? 0,
                        Value5 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_MAY) ?? 0,
                        Value6 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_JUN) ?? 0,
                        Value7 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_JUL) ?? 0,
                        Value8 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_AUG) ?? 0,
                        Value9 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_SEP) ?? 0,
                        Value10 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_OCT) ?? 0,
                        Value11 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_NOV) ?? 0,
                        Value12 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_SEP) ?? 0,
                        ValueSumYear = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_SUM_YEAR) ?? 0,

                    });

                    for (var i = 0; i < sanBayGroup.Count(); i++)
                    {
                        data.Tab1.Add(new RevenueReportModel
                        {
                            Name = sanBayGroup[i].TEXT,
                            Value1 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_JAN) ?? 0,
                            Value2 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_FEB) ?? 0,
                            Value3 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_MAR) ?? 0,
                            Value4 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_APR) ?? 0,
                            Value5 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_MAY) ?? 0,
                            Value6 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_JUN) ?? 0,
                            Value7 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_JUL) ?? 0,
                            Value8 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_AUG) ?? 0,
                            Value9 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_SEP) ?? 0,
                            Value10 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_OCT) ?? 0,
                            Value11 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_NOV) ?? 0,
                            Value12 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_SEP) ?? 0,
                            ValueSumYear = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                            Order = order + 3 + i,
                            Parent = hhk.GROUP_ITEM + "ND" + "X",
                            Level = 3,
                            Id = hhk.GROUP_ITEM + "ND" + "X" + sanBayGroup[i].CODE,
                        });
                    }

                    data.Tab1.Add(new RevenueReportModel
                    {
                        Name = "Qua FHS",
                        IsBold = true,
                        Order = order + 2,
                        Parent = hhk.GROUP_ITEM + "ND",
                        Level = 2,
                        Id = hhk.GROUP_ITEM + "ND" + "FHS",
                        Value1 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP")  && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_JAN) ?? 0,
                        Value2 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP")  && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_FEB) ?? 0,
                        Value3 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP")  && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_MAR) ?? 0,
                        Value4 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP")  && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_APR) ?? 0,
                        Value5 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP")  && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_MAY) ?? 0,
                        Value6 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP")  && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_JUN) ?? 0,
                        Value7 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP")  && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_JUL) ?? 0,
                        Value8 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP")  && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_AUG) ?? 0,
                        Value9 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP")  && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_SEP) ?? 0,
                        Value10 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP")  && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_OCT) ?? 0,
                        Value11 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP")  && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_NOV) ?? 0,
                        Value12 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP")  && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_SEP) ?? 0,
                        ValueSumYear = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP")  && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_SUM_YEAR) ?? 0,

                    });
                    data.Tab1.Add(new RevenueReportModel
                    {
                        Name = "NBA",
                        Order = order + 2,
                        Parent = hhk.GROUP_ITEM + "ND" + "FHS",
                        Level = 2,
                        Id = hhk.GROUP_ITEM + "ND" + "FHS" + "NBA",
                        Value1 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" ) && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_JAN) ?? 0,
                        Value2 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" ) && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_FEB) ?? 0,
                        Value3 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" ) && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_MAR) ?? 0,
                        Value4 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" ) && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_APR) ?? 0,
                        Value5 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" ) && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_MAY) ?? 0,
                        Value6 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" ) && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_JUN) ?? 0,
                        Value7 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" ) && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_JUL) ?? 0,
                        Value8 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" ) && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_AUG) ?? 0,
                        Value9 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" ) && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_SEP) ?? 0,
                        Value10 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" ) && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_OCT) ?? 0,
                        Value11 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" ) && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_NOV) ?? 0,
                        Value12 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" ) && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_SEP) ?? 0,
                        ValueSumYear = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" ) && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_SUM_YEAR) ?? 0,

                    });
                    data.Tab1.Add(new RevenueReportModel
                    {
                        Name = "TNS",
                        Order = order + 2,
                        Parent = hhk.GROUP_ITEM + "ND" + "FHS",
                        Level = 2,
                        Id = hhk.GROUP_ITEM + "ND" + "FHS" + "TNS",
                        Value1 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_JAN) ?? 0,
                        Value2 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_FEB) ?? 0,
                        Value3 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_MAR) ?? 0,
                        Value4 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_APR) ?? 0,
                        Value5 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_MAY) ?? 0,
                        Value6 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_JUN) ?? 0,
                        Value7 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_JUL) ?? 0,
                        Value8 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_AUG) ?? 0,
                        Value9 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_SEP) ?? 0,
                        Value10 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_OCT) ?? 0,
                        Value11 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_NOV) ?? 0,
                        Value12 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_SEP) ?? 0,
                        ValueSumYear = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_SUM_YEAR) ?? 0,

                    });


                    data.Tab1.Add(new RevenueReportModel
                    {
                        Name = "Quốc tế",
                        Value1 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_JAN) ?? 0,
                        Value2 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_FEB) ?? 0,
                        Value3 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_MAR) ?? 0,
                        Value4 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_APR) ?? 0,
                        Value5 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_MAY) ?? 0,
                        Value6 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_JUN) ?? 0,
                        Value7 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_JUL) ?? 0,
                        Value8 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_AUG) ?? 0,
                        Value9 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_SEP) ?? 0,
                        Value10 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_OCT) ?? 0,
                        Value11 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_NOV) ?? 0,
                        Value12 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_SEP) ?? 0,
                        ValueSumYear = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        IsBold = true,
                        Order = order + 1,
                        Parent = hhk.GROUP_ITEM,
                        Level = 2,
                        Id = hhk.GROUP_ITEM + "QT",
                    });

                    data.Tab1.Add(new RevenueReportModel
                    {
                        Name = "Qua xe",
                        IsBold = true,
                        Order = order + 2,
                        Parent = hhk.GROUP_ITEM + "QT",
                        Level = 2,
                        Id = hhk.GROUP_ITEM + "QT" + "X",
                        Value1 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_JAN) ?? 0,
                        Value2 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_FEB) ?? 0,
                        Value3 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_MAR) ?? 0,
                        Value4 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_APR) ?? 0,
                        Value5 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_MAY) ?? 0,
                        Value6 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_JUN) ?? 0,
                        Value7 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_JUL) ?? 0,
                        Value8 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_AUG) ?? 0,
                        Value9 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_SEP) ?? 0,
                        Value10 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_OCT) ?? 0,
                        Value11 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_NOV) ?? 0,
                        Value12 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_SEP) ?? 0,
                        ValueSumYear = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_SUM_YEAR) ?? 0,

                    });

                    for (var i = 0; i < sanBayGroup.Count(); i++)
                    {
                        data.Tab1.Add(new RevenueReportModel
                        {
                            Name = sanBayGroup[i].TEXT,
                            Value1 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_JAN) ?? 0,
                            Value2 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_FEB) ?? 0,
                            Value3 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_MAR) ?? 0,
                            Value4 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_APR) ?? 0,
                            Value5 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_MAY) ?? 0,
                            Value6 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_JUN) ?? 0,
                            Value7 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_JUL) ?? 0,
                            Value8 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_AUG) ?? 0,
                            Value9 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_SEP) ?? 0,
                            Value10 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_OCT) ?? 0,
                            Value11 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_NOV) ?? 0,
                            Value12 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_SEP) ?? 0,
                            ValueSumYear = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP" && x.SanLuongProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                            Order = order + 3 + i,
                            Parent = hhk.GROUP_ITEM + "QT" + "X",
                            Level = 3,
                            Id = hhk.GROUP_ITEM + "QT" + "X" + sanBayGroup[i].CODE,
                        });
                    }

                    data.Tab1.Add(new RevenueReportModel
                    {
                        Name = "Qua FHS",
                        IsBold = true,
                        Order = order + 2,
                        Parent = hhk.GROUP_ITEM + "QT",
                        Level = 2,
                        Id = hhk.GROUP_ITEM + "QT" + "FHS",
                        Value1 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_JAN) ?? 0,
                        Value2 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_FEB) ?? 0,
                        Value3 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_MAR) ?? 0,
                        Value4 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_APR) ?? 0,
                        Value5 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_MAY) ?? 0,
                        Value6 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_JUN) ?? 0,
                        Value7 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_JUL) ?? 0,
                        Value8 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_AUG) ?? 0,
                        Value9 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_SEP) ?? 0,
                        Value10 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_OCT) ?? 0,
                        Value11 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_NOV) ?? 0,
                        Value12 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_SEP) ?? 0,
                        ValueSumYear = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_SUM_YEAR) ?? 0,

                    });
                    data.Tab1.Add(new RevenueReportModel
                    {
                        Name = "NBA",
                        Order = order + 2,
                        Parent = hhk.GROUP_ITEM + "QT" + "FHS",
                        Level = 2,
                        Id = hhk.GROUP_ITEM + "QT" + "FHS" + "NBA",
                        Value1 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_JAN) ?? 0,
                        Value2 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_FEB) ?? 0,
                        Value3 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_MAR) ?? 0,
                        Value4 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_APR) ?? 0,
                        Value5 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_MAY) ?? 0,
                        Value6 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_JUN) ?? 0,
                        Value7 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_JUL) ?? 0,
                        Value8 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_AUG) ?? 0,
                        Value9 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_SEP) ?? 0,
                        Value10 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_OCT) ?? 0,
                        Value11 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_NOV) ?? 0,
                        Value12 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_SEP) ?? 0,
                        ValueSumYear = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_SUM_YEAR) ?? 0,

                    });
                    data.Tab1.Add(new RevenueReportModel
                    {
                        Name = "TNS",
                        Order = order + 2,
                        Parent = hhk.GROUP_ITEM + "QT" + "FHS",
                        Level = 2,
                        Id = hhk.GROUP_ITEM + "QT" + "FHS" + "TNS",
                        Value1 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_JAN) ?? 0,
                        Value2 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_FEB) ?? 0,
                        Value3 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_MAR) ?? 0,
                        Value4 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_APR) ?? 0,
                        Value5 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_MAY) ?? 0,
                        Value6 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_JUN) ?? 0,
                        Value7 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_JUL) ?? 0,
                        Value8 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_AUG) ?? 0,
                        Value9 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_SEP) ?? 0,
                        Value10 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_OCT) ?? 0,
                        Value11 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_NOV) ?? 0,
                        Value12 = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_SEP) ?? 0,
                        ValueSumYear = dataDetails.Where(x => (x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP") && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_SUM_YEAR) ?? 0,

                    });
                    order += 3 + countGroup * 2;
                }
                #endregion

                #region Tab2

                int orderTab2 = 0;
                data.Tab2.Add(new RevenueReportModel
                {
                    Name = "TỔNG CỘNG",
                    Value1 = dataDetails.Where(x => x.SanLuongProfitCenter.SanBay.AREA_CODE == "MB").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                    Value2 = dataDetails.Where(x => x.SanLuongProfitCenter.SanBay.AREA_CODE == "MT").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                    Value3 = dataDetails.Where(x => x.SanLuongProfitCenter.SanBay.AREA_CODE == "MN").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                    ValueSumYear = dataDetails.Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                    IsBold = true,
                    Order = -1,
                    Level = 0,
                    Id = "SUM",
                });

                foreach (var hhk in lstHangHangKhong)
                {
                    data.Tab2.Add(new RevenueReportModel
                    {
                        Name = hhk.GROUP_ITEM,
                        Value1 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.SanLuongProfitCenter.SanBay.AREA_CODE == "MB").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        Value2 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.SanLuongProfitCenter.SanBay.AREA_CODE == "MT").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        Value3 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.SanLuongProfitCenter.SanBay.AREA_CODE == "MN").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        ValueSumYear = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        IsBold = true,
                        Order = orderTab2,
                        Parent = "SUM",
                        Level = 1,
                        Id = hhk.GROUP_ITEM,
                    });
                    data.Tab2.Add(new RevenueReportModel
                    {
                        Name = "Nội địa",
                        Value1 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.SanBay.AREA_CODE == "MB").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        Value2 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.SanBay.AREA_CODE == "MT").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        Value3 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.SanBay.AREA_CODE == "MN").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        ValueSumYear = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        IsBold = true,
                        Order = orderTab2 + 1,
                        Parent = hhk.GROUP_ITEM,
                        Level = 2,
                        Id = hhk.GROUP_ITEM + "ND",
                    });

                    data.Tab2.Add(new RevenueReportModel
                    {
                        Name = "Qua xe",
                        Value1 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.SanBay.AREA_CODE == "MB").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        Value2 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.SanBay.AREA_CODE == "MT").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        Value3 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.SanBay.AREA_CODE == "MN").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        ValueSumYear = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        IsBold = true,
                        Order = orderTab2 + 1,
                        Parent = hhk.GROUP_ITEM + "ND",
                        Level = 2,
                        Id = hhk.GROUP_ITEM + "ND" + "X",
                    });

                    for (var i = 0; i < sanBayGroup.Count(); i++)
                    {
                        data.Tab2.Add(new RevenueReportModel
                        {
                            Name = sanBayGroup[i].TEXT,
                            Value1 = dataDetails.Where(x => x.SanLuongProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.SanBay.AREA_CODE == "MB").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                            Value2 = dataDetails.Where(x => x.SanLuongProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.SanBay.AREA_CODE == "MT").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                            Value3 = dataDetails.Where(x => x.SanLuongProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.SanBay.AREA_CODE == "MN").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                            ValueSumYear = dataDetails.Where(x => x.SanLuongProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                            Order = orderTab2 + 2 + i,
                            Parent = hhk.GROUP_ITEM + "ND" + "X",
                            Level = 3,
                            Id = hhk.GROUP_ITEM + "ND" + "X" + sanBayGroup[i].TEXT,
                        });
                    }

                    data.Tab2.Add(new RevenueReportModel
                    {
                        Name = "Qua FHS",
                        Value1 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.SanBay.AREA_CODE == "MB").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        Value2 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.SanBay.AREA_CODE == "MT").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        Value3 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.SanBay.AREA_CODE == "MN").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        ValueSumYear = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        IsBold = true,
                        Order = orderTab2 + 1,
                        Parent = hhk.GROUP_ITEM + "ND",
                        Level = 2,
                        Id = hhk.GROUP_ITEM + "ND" + "FHS",
                    });

                    data.Tab2.Add(new RevenueReportModel
                    {
                        Name = "NBA",
                        Value1 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.SanBay.AREA_CODE == "MB").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        Value2 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.SanBay.AREA_CODE == "MT").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        Value3 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.SanBay.AREA_CODE == "MN").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        ValueSumYear = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        Order = orderTab2 + 1,
                        Parent = hhk.GROUP_ITEM + "ND" + "FHS",
                        Level = 2,
                        Id = hhk.GROUP_ITEM + "ND" + "FHS" + "NBA",
                    });

                    data.Tab2.Add(new RevenueReportModel
                    {
                        Name = "TNS",
                        Value1 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.SanBay.AREA_CODE == "MB").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        Value2 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.SanBay.AREA_CODE == "MT").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        Value3 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.SanBay.AREA_CODE == "MN").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        ValueSumYear = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                       Order = orderTab2 + 1,
                        Parent = hhk.GROUP_ITEM + "ND" + "FHS",
                        Level = 2,
                        Id = hhk.GROUP_ITEM + "ND" + "FHS" + "TNS",
                    });
                }
                #endregion

                #region Tab3

                var orderTab3 = 0;
                var lstAirport = UnitOfWork.Repository<SanBayRepo>().Queryable().Where(x => x.OTHER_PM_CODE != null && x.OTHER_PM_CODE != "").ToList();
                data.Tab3.Add(new RevenueReportModel
                {
                    Name = "TỔNG CỘNG",
                    Value1 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == "VN").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                    Value2 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == "BL").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                    Value3 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == "0V").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                    Value4 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == "VJ").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                    Value5 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == "QH").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                    Value6 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == "VU").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                    Value7 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == "HKTN#").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                    Value8 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.TYPE == "ND").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                    Value9 = dataDetails.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == "HKQT").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                    Value10 = dataDetails.Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                    IsBold = true,
                    Order = -1,
                    Level = 0
                });
                foreach (var item in lstAirport)
                {
                    data.Tab3.Add(new RevenueReportModel
                    {
                        Name = item.NAME,
                        Code = item.CODE,
                        Value1 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE == item.CODE && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == "VN").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        Value2 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE == item.CODE && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == "BL").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        Value3 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE == item.CODE && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == "0V").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        Value4 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE == item.CODE && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == "VJ").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        Value5 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE == item.CODE && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == "QH").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        Value6 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE == item.CODE && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == "VU").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        Value7 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE == item.CODE && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == "HKTN#").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        Value8 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE == item.CODE && x.SanLuongProfitCenter.HangHangKhong.TYPE == "ND").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        Value9 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE == item.CODE && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == "HKQT").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        Value10 = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE == item.CODE).Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        Order = orderTab3,
                        Level = 0,
                        Parent = "-1",
                    });
                    orderTab3 += 1;
                }
                #endregion

                #region Tab4

                data.TabSL_TN.Add(new RevenueReportModelSL_Tra_Nap
                {
                    Name = "TỔNG CỘNG",
                    Value1ND = dataDetails.Where(x=>x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_JAN) ?? 0,
                    Value2ND = dataDetails.Where(x=>x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_FEB) ?? 0,
                    Value3ND = dataDetails.Where(x=>x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_MAR) ?? 0,
                    Value4ND = dataDetails.Where(x=>x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_APR) ?? 0,
                    Value5ND = dataDetails.Where(x=>x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_MAY) ?? 0,
                    Value6ND = dataDetails.Where(x=>x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_JUN) ?? 0,
                    Value7ND = dataDetails.Where(x=>x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_JUL) ?? 0,
                    Value8ND = dataDetails.Where(x=>x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_AUG) ?? 0,
                    Value9ND = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_SEP) ?? 0,
                    Value10ND = dataDetails.Where(x=>x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_OCT) ?? 0,
                    Value11ND = dataDetails.Where(x=>x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_NOV) ?? 0,
                    Value12ND = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_SEP) ?? 0,
                    ValueSumYearND = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                    Value1QT = dataDetails.Where(x=>x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_JAN) ?? 0,
                    Value2QT = dataDetails.Where(x=>x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_FEB) ?? 0,
                    Value3QT = dataDetails.Where(x=>x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_MAR) ?? 0,
                    Value4QT = dataDetails.Where(x=>x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_APR) ?? 0,
                    Value5QT = dataDetails.Where(x=>x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_MAY) ?? 0,
                    Value6QT = dataDetails.Where(x=>x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_JUN) ?? 0,
                    Value7QT = dataDetails.Where(x=>x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_JUL) ?? 0,
                    Value8QT = dataDetails.Where(x=>x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_AUG) ?? 0,
                    Value9QT = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_SEP) ?? 0,
                    Value10QT = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_OCT) ?? 0,
                    Value11QT = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_NOV) ?? 0,
                    Value12QT = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_SEP) ?? 0,
                    ValueSumYearQT = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                    ValueSumYearAll_ND_QT = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_SUM_YEAR) + dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                    IsBold = true,
                    Order = -1,
                    Level = 0
                });
                var order_TN = 0;
                foreach (var hhk in lstHangHangKhong)
                {
                    var item = new RevenueReportModelSL_Tra_Nap
                    {
                        Code = hhk.CODE,
                        Name = hhk.GROUP_ITEM,
                        Value1ND = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM).Sum(x => x.VALUE_JAN) ?? 0,
                        Value2ND = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM).Sum(x => x.VALUE_FEB) ?? 0,
                        Value3ND = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM).Sum(x => x.VALUE_MAR) ?? 0,
                        Value4ND = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM).Sum(x => x.VALUE_APR) ?? 0,
                        Value5ND = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM).Sum(x => x.VALUE_MAY) ?? 0,
                        Value6ND = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM).Sum(x => x.VALUE_JUN) ?? 0,
                        Value7ND = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM).Sum(x => x.VALUE_JUL) ?? 0,
                        Value8ND = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM).Sum(x => x.VALUE_AUG) ?? 0,
                        Value9ND = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM).Sum(x => x.VALUE_SEP) ?? 0,
                        Value10ND = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010"&& x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM).Sum(x => x.VALUE_OCT) ?? 0,
                        Value11ND = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010"&& x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM).Sum(x => x.VALUE_NOV) ?? 0,
                        Value12ND = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_SEP) ?? 0,
                        ValueSumYearND = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        Value1QT = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10020"&& x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM).Sum(x => x.VALUE_JAN) ?? 0,
                        Value2QT = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10020"&& x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM).Sum(x => x.VALUE_FEB) ?? 0,
                        Value3QT = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10020"&& x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM).Sum(x => x.VALUE_MAR) ?? 0,
                        Value4QT = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10020"&& x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM).Sum(x => x.VALUE_APR) ?? 0,
                        Value5QT = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10020"&& x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM).Sum(x => x.VALUE_MAY) ?? 0,
                        Value6QT = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10020"&& x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM).Sum(x => x.VALUE_JUN) ?? 0,
                        Value7QT = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10020"&& x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM).Sum(x => x.VALUE_JUL) ?? 0,
                        Value8QT = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10020"&& x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM).Sum(x => x.VALUE_AUG) ?? 0,
                        Value9QT = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM).Sum(x => x.VALUE_SEP) ?? 0,
                        Value10QT = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10020"&& x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM).Sum(x => x.VALUE_OCT) ?? 0,
                        Value11QT = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10020"&& x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_NOV) ?? 0,
                        Value12QT = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM).Sum(x => x.VALUE_SEP) ?? 0,
                        ValueSumYearQT = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM).Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        ValueSumYearAll_ND_QT = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010"&& x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM).Sum(x => x.VALUE_SUM_YEAR) + dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        IsBold = true,
                        Parent = "-1",
                        Order = order_TN,
                        Level = 1
                    };
                    data.TabSL_TN.Add(item);

                    var parentOrder = order_TN;

                    foreach (var sb in lstAirport)
                    {
                        var itemChild = new RevenueReportModelSL_Tra_Nap
                        {
                            Code = sb.CODE,
                            Name = sb.CODE,
                            Value1ND = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM && x.SanLuongProfitCenter.SAN_BAY_CODE == sb.CODE).Sum(x => x.VALUE_JAN) ?? 0,
                            Value2ND = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM && x.SanLuongProfitCenter.SAN_BAY_CODE == sb.CODE).Sum(x => x.VALUE_FEB) ?? 0,
                            Value3ND = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM && x.SanLuongProfitCenter.SAN_BAY_CODE == sb.CODE).Sum(x => x.VALUE_MAR) ?? 0,
                            Value4ND = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM && x.SanLuongProfitCenter.SAN_BAY_CODE == sb.CODE).Sum(x => x.VALUE_APR) ?? 0,
                            Value5ND = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM && x.SanLuongProfitCenter.SAN_BAY_CODE == sb.CODE).Sum(x => x.VALUE_MAY) ?? 0,
                            Value6ND = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM && x.SanLuongProfitCenter.SAN_BAY_CODE == sb.CODE).Sum(x => x.VALUE_JUN) ?? 0,
                            Value7ND = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM && x.SanLuongProfitCenter.SAN_BAY_CODE == sb.CODE).Sum(x => x.VALUE_JUL) ?? 0,
                            Value8ND = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM && x.SanLuongProfitCenter.SAN_BAY_CODE == sb.CODE).Sum(x => x.VALUE_AUG) ?? 0,
                            Value9ND = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM && x.SanLuongProfitCenter.SAN_BAY_CODE == sb.CODE).Sum(x => x.VALUE_SEP) ?? 0,
                            Value10ND = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM && x.SanLuongProfitCenter.SAN_BAY_CODE == sb.CODE).Sum(x => x.VALUE_OCT) ?? 0,
                            Value11ND = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM && x.SanLuongProfitCenter.SAN_BAY_CODE == sb.CODE).Sum(x => x.VALUE_NOV) ?? 0,
                            Value12ND = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM && x.SanLuongProfitCenter.SAN_BAY_CODE == sb.CODE).Sum(x => x.VALUE_SEP) ?? 0,
                            ValueSumYearND = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM && x.SanLuongProfitCenter.SAN_BAY_CODE == sb.CODE).Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                            Value1QT = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM && x.SanLuongProfitCenter.SAN_BAY_CODE == sb.CODE).Sum(x => x.VALUE_JAN) ?? 0,
                            Value2QT = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM && x.SanLuongProfitCenter.SAN_BAY_CODE == sb.CODE).Sum(x => x.VALUE_FEB) ?? 0,
                            Value3QT = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM && x.SanLuongProfitCenter.SAN_BAY_CODE == sb.CODE).Sum(x => x.VALUE_MAR) ?? 0,
                            Value4QT = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM && x.SanLuongProfitCenter.SAN_BAY_CODE == sb.CODE).Sum(x => x.VALUE_APR) ?? 0,
                            Value5QT = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM && x.SanLuongProfitCenter.SAN_BAY_CODE == sb.CODE).Sum(x => x.VALUE_MAY) ?? 0,
                            Value6QT = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM && x.SanLuongProfitCenter.SAN_BAY_CODE == sb.CODE).Sum(x => x.VALUE_JUN) ?? 0,
                            Value7QT = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM && x.SanLuongProfitCenter.SAN_BAY_CODE == sb.CODE).Sum(x => x.VALUE_JUL) ?? 0,
                            Value8QT = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM && x.SanLuongProfitCenter.SAN_BAY_CODE == sb.CODE).Sum(x => x.VALUE_AUG) ?? 0,
                            Value9QT = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM && x.SanLuongProfitCenter.SAN_BAY_CODE == sb.CODE).Sum(x => x.VALUE_SEP) ?? 0,
                            Value10QT = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM && x.SanLuongProfitCenter.SAN_BAY_CODE == sb.CODE).Sum(x => x.VALUE_OCT) ?? 0,
                            Value11QT = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM && x.SanLuongProfitCenter.SAN_BAY_CODE == sb.CODE).Sum(x => x.VALUE_NOV) ?? 0,
                            Value12QT = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM && x.SanLuongProfitCenter.SAN_BAY_CODE == sb.CODE).Sum(x => x.VALUE_SEP) ?? 0,
                            ValueSumYearQT = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM && x.SanLuongProfitCenter.SAN_BAY_CODE == sb.CODE).Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                            ValueSumYearAll_ND_QT = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM== hhk.GROUP_ITEM && x.SanLuongProfitCenter.SAN_BAY_CODE == sb.CODE).Sum(x => x.VALUE_SUM_YEAR) + dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.SanLuongProfitCenter.SAN_BAY_CODE == sb.CODE).Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                            Order = order_TN +1,
                            Parent = parentOrder.ToString(),
                            Level = 2
                        };
                        data.TabSL_TN.Add(itemChild);
                        order_TN++;
                    }
                    order_TN++;
                }
                #endregion
                return data;
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                this.State = false;
                this.Exception = ex;
                return new RevenueByFeeReportModel();
            }
        }

        private readonly object lockObject = new object();

        public async Task<IList<SupplyReportModel>> GetDataTraNapCungUng(int year, string phienBan, string kichBan, string hangHangKhong)
        {
            try
            {
                var data = new List<SupplyReportModel>();
                var lstHangHangKhong = UnitOfWork.Repository<HangHangKhongRepo>().GetAll().GroupBy(x => x.GROUP_ITEM).Select(x => x.First()).ToList();
                var sanBayGroup = UnitOfWork.Repository<NhomSanBayRepo>().GetAll().ToList();
                sanBayGroup.RemoveAll(sanBay => sanBay.CODE == "NI-N");
                var sanBayFHS = UnitOfWork.Repository<SanBayRepo>().Queryable().Where(x => x.NHOM_SAN_BAY_CODE == "NI-N").ToList();
                lstHangHangKhong = string.IsNullOrEmpty(hangHangKhong) ? lstHangHangKhong : lstHangHangKhong.Where(x => x.CODE == hangHangKhong).ToList();
                lstHangHangKhong = lstHangHangKhong.Where(x => !string.IsNullOrEmpty(x.GROUP_ITEM)).ToList();

                var dataHeaderDoanhThu = UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == kichBan && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                if (dataHeaderDoanhThu.Count() == 0)
                {
                    return new List<SupplyReportModel>();
                }

                var dataInHeader = UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.TIME_YEAR == year && dataHeaderDoanhThu.Contains(x.TEMPLATE_CODE)).ToList();

                var dataDetails = dataInHeader.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010" || x.KHOAN_MUC_SAN_LUONG_CODE == "10020").ToList();

                int countGroup = sanBayGroup.Count();
                int countFHS = sanBayFHS.Count();
                int order = 0;
                
                #region Tab1

                var valueSum = new SupplyReportModel
                {
                    Name = "DOANH THU JET HK",
                    IsBold = true,
                    Order = -1,
                    Parent = null,
                    Level = 0
                };
                foreach (var hhk in lstHangHangKhong)
                {
                    //Hãng hàng không
                    var valueHhk = new SupplyReportModel
                    {
                        Name = hhk.GROUP_ITEM,
                        IsBold = true,
                        Order = order,
                        Parent = -1,
                        Level = 0
                    };
                    //Nội địa
                    var valueND = new SupplyReportModel
                    {
                        Name = "Nội địa",
                        IsBold = true,
                        Order = order + 1,
                        Parent = order,
                        Level = 1
                    };

                    var valueNDXe = new SupplyReportModel
                    {
                        Name = "Qua xe",
                        IsBold = true,
                        Order = order + 2,
                        Parent = order + 1,
                        Level = 2
                    };

                    var valueNDFhs = new SupplyReportModel
                    {
                        Name = "Qua FHS",
                        IsBold = true,
                        Order = order + 3 + countGroup,
                        Parent = order + 1,
                        Level = 2
                    };
                    // Quốc tế
                    var valueQT = new SupplyReportModel
                    {
                        Name = "Quốc tế",
                        IsBold = true,
                        Order = order + 4 + countGroup + countFHS,
                        Parent = order,
                        Level = 1
                    };

                    var valueQTXe = new SupplyReportModel
                    {
                        Name = "Qua xe",
                        IsBold = true,
                        Order = order + 5 + countGroup + countFHS,
                        Parent = order + 4 + countGroup + countFHS,
                        Level = 2
                    };

                    var valueQTFhs = new SupplyReportModel
                    {
                        Name = "Qua FHS",
                        IsBold = true,
                        Order = order + 6 + countGroup + countFHS + countGroup,
                        Parent = order + 4 + countGroup + countFHS,
                        Level = 2
                    };

                    // ND Qua xe
                    Task task1 = Task.Run(() =>
                    {
                        lock (lockObject)
                        {
                            //Lấy giá
                            var tnkCode = "TNK" + "-" + hhk.GROUP_ITEM;
                            // Giá thuế nhập khẩu
                            var thueTNK = UnitOfWork.Repository<SharedDataRepo>().Queryable().FirstOrDefault(x => x.CODE == tnkCode).VALUE;
                            // Giá mops
                            var pricePlat = UnitOfWork.Repository<SharedDataRepo>().Queryable().FirstOrDefault(x => x.CODE == "1").VALUE;
                            var priceTG = UnitOfWork.Repository<SharedDataRepo>().Queryable().FirstOrDefault(x => x.CODE == "2").VALUE;
                            var priceHSQD = UnitOfWork.Repository<SharedDataRepo>().Queryable().FirstOrDefault(x => x.CODE == "3").VALUE;
                            var ThueXBQ = UnitOfWork.Repository<SharedDataRepo>().Queryable().FirstOrDefault(x => x.CODE == "23").VALUE;
                            var priceMops = pricePlat * priceTG * priceHSQD;
                            var priceTNK = thueTNK * pricePlat * priceHSQD * priceTG;

                            for (var i = 0; i < sanBayGroup.Count(); i++)
                            {
                                var sanBayCode = UnitOfWork.Repository<SanBayRepo>().Queryable().FirstOrDefault(x => x.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE).CODE;
                                // Giá dịch vụ
                                decimal priceD = 0;
                                var unitPrice = UnitOfWork.Repository<UnitPricePlanRepo>().Queryable().FirstOrDefault(x => x.SAN_BAY_CODE == sanBayCode);

                                if (unitPrice != null)
                                {
                                    var groupSb = hhk.GROUP_ITEM;
                                    // giá tiền nhiên liệu
                                    priceD = groupSb == "VN" ? unitPrice.VN : groupSb == "0V" ? unitPrice.OV : groupSb == "BL" ? unitPrice.BL : groupSb == "VJ" ? unitPrice.VJ : groupSb == "QH" ? unitPrice.QH : groupSb == "VU" ? unitPrice.VU : groupSb == "HKTN#" ? unitPrice.HKTN_OTHER : (groupSb == "HKQT" && hhk.TYPE == "ND") ? unitPrice.HKNN_ND : unitPrice.HKNN_QT;
                                }
                                //Sản lượng
                                var valueSL = dataDetails.Where(x => x.SanLuongProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_SUM_YEAR) ?? 0;
                                // Đơn giá
                                var priceDG = priceMops + priceTNK + priceD;
                                var item = new SupplyReportModel
                                {
                                    Name = sanBayGroup[i].TEXT,
                                    ValueSL = valueSL,
                                    ValueDG = priceDG,
                                    ValueMOPS = priceMops,
                                    ValueTNK = priceTNK,
                                    ValueD = priceD,
                                    ValueDT = valueSL * priceDG,
                                    ValueDTMOPS = valueSL * priceMops,
                                    ValueDTTNK = valueSL * priceTNK,
                                    ValueDTD = valueSL * priceD,
                                    Order = order + 3 + i,
                                    Parent = order + 2,
                                    Level = 3
                                };
                                data.Add(item);
                                // Giá trị qua xe
                                valueNDXe.ValueSL = valueNDXe.ValueSL + item.ValueSL;
                                valueNDXe.ValueDT = valueNDXe.ValueDT + item.ValueDT;
                                valueNDXe.ValueDTMOPS = valueNDXe.ValueDTMOPS + item.ValueDTMOPS;
                                valueNDXe.ValueDTTNK = valueNDXe.ValueDTTNK + item.ValueDTTNK;
                                valueNDXe.ValueDTD = valueNDXe.ValueDTD + item.ValueDTD;
                            }

                            data.Add(valueNDXe);
                        }
                    });
                    //ND Qua FHS
                    Task task2 = Task.Run(() =>
                    {
                        lock (lockObject)
                        {
                            //Lấy giá
                            var tnkCode = "TNK" + "-" + hhk.GROUP_ITEM;
                            // Giá thuế nhập khẩu
                            var thueTNK = UnitOfWork.Repository<SharedDataRepo>().Queryable().FirstOrDefault(x => x.CODE == tnkCode).VALUE;
                            // Giá mops
                            var pricePlat = UnitOfWork.Repository<SharedDataRepo>().Queryable().FirstOrDefault(x => x.CODE == "1").VALUE;
                            var priceTG = UnitOfWork.Repository<SharedDataRepo>().Queryable().FirstOrDefault(x => x.CODE == "2").VALUE;
                            var priceHSQD = UnitOfWork.Repository<SharedDataRepo>().Queryable().FirstOrDefault(x => x.CODE == "3").VALUE;
                            var priceMops = pricePlat * priceTG * priceHSQD;
                            var priceTNK = thueTNK * pricePlat * priceHSQD * priceTG;
                            for (var i = 0; i < sanBayFHS.Count(); i++)
                            {
                                var sanBayCode = sanBayFHS[i].CODE;
                                var currencyUSD = UnitOfWork.Repository<SharedDataRepo>().Queryable().FirstOrDefault(x => x.CODE == "2").VALUE;
                                // Giá dịch vụ
                                decimal priceD = 0;
                                var unitPrice = UnitOfWork.Repository<UnitPricePlanRepo>().Queryable().FirstOrDefault(x => x.SAN_BAY_CODE == sanBayCode);

                                if (unitPrice != null)
                                {
                                    var groupSb = hhk.GROUP_ITEM;
                                    // giá tiền nhiên liệu
                                    priceD = groupSb == "VN" ? unitPrice.VN : groupSb == "0V" ? unitPrice.OV : groupSb == "BL" ? unitPrice.BL : groupSb == "VJ" ? unitPrice.VJ : groupSb == "QH" ? unitPrice.QH : groupSb == "VU" ? unitPrice.VU : groupSb == "HKTN#" ? unitPrice.HKTN_OTHER : (groupSb == "HKQT" && hhk.TYPE == "ND") ? unitPrice.HKNN_ND : unitPrice.HKNN_QT;
                                }
                                //Giá phí ngầm
                                decimal priceFhs = 0;
                                sanBayCode = sanBayCode == "NAF" ? "NBA" : sanBayCode == "TAP" ? "TNS" : sanBayCode;

                                var hangHangKhongCode = hhk.GROUP_ITEM;
                                var shareDataCode = "FHS" + "-" + sanBayCode + "-" + hangHangKhongCode;

                                var phiNgam = UnitOfWork.Repository<SharedDataRepo>().Queryable().FirstOrDefault(x => x.CODE == shareDataCode);
                                if (phiNgam != null)
                                {
                                    priceFhs = phiNgam.VALUE * currencyUSD;
                                }
                                // Đơn giá
                                var priceDG = priceMops + priceTNK + priceD + priceFhs;
                                var item = new SupplyReportModel
                                {
                                    Name = sanBayCode,
                                    ValueDG = priceDG,
                                    ValueMOPS = priceMops,
                                    ValueTNK = priceTNK,
                                    ValueD = priceD,
                                    ValueFH = priceFhs,
                                    Order = order + 4 + countGroup + i,
                                    Parent = order + 3 + countGroup,
                                    Level = 3
                                };
                                data.Add(item);
                            }
                            data.Add(valueNDFhs);
                        }
                    });
                    //QT Qua xe
                    Task task3 = Task.Run(() =>
                    {
                        lock (lockObject)
                        {
                            //Lấy giá
                            // Giá mops
                            var pricePlat = UnitOfWork.Repository<SharedDataRepo>().Queryable().FirstOrDefault(x => x.CODE == "1").VALUE;
                            var priceTG = UnitOfWork.Repository<SharedDataRepo>().Queryable().FirstOrDefault(x => x.CODE == "2").VALUE;
                            var priceHSQD = UnitOfWork.Repository<SharedDataRepo>().Queryable().FirstOrDefault(x => x.CODE == "3").VALUE;
                            var priceMops = pricePlat * priceTG * priceHSQD;
                            for (var i = 0; i < sanBayGroup.Count(); i++)
                            {
                                var sanBayCode = UnitOfWork.Repository<SanBayRepo>().Queryable().FirstOrDefault(x => x.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE).CODE;
                                // Giá dịch vụ
                                decimal priceD = 0;
                                var unitPrice = UnitOfWork.Repository<UnitPricePlanRepo>().Queryable().FirstOrDefault(x => x.SAN_BAY_CODE == sanBayCode);

                                if (unitPrice != null)
                                {
                                    var groupSb = hhk.GROUP_ITEM;
                                    // giá tiền nhiên liệu
                                    priceD = groupSb == "VN" ? unitPrice.VN : groupSb == "0V" ? unitPrice.OV : groupSb == "BL" ? unitPrice.BL : groupSb == "VJ" ? unitPrice.VJ : groupSb == "QH" ? unitPrice.QH : groupSb == "VU" ? unitPrice.VU : groupSb == "HKTN#" ? unitPrice.HKTN_OTHER : (groupSb == "HKQT" && hhk.TYPE == "ND") ? unitPrice.HKNN_ND : unitPrice.HKNN_QT;
                                }
                                //Sản lượng
                                var valueSL = dataDetails.Where(x => x.SanLuongProfitCenter.SanBay.NHOM_SAN_BAY_CODE == sanBayGroup[i].CODE && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_SUM_YEAR) ?? 0;
                                // Đơn giá
                                var priceDG = priceMops + priceD;
                                var item = new SupplyReportModel
                                {
                                    Name = sanBayGroup[i].TEXT,
                                    ValueSL = valueSL,
                                    ValueDG = priceDG,
                                    ValueMOPS = priceMops,
                                    ValueD = priceD,
                                    ValueDT = valueSL * priceDG,
                                    ValueDTMOPS = valueSL * priceMops,
                                    ValueDTD = valueSL * priceD,
                                    Order = order + 6 + countGroup + countFHS + i,
                                    Parent = order + 5 + countGroup + countFHS,
                                    Level = 3
                                };
                                data.Add(item);
                                // Giá trị qua xe
                                valueQTXe.ValueSL = valueQTXe.ValueSL + item.ValueSL;
                                valueQTXe.ValueDT = valueQTXe.ValueDT + item.ValueDT;
                                valueQTXe.ValueDTMOPS = valueQTXe.ValueDTMOPS + item.ValueDTMOPS;
                                valueQTXe.ValueDTTNK = valueQTXe.ValueDTTNK + item.ValueDTTNK;
                                valueQTXe.ValueDTD = valueQTXe.ValueDTD + item.ValueDTD;
                            }
                            data.Add(valueQTXe);
                        }
                    });
                    // QT Qua FHS
                    Task task4 = Task.Run(() =>
                    {
                        lock (lockObject)
                        {
                            //Lấy giá

                            // Giá mops
                            var pricePlat = UnitOfWork.Repository<SharedDataRepo>().Queryable().FirstOrDefault(x => x.CODE == "1").VALUE;
                            var priceTG = UnitOfWork.Repository<SharedDataRepo>().Queryable().FirstOrDefault(x => x.CODE == "2").VALUE;
                            var priceHSQD = UnitOfWork.Repository<SharedDataRepo>().Queryable().FirstOrDefault(x => x.CODE == "3").VALUE;
                            var priceMops = pricePlat * priceTG * priceHSQD;
                            for (var i = 0; i < sanBayFHS.Count(); i++)
                            {
                                var sanBayCode = sanBayFHS[i].CODE;
                                var currencyUSD = UnitOfWork.Repository<SharedDataRepo>().Queryable().FirstOrDefault(x => x.CODE == "2").VALUE;
                                // Giá dịch vụ
                                decimal priceD = 0;
                                var unitPrice = UnitOfWork.Repository<UnitPricePlanRepo>().Queryable().FirstOrDefault(x => x.SAN_BAY_CODE == sanBayCode);

                                if (unitPrice != null)
                                {
                                    var groupSb = hhk.GROUP_ITEM;
                                    // giá tiền nhiên liệu
                                    priceD = groupSb == "VN" ? unitPrice.VN : groupSb == "0V" ? unitPrice.OV : groupSb == "BL" ? unitPrice.BL : groupSb == "VJ" ? unitPrice.VJ : groupSb == "QH" ? unitPrice.QH : groupSb == "VU" ? unitPrice.VU : groupSb == "HKTN#" ? unitPrice.HKTN_OTHER : (groupSb == "HKQT" && hhk.TYPE == "ND") ? unitPrice.HKNN_ND : unitPrice.HKNN_QT;
                                }
                                var valueSL = dataDetails.Where(x=> x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.SAN_BAY_CODE == sanBayFHS[i].CODE).Sum(x => x.VALUE_SUM_YEAR) ?? 0;
                                //Giá phí ngầm
                                decimal priceFhs = 0;
                                sanBayCode = sanBayCode == "NAF" ? "NBA" : sanBayCode == "TAP" ? "TNS" : sanBayCode;

                                var hangHangKhongCode = hhk.GROUP_ITEM;
                                var shareDataCode = "FHS" + "-" + sanBayCode + "-" + hangHangKhongCode;

                                var phiNgam = UnitOfWork.Repository<SharedDataRepo>().Queryable().FirstOrDefault(x => x.CODE == shareDataCode);
                                if (phiNgam != null)
                                {
                                    priceFhs = phiNgam.VALUE * currencyUSD;
                                }
                                // Đơn giá
                                var priceDG = priceMops + priceD + priceFhs;
                                var item = new SupplyReportModel
                                {
                                    Name = sanBayCode,
                                    ValueSL = valueSL,
                                    ValueDG = priceDG,
                                    ValueMOPS = priceMops,
                                    ValueD = priceD,
                                    ValueFH = priceFhs,
                                    ValueDT = valueSL * priceDG,
                                    ValueDTMOPS = valueSL * priceMops,
                                    ValueDTD = valueSL * priceD,
                                    ValueDTFH = valueSL * priceFhs,
                                    Order = order + 7 + countGroup + countFHS + countGroup + i,
                                    Parent = order + 6 + countGroup + countFHS + countGroup,
                                    Level = 3
                                };
                                data.Add(item);
                                // gía trị phí ngầm
                                valueQTFhs.ValueSL = valueQTFhs.ValueSL + item.ValueSL;
                                valueQTFhs.ValueDT = valueQTFhs.ValueDT + item.ValueDT;
                                valueQTFhs.ValueDTMOPS = valueQTFhs.ValueDTMOPS + item.ValueDTMOPS;
                                valueQTFhs.ValueDTD = valueQTFhs.ValueDTD + item.ValueDTD;
                                valueQTFhs.ValueDTFH = valueQTFhs.ValueDTFH + item.ValueDTFH;
                            }
                            data.Add(valueQTFhs);
                        }
                    });

                    await Task.WhenAll(task1, task2, task3, task4);

                    // tổng nội địa
                    valueND.ValueSL = valueNDXe.ValueSL + valueNDFhs.ValueSL;
                    valueND.ValueDT = valueNDXe.ValueDT + valueNDFhs.ValueDT;
                    valueND.ValueDTMOPS = valueNDXe.ValueDTMOPS + valueNDFhs.ValueDTMOPS;
                    valueND.ValueDTTNK = valueNDXe.ValueDTTNK + valueNDFhs.ValueDTTNK;
                    valueND.ValueDTD = valueNDXe.ValueDTD + valueNDFhs.ValueDTD;
                    data.Add(valueND);
                    // tổng quốc tế
                    valueQT.ValueSL = valueQTXe.ValueSL + valueQTFhs.ValueSL;
                    valueQT.ValueDT = valueQTXe.ValueDT + valueQTFhs.ValueDT;
                    valueQT.ValueDTMOPS = valueQTXe.ValueDTMOPS + valueQTFhs.ValueDTMOPS;
                    valueQT.ValueDTD = valueQTXe.ValueDTD + valueQTFhs.ValueDTD;
                    valueQT.ValueDTFH = valueQTXe.ValueDTFH + valueQTFhs.ValueDTFH;
                    data.Add(valueQT);
                    // tổng hhk
                    valueHhk.ValueSL = valueND.ValueSL + valueQT.ValueSL;
                    valueHhk.ValueDT = valueND.ValueDT + valueQT.ValueDT;
                    valueHhk.ValueDTMOPS = valueND.ValueDTMOPS + valueQT.ValueDTMOPS;
                    valueHhk.ValueDTTNK = valueND.ValueDTTNK + valueQT.ValueDTTNK;
                    valueHhk.ValueDTD = valueND.ValueDTD + valueQT.ValueDTD;
                    valueHhk.ValueDTFH = valueND.ValueDTFH + valueQT.ValueDTFH;
                    data.Add(valueHhk);

                  
                    order += 7 + countGroup * 2 + countFHS * 2;
                    valueSum.ValueSL = valueSum.ValueSL + valueHhk.ValueSL;
                    valueSum.ValueDT = valueSum.ValueDT + valueHhk.ValueDT;
                    valueSum.ValueDTMOPS = valueSum.ValueDTMOPS + valueHhk.ValueDTMOPS;
                    valueSum.ValueDTTNK = valueSum.ValueDTTNK + valueHhk.ValueDTTNK;
                    valueSum.ValueDTD = valueSum.ValueDTD + valueHhk.ValueDTD;
                    valueSum.ValueDTFH = valueSum.ValueDTFH + valueHhk.ValueDTFH;

                }
                data.Add(valueSum);

                var hsbvmt = UnitOfWork.Repository<SharedDataRepo>().Get("25");
                foreach (var i in data)
                {
                    i.ValueThue = i.ValueSL * hsbvmt.VALUE;
                }

                var headerDT = UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == kichBan && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var dataDT = UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => headerDT.Contains(x.TEMPLATE_CODE)).ToList();
                data.Add(new SupplyReportModel
                {
                    Name = "DOANH THU BÁN TẠI HÀN QUỐC",
                    IsBold = true,
                    Order = data.Count() + 1,
                    Parent = null,
                    Level = 0,
                    ValueSL = dataDetails.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE == "ICN" || x.SanLuongProfitCenter.SAN_BAY_CODE == "PUS").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                    ValueDT = dataDT.Where(x => x.KHOAN_MUC_DOANH_THU_CODE == "2002" && (x.DoanhThuProfitCenter.SAN_BAY_CODE == "ICN" || x.DoanhThuProfitCenter.SAN_BAY_CODE == "PUS")).Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                });

                var discount = UnitOfWork.Repository<SharedDataRepo>().Get("24");
                data.Add(new SupplyReportModel
                {
                    Name = "GIẢM GIÁ",
                    IsBold = true,
                    Order = data.Count() + 2,
                    Parent = null,
                    Level = 0,
                    ValueThue = discount.VALUE,
                    ValueDT = (data.FirstOrDefault(x => x.Name == "VN").ValueDTD + data.FirstOrDefault(x => x.Name == "0V").ValueDTD) * discount.VALUE,
                    ValueDTD = (data.FirstOrDefault(x => x.Name == "VN").ValueDTD + data.FirstOrDefault(x => x.Name == "0V").ValueDTD) * discount.VALUE,
                });

                #endregion
                return data;
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                this.State = false;
                this.Exception = ex;
                return new List<SupplyReportModel>();
            }
        }

        public SynthesizeThePlanReportModel GetDataKeHoachTongHop(int year, string phienBan, string kichBan, string area)
        {
            try
            {
                var data = new SynthesizeThePlanReportModel();
                var lstSanBay = UnitOfWork.Repository<SanBayRepo>().Queryable().Where(x => x.OTHER_PM_CODE != null && x.OTHER_PM_CODE != "").ToList();
                lstSanBay = !string.IsNullOrEmpty(area) ? lstSanBay.Where(x => x.AREA_CODE == area).ToList() : lstSanBay;

                var lstAreas = UnitOfWork.Repository<AreaRepo>().GetAll().ToList();
                lstAreas = string.IsNullOrEmpty(area) ? lstAreas : lstAreas.Where(x => x.CODE == area).ToList();


                #region KẾ HOẠCH SẢN LƯỢNG
                var dataHeaderSanLuong = UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == kichBan && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var dataInHeader = UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.TIME_YEAR == year && dataHeaderSanLuong.Contains(x.TEMPLATE_CODE)).ToList();

                var orderArea = 1;
                foreach(var a in lstAreas)
                {
                    var dataArea = dataInHeader.Where(x => x.SanLuongProfitCenter.SanBay.AREA_CODE == a.CODE);
                    data.SanLuong.Add(new SanLuong
                    {
                        Id = Guid.NewGuid().ToString(),
                        Code = a.TEXT,
                        IsBold = true,
                        Stt = ConvertNumberOrder(orderArea),
                        Value1 = dataArea.Where(x => x.SanLuongProfitCenter.HangHangKhong.IS_VNA && x.SanLuongProfitCenter.HangHangKhong.TYPE == "ND").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        Value2 = dataArea.Where(x => !x.SanLuongProfitCenter.HangHangKhong.IS_VNA && x.SanLuongProfitCenter.HangHangKhong.TYPE == "ND").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        Value3 = dataArea.Where(x => x.SanLuongProfitCenter.HangHangKhong.TYPE == "ND").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        Value4 = dataArea.Where(x => x.SanLuongProfitCenter.HangHangKhong.TYPE == "QT").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        Value5 = dataArea.Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                    });
                    var orderSL = 1;
                    foreach (var sb in lstSanBay.Where(x => x.AREA_CODE == a.CODE))
                    {
                        var dataSb = dataInHeader.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE == sb.CODE);
                        var item = new SanLuong
                        {
                            Id = Guid.NewGuid().ToString(),
                            Code = sb.CODE,
                            Name = sb.NAME,
                            Value1 = dataSb.Where(x => x.SanLuongProfitCenter.HangHangKhong.IS_VNA && x.SanLuongProfitCenter.HangHangKhong.TYPE == "ND").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                            Value2 = dataSb.Where(x => !x.SanLuongProfitCenter.HangHangKhong.IS_VNA && x.SanLuongProfitCenter.HangHangKhong.TYPE == "ND").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                            Value3 = dataSb.Where(x => x.SanLuongProfitCenter.HangHangKhong.TYPE == "ND").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                            Value4 = dataSb.Where(x => x.SanLuongProfitCenter.HangHangKhong.TYPE == "QT").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                            Value5 = dataSb.Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                            Stt = orderSL.ToString(),
                        };
                        data.SanLuong.Add(item);
                        orderSL += 1;
                    }
                    orderArea += 1;
                }
                #endregion

                #region KẾ HOẠCH ĐẦU TƯ XDCB, TTB

                var lstProject = UnitOfWork.Repository<ProjectRepo>().Queryable().Where(x => x.YEAR == year).ToList();
                lstProject = string.IsNullOrEmpty(area) ? lstProject.ToList() : lstProject.Where(x => x.AREA_CODE == area).ToList();

                data.DauTu.Add(new DauTu
                {
                    Stt = "I",
                    Name = "Đầu tư XDCB",
                    IsBold = true
                });
                var orderXDCB = 1;
                var headerXDCB = UnitOfWork.Repository<DauTuXayDungRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == kichBan && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var dataXDCB = UnitOfWork.Repository<DauTuXayDungDataRepo>().Queryable().Where(x => headerXDCB.Contains(x.TEMPLATE_CODE)).ToList();
                foreach (var project in lstProject.Where(x => x.LOAI_HINH == "XDCB"))
                {
                    data.DauTu.Add(new DauTu
                    {
                        Stt = orderXDCB.ToString(),
                        Name = project.NAME,
                        Value1 = dataXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == project.CODE).FirstOrDefault()?.VALUE_2,
                        Value2 = dataXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == project.CODE).Sum(x => x.VALUE_1) ?? 0,
                        Value4 = dataXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == project.CODE).FirstOrDefault()?.VALUE_3,
                        Des = dataXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == project.CODE).FirstOrDefault()?.DESCRIPTION,
                        IsBold = project.TYPE == "TTB-LON" || string.IsNullOrEmpty(project.TYPE) ? true : false,
                    });
                    if (project.TYPE == "TTB-LON" || string.IsNullOrEmpty(project.TYPE))
                    {
                        data.DauTu.Add(new DauTu
                        {
                            Stt = "a",
                            Name = "Chuẩn bị đầu tư và chuẩn bị thực hiện dự án",
                            Value1 = dataXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == project.CODE && x.KHOAN_MUC_DAU_TU_CODE == "CBDT").FirstOrDefault()?.VALUE_2,
                            Value2 = dataXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == project.CODE && x.KHOAN_MUC_DAU_TU_CODE == "CBDT").Sum(x => x.VALUE_1) ?? 0,
                            Value4 = dataXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == project.CODE && x.KHOAN_MUC_DAU_TU_CODE == "CBDT").FirstOrDefault()?.VALUE_3,
                            Des = dataXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == project.CODE && x.KHOAN_MUC_DAU_TU_CODE == "CBDT").FirstOrDefault()?.DESCRIPTION,
                        });
                        data.DauTu.Add(new DauTu
                        {
                            Stt = "b",
                            Name = "Thực hiện dự án",
                            Value1 = dataXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == project.CODE && x.KHOAN_MUC_DAU_TU_CODE == "TTDT").FirstOrDefault()?.VALUE_2,
                            Value2 = dataXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == project.CODE && x.KHOAN_MUC_DAU_TU_CODE == "TTDT").Sum(x => x.VALUE_1) ?? 0,
                            Value4 = dataXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == project.CODE && x.KHOAN_MUC_DAU_TU_CODE == "TTDT").FirstOrDefault()?.VALUE_3,
                            Des = dataXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == project.CODE && x.KHOAN_MUC_DAU_TU_CODE == "TTDT").FirstOrDefault()?.DESCRIPTION,
                        });

                    }
                    orderXDCB += 1;
                }

                data.DauTu.Add(new DauTu
                {
                    Stt = "II",
                    Name = "Đầu tư trang thiết bị",
                    IsBold = true
                });
                var orderTTB = 1;
                var headerTTB = UnitOfWork.Repository<DauTuTrangThietBiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == kichBan && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var dataTTB = UnitOfWork.Repository<DauTuTrangThietBiDataRepo>().Queryable().Where(x => headerTTB.Contains(x.TEMPLATE_CODE)).ToList();
                foreach (var project in lstProject.Where(x => x.LOAI_HINH == "TTB"))
                {
                    data.DauTu.Add(new DauTu
                    {
                        Stt = orderTTB.ToString(),
                        Name = project.NAME,
                        Value1 = dataTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == project.CODE).FirstOrDefault()?.VALUE_2,
                        Value2 = dataTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == project.CODE).Sum(x => x.VALUE_5) ?? 0,
                        Value4 = dataTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == project.CODE).FirstOrDefault()?.VALUE_3,
                        Des = dataTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == project.CODE).FirstOrDefault()?.DESCRIPTION,
                        IsBold = project.TYPE == "TTB-LON" || string.IsNullOrEmpty(project.TYPE) ? true : false,
                    });
                    if (project.TYPE == "TTB-LON" || string.IsNullOrEmpty(project.TYPE))
                    {
                        data.DauTu.Add(new DauTu
                        {
                            Stt = "a",
                            Name = "Chuẩn bị đầu tư và chuẩn bị thực hiện dự án",
                            Value1 = dataTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == project.CODE && x.KHOAN_MUC_DAU_TU_CODE == "CBDT").FirstOrDefault()?.VALUE_2,
                            Value2 = dataTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == project.CODE && x.KHOAN_MUC_DAU_TU_CODE == "CBDT").Sum(x => x.VALUE_1) ?? 0,
                            Value4 = dataTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == project.CODE && x.KHOAN_MUC_DAU_TU_CODE == "CBDT").FirstOrDefault()?.VALUE_3,
                            Des = dataTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == project.CODE && x.KHOAN_MUC_DAU_TU_CODE == "CBDT").FirstOrDefault()?.DESCRIPTION,
                        });
                        data.DauTu.Add(new DauTu
                        {
                            Stt = "b",
                            Name = "Thực hiện dự án",
                            Value1 = dataTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == project.CODE && x.KHOAN_MUC_DAU_TU_CODE == "TTDT").FirstOrDefault()?.VALUE_2,
                            Value2 = dataTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == project.CODE && x.KHOAN_MUC_DAU_TU_CODE == "TTDT").Sum(x => x.VALUE_1) ?? 0,
                            Value4 = dataTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == project.CODE && x.KHOAN_MUC_DAU_TU_CODE == "TTDT").FirstOrDefault()?.VALUE_3,
                            Des = dataTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == project.CODE && x.KHOAN_MUC_DAU_TU_CODE == "TTDT").FirstOrDefault()?.DESCRIPTION,
                        });

                    }
                    orderTTB += 1;
                }
                #endregion

                #region KẾ HOẠCH SỬA CHỮA LỚN

                var headerSCL = UnitOfWork.Repository<SuaChuaLonRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == kichBan && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var dataSCL = UnitOfWork.Repository<SuaChuaLonDataRepo>().Queryable().Where(x => headerSCL.Contains(x.TEMPLATE_CODE)).ToList();

                if (string.IsNullOrEmpty(area))
                {
                    data.SuaChuaLon.AddRange(GetDataSCLByArea("MB", year, dataSCL));
                    data.SuaChuaLon.AddRange(GetDataSCLByArea("MT", year, dataSCL));
                    data.SuaChuaLon.AddRange(GetDataSCLByArea("MN", year, dataSCL));
                    data.SuaChuaLon.AddRange(GetDataSCLByArea("CQ", year, dataSCL));
                    data.SuaChuaLon.AddRange(GetDataSCLByArea("VT", year, dataSCL));
                }
                else
                {
                    data.SuaChuaLon.AddRange(GetDataSCLByArea(area, year, dataSCL));
                }

                #endregion

                #region KẾ HOẠCH CHI PHÍ
                var elements = UnitOfWork.Repository<ReportChiPhiCodeRepo>().GetAll().OrderBy(x => x.C_ORDER).ToList();
                var dataHeaderCP = UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == kichBan && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var dataCP = UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.TIME_YEAR == year && dataHeaderCP.Contains(x.TEMPLATE_CODE)).ToList();
                var firstCode = area == "CQ" ? "CQ" : area == "MB" ? "B" : area == "MT" ? "T" : area == "MN" ? "N" : area == "VT" ? "VT" : "";
                
                foreach(var i in elements)
                {
                    var item = new ChiPhi
                    {
                        Stt = i.STT,
                        name = i.GROUP_NAME,
                        valueCP = dataCP.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(firstCode + i.GROUP_1_ID + i.GROUP_2_ID))?.Sum(x => x.QUANTITY * x.PRICE) ?? 0,
                        IsBold = i.IS_BOLD,
                    };
                    data.ChiPhi.Add(item);
                }
                #endregion

                return data;
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                this.State = false;
                this.Exception = ex;
                return new SynthesizeThePlanReportModel();
            }
        }

        public List<SuaChuaLon> GetDataSCLByArea(string area,int year, List<T_BP_SUA_CHUA_LON_DATA> data)
        {
            try
            {
                var dataR = new List<SuaChuaLon>();
                if (area == "CQ") data = data.Where(x => x.ORG_CODE.Contains("100001")).ToList();
                if (area == "MB") data = data.Where(x => x.ORG_CODE.Contains("100002")).ToList();
                if (area == "MT") data = data.Where(x => x.ORG_CODE.Contains("100003")).ToList();
                if (area == "MN") data = data.Where(x => x.ORG_CODE.Contains("100004")).ToList();
                if (area == "VT") data = data.Where(x => x.ORG_CODE.Contains("100005")).ToList();
                var elementCodes = data.Select(x => x.KHOAN_MUC_SUA_CHUA_CODE).Distinct().ToList();
                var elementChild = UnitOfWork.Repository<KhoanMucSuaChuaRepo>().Queryable().Where(x => x.TIME_YEAR == year && elementCodes.Contains(x.CODE)).ToList();
                foreach (var i in elementChild.Select(x => x.PARENT_CODE).Distinct().ToList())
                {
                    var p = UnitOfWork.Repository<KhoanMucSuaChuaRepo>().Queryable().FirstOrDefault(x => x.TIME_YEAR == year && x.CODE == i);
                    if (p != null) elementChild.Add(p);
                }
                elementChild = elementChild.DistinctBy(x => x.CODE).ToList();

                var orderChild = 0;
                var orderParent = 0;
                dataR.Add(new SuaChuaLon
                {
                    IsBold = true,
                    name = area == "CQ" ? " CƠ QUAN CÔNG TY" : area == "MB" ? "CHI NHÁNH MIỀN BẮC" : area == "MT" ? "CHI NHÁNH MIỀN TRUNG" : area == "MN" ? "CHI NHÁNH MIỀN NAM" : "CHI NHÁNH VẬN TẢI",
                    valueKP = data.Sum(x => x.VALUE) ?? 0,
                });
                foreach (var i in elementChild.OrderBy(x => x.C_ORDER))
                {
                    if (i.IS_GROUP)
                    {
                        orderChild = 0;
                        orderParent += 1;
                    }
                    var d = new SuaChuaLon
                    {
                        stt = i.IS_GROUP ? ConvertNumberOrder(orderParent) : orderChild.ToString(),
                        code = i.CODE,
                        parentCode = i.PARENT_CODE,
                        IsBold = i.IS_GROUP ? true : false,
                        name = i.NAME,
                        valueKP = data.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE == i.CODE).Sum(x => x.VALUE) ?? 0,
                        valueQM = data.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE == i.CODE && !string.IsNullOrEmpty(x.QUY_MO)).FirstOrDefault()?.QUY_MO,
                        des = data.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE == i.CODE && !string.IsNullOrEmpty(x.DESCRIPTION)).FirstOrDefault()?.DESCRIPTION,
                    };
                    orderChild += 1;
                    dataR.Add(d);
                }
                foreach (var i in dataR)
                {
                    var childs = dataR.Where(x => x.parentCode == i.code);
                    if (childs.Count() != 0 || i.valueKP == 0)
                    {
                        i.valueKP = childs.Sum(x => x.valueKP);
                    }
                }
                return dataR;
            }
            catch(Exception ex)
            {
                return new List<SuaChuaLon>();
            }
        }

        public List<SuaChuaThuongXuyenReportModel> GetDataSCTXByArea(string area, int year, List<T_BP_SUA_CHUA_THUONG_XUYEN_DATA> data)
        {
            try
            {
                var dataR = new List<SuaChuaThuongXuyenReportModel>();
                if (area == "CQ") data = data.Where(x => x.ORG_CODE.Contains("100001")).ToList();
                if (area == "MB") data = data.Where(x => x.ORG_CODE.Contains("100002")).ToList();
                if (area == "MT") data = data.Where(x => x.ORG_CODE.Contains("100003")).ToList();
                if (area == "MN") data = data.Where(x => x.ORG_CODE.Contains("100004")).ToList();
                if (area == "VT") data = data.Where(x => x.ORG_CODE.Contains("100005")).ToList();
                var elementCodes = data.Select(x => x.KHOAN_MUC_SUA_CHUA_CODE).Distinct().ToList();
                var elementChild = UnitOfWork.Repository<KhoanMucSuaChuaRepo>().Queryable().Where(x => x.TIME_YEAR == year && elementCodes.Contains(x.CODE)).ToList();
                foreach (var i in elementChild.Select(x => x.PARENT_CODE).Distinct().ToList())
                {
                    var p = UnitOfWork.Repository<KhoanMucSuaChuaRepo>().Queryable().FirstOrDefault(x => x.TIME_YEAR == year && x.CODE == i);
                    if (p != null) elementChild.Add(p);
                }
                elementChild = elementChild.DistinctBy(x => x.CODE).ToList();

                var orderChild = 0;
                var orderParent = 0;
                dataR.Add(new SuaChuaThuongXuyenReportModel
                {
                    IsBold = true,
                    Name = area == "CQ" ? " CƠ QUAN CÔNG TY" : area == "MB" ? "CHI NHÁNH MIỀN BẮC" : area == "MT" ? "CHI NHÁNH MIỀN TRUNG" : area == "MN" ? "CHI NHÁNH MIỀN NAM" : "CHI NHÁNH VẬN TẢI",
                    valueGT = data.Sum(x => x.VALUE) ?? 0,
                });
                foreach (var i in elementChild.OrderBy(x => x.C_ORDER))
                {
                    if (i.IS_GROUP)
                    {
                        orderChild = 0;
                        orderParent += 1;
                    }
                    var d = new SuaChuaThuongXuyenReportModel
                    {
                        Stt = i.IS_GROUP ? ConvertNumberOrder(orderParent) : orderChild.ToString(),
                        Code = i.CODE,
                        Parent = i.PARENT_CODE,
                        IsBold = i.IS_GROUP ? true : false,
                        Name = i.NAME,
                        valueGT = data.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE == i.CODE).Sum(x => x.VALUE) ?? 0,
                        valueQM = data.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE == i.CODE && !string.IsNullOrEmpty(x.QUY_MO)).FirstOrDefault()?.QUY_MO,
                        Des = data.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE == i.CODE && !string.IsNullOrEmpty(x.DESCRIPTION)).FirstOrDefault()?.DESCRIPTION,
                    };
                    orderChild += 1;
                    dataR.Add(d);
                }
                foreach (var i in dataR)
                {
                    var childs = dataR.Where(x => x.Parent == i.Code);
                    if (childs.Count() != 0 || i.valueGT == 0)
                    {
                        i.valueGT = childs.Sum(x => x.valueGT);
                    }
                }
                return dataR;
            }
            catch (Exception ex)
            {
                return new List<SuaChuaThuongXuyenReportModel>();
            }
        }

        public string ConvertNumberOrder(decimal number)
        {
            try
            {
                string strRet = string.Empty;
                decimal _Number = number;
                Boolean _Flag = true;
                string[] ArrLama = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
                int[] ArrNumber = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
                int i = 0;
                while (_Flag)
                {
                    while (_Number >= ArrNumber[i])
                    {
                        _Number -= ArrNumber[i];
                        strRet += ArrLama[i];
                        if (_Number < 1)
                            _Flag = false;
                    }
                    i++;
                }
                return strRet;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<ReportCKPModel> GetDataCKP (int year, string phienBan, string kichBan, string area)
        {
            try
            {
                var data = new ReportCKPModel();
                var lstSanBay = UnitOfWork.Repository<SanBayRepo>().Queryable().Where(x => x.OTHER_PM_CODE != null && x.OTHER_PM_CODE != "").ToList();
                lstSanBay = !string.IsNullOrEmpty(area) ? lstSanBay.Where(x => x.AREA_CODE == area).ToList() : lstSanBay;
                Dictionary<string, string> ChiNhanh = new Dictionary<string, string>()
                {
                    {"", "" },
                    {"MB", "CNMB" },
                    {"MN", "CNMN" },
                    {"MT", "CNMT" },
                    {"VT", "CNVT" },
                    {"CQ", "CQCT" }
                };
                var sapCode = ChiNhanh[area];
                var costCenter = UnitOfWork.Repository<CostCenterRepo>().Queryable().FirstOrDefault(x => x.SAP_CODE == sapCode).CODE;
                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                Task task2 = Task.Run(() =>
                {
                    lock (lockObject)
                    {
                        #region KẾ HOẠCH ĐẦU TƯ, MUA SẮM TRANG THIẾT BỊ
                        var sapCodeDT = ChiNhanh[area];
                        var costCenterDT = UnitOfWork.Repository<CostCenterRepo>().Queryable().FirstOrDefault(x => x.SAP_CODE == sapCodeDT).CODE;

                        var dataHeaderDTXD = !string.IsNullOrEmpty(area) ? UnitOfWork.Repository<DauTuXayDungRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == kichBan && x.STATUS == "03" && x.TEMPLATE_CODE != string.Empty && x.ORG_CODE.StartsWith(costCenterDT)).Select(x => x.TEMPLATE_CODE).ToList() :
                                                                         UnitOfWork.Repository<DauTuXayDungRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == kichBan && x.STATUS == "03" && x.TEMPLATE_CODE != string.Empty).Select(x => x.TEMPLATE_CODE).ToList();
                        var dataHeaderDTTTB = !string.IsNullOrEmpty(area) ? UnitOfWork.Repository<DauTuTrangThietBiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == kichBan && x.STATUS == "03" && x.TEMPLATE_CODE != string.Empty && x.ORG_CODE.StartsWith(costCenterDT)).Select(x => x.TEMPLATE_CODE).ToList() :
                                                                         UnitOfWork.Repository<DauTuTrangThietBiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == kichBan && x.STATUS == "03" && x.TEMPLATE_CODE != string.Empty).Select(x => x.TEMPLATE_CODE).ToList();
                        if (dataHeaderDTXD.Count() + dataHeaderDTTTB.Count() == 0)
                        {
                            cancellationTokenSource.Cancel();
                        }

                        var dataInHeaderDTXD = UnitOfWork.Repository<DauTuXayDungDataRepo>().Queryable().Where(x => x.TIME_YEAR == year && dataHeaderDTXD.Contains(x.TEMPLATE_CODE)).ToList();
                        var dataInHeaderDTTTB = UnitOfWork.Repository<DauTuTrangThietBiDataRepo>().Queryable().Where(x => x.TIME_YEAR == year && dataHeaderDTTTB.Contains(x.TEMPLATE_CODE)).ToList();

                        var lstPjInDTXD = dataInHeaderDTXD.Select(x => x.DauTuXayDungProfitCenter.PROJECT_CODE).Distinct().ToList();
                        var lstPjInDTTTB = dataInHeaderDTTTB.Select(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE).Distinct().ToList();
                        var lstProject = new List<string>();
                        lstProject.AddRange(lstPjInDTXD);
                        lstProject.AddRange(lstPjInDTTTB);
                        lstProject = lstProject.Distinct().ToList();
                        var dataHeaderDTXD_DLCKP = !string.IsNullOrEmpty(area) ? UnitOfWork.Repository<DauTuXayDungRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == kichBan && x.STATUS == "03" && x.TEMPLATE_CODE != string.Empty && x.ORG_CODE.StartsWith(costCenterDT)).Select(x => x.TEMPLATE_CODE).ToList() :
                                                                        UnitOfWork.Repository<DauTuXayDungRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == kichBan && x.STATUS == "03" && x.TEMPLATE_CODE != string.Empty).Select(x => x.TEMPLATE_CODE).ToList();
                        var dataHeaderTTB_DLCKP = !string.IsNullOrEmpty(area) ? UnitOfWork.Repository<DauTuTrangThietBiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == kichBan && x.STATUS == "03" && x.TEMPLATE_CODE != string.Empty && x.ORG_CODE.StartsWith(costCenterDT)).Select(x => x.TEMPLATE_CODE).ToList() :
                                                                        UnitOfWork.Repository<DauTuTrangThietBiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == kichBan && x.STATUS == "03" && x.TEMPLATE_CODE != string.Empty).Select(x => x.TEMPLATE_CODE).ToList();

                        var dataInHeaderXD_DLCKP = UnitOfWork.Repository<DauTuXayDungDataRepo>().Queryable().Where(x => x.TIME_YEAR == year && dataHeaderDTXD_DLCKP.Contains(x.TEMPLATE_CODE)).ToList();
                        var dataInHeaderTTB_DLCKP = UnitOfWork.Repository<DauTuTrangThietBiDataRepo>().Queryable().Where(x => x.TIME_YEAR == year && dataHeaderTTB_DLCKP.Contains(x.TEMPLATE_CODE)).ToList();

                        var orderDT = 1;
                        foreach (var pj in lstProject)
                        {
                            var item = new DauTuCKP
                            {
                                Name = UnitOfWork.Repository<ProjectRepo>().Queryable().FirstOrDefault(x => x.CODE == pj).NAME,
                                Col2= dataInHeaderDTXD.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == pj && x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.VALUE) + dataInHeaderDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == pj && x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.VALUE) ?? 0,
                                Col4 = dataInHeaderXD_DLCKP.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == pj && x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH1) + dataInHeaderTTB_DLCKP.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == pj && x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH1) ?? 0,
                                Col5 = dataInHeaderXD_DLCKP.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == pj && x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH2) + dataInHeaderTTB_DLCKP.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == pj && x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH2) ?? 0,
                                Col6 = dataInHeaderXD_DLCKP.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == pj && x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH3) + dataInHeaderTTB_DLCKP.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == pj && x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH3) ?? 0,
                                Col7 = dataInHeaderXD_DLCKP.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == pj && x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH4) + dataInHeaderTTB_DLCKP.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == pj && x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH4) ?? 0,
                                Col8 = dataInHeaderXD_DLCKP.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == pj && x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH5) + dataInHeaderTTB_DLCKP.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == pj && x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH5) ?? 0,
                                Col9 = dataInHeaderXD_DLCKP.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == pj && x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH6) + dataInHeaderTTB_DLCKP.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == pj && x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH6) ?? 0,
                                Col10 = dataInHeaderXD_DLCKP.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == pj && x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH7) + dataInHeaderTTB_DLCKP.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == pj && x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH7) ?? 0,
                                Col11 = dataInHeaderXD_DLCKP.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == pj && x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH8) + dataInHeaderTTB_DLCKP.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == pj && x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH8) ?? 0,
                                Col12 = dataInHeaderXD_DLCKP.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == pj && x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH9) + dataInHeaderTTB_DLCKP.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == pj && x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH9) ?? 0,
                                Col13 = dataInHeaderXD_DLCKP.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == pj && x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH10) + dataInHeaderTTB_DLCKP.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == pj && x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH10) ?? 0,
                                Col14 = dataInHeaderXD_DLCKP.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == pj && x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH11) + dataInHeaderTTB_DLCKP.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == pj && x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH11) ?? 0,
                                Col15 = dataInHeaderXD_DLCKP.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == pj && x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH12) + dataInHeaderTTB_DLCKP.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == pj && x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH12) ?? 0,
                                Order = orderDT,
                            };
                            data.DauTu.Add(item);
                            orderDT += 1;
                        }

                        data.DauTu.Add(new DauTuCKP
                        {
                            Name = "TỔNG CỘNG",
                            Col2 = dataInHeaderDTXD.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.VALUE) + dataInHeaderDTTTB.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.VALUE) ?? 0,
                            Col4 = dataInHeaderXD_DLCKP.Where(x =>x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH1) + dataInHeaderTTB_DLCKP.Where(x =>x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH1) ?? 0,
                            Col5 = dataInHeaderXD_DLCKP.Where(x =>x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH2) + dataInHeaderTTB_DLCKP.Where(x =>x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH2) ?? 0,
                            Col6 = dataInHeaderXD_DLCKP.Where(x =>x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH3) + dataInHeaderTTB_DLCKP.Where(x =>x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH3) ?? 0,
                            Col7 = dataInHeaderXD_DLCKP.Where(x =>x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH4) + dataInHeaderTTB_DLCKP.Where(x =>x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH4) ?? 0,
                            Col8 = dataInHeaderXD_DLCKP.Where(x =>x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH5) + dataInHeaderTTB_DLCKP.Where(x =>x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH5) ?? 0,
                            Col9 = dataInHeaderXD_DLCKP.Where(x =>x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH6) + dataInHeaderTTB_DLCKP.Where(x =>x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH6) ?? 0,
                            Col10 = dataInHeaderXD_DLCKP.Where(x =>x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH7) + dataInHeaderTTB_DLCKP.Where(x =>x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH7) ?? 0,
                            Col11 = dataInHeaderXD_DLCKP.Where(x =>x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH8) + dataInHeaderTTB_DLCKP.Where(x =>x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH8) ?? 0,
                            Col12 = dataInHeaderXD_DLCKP.Where(x =>x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH9) + dataInHeaderTTB_DLCKP.Where(x =>x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH9) ?? 0,
                            Col13 = dataInHeaderXD_DLCKP.Where(x =>x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH10) + dataInHeaderTTB_DLCKP.Where(x =>x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH10) ?? 0,
                            Col14 = dataInHeaderXD_DLCKP.Where(x =>x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH11) + dataInHeaderTTB_DLCKP.Where(x =>x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH11) ?? 0,
                            Col15 = dataInHeaderXD_DLCKP.Where(x =>x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH12) + dataInHeaderTTB_DLCKP.Where(x =>x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.MONTH12) ?? 0,
                            Order = 0,
                            IsBold = true,
                        });

                        #endregion

                    }
                });

                Task task3 = Task.Run(() =>
                {
                    lock (lockObject)
                    {

                        var header1_1 = UnitOfWork.Repository<SuaChuaLonRepo>().Queryable().Where(x => x.PHIEN_BAN == "PB1" && x.KICH_BAN == kichBan && x.TIME_YEAR == year && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                        var details1_1 = UnitOfWork.Repository<SuaChuaLonDataRepo>().Queryable().Where(x => header1_1.Contains(x.TEMPLATE_CODE)).ToList();

                        var header3 = UnitOfWork.Repository<SuaChuaLonRepo>().Queryable().Where(x => x.PHIEN_BAN == "PB3" && x.KICH_BAN == kichBan && x.TIME_YEAR == year && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                        var details3 = UnitOfWork.Repository<SuaChuaLonDataRepo>().Queryable().Where(x => header3.Contains(x.TEMPLATE_CODE)).ToList();

                        var header4 = UnitOfWork.Repository<SuaChuaLonRepo>().Queryable().Where(x => x.PHIEN_BAN == "PB4" && x.KICH_BAN == kichBan && x.TIME_YEAR == year && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                        var details4 = UnitOfWork.Repository<SuaChuaLonDataRepo>().Queryable().Where(x => header4.Contains(x.TEMPLATE_CODE)).ToList();

                        var header5 = UnitOfWork.Repository<SuaChuaLonRepo>().Queryable().Where(x => x.PHIEN_BAN == "PB5" && x.KICH_BAN == kichBan && x.TIME_YEAR == year && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                        var details5 = UnitOfWork.Repository<SuaChuaLonDataRepo>().Queryable().Where(x => header3.Contains(x.TEMPLATE_CODE)).ToList();

                        string orgCost = ChiNhanh[area];

                        #region KẾ HOẠCH SỬA CHỮA LỚN TÀI SẢN CỐ ĐỊNH
                        var dataHeaderSuaChuaLon = UnitOfWork.Repository<SuaChuaLonRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == kichBan && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                        /*if (dataHeaderSuaChuaLon.Count() == 0)
                        {
                            cancellationTokenSource.Cancel();

                        }*/
                        var dataHeaderDLTHSCL = UnitOfWork.Repository<SuaChuaLonRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB5" && x.KICH_BAN == kichBan && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();

                        var dataInHeaderSCL = UnitOfWork.Repository<SuaChuaLonDataRepo>().Queryable().Where(x => x.TIME_YEAR == year && dataHeaderSuaChuaLon.Contains(x.TEMPLATE_CODE)).ToList();
                        var dataInHeaderDLTHSCL = UnitOfWork.Repository<SuaChuaLonDataRepo>().Queryable().Where(x => x.TIME_YEAR == year && dataHeaderDLTHSCL.Contains(x.TEMPLATE_CODE)).ToList();
                        var sumSCL = new SuaChuaCKP
                        {
                            Name = "TỔNG CỘNG",
                            Order = 0,
                            IsBold = true
                        };
                        var lstData = !string.IsNullOrEmpty(area) ? dataInHeaderSCL.Where(x => x.ORG_CODE.StartsWith(costCenter)).ToList() : dataInHeaderSCL;
                        var lstDataDLTH = !string.IsNullOrEmpty(area) ? dataInHeaderDLTHSCL.Where(x => x.ORG_CODE.StartsWith(costCenter)).ToList() : dataInHeaderDLTHSCL;

                        var lstKhoanMuc = lstData.Select(x => x.KHOAN_MUC_SUA_CHUA_CODE).Distinct().ToList();
                        var lstKhoanMucDLTH = lstDataDLTH.Select(x => x.KHOAN_MUC_SUA_CHUA_CODE).Distinct().ToList();
                        var orderSCL = 1;

                        List<SuaChuaCKP> lstElementSCL = new List<SuaChuaCKP>();
                        foreach (var kmCode in lstKhoanMuc)
                        {
                            var name = lstData.FirstOrDefault(x => x.KHOAN_MUC_SUA_CHUA_CODE == kmCode).KhoanMucSuaChua.NAME;
                            // Lấy mã cha
                            var parentCodeItem = lstData.FirstOrDefault(x => x.KHOAN_MUC_SUA_CHUA_CODE == kmCode).KhoanMucSuaChua.PARENT_CODE;
                            var dataCol2 = details3.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE == kmCode);
                            var dataCol4 = details4.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE.StartsWith(kmCode)).ToList();
                            var item = new SuaChuaCKP
                            {
                                code = kmCode,
                                parentCode = parentCodeItem,
                                Col1 = lstData.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE == kmCode).Sum(x => x.VALUE) ?? 0,
                                Col2 = dataCol2.Sum(x => x.MONTH1 + x.MONTH2 + x.MONTH3 + x.MONTH4 + x.MONTH5 + x.MONTH6 + x.MONTH7 + x.MONTH8 + x.MONTH9 + x.MONTH10 + x.MONTH11 + x.MONTH12) ?? 0,
                                Col4 = dataCol4.Sum(x => x.MONTH1) ?? 0,
                                Col5 = dataCol4.Sum(x => x.MONTH2) ?? 0,
                                Col6 = dataCol4.Sum(x => x.MONTH3) ?? 0,
                                Col7 = dataCol4.Sum(x => x.MONTH4) ?? 0,
                                Col8 = dataCol4.Sum(x => x.MONTH5) ?? 0,
                                Col9 = dataCol4.Sum(x => x.MONTH6) ?? 0,
                                Col10 = dataCol4.Sum(x => x.MONTH7) ?? 0,
                                Col11= dataCol4.Sum(x => x.MONTH8) ?? 0,
                                Col12 = dataCol4.Sum(x => x.MONTH9) ?? 0,
                                Col13 = dataCol4.Sum(x => x.MONTH10) ?? 0,
                                Col14 = dataCol4.Sum(x => x.MONTH11) ?? 0,
                                Col15 = dataCol4.Sum(x => x.MONTH12) ?? 0,

                                Name = name,
                                IsBold = false
                            };
                            item.Col3 = item.Col1 + item.Col2;
                            lstElementSCL.Add(item);
                            


                        }

                        var lstParentCode = lstElementSCL.Select(x => x.parentCode).Distinct().ToList();
                        foreach (var code in lstParentCode)
                        {
                            if (string.IsNullOrEmpty(code))
                            {
                                continue;
                            }
                            var name = UnitOfWork.Repository<KhoanMucSuaChuaRepo>().Queryable().FirstOrDefault(x => x.CODE == code)?.NAME;
                            var countSCL = lstElementSCL.Where(x => x.parentCode == code).Count();
                            var parent = new SuaChuaCKP
                            {
                                Name = name,
                                Col1 = lstElementSCL.Where(x => x.parentCode == code).Sum(x => x.Col1),
                                Col2 = lstElementSCL.Where(x => x.parentCode == code).Sum(x => x.Col2),
                                Col3 = lstElementSCL.Where(x => x.parentCode == code).Sum(x => x.Col3),
                                Col4 = lstElementSCL.Where(x => x.parentCode == code).Sum(x => x.Col4),
                                Col5 = lstElementSCL.Where(x => x.parentCode == code).Sum(x => x.Col5),
                                Col6 = lstElementSCL.Where(x => x.parentCode == code).Sum(x => x.Col6),
                                Col7 = lstElementSCL.Where(x => x.parentCode == code).Sum(x => x.Col7),
                                Col8 = lstElementSCL.Where(x => x.parentCode == code).Sum(x => x.Col8),
                                Col9 = lstElementSCL.Where(x => x.parentCode == code).Sum(x => x.Col9),
                                Col10 = lstElementSCL.Where(x => x.parentCode == code).Sum(x => x.Col10),
                                Col11 = lstElementSCL.Where(x => x.parentCode == code).Sum(x => x.Col11),
                                Col12 = lstElementSCL.Where(x => x.parentCode == code).Sum(x => x.Col12),
                                Col13 = lstElementSCL.Where(x => x.parentCode == code).Sum(x => x.Col13),
                                Col14 = lstElementSCL.Where(x => x.parentCode == code).Sum(x => x.Col14),
                                Col15 = lstElementSCL.Where(x => x.parentCode == code).Sum(x => x.Col15),

                                Order = orderSCL,
                                IsBold = true
                            };
                            data.SuaChuaLon.Add(parent);
                            var lstChild = lstElementSCL.Where(x => x.parentCode == code).ToList();
                            lstChild = lstChild.Distinct().ToList();
                            foreach(var item in lstChild)
                            {
                                item.Order = orderSCL + 1;
                                orderSCL++;
                                data.SuaChuaLon.Add(item);
                            }
                            orderSCL++;
                            sumSCL.Col1 = sumSCL.Col1 + parent.Col1;
                            sumSCL.Col2 = sumSCL.Col2 + parent.Col2;
                            sumSCL.Col3 = sumSCL.Col3 + parent.Col3;
                            sumSCL.Col4 = sumSCL.Col4 + parent.Col4;
                            sumSCL.Col5 = sumSCL.Col5 + parent.Col5;
                            sumSCL.Col6 = sumSCL.Col6 + parent.Col6;
                            sumSCL.Col7 = sumSCL.Col7 + parent.Col7;
                            sumSCL.Col8 = sumSCL.Col8 + parent.Col8;
                            sumSCL.Col9 = sumSCL.Col9 + parent.Col9;
                            sumSCL.Col10 = sumSCL.Col10 + parent.Col10;
                            sumSCL.Col11 = sumSCL.Col11 + parent.Col11;
                            sumSCL.Col12 = sumSCL.Col12 + parent.Col12;
                            sumSCL.Col13 = sumSCL.Col13 + parent.Col13;
                            sumSCL.Col14 = sumSCL.Col14 + parent.Col14;
                            sumSCL.Col15 = sumSCL.Col15 + parent.Col15;

                        }

                        data.SuaChuaLon.Add(sumSCL);
                        #endregion

                    }
                });

                Task task4 = Task.Run(() =>
                {
                    lock (lockObject)
                    {

                        var header1_1 = UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.PHIEN_BAN == "PB1" && x.KICH_BAN == kichBan && x.TIME_YEAR == year && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                        var details1_1 = UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => header1_1.Contains(x.TEMPLATE_CODE)).ToList();

                        var header3 = UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.PHIEN_BAN == "PB3" && x.KICH_BAN == kichBan && x.TIME_YEAR == year && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                        var details3 = UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => header3.Contains(x.TEMPLATE_CODE)).ToList();

                        var header5 = UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.PHIEN_BAN == "PB5" && x.KICH_BAN == kichBan && x.TIME_YEAR == year && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                        var details5 = UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => header3.Contains(x.TEMPLATE_CODE)).ToList();

                        var elements = UnitOfWork.Repository<ReportChiPhiCodeRepo>().GetAll().ToList();

                        foreach (var e in elements)
                        {
                            var i = new ChiPhiCKP
                            {
                                Name = e.GROUP_NAME,
                                IsBold = string.IsNullOrEmpty(e.GROUP_2_ID) ? true : false,
                                Col1 = details1_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.GROUP_1_ID + e.GROUP_2_ID)).Sum(x => x.QUANTITY * x.PRICE),
                                Col2 = details3.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.GROUP_1_ID + e.GROUP_2_ID)).Sum(x => x.QUANTITY * x.PRICE),

                            };
                            i.Col3 = i.Col1 + i.Col2;
                            data.ChiPhi.Add(i);
                        }
                    }
                });

                await Task.WhenAll(task2, task3, task4);

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public RevenueByFeeReportModel GetDataDoanhThuTheoPhi(int year, string phienBan, string kichBan, string hangHangKhong)
        {
            try
            {
                var data = new RevenueByFeeReportModel();
                var lstHangHangKhong = UnitOfWork.Repository<HangHangKhongRepo>().GetAll().GroupBy(x => x.GROUP_ITEM).Select(x => x.First()).ToList();
                lstHangHangKhong = string.IsNullOrEmpty(hangHangKhong) ? lstHangHangKhong : lstHangHangKhong.Where(x => x.CODE == hangHangKhong).ToList();
                lstHangHangKhong = lstHangHangKhong.Where(x => !string.IsNullOrEmpty(x.GROUP_ITEM)).ToList();

                var s1 = UnitOfWork.Repository<SharedDataRepo>().Get("1").VALUE;
                var s2 = UnitOfWork.Repository<SharedDataRepo>().Get("2").VALUE;
                var s3 = UnitOfWork.Repository<SharedDataRepo>().Get("3").VALUE;

                var dataHeaderDoanhThu = UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == kichBan && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                if (dataHeaderDoanhThu.Count() == 0)
                {
                    return new RevenueByFeeReportModel();
                }

                var dataInHeader = UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM != null && x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM != "" && dataHeaderDoanhThu.Contains(x.TEMPLATE_CODE)).ToList();

                var headerSL = UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == kichBan && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var dataSL = UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM != null && x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM != "" && headerSL.Contains(x.TEMPLATE_CODE)).ToList();


                var dataDetailsTab1 = dataInHeader.Where(x => x.KHOAN_MUC_DOANH_THU_CODE == "2001" || x.KHOAN_MUC_DOANH_THU_CODE == "2002").ToList();
                var dataDetailsTab2 = dataSL.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010" || x.KHOAN_MUC_SAN_LUONG_CODE == "10020").ToList();
                var dataDetailsTab3 = dataSL.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010" || x.KHOAN_MUC_SAN_LUONG_CODE == "10020").ToList();
                var dataDetailsTab5 = dataSL.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010").ToList();


                var shareData = UnitOfWork.Repository<SharedDataRepo>().Get("18").VALUE;

                var sumTab1 = new RevenueReportModel
                {
                    Name = "TỔNG CỘNG",
                    Value1 = dataDetailsTab1.Sum(x => x.VALUE_JAN) ?? 0,
                    Value2 = dataDetailsTab1.Sum(x => x.VALUE_FEB) ?? 0,
                    Value3 = dataDetailsTab1.Sum(x => x.VALUE_MAR) ?? 0,
                    Value4 = dataDetailsTab1.Sum(x => x.VALUE_APR) ?? 0,
                    Value5 = dataDetailsTab1.Sum(x => x.VALUE_MAY) ?? 0,
                    Value6 = dataDetailsTab1.Sum(x => x.VALUE_JUN) ?? 0,
                    Value7 = dataDetailsTab1.Sum(x => x.VALUE_JUL) ?? 0,
                    Value8 = dataDetailsTab1.Sum(x => x.VALUE_AUG) ?? 0,
                    Value9 = dataDetailsTab1.Sum(x => x.VALUE_SEP) ?? 0,
                    Value10 = dataDetailsTab1.Sum(x => x.VALUE_OCT) ?? 0,
                    Value11 = dataDetailsTab1.Sum(x => x.VALUE_NOV) ?? 0,
                    Value12 = dataDetailsTab1.Sum(x => x.VALUE_SEP) ?? 0,
                    ValueSumYear = dataDetailsTab1.Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                    Order = -1,
                    IsBold = true,
                };
                data.Tab1.Add(sumTab1);
                var sumTab2 = new RevenueReportModel
                {
                    Name = "TỔNG CỘNG",
                    Value1 = dataDetailsTab2.Sum(x => x.VALUE_JAN) * s2 * 45 ?? 0,
                    Value2 = dataDetailsTab2.Sum(x => x.VALUE_FEB) * s2 * 45 ?? 0,
                    Value3 = dataDetailsTab2.Sum(x => x.VALUE_MAR) * s2 * 45 ?? 0,
                    Value4 = dataDetailsTab2.Sum(x => x.VALUE_APR) * s2 * 45 ?? 0,
                    Value5 = dataDetailsTab2.Sum(x => x.VALUE_MAY) * s2 * 45 ?? 0,
                    Value6 = dataDetailsTab2.Sum(x => x.VALUE_JUN) * s2 * 45 ?? 0,
                    Value7 = dataDetailsTab2.Sum(x => x.VALUE_JUL) * s2 * 45 ?? 0,
                    Value8 = dataDetailsTab2.Sum(x => x.VALUE_AUG) * s2 * 45 ?? 0,
                    Value9 = dataDetailsTab2.Sum(x => x.VALUE_SEP) * s2 * 45 ?? 0,
                    Value10 = dataDetailsTab2.Sum(x => x.VALUE_OCT) * s2 * 45 ?? 0,
                    Value11 = dataDetailsTab2.Sum(x => x.VALUE_NOV) * s2 * 45 ?? 0,
                    Value12 = dataDetailsTab2.Sum(x => x.VALUE_SEP) * s2 * 45 ?? 0,
                    ValueSumYear = dataDetailsTab2.Sum(x => x.VALUE_SUM_YEAR) * s2 * 45 ?? 0,
                    Order = -1,
                    IsBold = true,
                };

                data.Tab2.Add(sumTab2);
                var sumTab3 = new RevenueReportModel
                {
                    Name = "TỔNG CỘNG",
                    Value1 = dataDetailsTab3.Sum(x => x.VALUE_JAN) * shareData?? 0,
                    Value2 = dataDetailsTab3.Sum(x => x.VALUE_FEB) *shareData?? 0,
                    Value3 = dataDetailsTab3.Sum(x => x.VALUE_MAR) *shareData?? 0,
                    Value4 = dataDetailsTab3.Sum(x => x.VALUE_APR) *shareData?? 0,
                    Value5 = dataDetailsTab3.Sum(x => x.VALUE_MAY) *shareData?? 0,
                    Value6 = dataDetailsTab3.Sum(x => x.VALUE_JUN) *shareData?? 0,
                    Value7 = dataDetailsTab3.Sum(x => x.VALUE_JUL) *shareData?? 0,
                    Value8 = dataDetailsTab3.Sum(x => x.VALUE_AUG) *shareData?? 0,
                    Value9 = dataDetailsTab3.Sum(x => x.VALUE_SEP) * shareData ?? 0,
                    Value10 = dataDetailsTab3.Sum(x => x.VALUE_OCT) *shareData?? 0,
                    Value11 = dataDetailsTab3.Sum(x => x.VALUE_NOV) *shareData?? 0,
                    Value12 = dataDetailsTab3.Sum(x => x.VALUE_SEP) *shareData?? 0,
                    ValueSumYear = dataDetailsTab3.Sum(x => x.VALUE_SUM_YEAR) *shareData?? 0,
                    Order = -1,
                    IsBold = true,
                };
                data.Tab3.Add(sumTab3);
                
                var order = 0;
                foreach (var hhk in lstHangHangKhong)
                {
                    var tab1 = new RevenueReportModel
                    {
                        Name = hhk.GROUP_ITEM,
                        Value1 = dataDetailsTab1.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_JAN) ?? 0,
                        Value2 = dataDetailsTab1.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_FEB) ?? 0,
                        Value3 = dataDetailsTab1.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_MAR) ?? 0,
                        Value4 = dataDetailsTab1.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_APR) ?? 0,
                        Value5 = dataDetailsTab1.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_MAY) ?? 0,
                        Value6 = dataDetailsTab1.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_JUN) ?? 0,
                        Value7 = dataDetailsTab1.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_JUL) ?? 0,
                        Value8 = dataDetailsTab1.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_AUG) ?? 0,
                        Value9 = dataDetailsTab1.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_SEP) ?? 0,
                        Value10 = dataDetailsTab1.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_OCT) ?? 0,
                        Value11 = dataDetailsTab1.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_NOV) ?? 0,
                        Value12 = dataDetailsTab1.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_SEP) ?? 0,
                        ValueSumYear = dataDetailsTab1.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        Order = order,
                    };
                    data.Tab1.Add(tab1);


                    var d2 = dataDetailsTab2.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP").ToList();
                    var tab2 = new RevenueReportModel
                    {
                        Name = hhk.GROUP_ITEM,
                        Value1 = d2.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_JAN) * s2 * 45 ?? 0,
                        Value2 = d2.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_FEB) * s2 * 45 ?? 0,
                        Value3 = d2.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_MAR) * s2 * 45 ?? 0,
                        Value4 = d2.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_APR) * s2 * 45 ?? 0,
                        Value5 = d2.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_MAY) * s2 * 45 ?? 0,
                        Value6 = d2.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_JUN) * s2 * 45 ?? 0,
                        Value7 = d2.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_JUL) * s2 * 45 ?? 0,
                        Value8 = d2.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_AUG) * s2 * 45 ?? 0,
                        Value9 = d2.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_SEP) * s2 * 45 ?? 0,
                        Value10 = d2.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_OCT) * s2 * 45 ?? 0,
                        Value11 = d2.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_NOV) * s2 * 45 ?? 0,
                        Value12 = d2.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_SEP) * s2 * 45 ?? 0,
                        ValueSumYear = d2.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_SUM_YEAR) * s2 * 45 ?? 0,
                        Order = order,
                    };
                    data.Tab2.Add(tab2);

                    var tab3 = new RevenueReportModel
                    {
                        Name = hhk.GROUP_ITEM,
                        Value1 = dataDetailsTab3.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_JAN) * shareData ?? 0,
                        Value2 = dataDetailsTab3.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_FEB) * shareData ?? 0,
                        Value3 = dataDetailsTab3.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_MAR) * shareData ?? 0,
                        Value4 = dataDetailsTab3.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_APR) * shareData ?? 0,
                        Value5 = dataDetailsTab3.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_MAY) * shareData ?? 0,
                        Value6 = dataDetailsTab3.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_JUN) * shareData ?? 0,
                        Value7 = dataDetailsTab3.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_JUL) * shareData ?? 0,
                        Value8 = dataDetailsTab3.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_AUG) * shareData ?? 0,
                        Value9 = dataDetailsTab3.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_SEP) * shareData ?? 0,
                        Value10 = dataDetailsTab3.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_OCT) * shareData ?? 0,
                        Value11 = dataDetailsTab3.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_NOV) * shareData ?? 0,
                        Value12 = dataDetailsTab3.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_SEP) * shareData ?? 0,
                        ValueSumYear = dataDetailsTab3.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_SUM_YEAR) * shareData ?? 0,
                        Order = order,
                    };
                    data.Tab3.Add(tab3);

                    //tab5
                    var shareDataCode = "TNK" + "-" + hhk.GROUP_ITEM;
                    var priceTNK = UnitOfWork.Repository<SharedDataRepo>().Get(shareDataCode).VALUE;
                    var tab5 = new RevenueReportModel
                    {
                        Name = hhk.GROUP_ITEM,
                        Value1 = dataDetailsTab5.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_JAN) * priceTNK * s1 * s2 * s3 ?? 0,
                        Value2 = dataDetailsTab5.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_FEB) * priceTNK * s1 * s2 * s3 ?? 0,
                        Value3 = dataDetailsTab5.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_MAR) * priceTNK * s1 * s2 * s3 ?? 0,
                        Value4 = dataDetailsTab5.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_APR) * priceTNK * s1 * s2 * s3 ?? 0,
                        Value5 = dataDetailsTab5.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_MAY) * priceTNK * s1 * s2 * s3 ?? 0,
                        Value6 = dataDetailsTab5.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_JUN) * priceTNK * s1 * s2 * s3 ?? 0,
                        Value7 = dataDetailsTab5.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_JUL) * priceTNK * s1 * s2 * s3 ?? 0,
                        Value8 = dataDetailsTab5.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_AUG) * priceTNK * s1 * s2 * s3 ?? 0,
                        Value9 = dataDetailsTab5.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_SEP) * priceTNK * s1 * s2 * s3 ?? 0,
                        Value10 = dataDetailsTab5.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_OCT) * priceTNK * s1 * s2 * s3 ?? 0,
                        Value11 = dataDetailsTab5.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_NOV) * priceTNK * s1 * s2 * s3 ?? 0,
                        Value12 = dataDetailsTab5.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_SEP) * priceTNK * s1 * s2 * s3 ?? 0,
                        ValueSumYear = dataDetailsTab5.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_SUM_YEAR) * priceTNK * s1 * s2 * s3 ?? 0,
                        Order = order,
                    };
                    data.Tab5.Add(tab5);

                    data.Tab4.Add(new RevenueReportModel
                    {
                        Name = hhk.GROUP_ITEM,
                        Value1 = tab1.Value1 + tab2.Value1 + tab3.Value1 + tab5.Value1,
                        Value2 = tab1.Value2 + tab2.Value2 + tab3.Value2 + tab5.Value2,
                        Value3 = tab1.Value3 + tab2.Value3 + tab3.Value3 + tab5.Value3,
                        Value4 = tab1.Value4 + tab2.Value4 + tab3.Value4 + tab5.Value4,
                        Value5 = tab1.Value5 + tab2.Value5 + tab3.Value5 + tab5.Value5,
                        Value6 = tab1.Value6 + tab2.Value6 + tab3.Value6 + tab5.Value6,
                        Value7 = tab1.Value7 + tab2.Value7 + tab3.Value7 + tab5.Value7,
                        Value8 = tab1.Value8 + tab2.Value8 + tab3.Value8 + tab5.Value8,
                        Value9 = tab1.Value9 + tab2.Value9 + tab3.Value9 + tab5.Value9,
                        Value10 = tab1.Value10 + tab2.Value10 + tab3.Value10 + tab5.Value10,
                        Value11 = tab1.Value11 + tab2.Value11 + tab3.Value11 + tab5.Value11,
                        Value12 = tab1.Value12 + tab2.Value12 + tab3.Value12 + tab5.Value12,
                        ValueSumYear = tab1.ValueSumYear + tab2.ValueSumYear + tab3.ValueSumYear + tab5.ValueSumYear,
                        Order = order,
                    });
                    

                    order++;
                }

                data.Tab4.Add(new RevenueReportModel
                {
                    Name = "TỔNG CỘNG",
                    Value1 = data.Tab4.Sum(x => x.Value1),
                    Value2 = data.Tab4.Sum(x=>x.Value2),
                    Value3 = data.Tab4.Sum(x => x.Value3),
                    Value4 = data.Tab4.Sum(x => x.Value4),
                    Value5 = data.Tab4.Sum(x => x.Value5),
                    Value6 = data.Tab4.Sum(x => x.Value6),
                    Value7 = data.Tab4.Sum(x => x.Value7),
                    Value8 = data.Tab4.Sum(x => x.Value8),
                    Value9 = data.Tab4.Sum(x => x.Value9),
                    Value10 = data.Tab4.Sum(x => x.Value10),
                    Value11 = data.Tab4.Sum(x => x.Value11),
                    Value12 = data.Tab4.Sum(x => x.Value12),
                    ValueSumYear = data.Tab4.Sum(x => x.ValueSumYear),
                    Order = -1,
                    IsBold = true,
                });

                data.Tab5.Add(new RevenueReportModel
                {
                    Name = "TỔNG CỘNG",
                    Value1 = data.Tab5.Sum(x=>x.Value1),
                    Value2 = data.Tab5.Sum(x => x.Value2),
                    Value3 = data.Tab5.Sum(x => x.Value3),
                    Value4 = data.Tab5.Sum(x => x.Value4),
                    Value5 = data.Tab5.Sum(x => x.Value5),
                    Value6 = data.Tab5.Sum(x => x.Value6),
                    Value7 = data.Tab5.Sum(x => x.Value7),
                    Value8 = data.Tab5.Sum(x => x.Value8),
                    Value9 = data.Tab5.Sum(x => x.Value9),
                    Value10 = data.Tab5.Sum(x => x.Value10),
                    Value11 = data.Tab5.Sum(x => x.Value11),
                    Value12 = data.Tab5.Sum(x => x.Value12),
                    ValueSumYear = data.Tab5.Sum(x => x.ValueSumYear),
                    Order = -1,
                    IsBold = true
                });

                var discount = UnitOfWork.Repository<SharedDataRepo>().Get("24").VALUE;
                data.Tab1.Add(new RevenueReportModel
                {
                    Name = "GIẢM GIÁ",
                    IsBold = true,
                    Value1 = data.Tab1.Where(x => x.Name == "VN" || x.Name == "0V").Sum(x => x.Value1) * discount,
                    Value2 = data.Tab1.Where(x => x.Name == "VN" || x.Name == "0V").Sum(x => x.Value2) * discount,
                    Value3 = data.Tab1.Where(x => x.Name == "VN" || x.Name == "0V").Sum(x => x.Value3) * discount,
                    Value4 = data.Tab1.Where(x => x.Name == "VN" || x.Name == "0V").Sum(x => x.Value4) * discount,
                    Value5 = data.Tab1.Where(x => x.Name == "VN" || x.Name == "0V").Sum(x => x.Value5) * discount,
                    Value6 = data.Tab1.Where(x => x.Name == "VN" || x.Name == "0V").Sum(x => x.Value6) * discount,
                    Value7 = data.Tab1.Where(x => x.Name == "VN" || x.Name == "0V").Sum(x => x.Value7) * discount,
                    Value8 = data.Tab1.Where(x => x.Name == "VN" || x.Name == "0V").Sum(x => x.Value8) * discount,
                    Value9 = data.Tab1.Where(x => x.Name == "VN" || x.Name == "0V").Sum(x => x.Value9) * discount,
                    Value10 = data.Tab1.Where(x => x.Name == "VN" || x.Name == "0V").Sum(x => x.Value10) * discount,
                    Value11 = data.Tab1.Where(x => x.Name == "VN" || x.Name == "0V").Sum(x => x.Value11) * discount,
                    Value12 = data.Tab1.Where(x => x.Name == "VN" || x.Name == "0V").Sum(x => x.Value12) * discount,
                    ValueSumYear = data.Tab1.Where(x => x.Name == "VN" || x.Name == "0V").Sum(x => x.ValueSumYear) * discount,
                    Order = 100
                });

                return data;
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                this.State = false;
                this.Exception = ex;
                return new RevenueByFeeReportModel();
            }
        }

        public IList<SynthesisReportModel> GetData(int year, string phienBan)
        {
            var data = new List<SynthesisReportModel>();
            var elements = UnitOfWork.Repository<ReportSXKDElementRepo>().GetAll().OrderBy(x => x.C_ORDER).ToList();
            foreach (var e in elements)
            {
                var i = new SynthesisReportModel
                {
                    PId = e.ID,
                    Parent = e.PARENT,
                    Stt = e.STT,
                    Name = e.NAME,
                    UnitName = e.DVT,
                    IsBold = e.IS_BOLD,
                    Order = e.C_ORDER,
                    Value1 = string.IsNullOrEmpty(e.KH_C) ? 0 : Convert.ToDecimal(UnitOfWork.GetSession().CreateSQLQuery($"{e.KH_C.Replace("[YEAR]", year.ToString()).Replace("[PHIEN_BAN]", phienBan)}").List()[0]),
                    Value2 = string.IsNullOrEmpty(e.KH_TB) ? 0 : Convert.ToDecimal(UnitOfWork.GetSession().CreateSQLQuery($"{e.KH_TB.Replace("[YEAR]", year.ToString()).Replace("[PHIEN_BAN]", phienBan)}").List()[0]),
                    Value3 = string.IsNullOrEmpty(e.KH_T) ? 0 : Convert.ToDecimal(UnitOfWork.GetSession().CreateSQLQuery($"{e.KH_T.Replace("[YEAR]", year.ToString()).Replace("[PHIEN_BAN]", phienBan)}").List()[0]),
                };
                data.Add(i);
            }
            foreach (var d in data.OrderByDescending(x => x.Order))
            {
                var childs = data.Where(x => x.Parent == d.PId).ToList();
                d.Value1 = childs.Sum(x => x.Value1) == 0 || d.Value1 != 0 ? d.Value1 : childs.Sum(x => x.Value1);
                d.Value2 = childs.Sum(x => x.Value2) == 0 || d.Value2 != 0 ? d.Value2 : childs.Sum(x => x.Value2);
                d.Value3 = childs.Sum(x => x.Value3) == 0 || d.Value3 != 0 ? d.Value3 : childs.Sum(x => x.Value3);
            }

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



        private ICellStyle GetCellStyleNumber(IWorkbook templateWorkbook)
        {
            ICellStyle styleCellNumber = templateWorkbook.CreateCellStyle();
            styleCellNumber.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");
            return styleCellNumber;
        }

        internal void ExportExcelSanLuong(ref MemoryStream outFileStream, string path, int year, string phienBan, string kichBan, string hangHangKhong)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                IWorkbook templateWorkbook;
                templateWorkbook = new XSSFWorkbook(fs);
                fs.Close();
                //Sản Lượng theo tháng
                ISheet sheetTab1 = templateWorkbook.GetSheetAt(0);
                var data = GetDataSanLuong(year, phienBan, kichBan, hangHangKhong);
                var startRow = 6;
                var NUM_CELL = 14;
                InsertDataSanLuongTab1(templateWorkbook, sheetTab1, data.Tab1, startRow, NUM_CELL);
                // Sản lượng theo chi nhánh
                var startRowTab2 = 6;
                var NUM_CELL_Tab2 = 5;
                ISheet sheetTab2 = templateWorkbook.GetSheetAt(1);
                InsertDataSanLuongTab2(templateWorkbook, sheetTab2, data.Tab2, startRowTab2, NUM_CELL_Tab2);
                //Sản lượng theo sân bay
                var startRowTab3 = 7;
                var NUM_CELL_Tab3 = 12;
                ISheet sheetTab3 = templateWorkbook.GetSheetAt(2);
                InsertDataSanLuongTab3(templateWorkbook, sheetTab3, data.Tab3, startRowTab3, NUM_CELL_Tab3);

                //Sản lượng theo sân bay
                var startRowTab4= 7;
                var NUM_CELL_Tab4 = 28;
                ISheet sheetTab4 = templateWorkbook.GetSheetAt(3);
                InsertDataSanLuongTab4(templateWorkbook, sheetTab4, data.TabSL_TN, startRowTab4, NUM_CELL_Tab4);

                templateWorkbook.Write(outFileStream);
            }
            catch (Exception ex)
            {
                this.State = false;
                this.ErrorMessage = "Có lỗi xẩy ra trong quá trình tạo file excel!";
                this.Exception = ex;
            }
        }

        internal void InsertDataSanLuongTab1(IWorkbook templateWorkbook, ISheet sheet, IList<RevenueReportModel> dataDetails, int startRow, int NUM_CELL)
        {
            ICellStyle styleCellBold = templateWorkbook.CreateCellStyle();
            styleCellBold.CloneStyleFrom(sheet.GetRow(7).Cells[0].CellStyle);
            var fontBold = templateWorkbook.CreateFont();
            fontBold.Boldweight = (short)FontBoldWeight.Bold;
            fontBold.FontHeightInPoints = 11;
            fontBold.FontName = "Times New Roman";

            ICellStyle styleName = templateWorkbook.CreateCellStyle();
            styleName.CloneStyleFrom(sheet.GetRow(6).Cells[0].CellStyle);

            ICellStyle styleBody = templateWorkbook.CreateCellStyle();
            styleBody.CloneStyleFrom(sheet.GetRow(7).Cells[1].CellStyle);
            styleBody.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");
            ICellStyle styleBodySum = templateWorkbook.CreateCellStyle();
            styleBodySum.CloneStyleFrom(sheet.GetRow(7).Cells[2].CellStyle);
            styleBodySum.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");
            for (int i = 0; i < dataDetails.Count(); i++)
            {
                var space = new StringBuilder();
                for(int j = 0; j< dataDetails[i].Level; j++)
                {
                    space.Append("    ");
                }
                var dataRow = dataDetails[i];
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, NUM_CELL);
                rowCur.Cells[0].SetCellValue(space.ToString() + dataDetails[i].Name.ToString());
                rowCur.Cells[1].SetCellValue(dataDetails[i]?.ValueSumYear == null ? 0 : (double)dataDetails[i]?.ValueSumYear);
                rowCur.Cells[2].SetCellValue(dataDetails[i]?.Value1 == null ? 0 : (double)dataDetails[i]?.Value1);
                rowCur.Cells[3].SetCellValue(dataDetails[i]?.Value2 == null ? 0 : (double)dataDetails[i]?.Value2);
                rowCur.Cells[4].SetCellValue(dataDetails[i]?.Value3 == null ? 0 : (double)dataDetails[i]?.Value3);
                rowCur.Cells[5].SetCellValue(dataDetails[i]?.Value4 == null ? 0 : (double)dataDetails[i]?.Value4);
                rowCur.Cells[6].SetCellValue(dataDetails[i]?.Value5 == null ? 0 : (double)dataDetails[i]?.Value5);
                rowCur.Cells[7].SetCellValue(dataDetails[i]?.Value6 == null ? 0 : (double)dataDetails[i]?.Value6);
                rowCur.Cells[8].SetCellValue(dataDetails[i]?.Value7 == null ? 0 : (double)dataDetails[i]?.Value7);
                rowCur.Cells[9].SetCellValue(dataDetails[i]?.Value8 == null ? 0 : (double)dataDetails[i]?.Value8);
                rowCur.Cells[10].SetCellValue(dataDetails[i]?.Value9 == null ? 0 : (double)dataDetails[i]?.Value9);
                rowCur.Cells[11].SetCellValue(dataDetails[i]?.Value10 == null ? 0 : (double)dataDetails[i]?.Value10);
                rowCur.Cells[12].SetCellValue(dataDetails[i]?.Value11 == null ? 0 : (double)dataDetails[i]?.Value11);
                rowCur.Cells[13].SetCellValue(dataDetails[i]?.Value12 == null ? 0 : (double)dataDetails[i]?.Value12);

                for (int j = 0; j < NUM_CELL; j++)
                {
                    if (dataDetails[i].IsBold)
                    {
                        if (j == 0)
                        {
                            rowCur.Cells[j].CellStyle = styleCellBold;
                            rowCur.Cells[j].CellStyle.SetFont(fontBold);
                        }
                        else
                        {
                            rowCur.Cells[j].CellStyle = styleBodySum;
                            rowCur.Cells[j].CellStyle.SetFont(fontBold);
                        }
                    }
                    else
                    {
                        rowCur.Cells[j].CellStyle = styleBody;
                    }
                }
            }
        }

        internal void InsertDataSanLuongTab2(IWorkbook templateWorkbook, ISheet sheet, IList<RevenueReportModel> dataDetails, int startRow, int NUM_CELL)
        {
            ICellStyle styleCellBold = templateWorkbook.CreateCellStyle();
            styleCellBold.CloneStyleFrom(sheet.GetRow(7).Cells[0].CellStyle);
            var fontBold = templateWorkbook.CreateFont();
            fontBold.Boldweight = (short)FontBoldWeight.Bold;
            fontBold.FontHeightInPoints = 11;
            fontBold.FontName = "Times New Roman";

            ICellStyle styleName = templateWorkbook.CreateCellStyle();
            styleName.CloneStyleFrom(sheet.GetRow(6).Cells[0].CellStyle);

            ICellStyle styleBody = templateWorkbook.CreateCellStyle();
            styleBody.CloneStyleFrom(sheet.GetRow(7).Cells[1].CellStyle);
            styleBody.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");
            ICellStyle styleBodySum = templateWorkbook.CreateCellStyle();
            styleBodySum.CloneStyleFrom(sheet.GetRow(7).Cells[2].CellStyle);
            styleBodySum.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");
            for (int i = 0; i < dataDetails.Count(); i++)
            {
                var dataRow = dataDetails[i];
                var space = new StringBuilder();
                for (int j = 0; j < dataDetails[i].Level; j++)
                {
                    space.Append("    ");
                }
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, NUM_CELL);
                rowCur.Cells[0].SetCellValue(space.ToString() + dataDetails[i].Name.ToString());
                rowCur.Cells[1].SetCellValue(dataDetails[i]?.ValueSumYear == null ? 0 : (double)dataDetails[i]?.ValueSumYear);
                rowCur.Cells[2].SetCellValue(dataDetails[i]?.Value1 == null ? 0 : (double)dataDetails[i]?.Value1);
                rowCur.Cells[3].SetCellValue(dataDetails[i]?.Value2 == null ? 0 : (double)dataDetails[i]?.Value2);
                rowCur.Cells[4].SetCellValue(dataDetails[i]?.Value3 == null ? 0 : (double)dataDetails[i]?.Value3);
                for (int j = 0; j < NUM_CELL; j++)
                {
                    if (dataDetails[i].IsBold)
                    {
                        if (j == 0)
                        {
                            rowCur.Cells[j].CellStyle = styleCellBold;
                            rowCur.Cells[j].CellStyle.SetFont(fontBold);
                        }
                        else
                        {
                            rowCur.Cells[j].CellStyle = styleBodySum;
                            rowCur.Cells[j].CellStyle.SetFont(fontBold);
                        }
                    }
                    else
                    {
                        rowCur.Cells[j].CellStyle = styleBody;
                    }
                }
            }
        }

        internal void InsertDataSanLuongTab3(IWorkbook templateWorkbook, ISheet sheet, IList<RevenueReportModel> dataDetails, int startRow, int NUM_CELL)
        {
            ICellStyle styleCellBold = templateWorkbook.CreateCellStyle();
            styleCellBold.CloneStyleFrom(sheet.GetRow(7).Cells[0].CellStyle);
            var fontBold = templateWorkbook.CreateFont();
            fontBold.Boldweight = (short)FontBoldWeight.Bold;
            fontBold.FontHeightInPoints = 11;
            fontBold.FontName = "Times New Roman";

            ICellStyle styleName = templateWorkbook.CreateCellStyle();
            styleName.CloneStyleFrom(sheet.GetRow(7).Cells[0].CellStyle);

            ICellStyle styleBody = templateWorkbook.CreateCellStyle();
            styleBody.CloneStyleFrom(sheet.GetRow(8).Cells[1].CellStyle);
            styleBody.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");
            ICellStyle styleBodySum = templateWorkbook.CreateCellStyle();
            styleBodySum.CloneStyleFrom(sheet.GetRow(8).Cells[2].CellStyle);
            styleBodySum.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");
            for (int i = 0; i < dataDetails.Count(); i++)
            {
                var dataRow = dataDetails[i];
                var space = new StringBuilder();
                for (int j = 0; j < dataDetails[i].Level; j++)
                {
                    space.Append("    ");
                }
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, NUM_CELL);
                rowCur.Cells[0].SetCellValue(dataDetails[i].Name.ToString());
                rowCur.Cells[1].SetCellValue(dataDetails[i]?.Code == null ? "" : dataDetails[i]?.Code);
                rowCur.Cells[2].SetCellValue(dataDetails[i]?.Value1 == null ? 0 : (double)dataDetails[i]?.Value1);
                rowCur.Cells[3].SetCellValue(dataDetails[i]?.Value2 == null ? 0 : (double)dataDetails[i]?.Value2);
                rowCur.Cells[4].SetCellValue(dataDetails[i]?.Value3 == null ? 0 : (double)dataDetails[i]?.Value3);
                rowCur.Cells[5].SetCellValue(dataDetails[i]?.Value4 == null ? 0 : (double)dataDetails[i]?.Value4);
                rowCur.Cells[6].SetCellValue(dataDetails[i]?.Value5 == null ? 0 : (double)dataDetails[i]?.Value5);
                rowCur.Cells[7].SetCellValue(dataDetails[i]?.Value6 == null ? 0 : (double)dataDetails[i]?.Value6);
                rowCur.Cells[8].SetCellValue(dataDetails[i]?.Value7 == null ? 0 : (double)dataDetails[i]?.Value7);
                rowCur.Cells[9].SetCellValue(dataDetails[i]?.Value8 == null ? 0 : (double)dataDetails[i]?.Value8);
                rowCur.Cells[10].SetCellValue(dataDetails[i]?.Value9 == null ? 0 : (double)dataDetails[i]?.Value9);
                rowCur.Cells[11].SetCellValue(dataDetails[i]?.Value10 == null ? 0 : (double)dataDetails[i]?.Value10);
                for (int j = 0; j < NUM_CELL; j++)
                {
                    if (dataDetails[i].IsBold)
                    {
                        if (j == 0 || j == 1)
                        {
                            rowCur.Cells[j].CellStyle = styleCellBold;
                            rowCur.Cells[j].CellStyle.SetFont(fontBold);
                        }
                        else
                        {
                            rowCur.Cells[j].CellStyle = styleBodySum;
                            rowCur.Cells[j].CellStyle.SetFont(fontBold);
                        }
                    }
                    else
                    {
                        rowCur.Cells[j].CellStyle = styleBody;
                    }
                }
            }
        }

        internal void InsertDataSanLuongTab4(IWorkbook templateWorkbook, ISheet sheet, IList<RevenueReportModelSL_Tra_Nap> dataDetails, int startRow, int NUM_CELL)
        {
            ICellStyle styleCellBold = templateWorkbook.CreateCellStyle();
            styleCellBold.CloneStyleFrom(sheet.GetRow(7).Cells[0].CellStyle);
            var fontBold = templateWorkbook.CreateFont();
            fontBold.Boldweight = (short)FontBoldWeight.Bold;
            fontBold.FontHeightInPoints = 11;
            fontBold.FontName = "Times New Roman";

            ICellStyle styleName = templateWorkbook.CreateCellStyle();
            styleName.CloneStyleFrom(sheet.GetRow(7).Cells[0].CellStyle);

            ICellStyle styleBody = templateWorkbook.CreateCellStyle();
            styleBody.CloneStyleFrom(sheet.GetRow(8).Cells[1].CellStyle);
            styleBody.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");
            ICellStyle styleBodySum = templateWorkbook.CreateCellStyle();
            styleBodySum.CloneStyleFrom(sheet.GetRow(8).Cells[2].CellStyle);
            styleBodySum.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");
            
            for (int i = 0; i < dataDetails.Count(); i++)
            {
                var dataRow = dataDetails[i];
                var space = new StringBuilder();
                for (int j = 0; j < dataDetails[i].Level; j++)
                {
                    space.Append("    ");
                }
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, NUM_CELL);
                rowCur.Cells[0].SetCellValue(space.ToString() + dataDetails[i].Name.ToString());
                rowCur.Cells[1].SetCellValue(dataDetails[i]?.Value1ND== null ? 0 : (double)dataDetails[i]?.Value1ND);
                rowCur.Cells[2].SetCellValue(dataDetails[i]?.Value2ND== null ? 0 : (double)dataDetails[i]?.Value2ND);
                rowCur.Cells[3].SetCellValue(dataDetails[i]?.Value3ND== null ? 0 : (double)dataDetails[i]?.Value3ND);
                rowCur.Cells[4].SetCellValue(dataDetails[i]?.Value4ND== null ? 0 : (double)dataDetails[i]?.Value4ND);
                rowCur.Cells[5].SetCellValue(dataDetails[i]?.Value5ND== null ? 0 : (double)dataDetails[i]?.Value5ND);
                rowCur.Cells[6].SetCellValue(dataDetails[i]?.Value6ND== null ? 0 : (double)dataDetails[i]?.Value6ND);
                rowCur.Cells[7].SetCellValue(dataDetails[i]?.Value7ND== null ? 0 : (double)dataDetails[i]?.Value7ND);
                rowCur.Cells[8].SetCellValue(dataDetails[i]?.Value8ND== null ? 0 : (double)dataDetails[i]?.Value8ND);
                rowCur.Cells[9].SetCellValue(dataDetails[i]?.Value9ND== null ? 0 : (double)dataDetails[i]?.Value9ND);
                rowCur.Cells[10].SetCellValue(dataDetails[i]?.Value10ND== null ? 0 : (double)dataDetails[i]?.Value10ND);
                rowCur.Cells[11].SetCellValue(dataDetails[i]?.Value11ND== null ? 0 : (double)dataDetails[i]?.Value11ND);
                rowCur.Cells[12].SetCellValue(dataDetails[i]?.Value12ND== null ? 0 : (double)dataDetails[i]?.Value12ND);
                rowCur.Cells[13].SetCellValue(dataDetails[i]?.Value1QT== null ? 0 : (double)dataDetails[i]?.Value1QT);
                rowCur.Cells[14].SetCellValue(dataDetails[i]?.Value2QT== null ? 0 : (double)dataDetails[i]?.Value2QT);
                rowCur.Cells[15].SetCellValue(dataDetails[i]?.Value3QT== null ? 0 : (double)dataDetails[i]?.Value3QT);
                rowCur.Cells[16].SetCellValue(dataDetails[i]?.Value4QT== null ? 0 : (double)dataDetails[i]?.Value4QT);
                rowCur.Cells[17].SetCellValue(dataDetails[i]?.Value5QT== null ? 0 : (double)dataDetails[i]?.Value5QT);
                rowCur.Cells[18].SetCellValue(dataDetails[i]?.Value6QT== null ? 0 : (double)dataDetails[i]?.Value6QT);
                rowCur.Cells[19].SetCellValue(dataDetails[i]?.Value7QT== null ? 0 : (double)dataDetails[i]?.Value7QT);
                rowCur.Cells[20].SetCellValue(dataDetails[i]?.Value8QT== null ? 0 : (double)dataDetails[i]?.Value8QT);
                rowCur.Cells[21].SetCellValue(dataDetails[i]?.Value9QT== null ? 0 : (double)dataDetails[i]?.Value9QT);
                rowCur.Cells[22].SetCellValue(dataDetails[i]?.Value10QT== null ? 0 : (double)dataDetails[i]?.Value10QT);
                rowCur.Cells[23].SetCellValue(dataDetails[i]?.Value11QT== null ? 0 : (double)dataDetails[i]?.Value11QT);
                rowCur.Cells[24].SetCellValue(dataDetails[i]?.Value12QT== null ? 0 : (double)dataDetails[i]?.Value12QT);
                rowCur.Cells[25].SetCellValue(dataDetails[i]?.ValueSumYearND== null ? 0 : (double)dataDetails[i]?.ValueSumYearND);
                rowCur.Cells[26].SetCellValue(dataDetails[i]?.ValueSumYearQT== null ? 0 : (double)dataDetails[i]?.ValueSumYearQT);
                rowCur.Cells[27].SetCellValue(dataDetails[i]?.ValueSumYearAll_ND_QT== null ? 0 : (double)dataDetails[i]?.ValueSumYearAll_ND_QT);


                for (int j = 0; j < NUM_CELL; j++)
                {
                    if (dataDetails[i].IsBold)
                    {
                        if(j == 0)
                        {
                            rowCur.Cells[j].CellStyle = styleCellBold;
                            rowCur.Cells[j].CellStyle.SetFont(fontBold);
                        }
                        else
                        {
                            rowCur.Cells[j].CellStyle = styleBodySum;
                            rowCur.Cells[j].CellStyle.SetFont(fontBold);
                        }
                    }
                    else
                    {
                        rowCur.Cells[j].CellStyle = styleBody;
                    }
                }
            }
        }
        internal void ExportExcelDoanhThu(ref MemoryStream outFileStream, string path, int year, string phienBan, string kichBan, string hangHangKhong)
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

                ICellStyle styleCellBold = templateWorkbook.CreateCellStyle();
                styleCellBold.CloneStyleFrom(sheet.GetRow(6).Cells[0].CellStyle);
                var fontBold = templateWorkbook.CreateFont();
                fontBold.Boldweight = (short)FontBoldWeight.Bold;
                fontBold.FontHeightInPoints = 11;
                fontBold.FontName = "Times New Roman";

                ICellStyle styleBody = templateWorkbook.CreateCellStyle();
                styleBody.CloneStyleFrom(sheet.GetRow(7).Cells[1].CellStyle);
                styleBody.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");
                ICellStyle styleBodySum = templateWorkbook.CreateCellStyle();
                styleBodySum.CloneStyleFrom(sheet.GetRow(7).Cells[0].CellStyle);
                styleBodySum.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");

                var data = GetDataDoanhThu(year, phienBan, kichBan, hangHangKhong);

                if (data.Count <= 1)
                {
                    this.State = false;
                    this.ErrorMessage = "Không có dữ liệu!";
                    return;
                }

                var startRow = 6;

                for (int i = 0; i < data.Count(); i++)
                {
                    var dataRow = data[i];
                    IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, 14);
                    rowCur.Cells[0].SetCellValue(data[i].Name.ToString());
                    rowCur.Cells[1].SetCellValue(data[i]?.ValueSumYear == null ? 0 : (double)data[i]?.ValueSumYear);
                    rowCur.Cells[2].SetCellValue(data[i]?.Value1 == null ? 0 : (double)data[i]?.Value1);
                    rowCur.Cells[3].SetCellValue(data[i]?.Value2 == null ? 0 : (double)data[i]?.Value2);
                    rowCur.Cells[4].SetCellValue(data[i]?.Value3 == null ? 0 : (double)data[i]?.Value3);
                    rowCur.Cells[5].SetCellValue(data[i]?.Value4 == null ? 0 : (double)data[i]?.Value4);
                    rowCur.Cells[6].SetCellValue(data[i]?.Value5 == null ? 0 : (double)data[i]?.Value5);
                    rowCur.Cells[7].SetCellValue(data[i]?.Value6 == null ? 0 : (double)data[i]?.Value6);
                    rowCur.Cells[8].SetCellValue(data[i]?.Value7 == null ? 0 : (double)data[i]?.Value7);
                    rowCur.Cells[9].SetCellValue(data[i]?.Value8 == null ? 0 : (double)data[i]?.Value8);
                    rowCur.Cells[10].SetCellValue(data[i]?.Value9 == null ? 0 : (double)data[i]?.Value9);
                    rowCur.Cells[11].SetCellValue(data[i]?.Value10 == null ? 0 : (double)data[i]?.Value10);
                    rowCur.Cells[12].SetCellValue(data[i]?.Value11 == null ? 0 : (double)data[i]?.Value11);
                    rowCur.Cells[13].SetCellValue(data[i]?.Value12 == null ? 0 : (double)data[i]?.Value12);
                    for (var j = 0; j <= 13; j++)
                    {
                        if (data[i].IsBold)
                        {
                            if (j == 0)
                            {
                                rowCur.Cells[j].CellStyle = styleCellBold;
                                rowCur.Cells[j].CellStyle.SetFont(fontBold);
                            }
                            else
                            {
                                rowCur.Cells[j].CellStyle = styleBodySum;
                                rowCur.Cells[j].CellStyle.SetFont(fontBold);
                            }
                        }
                        else
                        {
                            rowCur.Cells[j].CellStyle = styleBody;
                        }
                    }
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


        internal void ExportExcelDoanhThuTheoChiPhi(ref MemoryStream outFileStream, string path, int year, string phienBan, string kichBan, string hangHangKhong)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                IWorkbook templateWorkbook;
                templateWorkbook = new XSSFWorkbook(fs);
                fs.Close();
                //Doanh thu giá dịch vụ
                ISheet sheetTab1 = templateWorkbook.GetSheetAt(0);
                var data = GetDataDoanhThuTheoPhi(year, phienBan, kichBan, hangHangKhong);

                /*if (data.Tab1.Count <= 1 || data.Tab2.Count <= 1 || data.Tab3.Count <= 1 || data.Tab4.Count <= 1)
                {
                    this.State = false;
                    this.ErrorMessage = "Không có dữ liệu!";
                    return;
                }*/
                var startRow = 6;
                InsertDataToTableDTCP(templateWorkbook, sheetTab1, data.Tab1, startRow);
                // Phí ngầm
                ISheet sheetTab2 = templateWorkbook.GetSheetAt(1);
                InsertDataToTableDTCP(templateWorkbook, sheetTab2, data.Tab2, startRow);
                //Doanh thu tiền nhiên liệu
                ISheet sheetTab3 = templateWorkbook.GetSheetAt(2);
                InsertDataToTableDTCP(templateWorkbook, sheetTab3, data.Tab3, startRow);
                //Tổng doanh thu
                ISheet sheetTab4 = templateWorkbook.GetSheetAt(3);
                InsertDataToTableDTCP(templateWorkbook, sheetTab4, data.Tab4, startRow);

                templateWorkbook.Write(outFileStream);
            }
            catch (Exception ex)
            {
                this.State = false;
                this.ErrorMessage = "Có lỗi xẩy ra trong quá trình tạo file excel!";
                this.Exception = ex;
            }
        }

        internal void InsertDataToTableDTCP(IWorkbook templateWorkbook, ISheet sheet, IList<RevenueReportModel> dataDetails, int startRow)
        {
            ICellStyle styleCellBold = templateWorkbook.CreateCellStyle();
            styleCellBold.CloneStyleFrom(sheet.GetRow(6).Cells[0].CellStyle);
            styleCellBold.WrapText = true;
            var fontBold = templateWorkbook.CreateFont();
            fontBold.Boldweight = (short)FontBoldWeight.Bold;
            fontBold.FontHeightInPoints = 11;
            fontBold.FontName = "Times New Roman";

            ICellStyle styleBody = templateWorkbook.CreateCellStyle();
            styleBody.CloneStyleFrom(sheet.GetRow(7).Cells[0].CellStyle);
            styleBody.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");

            ICellStyle styleBodySum = templateWorkbook.CreateCellStyle();
            styleBodySum.CloneStyleFrom(sheet.GetRow(7).Cells[1].CellStyle);
            styleBodySum.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");
            for (int i = 0; i < dataDetails.Count(); i++)
            {
                var dataRow = dataDetails[i];
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, 14);
                rowCur.Cells[0].SetCellValue(dataDetails[i].Name.ToString());
                rowCur.Cells[1].SetCellValue(dataDetails[i]?.ValueSumYear == null ? 0 : (double)dataDetails[i]?.ValueSumYear);
                rowCur.Cells[2].SetCellValue(dataDetails[i]?.Value1 == null ? 0 : (double)dataDetails[i]?.Value1);
                rowCur.Cells[3].SetCellValue(dataDetails[i]?.Value2 == null ? 0 : (double)dataDetails[i]?.Value2);
                rowCur.Cells[4].SetCellValue(dataDetails[i]?.Value3 == null ? 0 : (double)dataDetails[i]?.Value3);
                rowCur.Cells[5].SetCellValue(dataDetails[i]?.Value4 == null ? 0 : (double)dataDetails[i]?.Value4);
                rowCur.Cells[6].SetCellValue(dataDetails[i]?.Value5 == null ? 0 : (double)dataDetails[i]?.Value5);
                rowCur.Cells[7].SetCellValue(dataDetails[i]?.Value6 == null ? 0 : (double)dataDetails[i]?.Value6);
                rowCur.Cells[8].SetCellValue(dataDetails[i]?.Value7 == null ? 0 : (double)dataDetails[i]?.Value7);
                rowCur.Cells[9].SetCellValue(dataDetails[i]?.Value8 == null ? 0 : (double)dataDetails[i]?.Value8);
                rowCur.Cells[10].SetCellValue(dataDetails[i]?.Value9 == null ? 0 : (double)dataDetails[i]?.Value9);
                rowCur.Cells[11].SetCellValue(dataDetails[i]?.Value10 == null ? 0 : (double)dataDetails[i]?.Value10);
                rowCur.Cells[12].SetCellValue(dataDetails[i]?.Value11 == null ? 0 : (double)dataDetails[i]?.Value11);
                rowCur.Cells[13].SetCellValue(dataDetails[i]?.Value12 == null ? 0 : (double)dataDetails[i]?.Value12);

                for (var j = 0; j <= 13; j++)
                {
                    if (dataDetails[i].IsBold)
                    {
                        if (j == 0)
                        {
                            rowCur.Cells[j].CellStyle = styleCellBold;
                            rowCur.Cells[j].CellStyle.SetFont(fontBold);
                        }
                        else
                        {
                            rowCur.Cells[j].CellStyle = styleBodySum;
                            rowCur.Cells[j].CellStyle.SetFont(fontBold);
                        }
                    }
                    else
                    {
                        rowCur.Cells[j].CellStyle = styleBody;
                    }
                }
            }
        }

        internal async Task<MemoryStream> ExportExcelTraNapCungUng(string path, int year, string phienBan, string kichBan, string hangHangKhong)
        {
            try
            {
                MemoryStream outFileStream = new MemoryStream();
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                IWorkbook templateWorkbook;
                templateWorkbook = new XSSFWorkbook(fs);
                fs.Close();
                //Sản Lượng theo tháng
                ISheet sheetTab1 = templateWorkbook.GetSheetAt(0);
                var data = await GetDataTraNapCungUng(year, phienBan, kichBan, hangHangKhong);
                var dataOrder = data.OrderBy(x => x.Order).ToList();
                var startRow = 7;
                var NUM_CELL = 13;
                InsertDataTraNapCungUng(templateWorkbook, sheetTab1, dataOrder, startRow, NUM_CELL);
                templateWorkbook.Write(outFileStream);
                return outFileStream;
            }
            catch (Exception ex)
            {
                this.State = false;
                this.ErrorMessage = "Có lỗi xẩy ra trong quá trình tạo file excel!";
                this.Exception = ex;
                return new MemoryStream();
            }
        }

        internal void InsertDataTraNapCungUng(IWorkbook templateWorkbook, ISheet sheet, IList<SupplyReportModel> dataDetails, int startRow, int NUM_CELL)
        {
            ICellStyle styleCellBold = templateWorkbook.CreateCellStyle();
            styleCellBold.CloneStyleFrom(sheet.GetRow(7).Cells[0].CellStyle);
            var fontBold = templateWorkbook.CreateFont();
            fontBold.Boldweight = (short)FontBoldWeight.Bold;
            fontBold.FontHeightInPoints = 11;
            fontBold.FontName = "Times New Roman";

            ICellStyle styleName = templateWorkbook.CreateCellStyle();
            styleName.CloneStyleFrom(sheet.GetRow(7).Cells[0].CellStyle);

            ICellStyle styleBody = templateWorkbook.CreateCellStyle();
            styleBody.CloneStyleFrom(sheet.GetRow(7).Cells[2].CellStyle);
            styleBody.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");

            ICellStyle styleBodySum = templateWorkbook.CreateCellStyle();
            styleBodySum.CloneStyleFrom(sheet.GetRow(7).Cells[1].CellStyle);
            styleBodySum.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");
            for (int i = 0; i < dataDetails.Count(); i++)
            {
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, NUM_CELL);
                rowCur.Cells[0].SetCellValue(dataDetails[i].Name.ToString());
                rowCur.Cells[1].SetCellValue(dataDetails[i]?.ValueSL== null ? 0 : (double)dataDetails[i]?.ValueSL);
                rowCur.Cells[2].SetCellValue(dataDetails[i]?.ValueDG== null ? 0 : (double)dataDetails[i]?.ValueDG);
                rowCur.Cells[3].SetCellValue(dataDetails[i]?.ValueMOPS== null ? 0 : (double)dataDetails[i]?.ValueMOPS);
                rowCur.Cells[4].SetCellValue(dataDetails[i]?.ValueTNK== null ? 0 : (double)dataDetails[i]?.ValueTNK);
                rowCur.Cells[5].SetCellValue(dataDetails[i]?.ValueD== null ? 0 : (double)dataDetails[i]?.ValueD);
                rowCur.Cells[6].SetCellValue(dataDetails[i]?.ValueFH== null ? 0 : (double)dataDetails[i]?.ValueFH);
                rowCur.Cells[7].SetCellValue(dataDetails[i]?.ValueThue== null ? 0 : (double)dataDetails[i]?.ValueThue);
                rowCur.Cells[8].SetCellValue(dataDetails[i]?.ValueDT== null ? 0 : (double)dataDetails[i]?.ValueDT);
                rowCur.Cells[9].SetCellValue(dataDetails[i]?.ValueDTMOPS == null ? 0 : (double)dataDetails[i]?.ValueDTMOPS);
                rowCur.Cells[10].SetCellValue(dataDetails[i]?.ValueDTTNK== null ? 0 : (double)dataDetails[i]?.ValueDTTNK);
                rowCur.Cells[11].SetCellValue(dataDetails[i]?.ValueDTD== null ? 0 : (double)dataDetails[i]?.ValueDTD);
                rowCur.Cells[12].SetCellValue(dataDetails[i]?.ValueDTFH== null ? 0 : (double)dataDetails[i]?.ValueDTFH);

                for (int j = 0; j < NUM_CELL; j++)
                {
                    if (dataDetails[i].IsBold)
                    {
                        if (j == 0)
                        {
                            rowCur.Cells[j].CellStyle = styleCellBold;
                            rowCur.Cells[j].CellStyle.SetFont(fontBold);
                        }
                        else
                        {
                            rowCur.Cells[j].CellStyle = styleBodySum;
                            rowCur.Cells[j].CellStyle.SetFont(fontBold);
                        }
                    }
                    else
                    {
                        if(j == 0)
                        {
                            rowCur.Cells[j].CellStyle = styleName;
                        }
                        else
                        {
                            rowCur.Cells[j].CellStyle = styleBody;
                        }
                    }
                }
            }
        }

        internal async Task<MemoryStream> ExportExcelTongHop(string path, int year, string phienBan, string kichBan, string area)
        {
            //try
            //{
            MemoryStream outFileStream = new MemoryStream();
            //    FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            //    IWorkbook templateWorkbook;
            //    templateWorkbook = new XSSFWorkbook(fs);
            //    fs.Close();

            //    var data = GetDataKeHoachTongHop(year, phienBan, kichBan, area);
            //    //San Luong
            //    var dataSL = data.SanLuong.OrderBy(x => x.Order).ToList();
            //    var dataDT = data.DauTu.OrderBy(x => x.Order).ToList();
            //    var dataSC = data.SuaChuaLon.OrderBy(x => x.Order).ToList();
            //    var dataCP = data.ChiPhi.OrderBy(x => x.Order).ToList();

            //    var countSL = dataSL.Count();
            //    var countDt = dataDT.Count();
            //    var countSC = dataSC.Count();
            //    var countCP = dataCP.Count();

            //    var stringHeaderSL = "I.KẾ HOẠCH SẢN LƯỢNG";
            //    var stringHeaderDT = "II.KẾ HOẠCH ĐẦU TƯ, MUA SẮM TRANG THIẾT BỊ";
            //    var stringHeaderSC = "III. KẾ HOẠCH SỬA CHỮA LỚN TÀI SẢN CỐ ĐỊNH";
            //    var stringHeaderCP = "IV. KẾ HOẠCH CHI PHÍ";

            //    var rowStartSL = 7;
            //    var rowStartDT = rowStartSL + countSL;
            //    var rowStartSC = rowStartDT + countDt + 4;
            //    var rowStartCP = rowStartSC + countSC +4;
            //    ISheet sheet = templateWorkbook.GetSheetAt(0);
            //    ICellStyle styleCellBold = templateWorkbook.CreateCellStyle();
            //    styleCellBold.CloneStyleFrom(sheet.GetRow(7).Cells[0].CellStyle);
            //    styleCellBold.WrapText = true;

            //    ICellStyle styleCellName = templateWorkbook.CreateCellStyle();
            //    styleCellName.CloneStyleFrom(sheet.GetRow(7).Cells[1].CellStyle);


            //    ICellStyle styleBodyBold = templateWorkbook.CreateCellStyle();
            //    styleBodyBold.CloneStyleFrom(sheet.GetRow(8).Cells[0].CellStyle);
            //    styleBodyBold.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");
            //    styleBodyBold.WrapText = true;

            //    ICellStyle styleBody = templateWorkbook.CreateCellStyle();
            //    styleBody.CloneStyleFrom(sheet.GetRow(8).Cells[1].CellStyle);
            //    styleBody.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");
            //    styleBody.WrapText = true;

            //    ICellStyle styleHeader = templateWorkbook.CreateCellStyle();
            //    styleHeader.CloneStyleFrom(sheet.GetRow(5).Cells[0].CellStyle);
            //    Task task1 = Task.Run(() =>
            //    {
            //        lock (lockObject)
            //        {
            //            var NUM_CELL = 8;
            //            InsertDataTongHopSL(templateWorkbook, sheet, dataSL, rowStartSL, NUM_CELL);
            //        }
            //    });
            //    //Dau Tu
            //    Task task2 = Task.Run(() =>
            //    {
            //        lock (lockObject)
            //        {
            //            var NUM_CELL = 8;
            //            InsertDataTongHopDT(templateWorkbook, sheet, dataDT, rowStartDT, NUM_CELL, styleCellBold, styleCellName, styleBodyBold, styleBody, styleHeader);
            //        }
            //    });
            //    //Sua Chua
            //    Task task3 = Task.Run(() =>
            //    {
            //        lock (lockObject)
            //        {
            //            var NUM_CELL = 6;
            //            InsertDataTongHopSC(templateWorkbook, sheet, dataSC, rowStartSC, NUM_CELL, styleCellBold, styleCellName, styleBodyBold, styleBody, styleHeader);
            //        }
            //    });
            //    //Chi phi
            //    Task task4 = Task.Run(() =>
            //    {
            //        lock (lockObject)
            //        {
            //            var NUM_CELL = 17;
            //            InsertDataTongHopCP(templateWorkbook, sheet, dataCP, rowStartCP, NUM_CELL, styleCellBold, styleCellName, styleBodyBold, styleBody, styleHeader);
            //        }
            //    });

            //    await Task.WhenAll(task1, task2, task3, task4);
            //    templateWorkbook.Write(outFileStream);
            return outFileStream;

            //}
            //catch (Exception ex)
            //{
            //    this.State = false;
            //    this.ErrorMessage = "Có lỗi xẩy ra trong quá trình tạo file excel!";
            //    this.Exception = ex;
            //    return new MemoryStream();
            //}

        }

        internal void InsertDataTongHopSL(IWorkbook templateWorkbook, ISheet sheet, List<SanLuong> dataDetails, int startRow, int NUM_CELL)
        {
            ICellStyle styleCellBold = templateWorkbook.CreateCellStyle();
            styleCellBold.CloneStyleFrom(sheet.GetRow(7).Cells[0].CellStyle);
            styleCellBold.WrapText = true;
            var fontBold = templateWorkbook.CreateFont();
            fontBold.Boldweight = (short)FontBoldWeight.Bold;
            fontBold.FontHeightInPoints = 11;
            fontBold.FontName = "Times New Roman";

            ICellStyle styleCellName = templateWorkbook.CreateCellStyle();
            styleCellName.CloneStyleFrom(sheet.GetRow(7).Cells[2].CellStyle);


            ICellStyle styleBodyBold = templateWorkbook.CreateCellStyle();
            styleBodyBold.CloneStyleFrom(sheet.GetRow(8).Cells[0].CellStyle);
            styleBodyBold.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");
            styleBodyBold.WrapText = true;

            ICellStyle styleBody = templateWorkbook.CreateCellStyle();
            styleBody.CloneStyleFrom(sheet.GetRow(8).Cells[1].CellStyle);
            styleBody.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");
            styleBody.WrapText = true;

            ICellStyle styleHeader = templateWorkbook.CreateCellStyle();
            styleHeader.CloneStyleFrom(sheet.GetRow(5).Cells[0].CellStyle);
            // Insert Header
            var stringHeaderSL = "I.KẾ HOẠCH SẢN LƯỢNG";
            var startHeader = 4;
            for(int i = 0; i < 3; i++)
            {
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startHeader++, NUM_CELL);
                if(i == 0)
                {
                    rowCur.Cells[0].SetCellValue(stringHeaderSL);
                    for(int j = 0; j < NUM_CELL; j++)
                    {
                        rowCur.Cells[j].CellStyle = styleHeader;
                    }

                }
                else if( i == 1)
                {
                    rowCur.Cells[0].SetCellValue("STT");
                    rowCur.Cells[1].SetCellValue("SẢN LƯỢNG TRA NẠP THEO SÂN BAY");
                    rowCur.Cells[3].SetCellValue("HKVN");
                    rowCur.Cells[6].SetCellValue("HKQT");
                    rowCur.Cells[7].SetCellValue("CỘNG");
                }
                else
                {
                    rowCur.Cells[3].SetCellValue("VNA");
                    rowCur.Cells[4].SetCellValue("HKVN#");
                    rowCur.Cells[5].SetCellValue("TỔNG");
                    sheet.AddMergedRegion(new CellRangeAddress(5, 6, 0, 0));
                    sheet.AddMergedRegion(new CellRangeAddress(5, 6, 1, 2));
                    sheet.AddMergedRegion(new CellRangeAddress(5, 5, 3, 5));
                    sheet.AddMergedRegion(new CellRangeAddress(5, 6, 6, 6));
                    sheet.AddMergedRegion(new CellRangeAddress(5, 6, 7, 7));
                    sheet.AddMergedRegion(new CellRangeAddress(4, 4, 0, 7));
                }
            }
            for (int i = 0; i < dataDetails.Count(); i++)
            {
                var dataRow = dataDetails[i];
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, NUM_CELL);
                rowCur.Cells[0].SetCellValue(dataRow.Order);
                rowCur.Cells[1].SetCellValue(dataRow.Name);
                rowCur.Cells[2].SetCellValue(dataRow.Code);
                rowCur.Cells[3].SetCellValue((double)(dataRow.Value1));
                rowCur.Cells[4].SetCellValue((double)dataRow.Value2);
                rowCur.Cells[5].SetCellValue((double)dataRow.Value3);
                rowCur.Cells[6].SetCellValue((double)dataRow.Value4);
                rowCur.Cells[7].SetCellValue((double)dataRow.Value5);
                for(int j = 0; j < NUM_CELL; j++)
                {
                    if (dataRow.IsBold)
                    {
                        if(j == 1 || j == 2)
                        {
                            rowCur.Cells[j].CellStyle = styleCellBold;
                            rowCur.Cells[j].CellStyle.SetFont(fontBold);
                        }
                        else
                        {
                            rowCur.Cells[j].CellStyle = styleBodyBold;
                            rowCur.Cells[j].CellStyle.SetFont(fontBold);
                        }
                    }
                    else
                    {
                        if(j == 1 || j == 2)
                        {
                            rowCur.Cells[j].CellStyle = styleCellName;
                        }
                        else
                        {
                            rowCur.Cells[j].CellStyle = styleBody;
                        }
                    }
                }
            }
        }

        internal void InsertDataTongHopDT(IWorkbook templateWorkbook, ISheet sheet, List<DauTu> dataDetails, int startRow, int NUM_CELL,ICellStyle  styleCellBold, ICellStyle styleCellName, ICellStyle styleBodyBold, ICellStyle styleBody, ICellStyle styleHeader )
        {
            var fontBold = templateWorkbook.CreateFont();
            fontBold.Boldweight = (short)FontBoldWeight.Bold;
            fontBold.FontHeightInPoints = 11;
            fontBold.FontName = "Times New Roman";
            var stringHeader = "II.KẾ HOẠCH ĐẦU TƯ, MUA SẮM TRANG THIẾT BỊ";
            var startHeader = startRow + 2;

            var title = startHeader;
            for (int i = 0; i < 2; i++)
            {
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startHeader++, NUM_CELL);
                if (i == 0)
                {
                    rowCur.Cells[0].SetCellValue(stringHeader);
                    for (int j = 0; j < NUM_CELL; j++)
                    {
                        rowCur.Cells[j].CellStyle = styleHeader;
                    }

                }
                else
                {
                    rowCur.Cells[0].SetCellValue("STT");
                    rowCur.Cells[1].SetCellValue("DANH MỤC ĐẦU TƯ MUA SẮM");
                    rowCur.Cells[2].SetCellValue("NGUỒN VỐN");
                    rowCur.Cells[3].SetCellValue("TỔNG VỐN ĐẦU TƯ GIAI ĐOẠN");
                    rowCur.Cells[4].SetCellValue("KẾ HOẠCH KINH PHÍ ĐẦU TƯ NĂM");
                    rowCur.Cells[5].SetCellValue("TIẾN ĐỘ TRIỂN KHAI GIAI ĐOẠN ĐẦU TƯ");
                    rowCur.Cells[6].SetCellValue("DỮ LIỆU THỰC HIỆN");
                    rowCur.Cells[7].SetCellValue("GHI CHÚ");

                    for(int j = 0; j < NUM_CELL; j++)
                    {
                        rowCur.Cells[j].CellStyle = styleHeader;
                    }

                    sheet.AddMergedRegion(new CellRangeAddress(title, title, 0, 7));
                }
            }

            startRow = startHeader;
            for (int i = 0; i < dataDetails.Count(); i++)
            {
                var dataRow = dataDetails[i];
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, NUM_CELL);
                rowCur.Cells[0].SetCellValue(dataRow.Order);
                rowCur.Cells[1].SetCellValue(dataRow.Name);
                rowCur.Cells[2].SetCellValue(dataRow.Value1);
                rowCur.Cells[3].SetCellValue((double)dataRow.Value2);
                rowCur.Cells[4].SetCellValue((double)dataRow.Value3);
                rowCur.Cells[5].SetCellValue(dataRow.Value4);
                rowCur.Cells[6].SetCellValue((double)dataRow.ValueDLTH);
                rowCur.Cells[7].SetCellValue(dataRow.Value5);
                for (int j = 0; j < NUM_CELL; j++)
                {
                    if (dataRow.IsBold)
                    {
                        if(j == 1 || j == 5 || j == 7)
                        {
                            rowCur.Cells[j].CellStyle = styleCellBold;
                            rowCur.Cells[j].CellStyle.SetFont(fontBold);
                        }
                        else
                        {
                            rowCur.Cells[j].CellStyle = styleBodyBold;
                            rowCur.Cells[j].CellStyle.SetFont(fontBold);
                        }
                    }
                    else
                    {
                        if (j == 1 || j == 5 || j == 7)
                        {
                            rowCur.Cells[j].CellStyle = styleCellName;
                        }
                        else
                        {
                            rowCur.Cells[j].CellStyle = styleBody;
                        }
                    }
                }
            }
        }

        internal void InsertDataTongHopSC(IWorkbook templateWorkbook, ISheet sheet, List<SuaChuaLon> dataDetails, int startRow, int NUM_CELL, ICellStyle styleCellBold, ICellStyle styleCellName, ICellStyle styleBodyBold, ICellStyle styleBody, ICellStyle styleHeader)
        {
            var fontBold = templateWorkbook.CreateFont();
            fontBold.Boldweight = (short)FontBoldWeight.Bold;
            fontBold.FontHeightInPoints = 11;
            fontBold.FontName = "Times New Roman";
            var stringHeader = "III.KẾ HOẠCH SỬA CHỮA LỚN TÀI SẢN CỐ ĐỊNH";
            var startHeader = startRow + 2;

            var title = startHeader;
            for (int i = 0; i < 2; i++)
            {
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startHeader++, NUM_CELL);
                if (i == 0)
                {
                    rowCur.Cells[0].SetCellValue(stringHeader);
                    for (int j = 0; j < NUM_CELL; j++)
                    {
                        rowCur.Cells[j].CellStyle = styleHeader;
                    }

                }
                else
                {
                    rowCur.Cells[0].SetCellValue("STT");
                    rowCur.Cells[1].SetCellValue("DANH MỤC SỬA CHỮA LỚN");
                    rowCur.Cells[2].SetCellValue("KẾ HOẠCH KINH PHÍ SỬA CHỮA NĂM 2024");
                    rowCur.Cells[3].SetCellValue("QUY MÔ SỬA CHỮA");
                    rowCur.Cells[4].SetCellValue("DỮ LIỆU THỰC HIỆN");
                    rowCur.Cells[5].SetCellValue("GHI CHÚ");


                    for (int j = 0; j < NUM_CELL; j++)
                    {
                        rowCur.Cells[j].CellStyle = styleHeader;
                    }

                    sheet.AddMergedRegion(new CellRangeAddress(title, title, 0, 5));
                }
            }

            startRow = startHeader;
            for (int i = 0; i < dataDetails.Count(); i++)
            {
                var dataRow = dataDetails[i];
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, NUM_CELL);
                rowCur.Cells[0].SetCellValue(dataRow.Order);
                rowCur.Cells[1].SetCellValue(dataRow.name);
                rowCur.Cells[2].SetCellValue(dataRow.valueKP == null ? 0: Convert.ToDouble(dataRow.valueKP));
                rowCur.Cells[3].SetCellValue(dataRow.valueQM);
                rowCur.Cells[4].SetCellValue(dataRow.valueDLTH == null ? 0 : Convert.ToDouble(dataRow.valueDLTH));
                rowCur.Cells[5].SetCellValue(dataRow.des);

                for (int j = 0; j < NUM_CELL; j++)
                {
                    if (dataRow.IsBold)
                    {
                        if (j == 1 || j == 5 || j == 7)
                        {
                            rowCur.Cells[j].CellStyle = styleCellBold;
                            rowCur.Cells[j].CellStyle.SetFont(fontBold);
                        }
                        else
                        {
                            rowCur.Cells[j].CellStyle = styleBodyBold;
                            rowCur.Cells[j].CellStyle.SetFont(fontBold);
                        }
                    }
                    else
                    {
                        if (j == 1 || j == 5 || j == 7)
                        {
                            rowCur.Cells[j].CellStyle = styleCellName;
                        }
                        else
                        {
                            rowCur.Cells[j].CellStyle = styleBody;
                        }
                    }
                }
            }
        }


        internal void InsertDataTongHopCP(IWorkbook templateWorkbook, ISheet sheet, List<ChiPhi> dataDetails, int startRow, int NUM_CELL, ICellStyle styleCellBold, ICellStyle styleCellName, ICellStyle styleBodyBold, ICellStyle styleBody, ICellStyle styleHeader)
        {
            var fontBold = templateWorkbook.CreateFont();
            fontBold.Boldweight = (short)FontBoldWeight.Bold;
            fontBold.FontHeightInPoints = 11;
            fontBold.FontName = "Times New Roman";
            var stringHeader = "IV. KẾ HOẠCH CHI PHÍ";
            var startHeader = startRow + 2;

            var title = startHeader;
            for (int i = 0; i < 3; i++)
            {
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startHeader++, NUM_CELL);
                if (i == 0)
                {
                    rowCur.Cells[0].SetCellValue(stringHeader);
                    for (int j = 0; j < NUM_CELL; j++)
                    {
                        rowCur.Cells[j].CellStyle = styleHeader;
                    }

                }
                else if (i == 1)
                {
                    rowCur.Cells[0].SetCellValue("STT");
                    rowCur.Cells[1].SetCellValue("DANH MỤC CHI PHÍ");
                    rowCur.Cells[2].SetCellValue("KẾ HOẠCH NĂM 2024");
                    rowCur.Cells[4].SetCellValue("THÁNG 1");
                    rowCur.Cells[5].SetCellValue("THÁNG 2");
                    rowCur.Cells[6].SetCellValue("THÁNG 3");
                    rowCur.Cells[7].SetCellValue("THÁNG 4");
                    rowCur.Cells[8].SetCellValue("THÁNG 5");
                    rowCur.Cells[9].SetCellValue("THÁNG 6");
                    rowCur.Cells[10].SetCellValue("THÁNG 7");
                    rowCur.Cells[11].SetCellValue("THÁNG 8");
                    rowCur.Cells[12].SetCellValue("THÁNG 9");
                    rowCur.Cells[13].SetCellValue("THÁNG 10");
                    rowCur.Cells[14].SetCellValue("THÁNG 11");
                    rowCur.Cells[15].SetCellValue("THÁNG 12");
                    rowCur.Cells[16].SetCellValue("GHI CHÚ");

                    for (int j = 0; j < NUM_CELL; j++)
                    {
                        rowCur.Cells[j].CellStyle = styleHeader;
                    }
                }
                else
                {
                    rowCur.Cells[2].SetCellValue("CHI PHÍ");
                    rowCur.Cells[3].SetCellValue("ĐƠN GIÁ CHI PHÍ TRÊN 1 TẤN NHIÊN LIỆU");
                    for (int j = 0; j < NUM_CELL; j++)
                    {
                        rowCur.Cells[j].CellStyle = styleHeader;
                    }
                    sheet.AddMergedRegion(new CellRangeAddress(title, title, 0, 16));
                    sheet.AddMergedRegion(new CellRangeAddress(title+1, title+1, 2, 3));
                    for (int j = 0; j < NUM_CELL; j++)
                    {
                        if (j != 2 && j != 3)
                        {
                            sheet.AddMergedRegion(new CellRangeAddress(title+1, title+2, j, j));
                        }
                    }
                }

            }

            startRow = startHeader;
            for (int i = 0; i < dataDetails.Count(); i++)
            {
                var dataRow = dataDetails[i];
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, NUM_CELL);
                rowCur.Cells[0].SetCellValue(dataRow.Order);
                rowCur.Cells[1].SetCellValue(dataRow.name);
                rowCur.Cells[2].SetCellValue(Convert.ToDouble(dataRow.price));
                rowCur.Cells[3].SetCellValue(Convert.ToDouble(dataRow.valueCP));
                rowCur.Cells[4].SetCellValue(Convert.ToDouble(dataRow.Value1));
                rowCur.Cells[5].SetCellValue(Convert.ToDouble(dataRow.Value2));
                rowCur.Cells[6].SetCellValue(Convert.ToDouble(dataRow.Value3));
                rowCur.Cells[7].SetCellValue(Convert.ToDouble(dataRow.Value4));
                rowCur.Cells[8].SetCellValue(Convert.ToDouble(dataRow.Value5));
                rowCur.Cells[9].SetCellValue(Convert.ToDouble(dataRow.Value6));
                rowCur.Cells[10].SetCellValue(Convert.ToDouble(dataRow.Value7));
                rowCur.Cells[11].SetCellValue(Convert.ToDouble(dataRow.Value8));
                rowCur.Cells[12].SetCellValue(Convert.ToDouble(dataRow.Value9));
                rowCur.Cells[13].SetCellValue(Convert.ToDouble(dataRow.Value10));
                rowCur.Cells[14].SetCellValue(Convert.ToDouble(dataRow.Value11));
                rowCur.Cells[15].SetCellValue(Convert.ToDouble(dataRow.Value12));
                rowCur.Cells[16].SetCellValue(Convert.ToDouble(dataRow.description));

                for (int j = 0; j < NUM_CELL; j++)
                {
                    if (dataRow.IsBold)
                    {
                        if (j == 1 || j == NUM_CELL)
                        {
                            rowCur.Cells[j].CellStyle = styleCellBold;
                            rowCur.Cells[j].CellStyle.SetFont(fontBold);
                        }
                        else
                        {
                            rowCur.Cells[j].CellStyle = styleBodyBold;
                            rowCur.Cells[j].CellStyle.SetFont(fontBold);
                        }
                    }
                    else
                    {
                        if (j == 1 || j == NUM_CELL)
                        {
                            rowCur.Cells[j].CellStyle = styleCellName;
                        }
                        else
                        {
                            rowCur.Cells[j].CellStyle = styleBody;
                        }
                    }
                }
            }
        }
        internal List<SuaChuaLon> GetReportDataSuaChuaLon(int year, string phienBan, string kichBan, string area)
        {
            var data = new List<SuaChuaLon>();
            var headerSCL = UnitOfWork.Repository<SuaChuaLonRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == kichBan && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
            var dataSCL = UnitOfWork.Repository<SuaChuaLonDataRepo>().Queryable().Where(x => headerSCL.Contains(x.TEMPLATE_CODE)).ToList();

            if (string.IsNullOrEmpty(area))
            {
                data.AddRange(GetDataSCLByArea("MB", year, dataSCL));
                data.AddRange(GetDataSCLByArea("MT", year, dataSCL));
                data.AddRange(GetDataSCLByArea("MN", year, dataSCL));
                data.AddRange(GetDataSCLByArea("CQ", year, dataSCL));
                data.AddRange(GetDataSCLByArea("VT", year, dataSCL));
            }
            else
            {
                data.AddRange(GetDataSCLByArea(area, year, dataSCL));
            }
            return data;
        }
        internal List<SuaChuaLonReportModel> GetReportDataSuaChuaLonByArea(int year, string phienBan, string kichBan, string area)
        {
            var data = new List<SuaChuaLonReportModel>();
            Dictionary<string, string> ChiNhanh = new Dictionary<string, string>()
                {
                    {"", "" },
                    {"MB", "CNMB" },
                    {"MN", "CNMN" },
                    {"MT", "CNMT" },
                    {"VT", "CNVT" },
                    {"CQ", "CQCT" }
                };

            var sapCode = ChiNhanh[area];
            var costCenter = UnitOfWork.Repository<CostCenterRepo>().Queryable().FirstOrDefault(x => x.SAP_CODE == sapCode).CODE;

            var lstCostCenterChild = UnitOfWork.Repository<CostCenterRepo>().Queryable().Where(x => x.PARENT_CODE == costCenter).Select(x => x.CODE).ToList();
            // Lấy template theo costCenter
            var dataHeaderSCL = !string.IsNullOrEmpty(area) ? UnitOfWork.Repository<SuaChuaLonRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == kichBan && x.STATUS == "03" && lstCostCenterChild.Contains(x.ORG_CODE)).Select(x => x.TEMPLATE_CODE).ToList()
                                                        : UnitOfWork.Repository<SuaChuaLonRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == kichBan && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
            if (dataHeaderSCL.Count() == 0)
            {
                return data;
            }
            var order = 0;
            var dataInHeaderSCL = UnitOfWork.Repository<SuaChuaLonDataRepo>().Queryable().Where(x => x.TIME_YEAR == year && dataHeaderSCL.Contains(x.TEMPLATE_CODE)).ToList();
            if (string.IsNullOrEmpty(area))
            {
                var sumSCL = new SuaChuaLonReportModel
                {
                    Name = "TỔNG CỘNG TOÀN CÔNG TY",
                    valueGT = dataInHeaderSCL.Sum(x => x.VALUE) ?? 0,
                    Order = order,
                    IsBold = true
                };
                var parentCode = string.Empty;

                List<string> lstParentCode = new List<string>();
                //Lấy danh sashc khoản mục cha
                foreach (var item in dataInHeaderSCL)
                {
                    var parent = item.KhoanMucSuaChua.PARENT_CODE;
                    lstParentCode.Add(parent);
                }
                lstParentCode = lstParentCode.Distinct().ToList();
                foreach (var code in lstParentCode)
                {
                    var name = UnitOfWork.Repository<KhoanMucSuaChuaRepo>().Queryable().FirstOrDefault(x => x.CODE == code).NAME;
                    var sumItem = new SuaChuaLonReportModel
                    {
                        Name = name,
                        valueGT = dataInHeaderSCL.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE.Contains(code)).Sum(x => x.VALUE) ?? 0,
                        Order = order + 1,
                        Parent = "0"
                    };
                    data.Add(sumItem);
                    order++;
                }
                data.Add(sumSCL);
            }
            // Các chi nhánh
            Dictionary<string, string> ChiNhanhKhac = new Dictionary<string, string>()
                {
                    {"MB", "CNMB" },
                    {"MN", "CNMN" },
                    {"MT", "CNMT" },
                    {"VT", "CNVT" },
                    {"CQ", "CQCT" }
                };
            Dictionary<string, string> TenChiNhanh = new Dictionary<string, string>()
                {
                    {"MB", "CHI NHÁNH KHU VỰC MIỀN BẮC" },
                    {"MN", "CHI NHÁNH KHU VỰC MIỀN NAM" },
                    {"MT", "CHI NHÁNH KHU VỰC MIỀN TRUNG" },
                    {"VT", "CHI NHÁNH VẬN TẢI" },
                    {"CQ", "CƠ QUAN CÔNG TY" },
                };
            if (string.IsNullOrEmpty(area))
            {
                foreach (var cn in ChiNhanhKhac)
                {
                    var sapCode2 = ChiNhanhKhac[cn.Key];
                    var costCenter2 = UnitOfWork.Repository<CostCenterRepo>().Queryable().FirstOrDefault(x => x.SAP_CODE == sapCode2).CODE;

                    var lstCostCenterChild2 = UnitOfWork.Repository<CostCenterRepo>().Queryable().Where(x => x.PARENT_CODE == costCenter2).Select(x => x.CODE).ToList();
                    // Lấy template theo costCenter
                    var dataHeaderSCL2 = UnitOfWork.Repository<SuaChuaLonRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == kichBan && x.STATUS == "03" && lstCostCenterChild2.Contains(x.ORG_CODE)).Select(x => x.TEMPLATE_CODE).ToList();
                    if (dataHeaderSCL2.Count() == 0)
                    {
                        continue;
                    }
                    var dataInHeaderSCL2 = UnitOfWork.Repository<SuaChuaLonDataRepo>().Queryable().Where(x => x.TIME_YEAR == year && dataHeaderSCL2.Contains(x.TEMPLATE_CODE)).ToList();
                    var name = TenChiNhanh[cn.Key];
                    var sumSCL2 = new SuaChuaLonReportModel
                    {
                        Name = name,
                        valueGT = dataInHeaderSCL2.Sum(x => x.VALUE) ?? 0,
                        Order = order + 1,
                        IsBold = true,
                        Parent = "0"
                    };
                    data.Add(sumSCL2);
                    order++;
                    List<string> lstParentCodeArea = new List<string>();
                    foreach (var item in dataInHeaderSCL2)
                    {
                        var prCode = item.KhoanMucSuaChua.PARENT_CODE;
                        lstParentCodeArea.Add(prCode);
                    }
                    lstParentCodeArea = lstParentCodeArea.Distinct().ToList();
                    var parentOrder = order;
                    foreach (var code in lstParentCodeArea)
                    {
                        var nameElement = UnitOfWork.Repository<KhoanMucSuaChuaRepo>().Queryable().FirstOrDefault(x => x.CODE == code).NAME;
                        var sumElement = new SuaChuaLonReportModel
                        {
                            Name = nameElement,
                            valueGT = dataInHeaderSCL2.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE.Contains(code)).Sum(x => x.VALUE) ?? 0,
                            Order = order + 1,
                            IsBold = true,
                            Parent = parentOrder.ToString()
                        };
                        data.Add(sumElement);
                        order++;
                        var dataElement = dataInHeaderSCL2.Where(x => x.KhoanMucSuaChua.PARENT_CODE == code).ToList();
                        var parentOderChild = order;
                        foreach (var element in dataElement)
                        {
                            var elementItem = new SuaChuaLonReportModel
                            {
                                Name = element.KhoanMucSuaChua.NAME,
                                valueGT = dataElement.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE == element.KHOAN_MUC_SUA_CHUA_CODE).Sum(x => x.VALUE) ?? 0,
                                Order = order + 1,
                                Parent = parentOderChild.ToString()
                            };
                            data.Add(elementItem);
                            order++;
                        }
                    }
                    order++;
                }
            }
            else
            {
                var sapCode2 = ChiNhanhKhac[area];
                var costCenter2 = UnitOfWork.Repository<CostCenterRepo>().Queryable().FirstOrDefault(x => x.SAP_CODE == sapCode2).CODE;

                var lstCostCenterChild2 = UnitOfWork.Repository<CostCenterRepo>().Queryable().Where(x => x.PARENT_CODE == costCenter2).Select(x => x.CODE).ToList();
                // Lấy template theo costCenter
                var dataHeaderSCL2 = UnitOfWork.Repository<SuaChuaLonRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == kichBan && x.STATUS == "03" && lstCostCenterChild2.Contains(x.ORG_CODE)).Select(x => x.TEMPLATE_CODE).ToList();
                if (dataHeaderSCL2.Count() == 0)
                {
                    return data;
                }
                var dataInHeaderSCL2 = UnitOfWork.Repository<SuaChuaLonDataRepo>().Queryable().Where(x => x.TIME_YEAR == year && dataHeaderSCL2.Contains(x.TEMPLATE_CODE)).ToList();
                var name = TenChiNhanh[area];
                var sumSCL2 = new SuaChuaLonReportModel
                {
                    Name = name,
                    valueGT = dataInHeaderSCL2.Sum(x => x.VALUE) ?? 0,
                    Order = order + 1,
                    IsBold = true
                };
                data.Add(sumSCL2);
                order++;
                List<string> lstParentCodeArea = new List<string>();
                foreach (var item in dataInHeaderSCL2)
                {
                    var prCode = item.KhoanMucSuaChua.PARENT_CODE;
                    lstParentCodeArea.Add(prCode);
                }
                lstParentCodeArea = lstParentCodeArea.Distinct().ToList();
                var parentOrder = order;
                int sttParent = 1;
                foreach (var code in lstParentCodeArea)
                {
                    if (string.IsNullOrEmpty(code))
                    {
                        continue;
                    }
                    var nameElement = UnitOfWork.Repository<KhoanMucSuaChuaRepo>().Queryable().FirstOrDefault(x => x.CODE == code).NAME;
                    var sumElement = new SuaChuaLonReportModel
                    {
                        Stt = UtilsCore.IntToRoman(sttParent),
                        Name = nameElement,
                        valueGT = dataInHeaderSCL2.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE.Contains(code)).Sum(x => x.VALUE) ?? 0,
                        Order = order + 1,
                        Parent = parentOrder.ToString(),
                        IsBold = true
                    };
                    data.Add(sumElement);
                    sttParent++;
                    order++;
                    var lstChildCode = dataInHeaderSCL2.Where(x=>x.KhoanMucSuaChua.PARENT_CODE == code).Select(x => x.KHOAN_MUC_SUA_CHUA_CODE).Distinct().ToList();
                    var dataElement = dataInHeaderSCL2.Where(x => x.KhoanMucSuaChua.PARENT_CODE == code).ToList();
                    var parentOrderChild = order;
                    int sttChild = 1;
                    dataElement = dataElement.Distinct().ToList();
                    foreach (var element in lstChildCode)
                    {
                        var elementItem = new SuaChuaLonReportModel
                        {
                            Stt = sttChild.ToString(),
                            Name = dataElement.FirstOrDefault(x=>x.KHOAN_MUC_SUA_CHUA_CODE == element)?.KhoanMucSuaChua?.NAME,
                            valueGT = dataElement.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE == element).Sum(x => x.VALUE) ?? 0,
                            Order = order + 1,
                            Parent = parentOrderChild.ToString()
                        };
                        if(elementItem.valueGT != 0)
                        {
                            data.Add(elementItem);
                        }
                        order++;
                        sttChild++;
                    }
                }
            }
            return data;
        }

        internal List<SuaChuaThuongXuyenReportModel> GetReportDataSuaChuaThuongXuyen(int year, string phienBan, string kichBan, string area)
        {
            var data = new List<SuaChuaThuongXuyenReportModel>();
            var headerSCL = UnitOfWork.Repository<SuaChuaThuongXuyenRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == kichBan && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
            var dataSCL = UnitOfWork.Repository<SuaChuaThuongXuyenDataRepo>().Queryable().Where(x => headerSCL.Contains(x.TEMPLATE_CODE)).ToList();
            data.Add(new SuaChuaThuongXuyenReportModel
            {
                Name = "TỔNG CỘNG TOÀN CÔNG TY",
                valueGT = dataSCL.Sum(x => x.VALUE) ?? 0,
                IsBold = true
            });
            var elementCodes = dataSCL.Select(x => x.KHOAN_MUC_SUA_CHUA_CODE).Distinct().ToList();
            var elementChild = UnitOfWork.Repository<KhoanMucSuaChuaRepo>().Queryable().Where(x => x.TIME_YEAR == year && elementCodes.Contains(x.CODE)).ToList();
            foreach (var i in elementChild.Select(x => x.PARENT_CODE).Distinct().ToList())
            {
                var p = UnitOfWork.Repository<KhoanMucSuaChuaRepo>().Queryable().FirstOrDefault(x => x.TIME_YEAR == year && x.CODE == i);
                if (p != null)
                {
                    data.Add(new SuaChuaThuongXuyenReportModel
                    {
                        Stt = "-",
                        Name = p.NAME,
                        valueGT = dataSCL.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE.Contains(p.CODE)).Sum(x => x.VALUE) ?? 0
                    });
                }
            }

            if (string.IsNullOrEmpty(area))
            {
                data.AddRange(GetDataSCTXByArea("MB", year, dataSCL));
                data.AddRange(GetDataSCTXByArea("MT", year, dataSCL));
                data.AddRange(GetDataSCTXByArea("MN", year, dataSCL));
                data.AddRange(GetDataSCTXByArea("CQ", year, dataSCL));
                data.AddRange(GetDataSCTXByArea("VT", year, dataSCL));
            }
            else
            {
                data.AddRange(GetDataSCTXByArea(area, year, dataSCL));
            }
            return data;
        }

        public MemoryStream ExportExcelSuaChuaLon(string path, int year, string phienBan, string kichBan, string area)
        {
            MemoryStream outFileStream = new MemoryStream();
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            IWorkbook templateWorkbook;
            templateWorkbook = new XSSFWorkbook(fs);
            fs.Close();
            var data = GetReportDataSuaChuaLon(year, phienBan, kichBan, area);
            ISheet sheet = templateWorkbook.GetSheetAt(0);
            var dataSL = data.OrderBy(x => x.Order).ToList();
            var startRow = 6;
            var NUM_CELL = 5;
            InsertDataToTableSuaChuaLon(templateWorkbook, sheet, dataSL, startRow, NUM_CELL);
            templateWorkbook.Write(outFileStream);
            return outFileStream;
        }

        internal void InsertDataToTableSuaChuaLon(IWorkbook templateWorkbook, ISheet sheet, List<SuaChuaLon> dataDetails, int startRow, int NUM_CELL)
        {
            ICellStyle styleCellBold = templateWorkbook.CreateCellStyle();
            styleCellBold.CloneStyleFrom(sheet.GetRow(6).Cells[0].CellStyle);
            styleCellBold.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");

            var fontBold = templateWorkbook.CreateFont();
            fontBold.Boldweight = (short)FontBoldWeight.Bold;
            fontBold.FontHeightInPoints = 11;
            fontBold.FontName = "Times New Roman";

            ICellStyle styleNumber = templateWorkbook.CreateCellStyle();
            styleNumber.CloneStyleFrom(sheet.GetRow(6).Cells[1].CellStyle);

            ICellStyle styleNumberBold = templateWorkbook.CreateCellStyle();
            styleNumberBold.CloneStyleFrom(sheet.GetRow(6).Cells[2].CellStyle);
            styleNumberBold.Alignment = HorizontalAlignment.Right;

            ICellStyle styleBody = templateWorkbook.CreateCellStyle();
            styleBody.CloneStyleFrom(sheet.GetRow(7).Cells[0].CellStyle);
            styleBody.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");
            styleBody.WrapText = true;
            styleCellBold.WrapText = true;

            foreach (var item in dataDetails.OrderBy(x => x.Order))
            {
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, NUM_CELL);
                rowCur.Cells[0].SetCellValue(item.stt);
                rowCur.Cells[1].SetCellValue(item.name);
                rowCur.Cells[2].SetCellValue((double)item.valueKP);
                rowCur.Cells[3].SetCellValue(item.valueQM);
                rowCur.Cells[4].SetCellValue(item.des);
                if (item.IsBold)
                {
                    rowCur.Cells[0].CellStyle = styleCellBold;
                    rowCur.Cells[1].CellStyle = styleCellBold;
                    rowCur.Cells[2].CellStyle = styleNumberBold;
                    rowCur.Cells[3].CellStyle = styleCellBold;
                    rowCur.Cells[4].CellStyle = styleCellBold;
                }
                else
                {
                    rowCur.Cells[0].CellStyle = styleBody;
                    rowCur.Cells[1].CellStyle = styleBody;
                    rowCur.Cells[2].CellStyle = styleNumber;
                    rowCur.Cells[3].CellStyle = styleNumber;
                    rowCur.Cells[4].CellStyle = styleNumber;
                }
            }
        }
        
        internal ReportChiPhiModel GetReportDataChiPhi(int year, string phienBan, string kichBan)
        {
            try
            {
                var data = new ReportChiPhiModel();
                var header = UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.PHIEN_BAN == phienBan && x.KICH_BAN == kichBan && x.STATUS == "03" && x.TIME_YEAR == year).Select(x=>x.TEMPLATE_CODE).ToList();
                var dataInHeaderCP = UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => x.TIME_YEAR == year && header.Contains(x.TEMPLATE_CODE)).ToList();
                var elements = UnitOfWork.Repository<ReportChiPhiCodeRepo>().GetAll().OrderBy(x => x.C_ORDER).ToList();
                foreach(var i in elements)
                {
                    var item = new ChiPhiInReport
                    {
                        Stt = i.STT,
                        name = i.GROUP_NAME,
                        IsBold = i.IS_BOLD,
                        valueCQCT = dataInHeaderCP.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("CQ" + i.GROUP_1_ID + i.GROUP_2_ID))?.Sum(x => x.QUANTITY * x.PRICE) ?? 0,
                        valueCNMB = dataInHeaderCP.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("B" + i.GROUP_1_ID + i.GROUP_2_ID))?.Sum(x => x.QUANTITY * x.PRICE) ?? 0,
                        valueCNMT = dataInHeaderCP.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("T" + i.GROUP_1_ID + i.GROUP_2_ID))?.Sum(x => x.QUANTITY * x.PRICE) ?? 0,
                        valueCNMN = dataInHeaderCP.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("N" + i.GROUP_1_ID + i.GROUP_2_ID))?.Sum(x => x.QUANTITY * x.PRICE) ?? 0,
                        valueCNVT = dataInHeaderCP.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("VT" + i.GROUP_1_ID + i.GROUP_2_ID))?.Sum(x => x.QUANTITY * x.PRICE) ?? 0,
                        valueTcty = dataInHeaderCP.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(i.GROUP_1_ID + i.GROUP_2_ID))?.Sum(x => x.QUANTITY * x.PRICE) ?? 0,
                    };
                    data.chiPhiInReports.Add(item);
                }
                return data;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        internal ReportChiPhiModel SyncElement(List<string> lstParent, List<T_BP_KE_HOACH_CHI_PHI_DATA> lstData)
        {
            var dataAll = new ReportChiPhiModel();
            var Sum = new ChiPhiInReport
            {
                code = "",
                name = "Tổng cộng",
                IsBold = true,
                Order = 0,
            };
            var order = 1;

            foreach (var element in lstParent)
            {
                var codeMB = "B" + element;
                var codeMT = "T" + element;
                var codeMN = "N" + element;
                var codeVT = "VT" + element;
                var codeCQ = "CQ" + element;
                var elementValue = new ChiPhiInReport
                {
                    code = element,
                    name = UnitOfWork.Repository<ReportChiPhiCodeRepo>().Queryable().Where(x => x.GROUP_1_ID == element)?.FirstOrDefault()?.GROUP_NAME,
                    valueCNMB = lstData.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.StartsWith(codeMB)).Sum(x => x.AMOUNT)?? 0,
                    valueCNMT = lstData.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.StartsWith(codeMT)).Sum(x => x.AMOUNT)?? 0,
                    valueCNMN = lstData.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.StartsWith(codeMN)).Sum(x => x.AMOUNT)?? 0,
                    valueCNVT = lstData.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.StartsWith(codeVT)).Sum(x => x.AMOUNT)?? 0,
                    valueCQCT = lstData.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.StartsWith(codeCQ)).Sum(x => x.AMOUNT)?? 0,
                    IsBold = true,
                    Order = order
                };
                Sum.valueCNMB = Sum.valueCNMB + elementValue.valueCNMB;
                Sum.valueCNMT = Sum.valueCNMT + elementValue.valueCNMT;
                Sum.valueCNMN = Sum.valueCNMN + elementValue.valueCNMN;
                Sum.valueCNVT = Sum.valueCNVT + elementValue.valueCNVT;
                Sum.valueCQCT = Sum.valueCQCT + elementValue.valueCQCT;
                
                dataAll.chiPhiInReports.Add(elementValue);
                var lstChildInSyncCost = UnitOfWork.Repository<ReportChiPhiCodeRepo>().Queryable().Where(x => x.GROUP_1_ID == element).Select(x => x.GROUP_2_ID).Distinct().ToList();
                foreach (var item in lstChildInSyncCost)
                {
                    if (string.IsNullOrEmpty(item))
                    {
                        continue;
                    }
                    var checkKm = element + item;
                    var codeChildMB = codeMB + item;
                    var codeChildMT = codeMT + item;
                    var codeChildMN = codeMN + item;
                    var codeChildVT = codeVT + item;
                    var codeChildCQ = codeCQ + item;
                    
                    var elementItemValue = new ChiPhiInReport
                    {
                        code = item,
                        name = UnitOfWork.Repository<ReportChiPhiCodeRepo>().Queryable().FirstOrDefault(x => x.GROUP_2_ID == item)?.GROUP_NAME,
                        valueCNMB = lstData.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.StartsWith(codeChildMB)).Sum(x => x.AMOUNT) ?? 0,
                        valueCNMT = lstData.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.StartsWith(codeChildMT)).Sum(x => x.AMOUNT) ?? 0,
                        valueCNMN = lstData.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.StartsWith(codeChildMN)).Sum(x => x.AMOUNT) ?? 0,
                        valueCNVT = lstData.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.StartsWith(codeChildVT)).Sum(x => x.AMOUNT) ?? 0,
                        valueCQCT = lstData.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.StartsWith(codeChildCQ)).Sum(x => x.AMOUNT) ?? 0,
                        Order = order
                    };
                    dataAll.chiPhiInReports.Add(elementItemValue);
                    order++;
                }
                order++;
            }
            dataAll.chiPhiInReports.Add(Sum);
            return dataAll;
        }

        internal List<ReportDauTuModel> GetDataReportDauTu(int year, string phienBan, string kichBan, string area)
        {
            try
            {
                var headerXDCB = UnitOfWork.Repository<DauTuXayDungRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == kichBan && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var dataXDCB = UnitOfWork.Repository<DauTuXayDungDataRepo>().Queryable().Where(x => headerXDCB.Contains(x.TEMPLATE_CODE)).ToList();

                var headerTTB = UnitOfWork.Repository<DauTuTrangThietBiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == kichBan && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var dataTTB = UnitOfWork.Repository<DauTuTrangThietBiDataRepo>().Queryable().Where(x => headerTTB.Contains(x.TEMPLATE_CODE)).ToList();


                var data = new List<ReportDauTuModel>();
                data.Add(new ReportDauTuModel
                {
                    IsBold = true,
                    name = "TỔNG CỘNG TOÀN CÔNG TY",
                    Col2 = dataXDCB.Sum(x => x.VALUE_1) + dataTTB.Sum(x => x.VALUE_1) ?? 0,
                    Col3 = dataXDCB.Sum(x => x.VALUE_8) + dataTTB.Sum(x => x.VALUE_10),

                });
                data.Add(new ReportDauTuModel
                {
                    Stt = "1",
                    name = "Đầu tư xây dựng cơ bản",
                    Col2 = dataXDCB.Sum(x => x.VALUE_1) ?? 0,
                    Col3 = dataXDCB.Sum(x => x.VALUE_8),
                });
                data.Add(new ReportDauTuModel
                {
                    Stt = "2",
                    name = "Đầu tư trang thiết bị",
                    Col2 = dataTTB.Sum(x => x.VALUE_1) ?? 0,
                    Col3 = dataTTB.Sum(x => x.VALUE_10),
                });
                data.Add(new ReportDauTuModel
                {
                    Stt = "-",
                    name = "Trang thiết bị lẻ",
                    Col2 = dataTTB.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "TTB-LE").Sum(x => x.VALUE_1) ?? 0,
                    Col3 = dataTTB.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "TTB-LE").Sum(x => x.VALUE_10),
                });
                data.Add(new ReportDauTuModel
                {
                    Stt = "-",
                    name = "Trang thiết bị",
                    Col2 = dataTTB.Where(x => x.KHOAN_MUC_DAU_TU_CODE != "TTB-LE").Sum(x => x.VALUE_1) ?? 0,
                    Col3 = dataTTB.Where(x => x.KHOAN_MUC_DAU_TU_CODE != "TTB-LE").Sum(x => x.VALUE_10),
                });
                if (string.IsNullOrEmpty(area))
                {
                    data.AddRange(GetDataReportDauTuByArea(year, dataXDCB, dataTTB, "MB"));
                    data.AddRange(GetDataReportDauTuByArea(year, dataXDCB, dataTTB, "MT"));
                    data.AddRange(GetDataReportDauTuByArea(year, dataXDCB, dataTTB, "MN"));
                    data.AddRange(GetDataReportDauTuByArea(year, dataXDCB, dataTTB, "VT"));
                    data.AddRange(GetDataReportDauTuByArea(year, dataXDCB, dataTTB, "CQ"));
                }
                else
                {
                    data.AddRange(GetDataReportDauTuByArea(year, dataXDCB, dataTTB, area));
                }
                
                return data;
            }
            catch(Exception ex)
            {
                return new List<ReportDauTuModel>();
            }
        }

        public List<ReportDauTuModel> GetDataReportDauTuByArea(int year, List<T_BP_DAU_TU_XAY_DUNG_DATA> dataXDCB, List<T_BP_DAU_TU_TRANG_THIET_BI_DATA> dataTTB, string area)
        {
            try
            {
                var data = new List<ReportDauTuModel>();
                var projects = UnitOfWork.Repository<ProjectRepo>().Queryable().Where(x => x.YEAR == year && x.AREA_CODE == area).OrderByDescending(x => x.TYPE).ToList();
                data.Add(new ReportDauTuModel
                {
                    IsBold = true,
                    name = area == "MB" ? "Chi nhánh khu vực Miền Bắc" : area == "MT" ? "Chi nhánh khu vực Miền Trung" : area == "MN" ? "Chi nhánh khu vực Miền Nam" : area == "VT" ? "Chi nhánh Vận Tải" : "Cơ quan Công ty",
                });

                data.Add(new ReportDauTuModel
                {
                    Stt = "I",
                    IsBold = true,
                    name = "Đầu tư XDCB",
                    Col2 = dataXDCB.Where(x => x.DauTuXayDungProfitCenter.Project.AREA_CODE == area && x.DauTuXayDungProfitCenter.Project.LOAI_HINH == "XDCB").Sum(x => x.VALUE_1) ?? 0,
                    Col3 = dataXDCB.Where(x => x.DauTuXayDungProfitCenter.Project.AREA_CODE == area && x.DauTuXayDungProfitCenter.Project.LOAI_HINH == "XDCB").Sum(x => x.VALUE_8),

                });
                var orderXDCB = 1;
                foreach (var i in projects.Where(x => x.LOAI_HINH == "XDCB"))
                {
                    data.Add(new ReportDauTuModel
                    {
                        Stt = orderXDCB.ToString(),
                        name = i.NAME,
                        Col1 = dataXDCB.FirstOrDefault(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && !string.IsNullOrEmpty(x.VALUE_2))?.VALUE_2,
                        Col2 = dataXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE).Sum(x => x.VALUE_1) ?? 0,
                        Col3 = dataXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE).Sum(x => x.VALUE_8),
                        Col4 = dataXDCB.FirstOrDefault(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && !string.IsNullOrEmpty(x.VALUE_3))?.VALUE_3,
                        Des = dataXDCB.FirstOrDefault(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && !string.IsNullOrEmpty(x.DESCRIPTION))?.DESCRIPTION,
                    });
                    if (string.IsNullOrEmpty(i.TYPE) || i.TYPE == "TTB-LON")
                    {
                        data.Add(new ReportDauTuModel
                        {
                            Stt = "a",
                            name = "Chuẩn bị đầu tư và chuẩn bị thực hiện dự án",
                            Col1 = dataXDCB.FirstOrDefault(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "CBDT" && !string.IsNullOrEmpty(x.VALUE_2))?.VALUE_2,
                            Col2 = dataXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "CBDT").Sum(x => x.VALUE_1) ?? 0,
                            Col3 = dataXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "CBDT").Sum(x => x.VALUE_8),
                            Col4 = dataXDCB.FirstOrDefault(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "CBDT" && !string.IsNullOrEmpty(x.VALUE_3))?.VALUE_3,
                            Des = dataXDCB.FirstOrDefault(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "CBDT" && !string.IsNullOrEmpty(x.DESCRIPTION))?.DESCRIPTION,
                        });
                        data.Add(new ReportDauTuModel
                        {
                            Stt = "b",
                            name = "Thực hiện dự án",
                            Col1 = dataXDCB.FirstOrDefault(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "TTDT" && !string.IsNullOrEmpty(x.VALUE_2))?.VALUE_2,
                            Col2 = dataXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "TTDT").Sum(x => x.VALUE_1) ?? 0,
                            Col3 = dataXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "TTDT").Sum(x => x.VALUE_8),
                            Col4 = dataXDCB.FirstOrDefault(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "TTDT" && !string.IsNullOrEmpty(x.VALUE_3))?.VALUE_3,
                            Des = dataXDCB.FirstOrDefault(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "TTDT" && !string.IsNullOrEmpty(x.DESCRIPTION))?.DESCRIPTION,
                        });
                    }
                    orderXDCB += 1;
                }


                data.Add(new ReportDauTuModel
                {
                    Stt = "II",
                    IsBold = true,
                    name = "Đầu tư trang thiết bị",
                    Col2 = dataTTB.Where(x => x.DauTuTrangThietBiProfitCenter.Project.AREA_CODE == area && x.DauTuTrangThietBiProfitCenter.Project.LOAI_HINH == "XDCB").Sum(x => x.VALUE_1) ?? 0,
                    Col3 = dataTTB.Where(x => x.DauTuTrangThietBiProfitCenter.Project.AREA_CODE == area && x.DauTuTrangThietBiProfitCenter.Project.LOAI_HINH == "XDCB").Sum(x => x.VALUE_10),

                });
                var orderTTB = 1;
                foreach (var i in projects.Where(x => x.LOAI_HINH == "TTB"))
                {
                    data.Add(new ReportDauTuModel
                    {
                        Stt = orderTTB.ToString(),
                        name = i.NAME,
                        Col1 = dataTTB.FirstOrDefault(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && !string.IsNullOrEmpty(x.VALUE_2))?.VALUE_2,
                        Col2 = dataTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE).Sum(x => x.VALUE_1) ?? 0,
                        Col3 = dataTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE).Sum(x => x.VALUE_10),
                        Col4 = dataTTB.FirstOrDefault(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && !string.IsNullOrEmpty(x.VALUE_3))?.VALUE_3,
                        Des = dataTTB.FirstOrDefault(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && !string.IsNullOrEmpty(x.DESCRIPTION))?.DESCRIPTION,
                    });
                    if (string.IsNullOrEmpty(i.TYPE) || i.TYPE == "TTB-LON")
                    {
                        data.Add(new ReportDauTuModel
                        {
                            Stt = "a",
                            name = "Chuẩn bị đầu tư và chuẩn bị thực hiện dự án",
                            Col1 = dataTTB.FirstOrDefault(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "CBDT" && !string.IsNullOrEmpty(x.VALUE_2))?.VALUE_2,
                            Col2 = dataTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "CBDT").Sum(x => x.VALUE_1) ?? 0,
                            Col3 = dataTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "CBDT").Sum(x => x.VALUE_10),
                            Col4 = dataTTB.FirstOrDefault(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "CBDT" && !string.IsNullOrEmpty(x.VALUE_3))?.VALUE_3,
                            Des = dataTTB.FirstOrDefault(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "CBDT" && !string.IsNullOrEmpty(x.DESCRIPTION))?.DESCRIPTION,
                        });
                        data.Add(new ReportDauTuModel
                        {
                            Stt = "b",
                            name = "Thực hiện dự án",
                            Col1 = dataTTB.FirstOrDefault(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "TTDT" && !string.IsNullOrEmpty(x.VALUE_2))?.VALUE_2,
                            Col2 = dataTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "TTDT").Sum(x => x.VALUE_1) ?? 0,
                            Col3 = dataTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "TTDT").Sum(x => x.VALUE_10),
                            Col4 = dataTTB.FirstOrDefault(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "TTDT" && !string.IsNullOrEmpty(x.VALUE_3))?.VALUE_3,
                            Des = dataTTB.FirstOrDefault(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "TTDT" && !string.IsNullOrEmpty(x.DESCRIPTION))?.DESCRIPTION,
                        });
                    }
                    orderTTB += 1;
                }
                return data;
            }
            catch (Exception ex)
            {
                return new List<ReportDauTuModel>();
            }
        }

        internal List<ReportDauTuModel> GetDataDauTuInArea(int year, string phienBan, string kichBan, string area,ref int order)
        {
            var data = new List<ReportDauTuModel>();
            Dictionary<string, string> ChiNhanh = new Dictionary<string, string>()
                {
                    {"", "" },
                    {"MB", "CNMB" },
                    {"MN", "CNMN" },
                    {"MT", "CNMT" },
                    {"VT", "CNVT" },
                    {"CQ", "CQCT" }
                };
            Dictionary<string, string> TenChiNhanh = new Dictionary<string, string>()
            {
                {"MB", "Chi nhánh Miền Bắc" },
                {"MT", "Chi nhánh Miền Trung" },
                {"MN", "Chi nhánh Miền Nam" },
                {"VT", "Chi nhánh Vận Tải" },
                {"CQ", "Chi nhánh Cơ quan Công ty" },

            };
            var sapCodeDT = ChiNhanh[area];
            var costCenterDT = UnitOfWork.Repository<CostCenterRepo>().Queryable().FirstOrDefault(x => x.SAP_CODE == sapCodeDT).CODE;

            var dataHeaderDTXD = UnitOfWork.Repository<DauTuXayDungRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == kichBan && x.STATUS == "03" && x.TEMPLATE_CODE != string.Empty && x.ORG_CODE.StartsWith(costCenterDT)).Select(x => x.TEMPLATE_CODE).ToList();

            var dataHeaderDTTTB = UnitOfWork.Repository<DauTuTrangThietBiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == kichBan && x.STATUS == "03" && x.TEMPLATE_CODE != string.Empty && x.ORG_CODE.StartsWith(costCenterDT)).Select(x => x.TEMPLATE_CODE).ToList();
                                                            
            if (dataHeaderDTTTB.Count() + dataHeaderDTXD.Count() == 0)
            {
                return new List<ReportDauTuModel>();
            }
            var dataInHeaderDTXD = UnitOfWork.Repository<DauTuXayDungDataRepo>().Queryable().Where(x => x.TIME_YEAR == year && dataHeaderDTXD.Contains(x.TEMPLATE_CODE)).ToList();
            var dataInHeaderDTTTB = UnitOfWork.Repository<DauTuTrangThietBiDataRepo>().Queryable().Where(x => x.TIME_YEAR == year && dataHeaderDTTTB.Contains(x.TEMPLATE_CODE)).ToList();
            var lstPjInDTXD = dataInHeaderDTXD.Select(x => x.DauTuXayDungProfitCenter.PROJECT_CODE).Distinct().ToList();
            var lstPjInDTTTB = dataInHeaderDTTTB.Select(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE).Distinct().ToList();
            var sumCN = new ReportDauTuModel
            {
                name = TenChiNhanh[area],
                Order = order,
                valueKHKP = dataInHeaderDTTTB.Where(x=>x.KHOAN_MUC_DAU_TU_CODE == "4011").Sum(x=>x.VALUE) + dataInHeaderDTXD.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4011").Sum(x => x.VALUE) ?? 0,
                valueVDT = dataInHeaderDTTTB.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.VALUE)+ dataInHeaderDTXD.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.VALUE) ?? 0,
                IsBold = true,
            };
            var sumDTXD = new ReportDauTuModel
            {
                name = UnitOfWork.Repository<LoaiHinhDauTuRepo>().Queryable().FirstOrDefault(x => x.CODE == "XDCB").TEXT,
                Order = order + 1,
                valueKHKP = dataInHeaderDTXD.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4011").Sum(x => x.VALUE) ?? 0,
                valueVDT = dataInHeaderDTXD.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.VALUE) ?? 0,
                IsBold = true
            };
            data.Add(sumCN);
            data.Add(sumDTXD);

            order = order+1;
            foreach(var pj in lstPjInDTXD)
            {
                var item = new ReportDauTuModel
                {
                    name = UnitOfWork.Repository<ProjectRepo>().Queryable().FirstOrDefault(x => x.CODE == pj)?.NAME,
                    Order = order,
                    equity_sources = dataInHeaderDTXD.FirstOrDefault(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == pj)?.EQUITY_SOURCES,
                    valueVDT = dataInHeaderDTXD.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4001" && x.DauTuXayDungProfitCenter.PROJECT_CODE == pj)?.Sum(x => x.VALUE) ?? 0,
                    valueKHKP = dataInHeaderDTXD.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4011" && x.DauTuXayDungProfitCenter.PROJECT_CODE == pj)?.Sum(x => x.VALUE) ?? 0,
                    tdtk = dataInHeaderDTXD.FirstOrDefault(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == pj)?.TDTK,
                    description = dataInHeaderDTXD.FirstOrDefault(x=>x.DauTuXayDungProfitCenter.PROJECT_CODE == pj)?.DESCRIPTION
                };
                data.Add(item);
                order++;
                var itemGD = new ReportDauTuModel
                {
                    name = /*UnitOfWork.Repository<ProjectRepo>().Queryable().FirstOrDefault(x => x.CODE == pj)?.GiaiDoan.TEXT*/ "",
                    Order = order,
                    equity_sources = dataInHeaderDTXD.FirstOrDefault(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == pj)?.EQUITY_SOURCES,
                    valueVDT = dataInHeaderDTXD.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4001" && x.DauTuXayDungProfitCenter.PROJECT_CODE == pj)?.Sum(x => x.VALUE) ?? 0,
                    valueKHKP = dataInHeaderDTXD.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4011" && x.DauTuXayDungProfitCenter.PROJECT_CODE == pj)?.Sum(x => x.VALUE) ?? 0,
                    tdtk = dataInHeaderDTXD.FirstOrDefault(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == pj)?.TDTK,
                    description = dataInHeaderDTXD.FirstOrDefault(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == pj)?.DESCRIPTION,
                    lever = 1
                };
                data.Add(itemGD);

                order++;
            }
            var sumDTTTB = new ReportDauTuModel
            {
                name = UnitOfWork.Repository<LoaiHinhDauTuRepo>().Queryable().FirstOrDefault(x => x.CODE == "TTB").TEXT,
                Order = order,
                valueKHKP = dataInHeaderDTTTB.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4011").Sum(x => x.VALUE) ?? 0,
                valueVDT = dataInHeaderDTTTB.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.VALUE) ?? 0,
                IsBold = true,
            };
            data.Add(sumDTTTB);
            order = order++;
            foreach (var pj in lstPjInDTTTB)
            {
                var item = new ReportDauTuModel
                {
                    name = UnitOfWork.Repository<ProjectRepo>().Queryable().FirstOrDefault(x => x.CODE == pj)?.NAME,
                    Order = order,
                    equity_sources = dataInHeaderDTTTB.FirstOrDefault(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == pj)?.EQUITY_SOURCES,
                    valueVDT = dataInHeaderDTTTB.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4001" && x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == pj)?.Sum(x => x.VALUE) ?? 0,
                    valueKHKP = dataInHeaderDTTTB.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4011" && x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == pj)?.Sum(x => x.VALUE) ?? 0,
                    tdtk = dataInHeaderDTTTB.FirstOrDefault(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == pj)?.TDTK,
                    description = dataInHeaderDTTTB.FirstOrDefault(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == pj)?.DESCRIPTION
                };
                data.Add(item);
                order++;
                var itemGD = new ReportDauTuModel
                {
                    name = /*UnitOfWork.Repository<ProjectRepo>().Queryable().FirstOrDefault(x => x.CODE == pj)?.GiaiDoan.TEXT*/ "",
                    Order = order,
                    equity_sources = dataInHeaderDTTTB.FirstOrDefault(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == pj)?.EQUITY_SOURCES,
                    valueVDT = dataInHeaderDTTTB.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4001" && x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == pj)?.Sum(x => x.VALUE) ?? 0,
                    valueKHKP = dataInHeaderDTTTB.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4011" && x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == pj)?.Sum(x => x.VALUE) ?? 0,
                    tdtk = dataInHeaderDTTTB.FirstOrDefault(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == pj)?.TDTK,
                    description = dataInHeaderDTTTB.FirstOrDefault(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == pj)?.DESCRIPTION,
                    lever = 1

                };
                data.Add(itemGD);

                order++;
            }
            return data;
        }
        internal ReportCompaseDTModel GetDataReportCompaseDT(int year, string phienBan, string kichBan, string hangHangKhong)
        {
            try
            {
                var data = new ReportCompaseDTModel();
                var lstHangHangKhong = UnitOfWork.Repository<HangHangKhongRepo>().GetAll().GroupBy(x => x.GROUP_ITEM).Select(x => x.First()).Where(x => x.GROUP_ITEM != null && x.GROUP_ITEM != "").ToList();
                var sanBayGroup = UnitOfWork.Repository<NhomSanBayRepo>().GetAll().ToList();
                lstHangHangKhong = string.IsNullOrEmpty(hangHangKhong) ? lstHangHangKhong : lstHangHangKhong.Where(x => x.CODE == hangHangKhong).ToList();

                var dataHeaderDoanhThu = UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == phienBan && x.KICH_BAN == kichBan && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                if (dataHeaderDoanhThu.Count() == 0)
                {
                    return new ReportCompaseDTModel();
                }

                var dataInHeader = UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => x.TIME_YEAR == year && dataHeaderDoanhThu.Contains(x.TEMPLATE_CODE)).ToList();

                var dataDetails = dataInHeader.Where(x => x.KHOAN_MUC_DOANH_THU_CODE == "2001" || x.KHOAN_MUC_DOANH_THU_CODE == "2002").ToList();

                var dataTab1 = GetDataDoanhThu(year, phienBan, kichBan, hangHangKhong);

                data.Tab2.Add(new RevenueReportModel
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
                    var dataItem = dataTab1.FirstOrDefault(x => x.Code == hhk.GROUP_ITEM);
                    dataItem.IsBold = false;
                    data.Tab2.Add(dataItem);
                }

                // Tính vs3.0
                var dataSLHeader = UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB2" && x.KICH_BAN == kichBan && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                if (dataSLHeader.Count() == 0)
                {
                    return new ReportCompaseDTModel();
                }

                var dataInSLHeader = UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.TIME_YEAR == year && dataSLHeader.Contains(x.TEMPLATE_CODE)).ToList();
                var dataDetailsSL = dataInSLHeader.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010" || x.KHOAN_MUC_SAN_LUONG_CODE == "10020").ToList();
                var dataND = dataInSLHeader.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010").ToList();
                var dataQT = dataInSLHeader.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10020").ToList();
                var unit_price = UnitOfWork.Repository<UnitPriceRepo>().GetAll();
                var lstDataDT = new List<T_BP_KE_HOACH_DOANH_THU_DATA>();
                var lstDataTest = new List<T_BP_KE_HOACH_SAN_LUONG_DATA>();
                var lstDataTestDT = new List<T_BP_KE_HOACH_DOANH_THU_DATA>();
                // Tính doanh thu khách hàng VN
                foreach (var dataSL in dataND)
                {
                    var group = dataSL.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM;
                    var HHKCode = dataSL.SanLuongProfitCenter.HANG_HANG_KHONG_CODE;
                    if (group == "HKTN#")
                    {
                        HHKCode = "VU1";
                    }
                    var priceND = unit_price.FirstOrDefault(x => x.WAREHOUSE_ID == dataSL.SanLuongProfitCenter.SanBay.OTHER_PM_CODE && x.SHORT_OBJECT_ID ==HHKCode && dataSL.TIME_YEAR == x.YEAR);
                    var unit = priceND != null ? priceND.UNIT_ID : "Kg";
                    var price = priceND!= null ? priceND.SERVICE_PRICE : 0;
                    price = unit == "Kg" ? price : price * 336;
                    
                    var centercode = UnitOfWork.Repository<SanLuongProfitCenterRepo>().Queryable().FirstOrDefault(x => x.SAN_BAY_CODE == dataSL.SanLuongProfitCenter.SAN_BAY_CODE && x.HANG_HANG_KHONG_CODE == dataSL.SanLuongProfitCenter.HANG_HANG_KHONG_CODE)?.ToString();
                    if(dataSL.SanLuongProfitCenter == null)
                    {
                        continue;
                    }
                    var profitCenterCode = new T_MD_DOANH_THU_PROFIT_CENTER
                    {
                        CODE = Guid.NewGuid().ToString(),
                        HANG_HANG_KHONG_CODE = dataSL.SanLuongProfitCenter.HANG_HANG_KHONG_CODE,
                        SAN_BAY_CODE = dataSL.SanLuongProfitCenter.SAN_BAY_CODE,
                        HangHangKhong = dataSL.SanLuongProfitCenter.HangHangKhong,
                        SanBay = dataSL.SanLuongProfitCenter.SanBay
                    };
                    if(dataSL.SanLuongProfitCenter. HANG_HANG_KHONG_CODE == null)
                    {
                        var a = dataSL.SanLuongProfitCenter;
                    };
                    var item = new T_BP_KE_HOACH_DOANH_THU_DATA
                    {
                        PKID = Guid.NewGuid().ToString(),
                        ORG_CODE = dataSL.ORG_CODE,
                        TEMPLATE_CODE = dataSL.TEMPLATE_CODE,
                        DOANH_THU_PROFIT_CENTER_CODE = profitCenterCode.CODE,
                        KHOAN_MUC_DOANH_THU_CODE = dataSL.KHOAN_MUC_SAN_LUONG_CODE,
                        DoanhThuProfitCenter = profitCenterCode,
                        TIME_YEAR = dataSL.TIME_YEAR,
                        VERSION = dataSL.VERSION,
                        VALUE_JAN = dataSL.VALUE_JAN * price,
                        VALUE_FEB = dataSL.VALUE_FEB * price,
                        VALUE_MAR = dataSL.VALUE_MAR * price,
                        VALUE_APR = dataSL.VALUE_APR * price,
                        VALUE_MAY = dataSL.VALUE_MAY * price,
                        VALUE_JUN = dataSL.VALUE_JUN * price,
                        VALUE_JUL = dataSL.VALUE_JUL * price,
                        VALUE_AUG = dataSL.VALUE_AUG * price,
                        VALUE_SEP = dataSL.VALUE_SEP * price,
                        VALUE_OCT = dataSL.VALUE_OCT * price,
                        VALUE_NOV = dataSL.VALUE_NOV * price,
                        VALUE_DEC = dataSL.VALUE_DEC * price,
                        VALUE_SUM_YEAR = dataSL.VALUE_SUM_YEAR * price
                    };
                    lstDataDT.Add(item);
                }
                // Tính doanh thu khách quốc tế
                var TyGia = UnitOfWork.Repository<SharedDataRepo>().Queryable().FirstOrDefault(x => x.CODE == "2").VALUE;
                foreach (var dataSL in dataQT)
                {

                    if (dataSL.SanLuongProfitCenter == null)
                    {
                        continue;
                    }
                    var group = dataSL.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM;
                    var HHKCode = dataSL.SanLuongProfitCenter.HANG_HANG_KHONG_CODE;
                    if (group == "HKTN#")
                    {
                        HHKCode = "VU1";
                    }
                    var priceQT = unit_price.FirstOrDefault(x => x.WAREHOUSE_ID == dataSL.SanLuongProfitCenter.SanBay.OTHER_PM_CODE && x.SHORT_OBJECT_ID ==HHKCode && dataSL.TIME_YEAR == x.YEAR);
                    var unit = priceQT != null ? priceQT.UNIT_ID : "Kg";
                    var price = priceQT != null ? priceQT.SERVICE_PRICE : 0;
                    price = unit == "Kg" ? price : price * 336;
                    var centercode = UnitOfWork.Repository<DoanhThuProfitCenterRepo>().Queryable().FirstOrDefault(x => x.SAN_BAY_CODE == dataSL.SanLuongProfitCenter.SAN_BAY_CODE && x.HANG_HANG_KHONG_CODE == dataSL.SanLuongProfitCenter.HANG_HANG_KHONG_CODE);
                    var profitCenterCode = new T_MD_DOANH_THU_PROFIT_CENTER
                    {
                        CODE = Guid.NewGuid().ToString(),
                        HANG_HANG_KHONG_CODE = dataSL.SanLuongProfitCenter.HANG_HANG_KHONG_CODE,
                        SAN_BAY_CODE = dataSL.SanLuongProfitCenter.SAN_BAY_CODE,
                        HangHangKhong = dataSL.SanLuongProfitCenter.HangHangKhong,
                        SanBay = dataSL.SanLuongProfitCenter.SanBay
                    };
                    var priceDTQT = price * TyGia;
                    if (priceQT!= null)
                    {
                        priceDTQT = priceQT.CURRENCY_ID == "USD" ? (price * TyGia) : price;
                    }
                    var item = new T_BP_KE_HOACH_DOANH_THU_DATA
                    {
                        PKID = Guid.NewGuid().ToString(),
                        ORG_CODE = dataSL.ORG_CODE,
                        TEMPLATE_CODE = dataSL.TEMPLATE_CODE,
                        DOANH_THU_PROFIT_CENTER_CODE = profitCenterCode?.CODE,
                        DoanhThuProfitCenter = profitCenterCode,
                        KHOAN_MUC_DOANH_THU_CODE = dataSL.KHOAN_MUC_SAN_LUONG_CODE,
                        TIME_YEAR = dataSL.TIME_YEAR,
                        VERSION = dataSL.VERSION,
                        VALUE_JAN = dataSL.VALUE_JAN * priceDTQT,
                        VALUE_FEB = dataSL.VALUE_FEB * priceDTQT,
                        VALUE_MAR = dataSL.VALUE_MAR * priceDTQT,
                        VALUE_APR = dataSL.VALUE_APR * priceDTQT,
                        VALUE_MAY = dataSL.VALUE_MAY * priceDTQT,
                        VALUE_JUN = dataSL.VALUE_JUN * priceDTQT,
                        VALUE_JUL = dataSL.VALUE_JUL * priceDTQT,
                        VALUE_AUG = dataSL.VALUE_AUG * priceDTQT,
                        VALUE_SEP = dataSL.VALUE_SEP * priceDTQT,
                        VALUE_OCT = dataSL.VALUE_OCT * priceDTQT,
                        VALUE_NOV = dataSL.VALUE_NOV * priceDTQT,
                        VALUE_DEC = dataSL.VALUE_DEC * priceDTQT,
                        VALUE_SUM_YEAR = dataSL.VALUE_SUM_YEAR * priceDTQT
                    };
                    lstDataDT.Add(item);
                    if (dataSL.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == "QH")
                    {
                        lstDataTest.Add(dataSL);
                        lstDataTestDT.Add(item);
                    }
                }
                var order = 0;
                var itemSum = new ReportCompaseDT
                {
                    Name = "TỔNG CỘNG",
                    Value1 = lstDataDT.Sum(x => x.VALUE_JAN) ?? 0,
                    Value2 = lstDataDT.Sum(x => x.VALUE_FEB) ?? 0,
                    Value3 = lstDataDT.Sum(x => x.VALUE_MAR) ?? 0,
                    Value4 = lstDataDT.Sum(x => x.VALUE_APR) ?? 0,
                    Value5 = lstDataDT.Sum(x => x.VALUE_MAY) ?? 0,
                    Value6 = lstDataDT.Sum(x => x.VALUE_JUN) ?? 0,
                    Value7 = lstDataDT.Sum(x => x.VALUE_JUL) ?? 0,
                    Value8 = lstDataDT.Sum(x => x.VALUE_AUG) ?? 0,
                    Value9 = lstDataDT.Sum(x => x.VALUE_SEP) ?? 0,
                    Value10 = lstDataDT.Sum(x => x.VALUE_OCT) ?? 0,
                    Value11 = lstDataDT.Sum(x => x.VALUE_NOV) ?? 0,
                    Value12 = lstDataDT.Sum(x => x.VALUE_DEC) ?? 0,
                    ValueSumYear = lstDataDT.Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                    Isbold = true,
                    Order = -1,
                };
                data.Tab1.Add(itemSum);
                decimal sumTest = 0;
                foreach (var hhk in lstHangHangKhong)
                {
                    var item = new ReportCompaseDT
                    {
                        Code = hhk.GROUP_ITEM,
                        Name = hhk.GROUP_ITEM,
                        Value1 = lstDataDT.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_JAN) ?? 0,
                        Value2 = lstDataDT.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_FEB) ?? 0,
                        Value3 = lstDataDT.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_MAR ) ?? 0,
                        Value4 = lstDataDT.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_APR ) ?? 0,
                        Value5 = lstDataDT.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_MAY ) ?? 0,
                        Value6 = lstDataDT.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_JUN ) ?? 0,
                        Value7 = lstDataDT.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_JUL ) ?? 0,
                        Value8 = lstDataDT.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_AUG ) ?? 0,
                        Value9 = lstDataDT.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_SEP) ?? 0,
                        Value10 = lstDataDT.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_OCT ) ?? 0,
                        Value11 = lstDataDT.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_NOV ) ?? 0,
                        Value12 = lstDataDT.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_DEC ) ?? 0,
                        ValueSumYear = lstDataDT.Where(x => x.DoanhThuProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_SUM_YEAR ) ?? 0,
                        Isbold = false,
                        Order = order,
                    };
                    order++;
                    data.Tab1.Add(item);
                    if(hhk.GROUP_ITEM == "QH")
                    {
                        sumTest = item.Value2;
                    }
                }
                var orderTab3 = 0;
                for(int i = 0; i < data.Tab1.Count(); i++)
                {
                        var item = new ReportCompaseDT
                        {
                            Code = data.Tab1[i].Name,
                            Name = data.Tab1[i].Name,
                            Value1 = (data.Tab1[i].Value1 / data.Tab2[i].Value1) * 100,
                            Value2 = (data.Tab1[i].Value2 / data.Tab2[i].Value2 ) * 100,
                            Value3 = (data.Tab1[i].Value3 / data.Tab2[i].Value3 ) * 100,
                            Value4 = (data.Tab1[i].Value4 / data.Tab2[i].Value4 ) * 100,
                            Value5 = (data.Tab1[i].Value5 / data.Tab2[i].Value5 ) * 100,
                            Value6 = (data.Tab1[i].Value6 / data.Tab2[i].Value6 ) * 100,
                            Value7 = (data.Tab1[i].Value7 / data.Tab2[i].Value7 ) * 100,
                            Value8 = (data.Tab1[i].Value8 / data.Tab2[i].Value8 ) * 100,
                            Value9 = (data.Tab1[i].Value9 / data.Tab2[i].Value9) * 100,
                            Value10 = (data.Tab1[i].Value10 / data.Tab2[i].Value10 ) * 100,
                            Value11 = (data.Tab1[i].Value11 / data.Tab2[i].Value11 ) * 100,
                            Value12 = (data.Tab1[i].Value12 / data.Tab2[i].Value12 ) * 100,
                            ValueSumYear = (data.Tab1[i].ValueSumYear / data.Tab2[i].ValueSumYear ) * 100,
                            Isbold = false,
                            Order = orderTab3
                        };
                    if(i == 0)
                    {
                        item.Isbold = true;
                    }
                    orderTab3++;
                    data.Tab3.Add(item);

                }
                return data;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void ExportExcelCompaseDT(ref MemoryStream outFileStream, string path, int year, string phienBan, string kichBan, string hangHangKhong)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                IWorkbook templateWorkbook;
                templateWorkbook = new XSSFWorkbook(fs);
                fs.Close();
                ISheet sheet = templateWorkbook.GetSheetAt(0);
                var data = GetDataReportCompaseDT(year, phienBan, kichBan, hangHangKhong);

                if (data.Tab1.Count <= 1 || data.Tab2.Count <=1)
                {
                    this.State = false;
                    this.ErrorMessage = "Không có dữ liệu!";
                    return;
                }
                // export V3.0
                var startRowV3 = 6;
                var NUM_CELL = 14;
                InsertDataCompaseDT(templateWorkbook, sheet, data.Tab1, startRowV3, NUM_CELL);
                // export KH
                var startRowKH = startRowV3 + data.Tab1.Count();
                InsertDataHeader(templateWorkbook, sheet, "KH", startRowKH, NUM_CELL);
                var startRowKHData = startRowV3 + data.Tab1.Count() + 1;
                InsertDataCompaseDTTab2(templateWorkbook, sheet, data.Tab2, startRowKHData, NUM_CELL);
                var startRowTab3 = startRowKH + data.Tab2.Count();
                InsertDataHeader(templateWorkbook, sheet, "TH/KH(%)", startRowTab3, NUM_CELL);
                var startRowTab3Data = startRowKH + data.Tab2.Count() + 1;
                InsertDataCompaseDTTab3(templateWorkbook, sheet, data.Tab3, startRowTab3Data, NUM_CELL);
                // export tab3
                templateWorkbook.Write(outFileStream);
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        internal void InsertDataHeader(IWorkbook templateWorkbook, ISheet sheet,string table, int startRow, int NUM_CELL)
        {
            ICellStyle styleCellBold = templateWorkbook.CreateCellStyle();
            styleCellBold.CloneStyleFrom(sheet.GetRow(5).Cells[0].CellStyle);
            var fontBold = templateWorkbook.CreateFont();
            fontBold.Boldweight = (short)FontBoldWeight.Bold;
            fontBold.FontHeightInPoints = 11;
            fontBold.FontName = "Times New Roman";
            
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, NUM_CELL);
                rowCur.Cells[0].SetCellValue(table.ToString());
                rowCur.Cells[1].SetCellValue("THÁNG 1");
                rowCur.Cells[2].SetCellValue("THÁNG 2");
                rowCur.Cells[3].SetCellValue("THÁNG 3");
                rowCur.Cells[4].SetCellValue("THÁNG 4");
                rowCur.Cells[5].SetCellValue("THÁNG 5");
                rowCur.Cells[6].SetCellValue("THÁNG 6");
                rowCur.Cells[7].SetCellValue("THÁNG 7");
                rowCur.Cells[8].SetCellValue("THÁNG 8");
                rowCur.Cells[9].SetCellValue("THÁNG 9");
                rowCur.Cells[10].SetCellValue("THÁNG 10");
                rowCur.Cells[11].SetCellValue("THÁNG 11");
                rowCur.Cells[12].SetCellValue("THÁNG 12");
                rowCur.Cells[13].SetCellValue("TỔNG NĂM");

                for(int i = 0; i < NUM_CELL; i++)
            {
                rowCur.Cells[i].CellStyle = styleCellBold;
            }


        }

        internal void InsertDataCompaseDT(IWorkbook templateWorkbook, ISheet sheet, IList<ReportCompaseDT> dataDetails, int startRow, int NUM_CELL)
        {
            ICellStyle styleCellBold = templateWorkbook.CreateCellStyle();
            styleCellBold.CloneStyleFrom(sheet.GetRow(6).Cells[0].CellStyle);
            var fontBold = templateWorkbook.CreateFont();
            fontBold.Boldweight = (short)FontBoldWeight.Bold;
            fontBold.FontHeightInPoints = 11;
            fontBold.FontName = "Times New Roman";

            ICellStyle styleName = templateWorkbook.CreateCellStyle();
            styleName.CloneStyleFrom(sheet.GetRow(6).Cells[1].CellStyle);
            ICellStyle styleBody = templateWorkbook.CreateCellStyle();
            styleBody.CloneStyleFrom(sheet.GetRow(7).Cells[1].CellStyle);
            styleBody.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");
            ICellStyle styleBodySum = templateWorkbook.CreateCellStyle();
            styleBodySum.CloneStyleFrom(sheet.GetRow(7).Cells[0].CellStyle);
            styleBodySum.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");

            for (int i = 0; i < dataDetails.Count(); i++)
            {
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, NUM_CELL);
                rowCur.Cells[0].SetCellValue(dataDetails[i].Name);
                rowCur.Cells[1].SetCellValue(Math.Round((double)dataDetails[i].Value1));
                rowCur.Cells[2].SetCellValue(Math.Round((double)dataDetails[i].Value2));
                rowCur.Cells[3].SetCellValue(Math.Round((double)dataDetails[i].Value3));
                rowCur.Cells[4].SetCellValue(Math.Round((double)dataDetails[i].Value4));
                rowCur.Cells[5].SetCellValue(Math.Round((double)dataDetails[i].Value5));
                rowCur.Cells[6].SetCellValue(Math.Round((double)dataDetails[i].Value6));
                rowCur.Cells[7].SetCellValue(Math.Round((double)dataDetails[i].Value7));
                rowCur.Cells[8].SetCellValue(Math.Round((double)dataDetails[i].Value8));
                rowCur.Cells[9].SetCellValue(Math.Round((double)dataDetails[i].Value9));
                rowCur.Cells[10].SetCellValue(Math.Round((double)dataDetails[i].Value10));
                rowCur.Cells[11].SetCellValue(Math.Round((double)dataDetails[i].Value11));
                rowCur.Cells[12].SetCellValue(Math.Round((double)dataDetails[i].Value12));
                rowCur.Cells[13].SetCellValue(Math.Round((double)dataDetails[i].ValueSumYear));

                for (int j = 0; j < NUM_CELL; j++)
                {
                    if (dataDetails[i].Isbold)
                    {
                        if (j == 0)
                        {
                            rowCur.Cells[j].CellStyle = styleCellBold;
                            rowCur.Cells[j].CellStyle.SetFont(fontBold);
                        }
                        else
                        {
                            rowCur.Cells[j].CellStyle = styleBodySum;
                            rowCur.Cells[j].CellStyle.SetFont(fontBold);
                        }
                    }
                    else
                    {
                        if (j == 0)
                        {
                            rowCur.Cells[j].CellStyle = styleName;
                        }
                        else
                        {
                            rowCur.Cells[j].CellStyle = styleBody;
                        }
                    }
                }
            }
        }
        internal void InsertDataCompaseDTTab2(IWorkbook templateWorkbook, ISheet sheet, IList<RevenueReportModel> dataDetails, int startRow, int NUM_CELL)
        {
            ICellStyle styleCellBold = templateWorkbook.CreateCellStyle();
            styleCellBold.CloneStyleFrom(sheet.GetRow(6).Cells[0].CellStyle);
            var fontBold = templateWorkbook.CreateFont();
            fontBold.Boldweight = (short)FontBoldWeight.Bold;
            fontBold.FontHeightInPoints = 11;
            fontBold.FontName = "Times New Roman";

            ICellStyle styleName = templateWorkbook.CreateCellStyle();
            styleName.CloneStyleFrom(sheet.GetRow(7).Cells[1].CellStyle);
            ICellStyle styleBody = templateWorkbook.CreateCellStyle();
            styleBody.CloneStyleFrom(sheet.GetRow(7).Cells[1].CellStyle);
            styleBody.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");
            ICellStyle styleBodySum = templateWorkbook.CreateCellStyle();
            styleBodySum.CloneStyleFrom(sheet.GetRow(7).Cells[0].CellStyle);
            styleBodySum.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");

            for (int i = 0; i < dataDetails.Count(); i++)
            {
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, NUM_CELL);
                rowCur.Cells[0].SetCellValue(dataDetails[i].Name);
                rowCur.Cells[1].SetCellValue(Math.Round((double)dataDetails[i].Value1));
                rowCur.Cells[2].SetCellValue(Math.Round((double)dataDetails[i].Value2));
                rowCur.Cells[3].SetCellValue(Math.Round((double)dataDetails[i].Value3));
                rowCur.Cells[4].SetCellValue(Math.Round((double)dataDetails[i].Value4));
                rowCur.Cells[5].SetCellValue(Math.Round((double)dataDetails[i].Value5));
                rowCur.Cells[6].SetCellValue(Math.Round((double)dataDetails[i].Value6));
                rowCur.Cells[7].SetCellValue(Math.Round((double)dataDetails[i].Value7));
                rowCur.Cells[8].SetCellValue(Math.Round((double)dataDetails[i].Value8));
                rowCur.Cells[9].SetCellValue(Math.Round((double)dataDetails[i].Value9));
                rowCur.Cells[10].SetCellValue(Math.Round((double)dataDetails[i].Value10));
                rowCur.Cells[11].SetCellValue(Math.Round((double)dataDetails[i].Value11));
                rowCur.Cells[12].SetCellValue(Math.Round((double)dataDetails[i].Value12));
                rowCur.Cells[13].SetCellValue(Math.Round((double)dataDetails[i].ValueSumYear));

                for (int j = 0; j < NUM_CELL; j++)
                {
                    if (dataDetails[i].IsBold)
                    {
                        if (j == 0)
                        {
                            rowCur.Cells[j].CellStyle = styleCellBold;
                            rowCur.Cells[j].CellStyle.SetFont(fontBold);
                        }
                        else
                        {
                            rowCur.Cells[j].CellStyle = styleBodySum;
                            rowCur.Cells[j].CellStyle.SetFont(fontBold);
                        }
                    }
                    else
                    {
                        if (j == 0)
                        {
                            rowCur.Cells[j].CellStyle = styleName;
                            rowCur.Cells[j].CellStyle.Alignment = HorizontalAlignment.Left;
                        }
                        else
                        {
                            rowCur.Cells[j].CellStyle = styleBody;
                        }
                    }
                }
            }
        }

        internal void InsertDataCompaseDTTab3(IWorkbook templateWorkbook, ISheet sheet, IList<ReportCompaseDT> dataDetails, int startRow, int NUM_CELL)
        {
            ICellStyle styleCellBold = templateWorkbook.CreateCellStyle();
            styleCellBold.CloneStyleFrom(sheet.GetRow(6).Cells[0].CellStyle);
            var fontBold = templateWorkbook.CreateFont();
            fontBold.Boldweight = (short)FontBoldWeight.Bold;
            fontBold.FontHeightInPoints = 11;
            fontBold.FontName = "Times New Roman";

            ICellStyle styleName = templateWorkbook.CreateCellStyle();
            styleName.CloneStyleFrom(sheet.GetRow(7).Cells[0].CellStyle);
            ICellStyle styleBody = templateWorkbook.CreateCellStyle();
            styleBody.CloneStyleFrom(sheet.GetRow(7).Cells[1].CellStyle);
            styleBody.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");
            ICellStyle styleBodySum = templateWorkbook.CreateCellStyle();
            styleBodySum.CloneStyleFrom(sheet.GetRow(7).Cells[0].CellStyle);
            styleBodySum.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");

            for (int i = 0; i < dataDetails.Count(); i++)
            {
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, NUM_CELL);
                rowCur.Cells[0].SetCellValue(dataDetails[i].Name);
                rowCur.Cells[1].SetCellValue(Math.Round((double)dataDetails[i].Value1));
                rowCur.Cells[2].SetCellValue(Math.Round((double)dataDetails[i].Value2));
                rowCur.Cells[3].SetCellValue(Math.Round((double)dataDetails[i].Value3));
                rowCur.Cells[4].SetCellValue(Math.Round((double)dataDetails[i].Value4));
                rowCur.Cells[5].SetCellValue(Math.Round((double)dataDetails[i].Value5));
                rowCur.Cells[6].SetCellValue(Math.Round((double)dataDetails[i].Value6));
                rowCur.Cells[7].SetCellValue(Math.Round((double)dataDetails[i].Value7));
                rowCur.Cells[8].SetCellValue(Math.Round((double)dataDetails[i].Value8));
                rowCur.Cells[9].SetCellValue(Math.Round((double)dataDetails[i].Value9));
                rowCur.Cells[10].SetCellValue(Math.Round((double)dataDetails[i].Value10));
                rowCur.Cells[11].SetCellValue(Math.Round((double)dataDetails[i].Value11));
                rowCur.Cells[12].SetCellValue(Math.Round((double)dataDetails[i].Value12));
                rowCur.Cells[13].SetCellValue(Math.Round((double)dataDetails[i].ValueSumYear));

                for (int j = 0; j < NUM_CELL; j++)
                {
                    if (dataDetails[i].Isbold)
                    {
                        if (j == 0)
                        {
                            rowCur.Cells[j].CellStyle = styleCellBold;
                            rowCur.Cells[j].CellStyle.SetFont(fontBold);
                        }
                        else
                        {
                            rowCur.Cells[j].CellStyle = styleBodySum;
                            rowCur.Cells[j].CellStyle.SetFont(fontBold);
                        }
                    }
                    else
                    {
                        if (j == 0)
                        {
                            rowCur.Cells[j].CellStyle = styleName;
                            rowCur.Cells[j].CellStyle.Alignment = HorizontalAlignment.Left;
                        }
                        else
                        {
                            rowCur.Cells[j].CellStyle = styleBody;
                        }
                    }
                }
            }
        }
    }
}