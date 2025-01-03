using NPOI.SS.UserModel;
using SMO.Models;
using System.Collections.Generic;
using System.Linq;

namespace SMO
{
    public class ReportUtilities
    {
        /// <summary>
        /// Tạo mới một dòng, và cell của nó
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="numRow"></param>
        public static IRow CreateRow(ref ISheet worksheet, int numRow, int numCell = 500)
        {
            var row = worksheet.GetRow(numRow);
            if (row == null)
            {
                worksheet.CreateRow(numRow);
                row = worksheet.GetRow(numRow);
            }

            for (int i = 0; i < numCell; i++)
            {
                CreateCell(ref row, i);
            }
            return row;
        }

        public static IRow CreateAndShiftRow(ref ISheet worksheet, int numRow, int numCell = 500)
        {
            var row = worksheet.GetRow(numRow);
            if (row == null)
            {
                worksheet.CreateRow(numRow);
                row = worksheet.GetRow(numRow);
            }

            for (int i = 0; i < numCell; i++)
            {
                CreateCell(ref row, i);
            }
            return row;
        }

        /// <summary>
        /// Kiểm tra cell đã tồn tại chưa, nếu chưa thì create cell
        /// </summary>
        /// <param name="row"></param>
        /// <param name="numCell"></param>
        public static void CreateCell(ref IRow row, int numCell)
        {
            ICell cell = row.GetCell(numCell);

            if (cell == null)
            {
                row.CreateCell(numCell);
            }
        }

        public static void CopyRow(ref ISheet worksheet, int sourceRowNum, int destinationRowNum)
        {
            // Get the source / new row
            IRow newRow = worksheet.GetRow(destinationRowNum);
            IRow sourceRow = worksheet.GetRow(sourceRowNum);

            // If the row exist in destination, push down all rows by 1 else create a new row
            if (newRow != null)
            {
                worksheet.ShiftRows(destinationRowNum, worksheet.LastRowNum, 1, true, true);
            }
            else
            {
                newRow = worksheet.CreateRow(destinationRowNum);
            }

            //newRow.Height = sourceRow.Height;

            // Loop through source columns to add to new row
            for (int i = 0; i < sourceRow.LastCellNum; i++)
            {
                // Grab a copy of the old/new cell
                ICell oldCell = sourceRow.GetCell(i);
                ICell newCell = newRow.CreateCell(i);

                // If the old cell is null jump to next cell
                if (oldCell == null)
                {
                    continue;
                }

                // Use old cell style
                newCell.CellStyle = oldCell.CellStyle;


                // Set the cell data value
                switch (oldCell.CellType)
                {
                    case CellType.Blank:
                        break;
                    case CellType.Boolean:
                        newCell.SetCellValue(oldCell.BooleanCellValue);
                        break;
                    case CellType.Error:
                        newCell.SetCellValue(oldCell.ErrorCellValue);
                        break;
                    case CellType.Formula:
                        newCell.SetCellValue(oldCell.CellFormula);
                        break;
                    case CellType.Numeric:
                        newCell.SetCellValue(oldCell.NumericCellValue);
                        break;
                    case CellType.String:
                        newCell.SetCellValue(oldCell.RichStringCellValue);
                        break;
                }
            }
        }

        public static void DeleteRow(ref ISheet sheet, IRow row)
        {
            sheet.RemoveRow(row);   // this only deletes all the cell values

            int rowIndex = row.RowNum;

            int lastRowNum = sheet.LastRowNum;

            if (rowIndex >= 0 && rowIndex < lastRowNum)
            {
                sheet.ShiftRows(rowIndex + 1, lastRowNum, -1);
            }
        }
        public static SynthesizeThePlanReportModel ProcessData(SynthesizeThePlanReportModel Model)
        {
            List<string> ListchildCode = new List<string> { "G001", "G002", "G003", "G004", "G005", "G006", "G007", "G008", "G009", "G010", "G011", "G012", "G019" };
            List<string> CodePB = new List<string> { "6277G002B", "6277G003AB", "6277G004AB", "6277G005AB", "6277G006AB", "6277G007AB" };
            List<string> CodeB2 = new List<string> { "6277G002B2", "6277G003AB2", "6277G004AB2", "6277G005AB2", "6277G006AB2", "6277G007AB2" };
            List<string> ParrentPB = new List<string> { "6277G003", "6277G004", "6277G005", "6277G006", "6277G007" };


            foreach (var c in Model.ChiPhi)
            {
                if (CodeB2.Contains(c.code))
                {
                    var lencode = c.code.Length - 1;
                    c.valueCP = Model.ChiPhi.Where(y => y.code == c.code.Substring(0, lencode) + "3").Sum(z => z.valueCP / 2);
                }
                if (CodePB.Contains(c.code))
                {
                    c.valueCP = Model.ChiPhi.Where(y => y.code == $"{c.code}1" || y.code == $"{c.code}2").Sum(z => z.valueCP);
                }

                if (c.code == "6277G002")
                {
                    c.valueCP = Model.ChiPhi.Where(y => y.code == $"{c.code}A" || y.code == $"{c.code}B").Sum(z => z.valueCP);
                }
                if (ParrentPB.Contains(c.code))
                {
                    c.valueCP = Model.ChiPhi.Where(y => y.code == $"{c.code}AA" || y.code == $"{c.code}AB").Sum(z => z.valueCP);
                }
                var a = Model.ChiPhi.Where(y => ListchildCode.Contains(y.parentCode2));
                if (c.code == "6277")
                {
                    c.valueCP = Model.ChiPhi.Where(y => ListchildCode.Contains(y.parentCode2)).Sum(z => z.valueCP);
                }
                if (c.name == "Thuê s/c thường xuyên nhà cửa vật kiến trúc" && c.Stt == "2.1")
                {
                    c.valueCP = Model.SuaChuaThuongXuyen.Where(x => x.Stt == "I").Sum(x => x.valueGT);
                }
                if (c.name.Contains("Giá trị thực hiện trong năm") && c.Stt == "2.2.2")
                {
                    c.valueCP = Model.SuaChuaLon.Where(x => x.stt == "I").Sum(x => x.valueKP) / 2;
                }
                if (c.name.Contains("Giá trị SCL dự kiến trong năm") && c.Stt == "2.3")
                {
                    c.valueCP = Model.SuaChuaLon.Where(x => x.stt == "I").Sum(x => x.valueKP);
                }
                if (c.name.Contains("Thuê SCTX máy móc, thiết bị") && c.Stt == "3.1")
                {
                    c.valueCP = Model.SuaChuaThuongXuyen.Where(x => x.Name.Contains("SCTX - Máy móc TB")).Sum(x => x.valueGT);
                }
                if (c.name.Contains("Giá trị SCL dự kiến trong năm") && c.Stt == "3.3")
                {
                    c.valueCP = Model.SuaChuaLon.Where(x => x.name.Contains("SCL Máy móc TB")).Sum(x => x.valueKP);
                }
                if (c.name.Contains("Giá trị thực hiện trong năm") && c.Stt == "3.2.2")
                {
                    c.valueCP = Model.SuaChuaLon.Where(x => x.name.Contains("SCL Máy móc TB")).Sum(x => x.valueKP) / 2;
                }
                if (c.name.Contains("Thuê s/c thường xuyên phương tiện vận tải") && c.Stt == "4.1")
                {
                    c.valueCP = Model.SuaChuaThuongXuyen.Where(x => x.Name.Contains("SCTX PTVT")).Sum(x => x.valueGT);
                }
                if (c.name.Contains("Thuê s/c thường xuyên thiết bị quản lý") && c.Stt == "5.1")
                {
                    c.valueCP = Model.SuaChuaThuongXuyen.Where(x => x.Name.Contains("SCTX Thiết bị quản lý")).Sum(x => x.valueGT);
                }
                if (c.name.Contains("Thuê s/c thường xuyên tài sản cố định khác") && c.Stt == "7.1")
                {
                    c.valueCP = Model.SuaChuaThuongXuyen.Where(x => x.Name.Contains("SCTX TSCĐ Khác")).Sum(x => x.valueGT);
                }
                if (c.name.Contains("Giá trị thực hiện trong năm") && c.Stt == "4.2.2")
                {
                    c.valueCP = Model.SuaChuaLon.Where(x => x.name.Contains("SCL PTVT")).Sum(x => x.valueKP) / 2;
                }
                if (c.name.Contains("Giá trị SCL dự kiến trong năm") && c.Stt == "4.3")
                {
                    c.valueCP = Model.SuaChuaLon.Where(x => x.name.Contains("SCL PTVT")).Sum(x => x.valueKP);
                }
                if (c.name.Contains("Giá trị thực hiện trong năm") && c.Stt == "5.2.2")
                {
                    c.valueCP = Model.SuaChuaLon.Where(x => x.name.Contains("Thiết bị quản lý")).Sum(x => x.valueKP) / 2;
                }
                if (c.name.Contains("Giá trị SC dự kiến trong năm") && c.Stt == "5.2.3")
                {
                    c.valueCP = Model.SuaChuaLon.Where(x => x.name.Contains("Thiết bị quản lý")).Sum(x => x.valueKP);
                }
                if (c.name.Contains("Thuê s/c thường xuyên kho bể") && c.Stt == "6.1")
                {
                    c.valueCP = Model.SuaChuaThuongXuyen.Where(x => x.Name.Contains("SCTX Kho Bể")).Sum(x => x.valueGT);
                }
                if (c.name.Contains("Giá trị thực hiện trong năm") && c.Stt == "7.2.2")
                {
                    c.valueCP = Model.SuaChuaLon.Where(x => x.name.Contains("SCL TSCĐ khác")).Sum(x => x.valueKP) / 2;
                }
                if (c.name.Contains("Giá trị SC dự kiến trong năm") && c.Stt == "7.2.3")
                {
                    c.valueCP = Model.SuaChuaLon.Where(x => x.name.Contains("SCL TSCĐ khác")).Sum(x => x.valueKP);
                }
                if (c.name.Contains("Giá trị thực hiện trong năm") && c.Stt == "6.2.2")
                {
                    c.valueCP = Model.SuaChuaLon.Where(x => x.name.Contains("SCL Kho bể")).Sum(x => x.valueKP) / 2;
                }
                if (c.name.Contains("Giá trị SCL dự kiến trong năm") && c.Stt == "6.3")
                {
                    c.valueCP = Model.SuaChuaLon.Where(x => x.name.Contains("SCL Kho bể")).Sum(x => x.valueKP);
                }
            }
            return Model;
        }
    }
}