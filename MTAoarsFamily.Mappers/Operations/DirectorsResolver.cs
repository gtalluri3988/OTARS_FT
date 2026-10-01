using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.Repositories.Interfaces;
using MTAoarsFamily.DomainModels;
using MTAoarsFamily.ViewModels.Operations;
using AutoMapper;
using MTAoarsFamily.Utilities.Constants;
using MTAoarsFamily.Utilities.Mvc;
using MTAoarsFamily.Utilities.Interfaces;

namespace MTAoarsFamily.Mappers.Operations
{
    class DirectorResolver : ValueResolver<Agency, List<DirectorViewModel>>
    {
        IMemberRepository memberRepository;
        IScopeDataProvider dataProvider;
        public DirectorResolver(IMemberRepository memberRepository,IScopeDataProvider dataProvider)
        {
            this.memberRepository = memberRepository;
            this.dataProvider = dataProvider;
        }

        protected override List<DirectorViewModel> ResolveCore(Agency source)
        {
            var result = new List<DirectorViewModel>();
            var members = source.AgencyMembers.Where(desig => desig.LookupDesignation.Code == LookupConstants.Designation.Director)
                .Select(p => p.Member);
            var companyId = dataProvider.Get<Identity>(GlobalConstants.CurrentIdentity).CompanyID;
            var ap = source.AgencyPrincipals.FirstOrDefault(p => p.CompanyID == companyId);
            foreach (var member in members)
            {
                var director = Mapper.Map<DirectorViewModel>(member);               
                if (ap != null) {
                    director.AgencyType = Mapper.Map<LookupItem>(ap.LookupAgencyType);
                }
                result.Add(director);
                
            }
            return result;
        }

    }
}
