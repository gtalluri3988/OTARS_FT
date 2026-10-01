using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.Utilities.Constants;
using MTAoarsFamily.Utilities.Mvc;
using MTAoarsFamily.Services;
using MTAoarsFamily.Utilities.IoC;
using Microsoft.Practices.Unity;
using MTAoarsFamily.Services.Operations;
using MTAoarsFamily.Services.Interfaces;
using MTAoarsFamily.Utilities.Interfaces;


namespace MTAoarsFamily.Schedulers
{
    class Program
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            try
            {
                ServiceLoader.Load();

                IConflictService conflictService;
                ObjectContainer.Container.RegisterType<IScopeDataProvider, SchedulerDataProvider>();

                var dataProvider = ObjectContainer.Container.Resolve<IScopeDataProvider>();
                var identity = (ObjectContainer.Container.Resolve<IUserService>()).Get("almano@ism.net.my");
                dataProvider.Register(GlobalConstants.CurrentIdentity, identity);

                if (args.Length < 1)
                    throw new Exception("Wrong number of arguments. One argument is required");

                switch (args[0])
                {
                    case LookupConstants.Schedulers.GenerateInvoice:
                        var invoiceService = ObjectContainer.Container.Resolve<IInvoiceService>();
                        if (args.Length == 3)
                        {
                            var year = int.Parse(args[1]);
                            var month = int.Parse(args[2]);
                            invoiceService.Generate(year, month);
                        }
                        else
                        {
                            invoiceService.Generate();
                        }
                        break;
                    case LookupConstants.Schedulers.GenerateRenewal:
                        var generateRenewalService = ObjectContainer.Container.Resolve<IRenewalService>();
                        generateRenewalService.GenerateRenewableRecords();
                        break;
                    case LookupConstants.Schedulers.RenewalReminder:
                        var reminderRenewalService = ObjectContainer.Container.Resolve<IRenewalService>();
                        reminderRenewalService.SendReminders();
                        break;
                    case LookupConstants.Schedulers.ProcessRenewal:
                        var processRenewalService = ObjectContainer.Container.Resolve<IRenewalService>();
                        processRenewalService.Process();
                        break;
                    case LookupConstants.Schedulers.ProcessCPD:
                        var cpdService = ObjectContainer.Container.Resolve<ICPDService>();
                        cpdService.Process();
                        break;
                    case LookupConstants.Schedulers.ProcessCBC:
                        var cbcService = ObjectContainer.Container.Resolve<ICBCService>();
                        cbcService.Process();
                        break;
                    case LookupConstants.Schedulers.SendFirstConflictReminder:
                        conflictService = ObjectContainer.Container.Resolve<IConflictService>();
                        conflictService.SendFirstReminders();
                        break;
                    case LookupConstants.Schedulers.SendSecondConflictReminder:
                        conflictService = ObjectContainer.Container.Resolve<IConflictService>();
                        conflictService.SendSecondReminders();
                        break;
                    case LookupConstants.Schedulers.DefaultOpenConflictCases:
                        conflictService = ObjectContainer.Container.Resolve<IConflictService>();
                        conflictService.DefaultOpenCases();
                        break;
                    case LookupConstants.Schedulers.AutoCloseConflictCases:
                        conflictService = ObjectContainer.Container.Resolve<IConflictService>();
                        conflictService.AutoClose();
                        break;
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex);
            }
        }

    }// class
}// namespace

