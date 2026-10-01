using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.DomainModels;
using System.Data.Objects.DataClasses;
using MTAoarsFamily.ViewModels.Operations;
using MTAoarsFamily.Utilities.Extensions;
using AutoMapper;
using MTAoarsFamily.Repositories.Interfaces;
using MTAoarsFamily.Utilities.Constants;
using MTAoarsFamily.ViewModels.Shared;



namespace MTAoarsFamily.Mappers.Operations
{

    class ShareholderDisplayResolver : ValueResolver<Agency, List<MemberDisplayViewModel>>
    {
        IMemberRepository memberRepository;

        public ShareholderDisplayResolver(IMemberRepository memberRepository)
        {
            this.memberRepository = memberRepository;
        }

        protected override List<MemberDisplayViewModel> ResolveCore(Agency source)
        {
            var result = new List<MemberDisplayViewModel>();
            var agencyMembers = source.AgencyMembers.Where(desig => desig.LookupDesignation.Code == LookupConstants.Designation.Shareholder);

            foreach (var am in agencyMembers)
            {
                var member = Mapper.Map<MemberDisplayViewModel>(am.Member);
                member.ShareAmount = am.AmountShareholding;
                member.SharePercentage = Convert.ToDecimal(am.ShareholdingPercentage);
                member.NewBusinessRegistrationNumber = am.NewBusinessRegistrationNumber;
                result.Add(member);
            }
            return result;
        }

    }

}
