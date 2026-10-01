using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.DomainModels;

namespace MTAoarsFamily.Repositories.Interfaces {
    public interface IRunnerRepository {

        string GetNext(string code);
    }
}
