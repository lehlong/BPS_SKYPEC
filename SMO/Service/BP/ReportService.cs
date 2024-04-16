using NHibernate.Criterion;
using NPOI.SS.Formula.Functions;
using SMO.Repository.Common;
using SMO.Repository.Implement.BP.DAU_TU_TRANG_THIET_BI;
using SMO.Repository.Implement.BP.DAU_TU_XAY_DUNG;
using SMO.Repository.Implement.MD;
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
                    IsBold= true,
                });
                data.BM01D.Add(new ReportModel
                {
                    Id = "A.I.1",
                    Stt = "1",
                    Name = "1. Các dự án chuẩn bị đầu tư",
                    Parent = "A.I",
                });
                foreach(var i in lstProjectTtb.Where(x => x.YEAR < year && x.GIAI_DOAN == "CBDT"))
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
                        Tdtk = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE ).FirstOrDefault()?.TDTK ?? "",
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
                data.BM02A = RecursiveData(data.BM02A);
                return data;
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                return new ReportDataCenter();
            }
        }

        public List<ReportModel> RecursiveData(List<ReportModel> data)
        {
            foreach (var i in data.OrderByDescending(x => x.Id))
            {
                var child = data.Where(x => x.Parent == i.Id).ToList();
                i.Col1 = child.Count() == 0 ? i.Col1 : child.Sum(x => x.Col1);
                i.Col2 = child.Count() == 0 ? i.Col2 : child.Sum(x => x.Col2);
                i.Col4 = child.Count() == 0 ? i.Col4 : child.Sum(x => x.Col4);
                i.Col5 = child.Count() == 0 ? i.Col5 : child.Sum(x => x.Col5);
                i.Col7 = child.Count() == 0 ? i.Col7 : child.Sum(x => x.Col7);
                i.Col8 = child.Count() == 0 ? i.Col8 : child.Sum(x => x.Col8);
            }
            return data;
        }
    }

    public class ReportDataCenter
    {
        public List<ReportModel> BM01D { get; set; } = new List<ReportModel>();
        public List<ReportModel> BM02A { get; set; } = new List<ReportModel>();
    }

    public class ReportModel
    {
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