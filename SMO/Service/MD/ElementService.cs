using iTextSharp.text;
using Microsoft.Office.Interop.Excel;
using NHibernate.Criterion;
using SMO.Core.Entities;
using SMO.Models;
using SMO.Repository.Implement.MD;

using System;
using System.Collections.Generic;
using System.Data;
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
                return -1;
            }
        }

        public KeHoachGiaThanhData CalculateValueElement(int year, string warehouseCode, string deliveryConditionsCode)
        {
            try
            {
                var data = new KeHoachGiaThanhData();
                var lstElementTypeSystem = UnitOfWork.Repository<ElementRepo>().Queryable().Where(x => x.ELEMENT_TYPE == "S" && x.SCREEN == "GIA_THANH").OrderBy(x => x.PRIORITY).ToList();
                foreach (var element in lstElementTypeSystem)
                {
                    try
                    {
                        var value = UnitOfWork.GetSession().CreateSQLQuery($"{element.QUERY.Replace("[KHO]", warehouseCode).Replace("[DKGH]", deliveryConditionsCode).Replace("[YEAR]", year.ToString())}").List()[0];
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

                //Pre kho 1
                foreach (var warehouse in lstWarehouse)
                {
                    foreach (var deliveryCondition in lstDeliveryConditions)
                    {
                        var dataCalculate = CalculateValueElement(year, warehouse.CODE, deliveryCondition.CODE);
                        dataCalculate.WarehouseCode = warehouse.CODE;
                        dataCalculate.Warehouse = warehouse.TEXT;
                        dataCalculate.DeliveryConditionsCode = deliveryCondition.CODE;
                        dataCalculate.DeliveryConditions = deliveryCondition.TEXT;
                        data.KeHoachGiaThanhData.Add(dataCalculate);
                    }
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
                var lstRoute = UnitOfWork.Repository<RouteRepo>().GetAll();
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
                        Premium = data.PreTrungBinhKhoData.FirstOrDefault(x => x.WarehouseCode.Trim() == route.FIRST_POINT.Trim()) == null ?0 : data.PreTrungBinhKhoData.FirstOrDefault(x => x.WarehouseCode.Trim() == route.FIRST_POINT.Trim()).PreDN1
                    };
                    data.PreData.Add(item); 
                }

                var lstSanBay = UnitOfWork.Repository<SanBayRepo>().Queryable().Where(x => x.OTHER_PM_CODE != null && x.OTHER_PM_CODE != "").ToList();

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
                        Value = item.QUERY == "" || item.QUERY == null ? 0 : Convert.ToDecimal(UnitOfWork.GetSession().CreateSQLQuery($"{item.QUERY.Replace("[YEAR]",year.ToString())}").List()[0])
                        
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
                UnitOfWork.Rollback();
                return new DataCenterModel();
            }
        }
    }
}
