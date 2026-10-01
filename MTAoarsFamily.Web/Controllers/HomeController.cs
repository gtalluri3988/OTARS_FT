using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MTAoarsFamily.Services.Interfaces;
using MTAoarsFamily.Utilities.Mvc;
using MTAoarsFamily.Utilities.Interfaces;
using MTAoarsFamily.Utilities.IoC;
using MTAoarsFamily.Utilities.Constants;
using Microsoft.Practices.Unity;
using MTAoarsFamily.Utilities;
using MTAoarsFamily.Utilities.Config;
using System.Security.Claims;

namespace MTAoarsFamily.Web.Controllers
{

    public class HomeController : BaseController
    {

        IHomeService homeService;
        IUserService userService;
        ConfigManager config;

        public HomeController(IHomeService homeService, IUserService userService, ConfigManager config)
        {
            this.homeService = homeService;
            this.userService = userService;
            this.config = config;
        }

        public ActionResult Authorize(string userName)
        {
            var dataProvider = ObjectContainer.Container.Resolve<IScopeDataProvider>();
            var identity = userService.Get(userName);
            identity.UserLoginID = userService.SaveLoginDetail(identity.UserID, Request.UserHostAddress);
            dataProvider.Register(GlobalConstants.CurrentIdentity, identity);
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Index()
        {
            var user = User as ClaimsPrincipal;
            var sub = user == null
                ? null
                : user.Claims.Where(c => c.Type.Equals("sub"))
                    .Select(c => c.Value)
                    .SingleOrDefault();

            logger.Info("HomeController.Index: entered. CurrentUser.IsLogin: {0}, Cookie authenticated: {1}, sub present: {2}",
                base.CurrentUser.IsLogin, user != null && user.Identity.IsAuthenticated, !string.IsNullOrEmpty(sub));

            // Restore the application identity when the authentication cookie is
            // valid but CurrentIdentity is not present in the session.
            if (!base.CurrentUser.IsLogin && user != null &&
                user.Identity.IsAuthenticated && !string.IsNullOrEmpty(sub))
            {
                logger.Debug("HomeController.Index: session identity missing, attempting restore from sub claim.");
                var identity = userService.Get(sub);
                if (identity != null)
                {
                    logger.Info("HomeController.Index: identity restored from sub claim. UserID: {0}", identity.UserID);
                    var dataProvider = ObjectContainer.Container.Resolve<IScopeDataProvider>();
                    dataProvider.Remove(GlobalConstants.CurrentIdentity);
                    dataProvider.Register(GlobalConstants.CurrentIdentity, identity);
                }
                else
                {
                    logger.Warn("HomeController.Index: authenticated cookie found but no matching local user for sub claim. Possible environment/user-mapping issue.");
                }
            }

            if (!base.CurrentUser.IsLogin)
            {
                // If this fires repeatedly for the same client it indicates a redirect loop with the SSO provider.
                logger.Warn("HomeController.Index: user still not logged in, redirecting to SSOUrl: {0}", config.SSOUrl);
                return Redirect(config.SSOUrl);
            }

            var model = homeService.GetModel();
            return View(model);
        }

        public ActionResult Print()
        {
            return View();
        }

        public ActionResult Error(string responseCode)
        {
            logger.Info("Error Page...");
            TempData["ResponseCode"] = "";
            if (!string.IsNullOrEmpty(responseCode))
                TempData["ResponseCode"] = responseCode;
            if (base.CurrentUser.IsLogin)
                return View("Error", "~/Views/Shared/_Layout.cshtml", null);
            return View();
        }
    }// class
}
