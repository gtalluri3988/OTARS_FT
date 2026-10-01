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

    public class CBCRepository : GenericRepository<CBCHeader>, ICBCRepository {
        ConfigManager config;
        IScopeDataProvider dataProvider;
        public CBCRepository(EntityContext context, ConfigManager config, IScopeDataProvider dataProvider)
            : base(context) {
                this.config = config;
                this.dataProvider = dataProvider;
        }

        public CBCHeader Get(int year, int quarter) {
            var item = Context.CBCHeaders.FirstOrDefault(p => p.Year == year && p.Quarter == quarter);
            return item;
        }

        public IEnumerable<CBCPivotDetail> GetPivot(int year, int quarter) {
            return Context.GetCBCPivot(year, quarter).ToList();
        }

        public IEnumerable<CBCDetail> GetDetails(int year, int quarter, long agencyId) {
            return Context.CBCDetails.Where(p => p.CBCHeader.Year == year && p.CBCHeader.Quarter == quarter
                && p.AgencyID == agencyId).ToList();
        }

        public IEnumerable<CBCDetail> GetDetails(int quarterValue) {
            return Context.CBCDetails.Where(p => (p.CBCHeader.Year* 4 + p.CBCHeader.Quarter) == quarterValue).ToList();

        }

    }// class

}// namespace
