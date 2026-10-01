using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.ViewModels.Notifications;

namespace MTAoarsFamily.ViewModels.Interfaces {
    public interface IExistableTerminationList {

        List<TerminatedVariableSource> TerminationList { get; }

    }// interface
}// namespae
