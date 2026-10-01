using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.ViewModels.Operations;
using Microsoft.Practices.Unity;
using MTAoarsFamily.Utilities.Attributes;
using MTAoarsFamily.Utilities.Mvc;

namespace MTAoarsFamily.ViewModels.Maintenance
{
    public  class SearchViewModel
    {
        [Dependency]
        public AgencySearchRequestViewModel AgencySearch { get; set; }

        [Binder(typeof(JsonModelBinder))]
        public IEnumerable<TerminationSearchResponseViewModel> TerminationAgents { get; set; }



    }
}
