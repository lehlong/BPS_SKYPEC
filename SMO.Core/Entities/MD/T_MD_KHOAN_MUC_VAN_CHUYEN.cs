using SMO.Core.Common;
using SMO.Core.Entities.BP.KE_HOACH_VAN_CHUYEN.KE_HOACH_VAN_CHUYEN_DATA_BASE;
using SMO.Core.Entities.BP.KE_HOACH_VAN_CHUYEN;
using System;
using System.Collections.Generic;

namespace SMO.Core.Entities.MD
{
    public class T_MD_KHOAN_MUC_VAN_CHUYEN : CoreElement, ICloneable
    {
        public T_MD_KHOAN_MUC_VAN_CHUYEN() : base()
        {
            Values = new decimal[11];
        }

        public virtual decimal ValuesSL { get; set; }
        public virtual decimal ValuesCL { get; set; }
        public virtual decimal ValuesSC { get; set; }
        public virtual decimal ValuesT { get; set; }
        public virtual decimal ValuesM3 { get; set; }
        public virtual decimal ValuesTVTB { get; set; }
        public virtual decimal ValuesTVC { get; set; }
        public virtual decimal ValuesTVT { get; set; }
        public virtual decimal ValuesTN { get; set; }
        public virtual decimal ValuesLCT { get; set; }
        public virtual decimal ValuesLCM3 { get; set; }
        public virtual string ParentOrder { get; set; }
        public virtual bool Isbold { get; set; }

        public virtual bool IsChecked { get; set; }


        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        public override bool Equals(object obj)
        {
            return obj is T_MD_KHOAN_MUC_VAN_CHUYEN element &&
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

        private T_MD_VAN_CHUYEN_PROFIT_CENTER _VanChuyenProfitCenter;
        public virtual T_MD_VAN_CHUYEN_PROFIT_CENTER VanChuyenProfitCenter
        {
            get
            {
                if (_VanChuyenProfitCenter == null)
                {
                    _VanChuyenProfitCenter = new T_MD_VAN_CHUYEN_PROFIT_CENTER();
                }
                return _VanChuyenProfitCenter;
            }
            set
            {
                _VanChuyenProfitCenter = value;
            }
        }

        public static explicit operator T_MD_KHOAN_MUC_VAN_CHUYEN(T_BP_KE_HOACH_VAN_CHUYEN_DATA_HISTORY v)
        {
            if (v == null)
            {
                return new T_MD_KHOAN_MUC_VAN_CHUYEN();
            }
            return new T_MD_KHOAN_MUC_VAN_CHUYEN
            {
                TEMPLATE_CODE = v.TEMPLATE_CODE + v.VAN_CHUYEN_PROFIT_CENTER_CODE,
                TEMPLATE_CODE_PURE = v.TEMPLATE_CODE,
                ORG_CODE = v.ORG_CODE,
                ORG_NAME = string.IsNullOrEmpty(v.TEMPLATE_CODE) ? v.Organize.NAME : $"Phòng ban: {v.VanChuyenProfitCenter.Organize.NAME}\nDự án: {v.VanChuyenProfitCenter.Route.NAME}\nMẫu: {v.TEMPLATE_CODE}\nĐơn vị nộp: {v.Template.Organize.NAME}\nTrạng thái: {Approve_Status.GetStatusText(v.STATUS)}",
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

        public static explicit operator T_MD_KHOAN_MUC_VAN_CHUYEN(T_BP_KE_HOACH_VAN_CHUYEN_DATA v)
        {
            if (v == null)
            {
                return new T_MD_KHOAN_MUC_VAN_CHUYEN();
            }
            return new T_MD_KHOAN_MUC_VAN_CHUYEN
            {
                TEMPLATE_CODE = v.TEMPLATE_CODE + v.VAN_CHUYEN_PROFIT_CENTER_CODE,
                TEMPLATE_CODE_PURE = v.TEMPLATE_CODE,
                ORG_CODE = v.ORG_CODE,
                ORG_NAME = string.IsNullOrEmpty(v.TEMPLATE_CODE) ? v.Organize.NAME : $"Phòng ban: {v.VanChuyenProfitCenter.Organize.NAME}\nDự án: {v.VanChuyenProfitCenter.Route.NAME}\nMẫu: {v.TEMPLATE_CODE}\nĐơn vị nộp: {v.Template.Organize.NAME}\nTrạng thái: {Approve_Status.GetStatusText(v.STATUS)}",
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

        public static explicit operator T_MD_KHOAN_MUC_VAN_CHUYEN(T_BP_KE_HOACH_VAN_CHUYEN_DATA_BASE v)
        {
            return new T_MD_KHOAN_MUC_VAN_CHUYEN
            {
                ORG_NAME = $"{v.MATERIAL} ({v.UNIT})",
                TEMPLATE_CODE = v.TEMPLATE_CODE + v.VAN_CHUYEN_PROFIT_CENTER_CODE,
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

        public static explicit operator T_MD_KHOAN_MUC_VAN_CHUYEN(T_BP_KE_HOACH_VAN_CHUYEN_DATA_BASE_HISTORY v)
        {
            return new T_MD_KHOAN_MUC_VAN_CHUYEN
            {
                ORG_NAME = $"{v.MATERIAL} ({v.UNIT})",
                TEMPLATE_CODE = v.TEMPLATE_CODE + v.VAN_CHUYEN_PROFIT_CENTER_CODE,
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
