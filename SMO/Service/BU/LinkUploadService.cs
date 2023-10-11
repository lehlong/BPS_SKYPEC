using SMO.Core.Entities.BU;
using SMO.Repository.Implement.BU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMO.Service.BU
{
    public class LinkUploadService : GenericService<T_BU_LINK_UPLOAD, LinkUploadRepo>
    {
        public LinkUploadService() : base()
        {

        }
    }
}