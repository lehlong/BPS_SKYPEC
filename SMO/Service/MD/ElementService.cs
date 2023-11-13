using NHibernate.Criterion;
using SMO.Core.Entities;
using SMO.Models;
using SMO.Repository.Implement.MD;

using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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
                DataTable table = new DataTable();
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
                var lstElementTypeSystem = UnitOfWork.Repository<ElementRepo>().Queryable().Where(x => x.ELEMENT_TYPE == "S").OrderBy(x => x.PRIORITY).ToList();
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
                var lstElementTypeUser = UnitOfWork.Repository<ElementRepo>().Queryable().Where(x => x.ELEMENT_TYPE == "U").OrderBy(x => x.PRIORITY).ToList();
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

        public DataCenterModel GetDataKeHoachGiaThanh(int year)
        {
            try
            {
                var data = new DataCenterModel();
                var lstWarehouse = UnitOfWork.Repository<WarehouseRepo>().GetAll().ToList();
                var lstDeliveryConditions = UnitOfWork.Repository<DeliveryConditionsRepo>().GetAll().ToList();
                foreach (var warehouse in lstWarehouse)
                {
                    foreach (var deliveryCondition in lstDeliveryConditions)
                    {
                        var dataCalculate = CalculateValueElement(year, warehouse.CODE, deliveryCondition.CODE);
                        dataCalculate.Warehouse = warehouse.TEXT;
                        dataCalculate.DeliveryConditions = deliveryCondition.TEXT;
                        data.KeHoachGiaThanhData.Add(dataCalculate);
                    }
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
