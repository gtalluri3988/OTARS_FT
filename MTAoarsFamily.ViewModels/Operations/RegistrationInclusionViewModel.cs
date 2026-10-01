using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using MTAoarsFamily.ViewModels.Interfaces;
using MTAoarsFamily.Utilities.Mvc;

namespace MTAoarsFamily.ViewModels.Operations {

    public class RegistrationInclusionViewModel : IExistableGuarantor, IExistableIntermediary, IExistableCompanyGuarantor {

        public AgencyDisplayViewModel Agency { get; set; }

        public long AgencyID { get; set; }

        public long CompanyID { get; set; }

        public bool IsGeneral { get; set; }

        public bool IsFamily { get; set; }

        public DateTime? DateAppointed { get; set; }

        public LookupItem AgencyType { get; set; }

        [Dependency]
        public GuarantorViewModel Guarantor { get; set; }

        [Dependency]
        public CorporateNomineeViewModel CorporateNominee { get; set; }

        public string PhotoPath { get; set; }

    }// class

}// namespace
