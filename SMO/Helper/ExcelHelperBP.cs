using NHibernate.Mapping;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using NPOI.XWPF.UserModel;
using SMO.AppCode.Class;
using SMO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SMO.Helper
{
    public static class ExcelHelperBP
    {
        public static void InsertBodyTable(ref IWorkbook workbook,
            ref ISheet sheet,
            IList<IList<ExcelCellMeta>> metaTBody,
            int NUM_CELL,
            bool ignoreFirstColumn)
        {
            ReportUtilities.CreateRow(ref sheet, 0, NUM_CELL);

            ICellStyle styleCellDetail = workbook.CreateCellStyle();
            styleCellDetail.CloneStyleFrom(sheet.GetRow(8).Cells[0].CellStyle);
            styleCellDetail.WrapText = true;

            ICellStyle styleCellNumber = workbook.CreateCellStyle();
            styleCellNumber.CloneStyleFrom(styleCellDetail);
            styleCellNumber.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.00");

            ICellStyle styleCellBold = workbook.CreateCellStyle();
            styleCellBold.CloneStyleFrom(sheet.GetRow(7).Cells[0].CellStyle);
            styleCellBold.WrapText = true;
            var fontBold = workbook.CreateFont();
            fontBold.IsBold = true;
            fontBold.FontHeightInPoints = 11;
            fontBold.FontName = "Times New Roman";

            ICellStyle styleCellLastDetail = workbook.CreateCellStyle();
            styleCellLastDetail.CloneStyleFrom(sheet.GetRow(9).Cells[0].CellStyle);
            styleCellLastDetail.WrapText = true;
            styleCellLastDetail.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.00");

            var numRowCur = 8;
            foreach (var row in metaTBody)
            {
                var tempIgnoreColumn = ignoreFirstColumn;
                var columns = 0;
                ReportUtilities.CopyRow(ref sheet, 9, numRowCur);
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, numRowCur, NUM_CELL);
                foreach (var cell in row)
                {
                    if (tempIgnoreColumn)
                    {
                        tempIgnoreColumn = false;
                        continue;
                    }
                    rowCur.Height = -1;

                    if (columns < NUM_CELL - 16 || columns == NUM_CELL - 1)
                    {
                        rowCur.Cells[columns].CellStyle = styleCellDetail;
                        var lstTexts = Regex.Split(cell.Content, "\t+");
                        if (lstTexts.Length > 2)
                        {
                            // display many line
                            var level = Regex.Matches(cell.Content, "\t").Count / lstTexts.Length;
                            var spacesLevel = string.Empty;
                            for (int i = 0; i <= level; i++)
                            {
                                spacesLevel += "    ";
                            }
                            var cellText = string.Empty;
                            for (int i = 0; i < lstTexts.Length; i++)
                            {
                                if (!string.IsNullOrEmpty(lstTexts[i]))
                                {
                                    cellText += spacesLevel + lstTexts[i] + (i < lstTexts.Length - 1 ? Environment.NewLine : string.Empty);
                                }
                            }

                            rowCur.Cells[columns].SetCellValue(cellText);
                        }
                        else
                        {
                            rowCur.Cells[columns].SetCellValue(cell.Content.Replace("\t", "    "));
                        }
                    }
                    else
                    {
                        var lstTexts = Regex.Split(cell.Content, "\t+");
                        if (lstTexts.Length > 2 || !double.TryParse(lstTexts.Last().Replace(".", "").Replace(",", "."), out _))
                        {
                            rowCur.Cells[columns].CellStyle = styleCellDetail;
                            // display many line
                            var cellText = string.Empty;
                            for (int i = 0; i < lstTexts.Length; i++)
                            {
                                if (!string.IsNullOrEmpty(lstTexts[i]))
                                {
                                    cellText += lstTexts[i] + (i < lstTexts.Length - 1 ? Environment.NewLine : string.Empty);
                                }
                            }

                            rowCur.Cells[columns].SetCellValue(cellText);
                        }
                        else
                        {
                            rowCur.Cells[columns].SetCellValue(UtilsCore.StringToDouble(cell.Content.Trim().Replace(".", "").Replace(",", ".")));
                            rowCur.Cells[columns].CellStyle = styleCellNumber;
                        }
                    }
                    columns++;
                }
                numRowCur++;
            }

            //Xóa dòng thừa cuối cùng khi tạo các dòng cho detail
            IRow rowLastDetail = ReportUtilities.CreateRow(ref sheet, numRowCur, NUM_CELL);
            ReportUtilities.DeleteRow(ref sheet, rowLastDetail);
            var rowcur = sheet.GetRow(numRowCur - 1);
            for(int i = 0; i < NUM_CELL; i++)
            {
                rowcur.Cells[i].CellStyle = styleCellLastDetail;
            }

            rowLastDetail = ReportUtilities.CreateRow(ref sheet, numRowCur, NUM_CELL);
            ReportUtilities.DeleteRow(ref sheet, rowLastDetail);

            // set size for culumn
            for (int i = 1; i < NUM_CELL; i++)
            {
                sheet.AutoSizeColumn(i);
            }
        }


        public static void InsertBodyTableByYear(ref IWorkbook workbook,
            ref ISheet sheet,
            IList<IList<ExcelCellMeta>> metaTBody,
            int NUM_CELL,
            string module,
            bool ignoreFirstColumn
            )
        {

            ICellStyle styleCellNumber = workbook.CreateCellStyle();
            styleCellNumber.CloneStyleFrom(sheet.GetRow(9).Cells[0].CellStyle);
            styleCellNumber.WrapText = true;
            ICellStyle styleCellFirst = workbook.CreateCellStyle();
            styleCellFirst.CloneStyleFrom(sheet.GetRow(10).Cells[0].CellStyle);
            styleCellFirst.WrapText = true;
            var startRow = 9;
            if(module.Trim() == "KeHoachChiPhi" || module.Trim() == "DauTuTrangThietBi" || module.Trim() == "KeHoachVanChuyen")
            {
                startRow = 10;
            }
            if(module.Trim() == "DauTuNgoaiDoanhNghiep")
            {
                startRow = 8;
            }
            for (int i = 0; i < metaTBody.Count(); i++)
            {
                if (module.Trim() == "KeHoachSanLuong")
                {
                    IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, NUM_CELL);
                    for (int j = 0; j< NUM_CELL; j++)
                    {
                        if(i == metaTBody.Count()-1)
                        {
                            rowCur.Cells[0].SetCellValue("");
                            rowCur.Cells[1].SetCellValue(metaTBody[i][0]?.Content?.ToString());
                            rowCur.Cells[2].SetCellValue(UtilsCore.StringToDouble(metaTBody[i][1]?.Content.Trim().Replace(".", "").Replace(",", ".")));
                            rowCur.Cells[3].SetCellValue(UtilsCore.StringToDouble(metaTBody[i][2]?.Content.Trim().Replace(".", "").Replace(",", ".")));
                            rowCur.Cells[4].SetCellValue(UtilsCore.StringToDouble(metaTBody[i][3]?.Content.Trim().Replace(".", "").Replace(",", ".")));
                            rowCur.Cells[5].SetCellValue(UtilsCore.StringToDouble(metaTBody[i][4]?.Content.Trim().Replace(".", "").Replace(",", ".")));
                            rowCur.Cells[6].SetCellValue(UtilsCore.StringToDouble(metaTBody[i][5]?.Content.Trim().Replace(".", "").Replace(",", ".")));
                            rowCur.Cells[0].CellStyle = styleCellNumber;
                            rowCur.Cells[1].CellStyle = styleCellNumber;
                            rowCur.Cells[2].CellStyle = styleCellNumber;
                            rowCur.Cells[3].CellStyle = styleCellNumber;
                            rowCur.Cells[4].CellStyle = styleCellNumber;
                            rowCur.Cells[5].CellStyle = styleCellNumber;
                            rowCur.Cells[6].CellStyle = styleCellNumber;
                            break;
                        }
                        if (j == 0 || j == 1)
                        {
                            rowCur.Cells[j].SetCellValue(metaTBody[i][j]?.Content?.ToString());
                            rowCur.Cells[j].CellStyle = styleCellNumber;
                        }
                        else
                        {
                            rowCur.Cells[j].SetCellValue(UtilsCore.StringToDouble(metaTBody[i][j]?.Content.Trim().Replace(".", "").Replace(",", ".")));
                            rowCur.Cells[j].CellStyle = styleCellNumber;
                        }
                        
                    }
                    
                }
                else if (module.Trim() == "KeHoachSuaChuaLon" || module.Trim() == "SuaChuaThuongXuyen")
                {
                    IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, NUM_CELL);
                    for(int j = 0; j < NUM_CELL; j++)
                    {
                        if(j == 0 ||j == 1 || j == 2 || j == NUM_CELL-1)
                        {
                            rowCur.Cells[0].CellStyle = styleCellFirst;
                            if (j == 0)
                            {
                                rowCur.Cells[j].SetCellValue(metaTBody[i][0]?.Content?.ToString());
                                rowCur.Cells[j].CellStyle = styleCellNumber;
                            }
                            else
                            {
                                rowCur.Cells[j].SetCellValue(metaTBody[i][j]?.Content?.ToString());
                                rowCur.Cells[j].CellStyle = styleCellNumber;
                            }
                        }
                        else
                        {
                            rowCur.Cells[j].SetCellValue(UtilsCore.StringToDouble(metaTBody[i][j]?.Content.Trim().Replace(".", "").Replace(",", ".")));
                            rowCur.Cells[j].CellStyle = styleCellNumber;
                        }

                    }
                }
                else if(module.Trim() == "KeHoachChiPhi")
                {
                    var startCell = 4;
                    var sanbay = (NUM_CELL - 8)/2;
                    var cellsum = startCell + sanbay +1;
                    IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, NUM_CELL);
                    for(int j = 0; j < NUM_CELL-1; j++)
                    {
                        rowCur.Cells[j].CellStyle = styleCellFirst;
                    }
                    for(int j = 1; j < NUM_CELL; j++)
                    {
                        if(j == NUM_CELL - 1)
                        {
                            rowCur.Cells[NUM_CELL - 2].SetCellValue(metaTBody[i][j]?.Content);
                            break;
                        }
                        else if(j > startCell -1 && j < cellsum-1)
                        {
                            rowCur.Cells[j - 1].SetCellValue(UtilsCore.StringToDouble(metaTBody[i][j]?.Content.Trim().Replace(".", "").Replace(",", ".")));

                        }
                        else if(j > cellsum-1 && j < cellsum+ sanbay+1)
                        {
                            rowCur.Cells[j - 1].SetCellValue(UtilsCore.StringToDouble(metaTBody[i][j]?.Content.Trim().Replace(".", "").Replace(",", ".")));
                        }
                        else
                        {
                            rowCur.Cells[j - 1].SetCellValue(metaTBody[i][j].Content);
                        }

                    }
                }
                else if (module.Trim() == "DauTuXayDung" || module.Trim() == "DauTuTrangThietBi")
                {
                    IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, NUM_CELL);
                    for(int cell = 0; cell < NUM_CELL; cell++)
                    {
                        if(cell==0||cell == 1|| cell == 4 || cell == NUM_CELL-1)
                        {
                            rowCur.Cells[cell].SetCellValue(metaTBody[i][cell]?.Content);
                            rowCur.Cells[cell].CellStyle = styleCellFirst;
                        }
                        else
                        {
                            rowCur.Cells[cell].SetCellValue(UtilsCore.StringToDouble(metaTBody[i][cell]?.Content.Trim().Replace(".", "").Replace(",", ".")));
                            rowCur.Cells[cell].CellStyle = styleCellFirst;

                        }
                    }
                }

                else if(module.Trim() == "DauTuNgoaiDoanhNghiep")
                {
                    IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, NUM_CELL);
                    for (int cell = 0; cell < NUM_CELL; cell++)
                    {
                        rowCur.Cells[cell].SetCellValue(metaTBody[i][cell]?.Content);
                        rowCur.Cells[cell].CellStyle = styleCellNumber;
                    }
                }

                else if(module.Trim() == "KeHoachVanChuyen")
                {
                    IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, NUM_CELL);
                    for (int cell = 0; cell < NUM_CELL; cell++)
                    {
                        rowCur.Cells[cell].SetCellValue(metaTBody[i][cell]?.Content);
                        rowCur.Cells[cell].CellStyle = styleCellFirst;
                    }
                }
                
                else
                {
                    IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, NUM_CELL);
                    rowCur.Cells[0].SetCellValue("");
                    rowCur.Cells[1].SetCellValue(metaTBody[i][0]?.Content?.ToString());
                    rowCur.Cells[2].SetCellValue(UtilsCore.StringToDouble(metaTBody[i][1]?.Content.Trim().Replace(".", "").Replace(",", ".")));
                    rowCur.Cells[3].SetCellValue(UtilsCore.StringToDouble(metaTBody[i][2]?.Content.Trim().Replace(".", "").Replace(",", ".")));
                    rowCur.Cells[4].SetCellValue(UtilsCore.StringToDouble(metaTBody[i][3]?.Content.Trim().Replace(".", "").Replace(",", ".")));
                    rowCur.Cells[5].SetCellValue(UtilsCore.StringToDouble(metaTBody[i][4]?.Content.Trim().Replace(".", "").Replace(",", ".")));
                    rowCur.Cells[6].SetCellValue(UtilsCore.StringToDouble(metaTBody[i][5]?.Content.Trim().Replace(".", "").Replace(",", ".")));
                    rowCur.Cells[2].CellStyle = styleCellNumber;
                    rowCur.Cells[3].CellStyle = styleCellNumber;
                    rowCur.Cells[4].CellStyle = styleCellNumber;
                    rowCur.Cells[5].CellStyle = styleCellNumber;
                    rowCur.Cells[6].CellStyle = styleCellNumber;
                }
                
            }
        }

        public static void InsertHeaderTable(ref IWorkbook workbook,
            ref ISheet sheet,
            IList<IList<ExcelCellMeta>> metaTHeader,
            int NUM_CELL,
            string module,
            bool ignoreFirstColumn)
        {
            ICellStyle styleCellHeader = workbook.CreateCellStyle();
            styleCellHeader.CloneStyleFrom(sheet.GetRow(7).Cells[0].CellStyle);
            styleCellHeader.WrapText = true;
            styleCellHeader.Alignment = HorizontalAlignment.Center;
            
            var numRowCur = 7;
            var rowStart = 0;
            foreach (var row in metaTHeader)
            {
                var tempIgnoreColumn = ignoreFirstColumn;
                var columns = 0;
                //ReportUtilities.CopyRow(ref sheet, 8, numRowCur);
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, numRowCur, NUM_CELL);
                if (module.Trim() == "KeHoachSuaChuaLon" || module.Trim() == "SuaChuaThuongXuyen")
                {
                    for(int cell = 0; cell < NUM_CELL; cell++)
                    {
                        rowCur.Cells[cell].CellStyle = styleCellHeader;
                        if (cell == 0)
                        {
                            sheet.SetColumnWidth(cell, 8000);
                        }
                        else
                        {
                            sheet.SetColumnWidth(cell, 5000);
                        }
                    }
                    if (rowStart == 1)
                    {
                        columns = 2;

                        foreach (var cell in row)
                        {
                            rowCur.Height = -1;
                            rowCur.Cells[columns].SetCellValue(cell.Content);
                            columns++;
                        }
                        //merge
                        
                    }

                    else
                    {
                        for(int i = 0; i< NUM_CELL; i++)
                        {
                            if(i == NUM_CELL - 1)
                            {
                                rowCur.Height = -1;
                                ICellStyle styleCellMerge = workbook.CreateCellStyle();
                                styleCellMerge.CloneStyleFrom(sheet.GetRow(7).Cells[0].CellStyle);
                                styleCellMerge.WrapText = true;
                                rowCur.Cells[NUM_CELL-1].CellStyle = styleCellMerge;
                                rowCur.Cells[NUM_CELL - 1].SetCellValue(row[row.Count-1].Content);
                            }
                            else
                            {
                                rowCur.Height = -1;
                                rowCur.Cells[i].CellStyle = styleCellHeader;
                                if(i > row.Count - 2)
                                {
                                    continue;
                                }
                                rowCur.Cells[i].SetCellValue(row[i].Content);
                            }
                        }
                        sheet.AddMergedRegion(new CellRangeAddress(7, 7, 2, NUM_CELL - 2));

                        sheet.AddMergedRegion(new CellRangeAddress(7, 8, NUM_CELL - 1, NUM_CELL - 1));
                    } 
                    rowStart++;  
                }
                else if(module.Trim() == "KeHoachChiPhi")
                {
                    var cellstart = 4;
                    var sanbay = metaTHeader[2].Count / 2;
                    var CellSum = cellstart + sanbay + 1;
                    for (int cell = 0; cell < NUM_CELL-1; cell++)
                    {
                        rowCur.Cells[cell].CellStyle = styleCellHeader;
                    }
                    if (rowStart == 0)
                    {
                        
                        for(int cell = 1; cell< row.Count; cell++)
                        {
                            rowCur.Cells[cell-1].SetCellValue(row[cell]?.Content);
                            rowCur.Cells[cell-1].CellStyle = styleCellHeader;
                        }
                    }
                    else if(rowStart == 1)
                    {
                        for(int cell =  cellstart-1; cell < NUM_CELL; cell++)
                        {
                            if(cell == cellstart-1)
                            {
                                rowCur.Cells[cell].SetCellValue(row[0]?.Content);
                                rowCur.Cells[cell].CellStyle = styleCellHeader;
                            }

                            if(cell == CellSum-1)
                            {
                                rowCur.Cells[cell].SetCellValue(row[1]?.Content);
                                rowCur.Cells[cell+1].SetCellValue(row[2]?.Content);

                                rowCur.Cells[cell].CellStyle = styleCellHeader;
                                rowCur.Cells[cell+1].CellStyle = styleCellHeader;
                            }

                            if(cell == NUM_CELL - 3)
                            {
                                rowCur.Cells[cell].SetCellValue(row[3]?.Content);
                                rowCur.Cells[cell + 1].SetCellValue(row[4]?.Content);
                                rowCur.Cells[cell].CellStyle = styleCellHeader;
                                rowCur.Cells[cell+1].CellStyle = styleCellHeader;
                            }
                        }
                    }
                    else
                    {
                        var indexRow = 0;
                        for(int cell = 0; cell < NUM_CELL; cell++)
                        {
                            if(cell == 1)
                            {
                                sheet.SetColumnWidth(cell, 8000);
                            }
                            else
                            {
                                sheet.SetColumnWidth(cell, 5000);
                            }
                        }
                        for(int cell = cellstart-1; cell < CellSum-1; cell++){
                            rowCur.Cells[cell].SetCellValue(row[indexRow]?.Content);
                            rowCur.Cells[cell].CellStyle = styleCellHeader;
                            indexRow++;
                        }
                        for(int cell = CellSum; cell < NUM_CELL - 3; cell++)
                        {
                            rowCur.Cells[cell].SetCellValue(row[indexRow]?.Content);
                            rowCur.Cells[cell].CellStyle = styleCellHeader;
                            indexRow++;
                        }
                        sheet.AddMergedRegion(new CellRangeAddress(7, 9, 0, 0));
                        sheet.AddMergedRegion(new CellRangeAddress(7, 9, 1, 1));
                        sheet.AddMergedRegion(new CellRangeAddress(7, 9, 2, 2));
                        sheet.AddMergedRegion(new CellRangeAddress(8, 9, CellSum-1, CellSum - 1));
                        sheet.AddMergedRegion(new CellRangeAddress(8, 9, NUM_CELL - 3, NUM_CELL - 3));
                        sheet.AddMergedRegion(new CellRangeAddress(8, 9, NUM_CELL - 2, NUM_CELL - 2));
                        sheet.AddMergedRegion(new CellRangeAddress(7, 7, 3, NUM_CELL - 2));
                        sheet.AddMergedRegion(new CellRangeAddress(8, 8, 3, CellSum - 2));
                        sheet.AddMergedRegion(new CellRangeAddress(8, 8, CellSum, NUM_CELL - 4));
                    }
                    
                    rowStart++;
                }
                else if (module.Trim() == "DauTuXayDung")
                {
                    var cellInfor = 2;
                    var cellValue = 5;
                    var cellGiaiNgan = 8;
                    if(rowStart == 0)
                    {
                        for (int i = 0; i < NUM_CELL; i++)
                        {
                            rowCur.Cells[i].CellStyle = styleCellHeader;
                            if (i == 0 || i == 1)
                            {
                                rowCur.Cells[i].SetCellValue(row[i]?.Content);
                            }
                            else if (i == cellInfor)
                            {
                                rowCur.Cells[i].SetCellValue(row[i]?.Content);
                            }
                            else if (i == cellValue)
                            {
                                rowCur.Cells[i].SetCellValue(row[3]?.Content);
                            }
                            else if (i == cellGiaiNgan)
                            {
                                rowCur.Cells[i].SetCellValue(row[4]?.Content);
                            }
                            else if (i == NUM_CELL - 1)
                            {
                                rowCur.Cells[i].SetCellValue(row[5]?.Content);
                            }
                        }
                    }
                    else
                    {
                        for(int cell = 2; cell < NUM_CELL; cell++)
                        {
                            sheet.SetColumnWidth(cell, 5000);
                        }
                        var indexRow = 0;
                        for (int i = cellInfor; i< cellValue; i++)
                        {
                            rowCur.Cells[i].SetCellValue(row[indexRow]?.Content);
                            rowCur.Cells[i].CellStyle = styleCellHeader;
                            indexRow++;
                        }
                        for(int i = cellValue; i < cellGiaiNgan; i++)
                        {
                            rowCur.Cells[i].SetCellValue(row[indexRow]?.Content);
                            rowCur.Cells[i].CellStyle = styleCellHeader;
                            indexRow++;
                        }
                        for(int i = cellGiaiNgan; i < NUM_CELL - 1; i++)
                        {
                            rowCur.Cells[i].SetCellValue(row[indexRow]?.Content);
                            rowCur.Cells[i].CellStyle = styleCellHeader;
                            indexRow++;
                        }
                        rowCur.Cells[NUM_CELL-1].CellStyle = styleCellHeader;
                        sheet.AddMergedRegion(new CellRangeAddress(7, 8, 0, 0));
                        sheet.AddMergedRegion(new CellRangeAddress(7, 8, 1, 1));
                        sheet.AddMergedRegion(new CellRangeAddress(7, 8, NUM_CELL-1, NUM_CELL-1));
                        sheet.AddMergedRegion(new CellRangeAddress(7, 7, cellInfor, cellValue - 1));
                        sheet.AddMergedRegion(new CellRangeAddress(7, 7, cellValue, cellGiaiNgan - 1));
                        sheet.AddMergedRegion(new CellRangeAddress(7, 7, cellGiaiNgan, NUM_CELL - 2));

                    }
                    rowStart++;
                }
                else if(module.Trim() == "DauTuTrangThietBi")
                {
                    var cellInfor = 2;
                    var cellValue = 5;
                    var cellGiaiNgan = 10;
                    for (int cell = 0; cell < NUM_CELL; cell++)
                    {
                        rowCur.Cells[cell].CellStyle = styleCellHeader;
                    }
                    if (rowStart == 0)
                    {
                        for (int i = 0; i < NUM_CELL; i++)
                        {
                            rowCur.Cells[i].CellStyle = styleCellHeader;
                            if (i == 0 || i == 1)
                            {
                                rowCur.Cells[i].SetCellValue(row[i]?.Content);
                            }
                            else if (i == cellInfor)
                            {
                                rowCur.Cells[i].SetCellValue(row[i]?.Content);
                            }
                            else if (i == cellValue)
                            {
                                rowCur.Cells[i].SetCellValue(row[3]?.Content);
                            }
                            else if (i == cellGiaiNgan)
                            {
                                rowCur.Cells[i].SetCellValue(row[4]?.Content);
                            }
                            else if (i == NUM_CELL - 1)
                            {
                                rowCur.Cells[i].SetCellValue(row[5]?.Content);
                            }
                        }
                    }
                    else if(rowStart == 1)
                    {
                        var indexRow = 0;
                        for(int cell = cellInfor; cell < NUM_CELL - 1; cell++)
                        {
                            rowCur.Cells[cell].CellStyle = styleCellHeader;
                            if (cell < cellValue + 2)
                            {
                                rowCur.Cells[cell].SetCellValue(row[indexRow]?.Content);
                                indexRow++;
                            }
                            else if (cell >= cellGiaiNgan - 1)
                            {
                                rowCur.Cells[cell].SetCellValue(row[indexRow]?.Content);
                                indexRow++;
                            }
                        }
                    }
                    else
                    {
                        for (int cell = 2; cell < NUM_CELL; cell++)
                        {
                            sheet.SetColumnWidth(cell, 5000);
                        }
                        var indexRow = 0;

                        for(int cell = cellValue + 1; cell < cellValue + 4; cell++)
                        {
                            rowCur.Cells[cell].CellStyle = styleCellHeader;
                            rowCur.Cells[cell].SetCellValue(row[indexRow]?.Content);
                            indexRow++;
                        }
                        rowCur.Cells[NUM_CELL - 1].CellStyle = styleCellHeader;
                        sheet.AddMergedRegion(new CellRangeAddress(7, 9, 0, 0));
                        sheet.AddMergedRegion(new CellRangeAddress(7, 9, 1, 1));
                        sheet.AddMergedRegion(new CellRangeAddress(8, 9, 2, 2));
                        sheet.AddMergedRegion(new CellRangeAddress(8, 9, 3, 3));
                        sheet.AddMergedRegion(new CellRangeAddress(8, 9, 4, 4));
                        sheet.AddMergedRegion(new CellRangeAddress(8, 9, 5, 5));
                        sheet.AddMergedRegion(new CellRangeAddress(8, 8, 6, 8));
                        sheet.AddMergedRegion(new CellRangeAddress(8, 9, 9, 9));
                        sheet.AddMergedRegion(new CellRangeAddress(8, 9, 10, 10));
                        sheet.AddMergedRegion(new CellRangeAddress(8, 9, 11, 11));
                        sheet.AddMergedRegion(new CellRangeAddress(7, 9, NUM_CELL-1, NUM_CELL-1));
                        sheet.AddMergedRegion(new CellRangeAddress(7, 7, cellInfor, cellValue - 1));
                        sheet.AddMergedRegion(new CellRangeAddress(7, 7, cellValue, cellGiaiNgan - 1));
                        sheet.AddMergedRegion(new CellRangeAddress(7, 7, cellGiaiNgan, NUM_CELL - 2));

                    }
                    rowStart++;
                }
                 else if(module.Trim() == "DauTuNgoaiDoanhNghiep")
                {
                    for (int cell = 0; cell < NUM_CELL - 1; cell++)
                    {
                        rowCur.Cells[cell].CellStyle = styleCellHeader;
                    }
                    for (int cell = 0; cell < NUM_CELL; cell++)
                    {
                        sheet.SetColumnWidth(cell, 5000);

                        rowCur.Cells[cell].CellStyle = styleCellHeader;
                        rowCur.Cells[cell].SetCellValue(row[cell]?.Content);
                    }
                }
                else if(module.Trim() == "KeHoachVanChuyen")
                {
                    for (int cell = 0; cell < NUM_CELL; cell++)
                    {
                        rowCur.Cells[cell].CellStyle = styleCellHeader;
                    }
                    if (rowStart == 0)
                    {
                        for (int cell = 0; cell < 6; cell++)
                        {
                            rowCur.Cells[cell].SetCellValue(row[cell]?.Content);
                            rowCur.Cells[cell].CellStyle = styleCellHeader;
                        }
                        rowCur.Cells[NUM_CELL - 2].SetCellValue(row[6]?.Content);
                    }
                    else if(rowStart == 1)
                    {
                        var indexRow = 0;
                        for (int cell = 5; cell< 8; cell++)
                        {
                            rowCur.Cells[cell].SetCellValue(row[indexRow]?.Content);
                            indexRow++;
                        }
                        for(int cell = NUM_CELL - 2; cell < NUM_CELL; cell++)
                        {
                            rowCur.Cells[cell].SetCellValue(row[indexRow]?.Content);
                            indexRow++;
                        }
                    }
                    else
                    {
                        var indexRow = 0;
                        for (int cell = 2; cell < NUM_CELL; cell++)
                        {
                            sheet.SetColumnWidth(cell, 5000);
                        }
                        for (int cell = 7; cell < NUM_CELL - 2; cell++)
                        {
                            rowCur.Cells[cell].SetCellValue(row[indexRow]?.Content);
                            indexRow++;
                        }
                        sheet.AddMergedRegion(new CellRangeAddress(7, 9, 0, 0));
                        sheet.AddMergedRegion(new CellRangeAddress(7, 9, 1, 1));
                        sheet.AddMergedRegion(new CellRangeAddress(7, 9, 2, 2));
                        sheet.AddMergedRegion(new CellRangeAddress(7, 9, 3, 3));
                        sheet.AddMergedRegion(new CellRangeAddress(7, 9, 4, 4));
                        sheet.AddMergedRegion(new CellRangeAddress(8, 9, 5, 5));
                        sheet.AddMergedRegion(new CellRangeAddress(8, 9, 6, 6));
                        sheet.AddMergedRegion(new CellRangeAddress(8, 8, 7, 10));
                        sheet.AddMergedRegion(new CellRangeAddress(7, 7, 5, 10));
                        sheet.AddMergedRegion(new CellRangeAddress(7, 7, 11, 12));
                        sheet.AddMergedRegion(new CellRangeAddress(8, 9, NUM_CELL - 2, NUM_CELL - 2));
                        sheet.AddMergedRegion(new CellRangeAddress(8, 9, NUM_CELL - 1, NUM_CELL - 1));
                    }
                    rowStart++;
                }
                else
                {
                    foreach (var cell in row)
                    {
                        rowCur.Height = -1;
                        rowCur.Cells[columns].CellStyle = styleCellHeader;
                        rowCur.Cells[columns].SetCellValue(cell.Content);
                        columns++;
                    }
                }
                numRowCur++;
            }

            //Xóa dòng thừa cuối cùng khi tạo các dòng cho detail
            //IRow rowLastDetail = ReportUtilities.CreateRow(ref sheet, numRowCur, NUM_CELL);
            //ReportUtilities.DeleteRow(ref sheet, rowLastDetail);

        }

        public static void InitHeaderFile(ref ISheet sheet, int year, string centerName, int? version, int NUM_CELL, string templateName, string unit, string name, decimal exchangeRate)
        {
            var rowHeader1 = ReportUtilities.CreateRow(ref sheet, 1, NUM_CELL);
            ReportUtilities.CreateCell(ref rowHeader1, NUM_CELL);
            rowHeader1.Cells[0].SetCellValue(rowHeader1.Cells[0].StringCellValue + $" {centerName}");
            rowHeader1.Cells[1].SetCellValue(name);

            var rowHeader2 = ReportUtilities.CreateRow(ref sheet, 2, NUM_CELL);
            ReportUtilities.CreateCell(ref rowHeader2, NUM_CELL);
            rowHeader2.Cells[0].SetCellValue(rowHeader2.Cells[0].StringCellValue + $" {year}");

            if (version > 0)
            {
                var rowHeader3 = ReportUtilities.CreateRow(ref sheet, 3, NUM_CELL);
                ReportUtilities.CreateCell(ref rowHeader3, NUM_CELL);
                rowHeader3.Cells[0].SetCellValue(rowHeader3.Cells[0].StringCellValue + $" {version}");
            }

            var rowHeader4 = ReportUtilities.CreateRow(ref sheet, 4, NUM_CELL);
            ReportUtilities.CreateCell(ref rowHeader4, NUM_CELL);

            rowHeader4.Cells[0].SetCellValue(templateName);

            var rowHeader5 = ReportUtilities.CreateRow(ref sheet, 5, NUM_CELL);
            ReportUtilities.CreateCell(ref rowHeader5, NUM_CELL);
            if (unit == "VND")
            {
                // hiển thị đơn vị đồng, nghìn đồng, triệu đồng
                if (exchangeRate == 1)
                {
                    rowHeader5.Cells[0].SetCellValue($"Đơn vị ({unit}): Đồng");
                }
                else if (exchangeRate == 1000)
                {
                    rowHeader5.Cells[0].SetCellValue($"Đơn vị ({unit}): Nghìn đồng");
                }
                else if (exchangeRate == 1000000)
                {
                    rowHeader5.Cells[0].SetCellValue($"Đơn vị ({unit}): Triệu đồng");
                }
                else
                {
                    rowHeader5.Cells[0].SetCellValue($"Đơn vị ({unit}): {exchangeRate.ToStringVnWithoutDecimal()}");
                }
            }
            else
            {
                rowHeader5.Cells[0].SetCellValue($"Đơn vị ({unit}): {exchangeRate.ToStringVnWithoutDecimal(true)}");
            }

        }

        public static void insertBodyKeHoachTaiChinh(ref IWorkbook workbook, List<KeHoachTaiChinhData> dataDetails,string module, ref ISheet sheet, int NUM_CELL)
        {
            int startRow = 6;
            ICellStyle styleCellNumber = workbook.CreateCellStyle();
            styleCellNumber.CloneStyleFrom(sheet.GetRow(6).Cells[0].CellStyle);
            styleCellNumber.WrapText = true;
            if (module.Trim() == "KeHoachTaiChinh")
            {
                foreach (var item in dataDetails.Where(x => x.Screen == "KE_HOACH_TAI_CHINH").OrderBy(x => x.Order))
                {
                    IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, NUM_CELL);
                    rowCur.Cells[0].SetCellValue(item.ElementName);
                    rowCur.Cells[1].SetCellValue(item.UnitCode);
                    rowCur.Cells[2].SetCellValue(UtilsCore.StringToDouble(item?.Value?.ToStringVN().Trim().Replace(".", "").Replace(",", ".")));
                    rowCur.Cells[0].CellStyle = styleCellNumber;
                    rowCur.Cells[1].CellStyle = styleCellNumber;
                    rowCur.Cells[2].CellStyle = styleCellNumber;
                }
            } 
            else {
                foreach (var item in dataDetails.Where(x => x.Screen == "KE_HOACH_TAI_CHINH_2").OrderBy(x => x.Order))
                {
                    IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, NUM_CELL);
                    rowCur.Cells[0].SetCellValue(item.ElementName);
                    rowCur.Cells[1].SetCellValue(item.UnitCode);
                    rowCur.Cells[2].SetCellValue(UtilsCore.StringToDouble(item?.Value?.ToStringVN().Trim().Replace(".", "").Replace(",", ".")));
                    rowCur.Cells[0].CellStyle = styleCellNumber;
                    rowCur.Cells[1].CellStyle = styleCellNumber;
                    rowCur.Cells[2].CellStyle = styleCellNumber;
                }
            }
        }

        public static void insertBodyKeHoachGiaVon(ref IWorkbook workbook, DataCenterModel data, string module, ref ISheet sheet, int NUM_CELL)
        {
            
            if (module.Trim() == "KeHoachGiaVon")
            {
                int startRow = 7;
                ICellStyle styleCellNumber = workbook.CreateCellStyle();
                styleCellNumber.CloneStyleFrom(sheet.GetRow(8).Cells[0].CellStyle);
                styleCellNumber.DataFormat = workbook.CreateDataFormat().GetFormat("#,###");
                styleCellNumber.WrapText = true;

                ICellStyle styleCellName = workbook.CreateCellStyle();
                styleCellName.CloneStyleFrom(sheet.GetRow(8).Cells[0].CellStyle);
                styleCellName.WrapText = true;

                ICellStyle styleCellBold = workbook.CreateCellStyle();
                styleCellBold.CloneStyleFrom(sheet.GetRow(7).Cells[0].CellStyle);

                ICellStyle styleCellHeader = workbook.CreateCellStyle();
                styleCellHeader.CloneStyleFrom(sheet.GetRow(6).Cells[0].CellStyle);
                foreach (var item in data.KeHoachGiaThanhData.OrderBy(x => x.AreaCode).ThenBy(x => x.Warehouse))
                {
                    if (item.S0001 == 0 && item.S0002 == 0 && item.U0001 == 0 && item.S0003 == 0 && item.S0004 == 0
                        && item.U0002 == 0 && item.S0005 == 0 && item.U0003 == 0 && item.S0006 == 0 && item.S0007 == 0
                        && item.U0004 == 0 && item.U0005 == 0 && item.U0006 == 0 && item.U0007 == 0 && item.U0008 == 0
                        && item.U0009 == 0 && item.U0010 == 0)
                    {
                        continue;
                    }
                    IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, NUM_CELL);
                    for (int i = 3; i < NUM_CELL; i++)
                    {
                        rowCur.Cells[i].CellStyle = styleCellNumber;
                    }
                    rowCur.Cells[0].CellStyle = styleCellName;
                    rowCur.Cells[1].CellStyle = styleCellName;
                    rowCur.Cells[2].CellStyle = styleCellName;
                    rowCur.Cells[0].SetCellValue(item.Warehouse);
                    rowCur.Cells[1].SetCellValue(item.AreaCode);
                    rowCur.Cells[2].SetCellValue(item.DeliveryConditions);
                    rowCur.Cells[3].SetCellValue((double)item.S0001);
                    rowCur.Cells[4].SetCellValue((double)item.S0002);
                    rowCur.Cells[5].SetCellValue((double)item.U0001);
                    rowCur.Cells[6].SetCellValue((double)item.S0003);
                    rowCur.Cells[7].SetCellValue((double)item.S0004);
                    rowCur.Cells[8].SetCellValue((double)item.U0002);
                    rowCur.Cells[9].SetCellValue((double)item.S0005);
                    rowCur.Cells[10].SetCellValue((double)item.U0003);
                    rowCur.Cells[11].SetCellValue((double)item.S0006);
                    rowCur.Cells[12].SetCellValue((double)item.S0007);
                    rowCur.Cells[13].SetCellValue((double)item.U0004);
                    rowCur.Cells[14].SetCellValue((double)item.U0005);
                    rowCur.Cells[15].SetCellValue((double)item.U0006);
                    rowCur.Cells[16].SetCellValue((double)item.U0007);
                    rowCur.Cells[17].SetCellValue((double)item.U0008);
                    rowCur.Cells[18].SetCellValue((double)item.U0009);
                    rowCur.Cells[19].SetCellValue((double)item.U0010);
                }
            }
            else if(module.Trim() == "KeHoachGiaVon2")
            {
                ICellStyle styleCellNumber = workbook.CreateCellStyle();
                styleCellNumber.CloneStyleFrom(sheet.GetRow(9).Cells[0].CellStyle);
                styleCellNumber.DataFormat = workbook.CreateDataFormat().GetFormat("#,###");
                styleCellNumber.WrapText = true;

                ICellStyle styleCellName = workbook.CreateCellStyle();
                styleCellName.CloneStyleFrom(sheet.GetRow(9).Cells[0].CellStyle);
                styleCellName.WrapText = true;


                ICellStyle styleCellBold = workbook.CreateCellStyle();
                styleCellBold.CloneStyleFrom(sheet.GetRow(8).Cells[0].CellStyle);
                styleCellBold.DataFormat = workbook.CreateDataFormat().GetFormat("#,###");

                ICellStyle styleCellHeader = workbook.CreateCellStyle();
                styleCellHeader.CloneStyleFrom(sheet.GetRow(7).Cells[0].CellStyle);
                int startRow = 8;
                foreach (var item in data.KeHoachGiaVonData.OrderBy(x=> x.Order))
                {
                    IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, NUM_CELL);
                    for(int i = 0; i < NUM_CELL; i++)
                    {
                        if (item.IsBold)
                        {
                            rowCur.Cells[i].CellStyle = styleCellBold;
                        }
                        else
                        {
                            rowCur.Cells[i].CellStyle = styleCellNumber;
                        }
                    }
                    rowCur.Cells[0].SetCellValue(item.Name);
                    rowCur.Cells[1].SetCellValue((double)(item.Value1));
                    rowCur.Cells[2].SetCellValue((double)(item.Value2));
                    rowCur.Cells[3].SetCellValue((double)(item.Value3));
                    rowCur.Cells[4].SetCellValue((double)(item.Value4));
                    rowCur.Cells[5].SetCellValue((double)(item.Value5));
                    rowCur.Cells[6].SetCellValue((double)(item.Value6));
                    rowCur.Cells[7].SetCellValue((double)(item.Value7));
                    rowCur.Cells[8].SetCellValue((double)(item.Value8));
                    rowCur.Cells[9].SetCellValue((double)(item.Value9));
                    rowCur.Cells[10].SetCellValue((double)(item.Value10));
                    rowCur.Cells[11].SetCellValue((double)(item.Value11));
                    rowCur.Cells[12].SetCellValue((double)(item.Value12));
                    rowCur.Cells[13].SetCellValue((double)(item.Value13));
                    rowCur.Cells[14].SetCellValue((double)(item.Value14));
                    rowCur.Cells[15].SetCellValue((double)(item.Value15));
                    rowCur.Cells[16].SetCellValue((double)(item.Value16));
                    rowCur.Cells[17].SetCellValue((double)(item.Value17));
                    rowCur.Cells[18].SetCellValue((double)(item.Value18));
                    rowCur.Cells[19].SetCellValue((double)(item.Value19));
                    rowCur.Cells[20].SetCellValue((double)(item.Value20));

                }
            }
            else
            {
                ICellStyle styleCellNumber = workbook.CreateCellStyle();
                styleCellNumber.CloneStyleFrom(sheet.GetRow(8).Cells[0].CellStyle);
                styleCellNumber.DataFormat = workbook.CreateDataFormat().GetFormat("#,###");
                styleCellNumber.WrapText = true;

                ICellStyle styleCellName = workbook.CreateCellStyle();
                styleCellName.CloneStyleFrom(sheet.GetRow(8).Cells[0].CellStyle);
                styleCellName.WrapText = true;


                ICellStyle styleCellBold = workbook.CreateCellStyle();
                styleCellBold.CloneStyleFrom(sheet.GetRow(7).Cells[0].CellStyle);
                styleCellBold.DataFormat = workbook.CreateDataFormat().GetFormat("#,###");

                ICellStyle styleCellHeader = workbook.CreateCellStyle();
                styleCellHeader.CloneStyleFrom(sheet.GetRow(6).Cells[0].CellStyle);
                int startRow = 7;
                foreach (var item in data.KeHoachGiaVonTheoThang.OrderBy(x => x.Order))
                {
                    IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, NUM_CELL);
                    for (int i = 0; i < NUM_CELL; i++)
                    {
                        if (item.IsBold)
                        {
                            rowCur.Cells[i].CellStyle = styleCellBold;
                        }
                        else
                        {
                            rowCur.Cells[i].CellStyle = styleCellNumber;
                        }
                    }
                    StringBuilder space = new StringBuilder();
                    for(int i = 0;i < item.Level; i++)
                    {
                        space.Append("        ");
                    }
                    rowCur.Cells[0].SetCellValue(space.ToString() + item.Name);
                    rowCur.Cells[1].SetCellValue(item.DVT);
                    rowCur.Cells[2].SetCellValue((double)item.ValueDG);
                    rowCur.Cells[3].SetCellValue((double)(item.Value1));
                    rowCur.Cells[4].SetCellValue((double)(item.Value2));
                    rowCur.Cells[5].SetCellValue((double)(item.Value3));
                    rowCur.Cells[6].SetCellValue((double)(item.Value4));
                    rowCur.Cells[7].SetCellValue((double)(item.Value5));
                    rowCur.Cells[8].SetCellValue((double)(item.Value6));
                    rowCur.Cells[9].SetCellValue((double)(item.Value7));
                    rowCur.Cells[10].SetCellValue((double)(item.Value8));
                    rowCur.Cells[11].SetCellValue((double)(item.Value9));
                    rowCur.Cells[12].SetCellValue((double)(item.Value10));
                    rowCur.Cells[13].SetCellValue((double)(item.Value11));
                    rowCur.Cells[14].SetCellValue((double)(item.Value12));
                    rowCur.Cells[15].SetCellValue((double)(item.SumGV));

                }
            }
        }

        public static void InsertHeaderKeHoachTaiChinh(ref IWorkbook workbook, string year, string module, ref ISheet sheet, int NUM_CELL)
        {
            IRow rowCur = ReportUtilities.CreateRow(ref sheet, 2, NUM_CELL);
            var yearKH = $"Năm ngân sách: {year}";
            rowCur.Cells[0].SetCellValue(yearKH);
        }

        public static void InsertHeaderKeHoachGiaVon(ref IWorkbook workbook, string year, string module, ref ISheet sheet, int NUM_CELL)
        {
            IRow rowCur = ReportUtilities.CreateRow(ref sheet, 2, NUM_CELL);
            var yearKH = $"Năm ngân sách: {year}";
            rowCur.Cells[0].SetCellValue(yearKH);
        }

    }
}
