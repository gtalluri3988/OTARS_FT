using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.DomainModels;
using MTAoarsFamily.Repositories.Interfaces;
using MTAoarsFamily.Utilities.Constants;
using MTAoarsFamily.Utilities.Interfaces;
using MTAoarsFamily.Utilities.Mvc;

namespace MTAoarsFamily.Repositories.Reports
{
    public class TBESpecialAgentsReportResultRepository:IReportRepository{

        EntityContext context;
        IScopeDataProvider dataProvider;

        public TBESpecialAgentsReportResultRepository(EntityContext context, IScopeDataProvider dataProvider)
        {
            this.context = context;
            this.dataProvider = dataProvider;
        }
        public Dictionary<string, IEnumerable<dynamic>> GetData(Dictionary<string, string> parameters)
        {
            var company = parameters["Company"];
            var icno = parameters["ICNumber"];
            var output = new Dictionary<string, IEnumerable<dynamic>>();
            output.Add(LookupConstants.Reports.TBESpecialAgents, context.GetTBESpecialExemptedReport(company,icno));
            return output;
        }


        public Dictionary<string, string> GetParameters(Dictionary<string, string> parameters)
        {
            var output = new Dictionary<string, string>();         
            return output;
        }
    
    }
}
