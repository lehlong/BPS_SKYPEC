﻿using SMO.Core.Entities.BP.SUA_CHUA_THUONG_XUYEN.SUA_CHUA_THUONG_XUYEN_DATA_BASE;
using SMO.Repository.Common;
using SMO.Repository.Interface.BP.SUA_CHUA_THUONG_XUYEN.SUA_CHUA_THUONG_XUYEN_DATA_BASE;

namespace SMO.Repository.Implement.BP.SUA_CHUA_THUONG_XUYEN.SUA_CHUA_THUONG_XUYEN_DATA_BASE
{
    public class SuaChuaThuongXuyenDataBaseRepo : GenericRepository<T_BP_SUA_CHUA_THUONG_XUYEN_DATA_BASE>, ISuaChuaThuongXuyenDataBaseRepo
    {
        public SuaChuaThuongXuyenDataBaseRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {
        }
    }
}
