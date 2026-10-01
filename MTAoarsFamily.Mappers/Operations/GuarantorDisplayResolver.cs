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
using MTAoarsFamily.Utilities.Interfaces;
using MTAoarsFamily.Utilities.Mvc;



namespace MTAoarsFamily.Mappers.Operations {

    class GuarantorDisplayResolver : ValueResolver<Agency, GuarantorDisplayViewModel> {
        IMemberRepository memberRepository;
        IScopeDataProvider dataProvider;

        public GuarantorDisplayResolver(IMemberRepository memberRepository, IScopeDataProvider dataProvider) {
            this.memberRepository = memberRepository;
            this.dataProvider = dataProvider;
        }

        protected override GuarantorDisplayViewModel ResolveCore(Agency source) {
            var companyId = dataProvider.Get<Identity>(GlobalConstants.CurrentIdentity).CompanyID;
            var ap = source.AgencyPrincipals.FirstOrDefault(p=>p.CompanyID == companyId);
            if(ap == null) return null;
            var guarantor = ap.AgencyPrincipalGuarantors.FirstOrDefault();
            if(guarantor == null) return null;
            return Mapper.Map<GuarantorDisplayViewModel>(guarantor);
        }

    }

}
