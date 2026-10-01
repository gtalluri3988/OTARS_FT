using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.ViewModels.Shared;
using MTAoarsFamily.Repositories.Interfaces;
using MTAoarsFamily.DomainModels;
using MTAoarsFamily.ViewModels.Interfaces;
using MTAoarsFamily.ViewModels.Operations;
using MTAoarsFamily.Utilities.Config;
using MTAoarsFamily.Utilities.Interfaces;
using MTAoarsFamily.Utilities.Mvc;
using MTAoarsFamily.Utilities.Constants;

namespace MTAoarsFamily.Validators.Operations {
    public class CBCValidator : IValidator<CBCStartViewModel> {
        ICBCRepository cbcRepository;
        IScopeDataProvider dataProvider;

        public CBCValidator(ICBCRepository cbcRepository, IScopeDataProvider dataProvider) {
            this.cbcRepository = cbcRepository;
            this.dataProvider = dataProvider;
        }

        public IEnumerable<ValidationMessage> Validate(CBCStartViewModel item) {
            var identity = dataProvider.Get<Identity>(GlobalConstants.CurrentIdentity);
            if (identity.IsISMOrMTA) yield break;
            var header = cbcRepository.Get(Convert.ToInt32(item.Year.ID), Convert.ToInt32(item.Quarter.ID));
            if (header == null) yield break;
            var code = header.LookupCBCHeaderStatu.Code;
            var description = header.LookupCBCHeaderStatu.Description;
            if (code == LookupConstants.CBCHeaderStatus.Saved || code == LookupConstants.CBCHeaderStatus.Rejected || code == LookupConstants.CBCHeaderStatus.Submitted) yield break;
            yield return new ValidationMessage("", "The CBC details for the year {0} and quarter {1} has been {2}. Further modifications are not allowed. Please contact MTA", item.Year.ID.ToString(), item.Quarter.ID.ToString(), description);
        }

    }// class
}// namespace
