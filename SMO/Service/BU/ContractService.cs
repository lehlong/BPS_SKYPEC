
using iTextSharp.text.pdf.codec;
using Microsoft.Ajax.Utilities;
using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using SMO.Core.Entities;
using SMO.Core.Entities.BU;
using SMO.Repository.Common;
using SMO.Repository.Implement.BU;
using SMO.Repository.Implement.MD;
using SMO.Service.AD;
using SMO.Service.Class;
using SMO.Service.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.UI.WebControls;
using static iTextSharp.text.pdf.AcroFields;
using NPOI.POIFS.Properties;
using SMO.Repository.Implement.AD;
using NPOI.HPSF;
using NHibernate.Engine;
using NPOI.SS.Formula.Functions;
using System.Web.UI.WebControls.WebParts;
using SMO.Repository.Interface.BU;
using iTextSharp.text;
using SMO.Repository.Implement.CM;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SMO.Models;

namespace SMO.Service.BU
{
    public class ContractService : GenericService<T_BU_CONTRACT, ContractRepo>
    {
        public List<string> ObjListUser { get; set; }
        public List<string> listLink { get; set; }
        public List<string> listLinkRemove { get; set; }
        public List<string> listFileRemove { get; set; }
        public int version { get; set; }

        public ContractService() : base()
        {
            ObjListUser = new List<string>();
            listLink = new List<string>();
            listLinkRemove = new List<string>();
            listFileRemove = new List<string>();
        }
        public void Create(HttpRequestBase Request)
        {
            try
            {
                State = true;
                ErrorMessage = "";
                if (string.IsNullOrEmpty(this.ObjDetail.CONTRACT_NUMBER))
                {
                    ErrorMessage = "Vui lòng nhập số hợp đồng!";
                    State = false;
                    return;
                }
                if (string.IsNullOrEmpty(this.ObjDetail.NAME))
                {
                    ErrorMessage = "Vui lòng nhập tên hợp đồng!";
                    State = false;
                    return;
                }
                if (this.ObjDetail.CONTRACT_VALUE_VAT <= 0)
                {
                    ErrorMessage = "Vui lòng nhập nhập giá trị hợp đồng!";
                    State = false;
                    return;
                }
                if (string.IsNullOrEmpty(this.ObjDetail.APPROVER))
                {
                    ErrorMessage = "Vui lòng chọn người trình duyệt hợp đồng!";
                    State = false;
                    return;
                }
                if (string.IsNullOrEmpty(this.ObjDetail.CONTRACT_MANAGER))
                {
                    ErrorMessage += "Vui lòng chọn người quản lý hợp đồng!";
                    State = false;
                    return;
                }
                if (string.IsNullOrEmpty(this.ObjDetail.CUSTOMER))
                {
                    ErrorMessage = "Vui nhập tên khách hàng!";
                    State = false;
                    return;
                }
                if (string.IsNullOrEmpty(this.ObjDetail.REPRESENT_A))
                {
                    ErrorMessage = "Vui lòng nhập bên A của hợp đồng!";
                    State = false;
                    return;
                }
                if (string.IsNullOrEmpty(this.ObjDetail.REPRESENT_B))
                {
                    ErrorMessage = "Vui lòng nhập bên B của hợp đồng!";
                    State = false;
                    return;
                }
                if(this.ObjDetail.FINISH_DATE < this.ObjDetail.START_DATE)
                {
                    ErrorMessage = "Ngày kết thúc hợp đồng phải lớn hơn ngày bắt đầu hợp đồng!";
                    State = false;
                    return;
                }
                // lưu file
                var lstFileStream = new List<FILE_STREAM>();
                for (int i = 0; i < Request.Files.AllKeys.Length; i++)
                {
                    var file = Request.Files[i];
                    lstFileStream.Add(new FILE_STREAM()
                    {
                        PKID = Guid.NewGuid().ToString(),
                        FILE_OLD_NAME = file.FileName,
                        FILE_NAME = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName),
                        FILE_EXT = Path.GetExtension(file.FileName),
                        FILE_SIZE = file.ContentLength,
                        FILESTREAM = Request.Files[i]
                    });
                }
                FileStreamService.InsertFile(lstFileStream);
                // hết lưu file
                // bắt đàu lưu vào db
                UnitOfWork.BeginTransaction();
                string idFileChild = Guid.NewGuid().ToString();
                string id = Guid.NewGuid().ToString();
                string nameContract = Guid.NewGuid().ToString();
                // lưu file vào db
                foreach (var item in lstFileStream)
                {
                    var fileUpload = new T_BU_FILE_UPLOAD()
                    {
                        ID = item.PKID,
                        //CONNECTION_ID = systemConfigService.ObjDetail.CURRENT_CONNECTION,
                        //DATABASE_NAME = systemConfigService.ObjDetail.CURRENT_DATABASE_NAME,                           
                        FILE_EXT = item.FILE_EXT,
                        FILE_NAME = item.FILE_NAME,
                        FILE_OLD_NAME = item.FILE_OLD_NAME,
                        FILE_SIZE = item.FILE_SIZE,
                        DIRECTORY_PATH = item.DIRECTORY_PATH,
                        FULL_PATH = item.FULL_PATH,
                        PARENT = idFileChild,
                        CREATE_BY = ProfileUtilities.User.USER_NAME,
                        IS_DELETE = false,
                        VERSION = 1,
                    };
                    UnitOfWork.Repository<Repository.Implement.BU.FileUploadRepo>().Create(fileUpload);
                }
                this.ObjDetail.ID = id;
                this.ObjDetail.NAME_CONTRACT = nameContract;
                this.ObjDetail.CREATE_BY = ProfileUtilities.User.USER_NAME;
                this.ObjDetail.VERSION = 1;
                this.ObjDetail.STATUS = ConstContract.KhoiTao;
                this.ObjDetail.FILE_CHILD = idFileChild;
                this.CurrentRepository.Create(this.ObjDetail);
                // tạo người theo dõi
                foreach (var item in this.ObjListUser)
                {
                    var follower = new T_BU_CONTRACT_HUMAN()
                    {
                        ID = Guid.NewGuid().ToString(),
                        USERNAME = item,
                        CONTRACT_ID = nameContract,
                        ACTION = ConstContract.NguoiTheoDoi,
                        VERSION = 1,
                        CREATE_BY = ProfileUtilities.User.USER_NAME
                    };
                    UnitOfWork.Repository<ContractHumanRepo>().Create(follower);
                }
                // tạo lịch sử
                var history = new T_BU_CONTRACT_HISTORY()
                {
                    ID = Guid.NewGuid().ToString(),
                    ACTION = ConstContract.KhoiTao,
                    CREATE_BY = ProfileUtilities.User.USER_NAME,
                    VERSION = 1,
                    NAME_CONTRACT = nameContract,

                };
                UnitOfWork.Repository<ContractHistoryRepo>().Create(history);

                UnitOfWork.Commit();

            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                this.State = false;
                this.Exception = ex;
            }
        }
        public void GetOldVersion(string nameContract, int version)
        {
            try
            {
                getMaxVersion(nameContract);
                if (version == 0 || version == this.version)
                {
                    this.ObjDetail = this.CurrentRepository.Queryable().Where(x => x.NAME_CONTRACT == nameContract).FirstOrDefault();
                }
                else
                {
                    var oldVersionContract = UnitOfWork.Repository<ContractVersionRepo>().Queryable().Where(x => x.VERSION == version && x.NAME_CONTRACT == nameContract).FirstOrDefault();
                    this.ObjDetail = ConvertContractToContractVersion(oldVersionContract);
                }

            }
            catch
            {

            }
        }
        public void getMaxVersion(string nameContract)
        {
            try
            {
                this.version = this.CurrentRepository.Queryable().Where(x => x.NAME_CONTRACT.Equals(nameContract)).FirstOrDefault().VERSION;
            }
            catch
            {

            }
        }
        public void getListFollowers(string id, int version)
        {
            IUnitOfWork UnitOfWork = new NHUnitOfWork();
            ObjListUser = UnitOfWork.Repository<ContractHumanRepo>().GetAll().Where(x => x.CONTRACT_ID == id && x.VERSION == version).Select(x => x.USERNAME).ToList();

        }

        public ViewDashboardContractModel GetDataDashboard()
        {
            IUnitOfWork UnitOfWork = new NHUnitOfWork();
            var data = new ViewDashboardContractModel();
            data.TotalContract = UnitOfWork.Repository<ContractRepo>().GetAll().Count().ToString();
            data.SumValueTotalContract = (UnitOfWork.Repository<ContractRepo>().GetAll().Sum(x => x.CONTRACT_VALUE_VAT) / 1000000000).ToString("0.00");
            data.SumContractPayment = (UnitOfWork.Repository<PaymentRepo>().GetAll().Sum(x => x.AMOUNT) / 1000000000).ToString("0.00");

            data.dataDashboard1 = new List<ViewDashboardBase>();
            data.dataDashboard2 = new List<ViewDashboardBase>();

            var costCenter = UnitOfWork.Repository<CostCenterRepo>().GetAll();
            foreach (var item in costCenter)
            {
                var contractByCostCenter = UnitOfWork.Repository<ContractRepo>().Queryable().Where(x => x.DEPARTMENT == item.CODE).Count();
                if (contractByCostCenter != 0)
                {
                    data.dataDashboard2.Add(new ViewDashboardBase
                    {
                        Text = item.NAME,
                        Value = contractByCostCenter
                    });
                }
            }

            if (UnitOfWork.Repository<ContractRepo>().Queryable().Where(x => x.STATUS == "01").Count() != 0)
            {
                data.dataDashboard1.Add(new ViewDashboardBase
                {
                    Text = "Khởi tạo",
                    Value = UnitOfWork.Repository<ContractRepo>().Queryable().Where(x => x.STATUS == "01").Count()
                });
            }
            if (UnitOfWork.Repository<ContractRepo>().Queryable().Where(x => x.STATUS == "02").Count() != 0)
            {
                data.dataDashboard1.Add(new ViewDashboardBase
                {
                    Text = "Trình duyệt",
                    Value = UnitOfWork.Repository<ContractRepo>().Queryable().Where(x => x.STATUS == "02").Count()
                });
            }
            if (UnitOfWork.Repository<ContractRepo>().Queryable().Where(x => x.STATUS == "03").Count() != 0)
            {
                data.dataDashboard1.Add(new ViewDashboardBase
                {
                    Text = "Phê duyệt",
                    Value = UnitOfWork.Repository<ContractRepo>().Queryable().Where(x => x.STATUS == "03").Count()
                });
            }
            if (UnitOfWork.Repository<ContractRepo>().Queryable().Where(x => x.STATUS == "04").Count() != 0)
            {
                data.dataDashboard1.Add(new ViewDashboardBase
                {
                    Text = "Từ chối phê duyệt",
                    Value = UnitOfWork.Repository<ContractRepo>().Queryable().Where(x => x.STATUS == "04").Count()
                });
            }
            if (UnitOfWork.Repository<ContractRepo>().Queryable().Where(x => x.STATUS == "05").Count() != 0)
            {
                data.dataDashboard1.Add(new ViewDashboardBase
                {
                    Text = "Huỷ trình duyệt",
                    Value = UnitOfWork.Repository<ContractRepo>().Queryable().Where(x => x.STATUS == "05").Count()
                });
            }
            return data;
        }

        public void Search()
        {
            try
            {
                int iTotalRecord = 0;
                this.ObjList = this.CurrentRepository.Search(this.ObjDetail, this.NumerRecordPerPage, this.Page, out iTotalRecord).OrderByDescending(x => x.CREATE_DATE).ToList();
                this.TotalRecord = iTotalRecord;
                foreach (var item in this.ObjList)
                {
                    var sumPaymentContract = getAmount(item.NAME_CONTRACT, item.VERSION);
                    if (sumPaymentContract != 0 && item.CONTRACT_VALUE_VAT != 0)
                    {
                        item.PHANTRAM = Math.Floor((getAmount(item.NAME_CONTRACT, item.VERSION) / item.CONTRACT_VALUE_VAT) * 100);

                    }
                }
            }
            catch (Exception ex)
            {
                this.State = false;
                this.Exception = ex;
            }
        }
        public void TrinhDuyet()
        {
            try
            {
                if (ProfileUtilities.User.USER_NAME.Equals(this.ObjDetail.CREATE_BY))
                {
                    this.ObjDetail.STATUS = ConstContract.TrinhDuyet;
                    UnitOfWork.BeginTransaction();
                    this.CurrentRepository.Update(this.ObjDetail);
                    //var history = new T_BU_CONTRACT_HISTORY()
                    //{
                    //    ID = Guid.NewGuid().ToString(),
                    //    ACTION = ConstContract.TrinhDuyet,
                    //    CREATE_BY = ProfileUtilities.User.USER_NAME,
                    //    OLD_VALUE = "Khởi tạo",
                    //    NEW_VALUE = "Trình duyệt",
                    //};
                    //UnitOfWork.Repository<ContractHistoryRepo>().Create(history);
                    var process = new T_BU_CONTRACT_PROCESS()
                    {
                        ID = Guid.NewGuid().ToString(),
                        ACTION = ConstContract.TrinhDuyet,
                        VERSION = this.ObjDetail.VERSION,
                        CREATE_BY = ProfileUtilities.User.USER_NAME,
                        CONTRACT_NAME = this.ObjDetail.NAME_CONTRACT
                    };
                    UnitOfWork.Repository<ContractProcessRepo>().Create(process);
                    UnitOfWork.Commit();

                }
                else
                {
                    this.State = false;

                }
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                this.State = false;
                this.Exception = ex;
            }

        }
        public void HuyTrinhDuyet()
        {
            try
            {
                if (ProfileUtilities.User.USER_NAME.Equals(this.ObjDetail.CREATE_BY))
                {
                    this.ObjDetail.STATUS = ConstContract.KhoiTao;
                    UnitOfWork.BeginTransaction();
                    this.CurrentRepository.Update(this.ObjDetail);
                    //var history = new T_BU_CONTRACT_HISTORY()
                    //{
                    //    ID = Guid.NewGuid().ToString(),
                    //    ACTION = ConstContract.TrinhDuyet,
                    //    CREATE_BY = ProfileUtilities.User.USER_NAME,
                    //    OLD_VALUE = "Khởi tạo",
                    //    NEW_VALUE = "Trình duyệt",
                    //};
                    //UnitOfWork.Repository<ContractHistoryRepo>().Create(history);
                    var process = new T_BU_CONTRACT_PROCESS()
                    {
                        ID = Guid.NewGuid().ToString(),
                        ACTION = ConstContract.HuyTrinhDuyet,
                        VERSION = this.ObjDetail.VERSION,
                        CREATE_BY = ProfileUtilities.User.USER_NAME,
                        CONTRACT_NAME = this.ObjDetail.NAME_CONTRACT
                    };
                    UnitOfWork.Repository<ContractProcessRepo>().Create(process);
                    UnitOfWork.Commit();

                }
                else
                {
                    this.State = false;

                }
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                this.State = false;
                this.Exception = ex;
            }

        }

        public void PheDuyet()
        {
            try
            {
                if (ProfileUtilities.User.USER_NAME.Equals(this.ObjDetail.APPROVER) || ProfileUtilities.User.USER_NAME.Equals("admin"))
                {
                    this.ObjDetail.STATUS = ConstContract.PheDuyet;
                    UnitOfWork.BeginTransaction();
                    this.CurrentRepository.Update(this.ObjDetail);
                    //var history = new T_BU_CONTRACT_HISTORY()
                    //{
                    //    ID = Guid.NewGuid().ToString(),
                    //    ACTION = ConstContract.TrinhDuyet,
                    //    CREATE_BY = ProfileUtilities.User.USER_NAME,
                    //    OLD_VALUE = "Khởi tạo",
                    //    NEW_VALUE = "Trình duyệt",
                    //};
                    //UnitOfWork.Repository<ContractHistoryRepo>().Create(history);
                    var process = new T_BU_CONTRACT_PROCESS()
                    {
                        ID = Guid.NewGuid().ToString(),
                        ACTION = ConstContract.PheDuyet,
                        VERSION = this.ObjDetail.VERSION,
                        CREATE_BY = ProfileUtilities.User.USER_NAME,
                        CONTRACT_NAME = this.ObjDetail.NAME_CONTRACT
                    };
                    UnitOfWork.Repository<ContractProcessRepo>().Create(process);
                    UnitOfWork.Commit();

                }
                else
                {
                    this.State = false;

                }
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                this.State = false;
                this.Exception = ex;
            }
        }
        public void TuChoi()
        {
            try
            {
                if (ProfileUtilities.User.USER_NAME.Equals(this.ObjDetail.APPROVER) || ProfileUtilities.User.USER_NAME.Equals("admin"))
                {
                    this.ObjDetail.STATUS = ConstContract.TuChoi;
                    UnitOfWork.BeginTransaction();
                    this.CurrentRepository.Update(this.ObjDetail);
                    //var history = new T_BU_CONTRACT_HISTORY()
                    //{
                    //    ID = Guid.NewGuid().ToString(),
                    //    ACTION = ConstContract.TrinhDuyet,
                    //    CREATE_BY = ProfileUtilities.User.USER_NAME,
                    //    OLD_VALUE = "Khởi tạo",
                    //    NEW_VALUE = "Trình duyệt",
                    //};
                    //UnitOfWork.Repository<ContractHistoryRepo>().Create(history);
                    var process = new T_BU_CONTRACT_PROCESS()
                    {
                        ID = Guid.NewGuid().ToString(),
                        ACTION = ConstContract.TuChoi,
                        VERSION = this.ObjDetail.VERSION,
                        CREATE_BY = ProfileUtilities.User.USER_NAME,
                        CONTRACT_NAME = this.ObjDetail.NAME_CONTRACT
                    };
                    UnitOfWork.Repository<ContractProcessRepo>().Create(process);
                    UnitOfWork.Commit();
                }
                else
                {
                    this.State = false;

                }
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                this.State = false;
                this.Exception = ex;
            }
        }
        public string getContractChild(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return "";
                }
                id = id.ToLower();
                var list = this.CurrentRepository.Queryable().Where(x => id.Equals(x.PARENT.ToLower())).ToList();
                foreach (var item in list)
                {
                    item.PHANTRAM = Math.Floor((getAmount(item.NAME_CONTRACT, item.VERSION) / item.CONTRACT_VALUE_VAT) * 100);
                }
                return converListContract(list);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public string converListContract(List<T_BU_CONTRACT> listContract)
        {
            var alertRed = UnitOfWork.Repository<ContractAlertRepo>().Queryable().FirstOrDefault(x => x.CODE == "RED").NUMBER;
            var alertYellow = UnitOfWork.Repository<ContractAlertRepo>().Queryable().FirstOrDefault(x => x.CODE == "YELLOW").NUMBER;

            var jsonSerializeSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var jsonData = listContract.Select((x, index) =>
            {
                bool isChild = (x.PARENT == null);
                return new
                {
                    isChild = false,
                    idContract = x.NAME_CONTRACT,
                    stt = index + 1,
                    nameContract = x.NAME,
                    numberContract = x.CONTRACT_NUMBER,
                    typeContract = x.ContractType?.TEXT,
                    nameCustomer = x.CustomerContract?.TEXT,
                    manager = x.ContractManager?.FULL_NAME,
                    department = x.CostCenter?.NAME,
                    valueOriginal = x.CONTRACT_VALUE.ToStringVN(),
                    signDay = x.SIGN_DAY.ToString("dd/MM/yyyy"),
                    startDate = x.START_DATE.ToString("dd/MM/yyyy"),
                    finishDate = x.FINISH_DATE.ToString("dd/MM/yyyy"),
                    statusCode = x.STATUS,
                    status = ConstContract.convertStatusToString(x.STATUS),
                    phase = ConstContract.convertPhaseToString(x.CONTRACT_PHASE),
                    version = x.VERSION,
                    progress = x.PHANTRAM,
                    file = x.FILE_CHILD,
                    alert = (x.FINISH_DATE - DateTime.Now).Days + 1 <= alertYellow && (x.FINISH_DATE - DateTime.Now).Days + 1 > alertRed ? "warning" : (x.FINISH_DATE - DateTime.Now).Days + 1 <= alertRed ? "danger" : "normal",
                    remainingDays = (x.FINISH_DATE - DateTime.Now).Days + 1,
                    createContract = ""
                };
            }).ToList();
            return JsonConvert.SerializeObject(jsonData, jsonSerializeSettings);
        }

        public void UpdateContract(HttpRequestBase Request)
        {
            try
            {
                State = true;
                ErrorMessage = "";
                if (string.IsNullOrEmpty(this.ObjDetail.NAME))
                {
                    ErrorMessage += "Vui lòng nhập tên hợp đồng!,";
                    State = false;
                }
                if (string.IsNullOrEmpty(this.ObjDetail.CONTRACT_NUMBER))
                {
                    ErrorMessage += "Vui lòng nhập số hợp đồng!,";
                    State = false;
                }
                if (this.ObjDetail.CONTRACT_VALUE_VAT <= 0)
                {
                    ErrorMessage += "Vui lòng nhập nhập giá trị hợp đồng!,";
                    State = false;
                }
                if (string.IsNullOrEmpty(this.ObjDetail.APPROVER))
                {
                    ErrorMessage += "Vui lòng chọn người trình duyệt hợp đồng!,";
                    State = false;
                }
                if (string.IsNullOrEmpty(this.ObjDetail.CONTRACT_MANAGER))
                {
                    ErrorMessage += "Vui lòng chọn người quản lý hợp đồng!,";
                    State = false;
                }
                if (string.IsNullOrEmpty(this.ObjDetail.CUSTOMER))
                {
                    ErrorMessage += "Vui nhập tên khách hàng!,";
                    State = false;
                }
                if (string.IsNullOrEmpty(this.ObjDetail.REPRESENT_A))
                {
                    ErrorMessage += "Vui lòng nhập bên A của hợp đồng!,";
                    State = false;
                }
                if (string.IsNullOrEmpty(this.ObjDetail.REPRESENT_B))
                {
                    ErrorMessage += "Vui lòng nhập bên B của hợp đồng!,";
                    State = false;
                }
                if (State == false)
                {
                    return;
                }

                var id = this.ObjDetail.ID;
                var contractOld = this.CurrentRepository.Get(id);
                //Validate oldValue
                contractOld.NOTES = contractOld.NOTES == null ? "" : contractOld.NOTES;
                // lưu file
                var lstFileStream = new List<FILE_STREAM>();
                for (int i = 0; i < Request.Files.AllKeys.Length; i++)
                {
                    var file = Request.Files[i];
                    lstFileStream.Add(new FILE_STREAM()
                    {
                        PKID = Guid.NewGuid().ToString(),
                        FILE_OLD_NAME = file.FileName,
                        FILE_NAME = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName),
                        FILE_EXT = Path.GetExtension(file.FileName),
                        FILE_SIZE = file.ContentLength,
                        FILESTREAM = Request.Files[i]
                    });
                }
                FileStreamService.InsertFile(lstFileStream);
                // hết lưu file
                // bắt đàu lưu vào db
                UnitOfWork.BeginTransaction();
                // check khác nhau lưu lịch sử
                if (!contractOld.NAME.Equals(this.ObjDetail.NAME))
                {
                    // tạo lịch sử
                    var history = new T_BU_CONTRACT_HISTORY()
                    {
                        ID = Guid.NewGuid().ToString(),
                        ACTION = ConstContract.SuaTen,
                        CREATE_BY = ProfileUtilities.User.USER_NAME,
                        OLD_VALUE = contractOld.NAME,
                        NEW_VALUE = this.ObjDetail.NAME,
                        VERSION = contractOld.VERSION,
                        NAME_CONTRACT = contractOld.NAME_CONTRACT,
                    };
                    contractOld.NAME = this.ObjDetail.NAME;

                    UnitOfWork.Repository<ContractHistoryRepo>().Create(history);
                }
                if (!contractOld.CONTRACT_NUMBER.Equals(this.ObjDetail.CONTRACT_NUMBER))
                {
                    // tạo lịch sử
                    var history = new T_BU_CONTRACT_HISTORY()
                    {
                        ID = Guid.NewGuid().ToString(),
                        ACTION = ConstContract.SuaSoHopDong,
                        CREATE_BY = ProfileUtilities.User.USER_NAME,
                        OLD_VALUE = contractOld.CONTRACT_NUMBER,
                        NEW_VALUE = this.ObjDetail.CONTRACT_NUMBER,
                        VERSION = contractOld.VERSION,
                        NAME_CONTRACT = contractOld.NAME_CONTRACT,

                    };
                    contractOld.CONTRACT_NUMBER = this.ObjDetail.CONTRACT_NUMBER;

                    UnitOfWork.Repository<ContractHistoryRepo>().Create(history);
                }
                if (contractOld.CONTRACT_TYPE != null)
                {
                    if (!contractOld.CONTRACT_TYPE.Equals(this.ObjDetail.CONTRACT_TYPE))
                    {
                        // tạo lịch sử
                        var history = new T_BU_CONTRACT_HISTORY()
                        {
                            ID = Guid.NewGuid().ToString(),
                            ACTION = ConstContract.SuaLoaiHopDong,
                            CREATE_BY = ProfileUtilities.User.USER_NAME,
                            OLD_VALUE = contractOld.CONTRACT_TYPE,
                            NEW_VALUE = this.ObjDetail.CONTRACT_TYPE,
                            VERSION = contractOld.VERSION,
                            NAME_CONTRACT = contractOld.NAME_CONTRACT,

                        };
                        UnitOfWork.Repository<ContractHistoryRepo>().Create(history);
                    }
                }
                contractOld.CONTRACT_TYPE = this.ObjDetail.CONTRACT_TYPE;

                if (!contractOld.CUSTOMER.Equals(this.ObjDetail.CUSTOMER))
                {
                    // tạo lịch sử
                    var history = new T_BU_CONTRACT_HISTORY()
                    {
                        ID = Guid.NewGuid().ToString(),
                        ACTION = ConstContract.SuaKhachHang,
                        CREATE_BY = ProfileUtilities.User.USER_NAME,
                        OLD_VALUE = contractOld.CUSTOMER,
                        NEW_VALUE = this.ObjDetail.CUSTOMER,
                        VERSION = contractOld.VERSION,
                        NAME_CONTRACT = contractOld.NAME_CONTRACT,

                    };
                    contractOld.CUSTOMER = this.ObjDetail.CUSTOMER;

                    UnitOfWork.Repository<ContractHistoryRepo>().Create(history);
                }
                if (!contractOld.DEPARTMENT.Equals(this.ObjDetail.DEPARTMENT))
                {
                    // tạo lịch sử
                    var history = new T_BU_CONTRACT_HISTORY()
                    {
                        ID = Guid.NewGuid().ToString(),
                        ACTION = ConstContract.SuaPhongBanPhuTrach,
                        CREATE_BY = ProfileUtilities.User.USER_NAME,
                        OLD_VALUE = contractOld.DEPARTMENT,
                        NEW_VALUE = this.ObjDetail.DEPARTMENT,
                        VERSION = contractOld.VERSION,
                        NAME_CONTRACT = contractOld.NAME_CONTRACT,
                    };
                    contractOld.DEPARTMENT = this.ObjDetail.DEPARTMENT;

                    UnitOfWork.Repository<ContractHistoryRepo>().Create(history);
                }
                if (!contractOld.CONTRACT_PHASE.Equals(this.ObjDetail.CONTRACT_PHASE))
                {
                    // tạo lịch sử
                    var history = new T_BU_CONTRACT_HISTORY()
                    {
                        ID = Guid.NewGuid().ToString(),
                        ACTION = ConstContract.SuaGiaiDoanHopDong,
                        CREATE_BY = ProfileUtilities.User.USER_NAME,
                        OLD_VALUE = contractOld.CONTRACT_PHASE,
                        NEW_VALUE = this.ObjDetail.CONTRACT_PHASE,
                        VERSION = contractOld.VERSION,
                        NAME_CONTRACT = contractOld.NAME_CONTRACT,

                    };
                    contractOld.CONTRACT_PHASE = this.ObjDetail.CONTRACT_PHASE;

                    UnitOfWork.Repository<ContractHistoryRepo>().Create(history);
                }
                if (!contractOld.SIGN_DAY.Equals(this.ObjDetail.SIGN_DAY))
                {
                    // tạo lịch sử
                    var history = new T_BU_CONTRACT_HISTORY()
                    {
                        ID = Guid.NewGuid().ToString(),
                        ACTION = ConstContract.SuaNgayKyKet,
                        CREATE_BY = ProfileUtilities.User.USER_NAME,
                        OLD_VALUE = contractOld.SIGN_DAY.ToString("dd/MM/yyyy"),
                        NEW_VALUE = this.ObjDetail.SIGN_DAY.ToString("dd/MM/yyyy"),
                        VERSION = contractOld.VERSION,
                        NAME_CONTRACT = contractOld.NAME_CONTRACT,

                    };
                    contractOld.SIGN_DAY = this.ObjDetail.SIGN_DAY;

                    UnitOfWork.Repository<ContractHistoryRepo>().Create(history);
                }
                if (!contractOld.START_DATE.Equals(this.ObjDetail.START_DATE))
                {
                    // tạo lịch sử
                    var history = new T_BU_CONTRACT_HISTORY()
                    {
                        ID = Guid.NewGuid().ToString(),
                        ACTION = ConstContract.SuaNgayBatDau,
                        CREATE_BY = ProfileUtilities.User.USER_NAME,
                        OLD_VALUE = contractOld.START_DATE.ToString("dd/MM/yyyy"),
                        NEW_VALUE = this.ObjDetail.START_DATE.ToString("dd/MM/yyyy"),
                        VERSION = contractOld.VERSION,
                        NAME_CONTRACT = contractOld.NAME_CONTRACT,

                    };
                    contractOld.START_DATE = this.ObjDetail.START_DATE;

                    UnitOfWork.Repository<ContractHistoryRepo>().Create(history);
                }
                if (!contractOld.FINISH_DATE.Equals(this.ObjDetail.FINISH_DATE))
                {
                    // tạo lịch sử
                    var history = new T_BU_CONTRACT_HISTORY()
                    {
                        ID = Guid.NewGuid().ToString(),
                        ACTION = ConstContract.SuaNgayKetThuc,
                        CREATE_BY = ProfileUtilities.User.USER_NAME,
                        OLD_VALUE = contractOld.FINISH_DATE.ToString("dd/MM/yyyy"),
                        NEW_VALUE = this.ObjDetail.FINISH_DATE.ToString("dd/MM/yyyy"),
                        VERSION = contractOld.VERSION,
                        NAME_CONTRACT = contractOld.NAME_CONTRACT,

                    };
                    contractOld.FINISH_DATE = this.ObjDetail.FINISH_DATE;

                    UnitOfWork.Repository<ContractHistoryRepo>().Create(history);
                }
                if (!contractOld.CONTRACT_VALUE.Equals(this.ObjDetail.CONTRACT_VALUE))
                {
                    // tạo lịch sử
                    var history = new T_BU_CONTRACT_HISTORY()
                    {
                        ID = Guid.NewGuid().ToString(),
                        ACTION = ConstContract.SuaGiaTriHopDong,
                        CREATE_BY = ProfileUtilities.User.USER_NAME,
                        OLD_VALUE = contractOld.CONTRACT_VALUE.ToString(),
                        NEW_VALUE = this.ObjDetail.CONTRACT_VALUE.ToString(),
                        VERSION = contractOld.VERSION,
                        NAME_CONTRACT = contractOld.NAME_CONTRACT,

                    };
                    contractOld.CONTRACT_VALUE = this.ObjDetail.CONTRACT_VALUE;

                    UnitOfWork.Repository<ContractHistoryRepo>().Create(history);
                }
                if (!contractOld.VAT.Equals(this.ObjDetail.VAT))
                {
                    // tạo lịch sử
                    var history = new T_BU_CONTRACT_HISTORY()
                    {
                        ID = Guid.NewGuid().ToString(),
                        ACTION = ConstContract.SuaVat,
                        CREATE_BY = ProfileUtilities.User.USER_NAME,
                        OLD_VALUE = contractOld.VAT.ToString(),
                        NEW_VALUE = this.ObjDetail.VAT.ToString(),
                        VERSION = contractOld.VERSION,
                        NAME_CONTRACT = contractOld.NAME_CONTRACT,

                    };
                    contractOld.VAT = this.ObjDetail.VAT;

                    UnitOfWork.Repository<ContractHistoryRepo>().Create(history);
                }
                if (!contractOld.CONTRACT_VALUE_VAT.Equals(this.ObjDetail.CONTRACT_VALUE_VAT))
                {
                    // tạo lịch sử
                    var history = new T_BU_CONTRACT_HISTORY()
                    {
                        ID = Guid.NewGuid().ToString(),
                        ACTION = ConstContract.SuaVat,
                        CREATE_BY = ProfileUtilities.User.USER_NAME,
                        OLD_VALUE = contractOld.CONTRACT_VALUE_VAT.ToString(),
                        NEW_VALUE = this.ObjDetail.CONTRACT_VALUE_VAT.ToString(),
                        VERSION = contractOld.VERSION,
                        NAME_CONTRACT = contractOld.NAME_CONTRACT,

                    };
                    contractOld.CONTRACT_VALUE_VAT = this.ObjDetail.CONTRACT_VALUE_VAT;

                    UnitOfWork.Repository<ContractHistoryRepo>().Create(history);
                }
                if (!contractOld.REPRESENT_A.Equals(this.ObjDetail.REPRESENT_A))
                {
                    // tạo lịch sử
                    var history = new T_BU_CONTRACT_HISTORY()
                    {
                        ID = Guid.NewGuid().ToString(),
                        ACTION = ConstContract.SuaDaiDienA,
                        CREATE_BY = ProfileUtilities.User.USER_NAME,
                        OLD_VALUE = contractOld.REPRESENT_A.ToString(),
                        NEW_VALUE = this.ObjDetail.REPRESENT_A.ToString(),
                        VERSION = contractOld.VERSION,
                        NAME_CONTRACT = contractOld.NAME_CONTRACT,

                    };
                    contractOld.REPRESENT_A = this.ObjDetail.REPRESENT_A;

                    UnitOfWork.Repository<ContractHistoryRepo>().Create(history);
                }
                if (!contractOld.REPRESENT_B.Equals(this.ObjDetail.REPRESENT_B))
                {
                    // tạo lịch sử
                    var history = new T_BU_CONTRACT_HISTORY()
                    {
                        ID = Guid.NewGuid().ToString(),
                        ACTION = ConstContract.SuaDaiDienB,
                        CREATE_BY = ProfileUtilities.User.USER_NAME,
                        OLD_VALUE = contractOld.REPRESENT_B.ToString(),
                        NEW_VALUE = this.ObjDetail.REPRESENT_B.ToString(),
                        VERSION = contractOld.VERSION,
                        NAME_CONTRACT = contractOld.NAME_CONTRACT,

                    };
                    contractOld.REPRESENT_B = this.ObjDetail.REPRESENT_B;

                    UnitOfWork.Repository<ContractHistoryRepo>().Create(history);
                }
                if (!contractOld.NOTES.Equals(this.ObjDetail.NOTES))
                {
                    // tạo lịch sử
                    var history = new T_BU_CONTRACT_HISTORY()
                    {
                        ID = Guid.NewGuid().ToString(),
                        ACTION = ConstContract.SuaMoTa,
                        CREATE_BY = ProfileUtilities.User.USER_NAME,
                        OLD_VALUE = contractOld.NOTES?.ToString(),
                        NEW_VALUE = this.ObjDetail.NOTES?.ToString(),
                        VERSION = contractOld.VERSION,
                        NAME_CONTRACT = contractOld.NAME_CONTRACT,

                    };
                    contractOld.NOTES = this.ObjDetail.NOTES;

                    UnitOfWork.Repository<ContractHistoryRepo>().Create(history);
                }
                if (!contractOld.APPROVER.Equals(this.ObjDetail.APPROVER))
                {
                    // tạo lịch sử
                    var history = new T_BU_CONTRACT_HISTORY()
                    {
                        ID = Guid.NewGuid().ToString(),
                        ACTION = ConstContract.SuaNguoiPheDuyet,
                        CREATE_BY = ProfileUtilities.User.USER_NAME,
                        OLD_VALUE = contractOld.APPROVER.ToString(),
                        NEW_VALUE = this.ObjDetail.APPROVER.ToString(),
                        VERSION = contractOld.VERSION,
                        NAME_CONTRACT = contractOld.NAME_CONTRACT,

                    };
                    contractOld.APPROVER = this.ObjDetail.APPROVER;

                    UnitOfWork.Repository<ContractHistoryRepo>().Create(history);
                }
                if (!contractOld.CONTRACT_MANAGER.Equals(this.ObjDetail.CONTRACT_MANAGER))
                {
                    // tạo lịch sử
                    var history = new T_BU_CONTRACT_HISTORY()
                    {
                        ID = Guid.NewGuid().ToString(),
                        ACTION = ConstContract.SuaNguoiQuanLy,
                        CREATE_BY = ProfileUtilities.User.USER_NAME,
                        OLD_VALUE = contractOld.CONTRACT_MANAGER.ToString(),
                        NEW_VALUE = this.ObjDetail.CONTRACT_MANAGER.ToString(),
                        VERSION = contractOld.VERSION,
                        NAME_CONTRACT = contractOld.NAME_CONTRACT,

                    };
                    contractOld.CONTRACT_MANAGER = this.ObjDetail.CONTRACT_MANAGER;

                    UnitOfWork.Repository<ContractHistoryRepo>().Create(history);
                }
                List<string> listFl = this.UnitOfWork.Repository<ContractHumanRepo>().Queryable().Where(x => x.CONTRACT_ID == contractOld.NAME_CONTRACT).Select(x => x.USERNAME).ToList();
                List<string> listFlAdd = new List<string>();
                List<string> listFlRemove = new List<string>();
                foreach (var item in listFl)
                {
                    if (!this.ObjListUser.Contains(item))
                    {
                        listFlAdd.Add(item);
                    }
                }
                foreach (var item in this.ObjListUser)
                {
                    if (!this.ObjListUser.Contains(item))
                    {
                        listFlRemove.Add(item);
                    }
                }
                foreach (var item in listFlAdd)
                {
                    var follower = new T_BU_CONTRACT_HUMAN()
                    {
                        ID = Guid.NewGuid().ToString(),
                        USERNAME = item,
                        CONTRACT_ID = id,
                        ACTION = ConstContract.NguoiTheoDoi,
                        CREATE_BY = ProfileUtilities.User.USER_NAME,
                        VERSION = contractOld.VERSION,
                    };
                    UnitOfWork.Repository<ContractHumanRepo>().Create(follower);
                    var history = new T_BU_CONTRACT_HISTORY()
                    {
                        ID = Guid.NewGuid().ToString(),
                        ACTION = ConstContract.ThemNguoiTheoDoi,
                        CREATE_BY = ProfileUtilities.User.USER_NAME,
                        OLD_VALUE = "",
                        NEW_VALUE = item,
                        VERSION = contractOld.VERSION,
                        NAME_CONTRACT = contractOld.NAME_CONTRACT,

                    };
                    UnitOfWork.Repository<ContractHistoryRepo>().Create(history);
                }
                foreach (var item in listFlRemove)
                {
                    var follower = UnitOfWork.Repository<ContractHumanRepo>().Queryable().Where(u => u.USERNAME == item && u.CONTRACT_ID == contractOld.NAME_CONTRACT && u.VERSION == contractOld.VERSION).FirstOrDefault();
                    UnitOfWork.Repository<ContractHumanRepo>().Delete(follower);
                    var history = new T_BU_CONTRACT_HISTORY()
                    {
                        ID = Guid.NewGuid().ToString(),
                        ACTION = ConstContract.XoaNguoiTheoDoi,
                        CREATE_BY = ProfileUtilities.User.USER_NAME,
                        OLD_VALUE = item,
                        NEW_VALUE = "",
                        VERSION = contractOld.VERSION,

                    };
                    UnitOfWork.Repository<ContractHistoryRepo>().Create(history);
                }
                // lưu file vào db
                foreach (var item in lstFileStream)
                {
                    var fileUpload = new T_BU_FILE_UPLOAD()
                    {
                        ID = item.PKID,
                        //CONNECTION_ID = systemConfigService.ObjDetail.CURRENT_CONNECTION,
                        //DATABASE_NAME = systemConfigService.ObjDetail.CURRENT_DATABASE_NAME,                           
                        FILE_EXT = item.FILE_EXT,
                        FILE_NAME = item.FILE_NAME,
                        FILE_OLD_NAME = item.FILE_OLD_NAME,
                        FILE_SIZE = item.FILE_SIZE,
                        DIRECTORY_PATH = item.DIRECTORY_PATH,
                        FULL_PATH = item.FULL_PATH,
                        PARENT = contractOld.FILE_CHILD,
                        CREATE_BY = ProfileUtilities.User.USER_NAME,
                        IS_DELETE = false,
                        VERSION = contractOld.VERSION,
                    };
                    UnitOfWork.Repository<Repository.Implement.BU.FileUploadRepo>().Create(fileUpload);
                    var history = new T_BU_CONTRACT_HISTORY()
                    {
                        ID = Guid.NewGuid().ToString(),
                        ACTION = ConstContract.ThemFile,
                        CREATE_BY = ProfileUtilities.User.USER_NAME,
                        OLD_VALUE = "",
                        NEW_VALUE = item.FILE_OLD_NAME,
                        VERSION = contractOld.VERSION,
                        NAME_CONTRACT = contractOld.NAME_CONTRACT,


                    };
                    UnitOfWork.Repository<ContractHistoryRepo>().Create(history);
                }
                foreach (var item in listFileRemove)
                {
                    var fileRemove = UnitOfWork.Repository<Repository.Implement.BU.FileUploadRepo>().Get(item);
                    fileRemove.IS_DELETE = true;
                    UnitOfWork.Repository<Repository.Implement.BU.FileUploadRepo>().Update(fileRemove);
                    var history = new T_BU_CONTRACT_HISTORY()
                    {
                        ID = Guid.NewGuid().ToString(),
                        ACTION = ConstContract.XoaFile,
                        CREATE_BY = ProfileUtilities.User.USER_NAME,
                        OLD_VALUE = fileRemove.FILE_OLD_NAME,
                        NEW_VALUE = "",
                        VERSION = contractOld.VERSION,
                        NAME_CONTRACT = contractOld.NAME_CONTRACT,

                    };
                    UnitOfWork.Repository<ContractHistoryRepo>().Create(history);
                }
                foreach (var item in this.listLink)
                {
                    var fileUpload = new T_BU_LINK_UPLOAD()
                    {
                        ID = Guid.NewGuid().ToString(),
                        LINK = item,
                        PARENT = contractOld.FILE_CHILD,
                        VERSION = contractOld.VERSION,
                        CREATE_BY = ProfileUtilities.User.USER_NAME,
                    };
                    UnitOfWork.Repository<LinkUploadRepo>().Create(fileUpload);
                    var history = new T_BU_CONTRACT_HISTORY()
                    {
                        ID = Guid.NewGuid().ToString(),
                        ACTION = ConstContract.ThemLink,
                        CREATE_BY = ProfileUtilities.User.USER_NAME,
                        OLD_VALUE = "",
                        NEW_VALUE = item,
                        VERSION = contractOld.VERSION,
                        NAME_CONTRACT = contractOld.NAME_CONTRACT,

                    };
                    UnitOfWork.Repository<ContractHistoryRepo>().Create(history);
                }
                foreach (var item in listLinkRemove)
                {
                    var LinkRemove = UnitOfWork.Repository<LinkUploadRepo>().Get(item);
                    LinkRemove.IS_DELETE = true;
                    UnitOfWork.Repository<LinkUploadRepo>().Update(LinkRemove);
                    var history = new T_BU_CONTRACT_HISTORY()
                    {
                        ID = Guid.NewGuid().ToString(),
                        ACTION = ConstContract.XoaLink,
                        CREATE_BY = ProfileUtilities.User.USER_NAME,
                        OLD_VALUE = LinkRemove.LINK,
                        NEW_VALUE = "",
                        VERSION = contractOld.VERSION,

                    };
                    UnitOfWork.Repository<ContractHistoryRepo>().Create(history);
                }
                this.CurrentRepository.Update(contractOld);

                UnitOfWork.Commit();

            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                this.State = false;
                this.Exception = ex;
            }
        }

        public void CreateVersionContract(HttpRequestBase Request)
        {
            try
            {
                State = true;
                ErrorMessage = "";
                if (string.IsNullOrEmpty(this.ObjDetail.NAME))
                {
                    ErrorMessage += "Vui lòng nhập tên hợp đồng!,";
                    State = false;
                }
                if (string.IsNullOrEmpty(this.ObjDetail.CONTRACT_NUMBER))
                {
                    ErrorMessage += "Vui lòng nhập số hợp đồng!,";
                    State = false;
                }
                if (this.ObjDetail.CONTRACT_VALUE_VAT <= 0)
                {
                    ErrorMessage += "Vui lòng nhập nhập giá trị hợp đồng!,";
                    State = false;
                }
                if (string.IsNullOrEmpty(this.ObjDetail.APPROVER))
                {
                    ErrorMessage += "Vui lòng chọn người trình duyệt hợp đồng!,";
                    State = false;
                }
                if (string.IsNullOrEmpty(this.ObjDetail.CONTRACT_MANAGER))
                {
                    ErrorMessage += "Vui lòng chọn người quản lý hợp đồng!,";
                    State = false;
                }
                if (string.IsNullOrEmpty(this.ObjDetail.CUSTOMER))
                {
                    ErrorMessage += "Vui nhập tên khách hàng!,";
                    State = false;
                }
                if (string.IsNullOrEmpty(this.ObjDetail.REPRESENT_A))
                {
                    ErrorMessage += "Vui lòng nhập bên A của hợp đồng!,";
                    State = false;
                }
                if (string.IsNullOrEmpty(this.ObjDetail.REPRESENT_B))
                {
                    ErrorMessage += "Vui lòng nhập bên B của hợp đồng!,";
                    State = false;
                }
                if (State == false)
                {
                    return;
                }

                var id = this.ObjDetail.ID;
                var contractOld = this.CurrentRepository.Get(id);
                // lưu file
                var lstFileStream = new List<FILE_STREAM>();
                for (int i = 0; i < Request.Files.AllKeys.Length; i++)
                {
                    var file = Request.Files[i];
                    lstFileStream.Add(new FILE_STREAM()
                    {
                        PKID = Guid.NewGuid().ToString(),
                        FILE_OLD_NAME = file.FileName,
                        FILE_NAME = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName),
                        FILE_EXT = Path.GetExtension(file.FileName),
                        FILE_SIZE = file.ContentLength,
                        FILESTREAM = Request.Files[i]
                    });
                }
                FileStreamService.InsertFile(lstFileStream);
                // hết lưu file
                // bắt đàu lưu vào db
                UnitOfWork.BeginTransaction();
                var oldVersion = new T_BU_CONTRACT_VERSION()
                {
                    ID = Guid.NewGuid().ToString(),
                    NAME = contractOld.NAME,
                    NAME_CONTRACT = contractOld.NAME_CONTRACT,
                    VERSION = contractOld.VERSION,
                    CONTRACT_MANAGER = contractOld.CONTRACT_MANAGER,
                    CONTRACT_NUMBER = contractOld.CONTRACT_NUMBER,
                    CONTRACT_PHASE = contractOld.CONTRACT_PHASE,
                    CONTRACT_TYPE = contractOld.CONTRACT_TYPE,
                    CONTRACT_VALUE = contractOld.CONTRACT_VALUE,
                    VAT = contractOld.CONTRACT_VALUE,
                    CONTRACT_VALUE_VAT = contractOld.CONTRACT_VALUE_VAT,
                    CREATE_BY = contractOld.CREATE_BY,
                    UPDATE_DATE = contractOld.CREATE_DATE,
                    FILE_CHILD = contractOld.FILE_CHILD,
                    PARENT = contractOld.PARENT,
                    APPROVER = contractOld.APPROVER,
                    REPRESENT_A = contractOld.REPRESENT_A,
                    REPRESENT_B = contractOld.REPRESENT_B,
                    NOTES = contractOld.NOTES,
                    STATUS = contractOld.STATUS,
                    START_DATE = contractOld.START_DATE,
                    FINISH_DATE = contractOld.FINISH_DATE,
                    DEPARTMENT = contractOld.DEPARTMENT,
                    CUSTOMER = contractOld.CUSTOMER,
                };
                contractOld.NAME = this.ObjDetail.NAME;
                contractOld.VERSION = contractOld.VERSION + 1;
                contractOld.CONTRACT_MANAGER = this.ObjDetail.CONTRACT_MANAGER;
                contractOld.CONTRACT_NUMBER = this.ObjDetail.CONTRACT_NUMBER;
                contractOld.CONTRACT_PHASE = this.ObjDetail.CONTRACT_PHASE;
                contractOld.CONTRACT_TYPE = this.ObjDetail.CONTRACT_TYPE;
                contractOld.CONTRACT_VALUE = this.ObjDetail.CONTRACT_VALUE;
                contractOld.VAT = this.ObjDetail.CONTRACT_VALUE;
                contractOld.CONTRACT_VALUE_VAT = this.ObjDetail.CONTRACT_VALUE_VAT;
                contractOld.CREATE_BY = this.ObjDetail.CREATE_BY;
                contractOld.CREATE_DATE = this.ObjDetail.UPDATE_DATE;
                contractOld.PARENT = this.ObjDetail.PARENT;
                contractOld.APPROVER = this.ObjDetail.APPROVER;
                contractOld.REPRESENT_A = this.ObjDetail.REPRESENT_A;
                contractOld.REPRESENT_B = this.ObjDetail.REPRESENT_B;
                contractOld.NOTES = this.ObjDetail.NOTES;
                contractOld.STATUS = contractOld.STATUS;
                contractOld.START_DATE = this.ObjDetail.START_DATE;
                contractOld.FINISH_DATE = this.ObjDetail.FINISH_DATE;
                contractOld.DEPARTMENT = this.ObjDetail.DEPARTMENT;
                contractOld.CUSTOMER = this.ObjDetail.CUSTOMER;
                this.CurrentRepository.Update(contractOld);
                UnitOfWork.Repository<ContractVersionRepo>().Create(oldVersion);
                var listFileRemoveDB = UnitOfWork.Repository<SMO.Repository.Implement.BU.FileUploadRepo>().Queryable().Where(x => x.IS_DELETE == false && x.PARENT == oldVersion.NAME_CONTRACT && x.VERSION == oldVersion.VERSION).ToList();
                foreach (var item in listFileRemoveDB)
                {
                    if (!listFileRemove.Contains(item.ID))
                    {
                        var idFile = Guid.NewGuid().ToString();
                        // var fileStream = UnitOfWork.Repository<SMO.Repository.Implement.CM.FileUploadRepo>().Get(item.ID);
                        var fileUpload = new T_BU_FILE_UPLOAD()
                        {
                            ID = idFile,
                            //CONNECTION_ID = systemConfigService.ObjDetail.CURRENT_CONNECTION,
                            //DATABASE_NAME = systemConfigService.ObjDetail.CURRENT_DATABASE_NAME,                           
                            FILE_EXT = item.FILE_EXT,
                            FILE_NAME = item.FILE_NAME,
                            FILE_OLD_NAME = item.FILE_OLD_NAME,
                            FILE_SIZE = item.FILE_SIZE,
                            DIRECTORY_PATH = item.DIRECTORY_PATH,
                            FULL_PATH = item.FULL_PATH,
                            PARENT = contractOld.FILE_CHILD,
                            CREATE_BY = ProfileUtilities.User.USER_NAME,
                            IS_DELETE = false,
                            VERSION = contractOld.VERSION,
                        };
                        var newFileStream = new T_CM_FILE_UPLOAD()
                        {
                            PKID = idFile,
                            FILE_EXT = item.FILE_EXT,
                            FILE_NAME = item.FILE_NAME,
                            FILE_OLD_NAME = item.FILE_OLD_NAME,
                            FILE_SIZE = item.FILE_SIZE,
                            DIRECTORY_PATH = item.DIRECTORY_PATH,
                            CREATE_BY = ProfileUtilities.User.USER_NAME,
                        };
                        UnitOfWork.Repository<SMO.Repository.Implement.BU.FileUploadRepo>().Create(fileUpload);
                        UnitOfWork.Repository<SMO.Repository.Implement.CM.FileUploadRepo>().Create(newFileStream);

                    }
                }
                var listLinkRemoveDB = UnitOfWork.Repository<SMO.Repository.Implement.BU.LinkUploadRepo>().Queryable().Where(x => x.IS_DELETE == false && x.PARENT == oldVersion.NAME_CONTRACT && x.VERSION == oldVersion.VERSION).ToList();
                foreach (var item in listLinkRemoveDB)
                {
                    if (!listLinkRemove.Contains(item.LINK))
                    {
                        var idLink = Guid.NewGuid().ToString();
                        var link = new T_BU_LINK_UPLOAD()
                        {
                            ID = idLink,
                            LINK = item.LINK,
                            PARENT = contractOld.FILE_CHILD,
                            CREATE_BY = ProfileUtilities.User.USER_NAME,
                            IS_DELETE = false,
                            VERSION = contractOld.VERSION,
                        };
                        UnitOfWork.Repository<SMO.Repository.Implement.BU.LinkUploadRepo>().Create(link);
                    }
                }
                foreach (var item in ObjListUser)
                {
                    var follower = new T_BU_CONTRACT_HUMAN()
                    {
                        ID = Guid.NewGuid().ToString(),
                        USERNAME = item,
                        CONTRACT_ID = contractOld.NAME_CONTRACT,
                        ACTION = ConstContract.NguoiTheoDoi,
                        CREATE_BY = ProfileUtilities.User.USER_NAME,
                        VERSION = contractOld.VERSION,
                    };
                    UnitOfWork.Repository<ContractHumanRepo>().Create(follower);
                }
                // thanh toán
                //var listPaymentNew = UnitOfWork.Repository<SMO.Repository.Implement.BU.PaymentRepo>().Queryable().Where(x => x.IS_DELETE == false && x.NAME_CONTRACT == oldVersion.NAME_CONTRACT && x.VERSION == oldVersion.VERSION).ToList();
                //foreach (var item in listPaymentNew)
                //{
                //    var PaymentNew = new T_BU_PAYMENT()
                //    {                      
                //        ACTION = ConstContract.NguoiTheoDoi,
                //        CREATE_BY = ProfileUtilities.User.USER_NAME,
                //        VERSION = contractOld.VERSION,
                //        // Điền các thuộc tính còn thiếu
                //        ID = Guid.NewGuid().ToString(), 
                //        NUMBER_INVOICE = item.NUMBER_INVOICE, 
                //        VALUE_INVOICE = item.VALUE_INVOICE, 
                //        AMOUNT = item.AMOUNT, 
                //        ADVANCE_PAYMENT = item.ADVANCE_PAYMENT,
                //        TOTAL = item.TOTAL, 
                //        NOTE = item.NOTE, 
                //        DATE = item.DATE, 
                //        CONTENT_PAYMENT = item.CONTENT_PAYMENT, 
                //        FILE_CHILD = item.FILE_CHILD,
                //        NAME_CONTRACT = item.NAME_CONTRACT, 
                //        UPDATE_HUMAN = item.UPDATE_HUMAN, 
                //        UPDATE_TIME = DateTime.Now,
                //        IS_DELETE = false 
                //    };
                //    UnitOfWork.Repository<PaymentRepo>().Create(PaymentNew);
                //    var listFilePaymentDB = UnitOfWork.Repository<SMO.Repository.Implement.BU.FileUploadRepo>().Queryable().Where(x => x.IS_DELETE == false && x.PARENT == item.FILE_CHILD && x.VERSION == item.VERSION).ToList();
                //    foreach (var item2 in listFilePaymentDB)
                //    {

                //        var idFile = Guid.NewGuid().ToString();
                //       // var fileStream = UnitOfWork.Repository<SMO.Repository.Implement.CM.FileUploadRepo>().Get(item2.ID);
                //        var fileUpload = new T_BU_FILE_UPLOAD()
                //        {
                //            ID = idFile,
                //            //CONNECTION_ID = systemConfigService.ObjDetail.CURRENT_CONNECTION,
                //            //DATABASE_NAME = systemConfigService.ObjDetail.CURRENT_DATABASE_NAME,                           
                //            FILE_EXT = item2.FILE_EXT,
                //            FILE_NAME = item2.FILE_NAME,
                //            FILE_OLD_NAME = item2.FILE_OLD_NAME,
                //            FILE_SIZE = item2.FILE_SIZE,
                //            DIRECTORY_PATH = item2.DIRECTORY_PATH,
                //            FULL_PATH = item2.FULL_PATH,
                //            PARENT = item.FILE_CHILD,
                //            CREATE_BY = ProfileUtilities.User.USER_NAME,
                //            IS_DELETE = false,
                //            VERSION = contractOld.VERSION,
                //        };
                //        var newFileStream = new T_CM_FILE_UPLOAD()
                //        {
                //            PKID = idFile,
                //            FILE_EXT = item2.FILE_EXT,
                //            FILE_NAME = item2.FILE_NAME,
                //            FILE_OLD_NAME = item2.FILE_OLD_NAME,
                //            FILE_SIZE = item2.FILE_SIZE,
                //            DIRECTORY_PATH = item2.DIRECTORY_PATH,
                //            CREATE_BY = ProfileUtilities.User.USER_NAME,
                //        };
                //        UnitOfWork.Repository<SMO.Repository.Implement.BU.FileUploadRepo>().Create(fileUpload);
                //        UnitOfWork.Repository<SMO.Repository.Implement.CM.FileUploadRepo>().Create(newFileStream);

                //    }
                //}

                // lưu file vào db
                foreach (var item in lstFileStream)
                {
                    var fileUpload = new T_BU_FILE_UPLOAD()
                    {
                        ID = item.PKID,
                        //CONNECTION_ID = systemConfigService.ObjDetail.CURRENT_CONNECTION,
                        //DATABASE_NAME = systemConfigService.ObjDetail.CURRENT_DATABASE_NAME,                           
                        FILE_EXT = item.FILE_EXT,
                        FILE_NAME = item.FILE_NAME,
                        FILE_OLD_NAME = item.FILE_OLD_NAME,
                        FILE_SIZE = item.FILE_SIZE,
                        DIRECTORY_PATH = item.DIRECTORY_PATH,
                        FULL_PATH = item.FULL_PATH,
                        PARENT = contractOld.FILE_CHILD,
                        CREATE_BY = ProfileUtilities.User.USER_NAME,
                        IS_DELETE = false,
                        VERSION = contractOld.VERSION,
                    };
                    UnitOfWork.Repository<SMO.Repository.Implement.BU.FileUploadRepo>().Create(fileUpload);
                }
                // lịch sử
                var history = new T_BU_CONTRACT_HISTORY()
                {
                    ID = Guid.NewGuid().ToString(),
                    ACTION = ConstContract.TaoVersionMoi,
                    CREATE_BY = ProfileUtilities.User.USER_NAME,
                    OLD_VALUE = (contractOld.VERSION - 1).ToString(),
                    NEW_VALUE = (contractOld.VERSION).ToString(),
                    VERSION = contractOld.VERSION,
                    NAME_CONTRACT = contractOld.NAME_CONTRACT,
                };
                UnitOfWork.Repository<ContractHistoryRepo>().Create(history);
                UnitOfWork.Commit();

            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                this.State = false;
                this.Exception = ex;
            }
        }

        public static T_BU_CONTRACT ConvertContractToContractVersion(T_BU_CONTRACT_VERSION contractVersion)
        {
            T_BU_CONTRACT contract = new T_BU_CONTRACT()
            {
                ID = contractVersion.ID,
                PARENT = contractVersion.PARENT,
                NAME = contractVersion.NAME,
                CONTRACT_NUMBER = contractVersion.CONTRACT_NUMBER,
                CONTRACT_TYPE = contractVersion.CONTRACT_TYPE,
                START_DATE = contractVersion.START_DATE,
                FINISH_DATE = contractVersion.FINISH_DATE,
                CONTRACT_VALUE = contractVersion.CONTRACT_VALUE,
                VAT = contractVersion.VAT,
                CONTRACT_VALUE_VAT = contractVersion.CONTRACT_VALUE_VAT,
                NOTES = contractVersion.NOTES,
                REPRESENT_A = contractVersion.REPRESENT_A,
                REPRESENT_B = contractVersion.REPRESENT_B,
                VERSION = contractVersion.VERSION,
                CONTRACT_PHASE = contractVersion.CONTRACT_PHASE,
                APPROVER = contractVersion.APPROVER,
                CONTRACT_MANAGER = contractVersion.CONTRACT_MANAGER,
                DEPARTMENT = contractVersion.DEPARTMENT,
                CUSTOMER = contractVersion.CUSTOMER,
                STATUS = contractVersion.STATUS,
                FILE_CHILD = contractVersion.FILE_CHILD,
                NAME_CONTRACT = contractVersion.NAME_CONTRACT
            };

            return contract;
        }

        public void GetParent()
        {
            var parent = this.CurrentRepository.Get(this.ObjDetail.PARENT);
            this.ObjDetail.NAME_PARENT = parent.NAME;
            this.ObjDetail.NAME = parent.NAME;
            this.ObjDetail.CONTRACT_TYPE = parent.CONTRACT_TYPE;
            this.ObjDetail.SIGN_DAY = parent.SIGN_DAY;
            this.ObjDetail.START_DATE = parent.START_DATE;
            this.ObjDetail.FINISH_DATE = parent.FINISH_DATE;
            this.ObjDetail.CONTRACT_PHASE = parent.CONTRACT_PHASE;
            this.ObjDetail.REPRESENT_A = parent.REPRESENT_A;
            this.ObjDetail.REPRESENT_B = parent.REPRESENT_B;
            this.ObjDetail.DEPARTMENT = parent.DEPARTMENT;
            this.ObjDetail.CONTRACT_MANAGER = parent.CONTRACT_MANAGER;
            this.ObjDetail.APPROVER = parent.APPROVER;
            this.ObjDetail.ParentContract.CONTRACT_NUMBER = parent.CONTRACT_NUMBER;
            this.ObjDetail.ParentContract.NAME = parent.NAME;
        }
        public decimal getAmount(string nameContact, int version)
        {
            var list = UnitOfWork.Repository<PaymentRepo>().Queryable().Where(x => x.NAME_CONTRACT == nameContact && x.VERSION == version && x.IS_DELETE != true).ToList();
            decimal amount = 0;
            foreach (var item in list)
            {
                amount += item.AMOUNT;
            }
            return amount;
        }
        public void checkAccessRights()
        {
            this.State = false;
            var user = ProfileUtilities.User.USER_NAME;
            foreach (var item in this.ObjListUser)
            {
                if (item == user)
                {
                    this.State = true;
                }
            }
            if (this.ObjDetail.APPROVER == user)
            {
                this.State = true;
            }
            if (this.ObjDetail.CONTRACT_MANAGER == user)
            {
                this.State = true;
            }
            if (this.ObjDetail.CREATE_BY == user)
            {
                this.State = true;
            }
        }
        public void ExportExcelTemplate(ref MemoryStream outFileStream, string path)
        {
            try
            {
                this.ObjList = this.CurrentRepository.Queryable().Where(x => x.PARENT == null).ToList();
                foreach (var item in this.ObjList)
                {
                    try
                    {
                        item.PHANTRAM = Math.Floor((getAmount(item.NAME_CONTRACT, item.VERSION) / item.CONTRACT_VALUE_VAT) * 100);
                    }
                    catch
                    {
                        item.PHANTRAM = 0;
                    }
                }
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    IWorkbook templateWorkbook;
                    templateWorkbook = new XSSFWorkbook(fs);

                    ISheet sheet = templateWorkbook.GetSheetAt(0);

                    // Bắt đầu từ dòng 2
                    int rowIndex = 1;

                    foreach (var item in this.ObjList)
                    {
                        IRow row = sheet.CreateRow(rowIndex);
                        row.CreateCell(0).SetCellValue(rowIndex);
                        row.CreateCell(1).SetCellValue(item.NAME);
                        row.CreateCell(1).SetCellValue(item.CONTRACT_NUMBER);
                        row.CreateCell(2).SetCellValue(item.CONTRACT_TYPE);
                        row.CreateCell(3).SetCellValue(item.CUSTOMER);
                        row.CreateCell(4).SetCellValue(item.CONTRACT_MANAGER);
                        row.CreateCell(5).SetCellValue(item.DEPARTMENT);
                        row.CreateCell(6).SetCellValue(item.CONTRACT_VALUE.ToStringVN());
                        row.CreateCell(7).SetCellValue(item.START_DATE.ToString("dd/MM/yyyy"));
                        row.CreateCell(8).SetCellValue(item.FINISH_DATE.ToString("dd/MM/yyyy"));
                        row.CreateCell(9).SetCellValue(item.SIGN_DAY.ToString("dd/MM/yyyy"));
                        row.CreateCell(10).SetCellValue(ConstContract.convertStatusToString(item.STATUS));
                        row.CreateCell(12).SetCellValue(ConstContract.convertPhaseToString(item.CONTRACT_PHASE));
                        row.CreateCell(13).SetCellValue(item.VERSION);
                        row.CreateCell(14).SetCellValue(item.PHANTRAM.ToString() + "%");
                        rowIndex++;
                    }

                    templateWorkbook.Write(outFileStream);
                }

            }
            catch (Exception ex)
            {
                this.State = false;
                this.ErrorMessage = "Có lỗi xẩy ra trong quá trình tạo file excel!";
                this.Exception = ex;
            }
        }

        public T_MD_CUSTOMER GetCustomer(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                return UnitOfWork.Repository<CustomerRepo>().Queryable().FirstOrDefault(x => x.CODE == code);
            }
            else
            {
                return new T_MD_CUSTOMER();
            }
        }
    }
}