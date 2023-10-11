using SMO.Core.Entities.BU;
using SMO.Repository.Implement.BU;
using SMO.Service.Class;
using SMO.Service.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SMO.Service.BU
{
    public class PaymentService : GenericService<T_BU_PAYMENT, PaymentRepo>
    {
        public T_BU_CONTRACT contract { get; set; }
        public decimal amount { get; set; }
        public List<string> deletePayment {  get; set; }
        public PaymentService() : base()
        {
            contract = new T_BU_CONTRACT();
            deletePayment = new List<string>();
            amount = 0;
        }
        public override void Search()
        {
            this.ObjList = this.CurrentRepository.Queryable().Where(x=>x.NAME_CONTRACT==this.ObjDetail.NAME_CONTRACT&&x.VERSION==this.ObjDetail.VERSION&&x.IS_DELETE!=true).ToList();
        }
        public void getContract()
        {
            var contractCheck = UnitOfWork.Repository<ContractRepo>().Queryable().Where(x=>x.NAME_CONTRACT==this.ObjDetail.NAME_CONTRACT).FirstOrDefault();
            if(contractCheck.VERSION==this.ObjDetail.VERSION)
            {
                contract = contractCheck;
            }
            else
            {
                var contractOld = UnitOfWork.Repository<ContractVersionRepo>().Queryable().Where(x => x.NAME_CONTRACT == this.ObjDetail.NAME_CONTRACT&&x.VERSION==this.ObjDetail.VERSION).FirstOrDefault();
                contract = ContractService.ConvertContractToContractVersion(contractOld);
            }
            Search();
            foreach(var item in this.ObjList)
            {
                this.amount += item.AMOUNT;
            }
        }
        public void Create(HttpRequestBase Request)
        {
            try
            {
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
                        VERSION = this.ObjDetail.VERSION,
                    };
                    UnitOfWork.Repository<Repository.Implement.BU.FileUploadRepo>().Create(fileUpload);
                }
                this.ObjDetail.ID = id;
                this.ObjDetail.CREATE_BY = ProfileUtilities.User.USER_NAME;
                this.ObjDetail.UPDATE_HUMAN = ProfileUtilities.User.FULL_NAME;
                this.ObjDetail.UPDATE_TIME = DateTime.Now;
                this.ObjDetail.FILE_CHILD = idFileChild;
                this.ObjDetail.IS_DELETE = false;
                this.CurrentRepository.Create(this.ObjDetail);             
                // tạo lịch sử
                var history = new T_BU_CONTRACT_HISTORY()
                {
                    ID = Guid.NewGuid().ToString(),
                    ACTION = ConstContract.TaoDotThanhToan,
                    CREATE_BY = ProfileUtilities.User.USER_NAME,
                    VERSION = this.ObjDetail.VERSION,
                    NAME_CONTRACT = this.ObjDetail.NAME_CONTRACT,

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
        public void Update(HttpRequestBase Request)
        {
            try
            {
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
                        PARENT = this.ObjDetail.FILE_CHILD,
                        CREATE_BY = ProfileUtilities.User.USER_NAME,
                        IS_DELETE = false,
                        VERSION = this.ObjDetail.VERSION,
                    };
                    UnitOfWork.Repository<Repository.Implement.BU.FileUploadRepo>().Create(fileUpload);
                }
                this.ObjDetail.UPDATE_HUMAN = ProfileUtilities.User.FULL_NAME;
                this.ObjDetail.UPDATE_TIME = DateTime.Now;
                this.ObjDetail.IS_DELETE = false;
                this.CurrentRepository.Update(this.ObjDetail);
                // tạo lịch sử
                var history = new T_BU_CONTRACT_HISTORY()
                {
                    ID = Guid.NewGuid().ToString(),
                    ACTION = ConstContract.SuaDotThanhToan,
                    CREATE_BY = ProfileUtilities.User.USER_NAME,
                    VERSION = this.ObjDetail.VERSION,
                    NAME_CONTRACT = this.ObjDetail.NAME_CONTRACT,

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
        public void DeletePayment()
        {
            try
            {

                UnitOfWork.BeginTransaction();
                foreach(var item in deletePayment)
                {
                    var itemDelete = this.CurrentRepository.Get(item);
                    itemDelete.IS_DELETE = true;
                    itemDelete.UPDATE_TIME = DateTime.Today;
                    this.CurrentRepository.Update(itemDelete);
                }
                // tạo lịch sử
                var history = new T_BU_CONTRACT_HISTORY()
                {
                    ID = Guid.NewGuid().ToString(),
                    ACTION = ConstContract.XoaDotThanhToan,
                    CREATE_BY = ProfileUtilities.User.USER_NAME,
                    VERSION = this.ObjDetail.VERSION,
                    NAME_CONTRACT = this.ObjDetail.NAME_CONTRACT,

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


    }
}