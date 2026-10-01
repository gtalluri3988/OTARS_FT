using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using MTAoarsFamily.ViewModels.Shared;

namespace MTAoarsFamily.Validators.Shared.Fluent {
    
    public class AddressViewModelValidator : AbstractValidator<AddressViewModel> {

        public AddressViewModelValidator() {

            RuleFor(x => x.Address1)
                .NotNull()
                .Length(1, 75);
            RuleFor(x => x.Address2)
                .Length(0, 75);
            RuleFor(x => x.City)
                .NotNull()
                .Length(1, 50);
            RuleFor(x => x.PostalCode)
                .NotNull()
                .Length(1, 6);
            RuleFor(x => x.State).NotNull();
        }

    }// class

}// namespace
