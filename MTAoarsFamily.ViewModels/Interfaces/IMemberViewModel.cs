using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.Utilities.Mvc;

namespace MTAoarsFamily.ViewModels.Interfaces {
    public interface IMemberViewModel {

        string Name { get; set; }

        LookupItem ICType { get; set; }

        string ICNumber { get; set; }

        LookupItem AgencyType{ get; set; }

        bool IsFamily { get; set; }

        bool IsGeneral { get; set; }

    }// class
}// namepsace
