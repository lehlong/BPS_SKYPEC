using NHibernate.Type;

using SMO.Core.Entities.MD;

namespace SMO.Repository.Mapping.MD
{
    public class T_MD_TEMPLATE_Map : BaseMapping<T_MD_TEMPLATE>
    {
        public T_MD_TEMPLATE_Map()
        {
            Id(x => x.CODE);
            Map(x => x.ACTIVE).CustomType<YesNoType>();
            Map(x => x.BUDGET_TYPE)
                .Not.Update();
            Map(x => x.OBJECT_TYPE)
                .Not.Update();
            Map(x => x.ELEMENT_TYPE)
                .Not.Update();
            Map(x => x.NAME);
            Map(x => x.NOTES);
            Map(x => x.TITLE);
            Map(x => x.ORG_CODE)
                .Not.Update();
            Map(x => x.IS_BASE).CustomType<YesNoType>();

            References(x => x.Organize, "ORG_CODE")
                .Not.Insert()
                .Not.Update()
                .LazyLoad();

            HasMany(x => x.DetailKeHoachSanLuong)
                        .KeyColumn("TEMPLATE_CODE")
                        .LazyLoad().Inverse().Cascade.Delete();
            HasMany(x => x.DetailKeHoachDoanhThu)
                        .KeyColumn("TEMPLATE_CODE")
                        .LazyLoad().Inverse().Cascade.Delete();
            HasMany(x => x.DetailKeHoachChiPhi)
                        .KeyColumn("TEMPLATE_CODE")
                        .LazyLoad().Inverse().Cascade.Delete();
            HasMany(x => x.DetailDauTuXayDung)
                        .KeyColumn("TEMPLATE_CODE")
                        .LazyLoad().Inverse().Cascade.Delete();
            HasMany(x => x.DetailDauTuTrangThietBi)
                        .KeyColumn("TEMPLATE_CODE")
                        .LazyLoad().Inverse().Cascade.Delete();
            HasMany(x => x.DetailDauTuNgoaiDoanhNghiep)
                        .KeyColumn("TEMPLATE_CODE")
                        .LazyLoad().Inverse().Cascade.Delete();

        }
    }
}
