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
    public class TBEReportResultRepository:IReportRepository{

        EntityContext context;
        IScopeDataProvider dataProvider;
        
        public TBEReportResultRepository(EntityContext context,  IScopeDataProvider dataProvider) {
            this.context = context;
            this.dataProvider = dataProvider;
        }
        public Dictionary<string, IEnumerable<dynamic>> GetData(Dictionary<string, string> parameters)
        {
            DateTime? fromDate=null;
            DateTime? toDate = null;
            if(parameters.ContainsKey("FromDate")) fromDate = DateTime.Parse(parameters["FromDate"]);
           if(parameters.ContainsKey("ToDate"))  toDate = DateTime.Parse(parameters["ToDate"]);
            var type = parameters["Type"];
            var identity = dataProvider.Get<Identity>(GlobalConstants.CurrentIdentity);
           string  companyCode = null;
           if (!identity.IsISMOrMTA) companyCode = identity.CompanyCode;
            var output = new Dictionary<string, IEnumerable<dynamic>>();
            output.Add(LookupConstants.Reports.TBEResult, context.GetTBEResultReport(fromDate, toDate, companyCode,type));
            return output;
        }


        public Dictionary<string, string> GetParameters(Dictionary<string, string> parameters)
        {
            var output = new Dictionary<string, string>();         
            return output;
        }
    
    }
}
