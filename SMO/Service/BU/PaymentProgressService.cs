using SMO.Core.Entities.BU;
using SMO.Repository.Implement.BU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMO.Service.BU
{
    public class PaymentProgressService : GenericService<T_BU_PAYMENT_PROGRESS, PaymentProgressRepo>
    {
        public T_BU_CONTRACT contract { get; set; }
        public PaymentProgressService()
        {
            contract = new T_BU_CONTRACT();
        }

        public override void Search()
        {
            this.ObjList = this.CurrentRepository.Queryable().Where(x => x.NAME_CONTRACT == this.ObjDetail.NAME_CONTRACT && x.VERSION == this.ObjDetail.VERSION && x.IS_DELETE != true).OrderBy(x=>x.CREATE_DATE).ToList();
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
        }

        public void Create(HttpRequestBase Request)
        {
            try
            {
                ErrorMessage = "";
                if (string.IsNullOrEmpty(this.ObjDetail.BATCH))
                {
                    ErrorMessage = "Vui lòng nhập đợt thanh toán!";
                    State = false;
                    return;
                }
                if (this.ObjDetail.PROGRESS <= 0)
                {
                    ErrorMessage = "Vui lòng nhập tiến độ hợp đồng!";
                    State = false;
                    return;
                }
                decimal progressTotal = 0;
                DateTime datePayment = DateTime.MinValue;
                foreach(var item in this.ObjList)
                {
                    progressTotal += item.PROGRESS;
                    if(item.DATE > datePayment)
                    {
                        datePayment = item.DATE;
                    }
                }
                if((progressTotal+this.ObjDetail.PROGRESS) > 100)
                {
                    ErrorMessage = "Tổng tiến độ lớn hơn 100!";
                    State = false;
                    return;
                }
                if(this.ObjDetail.DATE < datePayment)
                {
                    ErrorMessage = "Ngày thanh toán hiện tại nhỏ hơn ngày thanh toán đợt trước!";
                    State = false;
                    return;
                }
                decimal totalValue = 0;
                foreach(var item in this.ObjList)
                {
                    totalValue += item.PAYMENT_VALUE;
                }
                if(totalValue > this.contract.CONTRACT_VALUE_VAT)
                {
                    ErrorMessage = "Tổng giá trị lớn hơn giá trị sau thuế!";
                    State = false;
                    return;
                }
                if (this.ObjDetail.PAYMENT_VALUE <= 0)
                {
                    ErrorMessage = "Vui lòng nhập giá trị đợt thanh toán";
                    State = false;
                    return;
                }

                if (this.ObjDetail.DATE == DateTime.MinValue)
                {
                    ErrorMessage = "Vui lòng nhập ngày thanh toán!";
                    State = false;
                    return;
                }

                var id = Guid.NewGuid().ToString();
                UnitOfWork.BeginTransaction();
                this.ObjDetail.ID = id;
                this.ObjDetail.CREATE_BY = ProfileUtilities.User.USER_NAME;
                this.ObjDetail.UPDATE_HUMAN = ProfileUtilities.User.FULL_NAME;
                this.ObjDetail.UPDATE_TIME = DateTime.Now;
                this.ObjDetail.IS_DELETE = false;
                this.CurrentRepository.Create(this.ObjDetail);
                var history = new T_BU_CONTRACT_HISTORY()
                {
                    ID = Guid.NewGuid().ToString(),
                    ACTION = ConstContract.ThemTienDoThanhToan,
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
                throw ex;
            }
        }

        public void Update()
        {

        }

        public void Delete(HttpRequestBase request)
        {
            try
            {
                UnitOfWork.BeginTransaction();
                var id = request.Form["id"];
                var itemdelete = this.CurrentRepository.Get(request.Form["id"]);
                /*this.CurrentRepository.Get();*/
                itemdelete.IS_DELETE = true;
                itemdelete.UPDATE_TIME = DateTime.Today;
                this.CurrentRepository.Update(itemdelete);
                var history = new T_BU_CONTRACT_HISTORY()
                {
                    ID = Guid.NewGuid().ToString(),
                    ACTION = ConstContract.XoaTienDoThanhToan,
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

        public void Edit(HttpRequestBase request)
        {

        }
    }
}