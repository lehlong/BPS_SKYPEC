using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SMO.Core.Entities;
using SMO.Models;
using SMO.Repository.Implement.BP.KE_HOACH_DOANH_THU;
using SMO.Repository.Implement.BP.KE_HOACH_SAN_LUONG;
using SMO.Repository.Implement.MD;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SMO.Service.MD
{
    public class PhienBanService : GenericService<T_MD_PHIEN_BAN, PhienBanRepo>
    {
        public PhienBanService() : base()
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
            data.Add(new SynthesisReportModel
            {
                Stt = "I",
                Name = "Nộp ngân sách Nhà nước",
                UnitName = "Tr.đ",
                IsBold = true,
                Order = 1,
            });
            data.Add(new SynthesisReportModel
            {
                Name = "Trong đó: Các loại thuế",
                UnitName = "Tr.đ",
                Order = 2,
            });

           
            data.Add(new SynthesisReportModel
            {
                Stt = "II",
                Name = "Sản lượng",
                Order = 3,
                IsBold = true,
                
            });

           
            data.Add(new SynthesisReportModel
            {
                Stt = "1",
                Name = "Cung ứng cho VNA Group",
                Order = 4,
                
            });

            
            data.Add(new SynthesisReportModel
            {
                Name = "Cung ứng cho VNA",
                Order = 5,
                
            });

            data.Add(new SynthesisReportModel
            {
                Name = "Cung ứng cho các hãng hàng không khác trong VNA Group",
                Order = 6,
                
            });
            
            data.Add(new SynthesisReportModel
            {
                Stt = "2",
                Name = "Cung ứng cho đối tác khác (*)",
                UnitName = "Tr.đ",
                Order = 7,
                
            });
            data.Add(new SynthesisReportModel
            {
                Stt = "III",
                Name = "Tổng doanh thu và thu nhập khác",
                UnitName = "Tr.đ",
                Order = 8,
                IsBold = true,
            });

            data.Add(new SynthesisReportModel
            {
                Stt = "1",
                Name = "Doanh thu từ hoạt động SXKD",
                UnitName = "Tr.đ",
                Order = 9,
                
            });
            data.Add(new SynthesisReportModel
            {
                Name = " - Doanh thu cung ứng cho VNA ",
                UnitName = "Tr.đ",
                Order = 10,
            });

            
            data.Add(new SynthesisReportModel
            {
                Name = "+ Doanh thu VNA",
                UnitName = "Tr.đ",
                Order = 11,
               
            });
            data.Add(new SynthesisReportModel
            {
                Name = "Trong đó: CK/Giảm giá",
                Order = 12,
            });

            data.Add(new SynthesisReportModel
            {
                Name = "+ Doanh thu hãng HK trong VNA group",
                UnitName = "Tr.đ",
                Order = 13,
               
            });
            data.Add(new SynthesisReportModel
            {
                Name = "Trong đó: CK/Giảm giá",
                Order = 14,
            });

            data.Add(new SynthesisReportModel
            {
                Name = " - Doanh thu cung ứng cho đối tác khác (*)",
                UnitName = "Tr.đ",
                Order = 15,
               
            });
            data.Add(new SynthesisReportModel
            {
                Name = "Trong đó: CK/Giảm giá",
                Order = 16,
            });
            data.Add(new SynthesisReportModel
            {
                Stt = "2",
                Name = "Doanh thu từ hoạt động tài chính",
                UnitName = "Tr.đ",
                Order = 17,
            });
            data.Add(new SynthesisReportModel
            {
                Name = " - Chênh lệch tỷ giá",
                UnitName = "Tr.đ",
                Order = 18,
            });
            data.Add(new SynthesisReportModel
            {
                Name = " - Doanh thu HĐ tài chính khác",
                UnitName = "Tr.đ",
                Order = 19,
            });
            data.Add(new SynthesisReportModel
            {
                Stt = "3",
                Name = "Thu nhập khác",
                UnitName = "Tr.đ",
                Order = 20,
            });
            data.Add(new SynthesisReportModel
            {
                Stt = "IV",
                Name = "Tổng chi phí",
                UnitName = "Tr.đ",
                Order = 21,
                IsBold = true,
            });
            data.Add(new SynthesisReportModel
            {
                Stt = "1",
                Name = "Chi phí sản xuất kinh doanh",
                UnitName = "Tr.đ",
                Order = 22,
            });
            data.Add(new SynthesisReportModel
            {
                Name = " - Chi phí nhân công",
                UnitName = "Tr.đ",
                Order = 23,
            });
            data.Add(new SynthesisReportModel
            {
                Name = "Trong đó:",
                Order = 24,
            });
            data.Add(new SynthesisReportModel
            {
                Name = " + Quỹ lương",
                UnitName = "Tr.đ",
                Order = 25,
            });
            data.Add(new SynthesisReportModel
            {
                Name = " + Các khoản đóng góp (BHXH, BHYT, BHTN, KPCĐ)",
                UnitName = "Tr.đ",
                Order = 26,
            });
            data.Add(new SynthesisReportModel
            {
                Name = " - Chi phí nguyên vật liệu, vật tư, vốn hàng",
                UnitName = "Tr.đ",
                Order = 27,
            });
            data.Add(new SynthesisReportModel
            {
                Name = "- Khấu hao tài sản cố định",
                UnitName = "Tr.đ",
                Order = 28,
            });
            data.Add(new SynthesisReportModel
            {
                Name = "- Chi phí bảo dưỡng sửa chữa tài sản",
                UnitName = "Tr.đ",
                Order = 29,
            });
            data.Add(new SynthesisReportModel
            {
                Name = " - Chi phí dụng cụ sản xuất",
                UnitName = "Tr.đ",
                Order = 30,
            });
            data.Add(new SynthesisReportModel
            {
                Name = " - Chi phí dịch vụ mua ngoài (*)",
                UnitName = "Tr.đ",
                Order = 31,
            });
            data.Add(new SynthesisReportModel
            {
                Name = "- Chi phí khác bằng tiền (*)",
                UnitName = "Tr.đ",
                Order = 32,
            });
            data.Add(new SynthesisReportModel
            {
                Name = "- Dự phòng trợ cấp mất việc",
                UnitName = "Tr.đ",
                Order = 33,
            });
            data.Add(new SynthesisReportModel
            {
                Name = "- Thuế, phí lệ phí",
                UnitName = "Tr.đ",
                Order = 34,
            });
            data.Add(new SynthesisReportModel
            {
                Name = " - Chi phí dự phòng",
                UnitName = "Tr.đ",
                Order = 35,
            });
            data.Add(new SynthesisReportModel
            {
                Stt = "2",
                Name = "Chi phí tài chính",
                UnitName = "Tr.đ",
                Order = 36,
            });
            data.Add(new SynthesisReportModel
            {
                Stt = "3",
                Name = "Chi phí khác",
                UnitName = "Tr.đ",
                Order = 37,
            });
            data.Add(new SynthesisReportModel
            {
                Stt = "V",
                Name = "Lợi nhuận",
                Order = 38,
                IsBold = true,
            });
            data.Add(new SynthesisReportModel
            {
                Stt = "1",
                Name = "Tổng LN kế toán trước thuế",
                UnitName = "Tr.đ",
                Order = 39,
            });
            data.Add(new SynthesisReportModel
            {
                Name = "Trong đó: Lợi nhuận từ HĐ SXKD",
                Order = 40,
            });
            data.Add(new SynthesisReportModel
            {
                Stt = "2",
                Name = "Lợi nhuận sau thuế",
                UnitName = "Tr.đ",
                Order = 41,
            });
            data.Add(new SynthesisReportModel
            {
                Stt = "3",
                Name = "Lợi nhuận chia về TCTHK",
                UnitName = "Tr.đ",
                Order = 42,
            });
            data.Add(new SynthesisReportModel
            {
                Stt = "VI",
                Name = "Lao động sử dụng",
                UnitName = "Tr.đ",
                Order = 43,
                IsBold = true,
            });
            data.Add(new SynthesisReportModel
            {
                Name = " - Lao động bình quân",
                UnitName = "Người",
                Order = 44,
            });
            data.Add(new SynthesisReportModel
            {
                Name = "- Thu nhập bình quân",
                UnitName = "Tr.đ/Năm",
                Order = 45,
            });
            data.Add(new SynthesisReportModel
            {
                Stt = "VII",
                Name = "Vốn đầu tư của chủ sở hữu",
                Order = 46,
                IsBold = true,
            });
            data.Add(new SynthesisReportModel
            {
                Name = " - Vốn góp CSH cuối kỳ báo cáo",
                UnitName = "Tr.đ",
                Order = 47,
            });
            data.Add(new SynthesisReportModel
            {
                Name = "- Tăng giảm vốn góp CSH trong kỳ",
                UnitName = "Tr.đ",
                Order = 48,
            });
            data.Add(new SynthesisReportModel
            {
                Name = "- VCSH bình quân trong kỳ",
                UnitName = "Tr.đ",
                Order = 49,
            });
            data.Add(new SynthesisReportModel
            {
                Stt = "VIII",
                Name = "Tỷ suất LN thực hiện/Vốn CSH BQ",
                UnitName = "%",
                Order = 50,
                IsBold = true,
            });
            data.Add(new SynthesisReportModel
            {
                Stt = "IX",
                Name = "Kế hoạch đầu tư",
                Order = 51,
                IsBold = true
            });
            data.Add(new SynthesisReportModel
            {
                Stt = "1",
                Name = "Đầu tư XDCB và TTB",
                Order = 52,
            });
            data.Add(new SynthesisReportModel
            {
                Name = " - Giá trị khối lượng công việc hoàn thành ",
                UnitName = "Tr.đ",
                Order = 53,
            });
            data.Add(new SynthesisReportModel
            {
                Name = "- Giá trị giải ngân ",
                UnitName = "Tr.đ",
                Order = 54,
            });
            data.Add(new SynthesisReportModel
            {
                Stt = "2",
                Name = "Đầu tư vốn vào DN khác",
                UnitName = "Tr.đ",
                Order = 55,
            });
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
