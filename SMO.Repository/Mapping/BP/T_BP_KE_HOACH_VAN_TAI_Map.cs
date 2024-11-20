using NHibernate.Type;

using SMO.Core.Entities;
using SMO.Core.Entities.BP;

namespace SMO.Repository.Mapping.BP
{
    public class T_BP_KE_HOACH_VAN_TAI_Map : BaseMapping<T_BP_KE_HOACH_VAN_TAI>
    {
        public T_BP_KE_HOACH_VAN_TAI_Map()
        {
            Table("T_BP_KE_HOACH_VAN_TAI");
            Id(x => x.ID);
            Map(x => x.YEAR);
            Map(x => x.C_ORDER);
            Map(x => x.COL1);
            Map(x => x.COL2);
            Map(x => x.COL3);
            Map(x => x.COL4);
            Map(x => x.COL5);
            Map(x => x.COL6);
            Map(x => x.COL7);
            Map(x => x.COL8);
            Map(x => x.COL9);
            Map(x => x.COL10);
            Map(x => x.COL11);
            Map(x => x.COL12);
            Map(x => x.COL13);
            Map(x => x.COL14);
            Map(x => x.COL15);
            Map(x => x.COL16);
            Map(x => x.COL17);
            Map(x => x.COL18);
            Map(x => x.COL19);
            Map(x => x.COL20);
            Map(x => x.COL21);
            Map(x => x.COL22);
            Map(x => x.COL23);
            Map(x => x.COL24);
            Map(x => x.COL25);
            Map(x => x.COL26);
            Map(x => x.COL27);
            Map(x => x.COL28);
            Map(x => x.COL29);
            Map(x => x.COL30);
            Map(x => x.COL31);
            Map(x => x.COL32);
            Map(x => x.COL33);
            Map(x => x.COL34);
            Map(x => x.COL35);
            Map(x => x.COL36);
            Map(x => x.COL37);
            Map(x => x.COL38);
            Map(x => x.COL39);
            Map(x => x.COL40);
            Map(x => x.COL41);
            Map(x => x.COL42);
            Map(x => x.COL43);
            Map(x => x.COL44);
            Map(x => x.COL45);
            Map(x => x.COL46);
            Map(x => x.COL47);
            Map(x => x.COL48);
            Map(x => x.COL49);
            Map(x => x.COL50);
            Map(x => x.COL51);
            Map(x => x.COL52);
            Map(x => x.COL53);
            Map(x => x.COL54);
            Map(x => x.COL55);
            Map(x => x.COL56);
            Map(x => x.COL57);
            Map(x => x.COL58);
            Map(x => x.COL59);
            Map(x => x.COL60);
            Map(x => x.COL61);
            Map(x => x.COL62);
            Map(x => x.COL63);
            Map(x => x.COL64);
            Map(x => x.COL65);
            Map(x => x.COL66);
            Map(x => x.COL67);
            Map(x => x.COL68);
            Map(x => x.COL69);
            Map(x => x.COL70);
            Map(x => x.COL71);
            Map(x => x.COL72);
            Map(x => x.COL73);
            Map(x => x.COL74);
            Map(x => x.COL75);
            Map(x => x.COL76);
            Map(x => x.COL77);
            Map(x => x.COL78);
            Map(x => x.ACTIVE).Not.Nullable().CustomType<YesNoType>();
           
        }
    }
}
