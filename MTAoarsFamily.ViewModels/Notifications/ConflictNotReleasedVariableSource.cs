using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.ViewModels.Interfaces;

namespace MTAoarsFamily.ViewModels.Notifications {
    
    public class ConflictNotReleasedVariableSource : INotificationVariableSource {

        public string AgencyNumber { get; set; }
        
        public String CompanyName { get; set; }

    }// class

}// namesapce
