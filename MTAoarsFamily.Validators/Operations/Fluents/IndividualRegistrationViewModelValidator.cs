using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.ViewModels.Operations;
using FluentValidation;
using MTAoarsFamily.Utilities.IoC;
using MTAoarsFamily.Validators.Shared.Fluent;

namespace MTAoarsFamily.Validators.Operations.Fluents {
    [FluentRegisterable()]
    public class IndividualRegistrationViewModelValidator : AbstractValidator<IndividualRegistrationViewModel> {

        public IndividualRegistrationViewModelValidator(AddressViewModelValidator addressValidator) {
            RuleFor(x => x.Address)
                .SetValidator(addressValidator);
            RuleFor(x => x.CorporateNominee.Phone)
                .NotNull();
        }

    }// class
}// namespace
