using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MTAoarsFamily.Services.Interfaces;
using MTAoarsFamily.ViewModels.Shared;
using MTAoarsFamily.Utilities.Mvc;
using MTAoarsFamily.ViewModels.Notifications;
using MTAoarsFamily.Builders.Interfaces;

namespace MTAoarsFamily.Shell.Controllers {
    [MTAoarsFamilyAuthorize]
    public class MailController : BaseController {
        IMailService mailService;
        IMailBuilder mailBuilder;

        public MailController(IMailService mailService, IMailBuilder mailBuilder) {
            this.mailService = mailService;
            this.mailBuilder = mailBuilder;
        }

        public ActionResult Index() {
            return View();
        }
       
        public ActionResult Inbox() {
            return PartialView();  
        }

        public ActionResult Outbox() {
            return PartialView();
        }
        
        public ActionResult ReceivedList(GridPageViewModel input) {
            var model = mailService.GetReceivedMails(input);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SentList(GridPageViewModel input) {
            var model = mailService.GetSentMails(input);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Detail(long id) {
            var model = mailService.Read(id);
            return PartialView(model);
        }

        public ActionResult Reply(long id) {
            var model = mailService.GetReplyMail(id);
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Reply(MailReplyViewModel model) {
            if (!ModelState.IsValid) return new AjaxResponse(ModelState, "");
            mailService.Send(model);
            if (!ModelState.IsValid) return new AjaxResponse(ModelState, "");
            return new AjaxResponse(ModelState, "", Url.Action("Complete"), "");
        }

        public ActionResult Display(long id) {
            var model = mailService.Read(id);
            return View(model);
        }

        public ActionResult New() {
            var model = mailBuilder.GetNewMail();
            return PartialView(model);
        }

        [HttpPost]
        public JsonResult New(NewMailViewModel model) {
            if (!ModelState.IsValid) return new AjaxResponse(ModelState, "");
            mailService.Send(model);
            Check(mailService.CurrentContext);
            if (!ModelState.IsValid) return new AjaxResponse(ModelState, "");
            return new AjaxResponse(ModelState, "", Url.Action("Complete"), "");
        }
    }// class
}// namespace
