using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MTAoarsFamily.ViewModels.Accounts;
using MTAoarsFamily.Utilities.Mvc;
using MTAoarsFamily.Services.Interfaces;
using MTAoarsFamily.Utilities.Interfaces;
using MTAoarsFamily.Utilities.Constants;
using MTAoarsFamily.ViewModels.Shared;
using MTAoarsFamily.Utilities.Config;
using System.Security.Claims;
using Microsoft.Owin;
using MTAoarsFamily.Utilities.Extensions;

namespace MTAoarsFamily.Web.Controllers
{
    public class AccountController : BaseController
    {

        IUserService userService;
        IScopeDataProvider dataProvider;
        IMenuService menuService;
        ConfigManager config;

        public AccountController(IUserService userService, IScopeDataProvider dataProvider, IMenuService menuService, ConfigManager config)
        {
            this.userService = userService;
            this.dataProvider = dataProvider;
            this.menuService = menuService;
            this.config = config;
        }
        // [Authorize]
        public ActionResult LogOn()
        {
            logger.Info("AccountController.LogOn: entered. Cookie authenticated: {0}", this.User.Identity.IsAuthenticated);

            var identity = dataProvider.Get<Identity>(GlobalConstants.CurrentIdentity);
            //if (identity == null) { return Redirect(config.SSOUrl); }
            //else { return RedirectToAction("Index", "Home"); }

            // Finding 1: this only restores the server session after OWIN has accepted
            // the cookie, which is limited by the 15-minute idle and 60-minute absolute
            // expiry checks configured in Startup. It preserves the established SSO flow
            // when ASP.NET session state is recycled.
            //if (this.User.Identity.IsAuthenticated && identity == null)
            //{
            //    var claimsIdentity = User.Identity as ClaimsIdentity;
            //    var sub = claimsIdentity == null
            //        ? null
            //        : claimsIdentity.Claims.FirstOrDefault(i => i.Type == "sub");

            //    if (sub != null && !string.IsNullOrEmpty(sub.Value))
            //    {
            //        logger.Debug("AccountController.LogOn: restoring a session from a valid OWIN cookie.");
            //        identity = userService.Get(sub.Value);
            //        if (identity != null)
            //        {
            //            dataProvider.Remove(GlobalConstants.CurrentIdentity);
            //            dataProvider.Register(GlobalConstants.CurrentIdentity, identity);
            //        }
            //    }
            //}

            if (this.User.Identity.IsAuthenticated && identity == null)
            {
                var sub = (User.Identity as ClaimsIdentity).Claims.FirstOrDefault(i => i.Type == "sub").Value;
                identity = userService.Get(sub);
                dataProvider.Register(GlobalConstants.CurrentIdentity, identity);
            }

            if (identity != null)
            {
                logger.Info("AccountController.LogOn: identity present, redirecting to Home/Index.");
                return RedirectToAction("Index", "Home");
            }

            logger.Info("AccountController.LogOn: no identity resolved, showing LogOn view.");
            //return View(new LogOnViewModel());
            return Redirect(config.SSOUrl);
        }

        [HttpPost]
        public JsonResult LogOnSubmit(LogOnViewModel model)
        {
            logger.Info("AccountController.LogOnSubmit: entered for UserName: {0}", model == null ? null : model.UserName);

            var output = GetResponse();
            if (output.Result == false)
            {
                logger.Warn("AccountController.LogOnSubmit: model validation failed for UserName: {0}", model == null ? null : model.UserName);
                return Json(output);
            }

            if (model.CaptchaKey == null || model.CaptchaKey.Equals(Session[GlobalConstants.CaptchaKey]) == false)
            {
                logger.Warn("AccountController.LogOnSubmit: captcha mismatch for UserName: {0}", model.UserName);
                output.AddError("Security key does not match");
                output.Result = false;
                return Json(output);
            }

            if (!userService.Authenticate(model))
            {
                logger.Warn("AccountController.LogOnSubmit: authentication failed for UserName: {0}", model.UserName);
                output.AddError("User name or password does not match");
                output.Result = false;
                return Json(output);
            }
            var identity = userService.Get(model.UserName);
            dataProvider.Remove(GlobalConstants.CurrentIdentity);
            dataProvider.Register(GlobalConstants.CurrentIdentity, identity);
            FormsAuthentication.SetAuthCookie(model.UserName, false);
            output.RedirectUrl = Url.Action("Index", "Home");
            logger.Info("AccountController.LogOnSubmit: login succeeded for UserName: {0}", model.UserName);
            return Json(output);
        }

        public ActionResult LogOut()
        {
            var identity = dataProvider.Get<Identity>(GlobalConstants.CurrentIdentity);
            logger.Info("AccountController.LogOut: entered. UserID: {0}", identity == null ? (object)null : identity.UserID);

            // Finding 5: invalidate every authentication/session layer before redirecting away from the app.
            HttpContext.GetOwinContext().Authentication.SignOut("Cookies");
            FormsAuthentication.SignOut();
            dataProvider.Remove(GlobalConstants.CurrentIdentity);

            if (identity != null)
                userService.SaveLogoutDetail(identity.UserLoginID);
            Session.Clear();
            Session.Abandon();
            // Finding 5: expire the browser's session identifier so it cannot be replayed after logout.
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", string.Empty)
            {
                Expires = DateTime.UtcNow.AddDays(-1),
                HttpOnly = true,
                Secure = Request.IsSecureConnection
            });
            //return View("LogOn", new LogOnViewModel());
            //return Redirect("/home.aspx");
            logger.Info("AccountController.LogOut: redirecting to SSOUrl: {0}", config.SSOUrl);
            return Redirect(config.SSOUrl);
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
