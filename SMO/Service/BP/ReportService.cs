using NHibernate.Criterion;
using NPOI.SS.Formula.Functions;
using SMO.Repository.Common;
using SMO.Repository.Implement.BP.DAU_TU_TRANG_THIET_BI;
using SMO.Repository.Implement.BP.DAU_TU_XAY_DUNG;
using SMO.Repository.Implement.MD;
using SMO.Service.MD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMO.Service.BP
{
    public class ReportService
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public ReportService()
        {
            UnitOfWork = new NHUnitOfWork();
        }

        public ReportDataCenter GenDataBM01D(int year, string orgCode)
        {
            try
            {
                var data = new ReportDataCenter();
                var lstProject = UnitOfWork.Repository<ProjectRepo>().Queryable().Where(x => x.YEAR > 0).ToList();
                var lstProjectTtb = lstProject.Where(x => x.LOAI_HINH == "TTB").ToList();
                var lstProjectXdcb = lstProject.Where(x => x.LOAI_HINH == "XDCB").ToList();


                var dataDTTTB = UnitOfWork.Repository<DauTuTrangThietBiDataRepo>().Queryable().Where(x => x.TIME_YEAR == year).ToList();
                var dataDTXDCB = UnitOfWork.Repository<DauTuXayDungDataRepo>().Queryable().Where(x => x.TIME_YEAR == year).ToList();

                #region 
                data.BM01D.Add(new ReportModel
                {
                    Id = "A",
                    Stt = "A",
                    Name = "A. Dự án chuyển tiếp kỳ truớc",
                    IsBold = true,
                });
                data.BM01D.Add(new ReportModel
                {
                    Id = "A.I",
                    Stt = "I",
                    Name = "I. Đầu tư trang thiết bị",
                    Parent = "A",
                    IsBold = true,
                });
                data.BM01D.Add(new ReportModel
                {
                    Id = "A.I.1",
                    Stt = "1",
                    Name = "1. Các dự án chuẩn bị đầu tư",
                    Parent = "A.I",
                });
                foreach (var i in lstProjectTtb.Where(x => x.YEAR < year && x.GIAI_DOAN == "CBDT"))
                {
                    data.BM01D.Add(new ReportModel
                    {
                        Id = i.CODE,
                        Parent = "A.I.1",
                        Name = i.NAME,
                        Col1 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4001")?.Sum(x => x.VALUE) ?? 0,
                        Col2 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4002")?.Sum(x => x.VALUE) ?? 0,
                        Col3 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4003")?.Sum(x => x.VALUE) ?? 0,
                        Col4 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4010")?.Sum(x => x.VALUE) ?? 0,
                        Col5 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4011")?.Sum(x => x.VALUE) ?? 0,
                        Col6 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4012")?.Sum(x => x.VALUE) ?? 0,
                    });
                }


                data.BM01D.Add(new ReportModel
                {
                    Id = "A.I.2",
                    Stt = "2",
                    Name = "2. Các dự án thực hiện đầu tư",
                    Parent = "A.I",
                });
                foreach (var i in lstProjectTtb.Where(x => x.YEAR < year && x.GIAI_DOAN == "TTDT"))
                {
                    data.BM01D.Add(new ReportModel
                    {
                        Id = i.CODE,
                        Parent = "A.I.2",
                        Name = i.NAME,
                        Col1 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4001")?.Sum(x => x.VALUE) ?? 0,
                        Col2 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4002")?.Sum(x => x.VALUE) ?? 0,
                        Col3 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4003")?.Sum(x => x.VALUE) ?? 0,
                        Col4 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4010")?.Sum(x => x.VALUE) ?? 0,
                        Col5 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4011")?.Sum(x => x.VALUE) ?? 0,
                        Col6 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4012")?.Sum(x => x.VALUE) ?? 0,
                    });
                }


                data.BM01D.Add(new ReportModel
                {
                    Id = "A.I.3",
                    Stt = "3",
                    Name = "3. Đầu tư trang thiết bị lẻ",
                    Parent = "A.I",
                });

                foreach (var i in lstProjectTtb.Where(x => x.YEAR < year && x.GIAI_DOAN == "TTBL"))
                {
                    data.BM01D.Add(new ReportModel
                    {
                        Id = i.CODE,
                        Parent = "A.I.3",
                        Name = i.NAME,
                        Col1 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4001")?.Sum(x => x.VALUE) ?? 0,
                        Col2 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4002")?.Sum(x => x.VALUE) ?? 0,
                        Col3 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4003")?.Sum(x => x.VALUE) ?? 0,
                        Col4 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4010")?.Sum(x => x.VALUE) ?? 0,
                        Col5 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4011")?.Sum(x => x.VALUE) ?? 0,
                        Col6 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4012")?.Sum(x => x.VALUE) ?? 0,
                    });
                }

                data.BM01D.Add(new ReportModel
                {
                    Id = "A.II",
                    Stt = "II",
                    Name = "II. Đầu tư xây dựng cơ bản",
                    Parent = "A",
                    IsBold = true,
                });
                data.BM01D.Add(new ReportModel
                {
                    Id = "A.II.1",
                    Stt = "1",
                    Name = "1. Các dự án chuẩn bị đầu tư",
                    Parent = "A.II",
                });
                foreach (var i in lstProjectXdcb.Where(x => x.YEAR < year && x.GIAI_DOAN == "CBDT"))
                {
                    data.BM01D.Add(new ReportModel
                    {
                        Id = i.CODE,
                        Parent = "A.II.1",
                        Name = i.NAME,
                        Col1 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4001")?.Sum(x => x.VALUE) ?? 0,
                        Col2 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4002")?.Sum(x => x.VALUE) ?? 0,
                        Col3 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4003")?.Sum(x => x.VALUE) ?? 0,
                        Col4 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4010")?.Sum(x => x.VALUE) ?? 0,
                        Col5 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4011")?.Sum(x => x.VALUE) ?? 0,
                        Col6 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4012")?.Sum(x => x.VALUE) ?? 0,
                    });
                }

                data.BM01D.Add(new ReportModel
                {
                    Id = "A.II.2",
                    Stt = "2",
                    Name = "2. Các dự án thực hiện đầu tư",
                    Parent = "A.II",
                });
                foreach (var i in lstProjectXdcb.Where(x => x.YEAR < year && x.GIAI_DOAN == "TTDT"))
                {
                    data.BM01D.Add(new ReportModel
                    {
                        Id = i.CODE,
                        Parent = "A.II.2",
                        Name = i.NAME,
                        Col1 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4001")?.Sum(x => x.VALUE) ?? 0,
                        Col2 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4002")?.Sum(x => x.VALUE) ?? 0,
                        Col3 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4003")?.Sum(x => x.VALUE) ?? 0,
                        Col4 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4010")?.Sum(x => x.VALUE) ?? 0,
                        Col5 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4011")?.Sum(x => x.VALUE) ?? 0,
                        Col6 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4012")?.Sum(x => x.VALUE) ?? 0,
                    });
                }

                data.BM01D.Add(new ReportModel
                {
                    Id = "B",
                    Stt = "B",
                    Name = "B. Dự án đầu tư mới",
                    IsBold = true,
                });
                data.BM01D.Add(new ReportModel
                {
                    Id = "B.I",
                    Stt = "I",
                    Name = "I. Đầu tư trang thiết bị",
                    Parent = "B",
                    IsBold = true,
                });
                data.BM01D.Add(new ReportModel
                {
                    Id = "B.I.1",
                    Stt = "1",
                    Name = "1. Các dự án chuẩn bị đầu tư",
                    Parent = "B.I",
                });
                foreach (var i in lstProjectTtb.Where(x => x.YEAR == year && x.GIAI_DOAN == "CBDT"))
                {
                    data.BM01D.Add(new ReportModel
                    {
                        Id = i.CODE,
                        Parent = "B.I.1",
                        Name = i.NAME,
                        Col1 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4001")?.Sum(x => x.VALUE) ?? 0,
                        Col2 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4002")?.Sum(x => x.VALUE) ?? 0,
                        Col3 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4003")?.Sum(x => x.VALUE) ?? 0,
                        Col4 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4010")?.Sum(x => x.VALUE) ?? 0,
                        Col5 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4011")?.Sum(x => x.VALUE) ?? 0,
                        Col6 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4012")?.Sum(x => x.VALUE) ?? 0,
                    });
                }


                data.BM01D.Add(new ReportModel
                {
                    Id = "B.I.2",
                    Stt = "2",
                    Name = "2. Các dự án thực hiện đầu tư",
                    Parent = "B.I",
                });
                foreach (var i in lstProjectTtb.Where(x => x.YEAR == year && x.GIAI_DOAN == "TTDT"))
                {
                    data.BM01D.Add(new ReportModel
                    {
                        Id = i.CODE,
                        Parent = "B.I.2",
                        Name = i.NAME,
                        Col1 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4001")?.Sum(x => x.VALUE) ?? 0,
                        Col2 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4002")?.Sum(x => x.VALUE) ?? 0,
                        Col3 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4003")?.Sum(x => x.VALUE) ?? 0,
                        Col4 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4010")?.Sum(x => x.VALUE) ?? 0,
                        Col5 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4011")?.Sum(x => x.VALUE) ?? 0,
                        Col6 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4012")?.Sum(x => x.VALUE) ?? 0,
                    });
                }


                data.BM01D.Add(new ReportModel
                {
                    Id = "B.I.3",
                    Stt = "3",
                    Name = "3. Đầu tư trang thiết bị lẻ",
                    Parent = "B.I",
                });

                foreach (var i in lstProjectTtb.Where(x => x.YEAR == year && x.GIAI_DOAN == "TTBL"))
                {
                    data.BM01D.Add(new ReportModel
                    {
                        Id = i.CODE,
                        Parent = "B.I.3",
                        Name = i.NAME,
                        Col1 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4001")?.Sum(x => x.VALUE) ?? 0,
                        Col2 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4002")?.Sum(x => x.VALUE) ?? 0,
                        Col3 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4003")?.Sum(x => x.VALUE) ?? 0,
                        Col4 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4010")?.Sum(x => x.VALUE) ?? 0,
                        Col5 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4011")?.Sum(x => x.VALUE) ?? 0,
                        Col6 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4012")?.Sum(x => x.VALUE) ?? 0,
                    });
                }

                data.BM01D.Add(new ReportModel
                {
                    Id = "B.II",
                    Stt = "II",
                    Name = "II. Đầu tư xây dựng cơ bản",
                    Parent = "B",
                    IsBold = true,
                });
                data.BM01D.Add(new ReportModel
                {
                    Id = "B.II.1",
                    Stt = "1",
                    Name = "1. Các dự án chuẩn bị đầu tư",
                    Parent = "B.II",
                });
                foreach (var i in lstProjectXdcb.Where(x => x.YEAR == year && x.GIAI_DOAN == "CBDT"))
                {
                    data.BM01D.Add(new ReportModel
                    {
                        Id = i.CODE,
                        Parent = "B.II.1",
                        Name = i.NAME,
                        Col1 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4001")?.Sum(x => x.VALUE) ?? 0,
                        Col2 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4002")?.Sum(x => x.VALUE) ?? 0,
                        Col3 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4003")?.Sum(x => x.VALUE) ?? 0,
                        Col4 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4010")?.Sum(x => x.VALUE) ?? 0,
                        Col5 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4011")?.Sum(x => x.VALUE) ?? 0,
                        Col6 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4012")?.Sum(x => x.VALUE) ?? 0,
                    });
                }

                data.BM01D.Add(new ReportModel
                {
                    Id = "B.II.2",
                    Stt = "2",
                    Name = "2. Các dự án thực hiện đầu tư",
                    Parent = "B.II",
                });
                foreach (var i in lstProjectXdcb.Where(x => x.YEAR == year && x.GIAI_DOAN == "TTDT"))
                {
                    data.BM01D.Add(new ReportModel
                    {
                        Id = i.CODE,
                        Parent = "B.II.2",
                        Name = i.NAME,
                        Col1 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4001")?.Sum(x => x.VALUE) ?? 0,
                        Col2 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4002")?.Sum(x => x.VALUE) ?? 0,
                        Col3 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4003")?.Sum(x => x.VALUE) ?? 0,
                        Col4 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4010")?.Sum(x => x.VALUE) ?? 0,
                        Col5 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4011")?.Sum(x => x.VALUE) ?? 0,
                        Col6 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4012")?.Sum(x => x.VALUE) ?? 0,
                    });
                }
                #endregion

                return data;
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                return new ReportDataCenter();
            }
        }
        public ReportDataCenter GenDataBM02A(int year)
        {
            try
            {
                var data = new ReportDataCenter();
                var lstProject = UnitOfWork.Repository<ProjectRepo>().Queryable().Where(x => x.YEAR > 0).ToList();
                var lstProjectTtb = lstProject.Where(x => x.LOAI_HINH == "TTB").ToList();
                var lstProjectXdcb = lstProject.Where(x => x.LOAI_HINH == "XDCB").ToList();


                var dataDTTTB = UnitOfWork.Repository<DauTuTrangThietBiDataRepo>().Queryable().Where(x => x.TIME_YEAR == year).ToList();
                var dataDTXDCB = UnitOfWork.Repository<DauTuXayDungDataRepo>().Queryable().Where(x => x.TIME_YEAR == year).ToList();

                #region 
                data.BM02A.Add(new ReportModel
                {
                    Id = "A",
                    Stt = "A",
                    Name = "A. Dự án chuyển tiếp kỳ truớc",
                    IsBold = true,
                });
                data.BM02A.Add(new ReportModel
                {
                    Id = "A.I",
                    Stt = "I",
                    Name = "I. Đầu tư trang thiết bị",
                    Parent = "A",
                    IsBold = true,
                });
                data.BM02A.Add(new ReportModel
                {
                    Id = "A.I.1",
                    Stt = "1",
                    Name = "1. Các dự án chuẩn bị đầu tư",
                    Parent = "A.I",
                });
                foreach (var i in lstProjectTtb.Where(x => x.YEAR < year && x.GIAI_DOAN == "CBDT"))
                {
                    data.BM02A.Add(new ReportModel
                    {
                        Id = i.CODE,
                        Parent = "A.I.1",
                        Name = i.NAME,
                        Col1 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4001")?.Sum(x => x.VALUE) ?? 0,
                        Col2 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4002")?.Sum(x => x.VALUE) ?? 0,
                        Tdth = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.PROCESS ?? "",
                        Col4 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4010")?.Sum(x => x.VALUE) ?? 0,
                        Col5 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4011")?.Sum(x => x.VALUE) ?? 0,
                        Tdtk = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.TDTK ?? "",
                        Col7 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4020")?.Sum(x => x.VALUE) ?? 0,
                        Col8 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4021")?.Sum(x => x.VALUE) ?? 0,
                        Des = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.DESCRIPTION ?? "",
                    });
                }


                data.BM02A.Add(new ReportModel
                {
                    Id = "A.I.2",
                    Stt = "2",
                    Name = "2. Các dự án thực hiện đầu tư",
                    Parent = "A.I",
                });
                foreach (var i in lstProjectTtb.Where(x => x.YEAR < year && x.GIAI_DOAN == "TTDT"))
                {
                    data.BM02A.Add(new ReportModel
                    {
                        Id = i.CODE,
                        Parent = "A.I.2",
                        Name = i.NAME,
                        Col1 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4001")?.Sum(x => x.VALUE) ?? 0,
                        Col2 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4002")?.Sum(x => x.VALUE) ?? 0,
                        Tdth = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.PROCESS ?? "",
                        Col4 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4010")?.Sum(x => x.VALUE) ?? 0,
                        Col5 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4011")?.Sum(x => x.VALUE) ?? 0,
                        Tdtk = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.TDTK ?? "",
                        Col7 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4020")?.Sum(x => x.VALUE) ?? 0,
                        Col8 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4021")?.Sum(x => x.VALUE) ?? 0,
                        Des = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.DESCRIPTION ?? "",
                    });
                }


                data.BM02A.Add(new ReportModel
                {
                    Id = "A.I.3",
                    Stt = "3",
                    Name = "3. Đầu tư trang thiết bị lẻ",
                    Parent = "A.I",
                });

                foreach (var i in lstProjectTtb.Where(x => x.YEAR < year && x.GIAI_DOAN == "TTBL"))
                {
                    data.BM02A.Add(new ReportModel
                    {
                        Id = i.CODE,
                        Parent = "A.I.3",
                        Name = i.NAME,
                        Col1 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4001")?.Sum(x => x.VALUE) ?? 0,
                        Col2 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4002")?.Sum(x => x.VALUE) ?? 0,
                        Tdth = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.PROCESS ?? "",
                        Col4 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4010")?.Sum(x => x.VALUE) ?? 0,
                        Col5 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4011")?.Sum(x => x.VALUE) ?? 0,
                        Tdtk = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.TDTK ?? "",
                        Col7 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4020")?.Sum(x => x.VALUE) ?? 0,
                        Col8 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4021")?.Sum(x => x.VALUE) ?? 0,
                        Des = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.DESCRIPTION ?? "",
                    });
                }

                data.BM02A.Add(new ReportModel
                {
                    Id = "A.II",
                    Stt = "II",
                    Name = "II. Đầu tư xây dựng cơ bản",
                    Parent = "A",
                    IsBold = true,
                });
                data.BM02A.Add(new ReportModel
                {
                    Id = "A.II.1",
                    Stt = "1",
                    Name = "1. Các dự án chuẩn bị đầu tư",
                    Parent = "A.II",
                });
                foreach (var i in lstProjectXdcb.Where(x => x.YEAR < year && x.GIAI_DOAN == "CBDT"))
                {
                    data.BM02A.Add(new ReportModel
                    {
                        Id = i.CODE,
                        Parent = "A.II.1",
                        Name = i.NAME,
                        Col1 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4001")?.Sum(x => x.VALUE) ?? 0,
                        Col2 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4002")?.Sum(x => x.VALUE) ?? 0,
                        Tdth = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.PROCESS ?? "",
                        Col4 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4010")?.Sum(x => x.VALUE) ?? 0,
                        Col5 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4011")?.Sum(x => x.VALUE) ?? 0,
                        Tdtk = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.TDTK ?? "",
                        Col7 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4020")?.Sum(x => x.VALUE) ?? 0,
                        Col8 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4021")?.Sum(x => x.VALUE) ?? 0,
                        Des = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.DESCRIPTION ?? "",
                    });
                }

                data.BM02A.Add(new ReportModel
                {
                    Id = "A.II.2",
                    Stt = "2",
                    Name = "2. Các dự án thực hiện đầu tư",
                    Parent = "A.II",
                });
                foreach (var i in lstProjectXdcb.Where(x => x.YEAR < year && x.GIAI_DOAN == "TTDT"))
                {
                    data.BM02A.Add(new ReportModel
                    {
                        Id = i.CODE,
                        Parent = "A.II.2",
                        Name = i.NAME,
                        Col1 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4001")?.Sum(x => x.VALUE) ?? 0,
                        Col2 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4002")?.Sum(x => x.VALUE) ?? 0,
                        Tdth = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.PROCESS ?? "",
                        Col4 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4010")?.Sum(x => x.VALUE) ?? 0,
                        Col5 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4011")?.Sum(x => x.VALUE) ?? 0,
                        Tdtk = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.TDTK ?? "",
                        Col7 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4020")?.Sum(x => x.VALUE) ?? 0,
                        Col8 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4021")?.Sum(x => x.VALUE) ?? 0,
                        Des = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.DESCRIPTION ?? "",
                    });
                }

                data.BM02A.Add(new ReportModel
                {
                    Id = "B",
                    Stt = "B",
                    Name = "B. Dự án đầu tư mới",
                    IsBold = true,
                });
                data.BM02A.Add(new ReportModel
                {
                    Id = "B.I",
                    Stt = "I",
                    Name = "I. Đầu tư trang thiết bị",
                    Parent = "B",
                    IsBold = true,
                });
                data.BM02A.Add(new ReportModel
                {
                    Id = "B.I.1",
                    Stt = "1",
                    Name = "1. Các dự án chuẩn bị đầu tư",
                    Parent = "B.I",
                });
                foreach (var i in lstProjectTtb.Where(x => x.YEAR == year && x.GIAI_DOAN == "CBDT"))
                {
                    data.BM02A.Add(new ReportModel
                    {
                        Id = i.CODE,
                        Parent = "B.I.1",
                        Name = i.NAME,
                        Col1 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4001")?.Sum(x => x.VALUE) ?? 0,
                        Col2 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4002")?.Sum(x => x.VALUE) ?? 0,
                        Tdth = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.PROCESS ?? "",
                        Col4 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4010")?.Sum(x => x.VALUE) ?? 0,
                        Col5 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4011")?.Sum(x => x.VALUE) ?? 0,
                        Tdtk = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.TDTK ?? "",
                        Col7 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4020")?.Sum(x => x.VALUE) ?? 0,
                        Col8 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4021")?.Sum(x => x.VALUE) ?? 0,
                        Des = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.DESCRIPTION ?? "",
                    });
                }


                data.BM02A.Add(new ReportModel
                {
                    Id = "B.I.2",
                    Stt = "2",
                    Name = "2. Các dự án thực hiện đầu tư",
                    Parent = "B.I",
                });
                foreach (var i in lstProjectTtb.Where(x => x.YEAR == year && x.GIAI_DOAN == "TTDT"))
                {
                    data.BM02A.Add(new ReportModel
                    {
                        Id = i.CODE,
                        Parent = "B.I.2",
                        Name = i.NAME,
                        Col1 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4001")?.Sum(x => x.VALUE) ?? 0,
                        Col2 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4002")?.Sum(x => x.VALUE) ?? 0,
                        Tdth = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.PROCESS ?? "",
                        Col4 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4010")?.Sum(x => x.VALUE) ?? 0,
                        Col5 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4011")?.Sum(x => x.VALUE) ?? 0,
                        Tdtk = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.TDTK ?? "",
                        Col7 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4020")?.Sum(x => x.VALUE) ?? 0,
                        Col8 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4021")?.Sum(x => x.VALUE) ?? 0,
                        Des = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.DESCRIPTION ?? "",
                    });
                }


                data.BM02A.Add(new ReportModel
                {
                    Id = "B.I.3",
                    Stt = "3",
                    Name = "3. Đầu tư trang thiết bị lẻ",
                    Parent = "B.I",
                });

                foreach (var i in lstProjectTtb.Where(x => x.YEAR == year && x.GIAI_DOAN == "TTBL"))
                {
                    data.BM02A.Add(new ReportModel
                    {
                        Id = i.CODE,
                        Parent = "B.I.3",
                        Name = i.NAME,
                        Col1 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4001")?.Sum(x => x.VALUE) ?? 0,
                        Col2 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4002")?.Sum(x => x.VALUE) ?? 0,
                        Tdth = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.PROCESS ?? "",
                        Col4 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4010")?.Sum(x => x.VALUE) ?? 0,
                        Col5 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4011")?.Sum(x => x.VALUE) ?? 0,
                        Tdtk = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.TDTK ?? "",
                        Col7 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4020")?.Sum(x => x.VALUE) ?? 0,
                        Col8 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4021")?.Sum(x => x.VALUE) ?? 0,
                        Des = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.DESCRIPTION ?? "",
                    });
                }

                data.BM02A.Add(new ReportModel
                {
                    Id = "B.II",
                    Stt = "II",
                    Name = "II. Đầu tư xây dựng cơ bản",
                    Parent = "B",
                    IsBold = true,
                });
                data.BM02A.Add(new ReportModel
                {
                    Id = "B.II.1",
                    Stt = "1",
                    Name = "1. Các dự án chuẩn bị đầu tư",
                    Parent = "B.II",
                });
                foreach (var i in lstProjectXdcb.Where(x => x.YEAR == year && x.GIAI_DOAN == "CBDT"))
                {
                    data.BM02A.Add(new ReportModel
                    {
                        Id = i.CODE,
                        Parent = "B.II.1",
                        Name = i.NAME,
                        Col1 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4001")?.Sum(x => x.VALUE) ?? 0,
                        Col2 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4002")?.Sum(x => x.VALUE) ?? 0,
                        Tdth = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.PROCESS ?? "",
                        Col4 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4010")?.Sum(x => x.VALUE) ?? 0,
                        Col5 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4011")?.Sum(x => x.VALUE) ?? 0,
                        Tdtk = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.TDTK ?? "",
                        Col7 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4020")?.Sum(x => x.VALUE) ?? 0,
                        Col8 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4021")?.Sum(x => x.VALUE) ?? 0,
                        Des = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.DESCRIPTION ?? "",
                    });
                }

                data.BM02A.Add(new ReportModel
                {
                    Id = "B.II.2",
                    Stt = "2",
                    Name = "2. Các dự án thực hiện đầu tư",
                    Parent = "B.II",
                });
                foreach (var i in lstProjectXdcb.Where(x => x.YEAR == year && x.GIAI_DOAN == "TTDT"))
                {
                    data.BM02A.Add(new ReportModel
                    {
                        Id = i.CODE,
                        Parent = "B.II.2",
                        Name = i.NAME,
                        Col1 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4001")?.Sum(x => x.VALUE) ?? 0,
                        Col2 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4002")?.Sum(x => x.VALUE) ?? 0,
                        Tdth = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.PROCESS ?? "",
                        Col4 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4010")?.Sum(x => x.VALUE) ?? 0,
                        Col5 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4011")?.Sum(x => x.VALUE) ?? 0,
                        Tdtk = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.TDTK ?? "",
                        Col7 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4020")?.Sum(x => x.VALUE) ?? 0,
                        Col8 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4021")?.Sum(x => x.VALUE) ?? 0,
                        Des = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.DESCRIPTION ?? "",
                    });
                }
                #endregion
                foreach (var i in data.BM02A.OrderByDescending(x => x.Id))
                {
                    var child = data.BM02A.Where(x => x.Parent == i.Id).ToList();
                    i.Col1 = child.Count() == 0 ? i.Col1 : child.Sum(x => x.Col1);
                    i.Col2 = child.Count() == 0 ? i.Col2 : child.Sum(x => x.Col2);
                    i.Col4 = child.Count() == 0 ? i.Col4 : child.Sum(x => x.Col4);
                    i.Col5 = child.Count() == 0 ? i.Col5 : child.Sum(x => x.Col5);
                    i.Col7 = child.Count() == 0 ? i.Col7 : child.Sum(x => x.Col7);
                    i.Col8 = child.Count() == 0 ? i.Col8 : child.Sum(x => x.Col8);
                }
                return data;
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                return new ReportDataCenter();
            }
        }
        public ReportDataCenter GenDataBM02C(int year)
        {
            try
            {
                var service = new ElementService();
                var serviceKb = new KichBanService();
                var data = new ReportDataCenter();

                var dataKHTCPrevious = service.GetDataKeHoachTaiChinh(year - 1);
                var dataKHTC = service.GetDataKeHoachTaiChinh(year);
                var dataSXKD = serviceKb.GetData(year, "TB");

                var I = new ReportModel
                {
                    Id = "I",
                    Stt = "I",
                    Name = "I. Hoạt động SXKD",
                    IsBold = true,
                    Col7 = dataSXKD.Where(x => x.Name == "Doanh thu từ hoạt động SXKD").Sum(x => x.Value5),
                    Col8 = dataSXKD.Where(x => x.Name == "Chi phí sản xuất kinh doanh").Sum(x => x.Value5),
                };
                I.Col9 = I.Col7 - I.Col8;
                data.BM02C.Add(I);

                var II = new ReportModel
                {
                    Id = "II",
                    Stt = "II",
                    Name = "II. Hoạt động tài chính",
                    IsBold = true,
                    Col1 = dataKHTCPrevious.KeHoachTaiChinhData.Where(x => x.ElementCode == "U0137").Sum(x => x.Value),
                    Col2 = dataKHTCPrevious.KeHoachTaiChinhData.Where(x => x.ElementCode == "U0138").Sum(x => x.Value),
                    Col7 = dataKHTC.KeHoachTaiChinhData.Where(x => x.ElementCode == "U0137").Sum(x => x.Value),
                    Col8 = dataKHTC.KeHoachTaiChinhData.Where(x => x.ElementCode == "U0138").Sum(x => x.Value),
                };
                II.Col3 = II.Col1 - II.Col2;
                II.Col9 = II.Col7 - II.Col8;
                data.BM02C.Add(II);

                data.BM02C.Add(new ReportModel
                {
                    Id = "III",
                    Stt = "III",
                    Name = "III. Hoạt động khác",
                    IsBold = true
                });
                return data;
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                return new ReportDataCenter();
            }
        }
        public ReportDataCenter GenDataBM02D (int year, string kichBan)
        {
            try
            {
                var service = new KichBanService();
                var dataSXKDCurrent = service.GetData(year, kichBan);
                var data = new ReportDataCenter();
                foreach(var item in ElementDataReport)
                {
                    data.BM02D.Add(new ReportModel
                    {
                        Id = item.Id,
                        Parent = item.Parent,
                        Name = item.Name,
                        Unit = item.Unit,
                        IsBold = item.IsBold,
                        Col2 = dataSXKDCurrent.Where(x => x.Id == item.Id).Sum(x => x.Value2),
                        Col4 = dataSXKDCurrent.Where(x => x.Id == item.Id).Sum(x => x.Value5),
                    });
                }
                return data;
            }
            catch(Exception ex)
            {
                UnitOfWork.Rollback();
                return new ReportDataCenter();
            }
        }

        public ReportDataCenter GenDataBM02D1(int year, string phienBan)
        {
            try
            {
                var service = new PhienBanService();
                var dataSXKDCurrent = service.GetData(year, phienBan);
                var data = new ReportDataCenter();
                foreach (var item in ElementDataReport)
                {
                    data.BM02D1.Add(new ReportModel
                    {
                        Id = item.Id,
                        Parent = item.Parent,
                        Name = item.Name,
                        Unit = item.Unit,
                        IsBold = item.IsBold,
                        Col1 = dataSXKDCurrent.Where(x => x.Id == item.Id).Sum(x => x.Value1),
                        Col2 = dataSXKDCurrent.Where(x => x.Id == item.Id).Sum(x => x.Value2),
                        Col3 = dataSXKDCurrent.Where(x => x.Id == item.Id).Sum(x => x.Value3),
                    });
                }
                return data;
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                return new ReportDataCenter();
            }
        }
        public static List<ReportModel> ElementDataReport = new List<ReportModel>(){
            new ReportModel()
            {
                Id = "I",
                Name = "I. Sản lượng",
                IsBold = true,
            },
            new ReportModel()
            {
                Id = "I.1",
                Parent = "I",
                Name = "1. Cung ứng cho VNA Group",
            },
            new ReportModel()
            {
                Id = "I.1.1",
                Parent = "I.1",
                Name = "- Cung ứng cho VNA",
            },
            new ReportModel()
            {
                Id = "I.1.2",
                Parent = "I.1",
                Name = "- Cung ứng cho các DN khác trong VNA Group",
            },
            new ReportModel()
            {
                Id = "I.2",
                Parent = "I",
                Name = "2. Cung ứng cho đối tác khác (*)",
            },
            new ReportModel()
            {
                Id = "II",
                Name = "II. Doanh thu từ hoạt động SXKD",
                IsBold = true,
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "II.1",
                Parent = "II",
                Name = "1. Doanh thu cung ứng cho VNA Group",
                Unit = "Tr.đ/USD",
            },
            new ReportModel()
            {
                Id = "II.1.1",
                Parent = "II.1",
                Name = "- Doanh thu VNA",
                Unit = "Tr.đ/USD",
            },
            new ReportModel()
            {
                Id = "II.1.1.1",
                Parent = "II.1.1",
                Name = "Trong đó: CK/Giảm giá",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "II.1.2",
                Parent = "II.1",
                Name = "- Doanh thu các DN khác trong VNA group",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "II.1.2.1",
                Parent = "II.1.2",
                Name = "Trong đó: CK/Giảm giá",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "II.2",
                Parent = "II",
                Name = "2. Doanh thu cung ứng cho đối tác khác (*)",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "II.2.1",
                Parent = "II.2",
                Name = "Trong đó: CK/Giảm giá",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III",
                Name = "III. Các khoản chi phí",
                Unit = "Tr.đ/USD",
                IsBold = true
            },
            new ReportModel()
            {
                Id = "III.1",
                Parent = "III",
                Name = "1. Chi phí dịch vụ mua ngoài",
                Unit = "Tr.đ/USD",
            },
            new ReportModel()
            {
                Id = "III.1.1",
                Parent = "III.1",
                Name = "1.1. Thuê phương tiện vận tải, trang thiết bị",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.1.2",
                Parent = "III.1",
                Name = "1.2. Chi phí thuê văn phòng, mặt bằng",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.1.3",
                Parent = "III.1",
                Name = "1.3. Chi phí thông tin liên lạc",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.1.4",
                Parent = "III.1",
                Name = "1.4. Chi phí quảng cáo, tiếp thị",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.1.5",
                Parent = "III.1",
                Name = "1.5. Điện nước",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.1.6",
                Parent = "III.1",
                Name = "1.6. Chi phí mua bảo hiểm bắt buộc (BH tài sản, tai nạn lao động, cháy nổ…)",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.1.7",
                Parent = "III.1",
                Name = "1.7. Chi phí dịch vụ tư vấn, kiểm toán",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.1.8",
                Parent = "III.1",
                Name = "1.8. Hoa hồng, môi giới, đại lý",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.1.9",
                Parent = "III.1",
                Name = "1.9. Chi phí dịch vụ mua ngoài khác",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2",
                Parent = "III",
                Name = "2. Chi khác bằng tiền",
                Unit = "Tr.đ/USD",
            },
            new ReportModel()
            {
                Id = "III.2.1",
                Parent = "III.2",
                Name = "2.1. Chi đồng phục",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2.2",
                Parent = "III.2",
                Name = "2.2. Chi bồi dưỡng độc hại",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2.3",
                Parent = "III.2",
                Name = "2.3. Bảo hộ lao động",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2.4",
                Parent = "III.2",
                Name = "2.4. Văn phòng phẩm, in ấn",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2.5",
                Parent = "III.2",
                Name = "2.5. Tiếp khách, hội nghị, xúc tiến thương mại",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2.6",
                Parent = "III.2",
                Name = "2.6. Công tác phí",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2.6.1",
                Parent = "III.2.6",
                Name = "- Trong nước",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2.6.2",
                Parent = "III.2.6",
                Name = "- Ngoài nước",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "II.2.7",
                Parent = "III.2",
                Name = "2.7. Chi phí y tế",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2.8",
                Parent = "III.2",
                Name = "2.8. Chi đào tạo",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2.8.1",
                Parent = "III.2.8",
                Name = "- Trong nước",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2.8.2",
                Parent = "III.2.8",
                Name = "- Ngoài nước",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2.9",
                Parent = "III.2",
                Name = "2.9. Chi phòng cháy chữa cháy, phòng chống bão lụt, trực tự vệ",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2.10",
                Parent = "III.2",
                Name = "2.10. Chi vệ sinh văn phòng, diệt côn trùng, cây cảnh",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2.11",
                Parent = "III.2",
                Name = "2.11. Chi phí môi trường",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2.12",
                Parent = "III.2",
                Name = "2.12. Chi có tính chất phúc lợi: hiếu hỉ, nghỉ mát, thăm hỏi…",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2.13",
                Parent = "III.2",
                Name = "2.13. Chi bảo hiểm hưu trí tự nguyện",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2.14",
                Parent = "III.2",
                Name = "2.14. Thủ tục phí ngân hàng",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2.15",
                Parent = "III.2",
                Name = "2.15. Các khoản chi khác",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2.15.1",
                Parent = "III.2.15",
                Name = "- Mua tài liệu",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2.15.2",
                Parent = "III.2.15",
                Name = "- Kiểm định xe ô tô / thiết bị",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2.15.3",
                Parent = "III.2.15",
                Name = "- Chi khác ....",
                Unit = "Tr.đ/USD"
            },
        };
    }

    public class ReportDataCenter
    {
        public List<ReportModel> BM01D { get; set; } = new List<ReportModel>();
        public List<ReportModel> BM02A { get; set; } = new List<ReportModel>();
        public List<ReportModel> BM02C { get; set; } = new List<ReportModel>();
        public List<ReportModel> BM02D { get; set; } = new List<ReportModel>();
        public List<ReportModel> BM02D1 { get; set; } = new List<ReportModel>();
    }

    public class ReportModel
    {
        public string ElementCode { get; set; }
        public string Unit { get; set; }
        public string Stt { get; set; }
        public string Id { get; set; }
        public string Parent { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public bool IsBold { get; set; }
        public decimal? Col1 { get; set; }
        public decimal? Col2 { get; set; }
        public decimal? Col3 { get; set; }
        public decimal? Col4 { get; set; }
        public decimal? Col5 { get; set; }
        public decimal? Col6 { get; set; }
        public decimal? Col7 { get; set; }
        public decimal? Col8 { get; set; }
        public decimal? Col9 { get; set; }
        public decimal? Col10 { get; set; }
        public decimal? Col11 { get; set; }
        public decimal? Col12 { get; set; }
        public decimal? Col13 { get; set; }
        public decimal? Col14 { get; set; }
        public decimal? Col15 { get; set; }
        public decimal? Col16 { get; set; }
        public decimal? Col17 { get; set; }
        public decimal? Col18 { get; set; }
        public decimal? Col19 { get; set; }
        public decimal? Col20 { get; set; }
        public string Tdth { get; set; }
        public string Tdtk { get; set; }
        public string Des { get; set; }
    }
}