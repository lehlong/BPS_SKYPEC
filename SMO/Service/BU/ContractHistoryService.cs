using SMO.Core.Entities.BU;
using SMO.Repository.Implement.BU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMO.Service.BU
{
    public class ContractHistoryService : GenericService<T_BU_CONTRACT_HISTORY, ContractHistoryRepo>
    {
        public ContractHistoryService() : base()
        {

        }
        public void GetHistory(string nameContract,int version)
        {
            this.ObjList = this.CurrentRepository.Queryable().Where(x=>x.NAME_CONTRACT.Equals(nameContract)&&x.VERSION==version).ToList();
        }
    }
}