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

namespace MTAoarsFamily.Validators.Operations.Fluents {
    [FluentRegisterable]
    public class CBCStartViewModelValidator : AbstractValidator<CBCStartViewModel> {

        public CBCStartViewModelValidator() {
            RuleFor(x => x.Year)
                .NotNull().WithMessage("Please select Year");

            RuleFor(x => x.Quarter)
                .NotNull().WithMessage("Please select Quarter");

        }

    }// class

}// namespace
