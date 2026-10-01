using System.Collections.Generic;
using System.Linq;
using MTAoarsGeneral.Repositories.Interfaces;
using MTAoarsGeneral.Utilities.Constants;
using MTAoarsGeneral.ViewModels.Operations;

namespace MTAoarsGeneral.Validators.Operations
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
            logger.Info("[RegistrationValidation][General][Inclusion] Start. AgencyID={0}, CompanyID={1}",
                item == null ? 0 : item.AgencyID,
                item == null ? 0 : item.CompanyID);
            if (item == null)
            {
                logger.Warn("[RegistrationValidation][General][Inclusion] STOPPED: model is null.");
                yield break;
            }

            var corporateNominee = memberRepository.GetCorporateNominee(item.AgencyID);
            logger.Info("[RegistrationValidation][General][Inclusion] Corporate nominee found={0}", corporateNominee != null);
            if (corporateNominee == null) yield break;

            var icNumber = string.IsNullOrEmpty(corporateNominee.NewICNumber)
                ? corporateNominee.PassportNumber
                : corporateNominee.NewICNumber;

            logger.Info("[RegistrationValidation][General][Inclusion] Corporate nominee IC available={0}", !string.IsNullOrEmpty(icNumber));
            if (string.IsNullOrEmpty(icNumber)) yield break;

            var generalPrincipals = memberRepository.GetPrincipals(icNumber)
                .Where(p => p.LookupIntermediaryType.Code == LookupConstants.IntermediaryType.General)
                .ToList();
            logger.Info("[RegistrationValidation][General][Inclusion] Existing General principal count={0}", generalPrincipals.Count);

            if (generalPrincipals.Any(p => p.CompanyID == item.CompanyID))
            {
                logger.Warn("[RegistrationValidation][General][Inclusion] BLOCKED: principal exists in same TO.");
                yield return new ValidationMessage("",
                    "The member already has a General principal in the same company");
            }

            if (generalPrincipals.Count >= 2)
            {
                logger.Warn("[RegistrationValidation][General][Inclusion] BLOCKED: maximum two General principals reached.");
                yield return new ValidationMessage("",
                    "The member already has the maximum two General principals");
            }
            logger.Info("[RegistrationValidation][General][Inclusion] Completed.");
        }
    }
}
