using System.Configuration;

using V6Soft.Common.ModelFactory.Factories;
using V6Soft.Common.ModelFactory.Managers;
using V6Soft.Common.ModelFactory.Validators;
using V6Soft.Services.Accounting.Constants;
using V6Soft.Services.Accounting.DataBakers;
using V6Soft.Services.Accounting.DataFarmers;
using V6Soft.Services.Accounting.Interfaces;
using V6Soft.Services.Accounting.ServiceImpl;
using V6Soft.Services.Wcf.Common;


namespace V6Soft.Services.Accounting
{
    public class AccountingWindowsService : V6WindowsServiceBase
    {
        public AccountingWindowsService()
            : base(null, ServiceConfig.CustomerServiceName)
        {
        }

        protected override void OnServiceStart()
        {
            Logger.LogTraceEntry("OnServiceStart");
            string connString = ConfigurationManager.ConnectionStrings["V6Accounting"].ConnectionString;
            
            RegisterDependencies(connString);
            SetupRuntimeFactory(connString);

            Logger.LogTrace("Setup service host for ICustomerService");
            SetupServiceHost<ICustomerService, CustomerService>(
                ServiceConfig.CustomerServiceName,
                ServiceConfig.CustomerServiceHostUri);

            Logger.LogTrace("Setup service host for IDefinitionService");
            SetupServiceHost<IDefinitionService, DefinitionService>(
                ServiceConfig.DefinitionServiceName,
                ServiceConfig.DefinitionServiceHostUri);

            Logger.LogTrace("Setup service host for IMenuService");
            SetupServiceHost<IMenuService, MenuService>(
                ServiceConfig.MenuServiceName,
                ServiceConfig.MenuServiceHostUri);

            Logger.LogTrace("Setup service host for IPaymentService");
            SetupServiceHost<IPaymentMethodService, PaymentMethodService>(
                ServiceConfig.PaymentMethodServiceName,
                ServiceConfig.PaymentMethodServiceHostUri);

            Logger.LogTraceExit("OnServiceStart");
        }

        protected override void OnServiceStop() { }

        private void RegisterDependencies(string connString)
        {
            Logger.ExecuteWithFullLogging(() =>
            {
                DependencyInjector.RegisterType<ICustomerService, CustomerService>();
                DependencyInjector.RegisterType<ICustomerDataBaker, CustomerDataBaker>();
                DependencyInjector.RegisterType<ICustomerDataFarmer, CustomerDataFarmer>(connString);

                DependencyInjector.RegisterType<IDefinitionService, DefinitionService>();
                DependencyInjector.RegisterType<IDefinitionDataBaker, DefinitionDataBaker>();

                DependencyInjector.RegisterType<IMenuService, MenuService>();
                DependencyInjector.RegisterType<IMenuDataBaker, MenuDataBaker>();
                DependencyInjector.RegisterType<IMenuDataFarmer, MenuDataFarmer>(connString);

                DependencyInjector.RegisterType<IPaymentMethodService, PaymentMethodService>();
                DependencyInjector.RegisterType<IPaymentMethodDataBaker, PaymentMethodDataBaker>();
                DependencyInjector.RegisterType<IPaymentMethodDataFarmer, PaymentMethodFarmer>(connString);


            }, "RegisterDependencies");            
        }

        private void SetupRuntimeFactory(string connString)
        {
            Logger.ExecuteWithFullLogging(() =>
            {
                IModelDefinitionManager loader = CreateLoader(connString);
                DependencyInjector.RegisterInstance<IModelDefinitionManager>(loader);
                DynamicModelFactory.DefinitionLoader = loader;
                DynamicModelFactory.DynamicModelValidator = new DefaultModelValidator();
            }, "SetupRuntimeFactory");
        }

        private IModelDefinitionManager CreateLoader(string connString)
        {
            var loader = new DbModelDefinitionManager(connString);
            loader.LoadAll();
            return loader;
        }
    }
}
