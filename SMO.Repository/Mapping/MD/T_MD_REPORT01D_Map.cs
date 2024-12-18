using NHibernate.Type;

using SMO.Core.Entities;
using SMO.Core.Entities.MD;

namespace SMO.Repository.Mapping.MD
{
    public class T_MD_REPORT01D_Map : BaseMapping<T_MD_REPORT01D>
    {
        public T_MD_REPORT01D_Map()
        {
        
            Id(x => x.ID);
            Map(x => x.DN9T);
            Map(x => x.NAME1D);
            Map(x => x.TT);
            Map(x => x.GTDN);
            Map(x => x.TH);
            Map(x => x.KH);
            Map(x => x.C_ORDER);
            Map(x => x.GTGGDT);
            Map(x=>x.TLGV);
            Map(x => x.CT);
            Map(x => x.TlLN);
            Map(x => x.YEAR);
            Map(x => x.ISBOLD).CustomType<YesNoType>(); ;
            Map(x => x.ACTIVE).Not.Nullable().CustomType<YesNoType>();
        }
    }
}
