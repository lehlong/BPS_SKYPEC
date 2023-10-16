using NHibernate;

using SMO.Core.Common;
using SMO.Core.Entities;
using SMO.Core.Entities.BP;
using SMO.Core.Entities.BP.KE_HOACH_SAN_LUONG;
using SMO.Core.Entities.MD;
using SMO.Repository.Common;
using SMO.Repository.Implement.BP;
using SMO.Repository.Implement.BP.KE_HOACH_SAN_LUONG;
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
            if (lstDetail.Count() == 0)
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
                case TemplateObjectType.DoanhThu:

                    return UnitOfWork.Repository<TemplateDetailKeHoachSanLuongRepo>()
                        .GetManyWithFetch(x => x.TIME_YEAR == year && x.TEMPLATE_CODE == templateId, x => x.Center)
                        .Select(x => x.Center)
                        .Where(x => x.SAN_BAY_CODE == projectCode)
                        .Select(x => x.HANG_HANG_KHONG_CODE)
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

        internal IList<string> GetNodeDetailElement(Budget budget, string centerCode, int year, string templateId)
        {
            Get(templateId);
            switch (ObjDetail.OBJECT_TYPE.Trim())
            {
                case TemplateObjectType.SanLuong:
                    return GetNodeDetailElement<T_MD_TEMPLATE_DETAIL_KE_HOACH_SAN_LUONG, TemplateDetailKeHoachSanLuongRepo, T_MD_KHOAN_MUC_SAN_LUONG, T_MD_SAN_LUONG_PROFIT_CENTER>(centerCode, year, templateId);
                case TemplateObjectType.DoanhThu:
                    return GetNodeDetailElement<T_MD_TEMPLATE_DETAIL_KE_HOACH_SAN_LUONG, TemplateDetailKeHoachSanLuongRepo, T_MD_KHOAN_MUC_SAN_LUONG, T_MD_SAN_LUONG_PROFIT_CENTER>(centerCode, year, templateId);
                default:
                    return new List<string>();
            }
        }

        internal IList<string> GetNodeDetailKhoanMucSanLuong(Budget budget, string centerCode, int year, string templateId)
        {
            Get(templateId);
            return GetNodeDetailElement<T_MD_TEMPLATE_DETAIL_KE_HOACH_SAN_LUONG, TemplateDetailKeHoachSanLuongRepo, T_MD_KHOAN_MUC_SAN_LUONG, T_MD_SAN_LUONG_PROFIT_CENTER>(centerCode, year, templateId);
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

        internal void UpdateDetailInformationOther(string projectCode, string companyCode, string template, int year, IList<string> detailCodes, Budget budget)
        {
            Get(template);
            var otherProfitCenter = GetSanLuongProfitCenter(companyCode, projectCode);
            if (otherProfitCenter != null)
            {
                UpdateDetailInformation(otherProfitCenter.CODE, template, year, detailCodes, budget);
            }
            else
            {
                switch (budget)
                {
                    case Budget.SAN_LUONG:
                        try
                        {
                            UnitOfWork.BeginTransaction();
                            var centerCode = Guid.NewGuid().ToString();
                            // create other profit center
                            UnitOfWork.Repository<SanLuongProfitCenterRepo>().Create(new T_MD_SAN_LUONG_PROFIT_CENTER
                            {
                                CODE = centerCode,
                                HANG_HANG_KHONG_CODE = companyCode,
                                SAN_BAY_CODE = projectCode,
                            });

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
                    case Budget.DOANH_THU:
                        try
                        {
                            UnitOfWork.BeginTransaction();
                            var centerCode = Guid.NewGuid().ToString();
                            // create other profit center
                            UnitOfWork.Repository<SanLuongProfitCenterRepo>().Create(new T_MD_SAN_LUONG_PROFIT_CENTER
                            {
                                CODE = centerCode,
                                HANG_HANG_KHONG_CODE = companyCode,
                                SAN_BAY_CODE = projectCode,
                            });

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

                    default:
                        Exception = new FormatException("Type of center not support");
                        State = false;
                        ErrorMessage = "Type of center not support";
                        break;
                }
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
