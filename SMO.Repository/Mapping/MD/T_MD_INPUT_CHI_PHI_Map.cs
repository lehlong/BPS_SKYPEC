using NHibernate.Type;

using SMO.Core.Entities.MD;

namespace SMO.Repository.Mapping.MD
{
    public class T_MD_INPUT_CHI_PHI_map : BaseMapping<T_MD_INPUT_CHI_PHI>
    {
        public T_MD_INPUT_CHI_PHI_map()
        {
            Id(x => x.ID);
            Map(x => x.ID_CENTER);
            Map(x => x.GROUP_1_ID);
            Map(x => x.GROUP_2_ID);
            Map(x => x.GROUP_NAME);
            Map(x => x.UOC_THUC_HIEN);
            Map(x => x.TH9T);
            Map(x => x.C_ORDER);
            Map(x => x.STT);
            Map(x => x.TIME_YEAR);
            Map(x => x.IS_BOLD).CustomType<YesNoType>();
            Map(x => x.ACTIVE).CustomType<YesNoType>();

        }
    }
}
