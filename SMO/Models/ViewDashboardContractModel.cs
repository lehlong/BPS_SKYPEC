using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMO.Models
{
    public class ViewDashboardContractModel
    {
        public string TotalContract { get; set; }
        public string SumValueTotalContract { get; set; }
        public string SumContractPayment { get; set; }
        public List<ViewDashboardBase> dataDashboard1 { get; set; }
        public List<ViewDashboardBase> dataDashboard2 { get; set; }
    }

    public class ViewDashboardBase
    {
        public string Text { get; set; }
        public int Value { get; set; }
    }
}