using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using MTAoarsFamily.DomainModels;
using MTAoarsFamily.ViewModels.Operations;
using MTAoarsFamily.Utilities.Interfaces;
using MTAoarsFamily.Utilities.Constants;
using MTAoarsFamily.Repositories.Interfaces;
using MTAoarsFamily.ViewModels.Shared;
using MTAoarsFamily.Utilities.Mvc;

namespace MTAoarsFamily.Mappers.Operations
{
    class GuarantorResolver : ValueResolver<Agency, GuarantorViewModel>
    {
        IMemberRepository memberRepository;
        IScopeDataProvider dataProvider;
        public GuarantorResolver(IMemberRepository memberRepository, IScopeDataProvider dataProvider)
        {
            this.memberRepository = memberRepository;
            this.dataProvider = dataProvider;
        }
        protected override GuarantorViewModel ResolveCore(Agency source)
        {
            GuarantorViewModel guarantorModel = new GuarantorViewModel();
            var companyId = dataProvider.Get<Identity>(GlobalConstants.CurrentIdentity).CompanyID;
            var ap = source.AgencyPrincipals.FirstOrDefault(p => p.CompanyID == companyId);
            if (ap == null) return null;
            var guarantor = ap.AgencyPrincipalGuarantors.FirstOrDefault();
            if (guarantor == null) return null;
            guarantorModel.GuarantorType =  Mapper.Map<LookupItem>(guarantor.LookupGuarantorType);
            Mapper.Map(guarantor, guarantorModel);
            return guarantorModel;
        }
    }
}
