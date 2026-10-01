using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.Repositories.Interfaces;
using MTAoarsFamily.Repositories.Shared;
using MTAoarsFamily.DomainModels;
using MTAoarsFamily.Utilities.Config;
using MTAoarsFamily.Utilities.Constants;

namespace MTAoarsFamily.Repositories.Operations {

    public class UploadRepository : GenericRepository<UploadHistory>, IUploadRepository {
        ConfigManager config;
        public UploadRepository(EntityContext context, ConfigManager config)
            : base(context) {
                this.config = config;
        }

        public string GetAgencyNumber(long agencyId) {
            return Context.AgencyPrincipals.Where(p => p.AgencyID == agencyId).OrderByDescending(p => p.CreatedDate)
                .Select(p => p.AgencyNumber).FirstOrDefault();
        }

    }// class

}// namespace
