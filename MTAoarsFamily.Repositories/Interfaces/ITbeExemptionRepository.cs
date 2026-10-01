using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.DomainModels;

namespace MTAoarsFamily.Repositories.Interfaces {
    
    public interface ITbeExemptionRepository : IRepository<TbeExemption> {

        IEnumerable<TbeExemption> Search(string icNumber);

    }// interface

}// namespace
