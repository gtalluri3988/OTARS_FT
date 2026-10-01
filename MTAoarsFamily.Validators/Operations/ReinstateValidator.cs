using System;
using System.Collections.Generic;
using System.Linq;
using MTAoarsFamily.ViewModels.Operations;
using MTAoarsFamily.ViewModels.Shared;
using MTAoarsFamily.Utilities.Interfaces;
using MTAoarsFamily.Utilities.Constants;
using MTAoarsFamily.Repositories.Interfaces;
using MTAoarsFamily.Utilities.Mvc;

namespace MTAoarsFamily.Validators.Operations
{
    public class ReinstateValidator : IValidator<RegistrationIndexViewModel>
    {
        IMemberRepository memberRepository;
        IScopeDataProvider dataProvider;

        public ReinstateValidator(IMemberRepository memberRepository, IScopeDataProvider dataProvider)
        {
            this.memberRepository = memberRepository;
            this.dataProvider = dataProvider;
        }

        public IEnumerable<ValidationMessage> Validate(RegistrationIndexViewModel item)
        {
            var currentIdentity = dataProvider.Get<Identity>(GlobalConstants.CurrentIdentity);
            var agency = memberRepository.GetAgencies(item.ICNumber).FirstOrDefault();
            if (agency == null) yield break;
            var principal = agency.AgencyPrincipals.Where(p => p.CompanyID == currentIdentity.CompanyID).FirstOrDefault();
            if (principal != null) yield break;
        }

    }// class
}// namespace
