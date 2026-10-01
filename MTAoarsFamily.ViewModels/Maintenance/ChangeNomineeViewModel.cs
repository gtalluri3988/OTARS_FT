using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.ViewModels.Operations;
using MTAoarsFamily.ViewModels.Interfaces;
using MTAoarsFamily.Utilities.Constants;
using MTAoarsFamily.Utilities.Mvc;

namespace MTAoarsFamily.ViewModels.Maintenance {
    public class ChangeNomineeViewModel: IExistableTbeDetails {

        public long PrincipalID { get; set; }

        public RegistrationViewModel RegistrationModel { get; set; }

        public string ICNumber { 
            get { 
                var code = RegistrationModel.CorporateNominee.ICType.Code;
                if(code == LookupConstants.ICTypes.NewIc)return RegistrationModel.CorporateNominee.NewICNumber;
                return RegistrationModel.CorporateNominee.PassportNumber;
            }
        }

        public bool IsFamily => true;

        public bool IsGeneral => false;

        public LookupItem TbeCategory {
            get {
                return RegistrationModel.CorporateNominee.TbeCategory;
            }
        }
        public bool IsOld { get; set; }

    }// class
}// namespace
