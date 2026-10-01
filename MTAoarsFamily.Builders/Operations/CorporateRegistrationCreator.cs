using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.Repositories.Interfaces;
using MTAoarsFamily.Builders.Interfaces;
using MTAoarsFamily.ViewModels.Operations;
using Microsoft.Practices.Unity;
using MTAoarsFamily.Validators.Operations.Fluents;

namespace MTAoarsFamily.Builders.Operations
{
    public class CorporateRegistrationCreator : RegistrationCreator, IRegsitrationCreator
    {

        IUnityContainer container;
        public CorporateRegistrationCreator(ILookupRepository lookupRepository, IUnityContainer container)
            : base(lookupRepository,container)
        {
            this.container = container;
        }


        public override void Process(RegistrationViewModel model)
        {
            base.Process(model);
        }

        public RegistrationViewModel Create(List<string> parameters)
        {
            base.Parameters = parameters;
            var model = new CorporateRegistrationViewModel();
            Process(model);
          
            return model;
        }
      
    }
}