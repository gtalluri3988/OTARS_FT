using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MTAoarsFamily.ViewModels.Reports;
using MTAoarsFamily.Utilities.Constants;
using MTAoarsFamily.Services.Interfaces;
using MTAoarsFamily.Utilities.Interfaces;
using MTAoarsFamily.Utilities.Mvc;

namespace MTAoarsFamily.Web.Controllers
{
    public class ReportController : Controller
    {
        ILookupService lookupService;
        IScopeDataProvider dataProvider;

        public ReportController(ILookupService lookupService, IScopeDataProvider dataProvider)
        {
            this.lookupService = lookupService;
            this.dataProvider = dataProvider;
        }

        public ActionResult Invoice()
        {
            var model = new InvoiceReportViewModel();
            return View(model);
        }

        public ActionResult InvoiceDetail()
        {
            var model = new InvoiceDetailReportViewModel();
            model.ReportFor = LookupConstants.Reports.InvoiceDetail;
            return View(model);
        }
        public ActionResult Ascii()
        {
            var model = new AsciiReportViewModel() { FromDate = DateTime.Now.AddMonths(-1), ToDate = DateTime.Now };
            return View(model);
        }

        public ActionResult RenewalAscii()
        {
            var model = new RenewalAsciiReportViewModel() { FromDate = DateTime.Now.AddMonths(-1), ToDate = DateTime.Now };
            return View(model);
        }

        public ActionResult UserLogin()
        {
            var model = new UserLoginReportViewModel() { FromDate = DateTime.Now.AddMonths(-1), ToDate = DateTime.Now };
            return View(model);
        }

        public ActionResult FamilyNewRegistration()
        {
            var model = new NewRegistrationStatisticsReportViewModel()
            {
                IntermediaryTypeCode = LookupConstants.IntermediaryType.Family,
                Caption = "Family"
            };
            return View("NewRegistrationStatistics", model);
        }

        public ActionResult FamilyRenewal()
        {
            var model = new RenewalStatisticsReportViewModel()
            {
                IntermediaryTypeCode = LookupConstants.IntermediaryType.Family,
                Caption = "Family"
            };
            return View("RenewalStatistics", model);
        }

        public ActionResult FamilyActiveAgents()
        {
            var model = new AgentsStatisticsReportViewModel()
            {
                IntermediaryTypeCode = LookupConstants.IntermediaryType.Family,
                Caption = "Family",
                Date = DateTime.Now
            };
            return View("ActiveAgentsStatistics", model);
        }

        public ActionResult FamilyTerminatedAgents()
        {
            var model = new AgentsStatisticsReportViewModel()
            {
                IntermediaryTypeCode = LookupConstants.IntermediaryType.Family,
                Caption = "Family",
                Date = DateTime.Now
            };
            return View("TerminatedAgentsStatistics", model);
        }

        public ActionResult CPD()
        {
            var model = new CPDReportViewModel();
            return View(model);
        }

        public ActionResult BulkRegistration()
        {
            var model = new BulkRegistrationReportViewModel();
            return View(model);
        }

        public ActionResult Summary()
        {
            var model = new SummaryReportViewModel();
            return View(model);
        }

        public ActionResult Agents()
        {
            var model = new AgentsReportViewModel();
            return View(model);
        }

        public ActionResult ALC()
        {
            var model = new AgentsReportViewModel();
            return View(model);
        }

        public ActionResult AutoTermination()
        {
            var model = new TerminationListReportViewModel();
            model.ActionID = lookupService.GetTerminationActions().First(a => a.Code == LookupConstants.TerminationAction.Terminate).ID;
            model.ReportFor = LookupConstants.Reports.TerminationList;
            return View("AutoTermination", model);
        }

        public ActionResult TerminationList()
        {
            var model = new TerminationListReportViewModel();
            model.ActionID = lookupService.GetTerminationActions().First(a => a.Code == LookupConstants.TerminationAction.Terminate).ID;
            model.ReportFor = LookupConstants.Reports.TerminationList;
            return View("TerminationListReport", model);
        }

        public ActionResult TerminationSummary()
        {
            var model = new TerminationSummaryReportViewModel();
            model.ReportFor = LookupConstants.Reports.TerminationSummary;
            return View("TerminationSummaryReport", model);
        }

        public ActionResult Consolidate()
        {
            var model = new ConsolidateReportViewModel();
            var identity = dataProvider.Get<Identity>(GlobalConstants.CurrentIdentity);
            model.IsMTA = identity.IsISMOrMTA;
            model.StatusCodes = lookupService.GetTerminationActions().Union(lookupService.GetRenewalDetailStatuses().ToList().Where(s => s.Code == LookupConstants.RenewalDetailStatus.Renew)).ToList();
            return View("ConsolidateReport", model);
        }

        public ActionResult TBEResult()
        {
            var model = new TBEResultsReportViewModel();
            return View(model);
        }

        public ActionResult TrainingDetail()
        {
            var model = new TrainingDetailReportViewModel();
            return View(model);
        }

        public ActionResult ReferredListing()
        {
            var model = new ReferredListingReportViewModel() { AgentType = 1 };
            return View(model);
        }

        public ActionResult ReferredListingAdmin()
        {
            var model = new ReferredListingReportViewModel() { AgentType = 2 };
            return View(model);
        }

        public ActionResult TBESpecialAgents()
        {
            var model = new TBESpecialAgentsReportViewModel();
            return View(model);
        }

        public ActionResult AdministrativeAuditTrail()
        {
            var model = new AdministrativeAuditTrailReportViewModel() { FromDate = DateTime.Now.AddMonths(-1), ToDate = DateTime.Now };
            return View(model);
        }

        public ActionResult PhotoUploadSummary()
        {
            var model = new PhotoUploadSummaryReportViewModel() { FromDate = DateTime.Now.AddMonths(-1), ToDate = DateTime.Now };
            return View(model);
        }

        public ActionResult TBEExamination()
        {
            var model = new TBEExaminationViewModel() { FromDate = DateTime.Now.AddMonths(-1), ToDate = DateTime.Now };
            return View(model);
        }

        public ActionResult ResignedActiveAgents()
        {
            var now = DateTime.Now;
            var model = new ResignedActiveAgentsReportViewModel() { Month = now.Month, Year = now.Year };
            return View(model);
        }
        public ActionResult TBEExemption()
        {
            var model = new TBEExemptionViewModel() { FromDate = DateTime.Now.AddMonths(-1), ToDate = DateTime.Now };
            return View(model);
        }

    }// class
}// namespace
