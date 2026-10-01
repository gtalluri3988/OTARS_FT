using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.ViewModels.Operations;
using MTAoarsFamily.Utilities.Mvc;

namespace MTAoarsFamily.ViewModels.Interfaces {
    public interface IExistableNewIC {

        LookupItem ICType { get; set; }

        string ICNumber { get; set; }

    }
}
