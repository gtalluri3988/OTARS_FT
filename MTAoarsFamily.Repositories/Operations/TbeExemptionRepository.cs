using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.Repositories.Interfaces;
using MTAoarsFamily.Repositories.Shared;
using MTAoarsFamily.DomainModels;
using MTAoarsFamily.Utilities.Config;

namespace MTAoarsFamily.Repositories.Operations {

    public class TbeExemptionRepository : GenericRepository<TbeExemption>, ITbeExemptionRepository {

        public TbeExemptionRepository(EntityContext context)
            : base(context) {
              
        }

        public IEnumerable<TbeExemption> Search(string icNumber) {
            return Context.TbeExemptions.Where(p => p.ICNumber == icNumber).AsEnumerable();
        }


    }// class

}// namespace
