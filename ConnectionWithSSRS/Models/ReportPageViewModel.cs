using System.Collections.Generic;
using ConnectionWithSSRS.ReportService2010;

namespace ConnectionWithSSRS.Models
{
    public class ReportPageViewModel
    {
        public ApplicationUser User { get; set; }

        public IList<CatalogItem> Reports { get; set; }
    }
}