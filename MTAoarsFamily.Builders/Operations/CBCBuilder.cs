using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.Builders.Interfaces;
using MTAoarsFamily.ViewModels.Operations;
using MTAoarsFamily.Utilities.IoC;
using MTAoarsFamily.Utilities.Interfaces;
using MTAoarsFamily.Utilities.Mvc;
using MTAoarsFamily.Utilities.Constants;
using MTAoarsFamily.Repositories.Interfaces;
using MTAoarsFamily.DomainModels;
using MTAoarsFamily.Builders.Shared;
using MTAoarsFamily.Validators;

namespace MTAoarsFamily.Builders.Operations {
    public class CBCBuilder : BaseBuilder, ICBCBuilder {

        IObjectCreator viewModelCreator;
        ILookupRepository lookupRepository;
        IScopeDataProvider dataProvider;
        IAgencyBuilder agencyBuilder;
        long detailGenerated;
        long familyIntermediaryTypeId;

        public CBCBuilder(IObjectCreator viewModelCreator, ILookupRepository lookupRepository, IScopeDataProvider dataProvider, IAgencyBuilder agencyBuilder) {
            this.viewModelCreator = viewModelCreator;
            this.lookupRepository = lookupRepository;
            this.dataProvider = dataProvider;
            this.agencyBuilder = agencyBuilder;
            this.agencyBuilder.CurrentContext = this.CurrentContext;
            detailGenerated = lookupRepository.Get<LookupCBCDetailStatus>(LookupConstants.CBCDetailStatus.Generated).ID;
            familyIntermediaryTypeId = lookupRepository.Get<LookupIntermediaryType>(LookupConstants.IntermediaryType.Family).ID;
        }

        public CBCStartViewModel GetStartViewModel() {
            var model = viewModelCreator.Create<CBCStartViewModel>();
            var identity = dataProvider.Get<Identity>(GlobalConstants.CurrentIdentity);
            if (identity.IsMTA == false) {
                model.Company = new LookupItem { ID = identity.CompanyID, Code = identity.CompanyCode, Description = identity.CompanyName };
            }
            return model;
        }
        
        public CBCDetailViewModel Search(string agencyNumber, long companyId) {
            var model = agencyBuilder.Search<CBCDetailViewModel>(agencyNumber, companyId);
            if (model == null) return null;
            if (model.IntermediaryTypeID != familyIntermediaryTypeId) {
                CurrentContext.ValidationMessages.Add(new ValidationMessage("", "CBC is applied only for family insurance agents"));
                return null;
            }
            model.StatusID = detailGenerated;
            return model;
        }

    }// class
}// namespace
