using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MTAoarsFamily.ViewModels.Interfaces {
    
    public interface INotificationValueProvider {

        string GetValue(INotificationVariableSource source);

    }// interface

}// namespace
