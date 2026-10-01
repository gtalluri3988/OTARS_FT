using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using MTAoarsFamily.DomainModels;
using MTAoarsFamily.ViewModels.Shared;
using MTAoarsFamily.Utilities.Mvc;

namespace MTAoarsFamily.Mappers.Shared {
    public class LookupMapper : IMapper {

        public void Map() {
            Mapper.CreateMap<ILookupEntity, LookupItem>();
            Mapper.CreateMap<Company, LookupItem>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Name));
            Mapper.CreateMap<Activity, LookupItem>();
        }

    }
}
