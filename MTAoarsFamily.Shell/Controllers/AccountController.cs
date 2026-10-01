using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MTAoarsFamily.ViewModels.Accounts;
using MTAoarsFamily.Utilities.Mvc;
using MTAoarsFamily.Services.Interfaces;
using MTAoarsFamily.Utilities.Interfaces;
using MTAoarsFamily.Utilities.Constants;
using MTAoarsFamily.ViewModels.Shared;


namespace MTAoarsFamily.Shell.Controllers
{
    public class AccountController : Controller
    {

        IUserService userService;
        IScopeDataProvider dataProvider;
        public AccountController(IUserService userService, IScopeDataProvider dataProvider)
        {
            this.userService = userService;
            this.dataProvider = dataProvider;
        }

        public ActionResult LogOn()
        {
            return View(new LogOnViewModel());
        }

        [HttpPost]
        public JsonResult LogOn(LogOnViewModel model)
        {
            if (!ModelState.IsValid) return new AjaxResponse(ModelState, "");
            //if (model.CaptchaKey == null || model.CaptchaKey.Equals(Session[GlobalConstants.CaptchaKey]) == false) {
            //    return new AjaxResponse(ModelState, "Security key does not match");
            //}
            if (!userService.Authenticate(model))
            {
                return new AjaxResponse(ModelState, "User name or password does not match");
            }
            var identity = userService.Get(model.UserName);
            dataProvider.Register(GlobalConstants.CurrentIdentity, identity);
            return new AjaxResponse(ModelState, "", Url.Action("Index", "Home"), "");
        }

        public ActionResult LogOut()
        {
            dataProvider.Register<Identity>(GlobalConstants.CurrentIdentity, null);
            return View("LogOn", new LogOnViewModel());
        }

        public ActionResult Unauthorized()
        {
            return View();
        }

        public ActionResult CaptchaImage()
        {
            var imageBuilder = new XCaptcha.ImageBuilder();
            var random = new XCaptcha.RandomTextGenerator();
            var result = imageBuilder.Create(random.Create(5, true));
            Session.Add(GlobalConstants.CaptchaKey, result.Solution);
            return new FileContentResult(result.Image, result.ContentType);

        }
    }// class
}// namespace
