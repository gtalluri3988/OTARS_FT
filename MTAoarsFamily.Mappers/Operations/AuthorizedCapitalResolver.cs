using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using MTAoarsFamily.DomainModels;
using MTAoarsFamily.Utilities.Interfaces;
using MTAoarsFamily.Utilities.Mvc;
using MTAoarsFamily.Utilities.Constants;

namespace MTAoarsFamily.Mappers.Operations {
    class AuthorizedCapitalResolver :ValueResolver<Agency, bool>
    {
        
        IScopeDataProvider dataProvider;
        public AuthorizedCapitalResolver(IScopeDataProvider dataProvider)
        {           
            this.dataProvider = dataProvider;
        }
        protected override bool ResolveCore(Agency source)
        {
           
            var companyId = dataProvider.Get<Identity>(GlobalConstants.CurrentIdentity).CompanyID;
            var ap = source.AgencyPrincipals.FirstOrDefault(p => p.CompanyID == companyId);
            if (ap == null) return false;
            var code = ap.LookupAgencyType.Code;
            if (code == LookupConstants.AgencyType.PrivateLimitedCompany || code == LookupConstants.AgencyType.PublicLimitedCompany
                || code == LookupConstants.AgencyType.Cooperative || code == LookupConstants.AgencyType.GovernmentAgency) {
                    return true;                 
            }
            return false;  
        }
    }
}
