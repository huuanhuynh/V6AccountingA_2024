using System.Configuration;

using V6Soft.Common.ModelFactory;
using V6Soft.Common.ModelFactory.Factories;
using V6Soft.Common.ModelFactory.Managers;
using V6Soft.Common.ModelFactory.Validators;
using V6Soft.Services.Assistant.Constants;
using V6Soft.Services.Assistant.DataBakers;
using V6Soft.Services.Assistant.DataFarmers;
using V6Soft.Services.Assistant.Interfaces;
using V6Soft.Services.Assistant.ServiceImpl;
using V6Soft.Services.Wcf.Common;


namespace V6Soft.Services.Assistant 
{
    public class AssistantWindowsService : V6WindowsServiceBase 
    {
        public AssistantWindowsService()
            : base(null, ServiceConfig.AssistantServiceName)
        {
        }

        protected override void OnServiceStart()
        {
            Logger.LogTraceEntry("OnServiceStart");
            string connString = ConfigurationManager.ConnectionStrings["V6Accounting"].ConnectionString;
            
            RegisterDependencies(connString);
            SetupRuntimeFactory(connString);

            Logger.LogTrace("Setup service host for IAssistantService");
            SetupServiceHost<IAssistantService, AssistantService>(
                ServiceConfig.AssistantServiceName,
                ServiceConfig.AssistantServiceHostUri);
            Logger.LogTraceExit("OnServiceStart");
        }

        protected override void OnServiceStop() { }

        private void RegisterDependencies(string connString)
        {
            Logger.ExecuteWithFullLogging(() =>
            {
                DependencyInjector.RegisterType<IAssistantService, AssistantService>();
                DependencyInjector.RegisterType<IAssistantDataBaker, AssistantDataBaker>();
                DependencyInjector.RegisterType<IAssistantDataFarmer, AssistantDataFarmer>(connString);
                
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
