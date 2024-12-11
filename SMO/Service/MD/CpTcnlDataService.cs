using iTextSharp.text;
using SMO.Core.Entities;
using SMO.Core.Entities.MD;
using SMO.Models;
using SMO.Repository.Implement.MD;
using SMO.Repository.Interface.MD;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMO.Service.MD
{
    public class CpTcnlDataService : GenericService<T_MD_CP_TCNL_DATA, CptcnlDataRepo>
    {
        public CpTcnlDataService() : base()
        {

        }
        public IList<CpTcnlImport> GetdataCpnl(int year)
        {
            try
            {
                var listDataHeader = UnitOfWork.Repository<CptcnlRepo>().GetAll();
                var data = new List<CpTcnlImport>();
                var listData = UnitOfWork.Repository<CptcnlDataRepo>().Queryable().Where(x => x.YEAR == year).ToList();
                var a = UnitOfWork.Repository<CptcnlDataRepo>().GetAll();
                foreach (var e in listDataHeader)
                {
                   
                    var temp = new CpTcnlImport
                    {
                        ID = e.ID,
                        PARENT = e.PARENT,
                        STT = e.STT,
                        NAME = e.NAME,
                        IS_BOLD = e.IS_BOLD,
                        U_CBQL = listData.FirstOrDefault(x => x.ID_CENTER == e.ID && x.YEAR == year)?.U_CBQL,
                        U_CQCT = listData.FirstOrDefault(x => x.ID_CENTER == e.ID && x.YEAR == year)?.U_CQCT,
                        U_OIL_SOUCE = listData.FirstOrDefault(x => x.ID_CENTER == e.ID && x.YEAR == year)?.U_OIL_SOUCE,
                        U_MB = listData.FirstOrDefault(x => x.ID_CENTER == e.ID && x.YEAR == year)?.U_MB,
                        U_MT = listData.FirstOrDefault(x => x.ID_CENTER == e.ID && x.YEAR == year)?.U_MT,
                        U_VT = listData.FirstOrDefault(x => x.ID_CENTER == e.ID && x.YEAR == year)?.U_VT,
                        U_MN = listData.FirstOrDefault(x => x.ID_CENTER == e.ID && x.YEAR == year)?.U_MN,

                    };
                    data.Add(temp);
                }
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
                this.State = false;
                this.Exception = ex;
                return new List<CpTcnlImport>();
            }

        }
        public void UpdateData(List<CpTcnlImport> data, int year)
        {
            try
            {
                var databytime = UnitOfWork.Repository<CptcnlDataRepo>().Queryable().Where(x => x.YEAR == year).ToList();
                UnitOfWork.BeginTransaction();
                foreach (var item in data)
                {
                    var chexexist = databytime.FirstOrDefault(x => x.ID_CENTER == item.ID && x.YEAR == year);
                    if (chexexist == null)
                    {
                        var pdata = new T_MD_CP_TCNL_DATA
                        {

                            ID = Guid.NewGuid(),
                            ID_CENTER = item.ID,
                            U_CBQL = item.U_CBQL,
                            U_CQCT = item.U_CQCT,
                            U_OIL_SOUCE = item.U_OIL_SOUCE,
                            U_MB = item.U_MB,
                            U_MT = item.U_MT,
                            U_VT = item.U_VT,
                            U_MN = item.U_MN,
                            YEAR = year,

                        };
                        UnitOfWork.Repository<CptcnlDataRepo>().Create(pdata);
                    }
                    else
                    {
                        chexexist.U_CBQL = item.U_CBQL;
                        chexexist.U_CQCT = item.U_CQCT;
                        chexexist.U_OIL_SOUCE = item.U_OIL_SOUCE;
                        chexexist.U_MB = item.U_MB;
                        chexexist.U_MT = item.U_MT;
                        chexexist.U_VT = item.U_VT;
                        chexexist.U_MN = item.U_MN;
                        
                        UnitOfWork.Repository<CptcnlDataRepo>().Update(chexexist);
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
    }
    public class CpTcnlImport
    {

        public string ID { get; set; }
        public string PARENT { get; set; }
        public bool IS_BOLD { get; set; }
        public string STT { get; set; }
        public string NAME { get; set; }
        public decimal? U_CBQL { get; set; }
        public decimal? U_CQCT { get; set; }
        public decimal? U_OIL_SOUCE { get; set; }
        public decimal? U_MB { get; set; }
        public decimal? U_MT { get; set; }
        public decimal? U_VT { get; set; }
        public decimal? U_MN { get; set; }

    }

}
   
