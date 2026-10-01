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
    public class AdministrativeAgentPartnershipViewModel : AdministrativeAgentViewModel, IExistableAgencyBanker, IExistableAgency
    {

        public AdministrativeAgentPartnershipViewModel()
        {
            this.Partners = new List<PartnerViewModel>();
        }

        [Binder(typeof(JsonModelBinder))]
        public List<PartnerViewModel> Partners { get; set; }

        [Dependency]
        public AgencyBankerViewModel AgencyBanker { get; set; }

    }// class
}
