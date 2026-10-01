using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.Utilities.Mvc;

namespace MTAoarsFamily.ViewModels.Maintenance {
    
    public class ReferredHeaderViewModel {

        public ReferredHeaderViewModel() {
            Details = new List<ReferredDetailViewModel>();
        }

        public long ID { get; set; }

        public LookupItem Company{ get; set; }

        public LookupItem Status { get; set; }

        public List<ReferredDetailViewModel> Details { get; set; }
    }// class

}// namespace
