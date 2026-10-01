using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.ViewModels.Operations;
using MTAoarsFamily.Repositories.Operations;
using MTAoarsFamily.ViewModels.Shared;
using MTAoarsFamily.Utilities.Interfaces;
using MTAoarsFamily.Utilities.Constants;
using MTAoarsFamily.Repositories.Interfaces;
using MTAoarsFamily.DomainModels;
using MTAoarsFamily.Utilities.Mvc;

namespace MTAoarsFamily.Validators.Operations {
    public class ConflictValidator : IValidator<RegistrationConflictViewModel> {


        public IEnumerable<ValidationMessage> Validate(RegistrationConflictViewModel item) {
            if (item.SourceCompanyID <= 0) {
                yield return new ValidationMessage("", "Please select Source Company");
            }
           if(item.ConflictAttachments.Count < 2){
               yield return new ValidationMessage("", "Please attach Agent Movement Form and Termination Letter");
           }
        }

    }// class
}// namesapce
