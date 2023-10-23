using SMO.Core.Entities;
using SMO.Repository.Implement.MD;

using System;

namespace SMO.Service.MD
{
    public class PhanLoaiDauTuService : GenericService<T_MD_PHAN_LOAI_DAU_TU, PhanLoaiDauTuRepo>
    {
        public PhanLoaiDauTuService() : base()
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
