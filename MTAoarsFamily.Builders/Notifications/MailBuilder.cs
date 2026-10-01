using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.Builders.Interfaces;
using MTAoarsFamily.ViewModels.Notifications;
using MTAoarsFamily.Repositories.Interfaces;
using MTAoarsFamily.DomainModels;
using MTAoarsFamily.Utilities.Interfaces;
using MTAoarsFamily.ViewModels.Shared;
using MTAoarsFamily.Utilities.Constants;

namespace MTAoarsFamily.Builders.Notifications {
    public class MailBuilder : IMailBuilder {

        ICompanyRepository companyRepository;
        IScopeDataProvider dataProvider;

        public MailBuilder(ICompanyRepository companyRepository, IScopeDataProvider dataProvider) {
            this.companyRepository = companyRepository;
            this.dataProvider = dataProvider;
        }

        public NewMailViewModel GetNewMail() {
            var model = new NewMailViewModel();
            model.Companies = companyRepository.GetAll().ToLookupItem();
            return model;
        }


      
    }// class
}// namespace
