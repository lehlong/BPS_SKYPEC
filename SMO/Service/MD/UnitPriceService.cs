using SMO.Core.Entities;
using SMO.Repository.Implement.MD;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SMO.Service.MD
{
    public class UnitPriceService : GenericService<T_MD_UNIT_PRICE, UnitPriceRepo>
    {
        public UnitPriceService() : base()
        {

        }

        public override void Create()
        {
            try
            {
                if (!CheckExist(x => x.SAN_BAY_CODE == ObjDetail.SAN_BAY_CODE && x.YEAR == ObjDetail.YEAR))
                {
                    base.Create();
                }
                else
                {
                    State = false;
                    MesseageCode = "1101";
                }
            }
            catch (Exception ex)
            {
                State = false;
                Exception = ex;
            }
        }

        public void SynchornizeData(int year)
        {
            try
            {
                string connection = ConfigurationManager.ConnectionStrings["SKYPEC"].ConnectionString;
                DataTable tableData = new DataTable();
                using (SqlConnection con = new SqlConnection(connection))
                {
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM SKYPECLGS_KHBAY WHERE TranYear = '{year}'", con);
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
