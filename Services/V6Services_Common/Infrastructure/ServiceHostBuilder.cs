using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Security;

using V6Soft.Common.DeInWcf;
using V6Soft.Common.ExceptionHandling;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils.DependencyInjection;


namespace V6Soft.Services.Common.Infrastructure
{
    /// <summary>
    /// This class allows for configuring of a service host with default settings and overriding these settings when necessary.
    /// </summary>
    public class ServiceHostBuilder
    {
        /// <summary>
        /// Flag to indicate if metadata exchange (mex) endpoints is enabled 
        /// </summary>
        public static bool EnableMexEndpoints = true;

        public static ILogger Logger;

        /// <summary>
        /// Returns a <see cref="V6Soft.Common.DeInWcf.DeInServiceHost"/> instance with default settings suitable 
        /// for this service context.
        /// </summary>
        public static DeInServiceHost Build<TIService, TService>(IDependencyInjector container, string name, string uri)
        {
            var serviceHost = new DeInServiceHost(container, typeof(TService), new Uri(uri));

            SetCredentials(serviceHost);
            SetAuthorizationSettings(serviceHost);
            //TODO: Should allow en/disable certificate in configuration.
            //SetCertificate(serviceHost);
            SetThrottlingBehavior(serviceHost);
            SetBindingAndSecuritySettings<TIService>(serviceHost, uri);
            SetServiceHostMexEndpoints(serviceHost);

            return serviceHost;
        }

        private static void SetServiceHostMexEndpoints(ServiceHost serviceHost)
        {
            LogTraceEntry("Setup Mex Endpoints");

            if (EnableMexEndpoints)
            {
                ServiceMetadataBehavior smb = serviceHost.Description.Behaviors.Find<ServiceMetadataBehavior>();
                if (smb == null)
                {
                    smb = new ServiceMetadataBehavior();
                }
                smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                serviceHost.Description.Behaviors.Add(smb);
                serviceHost.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexTcpBinding(), "mex");
            }

            LogTraceExit("Setup Mex Endpoints");
        }

        private static void SetBindingAndSecuritySettings<TIService>(ServiceHost serviceHost, string uri)
        {
            LogTraceEntry("Setup tcp binding and security");

            NetTcpBinding tcpBinding = new NetTcpBinding();
            //tcpBinding.Security.Mode = SecurityMode.Message;
            //tcpBinding.Security.Message.ClientCredentialType = MessageCredentialType.UserName;

            //TODO: Should use a type of security mode.
            tcpBinding.Security.Mode = SecurityMode.None;
            tcpBinding.Security.Transport.ClientCredentialType = TcpClientCredentialType.None;

            int quotaSize = 1000 * 1024; // m_ReaderQuotasSizeK * BytesPerKilobyte;
            tcpBinding.MaxReceivedMessageSize = quotaSize;
            tcpBinding.MaxBufferSize = quotaSize;
            tcpBinding.ReaderQuotas.MaxArrayLength = quotaSize;
            tcpBinding.ReaderQuotas.MaxBytesPerRead = quotaSize;
            tcpBinding.ReaderQuotas.MaxStringContentLength = quotaSize;

            serviceHost.AddServiceEndpoint(typeof(TIService), tcpBinding, uri);

            LogTraceExit("Setup tcp binding and security");
        }

        private static void SetThrottlingBehavior(ServiceHost serviceHost)
        {
            LogTraceEntry("Set throttling behavior");

            var throttlingBehavior = new ServiceThrottlingBehavior();
            throttlingBehavior.MaxConcurrentCalls = 100;
            throttlingBehavior.MaxConcurrentInstances = Int32.MaxValue;
            throttlingBehavior.MaxConcurrentSessions = 5000;

            serviceHost.Description.Behaviors.Add(throttlingBehavior);

            LogTraceExit("Set throttling behavior");
        }

        private static void SetCertificate(ServiceHost serviceHost)
        {
            LogTraceEntry("Set Host Certificate");

            try
            {
                // Try to get certificate from certificate store
                serviceHost.Credentials.ServiceCertificate.SetCertificate(StoreLocation.LocalMachine,
                    StoreName.My, X509FindType.FindBySubjectName, System.Net.Dns.GetHostName());
            }
            catch (InvalidOperationException)
            {
                throw new V6Exception(
                    "Certificate for server not found in certificate store. Searching for subject with name '" + System.Net.Dns.GetHostName() + "' in Local machine store location, My/Personal store",
                    ExceptionType.Technical, ExceptionSeverity.Critical);
            }

            LogTraceExit("Set Host Certificate");
        }

        private static void SetAuthorizationSettings(ServiceHost serviceHost)
        {
            LogTraceEntry("Set Service Host Authorization");

            var authorizationPolicies = new ReadOnlyCollection<IAuthorizationPolicy>(
                new List<IAuthorizationPolicy>() { new V6UserRolesPolicy() }
            );
            serviceHost.Authorization.ExternalAuthorizationPolicies = authorizationPolicies;
            serviceHost.Authorization.PrincipalPermissionMode = PrincipalPermissionMode.Custom;
            serviceHost.Authorization.ServiceAuthorizationManager = new V6ServiceAuthorizationManager();

            LogTraceExit("Set Service Host Authorization");
        }

        private static void SetCredentials(ServiceHost serviceHost)
        {
            LogTraceEntry("Set Service Host Credentials");

            var authenticationSettings = serviceHost.Credentials.UserNameAuthentication;
            authenticationSettings.IncludeWindowsGroups = false;
            authenticationSettings.UserNamePasswordValidationMode = UserNamePasswordValidationMode.Custom;
            authenticationSettings.CustomUserNamePasswordValidator = new V6UserValidator();

            LogTraceExit("Set Service Host Credentials");
        }

        private static void LogTraceEntry(string message)
        {
            if (Logger != null)
            {
                Logger.LogTraceEntry(message);
            }
        }

        private static void LogTraceExit(string message)
        {
            if (Logger != null)
            {
                Logger.LogTraceExit(message);
            }
        }
    }
}
