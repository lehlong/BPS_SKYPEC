using SMO.Core.Entities.BU;
using SMO.Repository.Common;
using SMO.Repository.Interface.BU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Implement.BU
{
    public class PaymentProgressRepo : GenericRepository<T_BU_PAYMENT_PROGRESS>, IPaymentProgress
    {
        public PaymentProgressRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {

        }
    }
}
