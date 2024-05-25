using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SMO.Core.Entities;
using SMO.Repository.Implement.MD;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SMO.Service.MD
{
    public class UnitPricePlanService : GenericService<T_MD_UNIT_PRICE_PLAN, UnitPricePlanRepo>
    {
        public UnitPricePlanService() : base()
        {

        }
        public void ExportExcel(ref MemoryStream outFileStream, string path, int NUMCELL, int year)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            IWorkbook templateWorkbook;
            templateWorkbook = new XSSFWorkbook(fs);
            templateWorkbook.SetSheetName(0, ModulType.GetTextSheetName(ModulType.KeHoachSanLuong));
            fs.Close();
            ISheet sheet = templateWorkbook.GetSheetAt(0);
            var startRow = 5;
            ExportData(templateWorkbook, sheet, startRow, NUMCELL, year);
                    
            templateWorkbook.Write(outFileStream);
        }
        public void ExportData(IWorkbook templateWorkbook, ISheet sheet, int startRow, int NUM_CELL, int year)
        {
            var data = UnitOfWork.Repository<UnitPricePlanRepo>().Queryable().Where(x => x.YEAR == year).ToList();

            foreach (var item in data)
            {
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, 12);

                rowCur.Cells[0].SetCellValue(item.SAN_BAY_CODE);
                rowCur.Cells[1].SetCellValue(item.VN == null ? "" : item.VN.ToStringVN());
                rowCur.Cells[2].SetCellValue(item.OV == null ? "" : item.OV.ToStringVN());
                rowCur.Cells[3].SetCellValue(item.BL == null ? "" : item.BL.ToStringVN());
                rowCur.Cells[4].SetCellValue(item.VJ == null ? "" : item.VJ.ToStringVN());
                rowCur.Cells[5].SetCellValue(item.QH == null ? "" : item.QH.ToStringVN());
                rowCur.Cells[6].SetCellValue(item.VU == null ? "" : item.VU.ToStringVN());
                rowCur.Cells[7].SetCellValue(item.HKTN_OTHER == null ? "" : item.HKTN_OTHER.ToStringVN());
                rowCur.Cells[8].SetCellValue(item.HKNN_ND == null ? "" : item.HKNN_ND.ToStringVN());
                rowCur.Cells[9].SetCellValue(item.HKNN_QT == null ? "" : item.HKNN_QT.ToStringVN());

            }
        }
    }
}
