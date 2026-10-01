using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.DomainModels;

namespace MTAoarsFamily.Repositories.Interfaces {
    
    public interface IMiiResultRepository : IRepository<MiiResult> {

        IEnumerable<MiiResult> Search(string icNumber);

        MiiResult Search(int id);

        void Update(MiiResult source,out string message);
    }// interface

}// namespace
