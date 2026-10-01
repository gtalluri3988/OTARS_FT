using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using MTAoarsFamily.Utilities.Attributes;
using MTAoarsFamily.Utilities.Mvc;

namespace MTAoarsFamily.ViewModels.Operations
{
    public class MaintenanceSearchViewModel
    {

        [Dependency]
        public AgencySearchRequestViewModel AgencySearch { get; set; }

        [Binder(typeof(JsonModelBinder))]
        [Dependency]
        public List<MaintenanceSearchResponseViewModel> Agents { get; set; }
    }
}
