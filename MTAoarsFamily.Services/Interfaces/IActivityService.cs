using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.ViewModels.Maintenance;

namespace MTAoarsFamily.Services.Interfaces {
    public interface IActivityService : IBaseService {

        void Save(AdminActivityViewModel model);

    }// interface
}// 
