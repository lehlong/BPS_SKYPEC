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
                string spacedFormula = AddSpacesToFormula("x=" + formula);

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

                //Pre kho 1
                foreach (var item in puchaseData)
                {
                    var dataCalculate = CalculateValueElement(year, item.WAREHOUSE_CODE, item.DELIVERY_CONDITIONS_CODE, item.ID_KHNH);
                    dataCalculate.WarehouseCode = item.WAREHOUSE_CODE;
                    dataCalculate.Warehouse = item.WAREHOUSE_CODE;
                    dataCalculate.DeliveryConditionsCode = item.DELIVERY_CONDITIONS_CODE;
                    dataCalculate.DeliveryConditions = item.DELIVERY_CONDITIONS_CODE;
                    data.KeHoachGiaThanhData.Add(dataCalculate);
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
                    dataCalculate.AreaCode = item.AREA_ID;
                    data.KeHoachGiaThanhData.Add(dataCalculate);
                }

                var value2 = lstSharedData.FirstOrDefault(x => x.CODE == "1").VALUE * lstSharedData.FirstOrDefault(x => x.CODE == "3").VALUE;
                var value3 = data.KeHoachGiaThanhData.Sum(x => x.U0008 * x.S0002) / data.KeHoachGiaThanhData.Sum(x => x.S0002);
                var value4 = value2 + value3;
                var value5 = data.KeHoachGiaThanhData.Sum(x => x.U0004 * x.S0002) / data.KeHoachGiaThanhData.Sum(x => x.S0002);
                var value6 = lstSharedData.FirstOrDefault(x => x.CODE == "22").VALUE;
                var value7 = (value2 + value3 + value5) * lstSharedData.FirstOrDefault(x => x.CODE == "19").VALUE + (value2 + value3 + value5) * lstSharedData.FirstOrDefault(x => x.CODE == "20").VALUE;
                var value8 = (value2 + value3 + value5) * lstSharedData.FirstOrDefault(x => x.CODE == "19").VALUE;
                var value9 = (value2 + value3 + value5) * lstSharedData.FirstOrDefault(x => x.CODE == "20").VALUE;
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

                    foreach (var hhk in lstHangHangKhong)
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
                            Value7 = value7,
                            Value8 = value8,
                            Value9 = value9,
                            Value11 = value11,
                            Order = order + 2,
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
                            Value7 = value7,
                            Value8 = value8,
                            Value9 = value9,
                            Value10 = FHS_NBA ?? 0,
                            Value11 = value11,
                            Order = order + 4,
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
                            Value7 = value7,
                            Value8 = value8,
                            Value9 = value9,
                            Value10 = FHS_TNS ?? 0,
                            Value11 = value11,
                            Order = order + 5,
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
                            Value7 = value7,
                            Value8 = value8,
                            Value9 = value9,
                            Value11 = value11,
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
                            Value7 = value7,
                            Value8 = value8,
                            Value9 = value9,
                            Value10 = FHS_NBA ?? 0,
                            Value11 = value11,
                            Order = order + 9,
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
                            Value7 = value7,
                            Value8 = value8,
                            Value9 = value9,
                            Value10 = FHS_TNS ?? 0,
                            Value11 = value11,
                            Order = order + 10,
                            Level = 3
                        });

                        order += 11;
                    }

                    foreach (var item in data.KeHoachGiaVonData)
                    {
                        var value12 = (item.Value2 + item.Value3 + item.Value5 + item.Value7 + item.Value10) * item.Value11 + item.Value6;
                        item.Value12 = value12;
                        item.Value14 = item.Value1 * item.Value11 * item.Value2;
                        item.Value15 = item.Value1 * item.Value11 * item.Value3;
                        item.Value16 = item.Value1 * item.Value11 * item.Value5;
                        item.Value17 = item.Value1 * item.Value6;
                        item.Value18 = item.Value1 * item.Value11 * item.Value8;
                        item.Value19 = item.Value1 * item.Value11 * item.Value9;
                        item.Value20 = item.Value1 * item.Value11 * item.Value10;
                        item.Value13 = item.Value14 + item.Value15 + item.Value16 + item.Value17 + item.Value18 + item.Value19 + item.Value20;
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
    }
}
