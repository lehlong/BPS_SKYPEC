using SMO.Core.Entities;
using SMO.Models;
using SMO.Repository.Implement.MD;

using System;
using System.Collections.Generic;
using System.Linq;

namespace SMO.Service.MD
{
    public class PurchaseDataService : GenericService<T_MD_PURCHASE_DATA, PurchaseDataRepo>
    {
        public PurchaseDataService() : base()
        {

        }

        public IList<T_MD_PURCHASE_DATA> GetDataByTimeYear(int year)
        {
            try
            {
                var checkData = UnitOfWork.Repository<PurchaseDataRepo>().Queryable().Any(x => x.TIME_YEAR == year);
                if (!checkData)
                {
                    UnitOfWork.BeginTransaction();
                    var lstWarehouse = UnitOfWork.Repository<WarehouseRepo>().GetAll().ToList();
                    var lstDeliveryConditions = UnitOfWork.Repository<DeliveryConditionsRepo>().GetAll().ToList();
                    foreach (var warehouse in lstWarehouse)
                    {
                        foreach (var deliveryCondition in lstDeliveryConditions)
                        {
                            var item = new T_MD_PURCHASE_DATA
                            {
                                ID = Guid.NewGuid(),
                                WAREHOUSE_CODE = warehouse.CODE,
                                DELIVERY_CONDITIONS_CODE = deliveryCondition.CODE,
                                TIME_YEAR = year
                            };
                            UnitOfWork.Repository<PurchaseDataRepo>().Create(item);
                        }
                    }
                    UnitOfWork.Commit();
                }
                return UnitOfWork.Repository<PurchaseDataRepo>().Queryable().Where(x => x.TIME_YEAR == year).ToList();
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                this.State = false;
                this.Exception = ex;
                return new List<T_MD_PURCHASE_DATA>();
            }
        }

        public void UpdatePurchaseData(List<T_MD_PURCHASE_DATA> data)
        {
            try
            {
                UnitOfWork.BeginTransaction();
                foreach(var item in data)
                {
                    var pdata = UnitOfWork.Repository<PurchaseDataRepo>().Queryable().FirstOrDefault(x => x.TIME_YEAR == item.TIME_YEAR && x.WAREHOUSE_CODE == item.WAREHOUSE_CODE && x.DELIVERY_CONDITIONS_CODE == item.DELIVERY_CONDITIONS_CODE);
                    if (pdata != null)
                    {
                        pdata.S0001 = item.S0001;
                        pdata.S0002 = item.S0002;
                        pdata.S0003 = item.S0003;
                        pdata.S0004 = item.S0004;
                        pdata.S0005 = item.S0005;
                        pdata.S0006 = item.S0006;
                        pdata.S0007 = item.S0007;
                        UnitOfWork.Repository<PurchaseDataRepo>().Update(pdata);
                    }
                    else
                    {
                        continue;
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
    }
}
