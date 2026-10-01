using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.Builders.Interfaces;
using MTAoarsFamily.ViewModels.Operations;
using MTAoarsFamily.Repositories.Interfaces;
using MTAoarsFamily.Validators.Operations.Fluents;
using Microsoft.Practices.Unity;
namespace MTAoarsFamily.Builders.Operations
{
    public class IndividualRegistrationCreator : RegistrationCreator, IRegsitrationCreator
    {
        IUnityContainer container;

        public IndividualRegistrationCreator(ILookupRepository lookupRepository, IUnityContainer container)
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
            var model = new IndividualRegistrationViewModel();       
            
            
            Process(model);
           
            return model;
        }

       
    }
}
