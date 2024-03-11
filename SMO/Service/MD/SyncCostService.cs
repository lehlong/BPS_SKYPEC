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
    public class SyncCostService : GenericService<T_MD_SYNC_COST, SyncCostRepo>
    {
        public SyncCostService() : base()
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
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM CHIPHITHUCHIEN_OMEGA WHERE Nam='{year}'", con);
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

                var data = ConvertDataTableToSyncCost(tableData);

                UnitOfWork.BeginTransaction();
                UnitOfWork.Repository<SyncCostRepo>().Queryable().Where(x => x.YEAR == year).Delete();
                foreach (var item in data)
                {
                    item.YEAR = year;
                    item.ACTIVE = true;
                    UnitOfWork.Repository<SyncCostRepo>().Create(item);
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

        public List<T_MD_SYNC_COST> ConvertDataTableToSyncCost(DataTable dt)
        {
            try
            {
                var convertedList = from rw in dt.AsEnumerable()
                                    select new T_MD_SYNC_COST()
                                    {
                                        ID = Guid.NewGuid(),
                                        CHI_NHANH = Convert.ToString(rw["Chinhanh"]),
                                        GROUP_3_ID = Convert.ToString(rw["Group3ID"]),
                                        GROUP_NAME_3 = Convert.ToString(rw["GroupName3"]),
                                        GROUP_2_ID = Convert.ToString(rw["Group2ID"]),
                                        GROUP_NAME_2 = Convert.ToString(rw["GroupName2"]),
                                        GROUP_1_ID = Convert.ToString(rw["Group1ID"]),
                                        GROUP_NAME_1 = Convert.ToString(rw["GroupName1"]),
                                        GROUP_NAME_E2= Convert.ToString(rw["GroupNameE2"]),
                                        MONTH = Int32.Parse(rw["Thang"].ToString()),
                                        VALUE = rw["GiatriThuchien"].ToString() == null || rw["GiatriThuchien"].ToString() == "" ? 0 : Convert.ToDecimal(rw["GiatriThuchien"]),
                                        ACCUMULATION = rw["GiatriLuyKe"].ToString() == null || rw["GiatriLuyKe"].ToString() == "" ? 0 : Convert.ToDecimal(rw["GiatriLuyKe"]),

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
