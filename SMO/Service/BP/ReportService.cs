using NHibernate.Criterion;
using NPOI.HSSF.Record.Chart;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SMO.Repository.Common;
using SMO.Repository.Implement.BP.DAU_TU_NGOAI_DOANH_NGHIEP;
using SMO.Repository.Implement.BP.DAU_TU_TRANG_THIET_BI;
using SMO.Repository.Implement.BP.DAU_TU_XAY_DUNG;
using SMO.Repository.Implement.BP.KE_HOACH_CHI_PHI;
using SMO.Repository.Implement.BP.KE_HOACH_DOANH_THU;
using SMO.Repository.Implement.BP.KE_HOACH_SAN_LUONG;
using SMO.Repository.Implement.MD;
using SMO.Service.BP.KE_HOACH_SAN_LUONG;
using SMO.Service.MD;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SMO.Service.BP
{
    public class ReportService
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public ReportService()
        {
            UnitOfWork = new NHUnitOfWork();
        }

        public void ExportExcelGridData(ref MemoryStream outFileStream, List<ReportModel> lstData, string path, int NUMCELL, string Template)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            IWorkbook templateWorkbook;
            templateWorkbook = new XSSFWorkbook(fs);
            templateWorkbook.SetSheetName(0, ModulType.GetTextSheetName(ModulType.KeHoachSanLuong));
            fs.Close();
            ISheet sheet = templateWorkbook.GetSheetAt(0);
            var startRow = 10;
            switch (Template)
            {
                case "BM_02A":
                    InsertDataToTableBM_02A(templateWorkbook, sheet, lstData, startRow, NUMCELL);
                    break;
                case "BM_02B":
                    startRow = 8;
                    InsertDataToTableBM_02B(templateWorkbook, sheet, lstData, startRow, NUMCELL);
                    break;
                case "BM_01D":
                    InsertDataToTableBM_01D(templateWorkbook, sheet, lstData, startRow, NUMCELL);
                    break;
                case "BM_02C":
                    startRow = 7;
                    InsertDataToTableBM_02C(templateWorkbook, sheet, lstData, startRow, NUMCELL);
                    break;
                case "BM_02C1":
                    startRow = 7;
                    InsertDataToTableBM_02C(templateWorkbook, sheet, lstData, startRow, NUMCELL);
                    break;
                case "BM_02D":
                    startRow = 8;
                    InsertDataToTableBM_02D(templateWorkbook, sheet, lstData, startRow, NUMCELL);
                    break;
                case "BM_02D1":
                    startRow = 8;
                    InsertDataToTableBM_02D1(templateWorkbook, sheet, lstData, startRow, NUMCELL);
                    break;
                default:
                    InsertDataToTableBM_02B(templateWorkbook, sheet, lstData, startRow, NUMCELL);
                    break;
            }
            templateWorkbook.Write(outFileStream);
        }

        public void InsertDataToTableBM_02A(IWorkbook templateWorkbook, ISheet sheet, IList<ReportModel> dataDetails, int startRow, int NUM_CELL)
        {
            ICellStyle styleCellBold = templateWorkbook.CreateCellStyle(); // chữ in đậm
            styleCellBold.CloneStyleFrom(sheet.GetRow(10).Cells[0].CellStyle);
            var fontBold = templateWorkbook.CreateFont();
            fontBold.Boldweight = (short)FontBoldWeight.Bold;
            fontBold.FontHeightInPoints = 13;
            fontBold.FontName = "Times New Roman";

            ICellStyle styleCellName = templateWorkbook.CreateCellStyle();
            styleCellName.CloneStyleFrom(sheet.GetRow(10).Cells[1].CellStyle);

            ICellStyle styleBody = templateWorkbook.CreateCellStyle();
            styleBody.CloneStyleFrom(sheet.GetRow(11).Cells[0].CellStyle);
            styleBody.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");
            ICellStyle styleBodyBold = templateWorkbook.CreateCellStyle();
            styleBodyBold.CloneStyleFrom(sheet.GetRow(11).Cells[1].CellStyle);
            styleBodyBold.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");

            foreach(var item in dataDetails)
            {
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, NUM_CELL);
                rowCur.Cells[0].SetCellValue(item.Name);
                rowCur.Cells[1].SetCellValue(item.Col1 == null ? 0 : Convert.ToDouble(item.Col1));
                rowCur.Cells[2].SetCellValue(item.Col2 == null ? 0 : Convert.ToDouble(item.Col2));
                rowCur.Cells[3].SetCellValue(item.Tdth);
                rowCur.Cells[4].SetCellValue(item.Col4 == null ? 0 : Convert.ToDouble(item.Col4));
                rowCur.Cells[5].SetCellValue(item.Col5 == null ? 0 : Convert.ToDouble(item.Col5));
                rowCur.Cells[6].SetCellValue(item.Tdtk);
                rowCur.Cells[7].SetCellValue(item.Col7 == null ? 0 : Convert.ToDouble(item.Col7));
                rowCur.Cells[8].SetCellValue(item.Col8 == null ? 0 : Convert.ToDouble(item.Col8));
                rowCur.Cells[9].SetCellValue(item.Des);
                for(int i = 0; i < NUM_CELL; i++)
                {
                    if (item.IsBold)
                    {
                        if(i == 0 || i == NUM_CELL - 1)
                        {
                            rowCur.Cells[i].CellStyle = styleCellBold;
                            rowCur.Cells[i].CellStyle.SetFont(fontBold);
                        }
                        else
                        {
                            rowCur.Cells[i].CellStyle = styleBodyBold;
                            rowCur.Cells[i].CellStyle.SetFont(fontBold);
                        }
                    }
                    else
                    {
                        if(i == 0 ||  i == NUM_CELL - 1)
                        {
                            rowCur.Cells[i].CellStyle = styleCellName;
                        }
                        else
                        {
                            rowCur.Cells[i].CellStyle = styleBody;

                        }
                    }
                }
            }
            IRow rowEnd = ReportUtilities.CreateRow(ref sheet, startRow + 2, NUM_CELL);
            rowEnd.Cells[7].SetCellValue("Ngày  .......... tháng ........... năm..............");
            IRow rowEnd2 = ReportUtilities.CreateRow(ref sheet, startRow + 3, NUM_CELL);
            rowEnd2.Cells[7].SetCellValue("Người đại diện ký, ghi rõ họ tên");
            rowEnd2.Cells[7].CellStyle.SetFont(fontBold);
        }

        public void InsertDataToTableBM_02B(IWorkbook templateWorkbook, ISheet sheet, IList<ReportModel> dataDetails, int startRow, int NUM_CELL)
        {
            ICellStyle styleCellBold = templateWorkbook.CreateCellStyle(); // chữ in đậm
            styleCellBold.CloneStyleFrom(sheet.GetRow(8).Cells[0].CellStyle);
            var fontBold = templateWorkbook.CreateFont();
            fontBold.Boldweight = (short)FontBoldWeight.Bold;
            fontBold.FontHeightInPoints = 13;
            fontBold.FontName = "Times New Roman";

            ICellStyle styleCellName = templateWorkbook.CreateCellStyle();
            styleCellName.CloneStyleFrom(sheet.GetRow(8).Cells[1].CellStyle);

            ICellStyle styleBody = templateWorkbook.CreateCellStyle();
            styleBody.CloneStyleFrom(sheet.GetRow(9).Cells[0].CellStyle);
            styleBody.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");
            ICellStyle styleBodyBold = templateWorkbook.CreateCellStyle();
            styleBodyBold.CloneStyleFrom(sheet.GetRow(9).Cells[1].CellStyle);
            styleBodyBold.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");

            foreach (var item in dataDetails)
            {
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, NUM_CELL);
                rowCur.Cells[0].SetCellValue(item.Name);
                rowCur.Cells[1].SetCellValue(item.Col1 == null ? 0 : Convert.ToDouble(item.Col1));
                rowCur.Cells[2].SetCellValue(item.Col2 == null ? 0 : Convert.ToDouble(item.Col2));
                rowCur.Cells[3].SetCellValue(item.Col3 == null ? 0 : Convert.ToDouble(item.Col3));
                rowCur.Cells[4].SetCellValue(item.Col4 == null ? 0 : Convert.ToDouble(item.Col4));
                rowCur.Cells[5].SetCellValue(item.Col5 == null ? 0 : Convert.ToDouble(item.Col5));
                rowCur.Cells[6].SetCellValue(item.Col6 == null ? 0 : Convert.ToDouble(item.Col6));
                rowCur.Cells[7].SetCellValue(item.Des);
                for (int i = 0; i < NUM_CELL; i++)
                {
                    if (item.IsBold)
                    {
                        if ( i == 0 || i == NUM_CELL - 1)
                        {
                            rowCur.Cells[i].CellStyle = styleCellBold;
                            rowCur.Cells[i].CellStyle.SetFont(fontBold);
                        }
                        else
                        {
                            rowCur.Cells[i].CellStyle = styleBodyBold;
                            rowCur.Cells[i].CellStyle.SetFont(fontBold);
                        }
                    }
                    else
                    {
                        if ( i == 0 || i == NUM_CELL - 1)
                        {
                            rowCur.Cells[i].CellStyle = styleCellName;
                        }
                        else
                        {
                            rowCur.Cells[i].CellStyle = styleBody;

                        }
                    }
                }
            }
            IRow rowEnd = ReportUtilities.CreateRow(ref sheet, startRow + 2, NUM_CELL);
            rowEnd.Cells[7].SetCellValue("Ngày  .......... tháng ........... năm..............");
            IRow rowEnd2 = ReportUtilities.CreateRow(ref sheet, startRow + 3, NUM_CELL);
            rowEnd2.Cells[7].SetCellValue("Người đại diện ký, ghi rõ họ tên");
            rowEnd2.Cells[7].CellStyle.SetFont(fontBold);

        }

        public void InsertDataToTableBM_02C(IWorkbook templateWorkbook, ISheet sheet, IList<ReportModel> dataDetails, int startRow, int NUM_CELL)
        {
            ICellStyle styleCellBold = templateWorkbook.CreateCellStyle(); // chữ in đậm
            styleCellBold.CloneStyleFrom(sheet.GetRow(7).Cells[0].CellStyle);
            var fontBold = templateWorkbook.CreateFont();
            fontBold.Boldweight = (short)FontBoldWeight.Bold;
            fontBold.FontHeightInPoints = 13;
            fontBold.FontName = "Times New Roman";

            ICellStyle styleCellName = templateWorkbook.CreateCellStyle();
            styleCellName.CloneStyleFrom(sheet.GetRow(7).Cells[1].CellStyle);

            ICellStyle styleBody = templateWorkbook.CreateCellStyle();
            styleBody.CloneStyleFrom(sheet.GetRow(8).Cells[0].CellStyle);
            styleBody.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");
            ICellStyle styleBodyBold = templateWorkbook.CreateCellStyle();
            styleBodyBold.CloneStyleFrom(sheet.GetRow(8).Cells[1].CellStyle);
            styleBodyBold.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");

            ICellStyle styleEnd = templateWorkbook.CreateCellStyle();
            styleEnd.CloneStyleFrom(sheet.GetRow(8).Cells[2].CellStyle);

            foreach (var item in dataDetails)
            {
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, NUM_CELL);
                if(item.Stt == "SUM")
                {
                    rowCur.Cells[0].SetCellValue("");
                }
                else
                {
                    rowCur.Cells[0].SetCellValue(item.Stt);
                }
                rowCur.Cells[1].SetCellValue(item.Name);
                rowCur.Cells[2].SetCellValue(item.Col1 == null ? 0 : Convert.ToDouble(item.Col1));
                rowCur.Cells[3].SetCellValue(item.Col2 == null ? 0 : Convert.ToDouble(item.Col2));
                rowCur.Cells[4].SetCellValue(item.Col3 == null ? 0 : Convert.ToDouble(item.Col3));
                rowCur.Cells[5].SetCellValue(item.Col4 == null ? 0 : Convert.ToDouble(item.Col4));
                rowCur.Cells[6].SetCellValue(item.Col5 == null ? 0 : Convert.ToDouble(item.Col5));
                rowCur.Cells[7].SetCellValue(item.Col6 == null ? 0 : Convert.ToDouble(item.Col6));
                rowCur.Cells[8].SetCellValue(item.Col7 == null ? 0 : Convert.ToDouble(item.Col7));
                rowCur.Cells[9].SetCellValue(item.Col8 == null ? 0 : Convert.ToDouble(item.Col8));
                rowCur.Cells[10].SetCellValue(item.Col9 == null ? 0 : Convert.ToDouble(item.Col9));

                for (int i = 0; i < NUM_CELL; i++)
                {
                    if (item.IsBold)
                    {
                        if (i == 1)
                        {
                            rowCur.Cells[i].CellStyle = styleCellBold;
                            rowCur.Cells[i].CellStyle.SetFont(fontBold);
                        }
                        else
                        {
                            rowCur.Cells[i].CellStyle = styleBodyBold;
                            rowCur.Cells[i].CellStyle.SetFont(fontBold);
                        }
                    }
                    else
                    {
                        if (i == 1)
                        {
                            rowCur.Cells[i].CellStyle = styleCellName;
                        }
                        else
                        {
                            rowCur.Cells[i].CellStyle = styleBody;

                        }
                    }
                }
            }
            IRow rowEnd = ReportUtilities.CreateRow(ref sheet, startRow + 2, NUM_CELL);
            rowEnd.Cells[7].SetCellValue("Ngày  .......... tháng ........... năm..............");
            IRow rowEnd2 = ReportUtilities.CreateRow(ref sheet, startRow + 3, NUM_CELL);
            rowEnd2.Cells[7].SetCellValue("Người đại diện ký, ghi rõ họ tên");
            rowEnd2.Cells[7].CellStyle.SetFont(fontBold);

        }

        public void InsertDataToTableBM_02D(IWorkbook templateWorkbook, ISheet sheet, IList<ReportModel> dataDetails, int startRow, int NUM_CELL)
        {
            ICellStyle styleCellBold = templateWorkbook.CreateCellStyle(); // chữ in đậm
            styleCellBold.CloneStyleFrom(sheet.GetRow(8).Cells[0].CellStyle);
            var fontBold = templateWorkbook.CreateFont();
            fontBold.Boldweight = (short)FontBoldWeight.Bold;
            fontBold.FontHeightInPoints = 13;
            fontBold.FontName = "Times New Roman";

            ICellStyle styleCellName = templateWorkbook.CreateCellStyle();
            styleCellName.CloneStyleFrom(sheet.GetRow(8).Cells[1].CellStyle);

            ICellStyle styleBody = templateWorkbook.CreateCellStyle();
            styleBody.CloneStyleFrom(sheet.GetRow(9).Cells[0].CellStyle);
            styleBody.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");
            ICellStyle styleBodyBold = templateWorkbook.CreateCellStyle();
            styleBodyBold.CloneStyleFrom(sheet.GetRow(9).Cells[1].CellStyle);
            styleBodyBold.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");

            ICellStyle styleEnd = templateWorkbook.CreateCellStyle();
            styleEnd.CloneStyleFrom(sheet.GetRow(8).Cells[2].CellStyle);

            foreach (var item in dataDetails)
            {
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, NUM_CELL);
                if (item.Id == "SUM")
                {
                    rowCur.Cells[0].SetCellValue("");
                }
                else
                {
                    rowCur.Cells[0].SetCellValue(item.Id);
                }
                rowCur.Cells[1].SetCellValue(item.NameExcel);
                rowCur.Cells[2].SetCellValue(item.Unit);
                rowCur.Cells[3].SetCellValue(item.Col1 == null ? 0 : Convert.ToDouble(item.Col1));
                rowCur.Cells[4].SetCellValue(item.Col2 == null ? 0 : Convert.ToDouble(item.Col2));
                rowCur.Cells[5].SetCellValue(item.Col3 == null ? 0 : Convert.ToDouble(item.Col3));
                rowCur.Cells[6].SetCellValue(item.Col4 == null ? 0 : Convert.ToDouble(item.Col4));
                rowCur.Cells[7].SetCellValue(item.Col5 == null ? 0 : Convert.ToDouble(item.Col5));
                for (int i = 0; i < NUM_CELL; i++)
                {
                    if (item.IsBold)
                    {
                        if (i == 1 || i == 0)
                        {
                            rowCur.Cells[i].CellStyle = styleCellBold;
                            rowCur.Cells[i].CellStyle.SetFont(fontBold);
                        }
                        else
                        {
                            rowCur.Cells[i].CellStyle = styleBodyBold;
                            rowCur.Cells[i].CellStyle.SetFont(fontBold);
                        }
                    }
                    else
                    {
                        if (i == 1 || i == 0)
                        {
                            rowCur.Cells[i].CellStyle = styleCellName;
                        }
                        else
                        {
                            rowCur.Cells[i].CellStyle = styleBody;

                        }
                    }
                }
            }
            IRow rowEnd = ReportUtilities.CreateRow(ref sheet, startRow + 2, NUM_CELL);
            rowEnd.Cells[5].SetCellValue("Ngày  .......... tháng ........... năm..............");
            IRow rowEnd2 = ReportUtilities.CreateRow(ref sheet, startRow + 3, NUM_CELL);
            rowEnd2.Cells[5].SetCellValue("Người đại diện ký, ghi rõ họ tên");
            rowEnd2.Cells[5].CellStyle.SetFont(fontBold);

        }
        public void InsertDataToTableBM_02D1(IWorkbook templateWorkbook, ISheet sheet, IList<ReportModel> dataDetails, int startRow, int NUM_CELL)
        {
            ICellStyle styleCellBold = templateWorkbook.CreateCellStyle(); // chữ in đậm
            styleCellBold.CloneStyleFrom(sheet.GetRow(8).Cells[0].CellStyle);
            var fontBold = templateWorkbook.CreateFont();
            fontBold.Boldweight = (short)FontBoldWeight.Bold;
            fontBold.FontHeightInPoints = 13;
            fontBold.FontName = "Times New Roman";

            ICellStyle styleCellName = templateWorkbook.CreateCellStyle();
            styleCellName.CloneStyleFrom(sheet.GetRow(8).Cells[1].CellStyle);

            ICellStyle styleBody = templateWorkbook.CreateCellStyle();
            styleBody.CloneStyleFrom(sheet.GetRow(9).Cells[0].CellStyle);
            styleBody.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");
            ICellStyle styleBodyBold = templateWorkbook.CreateCellStyle();
            styleBodyBold.CloneStyleFrom(sheet.GetRow(9).Cells[1].CellStyle);
            styleBodyBold.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");

            ICellStyle styleEnd = templateWorkbook.CreateCellStyle();
            styleEnd.CloneStyleFrom(sheet.GetRow(8).Cells[2].CellStyle);

            foreach (var item in dataDetails)
            {
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, NUM_CELL);
                if (item.Id == "SUM")
                {
                    rowCur.Cells[0].SetCellValue("");
                }
                else
                {
                    rowCur.Cells[0].SetCellValue(item.Id);
                }
                rowCur.Cells[1].SetCellValue(item.NameExcel);
                rowCur.Cells[2].SetCellValue(item.Unit);
                rowCur.Cells[3].SetCellValue(item.Col1 == null ? 0 : Convert.ToDouble(item.Col1));
                rowCur.Cells[4].SetCellValue(item.Col2 == null ? 0 : Convert.ToDouble(item.Col2));
                rowCur.Cells[5].SetCellValue(item.Col4 == null ? 0 : Convert.ToDouble(item.Col4));
                for (int i = 0; i < NUM_CELL; i++)
                {
                    if (item.IsBold)
                    {
                        if (i == 1 || i == 0)
                        {
                            rowCur.Cells[i].CellStyle = styleCellBold;
                            rowCur.Cells[i].CellStyle.SetFont(fontBold);
                        }
                        else
                        {
                            rowCur.Cells[i].CellStyle = styleBodyBold;
                            rowCur.Cells[i].CellStyle.SetFont(fontBold);
                        }
                    }
                    else
                    {
                        if (i == 1 || i == 0  )
                        {
                            rowCur.Cells[i].CellStyle = styleCellName;
                        }
                        else
                        {
                            rowCur.Cells[i].CellStyle = styleBody;

                        }
                    }
                }
            }
            IRow rowEnd = ReportUtilities.CreateRow(ref sheet, startRow + 2, NUM_CELL);
            rowEnd.Cells[3].SetCellValue("Ngày  .......... tháng ........... năm..............");
            IRow rowEnd2 = ReportUtilities.CreateRow(ref sheet, startRow + 3, NUM_CELL);
            rowEnd2.Cells[3].SetCellValue("Người đại diện ký, ghi rõ họ tên");
            rowEnd2.Cells[3].CellStyle.SetFont(fontBold);

        }
        public void InsertDataToTableBM_01D(IWorkbook templateWorkbook, ISheet sheet, IList<ReportModel> dataDetails, int startRow, int NUM_CELL)
        {
            ICellStyle styleCellBold = templateWorkbook.CreateCellStyle(); // chữ in đậm
            styleCellBold.CloneStyleFrom(sheet.GetRow(10).Cells[0].CellStyle);
            var fontBold = templateWorkbook.CreateFont();
            fontBold.Boldweight = (short)FontBoldWeight.Bold;
            fontBold.FontHeightInPoints = 13;
            fontBold.FontName = "Times New Roman";

            ICellStyle styleCellName = templateWorkbook.CreateCellStyle();
            styleCellName.CloneStyleFrom(sheet.GetRow(10).Cells[1].CellStyle);

            ICellStyle styleBody = templateWorkbook.CreateCellStyle();
            styleBody.CloneStyleFrom(sheet.GetRow(11).Cells[0].CellStyle);
            styleBody.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");
            ICellStyle styleBodyBold = templateWorkbook.CreateCellStyle();
            styleBodyBold.CloneStyleFrom(sheet.GetRow(11).Cells[1].CellStyle);
            styleBodyBold.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");

            foreach (var item in dataDetails)
            {
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, NUM_CELL);
                rowCur.Cells[0].SetCellValue(item.Stt);
                rowCur.Cells[1].SetCellValue(item.NameExcel);
                rowCur.Cells[2].SetCellValue(item.Col1 == null ? 0 : Convert.ToDouble(item.Col1));
                rowCur.Cells[3].SetCellValue(item.Col2 == null ? 0 : Convert.ToDouble(item.Col2));
                rowCur.Cells[4].SetCellValue(item.Col3 == null ? 0 : Convert.ToDouble(item.Col3));
                rowCur.Cells[5].SetCellValue(item.Col4 == null ? 0 : Convert.ToDouble(item.Col4));
                rowCur.Cells[6].SetCellValue(item.Col5 == null ? 0 : Convert.ToDouble(item.Col5));
                rowCur.Cells[7].SetCellValue(item.Col6 == null ? 0 : Convert.ToDouble(item.Col6));
                rowCur.Cells[8].SetCellValue(item.Col7 == null ? 0 : Convert.ToDouble(item.Col7));
                rowCur.Cells[9].SetCellValue(item.Col8 == null ? 0 : Convert.ToDouble(item.Col8));
                rowCur.Cells[10].SetCellValue(item.Col9 == null ? 0 : Convert.ToDouble(item.Col9));
                rowCur.Cells[11].SetCellValue(item.Col10 == null ? 0 : Convert.ToDouble(item.Col10));
                rowCur.Cells[12].SetCellValue(item.Col11 == null ? 0 : Convert.ToDouble(item.Col11));
                rowCur.Cells[13].SetCellValue(item.Col12 == null ? 0 : Convert.ToDouble(item.Col12));
                rowCur.Cells[14].SetCellValue(item.Col13 == null ? 0 : Convert.ToDouble(item.Col13));
                for (int i = 0; i < NUM_CELL; i++)
                {
                    if (item.IsBold)
                    {
                        if (i == 1)
                        {
                            rowCur.Cells[i].CellStyle = styleCellBold;
                            rowCur.Cells[i].CellStyle.SetFont(fontBold);
                        }
                        else
                        {
                            rowCur.Cells[i].CellStyle = styleBodyBold;
                            rowCur.Cells[i].CellStyle.SetFont(fontBold);
                        }
                    }
                    else
                    {
                        if (i == 1)
                        {
                            rowCur.Cells[i].CellStyle = styleCellName;
                        }
                        else
                        {
                            rowCur.Cells[i].CellStyle = styleBody;

                        }
                    }
                }
            }

            IRow rowEnd = ReportUtilities.CreateRow(ref sheet, startRow + 2, NUM_CELL);
            rowEnd.Cells[11].SetCellValue("Ngày  .......... tháng ........... năm..............");
            IRow rowEnd2 = ReportUtilities.CreateRow(ref sheet, startRow + 3, NUM_CELL);
            rowEnd2.Cells[11].SetCellValue("Người đại diện ký, ghi rõ họ tên");
            rowEnd2.Cells[11].CellStyle.SetFont(fontBold);

        }
        public ReportDataCenter GenDataBM01D(int year, string orgCode)
        {
            try
            {
                var data = new ReportDataCenter();
                var lstProject = UnitOfWork.Repository<ProjectRepo>().Queryable().Where(x => x.YEAR > 0).ToList();
                var lstProjectTtb = lstProject.Where(x => x.LOAI_HINH == "TTB").ToList();
                var lstProjectXdcb = lstProject.Where(x => x.LOAI_HINH == "XDCB").ToList();


                var dataDTTTB = UnitOfWork.Repository<DauTuTrangThietBiDataRepo>().Queryable().Where(x => x.TIME_YEAR == year).ToList();
                var dataDTXDCB = UnitOfWork.Repository<DauTuXayDungDataRepo>().Queryable().Where(x => x.TIME_YEAR == year).ToList();

                #region 
                data.BM01D.Add(new ReportModel
                {
                    Id = "A",
                    Stt = "A",
                    Name = "A. Dự án chuyển tiếp kỳ truớc",
                    NameExcel = "Dự án chuyển tiếp kỳ truớc",
                    IsBold = true,
                });
                data.BM01D.Add(new ReportModel
                {
                    Id = "A.I",
                    Stt = "I",
                    Name = "I. Đầu tư trang thiết bị",
                    NameExcel = "Đầu tư trang thiết bị",
                    Parent = "A",
                    IsBold = true,
                }); 
                data.BM01D.Add(new ReportModel
                {
                    Id = "A.I.1",
                    Stt = "1",
                    Name = "1. Các dự án chuẩn bị đầu tư",
                    NameExcel = "Các dự án chuẩn bị đầu tư",
                    Parent = "A.I",
                });
                foreach (var i in lstProjectTtb.Where(x => x.YEAR < year && x.GIAI_DOAN == "CBDT"))
                {
                    data.BM01D.Add(new ReportModel
                    {
                        Id = i.CODE,
                        Parent = "A.I.1",
                        Name = i.NAME,
                        NameExcel = i.NAME,
                        Col1 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4001")?.Sum(x => x.VALUE) ?? 0,
                        Col2 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4002")?.Sum(x => x.VALUE) ?? 0,
                        Col3 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4003")?.Sum(x => x.VALUE) ?? 0,
                        Col4 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4010")?.Sum(x => x.VALUE) ?? 0,
                        Col5 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4011")?.Sum(x => x.VALUE) ?? 0,
                        Col6 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4012")?.Sum(x => x.VALUE) ?? 0,
                    });
                }


                data.BM01D.Add(new ReportModel
                {
                    Id = "A.I.2",
                    Stt = "2",
                    Name = "2. Các dự án thực hiện đầu tư",
                    NameExcel = "Các dự án thực hiện đầu tư",
                    Parent = "A.I",
                });
                foreach (var i in lstProjectTtb.Where(x => x.YEAR < year && x.GIAI_DOAN == "TTDT"))
                {
                    data.BM01D.Add(new ReportModel
                    {
                        Id = i.CODE,
                        Parent = "A.I.2",
                        Name = i.NAME,
                        NameExcel = i.NAME,
                        Col1 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4001")?.Sum(x => x.VALUE) ?? 0,
                        Col2 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4002")?.Sum(x => x.VALUE) ?? 0,
                        Col3 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4003")?.Sum(x => x.VALUE) ?? 0,
                        Col4 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4010")?.Sum(x => x.VALUE) ?? 0,
                        Col5 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4011")?.Sum(x => x.VALUE) ?? 0,
                        Col6 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4012")?.Sum(x => x.VALUE) ?? 0,
                    });
                }


                data.BM01D.Add(new ReportModel
                {
                    Id = "A.I.3",
                    Stt = "3",
                    Name = "3. Đầu tư trang thiết bị lẻ",
                    NameExcel = "Đầu tư trang thiết bị lẻ",
                    Parent = "A.I",
                });

                foreach (var i in lstProjectTtb.Where(x => x.YEAR < year && x.GIAI_DOAN == "TTBL"))
                {
                    data.BM01D.Add(new ReportModel
                    {
                        Id = i.CODE,
                        Parent = "A.I.3",
                        Name = i.NAME,
                        NameExcel = i.NAME,
                        Col1 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4001")?.Sum(x => x.VALUE) ?? 0,
                        Col2 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4002")?.Sum(x => x.VALUE) ?? 0,
                        Col3 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4003")?.Sum(x => x.VALUE) ?? 0,
                        Col4 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4010")?.Sum(x => x.VALUE) ?? 0,
                        Col5 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4011")?.Sum(x => x.VALUE) ?? 0,
                        Col6 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4012")?.Sum(x => x.VALUE) ?? 0,
                    });
                }

                data.BM01D.Add(new ReportModel
                {
                    Id = "A.II",
                    Stt = "II",
                    Name = "II. Đầu tư xây dựng cơ bản",
                    NameExcel = "Đầu tư xây dựng cơ bản",
                    Parent = "A",
                    IsBold = true,
                });
                data.BM01D.Add(new ReportModel
                {
                    Id = "A.II.1",
                    Stt = "1",
                    Name = "1. Các dự án chuẩn bị đầu tư",
                    NameExcel = "Các dự án chuẩn bị đầu tư",
                    Parent = "A.II",
                });
                foreach (var i in lstProjectXdcb.Where(x => x.YEAR < year && x.GIAI_DOAN == "CBDT"))
                {
                    data.BM01D.Add(new ReportModel
                    {
                        Id = i.CODE,
                        Parent = "A.II.1",
                        Name = i.NAME,
                        NameExcel = i.NAME,
                        Col1 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4001")?.Sum(x => x.VALUE) ?? 0,
                        Col2 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4002")?.Sum(x => x.VALUE) ?? 0,
                        Col3 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4003")?.Sum(x => x.VALUE) ?? 0,
                        Col4 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4010")?.Sum(x => x.VALUE) ?? 0,
                        Col5 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4011")?.Sum(x => x.VALUE) ?? 0,
                        Col6 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4012")?.Sum(x => x.VALUE) ?? 0,
                    });
                }

                data.BM01D.Add(new ReportModel
                {
                    Id = "A.II.2",
                    Stt = "2",
                    Name = "2. Các dự án thực hiện đầu tư",
                    NameExcel = "Các dự án thực hiện đầu tư",
                    Parent = "A.II",
                });
                foreach (var i in lstProjectXdcb.Where(x => x.YEAR < year && x.GIAI_DOAN == "TTDT"))
                {
                    data.BM01D.Add(new ReportModel
                    {
                        Id = i.CODE,
                        Parent = "A.II.2",
                        Name = i.NAME,
                        NameExcel = i.NAME,
                        Col1 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4001")?.Sum(x => x.VALUE) ?? 0,
                        Col2 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4002")?.Sum(x => x.VALUE) ?? 0,
                        Col3 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4003")?.Sum(x => x.VALUE) ?? 0,
                        Col4 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4010")?.Sum(x => x.VALUE) ?? 0,
                        Col5 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4011")?.Sum(x => x.VALUE) ?? 0,
                        Col6 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4012")?.Sum(x => x.VALUE) ?? 0,
                    });
                }

                data.BM01D.Add(new ReportModel
                {
                    Id = "B",
                    Stt = "B",
                    Name = "B. Dự án đầu tư mới",
                    NameExcel = "Dự án đầu tư mới",
                    IsBold = true,
                });
                data.BM01D.Add(new ReportModel
                {
                    Id = "B.I",
                    Stt = "I",
                    Name = "I. Đầu tư trang thiết bị",
                    NameExcel = "Đầu tư trang thiết bị",
                    Parent = "B",
                    IsBold = true,
                });
                data.BM01D.Add(new ReportModel
                {
                    Id = "B.I.1",
                    Stt = "1",
                    Name = "1. Các dự án chuẩn bị đầu tư",
                    NameExcel = "Các dự án chuẩn bị đầu tư",
                    Parent = "B.I",
                });
                foreach (var i in lstProjectTtb.Where(x => x.YEAR == year && x.GIAI_DOAN == "CBDT"))
                {
                    data.BM01D.Add(new ReportModel
                    {
                        Id = i.CODE,
                        Parent = "B.I.1",
                        Name = i.NAME,
                        NameExcel = i.NAME,
                        Col1 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4001")?.Sum(x => x.VALUE) ?? 0,
                        Col2 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4002")?.Sum(x => x.VALUE) ?? 0,
                        Col3 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4003")?.Sum(x => x.VALUE) ?? 0,
                        Col4 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4010")?.Sum(x => x.VALUE) ?? 0,
                        Col5 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4011")?.Sum(x => x.VALUE) ?? 0,
                        Col6 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4012")?.Sum(x => x.VALUE) ?? 0,
                    });
                }


                data.BM01D.Add(new ReportModel
                {
                    Id = "B.I.2",
                    Stt = "2",
                    Name = "2. Các dự án thực hiện đầu tư",
                    NameExcel = "Các dự án thực hiện đầu tư",
                    Parent = "B.I",
                });
                foreach (var i in lstProjectTtb.Where(x => x.YEAR == year && x.GIAI_DOAN == "TTDT"))
                {
                    data.BM01D.Add(new ReportModel
                    {
                        Id = i.CODE,
                        Parent = "B.I.2",
                        Name = i.NAME,
                        NameExcel = i.NAME,
                        Col1 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4001")?.Sum(x => x.VALUE) ?? 0,
                        Col2 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4002")?.Sum(x => x.VALUE) ?? 0,
                        Col3 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4003")?.Sum(x => x.VALUE) ?? 0,
                        Col4 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4010")?.Sum(x => x.VALUE) ?? 0,
                        Col5 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4011")?.Sum(x => x.VALUE) ?? 0,
                        Col6 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4012")?.Sum(x => x.VALUE) ?? 0,
                    });
                }


                data.BM01D.Add(new ReportModel
                {
                    Id = "B.I.3",
                    Stt = "3",
                    Name = "3. Đầu tư trang thiết bị lẻ",
                    NameExcel = "Đầu tư trang thiết bị lẻ",
                    Parent = "B.I",
                });

                foreach (var i in lstProjectTtb.Where(x => x.YEAR == year && x.GIAI_DOAN == "TTBL"))
                {
                    data.BM01D.Add(new ReportModel
                    {
                        Id = i.CODE,
                        Parent = "B.I.3",
                        Name = i.NAME,
                        NameExcel = i.NAME,
                        Col1 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4001")?.Sum(x => x.VALUE) ?? 0,
                        Col2 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4002")?.Sum(x => x.VALUE) ?? 0,
                        Col3 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4003")?.Sum(x => x.VALUE) ?? 0,
                        Col4 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4010")?.Sum(x => x.VALUE) ?? 0,
                        Col5 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4011")?.Sum(x => x.VALUE) ?? 0,
                        Col6 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4012")?.Sum(x => x.VALUE) ?? 0,
                    });
                }

                data.BM01D.Add(new ReportModel
                {
                    Id = "B.II",
                    Stt = "II",
                    Name = "II. Đầu tư xây dựng cơ bản",
                    NameExcel = "Đầu tư xây dựng cơ bản",
                    Parent = "B",
                    IsBold = true,
                });
                data.BM01D.Add(new ReportModel
                {
                    Id = "B.II.1",
                    Stt = "1",
                    Name = "1. Các dự án chuẩn bị đầu tư",
                    NameExcel = "Các dự án chuẩn bị đầu tư",
                    Parent = "B.II",
                });
                foreach (var i in lstProjectXdcb.Where(x => x.YEAR == year && x.GIAI_DOAN == "CBDT"))
                {
                    data.BM01D.Add(new ReportModel
                    {
                        Id = i.CODE,
                        Parent = "B.II.1",
                        Name = i.NAME,
                        NameExcel = i.NAME,
                        Col1 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4001")?.Sum(x => x.VALUE) ?? 0,
                        Col2 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4002")?.Sum(x => x.VALUE) ?? 0,
                        Col3 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4003")?.Sum(x => x.VALUE) ?? 0,
                        Col4 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4010")?.Sum(x => x.VALUE) ?? 0,
                        Col5 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4011")?.Sum(x => x.VALUE) ?? 0,
                        Col6 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4012")?.Sum(x => x.VALUE) ?? 0,
                    });
                }

                data.BM01D.Add(new ReportModel
                {
                    Id = "B.II.2",
                    Stt = "2",
                    Name = "2. Các dự án thực hiện đầu tư",
                    NameExcel = "Các dự án thực hiện đầu tư",
                    Parent = "B.II",
                });
                foreach (var i in lstProjectXdcb.Where(x => x.YEAR == year && x.GIAI_DOAN == "TTDT"))
                {
                    data.BM01D.Add(new ReportModel
                    {
                        Id = i.CODE,
                        Parent = "B.II.2",
                        Name = i.NAME,
                        NameExcel = i.NAME,
                        Col1 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4001")?.Sum(x => x.VALUE) ?? 0,
                        Col2 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4002")?.Sum(x => x.VALUE) ?? 0,
                        Col3 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4003")?.Sum(x => x.VALUE) ?? 0,
                        Col4 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4010")?.Sum(x => x.VALUE) ?? 0,
                        Col5 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4011")?.Sum(x => x.VALUE) ?? 0,
                        Col6 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4012")?.Sum(x => x.VALUE) ?? 0,
                    });
                }
                #endregion

                return data;
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                return new ReportDataCenter();
            }
        }
        public ReportDataCenter GenDataBM02A(int year)
        {
            try
            {
                var data = new ReportDataCenter();
                var lstProject = UnitOfWork.Repository<ProjectRepo>().Queryable().Where(x => x.YEAR > 0).ToList();
                var lstProjectTtb = lstProject.Where(x => x.LOAI_HINH == "TTB").ToList();
                var lstProjectXdcb = lstProject.Where(x => x.LOAI_HINH == "XDCB").ToList();


                var dataDTTTB = UnitOfWork.Repository<DauTuTrangThietBiDataRepo>().Queryable().Where(x => x.TIME_YEAR == year).ToList();
                var dataDTXDCB = UnitOfWork.Repository<DauTuXayDungDataRepo>().Queryable().Where(x => x.TIME_YEAR == year).ToList();

                #region 
                data.BM02A.Add(new ReportModel
                {
                    Id = "A",
                    Stt = "A",
                    Name = "A. Dự án chuyển tiếp kỳ truớc",
                    IsBold = true,
                });
                data.BM02A.Add(new ReportModel
                {
                    Id = "A.I",
                    Stt = "I",
                    Name = "I. Đầu tư trang thiết bị",
                    Parent = "A",
                    IsBold = true,
                });
                data.BM02A.Add(new ReportModel
                {
                    Id = "A.I.1",
                    Stt = "1",
                    Name = "1. Các dự án chuẩn bị đầu tư",
                    Parent = "A.I",
                });
                foreach (var i in lstProjectTtb.Where(x => x.YEAR < year && x.GIAI_DOAN == "CBDT"))
                {
                    data.BM02A.Add(new ReportModel
                    {
                        Id = i.CODE,
                        Parent = "A.I.1",
                        Name = i.NAME,
                        Col1 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4001")?.Sum(x => x.VALUE) ?? 0,
                        Col2 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4002")?.Sum(x => x.VALUE) ?? 0,
                        Tdth = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.PROCESS ?? "",
                        Col4 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4010")?.Sum(x => x.VALUE) ?? 0,
                        Col5 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4011")?.Sum(x => x.VALUE) ?? 0,
                        Tdtk = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.TDTK ?? "",
                        Col7 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4020")?.Sum(x => x.VALUE) ?? 0,
                        Col8 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4021")?.Sum(x => x.VALUE) ?? 0,
                        Des = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.DESCRIPTION ?? "",
                    });
                }


                data.BM02A.Add(new ReportModel
                {
                    Id = "A.I.2",
                    Stt = "2",
                    Name = "2. Các dự án thực hiện đầu tư",
                    Parent = "A.I",
                });
                foreach (var i in lstProjectTtb.Where(x => x.YEAR < year && x.GIAI_DOAN == "TTDT"))
                {
                    data.BM02A.Add(new ReportModel
                    {
                        Id = i.CODE,
                        Parent = "A.I.2",
                        Name = i.NAME,
                        Col1 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4001")?.Sum(x => x.VALUE) ?? 0,
                        Col2 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4002")?.Sum(x => x.VALUE) ?? 0,
                        Tdth = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.PROCESS ?? "",
                        Col4 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4010")?.Sum(x => x.VALUE) ?? 0,
                        Col5 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4011")?.Sum(x => x.VALUE) ?? 0,
                        Tdtk = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.TDTK ?? "",
                        Col7 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4020")?.Sum(x => x.VALUE) ?? 0,
                        Col8 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4021")?.Sum(x => x.VALUE) ?? 0,
                        Des = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.DESCRIPTION ?? "",
                    });
                }


                data.BM02A.Add(new ReportModel
                {
                    Id = "A.I.3",
                    Stt = "3",
                    Name = "3. Đầu tư trang thiết bị lẻ",
                    Parent = "A.I",
                });

                foreach (var i in lstProjectTtb.Where(x => x.YEAR < year && x.GIAI_DOAN == "TTBL"))
                {
                    data.BM02A.Add(new ReportModel
                    {
                        Id = i.CODE,
                        Parent = "A.I.3",
                        Name = i.NAME,
                        Col1 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4001")?.Sum(x => x.VALUE) ?? 0,
                        Col2 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4002")?.Sum(x => x.VALUE) ?? 0,
                        Tdth = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.PROCESS ?? "",
                        Col4 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4010")?.Sum(x => x.VALUE) ?? 0,
                        Col5 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4011")?.Sum(x => x.VALUE) ?? 0,
                        Tdtk = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.TDTK ?? "",
                        Col7 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4020")?.Sum(x => x.VALUE) ?? 0,
                        Col8 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4021")?.Sum(x => x.VALUE) ?? 0,
                        Des = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.DESCRIPTION ?? "",
                    });
                }

                data.BM02A.Add(new ReportModel
                {
                    Id = "A.II",
                    Stt = "II",
                    Name = "II. Đầu tư xây dựng cơ bản",
                    Parent = "A",
                    IsBold = true,
                });
                data.BM02A.Add(new ReportModel
                {
                    Id = "A.II.1",
                    Stt = "1",
                    Name = "1. Các dự án chuẩn bị đầu tư",
                    Parent = "A.II",
                });
                foreach (var i in lstProjectXdcb.Where(x => x.YEAR < year && x.GIAI_DOAN == "CBDT"))
                {
                    data.BM02A.Add(new ReportModel
                    {
                        Id = i.CODE,
                        Parent = "A.II.1",
                        Name = i.NAME,
                        Col1 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4001")?.Sum(x => x.VALUE) ?? 0,
                        Col2 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4002")?.Sum(x => x.VALUE) ?? 0,
                        Tdth = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.PROCESS ?? "",
                        Col4 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4010")?.Sum(x => x.VALUE) ?? 0,
                        Col5 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4011")?.Sum(x => x.VALUE) ?? 0,
                        Tdtk = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.TDTK ?? "",
                        Col7 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4020")?.Sum(x => x.VALUE) ?? 0,
                        Col8 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4021")?.Sum(x => x.VALUE) ?? 0,
                        Des = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.DESCRIPTION ?? "",
                    });
                }

                data.BM02A.Add(new ReportModel
                {
                    Id = "A.II.2",
                    Stt = "2",
                    Name = "2. Các dự án thực hiện đầu tư",
                    Parent = "A.II",
                });
                foreach (var i in lstProjectXdcb.Where(x => x.YEAR < year && x.GIAI_DOAN == "TTDT"))
                {
                    data.BM02A.Add(new ReportModel
                    {
                        Id = i.CODE,
                        Parent = "A.II.2",
                        Name = i.NAME,
                        Col1 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4001")?.Sum(x => x.VALUE) ?? 0,
                        Col2 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4002")?.Sum(x => x.VALUE) ?? 0,
                        Tdth = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.PROCESS ?? "",
                        Col4 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4010")?.Sum(x => x.VALUE) ?? 0,
                        Col5 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4011")?.Sum(x => x.VALUE) ?? 0,
                        Tdtk = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.TDTK ?? "",
                        Col7 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4020")?.Sum(x => x.VALUE) ?? 0,
                        Col8 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4021")?.Sum(x => x.VALUE) ?? 0,
                        Des = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.DESCRIPTION ?? "",
                    });
                }

                data.BM02A.Add(new ReportModel
                {
                    Id = "B",
                    Stt = "B",
                    Name = "B. Dự án đầu tư mới",
                    IsBold = true,
                });
                data.BM02A.Add(new ReportModel
                {
                    Id = "B.I",
                    Stt = "I",
                    Name = "I. Đầu tư trang thiết bị",
                    Parent = "B",
                    IsBold = true,
                });
                data.BM02A.Add(new ReportModel
                {
                    Id = "B.I.1",
                    Stt = "1",
                    Name = "1. Các dự án chuẩn bị đầu tư",
                    Parent = "B.I",
                });
                foreach (var i in lstProjectTtb.Where(x => x.YEAR == year && x.GIAI_DOAN == "CBDT"))
                {
                    data.BM02A.Add(new ReportModel
                    {
                        Id = i.CODE,
                        Parent = "B.I.1",
                        Name = i.NAME,
                        Col1 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4001")?.Sum(x => x.VALUE) ?? 0,
                        Col2 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4002")?.Sum(x => x.VALUE) ?? 0,
                        Tdth = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.PROCESS ?? "",
                        Col4 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4010")?.Sum(x => x.VALUE) ?? 0,
                        Col5 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4011")?.Sum(x => x.VALUE) ?? 0,
                        Tdtk = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.TDTK ?? "",
                        Col7 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4020")?.Sum(x => x.VALUE) ?? 0,
                        Col8 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4021")?.Sum(x => x.VALUE) ?? 0,
                        Des = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.DESCRIPTION ?? "",
                    });
                }


                data.BM02A.Add(new ReportModel
                {
                    Id = "B.I.2",
                    Stt = "2",
                    Name = "2. Các dự án thực hiện đầu tư",
                    Parent = "B.I",
                });
                foreach (var i in lstProjectTtb.Where(x => x.YEAR == year && x.GIAI_DOAN == "TTDT"))
                {
                    data.BM02A.Add(new ReportModel
                    {
                        Id = i.CODE,
                        Parent = "B.I.2",
                        Name = i.NAME,
                        Col1 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4001")?.Sum(x => x.VALUE) ?? 0,
                        Col2 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4002")?.Sum(x => x.VALUE) ?? 0,
                        Tdth = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.PROCESS ?? "",
                        Col4 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4010")?.Sum(x => x.VALUE) ?? 0,
                        Col5 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4011")?.Sum(x => x.VALUE) ?? 0,
                        Tdtk = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.TDTK ?? "",
                        Col7 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4020")?.Sum(x => x.VALUE) ?? 0,
                        Col8 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4021")?.Sum(x => x.VALUE) ?? 0,
                        Des = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.DESCRIPTION ?? "",
                    });
                }


                data.BM02A.Add(new ReportModel
                {
                    Id = "B.I.3",
                    Stt = "3",
                    Name = "3. Đầu tư trang thiết bị lẻ",
                    Parent = "B.I",
                });

                foreach (var i in lstProjectTtb.Where(x => x.YEAR == year && x.GIAI_DOAN == "TTBL"))
                {
                    data.BM02A.Add(new ReportModel
                    {
                        Id = i.CODE,
                        Parent = "B.I.3",
                        Name = i.NAME,
                        Col1 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4001")?.Sum(x => x.VALUE) ?? 0,
                        Col2 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4002")?.Sum(x => x.VALUE) ?? 0,
                        Tdth = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.PROCESS ?? "",
                        Col4 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4010")?.Sum(x => x.VALUE) ?? 0,
                        Col5 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4011")?.Sum(x => x.VALUE) ?? 0,
                        Tdtk = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.TDTK ?? "",
                        Col7 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4020")?.Sum(x => x.VALUE) ?? 0,
                        Col8 = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4021")?.Sum(x => x.VALUE) ?? 0,
                        Des = dataDTTTB.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.DESCRIPTION ?? "",
                    });
                }

                data.BM02A.Add(new ReportModel
                {
                    Id = "B.II",
                    Stt = "II",
                    Name = "II. Đầu tư xây dựng cơ bản",
                    Parent = "B",
                    IsBold = true,
                });
                data.BM02A.Add(new ReportModel
                {
                    Id = "B.II.1",
                    Stt = "1",
                    Name = "1. Các dự án chuẩn bị đầu tư",
                    Parent = "B.II",
                });
                foreach (var i in lstProjectXdcb.Where(x => x.YEAR == year && x.GIAI_DOAN == "CBDT"))
                {
                    data.BM02A.Add(new ReportModel
                    {
                        Id = i.CODE,
                        Parent = "B.II.1",
                        Name = i.NAME,
                        Col1 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4001")?.Sum(x => x.VALUE) ?? 0,
                        Col2 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4002")?.Sum(x => x.VALUE) ?? 0,
                        Tdth = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.PROCESS ?? "",
                        Col4 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4010")?.Sum(x => x.VALUE) ?? 0,
                        Col5 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4011")?.Sum(x => x.VALUE) ?? 0,
                        Tdtk = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.TDTK ?? "",
                        Col7 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4020")?.Sum(x => x.VALUE) ?? 0,
                        Col8 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4021")?.Sum(x => x.VALUE) ?? 0,
                        Des = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.DESCRIPTION ?? "",
                    });
                }

                data.BM02A.Add(new ReportModel
                {
                    Id = "B.II.2",
                    Stt = "2",
                    Name = "2. Các dự án thực hiện đầu tư",
                    Parent = "B.II",
                });
                foreach (var i in lstProjectXdcb.Where(x => x.YEAR == year && x.GIAI_DOAN == "TTDT"))
                {
                    data.BM02A.Add(new ReportModel
                    {
                        Id = i.CODE,
                        Parent = "B.II.2",
                        Name = i.NAME,
                        Col1 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4001")?.Sum(x => x.VALUE) ?? 0,
                        Col2 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4002")?.Sum(x => x.VALUE) ?? 0,
                        Tdth = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.PROCESS ?? "",
                        Col4 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4010")?.Sum(x => x.VALUE) ?? 0,
                        Col5 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4011")?.Sum(x => x.VALUE) ?? 0,
                        Tdtk = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.TDTK ?? "",
                        Col7 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4020")?.Sum(x => x.VALUE) ?? 0,
                        Col8 = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE && x.KHOAN_MUC_DAU_TU_CODE == "4021")?.Sum(x => x.VALUE) ?? 0,
                        Des = dataDTXDCB.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == i.CODE).FirstOrDefault()?.DESCRIPTION ?? "",
                    });
                }
                #endregion
                foreach (var i in data.BM02A.OrderByDescending(x => x.Id))
                {
                    var child = data.BM02A.Where(x => x.Parent == i.Id).ToList();
                    i.Col1 = child.Count() == 0 ? i.Col1 : child.Sum(x => x.Col1);
                    i.Col2 = child.Count() == 0 ? i.Col2 : child.Sum(x => x.Col2);
                    i.Col4 = child.Count() == 0 ? i.Col4 : child.Sum(x => x.Col4);
                    i.Col5 = child.Count() == 0 ? i.Col5 : child.Sum(x => x.Col5);
                    i.Col7 = child.Count() == 0 ? i.Col7 : child.Sum(x => x.Col7);
                    i.Col8 = child.Count() == 0 ? i.Col8 : child.Sum(x => x.Col8);
                }
                return data;
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                return new ReportDataCenter();
            }
        }

        public ReportDataCenter GenDataBM02B(int year)
        {
            try
            {
                var data = new ReportDataCenter();
                var lstProject = UnitOfWork.Repository<ProjectRepo>().Queryable().Where(x => x.YEAR > 0 && x.LOAI_HINH == "NDN").ToList();
                var template = UnitOfWork.Repository<DauTuNgoaiDoanhNghiepRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var dataCurrent = UnitOfWork.Repository<DauTuNgoaiDoanhNghiepDataRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.DauTuNgoaiDoanhNghiepProfitCenter != null && template.Contains(x.TEMPLATE_CODE)).ToList();

                #region 
                data.BM02A.Add(new ReportModel
                {
                    Id = "A",
                    Stt = "A",
                    Name = "A. Đầu tư vào ngành nghề kinh doanh chính",
                    IsBold = true,
                });
                data.BM02A.Add(new ReportModel
                {
                    Id = "A.I",
                    Stt = "I",
                    Name = "I. Đầu tư vào công ty con",
                    Parent = "A",
                });

                foreach (var prj in lstProject.Where(x => x.NGANH_NGHE == "KDC" && x.PHAN_LOAI == "CTC"))
                {
                    data.BM02A.Add(new ReportModel
                    {
                        Id = prj.CODE,
                        Stt = prj.CODE,
                        Name = prj.NAME,
                        Parent = "A.I"
                    });

                    data.BM02A.Add(new ReportModel
                    {
                        Id = prj.CODE + "5101",
                        Stt = prj.CODE + "5101",
                        Name = "- Giá trị đầu tư thực tế",
                        Parent = prj.CODE,
                        Col1 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_1),
                        Col2 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col3 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_3),
                        Col4 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_4),
                        Col5 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_5),
                        Col6 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_6),
                        Des = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).FirstOrDefault()?.DESCRIPTION,
                    });
                    data.BM02A.Add(new ReportModel
                    {
                        Id = prj.CODE + "5102",
                        Stt = prj.CODE + "5102",
                        Name = "- Giá trị đầu tư theo mệnh giá",
                        Parent = prj.CODE,
                        Col1 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_1),
                        Col2 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col3 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_3),
                        Col4 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_4),
                        Col5 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_5),
                        Col6 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_6),
                        Des = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).FirstOrDefault()?.DESCRIPTION,
                    });
                }

                data.BM02A.Add(new ReportModel
                {
                    Id = "A.II",
                    Stt = "II",
                    Name = "II. Đầu tư vào công ty liên kết",
                    Parent = "A",
                });

                foreach (var prj in lstProject.Where(x => x.NGANH_NGHE == "KDC" && x.PHAN_LOAI == "CTLK"))
                {
                    data.BM02A.Add(new ReportModel
                    {
                        Id = prj.CODE,
                        Stt = prj.CODE,
                        Name = prj.NAME,
                        Parent = "A.II"
                    });
                    data.BM02A.Add(new ReportModel
                    {
                        Id = prj.CODE + "5101",
                        Stt = prj.CODE + "5101",
                        Name = "- Giá trị đầu tư thực tế",
                        Parent = prj.CODE,
                        Col1 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_1),
                        Col2 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col3 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_3),
                        Col4 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_4),
                        Col5 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_5),
                        Col6 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_6),
                        Des = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).FirstOrDefault()?.DESCRIPTION,
                    });
                    data.BM02A.Add(new ReportModel
                    {
                        Id = prj.CODE + "5102",
                        Stt = prj.CODE + "5102",
                        Name = "- Giá trị đầu tư theo mệnh giá",
                        Parent = prj.CODE,
                        Col1 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_1),
                        Col2 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col3 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_3),
                        Col4 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_4),
                        Col5 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_5),
                        Col6 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_6),
                        Des = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).FirstOrDefault()?.DESCRIPTION,
                    });
                }

                data.BM02A.Add(new ReportModel
                {
                    Id = "A.III",
                    Stt = "III",
                    Name = "III. Đầu tư tài chính khác",
                    Parent = "A",
                });

                foreach (var prj in lstProject.Where(x => x.NGANH_NGHE == "KDC" && x.PHAN_LOAI == "TC"))
                {
                    data.BM02A.Add(new ReportModel
                    {
                        Id = prj.CODE,
                        Stt = prj.CODE,
                        Name = prj.NAME,
                        Parent = "A.III"
                    });
                    data.BM02A.Add(new ReportModel
                    {
                        Id = prj.CODE + "5101",
                        Stt = prj.CODE + "5101",
                        Name = "- Giá trị đầu tư thực tế",
                        Parent = prj.CODE,
                        Col1 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_1),
                        Col2 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col3 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_3),
                        Col4 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_4),
                        Col5 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_5),
                        Col6 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_6),
                        Des = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).FirstOrDefault()?.DESCRIPTION,
                    });
                    data.BM02A.Add(new ReportModel
                    {
                        Id = prj.CODE + "5102",
                        Stt = prj.CODE + "5102",
                        Name = "- Giá trị đầu tư theo mệnh giá",
                        Parent = prj.CODE,
                        Col1 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_1),
                        Col2 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col3 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_3),
                        Col4 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_4),
                        Col5 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_5),
                        Col6 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_6),
                        Des = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).FirstOrDefault()?.DESCRIPTION,
                    });
                }

                data.BM02A.Add(new ReportModel
                {
                    Id = "B",
                    Stt = "B",
                    Name = "B. Đầu tư vào ngành nghề khác",
                    IsBold = true,
                });
                data.BM02A.Add(new ReportModel
                {
                    Id = "B.I",
                    Stt = "I",
                    Name = "I. Đầu tư vào công ty con",
                    Parent = "B",
                });

                foreach (var prj in lstProject.Where(x => x.NGANH_NGHE == "OTHER" && x.PHAN_LOAI == "CTC"))
                {
                    data.BM02A.Add(new ReportModel
                    {
                        Id = prj.CODE,
                        Stt = prj.CODE,
                        Name = prj.NAME,
                        Parent = "B.I"
                    });
                    data.BM02A.Add(new ReportModel
                    {
                        Id = prj.CODE + "5101",
                        Stt = prj.CODE + "5101",
                        Name = "- Giá trị đầu tư thực tế",
                        Parent = prj.CODE,
                        Col1 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_1),
                        Col2 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col3 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_3),
                        Col4 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_4),
                        Col5 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_5),
                        Col6 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_6),
                        Des = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).FirstOrDefault()?.DESCRIPTION,
                    });
                    data.BM02A.Add(new ReportModel
                    {
                        Id = prj.CODE + "5102",
                        Stt = prj.CODE + "5102",
                        Name = "- Giá trị đầu tư theo mệnh giá",
                        Parent = prj.CODE,
                        Col1 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_1),
                        Col2 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col3 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_3),
                        Col4 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_4),
                        Col5 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_5),
                        Col6 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_6),
                        Des = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).FirstOrDefault()?.DESCRIPTION,
                    });
                }

                data.BM02A.Add(new ReportModel
                {
                    Id = "B.II",
                    Stt = "II",
                    Name = "II. Đầu tư vào công ty liên kết",
                    Parent = "B",
                });

                foreach (var prj in lstProject.Where(x => x.NGANH_NGHE == "OTHER" && x.PHAN_LOAI == "CTLK"))
                {
                    data.BM02A.Add(new ReportModel
                    {
                        Id = prj.CODE,
                        Stt = prj.CODE,
                        Name = prj.NAME,
                        Parent = "B.II"
                    });
                    data.BM02A.Add(new ReportModel
                    {
                        Id = prj.CODE + "5101",
                        Stt = prj.CODE + "5101",
                        Name = "- Giá trị đầu tư thực tế",
                        Parent = prj.CODE,
                        Col1 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_1),
                        Col2 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col3 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_3),
                        Col4 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_4),
                        Col5 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_5),
                        Col6 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_6),
                        Des = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).FirstOrDefault()?.DESCRIPTION,
                    });
                    data.BM02A.Add(new ReportModel
                    {
                        Id = prj.CODE + "5102",
                        Stt = prj.CODE + "5102",
                        Name = "- Giá trị đầu tư theo mệnh giá",
                        Parent = prj.CODE,
                        Col1 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_1),
                        Col2 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col3 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_3),
                        Col4 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_4),
                        Col5 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_5),
                        Col6 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_6),
                        Des = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).FirstOrDefault()?.DESCRIPTION,
                    });
                }

                data.BM02A.Add(new ReportModel
                {
                    Id = "B.III",
                    Stt = "III",
                    Name = "III. Đầu tư tài chính khác",
                    Parent = "B",
                });

                foreach (var prj in lstProject.Where(x => x.NGANH_NGHE == "OTHER" && x.PHAN_LOAI == "TC"))
                {
                    data.BM02A.Add(new ReportModel
                    {
                        Id = prj.CODE,
                        Stt = prj.CODE,
                        Name = prj.NAME,
                        Parent = "B.III"
                    });
                    data.BM02A.Add(new ReportModel
                    {
                        Id = prj.CODE + "5101",
                        Stt = prj.CODE + "5101",
                        Name = "- Giá trị đầu tư thực tế",
                        Parent = prj.CODE,
                        Col1 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_1),
                        Col2 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col3 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_3),
                        Col4 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_4),
                        Col5 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_5),
                        Col6 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_6),
                        Des = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).FirstOrDefault()?.DESCRIPTION,
                    });
                    data.BM02A.Add(new ReportModel
                    {
                        Id = prj.CODE + "5102",
                        Stt = prj.CODE + "5102",
                        Name = "- Giá trị đầu tư theo mệnh giá",
                        Parent = prj.CODE,
                        Col1 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_1),
                        Col2 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col3 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_3),
                        Col4 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_4),
                        Col5 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_5),
                        Col6 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_6),
                        Des = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).FirstOrDefault()?.DESCRIPTION,
                    });
                }

                #endregion
                foreach (var i in data.BM02A.OrderByDescending(x => x.Id))
                {
                    var child = data.BM02A.Where(x => x.Parent == i.Id).ToList();
                    i.Col1 = child.Count() == 0 ? i.Col1 : child.Sum(x => x.Col1);
                    i.Col2 = child.Count() == 0 ? i.Col2 : child.Sum(x => x.Col2);
                    i.Col3 = child.Count() == 0 ? i.Col3 : child.Sum(x => x.Col3);
                    i.Col5 = child.Count() == 0 ? i.Col5 : child.Sum(x => x.Col5);
                }
                return data;
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                return new ReportDataCenter();
            }
        }
        public ReportDataCenter GenDataBM02C(int year, string kichBan)
        {
            try
            {
                var service = new ElementService();
                var serviceKb = new KichBanService();
                var data = new ReportDataCenter();

                var dataKHTCPrevious = service.GetDataKeHoachTaiChinh(year - 1);
                var dataKHTC = service.GetDataKeHoachTaiChinh(year);
                var dataSXKD = serviceKb.GetData(year, kichBan);

                var I = new ReportModel
                {
                    Id = "I",
                    Stt = "I",
                    Name = "I. Hoạt động SXKD",
                    Parent = "SUM",
                    Col4 = dataSXKD.Where(x => x.Name == "Doanh thu từ hoạt động SXKD").Sum(x => x.Value4),
                    Col5 = dataSXKD.Where(x => x.Name == "Chi phí sản xuất kinh doanh").Sum(x => x.Value4),
                    Col7 = dataSXKD.Where(x => x.Name == "Doanh thu từ hoạt động SXKD").Sum(x => x.Value5),
                    Col8 = dataSXKD.Where(x => x.Name == "Chi phí sản xuất kinh doanh").Sum(x => x.Value5),
                };
                I.Col9 = I.Col7 - I.Col8;
                I.Col6 = I.Col4 - I.Col5;
                data.BM02C.Add(I);

                var II = new ReportModel
                {
                    Id = "II",
                    Stt = "II",
                    Parent = "SUM",
                    Name = "II. Hoạt động tài chính",
                    Col1 = dataKHTCPrevious.KeHoachTaiChinhData.Where(x => x.ElementCode == "U0137").Sum(x => x.Value),
                    Col2 = dataKHTCPrevious.KeHoachTaiChinhData.Where(x => x.ElementCode == "U0138").Sum(x => x.Value),
                    Col7 = dataKHTC.KeHoachTaiChinhData.Where(x => x.ElementCode == "U0137").Sum(x => x.Value),
                    Col8 = dataKHTC.KeHoachTaiChinhData.Where(x => x.ElementCode == "U0138").Sum(x => x.Value),
                };
                II.Col3 = II.Col1 - II.Col2;
                II.Col6 = II.Col4 - II.Col5;
                II.Col9 = II.Col7 - II.Col8;
                data.BM02C.Add(II);

                data.BM02C.Add(new ReportModel
                {
                    Id = "III",
                    Stt = "III",
                    Parent = "SUM",
                    Name = "III. Hoạt động khác",
                });

                data.BM02C.Add(new ReportModel
                {
                    Id = "SUM",
                    Stt = "SUM",
                    Name = "TỔNG CỘNG",
                    IsBold = true,
                    Col1 = I.Col1 + II.Col1,
                    Col2 = I.Col2 + II.Col2,
                    Col3 = I.Col3 + II.Col3,
                    Col4 = I.Col4 + II.Col4,
                    Col5 = I.Col5 + II.Col5,
                    Col6 = I.Col6 + II.Col6,
                    Col7 = I.Col7 + II.Col7,
                    Col8 = I.Col8 + II.Col8,
                    Col9 = I.Col9 + II.Col9,
                });
                return data;
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                return new ReportDataCenter();
            }
        }

        public ReportDataCenter GenDataBM02C1(int year, string phienBan)
        {
            try
            {
                var service = new ElementService();
                var servicePb = new PhienBanService();
                var data = new ReportDataCenter();

                var dataKHTC = service.GetDataKeHoachTaiChinh(year);
                var dataSXKD = servicePb.GetData(year, phienBan);

                var I = new ReportModel
                {
                    Id = "I",
                    Stt = "I",
                    Name = "I. Hoạt động SXKD",
                    Parent = "SUM",
                    Col1 = dataSXKD.Where(x => x.Name == "Doanh thu từ hoạt động SXKD").Sum(x => x.Value1),
                    Col2 = dataSXKD.Where(x => x.Name == "Chi phí sản xuất kinh doanh").Sum(x => x.Value1),
                    Col4 = dataSXKD.Where(x => x.Name == "Doanh thu từ hoạt động SXKD").Sum(x => x.Value2),
                    Col5 = dataSXKD.Where(x => x.Name == "Chi phí sản xuất kinh doanh").Sum(x => x.Value2),
                    Col7 = dataSXKD.Where(x => x.Name == "Doanh thu từ hoạt động SXKD").Sum(x => x.Value3),
                    Col8 = dataSXKD.Where(x => x.Name == "Chi phí sản xuất kinh doanh").Sum(x => x.Value4),
                };
                I.Col9 = I.Col7 - I.Col8;
                I.Col6 = I.Col4 - I.Col5;
                I.Col3 = I.Col1 - I.Col2;
                data.BM02C.Add(I);

                var II = new ReportModel
                {
                    Id = "II",
                    Stt = "II",
                    Parent = "SUM",
                    Name = "II. Hoạt động tài chính",
                    Col4 = dataKHTC.KeHoachTaiChinhData.Where(x => x.ElementCode == "U0137").Sum(x => x.Value),
                    Col5 = dataKHTC.KeHoachTaiChinhData.Where(x => x.ElementCode == "U0138").Sum(x => x.Value),
                };
                II.Col3 = II.Col1 - II.Col2;
                II.Col6 = II.Col4 - II.Col5;
                II.Col9 = II.Col7 - II.Col8;
                data.BM02C.Add(II);

                data.BM02C.Add(new ReportModel
                {
                    Id = "III",
                    Stt = "III",
                    Parent = "SUM",
                    Name = "III. Hoạt động khác",
                });

                data.BM02C.Add(new ReportModel
                {
                    Id = "SUM",
                    Stt = "SUM",
                    Name = "TỔNG CỘNG",
                    IsBold = true,
                    Col1 = I.Col1 + II.Col1,
                    Col2 = I.Col2 + II.Col2,
                    Col3 = I.Col3 + II.Col3,
                    Col4 = I.Col4 + II.Col4,
                    Col5 = I.Col5 + II.Col5,
                    Col6 = I.Col6 + II.Col6,
                    Col7 = I.Col7 + II.Col7,
                    Col8 = I.Col8 + II.Col8,
                    Col9 = I.Col9 + II.Col9,
                });
                return data;
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                return new ReportDataCenter();
            }
        }
        public ReportDataCenter GenDataBM02D(int year, string kichBan)
        {
            try
            {
                var data = new ReportDataCenter();


                var headerSL_TH_5 = UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.KICH_BAN == kichBan && x.PHIEN_BAN == "PB5" && x.TIME_YEAR == year - 5).Select(x => x.TEMPLATE_CODE).ToList();
                var dataSL_TH_5 = UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => headerSL_TH_5.Contains(x.TEMPLATE_CODE)).ToList();              
                var headerSL_KH_1 = UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.KICH_BAN == kichBan && x.PHIEN_BAN == "PB1" && x.TIME_YEAR == year - 1 && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var dataSL_KH_1 = UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => headerSL_KH_1.Contains(x.TEMPLATE_CODE)).ToList();                
                var headerSL_KH = UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.KICH_BAN == kichBan && x.PHIEN_BAN == "PB1" && x.TIME_YEAR == year && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var dataSL_KH = UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => headerSL_KH.Contains(x.TEMPLATE_CODE)).ToList();

                var headerDT_TH_5 = UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.KICH_BAN == kichBan && x.PHIEN_BAN == "PB5" && x.TIME_YEAR == year - 5).Select(x => x.TEMPLATE_CODE).ToList();
                var dataDT_TH_5 = UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => headerDT_TH_5.Contains(x.TEMPLATE_CODE) && (x.KHOAN_MUC_DOANH_THU_CODE == "2001" || x.KHOAN_MUC_DOANH_THU_CODE == "2002")).ToList();
                var headerDT_KH_1 = UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.KICH_BAN == kichBan && x.PHIEN_BAN == "PB1" && x.TIME_YEAR == year - 1 && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var dataDT_KH_1 = UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => headerDT_KH_1.Contains(x.TEMPLATE_CODE) && (x.KHOAN_MUC_DOANH_THU_CODE == "2001" || x.KHOAN_MUC_DOANH_THU_CODE == "2002")).ToList();
                var headerDT_KH = UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.KICH_BAN == kichBan && x.PHIEN_BAN == "PB1" && x.TIME_YEAR == year && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var dataDT_KH = UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => headerDT_KH.Contains(x.TEMPLATE_CODE) && (x.KHOAN_MUC_DOANH_THU_CODE == "2001" || x.KHOAN_MUC_DOANH_THU_CODE == "2002")).ToList();

                var headerCP_TH_5 = UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.KICH_BAN == kichBan && x.PHIEN_BAN == "PB5" && x.TIME_YEAR == year - 5).Select(x => x.TEMPLATE_CODE).ToList();
                var dataCP_TH_5 = UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => headerCP_TH_5.Contains(x.TEMPLATE_CODE)).ToList();
                var headerCP_KH_1 = UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.KICH_BAN == kichBan && x.PHIEN_BAN == "PB1" && x.TIME_YEAR == year - 1 && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var dataCP_KH_1 = UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => headerCP_KH_1.Contains(x.TEMPLATE_CODE)).ToList();
                var headerCP_KH = UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.KICH_BAN == kichBan && x.PHIEN_BAN == "PB1" && x.TIME_YEAR == year && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var dataCP_KH = UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => headerCP_KH.Contains(x.TEMPLATE_CODE)).ToList();


                data.BM02D = new List<ReportModel>(){
            new ReportModel()
            {
                Id = "I",
                Name = "I. Sản lượng",
                NameExcel = "Sản lượng",
                IsBold = true,
            },
            new ReportModel()
            {
                Id = "I.1",
                Parent = "I",
                Name = "1. Cung ứng cho VNA Group",
                NameExcel = "Cung ứng cho VNA Group",

            },
            new ReportModel()
            {
                Id = "I.1.1",
                Parent = "I.1",
                Name = "- Cung ứng cho VNA",
                Col1 = dataSL_TH_5.Where(x => x.SanLuongProfitCenter.HANG_HANG_KHONG_CODE == "VN").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                Col2 = dataSL_KH_1.Where(x => x.SanLuongProfitCenter.HANG_HANG_KHONG_CODE == "VN").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                Col3 = 0,
                Col4 = dataSL_KH.Where(x => x.SanLuongProfitCenter.HANG_HANG_KHONG_CODE == "VN").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                NameExcel = "- Cung ứng cho VNA",

            },
            new ReportModel()
            {
                Id = "I.1.2",
                Parent = "I.1",
                Name = "- Cung ứng cho các DN khác trong VNA Group",
                Col1 = dataSL_TH_5.Where(x => x.SanLuongProfitCenter.HANG_HANG_KHONG_CODE != "VN" && x.SanLuongProfitCenter.HangHangKhong.IS_VNA == true).Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                Col2 = dataSL_KH_1.Where(x => x.SanLuongProfitCenter.HANG_HANG_KHONG_CODE != "VN" && x.SanLuongProfitCenter.HangHangKhong.IS_VNA == true).Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                Col3 = 0,
                Col4 = dataSL_KH.Where(x => x.SanLuongProfitCenter.HANG_HANG_KHONG_CODE != "VN" && x.SanLuongProfitCenter.HangHangKhong.IS_VNA == true).Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                NameExcel = "- Cung ứng cho các DN khác trong VNA Group",

            },
            new ReportModel()
            {
                Id = "I.2",
                Parent = "I",
                Name = "2. Cung ứng cho đối tác khác (*)",
                Col1 = dataSL_TH_5.Where(x => x.SanLuongProfitCenter.HangHangKhong.IS_VNA == false).Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                Col2 = dataSL_KH_1.Where(x => x.SanLuongProfitCenter.HangHangKhong.IS_VNA == false).Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                Col3 = 0,
                NameExcel = "Cung ứng cho đối tác khác (*)",
                Col4 = dataSL_KH.Where(x => x.SanLuongProfitCenter.HangHangKhong.IS_VNA == false).Sum(x => x.VALUE_SUM_YEAR) ?? 0
            },
            new ReportModel()
            {
                Id = "II",
                Name = "II. Doanh thu từ hoạt động SXKD",
                NameExcel = "Doanh thu từ hoạt động SXKD",

                IsBold = true,
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "II.1",
                Parent = "II",
                Name = "1. Doanh thu cung ứng cho VNA Group",
                NameExcel = "Doanh thu cung ứng cho VNA Group",

                Unit = "Tr.đ/USD",
            },
            new ReportModel()
            {
                Id = "II.1.1",
                Parent = "II.1",
                Name = "- Doanh thu VNA",
                NameExcel = "- Doanh thu VNA",

                Unit = "Tr.đ/USD",
                Col1 = dataDT_TH_5.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == "VN").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                Col2 = dataDT_KH_1.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == "VN").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                Col3 = 0,
                Col4 = dataDT_KH.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == "VN").Sum(x => x.VALUE_SUM_YEAR) ?? 0
            },
            new ReportModel()
            {
                Id = "II.1.1.1",
                Parent = "II.1.1",
                Name = "Trong đó: CK/Giảm giá",
                NameExcel = "Trong đó: CK/Giảm giá",

                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "II.1.2",
                Parent = "II.1",
                Name = "- Doanh thu các DN khác trong VNA group",
                NameExcel = "- Doanh thu các DN khác trong VNA group",

                Unit = "Tr.đ/USD",
                Col1 = dataDT_TH_5.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE != "VN" && x.DoanhThuProfitCenter.HangHangKhong.IS_VNA == true).Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                Col2 = dataDT_KH_1.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE != "VN" && x.DoanhThuProfitCenter.HangHangKhong.IS_VNA == true).Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                Col3 = 0,
                Col4 = dataDT_KH.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE != "VN" && x.DoanhThuProfitCenter.HangHangKhong.IS_VNA == true).Sum(x => x.VALUE_SUM_YEAR) ?? 0
            },
            new ReportModel()
            {
                Id = "II.1.2.1",
                Parent = "II.1.2",
                Name = "Trong đó: CK/Giảm giá",
                NameExcel = "Trong đó: CK/Giảm giá",

                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "II.2",
                Parent = "II",
                Name = "2. Doanh thu cung ứng cho đối tác khác (*)",
                NameExcel = "Doanh thu cung ứng cho đối tác khác (*)",

                Unit = "Tr.đ/USD",
                Col1 = dataDT_TH_5.Where(x => x.DoanhThuProfitCenter.HangHangKhong.IS_VNA == false).Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                Col2 = dataDT_KH_1.Where(x => x.DoanhThuProfitCenter.HangHangKhong.IS_VNA == false).Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                Col3 = 0,
                Col4 = dataDT_KH.Where(x => x.DoanhThuProfitCenter.HangHangKhong.IS_VNA == false).Sum(x => x.VALUE_SUM_YEAR) ?? 0
            },
            new ReportModel()
            {
                Id = "II.2.1",
                Parent = "II.2",
                Name = "Trong đó: CK/Giảm giá",
                NameExcel = "Trong đó: CK/Giảm giá",

                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III",
                Name = "III. Các khoản chi phí",
                NameExcel = "Các khoản chi phí",

                Unit = "Tr.đ/USD",
                IsBold = true
            },
            new ReportModel()
            {
                Id = "III.1",
                Parent = "III",
                NameExcel = "Chi phí dịch vụ mua ngoài",

                Name = "1. Chi phí dịch vụ mua ngoài",
                Unit = "Tr.đ/USD",
            },
            new ReportModel()
            {
                Id = "III.1.1",
                Parent = "III.1",
                Name = "1.1. Chi phí bảo hiểm tài sản",
                NameExcel = "Chi phí bảo hiểm tài sản",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G001")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G001")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G001")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.1.2",
                Parent = "III.1",
                Name = "1.2. Thuê sửa chữa nhà cửa VKT",
                NameExcel = "Thuê sửa chữa nhà cửa VKT",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G002")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G002")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G002")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.1.3",
                Parent = "III.1",
                Name = "1.3. Thuê sửa chữa máy móc thiết bị",
                NameExcel = "Thuê sửa chữa máy móc thiết bị",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G003")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G003")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G003")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.1.4",
                Parent = "III.1",
                Name = "1.4. Thuê sửa chữa PTVT",
                NameExcel = "Thuê sửa chữa PTVT",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G004")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G004")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G004")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.1.5",
                Parent = "III.1",
                Name = "1.5. Thuê sửa chữa thiết bị quản lý",
                NameExcel = "Thuê sửa chữa thiết bị quản lý",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G005")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G005")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G005")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.1.6",
                Parent = "III.1",
                Name = "1.6. Thuê sửa chữa kho bể",
                NameExcel = "Thuê sửa chữa kho bể",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G006")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G006")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G006")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.1.7",
                Parent = "III.1",
                Name = "1.7. Thuê sửa chữa TSCĐ khác",
                NameExcel = "Thuê sửa chữa TSCĐ khác",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G007")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G007")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G007")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.1.8",
                Parent = "III.1",
                Name = "1.8. Thuê cửa hàng kho bãi",
                NameExcel = "Thuê cửa hàng kho bãi",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G008")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G008")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G008")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.1.9",
                Parent = "III.1",
                Name = "1.9. Thuê vận chuyển",
                NameExcel = "Thuê vận chuyển",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G009")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G009")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G009")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.1.10",
                Parent = "III.1",
                Name = "1.10. Tiền điện mua ngoài",
                NameExcel = "Tiền điện mua ngoài",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G010")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G010")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G010")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.1.11",
                Parent = "III.1",
                Name = "1.11. Tiền nước mua ngoài",
                NameExcel = "Tiền nước mua ngoài",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G011")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G011")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G011")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.1.12",
                Parent = "III.1",
                Name = "1.12. Cước thông tin liên lạc",
                NameExcel = "Cước thông tin liên lạc",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G012")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G012")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G012")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.1.13",
                Parent = "III.1",
                Name = "1.13. Chi phí dịch vụ mua ngoài khác",
                NameExcel = "Chi phí dịch vụ mua ngoài khác",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G019")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G019")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G019")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.2",
                Parent = "III",
                Name = "2. Chi khác bằng tiền",
                NameExcel = "Chi khác bằng tiền",

                Unit = "Tr.đ/USD",
            },
            new ReportModel()
            {
                Id = "III.2.1",
                Parent = "III.2",
                Name = "2.1. Chi ANAT, PCBT, PCCC",
                NameExcel = "Chi ANAT, PCBT, PCCC",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H001")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H001")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H001")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.2.2",
                Parent = "III.2",
                Name = "2.2. Chi phí trang phục ngành",
                NameExcel = "Chi phí trang phục ngành",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H002")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H002")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H002")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.2.3",
                Parent = "III.2",
                Name = "2.3. Chi giao dịch tiếp khách",
                NameExcel = "Chi giao dịch tiếp khách",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H004")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H004")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H004")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.2.4",
                Parent = "III.2",
                Name = "2.4. Chi quảng cáo, marketing",
                NameExcel = "Chi quảng cáo, marketing",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H014")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H014")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H014")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.2.5",
                Parent = "III.2",
                Name = "2.5. Chi hoa hồng môi giới",
                NameExcel = "Chi hoa hồng môi giới",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H005")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H005")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H005")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.2.6",
                Parent = "III.2",
                Name = "2.6. Chi đào tạo",
                NameExcel = "Chi đào tạo",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H006")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H006")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H006")).Sum(x => x.AMOUNT) ?? 0,
            },
            
            new ReportModel()
            {
                Id = "II.2.7",
                Parent = "III.2",
                Name = "2.7. Công tác phí, phép",
                NameExcel = "Công tác phí, phép",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H007")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H007")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H007")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.2.8",
                Parent = "III.2",
                Name = "2.8. Lệ phí cầu đường",
                NameExcel = "Lệ phí cầu đường",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H003")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H003")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H003")).Sum(x => x.AMOUNT) ?? 0,
            },
            
            new ReportModel()
            {
                Id = "III.2.9",
                Parent = "III.2",
                Name = "2.9. Chi bồi dưỡng độc hại",
                NameExcel = "Chi bồi dưỡng độc hại",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H008")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H008")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H008")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.2.10",
                Parent = "III.2",
                Name = "2.10. Phí nhượng quyền khai thác",
                NameExcel = "Phí nhượng quyền khai thác",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H009")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H009")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H009")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.2.11",
                Parent = "III.2",
                Name = "2.11. Chi VSCN, y tế, môi trường",
                NameExcel = "Chi VSCN, y tế, môi trường",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H010")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H010")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H010")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.2.12",
                Parent = "III.2",
                Name = "2.12. Phí ngân hàng",
                NameExcel = "Phí ngân hàng",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H015")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H015")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H015")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.2.13",
                Parent = "III.2",
                Name = "2.13. Khoản chi có tính chất phúc lợi",
                NameExcel = "Khoản chi có tính chất phúc lợi",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H018")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H018")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H018")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.2.14",
                Parent = "III.2",
                Name = "1.14. Chi bằng tiền khác",
                NameExcel = "Chi bằng tiền khác",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H019")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H019")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H019")).Sum(x => x.AMOUNT) ?? 0,
            },
            };

                foreach (var i in data.BM02D.OrderByDescending(x => x.Id))
                {
                    var child = data.BM02D.Where(x => x.Parent == i.Id).ToList();
                    i.Col1 = child.Count() == 0 ? i.Col1 : child.Count() != 0 && child.Sum(x => x.Col1) == 0 ? i.Col1 : child.Sum(x => x.Col1);
                    i.Col2 = child.Count() == 0 ? i.Col2 : child.Count() != 0 && child.Sum(x => x.Col2) == 0 ? i.Col2 : child.Sum(x => x.Col2);
                    i.Col3 = child.Count() == 0 ? i.Col3 : child.Count() != 0 && child.Sum(x => x.Col3) == 0 ? i.Col3 : child.Sum(x => x.Col3);
                    i.Col4 = child.Count() == 0 ? i.Col4 : child.Count() != 0 && child.Sum(x => x.Col4) == 0 ? i.Col4 : child.Sum(x => x.Col4);
                }
                return data;
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                return new ReportDataCenter();
            }
        }

        public List<ReportModel> GenDataBM2107(int year, int month, string phienBan, string kichBan)
        {
            try
            {
                var data = new List<ReportModel>();
                var header = UnitOfWork.Repository<DauTuXayDungRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var details = UnitOfWork.Repository<DauTuXayDungDataRepo>().Queryable().Where(x => header.Contains(x.TEMPLATE_CODE)).ToList();
                var projects = UnitOfWork.Repository<ProjectRepo>().Queryable().Where(x => x.LOAI_HINH == "XDCB" && x.YEAR > 0).ToList();
                var order = 1;
                data.Add(new ReportModel
                {
                    Id = "A",
                    Name = "Tổng kinh phí đầu tư XDCB",
                    IsBold = true
                });
                foreach(var project in projects)
                {
                    data.Add(new ReportModel
                    {
                        Id = $"A.{order}",
                        Parent = "A",
                        Name = $"{order}. {project.NAME}",
                    });
                    data.Add(new ReportModel
                    {
                        Id = $"A.{order}.a",
                        Parent = $"A.{order}",
                        Name = "a. Chuẩn bị đầu tư",
                    });
                    data.Add(new ReportModel
                    {
                        Id = $"A.{order}.b",
                        Parent = $"A.{order}",
                        Name = "b. Thực hiện đầu tư",
                    });

                    order++;
                }
                return data;

            }catch(Exception ex)
            {
                UnitOfWork.Rollback();
                return new List<ReportModel>();
            }
        }

        public ReportDataCenter GenDataBM02D1(int year, string phienBan)
        {
            try
            {
                var data = new ReportDataCenter();


                var headerSL_C = UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.PHIEN_BAN == phienBan && x.KICH_BAN == "C" && x.TIME_YEAR == year && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var dataSL_C = UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => headerSL_C.Contains(x.TEMPLATE_CODE)).ToList();
                var headerSL_TB = UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.PHIEN_BAN == phienBan && x.KICH_BAN == "TB" && x.TIME_YEAR == year && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var dataSL_TB = UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => headerSL_TB.Contains(x.TEMPLATE_CODE)).ToList();
                var headerSL_T = UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.PHIEN_BAN == phienBan && x.KICH_BAN == "T" && x.TIME_YEAR == year && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var dataSL_T = UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => headerSL_T.Contains(x.TEMPLATE_CODE)).ToList();

                var headerDT_C = UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.PHIEN_BAN == phienBan && x.KICH_BAN == "C" && x.TIME_YEAR == year && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var dataDT_C = UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => headerDT_C.Contains(x.TEMPLATE_CODE) && (x.KHOAN_MUC_DOANH_THU_CODE == "2001" || x.KHOAN_MUC_DOANH_THU_CODE == "2002")).ToList();
                var headerDT_TB = UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.PHIEN_BAN == phienBan && x.KICH_BAN == "TB" && x.TIME_YEAR == year && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var dataDT_TB = UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => headerDT_TB.Contains(x.TEMPLATE_CODE) && (x.KHOAN_MUC_DOANH_THU_CODE == "2001" || x.KHOAN_MUC_DOANH_THU_CODE == "2002")).ToList();
                var headerDT_T = UnitOfWork.Repository<KeHoachDoanhThuRepo>().Queryable().Where(x => x.PHIEN_BAN == phienBan && x.KICH_BAN == "T" && x.TIME_YEAR == year && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var dataDT_T = UnitOfWork.Repository<KeHoachDoanhThuDataRepo>().Queryable().Where(x => headerDT_T.Contains(x.TEMPLATE_CODE) && (x.KHOAN_MUC_DOANH_THU_CODE == "2001" || x.KHOAN_MUC_DOANH_THU_CODE == "2002")).ToList();


                var headerCP_C = UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.PHIEN_BAN == phienBan && x.KICH_BAN == "C" && x.TIME_YEAR == year && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var dataCP_C = UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => headerCP_C.Contains(x.TEMPLATE_CODE)).ToList();
                var headerCP_TB = UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.PHIEN_BAN == phienBan && x.KICH_BAN == "TB" && x.TIME_YEAR == year && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var dataCP_TB = UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => headerCP_TB.Contains(x.TEMPLATE_CODE)).ToList();
                var headerCP_T = UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.PHIEN_BAN == phienBan && x.KICH_BAN == "T" && x.TIME_YEAR == year && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var dataCP_T = UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => headerCP_T.Contains(x.TEMPLATE_CODE)).ToList();
                

                data.BM02D = new List<ReportModel>(){
            new ReportModel()
            {
                Id = "I",
                Name = "I. Sản lượng",
                NameExcel = "Sản lượng",
                IsBold = true,
            },
            new ReportModel()
            {
                Id = "I.1",
                Parent = "I",
                Name = "1. Cung ứng cho VNA Group",
                NameExcel = "Cung ứng cho VNA Group",

            },
            new ReportModel()
            {
                Id = "I.1.1",
                Parent = "I.1",
                Name = "- Cung ứng cho VNA",
                NameExcel = "- Cung ứng cho VNA",

                Col1 = dataSL_C.Where(x => x.SanLuongProfitCenter.HANG_HANG_KHONG_CODE == "VN").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                Col2 = dataSL_TB.Where(x => x.SanLuongProfitCenter.HANG_HANG_KHONG_CODE == "VN").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                Col3 = 0,
                Col4 = dataSL_T.Where(x => x.SanLuongProfitCenter.HANG_HANG_KHONG_CODE == "VN").Sum(x => x.VALUE_SUM_YEAR) ?? 0
            },
            new ReportModel()
            {
                Id = "I.1.2",
                Parent = "I.1",
                Name = "- Cung ứng cho các DN khác trong VNA Group",
                NameExcel = "- Cung ứng cho các DN khác trong VNA Group",

                Col1 = dataSL_C.Where(x => x.SanLuongProfitCenter.HANG_HANG_KHONG_CODE != "VN" && x.SanLuongProfitCenter.HangHangKhong.IS_VNA == true).Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                Col2 = dataSL_TB.Where(x => x.SanLuongProfitCenter.HANG_HANG_KHONG_CODE != "VN" && x.SanLuongProfitCenter.HangHangKhong.IS_VNA == true).Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                Col3 = 0,
                Col4 = dataSL_T.Where(x => x.SanLuongProfitCenter.HANG_HANG_KHONG_CODE != "VN" && x.SanLuongProfitCenter.HangHangKhong.IS_VNA == true).Sum(x => x.VALUE_SUM_YEAR) ?? 0
            },
            new ReportModel()
            {
                Id = "I.2",
                Parent = "I",
                Name = "2. Cung ứng cho đối tác khác (*)",
                NameExcel = "Cung ứng cho đối tác khác (*)",

                Col1 = dataSL_C.Where(x => x.SanLuongProfitCenter.HangHangKhong.IS_VNA == false).Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                Col2 = dataSL_TB.Where(x => x.SanLuongProfitCenter.HangHangKhong.IS_VNA == false).Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                Col3 = 0,
                Col4 = dataSL_T.Where(x => x.SanLuongProfitCenter.HangHangKhong.IS_VNA == false).Sum(x => x.VALUE_SUM_YEAR) ?? 0
            },
            new ReportModel()
            {
                Id = "II",
                Name = "II. Doanh thu từ hoạt động SXKD",
                NameExcel = "Doanh thu từ hoạt động SXKD",

                IsBold = true,
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "II.1",
                Parent = "II",
                Name = "1. Doanh thu cung ứng cho VNA Group",
                NameExcel = "Doanh thu cung ứng cho VNA Group",

                Unit = "Tr.đ/USD",
            },
            new ReportModel()
            {
                Id = "II.1.1",
                Parent = "II.1",
                Name = "- Doanh thu VNA",
                NameExcel = "- Doanh thu VNA",

                Unit = "Tr.đ/USD",
                Col1 = dataDT_C.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == "VN").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                Col2 = dataDT_TB.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == "VN").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                Col3 = 0,
                Col4 = dataDT_T.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE == "VN").Sum(x => x.VALUE_SUM_YEAR) ?? 0
            },
            new ReportModel()
            {
                Id = "II.1.1.1",
                Parent = "II.1.1",
                Name = "Trong đó: CK/Giảm giá",
                NameExcel = "Trong đó: CK/Giảm giá",

                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "II.1.2",
                Parent = "II.1",
                Name = "- Doanh thu các DN khác trong VNA group",
                NameExcel = "- Doanh thu các DN khác trong VNA group",

                Unit = "Tr.đ/USD",
                Col1 = dataDT_C.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE != "VN" && x.DoanhThuProfitCenter.HangHangKhong.IS_VNA == true).Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                Col2 = dataDT_TB.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE != "VN" && x.DoanhThuProfitCenter.HangHangKhong.IS_VNA == true).Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                Col3 = 0,
                Col4 = dataDT_T.Where(x => x.DoanhThuProfitCenter.HANG_HANG_KHONG_CODE != "VN" && x.DoanhThuProfitCenter.HangHangKhong.IS_VNA == true).Sum(x => x.VALUE_SUM_YEAR) ?? 0
            },
            new ReportModel()
            {
                Id = "II.1.2.1",
                Parent = "II.1.2",
                Name = "Trong đó: CK/Giảm giá",
                NameExcel = "Trong đó: CK/Giảm giá",

                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "II.2",
                Parent = "II",
                Name = "2. Doanh thu cung ứng cho đối tác khác (*)",
                NameExcel = "Doanh thu cung ứng cho đối tác khác (*)",

                Unit = "Tr.đ/USD",
                Col1 = dataDT_C.Where(x => x.DoanhThuProfitCenter.HangHangKhong.IS_VNA == false).Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                Col2 = dataDT_TB.Where(x => x.DoanhThuProfitCenter.HangHangKhong.IS_VNA == false).Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                Col3 = 0,
                Col4 = dataDT_T.Where(x => x.DoanhThuProfitCenter.HangHangKhong.IS_VNA == false).Sum(x => x.VALUE_SUM_YEAR) ?? 0
            },
            new ReportModel()
            {
                Id = "II.2.1",
                Parent = "II.2",
                Name = "Trong đó: CK/Giảm giá",
                NameExcel = "Trong đó: CK/Giảm giá",

                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III",
                Name = "III. Các khoản chi phí",
                NameExcel = "Các khoản chi phí",

                Unit = "Tr.đ/USD",
                IsBold = true
            },
            new ReportModel()
            {
                Id = "III.1",
                Parent = "III",
                Name = "1. Chi phí dịch vụ mua ngoài",
                NameExcel = "Chi phí dịch vụ mua ngoài",

                Unit = "Tr.đ/USD",
            },
            new ReportModel()
            {
                Id = "III.1.1",
                Parent = "III.1",
                Name = "1.1. Chi phí bảo hiểm tài sản",
                NameExcel = "Chi phí bảo hiểm tài sản",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G001")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G001")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G001")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.1.2",
                Parent = "III.1",
                Name = "1.2. Thuê sửa chữa nhà cửa VKT",
                NameExcel = "Thuê sửa chữa nhà cửa VKT",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G002")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G002")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G002")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.1.3",
                Parent = "III.1",
                Name = "1.3. Thuê sửa chữa máy móc thiết bị",
                NameExcel = "Thuê sửa chữa máy móc thiết bị",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G003")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G003")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G003")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.1.4",
                Parent = "III.1",
                Name = "1.4. Thuê sửa chữa PTVT",
                NameExcel = "Thuê sửa chữa PTVT",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G004")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G004")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G004")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.1.5",
                Parent = "III.1",
                Name = "1.5. Thuê sửa chữa thiết bị quản lý",
                NameExcel = "Thuê sửa chữa thiết bị quản lý",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G005")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G005")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G005")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.1.6",
                Parent = "III.1",
                Name = "1.6. Thuê sửa chữa kho bể",
                NameExcel = "Thuê sửa chữa kho bể",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G006")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G006")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G006")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.1.7",
                Parent = "III.1",
                Name = "1.7. Thuê sửa chữa TSCĐ khác",
                NameExcel = "Thuê sửa chữa TSCĐ khác",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G007")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G007")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G007")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.1.8",
                Parent = "III.1",
                Name = "1.8. Thuê cửa hàng kho bãi",
                NameExcel = "Thuê cửa hàng kho bãi",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G008")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G008")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G008")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.1.9",
                Parent = "III.1",
                Name = "1.9. Thuê vận chuyển",
                NameExcel = "Thuê vận chuyển",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G009")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G009")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G009")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.1.10",
                Parent = "III.1",
                Name = "1.10. Tiền điện mua ngoài",
                NameExcel = "Tiền điện mua ngoài",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G010")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G010")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G010")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.1.11",
                Parent = "III.1",
                Name = "1.11. Tiền nước mua ngoài",
                NameExcel = "Tiền nước mua ngoài",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G011")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G011")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G011")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.1.12",
                Parent = "III.1",
                Name = "1.12. Cước thông tin liên lạc",
                NameExcel = "Cước thông tin liên lạc",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G012")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G012")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G012")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.1.13",
                Parent = "III.1",
                Name = "1.13. Chi phí dịch vụ mua ngoài khác",
                NameExcel = "Chi phí dịch vụ mua ngoài khác",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G019")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G019")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G019")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.2",
                Parent = "III",
                Name = "2. Chi khác bằng tiền",
                NameExcel = "Chi khác bằng tiền",

                Unit = "Tr.đ/USD",
            },
            new ReportModel()
            {
                Id = "III.2.1",
                Parent = "III.2",
                Name = "2.1. Chi ANAT, PCBT, PCCC",
                NameExcel = "Chi ANAT, PCBT, PCCC",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H001")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H001")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H001")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.2.2",
                Parent = "III.2",
                Name = "2.2. Chi phí trang phục ngành",
                NameExcel = "Chi phí trang phục ngành",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H002")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H002")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H002")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.2.3",
                Parent = "III.2",
                Name = "2.3. Chi giao dịch tiếp khách",
                NameExcel = "Chi giao dịch tiếp khách",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H004")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H004")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H004")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.2.4",
                Parent = "III.2",
                Name = "2.4. Chi quảng cáo, marketing",
                NameExcel = "Chi quảng cáo, marketing",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H014")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H014")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H014")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.2.5",
                Parent = "III.2",
                Name = "2.5. Chi hoa hồng môi giới",
                NameExcel = "Chi hoa hồng môi giới",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H005")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H005")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H005")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.2.6",
                Parent = "III.2",
                Name = "2.6. Chi đào tạo",
                NameExcel = "Chi đào tạo",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H006")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H006")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H006")).Sum(x => x.AMOUNT) ?? 0,
            },

            new ReportModel()
            {
                Id = "II.2.7",
                Parent = "III.2",
                Name = "2.7. Công tác phí, phép",
                NameExcel = "Công tác phí, phép",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H007")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H007")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H007")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.2.8",
                Parent = "III.2",
                Name = "2.8. Lệ phí cầu đường",
                NameExcel = "Lệ phí cầu đường",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H003")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H003")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H003")).Sum(x => x.AMOUNT) ?? 0,
            },

            new ReportModel()
            {
                Id = "III.2.9",
                Parent = "III.2",
                Name = "2.9. Chi bồi dưỡng độc hại",
                NameExcel = "Chi bồi dưỡng độc hại",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H008")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H008")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H008")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.2.10",
                Parent = "III.2",
                Name = "2.10. Phí nhượng quyền khai thác",
                NameExcel = "Phí nhượng quyền khai thác",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H009")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H009")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H009")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.2.11",
                Parent = "III.2",
                Name = "2.11. Chi VSCN, y tế, môi trường",
                NameExcel = "Chi VSCN, y tế, môi trường",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H010")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H010")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H010")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.2.12",
                Parent = "III.2",
                Name = "2.12. Phí ngân hàng",
                NameExcel = "Phí ngân hàng",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H015")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H015")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H015")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.2.13",
                Parent = "III.2",
                Name = "2.13. Khoản chi có tính chất phúc lợi",
                NameExcel = "Khoản chi có tính chất phúc lợi",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H018")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H018")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H018")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Id = "III.2.14",
                Parent = "III.2",
                Name = "1.14. Chi bằng tiền khác",
                NameExcel = "Chi bằng tiền khác",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H019")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H019")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H019")).Sum(x => x.AMOUNT) ?? 0,
            },
            };

                foreach (var i in data.BM02D.OrderByDescending(x => x.Id))
                {
                    var child = data.BM02D.Where(x => x.Parent == i.Id).ToList();
                    i.Col1 = child.Count() == 0 ? i.Col1 : child.Count() != 0 && child.Sum(x => x.Col1) == 0 ? i.Col1 : child.Sum(x => x.Col1);
                    i.Col2 = child.Count() == 0 ? i.Col2 : child.Count() != 0 && child.Sum(x => x.Col2) == 0 ? i.Col2 : child.Sum(x => x.Col2);
                    i.Col3 = child.Count() == 0 ? i.Col3 : child.Count() != 0 && child.Sum(x => x.Col3) == 0 ? i.Col3 : child.Sum(x => x.Col3);
                    i.Col4 = child.Count() == 0 ? i.Col4 : child.Count() != 0 && child.Sum(x => x.Col4) == 0 ? i.Col4 : child.Sum(x => x.Col4);
                }
                return data;
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                return new ReportDataCenter();
            }
        }
        public static List<ReportModel> ElementDataReport = new List<ReportModel>(){
            new ReportModel()
            {
                Id = "I",
                Name = "I. Sản lượng",
                IsBold = true,
            },
            new ReportModel()
            {
                Id = "I.1",
                Parent = "I",
                Name = "1. Cung ứng cho VNA Group",
            },
            new ReportModel()
            {
                Id = "I.1.1",
                Parent = "I.1",
                Name = "- Cung ứng cho VNA",
            },
            new ReportModel()
            {
                Id = "I.1.2",
                Parent = "I.1",
                Name = "- Cung ứng cho các DN khác trong VNA Group",
            },
            new ReportModel()
            {
                Id = "I.2",
                Parent = "I",
                Name = "2. Cung ứng cho đối tác khác (*)",
            },
            new ReportModel()
            {
                Id = "II",
                Name = "II. Doanh thu từ hoạt động SXKD",
                IsBold = true,
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "II.1",
                Parent = "II",
                Name = "1. Doanh thu cung ứng cho VNA Group",
                Unit = "Tr.đ/USD",
            },
            new ReportModel()
            {
                Id = "II.1.1",
                Parent = "II.1",
                Name = "- Doanh thu VNA",
                Unit = "Tr.đ/USD",
            },
            new ReportModel()
            {
                Id = "II.1.1.1",
                Parent = "II.1.1",
                Name = "Trong đó: CK/Giảm giá",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "II.1.2",
                Parent = "II.1",
                Name = "- Doanh thu các DN khác trong VNA group",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "II.1.2.1",
                Parent = "II.1.2",
                Name = "Trong đó: CK/Giảm giá",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "II.2",
                Parent = "II",
                Name = "2. Doanh thu cung ứng cho đối tác khác (*)",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "II.2.1",
                Parent = "II.2",
                Name = "Trong đó: CK/Giảm giá",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III",
                Name = "III. Các khoản chi phí",
                Unit = "Tr.đ/USD",
                IsBold = true
            },
            new ReportModel()
            {
                Id = "III.1",
                Parent = "III",
                Name = "1. Chi phí dịch vụ mua ngoài",
                Unit = "Tr.đ/USD",
            },
            new ReportModel()
            {
                Id = "III.1.1",
                Parent = "III.1",
                Name = "1.1. Thuê phương tiện vận tải, trang thiết bị",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.1.2",
                Parent = "III.1",
                Name = "1.2. Chi phí thuê văn phòng, mặt bằng",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.1.3",
                Parent = "III.1",
                Name = "1.3. Chi phí thông tin liên lạc",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.1.4",
                Parent = "III.1",
                Name = "1.4. Chi phí quảng cáo, tiếp thị",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.1.5",
                Parent = "III.1",
                Name = "1.5. Điện nước",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.1.6",
                Parent = "III.1",
                Name = "1.6. Chi phí mua bảo hiểm bắt buộc (BH tài sản, tai nạn lao động, cháy nổ…)",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.1.7",
                Parent = "III.1",
                Name = "1.7. Chi phí dịch vụ tư vấn, kiểm toán",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.1.8",
                Parent = "III.1",
                Name = "1.8. Hoa hồng, môi giới, đại lý",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.1.9",
                Parent = "III.1",
                Name = "1.9. Chi phí dịch vụ mua ngoài khác",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2",
                Parent = "III",
                Name = "2. Chi khác bằng tiền",
                Unit = "Tr.đ/USD",
            },
            new ReportModel()
            {
                Id = "III.2.1",
                Parent = "III.2",
                Name = "2.1. Chi đồng phục",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2.2",
                Parent = "III.2",
                Name = "2.2. Chi bồi dưỡng độc hại",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2.3",
                Parent = "III.2",
                Name = "2.3. Bảo hộ lao động",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2.4",
                Parent = "III.2",
                Name = "2.4. Văn phòng phẩm, in ấn",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2.5",
                Parent = "III.2",
                Name = "2.5. Tiếp khách, hội nghị, xúc tiến thương mại",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2.6",
                Parent = "III.2",
                Name = "2.6. Công tác phí",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2.6.1",
                Parent = "III.2.6",
                Name = "- Trong nước",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2.6.2",
                Parent = "III.2.6",
                Name = "- Ngoài nước",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "II.2.7",
                Parent = "III.2",
                Name = "2.7. Chi phí y tế",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2.8",
                Parent = "III.2",
                Name = "2.8. Chi đào tạo",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2.8.1",
                Parent = "III.2.8",
                Name = "- Trong nước",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2.8.2",
                Parent = "III.2.8",
                Name = "- Ngoài nước",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2.9",
                Parent = "III.2",
                Name = "2.9. Chi phòng cháy chữa cháy, phòng chống bão lụt, trực tự vệ",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2.10",
                Parent = "III.2",
                Name = "2.10. Chi vệ sinh văn phòng, diệt côn trùng, cây cảnh",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2.11",
                Parent = "III.2",
                Name = "2.11. Chi phí môi trường",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2.12",
                Parent = "III.2",
                Name = "2.12. Chi có tính chất phúc lợi: hiếu hỉ, nghỉ mát, thăm hỏi…",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2.13",
                Parent = "III.2",
                Name = "2.13. Chi bảo hiểm hưu trí tự nguyện",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2.14",
                Parent = "III.2",
                Name = "2.14. Thủ tục phí ngân hàng",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2.15",
                Parent = "III.2",
                Name = "2.15. Các khoản chi khác",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2.15.1",
                Parent = "III.2.15",
                Name = "- Mua tài liệu",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2.15.2",
                Parent = "III.2.15",
                Name = "- Kiểm định xe ô tô / thiết bị",
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Id = "III.2.15.3",
                Parent = "III.2.15",
                Name = "- Chi khác ....",
                Unit = "Tr.đ/USD"
            },
        };
    }

    public class ReportDataCenter
    {
        public List<ReportModel> BM01D { get; set; } = new List<ReportModel>();
        public List<ReportModel> BM02A { get; set; } = new List<ReportModel>();
        public List<ReportModel> BM02C { get; set; } = new List<ReportModel>();
        public List<ReportModel> BM02D { get; set; } = new List<ReportModel>();
        public List<ReportModel> BM02D1 { get; set; } = new List<ReportModel>();
    }

    public class ReportModel
    {
        public string ElementCode { get; set; }
        public string Unit { get; set; }
        public string Stt { get; set; }
        public string Id { get; set; }
        public string Parent { get; set; }
        public string Name { get; set; }
        public string NameExcel { get; set; }

        public int Order { get; set; }
        public bool IsBold { get; set; }
        public decimal? Col1 { get; set; }
        public decimal? Col2 { get; set; }
        public decimal? Col3 { get; set; }
        public decimal? Col4 { get; set; }
        public decimal? Col5 { get; set; }
        public decimal? Col6 { get; set; }
        public decimal? Col7 { get; set; }
        public decimal? Col8 { get; set; }
        public decimal? Col9 { get; set; }
        public decimal? Col10 { get; set; }
        public decimal? Col11 { get; set; }
        public decimal? Col12 { get; set; }
        public decimal? Col13 { get; set; }
        public decimal? Col14 { get; set; }
        public decimal? Col15 { get; set; }
        public decimal? Col16 { get; set; }
        public decimal? Col17 { get; set; }
        public decimal? Col18 { get; set; }
        public decimal? Col19 { get; set; }
        public decimal? Col20 { get; set; }
        public string Tdth { get; set; }
        public string Tdtk { get; set; }
        public string Des { get; set; }
    }
}