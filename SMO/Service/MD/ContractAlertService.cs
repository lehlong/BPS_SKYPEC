﻿using SMO.Core.Entities;
using SMO.Core.Entities.MD;
using SMO.Repository.Implement.MD;

using System;

namespace SMO.Service.MD
{
    public class ContractAlertService : GenericService<T_MD_CONTRACT_ALERT, ContractAlertRepo>
    {
        public ContractAlertService() : base()
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
