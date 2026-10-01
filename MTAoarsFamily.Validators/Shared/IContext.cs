using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MTAoarsFamily.Validators.Shared {
    public interface IContext {

        List<ValidationMessage> ValidationMessages { get; }

        bool IsSuccess { get; }
            
    }
}
