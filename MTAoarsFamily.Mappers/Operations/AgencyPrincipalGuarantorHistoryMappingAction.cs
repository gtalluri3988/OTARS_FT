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

    class AgencyPrincipalGuarantorHistoryMappingAction : IMappingAction<AgencyPrincipal, AgencyPrincipalHistory> {

        public void Process(AgencyPrincipal source, AgencyPrincipalHistory destination) {
            foreach (var item in source.AgencyPrincipalGuarantors) {
                destination.AgencyPrincipalGuarantorHistories.Add(Mapper.Map<AgencyPrincipalGuarantorHistory>(item));
            }
        }

    }// class
}// namespace
