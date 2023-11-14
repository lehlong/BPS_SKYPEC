using SMO.Core.Entities;

namespace SMO.Repository.Mapping.BP
{
    public class T_BP_KE_HOACH_CHI_PHI_Map : BaseBPMapping<T_BP_KE_HOACH_CHI_PHI>
    {
        public T_BP_KE_HOACH_CHI_PHI_Map()
        {
            Map(x => x.TYPE_UPLOAD);
            Map(x => x.KICH_BAN);
            Map(x => x.PHIEN_BAN);
            References(x => x.KichBan).Column("KICH_BAN").Not.Insert().Not.Update().LazyLoad();
            References(x => x.PhienBan).Column("PHIEN_BAN").Not.Insert().Not.Update().LazyLoad();
        }
    }
}
