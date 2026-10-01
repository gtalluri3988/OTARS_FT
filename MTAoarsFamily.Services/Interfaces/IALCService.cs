using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.Repositories.Interfaces;
using MTAoarsFamily.ViewModels.Operations;

namespace MTAoarsFamily.Services.Interfaces {
    public interface IALCService : IBaseService {
        void Save(ALCHeaderViewModel input);

    }// interface
}// 
