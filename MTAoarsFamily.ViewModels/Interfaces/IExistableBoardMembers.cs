using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.ViewModels.Operations;
using MTAoarsFamily.Utilities.Attributes;
using MTAoarsFamily.Utilities.Mvc;
using Microsoft.Practices.Unity;

namespace MTAoarsFamily.ViewModels.Interfaces {
    public interface IExistableBoardMembers {

      List<DirectorViewModel> Directors { get; set; }
      List<ShareholderViewModel> Shareholders { get; set; }
      List<AdditionalCorporateNomineeViewModel> AdditionalCorporateNominees { get; set; }
      IEnumerable<LookupItem> ICTypes { get; set; }

    }
}
