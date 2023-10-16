using SMO.Core.Entities.BU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.BU
{
    class T_BU_CONTRACT_Map : BaseMapping<T_BU_CONTRACT>
    {
        public T_BU_CONTRACT_Map() {
            Table("T_BU_CONTRACT");
            Id(x=>x.ID);
            Map(x => x.PARENT);
            Map(x => x.NAME);
            Map(x => x.CONTRACT_NUMBER);
            Map(x => x.CONTRACT_TYPE);
            Map(x => x.START_DATE);
            Map(x => x.FINISH_DATE);
            Map(x => x.CONTRACT_VALUE);
            Map(x => x.VAT); 
            Map(x => x.CONTRACT_VALUE_VAT);
            Map(x => x.NOTES);
            Map(x => x.REPRESENT_A);
            Map(x => x.REPRESENT_B); 
            Map(x => x.VERSION);
            Map(x => x.CONTRACT_PHASE);
            Map(x => x.APPROVER);
            Map(x => x.CONTRACT_MANAGER);
            Map(x => x.DEPARTMENT);
            Map(x => x.CUSTOMER);
            Map(x => x.STATUS);
            Map(x => x.NAME_CONTRACT);
            Map(x => x.FILE_CHILD);
            Map(x => x.NAME_PARENT);
            Map(x => x.SIGN_DAY);
            HasMany(x => x.ChildContracts).KeyColumn("PARENT").LazyLoad();
            References(x => x.ContractType).Column("CONTRACT_TYPE").Not.Insert().Not.Update().LazyLoad();
            References(x => x.ParentContract).Column("PARENT").Not.Insert().Not.Update().LazyLoad();
            References(x => x.CostCenter).Column("DEPARTMENT").Not.Insert().Not.Update().LazyLoad();
            References(x => x.CustomerContract).Column("CUSTOMER").Not.Insert().Not.Update().LazyLoad();
            References(x => x.ContractManager).Column("CONTRACT_MANAGER").Not.Insert().Not.Update().LazyLoad();
        }
    }
}
