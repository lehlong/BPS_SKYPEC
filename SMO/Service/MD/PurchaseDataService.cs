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
    public class PurchaseDataService : GenericService<T_MD_PURCHASE_DATA, PurchaseDataRepo>
    {
        public PurchaseDataService() : base()
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
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM LGS_KH_NHAPHANG", con);
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

                var data = ConvertDataTableToPurchaseData(tableData);

                UnitOfWork.BeginTransaction();
                UnitOfWork.Repository<PurchaseDataRepo>().Queryable().Where(x => x.TIME_YEAR == year).Delete();
                foreach (var item in data)
                {
                    var sumWarehouse = data.Where(x => x.WAREHOUSE_CODE == item.WAREHOUSE_CODE).Sum(x => x.S0002);
                    var sumArea = data.Where(x => x.AREA_ID == item.AREA_ID).Sum(x => x.S0002);
                    item.S0001 = sumWarehouse == 0 || sumArea == 0 ? 0 : sumWarehouse / sumArea;
                    item.TIME_YEAR = year;
                    item.ACTIVE = true;
                    UnitOfWork.Repository<PurchaseDataRepo>().Create(item);
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

        public List<T_MD_PURCHASE_DATA> ConvertDataTableToPurchaseData(DataTable dt)
        {
            try
            {
                var convertedList = from rw in dt.AsEnumerable()
                                    select new T_MD_PURCHASE_DATA()
                                    {
                                        ID = Guid.NewGuid(),
                                        WAREHOUSE_CODE = Convert.ToString(rw["MaKho"]).Replace(" ",""),
                                        DELIVERY_CONDITIONS_CODE = Convert.ToString(rw["DKGH"]).Replace(" ", ""),
                                        ID_KHNH = rw["ID_KHNH"].ToString() == null || rw["ID_KHNH"].ToString() == "" ? 0 : Convert.ToInt32(rw["ID_KHNH"]),
                                        NAME = Convert.ToString(rw["ObjectName"]),
                                        S0002 = rw["Quantity"].ToString() == null || rw["Quantity"].ToString() == "" ? 0 : Convert.ToDecimal(rw["Quantity"]),
                                        S0003 = rw["Premium"].ToString() == null || rw["Premium"].ToString() == "" ? 0 : Convert.ToDecimal(rw["Premium"]),
                                        S0004 = rw["Pre_tau"].ToString() == null || rw["Pre_tau"].ToString() == "" ? 0 : Convert.ToDecimal(rw["Pre_tau"]),
                                        S0005 = rw["thung_Tan"].ToString() == null || rw["thung_Tan"].ToString() == "" ? 0 : Convert.ToDecimal(rw["thung_Tan"]),
                                        S0006 = rw["Bao_hiem"].ToString() == null || rw["Bao_hiem"].ToString() == "" ? 0 : Convert.ToDecimal(rw["Bao_hiem"]),
                                        S0007 = rw["ImportVATRate"].ToString() == null || rw["ImportVATRate"].ToString() == "" ? 0 : Convert.ToDecimal(rw["ImportVATRate"]),
                                        SIZE_ID = Convert.ToString(rw["SizeID"]),
                                        AREA_ID = Convert.ToString(rw["AreaID"]),
                                        CREATE_BY = ProfileUtilities.User.USER_NAME,
                                        CREATE_DATE = DateTime.Now,
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
