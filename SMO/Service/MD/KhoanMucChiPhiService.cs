﻿using SMO.Core.Entities;
using SMO.Repository.Implement.MD;

using System;

namespace SMO.Service.MD
{
    public class KhoanMucChiPhiService : GenericService<T_MD_KHOAN_MUC_CHI_PHI, KhoanMucChiPhiRepo>
    {
        public KhoanMucChiPhiService() : base()
        {

        }

        public override void Create()
        {
            try
            {
                if (CheckExist(x => x.TEXT==ObjDetail.TEXT))
                {
                    State = false;
                    MesseageCode = "7006";
                }
                else if (CheckExist(x => x.CODE == ObjDetail.CODE ))
                {
                    State = false;
                    MesseageCode = "1101";
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
