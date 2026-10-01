using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.ViewModels.Masters;

namespace MTAoarsFamily.Services.Interfaces {
    public interface ICPDService : IBaseService {
        void Process();
        bool Check(TrainingListViewModel model);
    }// interface
}// 
