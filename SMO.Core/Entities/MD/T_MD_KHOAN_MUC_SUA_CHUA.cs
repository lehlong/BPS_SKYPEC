using SMO.Core.Common;
using SMO.Core.Entities.BP.SUA_CHUA_LON;
using SMO.Core.Entities.BP.SUA_CHUA_LON.SUA_CHUA_LON_DATA_BASE;
using System;
using System.Collections.Generic;
using SMO.Core.Entities.BP.SUA_CHUA_THUONG_XUYEN;
using SMO.Core.Entities.BP.SUA_CHUA_THUONG_XUYEN.SUA_CHUA_THUONG_XUYEN_DATA_BASE;

namespace SMO.Core.Entities.MD
{
    public class T_MD_KHOAN_MUC_SUA_CHUA : CoreElement, ICloneable
    {
        public T_MD_KHOAN_MUC_SUA_CHUA() : base()
        {
            Values = new decimal[1];
        }

        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        public virtual bool IsChecked { get; set; }

        public override bool Equals(object obj)
        {
            return obj is T_MD_KHOAN_MUC_SUA_CHUA element &&
                   TIME_YEAR == element.TIME_YEAR &&
                   Values == element.Values &&
                   CODE == element.CODE;
        }

        public override int GetHashCode()
        {
            var hashCode = 2051980312;
            hashCode = hashCode * -1521134295 + TIME_YEAR.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<decimal[]>.Default.GetHashCode(Values);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CODE);
            return hashCode;
        }

        public static explicit operator T_MD_KHOAN_MUC_SUA_CHUA(T_BP_SUA_CHUA_LON_DATA_HISTORY v)
        {
            if (v == null)
            {
                return new T_MD_KHOAN_MUC_SUA_CHUA();
            }
            return new T_MD_KHOAN_MUC_SUA_CHUA
            {
                TEMPLATE_CODE = v.TEMPLATE_CODE + v.SUA_CHUA_PROFIT_CENTER_CODE,
                TEMPLATE_CODE_PURE = v.TEMPLATE_CODE,
                ORG_CODE = v.ORG_CODE,
                ORG_NAME = string.IsNullOrEmpty(v.TEMPLATE_CODE) ? v.Organize.NAME : $"Sân bay: {v.SuaChuaProfitCenter.SanBay.NAME}\nChi nhánh: {v.SuaChuaProfitCenter.CostCenter.NAME}\nMẫu: {v.TEMPLATE_CODE}\nĐơn vị nộp: {v.Template.Organize.NAME}\nTrạng thái: {Approve_Status.GetStatusText(v.STATUS)}",
                CENTER_CODE = v.ORG_CODE,
                Values = new decimal[1]
                {
                    v.VALUE ?? 0,
                },
                VERSION = v.VERSION,
                DESCRIPTION = v.DESCRIPTION,
                Template = v.Template
            };
        }

        public static explicit operator T_MD_KHOAN_MUC_SUA_CHUA(T_BP_SUA_CHUA_LON_DATA v)
        {
            if (v == null)
            {
                return new T_MD_KHOAN_MUC_SUA_CHUA();
            }
            return new T_MD_KHOAN_MUC_SUA_CHUA
            {
                TEMPLATE_CODE = v.TEMPLATE_CODE + v.SUA_CHUA_PROFIT_CENTER_CODE,
                TEMPLATE_CODE_PURE = v.TEMPLATE_CODE,
                ORG_CODE = v.ORG_CODE,
                ORG_NAME = string.IsNullOrEmpty(v.TEMPLATE_CODE) ? v.Organize.NAME : $"Sân bay: {v.SuaChuaProfitCenter.SanBay.NAME}\nChi nhánh: {v.SuaChuaProfitCenter.CostCenter.NAME}\nMẫu: {v.TEMPLATE_CODE}\nĐơn vị nộp: {v.Template.Organize.NAME}\nTrạng thái: {Approve_Status.GetStatusText(v.STATUS)}",
                CENTER_CODE = v.ORG_CODE,
                Values = new decimal[1]
                {
                    v.VALUE ?? 0,          
                },
                VERSION = v.VERSION,
                DESCRIPTION = v.DESCRIPTION,
                Template = v.Template
            };
        }

        public static explicit operator T_MD_KHOAN_MUC_SUA_CHUA(T_BP_SUA_CHUA_LON_DATA_BASE v)
        {
            return new T_MD_KHOAN_MUC_SUA_CHUA
            {
                ORG_NAME = $"{v.MATERIAL} ({v.UNIT})",
                TEMPLATE_CODE = v.TEMPLATE_CODE + v.SUA_CHUA_PROFIT_CENTER_CODE,
                TEMPLATE_CODE_PURE = v.TEMPLATE_CODE,
                ORG_CODE = v.ORG_CODE,
                IS_GROUP = false,
                CENTER_CODE = v.ORG_CODE,
                VERSION = v.VERSION,
                DESCRIPTION = v.DESCRIPTION,
                Template = v.Template,
                IsBase = true,
                ValuesBaseString = new string[1]
                {
                    HandleValueBaseString(quantity: v.QUANTITY, price: v.PRICE, amount: v.AMOUNT, time: v.TIME),
                },
            };
        }

        public static explicit operator T_MD_KHOAN_MUC_SUA_CHUA(T_BP_SUA_CHUA_LON_DATA_BASE_HISTORY v)
        {
            return new T_MD_KHOAN_MUC_SUA_CHUA
            {
                ORG_NAME = $"{v.MATERIAL} ({v.UNIT})",
                TEMPLATE_CODE = v.TEMPLATE_CODE + v.SUA_CHUA_PROFIT_CENTER_CODE,
                TEMPLATE_CODE_PURE = v.TEMPLATE_CODE,
                ORG_CODE = v.ORG_CODE,
                CENTER_CODE = v.ORG_CODE,
                VERSION = v.VERSION,
                IS_GROUP = false,
                DESCRIPTION = v.DESCRIPTION,
                Template = v.Template,
                IsBase = true,
                ValuesBaseString = new string[1]
                {
                    HandleValueBaseString(quantity: v.QUANTITY, price: v.PRICE, amount: v.AMOUNT, time: v.TIME),
                    
                },
            };
        }


        public static explicit operator T_MD_KHOAN_MUC_SUA_CHUA(T_BP_SUA_CHUA_THUONG_XUYEN_DATA_HISTORY v)
        {
            if (v == null)
            {
                return new T_MD_KHOAN_MUC_SUA_CHUA();
            }
            return new T_MD_KHOAN_MUC_SUA_CHUA
            {
                TEMPLATE_CODE = v.TEMPLATE_CODE + v.SUA_CHUA_PROFIT_CENTER_CODE,
                TEMPLATE_CODE_PURE = v.TEMPLATE_CODE,
                ORG_CODE = v.ORG_CODE,
                ORG_NAME = string.IsNullOrEmpty(v.TEMPLATE_CODE) ? v.Organize.NAME : $"Sân bay: {v.SuaChuaProfitCenter.SanBay.NAME}\nChi nhánh: {v.SuaChuaProfitCenter.CostCenter.NAME}\nMẫu: {v.TEMPLATE_CODE}\nĐơn vị nộp: {v.Template.Organize.NAME}\nTrạng thái: {Approve_Status.GetStatusText(v.STATUS)}",
                CENTER_CODE = v.ORG_CODE,
                Values = new decimal[1]
                {
                    v.VALUE ?? 0,
                },
                VERSION = v.VERSION,
                DESCRIPTION = v.DESCRIPTION,
                Template = v.Template
            };
        }

        public static explicit operator T_MD_KHOAN_MUC_SUA_CHUA(T_BP_SUA_CHUA_THUONG_XUYEN_DATA v)
        {
            if (v == null)
            {
                return new T_MD_KHOAN_MUC_SUA_CHUA();
            }
            return new T_MD_KHOAN_MUC_SUA_CHUA
            {
                TEMPLATE_CODE = v.TEMPLATE_CODE + v.SUA_CHUA_PROFIT_CENTER_CODE,
                TEMPLATE_CODE_PURE = v.TEMPLATE_CODE,
                ORG_CODE = v.ORG_CODE,
                ORG_NAME = string.IsNullOrEmpty(v.TEMPLATE_CODE) ? v.Organize.NAME : $"Sân bay: {v.SuaChuaProfitCenter.SanBay.NAME}\nChi nhánh: {v.SuaChuaProfitCenter.CostCenter.NAME}\nMẫu: {v.TEMPLATE_CODE}\nĐơn vị nộp: {v.Template.Organize.NAME}\nTrạng thái: {Approve_Status.GetStatusText(v.STATUS)}",
                CENTER_CODE = v.ORG_CODE,
                Values = new decimal[1]
                {
                    v.VALUE ?? 0,
                },
                VERSION = v.VERSION,
                DESCRIPTION = v.DESCRIPTION,
                Template = v.Template
            };
        }

        public static explicit operator T_MD_KHOAN_MUC_SUA_CHUA(T_BP_SUA_CHUA_THUONG_XUYEN_DATA_BASE v)
        {
            return new T_MD_KHOAN_MUC_SUA_CHUA
            {
                ORG_NAME = $"{v.MATERIAL} ({v.UNIT})",
                TEMPLATE_CODE = v.TEMPLATE_CODE + v.SUA_CHUA_PROFIT_CENTER_CODE,
                TEMPLATE_CODE_PURE = v.TEMPLATE_CODE,
                ORG_CODE = v.ORG_CODE,
                IS_GROUP = false,
                CENTER_CODE = v.ORG_CODE,
                VERSION = v.VERSION,
                DESCRIPTION = v.DESCRIPTION,
                Template = v.Template,
                IsBase = true,
                ValuesBaseString = new string[1]
                {
                    HandleValueBaseString(quantity: v.QUANTITY, price: v.PRICE, amount: v.AMOUNT, time: v.TIME),
                },
            };
        }

        public static explicit operator T_MD_KHOAN_MUC_SUA_CHUA(T_BP_SUA_CHUA_THUONG_XUYEN_DATA_BASE_HISTORY v)
        {
            return new T_MD_KHOAN_MUC_SUA_CHUA
            {
                ORG_NAME = $"{v.MATERIAL} ({v.UNIT})",
                TEMPLATE_CODE = v.TEMPLATE_CODE + v.SUA_CHUA_PROFIT_CENTER_CODE,
                TEMPLATE_CODE_PURE = v.TEMPLATE_CODE,
                ORG_CODE = v.ORG_CODE,
                CENTER_CODE = v.ORG_CODE,
                VERSION = v.VERSION,
                IS_GROUP = false,
                DESCRIPTION = v.DESCRIPTION,
                Template = v.Template,
                IsBase = true,
                ValuesBaseString = new string[1]
                {
                    HandleValueBaseString(quantity: v.QUANTITY, price: v.PRICE, amount: v.AMOUNT, time: v.TIME),

                },
            };
        }

    }
}
