﻿using SMO.Core.Entities;
using SMO.Repository.Implement.MD;

using System;

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
    }
}
