using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.Repositories.Interfaces;
using System.Data;
using System.Data.SqlClient;
using System.Data.EntityClient;
using MTAoarsFamily.Utilities.Config;
using MTAoarsFamily.Utilities.Constants;
using MTAoarsFamily.DomainModels;
using MTAoarsFamily.Utilities.Interfaces;
using MTAoarsFamily.Utilities.Mvc;

namespace MTAoarsFamily.Repositories.Reports {
    
    public class BulkRegistrationReportRepository : IReportRepository{

        EntityContext context;
        IScopeDataProvider dataProvider;
        public BulkRegistrationReportRepository(EntityContext context, IScopeDataProvider dataProvider) {
            this.context = context;
            this.dataProvider = dataProvider;
        }

        public Dictionary<string, IEnumerable<dynamic>> GetData(Dictionary<string, string> parameters) {
            var year = int.Parse(parameters["Year"]);
            bool? isValid = null;
            if (parameters["IsValid"] != "All") isValid = bool.Parse(parameters["IsValid"]);
            var identity = dataProvider.Get<Identity>(GlobalConstants.CurrentIdentity);
            long? companyId = null;
            if(!identity.IsISMOrMTA) companyId = identity.CompanyID;
            var output = new Dictionary<string, IEnumerable<dynamic>>();
            output.Add(LookupConstants.Reports.BulkRegistration, context.GetBulkRegistrationReport(year, isValid, companyId));
            return output;
        }


        public Dictionary<string, string> GetParameters(Dictionary<string, string> parameters) {
            var output = new Dictionary<string, string>();
            output.Add("Year", parameters["Year"]);
            return output;
        }

    }// class

}// namespace
