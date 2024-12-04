using iTextSharp.text;
using Microsoft.Office.Interop.Excel;
using NHibernate.Criterion;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SMO.Core.Entities;
using SMO.Helper;
using SMO.Models;
using SMO.Repository.Implement.BP.KE_HOACH_DOANH_THU;
using SMO.Repository.Implement.BP.KE_HOACH_SAN_LUONG;
using SMO.Repository.Implement.MD;

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace SMO.Service.MD
{
    public class ElementService : GenericService<T_MD_ELEMENT, ElementRepo>
    {
        public ElementService() : base()
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

        public string AddSpacesToFormula(string formula)
        {
            StringBuilder spacedFormula = new StringBuilder();
            foreach (char character in formula)
            {
                if (IsOperator(character))
                {
                    spacedFormula.Append(' ');
                    spacedFormula.Append(character);
                    spacedFormula.Append(' ');
                }
                else
                {
                    spacedFormula.Append(character);
                }
            }
            return spacedFormula.ToString();
        }

        public bool IsOperator(char character)
        {
            // Define the operators for your formula
            char[] operators = { '+', '-', '*', '/', '=', '<', '>', '!', '&', '|', '(', ')' };
            return Array.Exists(operators, op => op == character);
        }

        public decimal? AnalysisFormula(string formula, KeHoachGiaThanhData data)
        {
            try
            {
                var analysisFormula = "";
                var stringFormula = Regex.Replace(formula, @"\s", "");
                if (stringFormula == "S0003+S0004+S0006/S0005" && data.S0005 == 0)
                {
                    stringFormula = "S0003+S0004";
                }
                
                string spacedFormula = AddSpacesToFormula("x=" + stringFormula);

                string[] stringArray = spacedFormula.Replace("x = ", "").Split(' ');
                foreach (var str in stringArray)
                {
                    switch (str)
                    {
                        case "S0001":
                            analysisFormula += data.S0001.ToString();
                            break;
                        case "S0002":
                            analysisFormula += data.S0002.ToString();
                            break;
                        case "S0003":
                            analysisFormula += data.S0003.ToString();
                            break;
                        case "S0004":
                            analysisFormula += data.S0004.ToString();
                            break;
                        case "S0005":
                            analysisFormula += data.S0005.ToString();
                            break;
                        case "S0006":
                            analysisFormula += data.S0006.ToString();
                            break;
                        case "S0007":
                            analysisFormula += data.S0007.ToString();
                            break;
                        case "U0001":
                            analysisFormula += data.U0001.ToString();
                            break;
                        case "U0002":
                            analysisFormula += data.U0002.ToString();
                            break;
                        case "U0003":
                            analysisFormula += data.U0003.ToString();
                            break;
                        case "U0004":
                            analysisFormula += data.U0004.ToString();
                            break;
                        case "U0005":
                            analysisFormula += data.S0005.ToString();
                            break;
                        case "U0006":
                            analysisFormula += data.U0006.ToString();
                            break;
                        case "U0007":
                            analysisFormula += data.U0007.ToString();
                            break;
                        case "U0008":
                            analysisFormula += data.U0008.ToString();
                            break;
                        case "U0009":
                            analysisFormula += data.U0009.ToString();
                            break;
                        case "U0010":
                            analysisFormula += data.U0010.ToString();
                            break;
                        default:
                            analysisFormula += str;
                            break;
                    }
                }
                System.Data.DataTable table = new System.Data.DataTable();
               
                object result = table.Compute(analysisFormula, "");
                return Convert.ToDecimal(result);
            }
            catch (Exception ex)
            {
                var exception = ex;
                return -1;
            }
        }

        public KeHoachGiaThanhData CalculateValueElement(int year, string warehouseCode, string deliveryConditionsCode, int id)
        {
            try
            {
                var data = new KeHoachGiaThanhData();
                var lstElementTypeSystem = UnitOfWork.Repository<ElementRepo>().Queryable().Where(x => x.ELEMENT_TYPE == "S" && x.SCREEN == "GIA_THANH").OrderBy(x => x.PRIORITY).ToList();
                foreach (var element in lstElementTypeSystem)
                {
                    try
                    {
                        var value = UnitOfWork.GetSession().CreateSQLQuery($"{element.QUERY.Replace("[KHO]", warehouseCode).Replace("[DKGH]", deliveryConditionsCode).Replace("[ID_KHNH]", id.ToString()).Replace("[YEAR]", year.ToString())}").List()[0];
                        switch (element.CODE)
                        {
                            case "S0001":
                                data.S0001 = Convert.ToDecimal(value);
                                break;
                            case "S0002":
                                data.S0002 = Convert.ToDecimal(value);
                                break;
                            case "S0003":
                                data.S0003 = Convert.ToDecimal(value);
                                break;
                            case "S0004":
                                data.S0004 = Convert.ToDecimal(value);
                                break;
                            case "S0005":
                                data.S0005 = Convert.ToDecimal(value);
                                break;
                            case "S0006":
                                data.S0006 = Convert.ToDecimal(value);
                                break;
                            case "S0007":
                                data.S0007 = Convert.ToDecimal(value);
                                break;
                        }
                     
                    }
                    catch
                    {
                        UnitOfWork.Rollback();
                        this.State = false;
                        this.ErrorMessage = $"Câu lệnh SQL của element {element.CODE} sai! Vui lòng kiểm tra lại!";
                        return new KeHoachGiaThanhData();
                    }
                }
                //Analysis formula
                var lstElementTypeUser = UnitOfWork.Repository<ElementRepo>().Queryable().Where(x => x.ELEMENT_TYPE == "U" && x.SCREEN == "GIA_THANH").OrderBy(x => x.PRIORITY).ToList();
                foreach (var element in lstElementTypeUser)
                {
                    var value = AnalysisFormula(element.FORMULA, data);
                    if (value == -1)
                    {
                        UnitOfWork.Rollback();
                        this.State = false;
                        this.ErrorMessage = $"Sai công thức tại element {element.CODE}";
                        return new KeHoachGiaThanhData();
                    }
                    else
                    {
                        switch (element.CODE)
                        {
                            case "U0001":
                                data.U0001 = Convert.ToDecimal(value);
                                break;
                            case "U0002":
                                data.U0002 = Convert.ToDecimal(value);
                                break;
                            case "U0003":
                                data.U0003 = Convert.ToDecimal(value);
                                break;
                            case "U0004":
                                data.U0004 = Convert.ToDecimal(value);
                                break;
                            case "U0005":
                                data.U0005 = Convert.ToDecimal(value);
                                break;
                            case "U0006":
                                data.U0006 = Convert.ToDecimal(value);
                                break;
                            case "U0007":
                                data.U0007 = Convert.ToDecimal(value);
                                break;
                            case "U0008":
                                data.U0008 = Convert.ToDecimal(value);
                                break;
                            case "U0009":
                                data.U0009 = Convert.ToDecimal(value);
                                break;
                            case "U0010":
                                data.U0010 = Convert.ToDecimal(value);
                                break;
                        }
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                State = false;
                Exception = ex;
                return new KeHoachGiaThanhData();
            }
        }

        public decimal? AnalysisFormulaTaiChinh(string formula, IList<KeHoachTaiChinhData> data)
        {
            try
            {
                var analysisFormula = "";
                var stringFormula = Regex.Replace(formula, @"\s", "");
                string spacedFormula = AddSpacesToFormula("x=" + formula);

                string[] stringArray = spacedFormula.Replace("x = ", "").Split(' ');
                foreach (var str in stringArray)
                {
                    var item = data.FirstOrDefault(x => x.ElementCode == str);
                    if (item != null)
                    {
                        analysisFormula += item.Value;
                    }
                    else
                    {
                        analysisFormula += str;
                    }
                }
                System.Data.DataTable table = new System.Data.DataTable();
                object result = table.Compute(analysisFormula, "");
                var stringResult = result.ToString();
                return formula == "" || formula == null ? 0 : Convert.ToDecimal(result);
            }
            catch (Exception ex)
            {
                var exception = ex;
                return -1;
            }
        }
        public DataCenterModel GetDataKeHoachGiaThanh(int year)
        {
            try
            {
                var data = new DataCenterModel();
                var lstWarehouse = UnitOfWork.Repository<WarehouseRepo>().GetAll().ToList();
                var lstDeliveryConditions = UnitOfWork.Repository<DeliveryConditionsRepo>().GetAll().ToList();
                var lstSharedData = UnitOfWork.Repository<SharedDataRepo>().GetAll().ToList();
                var lstRoute = UnitOfWork.Repository<RouteRepo>().GetAll().ToList();
                var lstElement = UnitOfWork.Repository<ElementRepo>().GetAll().ToList();

                var puchaseData = UnitOfWork.Repository<PurchaseDataRepo>().Queryable().Where(x => x.TIME_YEAR == year).ToList();

                var order = 0;
                //Pre kho 1
                foreach (var item in puchaseData)
                {
                    var dataCalculate = CalculateValueElement(year, item.WAREHOUSE_CODE, item.DELIVERY_CONDITIONS_CODE, item.ID_KHNH);
                    dataCalculate.WarehouseCode = item.WAREHOUSE_CODE;
                    dataCalculate.Warehouse = item.WAREHOUSE_CODE;
                    dataCalculate.DeliveryConditionsCode = item.DELIVERY_CONDITIONS_CODE;
                    dataCalculate.DeliveryConditions = item.DELIVERY_CONDITIONS_CODE;
                    dataCalculate.Order = order;
                    data.KeHoachGiaThanhData.Add(dataCalculate);
                    order++; 
                }

                //Pre TB Kho
                var keHoachGiaThanhData = data.KeHoachGiaThanhData.GroupBy(x => x.WarehouseCode).Select(x => x.First()).ToList();
                foreach (var item in keHoachGiaThanhData)
                {
                    var pretbkho = new PreTrungBinhKhoData();
                    pretbkho.WarehouseCode = item.WarehouseCode;
                    pretbkho.Warehouse = item.Warehouse;

                    if (item.WarehouseCode == "LCH" || item.WarehouseCode == "DQU")
                    {
                        pretbkho.PreTrungBinh = item.U0008;
                    }
                    else
                    {
                        var a = data.KeHoachGiaThanhData.Where(x => x.WarehouseCode == item.WarehouseCode).Sum(x => x.U0008 * x.S0002);
                        var b = data.KeHoachGiaThanhData.Where(x => x.WarehouseCode == item.WarehouseCode).Sum(x => x.S0002);
                        pretbkho.PreTrungBinh = a == 0 || b == 0 ? 0 : a / b;
                    }

                    if (item.WarehouseCode == "CLA")
                    {
                        var c = data.KeHoachGiaThanhData.Where(x => x.Warehouse == item.Warehouse).Sum(x => x.S0002);
                        pretbkho.SanLuong = c == 0 ? 0 : c / lstSharedData.FirstOrDefault(x => x.CODE == "5").VALUE - data.KeHoachGiaThanhData.Where(x => x.WarehouseCode == "LCH").Sum(x => x.S0002);
                    }
                    else
                    {
                        var d = data.KeHoachGiaThanhData.Where(x => x.WarehouseCode == item.WarehouseCode).Sum(x => x.S0002);
                        pretbkho.SanLuong = d == 0 ? 0 : d / lstSharedData.FirstOrDefault(x => x.CODE == "5").VALUE;
                    }

                    var e = pretbkho.PreTrungBinh;
                    pretbkho.TrungBinh = e == 0 ? 0 : e * lstSharedData.FirstOrDefault(x => x.CODE == "2").VALUE / lstSharedData.FirstOrDefault(x => x.CODE == "4").VALUE;


                    var f = pretbkho.PreTrungBinh * lstSharedData.FirstOrDefault(x => x.CODE == "2").VALUE * 1000;
                    pretbkho.PreDN1 = f == 0 ? 0 : f / lstSharedData.FirstOrDefault(x => x.CODE == "4").VALUE;
                    data.PreTrungBinhKhoData.Add(pretbkho);
                }

                //Pre
                var elementSL = UnitOfWork.Repository<ElementRepo>().Queryable().FirstOrDefault(x => x.CODE == "S0008");
                foreach (var route in lstRoute)
                {
                    var item = new PreData
                    {
                        FirstPoint1 = route.FIRST_POINT,
                        FinalPoint = route.FINAL_POINT,
                        RouteCode = route.CODE,
                        RouteName = route.NAME,
                        Quantity = Convert.ToDecimal(UnitOfWork.GetSession().CreateSQLQuery($"{elementSL.QUERY.Replace("[ROUTE_CODE]", route.CODE).Replace("[YEAR]", year.ToString())}").List()[0]),
                        Premium = data.PreTrungBinhKhoData.FirstOrDefault(x => x.WarehouseCode.Trim() == route.FIRST_POINT.Trim()) == null ? 0 : data.PreTrungBinhKhoData.FirstOrDefault(x => x.WarehouseCode.Trim() == route.FIRST_POINT.Trim()).PreDN1
                    };
                    data.PreData.Add(item);
                }

                var lstSanBay = UnitOfWork.Repository<SanBayRepo>().Queryable().Where(x => x.OTHER_PM_CODE != null && x.OTHER_PM_CODE != "").ToList();

                //Dữ liệu
                foreach (var route in lstRoute)
                {
                    var item = new DuLieuData
                    {
                        RouteCode = route.CODE,
                        RouteName = route.NAME,
                        FinalPoint = route.FINAL_POINT,
                        FirstPoint = route.FIRST_POINT,
                        KmCoHang = route.KM_CO_HANG,
                        Quantity = Convert.ToDecimal(UnitOfWork.GetSession().CreateSQLQuery($"{lstElement.FirstOrDefault(x => x.CODE == "S0008").QUERY.Replace("[ROUTE_CODE]", route.CODE).Replace("[YEAR]", year.ToString())}").List()[0]),
                        TN = Convert.ToDecimal(UnitOfWork.GetSession().CreateSQLQuery($"{lstElement.FirstOrDefault(x => x.CODE == "S0011").QUERY.Replace("[ROUTE_CODE]", route.CODE).Replace("[YEAR]", year.ToString())}").List()[0]),
                        BR = Convert.ToDecimal(UnitOfWork.GetSession().CreateSQLQuery($"{lstElement.FirstOrDefault(x => x.CODE == "S0012").QUERY.Replace("[ROUTE_CODE]", route.CODE).Replace("[YEAR]", year.ToString())}").List()[0]),
                        BQ = Convert.ToDecimal(UnitOfWork.GetSession().CreateSQLQuery($"{lstElement.FirstOrDefault(x => x.CODE == "S0013").QUERY.Replace("[ROUTE_CODE]", route.CODE).Replace("[YEAR]", year.ToString())}").List()[0]),
                        HHKhoGoc = Convert.ToDecimal(UnitOfWork.GetSession().CreateSQLQuery($"{lstElement.FirstOrDefault(x => x.CODE == "S0014").QUERY.Replace("[ROUTE_CODE]", route.CODE).Replace("[YEAR]", year.ToString())}").List()[0]),
                        TauNoi = Convert.ToDecimal(UnitOfWork.GetSession().CreateSQLQuery($"{lstElement.FirstOrDefault(x => x.CODE == "S0015").QUERY.Replace("[ROUTE_CODE]", route.CODE).Replace("[YEAR]", year.ToString())}").List()[0]),
                        CongHH = Convert.ToDecimal(UnitOfWork.GetSession().CreateSQLQuery($"{lstElement.FirstOrDefault(x => x.CODE == "S0016").QUERY.Replace("[ROUTE_CODE]", route.CODE).Replace("[YEAR]", year.ToString())}").List()[0]),
                        DGVTTND = Convert.ToDecimal(UnitOfWork.GetSession().CreateSQLQuery($"{lstElement.FirstOrDefault(x => x.CODE == "S0017").QUERY.Replace("[ROUTE_CODE]", route.CODE).Replace("[YEAR]", year.ToString())}").List()[0]),
                        DCDGVTT = Convert.ToDecimal(UnitOfWork.GetSession().CreateSQLQuery($"{lstElement.FirstOrDefault(x => x.CODE == "S0018").QUERY.Replace("[ROUTE_CODE]", route.CODE).Replace("[YEAR]", year.ToString())}").List()[0]),
                        DGVTTNDSDC = Convert.ToDecimal(UnitOfWork.GetSession().CreateSQLQuery($"{lstElement.FirstOrDefault(x => x.CODE == "S0019").QUERY.Replace("[ROUTE_CODE]", route.CODE).Replace("[YEAR]", year.ToString())}").List()[0]),
                        DN1 = Convert.ToDecimal(UnitOfWork.GetSession().CreateSQLQuery($"{lstElement.FirstOrDefault(x => x.CODE == "S0020").QUERY.Replace("[ROUTE_CODE]", route.CODE).Replace("[YEAR]", year.ToString())}").List()[0]),
                        DN2 = Convert.ToDecimal(UnitOfWork.GetSession().CreateSQLQuery($"{lstElement.FirstOrDefault(x => x.CODE == "S0021").QUERY.Replace("[ROUTE_CODE]", route.CODE).Replace("[YEAR]", year.ToString())}").List()[0]),
                        DNTotal = Convert.ToDecimal(UnitOfWork.GetSession().CreateSQLQuery($"{lstElement.FirstOrDefault(x => x.CODE == "S0022").QUERY.Replace("[ROUTE_CODE]", route.CODE).Replace("[YEAR]", year.ToString())}").List()[0]),
                        DNOther = Convert.ToDecimal(UnitOfWork.GetSession().CreateSQLQuery($"{lstElement.FirstOrDefault(x => x.CODE == "S0023").QUERY.Replace("[ROUTE_CODE]", route.CODE).Replace("[YEAR]", year.ToString())}").List()[0]),
                        DNKSDC = Convert.ToDecimal(UnitOfWork.GetSession().CreateSQLQuery($"{lstElement.FirstOrDefault(x => x.CODE == "S0024").QUERY.Replace("[ROUTE_CODE]", route.CODE).Replace("[YEAR]", year.ToString())}").List()[0]),
                    };
                    var g = item.KmCoHang * lstSharedData.FirstOrDefault(x => x.CODE == "16").VALUE;
                    item.HaoHutVC = g == 0 ? 0 : g / 10000;
                    data.DuLieuData.Add(item);
                }

                //Đơn giá kho đầu nguồn
                foreach (var wh in lstWarehouse)
                {
                    var lstWhs = data.DuLieuData.Where(x => x.FinalPoint == wh.CODE).ToList();
                    var a = lstWhs.Sum(x => x.Quantity * x.HaoHutVC);
                    var b = lstWhs.Sum(x => x.Quantity);
                    var c = lstWhs.Sum(x => x.Quantity * x.CongHH);
                    var d = lstWhs.Sum(x => x.Quantity * x.DGVTTNDSDC);
                    var e = lstWhs.Sum(x => x.Quantity * x.DNKSDC);
                    var item = new DonGiaKhoDauNguonData
                    {
                        WarehouseCode = wh.CODE,
                        WarehouseName = wh.TEXT,
                        VanChuyenDuongBo = a == 0 || b == 0 ? 0 : a / b * 100,
                        Other = c == 0 || b == 0 ? 0 : c / b,
                        DonGiaTauNoi = d == 0 || b == 0 ? 0 : d / b / lstSharedData.FirstOrDefault(x => x.CODE == "5").VALUE,
                        DonGiaKhoDauNguon = e == 0 || b == 0 ? 0 : e / b / lstSharedData.FirstOrDefault(x => x.CODE == "5").VALUE,
                    };
                    item.TongHaoHut = item.VanChuyenDuongBo + item.Other;
                    data.DonGiaKhoDauNguonData.Add(item);
                }


                //Kho đầu nguồn
                foreach (var wh in lstWarehouse)
                {
                    var item = new KhoDauNguonData
                    {
                        WarehouseCode = wh.CODE,
                        WarehouseName = wh.TEXT
                    };
                    data.KhoDauNguonData.Add(item);
                }

                //Cung ứng
                foreach (var sb in lstSanBay)
                {
                    var item = new CungUngData
                    {
                        SanBayCode = sb.CODE,
                    };
                    data.CungUngData.Add(item);
                }

                //Mua hàng cung ứng tra nạp
                foreach (var sb in lstSanBay)
                {
                    var lstSb = data.PreData.Where(x => x.FinalPoint == sb.CODE).ToList();
                    var a = lstSb.Sum(x => x.Quantity * x.Premium);
                    var b = lstSb.Sum(x => x.Quantity);
                    var item = new MuaHangCungUngTraNapData
                    {
                        SanBayCode = sb.CODE,
                        Premium = a == 0 || b == 0 ? 0 : a / b / lstSharedData.FirstOrDefault(x => x.CODE == "5").VALUE,
                    };
                    data.MuaHangCungUngTraNapData.Add(item);
                }
                return data;
            }
            catch (Exception ex)
            {
                this.State = false;
                this.Exception = ex;
                UnitOfWork.Rollback();
                return new DataCenterModel();
            }
        }

        public DataCenterModel GetDataKeHoachGiaVon(int year, string area)
        {
            try
            {
                var data = new DataCenterModel();
                var lstWarehouse = UnitOfWork.Repository<WarehouseRepo>().GetAll().ToList();
                var lstDeliveryConditions = UnitOfWork.Repository<DeliveryConditionsRepo>().GetAll().ToList();
                var lstSharedData = UnitOfWork.Repository<SharedDataRepo>().GetAll().ToList();
                var lstRoute = UnitOfWork.Repository<RouteRepo>().GetAll().ToList();
                var lstElement = UnitOfWork.Repository<ElementRepo>().GetAll().ToList();

                var lstHangHangKhong = UnitOfWork.Repository<HangHangKhongRepo>().GetAll().GroupBy(x => x.GROUP_ITEM).Select(x => x.First()).ToList();
                var sanBayGroup = UnitOfWork.Repository<NhomSanBayRepo>().GetAll().ToList();

                var puchaseData = UnitOfWork.Repository<PurchaseDataRepo>().Queryable().Where(x => x.TIME_YEAR == year).ToList();
                if (!string.IsNullOrEmpty(area))
                {
                    puchaseData = puchaseData.Where(x => x.AREA_ID == area).ToList();
                }
                //Pre kho 1
                foreach (var item in puchaseData)
                {
                    var dataCalculate = CalculateValueElement(year, item.WAREHOUSE_CODE, item.DELIVERY_CONDITIONS_CODE, item.ID_KHNH);
                    dataCalculate.WarehouseCode = item.WAREHOUSE_CODE;
                    dataCalculate.Warehouse = item.WAREHOUSE_CODE;
                    dataCalculate.DeliveryConditionsCode = item.DELIVERY_CONDITIONS_CODE;
                    dataCalculate.DeliveryConditions = item.DELIVERY_CONDITIONS_CODE;
                    dataCalculate.S0008 = item.S0008 / 100;
                    dataCalculate.ThueSuat = dataCalculate.S0008 * (dataCalculate.U0003 + dataCalculate.S0006); // tính thuế NK($/Tấn) 
                    dataCalculate.AreaCode = item.AREA_ID;
                    data.KeHoachGiaThanhData.Add(dataCalculate);
                }

                var value2 = lstSharedData.FirstOrDefault(x => x.CODE == "1").VALUE * lstSharedData.FirstOrDefault(x => x.CODE == "3").VALUE;
                var value3 = lstSharedData.FirstOrDefault(x => x.CODE == "12").VALUE * lstSharedData.FirstOrDefault(x => x.CODE == "3").VALUE;
                var value4 = value2 + value3;
                //var value5 = data.KeHoachGiaThanhData.Sum(x => x.ThueSuat * x.S0002) / data.KeHoachGiaThanhData.Where(x => x.S0008 != 0).Sum(x => x.S0002);
                var value5 = lstSharedData.FirstOrDefault(x => x.CODE == "1").VALUE * lstSharedData.FirstOrDefault(x => x.CODE == "3").VALUE * lstSharedData.FirstOrDefault(x => x.CODE == "TNK-VN").VALUE;
                var value6 = lstSharedData.FirstOrDefault(x => x.CODE == "22").VALUE;
                var value8 = (value2 + value3 + value5) * lstSharedData.FirstOrDefault(x => x.CODE == "19").VALUE;
                var value9 = (value2 + value3 + value5);
                var value11 = lstSharedData.FirstOrDefault(x => x.CODE == "2").VALUE;

                var dataHeaderSanLuong = UnitOfWork.Repository<KeHoachSanLuongRepo>().Queryable().Where(x => x.TIME_YEAR == year && x.PHIEN_BAN == "PB1" && x.KICH_BAN == "TB" && x.STATUS == "03").Select(x => x.TEMPLATE_CODE).ToList();
                if (dataHeaderSanLuong.Count() != 0)
                {
                    var dataInHeader = UnitOfWork.Repository<KeHoachSanLuongDataRepo>().Queryable().Where(x => x.TIME_YEAR == year && dataHeaderSanLuong.Contains(x.TEMPLATE_CODE)).ToList();

                    //Tab2
                    int countGroup = sanBayGroup.Count();
                    int order = 0;

                    data.KeHoachGiaVonData.Add(new KeHoachGiaVonData
                    {
                        Code = "TC",
                        ParentCode = null,
                        Name = "TỔNG CỘNG",
                        Value1 = dataInHeader.Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        IsBold = true,
                        Order = -1,
                        Level = 0
                    });

                    foreach (var hhk in lstHangHangKhong.Where(x => !string.IsNullOrEmpty(x.GROUP_ITEM)))
                    {
                        var FHS_NBA = lstSharedData.FirstOrDefault(x => x.CODE == "FHS-NBA-" + hhk.GROUP_ITEM)?.VALUE;
                        var FHS_TNS = lstSharedData.FirstOrDefault(x => x.CODE == "FHS-TNS-" + hhk.GROUP_ITEM)?.VALUE;

                        data.KeHoachGiaVonData.Add(new KeHoachGiaVonData
                        {
                            Code = hhk.GROUP_ITEM + "-01",
                            ParentCode = "TC",
                            Name = hhk.GROUP_ITEM,
                            Value1 = dataInHeader.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM).Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                            IsBold = true,
                            Order = order,
                            Parent = "-1",
                            Level = 0
                        });
                        data.KeHoachGiaVonData.Add(new KeHoachGiaVonData
                        {
                            Code = hhk.GROUP_ITEM + "-02",
                            ParentCode = hhk.GROUP_ITEM + "-01",
                            Name = "Nội địa",
                            Value1 = dataInHeader.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                            IsBold = true,
                            Order = order + 1,
                            Parent = order.ToString(),
                            Level = 1
                        });

                        data.KeHoachGiaVonData.Add(new KeHoachGiaVonData
                        {
                            Code = hhk.GROUP_ITEM + "-03",
                            ParentCode = hhk.GROUP_ITEM + "-02",
                            Name = "Qua xe",
                            Value1 = dataInHeader.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                            Value2 = value2,
                            Value3 = value3,
                            Value4 = value4,
                            Value5 = value5,
                            Value6 = value6,
                            Value8 = value8,
                            Value9 = value9 * lstSharedData.FirstOrDefault(x => x.CODE == "20").VALUE,
                            Value11 = value11,
                            Order = order + 2,
                            IsBold = true,
                            Parent = (order + 1).ToString(),
                            Level = 2
                        });

                        data.KeHoachGiaVonData.Add(new KeHoachGiaVonData
                        {
                            Code = hhk.GROUP_ITEM + "-04",
                            ParentCode = hhk.GROUP_ITEM + "-02",
                            Name = "Qua FHS",
                            Value1 = dataInHeader.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP")).Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                            IsBold = true,
                            Order = order + 3,
                            Parent = (order + 1).ToString(),
                            Level = 2
                        });

                        data.KeHoachGiaVonData.Add(new KeHoachGiaVonData
                        {
                            Code = hhk.GROUP_ITEM + "-05",
                            ParentCode = hhk.GROUP_ITEM + "-04",
                            Name = "FHS NBA",
                            Value1 = dataInHeader.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                            Value2 = value2,
                            Value3 = value3,
                            Value4 = value4,
                            Value5 = value5,
                            Value6 = value6,
                            Value8 = value8,
                            Value9 = value9 * lstSharedData.FirstOrDefault(x => x.CODE == "21").VALUE,
                            Value10 = FHS_NBA ?? 0,
                            Value11 = value11,
                            Order = order + 4,
                            Parent = (order + 3).ToString(),
                            Level = 3
                        });
                        data.KeHoachGiaVonData.Add(new KeHoachGiaVonData
                        {
                            Code = hhk.GROUP_ITEM + "-06",
                            ParentCode = hhk.GROUP_ITEM + "-04",
                            Name = "FHS TNS",
                            Value1 = dataInHeader.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010" && x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                            Value2 = value2,
                            Value3 = value3,
                            Value4 = value4,
                            Value5 = value5,
                            Value6 = value6,
                            Value8 = value8,
                            Value9 = value9 * lstSharedData.FirstOrDefault(x => x.CODE == "21").VALUE,
                            Value10 = FHS_TNS ?? 0,
                            Value11 = value11,
                            Order = order + 5,
                            Parent = (order + 3).ToString(),
                            Level = 3
                        });


                        data.KeHoachGiaVonData.Add(new KeHoachGiaVonData
                        {
                            Code = hhk.GROUP_ITEM + "-07",
                            ParentCode = hhk.GROUP_ITEM + "-01",
                            Name = "Quốc tế",
                            Value1 = dataInHeader.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                            IsBold = true,
                            Order = order + 6,
                            Parent = order.ToString(),
                            Level = 1
                        });

                        data.KeHoachGiaVonData.Add(new KeHoachGiaVonData
                        {
                            Code = hhk.GROUP_ITEM + "-08",
                            ParentCode = hhk.GROUP_ITEM + "-07",
                            Name = "Qua xe",
                            Value1 = dataInHeader.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                            Value2 = value2,
                            Value3 = value3,
                            Value4 = value4,
                            Value5 = value5,
                            Value6 = value6,
                            Value8 = value8,
                            Value9 = value9 * lstSharedData.FirstOrDefault(x => x.CODE == "20").VALUE,
                            Value11 = value11,
                            Parent = (order + 6).ToString(),
                            Order = order + 7,
                            Level = 2
                        });

                        data.KeHoachGiaVonData.Add(new KeHoachGiaVonData
                        {
                            Code = hhk.GROUP_ITEM + "-09",
                            ParentCode = hhk.GROUP_ITEM + "-07",
                            Name = "Qua FHS",
                            Value1 = dataInHeader.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && (x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP")).Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                            IsBold = true,
                            Order = order + 8,
                            Parent = (order + 6).ToString(),
                            Level = 2
                        });

                        data.KeHoachGiaVonData.Add(new KeHoachGiaVonData
                        {
                            Code = hhk.GROUP_ITEM + "-10",
                            ParentCode = hhk.GROUP_ITEM + "-09",
                            Name = "FHS NBA",
                            Value1 = dataInHeader.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                            Value2 = value2,
                            Value3 = value3,
                            Value4 = value4,
                            Value5 = value5,
                            Value6 = value6,
                            Value8 = value8,
                            Value9 = value9 * lstSharedData.FirstOrDefault(x => x.CODE == "21").VALUE,
                            Value10 = FHS_NBA ?? 0,
                            Value11 = value11,
                            Order = order + 9,
                            Parent = (order + 8).ToString(),
                            Level = 3
                        });
                        data.KeHoachGiaVonData.Add(new KeHoachGiaVonData
                        {
                            Code = hhk.GROUP_ITEM + "-11",
                            ParentCode = hhk.GROUP_ITEM + "-09",
                            Name = "FHS TNS",
                            Value1 = dataInHeader.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP").Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                            Value2 = value2,
                            Value3 = value3,
                            Value4 = value4,
                            Value5 = value5,
                            Value6 = value6,
                            Value8 = value8,
                            Value9 = value9 * lstSharedData.FirstOrDefault(x => x.CODE == "21").VALUE,
                            Value10 = FHS_TNS ?? 0,
                            Value11 = value11,
                            Parent = (order + 8).ToString(),
                            Order = order + 10,
                            Level = 3
                        });

                        order += 11;
                    }
                    data.KeHoachGiaVonData.Add(new KeHoachGiaVonData
                    {
                        Code = "HQ",
                        ParentCode = "TC",
                        Name = "BÁN VNA TẠI HQ",
                        Value1 = dataInHeader.Where(x =>string.IsNullOrEmpty(x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM)).Sum(x => x.VALUE_SUM_YEAR) ?? 0,
                        Value2 = value2,
                        Value3 = value3,
                        Value4 = value4,
                        Value5 = value5,
                        Value6 = value6,
                        Value8 = value8,
                        Value9 = value9 * lstSharedData.FirstOrDefault(x => x.CODE == "21").VALUE,
                        Value10 = 45,
                        Value11 = value11,
                        IsBold = true,
                        Order = order,
                        Parent = "-1",
                        Level = 0
                    });
                    foreach (var item in data.KeHoachGiaVonData)
                    {
                        var value12 = item.Value2 + item.Value3 + item.Value5 + item.Value6 + item.Value7 + item.Value10;
                        item.Value12 = value12;
                        item.Value14 = item.Value1 * item.Value11 * item.Value2;
                        item.Value15 = item.Value1 * item.Value11 * item.Value3;
                        if (item.Code.Contains("-02") || item.Code.Contains("-03") || item.Code.Contains("-04") || item.Code.Contains("-05") || item.Code.Contains("-06"))
                        {
                            item.Value16 = item.Value1 * item.Value11 * item.Value5;
                        }
                        item.Value17 = item.Value1 * item.Value6;
                        item.Value18 = item.Value1 * item.Value11 * item.Value8;
                        item.Value19 = item.Value1 * item.Value11 * item.Value9;
                        item.Value20 = item.Value1 * item.Value11 * item.Value10;
                        item.Value13 = item.Value1 * item.Value12;
                        item.Value7 = item.Value9 + item.Value8;
                    }

                    foreach (var item in data.KeHoachGiaVonData.OrderByDescending(x => x.Order))
                    {
                        var child = data.KeHoachGiaVonData.Where(x => x.ParentCode == item.Code).ToList();
                        if (child.Count() != 0)
                        {
                            item.Value13 = child.Sum(x => x.Value13);
                            item.Value14 = child.Sum(x => x.Value14);
                            item.Value15 = child.Sum(x => x.Value15);
                            item.Value16 = child.Sum(x => x.Value16);
                            item.Value17 = child.Sum(x => x.Value17);
                            item.Value18 = child.Sum(x => x.Value18);
                            item.Value19 = child.Sum(x => x.Value19);
                            item.Value20 = child.Sum(x => x.Value20);
                        }
                    }


                    //Tính kế hoạch giá vốn theo tháng
                    decimal valueND = 0;
                    decimal valueQX = 0;
                    decimal valueQFS = 0;
                    var valueSumSL = data.KeHoachGiaVonData.FirstOrDefault(x => x.Code == "TC")?.Value1;

                    var dataDetails = dataInHeader;
                    var month1 = dataDetails.Sum(x => x.VALUE_JAN) ?? 0;
                    var month2 = dataDetails.Sum(x => x.VALUE_FEB) ?? 0;
                    var month3 = dataDetails.Sum(x => x.VALUE_MAR) ?? 0;
                    var month4 = dataDetails.Sum(x => x.VALUE_APR) ?? 0;
                    var month5 = dataDetails.Sum(x => x.VALUE_MAY) ?? 0;
                    var month6 = dataDetails.Sum(x => x.VALUE_JUN) ?? 0;
                    var month7 = dataDetails.Sum(x => x.VALUE_JUL) ?? 0;
                    var month8 = dataDetails.Sum(x => x.VALUE_AUG) ?? 0;
                    var month9 = dataDetails.Sum(x => x.VALUE_SEP) ?? 0;
                    var month10 = dataDetails.Sum(x => x.VALUE_OCT) ?? 0;
                    var month11 = dataDetails.Sum(x => x.VALUE_NOV) ?? 0;
                    var month12 = dataDetails.Sum(x => x.VALUE_SEP) ?? 0;
                    var sum = dataDetails.Sum(x => x.VALUE_SUM_YEAR) ?? 0;

                    foreach (var hhk in lstHangHangKhong)
                    {
                        var valueNDHHK = dataInHeader.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_SUM_YEAR) ?? 0;
                        valueND = valueND + valueNDHHK;
                    }
                    var orderTC = 0;
                    // Mops
                    var Mops = new KeHoachGiaVonTheoThang
                    {
                        Code = "Mops",
                        Name = "Mops",
                        DVT = "USD/tấn",
                        ValueDG = value2,
                        Value1 = month1 * value2 * value11,
                        Value2 = month2 * value2 * value11,
                        Value3 = month3 * value2 * value11,
                        Value4 = month4 * value2 * value11,
                        Value5 = month5 * value2 * value11,
                        Value6 = month6 * value2 * value11,
                        Value7 = month7 * value2 * value11,
                        Value8 = month8 * value2 * value11,
                        Value9 = month9 * value2 * value11,
                        Value10 = month10 * value2 * value11,
                        Value11 = month11 * value2 * value11,
                        Value12 = month12 * value2 * value11,
                        SumGV = sum * value2 * value11,
                        desMonth = "Dữ liệu kế hoạch Sản lượng theo tháng * Giá Platts (USD/thùng) * Hệ số quy đổi thùng/tấn * tỷ giá  ",
                        desDG= "Giá Platts (USD/thùng)* Hệ số quy đổi thùng/tấn",
                        destotal= "Tổng giá trị sản lượng tháng * Giá Platts (USD/thùng) * Hệ số quy đổi thùng/tấn * tỷ giá  ",
                        Order = 1,
                        Level = 0,
                        Parent = orderTC.ToString()
                    };
                    data.KeHoachGiaVonTheoThang.Add(Mops);
                    //Pre
                    var Pre = new KeHoachGiaVonTheoThang
                    {
                        Code = "Pre",
                        Name = "Pre + BH",
                        DVT = "USD/tấn",
                        ValueDG = value3,
                        Value1 = month1 * value3 * value11,
                        Value2 = month2 * value3 * value11,
                        Value3 = month3 * value3 * value11,
                        Value4 = month4 * value3 * value11,
                        Value5 = month5 * value3 * value11,
                        Value6 = month6 * value3 * value11,
                        Value7 = month7 * value3 * value11,
                        Value8 = month8 * value3 * value11,
                        Value9 = month9 * value3 * value11,
                        Value10 = month10 * value3 * value11,
                        Value11 = month11 * value3 * value11,
                        Value12 = month12 * value3 * value11,
                        SumGV = sum * value3 * value11,
                        Order = 2,
                        Level = 0,
                        Parent = orderTC.ToString(),
                        desDG= "Pre bình quân (nhập + mua)*Hệ số quy đổi thùng/tấn",
                        desMonth= "Dữ liệu kế hoạch Sản lượng theo tháng * Pre bình quân (nhập + mua)*Hệ số quy đổi thùng/tấn",
                        destotal= "Tổng giá trị kế hoạch Sản lượng theo tháng * Pre bình quân (nhập + mua)*Hệ số quy đổi thùng/tấn",

                    };
                    data.KeHoachGiaVonTheoThang.Add(Pre);

                    var SLND = new KeHoachGiaVonTheoThang
                    {
                        Code = "SLND",
                        Name = "Sản lượng nội địa",
                        ParentCode = "Thue",
                        DVT = "tấn",
                        Value1 = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_JAN) ?? 0,
                        Value2 = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_FEB) ?? 0,
                        Value3 = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_MAR) ?? 0,
                        Value4 = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_APR) ?? 0,
                        Value5 = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_MAY) ?? 0,
                        Value6 = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_JUN) ?? 0,
                        Value7 = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_JUL) ?? 0,
                        Value8 = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_AUG) ?? 0,
                        Value9 = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_SEP) ?? 0,
                        Value10 = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_OCT) ?? 0,
                        Value11 = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_NOV) ?? 0,
                        Value12 = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_SEP) ?? 0,
                        Order = 4,
                        Level = 1,
                        desMonth = "Dữ liệu khoản mục theo tháng lấy theo sản lượng nội địa",
                        destotal = "Tổng giá trị sản lượng nội địa theo tháng lấy theo san lượng nội địa",
                        Parent = 3.ToString()
                    };

                    var sumSL = dataDetails.Where(x => x.KHOAN_MUC_SAN_LUONG_CODE == "10010").Sum(x => x.VALUE_SUM_YEAR) ?? 0;
                    //Thue
                    var thue = new KeHoachGiaVonTheoThang
                    {
                        Code = "Thue",
                        Name = "Thuế nhập khẩu",
                        DVT = "USD/tấn",
                        ValueDG = value5,
                        Value1 = value5 * SLND.Value1 * value11,
                        Value2 = value5 * SLND.Value2 * value11,
                        Value3 = value5 * SLND.Value3 * value11,
                        Value4 = value5 * SLND.Value4 * value11,
                        Value5 = value5 * SLND.Value5 * value11,
                        Value6 = value5 * SLND.Value6 * value11,
                        Value7 = value5 * SLND.Value7 * value11,
                        Value8 = value5 * SLND.Value8 * value11,
                        Value9 = value5 * SLND.Value9 * value11,
                        Value10 = value5 * SLND.Value10 * value11,
                        Value11 = value5 * SLND.Value11 * value11,
                        Value12 = value5 * SLND.Value12 * value11,
                        SumGV = sumSL * value5 * value11,
                        desMonth = "Giá Platts (USD/thùng)*Hệ số quy đổi thùng/tấn*Thuế suất thuế nhập khẩu-VN *dữ liệu khoản mục theo tháng lấy theo sản lượng nội địa *Tỷ giá",
                        desDG = "Giá Platts (USD/thùng)*Hệ số quy đổi thùng/tấn*Thuế suất thuế nhập khẩu-VN",
                        destotal = "Giá Platts (USD/thùng)*Hệ số quy đổi thùng/tấn*Thuế suất thuế nhập khẩu-VN * Tổng giá trị sản lượng nội địa theo tháng lấy theo sản lượng nội địa *tỷ giá",
                        Order = 3,
                        Level = 0,
                        Parent = orderTC.ToString()
                    };
                    data.KeHoachGiaVonTheoThang.Add(thue);
                    data.KeHoachGiaVonTheoThang.Add(SLND);
                    //Dv mua ngoài
                    var dvmn = new KeHoachGiaVonTheoThang
                    {
                        Code = "DV",
                        Name = "Dịch vụ mua ngoài",
                        DVT = "VND/tấn",
                        ValueDG = value6,
                        Value1 = value6 * month1,
                        Value2 = value6 * month2,
                        Value3 = value6 * month3,
                        Value4 = value6 * month4,
                        Value5 = value6 * month5,
                        Value6 = value6 * month6,
                        Value7 = value6 * month7,
                        Value8 = value6 * month8,
                        Value9 = value6 * month9,
                        Value10 = value6 * month10,
                        Value11 = value6 * month11,
                        Value12 = value6 * month12,
                        SumGV = value6 * sum,
                        desMonth = "Đơn giá CP mua hàng * dự liệu kế hoạch sản lượng theo tháng",
                        desDG = "Đơn giá CP mua hàng",
                        destotal = "Đơn giá CP mua hàng * Tổng giá trị sản lượng theo tháng",
                        Order = 5,
                        Parent = orderTC.ToString()
                    };
                    data.KeHoachGiaVonTheoThang.Add(dvmn);

                    var SLQX = new KeHoachGiaVonTheoThang
                    {
                        Code = "SLQX",
                        Name = "Sản lượng qua xe",
                        DVT = "tấn",
                        Value1 = dataInHeader.Where(x=>x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP").Sum(x => x.VALUE_JAN) ?? 0,
                        Value2 = dataInHeader.Where(x=>x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP").Sum(x => x.VALUE_FEB) ?? 0,
                        Value3 = dataInHeader.Where(x=>x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP").Sum(x => x.VALUE_MAR) ?? 0,
                        Value4 = dataInHeader.Where(x=>x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP").Sum(x => x.VALUE_APR) ?? 0,
                        Value5 = dataInHeader.Where(x=>x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP").Sum(x => x.VALUE_MAY) ?? 0,
                        Value6 = dataInHeader.Where(x=>x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP").Sum(x => x.VALUE_JUN) ?? 0,
                        Value7 = dataInHeader.Where(x=>x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP").Sum(x => x.VALUE_JUL) ?? 0,
                        Value8 = dataInHeader.Where(x=>x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP").Sum(x => x.VALUE_AUG) ?? 0,
                        Value9 = dataInHeader.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP").Sum(x => x.VALUE_SEP) ?? 0,
                        Value10 = dataInHeader.Where(x=>x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP").Sum(x => x.VALUE_OCT) ?? 0,
                        Value11 = dataInHeader.Where(x=>x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP").Sum(x => x.VALUE_NOV) ?? 0,
                        Value12 = dataInHeader.Where(x=>x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP").Sum(x => x.VALUE_SEP) ?? 0,
                        desMonth = "Dữ liệu kế hoạch Sản lượng theo tháng lấy khác tra nạp ngầm nội địa và tra nạp ngầm tân sơn nhất",
                        destotal = "Tổng dữ liệu kế hoạch Sản lượng theo tháng lấy khác tra nạp ngầm nội địa và tra nạp ngầm tân sơn nhất",

                        Level = 1,

                        Order = 9,
                        Parent = 6.ToString()
                    };
                    var sumSLQX = dataInHeader.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE != "NAF" && x.SanLuongProfitCenter.SAN_BAY_CODE != "TAP").Sum(x => x.VALUE_SUM_YEAR) ?? 0;
                    var SLQN = new KeHoachGiaVonTheoThang
                    {
                        Code = "SLQN",
                        Name = "Sản lượng qua ngầm",
                        DVT = "tấn",
                        Value1 = dataInHeader.Where(x=>x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP").Sum(x => x.VALUE_JAN) ?? 0,
                        Value2 = dataInHeader.Where(x=>x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP").Sum(x => x.VALUE_FEB) ?? 0,
                        Value3 = dataInHeader.Where(x=>x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP").Sum(x => x.VALUE_MAR) ?? 0,
                        Value4 = dataInHeader.Where(x=>x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP").Sum(x => x.VALUE_APR) ?? 0,
                        Value5 = dataInHeader.Where(x=>x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP").Sum(x => x.VALUE_MAY) ?? 0,
                        Value6 = dataInHeader.Where(x=>x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP").Sum(x => x.VALUE_JUN) ?? 0,
                        Value7 = dataInHeader.Where(x=>x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP").Sum(x => x.VALUE_JUL) ?? 0,
                        Value8 = dataInHeader.Where(x=>x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP").Sum(x => x.VALUE_AUG) ?? 0,
                        Value9 = dataInHeader.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP").Sum(x => x.VALUE_SEP) ?? 0,
                        Value10 = dataInHeader.Where(x=>x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP").Sum(x => x.VALUE_OCT) ?? 0,
                        Value11 = dataInHeader.Where(x=>x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP").Sum(x => x.VALUE_NOV) ?? 0,
                        Value12 = dataInHeader.Where(x=>x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP").Sum(x => x.VALUE_SEP) ?? 0,
                        desMonth = "Dữ liệu kế hoạch Sản lượng theo tháng lấy  tra nạp ngầm nội địa và tra nạp ngầm tân sơn nhất",
                        destotal = "Tổng dữ liệu kế hoạch Sản lượng lấy  tra nạp ngầm nội địa và tra nạp ngầm tân sơn nhất",
                        Order = 10,
                        Level = 1,

                        Parent = 6.ToString()
                    };
                    var sumSLQN = dataInHeader.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP").Sum(x => x.VALUE_SUM_YEAR) ?? 0;
                    var test = dataInHeader.Where(x => x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF" || x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP");
                    //Tính chi phí
                    var TLXe = lstSharedData.FirstOrDefault(x => x.CODE == "20").VALUE;
                    var TLNg = lstSharedData.FirstOrDefault(x => x.CODE == "21").VALUE;
                    var valueDGTLXe = value11 *TLXe * (Mops.ValueDG + Pre.ValueDG + thue.ValueDG);
                    var valueDGTLNg = value11 * TLNg * (Mops.ValueDG + Pre.ValueDG + thue.ValueDG);

                    var CPXe = new KeHoachGiaVonTheoThang
                    {
                        Code = "CPXe",
                        Name = "Chi phí HH qua xe",
                        DVT = "USD/tấn",
                        ValueDG = valueDGTLXe,
                        Value1 = valueDGTLXe * SLQX.Value1,
                        Value2 = valueDGTLXe * SLQX.Value2,
                        Value3 = valueDGTLXe * SLQX.Value3,
                        Value4 = valueDGTLXe * SLQX.Value4,
                        Value5 = valueDGTLXe * SLQX.Value5,
                        Value6 = valueDGTLXe * SLQX.Value6,
                        Value7 = valueDGTLXe * SLQX.Value7,
                        Value8 = valueDGTLXe * SLQX.Value8,
                        Value9 = valueDGTLXe * SLQX.Value9,
                        Value10 = valueDGTLXe * SLQX.Value10,
                        Value11 = valueDGTLXe * SLQX.Value11,
                        Value12 = valueDGTLXe * SLQX.Value12,
                        SumGV = valueDGTLXe * sumSLQX,
                        desDG = "Tỷ lệ hao hụt tra nạp xe * tỷ giá *( đơn giá Mops +đơn giá pre +đơn giá thuế)",
                        desMonth = "Tỷ lệ hao hụt tra nạp xe * tỷ giá *( đơn giá Mops +đơn giá pre +đơn giá thuế) *dữ liệu kế hoạch Sản lượng theo tháng lấy khác tra nạp ngầm nội địa và tra nạp ngầm tân sơn nhất",
                        destotal= "Tỷ lệ hao hụt tra nạp xe * tỷ giá *( đơn giá Mops +đơn giá pre +đơn giá thuế)* Tổng dữ liệu kế hoạch Sản lượng lấy khác tra nạp ngầm nội địa và tra nạp ngầm tân sơn nhất",
                        Order = 7,
                        Level = 1,

                        Parent = 6.ToString()
                    };
                    data.KeHoachGiaVonTheoThang.Add(CPXe);
                    var CPQN = new KeHoachGiaVonTheoThang
                    {
                        Code = "CPQN",
                        Name = "Chi phí HH qua ngầm",
                        DVT = "USD/tấn",
                        ValueDG = valueDGTLNg,
                        Value1 = valueDGTLNg * SLQN.Value1,
                        Value2 = valueDGTLNg * SLQN.Value2,
                        Value3 = valueDGTLNg * SLQN.Value3,
                        Value4 = valueDGTLNg * SLQN.Value4,
                        Value5 = valueDGTLNg * SLQN.Value5,
                        Value6 = valueDGTLNg * SLQN.Value6,
                        Value7 = valueDGTLNg * SLQN.Value7,
                        Value8 = valueDGTLNg * SLQN.Value8,
                        Value9 = valueDGTLNg * SLQN.Value9,
                        Value10 = valueDGTLNg * SLQN.Value10,
                        Value11 = valueDGTLNg * SLQN.Value11,
                        Value12 = valueDGTLNg * SLQN.Value12,
                        SumGV = valueDGTLNg * sumSLQN,
                        desDG = "Tỷ lệ hao hụt tra nạp ngầm * tỷ giá *( đơn giá Mops +đơn giá pre +đơn giá thuế)",
                        desMonth = "Tỷ lệ hao hụt tra nạp ngầm * tỷ giá *( đơn giá Mops +đơn giá pre +đơn giá thuế)* dữ liệu kế hoạch Sản lượng theo tháng lấy  tra nạp ngầm nội địa và tra nạp ngầm tân sơn nhất",
                        destotal = "Tỷ lệ hao hụt tra nạp ngầm * tỷ giá *( đơn giá Mops +đơn giá pre +đơn giá thuế)*Tổng dữ liệu kế hoạch Sản lượng theo tháng lấy  tra nạp ngầm nội địa và tra nạp ngầm tân sơn nhất",
                        Order = 8,
                        Level = 1,

                        Parent = 6.ToString()
                    };

                    var CPHH = new KeHoachGiaVonTheoThang
                    {
                        Code = "CPHH",
                        Name = "Chi phí hao hụt",
                        DVT = "VND/tấn",
                        Value1 = CPXe.Value1 + CPQN.Value1,
                        Value2 = CPXe.Value2 + CPQN.Value2 ,
                        Value3 = CPXe.Value3 + CPQN.Value3 ,
                        Value4 = CPXe.Value4 + CPQN.Value4 ,
                        Value5 = CPXe.Value5 + CPQN.Value5 ,
                        Value6 = CPXe.Value6 + CPQN.Value6 ,
                        Value7 = CPXe.Value7 + CPQN.Value7 ,
                        Value8 = CPXe.Value8 + CPQN.Value8 ,
                        Value9 = CPXe.Value9 + CPQN.Value9 ,
                        Value10 =CPXe.Value10 + CPQN.Value10 ,
                        Value11 =CPXe.Value11+ CPQN.Value11,
                        Value12 = CPXe.Value12 + CPQN.Value12 ,
                        SumGV = CPXe.SumGV + CPQN.SumGV ,
                        Order = 6,
                        Level = 0,
                        desMonth= "Chi phí HH qua xe+Chi phí HH qua ngầm",
                        destotal= "Tổng chi phí HH qua xe+Tổng chi phí HH qua ngầm",
                        Parent = orderTC.ToString()
                    };
                    data.KeHoachGiaVonTheoThang.Add(CPHH);
                    data.KeHoachGiaVonTheoThang.Add(CPQN);
                    data.KeHoachGiaVonTheoThang.Add(SLQX);
                    data.KeHoachGiaVonTheoThang.Add(SLQN);

                    //Tính phí FHS
                    var fhs = new KeHoachGiaVonTheoThang();
                    foreach (var hhk in lstHangHangKhong)
                    {
                        var FHS_NBA = lstSharedData.FirstOrDefault(x => x.CODE == "FHS-NBA-" + hhk.GROUP_ITEM)?.VALUE;
                        var FHS_TNS = lstSharedData.FirstOrDefault(x => x.CODE == "FHS-TNS-" + hhk.GROUP_ITEM)?.VALUE;
                        // lấy phí fsh theo hãng hàng không
                        var valueSLNBA1 = dataInHeader.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF").Sum(x => x.VALUE_JAN) ?? 0;
                        var valueSLNBA2 = dataInHeader.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF").Sum(x => x.VALUE_FEB) ?? 0;
                        var valueSLNBA3 = dataInHeader.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF").Sum(x => x.VALUE_MAR) ?? 0;
                        var valueSLNBA4 = dataInHeader.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF").Sum(x => x.VALUE_APR) ?? 0;
                        var valueSLNBA5 = dataInHeader.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF").Sum(x => x.VALUE_MAY) ?? 0;
                        var valueSLNBA6 = dataInHeader.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF").Sum(x => x.VALUE_JUN) ?? 0;
                        var valueSLNBA7 = dataInHeader.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF").Sum(x => x.VALUE_JUL) ?? 0;
                        var valueSLNBA8 = dataInHeader.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF").Sum(x => x.VALUE_AUG) ?? 0;
                        var valueSLNBA9 = dataInHeader.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF").Sum(x => x.VALUE_SEP) ?? 0;
                        var valueSLNBA10 = dataInHeader.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF").Sum(x => x.VALUE_OCT) ?? 0;
                        var valueSLNBA11 = dataInHeader.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF").Sum(x => x.VALUE_NOV) ?? 0;
                        var valueSLNBA12 = dataInHeader.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF").Sum(x => x.VALUE_SEP) ?? 0;
                        var valueSLNBASum= dataInHeader.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.SAN_BAY_CODE == "NAF").Sum(x => x.VALUE_SUM_YEAR) ?? 0;



                        var valueSLTNS1 = dataInHeader.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP").Sum(x => x.VALUE_JAN) ?? 0;
                        var valueSLTNS2 = dataInHeader.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP").Sum(x => x.VALUE_FEB) ?? 0;
                        var valueSLTNS3 = dataInHeader.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP").Sum(x => x.VALUE_MAR) ?? 0;
                        var valueSLTNS4 = dataInHeader.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP").Sum(x => x.VALUE_APR) ?? 0;
                        var valueSLTNS5 = dataInHeader.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP").Sum(x => x.VALUE_MAY) ?? 0;
                        var valueSLTNS6 = dataInHeader.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP").Sum(x => x.VALUE_JUN) ?? 0;
                        var valueSLTNS7 = dataInHeader.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP").Sum(x => x.VALUE_JUL) ?? 0;
                        var valueSLTNS8 = dataInHeader.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP").Sum(x => x.VALUE_AUG) ?? 0;
                        var valueSLTNS9 = dataInHeader.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP").Sum(x => x.VALUE_SEP) ?? 0;
                        var valueSLTNS10 = dataInHeader.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP").Sum(x => x.VALUE_OCT) ?? 0;
                        var valueSLTNS11 = dataInHeader.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP").Sum(x => x.VALUE_NOV) ?? 0;
                        var valueSLTNS12 = dataInHeader.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP").Sum(x => x.VALUE_SEP) ?? 0;
                        var valueSLTNSSum = dataInHeader.Where(x => x.SanLuongProfitCenter.HangHangKhong.GROUP_ITEM == hhk.GROUP_ITEM && x.KHOAN_MUC_SAN_LUONG_CODE == "10020" && x.SanLuongProfitCenter.SAN_BAY_CODE == "TAP").Sum(x => x.VALUE_SUM_YEAR) ?? 0;



                        fhs.Value1 =fhs.Value1 + Convert.ToDecimal(FHS_NBA * value11 * valueSLNBA1  +  FHS_TNS * value11 * valueSLTNS1  );
                        fhs.Value2 =fhs.Value2 + Convert.ToDecimal(FHS_NBA * value11 * valueSLNBA2  +  FHS_TNS * value11 * valueSLTNS2  );
                        fhs.Value3 =fhs.Value3 + Convert.ToDecimal(FHS_NBA * value11 * valueSLNBA3  +  FHS_TNS * value11 * valueSLTNS3  );
                        fhs.Value4 =fhs.Value4 + Convert.ToDecimal(FHS_NBA * value11 * valueSLNBA4  +  FHS_TNS * value11 * valueSLTNS4  );
                        fhs.Value5 =fhs.Value5 + Convert.ToDecimal(FHS_NBA * value11 * valueSLNBA5  +  FHS_TNS * value11 * valueSLTNS5  );
                        fhs.Value6 =fhs.Value6 + Convert.ToDecimal(FHS_NBA * value11 * valueSLNBA6  +  FHS_TNS * value11 * valueSLTNS6  );
                        fhs.Value7 =fhs.Value7 + Convert.ToDecimal(FHS_NBA * value11 * valueSLNBA7  +  FHS_TNS * value11 * valueSLTNS7  );
                        fhs.Value8 =fhs.Value8 + Convert.ToDecimal(FHS_NBA * value11 * valueSLNBA8  + FHS_TNS * value11 * valueSLTNS8);
                        fhs.Value9 =fhs.Value9 + Convert.ToDecimal(FHS_NBA * value11 * valueSLNBA9 +  FHS_TNS * value11 * valueSLTNS9 );
                        fhs.Value10 =fhs.Value10 + Convert.ToDecimal(FHS_NBA * value11 * valueSLNBA10  +  FHS_TNS * value11 * valueSLTNS10  );
                        fhs.Value11 =fhs.Value11 + Convert.ToDecimal(FHS_NBA * value11 * valueSLNBA11  +  FHS_TNS * value11 * valueSLTNS11  );
                        fhs.Value12 =fhs.Value12 + Convert.ToDecimal(FHS_NBA * value11 * valueSLNBA12  +  FHS_TNS * value11 * valueSLTNS12  );
                        fhs.SumGV = fhs.SumGV + Convert.ToDecimal(FHS_NBA * value11 * valueSLNBASum + FHS_TNS * value11 * valueSLTNSSum);
                        fhs.desMonth = "Phí FHS-NBA theo hãng hàng không * tỷ giá * dữ liệu sản lượng quốc tế tra nạp ngầm Nội Bài + phí FSH_TNS theo hãng hàng không* tỷ giá* dữ liệu sản lượng quốc tế tra nạp ngầm Tân sơn nhất";
                        fhs.destotal = "Tổng Phí FHS-NBA theo hãng hàng không * tỷ giá * dữ liệu sản lượng quốc tế tra nạp ngầm Nội Bài + Tổng phí FSH_TNS theo hãng hàng không* tỷ giá* dữ liệu sản lượng quốc tế tra nạp ngầm Tân sơn nhất";
                    }
                    fhs.Order = 11;
                    fhs.Parent = orderTC.ToString();
                    fhs.Name = "Chi phí FHS";
                    fhs.DVT = "VND/tấn";
                    fhs.Level = 0;
                    data.KeHoachGiaVonTheoThang.Add(fhs);

                    // Tính toorng

                    var Tong = new KeHoachGiaVonTheoThang
                    {
                        Code = "Tong",
                        Order = 0,
                        IsBold = true,
                        Name = "Tổng cộng",
                        Value1 = Mops.Value1 + Pre.Value1 + thue.Value1 + dvmn.Value1 + CPHH.Value1 + fhs.Value1,
                        Value2 = Mops.Value2 + Pre.Value2 + thue.Value2 + dvmn.Value2 + CPHH.Value2 + fhs.Value2,
                        Value3 = Mops.Value3 + Pre.Value3 + thue.Value3 + dvmn.Value3 + CPHH.Value3 + fhs.Value3,
                        Value4 = Mops.Value4 + Pre.Value4 + thue.Value4 + dvmn.Value4 + CPHH.Value4 + fhs.Value4,
                        Value5 = Mops.Value5 + Pre.Value5 + thue.Value5 + dvmn.Value5 + CPHH.Value5 + fhs.Value5,
                        Value6 = Mops.Value6 + Pre.Value6 + thue.Value6 + dvmn.Value6 + CPHH.Value6 + fhs.Value6,
                        Value7 = Mops.Value7 + Pre.Value7 + thue.Value7 + dvmn.Value7 + CPHH.Value7 + fhs.Value7,
                        Value8 = Mops.Value8 + Pre.Value8 + thue.Value8 + dvmn.Value8 + CPHH.Value8 + fhs.Value8,
                        Value9 = Mops.Value9 + Pre.Value9 + thue.Value9 + dvmn.Value9 + CPHH.Value9 + fhs.Value9,
                        Value10 = Mops.Value10 + Pre.Value10 + thue.Value10 + dvmn.Value10 + CPHH.Value10 + fhs.Value10,
                        Value11 = Mops.Value11 + Pre.Value11 + thue.Value11 + dvmn.Value11 + CPHH.Value11 + fhs.Value11,
                        Value12 = Mops.Value12 + Pre.Value12 + thue.Value12 + dvmn.Value12 + CPHH.Value12 + fhs.Value12,
                        SumGV = Mops.SumGV + Pre.SumGV + thue.SumGV + dvmn.SumGV + CPHH.SumGV + fhs.SumGV,
                        Level = 0,
                        desMonth = "Tổng cộng theo tháng ",
                        destotal= "Tổng cộng",
                    };
                    data.KeHoachGiaVonTheoThang.Add(Tong);

                }



                return data;
            }
            catch (Exception ex)
            {
                this.State = false;
                this.Exception = ex;
                UnitOfWork.Rollback();
                return new DataCenterModel();
            }
        }
        public DataCenterModel GetDataKeHoachTaiChinh(int year)
        {
            try
            {
                var data = new DataCenterModel();
                var lstElement = UnitOfWork.Repository<ElementRepo>().Queryable().Where(x => x.SCREEN == "KE_HOACH_TAI_CHINH" || x.SCREEN == "KE_HOACH_TAI_CHINH_2").ToList();
                foreach (var item in lstElement.Where(x => x.ELEMENT_TYPE == "S").OrderBy(x => x.PRIORITY))
                {
                    var itemData = new KeHoachTaiChinhData
                    {
                        ElementCode = item.CODE,
                        ElementName = item.NAME,
                        UnitCode = item?.Unit?.TEXT,
                        Order = item.VALUE,
                        Screen = item.SCREEN,
                        Value = item.QUERY == "" || item.QUERY == null ? 0 : Convert.ToDecimal(UnitOfWork.GetSession().CreateSQLQuery($"{item.QUERY.Replace("[YEAR]", year.ToString())}").List()[0])

                    };
                    data.KeHoachTaiChinhData.Add(itemData);
                }
                foreach (var item in lstElement.Where(x => x.ELEMENT_TYPE == "U").OrderBy(x => x.PRIORITY))
                {
                    var valueFormula = AnalysisFormulaTaiChinh(item.FORMULA, data.KeHoachTaiChinhData);
                    var itemData = new KeHoachTaiChinhData
                    {
                        ElementCode = item.CODE,
                        ElementName = item.NAME,
                        UnitCode = item?.Unit?.TEXT,
                        Order = item.VALUE,
                        Screen = item.SCREEN,
                        Value = valueFormula == -1 ? 0 : valueFormula
                    };
                    data.KeHoachTaiChinhData.Add(itemData);
                }
                return data;
            }
            catch (Exception ex)
            {
                this.State = false;
                this.Exception = ex;
                UnitOfWork.Rollback();
                return new DataCenterModel();
            }
        }

        public void DowloadExcel(ref MemoryStream outFileStream, DataCenterModel data, string year, string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            IWorkbook workbook;
            workbook = new XSSFWorkbook(fs);
            workbook.SetSheetName(0, ModulType.GetTextSheetName("Kế-hoạch-tài-chính"));
            fs.Close();

            ISheet sheetYear = workbook.GetSheetAt(0);
            var NUM_CELL = 3;
            var module = "KeHoachTaiChinh";
            List<KeHoachTaiChinhData> dataDetails = data.KeHoachTaiChinhData;
            ExcelHelperBP.InsertHeaderKeHoachTaiChinh(ref workbook, year, module, ref sheetYear, NUM_CELL);
            ExcelHelperBP.insertBodyKeHoachTaiChinh(ref workbook, dataDetails, module, ref sheetYear, NUM_CELL);
            workbook.SetSheetName(1, ModulType.GetTextSheetName("Kế-hoạch-tài-chính-2"));
            ISheet sheetKHTC2 = workbook.GetSheetAt(1);
            var module2 = "KeHoachTaiChinh2";
            ExcelHelperBP.InsertHeaderKeHoachTaiChinh(ref workbook, year, module, ref sheetKHTC2, NUM_CELL);

            ExcelHelperBP.insertBodyKeHoachTaiChinh(ref workbook, dataDetails, module2, ref sheetKHTC2, NUM_CELL);
            workbook.Write(outFileStream);
        }

        public void DowloadExcelKHGV(ref MemoryStream outFileStream, DataCenterModel data, string year, string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            IWorkbook workbook;
            workbook = new XSSFWorkbook(fs);
            fs.Close();

            ISheet sheetYear = workbook.GetSheetAt(0);
            var NUM_CELL = 20;
            var module = "KeHoachGiaVon";
            ExcelHelperBP.InsertHeaderKeHoachGiaVon(ref workbook, year, module, ref sheetYear, NUM_CELL);
            ExcelHelperBP.insertBodyKeHoachGiaVon(ref workbook, data, module, ref sheetYear, NUM_CELL);
            ISheet sheetKHTC2 = workbook.GetSheetAt(1);
            var NUM_CELL2 = 21;
            var module2 = "KeHoachGiaVon2";
            ExcelHelperBP.InsertHeaderKeHoachGiaVon(ref workbook, year, module, ref sheetKHTC2, NUM_CELL2);
            ExcelHelperBP.insertBodyKeHoachGiaVon(ref workbook, data, module2, ref sheetKHTC2, NUM_CELL2);

            ISheet sheetKHTC3 = workbook.GetSheetAt(2);
            var NUM_CELL3 = 16;
            var module3 = "KeHoachGiaVontheothang";
            ExcelHelperBP.InsertHeaderKeHoachGiaVon(ref workbook, year, module, ref sheetKHTC3, NUM_CELL3);
            ExcelHelperBP.insertBodyKeHoachGiaVon(ref workbook, data, module3, ref sheetKHTC3, NUM_CELL3);
            workbook.Write(outFileStream);
        }
    }
}
