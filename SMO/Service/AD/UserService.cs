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
        public void ExportExcelGridData(ref MemoryStream outFileStream, string path, string Template)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            IWorkbook templateWorkbook;
            templateWorkbook = new XSSFWorkbook(fs);
            //templateWorkbook.SetSheetName(0, ModulType.GetTextSheetName(ModulType.KeHoachSanLuong));
            fs.Close();
            ISheet sheet = templateWorkbook.GetSheetAt(0);
            var startRow =7;
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
    }
}
