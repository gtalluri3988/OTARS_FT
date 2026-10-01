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



namespace MTAoarsFamily.Mappers.Operations {

    class SpouseDisplayResolver : ValueResolver<Agency, MemberDisplayViewModel> {
        IMemberRepository memberRepository;

        public SpouseDisplayResolver(IMemberRepository memberRepository) {
            this.memberRepository = memberRepository;
        }

        protected override MemberDisplayViewModel ResolveCore(Agency source) {
            var member = memberRepository.GetCorporateNominee(source.ID);
            if (member.Spouses.Count == 0) return null;
            return Mapper.Map<MemberDisplayViewModel>(member.Spouses.First());
        }

    }

}
