using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MTAoarsFamily.Utilities.IoC;
using MTAoarsFamily.ViewModels.Operations;
using MTAoarsFamily.Builders.Interfaces;
using MTAoarsFamily.Services.Interfaces;
using MTAoarsFamily.Utilities.Mvc;
using MTAoarsFamily.Utilities.Interfaces;
using MTAoarsFamily.ViewModels.Shared;
using MTAoarsFamily.Utilities.Constants;

namespace MTAoarsFamily.Shell.Controllers {
    [MTAoarsFamilyAuthorize]
    public class TerminationController : BaseController {

        IObjectCreator objectCreator;
        IAgencyBuilder agencyBuilder;
        IAgencyService agencyService;
        ILookupService lookupService;
        ITerminationService terminationService;

        public TerminationController(IObjectCreator objectCreator, IAgencyBuilder agencyBuilder, IAgencyService agencyService, ITerminationService terminationService, ILookupService lookupService) {
            this.objectCreator = objectCreator;
            this.agencyBuilder = agencyBuilder;
            this.agencyService = agencyService;
            this.terminationService = terminationService;
            this.lookupService = lookupService;
        }

        public ActionResult Search() {
            var model = objectCreator.Create<TerminationSearchViewModel>();
            model.TerminationActions = lookupService.GetTerminationActions();
            return View(model);
        }

        [HttpPost]
        public ActionResult List(TerminationSearchViewModel request) {
            var model = agencyBuilder.Search<TerminationSearchResponseViewModel>(request.AgencySearch);
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult AjaxList(TerminationSearchViewModel request) {
            var model = agencyBuilder.Search<TerminationSearchResponseViewModel>(request.AgencySearch.AgencyNumber);
            var status = "";
            if (model == null) {
                status = "The agent is not available";
            } else {
                var identity = objectCreator.Create<IScopeDataProvider>().Get<Identity>(GlobalConstants.CurrentIdentity);
                if (identity.IsMTA == false) {
                    if (model.CompanyID != identity.CompanyID) {
                        status = "This agent does not belong to your company";
                        model = null;
                    }
                }
            }
            return Json(new { Data = model, Status = status }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Process(TerminationSearchViewModel request) {
            terminationService.Process(request.TerminationAgents);
            return RedirectToAction("Complete");
        }

        public ActionResult Details(long id) {
            var model = objectCreator.Create<TerminationDetailViewModel>();
            model.Agency = agencyBuilder.GetAgencyByPrincipal(id);
            model.AgencyPrincipalID = id;
            return View(model);
        }

        [HttpPost]
        public JsonResult Terminate(long id) {
            agencyService.Terminate(id, DateTime.Now, false);
            return new AjaxResponse(ModelState, "", Url.Action("Complete"),id.ToString());
        }

        public ActionResult Complete() {
            return View();
        }
        
    }// class
}// namespace
