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

    public class RegistrationViewModelMemberCompanyValidator : IValidator<RegistrationViewModel>
    {
        IMemberRepository memberRepository;
        IScopeDataProvider dataProvider;

        public RegistrationViewModelMemberCompanyValidator(IMemberRepository memberRepository, IScopeDataProvider dataProvider)
        {
            this.memberRepository = memberRepository;
            this.dataProvider = dataProvider;
        }

        public IEnumerable<ValidationMessage> Validate(RegistrationViewModel item)
        {
            var currentIdentity = dataProvider.Get<Identity>(GlobalConstants.CurrentIdentity);
            var principals = memberRepository.GetPrincipals(item.CorporateNominee.NewICNumber);

            var generalPrincipals = principals.Where(p => p.LookupIntermediaryType.Code == LookupConstants.IntermediaryType.General).ToList();
            var familyPrincipals = principals.Where(p => p.LookupIntermediaryType.Code == LookupConstants.IntermediaryType.Family).ToList();

            var individualPrincipals = principals.Where(p => AgencyManager.IsIndividual(p.LookupAgencyType.Code)).ToList();
            var corperatePrincipals = principals.Where(p => !AgencyManager.IsIndividual(p.LookupAgencyType.Code)).ToList();

            var corperateGeneralPrincipals = generalPrincipals.Where(p => !AgencyManager.IsIndividual(p.LookupAgencyType.Code)).ToList();
            var corperateFamilyPrincipals = familyPrincipals.Where(p => !AgencyManager.IsIndividual(p.LookupAgencyType.Code)).ToList();

            var individualGeneralPrincipals = generalPrincipals.Where(p => AgencyManager.IsIndividual(p.LookupAgencyType.Code)).ToList();
            var individualFamilyPrincipals = familyPrincipals.Where(p => AgencyManager.IsIndividual(p.LookupAgencyType.Code)).ToList();


            if (item.Agency.IsGeneral && generalPrincipals.Exists(p => p.CompanyID == item.CompanyID && p.LookupAgencyType.Code == item.AgencyType.Code))
                yield return new ValidationMessage("", "The member is already exists under General intermediary type in the same company");

            var requestedIndividual = AgencyManager.IsIndividual(item.AgencyType.Code);
            var sameTypeInSameCompany = familyPrincipals.Any(p =>
                p.CompanyID == item.CompanyID &&
                AgencyManager.IsIndividual(p.LookupAgencyType.Code) == requestedIndividual);

            if (sameTypeInSameCompany)
            {
                yield return new ValidationMessage("", "The member is already exists under Family intermediary type in the same company and agency type");
            }

            if (familyPrincipals.Any(p => p.CompanyID != item.CompanyID))
            {
                yield return new ValidationMessage("", "The member is already exists under Family intermediary type in another company; only one TO is allowed");
            }
        }

    }// class
}
