using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MTAoarsFamily.ViewModels.Masters;
using MTAoarsFamily.Services.Interfaces;
using MTAoarsFamily.Builders.Interfaces;
using MTAoarsFamily.ViewModels.Operations;
using MTAoarsFamily.Utilities.Constants;
using MTAoarsFamily.Utilities.Interfaces;
using MTAoarsFamily.Utilities.Mvc;


namespace MTAoarsFamily.Web.Controllers {
    [MTAoarsFamilyAuthorize]
    public class ALCController : BaseController {
       IALCService alcService;

       public ALCController(IALCService alcService) {
           this.alcService = alcService;
        }

        public ActionResult List() {
            var model = new ALCHeaderViewModel();
            return View(model);
        }

        public ActionResult ValidateALC(ALCHeaderViewModel header) {
            var output = GetResponse();
            if (output.Result == false) return Json(output);
            output = GetResponse(alcService.CurrentContext);
            return Json(output);
        }

        public ActionResult Save(ALCHeaderViewModel header) {
            var output = GetResponse();
            if (output.Result == false) return Json(output);
            alcService.Save(header);
            output = GetResponse(alcService.CurrentContext);
            return Json(output);
        }

       /* public ActionResult Search(string agencyNumber) {
            var identity = dataProvider.Get<Identity>(GlobalConstants.CurrentIdentity);
            var item = agencyBuilder.Search<AgencySearchResponseViewModel>(agencyNumber, identity.CompanyID);
            var output = GetResponse(agencyBuilder.CurrentContext);
            output.Data = item;
            return Json(output);
        }

        public JsonResult ValidateALCDetail(ALCHeaderViewModel model) {
            var output = GetResponse();
            if (output.Result == false) return Json(output);
            var listModel = new TrainingListViewModel { TrainingModel = model, Year = Year };
            cpdService.Check(listModel);
            output = GetResponse(cpdService.CurrentContext);
            return Json(output);
        }*/
    }// class
}// namespace
