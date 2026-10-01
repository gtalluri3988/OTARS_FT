using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MTAoarsFamily.Utilities.IoC;
using MTAoarsFamily.ViewModels.Operations;
using MTAoarsFamily.Builders.Interfaces;
using MTAoarsFamily.Utilities.Mvc;
using MTAoarsFamily.Services.Interfaces;
using MTAoarsFamily.Utilities.Constants;
using MTAoarsFamily.Utilities.Interfaces;
using MTAoarsFamily.ViewModels.Masters;

namespace MTAoarsFamily.Web.Controllers {

    [MTAoarsFamilyAuthorize]
    public class ComplaintController : BaseController {
        
        IAgencyBuilder agencyBuilder;
        IScopeDataProvider dataProvider;

        public ComplaintController(IAgencyBuilder agencyBuilder, IScopeDataProvider dataProvider) {
            this.agencyBuilder = agencyBuilder;
            this.dataProvider = dataProvider;
        }

        public ActionResult List() {
            return View(new ComplaintViewModel());
        }

        public ActionResult Create() {
            return PartialView();
        }

        public ActionResult Edit() {
            return PartialView();
        }

        public ActionResult Display() {
            return PartialView();
        }

        public ActionResult Search(string agencyNumber) {
            var identity = dataProvider.Get<Identity>(GlobalConstants.CurrentIdentity);
            var item = agencyBuilder.Search<AgencySearchResponseViewModel>(agencyNumber, identity.CompanyID);
            var output = GetResponse(agencyBuilder.CurrentContext);
            output.Data = item;
            return Json(output);
        }

        public ActionResult Save(ComplaintViewModel model) {
            return View();
        }

        public ActionResult ValidateComplaint(ComplaintViewModel model) {
            return Json(GetResponse());
        }
    }// class
}// namespace
