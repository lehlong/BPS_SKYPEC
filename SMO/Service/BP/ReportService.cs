﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NHibernate.Criterion;
using NPOI.HSSF.Record.Chart;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.XWPF.UserModel;
using SMO.Models;
using SMO.Repository.Common;
using SMO.Repository.Implement.BP.DAU_TU_NGOAI_DOANH_NGHIEP;
using SMO.Repository.Implement.BP.DAU_TU_TRANG_THIET_BI;
using SMO.Repository.Implement.BP.DAU_TU_XAY_DUNG;
using SMO.Repository.Implement.BP.KE_HOACH_CHI_PHI;
using SMO.Repository.Implement.BP.KE_HOACH_DOANH_THU;
using SMO.Repository.Implement.BP.KE_HOACH_SAN_LUONG;
using SMO.Repository.Implement.BP.SUA_CHUA_LON;
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
                case "BM_01E":
                    startRow = 8;
                    InsertDataToTableBM_01E(templateWorkbook, sheet, lstData, startRow, NUMCELL);
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
                case "BM_02D2":
                    startRow = 8;
                    InsertDataToTableBM_02D2(templateWorkbook, sheet, lstData, startRow, NUMCELL);
                    break;
                case "BM_2107":
                    startRow = 9;
                    InsertDataToTableBM_2107(templateWorkbook, sheet, lstData, startRow, NUMCELL);
                    break;
                case "BM_2108":
                    startRow = 10;
                    InsertDataToTableBM_2108(templateWorkbook, sheet, lstData, startRow, NUMCELL);
                    break;
                case "BM_2109":
                    startRow = 8;
                    InsertDataToTableBM_2109(templateWorkbook, sheet, lstData, startRow, NUMCELL);
                    break;
                case "BM_2110":
                    startRow = 4;
                    InsertDataToTableBM_2110(templateWorkbook, sheet, lstData, startRow, NUMCELL);
                    break;
                default:
                    InsertDataToTableBM_02B(templateWorkbook, sheet, lstData, startRow, NUMCELL);
                    break;
            }
            templateWorkbook.Write(outFileStream);
        }
        private ICellStyle GetCellStyleNumber(IWorkbook templateWorkbook)
        {
            ICellStyle styleCellNumber = templateWorkbook.CreateCellStyle();
            styleCellNumber.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,##0");
            return styleCellNumber;
        }
        private ICellStyle GetCellStyleNumberDecimal(IWorkbook templateWorkbook)
        {
            ICellStyle styleCellNumber = templateWorkbook.CreateCellStyle();
            styleCellNumber.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,##0.000");
            return styleCellNumber;
        }
        public void ExportExcelTHKHDT(ref MemoryStream outFileStream, List<ReportDauTuModel> data, string path)
        {

            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            IWorkbook templateWorkbook;
            templateWorkbook = new XSSFWorkbook(fs);
            fs.Close();
            ISheet sheet = templateWorkbook.GetSheetAt(0);
            var styleCellNumber = GetCellStyleNumber(templateWorkbook);

            ICellStyle styleCellBold = templateWorkbook.CreateCellStyle(); // chữ in đậm
            styleCellBold.CloneStyleFrom(sheet.GetRow(1).Cells[0].CellStyle);
            styleCellBold.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,##0");
            var fontBold = templateWorkbook.CreateFont();
            fontBold.Boldweight = (short)FontBoldWeight.Bold;
            fontBold.FontHeightInPoints = 12;
            fontBold.FontName = "Times New Roman";

            var startRow = 9;

            for (int i = 0; i < data.Count(); i++)
            {
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, 7);
                rowCur.Cells[0].SetCellValue(data[i]?.Stt);
                rowCur.Cells[1].SetCellValue(data[i]?.name);
                rowCur.Cells[2].SetCellValue(data[i]?.Col1);
                if (data[i].Col2 == 0 || data[i].Col2 == null)
                {
                    rowCur.Cells[3].SetCellValue("");
                }
                else
                {
                    rowCur.Cells[3].CellStyle = styleCellNumber;
                    rowCur.Cells[3].SetCellValue((double)Math.Round(data[i].Col2));
                }
                if (data[i].Col3 == 0 || data[i].Col3 == null)
                {
                    rowCur.Cells[4].SetCellValue("");
                }
                else
                {
                    rowCur.Cells[4].CellStyle = styleCellNumber;
                    rowCur.Cells[4].SetCellValue((double)Math.Round(data[i].Col3));
                }
                rowCur.Cells[5].SetCellValue(data[i]?.Col4);
                rowCur.Cells[6].SetCellValue(data[i]?.Des);

                for (var j = 0; j < 7; j++)
                {
                    if (data[i].IsBold)
                    {
                        rowCur.Cells[j].CellStyle = styleCellBold;
                        rowCur.Cells[j].CellStyle.SetFont(fontBold);
                    }
                    rowCur.Cells[j].CellStyle.BorderBottom = BorderStyle.Thin;
                    rowCur.Cells[j].CellStyle.BorderTop = BorderStyle.Thin;
                    rowCur.Cells[j].CellStyle.BorderLeft = BorderStyle.Thin;
                    rowCur.Cells[j].CellStyle.BorderRight = BorderStyle.Thin;
                    rowCur.Cells[j].CellStyle.WrapText = true;
                }
            }

            templateWorkbook.Write(outFileStream);

        }

        public void ExportExcelTHKHSCTX(ref MemoryStream outFileStream, List<SuaChuaThuongXuyenReportModel> data, string path)
        {

            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            IWorkbook templateWorkbook;
            templateWorkbook = new XSSFWorkbook(fs);
            fs.Close();
            ISheet sheet = templateWorkbook.GetSheetAt(0);
            var styleCellNumber = GetCellStyleNumber(templateWorkbook);

            ICellStyle styleCellBold = templateWorkbook.CreateCellStyle(); // chữ in đậm
            styleCellBold.CloneStyleFrom(sheet.GetRow(1).Cells[0].CellStyle);
            styleCellBold.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,##0");
            var fontBold = templateWorkbook.CreateFont();
            fontBold.Boldweight = (short)FontBoldWeight.Bold;
            fontBold.FontHeightInPoints = 12;
            fontBold.FontName = "Times New Roman";

            var startRow = 9;

            for (int i = 0; i < data.Count(); i++)
            {
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, 5);
                rowCur.Cells[0].SetCellValue(data[i]?.Stt);
                rowCur.Cells[1].SetCellValue(data[i]?.Name);
                if (data[i].valueGT == 0 || data[i].valueGT == null)
                {
                    rowCur.Cells[2].SetCellValue("");
                }
                else
                {
                    rowCur.Cells[2].CellStyle = styleCellNumber;
                    rowCur.Cells[2].SetCellValue((double)Math.Round(data[i].valueGT));
                }

                rowCur.Cells[3].SetCellValue(data[i]?.valueQM);
                rowCur.Cells[4].SetCellValue(data[i]?.Des);

                for (var j = 0; j < 5; j++)
                {
                    if (data[i].IsBold)
                    {
                        rowCur.Cells[j].CellStyle = styleCellBold;
                        rowCur.Cells[j].CellStyle.SetFont(fontBold);
                    }
                    rowCur.Cells[j].CellStyle.BorderBottom = BorderStyle.Thin;
                    rowCur.Cells[j].CellStyle.BorderTop = BorderStyle.Thin;
                    rowCur.Cells[j].CellStyle.BorderLeft = BorderStyle.Thin;
                    rowCur.Cells[j].CellStyle.BorderRight = BorderStyle.Thin;
                    rowCur.Cells[j].CellStyle.WrapText = true;
                }
            }

            templateWorkbook.Write(outFileStream);

        }

        public void ExportExcelTHKHCN(ref MemoryStream outFileStream, SynthesizeThePlanReportModel data, string path)
        {

            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            IWorkbook templateWorkbook;
            templateWorkbook = new XSSFWorkbook(fs);
            fs.Close();
            ISheet sheet = templateWorkbook.GetSheetAt(0);
            var styleCellNumber = GetCellStyleNumber(templateWorkbook);

            ICellStyle styleCellBold = templateWorkbook.CreateCellStyle(); // chữ in đậm
            styleCellBold.CloneStyleFrom(sheet.GetRow(1).Cells[0].CellStyle);
            styleCellBold.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,##0");
            var fontBold = templateWorkbook.CreateFont();
            fontBold.Boldweight = (short)FontBoldWeight.Bold;
            fontBold.FontHeightInPoints = 12;
            fontBold.FontName = "Times New Roman";

            ICellStyle styleCellNormal = templateWorkbook.CreateCellStyle(); // chữ in đậm
            styleCellNormal.CloneStyleFrom(sheet.GetRow(0).Cells[0].CellStyle);
            styleCellNormal.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,##0");
            var fontNormal = templateWorkbook.CreateFont();
            fontNormal.FontHeightInPoints = 12;
            fontNormal.FontName = "Times New Roman";


            var startRow1 = 12;
            var startRow2 = 12 + data.SanLuong.Count() + 5;
            var startRow3 = 12 + data.SanLuong.Count() + 5 + data.DauTu.Count() + 5;
            var startRow4 = 12 + data.SanLuong.Count() + 5 + data.DauTu.Count() + 5 + data.SuaChuaLon.Count() + 6;

            sheet.ShiftRows(startRow1, startRow1 + data.SanLuong.Count(), data.SanLuong.Count());

            for (int i = 0; i < data.SanLuong.Count(); i++)
            {
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow1++, 7);
                rowCur.Cells[0].SetCellValue(data.SanLuong[i]?.Order.ToString());
                rowCur.Cells[1].SetCellValue(data.SanLuong[i]?.Name);
                if (data.SanLuong[i].Value1 == 0 || data.SanLuong[i].Value1 == null)
                {
                    rowCur.Cells[2].SetCellValue("");
                }
                else
                {
                    rowCur.Cells[2].CellStyle = styleCellNumber;
                    rowCur.Cells[2].CellStyle.Alignment = HorizontalAlignment.Right;
                    rowCur.Cells[2].SetCellValue((double)Math.Round(data.SanLuong[i].Value1));
                }
                if (data.SanLuong[i].Value2 == 0 || data.SanLuong[i].Value2 == null)
                {
                    rowCur.Cells[3].SetCellValue("");
                }
                else
                {
                    rowCur.Cells[3].CellStyle = styleCellNumber;
                    rowCur.Cells[3].SetCellValue((double)Math.Round(data.SanLuong[i].Value2));
                }
                if (data.SanLuong[i].Value3 == 0 || data.SanLuong[i].Value3 == null)
                {
                    rowCur.Cells[4].SetCellValue("");
                }
                else
                {
                    rowCur.Cells[4].CellStyle = styleCellNumber;
                    rowCur.Cells[4].SetCellValue((double)Math.Round(data.SanLuong[i].Value3));
                }
                if (data.SanLuong[i].Value4 == 0 || data.SanLuong[i].Value4 == null)
                {
                    rowCur.Cells[5].SetCellValue("");
                }
                else
                {
                    rowCur.Cells[5].CellStyle = styleCellNumber;
                    rowCur.Cells[5].SetCellValue((double)Math.Round(data.SanLuong[i].Value4));
                }
                if (data.SanLuong[i].Value5 == 0 || data.SanLuong[i].Value5 == null)
                {
                    rowCur.Cells[6].SetCellValue("");
                }
                else
                {
                    rowCur.Cells[6].CellStyle = styleCellNumber;
                    rowCur.Cells[6].SetCellValue((double)Math.Round(data.SanLuong[i].Value5));
                }

                for (var j = 0; j < 7; j++)
                {
                    if (data.SanLuong[i].IsBold)
                    {
                        rowCur.Cells[j].CellStyle = styleCellBold;
                        rowCur.Cells[j].CellStyle.SetFont(fontBold);
                    }
                    else
                    {
                        rowCur.Cells[j].CellStyle = styleCellNormal;
                        rowCur.Cells[j].CellStyle.SetFont(fontNormal);
                    }
                    rowCur.Cells[j].CellStyle.BorderBottom = BorderStyle.Thin;
                    rowCur.Cells[j].CellStyle.BorderTop = BorderStyle.Thin;
                    rowCur.Cells[j].CellStyle.BorderLeft = BorderStyle.Thin;
                    rowCur.Cells[j].CellStyle.BorderRight = BorderStyle.Thin;
                    rowCur.Cells[j].CellStyle.WrapText = true;
                }
            }

            //InsertDT
            sheet.ShiftRows(startRow2, startRow2 + data.DauTu.Count(), data.DauTu.Count());
            for (int i = 0; i < data.DauTu.Count(); i++)
            {
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow2++, 7);
                rowCur.Cells[0].SetCellValue(data.DauTu[i]?.Stt.ToString());
                rowCur.Cells[1].SetCellValue(data.DauTu[i]?.Name);
                rowCur.Cells[2].SetCellValue(data.DauTu[i]?.Value1);
                
                if (data.DauTu[i].Value2 == 0 || data.DauTu[i].Value2 == null)
                {
                    rowCur.Cells[3].SetCellValue("");
                }
                else
                {
                    rowCur.Cells[3].CellStyle = styleCellNumber;
                    rowCur.Cells[3].SetCellValue((double)Math.Round(data.DauTu[i].Value2));
                }
                if (data.DauTu[i].Value3 == 0 || data.DauTu[i].Value3 == null)
                {
                    rowCur.Cells[4].SetCellValue("");
                }
                else
                {
                    rowCur.Cells[4].CellStyle = styleCellNumber;
                    rowCur.Cells[4].SetCellValue((double)Math.Round(data.DauTu[i].Value3));
                }

                rowCur.Cells[5].SetCellValue(data.DauTu[i]?.Value4);
                rowCur.Cells[6].SetCellValue(data.DauTu[i]?.Des);

                for (var j = 0; j < 7; j++)
                {
                    if (data.DauTu[i].IsBold)
                    {
                        rowCur.Cells[j].CellStyle = styleCellBold;
                        rowCur.Cells[j].CellStyle.SetFont(fontBold);
                    }
                    else
                    {
                        rowCur.Cells[j].CellStyle = styleCellNormal;
                        rowCur.Cells[j].CellStyle.SetFont(fontNormal);
                    }
                    rowCur.Cells[j].CellStyle.BorderBottom = BorderStyle.Thin;
                    rowCur.Cells[j].CellStyle.BorderTop = BorderStyle.Thin;
                    rowCur.Cells[j].CellStyle.BorderLeft = BorderStyle.Thin;
                    rowCur.Cells[j].CellStyle.BorderRight = BorderStyle.Thin;
                    rowCur.Cells[j].CellStyle.WrapText = true;
                }
            }

            //InsertSC
            sheet.ShiftRows(startRow3, startRow3 + data.SuaChuaLon.Count(), data.SuaChuaLon.Count());
            for (int i = 0; i < data.SuaChuaLon.Count(); i++)
            {
                var cra = new NPOI.SS.Util.CellRangeAddress(startRow3 + i, startRow3 + i, 1, 2);
                var cra1 = new NPOI.SS.Util.CellRangeAddress(startRow3 + i, startRow3 + i, 5, 6);
                sheet.AddMergedRegion(cra);
                sheet.AddMergedRegion(cra1);
            }
            for (int i = 0; i < data.SuaChuaLon.Count(); i++)
            {
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow3++, 7);
                rowCur.Cells[0].SetCellValue(data.SuaChuaLon[i]?.stt);
                rowCur.Cells[1].SetCellValue(data.SuaChuaLon[i]?.name);
                
                if (data.SuaChuaLon[i].valueKP == 0 || data.SuaChuaLon[i].valueKP == null)
                {
                    rowCur.Cells[3].SetCellValue("");
                }
                else
                {
                    rowCur.Cells[3].CellStyle = styleCellNumber;
                    rowCur.Cells[3].SetCellValue((double)Math.Round(data.SuaChuaLon[i].valueKP));
                }
               
                rowCur.Cells[4].SetCellValue(data.SuaChuaLon[i]?.valueQM);
                rowCur.Cells[5].SetCellValue(data.SuaChuaLon[i]?.des);

                for (var j = 0; j < 7; j++)
                {
                    if (data.SuaChuaLon[i].IsBold)
                    {
                        rowCur.Cells[j].CellStyle = styleCellBold;
                        rowCur.Cells[j].CellStyle.SetFont(fontBold);
                    }
                    else
                    {
                        rowCur.Cells[j].CellStyle = styleCellNormal;
                        rowCur.Cells[j].CellStyle.SetFont(fontNormal);
                    }
                    rowCur.Cells[j].CellStyle.BorderBottom = BorderStyle.Thin;
                    rowCur.Cells[j].CellStyle.BorderTop = BorderStyle.Thin;
                    rowCur.Cells[j].CellStyle.BorderLeft = BorderStyle.Thin;
                    rowCur.Cells[j].CellStyle.BorderRight = BorderStyle.Thin;
                    rowCur.Cells[j].CellStyle.WrapText = true;
                }
            }

            sheet.ShiftRows(startRow4, startRow4 + data.ChiPhi.Count(), data.ChiPhi.Count());
            for (int i = 0; i < data.ChiPhi.Count(); i++)
            {
                var cra = new NPOI.SS.Util.CellRangeAddress(startRow4 + i, startRow4 + i, 1, 2);
                var cra1 = new NPOI.SS.Util.CellRangeAddress(startRow4 + i, startRow4 + i, 5, 6);
                sheet.AddMergedRegion(cra);
                sheet.AddMergedRegion(cra1);
            }
            for (int i = 0; i < data.ChiPhi.Count(); i++)
            {
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow4++, 7);
                rowCur.Cells[0].SetCellValue(data.ChiPhi[i]?.Stt);
                rowCur.Cells[1].SetCellValue(data.ChiPhi[i]?.name);
                
                if (data.ChiPhi[i].valueCP == 0 || data.ChiPhi[i].valueCP == null)
                {
                    rowCur.Cells[3].SetCellValue("");
                }
                else
                {
                    rowCur.Cells[3].CellStyle = styleCellNumber;
                    rowCur.Cells[3].SetCellValue((double)Math.Round(data.ChiPhi[i].valueCP));
                }

                if (data.ChiPhi[i].price == 0 || data.ChiPhi[i].price == null)
                {
                    rowCur.Cells[4].SetCellValue("");
                }
                else
                {
                    rowCur.Cells[4].CellStyle = styleCellNumber;
                    rowCur.Cells[4].SetCellValue((double)Math.Round(data.ChiPhi[i].price));
                }

                rowCur.Cells[5].SetCellValue(data.ChiPhi[i]?.description);

                for (var j = 0; j < 7; j++)
                {
                    if (data.ChiPhi[i].IsBold)
                    {
                        rowCur.Cells[j].CellStyle = styleCellBold;
                        rowCur.Cells[j].CellStyle.SetFont(fontBold);
                    }
                    else
                    {
                        rowCur.Cells[j].CellStyle = styleCellNormal;
                        rowCur.Cells[j].CellStyle.SetFont(fontNormal);
                    }
                    rowCur.Cells[j].CellStyle.BorderBottom = BorderStyle.Thin;
                    rowCur.Cells[j].CellStyle.BorderTop = BorderStyle.Thin;
                    rowCur.Cells[j].CellStyle.BorderLeft = BorderStyle.Thin;
                    rowCur.Cells[j].CellStyle.BorderRight = BorderStyle.Thin;
                    rowCur.Cells[j].CellStyle.WrapText = true;
                }
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

            foreach (var item in dataDetails)
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
                for (int i = 0; i < NUM_CELL; i++)
                {
                    if (item.IsBold)
                    {
                        if (i == 0 || i == 3 || i == 6 || i == NUM_CELL - 1)
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
                        if (i == 0 || i == 3 || i == 6 || i == NUM_CELL - 1)
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
                        if (i == 0 || i == NUM_CELL - 1)
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
                        if (i == 0 || i == NUM_CELL - 1)
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
                if (item.Stt == "SUM")
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
                        if (i == 0 || i == 1)
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
                        if (i == 0 || i == 1)
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
            //styleBody.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#.###");
            ICellStyle styleBodyBold = templateWorkbook.CreateCellStyle();
            styleBodyBold.CloneStyleFrom(sheet.GetRow(9).Cells[1].CellStyle);
            //styleBodyBold.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#.###");

            ICellStyle styleEnd = templateWorkbook.CreateCellStyle();
            styleEnd.CloneStyleFrom(sheet.GetRow(8).Cells[2].CellStyle);

            foreach (var item in dataDetails)
            {
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, NUM_CELL);
                
                rowCur.Cells[0].SetCellValue(item.Stt);
                rowCur.Cells[1].SetCellValue(item.NameExcel);
                rowCur.Cells[2].SetCellValue(item.Unit);
                rowCur.Cells[3].SetCellValue(item.Col1 == null ? "" : Convert.ToDecimal(item.Col1).ToStringVN());
                rowCur.Cells[4].SetCellValue(item.Col2 == null ? "" : Convert.ToDecimal(item.Col2).ToStringVN());
                rowCur.Cells[5].SetCellValue(item.Col3 == null ? "" : Convert.ToDecimal(item.Col3).ToStringVN());
                rowCur.Cells[6].SetCellValue(item.Col4 == null ? "" : Convert.ToDecimal(item.Col4).ToStringVN());
                rowCur.Cells[7].SetCellValue(item.Col5 == null ? "" : Convert.ToDecimal(item.Col5).ToStringVN());
                for (int i = 0; i < NUM_CELL; i++)
                {
                    if (item.IsBold)
                    {
                        if (i == 1 || i == 0 || i == 2)
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
                        if (i == 1 || i == 0 || i == 2)
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
                        if (i == 1 || i == 0 || i == 2)
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
                        if (i == 1 || i == 0 || i == 2)
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
        public void InsertDataToTableBM_02D2(IWorkbook templateWorkbook, ISheet sheet, IList<ReportModel> dataDetails, int startRow, int NUM_CELL)
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
                rowCur.Cells[0].SetCellValue(item.Name);
                rowCur.Cells[1].SetCellValue(item.Col1 == null ? 0 : Convert.ToDouble(item.Col1));
                rowCur.Cells[2].SetCellValue(item.Col2 == null ? 0 : Convert.ToDouble(item.Col2));
                rowCur.Cells[3].SetCellValue(item.Col3 == null ? 0 : Convert.ToDouble(item.Col3));
                rowCur.Cells[4].SetCellValue(item.Col4 == null ? 0 : Convert.ToDouble(item.Col4));
                rowCur.Cells[5].SetCellValue(item.Col5 == null ? 0 : Convert.ToDouble(item.Col5));
                rowCur.Cells[6].SetCellValue(item.Col6 == null ? 0 : Convert.ToDouble(item.Col6));
                rowCur.Cells[7].SetCellValue(item.Col7 == null ? 0 : Convert.ToDouble(item.Col7));
                rowCur.Cells[8].SetCellValue(item.Col8 == null ? 0 : Convert.ToDouble(item.Col8));
                rowCur.Cells[9].SetCellValue(item.Col9 == null ? 0 : Convert.ToDouble(item.Col9));

                for (int i = 0; i < NUM_CELL; i++)
                {
                    if (item.IsBold)
                    {
                        if (i == 0)
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
                        if (i == 0)
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
            rowEnd.Cells[NUM_CELL - 3].SetCellValue("Ngày  .......... tháng ........... năm..............");
            IRow rowEnd2 = ReportUtilities.CreateRow(ref sheet, startRow + 3, NUM_CELL);
            rowEnd2.Cells[NUM_CELL - 3].SetCellValue("Người đại diện ký, ghi rõ họ tên");
            rowEnd2.Cells[NUM_CELL - 3].CellStyle.SetFont(fontBold);

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
            rowEnd.Cells[11].SetCellValue("Ngày  .......... tháng ........... năm..............");
            IRow rowEnd2 = ReportUtilities.CreateRow(ref sheet, startRow + 3, NUM_CELL);
            rowEnd2.Cells[11].SetCellValue("Người đại diện ký, ghi rõ họ tên");
            rowEnd2.Cells[11].CellStyle.SetFont(fontBold);

        }

        public void InsertDataToTableBM_01E(IWorkbook templateWorkbook, ISheet sheet, IList<ReportModel> dataDetails, int startRow, int NUM_CELL)
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
                rowCur.Cells[7].SetCellValue(item.Col7 == null ? 0 : Convert.ToDouble(item.Col7));
                rowCur.Cells[8].SetCellValue(item.Col8 == null ? 0 : Convert.ToDouble(item.Col8));
                rowCur.Cells[9].SetCellValue(item.Col9 == null ? 0 : Convert.ToDouble(item.Col9));
                rowCur.Cells[10].SetCellValue(item.Col10 == null ? 0 : Convert.ToDouble(item.Col10));
                for (int i = 0; i < NUM_CELL; i++)
                {
                    if (item.IsBold)
                    {
                        if (i == 0)
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
                        if (i == 0)
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
            rowEnd.Cells[NUM_CELL - 3].SetCellValue("Ngày  .......... tháng ........... năm..............");
            IRow rowEnd2 = ReportUtilities.CreateRow(ref sheet, startRow + 3, NUM_CELL);
            rowEnd2.Cells[NUM_CELL - 3].SetCellValue("Người đại diện ký, ghi rõ họ tên");
            rowEnd2.Cells[NUM_CELL - 3].CellStyle.SetFont(fontBold);

        }
        public void InsertDataToTableBM_2107(IWorkbook templateWorkbook, ISheet sheet, IList<ReportModel> dataDetails, int startRow, int NUM_CELL)
        {
            ICellStyle styleCellBold = templateWorkbook.CreateCellStyle(); // chữ in đậm
            styleCellBold.CloneStyleFrom(sheet.GetRow(9).Cells[0].CellStyle);
            var fontBold = templateWorkbook.CreateFont();
            fontBold.Boldweight = (short)FontBoldWeight.Bold;
            fontBold.FontHeightInPoints = 13;
            fontBold.FontName = "Times New Roman";

            ICellStyle styleCellName = templateWorkbook.CreateCellStyle();
            styleCellName.CloneStyleFrom(sheet.GetRow(9).Cells[1].CellStyle);

            ICellStyle styleBody = templateWorkbook.CreateCellStyle();
            styleBody.CloneStyleFrom(sheet.GetRow(10).Cells[0].CellStyle);
            styleBody.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");
            ICellStyle styleBodyBold = templateWorkbook.CreateCellStyle();
            styleBodyBold.CloneStyleFrom(sheet.GetRow(10).Cells[1].CellStyle);
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
                rowCur.Cells[7].SetCellValue(item.Col7 == null ? 0 : Convert.ToDouble(item.Col7));
                rowCur.Cells[8].SetCellValue(item.Des);

                for (int i = 0; i < NUM_CELL; i++)
                {
                    if (item.IsBold)
                    {
                        if (i == 0 || i == NUM_CELL - 1)
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
                        if (i == 0 || i == NUM_CELL - 1)
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
            rowEnd.Cells[NUM_CELL - 3].SetCellValue("Ngày  .......... tháng ........... năm..............");
            IRow rowEnd2 = ReportUtilities.CreateRow(ref sheet, startRow + 3, NUM_CELL);
            rowEnd2.Cells[NUM_CELL - 3].SetCellValue("Người đại diện ký, ghi rõ họ tên");
            rowEnd2.Cells[NUM_CELL - 3].CellStyle.SetFont(fontBold);

        }

        public void InsertDataToTableBM_2108(IWorkbook templateWorkbook, ISheet sheet, IList<ReportModel> dataDetails, int startRow, int NUM_CELL)
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
                rowCur.Cells[0].SetCellValue(item.Name);
                rowCur.Cells[1].SetCellValue(item.Col1 == null ? 0 : Convert.ToDouble(item.Col1));
                rowCur.Cells[2].SetCellValue(item.Col2 == null ? 0 : Convert.ToDouble(item.Col2));
                rowCur.Cells[3].SetCellValue(item.Col3 == null ? 0 : Convert.ToDouble(item.Col3));
                rowCur.Cells[4].SetCellValue(item.Col4 == null ? 0 : Convert.ToDouble(item.Col4));
                rowCur.Cells[5].SetCellValue(item.Col5 == null ? 0 : Convert.ToDouble(item.Col5));
                rowCur.Cells[6].SetCellValue(item.Col6 == null ? 0 : Convert.ToDouble(item.Col6));
                rowCur.Cells[7].SetCellValue(item.Col7 == null ? 0 : Convert.ToDouble(item.Col7));
                rowCur.Cells[8].SetCellValue(item.Col8 == null ? 0 : Convert.ToDouble(item.Col8));
                rowCur.Cells[9].SetCellValue(item.Col9 == null ? 0 : Convert.ToDouble(item.Col9));
                rowCur.Cells[10].SetCellValue(item.Col10 == null ? 0 : Convert.ToDouble(item.Col10));
                rowCur.Cells[11].SetCellValue(item.Des);


                for (int i = 0; i < NUM_CELL; i++)
                {
                    if (item.IsBold)
                    {
                        if (i == 0 || i == NUM_CELL - 1)
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
                        if (i == 0 || i == NUM_CELL - 1)
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
            rowEnd.Cells[NUM_CELL - 3].SetCellValue("Ngày  .......... tháng ........... năm..............");
            IRow rowEnd2 = ReportUtilities.CreateRow(ref sheet, startRow + 3, NUM_CELL);
            rowEnd2.Cells[NUM_CELL - 3].SetCellValue("Người đại diện ký, ghi rõ họ tên");
            rowEnd2.Cells[NUM_CELL - 3].CellStyle.SetFont(fontBold);

        }

        public void InsertDataToTableBM_2109(IWorkbook templateWorkbook, ISheet sheet, IList<ReportModel> dataDetails, int startRow, int NUM_CELL)
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
                rowCur.Cells[6].SetCellValue(item.Des);


                for (int i = 0; i < NUM_CELL; i++)
                {
                    if (item.IsBold)
                    {
                        if (i == 0 || i == NUM_CELL - 1)
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
                        if (i == 0 || i == NUM_CELL - 1)
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
            rowEnd.Cells[NUM_CELL - 3].SetCellValue("Ngày  .......... tháng ........... năm..............");
            IRow rowEnd2 = ReportUtilities.CreateRow(ref sheet, startRow + 3, NUM_CELL);
            rowEnd2.Cells[NUM_CELL - 3].SetCellValue("Người đại diện ký, ghi rõ họ tên");
            rowEnd2.Cells[NUM_CELL - 3].CellStyle.SetFont(fontBold);

        }

        public void InsertDataToTableBM_2110(IWorkbook templateWorkbook, ISheet sheet, IList<ReportModel> dataDetails, int startRow, int NUM_CELL)
        {
            ICellStyle styleCellBold = templateWorkbook.CreateCellStyle(); // chữ in đậm
            styleCellBold.CloneStyleFrom(sheet.GetRow(4).Cells[0].CellStyle);
            var fontBold = templateWorkbook.CreateFont();
            fontBold.Boldweight = (short)FontBoldWeight.Bold;
            fontBold.FontHeightInPoints = 13;
            fontBold.FontName = "Times New Roman";

            ICellStyle styleCellName = templateWorkbook.CreateCellStyle();
            styleCellName.CloneStyleFrom(sheet.GetRow(4).Cells[1].CellStyle);

            ICellStyle styleBody = templateWorkbook.CreateCellStyle();
            styleBody.CloneStyleFrom(sheet.GetRow(5).Cells[0].CellStyle);
            styleBody.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");
            ICellStyle styleBodyBold = templateWorkbook.CreateCellStyle();
            styleBodyBold.CloneStyleFrom(sheet.GetRow(5).Cells[1].CellStyle);
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
                rowCur.Cells[6].SetCellValue(item.Des);


                for (int i = 0; i < NUM_CELL; i++)
                {
                    if (item.IsBold)
                    {
                        if (i == 0 || i == NUM_CELL - 1)
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
                        if (i == 0 || i == NUM_CELL - 1)
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
            rowEnd.Cells[NUM_CELL - 3].SetCellValue("Ngày  .......... tháng ........... năm..............");
            IRow rowEnd2 = ReportUtilities.CreateRow(ref sheet, startRow + 3, NUM_CELL);
            rowEnd2.Cells[NUM_CELL - 3].SetCellValue("Người đại diện ký, ghi rõ họ tên");
            rowEnd2.Cells[NUM_CELL - 3].CellStyle.SetFont(fontBold);

        }
        public ReportDataCenter GenDataBM01D(int year, string kichBan)
        {
            try
            {
                var data = new ReportDataCenter();

                data.BM01D = GetElementsBM01D(year);

                foreach (var d in data.BM01D)
                {
                    d.Col7 = d.Col6 == 0 || d.Col3 == 0 ? 0 : d.Col6 / d.Col3 * 100;
                    d.Col12 = d.Col11 == 0 || d.Col8 == 0 ? 0 : d.Col11 / d.Col8 * 100;
                }

                return data;
            }
            catch (Exception ex)
            {
                return new ReportDataCenter();
            }
        }

        public List<ReportModel> GetElementsBM01D(int year)
        {
            try
            {
                var data = new List<ReportModel>();
                var projects = UnitOfWork.Repository<ProjectRepo>().Queryable().Where(x => x.YEAR == year).ToList();

                data.Add(new ReportModel
                {
                    Id = "A",
                    Stt = "A",
                    Name = "Dự án chuyển tiếp kỳ truớc",
                    NameExcel = "Dự án chuyển tiếp kỳ truớc",
                    IsBold = true,
                });
                data.Add(new ReportModel
                {
                    Id = "A.I",
                    Stt = "I",
                    Name = "Đầu tư trang thiết bị",
                    NameExcel = "Đầu tư trang thiết bị",
                    Parent = "A",
                    IsBold = true,
                });

                data.Add(new ReportModel
                {
                    Id = "A.I.1",
                    Stt = "1",
                    Name = "Các dự án chuẩn bị đầu tư",
                    NameExcel = "Các dự án chuẩn bị đầu tư",
                    Parent = "A.I",
                    IsBold = true,
                });
                var orderAI1 = 1;
                foreach (var p in projects.Where(x => x.LOAI_HINH == "TTB" && x.CHUAN_BI_DAU_TU == true && x.CHUYEN_TIEP == true && x.TYPE == "TTB-LON"))
                {
                    data.Add(new ReportModel
                    {
                        Id = "A.I.1" + p.CODE,
                        Stt = "1." + orderAI1.ToString(),
                        Name = p.NAME,
                        NameExcel = "Các dự án chuẩn bị đầu tư",
                        Parent = "A.I.1",
                    });
                    orderAI1 += 1;
                }

                data.Add(new ReportModel
                {
                    Id = "A.I.2",
                    Stt = "2",
                    Name = "Các dự án thực hiện đầu tư",
                    NameExcel = "Các dự án thực hiện đầu tư",
                    Parent = "A.I",
                    IsBold = true,
                });
                var orderAI2 = 1;
                foreach (var p in projects.Where(x => x.LOAI_HINH == "TTB" && x.THUC_HIEN_DAU_TU == true && x.CHUYEN_TIEP == true && x.TYPE == "TTB-LON"))
                {
                    data.Add(new ReportModel
                    {
                        Id = "A.I.2" + p.CODE,
                        Stt = "2." + orderAI2.ToString(),
                        Name = p.NAME,
                        NameExcel = "Các dự án chuẩn bị đầu tư",
                        Parent = "A.I.2",
                    });
                    orderAI2 += 1;
                }


                data.Add(new ReportModel
                {
                    Id = "A.I.3",
                    Stt = "3",
                    Name = "Đầu tư trang thiết bị lẻ",
                    NameExcel = "Đầu tư trang thiết bị lẻ",
                    Parent = "A.I",
                    IsBold = true,
                });

                var orderAI3 = 1;
                foreach (var p in projects.Where(x => x.LOAI_HINH == "TTB" && x.TYPE == "TTB-LE" && x.CHUYEN_TIEP == true))
                {
                    data.Add(new ReportModel
                    {
                        Id = "A.I.3" + p.CODE,
                        Stt = "3." + orderAI3.ToString(),
                        Name = p.NAME,
                        NameExcel = "Các dự án chuẩn bị đầu tư",
                        Parent = "A.I.3",
                    });
                    orderAI3 += 1;
                }

                data.Add(new ReportModel
                {
                    Id = "A.II",
                    Stt = "II",
                    Name = "Đầu tư xây dựng cơ bản",
                    NameExcel = "Đầu tư xây dựng cơ bản",
                    Parent = "A",
                    IsBold = true,
                });
                data.Add(new ReportModel
                {
                    Id = "A.II.1",
                    Stt = "1",
                    Name = "Các dự án chuẩn bị đầu tư",
                    NameExcel = "Các dự án chuẩn bị đầu tư",
                    Parent = "A.II",
                    IsBold = true,
                });

                var orderAII1 = 1;
                foreach (var p in projects.Where(x => x.LOAI_HINH == "XDCB" && x.CHUYEN_TIEP == true && x.CHUAN_BI_DAU_TU == true))
                {
                    data.Add(new ReportModel
                    {
                        Id = "A.II.1" + p.CODE,
                        Stt = "1." + orderAII1.ToString(),
                        Name = p.NAME,
                        NameExcel = "Các dự án chuẩn bị đầu tư",
                        Parent = "A.II.1",
                    });
                    orderAII1 += 1;
                }

                data.Add(new ReportModel
                {
                    Id = "A.II.2",
                    Stt = "2",
                    Name = "Các dự án thực hiện đầu tư",
                    NameExcel = "Các dự án thực hiện đầu tư",
                    Parent = "A.II",
                    IsBold = true,
                });

                var orderAII2 = 1;
                foreach (var p in projects.Where(x => x.LOAI_HINH == "XDCB" && x.CHUYEN_TIEP == true && x.THUC_HIEN_DAU_TU == true))
                {
                    data.Add(new ReportModel
                    {
                        Id = "A.II.2" + p.CODE,
                        Stt = "2." + orderAII2.ToString(),
                        Name = p.NAME,
                        NameExcel = "Các dự án chuẩn bị đầu tư",
                        Parent = "A.II.2",
                    });
                    orderAII2 += 1;
                }

                data.Add(new ReportModel
                {
                    Id = "B",
                    Stt = "B",
                    Name = "Dự án đầu tư mới",
                    NameExcel = "Dự án đầu tư mới",
                    IsBold = true,
                });
                data.Add(new ReportModel
                {
                    Id = "B.I",
                    Stt = "I",
                    Name = "Đầu tư trang thiết bị",
                    NameExcel = "Đầu tư trang thiết bị",
                    Parent = "B",
                    IsBold = true,
                });
                data.Add(new ReportModel
                {
                    Id = "B.I.1",
                    Stt = "1",
                    Name = "Các dự án chuẩn bị đầu tư",
                    NameExcel = "Các dự án chuẩn bị đầu tư",
                    Parent = "B.I",
                    IsBold = true,
                });

                var orderBI1 = 1;
                foreach (var p in projects.Where(x => x.LOAI_HINH == "TTB" && x.CHUAN_BI_DAU_TU == true && x.DAU_TU_MOI == true && x.TYPE == "TTB-LON"))
                {
                    data.Add(new ReportModel
                    {
                        Id = "B.I.1" + p.CODE,
                        Stt = "1." + orderBI1.ToString(),
                        Name = p.NAME,
                        NameExcel = "Các dự án chuẩn bị đầu tư",
                        Parent = "B.I.1",
                    }); ; ;
                    orderBI1 += 1;
                }

                data.Add(new ReportModel
                {
                    Id = "B.I.2",
                    Stt = "2",
                    Name = "Các dự án thực hiện đầu tư",
                    NameExcel = "Các dự án thực hiện đầu tư",
                    Parent = "B.I",
                    IsBold = true,
                });

                var orderBI2 = 1;
                foreach (var p in projects.Where(x => x.LOAI_HINH == "TTB" && x.THUC_HIEN_DAU_TU == true && x.DAU_TU_MOI == true && x.TYPE == "TTB-LON"))
                {
                    data.Add(new ReportModel
                    {
                        Id = "B.I.2" + p.CODE,
                        Stt = "2." + orderBI2.ToString(),
                        Name = p.NAME,
                        NameExcel = "Các dự án thực hiện đầu tư",
                        Parent = "B.I.2",
                    });
                    orderBI2 += 1;
                }

                data.Add(new ReportModel
                {
                    Id = "B.I.3",
                    Stt = "3",
                    Name = "Đầu tư trang thiết bị lẻ",
                    NameExcel = "Đầu tư trang thiết bị lẻ",
                    Parent = "B.I",
                    IsBold = true,
                });

                var orderBI3 = 1;
                foreach (var p in projects.Where(x => x.LOAI_HINH == "TTB" && x.TYPE == "TTB-LE" && x.DAU_TU_MOI == true))
                {
                    data.Add(new ReportModel
                    {
                        Id = "B.I.3" + p.CODE,
                        Stt = "3." + orderBI3.ToString(),
                        Name = p.NAME,
                        NameExcel = "Các dự án chuẩn bị đầu tư",
                        Parent = "A.I.3",
                    });
                    orderBI3 += 1;
                }

                data.Add(new ReportModel
                {
                    Id = "B.II",
                    Stt = "II",
                    Name = "Đầu tư xây dựng cơ bản",
                    NameExcel = "Đầu tư xây dựng cơ bản",
                    Parent = "B",
                    IsBold = true,
                });
                data.Add(new ReportModel
                {
                    Id = "B.II.1",
                    Stt = "1",
                    Name = "Các dự án chuẩn bị đầu tư",
                    NameExcel = "Các dự án chuẩn bị đầu tư",
                    Parent = "B.II",
                    IsBold = true,
                });
                var orderBII1 = 1;
                foreach (var p in projects.Where(x => x.LOAI_HINH == "XDCB" && x.DAU_TU_MOI == true && x.CHUAN_BI_DAU_TU == true))
                {
                    data.Add(new ReportModel
                    {
                        Id = "B.II.1" + p.CODE,
                        Stt = "1." + orderBII1.ToString(),
                        Name = p.NAME,
                        NameExcel = "Các dự án chuẩn bị đầu tư",
                        Parent = "B.II.1",
                    });
                    orderBII1 += 1;
                }

                data.Add(new ReportModel
                {
                    Id = "B.II.2",
                    Stt = "2",
                    Name = "Các dự án thực hiện đầu tư",
                    NameExcel = "Các dự án thực hiện đầu tư",
                    Parent = "B.II",
                    IsBold = true,
                });
                var orderBII2 = 1;
                foreach (var p in projects.Where(x => x.LOAI_HINH == "XDCB" && x.DAU_TU_MOI == true && x.THUC_HIEN_DAU_TU == true))
                {
                    data.Add(new ReportModel
                    {
                        Id = "B.II.2" + p.CODE,
                        Stt = "2." + orderBII2.ToString(),
                        Name = p.NAME,
                        NameExcel = "Các dự án chuẩn bị đầu tư",
                        Parent = "B.II.2",
                    });
                    orderBII2 += 1;
                }

                return data;
            }
            catch (Exception ex)
            {
                return new List<ReportModel>();
            }
        }
        public ReportDataCenter GenDataBM02A(int year)
        {
            try
            {
                var data = new ReportDataCenter();
                data.BM02A = GetElementsBM01D(year);

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
        public ReportDataCenter GenDataBM01E(int year, string kichBan)
        {
            try
            {
                var data = new ReportDataCenter();
                var lstProject = UnitOfWork.Repository<ProjectRepo>().Queryable().Where(x => x.YEAR > 0 && x.LOAI_HINH == "NDN").ToList();

                var template = UnitOfWork.Repository<DauTuNgoaiDoanhNghiepRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.STATUS == "03" && x.KICH_BAN == kichBan && x.PHIEN_BAN == "PB5").Select(x => x.TEMPLATE_CODE).ToList();
                var dataCurrent = UnitOfWork.Repository<DauTuNgoaiDoanhNghiepDataRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.DauTuNgoaiDoanhNghiepProfitCenter != null && template.Contains(x.TEMPLATE_CODE)).ToList();

                var templateKH = UnitOfWork.Repository<DauTuNgoaiDoanhNghiepRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.STATUS == "03" && x.KICH_BAN == kichBan && x.PHIEN_BAN == "PB1").Select(x => x.TEMPLATE_CODE).ToList();
                var dataCurrentKH = UnitOfWork.Repository<DauTuNgoaiDoanhNghiepDataRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.DauTuNgoaiDoanhNghiepProfitCenter != null && templateKH.Contains(x.TEMPLATE_CODE)).ToList();



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
                        Col2 = dataCurrentKH.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col4 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col6 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_3),
                        Col7 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_4),
                        Col8 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_5),
                        Col9 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_6),
                        Des = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).FirstOrDefault()?.DESCRIPTION,
                    });
                    data.BM02A.Add(new ReportModel
                    {
                        Id = prj.CODE + "5102",
                        Stt = prj.CODE + "5102",
                        Name = "- Giá trị đầu tư theo mệnh giá",
                        Parent = prj.CODE,
                        Col1 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_1),
                        Col2 = dataCurrentKH.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col4 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col6 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_3),
                        Col7 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_4),
                        Col8 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_5),
                        Col9 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_6),
                        Des = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).FirstOrDefault()?.DESCRIPTION,
                    });
                    data.BM02A.Add(new ReportModel
                    {
                        Id = prj.CODE + "5103",
                        Stt = prj.CODE + "5103",
                        Name = "- Số lượng cổ phiếu (nếu đầu tư vào Cty cổ phần)",
                        Parent = prj.CODE,
                        Col1 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_1),
                        Col2 = dataCurrentKH.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col4 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col6 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_3),
                        Col7 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_4),
                        Col8 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_5),
                        Col9 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_6),
                        Des = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).FirstOrDefault()?.DESCRIPTION,
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
                        Col2 = dataCurrentKH.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col4 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col6 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_3),
                        Col7 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_4),
                        Col8 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_5),
                        Col9 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_6),
                        Des = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).FirstOrDefault()?.DESCRIPTION,
                    });
                    data.BM02A.Add(new ReportModel
                    {
                        Id = prj.CODE + "5102",
                        Stt = prj.CODE + "5102",
                        Name = "- Giá trị đầu tư theo mệnh giá",
                        Parent = prj.CODE,
                        Col1 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_1),
                        Col2 = dataCurrentKH.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col4 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col6 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_3),
                        Col7 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_4),
                        Col8 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_5),
                        Col9 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_6),
                        Des = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).FirstOrDefault()?.DESCRIPTION,
                    });
                    data.BM02A.Add(new ReportModel
                    {
                        Id = prj.CODE + "5103",
                        Stt = prj.CODE + "5103",
                        Name = "- Số lượng cổ phiếu (nếu đầu tư vào Cty cổ phần)",
                        Parent = prj.CODE,
                        Col1 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_1),
                        Col2 = dataCurrentKH.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col4 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col6 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_3),
                        Col7 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_4),
                        Col8 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_5),
                        Col9 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_6),
                        Des = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).FirstOrDefault()?.DESCRIPTION,
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
                        Col2 = dataCurrentKH.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col4 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col6 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_3),
                        Col7 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_4),
                        Col8 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_5),
                        Col9 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_6),
                        Des = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).FirstOrDefault()?.DESCRIPTION,
                    });
                    data.BM02A.Add(new ReportModel
                    {
                        Id = prj.CODE + "5102",
                        Stt = prj.CODE + "5102",
                        Name = "- Giá trị đầu tư theo mệnh giá",
                        Parent = prj.CODE,
                        Col1 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_1),
                        Col2 = dataCurrentKH.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col4 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col6 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_3),
                        Col7 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_4),
                        Col8 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_5),
                        Col9 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_6),
                        Des = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).FirstOrDefault()?.DESCRIPTION,
                    });
                    data.BM02A.Add(new ReportModel
                    {
                        Id = prj.CODE + "5103",
                        Stt = prj.CODE + "5103",
                        Name = "- Số lượng cổ phiếu (nếu đầu tư vào Cty cổ phần)",
                        Parent = prj.CODE,
                        Col1 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_1),
                        Col2 = dataCurrentKH.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col4 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col6 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_3),
                        Col7 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_4),
                        Col8 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_5),
                        Col9 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_6),
                        Des = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).FirstOrDefault()?.DESCRIPTION,
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
                        Col2 = dataCurrentKH.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col4 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col6 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_3),
                        Col7 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_4),
                        Col8 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_5),
                        Col9 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_6),
                        Des = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).FirstOrDefault()?.DESCRIPTION,
                    });
                    data.BM02A.Add(new ReportModel
                    {
                        Id = prj.CODE + "5102",
                        Stt = prj.CODE + "5102",
                        Name = "- Giá trị đầu tư theo mệnh giá",
                        Parent = prj.CODE,
                        Col1 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_1),
                        Col2 = dataCurrentKH.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col4 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col6 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_3),
                        Col7 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_4),
                        Col8 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_5),
                        Col9 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_6),
                        Des = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).FirstOrDefault()?.DESCRIPTION,
                    });
                    data.BM02A.Add(new ReportModel
                    {
                        Id = prj.CODE + "5103",
                        Stt = prj.CODE + "5103",
                        Name = "- Số lượng cổ phiếu (nếu đầu tư vào Cty cổ phần)",
                        Parent = prj.CODE,
                        Col1 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_1),
                        Col2 = dataCurrentKH.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col4 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col6 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_3),
                        Col7 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_4),
                        Col8 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_5),
                        Col9 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_6),
                        Des = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).FirstOrDefault()?.DESCRIPTION,
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
                        Col2 = dataCurrentKH.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col4 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col6 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_3),
                        Col7 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_4),
                        Col8 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_5),
                        Col9 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_6),
                        Des = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).FirstOrDefault()?.DESCRIPTION,
                    });
                    data.BM02A.Add(new ReportModel
                    {
                        Id = prj.CODE + "5102",
                        Stt = prj.CODE + "5102",
                        Name = "- Giá trị đầu tư theo mệnh giá",
                        Parent = prj.CODE,
                        Col1 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_1),
                        Col2 = dataCurrentKH.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col4 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col6 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_3),
                        Col7 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_4),
                        Col8 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_5),
                        Col9 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_6),
                        Des = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).FirstOrDefault()?.DESCRIPTION,
                    });
                    data.BM02A.Add(new ReportModel
                    {
                        Id = prj.CODE + "5103",
                        Stt = prj.CODE + "5103",
                        Name = "- Số lượng cổ phiếu (nếu đầu tư vào Cty cổ phần)",
                        Parent = prj.CODE,
                        Col1 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_1),
                        Col2 = dataCurrentKH.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col4 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col6 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_3),
                        Col7 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_4),
                        Col8 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_5),
                        Col9 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_6),
                        Des = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).FirstOrDefault()?.DESCRIPTION,
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
                        Col2 = dataCurrentKH.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col4 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col6 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_3),
                        Col7 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_4),
                        Col8 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_5),
                        Col9 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_6),
                        Des = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).FirstOrDefault()?.DESCRIPTION,
                    });
                    data.BM02A.Add(new ReportModel
                    {
                        Id = prj.CODE + "5102",
                        Stt = prj.CODE + "5102",
                        Name = "- Giá trị đầu tư theo mệnh giá",
                        Parent = prj.CODE,
                        Col1 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_1),
                        Col2 = dataCurrentKH.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col4 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col6 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_3),
                        Col7 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_4),
                        Col8 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_5),
                        Col9 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_6),
                        Des = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5102" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).FirstOrDefault()?.DESCRIPTION,
                    });
                    data.BM02A.Add(new ReportModel
                    {
                        Id = prj.CODE + "5103",
                        Stt = prj.CODE + "5103",
                        Name = "- Số lượng cổ phiếu (nếu đầu tư vào Cty cổ phần)",
                        Parent = prj.CODE,
                        Col1 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_1),
                        Col2 = dataCurrentKH.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col4 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5101" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_2),
                        Col6 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_3),
                        Col7 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_4),
                        Col8 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_5),
                        Col9 = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).Sum(x => x.VALUE_6),
                        Des = dataCurrent.Where(x => x.KHOAN_MUC_DAU_TU_CODE == "5103" && x.DauTuNgoaiDoanhNghiepProfitCenter.PROJECT_CODE == prj.CODE).FirstOrDefault()?.DESCRIPTION,
                    });
                }

                foreach (var d in data.BM02A)
                {
                    d.Col5 = d.Col4 == 0 || d.Col2 == 0 ? 0 : d.Col4 / d.Col2;
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
                Stt = "I",
                Id = "I",
                Name = "Sản lượng",
                NameExcel = "Sản lượng",
                IsBold = true,
            },
            new ReportModel()
            {
                Stt = "1",
                Id = "I.1",
                Parent = "I",
                Name = "Cung ứng cho VNA Group",
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
                Stt = "2",
                Id = "I.2",
                Parent = "I",
                Name = "Cung ứng cho đối tác khác (*)",
                Col1 = dataSL_TH_5.Where(x => x.SanLuongProfitCenter.HangHangKhong.IS_VNA == false).Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                Col2 = dataSL_KH_1.Where(x => x.SanLuongProfitCenter.HangHangKhong.IS_VNA == false).Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                Col3 = 0,
                NameExcel = "Cung ứng cho đối tác khác (*)",
                Col4 = dataSL_KH.Where(x => x.SanLuongProfitCenter.HangHangKhong.IS_VNA == false).Sum(x => x.VALUE_SUM_YEAR) ?? 0
            },
            new ReportModel()
            {
                Stt = "II",
                Id = "II",
                Name = "Doanh thu từ hoạt động SXKD",
                NameExcel = "Doanh thu từ hoạt động SXKD",

                IsBold = true,
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Stt = "1",
                Id = "II.1",
                Parent = "II",
                Name = "Doanh thu cung ứng cho VNA Group",
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
                Stt = "2",
                Id = "II.2",
                Parent = "II",
                Name = "Doanh thu cung ứng cho đối tác khác (*)",
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
                Stt = "III",
                Id = "III",
                Name = "Các khoản chi phí",
                NameExcel = "Các khoản chi phí",

                Unit = "Tr.đ/USD",
                IsBold = true
            },
            new ReportModel()
            {
                Stt = "1",
                Id = "III.1",
                Parent = "III",
                NameExcel = "Chi phí dịch vụ mua ngoài",

                Name = "Chi phí dịch vụ mua ngoài",
                Unit = "Tr.đ/USD",
            },
            new ReportModel()
            {
                Stt = "1.1",
                Id = "III.1.1",
                Parent = "III.1",
                Name = "Chi phí bảo hiểm tài sản",
                NameExcel = "Chi phí bảo hiểm tài sản",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G001")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G001")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G001")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "1.2",
                Id = "III.1.2",
                Parent = "III.1",
                Name = "Thuê sửa chữa nhà cửa VKT",
                NameExcel = "Thuê sửa chữa nhà cửa VKT",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G002")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G002")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G002")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "1.3",
                Id = "III.1.3",
                Parent = "III.1",
                Name = "Thuê sửa chữa máy móc thiết bị",
                NameExcel = "Thuê sửa chữa máy móc thiết bị",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G003")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G003")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G003")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "1.4",
                Id = "III.1.4",
                Parent = "III.1",
                Name = "Thuê sửa chữa PTVT",
                NameExcel = "Thuê sửa chữa PTVT",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G004")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G004")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G004")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "1.5",
                Id = "III.1.5",
                Parent = "III.1",
                Name = "Thuê sửa chữa thiết bị quản lý",
                NameExcel = "Thuê sửa chữa thiết bị quản lý",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G005")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G005")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G005")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "1.6",
                Id = "III.1.6",
                Parent = "III.1",
                Name = "Thuê sửa chữa kho bể",
                NameExcel = "Thuê sửa chữa kho bể",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G006")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G006")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G006")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "1.7",
                Id = "III.1.7",
                Parent = "III.1",
                Name = "Thuê sửa chữa TSCĐ khác",
                NameExcel = "Thuê sửa chữa TSCĐ khác",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G007")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G007")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G007")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "1.8",
                Id = "III.1.8",
                Parent = "III.1",
                Name = "Thuê cửa hàng kho bãi",
                NameExcel = "Thuê cửa hàng kho bãi",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G008")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G008")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G008")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "1.9",
                Id = "III.1.9",
                Parent = "III.1",
                Name = "Thuê vận chuyển",
                NameExcel = "Thuê vận chuyển",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G009")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G009")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G009")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "1.10",
                Id = "III.1.10",
                Parent = "III.1",
                Name = "Tiền điện mua ngoài",
                NameExcel = "Tiền điện mua ngoài",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G010")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G010")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G010")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "1.11",
                Id = "III.1.11",
                Parent = "III.1",
                Name = "Tiền nước mua ngoài",
                NameExcel = "Tiền nước mua ngoài",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G011")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G011")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G011")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "1.12",
                Id = "III.1.12",
                Parent = "III.1",
                Name = "Cước thông tin liên lạc",
                NameExcel = "Cước thông tin liên lạc",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G012")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G012")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G012")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "1.13",
                Id = "III.1.13",
                Parent = "III.1",
                Name = "Chi phí dịch vụ mua ngoài khác",
                NameExcel = "Chi phí dịch vụ mua ngoài khác",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G019")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G019")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G019")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "2",
                Id = "III.2",
                Parent = "III",
                Name = "Chi khác bằng tiền",
                NameExcel = "Chi khác bằng tiền",

                Unit = "Tr.đ/USD",
            },
            new ReportModel()
            {
                Stt = "2.1",
                Id = "III.2.1",
                Parent = "III.2",
                Name = "Chi ANAT, PCBT, PCCC",
                NameExcel = "Chi ANAT, PCBT, PCCC",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H001")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H001")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H001")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "2.2",
                Id = "III.2.2",
                Parent = "III.2",
                Name = "Chi phí trang phục ngành",
                NameExcel = "Chi phí trang phục ngành",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H002")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H002")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H002")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "2.3",
                Id = "III.2.3",
                Parent = "III.2",
                Name = "Chi giao dịch tiếp khách",
                NameExcel = "Chi giao dịch tiếp khách",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H004")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H004")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H004")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "2.4",
                Id = "III.2.4",
                Parent = "III.2",
                Name = "Chi quảng cáo, marketing",
                NameExcel = "Chi quảng cáo, marketing",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H014")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H014")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H014")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "2.5",
                Id = "III.2.5",
                Parent = "III.2",
                Name = "Chi hoa hồng môi giới",
                NameExcel = "Chi hoa hồng môi giới",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H005")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H005")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H005")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "2.6",
                Id = "III.2.6",
                Parent = "III.2",
                Name = "Chi đào tạo",
                NameExcel = "Chi đào tạo",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H006")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H006")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H006")).Sum(x => x.AMOUNT) ?? 0,
            },

            new ReportModel()
            {
                Stt = "2.7",
                Id = "II.2.7",
                Parent = "III.2",
                Name = "Công tác phí, phép",
                NameExcel = "Công tác phí, phép",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H007")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H007")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H007")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "2.8",
                Id = "III.2.8",
                Parent = "III.2",
                Name = "Lệ phí cầu đường",
                NameExcel = "Lệ phí cầu đường",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H003")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H003")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H003")).Sum(x => x.AMOUNT) ?? 0,
            },

            new ReportModel()
            {
                Stt = "2.9",
                Id = "III.2.9",
                Parent = "III.2",
                Name = "Chi bồi dưỡng độc hại",
                NameExcel = "Chi bồi dưỡng độc hại",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H008")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H008")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H008")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "2.10",
                Id = "III.2.10",
                Parent = "III.2",
                Name = "Phí nhượng quyền khai thác",
                NameExcel = "Phí nhượng quyền khai thác",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H009")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H009")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H009")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "2.11",
                Id = "III.2.11",
                Parent = "III.2",
                Name = "Chi VSCN, y tế, môi trường",
                NameExcel = "Chi VSCN, y tế, môi trường",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H010")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H010")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H010")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "2.12",
                Id = "III.2.12",
                Parent = "III.2",
                Name = "Phí ngân hàng",
                NameExcel = "Phí ngân hàng",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H015")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H015")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H015")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "2.13",
                Id = "III.2.13",
                Parent = "III.2",
                Name = "Khoản chi có tính chất phúc lợi",
                NameExcel = "Khoản chi có tính chất phúc lợi",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_TH_5.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H018")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_KH_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H018")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_KH.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H018")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "2.14",
                Id = "III.2.14",
                Parent = "III.2",
                Name = "Chi bằng tiền khác",
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
        public List<ReportModel> GenDataBM2107(int year, int month, string phienBan, string kichBan, string area)
        {
            try
            {
                var data = new List<ReportModel>();


                var header1_1 = UnitOfWork.Repository<DauTuXayDungRepo>().Queryable().Where(x => x.PHIEN_BAN == "PB1" && x.KICH_BAN == kichBan && x.TIME_YEAR == year && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var details1_1 = UnitOfWork.Repository<DauTuXayDungDataRepo>().Queryable().Where(x => header1_1.Contains(x.TEMPLATE_CODE)).ToList();

                var headerTH = UnitOfWork.Repository<DauTuXayDungRepo>().Queryable().Where(x => x.PHIEN_BAN == "PB5" && x.KICH_BAN == kichBan && x.TIME_YEAR == year && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var detailsTH = UnitOfWork.Repository<DauTuXayDungDataRepo>().Queryable().Where(x => headerTH.Contains(x.TEMPLATE_CODE)).ToList();


                var projects = UnitOfWork.Repository<ProjectRepo>().Queryable().Where(x => x.LOAI_HINH == "XDCB" && x.YEAR == year).ToList();
                projects = string.IsNullOrEmpty(area) ? projects : projects.Where(x => x.AREA_CODE == area).ToList();
                var order = 1;
                data.Add(new ReportModel
                {
                    Name = "Tổng kinh phí đầu tư XDCB",
                    Col1 = details1_1.Where(x => projects.Select(y => y.CODE).Contains(x.DauTuXayDungProfitCenter.PROJECT_CODE)).Sum(x => x.VALUE_1) ?? 0,
                    IsBold = true,
                });

                foreach (var project in projects)
                {
                    var i = new ReportModel
                    {
                        Stt = order.ToString(),
                        Name = project.NAME,
                        Col1 = details1_1.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == project.CODE).Sum(x => x.VALUE_1) ?? 0
                    };
                    data.Add(i);

                    if (project.TYPE == "TTB-LON" || string.IsNullOrEmpty(project.TYPE))
                    {
                        data.Add(new ReportModel
                        {
                            Stt = "a",
                            Name = "Chuẩn bị đầu tư và chuẩn bị thực hiện dự án",
                            Col1 = details1_1.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == project.CODE && x.KHOAN_MUC_DAU_TU_CODE == "CBDT").Sum(x => x.VALUE_1) ?? 0,
                       
                        });
                        data.Add(new ReportModel
                        {
                            Stt = "b",
                            Name = "Thực hiện dự án",
                            Col1 = details1_1.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == project.CODE && x.KHOAN_MUC_DAU_TU_CODE == "TTDT").Sum(x => x.VALUE_1) ?? 0,
                        });

                    }
                    order++;
                }
                return data;

            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                return new List<ReportModel>();
            }
        }
        public List<ReportModel> GenDataBM2108(int year, int month, string phienBan, string kichBan, string area)
        {
            try
            {
                var data = new List<ReportModel>();


                var header1_1 = UnitOfWork.Repository<DauTuTrangThietBiRepo>().Queryable().Where(x => x.PHIEN_BAN == "PB1" && x.KICH_BAN == kichBan && x.TIME_YEAR == year && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var details1_1 = UnitOfWork.Repository<DauTuTrangThietBiDataRepo>().Queryable().Where(x => header1_1.Contains(x.TEMPLATE_CODE)).ToList();

                var headerTH = UnitOfWork.Repository<DauTuTrangThietBiRepo>().Queryable().Where(x => x.PHIEN_BAN == "PB5" && x.KICH_BAN == kichBan && x.TIME_YEAR == year && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var detailsTH = UnitOfWork.Repository<DauTuTrangThietBiDataRepo>().Queryable().Where(x => headerTH.Contains(x.TEMPLATE_CODE)).ToList();


                var projects = UnitOfWork.Repository<ProjectRepo>().Queryable().Where(x => x.LOAI_HINH == "TTB" && x.YEAR == year && x.AREA_CODE == area).OrderByDescending(x => x.TYPE).ToList();
                projects = string.IsNullOrEmpty(area) ? projects : projects.Where(x => x.AREA_CODE == area).ToList();
                var order = 1;
                data.Add(new ReportModel
                {
                    Name = "Tổng kinh phí đầu tư XDCB",
                    Col1 = details1_1.Where(x => projects.Select(y => y.CODE).Contains(x.DauTuTrangThietBiProfitCenter.PROJECT_CODE)).Sum(x => x.VALUE_5) ?? 0,
                    IsBold = true,
                });

                foreach (var project in projects)
                {
                    var i = new ReportModel
                    {
                        Stt = order.ToString(),
                        Name = project.NAME,
                        Col1 = details1_1.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == project.CODE).Sum(x => x.VALUE_5) ?? 0
                    };
                    data.Add(i);

                    if (project.TYPE == "TTB-LON" || string.IsNullOrEmpty(project.TYPE))
                    {
                        data.Add(new ReportModel
                        {
                            Stt = "a",
                            Name = "Chuẩn bị đầu tư và chuẩn bị thực hiện dự án",
                            Col1 = details1_1.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == project.CODE && x.KHOAN_MUC_DAU_TU_CODE == "CBDT").Sum(x => x.VALUE_5) ?? 0,

                        });
                        data.Add(new ReportModel
                        {
                            Stt = "b",
                            Name = "Thực hiện dự án",
                            Col1 = details1_1.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == project.CODE && x.KHOAN_MUC_DAU_TU_CODE == "TTDT").Sum(x => x.VALUE_5) ?? 0,
                        });

                    }
                    order++;
                }
                return data;

            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                return new List<ReportModel>();
            }
        }
        public List<ReportModel> GenDataBM2109(int year, int month, string phienBan, string kichBan)
        {
            try
            {
                var data = new List<ReportModel>();


                var header1_1 = UnitOfWork.Repository<SuaChuaLonRepo>().Queryable().Where(x => x.PHIEN_BAN == "PB1" && x.KICH_BAN == kichBan && x.TIME_YEAR == year && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var details1_1 = UnitOfWork.Repository<SuaChuaLonDataRepo>().Queryable().Where(x => header1_1.Contains(x.TEMPLATE_CODE)).ToList();

                var header3 = UnitOfWork.Repository<SuaChuaLonRepo>().Queryable().Where(x => x.PHIEN_BAN == "PB3" && x.KICH_BAN == kichBan && x.TIME_YEAR == year && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var details3 = UnitOfWork.Repository<SuaChuaLonDataRepo>().Queryable().Where(x => header3.Contains(x.TEMPLATE_CODE)).ToList();

                var header5 = UnitOfWork.Repository<SuaChuaLonRepo>().Queryable().Where(x => x.PHIEN_BAN == "PB5" && x.KICH_BAN == kichBan && x.TIME_YEAR == year && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var details5 = UnitOfWork.Repository<SuaChuaLonDataRepo>().Queryable().Where(x => header3.Contains(x.TEMPLATE_CODE)).ToList();



                var elements = UnitOfWork.Repository<KhoanMucSuaChuaRepo>().Queryable().Where(x => x.TIME_YEAR == year).ToList();
                foreach (var e in elements)
                {
                    decimal col2 = 0;
                    decimal col4 = 0;
                    var dataCol2 = details3.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE == e.CODE);
                    var dataCol4 = details5.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE == e.CODE);
                    switch (month)
                    {
                        case 1:
                            col2 = dataCol2.Sum(x => x.MONTH1) ?? 0;
                            col4 = dataCol4.Sum(x => x.MONTH1) ?? 0;
                            break;
                        case 2:
                            col2 = dataCol2.Sum(x => x.MONTH1 + x.MONTH2) ?? 0;
                            col4 = dataCol4.Sum(x => x.MONTH1 + x.MONTH2) ?? 0;
                            break;
                        case 3:
                            col2 = dataCol2.Sum(x => x.MONTH1 + x.MONTH2 + x.MONTH3) ?? 0;
                            col4 = dataCol4.Sum(x => x.MONTH1 + x.MONTH2 + x.MONTH3) ?? 0;
                            break;
                        case 4:
                            col2 = dataCol2.Sum(x => x.MONTH1 + x.MONTH2 + x.MONTH3 + x.MONTH4) ?? 0;
                            col4 = dataCol4.Sum(x => x.MONTH1 + x.MONTH2 + x.MONTH3 + x.MONTH4) ?? 0;
                            break;
                        case 5:
                            col2 = dataCol2.Sum(x => x.MONTH1 + x.MONTH2 + x.MONTH3 + x.MONTH4 + x.MONTH5) ?? 0;
                            col4 = dataCol4.Sum(x => x.MONTH1 + x.MONTH2 + x.MONTH3 + x.MONTH4 + x.MONTH5) ?? 0;
                            break;
                        case 6:
                            col2 = dataCol2.Sum(x => x.MONTH1 + x.MONTH2 + x.MONTH3 + x.MONTH4 + x.MONTH5 + x.MONTH6) ?? 0;
                            col4 = dataCol4.Sum(x => x.MONTH1 + x.MONTH2 + x.MONTH3 + x.MONTH4 + x.MONTH5 + x.MONTH6) ?? 0;
                            break;
                        case 7:
                            col2 = dataCol2.Sum(x => x.MONTH1 + x.MONTH2 + x.MONTH3 + x.MONTH4 + x.MONTH5 + x.MONTH6 + x.MONTH7) ?? 0;
                            col4 = dataCol4.Sum(x => x.MONTH1 + x.MONTH2 + x.MONTH3 + x.MONTH4 + x.MONTH5 + x.MONTH6 + x.MONTH7) ?? 0;
                            break;
                        case 8:
                            col2 = dataCol2.Sum(x => x.MONTH1 + x.MONTH2 + x.MONTH3 + x.MONTH4 + x.MONTH5 + x.MONTH6 + x.MONTH7 + x.MONTH8) ?? 0;
                            col4 = dataCol4.Sum(x => x.MONTH1 + x.MONTH2 + x.MONTH3 + x.MONTH4 + x.MONTH5 + x.MONTH6 + x.MONTH7 + x.MONTH8) ?? 0;
                            break;
                        case 9:
                            col2 = dataCol2.Sum(x => x.MONTH1 + x.MONTH2 + x.MONTH3 + x.MONTH4 + x.MONTH5 + x.MONTH6 + x.MONTH7 + x.MONTH8 + x.MONTH9) ?? 0;
                            col4 = dataCol4.Sum(x => x.MONTH1 + x.MONTH2 + x.MONTH3 + x.MONTH4 + x.MONTH5 + x.MONTH6 + x.MONTH7 + x.MONTH8 + x.MONTH9) ?? 0;
                            break;
                        case 10:
                            col2 = dataCol2.Sum(x => x.MONTH1 + x.MONTH2 + x.MONTH3 + x.MONTH4 + x.MONTH5 + x.MONTH6 + x.MONTH7 + x.MONTH8 + x.MONTH9 + x.MONTH10) ?? 0;
                            col4 = dataCol4.Sum(x => x.MONTH1 + x.MONTH2 + x.MONTH3 + x.MONTH4 + x.MONTH5 + x.MONTH6 + x.MONTH7 + x.MONTH8 + x.MONTH9 + x.MONTH10) ?? 0;
                            break;
                        case 11:
                            col2 = dataCol2.Sum(x => x.MONTH1 + x.MONTH2 + x.MONTH3 + x.MONTH4 + x.MONTH5 + x.MONTH6 + x.MONTH7 + x.MONTH8 + x.MONTH9 + x.MONTH10 + x.MONTH11) ?? 0;
                            col4 = dataCol4.Sum(x => x.MONTH1 + x.MONTH2 + x.MONTH3 + x.MONTH4 + x.MONTH5 + x.MONTH6 + x.MONTH7 + x.MONTH8 + x.MONTH9 + x.MONTH10 + x.MONTH11) ?? 0;
                            break;
                        case 12:
                            col2 = dataCol2.Sum(x => x.MONTH1 + x.MONTH2 + x.MONTH3 + x.MONTH4 + x.MONTH5 + x.MONTH6 + x.MONTH7 + x.MONTH8 + x.MONTH9 + x.MONTH10 + x.MONTH11 + x.MONTH12) ?? 0;
                            col4 = dataCol4.Sum(x => x.MONTH1 + x.MONTH2 + x.MONTH3 + x.MONTH4 + x.MONTH5 + x.MONTH6 + x.MONTH7 + x.MONTH8 + x.MONTH9 + x.MONTH10 + x.MONTH11 + x.MONTH12) ?? 0;
                            break;
                    }
                    var i = new ReportModel
                    {
                        Id = e.CODE,
                        Parent = e.PARENT_CODE,
                        Name = e.NAME,
                        IsBold = e.CODE.Length <= 4 ? true : false,
                        Col1 = details1_1.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE == e.CODE).Sum(x => x.VALUE),
                        Col2 = col2,
                        Col4 = col4,
                        Order = e.C_ORDER,
                    };
                    i.Col3 = i.Col1 + i.Col2;
                    i.Col5 = i.Col4 == 0 || i.Col3 == 0 ? 0 : i.Col4 / i.Col3;
                    data.Add(i);
                }

                foreach (var d in data.OrderByDescending(x => x.Order))
                {
                    var child = data.Where(x => x.Parent == d.Id).ToList();
                    d.Col1 = child.Count() == 0 ? d.Col1 : child.Sum(x => x.Col1);
                    d.Col2 = child.Count() == 0 ? d.Col2 : child.Sum(x => x.Col2);
                    d.Col3 = child.Count() == 0 ? d.Col3 : child.Sum(x => x.Col3);
                    d.Col4 = child.Count() == 0 ? d.Col4 : child.Sum(x => x.Col4);
                    d.Col5 = child.Count() == 0 ? d.Col5 : child.Sum(x => x.Col5);
                }
                return data;

            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                return new List<ReportModel>();
            }
        }
        public List<ReportModel> GenDataBM2110(int year, int month, string phienBan, string kichBan)
        {
            try
            {
                var data = new List<ReportModel>();

                var header1_1 = UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.PHIEN_BAN == "PB1" && x.KICH_BAN == kichBan && x.TIME_YEAR == year && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var details1_1 = UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => header1_1.Contains(x.TEMPLATE_CODE)).ToList();

                var header3 = UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.PHIEN_BAN == "PB3" && x.KICH_BAN == kichBan && x.TIME_YEAR == year && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var details3 = UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => header3.Contains(x.TEMPLATE_CODE)).ToList();

                var header5 = UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.PHIEN_BAN == "PB5" && x.KICH_BAN == kichBan && x.TIME_YEAR == year && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var details5 = UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => header3.Contains(x.TEMPLATE_CODE)).ToList();

                var elements = UnitOfWork.Repository<ReportChiPhiCodeRepo>().GetAll().OrderBy(x => x.C_ORDER).ToList();

                var dataTH = UnitOfWork.Repository<SyncCostRepo>().Queryable().Where(x => x.YEAR == year && x.MONTH <= month).ToList();

                foreach (var e in elements)
                {
                    var i = new ReportModel
                    {
                        Stt = e.STT,
                        Name = e.GROUP_NAME,
                        IsBold = e.IS_BOLD,
                        Col1 = details1_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.GROUP_1_ID + e.GROUP_2_ID)).Sum(x => x.QUANTITY * x.PRICE),
                        Col2 = details3.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.GROUP_1_ID + e.GROUP_2_ID)).Sum(x => x.QUANTITY * x.PRICE),
                        Col4 = string.IsNullOrEmpty(e.GROUP_2_ID) ? dataTH.Where(x => x.GROUP_1_ID == e.GROUP_1_ID).Sum(x => x.VALUE) :
                        dataTH.Where(x => x.GROUP_1_ID == e.GROUP_1_ID && x.GROUP_2_ID.Contains(e.GROUP_2_ID)).Sum(x => x.VALUE),
                    };
                    i.Col3 = i.Col1 + i.Col2;
                    i.Col5 = i.Col4 == 0 || i.Col3 == 0 ? 0 : i.Col4 / i.Col3;
                    data.Add(i);
                }

                return data;

            }
            catch (Exception ex)
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
                Stt = "I",
                Id = "I",
                Name = "Sản lượng",
                NameExcel = "Sản lượng",
                IsBold = true,
            },
            new ReportModel()
            {
                Stt = "1",
                Id = "I.1",
                Parent = "I",
                Name = "Cung ứng cho VNA Group",
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
                Stt = "2",
                Id = "I.2",
                Parent = "I",
                Name = "Cung ứng cho đối tác khác (*)",
                NameExcel = "Cung ứng cho đối tác khác (*)",

                Col1 = dataSL_C.Where(x => x.SanLuongProfitCenter.HangHangKhong.IS_VNA == false).Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                Col2 = dataSL_TB.Where(x => x.SanLuongProfitCenter.HangHangKhong.IS_VNA == false).Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                Col3 = 0,
                Col4 = dataSL_T.Where(x => x.SanLuongProfitCenter.HangHangKhong.IS_VNA == false).Sum(x => x.VALUE_SUM_YEAR) ?? 0
            },
            new ReportModel()
            {
                Stt = "II",
                Id = "II",
                Name = "II. Doanh thu từ hoạt động SXKD",
                NameExcel = "Doanh thu từ hoạt động SXKD",

                IsBold = true,
                Unit = "Tr.đ/USD"
            },
            new ReportModel()
            {
                Stt = "1",
                Id = "II.1",
                Parent = "II",
                Name = "Doanh thu cung ứng cho VNA Group",
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
                Stt = "2",
                Id = "II.2",
                Parent = "II",
                Name = "Doanh thu cung ứng cho đối tác khác (*)",
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
                Stt = "III",
                Id = "III",
                Name = "Các khoản chi phí",
                NameExcel = "Các khoản chi phí",

                Unit = "Tr.đ/USD",
                IsBold = true
            },
            new ReportModel()
            {
                Stt = "1",
                Id = "III.1",
                Parent = "III",
                Name = "Chi phí dịch vụ mua ngoài",
                NameExcel = "Chi phí dịch vụ mua ngoài",

                Unit = "Tr.đ/USD",
            },
            new ReportModel()
            {
                Stt = "1.1",
                Id = "III.1.1",
                Parent = "III.1",
                Name = "Chi phí bảo hiểm tài sản",
                NameExcel = "Chi phí bảo hiểm tài sản",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G001")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G001")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G001")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "1.2",
                Id = "III.1.2",
                Parent = "III.1",
                Name = "Thuê sửa chữa nhà cửa VKT",
                NameExcel = "Thuê sửa chữa nhà cửa VKT",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G002")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G002")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G002")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "1.3",
                Id = "III.1.3",
                Parent = "III.1",
                Name = "Thuê sửa chữa máy móc thiết bị",
                NameExcel = "Thuê sửa chữa máy móc thiết bị",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G003")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G003")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G003")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "1.4",
                Id = "III.1.4",
                Parent = "III.1",
                Name = "Thuê sửa chữa PTVT",
                NameExcel = "Thuê sửa chữa PTVT",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G004")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G004")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G004")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "1.5",
                Id = "III.1.5",
                Parent = "III.1",
                Name = "Thuê sửa chữa thiết bị quản lý",
                NameExcel = "Thuê sửa chữa thiết bị quản lý",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G005")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G005")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G005")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "1.6",
                Id = "III.1.6",
                Parent = "III.1",
                Name = "Thuê sửa chữa kho bể",
                NameExcel = "Thuê sửa chữa kho bể",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G006")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G006")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G006")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "1.7",
                Id = "III.1.7",
                Parent = "III.1",
                Name = "Thuê sửa chữa TSCĐ khác",
                NameExcel = "Thuê sửa chữa TSCĐ khác",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G007")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G007")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G007")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "1.8",
                Id = "III.1.8",
                Parent = "III.1",
                Name = "Thuê cửa hàng kho bãi",
                NameExcel = "Thuê cửa hàng kho bãi",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G008")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G008")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G008")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "1.9",
                Id = "III.1.9",
                Parent = "III.1",
                Name = "Thuê vận chuyển",
                NameExcel = "Thuê vận chuyển",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G009")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G009")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G009")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "1.10",
                Id = "III.1.10",
                Parent = "III.1",
                Name = "Tiền điện mua ngoài",
                NameExcel = "Tiền điện mua ngoài",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G010")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G010")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G010")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "1.11",
                Id = "III.1.11",
                Parent = "III.1",
                Name = "Tiền nước mua ngoài",
                NameExcel = "Tiền nước mua ngoài",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G011")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G011")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G011")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "1.12",
                Id = "III.1.12",
                Parent = "III.1",
                Name = "Cước thông tin liên lạc",
                NameExcel = "Cước thông tin liên lạc",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G012")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G012")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G012")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "1.13",
                Id = "III.1.13",
                Parent = "III.1",
                Name = "Chi phí dịch vụ mua ngoài khác",
                NameExcel = "Chi phí dịch vụ mua ngoài khác",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G019")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G019")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6277G019")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "2",
                Id = "III.2",
                Parent = "III",
                Name = "Chi khác bằng tiền",
                NameExcel = "Chi khác bằng tiền",

                Unit = "Tr.đ/USD",
            },
            new ReportModel()
            {
                Stt = "2.1",
                Id = "III.2.1",
                Parent = "III.2",
                Name = "Chi ANAT, PCBT, PCCC",
                NameExcel = "Chi ANAT, PCBT, PCCC",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H001")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H001")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H001")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "2.2",
                Id = "III.2.2",
                Parent = "III.2",
                Name = "Chi phí trang phục ngành",
                NameExcel = "Chi phí trang phục ngành",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H002")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H002")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H002")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "2.3",
                Id = "III.2.3",
                Parent = "III.2",
                Name = "Chi giao dịch tiếp khách",
                NameExcel = "Chi giao dịch tiếp khách",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H004")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H004")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H004")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "2.4",
                Id = "III.2.4",
                Parent = "III.2",
                Name = "Chi quảng cáo, marketing",
                NameExcel = "Chi quảng cáo, marketing",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H014")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H014")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H014")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "2.5",
                Id = "III.2.5",
                Parent = "III.2",
                Name = "Chi hoa hồng môi giới",
                NameExcel = "Chi hoa hồng môi giới",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H005")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H005")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H005")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "2.6",
                Id = "III.2.6",
                Parent = "III.2",
                Name = "Chi đào tạo",
                NameExcel = "Chi đào tạo",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H006")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H006")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H006")).Sum(x => x.AMOUNT) ?? 0,
            },

            new ReportModel()
            {
                Stt = "2.7",
                Id = "II.2.7",
                Parent = "III.2",
                Name = "Công tác phí, phép",
                NameExcel = "Công tác phí, phép",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H007")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H007")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H007")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "2.8",
                Id = "III.2.8",
                Parent = "III.2",
                Name = "Lệ phí cầu đường",
                NameExcel = "Lệ phí cầu đường",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H003")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H003")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H003")).Sum(x => x.AMOUNT) ?? 0,
            },

            new ReportModel()
            {
                Stt = "2.9",
                Id = "III.2.9",
                Parent = "III.2",
                Name = "Chi bồi dưỡng độc hại",
                NameExcel = "Chi bồi dưỡng độc hại",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H008")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H008")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H008")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "2.10",
                Id = "III.2.10",
                Parent = "III.2",
                Name = "Phí nhượng quyền khai thác",
                NameExcel = "Phí nhượng quyền khai thác",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H009")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H009")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H009")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "2.11",
                Id = "III.2.11",
                Parent = "III.2",
                Name = "Chi VSCN, y tế, môi trường",
                NameExcel = "Chi VSCN, y tế, môi trường",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H010")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H010")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H010")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "2.12",
                Id = "III.2.12",
                Parent = "III.2",
                Name = "Phí ngân hàng",
                NameExcel = "Phí ngân hàng",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H015")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H015")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H015")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "2.13",
                Id = "III.2.13",
                Parent = "III.2",
                Name = "Khoản chi có tính chất phúc lợi",
                NameExcel = "Khoản chi có tính chất phúc lợi",

                Unit = "Tr.đ/USD",
                Col1 = dataCP_C.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H018")).Sum(x => x.AMOUNT) ?? 0,
                Col2 = dataCP_TB.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H018")).Sum(x => x.AMOUNT) ?? 0,
                Col3 = 0,
                Col4 = dataCP_T.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains("6278H018")).Sum(x => x.AMOUNT) ?? 0,
            },
            new ReportModel()
            {
                Stt = "2.14",
                Id = "III.2.14",
                Parent = "III.2",
                Name = "Chi bằng tiền khác",
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
        public List<ReportModel> GenDataBM02D2(int year, int month, string phienBan, string kichBan)
        {
            try
            {
                var data = new List<ReportModel>();
                data.Add(new ReportModel
                {
                    Id = "I",
                    Name = "I. Doanh thu",
                    IsBold = true,
                });
                data.Add(new ReportModel
                {
                    Id = "I.1",
                    Parent = "I",
                    Name = "1. Điều chỉnh tăng giá"
                });
                data.Add(new ReportModel
                {
                    Id = "I.1.1",
                    Parent = "I.1",
                    Name = "- Điều chỉnh đối với VNA"
                });
                data.Add(new ReportModel
                {
                    Id = "I.1.2",
                    Parent = "I.1",
                    Name = "- Điều chỉnh đối với các khách hàng khác"
                });
                data.Add(new ReportModel
                {
                    Id = "I.2",
                    Parent = "I",
                    Name = "2. Chiết khấu/giảm giá "
                });
                data.Add(new ReportModel
                {
                    Id = "I.2.1",
                    Parent = "I.2",
                    Name = "- Điều chỉnh đối với VNA"
                });
                data.Add(new ReportModel
                {
                    Id = "I.2.2",
                    Parent = "I.2",
                    Name = "- Điều chỉnh đối với các khách hàng khác"
                });
                data.Add(new ReportModel
                {
                    Id = "I.3",
                    Parent = "I",
                    Name = "3. Doanh thu từ hoạt động tài chính"
                });
                data.Add(new ReportModel
                {
                    Id = "I.4",
                    Parent = "I",
                    Name = "4. Thu nhập khác"
                });
                data.Add(new ReportModel
                {
                    Id = "I.5",
                    Parent = "I",
                    Name = "5. Các khoản thu bất thường khác"
                });
                data.Add(new ReportModel
                {
                    Id = "II",
                    Name = "II. Chi phí",
                    IsBold = true,
                });
                data.Add(new ReportModel
                {
                    Id = "II.1",
                    Parent = "II",
                    Name = "1. Chi phí SXKD (giá vốn, nguyên liệu, quỹ lương, chi có tính chất phúc lợi, chi xúc tiến thương mại, thay đổi chính sách khấu hao, thuê tài sản/mặt bằng,…)"
                });
                data.Add(new ReportModel
                {
                    Id = "II.2",
                    Parent = "II",
                    Name = "2. Chi phí tài chính (lãi vay, lỗ chênh lệch tỷ giá…)"

                });
                data.Add(new ReportModel
                {
                    Id = "II.3",
                    Parent = "II",
                    Name = "3. Các khoản dự phòng"

                });
                data.Add(new ReportModel
                {
                    Id = "II.3.1",
                    Parent = "II.3",
                    Name = "- Dự phòng giảm giá hàng tồn kho"
                });
                data.Add(new ReportModel
                {
                    Id = "II.3.2",
                    Parent = "II.3",
                    Name = "- Dự phòng phải thu khó đòi"
                });
                data.Add(new ReportModel
                {
                    Id = "II.3.3",
                    Parent = "II.3",
                    Name = "- Dự phòng tổn thất đầu tư vào đơn vị khác"
                });
                data.Add(new ReportModel
                {
                    Id = "II.4",
                    Parent = "II",
                    Name = "4. Các khoản chi bất thường khác"
                });
                data.Add(new ReportModel
                {
                    Id = "III",
                    Name = "III. Các dự án đầu tư đưa vào KH nhiều năm nhưng không thực hiện hoặc chưa hoàn thành",
                    IsBold = true,
                });
                data.Add(new ReportModel
                {
                    Id = "IV",
                    Name = "IV. Lợi nhuận trước thuế",
                    IsBold = true,
                });
                data.Add(new ReportModel
                {
                    Id = "IV.1",
                    Parent = "IV",
                    Name = "- Lợi nhuận gộp về bán hàng và cung cấp dịch vụ"
                });
                data.Add(new ReportModel
                {
                    Id = "IV.2",
                    Parent = "IV",
                    Name = " - Lợi nhuận thuần từ hoạt động kinh doanh"
                });
                data.Add(new ReportModel
                {
                    Id = "IV.3",
                    Parent = "IV",
                    Name = "- Lợi nhuận khác"
                });

                return data;

            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                return new List<ReportModel>();
            }
        }
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