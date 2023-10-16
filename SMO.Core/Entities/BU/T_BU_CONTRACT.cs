using SMO.Core.Entities.MD;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Core.Entities.BU
{
    public partial class T_BU_CONTRACT : BaseEntity
    {
        public virtual string ID { get; set; }
        public virtual string PARENT { get; set; }
        public virtual string NAME { get; set; }
        public virtual string CONTRACT_NUMBER { get; set; }
        public virtual string CONTRACT_TYPE { get; set; }
        public virtual DateTime START_DATE { get; set; }
        public virtual DateTime FINISH_DATE { get;set; }
        public virtual decimal CONTRACT_VALUE { get; set;}
        public virtual decimal VAT { get; set;}
        public virtual decimal CONTRACT_VALUE_VAT{ get; set;}
        public virtual string NOTES { get; set; }
        public virtual string REPRESENT_A { get; set; }
        public virtual string REPRESENT_B { get; set;}
        public virtual int VERSION { get; set;}
        public virtual string CONTRACT_PHASE { get; set;}
        public virtual string APPROVER { get; set;}
        public virtual string CONTRACT_MANAGER { get; set; }
        public virtual string DEPARTMENT { get; set; }
        public virtual string CUSTOMER { get; set; }
        public virtual string STATUS { get; set; }
        public virtual string FILE_CHILD { get; set; }
        public virtual string NAME_CONTRACT { get; set; }
        public virtual string NAME_PARENT { get;set; }
        public virtual decimal PHANTRAM { get; set; }
        public virtual DateTime SIGN_DAY { get; set; }
        public virtual IList<T_BU_CONTRACT> ChildContracts { get; set; }

        private T_BU_CONTRACT _ParentContract;
        public virtual T_BU_CONTRACT ParentContract
        {
            get
            {
                if (_ParentContract == null)
                {
                    _ParentContract = new T_BU_CONTRACT();
                }
                return _ParentContract;
            }
            set
            {
                _ParentContract = value;
            }
        }
        private T_MD_CONTRACT_TYPE _ContractType;
        public virtual T_MD_CONTRACT_TYPE ContractType
        {
            get
            {
                if (_ContractType == null)
                {
                    _ContractType = new T_MD_CONTRACT_TYPE();
                }
                return _ContractType;
            }
            set
            {
                _ContractType = value;
            }
        }

        private T_MD_CUSTOMER _CustomerContract;
        public virtual T_MD_CUSTOMER CustomerContract
        {
            get
            {
                if (_CustomerContract == null)
                {
                    _CustomerContract = new T_MD_CUSTOMER();
                }
                return _CustomerContract;
            }
            set
            {
                _CustomerContract = value;
            }
        }

        private T_MD_COST_CENTER _CostCenter;
        public virtual T_MD_COST_CENTER CostCenter
        {
            get
            {
                if (_CostCenter == null)
                {
                    _CostCenter = new T_MD_COST_CENTER();
                }
                return _CostCenter;
            }
            set
            {
                _CostCenter = value;
            }
        }

        private T_AD_USER _ContractManager;
        public virtual T_AD_USER ContractManager
        {
            get
            {
                if (_ContractManager == null)
                {
                    _ContractManager = new T_AD_USER();
                }
                return _ContractManager;
            }
            set
            {
                _ContractManager = value;
            }
        }
    }
}
