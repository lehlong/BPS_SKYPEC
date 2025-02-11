﻿using Hangfire.Annotations;
using Microsoft.Ajax.Utilities;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Office.Interop.Excel;
using NHibernate.Criterion;
using NPOI.HSSF.Record.Chart;
using NPOI.POIFS.Properties;
using NPOI.SS.Formula.Functions;
using NPOI.SS.Formula.PTG;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.XWPF.UserModel;
using SMO.Core.Entities.BP;
using SMO.Core.Entities.BP.SUA_CHUA_LON;
using SMO.Core.Entities.MD;
using SMO.Models;
using SMO.Repository.Common;
using SMO.Repository.Implement.BP;
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
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Web;
using static iTextSharp.text.pdf.AcroFields;

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
                case "BM_QT21":
                    startRow = 7;
                    InsertDataToTableBM_QT21(templateWorkbook, sheet, lstData, startRow, NUMCELL);
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
            styleCellBold.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,##0");

            var fontBold = templateWorkbook.CreateFont();
            fontBold.Boldweight = (short)FontBoldWeight.Bold;
            fontBold.FontHeightInPoints = 12;
            fontBold.FontName = "Times New Roman";

            ICellStyle styleCellNormal = templateWorkbook.CreateCellStyle(); // chữ in đậm
            styleCellNormal.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,##0");

            var fontNormal = templateWorkbook.CreateFont();
            fontNormal.FontHeightInPoints = 12;
            fontNormal.FontName = "Times New Roman";

            var startRow1 = 12;
            var startRow2 = 18 + data.SanLuong.Count();
            var startRow3 = 24 + data.SanLuong.Count() + data.DauTu.Count();
            var startRow4 = 31 + data.SanLuong.Count() + data.DauTu.Count() + data.SuaChuaLon.Count();


            sheet.ShiftRows(startRow1 + 1, startRow1 + data.SanLuong.Count() + 1, data.SanLuong.Count(), true, false);
            sheet.ShiftRows(startRow2 + 1, startRow2 + data.DauTu.Count() + 1, data.DauTu.Count(), true, false);
            sheet.ShiftRows(startRow3 + 1, startRow3 + data.SuaChuaLon.Count() + 1, data.SuaChuaLon.Count(), true, false);
            sheet.ShiftRows(startRow4 + 1, startRow4 + data.ChiPhi.Count() + 1, data.ChiPhi.Count(), true, false);


            for (int i = 0; i < data.SuaChuaLon.Count(); i++)
            {
                var cra = new NPOI.SS.Util.CellRangeAddress(startRow3 + i, startRow3 + i, 1, 2);
                var cra1 = new NPOI.SS.Util.CellRangeAddress(startRow3 + i, startRow3 + i, 5, 6);
                sheet.AddMergedRegion(cra);
                sheet.AddMergedRegion(cra1);
            }
            for (int i = 0; i < data.ChiPhi.Count(); i++)
            {
                var cra = new NPOI.SS.Util.CellRangeAddress(startRow4 + i, startRow4 + i, 1, 2);
                var cra1 = new NPOI.SS.Util.CellRangeAddress(startRow4 + i, startRow4 + i, 5, 6);
                sheet.AddMergedRegion(cra);
                sheet.AddMergedRegion(cra1);
            }

            for (int i = 0; i < data.SanLuong.Count(); i++)
            {
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow1++, 7);
                rowCur.Cells[0].SetCellValue(data.SanLuong[i]?.Stt);
                rowCur.Cells[1].SetCellValue(data.SanLuong[i]?.Name);
                if (data.SanLuong[i].Value1 == 0)
                {
                    rowCur.Cells[2].SetCellValue("");
                }
                else
                {
                    rowCur.Cells[2].CellStyle = styleCellNumber;
                    rowCur.Cells[2].SetCellValue((double)Math.Round(data.SanLuong[i].Value1));
                }
                if (data.SanLuong[i].Value2 == 0)
                {
                    rowCur.Cells[3].SetCellValue("");
                }
                else
                {
                    rowCur.Cells[3].CellStyle = styleCellNumber;
                    rowCur.Cells[3].SetCellValue((double)Math.Round(data.SanLuong[i].Value2));
                }
                if (data.SanLuong[i].Value3 == 0)
                {
                    rowCur.Cells[4].SetCellValue("");
                }
                else
                {
                    rowCur.Cells[4].CellStyle = styleCellNumber;
                    rowCur.Cells[4].SetCellValue((double)Math.Round(data.SanLuong[i].Value3));
                }
                if (data.SanLuong[i].Value4 == 0)
                {
                    rowCur.Cells[5].SetCellValue("");
                }
                else
                {
                    rowCur.Cells[5].CellStyle = styleCellNumber;
                    rowCur.Cells[5].SetCellValue((double)Math.Round(data.SanLuong[i].Value4));
                }
                if (data.SanLuong[i].Value5 == 0)
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
                }
            }

            //InsertDT
            for (int i = 0; i < data.DauTu.Count(); i++)
            {
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow2++, 7);
                rowCur.Cells[0].SetCellValue(data.DauTu[i]?.Stt.ToString());
                rowCur.Cells[1].SetCellValue(data.DauTu[i]?.Name);
                rowCur.Cells[2].SetCellValue(data.DauTu[i]?.Value1);

                if (data.DauTu[i].Value2 == 0)
                {
                    rowCur.Cells[3].SetCellValue("");
                }
                else
                {
                    rowCur.Cells[3].CellStyle = styleCellNumber;
                    rowCur.Cells[3].SetCellValue((double)Math.Round(data.DauTu[i].Value2));
                }
                if (data.DauTu[i].Value3 == 0)
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
                }
            }

            //InsertSC

            for (int i = 0; i < data.SuaChuaLon.Count(); i++)
            {
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow3++, 7);
                rowCur.Cells[0].SetCellValue(data.SuaChuaLon[i]?.stt);
                rowCur.Cells[1].SetCellValue(data.SuaChuaLon[i]?.name);

                if (data.SuaChuaLon[i].valueKP == 0)
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

            for (int i = 0; i < data.ChiPhi.Count(); i++)
            {
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow4++, 7);
                rowCur.Cells[0].SetCellValue(data.ChiPhi[i]?.Stt);
                rowCur.Cells[1].SetCellValue(data.ChiPhi[i]?.name);

                if (data.ChiPhi[i].valueCP == 0)
                {
                    rowCur.Cells[3].SetCellValue("");
                }
                else
                {
                    rowCur.Cells[3].CellStyle = styleCellNumber;
                    rowCur.Cells[3].SetCellValue((double)Math.Round(data.ChiPhi[i].valueCP));
                }

                if (data.ChiPhi[i].price == 0)
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

        public void ExportExcelTHKHCP(ref MemoryStream outFileStream, ReportChiPhiModel datas, string path)
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
            var parentList = datas.chiPhiInReports.Where(x => x.IsBold == true);
            var sumMB = parentList.Sum(x => x.valueCNMB);
            var sumMT = parentList.Sum(x => x.valueCNMT);
            var sumVT = parentList.Sum(x => x.valueCNVT);
            var sumMN = parentList.Sum(x => x.valueCNMN);
            var sumCQ = parentList.Sum(x => x.valueCQCT);
            var sumTcty = parentList.Sum(x => x.valueTcty);
            datas.chiPhiInReports.Insert(0, new ChiPhiInReport
            {
                Stt = "",
                name = "TỔNG CỘNG",
                IsBold = true,
                valueCQCT = sumCQ,
                valueCNMN = sumMN,
                valueCNMT = sumMT,
                valueCNVT = sumVT,
                valueCNMB = sumMB,
                valueTcty = sumTcty
            });
            var data = datas.chiPhiInReports;


            var startRow1 = 8;


            for (int i = 0; i < data.Count(); i++)
            {
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow1++, 8);
                rowCur.Cells[0].SetCellValue(data[i]?.Stt.ToString());
                rowCur.Cells[1].SetCellValue(data[i]?.name);
                if (data[i].valueCQCT == 0 || data[i].valueCQCT == null)
                {
                    rowCur.Cells[2].SetCellValue("");
                }
                else
                {
                    rowCur.Cells[2].CellStyle = styleCellNumber;
                    rowCur.Cells[2].CellStyle.Alignment = HorizontalAlignment.Right;
                    rowCur.Cells[2].SetCellValue((double)Math.Round(data[i].valueCQCT));
                }

                if (data[i].valueCNMB == 0 || data[i].valueCNMB == null)
                {
                    rowCur.Cells[3].SetCellValue("");
                }
                else
                {
                    rowCur.Cells[3].CellStyle = styleCellNumber;
                    rowCur.Cells[3].CellStyle.Alignment = HorizontalAlignment.Right;
                    rowCur.Cells[3].SetCellValue((double)Math.Round(data[i].valueCNMB));
                }

                if (data[i].valueCNMT == 0 || data[i].valueCNMT == null)
                {
                    rowCur.Cells[4].SetCellValue("");
                }
                else
                {
                    rowCur.Cells[4].CellStyle = styleCellNumber;
                    rowCur.Cells[4].CellStyle.Alignment = HorizontalAlignment.Right;
                    rowCur.Cells[4].SetCellValue((double)Math.Round(data[i].valueCNMT));
                }

                if (data[i].valueCNMN == 0 || data[i].valueCNMN == null)
                {
                    rowCur.Cells[5].SetCellValue("");
                }
                else
                {
                    rowCur.Cells[5].CellStyle = styleCellNumber;
                    rowCur.Cells[5].CellStyle.Alignment = HorizontalAlignment.Right;
                    rowCur.Cells[5].SetCellValue((double)Math.Round(data[i].valueCNMN));
                }

                if (data[i].valueCNVT == 0 || data[i].valueCNVT == null)
                {
                    rowCur.Cells[6].SetCellValue("");
                }
                else
                {
                    rowCur.Cells[6].CellStyle = styleCellNumber;
                    rowCur.Cells[6].CellStyle.Alignment = HorizontalAlignment.Right;
                    rowCur.Cells[6].SetCellValue((double)Math.Round(data[i].valueCNVT));
                }

                if (data[i].valueTcty == 0 || data[i].valueTcty == null)
                {
                    rowCur.Cells[7].SetCellValue("");
                }
                else
                {
                    rowCur.Cells[7].CellStyle = styleCellNumber;
                    rowCur.Cells[7].CellStyle.Alignment = HorizontalAlignment.Right;
                    rowCur.Cells[7].SetCellValue((double)Math.Round(data[i].valueTcty));
                }

                for (var j = 0; j < 8; j++)
                {
                    if (data[i].IsBold)
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
        public void InsertDataToTableBM_QT21(IWorkbook templateWorkbook, ISheet sheet, IList<ReportModel> dataDetails, int startRow, int NUM_CELL)
        {
            ICellStyle styleCellBold = templateWorkbook.CreateCellStyle(); // chữ in đậm
            styleCellBold.CloneStyleFrom(sheet.GetRow(8).Cells[0].CellStyle);
            var fontBold = templateWorkbook.CreateFont();
            fontBold.Boldweight = (short)FontBoldWeight.Bold;
            fontBold.FontHeightInPoints = 13;
            fontBold.FontName = "Times New Roman";

            ICellStyle styleCellName = templateWorkbook.CreateCellStyle();
            styleCellName.CloneStyleFrom(sheet.GetRow(7).Cells[1].CellStyle);

            ICellStyle styleBody = templateWorkbook.CreateCellStyle();
            styleBody.CloneStyleFrom(sheet.GetRow(7).Cells[1].CellStyle);
            styleBody.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");
            ICellStyle styleBodyBold = templateWorkbook.CreateCellStyle();
            styleBodyBold.CloneStyleFrom(sheet.GetRow(7).Cells[1].CellStyle);
            styleBodyBold.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,###");

            foreach (var item in dataDetails)
            {
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, NUM_CELL);
                rowCur.Cells[0].SetCellValue(item.Stt);
                rowCur.Cells[1].SetCellValue(item.Name);
                rowCur.Cells[2].SetCellValue(item.ValueQt == null ? 0 : Convert.ToDouble(item.ValueQt));
                rowCur.Cells[3].SetCellValue(item.ValuePlanYear == null ? 0 : Convert.ToDouble(item.ValuePlanYear));
                rowCur.Cells[4].SetCellValue(item.ValueAdditionPlan == null ? 0 : Convert.ToDouble(item.ValueAdditionPlan));
                rowCur.Cells[5].SetCellValue(item.ValueAfterAdditionPlan == null ? 0 : Convert.ToDouble(item.ValueAfterAdditionPlan));
                rowCur.Cells[6].SetCellValue(item.ValueTH9T == null ? 0 : Convert.ToDouble(item.ValueTH9T));
                rowCur.Cells[7].SetCellValue(item.ValueUocThucHien == null ? 0 : Convert.ToDouble(item.ValueUocThucHien));
                rowCur.Cells[8].SetCellValue(item.ValuePercentPlant == null ? 0 : Convert.ToDouble(item.ValuePercentPlant));
                rowCur.Cells[9].SetCellValue(item.ValuePlantNextYear == null ? 0 : Convert.ToDouble(item.ValuePlantNextYear));
                rowCur.Cells[10].SetCellValue(item.Des);

                for (int i = 0; i < NUM_CELL; i++)
                {
                    if (item.IsBold)
                    {
                        if (i == 0 || i ==1)
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
                        if (i == 0 || i ==1)
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

        internal ReportQT21Model GetReportDataQT21(int year, string phienBan, string kichBan, string area ,int month)
        {
            //
            try
            {
                var data = new ReportQT21Model();
                var headerCP_PB1 = UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB1" && x.KICH_BAN == kichBan && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var dataCP_PB1 = UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => headerCP_PB1.Contains(x.TEMPLATE_CODE)).ToList();
                var headerCP_PB1_next = UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year +1 && x.PHIEN_BAN == "PB1" && x.KICH_BAN == kichBan && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var dataCP_PB1_next = UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => headerCP_PB1_next.Contains(x.TEMPLATE_CODE)).ToList();
                var elements = UnitOfWork.Repository<ReportChiPhiCodeRepo>().GetAll().OrderBy(x => x.C_ORDER).ToList();
                var headerCP_BS = UnitOfWork.Repository<KeHoachChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB3" && x.KICH_BAN == kichBan && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var dataCP_BS = UnitOfWork.Repository<KeHoachChiPhiDataRepo>().Queryable().Where(x => headerCP_BS.Contains(x.TEMPLATE_CODE)).ToList();
                var valueUTH = UnitOfWork.Repository<InputChiPhiRepo>().Queryable().Where(x => x.TIME_YEAR == year&& string.IsNullOrEmpty(area) ? true :x.AREA_CODE== area).GroupBy(x=>x.ID_CENTER).Select(x=> new
                {
                    
                    ID_CENTER=x.Key,
                    UOC_THUC_HIEN=x.Sum(y=>y.UOC_THUC_HIEN),
                    TH9T=x.Sum(y=>y.TH9T)

                });
                Dictionary<string, string> OrgArea =
                     new Dictionary<string, string>()
                 {
                  {"CQ","100001"},
                  {"MB","100002"},
                  {"MT","100003"},
                  {"MN","100004"},
                  {"VT","100005"},
                     };
                // list mã 
              
                string[] LisTCostYear = { "6273C001C", "6273C002C", "6273C003C", "6273C004C", "6273C005C", "6273C006C", "6273C009C" };
                var item = new ChiPhiQT21InReport { };
                foreach (var e in elements)
                {
                    var dataUTH = valueUTH.FirstOrDefault(x => x.ID_CENTER == e.ID);
                    
                    string checkID = null;
                    switch (area)
                    {
                        case "MT":
                            checkID = e.IDMT;
                            break;
                        case "MN":
                            checkID = e.IDMN;
                            break;
                        case "MB":
                            checkID = e.IDMB;
                            break;
                        case "CQ":
                            checkID = e.IDCQ;
                            break;
                        case "VT":
                            checkID = e.IDVT;
                            break;
                        default:
                            checkID = null;
                            break;
                    };
                    if (string.IsNullOrEmpty(area))
                    {
                        var a = dataCP_PB1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.IDCQ != null ? e.IDCQ : e.GROUP_1_ID + e.GROUP_2_ID) && (x.ORG_CODE.Contains(OrgArea["CQ"])));
                        var PlanYearMB = dataCP_PB1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.IDMB != null ? e.IDMB : e.GROUP_1_ID + e.GROUP_2_ID) && (x.ORG_CODE.Contains(OrgArea["MB"]))).Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                        var PlanYearMT = dataCP_PB1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.IDMT != null ? e.IDMT : e.GROUP_1_ID + e.GROUP_2_ID) && (x.ORG_CODE.Contains(OrgArea["MT"]))).Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                        var PlanYearMN = dataCP_PB1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.IDMN != null ? e.IDMN : e.GROUP_1_ID + e.GROUP_2_ID) && (x.ORG_CODE.Contains(OrgArea["MN"]))).Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                        var PlanYearCQ = dataCP_PB1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.IDCQ != null ? e.IDCQ : e.GROUP_1_ID + e.GROUP_2_ID) && (x.ORG_CODE.Contains(OrgArea["CQ"]))).Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                        var PlanYearVT = dataCP_PB1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.IDVT != null ? e.IDVT : e.GROUP_1_ID + e.GROUP_2_ID) && (x.ORG_CODE.Contains(OrgArea["VT"]))).Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                        var AdditionMB = dataCP_BS.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.IDMB != null ? e.IDMB : e.GROUP_1_ID + e.GROUP_2_ID) && (x.ORG_CODE.Contains(OrgArea["MB"]))).Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                        var AdditionMT = dataCP_BS.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.IDMT != null ? e.IDMT : e.GROUP_1_ID + e.GROUP_2_ID) && (x.ORG_CODE.Contains(OrgArea["MT"]))).Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                        var AdditionMN = dataCP_BS.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.IDMN != null ? e.IDMN : e.GROUP_1_ID + e.GROUP_2_ID) && (x.ORG_CODE.Contains(OrgArea["MN"]))).Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                        var AdditionVT = dataCP_BS.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.IDVT != null ? e.IDVT : e.GROUP_1_ID + e.GROUP_2_ID) && (x.ORG_CODE.Contains(OrgArea["VT"]))).Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                        var AdditionCQ = dataCP_BS.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.IDCQ != null ? e.IDCQ : e.GROUP_1_ID + e.GROUP_2_ID) && (x.ORG_CODE.Contains(OrgArea["CQ"]))).Sum(x => x.QUANTITY * x.PRICE) ?? 0;

                        var PlanNextYearMB = dataCP_PB1_next.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.IDMB != null ? e.IDMB : e.GROUP_1_ID + e.GROUP_2_ID) && (x.ORG_CODE.Contains(OrgArea["MB"]))).Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                        var PlanNextYearMT = dataCP_PB1_next.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.IDMT != null ? e.IDMT : e.GROUP_1_ID + e.GROUP_2_ID) && (x.ORG_CODE.Contains(OrgArea["MT"]))).Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                        var PlanNextYearMN = dataCP_PB1_next.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.IDMN != null ? e.IDMN : e.GROUP_1_ID + e.GROUP_2_ID) && (x.ORG_CODE.Contains(OrgArea["MN"]))).Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                        var PlanNextYearCQ = dataCP_PB1_next.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.IDCQ != null ? e.IDCQ : e.GROUP_1_ID + e.GROUP_2_ID) && (x.ORG_CODE.Contains(OrgArea["CQ"]))).Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                        var PlanNextYearVT = dataCP_PB1_next.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.IDVT != null ? e.IDVT : e.GROUP_1_ID + e.GROUP_2_ID) && (x.ORG_CODE.Contains(OrgArea["VT"]))).Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                        if (e.GROUP_1_ID.Contains("6273") && e.GROUP_2_ID.EndsWith("B"))
                        {
                            PlanYearMB = dataCP_PB1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.GROUP_1_ID + e.GROUP_2_ID) && (x.ORG_CODE.Contains(OrgArea["MB"]))).Sum(x => x.QUANTITY * x.PRICE > 10000000 ? x.PRICE / 2 : x.PRICE) ?? 0;
                            PlanYearMT = dataCP_PB1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.GROUP_1_ID + e.GROUP_2_ID) && (x.ORG_CODE.Contains(OrgArea["MT"]))).Sum(x => x.QUANTITY * x.PRICE > 10000000 ? x.PRICE / 2 : x.PRICE) ?? 0;
                            PlanYearMN = dataCP_PB1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.GROUP_1_ID + e.GROUP_2_ID) && (x.ORG_CODE.Contains(OrgArea["MN"]))).Sum(x => x.QUANTITY * x.PRICE > 10000000 ? x.PRICE / 2 : x.PRICE) ?? 0;
                            PlanYearCQ = dataCP_PB1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.GROUP_1_ID + e.GROUP_2_ID) && (x.ORG_CODE.Contains(OrgArea["CQ"]))).Sum(x => x.QUANTITY * x.PRICE > 10000000 ? x.PRICE / 2 : x.PRICE) ?? 0;
                            PlanYearVT = dataCP_PB1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.GROUP_1_ID + e.GROUP_2_ID) && (x.ORG_CODE.Contains(OrgArea["VT"]))).Sum(x => x.QUANTITY * x.PRICE > 10000000 ? x.PRICE / 2 : x.PRICE) ?? 0;
                            AdditionMB = dataCP_BS.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.GROUP_1_ID + e.GROUP_2_ID) && (x.ORG_CODE.Contains(OrgArea["MB"]))).Sum(x => x.QUANTITY * x.PRICE > 10000000 ? x.PRICE / 2 : x.PRICE) ?? 0;
                            AdditionMT = dataCP_BS.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.GROUP_1_ID + e.GROUP_2_ID) && (x.ORG_CODE.Contains(OrgArea["MT"]))).Sum(x => x.QUANTITY * x.PRICE > 10000000 ? x.PRICE / 2 : x.PRICE) ?? 0;
                            AdditionMN = dataCP_BS.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.GROUP_1_ID + e.GROUP_2_ID) && (x.ORG_CODE.Contains(OrgArea["MN"]))).Sum(x => x.QUANTITY * x.PRICE > 10000000 ? x.PRICE / 2 : x.PRICE) ?? 0;
                            AdditionVT = dataCP_BS.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.GROUP_1_ID + e.GROUP_2_ID) && (x.ORG_CODE.Contains(OrgArea["VT"]))).Sum(x => x.QUANTITY * x.PRICE > 10000000 ? x.PRICE / 2 : x.PRICE) ?? 0;
                            AdditionCQ = dataCP_BS.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.GROUP_1_ID + e.GROUP_2_ID) && (x.ORG_CODE.Contains(OrgArea["CQ"]))).Sum(x => x.QUANTITY * x.PRICE > 10000000 ? x.PRICE / 2 : x.PRICE) ?? 0;
                            PlanNextYearMB = dataCP_PB1_next.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.GROUP_1_ID + e.GROUP_2_ID) && (x.ORG_CODE.Contains(OrgArea["MB"]))).Sum(x => x.QUANTITY * x.PRICE > 10000000 ? x.PRICE / 2 : x.PRICE) ?? 0;
                            PlanNextYearMT = dataCP_PB1_next.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.GROUP_1_ID + e.GROUP_2_ID) && (x.ORG_CODE.Contains(OrgArea["MT"]))).Sum(x => x.QUANTITY * x.PRICE > 10000000 ? x.PRICE / 2 : x.PRICE) ?? 0;
                            PlanNextYearMN = dataCP_PB1_next.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.GROUP_1_ID + e.GROUP_2_ID) && (x.ORG_CODE.Contains(OrgArea["MN"]))).Sum(x => x.QUANTITY * x.PRICE > 10000000 ? x.PRICE / 2 : x.PRICE) ?? 0;
                            PlanNextYearCQ = dataCP_PB1_next.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.GROUP_1_ID + e.GROUP_2_ID) && (x.ORG_CODE.Contains(OrgArea["CQ"]))).Sum(x => x.QUANTITY * x.PRICE > 10000000 ? x.PRICE / 2 : x.PRICE) ?? 0;
                            PlanNextYearVT = dataCP_PB1_next.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.GROUP_1_ID + e.GROUP_2_ID) && (x.ORG_CODE.Contains(OrgArea["VT"]))).Sum(x => x.QUANTITY * x.PRICE > 10000000 ? x.PRICE / 2 : x.PRICE) ?? 0;
                        }
                        if (e.GROUP_1_ID.Contains("6273") && e.GROUP_2_ID.EndsWith("C"))
                        {
                            PlanYearMB = dataCP_PB1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.IDMB != null ? e.IDMB : e.GROUP_1_ID + e.GROUP_2_ID.Substring(0, 4) + "B") && (x.ORG_CODE.Contains(OrgArea["MB"]))).Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                            PlanYearMT = dataCP_PB1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.IDMB != null ? e.IDMB : e.GROUP_1_ID + e.GROUP_2_ID.Substring(0, 4) + "B") && (x.ORG_CODE.Contains(OrgArea["MT"]))).Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                            PlanYearMN = dataCP_PB1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.IDMB != null ? e.IDMB : e.GROUP_1_ID + e.GROUP_2_ID.Substring(0, 4) + "B") && (x.ORG_CODE.Contains(OrgArea["MN"]))).Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                            PlanYearCQ = dataCP_PB1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.IDMB != null ? e.IDMB : e.GROUP_1_ID + e.GROUP_2_ID.Substring(0, 4) + "B") && (x.ORG_CODE.Contains(OrgArea["CQ"]))).Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                            PlanYearVT = dataCP_PB1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.IDMB != null ? e.IDMB : e.GROUP_1_ID + e.GROUP_2_ID.Substring(0, 4) + "B") && (x.ORG_CODE.Contains(OrgArea["VT"]))).Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                            AdditionMB = dataCP_BS.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.IDMB != null ? e.IDMB : e.GROUP_1_ID + e.GROUP_2_ID.Substring(0, 4) + "B") && (x.ORG_CODE.Contains(OrgArea["MB"]))).Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                            AdditionMT = dataCP_BS.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.IDMT != null ? e.IDMT : e.GROUP_1_ID + e.GROUP_2_ID.Substring(0, 4) + "B") && (x.ORG_CODE.Contains(OrgArea["MT"]))).Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                            AdditionMN = dataCP_BS.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.IDMN != null ? e.IDMN : e.GROUP_1_ID + e.GROUP_2_ID.Substring(0, 4) + "B") && (x.ORG_CODE.Contains(OrgArea["MN"]))).Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                            AdditionVT = dataCP_BS.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.IDVT != null ? e.IDVT : e.GROUP_1_ID + e.GROUP_2_ID.Substring(0, 4) + "B") && (x.ORG_CODE.Contains(OrgArea["VT"]))).Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                            AdditionCQ = dataCP_BS.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.IDCQ != null ? e.IDCQ : e.GROUP_1_ID + e.GROUP_2_ID.Substring(0, 4) + "B") && (x.ORG_CODE.Contains(OrgArea["CQ"]))).Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                            PlanNextYearMB = dataCP_PB1_next.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.IDMB != null ? e.IDMB : e.GROUP_1_ID + e.GROUP_2_ID.Substring(0, 4) + "B") && (x.ORG_CODE.Contains(OrgArea["MB"]))).Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                            PlanNextYearMT = dataCP_PB1_next.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.IDMB != null ? e.IDMB : e.GROUP_1_ID + e.GROUP_2_ID.Substring(0, 4) + "B") && (x.ORG_CODE.Contains(OrgArea["MT"]))).Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                            PlanNextYearMN = dataCP_PB1_next.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.IDMB != null ? e.IDMB : e.GROUP_1_ID + e.GROUP_2_ID.Substring(0, 4) + "B") && (x.ORG_CODE.Contains(OrgArea["MN"]))).Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                            PlanNextYearCQ = dataCP_PB1_next.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.IDMB != null ? e.IDMB : e.GROUP_1_ID + e.GROUP_2_ID.Substring(0, 4) + "B") && (x.ORG_CODE.Contains(OrgArea["CQ"]))).Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                            PlanNextYearVT = dataCP_PB1_next.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(e.IDMB != null ? e.IDMB : e.GROUP_1_ID + e.GROUP_2_ID.Substring(0, 4) + "B") && (x.ORG_CODE.Contains(OrgArea["VT"]))).Sum(x => x.QUANTITY * x.PRICE) ?? 0;

                        }

                       
                        item = new ChiPhiQT21InReport
                        {
                            Group_1_ID = e.GROUP_1_ID,
                            Group_2_ID = e.GROUP_2_ID,
                            Stt = e.STT,
                            name = e.GROUP_NAME,
                            ValuePlanYear=PlanYearCQ+PlanYearMB+PlanYearMN+PlanYearMT+PlanYearVT,
                            ValueAdditionPlan= AdditionMB + AdditionMT+ AdditionMN + AdditionVT + AdditionCQ,
                            ValuePlantNextYear=PlanNextYearCQ+ PlanNextYearMB+PlanNextYearMN+PlanNextYearMT+PlanNextYearVT,
                            IsBold=e.IS_BOLD,
                            ValueUocThucHien = dataUTH?.UOC_THUC_HIEN ?? 0,
                            ValueTH9T = dataUTH?.TH9T ?? 0,
                        };
                       
                     

                    }
                    else
                    {
                        var Col1data = dataCP_PB1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(checkID != null ? checkID : e.GROUP_1_ID + e.GROUP_2_ID) && (OrgArea.ContainsKey(area) ? x.ORG_CODE.Contains(OrgArea[area]) : true)).Sum(x => x.QUANTITY * x.PRICE);
                        var Col1dataNext = dataCP_PB1_next.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(checkID != null ? checkID : e.GROUP_1_ID + e.GROUP_2_ID) && (OrgArea.ContainsKey(area) ? x.ORG_CODE.Contains(OrgArea[area]) : true)).Sum(x => x.QUANTITY * x.PRICE);
                        var Col2data = dataCP_BS.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(checkID != null ? checkID : e.GROUP_1_ID + e.GROUP_2_ID) && (OrgArea.ContainsKey(area) ? x.ORG_CODE.Contains(OrgArea[area]) : true)).Sum(x => x.QUANTITY * x.PRICE);
                        if (e.GROUP_1_ID.Contains("6273") && e.GROUP_2_ID.EndsWith("B"))
                        {
                            Col1data = dataCP_PB1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(checkID != null ? checkID : e.GROUP_1_ID + e.GROUP_2_ID) && (OrgArea.ContainsKey(area) ? x.ORG_CODE.Contains(OrgArea[area]) : true)).Sum(x => x.QUANTITY * x.PRICE > 10000000 ? x.PRICE / 2 : x.PRICE);
                            Col1dataNext = dataCP_PB1_next.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(checkID != null ? checkID : e.GROUP_1_ID + e.GROUP_2_ID) && (OrgArea.ContainsKey(area) ? x.ORG_CODE.Contains(OrgArea[area]) : true)).Sum(x => x.QUANTITY * x.PRICE > 10000000 ? x.PRICE / 2 : x.PRICE);
                            Col2data = dataCP_BS.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(checkID != null ? checkID : e.GROUP_1_ID + e.GROUP_2_ID) && (OrgArea.ContainsKey(area) ? x.ORG_CODE.Contains(OrgArea[area]) : true)).Sum(x => x.QUANTITY * x.PRICE > 10000000 ? x.PRICE / 2 : x.PRICE);
                        }
                        if (e.GROUP_1_ID.Contains("6273") && e.GROUP_2_ID.EndsWith("C"))
                        {
                            Col1data = dataCP_PB1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(checkID != null ? checkID : e.GROUP_1_ID + e.GROUP_2_ID.Substring(0, 4) + "B") && (OrgArea.ContainsKey(area) ? x.ORG_CODE.Contains(OrgArea[area]) : true)).Sum(x => x.QUANTITY * x.PRICE);
                            Col1dataNext = dataCP_PB1_next.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(checkID != null ? checkID : e.GROUP_1_ID + e.GROUP_2_ID.Substring(0, 4) + "B") && (OrgArea.ContainsKey(area) ? x.ORG_CODE.Contains(OrgArea[area]) : true)).Sum(x => x.QUANTITY * x.PRICE);
                            Col2data = dataCP_BS.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(checkID != null ? checkID : e.GROUP_1_ID + e.GROUP_2_ID.Substring(0, 4) + "B") && (OrgArea.ContainsKey(area) ? x.ORG_CODE.Contains(OrgArea[area]) : true)).Sum(x => x.QUANTITY * x.PRICE);
                        }

                        item = new ChiPhiQT21InReport
                        {
                            Group_1_ID = e.GROUP_1_ID,
                            Group_2_ID = e.GROUP_2_ID,
                            Stt = e.STT,
                            name = e.GROUP_NAME,
                            ValuePlanYear = Col1data??0,
                            ValueAdditionPlan = Col2data??0,
                            ValuePlantNextYear = Col1dataNext??0,
                            IsBold = e.IS_BOLD,
                            ValueUocThucHien = dataUTH?.UOC_THUC_HIEN ?? 0,
                            ValueTH9T = dataUTH?.TH9T ?? 0,
                        };
                       
                    }

                    data.chiPhiQT21InReports.Add(item);
                }
                data.chiPhiQT21InReports.ForEach(x =>
                {
                    if (x.Group_1_ID == "6277" && x.Group_2_ID.EndsWith("B2"))
                    {
                        var Codelen = x.Group_2_ID.Length - 1; ;
                        x.ValueAdditionPlan = data.chiPhiQT21InReports.Where(y => y.Group_1_ID == "6277" && y.Group_2_ID == (x.Group_2_ID.Substring(0, Codelen) + "3")).Sum(y => y.ValueAdditionPlan / 2);
                        x.ValuePlanYear = data.chiPhiQT21InReports.Where(y => y.Group_1_ID == "6277" && y.Group_2_ID == (x.Group_2_ID.Substring(0, Codelen) + "3")).Sum(y => y.ValuePlanYear / 2);
                        x.ValuePlantNextYear = data.chiPhiQT21InReports.Where(y => y.Group_1_ID == "6277" && y.Group_2_ID == (x.Group_2_ID.Substring(0, Codelen) + "3")).Sum(y => y.ValuePlantNextYear / 2);
                    }
                });
                data.chiPhiQT21InReports.ForEach(x =>
                {
                    if (x.Group_1_ID == "6277" && x.Group_2_ID.EndsWith("B"))
                    {
                        x.ValueAdditionPlan = data.chiPhiQT21InReports.Where(y => y.Group_1_ID == "6277" && (y.Group_2_ID == x.Group_2_ID + "1" || y.Group_2_ID == x.Group_2_ID + "2")).Sum(y => y.ValueAdditionPlan);
                        x.ValuePlanYear = data.chiPhiQT21InReports.Where(y => y.Group_1_ID == "6277" && (y.Group_2_ID == x.Group_2_ID + "1" || y.Group_2_ID == x.Group_2_ID + "2")).Sum(y => y.ValuePlanYear);
                        x.ValuePlantNextYear = data.chiPhiQT21InReports.Where(y => y.Group_1_ID == "6277" && (y.Group_2_ID == x.Group_2_ID + "1" || y.Group_2_ID == x.Group_2_ID + "2")).Sum(y => y.ValuePlantNextYear);
                    }
                });
                List<string> listParrent = new List<string> { "G002", "G003", "G004", "G005", "G006", "G007" };
                data.chiPhiQT21InReports.ForEach(x =>
                {
                    if (x.Group_1_ID == "6277" && listParrent.Contains(x.Group_2_ID))
                    {

                        x.ValueAdditionPlan = data.chiPhiQT21InReports.Where(y => y.Group_1_ID == "6277" && (y.Group_2_ID == x.Group_2_ID + "AA" || y.Group_2_ID == x.Group_2_ID + "AB" || y.Group_2_ID == x.Group_2_ID + "A" || y.Group_2_ID == x.Group_2_ID + "B")).Sum(y => y.ValueAdditionPlan);
                        x.ValuePlantNextYear = data.chiPhiQT21InReports.Where(y => y.Group_1_ID == "6277" && (y.Group_2_ID == x.Group_2_ID + "AA" || y.Group_2_ID == x.Group_2_ID + "AB" || y.Group_2_ID == x.Group_2_ID + "A" || y.Group_2_ID == x.Group_2_ID + "B")).Sum(y => y.ValuePlantNextYear);
                        x.ValuePlanYear=data.chiPhiQT21InReports.Where(y => y.Group_1_ID == "6277" && (y.Group_2_ID == x.Group_2_ID + "AA" || y.Group_2_ID == x.Group_2_ID + "AB" || y.Group_2_ID == x.Group_2_ID + "A" || y.Group_2_ID == x.Group_2_ID + "B")).Sum(y => y.ValuePlanYear);

                    }
                });
                List<string> ListchildCode = new List<string> { "G001", "G002", "G003", "G004", "G005", "G006", "G007", "G008", "G009", "G010", "G011", "G012", "G019" };
                data.chiPhiQT21InReports.ForEach(x =>
                {
                    if (x.Group_1_ID == "6277" && string.IsNullOrEmpty(x.Group_2_ID))
                    {
                        x.ValueAdditionPlan = data.chiPhiQT21InReports.Where(y => y.Group_1_ID == "6277" && ListchildCode.Contains(y.Group_2_ID) ).Sum(y => y.ValueAdditionPlan);
                        x.ValuePlantNextYear = data.chiPhiQT21InReports.Where(y => y.Group_1_ID == "6277" && ListchildCode.Contains(y.Group_2_ID)).Sum(y => y.ValuePlantNextYear);
                        x.ValuePlanYear = data.chiPhiQT21InReports.Where(y => y.Group_1_ID == "6277" && ListchildCode.Contains(y.Group_2_ID)).Sum(y => y.ValuePlanYear);
                    }
                });
              
                data.chiPhiQT21InReports.ForEach(x =>
                {
                    if (x.IsBold == true)
                    {
                        x.ValueTH9T=data.chiPhiQT21InReports.Where(y => y.Group_1_ID == x.Group_1_ID).Sum(y => y.ValueTH9T);
                        x.ValueUocThucHien = data.chiPhiQT21InReports.Where(y => y.Group_1_ID == x.Group_1_ID).Sum(y => y.ValueUocThucHien);
                    }
                });
                data.chiPhiQT21InReports.ForEach(x =>
                {
                    if (x.IsBold == true)
                    {
                        x.ValueUocThucHien = data.chiPhiQT21InReports.Where(y => y.Group_1_ID == x.Group_1_ID).Sum(y => y.ValueUocThucHien);
                    }
                }

                );

                var boldReports = data.chiPhiQT21InReports.Where(x => x.IsBold == true).ToList();
                var SumtotalValueAfterAdditionPlan = boldReports.Sum(x => x.ValueAfterAdditionPlan);
                var SumTotalValuePlantNextYear = boldReports.Sum(x => x.ValuePlantNextYear);
                var SumtotalValuePlanYear = boldReports.Sum(x => x.ValuePlanYear);
                var SumValueAdditionPlan = boldReports.Sum(x => x.ValueAdditionPlan);
                var SumtotalValuePercentPlant = boldReports.Sum(x => x.ValuePercentPlant);
                var sumUTH = boldReports.Sum(x => x.ValueUocThucHien);
                var sumTH9T = boldReports.Sum(x => x.ValueTH9T);
                data.chiPhiQT21InReports.Insert(0, new ChiPhiQT21InReport
                {
                    name = "Tổng chi Phí",
                    ValuePlanYear = SumtotalValuePlanYear ,
                    ValueAdditionPlan = SumValueAdditionPlan ,
                    ValuePlantNextYear = SumTotalValuePlantNextYear ,
                    ValueAfterAdditionPlan=SumtotalValueAfterAdditionPlan,
                    IsBold = true,
                    ValueUocThucHien = sumUTH ,
                    ValueTH9T = sumTH9T ,
                });

                data.chiPhiQT21InReports.ForEach(x =>
                {
                    x.ValueAfterAdditionPlan = x.ValuePlanYear + x.ValueAdditionPlan;
                    x.ValuePercentPlant = x.ValueAfterAdditionPlan == 0 ? 0 : x.ValueUocThucHien / x.ValueAfterAdditionPlan;
                });


                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ReportDataCenter GenDataBM01D(int year)
        {
            try
            {
                var data = new ReportDataCenter();

                data.BM01D = GetElementsBM01D(year);
                foreach (var d in data.BM01D.OrderByDescending(x => x.Id))
                {
               
                    var child = data.BM01D.Where(x => x.Parent == d.Id).ToList();
                    d.Col1 = child.Count() == 0 ? d.Col1 : child.Sum(x => x.Col1);
                    d.Col20 = child.Count() == 0 ? d.Col20 : child.Sum(x => x.Col20);
                    d.Col8 = child.Count() == 0 ? d.Col8 : child.Sum(x => x.Col8);
                    d.Col9 = child.Count() == 0 ? d.Col9 : child.Sum(x => x.Col9);
                    d.Col10 = child.Count() == 0 ? d.Col10 : child.Sum(x => x.Col10);
                    d.Col11= child.Count() == 0 ? d.Col11 : child.Sum(x => x.Col11);
                    d.Col12 = child.Count() == 0 ? d.Col12 : child.Sum(x => x.Col12);
                    d.Col13 = child.Count() == 0 ? d.Col13 : child.Sum(x => x.Col13);
                    d.Col4 = child.Count() == 0 ? d.Col4 : child.Sum(x => x.Col4);
                    d.Col5 = child.Count() == 0 ? d.Col5 : child.Sum(x => x.Col5);

                }
                var da= data.BM01D.Where(x => x.Stt == "A" || x.Stt == "I" || x.Stt == "B" || x.Stt == "II");
                foreach (var d in data.BM01D.Where(x=>x.Stt=="A"|| x.Stt=="I"||x.Stt=="B"||x.Stt=="II"))
                {
                    var Listchinld= Findnode1D(d.Id,data);
                    d.Col1 = data.BM01D.Where(x => Listchinld.Contains(x.Id)).Select(x => new {x.Code ,x.Col1}).Distinct().Sum(x=>x.Col1);
                    d.Col20 = data.BM01D.Where(x => Listchinld.Contains(x.Id)).Select(x => new { x.Code, x.Col20 }).Distinct().Sum(x => x.Col20);
                    d.Col8 = data.BM01D.Where(x => Listchinld.Contains(x.Id)).Select(x => new { x.Code, x.Col8 }).Distinct().Sum(x => x.Col8);
                    d.Col9 = data.BM01D.Where(x => Listchinld.Contains(x.Id)).Select(x => new { x.Code, x.Col9 }).Distinct().Sum(x => x.Col9);
                    d.Col10 = data.BM01D.Where(x => Listchinld.Contains(x.Id)).Select(x => new { x.Code, x.Col10 }).Distinct().Sum(x => x.Col10);
                    d.Col11 = data.BM01D.Where(x => Listchinld.Contains(x.Id)).Select(x => new { x.Code, x.Col11 }).Distinct().Sum(x => x.Col11);
                    d.Col12 = data.BM01D.Where(x => Listchinld.Contains(x.Id)).Select(x => new { x.Code, x.Col12 }).Distinct().Sum(x => x.Col12);
                    d.Col13 = data.BM01D.Where(x => Listchinld.Contains(x.Id)).Select(x => new { x.Code, x.Col13 }).Distinct().Sum(x => x.Col13);
                    d.Col4 = data.BM01D.Where(x => Listchinld.Contains(x.Id)).Select(x => new { x.Code, x.Col4 }).Distinct().Sum(x => x.Col4);
                    d.Col5 = data.BM01D.Where(x => Listchinld.Contains(x.Id)).Select(x => new { x.Code, x.Col5 }).Distinct().Sum(x => x.Col5);

                }

                return data;
            }
            catch (Exception ex)
            {
                return new ReportDataCenter();
            }
        }

        public List<ReportModel1D> GetElementsBM01D(int year)
        {

            try
            {
                var data = new List<ReportModel1D>();
                var projects = UnitOfWork.Repository<ProjectRepo>().Queryable().Where(x => x.YEAR == year).ToList();
               
                var DataTTB = UnitOfWork.Repository<DauTuTrangThietBiDataRepo>().Queryable().Where(x => x.TIME_YEAR == year).ToList();
                var DataXD = UnitOfWork.Repository<DauTuXayDungDataRepo>().Queryable().Where(x => x.TIME_YEAR == year).ToList();
                var DataTTBcenter = UnitOfWork.Repository<DauTuTrangThietBiProfitCenterRepo>().GetAll().ToList();
                var DataXDcenter = UnitOfWork.Repository<DauTuXayDungProfitCenterRepo>().GetAll().ToList();
                var DataGn=UnitOfWork.Repository<InputGtgnRepo>().Queryable().Where(x => x.TIME_YEAR == year).ToList();
                var BM2107 = GenDataBM2107(year, 1, "PB1", "TB", "");
                var BM2108 = GenDataBM2108(year, 1, "PB1", "TB", "");
                data.Add(new ReportModel1D
                {
                    Id = "A",
                    Stt = "A",
                    Name = "Dự án chuyển tiếp kỳ truớc",
                    NameExcel = "Dự án chuyển tiếp kỳ truớc",
                    IsBold = true,
                });
                data.Add(new ReportModel1D
                {
                    Id = "A.I",
                    Stt = "I",
                    Name = "Đầu tư trang thiết bị",
                    NameExcel = "Đầu tư trang thiết bị",
                    Parent = "A",
                    IsBold = true,
                });

                data.Add(new ReportModel1D
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
                    var codeTTB = DataTTBcenter.FirstOrDefault(x => x.PROJECT_CODE == p.CODE)?.CODE;

                    data.Add(new ReportModel1D
                    {

                        Code = p.CODE,
                        Id = "A.I.1" + p.CODE,
                        Stt = "1." + orderAI1.ToString(),
                        Name = p.NAME,
                        NameExcel = "Các dự án chuẩn bị đầu tư",
                        Parent = "A.I.1",
                        Col1 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_1),
                        Col5= DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_5),
                        Col20 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_1) * (p.CAPITAL != 0 ? p.CAPITAL / 100 : 1),
                        Col4 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB).Sum(x=>x.VALUE_4),

                        Col8 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_9),
                        Col9 = 0,
                        Col10=0,
                        Col11=0,
                        Col12=0,
                        Col13 = DataGn.Where(x => x.PROJECT_CODE == p.CODE).Sum(x => x.KH) ?? 0,
                    });
                    
                    orderAI1 += 1;
                }

                data.Add(new ReportModel1D
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
                    var codeTTB = DataTTBcenter.FirstOrDefault(x => x.PROJECT_CODE == p.CODE)?.CODE;
                    data.Add(new ReportModel1D
                    {
                        Code=p.CODE,
                        Id = "A.I.2" + p.CODE,
                        Stt = "2." + orderAI2.ToString(),
                        Name = p.NAME,
                        NameExcel = "Các dự án chuẩn bị đầu tư",
                        Parent = "A.I.2",
                        Col1 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_1),
                        Col5 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_5),
                        Col20 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_1)* (p.CAPITAL!=0? p.CAPITAL/100:1),
                        Col4 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB).Sum(x=>x.VALUE_4),
                        Col8 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_9),
                        Col9 = 0,
                        Col10 = 0,
                        Col11 = 0,
                        Col12 = 0,
                        Col13 = DataGn.Where(x => x.PROJECT_CODE == p.CODE).Sum(x => x.KH) ?? 0,
                    });
                    orderAI2 += 1;
                }


                //data.Add(new ReportModel2B
                //{
                //    Id = "A.I.3",
                //    Stt = "3",
                //    Name = "Đầu tư trang thiết bị lẻ",
                //    NameExcel = "Đầu tư trang thiết bị lẻ",
                //    Parent = "A.I",
                //    IsBold = true,
                //});

                //var orderAI3 = 1;
                //foreach (var p in projects.Where(x => x.LOAI_HINH == "TTB" && x.TYPE == "TTB-LE" && x.CHUYEN_TIEP == true))
                //{
                //    var codeTTB = DataTTBcenter.FirstOrDefault(x => x.PROJECT_CODE == p.CODE)?.CODE;
                //    data.Add(new ReportModel2B
                //    {
                //        Code=p.CODE,
                //        Id = "A.I.3" + p.CODE,
                //        Stt = "3." + orderAI3.ToString(),
                //        Name = p.NAME,
                //        NameExcel = "Các dự án chuẩn bị đầu tư",
                //        Parent = "A.I.3",
                //        Col1 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_1),
                //        Col2 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.FirstOrDefault(x => !string.IsNullOrEmpty(x.VALUE_2))?.VALUE_2,
                //        Col5 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_5 * x.VALUE_6) ?? 0,
                //    });
                //    orderAI3 += 1;
                //}

                data.Add(new ReportModel1D
                {
                    Id = "A.II",
                    Stt = "II",
                    Name = "Đầu tư xây dựng cơ bản",
                    NameExcel = "Đầu tư xây dựng cơ bản",
                    Parent = "A",
                    IsBold = true,
                });
                data.Add(new ReportModel1D
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
                    var codeDataXD = DataXDcenter.FirstOrDefault(x => x.PROJECT_CODE == p.CODE)?.CODE;
                    data.Add(new ReportModel1D
                    {
                        Code = p.CODE,
                        Id = "A.II.1" + p.CODE,
                        Stt = "1." + orderAII1.ToString(),
                        Name = p.NAME,
                        NameExcel = "Các dự án chuẩn bị đầu tư",
                        Parent = "A.II.1",
                        Col1 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.Sum(x => x.VALUE_1),
                        Col5 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.Sum(x => x.VALUE_5),
                        Col20 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.Sum(x => x.VALUE_1)*(p.CAPITAL!=0? p.CAPITAL/100:1),
                        Col4 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.Sum(x => x.VALUE_4) ?? 0,
                        Col8 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.Sum(x => x.VALUE_7),
                        Col9 = BM2107.Where(x=>x.Code==codeDataXD).Sum(x=>x.Col7)+ BM2108.Where(x=>x.Code==codeDataXD).Sum(x=>x.Col10),
                        Col10 = BM2107.Where(x => x.Code == codeDataXD).Sum(x => x.Col6) + BM2108.Where(x => x.Code == codeDataXD).Sum(x => x.Col9),
                        Col11 = BM2107.Where(x => x.Code == codeDataXD).Sum(x => x.Col6) + BM2108.Where(x => x.Code == codeDataXD).Sum(x => x.Col9),
               
                        Col12= (BM2107.Where(x => x.Code == codeDataXD).Sum(x => x.Col6) + BM2108.Where(x => x.Code == codeDataXD).Sum(x => x.Col9)) != 0 ? BM2108.Where(x => x.Code == codeDataXD).Sum(x => x.Col6) + BM2108.Where(x => x.Code == codeDataXD).Sum(x => x.Col9)/ DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.Sum(x => x.VALUE_7):0,
                        Col13 = DataGn.Where(x => x.PROJECT_CODE == p.CODE).Sum(x => x.KH) ?? 0,
                    });
                    orderAII1 += 1;
                }

                data.Add(new ReportModel1D
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
                    var codeDataXD = DataXDcenter.FirstOrDefault(x => x.PROJECT_CODE == p.CODE)?.CODE;
                    data.Add(new ReportModel1D
                    {
                        Code = p.CODE,
                        Id = "A.II.2" + p.CODE,
                        Stt = "2." + orderAII2.ToString(),
                        Name = p.NAME,
                        NameExcel = "Các dự án chuẩn bị đầu tư",
                        Parent = "A.II.2",
                        Col1 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.Sum(x => x.VALUE_1),
                        Col5 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.Sum(x => x.VALUE_5),
                        Col4 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.Sum(x => x.VALUE_4) ?? 0,
                        Col20 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.Sum(x => x.VALUE_1)*(p.CAPITAL!=0? p.CAPITAL/100:1),
          
                        Col8 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.Sum(x => x.VALUE_7),
                        Col9 = BM2107.Where(x => x.Code == codeDataXD).Sum(x => x.Col7) + BM2108.Where(x => x.Code == codeDataXD).Sum(x => x.Col10),
                        Col10 = BM2107.Where(x => x.Code == codeDataXD).Sum(x => x.Col6) + BM2108.Where(x => x.Code == codeDataXD).Sum(x => x.Col9),
                        Col11 = BM2107.Where(x => x.Code == codeDataXD).Sum(x => x.Col6) + BM2108.Where(x => x.Code == codeDataXD).Sum(x => x.Col9),

                        Col12 = (BM2107.Where(x => x.Code == codeDataXD).Sum(x => x.Col6) + BM2108.Where(x => x.Code == codeDataXD).Sum(x => x.Col9)) != 0 ? BM2108.Where(x => x.Code == codeDataXD).Sum(x => x.Col6) + BM2108.Where(x => x.Code == codeDataXD).Sum(x => x.Col9) / DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.Sum(x => x.VALUE_7) : 0,
                        Col13 = DataGn.Where(x => x.PROJECT_CODE == p.CODE).Sum(x => x.KH) ?? 0,
                    });
                    orderAII2 += 1;
                }

                data.Add(new ReportModel1D
                {
                    
                    Id = "B",
                    Stt = "B",
                    Name = "Dự án đầu tư mới",
                    NameExcel = "Dự án đầu tư mới",
                    IsBold = true,
                });
                data.Add(new ReportModel1D
                {
                    Id = "B.I",
                    Stt = "I",
                    Name = "Đầu tư trang thiết bị",
                    NameExcel = "Đầu tư trang thiết bị",
                    Parent = "B",
                    IsBold = true,
                });
                data.Add(new ReportModel1D
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
                    var codeTTB = DataTTBcenter.FirstOrDefault(x => x.PROJECT_CODE == p.CODE)?.CODE;
                    data.Add(new ReportModel1D
                    {
                        Code = p.CODE,
                        Id = "B.I.1" + p.CODE,
                        Stt = "1." + orderBI1.ToString(),
                        Name = p.NAME,
                        NameExcel = "Các dự án chuẩn bị đầu tư",
                        Parent = "B.I.1",
                        Col1 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_1),
                        Col5 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_5),
                        Col4 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB).Sum(x=>x.VALUE_4),

                        Col20 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_1)*(p.CAPITAL!=0? p.CAPITAL/100:1),
                        //Col5 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_5 * x.VALUE_6) ?? 0,
                        Col8 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_9),
                        Col9 = 0,
                        Col10 = 0,
                        Col11 = 0,
                        Col12 = 0,
                        Col13 = DataGn.Where(x => x.PROJECT_CODE == p.CODE).Sum(x => x.KH) ?? 0,
                    }) ;
                    orderBI1 += 1;
                }

                data.Add(new ReportModel1D
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
                    var codeTTB = DataTTBcenter.FirstOrDefault(x => x.PROJECT_CODE == p.CODE)?.CODE;
                    data.Add(new ReportModel1D
                    {
                        Code=p.CODE,
                        Id = "B.I.2" + p.CODE,
                        Stt = "2." + orderBI2.ToString(),
                        Name = p.NAME,
                        NameExcel = "Các dự án thực hiện đầu tư",
                        Parent = "B.I.2",
                        Col1 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_1),
                        Col4 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_4),
                        Col5 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_5),
                        Col20 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_1)* (p.CAPITAL!=0? p.CAPITAL/100:1),
                        //Col5 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_5 * x.VALUE_6) ?? 0,
                        Col8 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_9),
                        Col9 = 0,
                        Col10 = 0,
                        Col11 = 0,
                        Col12 = 0,
                        Col13 = DataGn.Where(x => x.PROJECT_CODE == p.CODE).Sum(x => x.KH) ?? 0,
                    });
                    orderBI2 += 1;
                }

                data.Add(new ReportModel1D
                {
                    Id = "B.II",
                    Stt = "II",
                    Name = "Đầu tư xây dựng cơ bản",
                    NameExcel = "Đầu tư xây dựng cơ bản",
                    Parent = "B",
                    IsBold = true,
                });
                data.Add(new ReportModel1D
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
                    var codeDataXD = DataXDcenter.FirstOrDefault(x => x.PROJECT_CODE == p.CODE)?.CODE;
                    data.Add(new ReportModel1D
                    {
                        Code=p.CODE,
                        Id = "B.II.1" + p.CODE,
                        Stt = "1." + orderBII1.ToString(),
                        Name = p.NAME,
                        NameExcel = "Các dự án chuẩn bị đầu tư",
                        Parent = "B.II.1",
                        Col1 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.Sum(x => x.VALUE_1),
                        Col5 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.Sum(x => x.VALUE_5),
                        Col4 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.Sum(x => x.VALUE_4),
                        Col20 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.Sum(x=>x.VALUE_1)*(p.CAPITAL!=0? p.CAPITAL/100:1),
                        //Col5 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.Sum(x => x.VALUE_5) ?? 0,
                        Col8 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.Sum(x => x.VALUE_7),
                        Col9 = BM2107.Where(x => x.Code == codeDataXD).Sum(x => x.Col7) + BM2108.Where(x => x.Code == codeDataXD).Sum(x => x.Col10),
                        Col10 = BM2107.Where(x => x.Code == codeDataXD).Sum(x => x.Col6) + BM2108.Where(x => x.Code == codeDataXD).Sum(x => x.Col9),
                        Col11 = BM2107.Where(x => x.Code == codeDataXD).Sum(x => x.Col6) + BM2108.Where(x => x.Code == codeDataXD).Sum(x => x.Col9),
                        // Col12= Col10/Col8
                        Col12 = (BM2107.Where(x => x.Code == codeDataXD).Sum(x => x.Col6) + BM2108.Where(x => x.Code == codeDataXD).Sum(x => x.Col9)) != 0 ? (BM2107.Where(x => x.Code == codeDataXD).Sum(x => x.Col6) + BM2108.Where(x => x.Code == codeDataXD).Sum(x => x.Col9)) / (BM2107.Where(x => x.Code == codeDataXD).Sum(x => x.Col7) + BM2108.Where(x => x.Code == codeDataXD).Sum(x => x.Col10)) : 0,

                        Col13 = DataGn.Where(x => x.PROJECT_CODE == p.CODE).Sum(x => x.KH) ?? 0,
                    });
                    orderBII1 += 1;
                }

                data.Add(new ReportModel1D
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
                    var codeDataXD = DataXDcenter.FirstOrDefault(x => x.PROJECT_CODE == p.CODE)?.CODE;
                    data.Add(new ReportModel1D
                    {
                        Code=p.CODE,
                        Id = "B.II.2" + p.CODE,
                        Stt = "2." + orderBII2.ToString(),
                        Name = p.NAME,
                        NameExcel = "Các dự án chuẩn bị đầu tư",
                        Parent = "B.II.2",
                        Col1 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.Sum(x => x.VALUE_1),
                        Col20 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.Sum(x => x.VALUE_1) * (p.CAPITAL!=0? p.CAPITAL/100:1),
                        Col5 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.Sum(x => x.VALUE_5) ?? 0,
                        Col4 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.Sum(x => x.VALUE_4) ?? 0,
                        Col8 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.Sum(x => x.VALUE_7),
                        Col9 = BM2107.Where(x => x.Code == codeDataXD).Sum(x => x.Col7) + BM2108.Where(x => x.Code == codeDataXD).Sum(x => x.Col10),
                        Col10 = BM2107.Where(x => x.Code == codeDataXD).Sum(x => x.Col6) + BM2108.Where(x => x.Code == codeDataXD).Sum(x => x.Col9),
                        Col11 = BM2107.Where(x => x.Code == codeDataXD).Sum(x => x.Col6) + BM2108.Where(x => x.Code == codeDataXD).Sum(x => x.Col9),
                        // Col12= Col10/Col8
                        Col12 = (BM2107.Where(x => x.Code == codeDataXD).Sum(x => x.Col6) + BM2108.Where(x => x.Code == codeDataXD).Sum(x => x.Col9)) != 0 ? (BM2107.Where(x => x.Code == codeDataXD).Sum(x => x.Col6) + BM2108.Where(x => x.Code == codeDataXD).Sum(x => x.Col9)) / (BM2107.Where(x => x.Code == codeDataXD).Sum(x => x.Col7) + BM2108.Where(x => x.Code == codeDataXD).Sum(x => x.Col10)) : 0,
                        Col13 = DataGn.Where(x => x.PROJECT_CODE == p.CODE).Sum(x => x.KH) ?? 0,

                    });
                    orderBII2 += 1;
                }

                return data;
            }
            catch (Exception ex)
            {
                return new List<ReportModel1D>();
            }
        }
        public List<ReportModel2B> GetElementsBM01A(int year)
        {
            try
            {
                var data = new List<ReportModel2B>();
                var projects = UnitOfWork.Repository<ProjectRepo>().Queryable().Where(x => x.YEAR == year).ToList();
                var DataTTB = UnitOfWork.Repository<DauTuTrangThietBiDataRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.STATUS=="03").ToList();
                var DataXD = UnitOfWork.Repository<DauTuXayDungDataRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.STATUS == "03").ToList();
                var DataTTBcenter = UnitOfWork.Repository<DauTuTrangThietBiProfitCenterRepo>().GetAll().ToList();
                var DataXDcenter = UnitOfWork.Repository<DauTuXayDungProfitCenterRepo>().GetAll().ToList();
               

                data.Add(new ReportModel2B
                {
                    Id = "Total",
                    Stt = "",
                    Name = "Tổng cộng",
                    NameExcel = "Tổng cộng",
                    IsBold = true,
                });
                data.Add(new ReportModel2B
                {
                    Id = "A",
                    Stt = "A",
                    Name = "Dự án chuyển tiếp kỳ truớc",
                    NameExcel = "Dự án chuyển tiếp kỳ truớc",
                    IsBold = true,
                });
                data.Add(new ReportModel2B
                {
                    Id = "A.I",
                    Stt = "I",
                    Name = "Đầu tư trang thiết bị",
                    NameExcel = "Đầu tư trang thiết bị",
                    Parent = "A",
                    IsBold = true,
                });

                data.Add(new ReportModel2B
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
                    var codeTTB = DataTTBcenter.FirstOrDefault(x => x.PROJECT_CODE == p.CODE)?.CODE;
                    data.Add(new ReportModel2B
                    {
                        Code=p.CODE,
                        Id = "A.I.1" + p.CODE,
                        Stt = "1." + orderAI1.ToString(),
                        Name = p.NAME,
                        NameExcel = "Các dự án chuẩn bị đầu tư",
                        Parent = "A.I.1",
                        Col1=DataTTB.Where(x=>x.DAU_TU_PROFIT_CENTER_CODE==codeTTB)?.Sum(x=>x.VALUE_1),
                        Col2 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.FirstOrDefault(x=>!string.IsNullOrEmpty(x.VALUE_2))?.VALUE_2,
                        Col3 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.FirstOrDefault(x => !string.IsNullOrEmpty(x.VALUE_3))?.VALUE_3,
                        Col5 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_7),
                        Col6 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_6),
                        Col7 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_9) ,
                        Col8 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.FirstOrDefault(x => !string.IsNullOrEmpty(x.VALUE_8))?.VALUE_8,
                        Col9 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.FirstOrDefault(x => !string.IsNullOrEmpty(x.DESCRIPTION))?.DESCRIPTION,
                    });
                    orderAI1 += 1;
                }

                data.Add(new ReportModel2B
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
                    var codeTTB = DataTTBcenter.FirstOrDefault(x => x.PROJECT_CODE == p.CODE)?.CODE;

                    data.Add(new ReportModel2B
                    {
                        Code= p.CODE,
                        Id = "A.I.2" + p.CODE,
                        Stt = "2." + orderAI2.ToString(),
                        Name = p.NAME,
                        NameExcel = "Các dự án chuẩn bị đầu tư",
                        Parent = "A.I.2",
                        Col1 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_1),
                        Col2 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.FirstOrDefault(x => !string.IsNullOrEmpty(x.VALUE_2))?.VALUE_2,
                        Col3 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.FirstOrDefault(x => !string.IsNullOrEmpty(x.VALUE_3))?.VALUE_3,
                        Col5 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_7),
                        Col6 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_6),
                        Col7 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_9) ,
                        Col8 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.FirstOrDefault(x => !string.IsNullOrEmpty(x.VALUE_8))?.VALUE_8,
                        Col9 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.FirstOrDefault(x => !string.IsNullOrEmpty(x.DESCRIPTION))?.DESCRIPTION,
                    });
                    orderAI2 += 1;
                }


                data.Add(new ReportModel2B
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
                    var codeTTB = DataTTBcenter.FirstOrDefault(x => x.PROJECT_CODE == p.CODE)?.CODE;
                    data.Add(new ReportModel2B
                    {
                        Code=p.CODE,
                        Id = "A.I.3" + p.CODE,
                        Stt = "3." + orderAI3.ToString(),
                        Name = p.NAME,
                        NameExcel = "Các dự án chuẩn bị đầu tư",
                        Parent = "A.I.3",
                        Col1 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_1),
                        Col2 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.FirstOrDefault(x => !string.IsNullOrEmpty(x.VALUE_2))?.VALUE_2,
                        Col3 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.FirstOrDefault(x => !string.IsNullOrEmpty(x.VALUE_3))?.VALUE_3,
                        Col5 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_7),
                        Col6 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_6),
                        Col7 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_9),
                        Col8 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.FirstOrDefault(x => !string.IsNullOrEmpty(x.VALUE_8))?.VALUE_8,
                        Col9 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.FirstOrDefault(x => !string.IsNullOrEmpty(x.DESCRIPTION))?.DESCRIPTION,
                    });
                    orderAI3 += 1;
                }

                data.Add(new ReportModel2B
                {
                    Id = "A.II",
                    Stt = "II",
                    Name = "Đầu tư xây dựng cơ bản",
                    NameExcel = "Đầu tư xây dựng cơ bản",
                    Parent = "A",
                    IsBold = true,
                });
                data.Add(new ReportModel2B
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
                    var codeDataXD = DataXDcenter.FirstOrDefault(x => x.PROJECT_CODE == p.CODE)?.CODE;
                    data.Add(new ReportModel2B
                    {
                        Code=p.CODE,
                        Id = "A.II.1" + p.CODE,
                        Stt = "1." + orderAII1.ToString(),
                        Name = p.NAME,
                        NameExcel = "Các dự án chuẩn bị đầu tư",
                        Parent = "A.II.1",
                        Col1 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.Sum(x => x.VALUE_1),
                        Col2 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.FirstOrDefault(x => !string.IsNullOrEmpty(x.VALUE_2))?.VALUE_2,
                        Col3 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.FirstOrDefault(x => !string.IsNullOrEmpty(x.VALUE_3))?.VALUE_3,
                        Col5 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.Sum(x => x.VALUE_5),
                        Col8 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.FirstOrDefault(x => !string.IsNullOrEmpty(x.VALUE_6))?.VALUE_6,
                        Col9 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.FirstOrDefault(x => !string.IsNullOrEmpty(x.DESCRIPTION))?.DESCRIPTION,
                    });
                    orderAII1 += 1;
                }

                data.Add(new ReportModel2B
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
                    var codeDataXD = DataXDcenter.FirstOrDefault(x => x.PROJECT_CODE == p.CODE)?.CODE;
                    data.Add(new ReportModel2B
                    {
                        Code= p.CODE,
                        Id = "A.II.2" + p.CODE,
                        Stt = "2." + orderAII2.ToString(),
                        Name = p.NAME,
                        NameExcel = "Các dự án chuẩn bị đầu tư",
                        Parent = "A.II.2",
                        Col1 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.Sum(x => x.VALUE_1),
                        Col2 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.FirstOrDefault(x => !string.IsNullOrEmpty(x.VALUE_2))?.VALUE_2,
                        Col3 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.FirstOrDefault(x => !string.IsNullOrEmpty(x.VALUE_3))?.VALUE_3,
                        Col5 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.Sum(x => x.VALUE_5),
                        //Col6 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.Sum(x => x.VALUE_6),
                        //Col8 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.Sum(x => x.VALUE_10), = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.Sum(x => x.VALUE_5) * DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == DataXD).Sum(x => x.VALUE_6),
                        Col8 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.FirstOrDefault(x => !string.IsNullOrEmpty(x.VALUE_6))?.VALUE_6,
                        Col9 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.FirstOrDefault(x => !string.IsNullOrEmpty(x.DESCRIPTION))?.DESCRIPTION,
                    });
                    orderAII2 += 1;
                }

                data.Add(new ReportModel2B
                {
                    Id = "B",
                    Stt = "B",
                    Name = "Dự án đầu tư mới",
                    NameExcel = "Dự án đầu tư mới",
                    IsBold = true,
                });
                data.Add(new ReportModel2B
                {
                    Id = "B.I",
                    Stt = "I",
                    Name = "Đầu tư trang thiết bị",
                    NameExcel = "Đầu tư trang thiết bị",
                    Parent = "B",
                    IsBold = true,
                });
                data.Add(new ReportModel2B
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
                    var codeTTB = DataTTBcenter.FirstOrDefault(x => x.PROJECT_CODE == p.CODE)?.CODE;
                    data.Add(new ReportModel2B
                    {
                        Code = p.CODE,
                        Id = "B.I.1" + p.CODE,
                        Stt = "1." + orderBI1.ToString(),
                        Name = p.NAME,
                        NameExcel = "Các dự án chuẩn bị đầu tư",
                        Parent = "B.I.1",
                        Col1 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_1),
                        Col2 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.FirstOrDefault(x => !string.IsNullOrEmpty(x.VALUE_2))?.VALUE_2,
                        Col3 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.FirstOrDefault(x => !string.IsNullOrEmpty(x.VALUE_3))?.VALUE_3,
                        Col5 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_7),
                        Col6 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_6),
                        Col7 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_9) ,
                        Col8 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.FirstOrDefault(x => !string.IsNullOrEmpty(x.VALUE_8))?.VALUE_8,
                        Col9 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.FirstOrDefault(x => !string.IsNullOrEmpty(x.DESCRIPTION))?.DESCRIPTION,
                    }); ; ;
                    orderBI1 += 1;
                }

                data.Add(new ReportModel2B
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
                    var codeTTB = DataTTBcenter.FirstOrDefault(x => x.PROJECT_CODE == p.CODE)?.CODE;
                    data.Add(new ReportModel2B
                    {
                        Code = p.CODE,
                        Id = "B.I.2" + p.CODE,
                        Stt = "2." + orderBI2.ToString(),
                        Name = p.NAME,
                        NameExcel = "Các dự án thực hiện đầu tư",
                        Parent = "B.I.2",
                        Col1 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_1),
                        Col2 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.FirstOrDefault(x => !string.IsNullOrEmpty(x.VALUE_2))?.VALUE_2,
                        Col3 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.FirstOrDefault(x => !string.IsNullOrEmpty(x.VALUE_3))?.VALUE_3,
                        Col5 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_7),
                        Col6 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_6),
                        Col7 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_9),
                        Col8 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.FirstOrDefault(x => !string.IsNullOrEmpty(x.VALUE_8))?.VALUE_8,
                        Col9 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.FirstOrDefault(x => !string.IsNullOrEmpty(x.DESCRIPTION))?.DESCRIPTION,
                    }); ;
                    orderBI2 += 1;
                }

                data.Add(new ReportModel2B
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
                    var codeTTB = DataTTBcenter.FirstOrDefault(x => x.PROJECT_CODE == p.CODE)?.CODE;
                    data.Add(new ReportModel2B
                    {
                        Code=p.CODE,
                        Id = "B.I.3" + p.CODE,
                        Stt = "3." + orderBI3.ToString(),
                        Name = p.NAME,
                        NameExcel = "Các dự án chuẩn bị đầu tư",
                        Parent = "B.I.3",
                        Col1 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_1),
                        Col2 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.FirstOrDefault(x => !string.IsNullOrEmpty(x.VALUE_2))?.VALUE_2,
                        Col3 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.FirstOrDefault(x => !string.IsNullOrEmpty(x.VALUE_3))?.VALUE_3,
                        Col5 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_7),
                        Col6 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_6),
                        Col7 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.Sum(x => x.VALUE_9) ,
                        Col8 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.FirstOrDefault(x => !string.IsNullOrEmpty(x.VALUE_8))?.VALUE_8,
                        Col9 = DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeTTB)?.FirstOrDefault(x => !string.IsNullOrEmpty(x.DESCRIPTION))?.DESCRIPTION,
                    });
                    orderBI3 += 1;
                }

                data.Add(new ReportModel2B
                {
                    Id = "B.II",
                    Stt = "II",
                    Name = "Đầu tư xây dựng cơ bản",
                    NameExcel = "Đầu tư xây dựng cơ bản",
                    Parent = "B",
                    IsBold = true,
                });
                data.Add(new ReportModel2B
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
                    var codeDataXD = DataXDcenter.FirstOrDefault(x => x.PROJECT_CODE == p.CODE)?.CODE;
                    data.Add(new ReportModel2B
                    {
                        Code=p.CODE ,
                        Id = "B.II.1" + p.CODE,
                        Stt = "1." + orderBII1.ToString(),
                        Name = p.NAME,
                        NameExcel = "Các dự án chuẩn bị đầu tư",
                        Parent = "B.II.1",
                        Col1 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.Sum(x => x.VALUE_1),
                        Col2 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.FirstOrDefault(x => !string.IsNullOrEmpty(x.VALUE_2))?.VALUE_2,
                        Col3 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.FirstOrDefault(x => !string.IsNullOrEmpty(x.VALUE_3))?.VALUE_3,
                        Col5 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.Sum(x => x.VALUE_5),
                        //Col6 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.Sum(x => x.VALUE_6),
                        //Col7 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.Sum(x => x.VALUE_5) * DataTTB.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD).Sum(x => x.VALUE_6),
                        Col8 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.FirstOrDefault(x => !string.IsNullOrEmpty(x.VALUE_6))?.VALUE_6,
                        Col9 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.FirstOrDefault(x => !string.IsNullOrEmpty(x.DESCRIPTION))?.DESCRIPTION,
                    });
                    orderBII1 += 1;
                }

                data.Add(new ReportModel2B
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
                    var codeDataXD = DataXDcenter.FirstOrDefault(x => x.PROJECT_CODE == p.CODE)?.CODE;
                    data.Add(new ReportModel2B
                    {
                        Code = p.CODE,
                        Id = "B.II.2" + p.CODE,
                        Stt = "2." + orderBII2.ToString(),
                        Name = p.NAME,
                        NameExcel = "Các dự án chuẩn bị đầu tư",
                        Parent = "B.II.2",
                        Col1 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.Sum(x => x.VALUE_1),
                        Col2 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.FirstOrDefault(x => !string.IsNullOrEmpty(x.VALUE_2))?.VALUE_2,
                        Col3 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.FirstOrDefault(x => !string.IsNullOrEmpty(x.VALUE_3))?.VALUE_3,
                        Col5 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.Sum(x => x.VALUE_5),
                        //Col6 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.Sum(x => x.VALUE_6),
                        //Col7 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.Sum(x => x.VALUE_5 ),
                        Col8 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.FirstOrDefault(x => !string.IsNullOrEmpty(x.VALUE_6))?.VALUE_6,
                        Col9 = DataXD.Where(x => x.DAU_TU_PROFIT_CENTER_CODE == codeDataXD)?.FirstOrDefault(x => !string.IsNullOrEmpty(x.DESCRIPTION))?.DESCRIPTION,
                    });
                    orderBII2 += 1;
                }

                return data;
            }
            catch (Exception ex)
            {
                return new List<ReportModel2B>();
            }
        }
        public List<string> Findnode(string node, ReportDataCenter data)
        {
            List<string> nodeleave = new List<string>();

            var child = data.BM02B.Where(x => x.Parent == node).ToList();
            if (child.Count() == 0)
            {
                nodeleave.Add(node);
            }
            else
            {
                foreach (var i in child)
                {
                    nodeleave.AddRange(Findnode(i.Id, data));
                }
            }
            return nodeleave;
        }
        public List<string> Findnode1D(string node, ReportDataCenter data)
        {
            List<string> nodeleave = new List<string>();

            var child = data.BM01D.Where(x => x.Parent == node).ToList();
            if (child.Count() == 0)
            {
                nodeleave.Add(node);
            }
            else
            {
                foreach (var i in child)
                {
                    nodeleave.AddRange(Findnode1D(i.Id, data));
                }
            }
            return nodeleave;
        }

        public ReportDataCenter GenDataBM02A(int year)
        {
            try
            {
                var data = new ReportDataCenter();
                data.BM02B = GetElementsBM01A(year);

                foreach (var i in data.BM02B.OrderByDescending(x => x.Id))
                {
                    var child = data.BM02B.Where(x => x.Parent == i.Id).ToList();
                    i.Col1 = child.Count() == 0 ? i.Col1 : child.Sum(x => x.Col1);
                    //i.Col2 = child.Count() == 0 ? i.Col2 : child.Sum(x => x.Col2);
                    i.Col4 = child.Count() == 0 ? i.Col4 : child.Sum(x => x.Col4);
                    i.Col5 = child.Count() == 0 ? i.Col5 : child.Sum(x => x.Col5);
                    i.Col7 = child.Count() == 0 ? i.Col7 : child.Sum(x => x.Col7);
                    //i.Col8 = child.Count() == 0 ? i.Col8 : child.Sum(x => x.Col8);
                }
             
                foreach (var i in data.BM02B.Where(x => x.Stt=="A" || x.Stt=="B"||x.Stt=="I"||x.Stt=="II"))
                {
                   var Listchild = Findnode(i.Id, data);
                   i.Col1= data.BM02B.Where(x => Listchild.Contains(x.Id)).Select(x => new {x.Code,x.Col1}).Distinct().Sum(x => x.Col1);
                    i.Col4 = data.BM02B.Where(x => Listchild.Contains(x.Id)).Select(x => new { x.Code, x.Col4 }).Distinct().Sum(x => x.Col4);
                    i.Col5 = data.BM02B.Where(x => Listchild.Contains(x.Id)).Select(x => new { x.Code, x.Col5 }).Distinct().Sum(x => x.Col5);
                    i.Col7 = data.BM02B.Where(x => Listchild.Contains(x.Id)).Select(x => new { x.Code, x.Col7 }).Distinct().Sum(x => x.Col7);
                }

                foreach (var i in data.BM02B.Where(x=>x.Id== "Total"))
                {
                   
                    i.Col1 = data.BM02B.Where(x => x.Id == "A"|| x.Id=="B").Sum(x => x.Col1);
                    //i.Col2 = child.Count() == 0 ? i.Col2 : child.Sum(x => x.Col2);
                    i.Col4 = data.BM02B.Where(x => x.Id == "A" || x.Id == "B").Sum(x => x.Col4);    
                    i.Col5= data.BM02B.Where(x => x.Id == "A" || x.Id == "B").Sum(x => x.Col5);
                    i.Col7 = data.BM02B.Where(x => x.Id == "A" || x.Id == "B").Sum(x => x.Col7);
                    //i.Col8 = child.Count() == 0 ? i.Col8 : child.Sum(x => x.Col8);
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
                var d = UnitOfWork.Repository<Bm02bRepo>().Queryable().Where(x => x.YEAR == year).OrderBy(x => x.C_ORDER).ToList();
                foreach(var _d in d)
                {
                    data.BM_02B.Add(new BM02BModel
                    {
                        Col1 = _d.COL1,
                        Col2 = _d.COL2,
                        Col3 = _d.COL3,
                        Col4 = _d.COL4,
                        Col5 = _d.COL5,
                        Col6 = _d.COL6,
                        Col7 = _d.COL7,
                        Col8 = _d.COL8,
                        Col9 = _d.COL9,

                    });
                }
                return data;
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                return new ReportDataCenter();
            }
        }
        
        public List<T_MD_REPORT01D> GenDataBM01E(int year, string kichBan)
        {
            try
            {
                var data = UnitOfWork.Repository<Report01DRepo>().Queryable().Where(x => x.YEAR == year).ToList();
                return data;
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                return new List<T_MD_REPORT01D>();
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
                    Col2 = details1_1.Where(x => projects.Select(y => y.CODE).Contains(x.DauTuXayDungProfitCenter.PROJECT_CODE)).Sum(x => x.VALUE_5),
                    IsBold = true,
                });

                foreach (var project in projects)
                {
                    var i = new ReportModel
                    { 
                        Stt = order.ToString(),
                        Name = project.NAME,
                        Col1 = details1_1.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == project.CODE).Sum(x => x.VALUE_1) ?? 0,
                        Col2 = details1_1.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == project.CODE).Sum(x => x.VALUE_5),
                        Code= project.CODE
                    };
                    data.Add(i);

                    if (project.TYPE == "TTB-LON" || string.IsNullOrEmpty(project.TYPE))
                    {
                        data.Add(new ReportModel
                        {
                            Stt = "a",
                            Name = "Chuẩn bị đầu tư và chuẩn bị thực hiện dự án",
                            Col1 = details1_1.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == project.CODE && x.KHOAN_MUC_DAU_TU_CODE == "CBDT").Sum(x => x.VALUE_1) ?? 0,
                            Col2 = details1_1.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == project.CODE && x.KHOAN_MUC_DAU_TU_CODE == "CBDT").Sum(x => x.VALUE_5),

                        });
                        data.Add(new ReportModel
                        {
                            Stt = "b",
                            Name = "Thực hiện dự án",
                            Col1 = details1_1.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == project.CODE && x.KHOAN_MUC_DAU_TU_CODE == "TTDT").Sum(x => x.VALUE_1) ?? 0,
                            Col2 = details1_1.Where(x => x.DauTuXayDungProfitCenter.PROJECT_CODE == project.CODE && x.KHOAN_MUC_DAU_TU_CODE == "TTDT").Sum(x => x.VALUE_5),
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


                var projects = UnitOfWork.Repository<ProjectRepo>().Queryable().Where(x => x.LOAI_HINH == "TTB" && x.YEAR == year).OrderByDescending(x => x.TYPE).ToList();
                projects = string.IsNullOrEmpty(area) ? projects : projects.Where(x => x.AREA_CODE == area).ToList();
                projects = projects.Where(x => x.CODE != "DA-2024-5").ToList();
                var order = 1;
                var Dl1_1 = details1_1.Where(x => projects.Select(y => y.CODE).Contains(x.DauTuTrangThietBiProfitCenter.PROJECT_CODE));
                var DlTH = detailsTH.Where(x => projects.Select(y => y.CODE).Contains(x.DauTuTrangThietBiProfitCenter.PROJECT_CODE) && x.MONTH == month);
                var test = detailsTH.Where(x => projects.Select(y => y.CODE).Contains(x.DauTuTrangThietBiProfitCenter.PROJECT_CODE));
                var DlTHLK = detailsTH.Where(x => projects.Select(y => y.CODE).Contains(x.DauTuTrangThietBiProfitCenter.PROJECT_CODE) && x.MONTH <= month).Sum(x => x.VALUE_7) ?? 0;

                data.Add(new ReportModel
                {
                    Name = "Tổng kinh phí đầu tư XDCB",
                    Col1 = Dl1_1.Sum(x => x.VALUE_1) ?? 0,
                    Col4 = DlTH.Sum(x => x.VALUE_5),
                    Col5 = DlTH.Sum(x => x.VALUE_6),
                    Col6 = DlTH.Sum(x => x.VALUE_7) ?? 0,
                    Col7 = DlTHLK,
                    Col8 = Dl1_1.Sum(x => x.VALUE_1) == 0 ? 0 : (DlTHLK / Dl1_1.Sum(x => x.VALUE_1)) * 100,

                    IsBold = true,
                });

                foreach (var project in projects)
                {

                    var detail1_1 = details1_1.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == project.CODE);
                    var detailth = detailsTH.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == project.CODE && x.MONTH == month);
                    var detailthLK = detailsTH.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == project.CODE && x.MONTH <= month).Sum(x => x.VALUE_7) ?? 0;
                    var detail1_1CBDT = details1_1.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == project.CODE && x.KHOAN_MUC_DAU_TU_CODE == "CBDT");
                    var detailthCBDT = detailsTH.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == project.CODE && x.KHOAN_MUC_DAU_TU_CODE == "CBDT" && x.MONTH == month);
                    var detaillkCBDT = detailsTH.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == project.CODE && x.KHOAN_MUC_DAU_TU_CODE == "CBDT" && x.MONTH <= month).Sum(x => x.VALUE_7) ?? 0;
                    var detail1_1TTDT = details1_1.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == project.CODE && x.KHOAN_MUC_DAU_TU_CODE == "TTDT");
                    var detailthTTDT = detailsTH.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == project.CODE && x.KHOAN_MUC_DAU_TU_CODE == "TTDT" && x.MONTH == month);
                    var detaillkTTDT = detailsTH.Where(x => x.DauTuTrangThietBiProfitCenter.PROJECT_CODE == project.CODE && x.KHOAN_MUC_DAU_TU_CODE == "TTDT" && x.MONTH <= month).Sum(x => x.VALUE_7) ?? 0;

                    var i = new ReportModel
                    {
                        Code= project.CODE,
                        Stt = order.ToString(),
                        Name = project.NAME,
                        Col1 = detail1_1.Sum(x => x.VALUE_1) ?? 0,
                        Col4 = detailth.Sum(x => x.VALUE_5),
                        Col5 = detailth.Sum(x => x.VALUE_6),
                        Col6 = detailth.Sum(x => x.VALUE_7) ?? 0,
                        Col7 = detailthLK,
                        Col8 = detail1_1.Sum(x => x.VALUE_1) == 0 ? 0 : (detailthLK / (detail1_1.Sum(x => x.VALUE_1) ?? 0)) * 100,
                        //Col9 = detail1_1.Select(x=>x.VALUE_8)
                    };
                    data.Add(i);

                    if (project.TYPE == "TTB-LON" || string.IsNullOrEmpty(project.TYPE))
                    {

                        data.Add(new ReportModel
                        {
                            Stt = "a",
                            Name = "Chuẩn bị đầu tư và chuẩn bị thực hiện dự án",
                            Col1 = detail1_1CBDT.Sum(x => x.VALUE_1) ?? 0,
                            Col4 = detailthCBDT.Sum(x => x.VALUE_5),
                            Col5 = detailthCBDT.Sum(x => x.VALUE_6),
                            Col6 = detailthCBDT.Sum(x => x.VALUE_7) ?? 0,
                            Col7 = detaillkCBDT,
                            Col8 = detail1_1CBDT.Sum(x => x.VALUE_1) == 0 ? 0 : (detaillkCBDT / detail1_1CBDT.Sum(x => x.VALUE_1)) * 100,
                            //Col9= detail1_1CBDT.Select(x=>x.VALUE_8)

                        });
                        data.Add(new ReportModel
                        {
                            Stt = "b",
                            Name = "Thực hiện dự án",
                            Col1 = detail1_1TTDT.Sum(x => x.VALUE_1) ?? 0,
                            Col4 = detailthTTDT.Sum(x => x.VALUE_5),
                            Col5 = detailthTTDT.Sum(x => x.VALUE_6),
                            Col6 = detailthTTDT.Sum(x => x.VALUE_7) ?? 0,
                            Col7 = detaillkTTDT,
                            Col8 = detaillkTTDT == 0 ? 0 : (detaillkCBDT / detail1_1TTDT.Sum(x => x.VALUE_1)) * 100,
                            //Col9= detail1_1TTDT.Select(x=>x.VALUE_8)
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
        public List<ReportModel> GenDataBM2109(int year, int month, string phienBan, string kichBan, string area)
        {
            try
            {
                var data = new List<ReportModel>();

                var headerSCL = UnitOfWork.Repository<SuaChuaLonRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB1" && x.KICH_BAN == kichBan && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var dataSCL = UnitOfWork.Repository<SuaChuaLonDataRepo>().Queryable().Where(x => headerSCL.Contains(x.TEMPLATE_CODE)).ToList();

                var headerSCL_BS = UnitOfWork.Repository<SuaChuaLonRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB3" && x.KICH_BAN == kichBan && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var dataSCL_BS = UnitOfWork.Repository<SuaChuaLonDataRepo>().Queryable().Where(x => headerSCL_BS.Contains(x.TEMPLATE_CODE)).ToList();

                var headerSCL_TH = UnitOfWork.Repository<SuaChuaLonRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB5" && x.KICH_BAN == kichBan && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                var dataSCL_TH = UnitOfWork.Repository<SuaChuaLonDataRepo>().Queryable().Where(x => headerSCL_TH.Contains(x.TEMPLATE_CODE)).ToList();

                if (string.IsNullOrEmpty(area))
                {
                    data.AddRange(GetDataSCLByArea("MB", year, dataSCL, dataSCL_BS, dataSCL_TH, month));
                    data.AddRange(GetDataSCLByArea("MT", year, dataSCL, dataSCL_BS, dataSCL_TH, month));
                    data.AddRange(GetDataSCLByArea("MN", year, dataSCL, dataSCL_BS, dataSCL_TH, month));
                    data.AddRange(GetDataSCLByArea("CQ", year, dataSCL, dataSCL_BS, dataSCL_TH, month));
                    data.AddRange(GetDataSCLByArea("VT", year, dataSCL, dataSCL_BS, dataSCL_TH, month));
                }
                else
                {
                    data.AddRange(GetDataSCLByArea(area, year, dataSCL, dataSCL_BS, dataSCL_TH, month));
                }
                var SumCol1 = data.Where(x => string.IsNullOrEmpty(x.Stt)).Sum(x => x.Col1);
                var SumCol4 = data.Where(x => string.IsNullOrEmpty(x.Stt)).Sum(x => x.Col4);

                data.Insert(0, new ReportModel
                {
                    IsBold = true,
                    Name = "TỔNG KINH PHÍ SỬA CHỮA",
                    Col1 = data.Where(x => string.IsNullOrEmpty(x.Stt)).Sum(x => x.Col1),
                    Col2 = data.Where(x => string.IsNullOrEmpty(x.Stt)).Sum(x => x.Col2),
                    Col3 = data.Where(x => string.IsNullOrEmpty(x.Stt)).Sum(x => x.Col3),
                    Col4 = data.Where(x => string.IsNullOrEmpty(x.Stt)).Sum(x => x.Col4),
                    Col5 = SumCol1 == 0 || SumCol4 == 0 ? 0 : (SumCol4 / SumCol1) * 100
                });
                var parent = data.Where(x => x.IsBold == true && x.Parent != null).Select(x => new { x.Code, x.Name }).Distinct().ToList();
                foreach (var item in parent)
                {
                    data.Insert(1, new ReportModel
                    {
                        Name = item.Name,
                        Col1 = data.Where(y => y.Code == item.Code).Sum(y => y.Col1),
                        Col2 = data.Where(y => y.Code == item.Code).Sum(y => y.Col2),
                        Col3 = data.Where(y => y.Code == item.Code).Sum(y => y.Col3),
                        Col4 = data.Where(y => y.Code == item.Code).Sum(y => y.Col4),
                        Col5 = data.Where(y => y.Code == item.Code).Sum(y => y.Col1) == 0 ? 0 : (data.Where(y => y.Code == item.Code).Sum(y => y.Col4) / data.Where(y => y.Code == item.Code).Sum(y => y.Col1)) * 100
                    });
                }


                return data;

            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                return new List<ReportModel>();
            }
        }

        public List<ReportModel> GetDataSCLByArea(string area, int year,
            List<T_BP_SUA_CHUA_LON_DATA> data,
            List<T_BP_SUA_CHUA_LON_DATA> dataBS,
            List<T_BP_SUA_CHUA_LON_DATA> dataTH,
            int month)
        {
            try
            {
                var dataR = new List<ReportModel>();
                if (area == "CQ")
                {
                    data = data.Where(x => x.ORG_CODE.Contains("100001")).ToList();
                    dataBS = dataBS.Where(x => x.ORG_CODE.Contains("100001")).ToList();
                    dataTH = dataTH.Where(x => x.ORG_CODE.Contains("100001")).ToList();
                }
                if (area == "MB")
                {
                    data = data.Where(x => x.ORG_CODE.Contains("100002")).ToList();
                    dataBS = dataBS.Where(x => x.ORG_CODE.Contains("100002")).ToList();
                    dataTH = dataTH.Where(x => x.ORG_CODE.Contains("100002")).ToList();
                }
                if (area == "MT")
                {
                    data = data.Where(x => x.ORG_CODE.Contains("100003")).ToList();
                    dataBS = dataBS.Where(x => x.ORG_CODE.Contains("100003")).ToList();
                    dataTH = dataTH.Where(x => x.ORG_CODE.Contains("100003")).ToList();
                }
                if (area == "MN")
                {
                    data = data.Where(x => x.ORG_CODE.Contains("100004")).ToList();
                    dataBS = dataBS.Where(x => x.ORG_CODE.Contains("100004")).ToList();
                    dataTH = dataTH.Where(x => x.ORG_CODE.Contains("100004")).ToList();
                }
                if (area == "VT")
                {
                    data = data.Where(x => x.ORG_CODE.Contains("100005")).ToList();
                    dataBS = dataBS.Where(x => x.ORG_CODE.Contains("100005")).ToList();
                    dataTH = dataTH.Where(x => x.ORG_CODE.Contains("100005")).ToList();
                }
                var e = data.Union(dataBS).Union(dataTH);
                var elementCodes = e.Select(x => x.KHOAN_MUC_SUA_CHUA_CODE).Distinct().ToList();
                var elementChild = UnitOfWork.Repository<KhoanMucSuaChuaRepo>().Queryable().Where(x => x.TIME_YEAR == year && elementCodes.Contains(x.CODE)).ToList();
                var testdata = UnitOfWork.Repository<KhoanMucSuaChuaRepo>().Queryable().Where(x => x.TIME_YEAR == year && elementCodes.Contains(x.CODE)).Select(x => x.PARENT_CODE).Distinct();
                foreach (var i in elementChild.Select(x => x.PARENT_CODE).Distinct().ToList())
                {
                    var p = UnitOfWork.Repository<KhoanMucSuaChuaRepo>().Queryable().FirstOrDefault(x => x.TIME_YEAR == year && x.CODE == i);
                    if (p != null) elementChild.Add(p);
                }
                elementChild = elementChild.DistinctBy(x => x.CODE).ToList();

                var orderChild = 0;
                var orderParent = 0;
                var cn = new ReportModel
                {
                    IsBold = true,
                    Name = area == "CQ" ? " CƠ QUAN CÔNG TY" : area == "MB" ? "CHI NHÁNH MIỀN BẮC" : area == "MT" ? "CHI NHÁNH MIỀN TRUNG" : area == "MN" ? "CHI NHÁNH MIỀN NAM" : "CHI NHÁNH VẬN TẢI",
                    Col1 = data.Sum(x => x.VALUE) ?? 0,
                };
                switch (month)
                {
                    case 1:
                        cn.Col2 = dataBS.Sum(x => Convert.ToDecimal(x.MONTH1 == null ? 0 : x.MONTH1));
                        cn.Col4 = dataTH.Sum(x => Convert.ToDecimal(x.MONTH1 == null ? 0 : x.MONTH1));
                        break;
                    case 2:
                        cn.Col2 = dataBS.Sum(x => Convert.ToDecimal(x.MONTH2 == null ? 0 : x.MONTH2));
                        cn.Col4 = dataTH.Sum(x => Convert.ToDecimal(x.MONTH2 == null ? 0 : x.MONTH2));
                        break;
                    case 3:
                        cn.Col2 = dataBS.Sum(x => Convert.ToDecimal(x.MONTH3 == null ? 0 : x.MONTH3));
                        cn.Col4 = dataTH.Sum(x => Convert.ToDecimal(x.MONTH3 == null ? 0 : x.MONTH3));
                        break;
                    case 4:
                        cn.Col2 = dataBS.Sum(x => Convert.ToDecimal(x.MONTH4 == null ? 0 : x.MONTH4));
                        cn.Col4 = dataTH.Sum(x => Convert.ToDecimal(x.MONTH4 == null ? 0 : x.MONTH4));
                        break;
                    case 5:
                        cn.Col2 = dataBS.Sum(x => Convert.ToDecimal(x.MONTH5 == null ? 0 : x.MONTH5));
                        cn.Col4 = dataTH.Sum(x => Convert.ToDecimal(x.MONTH5 == null ? 0 : x.MONTH5));
                        break;
                    case 6:
                        cn.Col2 = dataBS.Sum(x => Convert.ToDecimal(x.MONTH6 == null ? 0 : x.MONTH6));
                        cn.Col4 = dataTH.Sum(x => Convert.ToDecimal(x.MONTH6 == null ? 0 : x.MONTH6));
                        break;
                    case 7:
                        cn.Col2 = dataBS.Sum(x => Convert.ToDecimal(x.MONTH7 == null ? 0 : x.MONTH7));
                        cn.Col4 = dataTH.Sum(x => Convert.ToDecimal(x.MONTH7 == null ? 0 : x.MONTH7));
                        break;
                    case 8:
                        cn.Col2 = dataBS.Sum(x => Convert.ToDecimal(x.MONTH8 == null ? 0 : x.MONTH8));
                        cn.Col4 = dataTH.Sum(x => Convert.ToDecimal(x.MONTH8 == null ? 0 : x.MONTH8));
                        break;
                    case 9:
                        cn.Col2 = dataBS.Sum(x => Convert.ToDecimal(x.MONTH9 == null ? 0 : x.MONTH9));
                        cn.Col4 = dataTH.Sum(x => Convert.ToDecimal(x.MONTH9 == null ? 0 : x.MONTH9));
                        break;
                    case 10:
                        cn.Col2 = dataBS.Sum(x => Convert.ToDecimal(x.MONTH10 == null ? 0 : x.MONTH10));
                        cn.Col4 = dataTH.Sum(x => Convert.ToDecimal(x.MONTH10 == null ? 0 : x.MONTH10));
                        break;
                    case 11:
                        cn.Col2 = dataBS.Sum(x => Convert.ToDecimal(x.MONTH11 == null ? 0 : x.MONTH11));
                        cn.Col4 = dataTH.Sum(x => Convert.ToDecimal(x.MONTH11 == null ? 0 : x.MONTH11));
                        break;
                    case 12:
                        cn.Col2 = dataBS.Sum(x => Convert.ToDecimal(x.MONTH12 == null ? 0 : x.MONTH12));
                        cn.Col4 = dataTH.Sum(x => Convert.ToDecimal(x.MONTH12 == null ? 0 : x.MONTH12));
                        break;
                }
                cn.Col3 = cn.Col1 + cn.Col2;
                cn.Col5 = cn.Col1 != 0 && cn.Col4 != 0 ? cn.Col4 / cn.Col1 * 100 : 0;
                dataR.Add(cn);
                foreach (var i in elementChild.OrderBy(x => x.C_ORDER))
                {
                    if (i.IS_GROUP)
                    {
                        orderChild = 0;
                        orderParent += 1;
                    }
                    var d = new ReportModel
                    {
                        Stt = i.IS_GROUP ? ConvertNumberOrder(orderParent) : orderChild.ToString(),
                        Code = i.CODE,
                        Parent = i.PARENT_CODE,
                        IsBold = i.IS_GROUP ? true : false,
                        Name = i.NAME,
                        Col1 = data.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE == i.CODE).Sum(x => x.VALUE) ?? 0,
                        Des = data.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE == i.CODE && !string.IsNullOrEmpty(x.DESCRIPTION)).FirstOrDefault()?.DESCRIPTION,
                    };
                    switch (month)
                    {
                        case 1:
                            d.Col2 = dataBS.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE == i.CODE).Sum(x => Convert.ToDecimal(x.MONTH1 == null ? 0 : x.MONTH1));
                            d.Col4 = dataTH.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE == i.CODE).Sum(x => Convert.ToDecimal(x.MONTH1 == null ? 0 : x.MONTH1));
                            break;
                        case 2:
                            d.Col2 = dataBS.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE == i.CODE).Sum(x => Convert.ToDecimal(x.MONTH2 == null ? 0 : x.MONTH2));
                            d.Col4 = dataTH.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE == i.CODE).Sum(x => Convert.ToDecimal(x.MONTH2 == null ? 0 : x.MONTH2));
                            break;
                        case 3:
                            d.Col2 = dataBS.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE == i.CODE).Sum(x => Convert.ToDecimal(x.MONTH3 == null ? 0 : x.MONTH3));
                            d.Col4 = dataTH.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE == i.CODE).Sum(x => Convert.ToDecimal(x.MONTH3 == null ? 0 : x.MONTH3));
                            break;
                        case 4:
                            d.Col2 = dataBS.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE == i.CODE).Sum(x => Convert.ToDecimal(x.MONTH4 == null ? 0 : x.MONTH4));
                            d.Col4 = dataTH.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE == i.CODE).Sum(x => Convert.ToDecimal(x.MONTH4 == null ? 0 : x.MONTH4));
                            break;
                        case 5:
                            d.Col2 = dataBS.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE == i.CODE).Sum(x => Convert.ToDecimal(x.MONTH5 == null ? 0 : x.MONTH5));
                            d.Col4 = dataTH.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE == i.CODE).Sum(x => Convert.ToDecimal(x.MONTH5 == null ? 0 : x.MONTH5));
                            break;
                        case 6:
                            d.Col2 = dataBS.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE == i.CODE).Sum(x => Convert.ToDecimal(x.MONTH6 == null ? 0 : x.MONTH6));
                            d.Col4 = dataTH.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE == i.CODE).Sum(x => Convert.ToDecimal(x.MONTH6 == null ? 0 : x.MONTH6));
                            break;
                        case 7:
                            d.Col2 = dataBS.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE == i.CODE).Sum(x => Convert.ToDecimal(x.MONTH7 == null ? 0 : x.MONTH7));
                            d.Col4 = dataTH.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE == i.CODE).Sum(x => Convert.ToDecimal(x.MONTH7 == null ? 0 : x.MONTH7));
                            break;
                        case 8:
                            d.Col2 = dataBS.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE == i.CODE).Sum(x => Convert.ToDecimal(x.MONTH8 == null ? 0 : x.MONTH8));
                            d.Col4 = dataTH.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE == i.CODE).Sum(x => Convert.ToDecimal(x.MONTH8 == null ? 0 : x.MONTH8));
                            break;
                        case 9:
                            d.Col2 = dataBS.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE == i.CODE).Sum(x => Convert.ToDecimal(x.MONTH9 == null ? 0 : x.MONTH9));
                            d.Col4 = dataTH.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE == i.CODE).Sum(x => Convert.ToDecimal(x.MONTH9 == null ? 0 : x.MONTH9));
                            break;
                        case 10:
                            d.Col2 = dataBS.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE == i.CODE).Sum(x => Convert.ToDecimal(x.MONTH10 == null ? 0 : x.MONTH10));
                            d.Col4 = dataTH.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE == i.CODE).Sum(x => Convert.ToDecimal(x.MONTH10 == null ? 0 : x.MONTH10));
                            break;
                        case 11:
                            d.Col2 = dataBS.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE == i.CODE).Sum(x => Convert.ToDecimal(x.MONTH11 == null ? 0 : x.MONTH11));
                            d.Col4 = dataTH.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE == i.CODE).Sum(x => Convert.ToDecimal(x.MONTH11 == null ? 0 : x.MONTH11));
                            break;
                        case 12:
                            d.Col2 = dataBS.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE == i.CODE).Sum(x => Convert.ToDecimal(x.MONTH12 == null ? 0 : x.MONTH12));
                            d.Col4 = dataTH.Where(x => x.KHOAN_MUC_SUA_CHUA_CODE == i.CODE).Sum(x => Convert.ToDecimal(x.MONTH12 == null ? 0 : x.MONTH12));
                            break;
                    }
                    d.Col3 = d.Col1 + d.Col2;
                    d.Col5 = d.Col1 != 0 && d.Col4 != 0 ? d.Col4 / d.Col1 * 100 : 0;
                    orderChild += 1;
                    dataR.Add(d);
                }
                foreach (var i in dataR)
                {
                    var childs = dataR.Where(x => x.Parent == i.Code);
                    if (childs.Count() != 0 || i.Col1 == 0)
                    {
                        i.Col1 = childs.Sum(x => x.Col1);
                    }
                    if (childs.Count() != 0 || i.Col2 == 0)
                    {
                        i.Col2 = childs.Sum(x => x.Col2);
                    }
                    if (childs.Count() != 0 || i.Col3 == 0)
                    {
                        i.Col3 = childs.Sum(x => x.Col3);
                    }
                    if (childs.Count() != 0 || i.Col4 == 0)
                    {
                        i.Col4 = childs.Sum(x => x.Col4);
                    }
                }
                return dataR;
            }
            catch (Exception ex)
            {
                return new List<ReportModel>();
            }
        }

        public string ConvertNumberOrder(decimal number)
        {
            try
            {
                string strRet = string.Empty;
                decimal _Number = number;
                Boolean _Flag = true;
                string[] ArrLama = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
                int[] ArrNumber = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
                int i = 0;
                while (_Flag)
                {
                    while (_Number >= ArrNumber[i])
                    {
                        _Number -= ArrNumber[i];
                        strRet += ArrLama[i];
                        if (_Number < 1)
                            _Flag = false;
                    }
                    i++;
                }
                return strRet;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<ReportModel> GenDataBM2110(int year, int month, string phienBan, string kichBan, string area)
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

                if (!string.IsNullOrEmpty(area))
                {
                    if (area == "MB")
                    {
                        details1_1 = details1_1.Where(x => x.ORG_CODE.Contains("100002")).ToList();
                        details3 = details3.Where(x => x.ORG_CODE.Contains("100002")).ToList();
                        details5 = details5.Where(x => x.ORG_CODE.Contains("100002")).ToList();
                        dataTH = dataTH.Where(x => x.CHI_NHANH == "CNMB").ToList();
                    }
                    if (area == "MT")
                    {
                        details1_1 = details1_1.Where(x => x.ORG_CODE.Contains("100003")).ToList();
                        details3 = details3.Where(x => x.ORG_CODE.Contains("100003")).ToList();
                        details5 = details5.Where(x => x.ORG_CODE.Contains("100003")).ToList();
                        dataTH = dataTH.Where(x => x.CHI_NHANH == "CNMT").ToList();
                    }
                    if (area == "MN")
                    {
                        details1_1 = details1_1.Where(x => x.ORG_CODE.Contains("100004")).ToList();
                        details3 = details3.Where(x => x.ORG_CODE.Contains("100004")).ToList();
                        details5 = details5.Where(x => x.ORG_CODE.Contains("100004")).ToList();
                        dataTH = dataTH.Where(x => x.CHI_NHANH == "CNMN").ToList();
                    }
                    if (area == "CQ")
                    {
                        details1_1 = details1_1.Where(x => x.ORG_CODE.Contains("100001")).ToList();
                        details3 = details3.Where(x => x.ORG_CODE.Contains("100001")).ToList();
                        details5 = details5.Where(x => x.ORG_CODE.Contains("100001")).ToList();
                    }
                    if (area == "VT")
                    {
                        details1_1 = details1_1.Where(x => x.ORG_CODE.Contains("100005")).ToList();
                        details3 = details3.Where(x => x.ORG_CODE.Contains("100005")).ToList();
                        details5 = details5.Where(x => x.ORG_CODE.Contains("100005")).ToList();
                        dataTH = dataTH.Where(x => x.CHI_NHANH == "CNVT").ToList();
                    }
                }


                foreach (var e in elements)
                {
                    string checkID = null;
                    if (area == "MT")
                    {
                        checkID = e.IDMT;
                    }
                    else if (area == "MN")
                    {
                        checkID = e.IDMN;
                    }
                    else if (area == "MB")
                    {
                        checkID = e.IDMB;
                    }
                    else if (area == "CQ")
                    {
                        checkID = e.IDCQ;
                    }
                    else if (area == "VT")
                    {
                        checkID = e.IDVT;
                    }
                    else
                    {
                        checkID = null;
                    };
                    if (string.IsNullOrEmpty(area))
                    {
                        var SumMB = details1_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(string.IsNullOrEmpty(e.IDMB) ? e.GROUP_1_ID + e.GROUP_2_ID : e.IDMB) && x.ORG_CODE.Contains("100002"))?.Sum(x => x.QUANTITY * x.PRICE) ??0;
                        var SumMT = details1_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(string.IsNullOrEmpty(e.IDMT) ? e.GROUP_1_ID + e.GROUP_2_ID : e.IDMT) && x.ORG_CODE.Contains("100003"))?.Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                        var SumMN = details1_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(string.IsNullOrEmpty(e.IDMN) ? e.GROUP_1_ID + e.GROUP_2_ID : e.IDMN) && x.ORG_CODE.Contains("100004"))?.Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                        var SumVT = details1_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(string.IsNullOrEmpty(e.IDVT) ? e.GROUP_1_ID + e.GROUP_2_ID : e.IDVT) && x.ORG_CODE.Contains("100005"))?.Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                        var SumCQ = details1_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(string.IsNullOrEmpty(e.IDCQ) ? e.GROUP_1_ID + e.GROUP_2_ID : e.IDCQ) && x.ORG_CODE.Contains("100001"))?.Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                        var SumMBcol3 = details3.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(string.IsNullOrEmpty(e.IDMB) ? e.GROUP_1_ID + e.GROUP_2_ID : e.IDMB) && x.ORG_CODE.Contains("100002"))?.Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                        var SumMTcol3 = details3.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(string.IsNullOrEmpty(e.IDMT) ? e.GROUP_1_ID + e.GROUP_2_ID : e.IDMT) && x.ORG_CODE.Contains("100003"))?.Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                        var SumMNcol3 = details3.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(string.IsNullOrEmpty(e.IDMN) ? e.GROUP_1_ID + e.GROUP_2_ID : e.IDMN) && x.ORG_CODE.Contains("100004"))?.Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                        var SumVTcol3 = details3.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(string.IsNullOrEmpty(e.IDVT) ? e.GROUP_1_ID + e.GROUP_2_ID : e.IDVT) && x.ORG_CODE.Contains("100005"))?.Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                        var SumCQcol3 = details3.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(string.IsNullOrEmpty(e.IDCQ) ? e.GROUP_1_ID + e.GROUP_2_ID : e.IDCQ) && x.ORG_CODE.Contains("100001"))?.Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                        if (e.GROUP_1_ID.Contains("6273") && e.GROUP_2_ID.EndsWith("B"))
                        {
                             SumMB = details1_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(string.IsNullOrEmpty(e.IDMB) ? e.GROUP_1_ID + e.GROUP_2_ID : e.IDMB) && x.ORG_CODE.Contains("100002"))?.Sum(x => x.PRICE>10000000 ? x.QUANTITY * x.PRICE /2:x.QUANTITY*x.PRICE) ?? 0;
                             SumMT = details1_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(string.IsNullOrEmpty(e.IDMT) ? e.GROUP_1_ID + e.GROUP_2_ID : e.IDMT) && x.ORG_CODE.Contains("100003"))?.Sum(x => x.PRICE > 10000000 ? x.QUANTITY * x.PRICE / 2 : x.QUANTITY * x.PRICE) ?? 0;
                             SumMN = details1_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(string.IsNullOrEmpty(e.IDMN) ? e.GROUP_1_ID + e.GROUP_2_ID : e.IDMN) && x.ORG_CODE.Contains("100004"))?.Sum(x => x.PRICE > 10000000 ? x.QUANTITY * x.PRICE / 2 : x.QUANTITY * x.PRICE) ?? 0;
                             SumVT = details1_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(string.IsNullOrEmpty(e.IDVT) ? e.GROUP_1_ID + e.GROUP_2_ID : e.IDVT) && x.ORG_CODE.Contains("100005"))?.Sum(x => x.PRICE > 10000000 ? x.QUANTITY * x.PRICE / 2 : x.QUANTITY * x.PRICE) ?? 0;
                             SumCQ = details1_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(string.IsNullOrEmpty(e.IDCQ) ? e.GROUP_1_ID + e.GROUP_2_ID : e.IDCQ) && x.ORG_CODE.Contains("100001"))?.Sum(x => x.PRICE > 10000000 ? x.QUANTITY * x.PRICE / 2 : x.QUANTITY * x.PRICE) ?? 0;
                             SumMBcol3 = details3.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(string.IsNullOrEmpty(e.IDMB) ? e.GROUP_1_ID + e.GROUP_2_ID : e.IDMB) && x.ORG_CODE.Contains("100002"))?.Sum(x => x.PRICE > 10000000 ? x.QUANTITY * x.PRICE / 2 : x.QUANTITY * x.PRICE) ?? 0;
                             SumMTcol3 = details3.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(string.IsNullOrEmpty(e.IDMT) ? e.GROUP_1_ID + e.GROUP_2_ID : e.IDMT) && x.ORG_CODE.Contains("100003"))?.Sum(x => x.PRICE > 10000000 ? x.QUANTITY * x.PRICE / 2 : x.QUANTITY * x.PRICE) ?? 0;
                             SumMNcol3 = details3.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(string.IsNullOrEmpty(e.IDMN) ? e.GROUP_1_ID + e.GROUP_2_ID : e.IDMN) && x.ORG_CODE.Contains("100004"))?.Sum(x => x.PRICE > 10000000 ? x.QUANTITY * x.PRICE / 2 : x.QUANTITY * x.PRICE) ?? 0;
                             SumVTcol3 = details3.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(string.IsNullOrEmpty(e.IDVT) ? e.GROUP_1_ID + e.GROUP_2_ID : e.IDVT) && x.ORG_CODE.Contains("100005"))?.Sum(x => x.PRICE > 10000000 ? x.QUANTITY * x.PRICE / 2 : x.QUANTITY * x.PRICE) ?? 0;
                             SumCQcol3 = details3.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(string.IsNullOrEmpty(e.IDCQ) ? e.GROUP_1_ID + e.GROUP_2_ID : e.IDCQ) && x.ORG_CODE.Contains("100001"))?.Sum(x => x.PRICE > 10000000 ? x.QUANTITY * x.PRICE / 2 : x.QUANTITY * x.PRICE) ?? 0;

                        }
                        if (e.GROUP_1_ID.Contains("6273") && e.GROUP_2_ID.EndsWith("C"))
                        {
                            SumMB = details1_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(string.IsNullOrEmpty(e.IDMB) ? e.GROUP_1_ID + e.GROUP_2_ID.Substring(0,4)+"B" : e.IDMB) && x.ORG_CODE.Contains("100002"))?.Sum(x =>  x.QUANTITY * x.PRICE) ?? 0;
                            SumMT = details1_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(string.IsNullOrEmpty(e.IDMT) ? e.GROUP_1_ID + e.GROUP_2_ID.Substring(0, 4) + "B" : e.IDMT) && x.ORG_CODE.Contains("100003"))?.Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                            SumMN = details1_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(string.IsNullOrEmpty(e.IDMN) ? e.GROUP_1_ID + e.GROUP_2_ID.Substring(0, 4) + "B" : e.IDMN) && x.ORG_CODE.Contains("100004"))?.Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                            SumVT = details1_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(string.IsNullOrEmpty(e.IDVT) ? e.GROUP_1_ID + e.GROUP_2_ID.Substring(0, 4) + "B" : e.IDVT) && x.ORG_CODE.Contains("100005"))?.Sum(x =>  x.QUANTITY * x.PRICE) ?? 0;
                            SumCQ = details1_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(string.IsNullOrEmpty(e.IDCQ) ? e.GROUP_1_ID + e.GROUP_2_ID.Substring(0, 4) + "B" : e.IDCQ) && x.ORG_CODE.Contains("100001"))?.Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                            SumMBcol3 = details3.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(string.IsNullOrEmpty(e.IDMB) ? e.GROUP_1_ID + e.GROUP_2_ID.Substring(0, 4) + "B" : e.IDMB) && x.ORG_CODE.Contains("100002"))?.Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                            SumMTcol3 = details3.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(string.IsNullOrEmpty(e.IDMT) ? e.GROUP_1_ID + e.GROUP_2_ID.Substring(0, 4) + "B" : e.IDMT) && x.ORG_CODE.Contains("100003"))?.Sum(x =>  x.QUANTITY * x.PRICE) ?? 0;
                            SumMNcol3 = details3.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(string.IsNullOrEmpty(e.IDMN) ? e.GROUP_1_ID + e.GROUP_2_ID.Substring(0, 4) + "B" : e.IDMN) && x.ORG_CODE.Contains("100004"))?.Sum(x =>  x.QUANTITY * x.PRICE) ?? 0;
                            SumVTcol3 = details3.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(string.IsNullOrEmpty(e.IDVT) ? e.GROUP_1_ID + e.GROUP_2_ID.Substring(0, 4) + "B" : e.IDVT) && x.ORG_CODE.Contains("100005"))?.Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                            SumCQcol3 = details3.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(string.IsNullOrEmpty(e.IDCQ) ? e.GROUP_1_ID + e.GROUP_2_ID.Substring(0, 4) + "B" : e.IDCQ) && x.ORG_CODE.Contains("100001"))?.Sum(x => x.QUANTITY * x.PRICE) ?? 0;
                        }

                            var i = new ReportModel
                        {
                            Group1 = e.GROUP_1_ID,
                            Group2=e.GROUP_2_ID,
                            Stt = e.STT,
                            Name = e.GROUP_NAME,
                            IsBold = e.IS_BOLD,
                            Col1 = SumMB+SumMN+SumMT+SumVT+SumCQ,
                            Col2 = SumMBcol3 + SumMNcol3 + SumMTcol3 + SumVTcol3 + SumCQcol3,
                            Col4 = string.IsNullOrEmpty(e.GROUP_2_ID) ? dataTH.Where(x => x.GROUP_1_ID == e.GROUP_1_ID).Sum(x => x.VALUE) :dataTH.Where(x => x.GROUP_1_ID == e.GROUP_1_ID && x.GROUP_2_ID.Contains(e.GROUP_2_ID)).Sum(x => x.VALUE),
                        };
                        i.Col3 = i.Col1 + i.Col2;
                        i.Col5 = i.Col4 == 0 || i.Col1 == 0 ? 0 : (i.Col4 / i.Col1) * 100;
                        data.Add(i);
                    }
                    else
                    {
                        var col1 = details1_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(string.IsNullOrEmpty(checkID) ? e.GROUP_1_ID + e.GROUP_2_ID : checkID)).Sum(x => x.QUANTITY * x.PRICE);
                        var col2 = details3.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(string.IsNullOrEmpty(checkID) ? e.GROUP_1_ID + e.GROUP_2_ID : checkID)).Sum(x => x.QUANTITY * x.PRICE);
                        if (e.GROUP_1_ID.Contains("6273") && e.GROUP_2_ID.EndsWith("B"))
                        {
                            col1 = details1_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(string.IsNullOrEmpty(checkID) ? e.GROUP_1_ID + e.GROUP_2_ID : checkID)).Sum(x => x.QUANTITY * x.PRICE > 10000000?x.PRICE/2:x.PRICE) ;
                            col2 = details3.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(string.IsNullOrEmpty(checkID) ? e.GROUP_1_ID + e.GROUP_2_ID : checkID)).Sum(x => x.QUANTITY * x.PRICE > 10000000 ? x.PRICE / 2 : x.PRICE);
                        }
                        if (e.GROUP_1_ID.Contains("6273") && e.GROUP_2_ID.EndsWith("C"))
                        {
                            col1 = details1_1.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(string.IsNullOrEmpty(checkID) ? e.GROUP_1_ID + e.GROUP_2_ID.Substring(0,4)+"B": checkID)).Sum(x => x.QUANTITY *  x.PRICE);
                            col2 = details3.Where(x => x.KHOAN_MUC_HANG_HOA_CODE.Contains(string.IsNullOrEmpty(checkID) ? e.GROUP_1_ID + e.GROUP_2_ID.Substring(0,4)+"B" : checkID)).Sum(x => x.QUANTITY * x.PRICE);
                        }
                        var i = new ReportModel
                        {
                            Group1 = e.GROUP_1_ID,
                            Group2 = e.GROUP_2_ID,
                            Stt = e.STT,
                            Name = e.GROUP_NAME,
                            IsBold = e.IS_BOLD,
                            Col1 = col1,
                            Col2 = col2,
                            Col4 = string.IsNullOrEmpty(e.GROUP_2_ID) ? dataTH.Where(x => x.GROUP_1_ID == e.GROUP_1_ID).Sum(x => x.VALUE) : dataTH.Where(x => x.GROUP_1_ID == e.GROUP_1_ID && x.GROUP_2_ID.Contains(e.GROUP_2_ID)).Sum(x => x.VALUE),
                        };
                        i.Col3 = i.Col1 + i.Col2;
                        i.Col5 = i.Col4 == 0 || i.Col1 == 0 ? 0 : (i.Col4 / i.Col1) * 100;
                        data.Add(i);
                    };
                   
                }
                List<string> CodeB2 = new List<string> { "G002B2", "G003AB2", "G004AB2", "G005AB2", "G006AB2", "G007AB2" };
                List<string> CodePB = new List<string> { "G002B", "G003AB", "G004AB", "G005AB", "G006AB", "G007AB" };
                List<string> ParrentPB = new List<string> { "G003", "G004", "G005", "G006", "G007" };

                data.ForEach(x =>
                {
                    if(x.Group1=="6277"&& CodeB2.Contains(x.Group2))
                    {
                        var lencode = x.Group2.Length - 1;
                        x.Col1 = data.Where(y => y.Group1 == "6277" && y.Group2 == x.Group2.Substring(0, lencode) + "3").Sum(y => y.Col1 / 2);
                        x.Col2 = data.Where(y => y.Group1 == "6277" && y.Group2 == x.Group2.Substring(0, lencode) + "3").Sum(y => y.Col2 / 2);
                        x.Col3 = x.Col1 + x.Col2;

                    }
                });
                data.ForEach(x =>
                {
                    if (x.Group1 == "6277" && CodePB.Contains(x.Group2))
                    {
                        var lencode = x.Group2.Length - 1;
                      
                        var a = data.Where(y => y.Group1 == "6277" && (y.Group2 == x.Group2 + "1" || y.Group2 == x.Group2 + "2"));
                        x.Col1 = data.Where(y => y.Group1 == "6277" && (y.Group2 == x.Group2 + "1"|| y.Group2 == x.Group2+ "2")).Sum(y => y.Col1 );
                        x.Col2 = data.Where(y => y.Group1 == "6277" && (y.Group2 == x.Group2+ "2"|| y.Group2 == x.Group2 + "1")).Sum(y => y.Col2 );
                        x.Col3 = x.Col1 + x.Col2;

                    }
                });
                data.ForEach(x =>
                {
                    if (x.Group1 == "6277" && x.Group2== "G002")
                    {
                        var lencode = x.Group2.Length - 1;
                        x.Col1 = data.Where(y => y.Group1 == "6277" && (y.Group2 == x.Group2+ "A" || y.Group2 == x.Group2 + "B")).Sum(y => y.Col1);
                        x.Col2 = data.Where(y => y.Group1 == "6277" && (y.Group2 == x.Group2+ "A" || y.Group2 == x.Group2 + "B")).Sum(y => y.Col2);
                        x.Col3 = x.Col1 + x.Col2;
                    };
                    if (x.Group1 == "6277" && ParrentPB.Contains(x.Group2))
                    {
                        var lencode = x.Group2.Length - 1;
                        x.Col1 = data.Where(y => y.Group1 == "6277" && (y.Group2 == x.Group2 + "AA" || y.Group2 == x.Group2 + "AB")).Sum(y => y.Col1);
                        x.Col2 = data.Where(y => y.Group1 == "6277" && (y.Group2 == x.Group2+ "AA" || y.Group2 == x.Group2 + "AB")).Sum(y => y.Col2);
                        x.Col3 = x.Col1 + x.Col2;
                    }

                });
                
                List<string> ListchildCode = new List<string> { "G001", "G002", "G003", "G004", "G005", "G006", "G007", "G008", "G009", "G010", "G011", "G012", "G019" };
                data.ForEach(x =>
                {
                    if (x.Group1 == "6277" && string.IsNullOrEmpty(x.Group2))
                    {
    
                        x.Col1 = data.Where(y => y.Group1 == "6277" && ListchildCode.Contains(y.Group2)).Sum(y => y.Col1);
                        x.Col2 = data.Where(y => y.Group1 == "6277" && ListchildCode.Contains(x.Group2)).Sum(y => y.Col2);
                        x.Col3 = x.Col1 + x.Col2;

                    }
                });
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
        public List<ReportModel1D> BM01D { get; set; } = new List<ReportModel1D>();
        public List<ReportModel> BM02A { get; set; } = new List<ReportModel>();
        public List<ReportModel> BM02C { get; set; } = new List<ReportModel>();
        public List<ReportModel2B> BM02B { get; set; } = new List<ReportModel2B>();
        public List<BM02BModel> BM_02B { get; set; } = new List<BM02BModel>();
        public List<ReportModel> BM02D { get; set; } = new List<ReportModel>();
        public List<ReportModel> BM02D1 { get; set; } = new List<ReportModel>();
    }
    public class ReportModel
    {
        public string Group1 { get; set; }
        public string Group2 { get; set; }
        public string Code { get; set; }
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
       
        public string Group_1_ID { get; set; }
        public string Group_2_ID { get; set; }
      
    
        public decimal ValueQt { get; set; }
        public decimal ValuePlanYear { get; set; }
        public decimal ValueAdditionPlan { get; set; }
        public decimal ValueAfterAdditionPlan { get; set; }

        public decimal ValueUocThucHien { get; set; }
        public decimal ValuePercentPlant { get; set; }
        public decimal ValuePlantNextYear { get; set; }
        public decimal ValueTH9T { get; set; }
        public int Level { get; set; }
    }

    public class ReportModel2B
    {
        public string Code { get; set; }
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
        public string Col2 { get; set; }
        public string Col3 { get; set; }
        public decimal? Col4 { get; set; }
        public decimal? Col5 { get; set; }
        public decimal? Col6 { get; set; }
        public decimal? Col7 { get; set; }
        public string Col8 { get; set; }
        public string Col9 { get; set; }
        public string Col10 { get; set; }
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

    public class BM02BModel
    {
        public string Col1 { get; set; }
        public string Col2 { get; set; }
        public string Col9 { get; set; } 
        public decimal Col3 { get; set; }
        public decimal Col4 { get; set; }
        public decimal Col5 { get; set; }
        public decimal Col6 { get; set; }
        public decimal Col7 { get; set; }
        public decimal Col8 { get; set; }

    }
    public class ReportModel1D
    {
        public string Code { get; set; }
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
        public string Col2 { get; set; }
        public string Col3 { get; set; }
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
        public string Col21 { get; set; } 
        public string Tdth { get; set; }
        public string Tdtk { get; set; }
        public string Des { get; set; }
    }
}