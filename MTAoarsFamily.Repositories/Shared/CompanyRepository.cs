using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.Repositories.Interfaces;
using MTAoarsFamily.DomainModels;

namespace MTAoarsFamily.Repositories.Shared {
    
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository {

        public CompanyRepository(EntityContext context)
            : base(context) {
        }


        public Company Get(string code) {
            return Context.Companies.FirstOrDefault(p => p.Code == code && p.IsActive);
        }

        public CompanyAbbreviation GetAbbreviations(long companyID)
        {
            return Context.CompanyAbbreviations.FirstOrDefault(x => x.CompanyID == companyID);
        }

    }// class

}// namespace
