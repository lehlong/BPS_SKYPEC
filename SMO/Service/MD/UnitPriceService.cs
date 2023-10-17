using SMO.Core.Entities;
using SMO.Repository.Implement.MD;

using System;

namespace SMO.Service.MD
{
    public class UnitPriceService : GenericService<T_MD_UNIT_PRICE, UnitPriceRepo>
    {
        public UnitPriceService() : base()
        {

        }

        public override void Create()
        {
            try
            {
                if (!CheckExist(x => x.SAN_BAY_CODE == ObjDetail.SAN_BAY_CODE && x.YEAR == ObjDetail.YEAR))
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
    }
}
