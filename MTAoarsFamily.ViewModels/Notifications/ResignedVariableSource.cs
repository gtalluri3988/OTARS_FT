using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.ViewModels.Interfaces;

namespace MTAoarsFamily.ViewModels.Notifications {
    public class ResignedVariableSource : INotificationVariableSource {

        public string AgencyNumber { get; set; }

        public DateTime StartDate { get; set; }
    }
}
