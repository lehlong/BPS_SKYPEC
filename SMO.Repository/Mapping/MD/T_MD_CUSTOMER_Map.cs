using NHibernate.Type;

using SMO.Core.Entities;

namespace SMO.Repository.Mapping.MD
{
    public class T_MD_CUSTOMER_Map : BaseMapping<T_MD_CUSTOMER>
    {
        public T_MD_CUSTOMER_Map()
        {
            Table("T_MD_CUSTOMER");
            Id(x => x.CODE);
            Map(x => x.TEXT).Nullable();
            Map(x => x.OLD_CODE).Nullable();
            Map(x => x.ADDRESS).Nullable();
            Map(x => x.MST).Nullable();
            Map(x => x.PHONE).Nullable();
            Map(x => x.BANK).Nullable();
            Map(x => x.OTHER_INFO).Nullable();
            Map(x => x.ACTIVE).Not.Nullable().CustomType<YesNoType>();
        }
    }
}
