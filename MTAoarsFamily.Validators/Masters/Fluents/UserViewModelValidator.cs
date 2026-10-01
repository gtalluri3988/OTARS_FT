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
    public class UserViewModelValidator : AbstractValidator<UserMasterViewModel> {

        public UserViewModelValidator() {
            RuleFor(x => x.Name)
                .NotNull()
                .Length(1, 50);
            RuleFor(x => x.UserName)
                .NotNull()
                .Length(1, 100);
          
            
        }

    }// class

}// namespace
