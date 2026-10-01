using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using MTAoarsFamily.DomainModels;
using MTAoarsFamily.ViewModels.Operations;
using MTAoarsFamily.ViewModels.Maintenance;
using MTAoarsFamily.Utilities.Constants;
using MTAoarsFamily.Utilities.Mvc;

namespace MTAoarsFamily.Mappers.Operations {

    public class CBCMapper : IMapper {

        public void Map() {
            Mapper.CreateMap<CBCHeader, CBCHeaderViewModel>()
                 .ForMember(dest => dest.Company, opt => opt.MapFrom(src => Mapper.Map<LookupItem>(src.Company)))
                  .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Mapper.Map<LookupItem>(src.LookupCBCHeaderStatu)));

            Mapper.CreateMap<CBCDetail, CBCDetailViewModel>()
                .ForMember(dest => dest.IntermediaryTypeDescription, opt => opt.MapFrom(src => src.LookupIntermediaryType.Description))
                .AfterMap((src, dest) => {
                    var cn = src.Agency.AgencyMembers
                        .FirstOrDefault(am => am.LookupDesignation.Code == LookupConstants.Designation.CorporateNominee);
                    if (cn != null) {
                        var m = cn.Member;
                        dest.NomineeICNumber = m.LookupICType.Code == LookupConstants.ICTypes.NewIc? m.NewICNumber : m.PassportNumber;
                        dest.NomineeName = m.Name;
                    }
                    var ap = src.Agency.AgencyPrincipals.FirstOrDefault(p => p.CompanyID == src.CBCHeader.CompanyID
                        && p.LookupIntermediaryType.Code == LookupConstants.IntermediaryType.Family);
                    if (ap != null) {
                        dest.AgencyNumber = ap.AgencyNumber;
                    }
                });
        }       

    }// class

}// namespace
