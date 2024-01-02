using NHibernate.Linq;
using NPOI.HSSF.Record.Chart;
using SMO.Core.Entities;
using SMO.Repository.Implement.MD;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using static iTextSharp.text.pdf.AcroFields;

namespace SMO.Service.MD
{
    public class UnitPriceService : GenericService<T_MD_UNIT_PRICE, UnitPriceRepo>
    {
        public UnitPriceService() : base()
        {

        }

        public void SynchornizeData(int year)
        {
            try
            {
                string connection = ConfigurationManager.ConnectionStrings["SKYPEC"].ConnectionString;
                DataTable tableData = new DataTable();
                using (SqlConnection con = new SqlConnection(connection))
                {
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM QLHH_BANGGIA", con);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    try
                    {
                        adapter.Fill(tableData);
                    }
                    catch (Exception ex)
                    {
                        this.State = false;
                        this.Exception = ex;
                    }
                }

                var data = ConvertDataTableToUnitPrice(tableData);

                UnitOfWork.BeginTransaction();
                UnitOfWork.Repository<UnitPriceRepo>().Queryable().Where(x => x.YEAR == year).Delete();
                foreach (var item in data)
                {
                    item.YEAR = year;
                    item.ACTIVE = true;
                    UnitOfWork.Repository<UnitPriceRepo>().Create(item);
                }
                UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                State = false;
                Exception = ex;
            }
        }

        public List<T_MD_UNIT_PRICE> ConvertDataTableToUnitPrice(DataTable dt)
        {
            try
            {
                var convertedList = from rw in dt.AsEnumerable()
                                    select new T_MD_UNIT_PRICE()
                                    {
                                        ID = Guid.NewGuid(),
                                        SHORT_OBJECT_ID = Convert.ToString(rw["ShortObjectID"]),
                                        OBJECT_ID = Convert.ToString(rw["ShortObjectID"]),
                                        OBJECT_TYPE_ID = Convert.ToString(rw["ObjectTypeID"]),
                                        CURRENCY_ID = Convert.ToString(rw["CurrencyID"]),
                                        MOPS_PRICE = rw["MopsPrice"].ToString() == null || rw["MopsPrice"].ToString() == "" ? 0 : Convert.ToDecimal(rw["MopsPrice"]),
                                        UNIT_ID = Convert.ToString(rw["UnitID"]),
                                        WAREHOUSE_ID = Convert.ToString(rw["WareHouseID"]),
                                        PRICE_01 = rw["Price01"].ToString() == null || rw["Price01"].ToString() == "" ? 0 : Convert.ToDecimal(rw["Price01"]),
                                        PRICE_02 = rw["Price02"].ToString() == null || rw["Price02"].ToString() == "" ? 0 : Convert.ToDecimal(rw["Price02"]),
                                        ENVIRONMENT_AMOUNT = rw["EnvironmentAmount"].ToString() == null || rw["EnvironmentAmount"].ToString() == "" ? 0 : Convert.ToDecimal(rw["EnvironmentAmount"]),
                                        SERVICE_PRICE = rw["ServicePrice"].ToString() == null || rw["ServicePrice"].ToString() == "" ? 0 : Convert.ToDecimal(rw["ServicePrice"]),
                                        PUMP_FEE = rw["PumpFee"].ToString() == null || rw["PumpFee"].ToString() == "" ? 0 : Convert.ToDecimal(rw["PumpFee"]),
                                    };
                return convertedList.ToList();
            }
            catch (Exception ex)
            {
                State = false;
                Exception = ex;
                return null;
            }

        }
    }
}
