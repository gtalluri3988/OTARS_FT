using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.DomainModels;

namespace MTAoarsFamily.Repositories.Interfaces {

    public interface ICompanyRepository : IRepository<Company> {

        Company Get(string code);

        CompanyAbbreviation GetAbbreviations(long companyID);

    }// interface

}// class
