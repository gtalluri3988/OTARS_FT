using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.ViewModels.Accounts;
using MTAoarsFamily.ViewModels.Shared;
using MTAoarsFamily.Utilities.Mvc;

namespace MTAoarsFamily.Services.Interfaces {
    public interface IUserService {

        bool Authenticate(LogOnViewModel model);

        Identity Get(long userId);

        Identity Get(string userName);

        long SaveLoginDetail(long userId, string hostIP);

        void SaveLogoutDetail(long userLoginId);
    }
}
