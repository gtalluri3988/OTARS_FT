using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using MTAoarsFamily.ViewModels.Operations;
using System.Globalization;
using MTAoarsFamily.Repositories.Interfaces;
using MTAoarsFamily.DomainModels;
using MTAoarsFamily.Utilities.Constants;

namespace MTAoarsFamily.Validators.Operations.Fluents {
    [FluentRegisterable]
    public class RegistrationStartViewModelValidator : AbstractValidator<RegistrationStartViewModel> {

        public RegistrationStartViewModelValidator(ILookupRepository lookupRepository) {
            RuleFor(x => x.Company).NotNull().WithMessage("Company is required");
            RuleFor(x => x.AgencyType).NotNull().WithMessage("Please select registration type");
        }

        

    }// class

}// namespace
