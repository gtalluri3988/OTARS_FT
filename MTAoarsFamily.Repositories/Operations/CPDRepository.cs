using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.Repositories.Interfaces;
using MTAoarsFamily.Repositories.Shared;
using MTAoarsFamily.DomainModels;
using MTAoarsFamily.Utilities.Config;
using MTAoarsFamily.Utilities.Constants;
using MTAoarsFamily.Utilities.Interfaces;
using MTAoarsFamily.Utilities.Mvc;

namespace MTAoarsFamily.Repositories.Operations {

    public class CPDRepository : GenericRepository<CPDDetail>, ICPDRepository {
        ConfigManager config;
        IScopeDataProvider dataProvider;
        public CPDRepository(EntityContext context, ConfigManager config, IScopeDataProvider dataProvider)
            : base(context) {
                this.config = config;
                this.dataProvider = dataProvider;
        }

        public bool IsProcessed(int year) {
            return Context.CPDDetails.Count(p => p.Year == year) > 0;
        }

        public void Process(int year) {
            var identity = dataProvider.Get<Identity>(GlobalConstants.CurrentIdentity);
            Context.ProcessCPD(year, identity.UserID);
        }

        public IEnumerable<CPDDetail> Get(int year, string statusCode) {
            return Context.CPDDetails.Where(p => p.Year == year && p.LookupCPDStatus.Code == statusCode).ToList();
        }

    }// class

}// namespace
