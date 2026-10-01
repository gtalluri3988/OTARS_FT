using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.ViewModels.Notifications;

namespace MTAoarsFamily.ViewModels.Shared {
    public class HomeViewModel {

        public HomeViewModel() {
            Mails = new List<MailViewModel>();
        }

        public int TotalMail { get; set; }

        public int TotalInvoice { get; set; }
        
        public int TotalRenewal { get; set; }

        
        public List<MailViewModel> Mails {
            get;
            private set;
        }

    }// class
}// namespace
