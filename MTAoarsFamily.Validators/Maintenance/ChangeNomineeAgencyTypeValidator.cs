using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.ViewModels.Shared;
using MTAoarsFamily.Repositories.Interfaces;
using MTAoarsFamily.DomainModels;
using MTAoarsFamily.ViewModels.Interfaces;
using MTAoarsFamily.ViewModels.Operations;
using MTAoarsFamily.ViewModels.Maintenance;
using MTAoarsFamily.Utilities.Constants;

namespace MTAoarsFamily.Validators.Operations
{
    public class UpdateNomineeAgencyTypeValidator : IValidator<ChangeNomineeViewModel>
    {
        IMemberRepository memberRepository;
        ILookupRepository lookupRepository;
        IRepository<AgencyPrincipal> principalRepository;

        public UpdateNomineeAgencyTypeValidator(IMemberRepository memberRepository, ILookupRepository lookupRepository, IRepository<AgencyPrincipal> principalRepository)
        {
            this.memberRepository = memberRepository;
            this.lookupRepository = lookupRepository;
            this.principalRepository = principalRepository;
        }

        public IEnumerable<ValidationMessage> Validate(ChangeNomineeViewModel item)
        {
            var nominee = item.RegistrationModel.CorporateNominee;
            var icNumber = nominee.ICType.Code == LookupConstants.ICTypes.NewIc ? nominee.NewICNumber : nominee.PassportNumber;
            var agencies = memberRepository.GetActiveAgencies(icNumber);
            var principalAgency = principalRepository.Get(item.PrincipalID).Agency;

            // var agency = agencies.Where(p => p.LookupAgencyType.Code != item.Model.AgencyType.Code).FirstOrDefault();
            var agency = agencies.FirstOrDefault();
            if (agency == null) yield break;
            //if (!agency.AgencyMembers.Any(x => x.DesignationID != 1)) yield break;

            if (!agency.AgencyMembers.Any(x => x.DesignationID == 1 && (x.Member.NewICNumber == icNumber || x.Member.PassportNumber == icNumber))) yield break;

            var type = lookupRepository.Get<LookupAgencyType>(agency.AgencyPrincipals.FirstOrDefault().TypeID);
            if (agency.ID == principalAgency.ID) yield return new ValidationMessage("", "This Agent / Coroporate Nominee \"{0}\" is already corporate nominee in this agency. Use Change Agency module to update other details", icNumber);
            else yield return new ValidationMessage("", "This Agent / Corporate Nominee  \"{0}\" is currently registered as an {1} in a different agency", icNumber, type.Description);
        }

    }// class
}// namespace
