using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using MTAoarsFamily.ViewModels.Operations;
using MTAoarsFamily.Repositories.Interfaces;
using MTAoarsFamily.DomainModels;
using MTAoarsFamily.Utilities.Mvc;
using MTAoarsFamily.Utilities.Interfaces;
using MTAoarsFamily.Utilities.Constants;

namespace MTAoarsFamily.Mappers.Operations {

   class AgencyTypeResolver : ValueResolver<Agency, LookupItem> {

        ILookupRepository lookupRepository;
        IScopeDataProvider dataProvider;
        public AgencyTypeResolver(ILookupRepository lookupRepository, IScopeDataProvider dataProvider) {
            this.lookupRepository = lookupRepository;
            this.dataProvider = dataProvider;
        }

        protected override LookupItem ResolveCore(Agency source) {
            var companyId = dataProvider.Get<Identity>(GlobalConstants.CurrentIdentity).CompanyID;
            var ap = source.AgencyPrincipals.FirstOrDefault(p => p.CompanyID == companyId);
            if (ap == null) return null;
            return Mapper.Map<LookupItem>(ap.LookupAgencyType);

        }

    }// class

}// namespace
