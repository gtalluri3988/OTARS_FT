using System.Collections.Generic;
using System.Linq;
using MTAoarsFamily.Repositories.Interfaces;
using MTAoarsFamily.Utilities.Constants;
using MTAoarsFamily.Utilities.Managers;
using MTAoarsFamily.ViewModels.Operations;

namespace MTAoarsFamily.Validators.Operations
{
    public class RegistrationInclusionViewModelMemberCompanyValidator : IValidator<RegistrationInclusionViewModel>
    {
        static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        readonly IMemberRepository memberRepository;

        public RegistrationInclusionViewModelMemberCompanyValidator(IMemberRepository memberRepository)
        {
            this.memberRepository = memberRepository;
        }

        public IEnumerable<ValidationMessage> Validate(RegistrationInclusionViewModel item)
        {
            logger.Info("[RegistrationValidation][Family][Inclusion] Start. AgencyID={0}, CompanyID={1}, AgencyType={2}",
                item == null ? 0 : item.AgencyID,
                item == null ? 0 : item.CompanyID,
                item == null || item.AgencyType == null ? "" : item.AgencyType.Code);
            if (item == null || item.AgencyType == null)
            {
                logger.Warn("[RegistrationValidation][Family][Inclusion] STOPPED: model or AgencyType is null.");
                yield break;
            }

            var corporateNominee = memberRepository.GetCorporateNominee(item.AgencyID);
            logger.Info("[RegistrationValidation][Family][Inclusion] Corporate nominee found={0}", corporateNominee != null);
            if (corporateNominee == null) yield break;

            var icNumber = string.IsNullOrEmpty(corporateNominee.NewICNumber)
                ? corporateNominee.PassportNumber
                : corporateNominee.NewICNumber;

            logger.Info("[RegistrationValidation][Family][Inclusion] Corporate nominee IC available={0}", !string.IsNullOrEmpty(icNumber));
            if (string.IsNullOrEmpty(icNumber)) yield break;

            var familyPrincipals = memberRepository.GetPrincipals(icNumber)
                .Where(p => p.LookupIntermediaryType.Code == LookupConstants.IntermediaryType.Family)
                .ToList();
            logger.Info("[RegistrationValidation][Family][Inclusion] Existing Family principal count={0}", familyPrincipals.Count);

            if (familyPrincipals.Any(p => p.CompanyID != item.CompanyID))
            {
                logger.Warn("[RegistrationValidation][Family][Inclusion] BLOCKED: principal exists in another TO.");
                yield return new ValidationMessage("",
                    "The member is already registered under Family in another company; only one TO is allowed");
            }

            var requestedIndividual = AgencyManager.IsIndividual(item.AgencyType.Code);
            if (familyPrincipals.Any(p =>
                p.CompanyID == item.CompanyID &&
                AgencyManager.IsIndividual(p.LookupAgencyType.Code) == requestedIndividual))
            {
                logger.Warn("[RegistrationValidation][Family][Inclusion] BLOCKED: same Family agent type already exists in same TO.");
                yield return new ValidationMessage("",
                    "The member already has the maximum one Family principal for this agency type in the same company");
            }
            logger.Info("[RegistrationValidation][Family][Inclusion] Completed.");
        }
    }
}
