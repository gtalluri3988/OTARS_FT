using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.Utilities.Mvc;
using MTAoarsFamily.ViewModels.Shared;
using Microsoft.Practices.Unity;
using System.ComponentModel.DataAnnotations;

namespace MTAoarsFamily.ViewModels.Operations {
    public class AgencyBankerViewModel {
        
        [Display(Name = "Bank")]
        public LookupItem Bank { get; set; }

        [Display(Name = "Account Type")]
        public LookupItem AccountType { get; set; }

        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }

        [Dependency]
        public AddressViewModel Address { get; set; }

    }
}
