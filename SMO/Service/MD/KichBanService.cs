﻿using iTextSharp.text;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SMO.Core.Entities;
using SMO.Models;
using SMO.Repository.Implement.BP.DAU_TU_NGOAI_DOANH_NGHIEP;
using SMO.Repository.Implement.BP.DAU_TU_TRANG_THIET_BI;
using SMO.Repository.Implement.BP.DAU_TU_XAY_DUNG;
using SMO.Repository.Implement.BP.KE_HOACH_CHI_PHI;
using SMO.Repository.Implement.BP.KE_HOACH_DOANH_THU;
using SMO.Repository.Implement.BP.KE_HOACH_SAN_LUONG;
using SMO.Repository.Implement.MD;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace SMO.Service.MD
{
    public class KichBanService : GenericService<T_MD_KICH_BAN, KichBanRepo>
    {
        public KichBanService() : base()
        {

        }

        public override void Create()
        {
            try
            {
                if (!CheckExist(x => x.CODE == ObjDetail.CODE))
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

        public IList<SynthesisReportModel> GetData(int year, string kichBan)
        {
            var data = new List<SynthesisReportModel>();
            var elements = UnitOfWork.Repository<ReportSXKDElementRepo>().GetAll().OrderBy(x => x.C_ORDER).ToList();
            foreach(var e in elements)
            {
                var i = new SynthesisReportModel
                {
                    PId = e.ID,
                    Parent = e.PARENT,
                    Stt = e.STT,
                    Name = e.NAME,
                    UnitName = e.DVT,
                    IsBold = e.IS_BOLD,
                    Order = e.C_ORDER,
                    Value2 = string.IsNullOrEmpty(e.KH_1) ? 0 : Convert.ToDecimal(UnitOfWork.GetSession().CreateSQLQuery($"{e.KH_1.Replace("[YEAR]", (year - 1).ToString()).Replace("[KICH_BAN]", kichBan)}").List()[0]),
                    Value4 = string.IsNullOrEmpty(e.UTH_1) ? 0 : Convert.ToDecimal(UnitOfWork.GetSession().CreateSQLQuery($"{e.UTH_1.Replace("[YEAR]", (year - 1).ToString()).Replace("[KICH_BAN]", kichBan)}").List()[0]),

                    Value5 = string.IsNullOrEmpty(e.KH_V1) ? 0 : Convert.ToDecimal(UnitOfWork.GetSession().CreateSQLQuery($"{e.KH_V1.Replace("[YEAR]", year.ToString()).Replace("[KICH_BAN]",kichBan)}").List()[0]),
                    Value6 = string.IsNullOrEmpty(e.KH_V2) ? 0 : Convert.ToDecimal(UnitOfWork.GetSession().CreateSQLQuery($"{e.KH_V2.Replace("[YEAR]", year.ToString()).Replace("[KICH_BAN]", kichBan)}").List()[0]),
                };
                data.Add(i);
            }
            foreach (var d in data.OrderByDescending(x => x.Order))
            {
                var childs = data.Where(x => x.Parent == d.PId).ToList();
                d.Value1 = childs.Sum(x => x.Value1) == 0 || d.Value1 != 0 ? d.Value1 : childs.Sum(x => x.Value1);
                d.Value2 = childs.Sum(x => x.Value2) == 0 || d.Value2 != 0 ? d.Value2 : childs.Sum(x => x.Value2);
                d.Value3 = childs.Sum(x => x.Value3) == 0 || d.Value3 != 0 ? d.Value3 : childs.Sum(x => x.Value3);
                d.Value4 = childs.Sum(x => x.Value4) == 0 || d.Value4 != 0 ? d.Value4 : childs.Sum(x => x.Value4);
                d.Value5 = childs.Sum(x => x.Value5) == 0 || d.Value5 != 0 ? d.Value5 : childs.Sum(x => x.Value5);
                d.Value6 = childs.Sum(x => x.Value6) == 0 || d.Value6 != 0 ? d.Value6 : childs.Sum(x => x.Value6);
            }
            foreach(var d in data)
            {
                d.Value7 = d.Value5 == 0 || d.Value1 == 0 ? 0 : d.Value5 / d.Value1;
                d.Value8 = d.Value5 == 0 || d.Value4 == 0 ? 0 : d.Value5 / d.Value4;
                d.Value9 = d.Value2 == 0 || d.Value4 == 0 ? 0: d.Value4 / d.Value2;
            }
            return data;
        }

        internal void ExportExcel(ref MemoryStream outFileStream,
                                        string path, int year, string kichBan)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                IWorkbook templateWorkbook;
                templateWorkbook = new XSSFWorkbook(fs);
                fs.Close();
                ISheet sheet = templateWorkbook.GetSheetAt(0);
                //var styleCellNumber = GetCellStyleNumber(templateWorkbook);
                //var styleCellNumberDecimal = GetCellStyleNumberDecimal(templateWorkbook);

                var data = GetData(year, kichBan);

                if (data.Count <= 1)
                {
                    this.State = false;
                    this.ErrorMessage = "Không có dữ liệu!";
                    return;
                }

                var startRow = 9;

                for (int i = 0; i < data.Count(); i++)
                {
                    var dataRow = data[i];
                    IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, 11);
                    rowCur.Cells[3].SetCellValue(data[i]?.Value1 == null ? 0 : (double)data[i]?.Value1);
                    rowCur.Cells[4].SetCellValue(data[i]?.Value2 == null ? 0 : (double)data[i]?.Value2);
                    rowCur.Cells[5].SetCellValue(data[i]?.Value3 == null ? 0 : (double)data[i]?.Value3);
                    rowCur.Cells[6].SetCellValue(data[i]?.Value4 == null ? 0 : (double)data[i]?.Value4);
                    rowCur.Cells[7].SetCellValue(data[i]?.Value5 == null ? 0 : (double)data[i]?.Value5);
                    rowCur.Cells[8].SetCellValue(data[i]?.Value6 == null ? 0 : (double)data[i]?.Value6);
                    rowCur.Cells[9].SetCellValue(data[i]?.Value7 == null ? 0 : (double)data[i]?.Value7);
                    rowCur.Cells[10].SetCellValue(data[i]?.Value8 == null ? 0 : (double)data[i]?.Value8);
                }
                templateWorkbook.Write(outFileStream);
            }
            catch (Exception ex)
            {
                this.State = false;
                this.ErrorMessage = "Có lỗi xẩy ra trong quá trình tạo file excel!";
                this.Exception = ex;
            }
        }
    }
}
