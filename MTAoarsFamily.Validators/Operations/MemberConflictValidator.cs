using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.ViewModels.Shared;
using MTAoarsFamily.Repositories.Interfaces;
using MTAoarsFamily.DomainModels;
using MTAoarsFamily.ViewModels.Interfaces;
using MTAoarsFamily.Utilities.Interfaces;
using MTAoarsFamily.Utilities.Constants;

namespace MTAoarsFamily.Validators.Operations {
    public class MemberConflictValidator : IValidator<IMemberViewModel> {
        IMemberRepository memberRepository;

        public MemberConflictValidator(IMemberRepository memberRepository) {
            this.memberRepository = memberRepository;
        }

        public IEnumerable<ValidationMessage> Validate(IMemberViewModel item) {
            var result = memberRepository.IsMemberExistsInConflict(item.ICNumber);
            if (result == true) {
                yield return new ValidationMessage("", "The member is already in conflict and cannot be processed now.");
            }
        }

    }// class
}// namespace
