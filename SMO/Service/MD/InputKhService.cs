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
         public IList<T_MD_HEADER_DM> GetdataDm(int year)
        {
            try
            {
                var data = new List<T_MD_HEADER_DM>();
                var listReport = UnitOfWork.Repository<HeaderDmRepo>().Queryable().Select(x=> new {x.NAME,x.STT}).OrderBy(x => x.STT).ToList();
                var listInput = UnitOfWork.Repository<DataDmRepo>().Queryable().Where(x => x.YEAR == year).ToList();

                var listData = from table1 in listReport
                               join table2 in listInput
                               on table1.STT equals table2.ID_CENTER into table3
                               from col in table3.DefaultIfEmpty()
                               select new { table1, col };

                foreach (var e in listData)
                {
                    var temp = new T_MD_HEADER_DM
                    {
                        STT = e.table1.STT,
                        NAME = e.table1.NAME,
                        VALUE = e.col?.VALUE,
                        NOTE=e.col?.NOTE,    
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
                return new List<T_MD_HEADER_DM>();
            }

        }

        public IList<T_MD_DATA_TRA_NAP> GetdataTraNapIP(int year)
        {
            try
            {
                var data = new List<T_MD_DATA_TRA_NAP>();
       
                var headerTraNap = UnitOfWork.Repository<SanBayRepo>().Queryable().Where(x => x.OTHER_PM_CODE != null && x.OTHER_PM_CODE != "").ToList();
                var dataTraNap = UnitOfWork.Repository<DataTraNapRepo>().Queryable().Where(x => x.YEAR == year).ToList();
                var listData = from table1 in headerTraNap
                               join table2 in dataTraNap
                               on table1.CODE equals table2.ID_CENTER into table3
                               from col in table3.DefaultIfEmpty()
                               select new { table1, col };
                foreach (var e in listData)
                {
                    var temp = new T_MD_DATA_TRA_NAP
                    {
                        VALUE=e.col?.VALUE,
                        ID_CENTER = e.table1.CODE
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
                return new List<T_MD_DATA_TRA_NAP>();
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
                            C_ORDER = item.C_ORDER,
                            NAME = item.NAME,
                            STT = item.STT,
                            YEAR = year,
                            IS_BOLD = item.IS_BOLD,
                            KH_V2 = item.KH_V2,
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
        public void UpdateDM(List<T_MD_HEADER_DM> data, int year)
        {
            try
            {
                var databytime = UnitOfWork.Repository<DataDmRepo>().Queryable().Where(x => x.YEAR == year).ToList();
                UnitOfWork.BeginTransaction();
                foreach (var item in data)
                {
                    var chexexist = databytime.FirstOrDefault(x => x.ID_CENTER == item.STT && x.YEAR == year);
                    if (chexexist == null)
                    {
                        var pdata = new T_MD_DATA_DM
                        {

                            ID = Guid.NewGuid(),
                            ID_CENTER = item.STT,
                            VALUE = item.VALUE,
                            YEAR = year,
                            NOTE=item.NOTE,
                        };
                      
                        UnitOfWork.Repository<DataDmRepo>().Create(pdata);
                    }
                    else
                    {
                        chexexist.VALUE = item.VALUE;
                        chexexist.NOTE = item.NOTE;
                        
                        UnitOfWork.Repository<DataDmRepo>().Update(chexexist);
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
        public void UpdateTraNap(List<T_MD_DATA_TRA_NAP> data, int year)
        {
            try
            {
                var dataTraNap = UnitOfWork.Repository<DataTraNapRepo>().Queryable().Where(x => x.YEAR == year).ToList();
                UnitOfWork.BeginTransaction();
                foreach (var item in data)
                {
                    var chexexist = dataTraNap.FirstOrDefault(x => x.ID_CENTER == item.ID_CENTER && x.YEAR == year);
                    if (chexexist == null)
                    {
                        var pdata = new T_MD_DATA_TRA_NAP
                        {

                            ID = Guid.NewGuid(),
                            ID_CENTER = item.ID_CENTER,
                            VALUE = item.VALUE,
                            YEAR = year,

                        };
                       
                        UnitOfWork.Repository<DataTraNapRepo>().Create(pdata);
                    }
                    else
                    {
                        chexexist.VALUE = item.VALUE;
                       
                        UnitOfWork.Repository<DataTraNapRepo>().Update(chexexist);
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