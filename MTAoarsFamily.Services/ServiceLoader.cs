using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using MTAoarsFamily.DomainModels;
using MTAoarsFamily.Mappers;
using MTAoarsFamily.Repositories.Interfaces;
using MTAoarsFamily.Repositories.Operations;
using MTAoarsFamily.Repositories.Shared;
using MTAoarsFamily.Services.Interfaces;
using MTAoarsFamily.Services.Operations;
using MTAoarsFamily.Services.Shared;
using MTAoarsFamily.Utilities.Config;
using MTAoarsFamily.Utilities.Constants;
using MTAoarsFamily.Utilities.Interfaces;
using MTAoarsFamily.Utilities.IoC;
using MTAoarsFamily.Utilities.Mvc;
using MTAoarsFamily.Validators;
using MTAoarsFamily.ViewModels.Shared;
using MTAoarsFamily.Repositories.Accounts;
using MTAoarsFamily.Services.Accounts;
using MTAoarsFamily.Repositories.Notifications;
using MTAoarsFamily.Services.Notifications;
using MTAoarsFamily.ViewModels.Operations;
using MTAoarsFamily.Validators.Operations.Fluents;
using MTAoarsFamily.Services.Reports;
using MTAoarsFamily.Builders.Interfaces;
using MTAoarsFamily.Builders.Operations;
using MTAoarsFamily.Builders.Notifications;
using MTAoarsFamily.Repositories.Maintenance;
using MTAoarsFamily.Services.Maintenance;
using MTAoarsFamily.Repositories.Administrative;
using MTAoarsFamily.Builders.Administrative;

namespace MTAoarsFamily.Services
{

    public static class ServiceLoader
    {

        public static void Load()
        {
            var container = ObjectContainer.Container;
            RegisterGlobalSettings(container);
            RegisterRepositories(container);
            RegisterServices(container);
            RegisterViewModel(container);
            RegisterMaps(container);
            RegisterValidators(container);
            RegisterBuilders(container);
            /*container.AddNewExtension<Interception>();

            container.Configure<Interception>()
               .SetInterceptorFor<ILookupRepository>(new InterfaceInterceptor());

            container.RegisterType<ILookupRepository, LookupRepository>(new InterceptionBehavior<PolicyInjectionBehavior>(),
            new Interceptor<TransparentProxyInterceptor>());*/
        }

        static void RegisterGlobalSettings(IUnityContainer container)
        {
            container.RegisterType<ConfigManager>(new ContainerControlledLifetimeManager());
            container.RegisterType<IObjectCreator, UnityObjectCreator>();
            container.RegisterType<IValidationProvider, ValidationProvider>(new ContainerControlledLifetimeManager());
            container.RegisterType<IScopeDataProvider, SessionScopeDataProvider>();
        }

        static void RegisterRepositories(IUnityContainer container)
        {
            var connectionString = container.Resolve<ConfigManager>().EntityConextConnectionString;
            container.RegisterType<EntityContext>(new InjectionConstructor(connectionString));
            //container.RegisterType<EntityContext>(new PerThreadLifetimeManager(), new InjectionConstructor(connectionString));
            container.RegisterType<IIbfimResultRepository, IbfimResultRepository>();
            container.RegisterType<IMiiResultRepository, MiiResultRepository>();
            container.RegisterType<ITbeExemptionRepository, TbeExemptionRepository>();
            container.RegisterType<ILookupRepository, LookupRepository>();
            container.RegisterType<IRegistrationUnitOfWork, RegistrationUnitOfWork>();
            container.RegisterType<IRunnerRepository, RunnerRepository>();
            container.RegisterType<IMemberRepository, MemberRepository>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IAgencyRepository, AgencyRepository>();
            container.RegisterType(typeof(IRepository<>), typeof(GenericRepository<>));
            container.RegisterType<IAgencyUnitOfWork, AgencyUnitOfWork>();
            container.RegisterType<ICompanyRepository, CompanyRepository>();
            container.RegisterType<IActivityRepository, ActivityRepository>();
            container.RegisterType<INotificationRepository, NotificationRepository>();
            container.RegisterType<IRenewalRepository, RenewalRepository>();
            container.RegisterType<IMailRepository, MailRepository>();
            container.RegisterType<IInvoiceRepository, InvoiceRepository>();
            container.RegisterType<IMenuRepository, MenuRepository>();
            container.RegisterType<ICPDRepository, CPDRepository>();
            container.RegisterType<ICBCRepository, CBCRepository>();
            container.RegisterType<IReferredRepository, ReferredRepository>();
            container.RegisterType<IConflictRepository, ConflictRepository>();
            container.RegisterType<IUploadRepository, UploadRepository>();
            container.RegisterType<IALCRepository, ALCRepository>();
            container.RegisterType<IPhotoRepository, PhotoRepository>();
            container.RegisterType<IReferredMemberRepository, ReferredMemberRepository>();
            container.RegisterType<IAdministrativeUnitOfWork, AdministrativeUnitOfWork>();
        }

        static void RegisterServices(IUnityContainer container)
        {
            container.RegisterType<IRegistrationService, RegistrationService>();
            container.RegisterType<ILookupService, LookupService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IAgencyService, AgencyService>();
            container.RegisterType<INotificaitonService, NotificationService>();
            container.RegisterType<IRenewalService, RenewalService>();
            container.RegisterType<ITerminationService, TerminationService>();
            container.RegisterType<IMailService, MailService>();
            container.RegisterType<IHomeService, HomeService>();
            container.RegisterType<IInvoiceService, InvoiceService>();
            container.RegisterType<IMenuService, MenuService>();
            container.RegisterType<IReportService, ReportService>();
            container.RegisterType<IActivityService, ActivityService>();
            container.RegisterType<ICPDService, CPDService>();
            container.RegisterType<ICBCService, CBCService>();
            container.RegisterType<IUploadService, UploadService>();
            container.RegisterType<IReferredService, ReferredService>();
            container.RegisterType<IConflictService, ConflictService>();
            container.RegisterType<IALCService, ALCService>();
            container.RegisterType<IAdministrativeService, AdministrativeService>();
        }

        static void RegisterBuilders(IUnityContainer container)
        {
            container.RegisterType<IAgencyBuilder, AgencyBuilder>();
            container.RegisterType<IRegistrationBuilder, RegistrationBuilder>();
            container.RegisterType<IRenewalBuilder, RenewalBuilder>();
            container.RegisterType<IMailBuilder, MailBuilder>();
            container.RegisterType<ITerminationBuilder, TerminationBuilder>();
            container.RegisterType<ICBCBuilder, CBCBuilder>();
            container.RegisterType<IReferredBuilder, ReferredBuilder>();
        }

        static void RegisterViewModel(IUnityContainer container)
        {
            container.RegisterType(typeof(List<>), new InjectionConstructor());
        }

        static void RegisterMaps(IUnityContainer container)
        {
            Mapper.Initialize(cnfg => {
                cnfg.ConstructServicesUsing(type => container.Resolve(type));
            });
            var assembly = Assembly.Load(new AssemblyName("MTAoarsFamily.Mappers"));
            var list = assembly.GetTypes()
                         .Where(type => type.GetInterfaces().Any(intr => intr == typeof(IMapper)));
            Parallel.ForEach(list,
                     item => (container.Resolve(item) as IMapper).Map()
                     );
        }

        static void RegisterValidators(IUnityContainer container)
        {
            var assembly = Assembly.Load(new AssemblyName("MTAoarsFamily.Validators"));
            var list = assembly.GetTypes()
                        .Where(type => type.GetCustomAttributes(typeof(FluentRegisterableAttribute), true).Any());
            Parallel.ForEach(list,
                item => {
                    var genericArgument = item.BaseType.GetGenericArguments().First();
                    var sourceType = typeof(FluentValidation.IValidator<>);
                    container.RegisterType(sourceType.MakeGenericType(genericArgument), item);
                });

            container.RegisterType<FluentValidation.IValidator<CorporateRegistrationViewModel>, AgencyBoardMembersValidator<CorporateRegistrationViewModel>>();
        }
    }// class

}// namespace
