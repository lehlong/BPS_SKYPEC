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
    public class InputChiPhiService :GenericService<T_MD_INPUT_CHI_PHI,InputChiPhiRepo>
    {
        public InputChiPhiService():base()
        {
            
        }
        public IList<T_MD_INPUT_CHI_PHI>GetDataChiPhi(int year,string area)
        {
            try
            {
               var  data= new List<T_MD_INPUT_CHI_PHI>();
                var listCP = UnitOfWork.Repository<ReportChiPhiCodeRepo>().Queryable().ToList();

                var listIP=UnitOfWork.Repository<InputChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year&& x.AREA_CODE==area).ToList();
                var ListAll = UnitOfWork.Repository<InputChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year).GroupBy(x=>x.ID_CENTER).Select(g => new T_MD_INPUT_CHI_PHI
                {
                    ID_CENTER = g.Key,
                    UOC_THUC_HIEN = g.Sum(x => x.UOC_THUC_HIEN),
                    TH9T = g.Sum(x => x.TH9T)
                }).ToList();
                var TB2 = string.IsNullOrEmpty(area) ? ListAll : listIP;
             

                var listData = from table1 in listCP
                               join table2 in TB2
                               on table1.ID equals table2.ID_CENTER into table3
                               from col in table3.DefaultIfEmpty()
                               select new { table1, col };
                
                 

                foreach (var i in listData)
                {
                    var item = new T_MD_INPUT_CHI_PHI
                    {
                        
                        ID_CENTER = i.table1.ID,
                        GROUP_1_ID = i.table1.GROUP_1_ID,
                        GROUP_2_ID = i.table1.GROUP_2_ID,
                        GROUP_NAME = i.table1.GROUP_NAME,
                        UOC_THUC_HIEN = i.col?.UOC_THUC_HIEN,
                        TH9T = i.col?.TH9T,
                        C_ORDER = i.table1.C_ORDER,
                        STT = i.table1.STT,
                  
                        IS_BOLD = i.table1.IS_BOLD,
                        TIME_YEAR=year
                    };
                    data.Add(item);

                }

                return data;
            }
            catch(Exception ex)
            {
                UnitOfWork.Rollback();
                this.State = false;
                this.Exception = ex;
                return new List<T_MD_INPUT_CHI_PHI>();
            }
        }
        public void UpdateData(List<T_MD_INPUT_CHI_PHI> data, int year, string area)
        {
            try
            {
                var databytime = UnitOfWork.Repository<InputChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year).ToList();
                UnitOfWork.BeginTransaction();
                foreach (var item in data)
                {
                        var chexexist = databytime.FirstOrDefault(x => x.ID_CENTER == item.ID_CENTER);
                    if(chexexist== null)
                    {
                        var pdata = new T_MD_INPUT_CHI_PHI
                        {
                            ID = Guid.NewGuid().ToString(),
                            ID_CENTER = item.ID_CENTER,
                            GROUP_1_ID = item.GROUP_1_ID,
                            GROUP_2_ID = item.GROUP_2_ID,
                            GROUP_NAME = item.GROUP_NAME,
                            UOC_THUC_HIEN = item.UOC_THUC_HIEN,
                            TH9T = item.TH9T,
                            C_ORDER = item.C_ORDER,
                            STT = item.STT,
                            TIME_YEAR = year,
                            IS_BOLD = item.IS_BOLD,
                            AREA_CODE=area
                        };
                        UnitOfWork.Repository<InputChiPhiRepo>().Create(pdata);
                            }
                    else
                    {
                        chexexist.UOC_THUC_HIEN = item.UOC_THUC_HIEN;
                        chexexist.TH9T = item.TH9T;
                        UnitOfWork.Repository<InputChiPhiRepo>().Update(chexexist);
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
}