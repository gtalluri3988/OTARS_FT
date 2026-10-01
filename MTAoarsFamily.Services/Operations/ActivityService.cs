using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.Services.Shared;
using MTAoarsFamily.Services.Interfaces;
using MTAoarsFamily.Validators;
using MTAoarsFamily.Repositories.Interfaces;
using AutoMapper;
using MTAoarsFamily.DomainModels;
using MTAoarsFamily.Utilities.Extensions;
using MTAoarsFamily.Utilities.Constants;
using System.Transactions;
using MTAoarsFamily.ViewModels.Notifications;
using MTAoarsFamily.ViewModels.Maintenance;

namespace MTAoarsFamily.Services.Operations {
    public class ActivityService : BaseService, IActivityService {

        IActivityRepository activityRepository;
        IRepository<ActivityDetail> activityDetailRepository;
        IRepository<Journal> journalRepository;
        IAgencyRepository agencyRepository;

        public ActivityService(IValidationProvider validationProvider, IActivityRepository activityRepository, IRepository<ActivityDetail> activityDetailRepository, IRepository<Journal> journalRepository, IAgencyRepository agencyRepository)
            : base(validationProvider) {
                this.activityRepository = activityRepository;
                this.activityDetailRepository = activityDetailRepository ;
                this.journalRepository = journalRepository;
                this.agencyRepository = agencyRepository;
        }

        public void Save(AdminActivityViewModel model) {
            using (var scope = new TransactionScope()) {
                foreach (var detail in model.Details) {
                    var activityDetail = new ActivityDetail {
                        ActivityID = model.Activity.ID, AgencyID = detail.AgencyID, CompanyID = model.Company.ID,
                        IsChargeable = detail.IsChargeable, InvoiceDate = model.InvoiceDate.Value
                    };
                    var principal = agencyRepository.Get(detail.AgencyID).AgencyPrincipals.First(p => p.AgencyNumber == detail.AgencyNumber);
                    activityDetailRepository.Save(activityDetail);
                    var journal = GetJournal(principal.ID, model.Activity.Code, String.Format("/AdminActivity/Index/{0}/{1}", model.Activity.ID, detail.AgencyID));
                    journalRepository.Save(journal);
                }
                activityDetailRepository.SaveChanges();
                journalRepository.SaveChanges();
                scope.Complete();
            }
        }
        
    }// class
}// namespace
