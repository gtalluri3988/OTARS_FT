using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.ViewModels.Operations;
using MTAoarsFamily.Utilities.Mvc;

namespace MTAoarsFamily.ViewModels.Interfaces {
    public interface IExistableAgency {

        AgencyViewModel Agency { get; set; }

        LookupItem AgencyType { get; }

    }
}
