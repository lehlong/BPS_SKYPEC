using SMO.Core.Entities;
using SMO.Repository.Implement.BP.KE_HOACH_SAN_LUONG;
using SMO.Repository.Implement.MD;

using System;
using System.Linq;

namespace SMO.Service.MD
{
    public class SharedDataService : GenericService<T_MD_SHARED_DATA, SharedDataRepo>
    {
        public SharedDataService() : base()
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

        public void CalCulaTion()
        {
            try
            {
                var lstPurchase = UnitOfWork.Repository<PurchaseDataRepo>().GetAll();
                var year = DateTime.Now.Year;
                decimal SumSL = UnitOfWork.Repository<PurchaseDataRepo>().GetAll().Sum(x=>x.S0002);
                decimal SumTS = 0;
                UnitOfWork.BeginTransaction();
                foreach(var item in lstPurchase)
                {
                    var S0007 = item.S0007/100; // thuế xuất ưu đãi
                    var S0008 = item.S0008/100; // thuế xuất nhập khẩu
                    var SLNhap = item.S0002;
                    var sumItem = S0007 * SLNhap + S0008 * SLNhap;
                    SumTS = SumTS + sumItem;
                   
                }
                var tnkBQ = SumTS / SumSL;
                var obj = UnitOfWork.Repository<SharedDataRepo>().Get("23");
                obj.VALUE = tnkBQ;
                var objVN = UnitOfWork.Repository<SharedDataRepo>().Get("TNK-VN");
                var objOV = UnitOfWork.Repository<SharedDataRepo>().Get("TNK-0V");
                var priceFlat = UnitOfWork.Repository<SharedDataRepo>().Queryable().FirstOrDefault(x => x.CODE == "1").VALUE;
                var HS = UnitOfWork.Repository<SharedDataRepo>().Queryable().FirstOrDefault(x => x.CODE == "3").VALUE;
                var TG = UnitOfWork.Repository<SharedDataRepo>().Queryable().FirstOrDefault(x => x.CODE == "2").VALUE;

                var  tnkVN = priceFlat * HS * tnkBQ * TG;
                objVN.VALUE = tnkVN;
                objOV.VALUE = tnkVN;
                UnitOfWork.Repository<SharedDataRepo>().Update(obj);
                UnitOfWork.Repository<SharedDataRepo>().Update(objVN);
                UnitOfWork.Repository<SharedDataRepo>().Update(objOV);
                UnitOfWork.Commit();
                State = true;
            }
            catch (Exception ex)
            {
                UnitOfWork.Rollback();
                State = false;
                throw ex;
            }
        }
    }
}
