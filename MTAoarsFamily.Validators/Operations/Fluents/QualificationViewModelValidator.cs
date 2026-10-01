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

namespace MTAoarsFamily.Validators.Operations.Fluents {
    
    [FluentRegisterable]
    public class QualificationViewModelValidator : AbstractValidator<QualificationViewModel> {

        public QualificationViewModelValidator(ILookupRepository lookupRepository, SpouseViewModelValidator spouseValidator) {
            RuleFor(x => x.EducationalQualification)
                .NotNull();
                
            RuleFor(x => x.SchoolName)
                //.NotNull()
                .Length(0, 100);
            RuleFor(x => x.Year)
                .NotNull();
                
                    
        }

    }// class

}// namespace
