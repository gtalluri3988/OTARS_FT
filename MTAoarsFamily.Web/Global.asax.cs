using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using MTAoarsFamily.Services;
using MTAoarsFamily.Web.Controllers;
using MTAoarsFamily.Utilities.Constants;
using MTAoarsFamily.Utilities.Interfaces;
using MTAoarsFamily.Utilities.IoC;
using MTAoarsFamily.Utilities.Mvc;
using MTAoarsFamily.ViewModels.Shared;
using MTAoarsFamily.Builders.Interfaces;
using MTAoarsFamily.Builders.Operations;
using MTAoarsFamily.Services.Interfaces;
using FluentValidation.Mvc;
using FluentValidation;
using MTAoarsFamily.ViewModels.Operations;
using MTAoarsFamily.Validators.Operations.Fluents;
using MTAoarsFamily.Builders.Notifications;
using System.Web;

namespace MTAoarsFamily.Web {
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new NoCacheAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            //routes.RouteExistingFiles = true;
            //routes.IgnoreRoute("Upload/Registration/{file}");

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }
        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            // Track auth-related requests only, to spot redirect loops without flooding the log.
            var path = Request.Path;
            if (path.IndexOf("Account", StringComparison.OrdinalIgnoreCase) >= 0 ||
                path.IndexOf("signin", StringComparison.OrdinalIgnoreCase) >= 0 ||
                path.IndexOf("signout", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                logger.Info("Application_BeginRequest: {0} {1} from {2}", Request.HttpMethod, path, Request.UserHostAddress);
            }

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, max-age=0");
        }
        protected void Application_Start()
        {
            logger.Info("Application_Start: application starting up.");
            MvcHandler.DisableMvcResponseHeader = true;
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            ServiceLoader.Load();
            RegisterControllers();
            RegisterModelbinders();
            DependencyResolver.SetResolver(new UnityContainerResolver());

            FluentValidationModelValidatorProvider.Configure(provider => {
                provider.ValidatorFactory = new UnityValidatorFactory(ObjectContainer.Container);     
            });

            var oldProvider = FilterProviders.Providers.Single(p => p is FilterAttributeFilterProvider);
            FilterProviders.Providers.Remove(oldProvider);
            FilterProviders.Providers.Add(new UnityContainerFilterAttributeFilterProvider());

            //Response.Cache.SetCacheability(HttpCacheability.NoCache);  // HTTP 1.1.
            //Response.Cache.AppendCacheExtension("no-store, must-revalidate");
            //Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0.
            //Response.AppendHeader("Expires", "0"); // Proxies.
        }

        protected void Session_Start() {
            // The following code will be removed after login implementation
            //var dataProvider = ObjectContainer.Container.Resolve<IScopeDataProvider>();
            //var identity = (ObjectContainer.Container.Resolve<IUserService>()).Get("up_817");
            //dataProvider.Register(GlobalConstants.CurrentIdentity, identity);
        }

        void Session_End(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
        }
        void RegisterControllers()
        {
            var container = ObjectContainer.Container;

            container.RegisterType<HomeController>();
            container.RegisterType<RegistrationController>();
            container.RegisterType<LookupController>();
            container.RegisterType<TerminationController>();
            container.RegisterType<AgencyController>();
            container.RegisterType<MailController>();
            container.RegisterType<RenewalController>();
            container.RegisterType<InvoiceController>();
            container.RegisterType<AccountController>();
            container.RegisterType<MaintenanceController>();
            container.RegisterType<SetupController>();
            container.RegisterType<CPDController>();
            container.RegisterType<CBCController>();
            container.RegisterType<UploadController>();
            container.RegisterType<ComplaintController>();
            container.RegisterType<ReportController>();
            container.RegisterType<ReferredController>();
            container.RegisterType<ConflictController>();
            container.RegisterType<ALCController>();
            container.RegisterType<EnquiryController>();
            container.RegisterType<IFilterProvider, UnityContainerFilterAttributeFilterProvider>();
            container.RegisterType<IViewEngine, RazorViewEngine>();
            container.RegisterType<AdministrativeController>();
        }

        void RegisterModelbinders()
        {
            ModelBinders.Binders.DefaultBinder = new CustomModelBinder();
            ModelBinders.Binders.Add(typeof(DateTime), new DateModelBinder());
            ModelBinders.Binders.Add(typeof(DateTime?), new DateModelBinder());
            ModelBinders.Binders.Add(typeof(Decimal), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(Decimal?), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(long), new LongModelBinder());
            ModelBinders.Binders.Add(typeof(long?), new LongModelBinder());
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            if (exception != null)
                logger.Error(exception, "Application_Error: unhandled exception on {0}", Request.Path);
        }

    }// class
}// namespace