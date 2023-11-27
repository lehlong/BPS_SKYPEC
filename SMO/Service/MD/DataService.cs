using SMO.Core.Entities;
using SMO.Models;
using SMO.Repository.Implement.MD;

using System;
using System.Collections.Generic;
using System.Linq;

namespace SMO.Service.MD
{
    public class DataService : GenericService<T_MD_DATA, DataRepo>
    {
        public DataService() : base()
        {

        }

        public IList<T_MD_DATA> GetDataByTimeYear(int year)
        {
            try
            {
                var checkData = UnitOfWork.Repository<DataRepo>().Queryable().Any(x => x.TIME_YEAR == year);
                if (!checkData)
                {
                    UnitOfWork.BeginTransaction();
                    var lstRoute = UnitOfWork.Repository<RouteRepo>().GetAll().ToList();
                    foreach (var route in lstRoute)
                    {
                        var item = new T_MD_DATA
                        {
                            ID = Guid.NewGuid(),
                            ROUTE_CODE = route.CODE,
                            ROUTE = route.NAME,
                            TIME_YEAR = year
                        };
                        UnitOfWork.Repository<DataRepo>().Create(item);
                    }
                    UnitOfWork.Commit();
                }
                return UnitOfWork.Repository<DataRepo>().Queryable().Where(x => x.TIME_YEAR == year).ToList();
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                this.State = false;
                this.Exception = ex;
                return new List<T_MD_DATA>();
            }
        }

        public void UpdateData(List<T_MD_DATA> data)
        {
            try
            {
                UnitOfWork.BeginTransaction();
                foreach (var item in data)
                {
                    var pdata = UnitOfWork.Repository<DataRepo>().Queryable().FirstOrDefault(x => x.TIME_YEAR == item.TIME_YEAR && x.ROUTE_CODE == item.ROUTE_CODE);
                    if (pdata != null)
                    {
                        pdata.S0011 = item.S0011;
                        pdata.S0012 = item.S0012;
                        pdata.S0013 = item.S0013;
                        pdata.S0014 = item.S0014;
                        pdata.S0015 = item.S0015;
                        pdata.S0016 = item.S0016;
                        pdata.S0017 = item.S0017;
                        pdata.S0018 = item.S0018;
                        pdata.S0019 = item.S0019;
                        pdata.S0020 = item.S0020;
                        pdata.S0021 = item.S0021;
                        pdata.S0022 = item.S0022;
                        pdata.S0023 = item.S0023;
                        pdata.S0024 = item.S0024;
                        UnitOfWork.Repository<DataRepo>().Update(pdata);
                    }
                    else
                    {
                        continue;
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
