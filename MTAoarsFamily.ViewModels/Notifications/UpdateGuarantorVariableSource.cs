    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.ViewModels.Interfaces;

namespace MTAoarsFamily.ViewModels.Notifications {
    
    public class UpdateGuarantorVariableSource : INotificationVariableSource {

        public string AgencyName { get; set; }

        public string GuarantorTypeDescription { get; set; }

    }// class

}// namesapce
