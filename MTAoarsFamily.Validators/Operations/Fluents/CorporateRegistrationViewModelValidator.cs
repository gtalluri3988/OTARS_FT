using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.ViewModels.Operations;
using FluentValidation;
using MTAoarsFamily.Utilities.IoC;
using MTAoarsFamily.Validators.Shared.Fluent;
using Microsoft.Practices.Unity;

namespace MTAoarsFamily.Validators.Operations.Fluents {

    public class CorporateRegistrationViewModelValidator<T> : AbstractValidator<T> 
        where T:RegistrationViewModel {


        public CorporateRegistrationViewModelValidator(AgencyViewModelValidator agencyValidator, AddressViewModelValidator addressValidator) {
            RuleFor(x => x.Address)
               .SetValidator(addressValidator);
            RuleFor(x => x.Agency).SetValidator(agencyValidator);
            
            RuleFor(x => x.CorporateNominee.Phone)
                .NotNull();
            /*RuleFor(x => x.CorporateNominee.Fax)
               .NotNull();*/
        }

    }// class
}// namespace
