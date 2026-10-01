using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.ViewModels.Shared;
using MTAoarsFamily.Utilities.Constants;
using MTAoarsFamily.ViewModels.Interfaces;
using Microsoft.Practices.Unity;

namespace MTAoarsFamily.ViewModels.Operations
{

    public class AdministrativeAgentSoleProprietorshipViewModel : AdministrativeAgentViewModel, IExistableAgencyBanker, IExistableAgency
    {
        [Dependency]
        public AgencyBankerViewModel AgencyBanker { get; set; }

    }// class

}// namespace
