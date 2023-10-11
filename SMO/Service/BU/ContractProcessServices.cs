using SMO.Core.Entities.BU;
using SMO.Repository.Implement.BU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMO.Service.BU
{
    public class ContractProcessServices : GenericService<T_BU_CONTRACT_PROCESS, ContractProcessRepo>
    {
        public ContractProcessServices() : base()
        {
           
        }
        public void GetListProcess(string id,int version)
        {
            this.ObjList = this.CurrentRepository.Queryable().Where(x=>x.CONTRACT_NAME == id&&x.VERSION<=version).ToList();
        }


    }
}