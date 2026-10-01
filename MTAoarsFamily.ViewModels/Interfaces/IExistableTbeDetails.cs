using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.Utilities.Mvc;

namespace MTAoarsFamily.ViewModels.Interfaces {
    public interface IExistableTbeDetails {

        string ICNumber { get;  }

        bool IsFamily { get;  }

        bool IsGeneral { get;  }

        LookupItem TbeCategory { get;  }

        bool IsOld { get; set; }
    }// interface
}// namespace
