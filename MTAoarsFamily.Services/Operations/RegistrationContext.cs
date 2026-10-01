using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.DomainModels;
using MTAoarsFamily.ViewModels.Operations;

namespace MTAoarsFamily.Services.Operations {

    class RegistrationContext {

        public RegistrationViewModel Model { get; set; }

        public Agency Agency { get; set; }
    }

}
