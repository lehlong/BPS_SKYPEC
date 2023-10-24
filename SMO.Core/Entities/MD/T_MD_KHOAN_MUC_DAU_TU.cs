using SMO.Core.Common;
using SMO.Core.Entities.BP.DAU_TU_XAY_DUNG;
using SMO.Core.Entities.BP.DAU_TU_XAY_DUNG.DAU_TU_XAY_DUNG_DATA_BASE;

using System;
using System.Collections.Generic;

namespace SMO.Core.Entities.MD
{
    public class T_MD_KHOAN_MUC_DAU_TU : CoreElement, ICloneable
    {
        private T_MD_DAU_TU_XAY_DUNG_PROFIT_CENTER _DauTuXayDungProfitCenter;
        public virtual T_MD_DAU_TU_XAY_DUNG_PROFIT_CENTER DauTuXayDungProfitCenter
        {
            get
            {
                if (_DauTuXayDungProfitCenter == null)
                {
                    _DauTuXayDungProfitCenter = new T_MD_DAU_TU_XAY_DUNG_PROFIT_CENTER();
                }
                return _DauTuXayDungProfitCenter;
            }
            set
            {
                _DauTuXayDungProfitCenter = value;
            }
        }
        public T_MD_KHOAN_MUC_DAU_TU() : base()
        {
            Values = new decimal[14];
        }

        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        public override bool Equals(object obj)
        {
            return obj is T_MD_KHOAN_MUC_DAU_TU element &&
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

        public static explicit operator T_MD_KHOAN_MUC_DAU_TU(T_BP_DAU_TU_XAY_DUNG_DATA_HISTORY v)
        {
            if (v == null)
            {
                return new T_MD_KHOAN_MUC_DAU_TU();
            }
            return new T_MD_KHOAN_MUC_DAU_TU
            {
                TEMPLATE_CODE = v.TEMPLATE_CODE + v.DAU_TU_PROFIT_CENTER_CODE,
                TEMPLATE_CODE_PURE = v.TEMPLATE_CODE,
                ORG_CODE = v.ORG_CODE,
                ORG_NAME = string.IsNullOrEmpty(v.TEMPLATE_CODE) ? v.Organize.NAME : $"Phòng ban: {v.DauTuXayDungProfitCenter.Organize.NAME}\nDự án: {v.DauTuXayDungProfitCenter.Project.NAME}\nMẫu: {v.TEMPLATE_CODE}\nĐơn vị nộp: {v.Template.Organize.NAME}\nTrạng thái: {Approve_Status.GetStatusText(v.STATUS)}",
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

        public static explicit operator T_MD_KHOAN_MUC_DAU_TU(T_BP_DAU_TU_XAY_DUNG_DATA v)
        {
            if (v == null)
            {
                return new T_MD_KHOAN_MUC_DAU_TU();
            }
            return new T_MD_KHOAN_MUC_DAU_TU
            {
                TEMPLATE_CODE = v.TEMPLATE_CODE + v.DAU_TU_PROFIT_CENTER_CODE,
                TEMPLATE_CODE_PURE = v.TEMPLATE_CODE,
                ORG_CODE = v.ORG_CODE,
                ORG_NAME = string.IsNullOrEmpty(v.TEMPLATE_CODE) ? v.Organize.NAME : $"Phòng ban: {v.DauTuXayDungProfitCenter.Organize.NAME}\nDự án: {v.DauTuXayDungProfitCenter.Project.NAME}\nMẫu: {v.TEMPLATE_CODE}\nĐơn vị nộp: {v.Template.Organize.NAME}\nTrạng thái: {Approve_Status.GetStatusText(v.STATUS)}",
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

        public static explicit operator T_MD_KHOAN_MUC_DAU_TU(T_BP_DAU_TU_XAY_DUNG_DATA_BASE v)
        {
            return new T_MD_KHOAN_MUC_DAU_TU
            {
                ORG_NAME = $"{v.MATERIAL} ({v.UNIT})",
                TEMPLATE_CODE = v.TEMPLATE_CODE + v.DAU_TU_PROFIT_CENTER_CODE,
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

        public static explicit operator T_MD_KHOAN_MUC_DAU_TU(T_BP_DAU_TU_XAY_DUNG_DATA_BASE_HISTORY v)
        {
            return new T_MD_KHOAN_MUC_DAU_TU
            {
                ORG_NAME = $"{v.MATERIAL} ({v.UNIT})",
                TEMPLATE_CODE = v.TEMPLATE_CODE + v.DAU_TU_PROFIT_CENTER_CODE,
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
