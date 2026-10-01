using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.ViewModels.Interfaces;
using MTAoarsFamily.Utilities.Attributes;

namespace MTAoarsFamily.ViewModels.Notifications {
    
    public class BatchTerminationVariableSource : INotificationVariableSource, IExistableTerminationList {

        public BatchTerminationVariableSource() {
            TerminationList = new List<TerminatedVariableSource>();
        }

        [ValueProvider(typeof(TerminationListValueProvider))]
        public List<TerminatedVariableSource> TerminationList { get; private set; }
        
    }// class

}// namesapce
