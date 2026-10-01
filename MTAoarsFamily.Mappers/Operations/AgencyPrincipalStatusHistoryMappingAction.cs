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
using MTAoarsFamily.ViewModels.Interfaces;



namespace MTAoarsFamily.Mappers.Operations {

    class AgencyPrincipalStatusHistoryMappingAction : IMappingAction<AgencyPrincipal, AgencyPrincipalHistory> {

        public void Process(AgencyPrincipal source, AgencyPrincipalHistory destination) {
            foreach (var item in source.AgencyPrincipalStatus) {
                destination.AgencyPrincipalStatusHistories.Add(Mapper.Map<AgencyPrincipalStatusHistory>(item));
            }
        }

    }// class
}// namespace
