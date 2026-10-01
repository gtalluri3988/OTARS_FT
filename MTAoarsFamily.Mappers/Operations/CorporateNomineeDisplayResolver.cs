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
using MTAoarsFamily.Utilities.Mvc;



namespace MTAoarsFamily.Mappers.Operations {

    class CorporateNomineeDisplayResolver : ValueResolver<Agency, MemberDisplayViewModel> {
        IMemberRepository memberRepository;

        public CorporateNomineeDisplayResolver(IMemberRepository memberRepository) {
            this.memberRepository = memberRepository;
        }

        protected override MemberDisplayViewModel ResolveCore(Agency source) {
            var am = source.AgencyMembers.First(p => p.LookupDesignation.Code == LookupConstants.Designation.CorporateNominee);
            var member = am.Member;
            var result = Mapper.Map<MemberDisplayViewModel>(member);
            result.IsPartTime = am.IsPartTime.GetValueOrDefault();
            result.Qualification = new QualificationDisplayViewModel();
            var  qualification = member.MemberEducationalQualifications.FirstOrDefault();
            if (qualification != null) {
                result.Qualification.EducationalQualification = Mapper.Map<LookupItem>(qualification.LookupEducationalQualification);
                result.Qualification.SchoolName = qualification.SchoolName;
                result.Qualification.Year = qualification.Year;
            }

            var ins = member.MemberInsuranceQualifications.FirstOrDefault();    
            if (ins != null) {
                result.Qualification.InsuranceQualification = ins.LookupInsuranceQualification.Description;
            }
            return result;
        }

    }

}
