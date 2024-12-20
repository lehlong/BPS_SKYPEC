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
    public class InputKhService : GenericService<T_MD_INPUT_CHI_PHI, InputChiPhiRepo>
    {
        public InputKhService() : base()
        {

        }
        public IList<T_MD_INPUT_KH> GetdataKh(int year)
        {
            try
            {
                var data = new List<T_MD_INPUT_KH>();
                var listReport = UnitOfWork.Repository<ReportSXKDElementRepo>().Queryable().OrderBy(x => x.C_ORDER).ToList();
                var listInput = UnitOfWork.Repository<InputKhRepo>().Queryable().Where(x => x.YEAR == year).ToList();

                var listData = from table1 in listReport
                               join table2 in listInput
                               on table1.ID equals table2.ID_CENTER into table3
                               from col in table3.DefaultIfEmpty()
                               select new { table1, col };

                foreach (var e in listData)
                {
                    var temp = new T_MD_INPUT_KH
                    {

                        ID = e.table1.ID,
                        PARENT = e.table1.PARENT,
                        C_ORDER = e.table1.C_ORDER,
                        IS_BOLD = e.table1.IS_BOLD,
                        STT = e.table1.STT,
                        NAME = e.table1.NAME,
                        DVT = e.table1.DVT,
                        KH_V2 = e.col?.KH_V2,
                        DN9T = e.col?.DN9T,
                        UTH = e.col?.UTH,
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
                return new List<T_MD_INPUT_KH>();
            }

        }

        public IList<dataGtgn> GetdataGtdn(int year)
        {
            try
            {
                var data = new List<dataGtgn>();
               var project= UnitOfWork.Repository<ProjectRepo>().Queryable().Where(x=>x.YEAR==year).ToList();
                var inputGn = UnitOfWork.Repository<InputGtgnRepo>().Queryable().Where(x => x.TIME_YEAR == year).ToList();
                for (int i = 0; i < project.Count; i++)
                {
                    var temp = new dataGtgn
                    {
                        CODE = project[i].CODE,
                        NAME = project[i].NAME,
                        UTH = inputGn.FirstOrDefault(x => x.PROJECT_CODE == project[i].CODE)?.UTH??0,
                        KH = inputGn.FirstOrDefault(x => x.PROJECT_CODE == project[i].CODE)?.KH ??0,
                        DN9T = inputGn.FirstOrDefault(x => x.PROJECT_CODE == project[i].CODE)?.DN9T??0,
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
                return new List<dataGtgn>();
            }

        }
        public class dataGtgn
        {
            public string CODE { get; set; }
            public string NAME { get; set; }
            public decimal? UTH { get; set; }
            public decimal? KH { get; set; }
            public decimal? DN9T { get; set; }
        }
        public IList<dataDm> GetdataDm(int year)
        {
            try
            {
                var data = new List<dataDm>();
                var listReport = UnitOfWork.Repository<HeaderDmRepo>().GetAll();
                var listInput = UnitOfWork.Repository<DataDmRepo>().Queryable().Where(x => x.YEAR == year).ToList();

                var listData = from table1 in listReport
                               join table2 in listInput
                               on table1.ID equals table2.ID_CENTER into table3
                               from col in table3.DefaultIfEmpty()
                               select new { table1, col };

                foreach (var e in listData)
                {
                    var temp = new dataDm
                    {

                        ID = e.table1.ID,
                        DVT = e.table1.DVT,
                        STT = e.table1.STT,
                        NAME = e.table1.NAME,
                        Value = e.col.VALUE,
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
                return new List<dataDm>();
            }

        }
        public void UpdateData(List<T_MD_INPUT_KH> data, int year)
        {
            try
            {
                var databytime = UnitOfWork.Repository<InputKhRepo>().Queryable().Where(x => x.YEAR == year).ToList();
                UnitOfWork.BeginTransaction();
                foreach (var item in data)
                {
                    var chexexist = databytime.FirstOrDefault(x => x.ID_CENTER == item.ID_CENTER && x.YEAR == year);
                    if (chexexist == null)
                    {
                        var pdata = new T_MD_INPUT_KH
                        {
                       
                            ID = Guid.NewGuid(),
                            PARENT = item.PARENT,
                            C_ORDER= item.C_ORDER,
                            NAME = item.NAME,
                            STT = item.STT,
                            YEAR = year,
                            IS_BOLD = item.IS_BOLD,
                            KH_V2=item.KH_V2,
                            ID_CENTER = item.ID_CENTER,
                            UTH = item.UTH,
                            DN9T = item.DN9T,

                        };
                        UnitOfWork.Repository<InputKhRepo>().Create(pdata);
                    }
                    else
                    {
                        chexexist.DN9T = item.DN9T;
                        chexexist.UTH = item.UTH;   
                        chexexist.KH_V2 = item.KH_V2;
                        UnitOfWork.Repository<InputKhRepo>().Update(chexexist);
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
        public void UpdateDataGtGn(List<T_MD_INPUT_GTDN> data, int year)
        {
            try
            {
                var databytime = UnitOfWork.Repository<InputGtgnRepo>().Queryable().Where(x => x.TIME_YEAR == year).ToList();
                UnitOfWork.BeginTransaction();
                foreach (var item in data)
                {
                    var chexexist = databytime.FirstOrDefault(x => x.PROJECT_CODE == item.PROJECT_CODE && x.TIME_YEAR == year);
                    if (chexexist == null)
                    {
                        var pdata = new T_MD_INPUT_GTDN
                        {
                            ID = Guid.NewGuid(),
                            PROJECT_CODE = item.PROJECT_CODE,
                         
                            KH = item.KH,
                       
                            TIME_YEAR = year,
                        };
                       
                        UnitOfWork.Repository<InputGtgnRepo>().Create(pdata);
                    }
                    else
                    {
                    
                        chexexist.KH = item.KH;
                        UnitOfWork.Repository<InputGtgnRepo>().Update(chexexist);
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
    public class dataDm
    {
        public string ID { get; set; }
        public string NAME { get; set; }
        public string NOTE { get; set; }
        public int STT { get; set; }
        public string DVT { get; set; }
        public int YEAR { get; set; }
        public decimal?  Value { get; set; }
    }
}