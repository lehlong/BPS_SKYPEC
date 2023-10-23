using SMO.Core.Entities;
using SMO.Repository.Implement.MD;

using System;

namespace SMO.Service.MD
{
    public class NganhNgheDauTuService : GenericService<T_MD_NGANH_NGHE_DAU_TU, NganhNgheDauTuRepo>
    {
        public NganhNgheDauTuService() : base()
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
    }
}
