using NHibernate.Engine;
using SMO.Core.Entities;
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
    public class ContractRepo : GenericRepository<T_BU_CONTRACT>,IContractRepo
    {
        public ContractRepo(NHUnitOfWork unitOfWork) : base(unitOfWork.Session)
        {

        }
        public override IList<T_BU_CONTRACT> Search(T_BU_CONTRACT objFilter, int pageSize, int pageIndex, out int total)
        {
            var query = Queryable();
            string[] resultArray = objFilter.NAME.Split(',');
            string stringFilterAll = null;
            stringFilterAll = resultArray[0];
            objFilter.NAME = resultArray[1];
            query = query.Where(x => x.PARENT == null).OrderByDescending(x => x.CREATE_DATE);
            if (!string.IsNullOrWhiteSpace(stringFilterAll))
            {
                query = query.Where(x=>x.NAME.ToLower().Contains(stringFilterAll)|| x.CONTRACT_NUMBER.ToLower().Contains(stringFilterAll)|| x.CONTRACT_TYPE.ToLower().Contains(stringFilterAll)|| x.CUSTOMER.ToLower().Contains(stringFilterAll));
            }
            if (null != objFilter.START_DATE)
            {
                query = query.Where(x => x.START_DATE >= objFilter.START_DATE);
            }
            if (null != objFilter.FINISH_DATE && objFilter.FINISH_DATE != DateTime.MinValue)
            {
                query = query.Where(x => x.START_DATE <= objFilter.FINISH_DATE);
            }
            if (!string.IsNullOrWhiteSpace(objFilter.NAME))
            {
                query = query.Where(x => x.NAME.ToLower().Contains(objFilter.NAME.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(objFilter.CONTRACT_NUMBER))
            {
                query = query.Where(x => x.CONTRACT_NUMBER.ToLower().Contains(objFilter.CONTRACT_NUMBER.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(objFilter.CONTRACT_TYPE))
            {
                query = query.Where(x => x.CONTRACT_TYPE.ToLower().Contains(objFilter.CONTRACT_TYPE.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(objFilter.CUSTOMER))
            {
                query = query.Where(x => x.CUSTOMER.ToLower().Contains(objFilter.CUSTOMER.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(objFilter.CONTRACT_MANAGER))
            {
                query = query.Where(x => x.CONTRACT_MANAGER.ToLower().Contains(objFilter.CONTRACT_MANAGER.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(objFilter.DEPARTMENT))
            {
                query = query.Where(x => x.DEPARTMENT.ToLower().Contains(objFilter.DEPARTMENT.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(objFilter.STATUS))
            {
                query = query.Where(x =>x.STATUS.ToLower().Contains(objFilter.STATUS.ToLower()));
                
            }
            if (!string.IsNullOrWhiteSpace(objFilter.CONTRACT_PHASE))
            {
                query = query.Where(x => (x.CONTRACT_PHASE).ToLower().Contains(objFilter.CONTRACT_PHASE));
            }

            return base.Paging(query, pageSize, pageIndex, out total).ToList();

        }

    }
}
