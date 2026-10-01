using System.Collections.Generic;
using Microsoft.Practices.Unity;
using MTAoarsFamily.Utilities.Attributes;
using MTAoarsFamily.Utilities.Mvc;

namespace MTAoarsFamily.ViewModels.Operations {
    public class TerminationSearchViewModel {

        [Dependency]
        public AgencySearchRequestViewModel AgencySearch { get; set; }
        
        [Binder(typeof(JsonModelBinder))]
        [Dependency]
        public List<TerminationSearchResponseViewModel> TerminationAgents { get; set; }

        public IEnumerable<LookupItem> TerminationActions { get; set; }
    }
}
