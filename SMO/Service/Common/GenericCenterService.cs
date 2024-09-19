using SMO.Core.Common;
using SMO.Repository.Common;

using System;

namespace SMO.Service.Common
{
    public class GenericCenterService<T, Repo> : GenericService<T, Repo> where T : CoreCenter where Repo : GenericCenterRepository<T>
    {
        public override void Create()
        {
            try
            {
          
                if (CheckExist(x => x.CODE == ObjDetail.CODE))
                {
                    
                    State = false;
                    MesseageCode = "1101";
                }
                else if (CheckExist(x => x.NAME == ObjDetail.NAME))
                {
                    State = false;
                    MesseageCode = "7006";
                }
                else
                {
                    base.Create();
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
