using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using MTAoarsFamily.ViewModels.Interfaces;

namespace MTAoarsFamily.ViewModels.Operations {

    public class RegistrationReinstationViewModel : IExistableIntermediary {

        public AgencyDisplayViewModel Agency { get; set; }

        public long AgencyID { get; set; }

        public long CompanyID { get; set; }

        public bool IsGeneral { get; set; }

        public bool IsFamily { get; set; }


    }// class

}// namespace
