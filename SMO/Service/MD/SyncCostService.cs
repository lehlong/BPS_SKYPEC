using NHibernate.Linq;
using NPOI.HSSF.Record.Chart;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SMO.Core.Entities;
using SMO.Repository.Implement.MD;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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

        public void ExportExcelGridData(ref MemoryStream outFileStream, IList<T_MD_SYNC_COST> lstData, string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            IWorkbook templateWorkbook;
            templateWorkbook = new XSSFWorkbook(fs);
            templateWorkbook.SetSheetName(0, ModulType.GetTextSheetName(ModulType.KeHoachSanLuong));
            fs.Close();
            ISheet sheet = templateWorkbook.GetSheetAt(0);
            var startRow = 8;
            var NUM_CELL = 12;
            InsertData(templateWorkbook, sheet, lstData, startRow, NUM_CELL);
            templateWorkbook.Write(outFileStream);
        }

        public void InsertData(IWorkbook templateWorkbook, ISheet sheet, IList<T_MD_SYNC_COST> dataDetails, int startRow, int NUM_CELL)
        {
            ICellStyle styleCellBold = templateWorkbook.CreateCellStyle();
            styleCellBold.CloneStyleFrom(sheet.GetRow(8).Cells[0].CellStyle);
            var fontBold = templateWorkbook.CreateFont();
            fontBold.Boldweight = (short)FontBoldWeight.Bold;
            fontBold.FontHeightInPoints = 11;
            fontBold.FontName = "Times New Roman";

            ICellStyle styleName = templateWorkbook.CreateCellStyle();
            styleName.CloneStyleFrom(sheet.GetRow(8).Cells[1].CellStyle);

            ICellStyle styleBody = templateWorkbook.CreateCellStyle();
            styleBody.CloneStyleFrom(sheet.GetRow(9).Cells[0].CellStyle);
            styleBody.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");
            ICellStyle styleBodySum = templateWorkbook.CreateCellStyle();
            styleBodySum.CloneStyleFrom(sheet.GetRow(9).Cells[1].CellStyle);
            styleBodySum.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");

            for (int i = 0; i < dataDetails.Count(); i++)
            {
                var dataRow = dataDetails[i];
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, NUM_CELL);
                rowCur.Cells[0].SetCellValue(dataRow.CHI_NHANH);
                rowCur.Cells[1].SetCellValue(dataRow.GROUP_3_ID);
                rowCur.Cells[2].SetCellValue(dataRow.GROUP_NAME_3);
                rowCur.Cells[3].SetCellValue(dataRow.GROUP_2_ID);
                rowCur.Cells[4].SetCellValue(dataRow.GROUP_NAME_2);
                rowCur.Cells[5].SetCellValue(dataRow.GROUP_NAME_E2);
                rowCur.Cells[6].SetCellValue(dataRow.GROUP_1_ID);
                rowCur.Cells[7].SetCellValue(dataRow.GROUP_NAME_1);
                rowCur.Cells[8].SetCellValue((double)dataRow.VALUE);
                rowCur.Cells[9].SetCellValue((double)dataRow.ACCUMULATION);
                rowCur.Cells[10].SetCellValue(dataRow.MONTH);
                rowCur.Cells[11].SetCellValue(dataRow.YEAR);

                for (int j = 0; j < NUM_CELL; j++)
                {
                    if (j == 0 || j == 1 || j == 2)
                    {
                        rowCur.Cells[j].CellStyle = styleName;
                    }
                    else
                    {
                        rowCur.Cells[j].CellStyle = styleBody;
                    }
                }
            }
        }
    }
}
