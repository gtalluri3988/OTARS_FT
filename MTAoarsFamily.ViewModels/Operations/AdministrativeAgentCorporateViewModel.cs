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

namespace MTAoarsFamily.ViewModels.Operations
{

    public class AdministrativeAgentCorporateViewModel : AdministrativeAgentViewModel, IExistableAgencyBanker, IExistableBoardMembers, IExistableAgency, IExistableAddressAgencyBoardMembers
    {
        public AdministrativeAgentCorporateViewModel()
        {
            Directors = new List<DirectorViewModel>();
            Shareholders = new List<ShareholderViewModel>();
            AdditionalCorporateNominees = new List<AdditionalCorporateNomineeViewModel>();
        }

        [Binder(typeof(JsonModelBinder))]
        public List<DirectorViewModel> Directors { get; set; }


        [Binder(typeof(JsonModelBinder))]
        public List<ShareholderViewModel> Shareholders { get; set; }

        public List<AdditionalCorporateNomineeViewModel> AdditionalCorporateNominees { get; set; }

        [Dependency]
        public AgencyBankerViewModel AgencyBanker { get; set; }

    }// class
}
