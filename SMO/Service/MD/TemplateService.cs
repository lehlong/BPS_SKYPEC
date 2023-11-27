using NHibernate;
using NHibernate.Linq;
using SMO.Core.Common;
using SMO.Core.Entities;
using SMO.Core.Entities.BP;
using SMO.Core.Entities.BP.DAU_TU_NGOAI_DOANH_NGHIEP;
using SMO.Core.Entities.BP.DAU_TU_TRANG_THIET_BI;
using SMO.Core.Entities.BP.DAU_TU_XAY_DUNG;
using SMO.Core.Entities.BP.KE_HOACH_CHI_PHI;
using SMO.Core.Entities.BP.KE_HOACH_SAN_LUONG;
using SMO.Core.Entities.BP.KE_HOACH_VAN_CHUYEN;
using SMO.Core.Entities.BP.SUA_CHUA_LON;
using SMO.Core.Entities.BP.SUA_CHUA_THUONG_XUYEN;
using SMO.Core.Entities.MD;
using SMO.Repository.Common;
using SMO.Repository.Implement.BP;
using SMO.Repository.Implement.BP.DAU_TU_NGOAI_DOANH_NGHIEP;
using SMO.Repository.Implement.BP.DAU_TU_TRANG_THIET_BI;
using SMO.Repository.Implement.BP.DAU_TU_XAY_DUNG;
using SMO.Repository.Implement.BP.KE_HOACH_CHI_PHI;
using SMO.Repository.Implement.BP.KE_HOACH_SAN_LUONG;
using SMO.Repository.Implement.BP.KE_HOACH_VAN_CHUYEN;
using SMO.Repository.Implement.BP.SUA_CHUA_LON;
using SMO.Repository.Implement.BP.SUA_CHUA_THUONG_XUYEN;
using SMO.Repository.Implement.MD;

using System;
using System.Collections.Generic;
using System.Linq;

namespace SMO.Service.MD
{
    public class TemplateService : GenericService<T_MD_TEMPLATE, TemplateRepo>
    {
        public int TIME_YEAR { get; set; }
        public string TEMPLATE_REFERENCE { get; set; }
        public override void Create()
        {
            if (string.IsNullOrEmpty(ObjDetail.NAME))
            {
                State = false;
                ErrorMessage = "Vui lòng nhập tên biểu mẫu!";
                return;
            }
            if (string.IsNullOrEmpty(ObjDetail.CODE))
            {
                State = false;
                MesseageCode = "1101";
                return;
            }
            var lstDetail = UnitOfWork.Repository<TemplateDetailKeHoachSanLuongRepo>().Queryable().Where(x => x.TEMPLATE_CODE == this.TEMPLATE_REFERENCE && x.TIME_YEAR == this.TIME_YEAR).ToList();
            if (lstDetail.Count() == 0 && this.TEMPLATE_REFERENCE != "-")
            {
                State = false;
                ErrorMessage = "Biểu mẫu nguồn cần copy không có dữ liệu!";
                return;
            }
            try
            {
                if (!CheckExist(x => x.CODE == ObjDetail.CODE))
                {
                    ObjDetail.ACTIVE = true;
                    base.Create();
                }
                else
                {
                    State = false;
                    MesseageCode = "1101";
                }
                if (this.TEMPLATE_REFERENCE != "-")
                {
                    UnitOfWork.BeginTransaction();
                    foreach (var item in lstDetail)
                    {
                        UnitOfWork.Repository<TemplateDetailKeHoachSanLuongRepo>().Create(new T_MD_TEMPLATE_DETAIL_KE_HOACH_SAN_LUONG
                        {
                            PKID = Guid.NewGuid().ToString(),
                            CENTER_CODE = item.CENTER_CODE,
                            ELEMENT_CODE = item.ELEMENT_CODE,
                            TEMPLATE_CODE = this.ObjDetail.CODE,
                            TIME_YEAR = this.TIME_YEAR,
                        });
                    }
                    UnitOfWork.Commit();
                }

            }
            catch (Exception ex)
            {
                State = false;
                Exception = ex;
            }
        }

        public override void Search()
        {
            if (!AuthorizeUtilities.IGNORE_USERS.Contains(ProfileUtilities.User.USER_NAME))
            {
                ObjDetail.ORG_CODE = ProfileUtilities.User.ORGANIZE_CODE;
            }
            base.Search();
        }

        #region Update detail information
        internal void UpdateDetailInformation(string centerCode, string template, int year, IList<string> detailCodes, Budget budget)
        {
            Get(template);
            switch (ObjDetail.OBJECT_TYPE.Trim())
            {
                case TemplateObjectType.SanLuong:
                    switch (budget)
                    {
                        case Budget.SAN_LUONG:
                            UpdateDetail<T_MD_TEMPLATE_DETAIL_KE_HOACH_SAN_LUONG, TemplateDetailKeHoachSanLuongRepo, T_MD_KHOAN_MUC_SAN_LUONG, T_MD_SAN_LUONG_PROFIT_CENTER, KeHoachSanLuongRepo, T_BP_KE_HOACH_SAN_LUONG>(centerCode, template, detailCodes, year);
                            break;

                        default:
                            Exception = new FormatException("Type of center not support");
                            State = false;
                            ErrorMessage = "Type of center not support";
                            break;
                    }
                    break;
                case TemplateObjectType.SuaChuaLon:
                    switch (budget)
                    {
                        case Budget.SUA_CHUA:
                            UpdateDetail<T_MD_TEMPLATE_DETAIL_SUA_CHUA_LON, TemplateDetailSuaChuaLonRepo, T_MD_KHOAN_MUC_SUA_CHUA, T_MD_SUA_CHUA_PROFIT_CENTER, SuaChuaLonRepo, T_BP_SUA_CHUA_LON>(centerCode, template, detailCodes, year);
                            break;

                        default:
                            Exception = new FormatException("Type of center not support");
                            State = false;
                            ErrorMessage = "Type of center not support";
                            break;
                    }
                    break;
                case TemplateObjectType.SuaChuaThuongXuyen:
                    switch (budget)
                    {
                        case Budget.SUA_CHUA:
                            UpdateDetail<T_MD_TEMPLATE_DETAIL_SUA_CHUA_THUONG_XUYEN, TemplateDetailSuaChuaThuongXuyenRepo, T_MD_KHOAN_MUC_SUA_CHUA, T_MD_SUA_CHUA_THUONG_XUYEN_PROFIT_CENTER, SuaChuaThuongXuyenRepo, T_BP_SUA_CHUA_THUONG_XUYEN>(centerCode, template, detailCodes, year);
                            break;

                        default:
                            Exception = new FormatException("Type of center not support");
                            State = false;
                            ErrorMessage = "Type of center not support";
                            break;
                    }
                    break;
                case TemplateObjectType.DoanhThu:
                    switch (budget)
                    {
                        case Budget.DOANH_THU:
                            UpdateDetail<T_MD_TEMPLATE_DETAIL_KE_HOACH_SAN_LUONG, TemplateDetailKeHoachSanLuongRepo, T_MD_KHOAN_MUC_SAN_LUONG, T_MD_SAN_LUONG_PROFIT_CENTER, KeHoachSanLuongRepo, T_BP_KE_HOACH_SAN_LUONG>(centerCode, template, detailCodes, year);
                            break;

                        default:
                            Exception = new FormatException("Type of center not support");
                            State = false;
                            ErrorMessage = "Type of center not support";
                            break;
                    }
                    break;
                case TemplateObjectType.ChiPhi:
                    switch (budget)
                    {
                        case Budget.CHI_PHI:
                            UpdateDetail<T_MD_TEMPLATE_DETAIL_KE_HOACH_CHI_PHI, TemplateDetailKeHoachChiPhiRepo, T_MD_KHOAN_MUC_HANG_HOA, T_MD_CHI_PHI_PROFIT_CENTER, KeHoachChiPhiRepo, T_BP_KE_HOACH_CHI_PHI>(centerCode, template, detailCodes, year);
                            break;

                        default:
                            Exception = new FormatException("Type of center not support");
                            State = false;
                            ErrorMessage = "Type of center not support";
                            break;
                    }
                    break;
                case TemplateObjectType.DauTuTrangThietBi:
                    switch (budget)
                    {
                        case Budget.DAU_TU_TRANG_THIET_BI:
                            UpdateDetail<T_MD_TEMPLATE_DETAIL_DAU_TU_TRANG_THIET_BI, TemplateDetailDauTuTrangThietBiRepo, T_MD_KHOAN_MUC_DAU_TU, T_MD_DAU_TU_TRANG_THIET_BI_PROFIT_CENTER, DauTuTrangThietBiRepo, T_BP_DAU_TU_TRANG_THIET_BI>(centerCode, template, detailCodes, year);
                            break;

                        default:
                            Exception = new FormatException("Type of center not support");
                            State = false;
                            ErrorMessage = "Type of center not support";
                            break;
                    }
                    break;
                case TemplateObjectType.DauTuXayDung:
                    switch (budget)
                    {
                        case Budget.DAU_TU_XAY_DUNG:
                            UpdateDetail<T_MD_TEMPLATE_DETAIL_DAU_TU_XAY_DUNG, TemplateDetailDauTuXayDungRepo, T_MD_KHOAN_MUC_DAU_TU, T_MD_DAU_TU_XAY_DUNG_PROFIT_CENTER, DauTuXayDungRepo, T_BP_DAU_TU_XAY_DUNG>(centerCode, template, detailCodes, year);
                            break;

                        default:
                            Exception = new FormatException("Type of center not support");
                            State = false;
                            ErrorMessage = "Type of center not support";
                            break;
                    }
                    break;
                case TemplateObjectType.DauTuNgoaiDoanhNghiep:
                    switch (budget)
                    {
                        case Budget.DAU_TU_NGOAI_DOANH_NGHIEP:
                            UpdateDetail<T_MD_TEMPLATE_DETAIL_DAU_TU_NGOAI_DOANH_NGHIEP, TemplateDetailDauTuNgoaiDoanhNghiepRepo, T_MD_KHOAN_MUC_DAU_TU, T_MD_DAU_TU_NGOAI_DOANH_NGHIEP_PROFIT_CENTER, DauTuNgoaiDoanhNghiepRepo, T_BP_DAU_TU_NGOAI_DOANH_NGHIEP>(centerCode, template, detailCodes, year);
                            break;

                        default:
                            Exception = new FormatException("Type of center not support");
                            State = false;
                            ErrorMessage = "Type of center not support";
                            break;
                    }
                    break;

                case TemplateObjectType.VanChuyen:
                    switch (budget)
                    {
                        case Budget.VAN_CHUYEN:
                            UpdateDetail<T_MD_TEMPLATE_DETAIL_KE_HOACH_VAN_CHUYEN, TemplateDetailKeHoachVanChuyenRepo, T_MD_KHOAN_MUC_VAN_CHUYEN, T_MD_VAN_CHUYEN_PROFIT_CENTER, KeHoachVanChuyenRepo, T_BP_KE_HOACH_VAN_CHUYEN>(centerCode, template, detailCodes, year);
                            break;

                        default:
                            Exception = new FormatException("Type of center not support");
                            State = false;
                            ErrorMessage = "Type of center not support";
                            break;
                    }
                    break;
                default:
                    break;
            }

        }

        private void UpdateDetail<TTemplateDetail, TTemplateDetailRepo, TElement, TCenter, TBPRepo, TBPEntity>(string centerCode, string template, IList<string> detailCodes, int year)
            where TTemplateDetail : BaseTemplateDetail<TElement, TCenter>
            where TTemplateDetailRepo : GenericTemplateDetailRepository<TTemplateDetail, TElement, TCenter>
            where TElement : CoreElement
            where TCenter : CoreCenter
            where TBPRepo : GenericBPRepository<TBPEntity>
            where TBPEntity : T_BP_BASE
        {
            if (detailCodes != null)
            {
                var detailsCost = from d in detailCodes
                                  select (TTemplateDetail)Activator.CreateInstance(typeof(TTemplateDetail), new object[]
                                  {
                                  Guid.NewGuid().ToString(), template, d, centerCode, year
                                  });
                if (detailsCost.Count() > 0)
                {
                    try
                    {
                        var repo = UnitOfWork.Repository<TTemplateDetailRepo>();
                        var existLstItems = repo.GetManyByExpression(x => x.TEMPLATE_CODE == template && x.CENTER_CODE == centerCode);
                        // lst delete
                        var deleteLst = existLstItems.Where(x => !detailsCost.Contains(x) && x.TIME_YEAR == year).ToList();
                        // check nếu mẫu đã được xử dụng thì không cho phép xóa
                        if (deleteLst.Count > 0 && UnitOfWork.Repository<TBPRepo>().CheckExist(x => x.TIME_YEAR == year && x.TEMPLATE_CODE == template))
                        {
                            ErrorMessage = "Không thể xóa khoản mục do khoản mục nằm trong mẫu đã được xử dụng";
                            State = false;
                            return;
                        }
                        // lst new
                        var addLst = detailsCost.Where(x => !existLstItems.Contains(x) && x.TIME_YEAR == year).ToList();
                        UnitOfWork.BeginTransaction();
                        repo.Delete(deleteLst);
                        repo.Create(addLst.ToList());
                        UnitOfWork.Commit();
                    }
                    catch (Exception e)
                    {
                        UnitOfWork.Rollback();
                        Exception = e;
                        ErrorMessage = e.Message;
                        State = false;
                    }
                }
            }
            else
            {
                try
                {
                    UnitOfWork.BeginTransaction();
                    var repo = UnitOfWork.Repository<TTemplateDetailRepo>();

                    repo.Delete(x => x.TEMPLATE_CODE == template && x.CENTER_CODE == centerCode);
                    UnitOfWork.Commit();
                }
                catch (Exception e)
                {
                    UnitOfWork.Rollback();
                    Exception = e;
                    ErrorMessage = e.Message;
                    State = false;
                }
            }
        }
        #endregion

        #region Get node details by center code
        internal IEnumerable<string> GetNodeDetailOtherCompany(Budget budget, string projectCode, int year, string templateId)
        {
            if (budget != Budget.OTHER_PROFIT_CENTER)
            {
                return new List<string>();
            }
            Get(templateId);
            if (ObjDetail == null)
            {
                return new List<string>();
            }
            switch (ObjDetail.OBJECT_TYPE.Trim())
            {
                case TemplateObjectType.SanLuong:

                    return UnitOfWork.Repository<TemplateDetailKeHoachSanLuongRepo>()
                        .GetManyWithFetch(x => x.TIME_YEAR == year && x.TEMPLATE_CODE == templateId, x => x.Center)
                        .Select(x => x.Center)
                        .Where(x => x.SAN_BAY_CODE == projectCode)
                        .Select(x => x.HANG_HANG_KHONG_CODE)
                        .Distinct();
                case TemplateObjectType.SuaChuaLon:

                    return UnitOfWork.Repository<TemplateDetailSuaChuaLonRepo>()
                        .GetManyWithFetch(x => x.TIME_YEAR == year && x.TEMPLATE_CODE == templateId, x => x.Center)
                        .Select(x => x.Center)
                        .Where(x => x.COST_CENTER_CODE == projectCode)
                        .Select(x => x.SAN_BAY_CODE)
                        .Distinct();
                case TemplateObjectType.SuaChuaThuongXuyen:

                    return UnitOfWork.Repository<TemplateDetailSuaChuaThuongXuyenRepo>()
                        .GetManyWithFetch(x => x.TIME_YEAR == year && x.TEMPLATE_CODE == templateId, x => x.Center)
                        .Select(x => x.Center)
                        .Where(x => x.COST_CENTER_CODE == projectCode)
                        .Select(x => x.SAN_BAY_CODE)
                        .Distinct();
                case TemplateObjectType.DoanhThu:
                    return UnitOfWork.Repository<TemplateDetailKeHoachSanLuongRepo>()
                        .GetManyWithFetch(x => x.TIME_YEAR == year && x.TEMPLATE_CODE == templateId, x => x.Center)
                        .Select(x => x.Center)
                        .Where(x => x.SAN_BAY_CODE == projectCode)
                        .Select(x => x.HANG_HANG_KHONG_CODE)
                        .Distinct();
                case TemplateObjectType.ChiPhi:

                    return UnitOfWork.Repository<TemplateDetailKeHoachChiPhiRepo>()
                        .GetManyWithFetch(x => x.TIME_YEAR == year && x.TEMPLATE_CODE == templateId, x => x.Center)
                        .Select(x => x.Center)
                        .Where(x => x.COST_CENTER_CODE == projectCode)
                        .Select(x => x.SAN_BAY_CODE)
                        .Distinct();

                case TemplateObjectType.DauTuXayDung:
                    return UnitOfWork.Repository<TemplateDetailDauTuXayDungRepo>()
                        .GetManyWithFetch(x => x.TIME_YEAR == year && x.TEMPLATE_CODE == templateId, x => x.Center)
                        .Select(x => x.Center)
                        .Where(x => x.ORG_CODE == projectCode)
                        .Select(x => x.PROJECT_CODE)
                        .Distinct();
                case TemplateObjectType.VanChuyen:
                    return UnitOfWork.Repository<TemplateDetailKeHoachVanChuyenRepo>()
                        .GetManyWithFetch(x => x.TIME_YEAR == year && x.TEMPLATE_CODE == templateId, x => x.Center)
                        .Select(x => x.Center)
                        .Where(x => x.ORG_CODE == projectCode)
                        .Select(x => x.ROUTE_CODE)
                        .Distinct();
                case TemplateObjectType.DauTuTrangThietBi:
                    return UnitOfWork.Repository<TemplateDetailDauTuTrangThietBiRepo>()
                        .GetManyWithFetch(x => x.TIME_YEAR == year && x.TEMPLATE_CODE == templateId, x => x.Center)
                        .Select(x => x.Center)
                        .Where(x => x.ORG_CODE == projectCode)
                        .Select(x => x.PROJECT_CODE)
                        .Distinct();
                default:
                    return new List<string>();
            }
        }

        internal IEnumerable<string> GetNodeDetailHangHangKhong(Budget budget, string projectCode, int year, string templateId)
        {
            Get(templateId);
            if (ObjDetail == null)
            {
                return new List<string>();
            }
            return UnitOfWork.Repository<TemplateDetailKeHoachSanLuongRepo>()
                        .GetManyWithFetch(x => x.TIME_YEAR == year && x.TEMPLATE_CODE == templateId, x => x.Center)
                        .Select(x => x.Center)
                        .Where(x => x.SAN_BAY_CODE == projectCode)
                        .Select(x => x.HANG_HANG_KHONG_CODE)
                        .Distinct();
        }

        internal IEnumerable<string> GetNodeDetailSanBay(Budget budget, string projectCode, int year, string templateId)
        {
            Get(templateId);
            if (ObjDetail == null)
            {
                return new List<string>();
            }
            if (ObjDetail.OBJECT_TYPE.Trim() == BudgetType.SuaChuaLon)
            {
                return UnitOfWork.Repository<TemplateDetailSuaChuaLonRepo>()
                                        .GetManyWithFetch(x => x.TIME_YEAR == year && x.TEMPLATE_CODE == templateId, x => x.Center)
                                        .Select(x => x.Center)
                                        .Where(x => x.COST_CENTER_CODE == projectCode)
                                        .Select(x => x.SAN_BAY_CODE)
                                        .Distinct();
            }
            else if (ObjDetail.OBJECT_TYPE.Trim() == BudgetType.ChiPhi)
            {
                return UnitOfWork.Repository<TemplateDetailKeHoachChiPhiRepo>()
                                        .GetManyWithFetch(x => x.TIME_YEAR == year && x.TEMPLATE_CODE == templateId, x => x.Center)
                                        .Select(x => x.Center)
                                        .Where(x => x.COST_CENTER_CODE == projectCode)
                                        .Select(x => x.SAN_BAY_CODE)
                                        .Distinct();
            }
            else
            {
                return UnitOfWork.Repository<TemplateDetailSuaChuaThuongXuyenRepo>()
                        .GetManyWithFetch(x => x.TIME_YEAR == year && x.TEMPLATE_CODE == templateId, x => x.Center)
                        .Select(x => x.Center)
                        .Where(x => x.COST_CENTER_CODE == projectCode)
                        .Select(x => x.SAN_BAY_CODE)
                        .Distinct();
            }

        }

        internal IEnumerable<string> GetNodeDetailCompany(Budget budget, string projectCode, int year, string templateId)
        {
            Get(templateId);
            if (ObjDetail == null)
            {
                return new List<string>();
            }
            return UnitOfWork.Repository<TemplateDetailDauTuXayDungRepo>()
                        .GetManyWithFetch(x => x.TIME_YEAR == year && x.TEMPLATE_CODE == templateId, x => x.Center)
                        .Select(x => x.Center)
                        .Where(x => x.PROJECT_CODE == projectCode)
                        .Select(x => x.ORG_CODE)
                        .Distinct();
        }

        internal IList<string> GetNodeDetailOtherElement(Budget budget, string projectCode, string companyCode, int year, string templateId)
        {
            if (budget != Budget.OTHER_PROFIT_CENTER)
            {
                return new List<string>();
            }
            var otherProfitCenterCode = GetOtherProfitCenter(companyCode, projectCode);
            if (otherProfitCenterCode == null)
            {
                return new List<string>();
            }
            else
            {
                return GetNodeDetailElement(budget, otherProfitCenterCode.CODE, year, templateId);
            }
        }

        internal IList<string> GetNodeDetailKhoanMucSanLuong(Budget budget, string projectCode, string companyCode, int year, string templateId)
        {
            var otherProfitCenterCode = GetSanLuongProfitCenter(companyCode, projectCode);
            if (otherProfitCenterCode == null)
            {
                return new List<string>();
            }
            else
            {
                return GetNodeDetailKhoanMucSanLuong(budget, otherProfitCenterCode.CODE, year, templateId);
            }
        }

        internal IList<string> GetNodeDetailKhoanMucSuaChua(Budget budget, string projectCode, string companyCode, int year, string templateId)
        {
            var otherProfitCenterCode = GetSuaChuaProfitCenter(companyCode, projectCode);
            if (otherProfitCenterCode == null)
            {
                return new List<string>();
            }
            else
            {
                return GetNodeDetailKhoanMucSuaChua(budget, otherProfitCenterCode.CODE, year, templateId);
            }
        }

        internal IList<string> GetNodeDetailKhoanMucChiPhi(Budget budget, string projectCode, string companyCode, int year, string templateId)
        {
            var otherProfitCenterCode = GetChiPhiProfitCenter(companyCode, projectCode);
            if (otherProfitCenterCode == null)
            {
                return new List<string>();
            }
            else
            {
                return GetNodeDetailKhoanMucHangHoa(budget, otherProfitCenterCode.CODE, year, templateId);
            }
        }

        internal IList<string> GetNodeDetailKhoanMucSuaChuaThuongXuyen(Budget budget, string projectCode, string companyCode, int year, string templateId)
        {
            var otherProfitCenterCode = GetSuaChuaThuongXuyenProfitCenter(companyCode, projectCode);
            if (otherProfitCenterCode == null)
            {
                return new List<string>();
            }
            else
            {
                return GetNodeDetailKhoanMucSuaChuaThuongXuyen(budget, otherProfitCenterCode.CODE, year, templateId);
            }
        }
        internal IList<string> GetNodeDetailKhoanMucHangHoa(Budget budget, string projectCode, string companyCode, int year, string templateId)
        {
            var otherProfitCenterCode = GetChiPhiProfitCenter(companyCode, projectCode);
            if (otherProfitCenterCode == null)
            {
                return new List<string>();
            }
            else
            {
                return GetNodeDetailKhoanMucHangHoa(budget, otherProfitCenterCode.CODE, year, templateId);
            }
        }

        internal IList<string> GetNodeDetailKhoanMucDauTuXayDung(Budget budget, string projectCode, string companyCode, int year, string templateId)
        {
            var otherProfitCenterCode = GetDauTuXayDungProfitCenter(companyCode, projectCode);
            if (otherProfitCenterCode == null)
            {
                return new List<string>();
            }
            else
            {
                return GetNodeDetailKhoanMucDauTuXayDung(budget, otherProfitCenterCode.CODE, year, templateId);
            }
        }

        internal IList<string> GetNodeDetailKhoanMucVanChuyen(Budget budget, string projectCode, string companyCode, int year, string templateId)
        {
            var otherProfitCenterCode = GetDauTuXayDungProfitCenter(companyCode, projectCode);
            if (otherProfitCenterCode == null)
            {
                return new List<string>();
            }
            else
            {
                return GetNodeDetailKhoanMucVanChuyen(budget, otherProfitCenterCode.CODE, year, templateId);
            }
        }

        internal IList<string> GetNodeDetailKhoanMucDoanhThu(Budget budget, string projectCode, string companyCode, int year, string templateId)
        {
            var otherProfitCenterCode = GetDoanhThuProfitCenter(companyCode, projectCode);
            if (otherProfitCenterCode == null)
            {
                return new List<string>();
            }
            else
            {
                return GetNodeDetailKhoanMucDoanhThu(budget, otherProfitCenterCode.CODE, year, templateId);
            }
        }

        internal IList<string> GetNodeDetailElement(Budget budget, string centerCode, int year, string templateId)
        {
            Get(templateId);
            switch (ObjDetail.OBJECT_TYPE.Trim())
            {
                case TemplateObjectType.SanLuong:
                    return GetNodeDetailElement<T_MD_TEMPLATE_DETAIL_KE_HOACH_SAN_LUONG, TemplateDetailKeHoachSanLuongRepo, T_MD_KHOAN_MUC_SAN_LUONG, T_MD_SAN_LUONG_PROFIT_CENTER>(centerCode, year, templateId);
                case TemplateObjectType.DoanhThu:
                    return GetNodeDetailElement<T_MD_TEMPLATE_DETAIL_KE_HOACH_DOANH_THU, TemplateDetailKeHoachDoanhThuRepo, T_MD_KHOAN_MUC_DOANH_THU, T_MD_DOANH_THU_PROFIT_CENTER>(centerCode, year, templateId);
                case TemplateObjectType.ChiPhi:
                    return GetNodeDetailElement<T_MD_TEMPLATE_DETAIL_KE_HOACH_CHI_PHI, TemplateDetailKeHoachChiPhiRepo, T_MD_KHOAN_MUC_HANG_HOA, T_MD_CHI_PHI_PROFIT_CENTER>(centerCode, year, templateId);
                case TemplateObjectType.DauTuXayDung:
                    return GetNodeDetailElement<T_MD_TEMPLATE_DETAIL_DAU_TU_XAY_DUNG, TemplateDetailDauTuXayDungRepo, T_MD_KHOAN_MUC_DAU_TU, T_MD_DAU_TU_XAY_DUNG_PROFIT_CENTER>(centerCode, year, templateId);
                case TemplateObjectType.DauTuTrangThietBi:
                    return GetNodeDetailElement<T_MD_TEMPLATE_DETAIL_DAU_TU_TRANG_THIET_BI, TemplateDetailDauTuTrangThietBiRepo, T_MD_KHOAN_MUC_DAU_TU, T_MD_DAU_TU_TRANG_THIET_BI_PROFIT_CENTER>(centerCode, year, templateId);
                case TemplateObjectType.DauTuNgoaiDoanhNghiep:
                    return GetNodeDetailElement<T_MD_TEMPLATE_DETAIL_DAU_TU_NGOAI_DOANH_NGHIEP, TemplateDetailDauTuNgoaiDoanhNghiepRepo, T_MD_KHOAN_MUC_DAU_TU, T_MD_DAU_TU_NGOAI_DOANH_NGHIEP_PROFIT_CENTER>(centerCode, year, templateId);
                case TemplateObjectType.VanChuyen:
                    return GetNodeDetailElement<T_MD_TEMPLATE_DETAIL_KE_HOACH_VAN_CHUYEN, TemplateDetailKeHoachVanChuyenRepo, T_MD_KHOAN_MUC_VAN_CHUYEN, T_MD_VAN_CHUYEN_PROFIT_CENTER>(centerCode, year, templateId);
                case TemplateObjectType.SuaChuaLon:
                    return GetNodeDetailElement<T_MD_TEMPLATE_DETAIL_SUA_CHUA_LON, TemplateDetailSuaChuaLonRepo, T_MD_KHOAN_MUC_SUA_CHUA, T_MD_SUA_CHUA_PROFIT_CENTER>(centerCode, year, templateId);
                case TemplateObjectType.SuaChuaThuongXuyen:
                    return GetNodeDetailElement<T_MD_TEMPLATE_DETAIL_SUA_CHUA_THUONG_XUYEN, TemplateDetailSuaChuaThuongXuyenRepo, T_MD_KHOAN_MUC_SUA_CHUA, T_MD_SUA_CHUA_THUONG_XUYEN_PROFIT_CENTER>(centerCode, year, templateId);

                default:
                    return new List<string>();
            }
        }

        internal IList<string> GetNodeDetailKhoanMucSanLuong(Budget budget, string centerCode, int year, string templateId)
        {
            Get(templateId);
            return GetNodeDetailElement<T_MD_TEMPLATE_DETAIL_KE_HOACH_SAN_LUONG, TemplateDetailKeHoachSanLuongRepo, T_MD_KHOAN_MUC_SAN_LUONG, T_MD_SAN_LUONG_PROFIT_CENTER>(centerCode, year, templateId);
        }
        internal IList<string> GetNodeDetailKhoanMucSuaChua(Budget budget, string centerCode, int year, string templateId)
        {
            Get(templateId);
            return GetNodeDetailElement<T_MD_TEMPLATE_DETAIL_SUA_CHUA_LON, TemplateDetailSuaChuaLonRepo, T_MD_KHOAN_MUC_SUA_CHUA, T_MD_SUA_CHUA_PROFIT_CENTER>(centerCode, year, templateId);
        }

        internal IList<string> GetNodeDetailKhoanMucHangHoa(Budget budget, string centerCode, int year, string templateId)
        {
            Get(templateId);
            return GetNodeDetailElement<T_MD_TEMPLATE_DETAIL_KE_HOACH_CHI_PHI, TemplateDetailKeHoachChiPhiRepo, T_MD_KHOAN_MUC_HANG_HOA, T_MD_CHI_PHI_PROFIT_CENTER>(centerCode, year, templateId);
        }
        internal IList<string> GetNodeDetailKhoanMucSuaChuaThuongXuyen(Budget budget, string centerCode, int year, string templateId)
        {
            Get(templateId);
            return GetNodeDetailElement<T_MD_TEMPLATE_DETAIL_SUA_CHUA_THUONG_XUYEN, TemplateDetailSuaChuaThuongXuyenRepo, T_MD_KHOAN_MUC_SUA_CHUA, T_MD_SUA_CHUA_THUONG_XUYEN_PROFIT_CENTER>(centerCode, year, templateId);
        }
        internal IList<string> GetNodeDetailKhoanMucChiPhi(Budget budget, string centerCode, int year, string templateId)
        {
            Get(templateId);
            return GetNodeDetailElement<T_MD_TEMPLATE_DETAIL_KE_HOACH_CHI_PHI, TemplateDetailKeHoachChiPhiRepo, T_MD_KHOAN_MUC_HANG_HOA, T_MD_CHI_PHI_PROFIT_CENTER>(centerCode, year, templateId);
        }
        internal IList<string> GetNodeDetailKhoanMucDauTuXayDung(Budget budget, string centerCode, int year, string templateId)
        {
            Get(templateId);
            return GetNodeDetailElement<T_MD_TEMPLATE_DETAIL_DAU_TU_XAY_DUNG, TemplateDetailDauTuXayDungRepo, T_MD_KHOAN_MUC_DAU_TU, T_MD_DAU_TU_XAY_DUNG_PROFIT_CENTER>(centerCode, year, templateId);
        }

        internal IList<string> GetNodeDetailKhoanMucVanChuyen(Budget budget, string centerCode, int year, string templateId)
        {
            Get(templateId);
            return GetNodeDetailElement<T_MD_TEMPLATE_DETAIL_KE_HOACH_VAN_CHUYEN, TemplateDetailKeHoachVanChuyenRepo, T_MD_KHOAN_MUC_VAN_CHUYEN, T_MD_VAN_CHUYEN_PROFIT_CENTER>(centerCode, year, templateId);
        }
        internal IList<string> GetNodeDetailKhoanMucDoanhThu(Budget budget, string centerCode, int year, string templateId)
        {
            Get(templateId);
            return GetNodeDetailElement<T_MD_TEMPLATE_DETAIL_KE_HOACH_DOANH_THU, TemplateDetailKeHoachDoanhThuRepo, T_MD_KHOAN_MUC_DOANH_THU, T_MD_DOANH_THU_PROFIT_CENTER>(centerCode, year, templateId);
        }

        internal string GetBPTypeName(string objectType, string budgetType, string elementType)
        {
            return GetBPType(objectType, budgetType, elementType)?.NAME;
        }

        private T_BP_TYPE GetBPType(string objectType, string budgetType, string elementType)
        {
            return UnitOfWork.Repository<TypeRepo>().GetFirstWithFetch(x => x.OBJECT_TYPE == objectType && x.BUDGET_TYPE == budgetType && x.ELEMENT_TYPE == elementType);
        }

        private string NonUnicode(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
    "đ",
    "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
    "í","ì","ỉ","ĩ","ị",
    "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
    "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
    "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
    "d",
    "e","e","e","e","e","e","e","e","e","e","e",
    "i","i","i","i","i",
    "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
    "u","u","u","u","u","u","u","u","u","u","u",
    "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }

        internal string GetBPTypeAcronymName(string objectType, string budgetType, string elementType)
        {
            var bpType = GetBPType(objectType, budgetType, elementType);
            return bpType?.ACRONYM_NAME;
        }

        private T_MD_OTHER_PROFIT_CENTER GetOtherProfitCenter(string companyCode, string projectCode)
        {
            return UnitOfWork.Repository<OtherProfitCenterRepo>()
                .GetFirstWithFetch(x => x.PROJECT_CODE == projectCode && x.COMPANY_CODE == companyCode);
        }

        private T_MD_SAN_LUONG_PROFIT_CENTER GetSanLuongProfitCenter(string companyCode, string projectCode)
        {
            return UnitOfWork.Repository<SanLuongProfitCenterRepo>()
                .GetFirstWithFetch(x => x.HANG_HANG_KHONG_CODE == companyCode && x.SAN_BAY_CODE == projectCode);
        }

        private T_MD_SUA_CHUA_PROFIT_CENTER GetSuaChuaProfitCenter(string companyCode, string projectCode)
        {
            return UnitOfWork.Repository<SuaChuaProfitCenterRepo>()
                .GetFirstWithFetch(x => x.SAN_BAY_CODE == companyCode && x.COST_CENTER_CODE == projectCode);
        }

        private T_MD_CHI_PHI_PROFIT_CENTER GetChiPhiProfitCenter(string companyCode, string projectCode)
        {
            return UnitOfWork.Repository<ChiPhiProfitCenterRepo>()
                .GetFirstWithFetch(x => x.SAN_BAY_CODE == companyCode && x.COST_CENTER_CODE == projectCode);
        }
        private T_MD_SUA_CHUA_THUONG_XUYEN_PROFIT_CENTER GetSuaChuaThuongXuyenProfitCenter(string companyCode, string projectCode)
        {
            return UnitOfWork.Repository<SuaChuaThuongXuyenProfitCenterRepo>()
                .GetFirstWithFetch(x => x.SAN_BAY_CODE == companyCode && x.COST_CENTER_CODE == projectCode);
        }
        
        private T_MD_DAU_TU_XAY_DUNG_PROFIT_CENTER GetDauTuXayDungProfitCenter(string companyCode, string projectCode)
        {
            return UnitOfWork.Repository<DauTuXayDungProfitCenterRepo>()
                .GetFirstWithFetch(x => x.ORG_CODE == companyCode && x.PROJECT_CODE == projectCode);
        }

        private T_MD_DOANH_THU_PROFIT_CENTER GetDoanhThuProfitCenter(string companyCode, string projectCode)
        {
            return UnitOfWork.Repository<DoanhThuProfitCenterRepo>()
                .GetFirstWithFetch(x => x.HANG_HANG_KHONG_CODE == companyCode && x.SAN_BAY_CODE == projectCode);
        }

        internal void UpdateDetailInformationOther(string projectCode, string companyCode, string template, int year, IList<string> detailCodes, Budget budget)
        {
            Get(template);
            //var otherProfitCenter = GetSanLuongProfitCenter(companyCode, projectCode);
            //if (otherProfitCenter != null)
            //{
            //    UpdateDetailInformation(otherProfitCenter.CODE, template, year, detailCodes, budget);
            //}
            //else
            //{
            switch (budget)
            {
                case Budget.SAN_LUONG:
                    try
                    {
                        UnitOfWork.BeginTransaction();
                        var profitCenter = UnitOfWork.Repository<SanLuongProfitCenterRepo>().Queryable().FirstOrDefault(x => x.HANG_HANG_KHONG_CODE == companyCode && x.SAN_BAY_CODE == projectCode);
                        var centerCode = profitCenter == null ? Guid.NewGuid().ToString() : profitCenter.CODE;
                        if (profitCenter == null)
                        {
                            UnitOfWork.Repository<SanLuongProfitCenterRepo>().Create(new T_MD_SAN_LUONG_PROFIT_CENTER
                            {
                                CODE = centerCode,
                                HANG_HANG_KHONG_CODE = companyCode,
                                SAN_BAY_CODE = projectCode,
                            });
                        }
                        else
                        {
                            UnitOfWork.Repository<TemplateDetailKeHoachSanLuongRepo>().Queryable().Where(x => x.CENTER_CODE == centerCode && x.TEMPLATE_CODE == template && x.TIME_YEAR == year).Delete();
                        }
                        
                        switch (ObjDetail.BUDGET_TYPE.Trim())
                        {
                            case BudgetType.SanLuong:
                                var detailsCostPL = from d in detailCodes
                                                    select new T_MD_TEMPLATE_DETAIL_KE_HOACH_SAN_LUONG
                                                    (Guid.NewGuid().ToString(), template, d, centerCode, year);

                                UnitOfWork.Repository<TemplateDetailKeHoachSanLuongRepo>().Create(detailsCostPL.ToList());
                                break;

                            default:
                                break;
                        }

                        UnitOfWork.Commit();
                    }
                    catch (Exception e)
                    {
                        UnitOfWork.Rollback();
                        Exception = e;
                        State = false;
                        ErrorMessage = e.Message;
                    }
                    break;

                case Budget.SUA_CHUA:
                    try
                    {
                        UnitOfWork.BeginTransaction();
                        var profitCenter = UnitOfWork.Repository<SuaChuaProfitCenterRepo>().Queryable().FirstOrDefault(x => x.SAN_BAY_CODE == companyCode && x.COST_CENTER_CODE == projectCode);
                        var centerCode = profitCenter == null ? Guid.NewGuid().ToString() : profitCenter.CODE;
                        if (profitCenter == null)
                        {
                            switch (ObjDetail.BUDGET_TYPE.Trim())
                            {
                                case BudgetType.SuaChuaLon:
                                    UnitOfWork.Repository<SuaChuaProfitCenterRepo>().Create(new T_MD_SUA_CHUA_PROFIT_CENTER
                                    {
                                        CODE = centerCode,
                                        SAN_BAY_CODE = companyCode,
                                        COST_CENTER_CODE = projectCode,
                                    });
                                    break;
                                case BudgetType.SuaChuaThuongXuyen:
                                    UnitOfWork.Repository<SuaChuaThuongXuyenProfitCenterRepo>().Create(new T_MD_SUA_CHUA_THUONG_XUYEN_PROFIT_CENTER
                                    {
                                        CODE = centerCode,
                                        SAN_BAY_CODE = companyCode,
                                        COST_CENTER_CODE = projectCode,
                                    });
                                    break;

                                default:
                                    break;
                            }
                        }
                        else
                        {
                            switch (ObjDetail.BUDGET_TYPE.Trim())
                            {
                                case BudgetType.SuaChuaLon:
                                    UnitOfWork.Repository<TemplateDetailSuaChuaLonRepo>().Queryable().Where(x => x.CENTER_CODE == centerCode && x.TEMPLATE_CODE == template && x.TIME_YEAR == year).Delete();
                                    break;
                                case BudgetType.SuaChuaThuongXuyen:
                                    UnitOfWork.Repository<TemplateDetailSuaChuaThuongXuyenRepo>().Queryable().Where(x => x.CENTER_CODE == centerCode && x.TEMPLATE_CODE == template && x.TIME_YEAR == year).Delete();
                                    break;

                                default:
                                    break;
                            }
                        }
                        
                        switch (ObjDetail.BUDGET_TYPE.Trim())
                        {
                            case BudgetType.SuaChuaLon:
                                var detailsCostPL = from d in detailCodes
                                                    select new T_MD_TEMPLATE_DETAIL_SUA_CHUA_LON
                                                    (Guid.NewGuid().ToString(), template, d, centerCode, year);

                                UnitOfWork.Repository<TemplateDetailSuaChuaLonRepo>().Create(detailsCostPL.ToList());
                                break;
                            case BudgetType.SuaChuaThuongXuyen:
                                var detailsCost = from d in detailCodes
                                                  select new T_MD_TEMPLATE_DETAIL_SUA_CHUA_THUONG_XUYEN
                                                  (Guid.NewGuid().ToString(), template, d, centerCode, year);

                                UnitOfWork.Repository<TemplateDetailSuaChuaThuongXuyenRepo>().Create(detailsCost.ToList());
                                break;

                            default:
                                break;
                        }

                        UnitOfWork.Commit();
                    }
                    catch (Exception e)
                    {
                        UnitOfWork.Rollback();
                        Exception = e;
                        State = false;
                        ErrorMessage = e.Message;
                    }
                    break;
                case Budget.DOANH_THU:
                    try
                    {
                        UnitOfWork.BeginTransaction();
                        var profitCenter = UnitOfWork.Repository<DoanhThuProfitCenterRepo>().Queryable().FirstOrDefault(x => x.HANG_HANG_KHONG_CODE == companyCode && x.SAN_BAY_CODE == projectCode);
                        var centerCode = profitCenter == null ? Guid.NewGuid().ToString() : profitCenter.CODE;
                        if (profitCenter == null)
                        {
                            UnitOfWork.Repository<DoanhThuProfitCenterRepo>().Create(new T_MD_DOANH_THU_PROFIT_CENTER
                            {
                                CODE = centerCode,
                                HANG_HANG_KHONG_CODE = companyCode,
                                SAN_BAY_CODE = projectCode,
                            });
                        }
                        else
                        {
                            UnitOfWork.Repository<TemplateDetailKeHoachDoanhThuRepo>().Queryable().Where(x => x.CENTER_CODE == centerCode && x.TEMPLATE_CODE == template && x.TIME_YEAR == year).Delete();
                        }
                        
                        switch (ObjDetail.BUDGET_TYPE.Trim())
                        {
                            case BudgetType.DoanhThu:
                                var detailsCostPL = from d in detailCodes
                                                    select new T_MD_TEMPLATE_DETAIL_KE_HOACH_DOANH_THU
                                                    (Guid.NewGuid().ToString(), template, d, centerCode, year);

                                UnitOfWork.Repository<TemplateDetailKeHoachDoanhThuRepo>().Create(detailsCostPL.ToList());
                                break;

                            default:
                                break;
                        }

                        UnitOfWork.Commit();
                    }
                    catch (Exception e)
                    {
                        UnitOfWork.Rollback();
                        Exception = e;
                        State = false;
                        ErrorMessage = e.Message;
                    }
                    break;

                case Budget.CHI_PHI:
                    try
                    {
                        UnitOfWork.BeginTransaction();
                        var profitCenter = UnitOfWork.Repository<ChiPhiProfitCenterRepo>().Queryable().FirstOrDefault(x => x.SAN_BAY_CODE == companyCode && x.COST_CENTER_CODE == projectCode);
                        var centerCode = profitCenter == null ? Guid.NewGuid().ToString() : profitCenter.CODE;

                        if (profitCenter == null)
                        {
                            UnitOfWork.Repository<ChiPhiProfitCenterRepo>().Create(new T_MD_CHI_PHI_PROFIT_CENTER
                            {
                                CODE = centerCode,
                                SAN_BAY_CODE = companyCode,
                                COST_CENTER_CODE = projectCode,
                            });
                        }
                        else
                        {
                            UnitOfWork.Repository<TemplateDetailKeHoachChiPhiRepo>().Queryable().Where(x => x.CENTER_CODE == centerCode && x.TEMPLATE_CODE == template && x.TIME_YEAR == year).Delete();
                        }
                        
                        switch (ObjDetail.BUDGET_TYPE.Trim())
                        {
                            case BudgetType.ChiPhi:
                                var detailsCostPL = from d in detailCodes
                                                    select new T_MD_TEMPLATE_DETAIL_KE_HOACH_CHI_PHI
                                                    (Guid.NewGuid().ToString(), template, d, centerCode, year);

                                UnitOfWork.Repository<TemplateDetailKeHoachChiPhiRepo>().Create(detailsCostPL.ToList());
                                break;

                            default:
                                break;
                        }

                        UnitOfWork.Commit();
                    }
                    catch (Exception e)
                    {
                        UnitOfWork.Rollback();
                        Exception = e;
                        State = false;
                        ErrorMessage = e.Message;
                    }
                    break;

                case Budget.DAU_TU_XAY_DUNG:
                    try
                    {
                        UnitOfWork.BeginTransaction();
                        var profitCenter = UnitOfWork.Repository<DauTuXayDungProfitCenterRepo>().Queryable().FirstOrDefault(x => x.ORG_CODE == companyCode && x.PROJECT_CODE == projectCode);
                        var centerCode = profitCenter == null ? Guid.NewGuid().ToString() : profitCenter.CODE;
                        if (profitCenter == null)
                        {
                            UnitOfWork.Repository<DauTuXayDungProfitCenterRepo>().Create(new T_MD_DAU_TU_XAY_DUNG_PROFIT_CENTER
                            {
                                CODE = centerCode,
                                ORG_CODE = companyCode,
                                PROJECT_CODE = projectCode,
                            });
                        }
                        else
                        {
                            UnitOfWork.Repository<TemplateDetailDauTuXayDungRepo>().Queryable().Where(x => x.CENTER_CODE == centerCode && x.TEMPLATE_CODE == template && x.TIME_YEAR == year).Delete();
                        }
                        
                        switch (ObjDetail.BUDGET_TYPE.Trim())
                        {
                            case BudgetType.DauTuXayDung:
                                var detailsCostPL = from d in detailCodes
                                                    select new T_MD_TEMPLATE_DETAIL_DAU_TU_XAY_DUNG
                                                    (Guid.NewGuid().ToString(), template, d, centerCode, year);

                                UnitOfWork.Repository<TemplateDetailDauTuXayDungRepo>().Create(detailsCostPL.ToList());
                                break;

                            default:
                                break;
                        }

                        UnitOfWork.Commit();
                    }
                    catch (Exception e)
                    {
                        UnitOfWork.Rollback();
                        Exception = e;
                        State = false;
                        ErrorMessage = e.Message;
                    }
                    break;
                case Budget.DAU_TU_TRANG_THIET_BI:
                    try
                    {
                        UnitOfWork.BeginTransaction();
                        var profitCenter = UnitOfWork.Repository<DauTuTrangThietBiProfitCenterRepo>().Queryable().FirstOrDefault(x => x.ORG_CODE == companyCode && x.PROJECT_CODE == projectCode);
                        var centerCode = profitCenter == null ? Guid.NewGuid().ToString() : profitCenter.CODE;
                        if (profitCenter == null)
                        {
                            UnitOfWork.Repository<DauTuTrangThietBiProfitCenterRepo>().Create(new T_MD_DAU_TU_TRANG_THIET_BI_PROFIT_CENTER
                            {
                                CODE = centerCode,
                                ORG_CODE = companyCode,
                                PROJECT_CODE = projectCode,
                            });
                        }
                        else
                        {
                            UnitOfWork.Repository<TemplateDetailDauTuTrangThietBiRepo>().Queryable().Where(x => x.CENTER_CODE == centerCode && x.TEMPLATE_CODE == template && x.TIME_YEAR == year).Delete();
                        }
                        
                        switch (ObjDetail.BUDGET_TYPE.Trim())
                        {
                            case BudgetType.DauTuTrangThietBi:
                                var detailsCostPL = from d in detailCodes
                                                    select new T_MD_TEMPLATE_DETAIL_DAU_TU_TRANG_THIET_BI
                                                    (Guid.NewGuid().ToString(), template, d, centerCode, year);

                                UnitOfWork.Repository<TemplateDetailDauTuTrangThietBiRepo>().Create(detailsCostPL.ToList());
                                break;

                            default:
                                break;
                        }

                        UnitOfWork.Commit();
                    }
                    catch (Exception e)
                    {
                        UnitOfWork.Rollback();
                        Exception = e;
                        State = false;
                        ErrorMessage = e.Message;
                    }
                    break;
                case Budget.DAU_TU_NGOAI_DOANH_NGHIEP:
                    try
                    {
                        UnitOfWork.BeginTransaction();
                        var profitCenter = UnitOfWork.Repository<DauTuNgoaiDoanhNghiepProfitCenterRepo>().Queryable().FirstOrDefault(x => x.ORG_CODE == companyCode && x.PROJECT_CODE == projectCode);
                        var centerCode = profitCenter == null ? Guid.NewGuid().ToString() : profitCenter.CODE;
                        if (profitCenter == null)
                        {
                            UnitOfWork.Repository<DauTuNgoaiDoanhNghiepProfitCenterRepo>().Create(new T_MD_DAU_TU_NGOAI_DOANH_NGHIEP_PROFIT_CENTER
                            {
                                CODE = centerCode,
                                ORG_CODE = companyCode,
                                PROJECT_CODE = projectCode,
                            });
                        }
                        else
                        {
                            UnitOfWork.Repository<TemplateDetailDauTuNgoaiDoanhNghiepRepo>().Queryable().Where(x => x.CENTER_CODE == centerCode && x.TEMPLATE_CODE == template && x.TIME_YEAR == year).Delete();
                        }
                        
                        switch (ObjDetail.BUDGET_TYPE.Trim())
                        {
                            case BudgetType.DauTuNgoaiDoanhNghiep:
                                var detailsCostPL = from d in detailCodes
                                                    select new T_MD_TEMPLATE_DETAIL_DAU_TU_NGOAI_DOANH_NGHIEP
                                                    (Guid.NewGuid().ToString(), template, d, centerCode, year);

                                UnitOfWork.Repository<TemplateDetailDauTuNgoaiDoanhNghiepRepo>().Create(detailsCostPL.ToList());
                                break;

                            default:
                                break;
                        }

                        UnitOfWork.Commit();
                    }
                    catch (Exception e)
                    {
                        UnitOfWork.Rollback();
                        Exception = e;
                        State = false;
                        ErrorMessage = e.Message;
                    }
                    break;

                case Budget.VAN_CHUYEN:
                    try
                    {
                        UnitOfWork.BeginTransaction();
                        var profitCenter = UnitOfWork.Repository<VanChuyenProfitCenterRepo>().Queryable().FirstOrDefault(x => x.ORG_CODE == companyCode && x.ROUTE_CODE == projectCode);
                        var centerCode = profitCenter == null ? Guid.NewGuid().ToString() : profitCenter.CODE;
                        if (profitCenter == null)
                        {
                            UnitOfWork.Repository<VanChuyenProfitCenterRepo>().Create(new T_MD_VAN_CHUYEN_PROFIT_CENTER
                            {
                                CODE = centerCode,
                                ORG_CODE = companyCode,
                                ROUTE_CODE = projectCode,
                            });
                        }
                        else
                        {
                            UnitOfWork.Repository<TemplateDetailKeHoachVanChuyenRepo>().Queryable().Where(x => x.CENTER_CODE == centerCode && x.TEMPLATE_CODE == template && x.TIME_YEAR == year).Delete();
                        }
                        
                        switch (ObjDetail.BUDGET_TYPE.Trim())
                        {
                            case BudgetType.VanChuyen:
                                var detailsCostPL = from d in detailCodes
                                                    select new T_MD_TEMPLATE_DETAIL_KE_HOACH_VAN_CHUYEN
                                                    (Guid.NewGuid().ToString(), template, d, centerCode, year);

                                UnitOfWork.Repository<TemplateDetailKeHoachVanChuyenRepo>().Create(detailsCostPL.ToList());
                                break;

                            default:
                                break;
                        }

                        UnitOfWork.Commit();
                    }
                    catch (Exception e)
                    {
                        UnitOfWork.Rollback();
                        Exception = e;
                        State = false;
                        ErrorMessage = e.Message;
                    }
                    break;

                default:
                    Exception = new FormatException("Type of center not support");
                    State = false;
                    ErrorMessage = "Type of center not support";
                    break;
            }

        }

        private IList<string> GetNodeDetailElement<TTemplateDetail, TTemplateDetailRepo, TElement, TCenter>(string centerCode, int year, string templateId)
            where TTemplateDetailRepo : GenericTemplateDetailRepository<TTemplateDetail, TElement, TCenter>
            where TTemplateDetail : BaseTemplateDetail<TElement, TCenter>
            where TElement : CoreElement
            where TCenter : CoreCenter
        {
            return UnitOfWork.Repository<TTemplateDetailRepo>()
                .GetManyByExpression(x => x.CENTER_CODE == centerCode
                && x.TEMPLATE_CODE == templateId
                && x.TIME_YEAR == year)
                .Select(x => x.ELEMENT_CODE).ToList();
        }


        #endregion

        internal void ToggleStatusTemplate(string templateId, bool currentStatus)
        {
            if (!currentStatus)
            {
                // active template
                Update(x => x.CODE == templateId, x => x.ACTIVE = !currentStatus);
            }
            else
            {
                // deactive template
                // check mẫu đã được nộp dữ liệu ở trạng thái khác Chưa trình duyệt không
                Get(templateId);
                if (ObjDetail == null)
                {
                    State = false;
                    ErrorMessage = "Mẫu không khả dụng";
                    return;
                }
                else
                {
                    var canDeactiveTemplate = false;
                    switch (ObjDetail.OBJECT_TYPE.Trim())
                    {
                        case TemplateObjectType.SanLuong:
                            canDeactiveTemplate = CheckStatusData<KeHoachSanLuongRepo, T_BP_KE_HOACH_SAN_LUONG>(templateId);
                            break;
                        case TemplateObjectType.DoanhThu:
                            canDeactiveTemplate = CheckStatusData<KeHoachSanLuongRepo, T_BP_KE_HOACH_SAN_LUONG>(templateId);
                            break;
                        case TemplateObjectType.ChiPhi:
                            canDeactiveTemplate = CheckStatusData<KeHoachChiPhiRepo, T_BP_KE_HOACH_CHI_PHI>(templateId);
                            break;
                        case TemplateObjectType.DauTuXayDung:
                            canDeactiveTemplate = CheckStatusData<DauTuXayDungRepo, T_BP_DAU_TU_XAY_DUNG>(templateId);
                            break;
                        case TemplateObjectType.DauTuTrangThietBi:
                            canDeactiveTemplate = CheckStatusData<DauTuTrangThietBiRepo, T_BP_DAU_TU_TRANG_THIET_BI>(templateId);
                            break;
                        case TemplateObjectType.DauTuNgoaiDoanhNghiep:
                            canDeactiveTemplate = CheckStatusData<DauTuNgoaiDoanhNghiepRepo, T_BP_DAU_TU_NGOAI_DOANH_NGHIEP>(templateId);
                            break;
                        case TemplateObjectType.VanChuyen:
                            canDeactiveTemplate = CheckStatusData<KeHoachVanChuyenRepo, T_BP_KE_HOACH_VAN_CHUYEN>(templateId);
                            break;
                        case TemplateObjectType.SuaChuaLon:
                            canDeactiveTemplate = CheckStatusData<SuaChuaLonRepo, T_BP_SUA_CHUA_LON>(templateId);
                            break;
                        case TemplateObjectType.SuaChuaThuongXuyen:
                            canDeactiveTemplate = CheckStatusData<SuaChuaThuongXuyenRepo, T_BP_SUA_CHUA_THUONG_XUYEN>(templateId);
                            break;


                        default:
                            State = false;
                            ErrorMessage = "Mẫu không khả dụng";
                            return;
                    }
                    if (canDeactiveTemplate)
                    {
                        Update(x => x.CODE == templateId, x => x.ACTIVE = !currentStatus);
                    }
                    else
                    {
                        State = false;
                        ErrorMessage = "Mẫu không thể Deactive do đã có dữ liệu ở trạng thái khác với Chưa trình duyệt";
                        return;
                    }
                }
            }

        }

        internal void CopyTemplate(int sourceYear, int destinationYear, string templateId)
        {
            Get(templateId);
            if (ObjDetail == null)
            {
                State = false;
                ErrorMessage = "Mẫu không khả dụng.";
                return;
            }
            else
            {
                switch (ObjDetail.OBJECT_TYPE.Trim())
                {
                    case TemplateObjectType.SanLuong:
                        CopyTemplate<TemplateDetailKeHoachSanLuongRepo, T_MD_TEMPLATE_DETAIL_KE_HOACH_SAN_LUONG, KhoanMucSanLuongRepo, T_MD_KHOAN_MUC_SAN_LUONG, SanLuongProfitCenterRepo, T_MD_SAN_LUONG_PROFIT_CENTER>(sourceYear, destinationYear, templateId);
                        break;
                    case TemplateObjectType.DoanhThu:
                        CopyTemplate<TemplateDetailKeHoachDoanhThuRepo, T_MD_TEMPLATE_DETAIL_KE_HOACH_DOANH_THU, KhoanMucDoanhThuRepo, T_MD_KHOAN_MUC_DOANH_THU, DoanhThuProfitCenterRepo, T_MD_DOANH_THU_PROFIT_CENTER>(sourceYear, destinationYear, templateId);
                        break;
                    case TemplateObjectType.ChiPhi:
                        CopyTemplate<TemplateDetailKeHoachChiPhiRepo, T_MD_TEMPLATE_DETAIL_KE_HOACH_CHI_PHI, KhoanMucHangHoaRepo, T_MD_KHOAN_MUC_HANG_HOA, ChiPhiProfitCenterRepo, T_MD_CHI_PHI_PROFIT_CENTER>(sourceYear, destinationYear, templateId);
                        break;
                    case TemplateObjectType.DauTuXayDung:
                        CopyTemplate<TemplateDetailDauTuXayDungRepo, T_MD_TEMPLATE_DETAIL_DAU_TU_XAY_DUNG, KhoanMucDauTuRepo, T_MD_KHOAN_MUC_DAU_TU, DauTuXayDungProfitCenterRepo, T_MD_DAU_TU_XAY_DUNG_PROFIT_CENTER>(sourceYear, destinationYear, templateId);
                        break;
                    case TemplateObjectType.DauTuTrangThietBi:
                        CopyTemplate<TemplateDetailDauTuTrangThietBiRepo, T_MD_TEMPLATE_DETAIL_DAU_TU_TRANG_THIET_BI, KhoanMucDauTuRepo, T_MD_KHOAN_MUC_DAU_TU, DauTuTrangThietBiProfitCenterRepo, T_MD_DAU_TU_TRANG_THIET_BI_PROFIT_CENTER>(sourceYear, destinationYear, templateId);
                        break;
                    case TemplateObjectType.DauTuNgoaiDoanhNghiep:
                        CopyTemplate<TemplateDetailDauTuNgoaiDoanhNghiepRepo, T_MD_TEMPLATE_DETAIL_DAU_TU_NGOAI_DOANH_NGHIEP, KhoanMucDauTuRepo, T_MD_KHOAN_MUC_DAU_TU, DauTuNgoaiDoanhNghiepProfitCenterRepo, T_MD_DAU_TU_NGOAI_DOANH_NGHIEP_PROFIT_CENTER>(sourceYear, destinationYear, templateId);
                        break;
                    case TemplateObjectType.VanChuyen:
                        CopyTemplate<TemplateDetailKeHoachVanChuyenRepo, T_MD_TEMPLATE_DETAIL_KE_HOACH_VAN_CHUYEN, KhoanMucVanChuyenRepo, T_MD_KHOAN_MUC_VAN_CHUYEN, VanChuyenProfitCenterRepo, T_MD_VAN_CHUYEN_PROFIT_CENTER>(sourceYear, destinationYear, templateId);
                        break;
                    //case TemplateObjectType.SuaChuaLon:
                    //    CopyTemplate<TemplateDetailSuaChuaLonRepo, T_MD_TEMPLATE_DETAIL_SUA_CHUA_LON, SuaChuaLonRepo, T_MD_KHOAN_MUC_SUA_CHUA, SuaChuaProfitCenterRepo, T_MD_SUA_CHUA_PROFIT_CENTER>(sourceYear, destinationYear, templateId);
                    //    break;
                    default:
                        State = false;
                        ErrorMessage = "Mẫu không khả dụng";
                        return;
                }
            }
        }

        private bool CheckStatusData<TRepo, TEntity>(string templateId)
            where TRepo : GenericRepository<TEntity>
            where TEntity : T_BP_BASE
        {
            var lstData = UnitOfWork.Repository<TRepo>().GetManyWithFetch(x => x.TEMPLATE_CODE == templateId);
            return lstData.Count == 0 || !lstData.Any(x => x.STATUS != Approve_Status.ChuaTrinhDuyet && x.STATUS != Approve_Status.TuChoi);
        }

        private void CopyTemplate<TTemplateDetailRepo, TTemplateDetail, TElementRepo, TElement, TCenterRepo, TCenter>(int sourceYear, int destinationYear, string templateId)
            where TTemplateDetailRepo : GenericTemplateDetailRepository<TTemplateDetail, TElement, TCenter>
            where TTemplateDetail : BaseTemplateDetail<TElement, TCenter>
            where TElement : CoreElement
            where TElementRepo : GenericElementRepository<TElement>
            where TCenter : CoreCenter
            where TCenterRepo : GenericCenterRepository<TCenter>

        {
            var lstSource = UnitOfWork.Repository<TTemplateDetailRepo>()
                .GetManyWithFetch(x => x.TEMPLATE_CODE == templateId && x.TIME_YEAR == sourceYear);
            if (lstSource == null || lstSource.Count == 0)
            {
                State = false;
                ErrorMessage = "Mẫu nguồn chưa được tạo.";
                return;
            }
            else
            {
                var lstDestination = UnitOfWork.Repository<TTemplateDetailRepo>().GetManyWithFetch(x => x.TEMPLATE_CODE == templateId && x.TIME_YEAR == destinationYear);
                if (lstDestination != null && lstDestination.Count > 0 && lstSource.Count > 0)
                {
                    State = false;
                    ErrorMessage = "Mẫu đích đã được tạo từ trước.";
                    return;
                }
                else
                {
                    var lstSourceElements = lstSource.Select(x => x.ELEMENT_CODE).Distinct();
                    var elementsInDestinationYear = UnitOfWork.Repository<TElementRepo>()
                        .GetManyWithFetch(x => x.TIME_YEAR == destinationYear && lstSourceElements.Contains(x.CODE))
                        .Select(x => x.CODE);
                    if (elementsInDestinationYear == null ||
                        lstSourceElements.Count() > elementsInDestinationYear.Count())
                    {
                        State = false;
                        ErrorMessage = $"Năm {destinationYear} chưa được khai báo những khoản mục sau: " +
                            $"{string.Join(", ", lstSourceElements.Where(x => !elementsInDestinationYear.ToList().Contains(x)).Distinct())}";
                        return;
                    }
                    else
                    {
                        UnitOfWork.OpenStatelessSession();
                        using (IStatelessSession session = UnitOfWork.GetStatelessSession())
                        using (ITransaction transaction = session.Transaction)
                        {
                            try
                            {
                                var currentUser = ProfileUtilities.User?.USER_NAME;
                                lstSource.ForEach(x => x.TIME_YEAR = destinationYear);
                                lstSource.ForEach(x => x.CREATE_BY = currentUser);
                                lstSource.ForEach(x => x.UPDATE_BY = null);
                                lstSource.ForEach(x => x.UPDATE_DATE = null);
                                lstSource.ForEach(x => x.PKID = Guid.NewGuid().ToString());

                                session.SetBatchSize(1000);
                                transaction.Begin();
                                foreach (var obj in lstSource)
                                {
                                    session.Insert(obj);
                                }
                                transaction.Commit();


                                // separate lstSource to each 1k rows
                                //for (int i = 0; i < Math.Ceiling(lstSource.Count / (double)1000); i++)
                                //{
                                //    repo.Create(lstSource.Skip(i * 1000).Take(1000));
                                //}
                            }
                            catch (Exception e)
                            {
                                transaction.Rollback();
                                State = false;
                                ErrorMessage = e.Message;
                                return;
                            }
                        }
                    }
                }
            }
        }
    }

}
