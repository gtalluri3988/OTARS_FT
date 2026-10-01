using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.Services.Shared;

namespace MTAoarsFamily.Services.Interfaces {
    public interface IBaseService {

        ServiceContext CurrentContext { get; }
    }
}
