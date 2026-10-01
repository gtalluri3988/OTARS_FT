using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MTAoarsFamily.ViewModels.Operations
{
    public class RegistrationResponseViewModel
    {
        public RegistrationViewModel Model { get; set; }
        public Dictionary<string,string> Errors { get; set; }
    }

}
