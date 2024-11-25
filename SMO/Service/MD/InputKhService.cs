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
                var listReport = UnitOfWork.Repository<ReportSXKDElementRepo>().Queryable().OrderBy(x=>x.C_ORDER).ToList();
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


                        };
                        UnitOfWork.Repository<InputKhRepo>().Create(pdata);
                    }
                    else
                    {
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
    }
    }