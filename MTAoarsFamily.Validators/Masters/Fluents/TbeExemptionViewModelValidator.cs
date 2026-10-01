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

namespace MTAoarsFamily.Validators.Operations.Fluents
{
    [FluentRegisterable]
    public class TbeExemptionViewModelValidator : AbstractValidator<TbeExemptionViewModel>
    {

        public TbeExemptionViewModelValidator()
        {
            RuleFor(x => x.ICNumber)
              .NotNull()
              .Must(ValidateNewIC)
              .WithMessage("New IC number should be 12 digits and begin with date of birth (yyMMdd) format");

            RuleFor(x => x.Name)
                .NotNull();
            RuleFor(x => x.DateDocumentReceived)
                .NotNull();

        }

        bool ValidateNewIC(string icNumber)
        {
            return icNumber.IsNewIC();
        }

    }// class

}// namespace
