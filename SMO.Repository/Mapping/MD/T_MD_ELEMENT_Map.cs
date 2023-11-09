using NHibernate.Type;

using SMO.Core.Entities;

namespace SMO.Repository.Mapping.MD
{
    public class T_MD_ELEMENT_Map : BaseMapping<T_MD_ELEMENT>
    {
        public T_MD_ELEMENT_Map()
        {
            Table("T_MD_ELEMENT");
            Id(x => x.CODE);
            Map(x => x.NAME).Nullable();
            Map(x => x.ELEMENT_TYPE).Nullable();
            Map(x => x.PRIORITY).Nullable();
            Map(x => x.STATUS).Nullable();
            Map(x => x.FORMULA).Nullable();
            Map(x => x.UNIT_CODE).Nullable();
            Map(x => x.SCREEN).Nullable();
            Map(x => x.QUERY).Nullable();
            Map(x => x.VALUE).Nullable();
            Map(x => x.ACTIVE).Not.Nullable().CustomType<YesNoType>();
        }
    }
}
