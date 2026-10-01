using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.ViewModels.Shared;
using MTAoarsFamily.Repositories.Interfaces;
using MTAoarsFamily.DomainModels;
using MTAoarsFamily.ViewModels.Interfaces;
using MTAoarsFamily.ViewModels.Operations;
using MTAoarsFamily.Utilities.Constants;

namespace MTAoarsFamily.Validators.Operations
{
    public class BusinessRegistrationNumberValidator : IValidator<RegistrationIndexViewModel>
    {
        IAgencyRepository agencyRepository;
        IMemberRepository memberRepository;

        public BusinessRegistrationNumberValidator(IAgencyRepository agencyRepository, IMemberRepository memberRepository)
        {
            this.agencyRepository = agencyRepository;
            this.memberRepository = memberRepository;
        }

        public IEnumerable<ValidationMessage> Validate(RegistrationIndexViewModel item)
        {
            if (string.IsNullOrWhiteSpace(item.BusinessRegistrationNumber)) yield break;
            if (item.AgencyType.Code == LookupConstants.AgencyType.Individual) yield break;

            //var agency = agencyRepository.SelectOtherTypeAgency(item.BusinesssRegistrationNumber, item.TypeCode);
            var agency = agencyRepository.Select(item.BusinessRegistrationNumber);
            if (agency == null) yield break;

            var intermediaryTypes = new List<string> { LookupConstants.IntermediaryType.Family };

            var isExist = agency.AgencyPrincipals.Where(
                p => p.LookupAgencyType.Code != LookupConstants.AgencyType.Individual
                && p.CompanyID == item.CompanyID
                && intermediaryTypes.Contains(p.LookupIntermediaryType.Code)
            ).Any(prop => prop.LookupAgencyType.Code == item.AgencyType.Code);
            if (isExist)
            {
                yield return new ValidationMessage("", "Business registration number {0} already registered under {1}", item.BusinessRegistrationNumber, item.AgencyType.Description);
            }

            var cn = memberRepository.GetCorporateNominee(agency.ID);
            var icNumber = cn.LookupICType.Code == LookupConstants.ICTypes.NewIc ? cn.NewICNumber : cn.PassportNumber;
            if (icNumber != item.ICNumber)
            {
                yield return new ValidationMessage("", "Business registration number {0} is not matching with the IC number", item.BusinessRegistrationNumber, item.AgencyType.Description);
            }
        }

    }// class
}// namespace
