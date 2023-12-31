﻿using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace SMO.Core.Entities.MD
{
    public class T_MD_TEMPLATE : BaseEntity, ICloneable
    {
        //public T_MD_TEMPLATE()
        //{
        //    DetailCosts = new List<T_MD_TEMPLATE_DETAIL_COST_PL>();
        //    DetailRevenues = new List<T_MD_TEMPLATE_DETAIL_REVENUE_PL>();
        //}
        [Required(ErrorMessage = "Trường này bắt buộc nhập", AllowEmptyStrings = false)]
        [MaxLength(length: 50, ErrorMessage = "Chỉ được phép nhập tối đa {1} kí tự")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Không được phép nhập dấu cách")]
        public virtual string CODE { get; set; }
        [Required(ErrorMessage = "Trường này bắt buộc nhập", AllowEmptyStrings = false)]
        public virtual string NAME { get; set; }
        [Required(ErrorMessage = "Trường này bắt buộc nhập", AllowEmptyStrings = false)]
        public virtual string TITLE { get; set; }
        [Required(ErrorMessage = "Trường này bắt buộc nhập", AllowEmptyStrings = false)]
        public virtual string ORG_CODE { get; set; }
        [Required(ErrorMessage = "Trường này bắt buộc nhập", AllowEmptyStrings = false)]
        public virtual string ELEMENT_TYPE { get; set; }
        /// <summary>
        /// Lấy giá trị từ constant class BudgetType
        /// </summary>
        [Required(ErrorMessage = "Trường này bắt buộc nhập", AllowEmptyStrings = false)]
        public virtual string BUDGET_TYPE { get; set; }
        /// <summary>
        /// Lấy giá trị từ constant class TemplateObjectType
        /// </summary>
        [Required(ErrorMessage = "Trường này bắt buộc nhập", AllowEmptyStrings = false)]
        public virtual string OBJECT_TYPE { get; set; }
        public virtual string NOTES { get; set; }
        public virtual bool IS_BASE { get; set; }

        [ScriptIgnore]
        [JsonIgnore]
        public virtual T_MD_COST_CENTER Organize { get; set; }
        
        [ScriptIgnore]
        [JsonIgnore]
        public virtual IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_SAN_LUONG> DetailKeHoachSanLuong { get; set; }
        [ScriptIgnore]
        [JsonIgnore]
        public virtual IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_DOANH_THU> DetailKeHoachDoanhThu { get; set; }
        [ScriptIgnore]
        [JsonIgnore]
        public virtual IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_CHI_PHI> DetailKeHoachChiPhi { get; set; }
        [ScriptIgnore]
        [JsonIgnore]
        public virtual IList<T_MD_TEMPLATE_DETAIL_DAU_TU_XAY_DUNG> DetailDauTuXayDung { get; set; }
        [ScriptIgnore]
        [JsonIgnore]
        public virtual IList<T_MD_TEMPLATE_DETAIL_DAU_TU_TRANG_THIET_BI> DetailDauTuTrangThietBi { get; set; }
        [ScriptIgnore]
        [JsonIgnore]
        public virtual IList<T_MD_TEMPLATE_DETAIL_DAU_TU_NGOAI_DOANH_NGHIEP> DetailDauTuNgoaiDoanhNghiep { get; set; }
        [ScriptIgnore]
        [JsonIgnore]
        public virtual IList<T_MD_TEMPLATE_DETAIL_KE_HOACH_VAN_CHUYEN> DetailKeHoachVanChuyen { get; set; }
        [ScriptIgnore]
        [JsonIgnore]
        public virtual IList<T_MD_TEMPLATE_DETAIL_SUA_CHUA_LON> DetailSuaChuaLon { get; set; }

        [ScriptIgnore]
        [JsonIgnore]
        public virtual IList<T_MD_TEMPLATE_DETAIL_SUA_CHUA_THUONG_XUYEN> DetailSuaChuaThuongXuyen { get; set; }

        public virtual object Clone()
        {
            return MemberwiseClone();
        }
    }
}
