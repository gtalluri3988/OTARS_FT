using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.ViewModels.Shared;

namespace MTAoarsFamily.ViewModels.Interfaces {
    public interface IExistableAddressAgencyBoardMembers : IExistableAgency, IExistableBoardMembers{

        AddressViewModel Address { get; set; }
    }
}
