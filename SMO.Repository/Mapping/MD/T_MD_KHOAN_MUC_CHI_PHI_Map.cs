using NHibernate.Type;

using SMO.Core.Entities;

namespace SMO.Repository.Mapping.MD
{
    public class T_MD_KHOAN_MUC_CHI_PHI_Map : BaseMapping<T_MD_KHOAN_MUC_CHI_PHI>
    {
        public T_MD_KHOAN_MUC_CHI_PHI_Map()
        {
            Table("T_MD_KHOAN_MUC_CHI_PHI");
            Id(x => x.CODE);
            Map(x => x.TEXT).Nullable();
            Map(x => x.ACTIVE).Not.Nullable().CustomType<YesNoType>();
        }
    }
}
