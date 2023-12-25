using SMO.Core.Common;
using SMO.Core.Entities.BP.KE_HOACH_CHI_PHI;
using SMO.Core.Entities.BP.KE_HOACH_CHI_PHI.KE_HOACH_CHI_PHI_DATA_BASE;
using System;
using System.Collections.Generic;

namespace SMO.Core.Entities.MD
{
    public class T_MD_KHOAN_MUC_HANG_HOA : CoreElement, ICloneable
    {
        public T_MD_KHOAN_MUC_HANG_HOA() : base()
        {
            Values = new decimal[6];
        }

        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        public override bool Equals(object obj)
        {
            return obj is T_MD_KHOAN_MUC_HANG_HOA element &&
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

        public static explicit operator T_MD_KHOAN_MUC_HANG_HOA(T_BP_KE_HOACH_CHI_PHI_DATA_HISTORY v)
        {
            if (v == null)
            {
                return new T_MD_KHOAN_MUC_HANG_HOA();
            }
            return new T_MD_KHOAN_MUC_HANG_HOA
            {
                TEMPLATE_CODE = v.TEMPLATE_CODE + v.CHI_PHI_PROFIT_CENTER_CODE,
                TEMPLATE_CODE_PURE = v.TEMPLATE_CODE,
                ORG_CODE = v.ORG_CODE,
                ORG_NAME = string.IsNullOrEmpty(v.TEMPLATE_CODE) ? v.Organize.NAME : $"Sân bay: {v.ChiPhiProfitCenter.SanBay.NAME}\nChi nhánh: {v.ChiPhiProfitCenter.CostCenter.NAME}\nMẫu: {v.TEMPLATE_CODE}\nĐơn vị nộp: {v.Template.Organize.NAME}\nTrạng thái: {Approve_Status.GetStatusText(v.STATUS)}",
                CENTER_CODE = v.ORG_CODE,
                Values = new decimal[6]
                {
                    v.QUANTITY ?? 0,
                    v.PRICE ?? 0,
                    v.AMOUNT ?? 0,
                    v.QUANTITY_TD ?? 0,
                    v.PRICE_TD ?? 0,
                    v.AMOUNT_TD ?? 0,
                },
                VERSION = v.VERSION,
                DESCRIPTION = v.DESCRIPTION,
                Template = v.Template
            };
        }

        public static explicit operator T_MD_KHOAN_MUC_HANG_HOA(T_BP_KE_HOACH_CHI_PHI_DATA v)
        {
            if (v == null)
            {
                return new T_MD_KHOAN_MUC_HANG_HOA();
            }
            return new T_MD_KHOAN_MUC_HANG_HOA
            {
                TEMPLATE_CODE = v.TEMPLATE_CODE + v.CHI_PHI_PROFIT_CENTER_CODE,
                TEMPLATE_CODE_PURE = v.TEMPLATE_CODE,
                ORG_CODE = v.ORG_CODE,
                ORG_NAME = string.IsNullOrEmpty(v.TEMPLATE_CODE) ? v.Organize.NAME : $"Sân bay: {v.ChiPhiProfitCenter.SanBay.NAME}\nChi nhánh: {v.ChiPhiProfitCenter.CostCenter.NAME}\nMẫu: {v.TEMPLATE_CODE}\nĐơn vị nộp: {v.Template.Organize.NAME}\nTrạng thái: {Approve_Status.GetStatusText(v.STATUS)}",
                CENTER_CODE = v.ORG_CODE,
                Values = new decimal[6]
                {
                    v.QUANTITY ?? 0,
                    v.PRICE ?? 0,
                    v.AMOUNT ?? 0,
                    v.QUANTITY_TD ?? 0,
                    v.PRICE_TD ?? 0,
                    v.AMOUNT_TD ?? 0,
                },
                VERSION = v.VERSION,
                DESCRIPTION = v.DESCRIPTION,
                Template = v.Template
            };
        }

        public static explicit operator T_MD_KHOAN_MUC_HANG_HOA(T_BP_KE_HOACH_CHI_PHI_DATA_BASE v)
        {
            return new T_MD_KHOAN_MUC_HANG_HOA
            {
                ORG_NAME = $"{v.MATERIAL} ({v.UNIT})",
                TEMPLATE_CODE = v.TEMPLATE_CODE + v.CHI_PHI_PROFIT_CENTER_CODE,
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

        public static explicit operator T_MD_KHOAN_MUC_HANG_HOA(T_BP_KE_HOACH_CHI_PHI_DATA_BASE_HISTORY v)
        {
            return new T_MD_KHOAN_MUC_HANG_HOA
            {
                ORG_NAME = $"{v.MATERIAL} ({v.UNIT})",
                TEMPLATE_CODE = v.TEMPLATE_CODE + v.CHI_PHI_PROFIT_CENTER_CODE,
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
