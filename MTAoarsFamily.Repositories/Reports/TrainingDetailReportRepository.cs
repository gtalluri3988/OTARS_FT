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
    
    public class TrainingDetailReportRepository : IReportRepository{

        EntityContext context;
        IScopeDataProvider dataProvider;
        public TrainingDetailReportRepository(EntityContext context, IScopeDataProvider dataProvider) {
            this.context = context;
            this.dataProvider = dataProvider;
        }

        public Dictionary<string, IEnumerable<dynamic>> GetData(Dictionary<string, string> parameters) {
            var output = new Dictionary<string, IEnumerable<dynamic>>();
            output.Add(LookupConstants.Reports.TrainingDetail, context.GetTrainingDetailReport(parameters["AgencyNumber"]));
            return output;
        }


        public Dictionary<string, string> GetParameters(Dictionary<string, string> parameters) {
            var output = new Dictionary<string, string>();
            return output;
        }

    }// class

}// namespace
