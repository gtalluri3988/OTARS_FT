using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.DomainModels;
using AutoMapper;
using MTAoarsFamily.ViewModels.Operations;
using MTAoarsFamily.Utilities.Constants;
using MTAoarsFamily.Utilities.IoC;
using MTAoarsFamily.Repositories.Interfaces;
using Microsoft.Practices.Unity;

namespace MTAoarsFamily.Mappers.Operations {
    public class MaintenanceMapper : IMapper {

        public void Map() {

            Mapper.CreateMap<SearchAgencyResult, MaintenanceSearchResponseViewModel>()
                .ForMember(dest => dest.ValidFromLabel, opt => opt.MapFrom(src => src.ValidFrom.Value.ToString(GlobalConstants.DateFormat)))
                .ForMember(dest => dest.ValidToLabel, opt => opt.MapFrom(src => src.ValidTo.Value.ToString(GlobalConstants.DateFormat)))
                .ForMember(dest => dest.ScheduledOn, opt => opt.UseValue(DateTime.Now))
                .AfterMap((src, dest) => {
                    var lookupRepository = ObjectContainer.Container.Resolve<ILookupRepository>();
                    var action = lookupRepository.Get<LookupTerminationAction>(LookupConstants.TerminationAction.Terminate);
                    dest.ActionID = action.ID;
                });
        }


    }// class
}// namespace
