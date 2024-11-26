using SMO.Core.Entities;
using SMO.Repository.Implement.AD;
using SMO.Service.MD;

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web.Configuration;
using System.Web;
using ExcelDataReader;
using System.Data;
using SMO.Repository.Implement.BP;
using NHibernate.Linq;
using SMO.Models;
using SMO.Repository.Implement.MD;
using SMO.Core.Entities.BP;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SMO.Service.BP;

namespace SMO.Service.AD
{
    public class UserService : GenericService<T_AD_USER, UserRepo>
    {
        public int YEAR { get; set; }
        public T_AD_USER_GROUP ObjUserGroup { get; set; }
        public List<T_AD_USER_GROUP> ObjListUserGroup { get; set; }
        public T_AD_ROLE ObjRole { get; set; }
        public List<T_AD_ROLE> ObjListRole { get; set; }
        public UserService() : base()
        {
            ObjUserGroup = new T_AD_USER_GROUP();
            ObjListUserGroup = new List<T_AD_USER_GROUP>();
            ObjRole = new T_AD_ROLE();
            ObjListRole = new List<T_AD_ROLE>();
        }

        public void SearchUserGroupForAdd()
        {
            this.Get(this.ObjDetail.USER_NAME);
            var lstUserGroupOfUser = this.ObjDetail.ListUserUserGroup.Select(x => x.USER_GROUP_CODE).ToList();
            var query = UnitOfWork.Repository<UserGroupRepo>().Queryable();
            query = query.Where(x => !lstUserGroupOfUser.Contains(x.CODE));
            if (!string.IsNullOrWhiteSpace(ObjUserGroup.CODE))
            {
                query = query.Where(x => x.CODE.ToLower().Contains(ObjUserGroup.CODE.ToLower()) || x.NAME.ToLower().Contains(ObjUserGroup.CODE.ToLower()));
            }
            this.ObjListUserGroup = query.ToList();
        }


        public void AddUserGroupToUser(string lstUserGroup, string userName)
        {
            try
            {
                UnitOfWork.BeginTransaction();

                foreach (var userGroupCode in lstUserGroup.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList())
                {
                    var item = new T_AD_USER_USER_GROUP()
                    {
                        USER_NAME = userName,
                        USER_GROUP_CODE = userGroupCode
                    };

                    if (ProfileUtilities.User != null)
                    {
                        item.CREATE_BY = ProfileUtilities.User.USER_NAME;
                        item.CREATE_DATE = this.CurrentRepository.GetDateDatabase();
                    }
                    UnitOfWork.Repository<UserUserGroupRepo>().Create(item);
                }
                UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                this.State = false;
                this.Exception = ex;
            }
        }

        public void CreateUserOrganzie()
        {
            try
            {
                if (!this.CheckExist(x => x.USER_NAME == this.ObjDetail.USER_NAME))
                {
                    UnitOfWork.BeginTransaction();
                    this.ObjDetail.PASSWORD = UtilsCore.EncryptStringMD5(this.ObjDetail.USER_NAME + "@123");
                    this.ObjDetail.ACTIVE = true;
                    this.ObjDetail.CREATE_BY = ProfileUtilities.User.USER_NAME;
                    this.ObjDetail.CREATE_DATE = DateTime.Now;
                    this.CurrentRepository.Create(this.ObjDetail);
                    UnitOfWork.Commit();
                }
                else
                {
                    UnitOfWork.Rollback();
                    this.State = false;
                    this.MesseageCode = "1104";
                }
            }
            catch (Exception ex)
            {
                this.State = false;
                this.Exception = ex;
            }
        }


        public override void Create()
        {
            try
            {
                if (!this.CheckExist(x => x.USER_NAME == this.ObjDetail.USER_NAME))
                {
                    this.ObjDetail.PASSWORD = UtilsCore.EncryptStringMD5(this.ObjDetail.USER_NAME + "@123");
                    this.ObjDetail.ACTIVE = true;
                    base.Create();
                }
                else
                {
                    this.State = false;
                    this.MesseageCode = "1104";
                }
            }
            catch (Exception ex)
            {
                this.State = false;
                this.Exception = ex;
            }
        }

        public override void Update()
        {
            try
            {
                var user = UnitOfWork.Repository<UserRepo>().Get(this.ObjDetail.USER_NAME);
                UnitOfWork.Repository<UserRepo>().Detach(user);

                UnitOfWork.BeginTransaction();
                this.ObjDetail.PASSWORD = user.PASSWORD;
                this.ObjDetail = this.CurrentRepository.Update(this.ObjDetail);
                this.ObjDetail.UPDATE_BY = ProfileUtilities.User.USER_NAME;
                this.ObjDetail.UPDATE_DATE = DateTime.Now;
                UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                this.State = false;
                this.Exception = ex;
            }
        }

        public void DeleteUserGroupOfUser(string lstUserGroup, string userName)
        {
            try
            {
                UnitOfWork.BeginTransaction();

                foreach (var userGroupCode in lstUserGroup.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList())
                {
                    var item = UnitOfWork.Repository<UserUserGroupRepo>().Queryable().FirstOrDefault(x => x.USER_NAME == userName && x.USER_GROUP_CODE == userGroupCode);
                    if (item != null)
                    {
                        UnitOfWork.Repository<UserUserGroupRepo>().Delete(item);
                    }
                }
                UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                this.State = false;
                this.Exception = ex;
            }
        }

        public List<NodeUser> BuildTreeUser()
        {
            var lstNode = new List<NodeUser>();
            var serviceOrganize = new CostCenterService();
            serviceOrganize.GetAll();
            foreach (var organize in serviceOrganize.ObjList.OrderBy(x => x.C_ORDER))
            {
                var nodeOrganize = new NodeUser()
                {
                    id = organize.CODE,
                    pId = organize.PARENT_CODE,
                    name = organize.NAME,
                    icon = "/Content/zTreeStyle/img/diy/donvi.gif",
                    type = "OG"
                };
                lstNode.Add(nodeOrganize);
                foreach (var user in organize.ListUser.OrderBy(x => x.USER_NAME))
                {
                    var nodeUser = new NodeUser()
                    {
                        id = user.USER_NAME,
                        pId = organize.CODE,
                        name = user.FULL_NAME + "<span class='spMaOnTree'>" + user.USER_NAME + "</span>",
                        userName = user.USER_NAME,
                        fullName = user.FULL_NAME,
                        icon = "/Content/zTreeStyle/img/diy/user.gif",
                        type = "US"
                    };
                    lstNode.Add(nodeUser);
                }
            }
            return lstNode;
        }

        public List<NodeCostCenter> BuildTreeOrg(string userName)
        {
            dynamic param = new ExpandoObject();
            param.IsFetch_ListUserOrg = true;
            this.Get(userName);

            var serviceOrg = new CostCenterService();
            var lstNode = serviceOrg.BuildTree();

            foreach (var node in lstNode)
            {
                if (node.id == this.ObjDetail.ORGANIZE_CODE ||
                    this.ObjDetail.ListUserOrg.Count(x => x.ORG_CODE == node.id) > 0)
                {
                    node.@checked = "true";
                }
                node.chkDisabled = false;
                if (node.id == this.ObjDetail.ORGANIZE_CODE)
                {
                    node.chkDisabled = true;
                }
            }
            return lstNode;
        }

        public List<NodeRight> BuildTreeRight(string userName, string orgCode)
        {
            var lstNode = new List<NodeRight>();

            dynamic param = new ExpandoObject();
            param.IsFetch_ListUserUserGroup = true;
            param.IsFetch_ListUserRight = true;
            param.IsFetch_ListUserRole = true;
            //param.IsFetch_ListUserOrg = true;

            this.Get(userName);

            //Danh sách tất cả các quyền
            var lstAllRight = UnitOfWork.Repository<RightRepo>().GetAll().OrderBy(x => x.C_ORDER);

            //Danh sách role của user theo usergroup
            var lstRole = new List<T_AD_ROLE>();
            foreach (var item1 in this.ObjDetail.ListUserUserGroup)
            {
                foreach (var item2 in item1.UserGroup.ListUserGroupRole)
                {
                    lstRole.Add(item2.Role);
                }
            }
            //Danh sách role riêng của user
            foreach (var item in this.ObjDetail.ListUserRole)
            {
                lstRole.Add(item.Role);
            }
            lstRole = lstRole.Distinct().ToList();

            //Danh sách các quyền của tập hợp role trên
            var lstRoleDetail = new List<T_AD_ROLE_DETAIL>();
            foreach (var item in lstRole)
            {
                lstRoleDetail.AddRange(item.ListRoleDetail);
            }
            lstRoleDetail = lstRoleDetail.Distinct().ToList();

            foreach (var item in lstAllRight)
            {
                var node = new NodeRight()
                {
                    id = item.CODE,
                    pId = item.PARENT,
                    name = "<span class='spMaOnTree'>" + item.CODE + "</span>" + item.NAME
                };
                if (lstRoleDetail.Count(x => x.FK_RIGHT == item.CODE) > 0)
                    node.@checked = "true";

                lstNode.Add(node);
            }

            if (this.ObjDetail.IS_MODIFY_RIGHT)
            {
                var lstRightModify = this.ObjDetail.ListUserRight.Where(x => x.ORG_CODE == orgCode);
                foreach (var item in lstRightModify)
                {
                    if (item.IS_ADD && lstRoleDetail.Count(x => x.FK_RIGHT == item.FK_RIGHT) == 0)
                    {
                        var node = lstNode.Where(x => x.id == item.FK_RIGHT).FirstOrDefault();
                        node.@checked = "true";
                        node.isAdd = "1";
                    }

                    if (item.IS_REMOVE && lstRoleDetail.Count(x => x.FK_RIGHT == item.FK_RIGHT) >= 0)
                    {
                        var node = lstNode.Where(x => x.id == item.FK_RIGHT).FirstOrDefault();
                        node.@checked = "false";
                        node.isRemove = "1";
                    }
                }
            }

            return lstNode;
        }

        public void UpdateOrgOfUser(string userName, string orgList)
        {
            try
            {
                this.Get(userName);

                var lstOrgCode = orgList.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                lstOrgCode.Remove(this.ObjDetail.ORGANIZE_CODE);

                UnitOfWork.BeginTransaction();

                UnitOfWork.GetSession().CreateSQLQuery($"DELETE T_AD_USER_ORG WHERE USER_NAME = '{userName}'").ExecuteUpdate();

                foreach (var item in lstOrgCode)
                {
                    UnitOfWork.Repository<UserOrgRepo>().Create(new T_AD_USER_ORG()
                    {
                        ORG_CODE = item,
                        USER_NAME = userName,
                        CREATE_BY = ProfileUtilities.User.USER_NAME
                    });
                }

                UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                this.State = false;
                this.Exception = ex;
            }



        }

        public void UpdateRightOfUser(string userName, string rightList, string statusList, string orgCode)
        {
            try
            {
                this.Get(userName);

                //Danh sách role của user theo usergroup
                var lstRole = new List<T_AD_ROLE>();
                foreach (var item1 in this.ObjDetail.ListUserUserGroup)
                {
                    foreach (var item2 in item1.UserGroup.ListUserGroupRole)
                    {
                        lstRole.Add(item2.Role);
                    }
                }
                lstRole = lstRole.Distinct().ToList();

                //Danh sách các quyền của tập hợp role trên
                var lstRoleDetail = new List<T_AD_ROLE_DETAIL>();
                foreach (var item in lstRole)
                {
                    lstRoleDetail.AddRange(item.ListRoleDetail);
                }
                lstRoleDetail = lstRoleDetail.Distinct().ToList();

                var lstStatus = statusList.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                var lstRight = rightList.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                var lstRightModify = this.ObjDetail.ListUserRight;

                UnitOfWork.BeginTransaction();

                for (int i = 0; i < lstRight.Length; i++)
                {
                    var right = lstRight[i];
                    if (lstRightModify.Count(x => x.FK_RIGHT == right && x.ORG_CODE == orgCode) > 0)
                    {
                        var righModify = lstRightModify.FirstOrDefault(x => x.FK_RIGHT == right && x.ORG_CODE == orgCode);
                        lstRightModify.Remove(righModify);
                        UnitOfWork.Repository<UserRightRepo>().Delete(righModify);
                    }

                    if (lstStatus[i] == "true")
                    {
                        if (lstRoleDetail.Count(x => x.FK_RIGHT == right) == 0 && lstRightModify.Count(x => x.FK_RIGHT == right && x.ORG_CODE == orgCode) == 0)
                        {
                            UnitOfWork.Repository<UserRightRepo>().Create(
                                new T_AD_USER_RIGHT()
                                {
                                    FK_RIGHT = right,
                                    USER_NAME = userName,
                                    ORG_CODE = orgCode,
                                    IS_ADD = true,
                                    IS_REMOVE = false
                                }
                            );
                        }
                    }
                    else if (lstStatus[i] == "false")
                    {
                        if (lstRoleDetail.Count(x => x.FK_RIGHT == right) >= 0 && lstRightModify.Count(x => x.FK_RIGHT == right && x.ORG_CODE == orgCode) == 0)
                        {
                            UnitOfWork.Repository<UserRightRepo>().Create(
                                new T_AD_USER_RIGHT()
                                {
                                    FK_RIGHT = right,
                                    USER_NAME = userName,
                                    ORG_CODE = orgCode,
                                    IS_ADD = false,
                                    IS_REMOVE = true
                                }
                            );
                        }
                    }
                }

                this.ObjDetail.IS_MODIFY_RIGHT = true;
                UnitOfWork.Repository<UserRepo>().Update(this.ObjDetail);
                UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                this.State = false;
                this.Exception = ex;
            }
        }

        public void ResetRightOfUser(string userName)
        {
            try
            {
                this.Get(userName);

                UnitOfWork.BeginTransaction();

                this.ObjDetail.IS_MODIFY_RIGHT = false;
                UnitOfWork.Repository<UserRepo>().Update(this.ObjDetail);

                foreach (var item in this.ObjDetail.ListUserRight)
                {
                    UnitOfWork.Repository<UserRightRepo>().Delete(item);
                }

                UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                this.State = false;
                this.Exception = ex;
            }
        }

        public void ResetPassword(string userName)
        {
            try
            {
                this.Get(userName);
                UnitOfWork.BeginTransaction();
                this.ObjDetail.PASSWORD = UtilsCore.EncryptStringMD5(this.ObjDetail.USER_NAME + "@123");
                this.ObjDetail.LAST_CHANGE_PASS_DATE = DateTime.Now;
                UnitOfWork.Repository<UserRepo>().Update(this.ObjDetail);
                UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                this.State = false;
                this.Exception = ex;
            }
        }

        public void SearchRoleForAdd()
        {
            this.Get(this.ObjDetail.USER_NAME);
            var lstRoleOfUser = this.ObjDetail.ListUserRole.Select(x => x.ROLE_CODE).ToList();
            foreach (var item in this.ObjDetail.ListUserUserGroup)
            {
                lstRoleOfUser.AddRange(item.UserGroup.ListUserGroupRole.Select(x => x.ROLE_CODE));
            }
            lstRoleOfUser = lstRoleOfUser.Distinct().ToList();

            var query = UnitOfWork.Repository<RoleRepo>().Queryable();
            query = query.Where(x => !lstRoleOfUser.Contains(x.CODE));
            if (!string.IsNullOrWhiteSpace(ObjRole.CODE))
            {
                query = query.Where(x => x.CODE.ToLower().Contains(ObjRole.CODE.ToLower()) || x.NAME.ToLower().Contains(ObjRole.CODE.ToLower()));
            }
            this.ObjListRole = query.ToList();
        }

        public void AddRoleToUser(string lstRole, string userName)
        {
            try
            {
                UnitOfWork.BeginTransaction();

                foreach (var roleCode in lstRole.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList())
                {
                    var item = new T_AD_USER_ROLE()
                    {
                        ROLE_CODE = roleCode,
                        USER_NAME = userName
                    };

                    if (ProfileUtilities.User != null)
                    {
                        item.CREATE_BY = ProfileUtilities.User.USER_NAME;
                        item.CREATE_DATE = this.CurrentRepository.GetDateDatabase();
                    }
                    UnitOfWork.Repository<UserRoleRepo>().Create(item);
                }
                UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                this.State = false;
                this.Exception = ex;
            }
        }


        public void DeleteRoleOfUser(string lstRole, string userName)
        {
            try
            {
                UnitOfWork.BeginTransaction();

                foreach (var roleCode in lstRole.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList())
                {
                    var item = UnitOfWork.Repository<UserRoleRepo>().Queryable().FirstOrDefault(x => x.ROLE_CODE == roleCode && x.USER_NAME == userName);
                    if (item != null)
                    {
                        UnitOfWork.Repository<UserRoleRepo>().Delete(item);
                    }
                }
                UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                this.State = false;
                this.Exception = ex;
            }
        }

        public void UpdatePass()
        {
            try
            {
                this.ObjDetail.OLD_PASSWORD = UtilsCore.EncryptStringMD5(this.ObjDetail.OLD_PASSWORD);
                var user = UnitOfWork.Repository<UserRepo>().Queryable().FirstOrDefault(x => x.USER_NAME == this.ObjDetail.USER_NAME && x.PASSWORD == this.ObjDetail.OLD_PASSWORD);
                if (user == null)
                {
                    this.State = false;
                    this.MesseageCode = "1103";
                    return;
                }
                UnitOfWork.BeginTransaction();
                user.PASSWORD = UtilsCore.EncryptStringMD5(this.ObjDetail.PASSWORD);
                user.LAST_CHANGE_PASS_DATE = DateTime.Now;
                this.CurrentRepository.Update(user);
                UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                this.State = false;
                this.Exception = ex;
            }
        }

        public void UpdatePhongBan(List<NodeUser> lstNode)
        {
            try
            {
                var lstId = lstNode.Select(x => x.id).ToList();
                var lstUser = this.CurrentRepository.Queryable().Where(x => lstId.Contains(x.USER_NAME));

                UnitOfWork.BeginTransaction();
                foreach (var user in lstUser)
                {
                    var find = lstNode.FirstOrDefault(x => x.id == user.USER_NAME);
                    if (find != null)
                    {
                        user.ORGANIZE_CODE = find.pId;
                        this.CurrentRepository.Update(user);
                    }
                }
                UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                this.State = false;
                this.Exception = ex;
            }
        }
        public IList<T_BP_KE_HOACH_VAN_TAI> GetDataVT(int year)
        {
            return UnitOfWork.Repository<KeHoachVanTaiRepo>().Queryable().Where(x => x.YEAR == year).OrderBy(x => x.C_ORDER).ToList();
        }

        public void ImportDataVT(HttpRequestBase request, int year)
        {
            try
            {
                var fileName = Guid.NewGuid().ToString("N");
                var pathSaveFile = Path.Combine(WebConfigurationManager.AppSettings["PathFileAttach"], DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), DateTime.Now.Day.ToString());
                if (!new DirectoryInfo(pathSaveFile).Exists)
                {
                    Directory.CreateDirectory(pathSaveFile);
                }
                var pathFile = Path.Combine(pathSaveFile, $"{fileName}.xlsx");
                request.Files[0].SaveAs(pathFile);
                if (UnitOfWork.Repository<KeHoachVanTaiRepo>().Queryable().Where(x => x.YEAR == year).Count() > 0)
                {
                    UnitOfWork.Repository<KeHoachVanTaiRepo>().Queryable().Where(x => x.YEAR == year).Delete();
                }
                UnitOfWork.BeginTransaction();
                var tableData = ReadData(pathFile);
                var order = 1;
                for (int i = 8; i < tableData.Rows.Count; i++)
                {
                    UnitOfWork.Repository<KeHoachVanTaiRepo>().Create(new Core.Entities.BP.T_BP_KE_HOACH_VAN_TAI
                    {
                        ID = Guid.NewGuid(),
                        C_ORDER = order,
                        YEAR = year,
                        COL1 = tableData.Rows[i][0].ToString(),
                        COL2 = tableData.Rows[i][1].ToString(),
                        COL3 = Convert.ToDecimal(tableData.Rows[i][2].ToString() == "" || tableData.Rows[i][2].ToString() == null ? "0" : tableData.Rows[i][2].ToString()),
                        COL4 = tableData.Rows[i][3].ToString() == "" || tableData.Rows[i][3].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][3].ToString() ?? "0"),
                        COL5 = tableData.Rows[i][4].ToString(),
                        COL6 = tableData.Rows[i][5].ToString() == "" || tableData.Rows[i][5].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][5].ToString() ?? "0"),
                        COL7 = tableData.Rows[i][6].ToString() == "" || tableData.Rows[i][6].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][6].ToString() ?? "0"),
                        COL8 = tableData.Rows[i][7].ToString() == "" || tableData.Rows[i][7].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][7].ToString() ?? "0"),
                        COL9 = tableData.Rows[i][8].ToString() == "" || tableData.Rows[i][8].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][8].ToString() ?? "0"),
                        COL10 = tableData.Rows[i][9].ToString() == "" || tableData.Rows[i][9].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][9].ToString() ?? "0"),
                        COL11 = tableData.Rows[i][10].ToString() == "" || tableData.Rows[i][10].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][10].ToString() ?? "0"),
                        COL12 = tableData.Rows[i][11].ToString() == "" || tableData.Rows[i][11].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][11].ToString() ?? "0"),
                        COL13 = tableData.Rows[i][12].ToString() == "" || tableData.Rows[i][12].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][12].ToString() ?? "0"),
                        COL14 = tableData.Rows[i][13].ToString() == "" || tableData.Rows[i][13].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][13].ToString() ?? "0"),
                        COL15 = tableData.Rows[i][14].ToString() == "" || tableData.Rows[i][14].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][14].ToString() ?? "0"),
                        COL16 = tableData.Rows[i][15].ToString() == "" || tableData.Rows[i][15].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][15].ToString() ?? "0"),
                        COL17 = tableData.Rows[i][16].ToString() == "" || tableData.Rows[i][16].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][16].ToString() ?? "0"),
                        COL18 = tableData.Rows[i][17].ToString() == "" || tableData.Rows[i][17].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][17].ToString() ?? "0"),
                        COL19 = tableData.Rows[i][18].ToString() == "" || tableData.Rows[i][18].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][18].ToString() ?? "0"),
                        COL20 = tableData.Rows[i][19].ToString() == "" || tableData.Rows[i][19].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][19].ToString() ?? "0"),
                        COL21 = tableData.Rows[i][20].ToString() == "" || tableData.Rows[i][20].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][20].ToString() ?? "0"),
                        COL22 = tableData.Rows[i][21].ToString() == "" || tableData.Rows[i][21].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][21].ToString() ?? "0"),
                        COL23 = tableData.Rows[i][22].ToString() == "" || tableData.Rows[i][22].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][22].ToString() ?? "0"),
                        COL24 = tableData.Rows[i][23].ToString() == "" || tableData.Rows[i][23].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][23].ToString() ?? "0"),
                        COL25 = tableData.Rows[i][24].ToString() == "" || tableData.Rows[i][24].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][24].ToString() ?? "0"),
                        COL26 = tableData.Rows[i][25].ToString() == "" || tableData.Rows[i][25].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][25].ToString() ?? "0"),
                        COL27 = tableData.Rows[i][26].ToString() == "" || tableData.Rows[i][26].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][26].ToString() ?? "0"),
                        COL28 = tableData.Rows[i][27].ToString() == "" || tableData.Rows[i][27].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][27].ToString() ?? "0"),
                        COL29 = tableData.Rows[i][28].ToString() == "" || tableData.Rows[i][28].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][28].ToString() ?? "0"),
                        COL30 = tableData.Rows[i][29].ToString() == "" || tableData.Rows[i][29].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][29].ToString() ?? "0"),
                        COL31 = tableData.Rows[i][30].ToString() == "" || tableData.Rows[i][30].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][30].ToString() ?? "0"),
                        COL32 = tableData.Rows[i][31].ToString() == "" || tableData.Rows[i][31].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][31].ToString() ?? "0"),
                        COL33 = tableData.Rows[i][32].ToString() == "" || tableData.Rows[i][32].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][32].ToString() ?? "0"),
                        COL34 = tableData.Rows[i][33].ToString() == "" || tableData.Rows[i][33].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][33].ToString() ?? "0"),
                        COL35 = tableData.Rows[i][34].ToString() == "" || tableData.Rows[i][34].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][34].ToString() ?? "0"),
                        COL36 = tableData.Rows[i][35].ToString() == "" || tableData.Rows[i][35].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][35].ToString() ?? "0"),
                        COL37 = tableData.Rows[i][36].ToString() == "" || tableData.Rows[i][36].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][36].ToString() ?? "0"),
                        COL38 = tableData.Rows[i][37].ToString() == "" || tableData.Rows[i][37].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][37].ToString() ?? "0"),
                        COL39 = tableData.Rows[i][38].ToString() == "" || tableData.Rows[i][38].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][38].ToString() ?? "0"),
                        COL40 = tableData.Rows[i][39].ToString() == "" || tableData.Rows[i][39].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][39].ToString() ?? "0"),
                        COL41 = tableData.Rows[i][40].ToString() == "" || tableData.Rows[i][40].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][40].ToString() ?? "0"),
                        COL42 = tableData.Rows[i][41].ToString() == "" || tableData.Rows[i][41].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][41].ToString() ?? "0"),
                        COL43 = tableData.Rows[i][42].ToString() == "" || tableData.Rows[i][42].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][42].ToString() ?? "0"),
                        COL44 = tableData.Rows[i][43].ToString() == "" || tableData.Rows[i][43].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][43].ToString() ?? "0"),
                        COL45 = tableData.Rows[i][44].ToString() == "" || tableData.Rows[i][44].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][44].ToString() ?? "0"),
                        COL46 = tableData.Rows[i][45].ToString() == "" || tableData.Rows[i][45].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][45].ToString() ?? "0"),
                        COL47 = tableData.Rows[i][46].ToString() == "" || tableData.Rows[i][46].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][46].ToString() ?? "0"),
                        COL48 = tableData.Rows[i][47].ToString() == "" || tableData.Rows[i][47].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][47].ToString() ?? "0"),
                        COL49 = tableData.Rows[i][48].ToString() == "" || tableData.Rows[i][48].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][48].ToString() ?? "0"),
                        COL50 = tableData.Rows[i][49].ToString() == "" || tableData.Rows[i][49].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][49].ToString() ?? "0"),
                        COL51 = tableData.Rows[i][50].ToString() == "" || tableData.Rows[i][50].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][50].ToString() ?? "0"),
                        COL52 = tableData.Rows[i][51].ToString() == "" || tableData.Rows[i][51].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][51].ToString() ?? "0"),
                        COL53 = tableData.Rows[i][52].ToString() == "" || tableData.Rows[i][52].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][52].ToString() ?? "0"),
                        COL54 = tableData.Rows[i][53].ToString() == "" || tableData.Rows[i][53].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][53].ToString() ?? "0"),
                        COL55 = tableData.Rows[i][54].ToString() == "" || tableData.Rows[i][54].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][54].ToString() ?? "0"),
                        COL56 = tableData.Rows[i][55].ToString() == "" || tableData.Rows[i][55].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][55].ToString() ?? "0"),
                        COL57 = tableData.Rows[i][56].ToString() == "" || tableData.Rows[i][56].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][56].ToString() ?? "0"),
                        COL58 = tableData.Rows[i][57].ToString() == "" || tableData.Rows[i][57].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][57].ToString() ?? "0"),
                        COL59 = tableData.Rows[i][58].ToString() == "" || tableData.Rows[i][58].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][58].ToString() ?? "0"),
                        COL60 = tableData.Rows[i][59].ToString() == "" || tableData.Rows[i][59].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][59].ToString() ?? "0"),
                        COL61 = tableData.Rows[i][60].ToString() == "" || tableData.Rows[i][60].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][60].ToString() ?? "0"),
                        COL62 = tableData.Rows[i][61].ToString() == "" || tableData.Rows[i][61].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][61].ToString() ?? "0"),
                        COL63 = tableData.Rows[i][62].ToString() == "" || tableData.Rows[i][62].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][62].ToString() ?? "0"),
                        COL64 = tableData.Rows[i][63].ToString() == "" || tableData.Rows[i][63].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][63].ToString() ?? "0"),
                        COL65 = tableData.Rows[i][64].ToString() == "" || tableData.Rows[i][64].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][64].ToString() ?? "0"),
                        COL66 = tableData.Rows[i][65].ToString() == "" || tableData.Rows[i][65].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][65].ToString() ?? "0"),
                        COL67 = tableData.Rows[i][66].ToString() == "" || tableData.Rows[i][66].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][66].ToString() ?? "0"),
                        COL68 = tableData.Rows[i][67].ToString() == "" || tableData.Rows[i][67].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][67].ToString() ?? "0"),
                        COL69 = tableData.Rows[i][68].ToString() == "" || tableData.Rows[i][68].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][68].ToString() ?? "0"),
                        COL70 = tableData.Rows[i][69].ToString() == "" || tableData.Rows[i][69].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][69].ToString() ?? "0"),
                        COL71 = tableData.Rows[i][70].ToString() == "" || tableData.Rows[i][70].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][70].ToString() ?? "0"),
                        COL72 = tableData.Rows[i][71].ToString() == "" || tableData.Rows[i][71].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][71].ToString() ?? "0"),
                        COL73 = tableData.Rows[i][72].ToString() == "" || tableData.Rows[i][72].ToString() == null ? 0 : Convert.ToDecimal(tableData.Rows[i][72].ToString() ?? "0"),
                    });
                    order++;
                }
                UnitOfWork.Commit();
                
                
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                this.State = false;
                this.Exception = ex;
            }
        }
        public void ExportExcelGridData(ref MemoryStream outFileStream, string path, List<T_BP_KE_HOACH_VAN_TAI> TreeData)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            IWorkbook templateWorkbook;
            templateWorkbook = new XSSFWorkbook(fs);
       
            fs.Close();
            ISheet sheet = templateWorkbook.GetSheetAt(0);
            var startRow =8;
            ICellStyle styleCellNumber = templateWorkbook.CreateCellStyle();
            styleCellNumber.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,##0");
            styleCellNumber.Alignment = HorizontalAlignment.Right;
            ICellStyle styleCellText = templateWorkbook.CreateCellStyle();
            styleCellText.Alignment = HorizontalAlignment.Left;
            var NUMCELL = 73;
            foreach (var item in TreeData)
            {
                IRow rowCur = ReportUtilities.CreateRow(ref sheet, startRow++, NUMCELL);
                rowCur.Cells[0].SetCellValue(item.COL1);
                rowCur.Cells[1].SetCellValue(item.COL2);
                rowCur.Cells[2].SetCellValue(Convert.ToDouble(item.COL3));
                rowCur.Cells[2].CellStyle = styleCellNumber;
                rowCur.Cells[3].SetCellValue(Convert.ToDouble(item.COL4));
                rowCur.Cells[3].CellStyle = styleCellNumber;
                rowCur.Cells[4].SetCellValue(item.COL5);
                rowCur.Cells[5].SetCellValue(Convert.ToDouble(item.COL6));
                rowCur.Cells[5].CellStyle = styleCellNumber;
                rowCur.Cells[6].SetCellValue(Convert.ToDouble(item.COL7));
                rowCur.Cells[6].CellStyle = styleCellNumber;
                rowCur.Cells[7].SetCellValue(Convert.ToDouble(item.COL8));
                rowCur.Cells[7].CellStyle = styleCellNumber;
                rowCur.Cells[8].SetCellValue(Convert.ToDouble(item.COL9));
                rowCur.Cells[8].CellStyle = styleCellNumber;
                rowCur.Cells[9].SetCellValue(Convert.ToDouble(item.COL10));
                rowCur.Cells[9].CellStyle = styleCellNumber;
                rowCur.Cells[10].SetCellValue(Convert.ToDouble(item.COL11));
                rowCur.Cells[10].CellStyle = styleCellNumber;
                rowCur.Cells[11].SetCellValue(Convert.ToDouble(item.COL12));
                rowCur.Cells[11].CellStyle = styleCellNumber;
                rowCur.Cells[12].SetCellValue(Convert.ToDouble(item.COL13));
                rowCur.Cells[12].CellStyle = styleCellNumber;
                rowCur.Cells[13].SetCellValue(Convert.ToDouble(item.COL14));
                rowCur.Cells[13].CellStyle = styleCellNumber;
                rowCur.Cells[14].SetCellValue(Convert.ToDouble(item.COL15));
                rowCur.Cells[14].CellStyle = styleCellNumber;
                rowCur.Cells[15].SetCellValue(Convert.ToDouble(item.COL16));
                rowCur.Cells[15].CellStyle = styleCellNumber;
                rowCur.Cells[16].SetCellValue(Convert.ToDouble(item.COL17));
                rowCur.Cells[16].CellStyle = styleCellNumber;
                rowCur.Cells[17].SetCellValue(Convert.ToDouble(item.COL18));
                rowCur.Cells[17].CellStyle = styleCellNumber;
                rowCur.Cells[18].SetCellValue(Convert.ToDouble(item.COL19));
                rowCur.Cells[18].CellStyle = styleCellNumber;
                rowCur.Cells[19].SetCellValue(Convert.ToDouble(item.COL20));
                rowCur.Cells[19].CellStyle = styleCellNumber;
                rowCur.Cells[20].SetCellValue(Convert.ToDouble(item.COL21));
                rowCur.Cells[20].CellStyle = styleCellNumber;
                rowCur.Cells[21].SetCellValue(Convert.ToDouble(item.COL22));
                rowCur.Cells[21].CellStyle = styleCellNumber;
                rowCur.Cells[22].SetCellValue(Convert.ToDouble(item.COL23));
                rowCur.Cells[22].CellStyle = styleCellNumber;
                rowCur.Cells[23].SetCellValue(Convert.ToDouble(item.COL24));
                rowCur.Cells[23].CellStyle = styleCellNumber;
                rowCur.Cells[24].SetCellValue(Convert.ToDouble(item.COL25));
                rowCur.Cells[24].CellStyle = styleCellNumber;
                rowCur.Cells[25].SetCellValue(Convert.ToDouble(item.COL26));
                rowCur.Cells[25].CellStyle = styleCellNumber;
                rowCur.Cells[26].SetCellValue(Convert.ToDouble(item.COL27));
                rowCur.Cells[26].CellStyle = styleCellNumber;
                rowCur.Cells[27].SetCellValue(Convert.ToDouble(item.COL28));
                rowCur.Cells[27].CellStyle = styleCellNumber;
                rowCur.Cells[28].SetCellValue(Convert.ToDouble(item.COL29));
                rowCur.Cells[28].CellStyle = styleCellNumber;
                rowCur.Cells[29].SetCellValue(Convert.ToDouble(item.COL30));
                rowCur.Cells[29].CellStyle = styleCellNumber;
                rowCur.Cells[30].SetCellValue(Convert.ToDouble(item.COL31));
                rowCur.Cells[30].CellStyle = styleCellNumber;
                rowCur.Cells[31].SetCellValue(Convert.ToDouble(item.COL32));
                rowCur.Cells[31].CellStyle = styleCellNumber;
                rowCur.Cells[32].SetCellValue(Convert.ToDouble(item.COL33));
                rowCur.Cells[32].CellStyle = styleCellNumber;
                rowCur.Cells[33].SetCellValue(Convert.ToDouble(item.COL34));
                rowCur.Cells[33].CellStyle = styleCellNumber;
                rowCur.Cells[34].SetCellValue(Convert.ToDouble(item.COL35));
                rowCur.Cells[34].CellStyle = styleCellNumber;
                rowCur.Cells[35].SetCellValue(Convert.ToDouble(item.COL36));
                rowCur.Cells[35].CellStyle = styleCellNumber;
                rowCur.Cells[36].SetCellValue(Convert.ToDouble(item.COL37));
                rowCur.Cells[36].CellStyle = styleCellNumber;
                rowCur.Cells[37].SetCellValue(Convert.ToDouble(item.COL38));
                rowCur.Cells[37].CellStyle = styleCellNumber;
                rowCur.Cells[38].SetCellValue(Convert.ToDouble(item.COL39));
                rowCur.Cells[38].CellStyle = styleCellNumber;
                rowCur.Cells[39].SetCellValue(Convert.ToDouble(item.COL40));
                rowCur.Cells[39].CellStyle = styleCellNumber;
                rowCur.Cells[40].SetCellValue(Convert.ToDouble(item.COL41));
                rowCur.Cells[40].CellStyle = styleCellNumber;
                rowCur.Cells[41].SetCellValue(Convert.ToDouble(item.COL42));
                rowCur.Cells[41].CellStyle = styleCellNumber;
                rowCur.Cells[42].SetCellValue(Convert.ToDouble(item.COL43));
                rowCur.Cells[42].CellStyle = styleCellNumber;
                rowCur.Cells[43].SetCellValue(Convert.ToDouble(item.COL44));
                rowCur.Cells[43].CellStyle = styleCellNumber;
                rowCur.Cells[44].SetCellValue(Convert.ToDouble(item.COL45));
                rowCur.Cells[44].CellStyle = styleCellNumber;
                rowCur.Cells[45].SetCellValue(Convert.ToDouble(item.COL46));
                rowCur.Cells[45].CellStyle = styleCellNumber;
                rowCur.Cells[46].SetCellValue(Convert.ToDouble(item.COL47));
                rowCur.Cells[46].CellStyle = styleCellNumber;
                rowCur.Cells[47].SetCellValue(Convert.ToDouble(item.COL48));
                rowCur.Cells[47].CellStyle = styleCellNumber;
                rowCur.Cells[48].SetCellValue(Convert.ToDouble(item.COL49));
                rowCur.Cells[48].CellStyle = styleCellNumber;
                rowCur.Cells[49].SetCellValue(Convert.ToDouble(item.COL50));
                rowCur.Cells[49].CellStyle = styleCellNumber;
                rowCur.Cells[50].SetCellValue(Convert.ToDouble(item.COL51));
                rowCur.Cells[50].CellStyle = styleCellNumber;
                rowCur.Cells[51].SetCellValue(Convert.ToDouble(item.COL52));
                rowCur.Cells[51].CellStyle = styleCellNumber;
                rowCur.Cells[52].SetCellValue(Convert.ToDouble(item.COL53));
                rowCur.Cells[52].CellStyle = styleCellNumber;
                rowCur.Cells[53].SetCellValue(Convert.ToDouble(item.COL54));
                rowCur.Cells[53].CellStyle = styleCellNumber;
                rowCur.Cells[54].SetCellValue(Convert.ToDouble(item.COL55));
                rowCur.Cells[54].CellStyle = styleCellNumber;
                rowCur.Cells[55].SetCellValue(Convert.ToDouble(item.COL56));
                rowCur.Cells[55].CellStyle = styleCellNumber;
                rowCur.Cells[56].SetCellValue(Convert.ToDouble(item.COL57));
                rowCur.Cells[56].CellStyle = styleCellNumber;
                rowCur.Cells[57].SetCellValue(Convert.ToDouble(item.COL58));
                rowCur.Cells[57].CellStyle = styleCellNumber;
                rowCur.Cells[58].SetCellValue(Convert.ToDouble(item.COL59));
                rowCur.Cells[58].CellStyle = styleCellNumber;
                rowCur.Cells[59].SetCellValue(Convert.ToDouble(item.COL60));
                rowCur.Cells[59].CellStyle = styleCellNumber;
                rowCur.Cells[60].SetCellValue(Convert.ToDouble(item.COL61));
                rowCur.Cells[60].CellStyle = styleCellNumber;
                rowCur.Cells[61].SetCellValue(Convert.ToDouble(item.COL62));
                rowCur.Cells[61].CellStyle = styleCellNumber;
                rowCur.Cells[62].SetCellValue(Convert.ToDouble(item.COL63));
                rowCur.Cells[62].CellStyle = styleCellNumber;
                rowCur.Cells[63].SetCellValue(Convert.ToDouble(item.COL64));
                rowCur.Cells[63].CellStyle = styleCellNumber;
                rowCur.Cells[64].SetCellValue(Convert.ToDouble(item.COL65));
                rowCur.Cells[64].CellStyle = styleCellNumber;
                rowCur.Cells[65].SetCellValue(Convert.ToDouble(item.COL66));
                rowCur.Cells[65].CellStyle = styleCellNumber;
                rowCur.Cells[66].SetCellValue(Convert.ToDouble(item.COL67));
                rowCur.Cells[66].CellStyle = styleCellNumber;
                rowCur.Cells[67].SetCellValue(Convert.ToDouble(item.COL68));
                rowCur.Cells[67].CellStyle = styleCellNumber;
                rowCur.Cells[68].SetCellValue(Convert.ToDouble(item.COL69));
                rowCur.Cells[68].CellStyle = styleCellNumber;
                rowCur.Cells[69].SetCellValue(Convert.ToDouble(item.COL70));
                rowCur.Cells[69].CellStyle = styleCellNumber;
                rowCur.Cells[70].SetCellValue(Convert.ToDouble(item.COL71));
                rowCur.Cells[70].CellStyle = styleCellNumber;
                rowCur.Cells[71].SetCellValue(Convert.ToDouble(item.COL72));
                rowCur.Cells[71].CellStyle = styleCellNumber;
                rowCur.Cells[72].SetCellValue(Convert.ToDouble(item.COL73));
                rowCur.Cells[72].CellStyle = styleCellNumber;
            }
            templateWorkbook.Write(outFileStream);
        }
            public static DataTable ReadData(string filePath)
        {
            using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                try
                {
                    IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    DataSet dataset = excelReader.AsDataSet();
                    if (dataset != null && dataset.Tables.Count > 0)
                    {
                        return dataset.Tables[0];
                    }
                    return null;
                }
                catch
                {
                    return null;
                }
            }
        }
        public  class VtData
        {
            public  Guid ID { get; set; }
            public  int YEAR { get; set; }
            public  string COL1 { get; set; }
            public  string COL2 { get; set; }
            public  decimal? COL3 { get; set; }
            public  decimal? COL4 { get; set; }
            public  string COL5 { get; set; }
            public  decimal? COL6 { get; set; }
            public  decimal? COL7 { get; set; }
            public  decimal? COL8 { get; set; }
            public  decimal? COL9 { get; set; }
            public  decimal? COL10 { get; set; }
            public  decimal? COL11 { get; set; }
            public  decimal? COL12 { get; set; }
            public  decimal? COL13 { get; set; }
            public  decimal? COL14 { get; set; }
            public  decimal? COL15 { get; set; }
            public  decimal? COL16 { get; set; }
            public  decimal? COL17 { get; set; }
            public  decimal? COL18 { get; set; }
            public  decimal? COL19 { get; set; }
            public  decimal? COL20 { get; set; }
            public  decimal? COL21 { get; set; }
            public  decimal? COL22 { get; set; }
            public  decimal? COL23 { get; set; }
            public  decimal? COL24 { get; set; }
            public  decimal? COL25 { get; set; }
            public  decimal? COL26 { get; set; }
            public  decimal? COL27 { get; set; }
            public  decimal? COL28 { get; set; }
            public  decimal? COL29 { get; set; }
            public  decimal? COL30 { get; set; }
            public  decimal? COL31 { get; set; }
            public  decimal? COL32 { get; set; }
            public  decimal? COL33 { get; set; }
            public  decimal? COL34 { get; set; }
            public  decimal? COL35 { get; set; }
            public  decimal? COL36 { get; set; }
            public  decimal? COL37 { get; set; }
            public  decimal? COL38 { get; set; }
            public  decimal? COL39 { get; set; }
            public  decimal? COL40 { get; set; }
            public  decimal? COL41 { get; set; }
            public  decimal? COL42 { get; set; }
            public  decimal? COL43 { get; set; }
            public  decimal? COL44 { get; set; }
            public  decimal? COL45 { get; set; }
            public  decimal? COL46 { get; set; }
            public  decimal? COL47 { get; set; }
            public  decimal? COL48 { get; set; }
            public  decimal? COL49 { get; set; }
            public  decimal? COL50 { get; set; }
            public  decimal? COL51 { get; set; }
            public  decimal? COL52 { get; set; }
            public  decimal? COL53 { get; set; }
            public  decimal? COL54 { get; set; }
            public  decimal? COL55 { get; set; }
            public  decimal? COL56 { get; set; }
            public  decimal? COL57 { get; set; }
            public  decimal? COL58 { get; set; }
            public  decimal? COL59 { get; set; }
            public  decimal? COL60 { get; set; }
            public  decimal? COL61 { get; set; }
            public  decimal? COL62 { get; set; }
            public  decimal? COL63 { get; set; }
            public  decimal? COL64 { get; set; }
            public  decimal? COL65 { get; set; }
            public  decimal? COL66 { get; set; }
            public  decimal? COL67 { get; set; }
            public  decimal? COL68 { get; set; }
            public  decimal? COL69 { get; set; }
            public  decimal? COL70 { get; set; }
            public  decimal? COL71 { get; set; }
            public  decimal? COL72 { get; set; }
            public  decimal? COL73 { get; set; }
            public  decimal? COL74 { get; set; }
            public  decimal? COL75 { get; set; }
            public  decimal? COL76 { get; set; }
            public  decimal? COL77 { get; set; }
            public  decimal? COL78 { get; set; }
            public  int C_ORDER { get; set; }

        }
    }
}
