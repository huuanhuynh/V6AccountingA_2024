using System.Web.Mvc;
using V6Soft.Web.Common.Application;
using V6Soft.Web.Common.Dependencies;
using DataAccessLayer.Implementations;
using DataAccessLayer.Implementations.Invoices;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Interfaces.Invoices;

namespace V6Soft.WebApi.Accounting.Application
{
    public class V6ApiAccountingEngine : V6AppEngineBase
    {
        public V6ApiAccountingEngine(string serviceInstanceId)
            : base(serviceInstanceId, "V6 Accounting Web API")
        {
            InstanceId = serviceInstanceId;
        }

        public void RegisterDependencies()
        {
            Logger.ExecuteWithFullLogging(() =>
            {
                RegisterLoginServices();
                RegisterMenuServices();
                RegisterCategoriesServices();
                RegisterBusinessServices();
                RegisterInvoiceServices();
                RegisterInvoice81Services();
            }, "V6ApiAccountingEngine RegisterDependencies");
        }

        public void RegisterInvoice81Services()
        {
            Logger.ExecuteWithFullLogging(() =>
            {
                DependencyInjector.RegisterType<IInvoice81Services, Invoice81Services>();
            }, "V6ApiAccountingEngine RegisterInvoice81Services");
        }

        public void RegisterInvoiceServices()
        {
            Logger.ExecuteWithFullLogging(() =>
            {
                DependencyInjector.RegisterType<IInvoiceServices, InvoiceServices>();
            }, "V6ApiAccountingEngine RegisterInvoiceServices");
        }

        public void RegisterBusinessServices()
        {
            Logger.ExecuteWithFullLogging(() =>
            {
                DependencyInjector.RegisterType<IBusinessServices, BusinessServices>();
            }, "V6ApiAccountingEngine RegisterBusinessServices");
        }

        public void RegisterCategoriesServices()
        {
            Logger.ExecuteWithFullLogging(() =>
            {
                DependencyInjector.RegisterType<ICategoriesServices, CategoriesServices>();
            }, "V6ApiAccountingEngine RegisterCategoriesServices");
        }

        public void RegisterMenuServices()
        {
            Logger.ExecuteWithFullLogging(() =>
            {
                DependencyInjector.RegisterType<IMenuServices, MenuServices>();
            }, "V6ApiAccountingEngine RegisterMenuServices");
        }

        public void RegisterLoginServices()
        {
            Logger.ExecuteWithFullLogging(() =>
            {
                DependencyInjector.RegisterType<ILoginServices, LoginServices>();
            }, "V6ApiAccountingEngine RegisterLoginServices");
        }

        protected override void OnStart(string[] args)
        {
            Logger.LogTraceEntry("V6ApiAccountingEngine OnStart");
            base.OnStart(args);
            DependencyResolver.SetResolver(new MvcDependencyResolver(DependencyInjector));

            Logger.LogTraceExit("V6ApiAccountingEngine OnStart");
        }
    }
}