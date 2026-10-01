using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.Validators;
using MTAoarsFamily.Validators.Shared;

namespace MTAoarsFamily.Services.Shared {
    public class ServiceContext : IContext {


        public ServiceContext() {
            ValidationMessages = new List<ValidationMessage>();
        }

        public List<ValidationMessage> ValidationMessages {
            get;
            private set;
        }

        public bool IsSuccess {
            get {
                return ValidationMessages.Count == 0;
            }
        }

    }// class
}// namespace
