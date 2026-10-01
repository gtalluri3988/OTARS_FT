    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.ViewModels.Interfaces;

namespace MTAoarsFamily.ViewModels.Notifications {
    
    public class ChangeNomineeVariableSource : INotificationVariableSource {

        public string NewICNumber { get; set; }

        public string Name { get; set; }
    }// class

}// namesapce
