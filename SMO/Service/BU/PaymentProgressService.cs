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
    public class PaymentProgressService : GenericService<T_BU_PAYMENT_PROGRESS, PaymentProgressRepo>
    {
        public T_BU_CONTRACT contract { get; set; }
        public decimal amount { get; set; }
        public List<string> deletePaymentProgress { get; set; }
        public PaymentProgressService() : base()
        {
            contract = new T_BU_CONTRACT();
            deletePaymentProgress = new List<string>();
            amount = 0;
        }
        public override void Search()
        {
            this.ObjList = this.CurrentRepository.Queryable().Where(x => x.NAME_CONTRACT == this.ObjDetail.NAME_CONTRACT && x.VERSION == this.ObjDetail.VERSION && x.IS_DELETE != true).ToList();
        }
        public void getContract()
        {
            var contractCheck = UnitOfWork.Repository<ContractRepo>().Queryable().Where(x => x.NAME_CONTRACT == this.ObjDetail.NAME_CONTRACT).FirstOrDefault();
            if (contractCheck.VERSION == this.ObjDetail.VERSION)
            {
                contract = contractCheck;
            }
            else
            {
                var contractOld = UnitOfWork.Repository<ContractVersionRepo>().Queryable().Where(x => x.NAME_CONTRACT == this.ObjDetail.NAME_CONTRACT && x.VERSION == this.ObjDetail.VERSION).FirstOrDefault();
                contract = ContractService.ConvertContractToContractVersion(contractOld);
            }
            Search();
        }
        public void Create(HttpRequestBase Request)
        {
            try
            {
                var lstContractVersion = UnitOfWork.Repository<ContractRepo>().Queryable().Where(x => x.NAME_CONTRACT == ObjDetail.NAME_CONTRACT);
                var version = lstContractVersion.Max(x => x.VERSION);
                var contract = lstContractVersion.FirstOrDefault(x => x.VERSION == version);
                var paymentProgress = UnitOfWork.Repository<PaymentProgressRepo>().Queryable().Where(x => x.NAME_CONTRACT == ObjDetail.NAME_CONTRACT && x.VERSION == version).ToList();
                
                if (paymentProgress.Sum(x => x.PAYMENT_VALUE) > contract.CONTRACT_VALUE_VAT)
                {
                    this.State = false;
                    this.ErrorMessage = "Tổng số tiền các đợt thanh toán lớn hơn giá trị hợp đồng!";
                    return;
                }
                if (paymentProgress.Count() != 0)
                {
                    var dayMax = paymentProgress.Max(x => x.DATE);
                    if (ObjDetail.DATE < dayMax)
                    {
                        this.State = false;
                        this.ErrorMessage = "Ngày thanh toán đợt này phải lớn hơn ngày thanh toán của các đợt khác!";
                        return;
                    }
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
                this.ObjDetail.IS_DELETE = false;
                this.ObjDetail.PROGRESS = this.ObjDetail.PAYMENT_VALUE / contract.CONTRACT_VALUE_VAT * 100;
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
                var lstContractVersion = UnitOfWork.Repository<ContractRepo>().Queryable().Where(x => x.NAME_CONTRACT == ObjDetail.NAME_CONTRACT);               
                var version = lstContractVersion.Max(x => x.VERSION);
                var contract = lstContractVersion.FirstOrDefault(x => x.VERSION == version);
                var paymentProgress = UnitOfWork.Repository<PaymentProgressRepo>().Queryable().Where(x => x.NAME_CONTRACT == ObjDetail.NAME_CONTRACT && x.VERSION == version).ToList();
                               
                if (paymentProgress.Sum(x => x.PAYMENT_VALUE) > contract.CONTRACT_VALUE_VAT)
                {
                    this.State = false;
                    this.ErrorMessage = "Tổng số tiền các đợt thanh toán lớn hơn giá trị hợp đồng!";
                    return;
                }
                if (paymentProgress.Count() != 0)
                {
                    var dayMax = paymentProgress.Max(x => x.DATE);
                    if (ObjDetail.DATE < dayMax)
                    {
                        this.State = false;
                        this.ErrorMessage = "Ngày thanh toán đợt này phải lớn hơn ngày thanh toán của các đợt khác!";
                        return;
                    }
                }

                UnitOfWork.Clear();
                UnitOfWork.BeginTransaction();
                
                this.ObjDetail.UPDATE_HUMAN = ProfileUtilities.User.FULL_NAME;
                this.ObjDetail.UPDATE_TIME = DateTime.Now;
                this.ObjDetail.IS_DELETE = false;
                this.ObjDetail.PROGRESS = this.ObjDetail.PAYMENT_VALUE / contract.CONTRACT_VALUE_VAT * 100;
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
        public void DeletePaymentProgress()
        {
            try
            {

                UnitOfWork.BeginTransaction();
                foreach (var item in deletePaymentProgress)
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