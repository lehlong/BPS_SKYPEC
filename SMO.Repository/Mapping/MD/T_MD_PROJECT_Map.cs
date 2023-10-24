using NHibernate.Type;

using SMO.Core.Entities;

namespace SMO.Repository.Mapping.MD
{
    public class T_MD_PROJECT_Map : BaseMapping<T_MD_PROJECT>
    {
        public T_MD_PROJECT_Map()
        {
            Table("T_MD_PROJECT");
            Id(x => x.CODE);
            Map(x => x.NAME).Nullable();
            Map(x => x.LOAI_HINH);
            Map(x => x.GIAI_DOAN);
            Map(x => x.YEAR);
            Map(x => x.NGANH_NGHE);
            Map(x => x.PHAN_LOAI);
            Map(x => x.ACTIVE).Not.Nullable().CustomType<YesNoType>();
            References(x => x.LoaiHinh).Column("LOAI_HINH").Not.Insert().Not.Update().LazyLoad();
            References(x => x.GiaiDoan).Column("GIAI_DOAN").Not.Insert().Not.Update().LazyLoad();
            References(x => x.NganhNghe).Column("NGANH_NGHE").Not.Insert().Not.Update().LazyLoad();
            References(x => x.PhanLoai).Column("PHAN_LOAI").Not.Insert().Not.Update().LazyLoad();
        }
    }
}
