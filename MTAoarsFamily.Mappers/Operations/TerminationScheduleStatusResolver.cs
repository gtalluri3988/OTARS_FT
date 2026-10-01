using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using MTAoarsFamily.ViewModels.Operations;
using MTAoarsFamily.Repositories.Interfaces;
using MTAoarsFamily.DomainModels;
using MTAoarsFamily.Utilities.Constants;

namespace MTAoarsFamily.Mappers.Operations {

    class TerminationScheduleStatusResolver : ValueResolver<TerminationSearchResponseViewModel, long> {

        ILookupRepository lookupRepository;

        public TerminationScheduleStatusResolver(ILookupRepository lookupRepository) {
            this.lookupRepository = lookupRepository;
        }

        protected override long ResolveCore(TerminationSearchResponseViewModel source) {
            return lookupRepository.Get<LookupTerminationStatus>(LookupConstants.TerminationStatus.Scheduled).ID;
        }

    }// class

}// namespace
