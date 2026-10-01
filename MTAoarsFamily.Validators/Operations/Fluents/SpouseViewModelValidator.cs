using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using MTAoarsFamily.ViewModels.Operations;

namespace MTAoarsFamily.Validators.Operations.Fluents {

  
    public class SpouseViewModelValidator : AbstractValidator<SpouseViewModel> {

        public SpouseViewModelValidator() {
            RuleFor(x => x.Name).NotNull();
        }

    }// class

}// namespace
