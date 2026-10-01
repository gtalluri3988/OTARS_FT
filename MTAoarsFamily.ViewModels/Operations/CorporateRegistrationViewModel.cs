using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.ViewModels.Shared;
using MTAoarsFamily.Utilities.Constants;
using Microsoft.Practices.Unity;
using MTAoarsFamily.Utilities.Attributes;
using MTAoarsFamily.Utilities.Mvc;
using MTAoarsFamily.ViewModels.Interfaces;

namespace MTAoarsFamily.ViewModels.Operations {

    public class CorporateRegistrationViewModel : RegistrationViewModel, IExistableAgencyBanker, IExistableBoardMembers, IExistableAgency, IExistableAddressAgencyBoardMembers {


        public CorporateRegistrationViewModel() {
            Directors = new MemberList<DirectorViewModel>(this);
            Shareholders = new MemberList<ShareholderViewModel>(this);
            AdditionalCorporateNominees = new MemberList<AdditionalCorporateNomineeViewModel>(this);
        }
        
        [Binder(typeof(JsonModelBinder))]
        public List<DirectorViewModel> Directors { get; set; }

        
        [Binder(typeof(JsonModelBinder))]
        public List<ShareholderViewModel> Shareholders { get; set; }

        public List<AdditionalCorporateNomineeViewModel> AdditionalCorporateNominees { get; set; }

        [Dependency]
        public AgencyBankerViewModel AgencyBanker { get; set; }
              
    }// class

}// namespace
