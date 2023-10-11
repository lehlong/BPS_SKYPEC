using SMO.Core.Entities.MD;
using SMO.Repository.Implement.MD;
using SMO.Service.Common;

using System;
using System.Collections.Generic;
using System.Linq;

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

        public void UpdateTree(List<NodeKhoanMucChung> lstNode)
        {
            try
            {
                var strSql = "";
                var order = 0;
                UnitOfWork.BeginTransaction();

                foreach (var item in lstNode)
                {
                    var isParent = lstNode.Count(x => x.pId == item.id) > 0;
                    if (!string.IsNullOrWhiteSpace(item.pId))
                    {
                        strSql = "UPDATE T_MD_KHOAN_MUC_CHUNG SET C_ORDER = :C_ORDER, PARENT_CODE = :PARENT_CODE, IS_GROUP = :IS_GROUP WHERE CODE = :CODE";
                        UnitOfWork.GetSession().CreateSQLQuery(strSql)
                            .SetParameter("C_ORDER", order)
                            .SetParameter("PARENT_CODE", item.pId)
                            .SetParameter("CODE", item.id)
                            .SetParameter("IS_GROUP", isParent ? "Y" : "N")
                            .ExecuteUpdate();
                    }
                    else
                    {
                        strSql = "UPDATE T_MD_KHOAN_MUC_CHUNG SET C_ORDER = :C_ORDER, PARENT_CODE = '', IS_GROUP = :IS_GROUP WHERE CODE = :CODE";
                        UnitOfWork.GetSession().CreateSQLQuery(strSql)
                            .SetParameter("C_ORDER", order)
                            .SetParameter("CODE", item.id)
                            .SetParameter("IS_GROUP", isParent ? "Y" : "N")
                            .ExecuteUpdate();
                    }
                    order++;
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
