using SMO.Core.Common;
using SMO.Core.Entities.BP.DAU_TU_NGOAI_DOANH_NGHIEP;
using SMO.Core.Entities.BP.DAU_TU_NGOAI_DOANH_NGHIEP.DAU_TU_NGOAI_DOANH_NGHIEP_DATA_BASE;
using SMO.Core.Entities.BP.DAU_TU_TRANG_THIET_BI;
using SMO.Core.Entities.BP.DAU_TU_TRANG_THIET_BI.DAU_TU_TRANG_THIET_BI_DATA_BASE;
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
        private T_MD_DAU_TU_TRANG_THIET_BI_PROFIT_CENTER _DauTuTrangThietBiProfitCenter;
        public virtual T_MD_DAU_TU_TRANG_THIET_BI_PROFIT_CENTER DauTuTrangThietBiProfitCenter
        {
            get
            {
                if (_DauTuTrangThietBiProfitCenter == null)
                {
                    _DauTuTrangThietBiProfitCenter = new T_MD_DAU_TU_TRANG_THIET_BI_PROFIT_CENTER();
                }
                return _DauTuTrangThietBiProfitCenter;
            }
            set
            {
                _DauTuTrangThietBiProfitCenter = value;
            }
        }

        private T_MD_DAU_TU_NGOAI_DOANH_NGHIEP_PROFIT_CENTER _DauTuNgoaiDoanhNghiepProfitCenter;
        public virtual T_MD_DAU_TU_NGOAI_DOANH_NGHIEP_PROFIT_CENTER DauTuNgoaiDoanhNghiepProfitCenter
        {
            get
            {
                if (_DauTuNgoaiDoanhNghiepProfitCenter == null)
                {
                    _DauTuNgoaiDoanhNghiepProfitCenter = new T_MD_DAU_TU_NGOAI_DOANH_NGHIEP_PROFIT_CENTER();
                }
                return _DauTuNgoaiDoanhNghiepProfitCenter;
            }
            set
            {
                _DauTuNgoaiDoanhNghiepProfitCenter = value;
            }
        }
        public T_MD_KHOAN_MUC_DAU_TU() : base()
        {
            Values = new decimal[6];
        }
        public virtual string EQUITY_SOURCES { get; set; }
        public virtual string TDTK { get; set; }
        public virtual string QKH { get; set; }

        // Trung gian để view

        public virtual string PROJECT_CODE { get; set; }
        public virtual string PROJECT_NAME { get; set; }

        public virtual decimal VALUE_1 { get; set; }
        public virtual string VALUE_2 { get; set; }

        public virtual string VALUE_3 { get; set; }

        public virtual decimal VALUE_4 { get; set; }
        public virtual decimal VALUE_5{ get; set; }
        public virtual string VALUE_6 { get; set; }
        public virtual decimal VALUE_7 { get; set; }
        public virtual decimal VALUE_8 { get; set; }

        // Value TTB
        public virtual decimal VALUETTB_1 { get; set; }
        public virtual string VALUETTB_2 { get; set; }

        public virtual string VALUETTB_3 { get; set; }

        public virtual string VALUETTB_4 { get; set; }
        public virtual decimal VALUETTB_5 { get; set; }
        public virtual decimal VALUETTB_6 { get; set; }
        public virtual decimal VALUETTB_7 { get; set; }
        public virtual string VALUETTB_8 { get; set; }
        public virtual decimal VALUETTB_9 { get; set; }
        public virtual decimal VALUETTB_10 { get; set; }
        public virtual bool ISEDIT { get; set; }

        public virtual string TYPE { get; set; }
        public virtual int LEVEL { get; set; }
        public virtual int ORDER { get; set; }
        public virtual string PARENT_ORDER { get; set; }


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

        //
        public static explicit operator T_MD_KHOAN_MUC_DAU_TU(T_BP_DAU_TU_TRANG_THIET_BI_DATA_HISTORY v)
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
                ORG_NAME = string.IsNullOrEmpty(v.TEMPLATE_CODE) ? v.Organize.NAME : $"Phòng ban: {v.DauTuTrangThietBiProfitCenter.Organize.NAME}\nDự án: {v.DauTuTrangThietBiProfitCenter.Project.NAME}\nMẫu: {v.TEMPLATE_CODE}\nĐơn vị nộp: {v.Template.Organize.NAME}\nTrạng thái: {Approve_Status.GetStatusText(v.STATUS)}",
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

        public static explicit operator T_MD_KHOAN_MUC_DAU_TU(T_BP_DAU_TU_TRANG_THIET_BI_DATA v)
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
                ORG_NAME = string.IsNullOrEmpty(v.TEMPLATE_CODE) ? v.Organize.NAME : $"Phòng ban: {v.DauTuTrangThietBiProfitCenter.Organize.NAME}\nDự án: {v.DauTuTrangThietBiProfitCenter.Project.NAME}\nMẫu: {v.TEMPLATE_CODE}\nĐơn vị nộp: {v.Template.Organize.NAME}\nTrạng thái: {Approve_Status.GetStatusText(v.STATUS)}",
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

        public static explicit operator T_MD_KHOAN_MUC_DAU_TU(T_BP_DAU_TU_TRANG_THIET_BI_DATA_BASE v)
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

        public static explicit operator T_MD_KHOAN_MUC_DAU_TU(T_BP_DAU_TU_TRANG_THIET_BI_DATA_BASE_HISTORY v)
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

        //
        public static explicit operator T_MD_KHOAN_MUC_DAU_TU(T_BP_DAU_TU_NGOAI_DOANH_NGHIEP_DATA_HISTORY v)
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
                ORG_NAME = string.IsNullOrEmpty(v.TEMPLATE_CODE) ? v.Organize.NAME : $"Phòng ban: {v.DauTuNgoaiDoanhNghiepProfitCenter.Organize.NAME}\nDự án: {v.DauTuNgoaiDoanhNghiepProfitCenter.Project.NAME}\nMẫu: {v.TEMPLATE_CODE}\nĐơn vị nộp: {v.Template.Organize.NAME}\nTrạng thái: {Approve_Status.GetStatusText(v.STATUS)}",
                CENTER_CODE = v.ORG_CODE,
                Values = new decimal[6]
                {
                    v.VALUE_1 ?? 0,
                    v.VALUE_2 ?? 0,
                    v.VALUE_3 ?? 0,
                    v.VALUE_4 ?? 0,
                    v.VALUE_5 ?? 0,
                    v.VALUE_6 ?? 0,
                },
                VERSION = v.VERSION,
                DESCRIPTION = v.DESCRIPTION,
                Template = v.Template
            };
        }

        public static explicit operator T_MD_KHOAN_MUC_DAU_TU(T_BP_DAU_TU_NGOAI_DOANH_NGHIEP_DATA v)
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
                ORG_NAME = string.IsNullOrEmpty(v.TEMPLATE_CODE) ? v.Organize.NAME : $"Phòng ban: {v.DauTuNgoaiDoanhNghiepProfitCenter.Organize.NAME}\nDự án: {v.DauTuNgoaiDoanhNghiepProfitCenter.Project.NAME}\nMẫu: {v.TEMPLATE_CODE}\nĐơn vị nộp: {v.Template.Organize.NAME}\nTrạng thái: {Approve_Status.GetStatusText(v.STATUS)}",
                CENTER_CODE = v.ORG_CODE,
                Values = new decimal[6]
                {
                    v.VALUE_1 ?? 0,
                    v.VALUE_2 ?? 0,
                    v.VALUE_3 ?? 0,
                    v.VALUE_4 ?? 0,
                    v.VALUE_5 ?? 0,
                    v.VALUE_6 ?? 0,
                },
                VERSION = v.VERSION,
                DESCRIPTION = v.DESCRIPTION,
                Template = v.Template
            };
        }

        public static explicit operator T_MD_KHOAN_MUC_DAU_TU(T_BP_DAU_TU_NGOAI_DOANH_NGHIEP_DATA_BASE v)
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
                ValuesBaseString = new string[6]
                {
                    HandleValueBaseString(quantity: v.QUANTITY_1, price: v.PRICE_1, amount: v.AMOUNT_1, time: v.TIME_1),
                    HandleValueBaseString(quantity: v.QUANTITY_2, price: v.PRICE_2, amount: v.AMOUNT_2, time: v.TIME_2),
                    HandleValueBaseString(quantity: v.QUANTITY_3, price: v.PRICE_3, amount: v.AMOUNT_3, time: v.TIME_3),
                    HandleValueBaseString(quantity: v.QUANTITY_4, price: v.PRICE_4, amount: v.AMOUNT_4, time: v.TIME_4),
                    HandleValueBaseString(quantity: v.QUANTITY_5, price: v.PRICE_5, amount: v.AMOUNT_5, time: v.TIME_5),
                    HandleValueBaseString(quantity: v.QUANTITY_6, price: v.PRICE_6, amount: v.AMOUNT_6, time: v.TIME_6),
                },
            };
        }

        public static explicit operator T_MD_KHOAN_MUC_DAU_TU(T_BP_DAU_TU_NGOAI_DOANH_NGHIEP_DATA_BASE_HISTORY v)
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
                ValuesBaseString = new string[6]
                {
                    HandleValueBaseString(quantity: v.QUANTITY_1, price: v.PRICE_1, amount: v.AMOUNT_1, time: v.TIME_1),
                    HandleValueBaseString(quantity: v.QUANTITY_2, price: v.PRICE_2, amount: v.AMOUNT_2, time: v.TIME_2),
                    HandleValueBaseString(quantity: v.QUANTITY_3, price: v.PRICE_3, amount: v.AMOUNT_3, time: v.TIME_3),
                    HandleValueBaseString(quantity: v.QUANTITY_4, price: v.PRICE_4, amount: v.AMOUNT_4, time: v.TIME_4),
                    HandleValueBaseString(quantity: v.QUANTITY_5, price: v.PRICE_5, amount: v.AMOUNT_5, time: v.TIME_5),
                    HandleValueBaseString(quantity: v.QUANTITY_6, price: v.PRICE_6, amount: v.AMOUNT_6, time: v.TIME_6),
                },
            };
        }

    }
}
