using NHibernate.Type;

using SMO.Core.Entities.MD;

namespace SMO.Repository.Mapping.MD
{
    class T_MD_KHOAN_MUC_CHUNG_Map : BaseCoreCenterMapping<T_MD_KHOAN_MUC_CHUNG>
    {
        public T_MD_KHOAN_MUC_CHUNG_Map() : base()
        {
            Id(x => x.CODE);
            Map(x => x.UNIT_CODE);
            Map(x => x.IS_GROUP).Not.Nullable().CustomType<YesNoType>();
            References(x => x.Parent, "PARENT_CODE")
                .Not.Insert()
                .Not.Update()
                .LazyLoad();
        }
    }
}
