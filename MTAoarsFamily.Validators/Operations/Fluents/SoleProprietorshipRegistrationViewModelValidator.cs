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
using MTAoarsFamily.Validators.Shared.Fluent;


namespace MTAoarsFamily.Validators.Operations.Fluents {
    [FluentRegisterable]
    public class SoleProprietorshipRegistrationViewModelValidator : CorporateRegistrationViewModelValidator<SoleProprietorshipRegistrationViewModel> {

        public SoleProprietorshipRegistrationViewModelValidator(AgencyViewModelValidator agencyValidator, AddressViewModelValidator addressValidator):base(agencyValidator, addressValidator) {
           
        }

    }// class

}// namespace
