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


namespace MTAoarsFamily.Mappers.Operations {

    class AgencyPrincipalViewModelResolver : ValueResolver<Agency, List<AgencyPrincipalDisplayViewModel>> {
       
        public AgencyPrincipalViewModelResolver() {
           
        }

        protected override List<AgencyPrincipalDisplayViewModel> ResolveCore(Agency source) {
            var output = new List<AgencyPrincipalDisplayViewModel>();
            foreach (var ap in source.AgencyPrincipals.Where(p => p.LookupIntermediaryType.Code == LookupConstants.IntermediaryType.Family)) {
                output.Add(Mapper.Map<AgencyPrincipalDisplayViewModel>(ap));
            }
            foreach (var aph in source.AgencyPrincipalHistories.Where(p => p.LookupIntermediaryType.Code == LookupConstants.IntermediaryType.Family)) {
                output.Add(Mapper.Map<AgencyPrincipalDisplayViewModel>(aph));
            }
            return output;
        }

        
    }

}
