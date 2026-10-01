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
using MTAoarsFamily.ViewModels.Notifications;

namespace MTAoarsFamily.Validators.Notifications.Fluents {
    
    [FluentRegisterable]
    public class NewMailViewModelValidator : AbstractValidator<NewMailViewModel> {

        
        public NewMailViewModelValidator() {
            RuleFor(x => x.CompanyIdentifiers)
                .Must((src, list) => (list != null && list.Count > 0))
                .WithMessage("Please select company");
            RuleFor(x => x.Subject).NotNull();
            RuleFor(x => x.Body).NotNull();
        }

    }// class

}// namespace
