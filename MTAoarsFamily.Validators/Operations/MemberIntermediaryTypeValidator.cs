using System;
using System.Collections.Generic;
using System.Linq;
using MTAoarsFamily.ViewModels.Shared;
using MTAoarsFamily.Repositories.Interfaces;
using MTAoarsFamily.DomainModels;
using MTAoarsFamily.ViewModels.Interfaces;
using MTAoarsFamily.ViewModels.Operations;
using MTAoarsFamily.Utilities.Constants;
namespace MTAoarsFamily.Validators.Operations
{

    public class MemberIntermediaryValidator : BaseValidator, IValidator<RegistrationIndexViewModel>
    {
        private static readonly NLog.Logger validationLogger = NLog.LogManager.GetCurrentClassLogger();
        IMemberRepository memberRepository;

        public MemberIntermediaryValidator(IMemberRepository memberRepository)
        {
            this.memberRepository = memberRepository;
        }

        public IEnumerable<ValidationMessage> Validate(RegistrationIndexViewModel item)
        {
            validationLogger.Info("[RegistrationValidation][Family][MemberIntermediaryValidator] START IC={0}, CompanyID={1}, IsIndividual={2}, IsFamily={3}",
                item == null ? "" : item.ICNumber,
                item == null ? 0 : item.CompanyID,
                item != null && item.IsIndividual,
                item != null && item.IsFamily);

            var principals = memberRepository.GetPrincipals(item.ICNumber).ToList();
            validationLogger.Info("[RegistrationValidation][Family][MemberIntermediaryValidator] GetPrincipals count={0}", principals.Count);
            if (principals.Count == 0)
            {
                validationLogger.Info("[RegistrationValidation][Family][MemberIntermediaryValidator] No principals found. PASS.");
                yield break;
            }

            foreach (var principal in principals)
            {
                validationLogger.Info("[RegistrationValidation][Family][MemberIntermediaryValidator] Principal ID={0}, CompanyID={1}, Company={2}, IsIndividual={3}, IsBancaStaff={4}, AgencyNumber={5}",
                    principal == null ? 0 : principal.ID,
                    principal == null ? 0 : principal.CompanyID,
                    principal == null || principal.Company == null ? "" : principal.Company.Name,
                    principal != null && principal.IsIndividual,
                    principal == null || !principal.IsBancaStaff.HasValue ? "NULL" : principal.IsBancaStaff.Value.ToString(),
                    principal == null ? "" : principal.AgencyNumber);
            }

            var member = memberRepository.Get(item.ICNumber);
            bool storedIsBancaStaff = member != null && member.IsBancaStaff.GetValueOrDefault();
            bool isBancaStaff = item.IsBancaStaff;
            validationLogger.Info("[RegistrationValidation][Family][MemberIntermediaryValidator] Member found={0}, RequestedIsBancaStaff={1}, StoredMemberIsBancaStaff={2}",
                member != null, isBancaStaff, storedIsBancaStaff);

            if (isBancaStaff)
            {
                var sameCompanyBancaPrincipal = principals.FirstOrDefault(p =>
                    p != null && p.IsBancaStaff == true && p.CompanyID == item.CompanyID);
                validationLogger.Info("[RegistrationValidation][Family][MemberIntermediaryValidator] Same-company Banca principal found={0}",
                    sameCompanyBancaPrincipal != null);
                if (sameCompanyBancaPrincipal != null)
                {
                    var companyName = sameCompanyBancaPrincipal.Company == null ? "the existing TO" : sameCompanyBancaPrincipal.Company.Name;
                    validationLogger.Warn("[RegistrationValidation][Family][MemberIntermediaryValidator] BLOCKED: second Banca principal in same TO. Company={0}", companyName);
                    yield return new ValidationMessage("", String.Format("The member with IC number \"{0}\" is already registered under Family with {1}. A second Banca principal under the same TO is not allowed", item.ICNumber, companyName));
                }
                else
                {
                    var bancaPrincipal = principals.Where(p => p != null && p.IsIndividual != item.IsIndividual).FirstOrDefault();
                    validationLogger.Info("[RegistrationValidation][Family][MemberIntermediaryValidator] Banca opposite-type principal found={0}", bancaPrincipal != null);
                    if (bancaPrincipal != null)
                    {
                        validationLogger.Warn("[RegistrationValidation][Family][MemberIntermediaryValidator] BLOCKED by existing Banca agent-type rule.");
                        yield return new ValidationMessage("", String.Format("The Banca member with IC number \"{0}\" is already registered under {1} with {2}", item.ICNumber, bancaPrincipal.LookupIntermediaryType.Description, bancaPrincipal.Company.Name));
                    }
                    else
                    {
                        validationLogger.Info("[RegistrationValidation][Family][MemberIntermediaryValidator] Banca quota PASS: different TO.");
                    }
                }
                yield break;
            }

            var familyPrincipals = principals.Where(p =>
                p != null).ToList();
            var sameTypeInSameCompany = familyPrincipals.Any(p =>
                p.CompanyID == item.CompanyID && p.IsIndividual == item.IsIndividual);
            var familyPrincipalInAnotherCompany = familyPrincipals.Any(p => p.CompanyID != item.CompanyID);
            validationLogger.Info("[RegistrationValidation][Family][MemberIntermediaryValidator] SameTypeInSameCompany={0}, PrincipalInAnotherCompany={1}",
                sameTypeInSameCompany, familyPrincipalInAnotherCompany);

            if (sameTypeInSameCompany || familyPrincipalInAnotherCompany)
            {
                var familyPrincipal = familyPrincipals.FirstOrDefault(p =>
                    (p.CompanyID == item.CompanyID && p.IsIndividual == item.IsIndividual) ||
                    p.CompanyID != item.CompanyID);
                var companyName = familyPrincipal == null || familyPrincipal.Company == null ? "the existing TO" : familyPrincipal.Company.Name;
                validationLogger.Warn("[RegistrationValidation][Family][MemberIntermediaryValidator] BLOCKED. Company={0}", companyName);
                yield return new ValidationMessage("", String.Format("The member with IC number \"{0}\" is already registered under Family with {1}", item.ICNumber, companyName));
            }
            else
            {
                validationLogger.Info("[RegistrationValidation][Family][MemberIntermediaryValidator] Family rule PASS. Opposite type in same TO is allowed.");
            }
            validationLogger.Info("[RegistrationValidation][Family][MemberIntermediaryValidator] END.");
        }

    }// class
}// namespace
