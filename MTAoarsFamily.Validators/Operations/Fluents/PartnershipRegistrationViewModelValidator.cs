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
    public class PartnershipRegistrationViewModelValidator : CorporateRegistrationViewModelValidator<PartnershipRegistrationViewModel> {

        public PartnershipRegistrationViewModelValidator(AgencyViewModelValidator agencyValidator,
            AddressViewModelValidator addressValidator) : base(agencyValidator, addressValidator){
            RuleFor(x =>  x.Partners)
                .Must(list => (list != null && list.Count > 0))
                .WithMessage("Atleast one partner is required");
        }

    }// class
}// namespace
