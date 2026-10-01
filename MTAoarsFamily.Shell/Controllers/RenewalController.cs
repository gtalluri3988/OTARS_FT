using System.Web.Mvc;
using AutoMapper;
using MTAoarsFamily.Builders.Interfaces;
using MTAoarsFamily.Services.Interfaces;
using MTAoarsFamily.Utilities.Constants;
using MTAoarsFamily.Utilities.Mvc;
using MTAoarsFamily.ViewModels.Operations;
using MTAoarsFamily.ViewModels.Shared;
using System.Collections.Generic;

namespace MTAoarsFamily.Shell.Controllers {
    [MTAoarsFamilyAuthorize]
    public class RenewalController : BaseController {
        IRenewalService renewalService;
        IRenewalBuilder renewalBuilder;
        ILookupService lookupService;

        public RenewalController(IRenewalService renewalService, IRenewalBuilder renewalBuilder, ILookupService lookupService) {
            this.renewalService = renewalService;
            this.renewalBuilder = renewalBuilder;
            this.lookupService = lookupService;
        }

        public ActionResult List() {
            var model = renewalBuilder.GetRenewalHeaderList();
            return View(model);
        }

        public ActionResult Details(long id) {
            var model = renewalBuilder.GetRenewlHeader(id);
            return View(model);
        }

        [HttpPost]
        public JsonResult PagedDetails(GridPageViewModel input, long? headerId) {
            var model = renewalService.GetRenwalDetails(input, headerId.Value, false);
            return Json(model);
        }

       /* [HttpPost]
        public JsonResult SaveDetails(RenewalHeaderViewModel model) {
            if (!ModelState.IsValid) return new AjaxResponse(ModelState, "");
            renewalService.Save(model);
            Check(renewalService.CurrentContext);
            if (!ModelState.IsValid) return new AjaxResponse(ModelState, "");
            return new AjaxResponse(ModelState, "", Url.Action("Complete"),"");
        }

        [HttpPost]
        public JsonResult SubmitDetails(RenewalHeaderViewModel model) {
            if (!ModelState.IsValid) return new AjaxResponse(ModelState, "");
            renewalService.Submit(model);
            Check(renewalService.CurrentContext);
            if (!ModelState.IsValid) return new AjaxResponse(ModelState, "");
            return new AjaxResponse(ModelState, "", Url.Action("Complete"), "");
        }*/

        public ActionResult Process() {
            renewalService.Process();
            return RedirectToAction("Complete");
        }

        public ActionResult Complete() {
            return View();
        }

        [HttpPost]
        public JsonResult SaveDetails(long id, List<RenewalDetailViewModel> items) {
            var header = new RenewalHeaderViewModel { ID = id, Details = items };
            renewalService.Save(header);
            return Json(new{ Status="Renewal detail Saved Successfully"});
        }

        [HttpPost]
        public JsonResult SubmitDetails(long id, List<RenewalDetailViewModel> items) {
            var header = new RenewalHeaderViewModel { ID = id, Details = items };
            renewalService.Submit(header);
            return Json(new { Status = "Renewal detail Submitted Successfully" });
        }

    }// class
}// namespace
