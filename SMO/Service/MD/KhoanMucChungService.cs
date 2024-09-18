using Hangfire.Annotations;
using NHibernate.Criterion;
using SMO.Core.Entities;
using SMO.Core.Entities.MD;
using SMO.Repository.Implement.MD;
using SMO.Service.Common;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ZXing;

namespace SMO.Service.MD
{
    public class KhoanMucChungService : GenericCenterService<T_MD_KHOAN_MUC_CHUNG, KhoanMucChungRepo>
    {
        public List<NodeKhoanMucChung> BuildTree()
        {
            var lstNode = new List<NodeKhoanMucChung>();
            GetAll();
            foreach (var item in ObjList.OrderBy(x => x.C_ORDER))
            {
                var node = new NodeKhoanMucChung()
                {
                    id = item.CODE,
                    pId = item.PARENT_CODE,
                    name = "<span class='spMaOnTree'>" + item.CODE + "</span>" + item.NAME,
                    //icon = "/Content/zTreeStyle/img/diy/donvi.gif"
                };
                lstNode.Add(node);
            }
            return lstNode;
        }

        public void RunTasK(List<NodeKhoanMucChung> lstNode, List<NodeKhoanMucChung> lstNodeTask,int num)
        {

            //var order = num;
            // foreach (var item in lstNodeTask)
            // {
            //     var isParent = lstNodeTask.Count(x => x.pId == item.id) > 0;
            //     if (!string.IsNullOrWhiteSpace(item.pId))
            //     {
            //         var ItemKMCP = new T_MD_KHOAN_MUC_CHUNG()
            //         {
            //             CODE = item.id,
            //             C_ORDER = order,
            //             PARENT_CODE = item.pId,
            //             IS_GROUP = isParent?true:false,
            //         };
            //         UnitOfWork.Repository<KhoanMucChungRepo>().Update(ItemKMCP);
            //     }
            //     else
            //     {

            //         var ItemKMCP = new T_MD_KHOAN_MUC_CHUNG()
            //         {
            //             CODE = item.id,
            //             C_ORDER = order,
            //             PARENT_CODE = "",
            //             IS_GROUP = isParent ? true : false,
            //         };
            //         UnitOfWork.Repository<KhoanMucChungRepo>().Update(ItemKMCP);

            //     }
            //     order++;
            // }

            foreach (var i in lstNodeTask)
            {
                if (i.id == null || i.id == "" || string.IsNullOrEmpty(i.id) || i.pId == null || i.pId == "" || string.IsNullOrEmpty(i.pId))
                {
                    continue;
                }
                else
                {
                    var item = UnitOfWork.Repository<KhoanMucChungRepo>().Get(i.id);
                    item.PARENT_CODE = i.pId;
                    item.IS_GROUP = lstNode.Where(x => x.pId == i.id).Count() > 0 ? true : false;
                    item.C_ORDER =num;
                    UnitOfWork.Repository<KhoanMucChungRepo>().Update(item);
                   num++;
                }

            }
        }
        public void UpdateTree(List<NodeKhoanMucChung> lstNode)
        {
            try
            {
                //UnitOfWork.BeginTransaction();
                //var flag = lstNode.Count() / 4;
                //var lstTask1 = lstNode.Skip(0).Take(flag).ToList();
                //var lstTask2 = lstNode.Skip(flag).Take(flag).ToList();
                //var lstTask3 = lstNode.Skip(flag * 2).Take(flag).ToList();
                //var lstTask4 = lstNode.Skip(flag * 3).ToList();

                //Task Task1 = Task.Run(() =>
                //{
                //    RunTasK(lstNode, lstTask1, 0);
                //});
                //Task Task2 = Task.Run(() =>
                //{
                //    RunTasK(lstNode, lstTask2, flag);
                //});
                //Task Task3 = Task.Run(() =>
                //{
                //    RunTasK(lstNode, lstTask3, flag * 2);
                //});
                //Task Task4 = Task.Run(() =>
                //{
                //    RunTasK(lstNode, lstTask4, flag * 3);
                //});

                //UnitOfWork.Commit();
                SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["SMO_MSSQL_Connection"].ConnectionString);
                objConn.Open();

                string tableName = "T_MD_KHOAN_MUC_CHUNG";
                SqlDataAdapter daAdapter = new SqlDataAdapter("SELECT * FROM " + tableName, objConn);
                daAdapter.UpdateBatchSize = 500;
                DataSet dataSet = new DataSet(tableName);

                daAdapter.FillSchema(dataSet, SchemaType.Source, tableName);
                daAdapter.Fill(dataSet, tableName);

                DataTable dataTable;
                dataTable = dataSet.Tables[tableName];
                dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["CODE"] };
                var order = 0;
                foreach (var item in lstNode)
                {
                    var isParent = lstNode.Count(x => x.pId == item.id) > 0;
                    var row = dataTable.Rows.Find(item.id);
                    if (!string.IsNullOrWhiteSpace(item.pId))
                    {
                        row.BeginEdit();
                        row["CODE"] = item.id;
                        row["C_ORDER"] = order;
                        row["PARENT_CODE"] = item.pId;
                        row["IS_GROUP"] = isParent ? "Y" : "N";
                        row.EndEdit();
                    }
                    else
                    {
                        row.BeginEdit();
                        row["CODE"] = item.id;
                        row["C_ORDER"] = order;
                        row["PARENT_CODE"] = "";
                        row["IS_GROUP"] = isParent ? "Y" : "N";
                        row.EndEdit();
                    }
                    order++;
                }
                SqlCommandBuilder objCommandBuilder = new SqlCommandBuilder(daAdapter);
                daAdapter.Update(dataSet, tableName);
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                State = false;
                Exception = ex;
            }
        }

        public override void Delete(string strLstSelected)
        {
            try
            {
                var lstId = strLstSelected.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
                foreach (var item in lstId)
                {
                    UnitOfWork.BeginTransaction();
                    if (!CheckExist(x => x.PARENT_CODE == item))
                    {
                        CurrentRepository.Delete(item);
                    }
                    else
                    {
                        State = false;
                        ErrorMessage = "Khoản mục này đang là cha của khoản mục khác.";
                        UnitOfWork.Rollback();
                        return;
                    }
                    UnitOfWork.Commit();
                }
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
