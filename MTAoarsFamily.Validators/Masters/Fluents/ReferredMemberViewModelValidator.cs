using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.ViewModels.Operations;
using FluentValidation;
using MTAoarsFamily.Utilities.IoC;
using Microsoft.Practices.Unity;
using MTAoarsFamily.Repositories.Interfaces;
using MTAoarsFamily.Utilities.Constants;
using MTAoarsFamily.DomainModels;
using MTAoarsFamily.ViewModels.Masters;
using MTAoarsFamily.Utilities.Extensions;
using MTAoarsFamily.ViewModels.Maintenance;

namespace MTAoarsFamily.Validators.Operations.Fluents {
    [FluentRegisterable]
    public class ReferredMemberViewModelValidator : AbstractValidator<ReferredDetailViewModel> {

        public ReferredMemberViewModelValidator() {
            RuleFor(x => x.Name)
                .NotNull()
                .Length(1, 50);
            RuleFor(x => x.Reason)
                .Must(x => x != null)
                .WithMessage("Please select Reason");
            RuleFor(x => x.Category)
                .Must(x=> x!= null)
                .WithMessage("Please select Category");
            RuleFor(x => x.ICNumber)
              .Must(ValidateNewIC)
              .WithMessage("New IC number should be 12 digits and begin with date of birth (yyMMdd) format");
            
        }

        bool ValidateNewIC(string icNumber) {
            return icNumber.IsNewIC();
        }

    }// class

}// namespace
