using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.ViewModels.Operations;

namespace MTAoarsFamily.Builders.Interfaces
{
    public interface IRegsitrationCreator
    {
         void Process(RegistrationViewModel model);
         RegistrationViewModel Create(List<string> parameters);
        
         Dictionary<string, string> ErrorHandler{get;set;}
    }
}
