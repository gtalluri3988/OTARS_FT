using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.ViewModels.Operations;
using MTAoarsFamily.ViewModels.Shared;

namespace MTAoarsFamily.ViewModels.Maintenance
{
   public  class MaintenanceViewModel
    {
       
       public string Type { get; set; }       
       public string ActionName { get; set; }
       public string ViewName { get; set; }
       public RegistrationViewModel Registration { get; set; }

    }


   

  
}
