using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.ViewModels.Shared;
using MTAoarsFamily.Repositories.Interfaces;
using MTAoarsFamily.DomainModels;
using MTAoarsFamily.ViewModels.Interfaces;
using MTAoarsFamily.Utilities.Interfaces;
using MTAoarsFamily.Utilities.Constants;
using MTAoarsFamily.ViewModels.Operations;
using MTAoarsFamily.Utilities.Mvc;
using MTAoarsFamily.Utilities.Managers;

namespace MTAoarsFamily.Validators.Operations
{
    public class MemberCompanyValidator : IValidator<RegistrationIndexViewModel>
    {
        static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        IMemberRepository memberRepository;
        IScopeDataProvider dataProvider;

        public MemberCompanyValidator(IMemberRepository memberRepository, IScopeDataProvider dataProvider)
        {
            this.memberRepository = memberRepository;
            this.dataProvider = dataProvider;
        }

        public IEnumerable<ValidationMessage> Validate(RegistrationIndexViewModel item)
        {
            var currentIdentity = dataProvider.Get<Identity>(GlobalConstants.CurrentIdentity);
            var principals = memberRepository.GetPrincipals(item.ICNumber);
            logger.Info("[RegistrationValidation][Family][MemberCompany] Start. IC={0}, CurrentCompanyID={1}, AgencyType={2}",
                item.ICNumber, currentIdentity == null ? 0 : currentIdentity.CompanyID, item.AgencyType == null ? "" : item.AgencyType.Code);
            logger.Info("[RegistrationValidation][Family][MemberCompany] Principal count={0}", principals.Count());

            //Individual Family Agent in TO A wish to join as a Corporate Family Agent in the same TO is allowed;
            if (principals.Count(p => p.LookupIntermediaryType.Code == LookupConstants.IntermediaryType.Family
                && (p.LookupAgencyType.Code != item.AgencyType.Code)
                && p.CompanyID == currentIdentity.CompanyID) > 0)
            {
                logger.Info("[RegistrationValidation][Family][MemberCompany] PASSED: opposite Family agency type in same TO is allowed.");
                yield break;
            }
            if (principals.Count(p => p.LookupIntermediaryType.Code == LookupConstants.IntermediaryType.Family
                && p.CompanyID == currentIdentity.CompanyID) > 0)
            {
                logger.Warn("[RegistrationValidation][Family][MemberCompany] BLOCKED: Family principal already exists in same TO.");
                yield return new ValidationMessage("", "The member already exists in the same company");
            }
            logger.Info("[RegistrationValidation][Family][MemberCompany] Completed.");
        }

    }// class
}// namespace
