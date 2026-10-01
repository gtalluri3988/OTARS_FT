using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.ViewModels.Operations;

namespace MTAoarsFamily.ViewModels.Interfaces {
    public interface IExistableCompanyGuarantor {

        GuarantorViewModel Guarantor { get;  }

        long CompanyID { get; set; }

        DateTime? DateAppointed { get; set; }

        CorporateNomineeViewModel CorporateNominee { get; }

        string PhotoPath { get; set; }
    }

}
