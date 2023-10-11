using SMO.Core.Entities.BU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Repository.Mapping.BU
{
     class T_BU_FILE_UPLOAD_Map : BaseMapping<T_BU_FILE_UPLOAD>
    {
        public T_BU_FILE_UPLOAD_Map()
        {
            Table("T_BU_FILE_UPLOAD");
            Id(x => x.ID);
            Map(x => x.FILE_NAME);
            Map(x => x.FILE_OLD_NAME);
            Map(x => x.FILE_EXT);
            Map(x => x.FILE_SIZE);
            Map(x => x.CONNECTION_ID);
            Map(x => x.PARENT);
            Map(x => x.DIRECTORY_PATH);
            Map(x => x.FULL_PATH);
            Map(x => x.IS_DELETE);
            Map(x => x.VERSION);
        }
    }
}
