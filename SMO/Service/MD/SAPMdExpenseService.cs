using SMO.Core.Entities.MD;
using SMO.Repository.Implement.MD;

using SMO.Service.AD;

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SMO.Service.MD
{
    public class SAPMdExpenseService : GenericService<T_SAP_MD_EXPENSE, SAPMdExpenseRepo>
    {
        public SAPMdExpenseService() : base()
        {

        }

        public void Synchronize()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                this.State = false;
                this.Exception = ex;
            }
        }

    }
}
