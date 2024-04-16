using NHibernate.Criterion;
using NPOI.SS.Formula.Functions;
using SMO.Repository.Common;
using SMO.Repository.Implement.BP.DAU_TU_TRANG_THIET_BI;
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
                var lstProject = UnitOfWork.Repository<ProjectRepo>().GetAll().ToList();
                var dataDTTTB = UnitOfWork.Repository<DauTuTrangThietBiDataRepo>().Queryable().Where(x => x.TIME_YEAR == year).ToList();

                var order = 1;
                foreach (var item in lstProject)
                {
                    var dtttb = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == item.CODE);
                    data.BM01D.Add(new ReportModel
                    {
                        Stt = order.ToString(),
                        Name = item.NAME,
                        Col1 = dtttb.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4001").Count() == 0 ? 0 : dtttb.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4001").Sum(x => x.VALUE),
                        Col2 = dtttb.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4002").Count() == 0 ? 0 : dtttb.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4002").Sum(x => x.VALUE),
                        Col3 = dtttb.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4011").Count() == 0 ? 0 : dtttb.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "4011").Sum(x => x.VALUE),
                    });
                    order++;
                }
                return data;
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                return new ReportDataCenter();
            }
        }
    }

    public class ReportDataCenter
    {
        public List<ReportModel> BM01D { get; set; } = new List<ReportModel>();
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
    }
}