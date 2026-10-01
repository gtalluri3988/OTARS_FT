using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsGeneral.ViewModels.Shared;
using MTAoarsGeneral.Repositories.Interfaces;
using MTAoarsGeneral.DomainModels;
using MTAoarsGeneral.ViewModels.Interfaces;
using MTAoarsGeneral.Utilities.Interfaces;
using MTAoarsGeneral.Utilities.Constants;
using MTAoarsGeneral.ViewModels.Operations;
using MTAoarsGeneral.Utilities.Mvc;
using MTAoarsGeneral.Utilities.Managers;

namespace MTAoarsGeneral.Validators.Operations
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
            logger.Info("[RegistrationValidation][General][MemberCompany] Start. IC={0}, CurrentCompanyID={1}, AgencyType={2}",
                item.ICNumber, currentIdentity == null ? 0 : currentIdentity.CompanyID, item.AgencyType == null ? "" : item.AgencyType.Code);
            logger.Info("[RegistrationValidation][General][MemberCompany] Principal count={0}", principals.Count());


            if (item.IsGeneral
                && principals.Count(p => p.LookupIntermediaryType.Code == LookupConstants.IntermediaryType.General
                    && p.CompanyID == currentIdentity.CompanyID && p.LookupAgencyType.Code == item.AgencyType.Code) > 0)
            {
                logger.Warn("[RegistrationValidation][General][MemberCompany] BLOCKED: General principal already exists in same company and same agency type.");
                yield return new ValidationMessage("", "The member is already exists under General intermediary type in the same company");
            }
            logger.Info("[RegistrationValidation][General][MemberCompany] Completed.");
        }

    }// class
}// namespace
