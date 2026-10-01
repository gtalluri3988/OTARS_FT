using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.ViewModels.Interfaces;

namespace MTAoarsFamily.ViewModels.Notifications {
    
    public class RenewalRejectionVariableSource : INotificationVariableSource {

        public int Total { get; set; }

        public int Year { get; set; }

        public int Quarter { get; set; }

    }// class

}// namesapce
