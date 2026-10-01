using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsGeneral.ViewModels.Shared;
using MTAoarsGeneral.Repositories.Interfaces;
using MTAoarsGeneral.DomainModels;
using MTAoarsGeneral.ViewModels.Interfaces;
using MTAoarsGeneral.ViewModels.Operations;
using MTAoarsGeneral.Utilities.Constants;
namespace MTAoarsGeneral.Validators.Operations
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
            validationLogger.Info("[RegistrationValidation][General][MemberIntermediaryValidator] START IC={0}, CompanyID={1}, IsIndividual={2}, IsGeneral={3}",
                item == null ? "" : item.ICNumber,
                item == null ? 0 : item.CompanyID,
                item != null && item.IsIndividual,
                item != null && item.IsGeneral);

            var principals = memberRepository.GetPrincipals(item.ICNumber).ToList();
            validationLogger.Info("[RegistrationValidation][General][MemberIntermediaryValidator] GetPrincipals count={0}", principals.Count);
            if (principals.Count == 0)
            {
                validationLogger.Info("[RegistrationValidation][General][MemberIntermediaryValidator] No principals found. PASS.");
                yield break;
            }

            foreach (var principalItem in principals)
            {
                validationLogger.Info("[RegistrationValidation][General][MemberIntermediaryValidator] Principal ID={0}, CompanyID={1}, Company={2}, IsIndividual={3}, IsBancaStaff={4}, AgencyNumber={5}",
                    principalItem == null ? 0 : principalItem.ID,
                    principalItem == null ? 0 : principalItem.CompanyID,
                    principalItem == null || principalItem.Company == null ? "" : principalItem.Company.Name,
                    principalItem != null && principalItem.IsIndividual,
                    principalItem == null || !principalItem.IsBancaStaff.HasValue ? "NULL" : principalItem.IsBancaStaff.Value.ToString(),
                    principalItem == null ? "" : principalItem.AgencyNumber);
            }

            var member = memberRepository.Get(item.ICNumber);
            bool storedIsBancaStaff = member != null && member.IsBancaStaff.GetValueOrDefault();
            bool isBancaStaff = item.IsBancaStaff;
            validationLogger.Info("[RegistrationValidation][General][MemberIntermediaryValidator] Member found={0}, RequestedIsBancaStaff={1}, StoredMemberIsBancaStaff={2}",
                member != null, isBancaStaff, storedIsBancaStaff);

            if (isBancaStaff)
            {
                var sameCompanyBancaPrincipal = principals.FirstOrDefault(p =>
                    p != null && p.IsBancaStaff == true && p.CompanyID == item.CompanyID);
                validationLogger.Info("[RegistrationValidation][General][MemberIntermediaryValidator] Same-company Banca principal found={0}",
                    sameCompanyBancaPrincipal != null);
                if (sameCompanyBancaPrincipal != null)
                {
                    var companyName = sameCompanyBancaPrincipal.Company == null ? "the existing TO" : sameCompanyBancaPrincipal.Company.Name;
                    validationLogger.Warn("[RegistrationValidation][General][MemberIntermediaryValidator] BLOCKED: second Banca principal in same TO. Company={0}", companyName);
                    yield return new ValidationMessage("", String.Format("The member with IC number \"{0}\" is already registered under General with {1}. A second Banca principal under the same TO is not allowed", item.ICNumber, companyName));
                }
                else
                {
                    var principal = principals.Where(p => p != null && p.IsIndividual != item.IsIndividual).FirstOrDefault();
                    validationLogger.Info("[RegistrationValidation][General][MemberIntermediaryValidator] Banca opposite-type principal found={0}", principal != null);
                    if (principal != null)
                    {
                        validationLogger.Warn("[RegistrationValidation][General][MemberIntermediaryValidator] BLOCKED by existing Banca agent-type rule.");
                        yield return new ValidationMessage("", String.Format("The Banca member with IC number \"{0}\" is already registered under {1} with {2}", item.ICNumber, principal.LookupIntermediaryType.Description, principal.Company.Name));
                    }
                    else
                    {
                        validationLogger.Info("[RegistrationValidation][General][MemberIntermediaryValidator] Banca quota PASS: different TO.");
                    }
                }
                yield break;
            }

            var generalPrincipals = principals.Where(p =>
                p != null).ToList();
            validationLogger.Info("[RegistrationValidation][General][MemberIntermediaryValidator] PrincipalCount={0}, SameCompany={1}",
                generalPrincipals.Count, generalPrincipals.Any(p => p.CompanyID == item.CompanyID));
            if (generalPrincipals.Count >= 2)
            {
                validationLogger.Warn("[RegistrationValidation][General][MemberIntermediaryValidator] BLOCKED by maximum two principals rule.");
                var sb = new StringBuilder();
                generalPrincipals.ForEach(p => sb.Append(String.Format("{0}, ", p.Company.Name)));
                yield return new ValidationMessage("", String.Format("The member with IC number \"{0}\" is already registered under General with the following companies {1}", item.ICNumber, sb));
            }

            var sameCompanyGeneralPrinciapal = generalPrincipals.FirstOrDefault(x => x.CompanyID == item.CompanyID);
            if (sameCompanyGeneralPrinciapal != null )
            {
                validationLogger.Warn("[RegistrationValidation][General][MemberIntermediaryValidator] BLOCKED by same-company rule. CompanyID={0}", item.CompanyID);
                var companyName = sameCompanyGeneralPrinciapal.Company == null ? "the existing company" : sameCompanyGeneralPrinciapal.Company.Name;
                var agencyType = sameCompanyGeneralPrinciapal.LookupAgencyType == null ? "the existing" : sameCompanyGeneralPrinciapal.LookupAgencyType.Description;
                yield return new ValidationMessage("", String.Format("The member with IC number \"{0}\" is already registered under General with the following company {1} under {2} Agency type", item.ICNumber, companyName, agencyType));
            }
            validationLogger.Info("[RegistrationValidation][General][MemberIntermediaryValidator] END.");
        }

    }// class
}// namespace
