﻿using SMO.Core.Common;
using SMO.Core.Entities.BP.SUA_CHUA_THUONG_XUYEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO.Core.Entities.MD
{
    public class T_MD_TEMPLATE_DETAIL_SUA_CHUA_THUONG_XUYEN : BaseTemplateDetail<T_MD_KHOAN_MUC_SUA_CHUA, T_MD_SUA_CHUA_THUONG_XUYEN_PROFIT_CENTER>
    {
        public T_MD_TEMPLATE_DETAIL_SUA_CHUA_THUONG_XUYEN()
        {
        }

        public T_MD_TEMPLATE_DETAIL_SUA_CHUA_THUONG_XUYEN(string pkid, string templateCode, string elementCode, string centerCode, int year) : base(pkid, templateCode, elementCode, centerCode, year)
        {
        }
        public virtual T_MD_TEMPLATE Template { get; set; }
        public virtual T_BP_SUA_CHUA_THUONG_XUYEN_DATA PLData { get; set; }

    }
}
