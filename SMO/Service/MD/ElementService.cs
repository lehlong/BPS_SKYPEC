using SMO.Core.Entities;
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

        public decimal? AnalysisFormula(string formula)
        {
            try
            {
                var analysisFormula = "";
                var stringFormula = Regex.Replace(formula, @"\s", "");
                string spacedFormula = AddSpacesToFormula("x=" + formula);

                string[] stringArray = spacedFormula.Replace("x = ","").Split(' ');
                foreach (var str in stringArray)
                {
                    var element = UnitOfWork.Repository<ElementRepo>().Queryable().FirstOrDefault(x => x.CODE == str);
                    if (element == null)
                    {
                        analysisFormula += str;
                    }
                    else
                    {
                        analysisFormula += element.VALUE.ToString();
                    }
                }
                DataTable table = new DataTable();
                object result = table.Compute(analysisFormula, "");
                return Convert.ToDecimal(result);
            }
            catch(Exception ex)
            {
                return -1;
            }
            
        }

        public void CalculateValueElement()
        {
            try
            {
                UnitOfWork.BeginTransaction();

                var lstElementTypeSystem = UnitOfWork.Repository<ElementRepo>().Queryable().Where(x => x.ELEMENT_TYPE == "S").OrderBy(x => x.PRIORITY).ToList();
                foreach (var element in lstElementTypeSystem)
                {
                    try
                    {
                        var value = UnitOfWork.GetSession().CreateSQLQuery($"{element.QUERY}").List()[0];
                        element.VALUE = Convert.ToDecimal(value);
                        UnitOfWork.Repository<ElementRepo>().Update(element);
                    }
                    catch
                    {
                        UnitOfWork.Rollback();
                        this.State = false;
                        this.ErrorMessage = $"Câu lệnh SQL của element {element.CODE} sai! Vui lòng kiểm tra lại!";
                        return;
                    }
                    
                }
                //Analysis formula
                var lstElementTypeUser = UnitOfWork.Repository<ElementRepo>().Queryable().Where(x => x.ELEMENT_TYPE == "U").OrderBy(x => x.PRIORITY).ToList();
                foreach (var element in lstElementTypeUser)
                {
                    var value = AnalysisFormula(element.FORMULA);
                    if(value == -1)
                    {
                        UnitOfWork.Rollback();
                        this.State = false;
                        this.ErrorMessage = $"Sai công thức tại element {element.CODE}";
                        return;
                    }
                    element.VALUE = Convert.ToDecimal(value);
                    UnitOfWork.Repository<ElementRepo>().Update(element);
                }

                UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                State = false;
                Exception = ex;
            }
        }
    }
}
