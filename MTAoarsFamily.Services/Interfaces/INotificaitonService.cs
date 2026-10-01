using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.ViewModels.Interfaces;

namespace MTAoarsFamily.Services.Interfaces {
    public interface INotificaitonService {

        bool Notify(long fromCompanyId, long toCompanyId, string notificationCode, INotificationVariableSource variableSource);

    }// interface
}// class
