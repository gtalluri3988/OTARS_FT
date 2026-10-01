using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.ViewModels.Interfaces;
using MTAoarsFamily.Utilities.Attributes;

namespace MTAoarsFamily.ViewModels.Notifications {
    
    public class BatchRenewalVariableSource : INotificationVariableSource, IExistableTerminationList, IExistableRenewalList {

        public BatchRenewalVariableSource() {
            TerminationList = new List<TerminatedVariableSource>();
            RenewalList = new List<RenewedVariableSource>();
        }

        [ValueProvider(typeof(TerminationListValueProvider))]
        public List<TerminatedVariableSource> TerminationList { get; private set; }

        [ValueProvider(typeof(RenewedListValueProvider))]
        public List<RenewedVariableSource> RenewalList { get; private set; }

        public int TotalTerminationCount {
            get { return TerminationList.Count; }
        }

        public int TotalRenewalCount {
            get { return RenewalList.Count; }
        }
        
    }// class

}// namesapce
